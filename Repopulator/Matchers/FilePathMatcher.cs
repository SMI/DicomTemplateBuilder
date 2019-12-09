using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Dicom;

namespace Repopulator.Matchers
{
    public class FilePathMatcher: RepopulatorMatcher
    {
        public CsvReader Reader { get;}

        public FilePathMatcher(CsvToDicomTagMapping map, DicomRepopulatorOptions options):base(map,options)
        {
            if(map.FilenameColumn == null)
                throw new ArgumentException("Map did not contain file name column");
            
            Reader = new CsvReader(map.CsvFile.OpenText());
            Reader.Read();
            Reader.ReadHeader();
            Reader.Configuration.TrimOptions = TrimOptions.Trim; 
        }
        
        public override RepopulatorJob Next()
        {
            if (!Reader.Read())
                return null;

            var fi = new FileInfo(Path.Combine(Options.InputFolder, Reader[Map.FilenameColumn.Index]));

            return new RepopulatorJob(Map,DicomFile.Open(fi.FullName),Reader.Context.Record);
        }

        public override void Dispose()
        {
            Reader.Dispose();
        }
    }
}
