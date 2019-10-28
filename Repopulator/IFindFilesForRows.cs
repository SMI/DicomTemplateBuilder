using System.IO;

namespace Repopulator
{
    /// <summary>
    /// Interface for classes that match Csv rows to files on disk.  This could be as simple as following a file URI or as
    /// complicated as reading a UID returning the corresponding files
    /// </summary>
    public interface IRepopulatorMatcher
    {
        /// <summary>
        /// Returns the next file to be repopulated and the corresponding values to overwrite with.  Returns
        /// null if there are no more files/rows to process
        /// </summary>
        RepopulatorJob Next();
    }

    public class FilePathMatcher
    {
        protected DicomRepopulatorOptions Args { get; }
        protected CsvToDicomTagMapping Map { get; }

        public FilePathMatcher(CsvToDicomTagMapping map, DicomRepopulatorOptions args)
        {
            Args = args;
            Map = map;
        }
    }

    public class RepopulatorJob
    {
        public CsvToDicomTagMapping Map { get; }
        public FileInfo File { get; }
        public string[] Cells { get; set; }
        public RepopulatorJob( CsvToDicomTagMapping map,FileInfo file, string[] cells)
        {
            File = file;
            Map = map;
            Cells = cells;
        }
    }
}
