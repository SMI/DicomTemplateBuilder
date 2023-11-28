using System;
using System.Collections.Generic;
using System.Linq;
using FellowOakDicom;

namespace Repopulator
{

    public enum ColumnRole
    {
        None = 0,

        /// <summary>
        /// The column that contains the alleged location of dcm files on disk
        /// </summary>
        FilePath,

        /// <summary>
        /// The column should be a top level subfolder under which to create files e.g. PatientID
        /// </summary>
        SubFolder
    }

    public class CsvToDicomColumn
    {
        public string Name { get; }
        public int Index { get; }
        public HashSet<DicomTag> TagsToPopulate { get; }

        public ColumnRole Role { get; }

        public CsvToDicomColumn(string colName, int index, ColumnRole role,params DicomTag[] mappedTags)
        {
            //cannot be DicomTag AND FilePath
            if (mappedTags != null && mappedTags.Any() && role == ColumnRole.FilePath)
                throw new ArgumentException("Column has ambiguous role, it should either provide dicom tag substitutions or be the file path column not both");

            //if you don't have DicomTags you must have some other role
            if ((mappedTags == null || !mappedTags.Any())&& role == ColumnRole.None)
                throw new ArgumentException("Column has no clear role, it should either provide dicom tag substitutions or be the file path column");

            if (index < 0)
                throw new ArgumentException("index cannot be negative");

            if (mappedTags != null)
            {
                var sq = mappedTags.FirstOrDefault(static t => t.DictionaryEntry.ValueRepresentations.Contains(DicomVR.SQ));
                if(sq != null)
                    throw new ArgumentException($"Sequence tags are not supported ({sq.DictionaryEntry.Keyword})");
            }


            Name = colName;
            Index = index;
            TagsToPopulate = new HashSet<DicomTag>(mappedTags?? Array.Empty<DicomTag>());
            Role = role;
        }
    }
}