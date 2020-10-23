using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dicom;
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

        public MemoryTarget MemoryLogTarget { get; } = new MemoryTarget {Name = "DicomRepopulatorProcessor_Memory"};

        private int _nInput;
        private int _nDone;
        private int _nErrors;
        private LoggingRule _loggingRule;

        /// <summary>
        /// The number of images found in the input directory (optionally a recursive scan)
        /// </summary>
        public int Input => _nInput;

        public int Done => _nDone;
        public int Errors => _nErrors;

        public DicomRepopulatorProcessor()
        {
            MemoryLogTarget.Layout = "${level} ${message}";

            if(LogManager.Configuration == null)
                SimpleConfigurator.ConfigureForTargetLogging(MemoryLogTarget,LogLevel.Trace);
            else
            {
                // specify what gets logged to the above target
                _loggingRule = new LoggingRule("*", LogLevel.Debug, MemoryLogTarget);

                // add target and rule to configuration
                LogManager.Configuration.AddTarget(MemoryLogTarget.Name, MemoryLogTarget);
                LogManager.Configuration.LoggingRules.Add(_loggingRule);
                LogManager.ReconfigExistingLoggers();
            }

            _logger = LogManager.GetCurrentClassLogger();
        }
        
        public int Process(DicomRepopulatorOptions options, CancellationToken token)
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
                    
                    if(token.IsCancellationRequested)
                    {
                        _logger.Info("User cancelled execution");
                        return -2;
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

            Directory.CreateDirectory(Path.GetDirectoryName(outPath));

            job.File.Save(outPath);

            Interlocked.Increment(ref _nDone);

            if(options.DeleteAsYouGo)
                job.FileInfo.Delete();

        }

        public void Dispose()
        {
            LogManager.Configuration.RemoveTarget(MemoryLogTarget.Name);
            LogManager.Configuration.LoggingRules.Remove(_loggingRule);
            MemoryLogTarget?.Dispose();
        }
    }
}
