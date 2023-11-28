﻿using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Repopulator.Matchers
{
    public class FilePathMatcher: RepopulatorMatcher
    {
        public CsvReader Reader { get;}

        public FilePathMatcher(CsvToDicomTagMapping map, DicomRepopulatorOptions options):base(map,options)
        {
            if(map.FilenameColumn == null)
                throw new ArgumentException("Map did not contain file name column");

            Reader = new CsvReader(map.CsvFile.OpenText(),
                new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture)
                    { TrimOptions = TrimOptions.Trim });
            Reader.Read();
            Reader.ReadHeader();
        }

        public override RepopulatorJob Next()
        {
            if (!Reader.Read())
                return null;

            var fi = new FileInfo(Path.Combine(Options.InputFolder, Reader[Map.FilenameColumn.Index]));

            return new RepopulatorJob(Map,fi,null,Reader.Parser.Record);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Reader.Dispose();
        }
    }
}
