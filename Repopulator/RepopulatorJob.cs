using System.Collections.Generic;
using System.IO;
using Dicom;
using Repopulator.Matchers;

namespace Repopulator
{
    public class RepopulatorJob
    {
        public CsvToDicomTagMapping Map { get; }
        public DicomFile File { get; }
        public string[] Cells { get; set; }
        public RepopulatorJob( CsvToDicomTagMapping map,DicomFile file, string[] cells)
        {
            File = file;
            Map = map;
            Cells = cells;
        }
    }
}