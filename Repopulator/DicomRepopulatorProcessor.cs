using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dicom;
using DicomTypeTranslation.Helpers;
using NLog;
using NLog.Config;
using NLog.Targets;
using Repopulator.Matchers;
using Repopulator.TagUpdaters;

namespace Repopulator
{
    public class DicomRepopulatorProcessor: IDisposable
    {
        private readonly ILogger _logger;

        private Stopwatch _stopwatch;

        private ParallelOptions _parallelOptions;
        private DicomAnonymizer _anonymizer;

        private ITagUpdater _tagUpdater;

        public IRepopulatorMatcher Matcher { get; private set; }

        public MemoryTarget MemoryLogTarget { get; } = new MemoryTarget(){Name = "DicomRepopulatorProcessor_Memory"};

        private int _nInput;
        private int _nDone;
        private int _nErrors;

        /// <summary>
        /// The number of images found in the input directory (optionally a recursive scan)
        /// </summary>
        public int Input => _nInput;

        public int Done => _nDone;
        public int Errors => _nErrors;

        public DicomRepopulatorProcessor(string currentDirectory = null)
        {
            string log = Path.Combine(currentDirectory ?? Environment.CurrentDirectory, "NLog.config");

            if(File.Exists(log))
                LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(log, false);
            else
                LogManager.Configuration = new LoggingConfiguration();
            
            MemoryLogTarget.Layout = "${level} ${message}";
            SimpleConfigurator.ConfigureForTargetLogging(MemoryLogTarget,LogLevel.Trace);

            _logger = LogManager.GetCurrentClassLogger();
            
            if (!DicomDatasetHelpers.CorrectFoDicomVersion())
                throw new ApplicationException("Incorrect fo-dicom version for the current platform");
        }
        
        public int Process(DicomRepopulatorOptions options)
        {
            _parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Math.Max(1,options.NumThreads) };
            _tagUpdater = new ParseStringsUpdater(options.Culture);
            _anonymizer = options.Anonymise ? new DicomAnonymizer() : null;

            if(!options.OutputDirectoryInfo.Exists)
                options.OutputDirectoryInfo.Create();
            
            _logger.Debug("Checking output directory for contents");
            if (options.OutputDirectoryInfo.EnumerateFileSystemInfos().Any())
            {
                _logger.Error("Output directory " + options.OutputDirectoryInfo.FullName + " is not empty");
                return -1;
            }
            
            _logger.Info("Starting to process");
            _stopwatch = Stopwatch.StartNew();
            
            //build map from the CSV headers
            var map = new CsvToDicomTagMapping();
            try
            {
                if(!map.BuildMap(options,out string log))
                    throw new Exception("Failed to build map:" + log);
                
                _logger.Info("Map built succesfully:" + log);
            }
            catch (ApplicationException e)
            {
                _logger.Error("Exception processing csv file: " + e.Message);
                return -1;
            }

            var csvFile = options.CsvFileInfo;
            _logger.Info("Starting " + csvFile.FullName);

            var factory = new MatcherFactory();

            using (Matcher = factory.Create(map, options))
            {
                if (Matcher == null)
                    throw new Exception("No suitable IRepopulatorMatcher could be built, ensure you have either file paths or instance UIDs in your csv file / extra mappings (otherwise we have no way to match rows to files)");

                _nInput = Matcher.GetInputFileCount();

                RepopulatorJob job = null;

                //while there are more jobs
                do
                {
                    try
                    {
                        job = Matcher.Next();
                    
                        if(job != null)
                            ProcessJob(job, options);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        Interlocked.Increment(ref _nErrors);
                    }
                } while (job != null && _nErrors < options.ErrorThreshold);

                if(_nErrors >= options.ErrorThreshold)
                    throw new Exception("Error threshold reached");
            }
            
            var sb = new StringBuilder();
            sb.AppendLine("\n=== Finished processing ===");
            sb.AppendLine("Total input files: " + _nInput);
            sb.AppendLine("Total failed: " + _nErrors);
            sb.Append("Total processed: " + _nDone);

            _logger.Info(sb.ToString());

            _logger.Info("Duration = " + _stopwatch.ElapsedMilliseconds / 1000.0 + "s");

            return 0;
        }
        
        
            
        private void ProcessJob(RepopulatorJob job, DicomRepopulatorOptions options)
        {
            if(options.Anonymise)
                _anonymizer.AnonymizeInPlace(job.File.Dataset);

            _tagUpdater.UpdateTags(job);
            
            //the relative location in the archive
            var inputRelativePath =
            job.File.File.Name.Replace(options.DirectoryToProcessInfo.FullName, "").TrimStart(Path.DirectorySeparatorChar);

            _logger.Debug("Saving output file");

            // Preserves any sub-directory structures
            var outPath = job.Map.SubFolderColumn != null
                ? Path.Combine(options.OutputDirectoryInfo.FullName, job.Cells[job.Map.SubFolderColumn.Index],inputRelativePath)
                : Path.Combine(options.OutputDirectoryInfo.FullName, inputRelativePath);

            _logger.Debug("Output file path = " + outPath.ToString());

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            job.File.Save(outPath);

            Interlocked.Increment(ref _nDone);

        }

        public void Dispose()
        {
            LogManager.Configuration.RemoveTarget(MemoryLogTarget.Name);
            MemoryLogTarget?.Dispose();
        }
    }
}
