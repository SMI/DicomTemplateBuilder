using System.Collections.Generic;
using System.IO;
using Dicom;
using Repopulator.Matchers;

namespace Repopulator
{
    public class RepopulatorJob
    {
        public CsvToDicomTagMapping Map { get; }
        
        /// <summary>
        /// Original file read during construction
        /// </summary>
        public FileInfo FileInfo { get; }
        public DicomFile File { get; }
        public string[] Cells { get; set; }

        /// <summary>
        /// Creates a new job to populated a given dicom file with the values in <paramref name="cells"/>
        /// </summary>
        /// <param name="map">Determines which <paramref name="cells"/> should be written into which tags in the dicom files</param>
        /// <param name="fileInfo">A file you want to repopulate</param>
        /// <param name="fileIfAlreadyOpen">Only provide if you have the DicomFile opened already otherwise send null</param>
        /// <param name="cells">Values to put into the file, must match the <paramref name="map"/></param>
        public RepopulatorJob( CsvToDicomTagMapping map,FileInfo fileInfo,DicomFile fileIfAlreadyOpen, string[] cells)
        {
            FileInfo = fileInfo;
            File = fileIfAlreadyOpen ?? DicomFile.Open(fileInfo.FullName);
            Map = map;
            Cells = cells;
        }
    }
}