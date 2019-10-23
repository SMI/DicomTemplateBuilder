using System.IO;
using System.Windows.Input;

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

        public DirectoryInfo OutputDirectoryInfo => new DirectoryInfo(OutputFolder);
        public DirectoryInfo DirectoryToProcessInfo => new DirectoryInfo(InputFolder);

        public FileInfo CsvFileInfo => new FileInfo(InputCsv);
    }
}