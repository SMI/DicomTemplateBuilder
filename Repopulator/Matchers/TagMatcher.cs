using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using FellowOakDicom;

namespace Repopulator.Matchers
{
    /// <summary>
    /// Matcher that reads the entire CSV file and builds a map of UIDs to rows at a granularity of either sop, series or study
    /// </summary>
    internal class TagMatcher : RepopulatorMatcher
    {
        private readonly string[] _fileList;
        private int _currentFile;

        private DicomTag _indexerTag;

        /// <summary>
        /// Map of all the InstanceUIDs described in the CSV and the row values on that CSV line
        /// </summary>
        readonly Dictionary<string,string[]> _indexerToRowMap = new();

        public TagMatcher(CsvToDicomTagMapping map, DicomRepopulatorOptions options):base(map,options)
        {
            _fileList = GetFileList().ToArray();

            var indexer = GetBestIndexer(map) ?? throw new ArgumentException("No valid indexer could be found, there must be a column in the map for either SOP, Series or Study instance UIDs");
            var conf = new CsvConfiguration(System.Globalization.CultureInfo.CurrentCulture)
            {
                TrimOptions=TrimOptions.Trim
            };
            using var reader = new CsvReader(map.CsvFile.OpenText(), conf);
            while (reader.Read())
            {
                var key = reader[indexer.Index];

                if(_indexerToRowMap.ContainsKey(key))
                    throw new Exception($"Multiple Csv rows describe the same '{_indexerTag}' '{key}'.  Error was on CSV line number '{reader.Parser.RawRow}'");

                _indexerToRowMap.Add(key,reader.Parser.Record.ToArray());
            }
        }

        private CsvToDicomColumn GetBestIndexer(CsvToDicomTagMapping map)
        {
            return
                GetIndexer(map,DicomTag.SOPInstanceUID)??
                GetIndexer(map,DicomTag.SeriesInstanceUID)??
                GetIndexer(map,DicomTag.StudyInstanceUID);
        }

        private CsvToDicomColumn GetIndexer(CsvToDicomTagMapping map, DicomTag tag)
        {
            var match = map.TagColumns.FirstOrDefault(c => c.TagsToPopulate.Contains(tag));

            if (match == null)
                return null;

            _indexerTag = tag;

            return match;
        }

        public override RepopulatorJob Next()
        {
            //we have run out of files to process
            if (_currentFile >= _fileList.Length)
                return null;

            try
            {
                var currentFile = new FileInfo(_fileList[_currentFile]);
                var df = DicomFile.Open(currentFile.FullName);
                var seek = df.Dataset.GetValue<string>(_indexerTag, 0);

                if(!_indexerToRowMap.TryGetValue(seek,out var row))
                    throw new Exception($"Csv did not contain a value for {_indexerTag} {seek} which was found in file '{_fileList[_currentFile]}'");

                return new RepopulatorJob(Map,currentFile,df,row);
            }
            finally
            {
                _currentFile++;
            }
        }

        public override int GetInputFileCount()
        {
            return _fileList.Length;
        }

        public override void Dispose()
        {

        }
    }
}