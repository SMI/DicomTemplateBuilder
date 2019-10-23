using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Dicom;
using DicomTypeTranslation.Helpers;
using NLog;

namespace TemplateBuilder.Repopulator
{
    public class DicomRepopulatorProcessor
    {
        private readonly ILogger _logger;

        private int _nInput;
        private int _nMatched;
        private int _nProcessed;

        private Stopwatch _stopwatch;

        private ParallelOptions _parallelOptions;


        public DicomRepopulatorProcessor(string currentDirectory = null)
        {
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(Path.Combine(currentDirectory ?? Environment.CurrentDirectory, "Microservices.NLog.config"), false);
            _logger = LogManager.GetCurrentClassLogger();

            if (!DicomDatasetHelpers.CorrectFoDicomVersion())
                throw new ApplicationException("Incorrect fo-dicom version for the current platform");
        }


        public int Process(RepopulatorUIState options)
        {
            _parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = options.NumThreads };

            _logger.Debug("CLI options:\n" + options);

            _logger.Debug("Checking output directory for contents");
            if (options.OutputDirectoryInfo.EnumerateFileSystemInfos().Any())
            {
                _logger.Error("Output directory " + options.OutputDirectoryInfo.FullName + " is not empty");
                return -1;
            }

            Dictionary<DicomTag, int> keyDicomTagToColumnIndexMapping;
            Dictionary<string, DicomDataset> replacementDict;

            _logger.Info("Starting to process");
            _stopwatch = Stopwatch.StartNew();
            
            var map = new CsvToDicomTagMapping();

            try
            {
                if(!map.BuildMap(options,out _))
                    throw new Exception("Failed to build map");
            }
            catch (ApplicationException e)
            {
                _logger.Error("Exception processing csv file: " + e.Message);
                return -1;
            }

            _logger.Info("Loaded " + map.Map.Count + " rows from " + options.CsvFileInfo.FullName);

            try
            {
                ProcessDicomFiles(options, map.Map);
            }
            catch (Exception e)
            {
                _logger.Error("Exception processing dicom files: " + e.Message);
                return -1;
            }

            var sb = new StringBuilder();
            sb.AppendLine("\n=== Finished processing ===");
            sb.AppendLine("Total input files: " + _nInput);
            sb.AppendLine("Total matched to input data: " + _nMatched);
            sb.Append("Total processed: " + _nProcessed);

            _logger.Info(sb.ToString());

            _logger.Info("Duration = " + _stopwatch.ElapsedMilliseconds / 1000.0 + "s");

            return 0;
        }

        
        /// <summary>
        /// Processes the Dicom files and alters the values according to the replacement dictionary.
        /// Makes an alternate copy of each Dicom file into the output directory.
        /// </summary>
        /// <param name="options">Options as specified on the command line.</param>
        /// contains new values for the tags to be altered.</param>
        private void ProcessDicomFiles(
            RepopulatorUIState options,
            Dictionary<string, DicomTag> map)
        {
            _logger.Info("Starting directory scan of " + options.DirectoryToProcessInfo.FullName);

            var dirStack = new Stack<DirectoryInfo>();
            dirStack.Push(options.DirectoryToProcessInfo);

            while (dirStack.Count > 0)
            {
                DirectoryInfo dir = dirStack.Pop();
                _logger.Info("Processing directory " + dir.FullName);

                if (!dir.Exists)
                    throw new ApplicationException("A previously seen directory can no longer be found: " + dir);

                /*Parallel.ForEach(
                    dir.EnumerateFiles("*.dcm"),
                    _parallelOptions,
                    currentFile => ProcessDicomFile(currentFile, keyDicomTagToColumnIndexMapping, replacementDict, options));*/

                DirectoryInfo[] subDirs = dir.GetDirectories();
                for (int i = subDirs.Length - 1; i >= 0; i--)
                {
                    _logger.Debug("Found subdirectory " + subDirs[i].FullName);
                    dirStack.Push(subDirs[i]);

                    string relativeDir = subDirs[i].FullName.Replace(options.DirectoryToProcessInfo.FullName + Path.DirectorySeparatorChar, "");
                    Directory.CreateDirectory(Path.Combine(options.OutputDirectoryInfo.FullName, relativeDir));
                }
            }
        }

        /// <summary>
        /// Processes a single Dicom file applying the required alterations as specified in the replacement dictionary.
        /// </summary>
        /// <param name="dFilePath">File to be processed.</param>
        /// <param name="keyDicomTagToColumnIndexMapping">DicomTag specifying the key and the corresponding column index in the CSV file.</param>
        /// <param name="replacementDict">Replacement dictionary that maps Series Ids to a Dicom data set that contains new values for the
        /// tags to be altered.</param>
        /// <param name="options">Command line options.</param>
        private void ProcessDicomFile(
            FileSystemInfo dFilePath,
            Dictionary<DicomTag, int> keyDicomTagToColumnIndexMapping,
            Dictionary<string, DicomDataset> replacementDict,
            RepopulatorUIState options)
        {
            _logger.Debug("Processing file " + dFilePath.FullName);

            Interlocked.Increment(ref _nInput);

            DicomFile dFile = DicomFile.Open(dFilePath.FullName);
            string inputRelativePath = dFilePath.FullName.Replace(options.DirectoryToProcessInfo.FullName, "").TrimStart(Path.DirectorySeparatorChar);

            string key;

            try
            {
                DicomTag keyDicomTag = keyDicomTagToColumnIndexMapping.Keys.First();
                string keyTagName = keyDicomTag.DictionaryEntry.Keyword;

                key = dFile.Dataset.GetValue<string>(keyDicomTag, 0);

                if (!replacementDict.ContainsKey(key))
                {
                    _logger.Warn("No replacement data loaded for file " + inputRelativePath + " with " + keyTagName + "=" + key);
                    return;
                }

                _logger.Debug("Matched file " + keyTagName + "=" + key + " with row from csv data");
                Interlocked.Increment(ref _nMatched);
            }
            catch (DicomDataException e)
            {
                _logger.Error("Exception reading data from file " + inputRelativePath + e.Message);
                return;
            }

            _logger.Debug("Updating file dataset");
            dFile.Dataset.AddOrUpdate(replacementDict[key]);

            _logger.Debug("Saving output file");

            // Preserves any sub-directory structures
            dFile.Save(Path.Combine(options.OutputDirectoryInfo.FullName, inputRelativePath));

            Interlocked.Increment(ref _nProcessed);
        }
    }
}
