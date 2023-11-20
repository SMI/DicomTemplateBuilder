using System.Globalization;
using System.IO;
using DicomTypeTranslation.TableCreation;
using YamlDotNet.Serialization;

namespace Repopulator
{
    public class DicomRepopulatorOptions
    {
        public const string DefaultPattern = "*.dcm";
        public const string DefaultFileNameColumn = ImagingTableCreation.RelativeFileArchiveURI;

        public string InputFolder;
        public string InputCsv;
        public string InputExtraMappings;
        public string OutputFolder;
        public int NumThreads;
        public bool IncludeSubdirectories = true;
        public string Pattern = DefaultPattern;
        public string FileNameColumn = DefaultFileNameColumn;
        public bool Anonymise;
        public int ErrorThreshold = 100;
        public string CultureName;

        [YamlIgnore]
        public CultureInfo Culture = CultureInfo.CurrentCulture;

        public string SubFolderColumn;

        /// <summary>
        /// True to delete input files as output files are generated
        /// </summary>
        public bool DeleteAsYouGo;

        [YamlIgnore]
        public DirectoryInfo OutputDirectoryInfo => new(OutputFolder);

        [YamlIgnore]
        public DirectoryInfo DirectoryToProcessInfo => new(InputFolder);

        [YamlIgnore]
        public FileInfo CsvFileInfo => new(InputCsv);

        [YamlIgnore]
        public FileInfo ExtraMappings => new(InputExtraMappings);


    }
}