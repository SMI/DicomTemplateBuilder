using System;
using System.Collections.Generic;
using System.Linq;
using Dicom;

namespace Repopulator
{
    public class CsvToDicomColumn
    {
        public string Name { get; }
        public int Index { get; }
        public HashSet<DicomTag> TagsToPopulate { get; }
        public bool IsFilePath { get; set; }


        public CsvToDicomColumn(string colName, int index, bool isFileColumn,params DicomTag[] mappedTags)
        {
            if (mappedTags != null && mappedTags.Any() && isFileColumn)
                throw new ArgumentException("Column has ambiguous role, it should either provide dicom tag substitutions or be the file path column not both");

            if ((mappedTags == null || !mappedTags.Any())&& !isFileColumn)
                throw new ArgumentException("Column has no clear role, it should either provide dicom tag substitutions or be the file path column");

            if (index < 0)
                throw new ArgumentException("index cannot be negative");

            if (mappedTags != null)
            {
                var sq = mappedTags.FirstOrDefault(t => t.DictionaryEntry.ValueRepresentations.Contains(DicomVR.SQ)); 
                if(sq != null)
                    throw new ArgumentException($"Sequence tags are not supported ({sq.DictionaryEntry.Keyword})");
            }


            Name = colName;
            Index = index;
            TagsToPopulate = new HashSet<DicomTag>(mappedTags?? new DicomTag[0]);
            IsFilePath = isFileColumn;
        }
    }
}