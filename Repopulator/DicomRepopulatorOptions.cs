using System.IO;
using DicomTypeTranslation.TableCreation;
using YamlDotNet.Serialization;

namespace Repopulator
{
    public class DicomRepopulatorOptions
    {
        public string InputFolder;
        public string InputCsv;
        public string InputExtraMappings;
        public string OutputFolder;
        public int NumThreads;
        public bool IncludeSubdirectories = true;
        public string Pattern = "*.dcm";
        public string FileNameColumn = ImagingTableCreation.RelativeFileArchiveURI;
        public bool Anonymise;
        public int ErrorThreshold = 100;

        [YamlIgnore]
        public DirectoryInfo OutputDirectoryInfo => new DirectoryInfo(OutputFolder);
        
        [YamlIgnore]
        public DirectoryInfo DirectoryToProcessInfo => new DirectoryInfo(InputFolder);
        
        [YamlIgnore]
        public FileInfo CsvFileInfo => new FileInfo(InputCsv);

        [YamlIgnore]
        public FileInfo ExtraMappings => new FileInfo(InputExtraMappings);
        
    }
}