using System.IO;
using System.Windows.Input;
using YamlDotNet.Serialization;

namespace TemplateBuilder.Repopulator
{
    public class RepopulatorUIState
    {
        public string InputFolder;
        public string InputCsv;
        public string OutputFolder;
        public int NumThreads;
        public bool IncludeSubdirectories;
        public string Pattern;

        [YamlIgnore]
        public DirectoryInfo OutputDirectoryInfo => new DirectoryInfo(OutputFolder);
        
        [YamlIgnore]
        public DirectoryInfo DirectoryToProcessInfo => new DirectoryInfo(InputFolder);
        
        [YamlIgnore]
        public FileInfo CsvFileInfo => new FileInfo(InputCsv);
    }
}