using System;
using Dicom;

namespace TemplateBuilder.Repopulator
{
    public class CsvToDicomColumn
    {
        public string Name { get; }
        public int Index { get; }
        public DicomTag MappedTag { get; }
        public bool IsFilePath { get; set; }

        public CsvToDicomColumn(string colName, int index, DicomTag mappedTag, bool isFileColumn)
        {
            if (mappedTag != null && isFileColumn)
                throw new ArgumentException("Column should either be a dicom tag or a file path column not both");

            if (mappedTag == null && !isFileColumn)
                throw new ArgumentException("Column must either contain dicom tags or be a file path column");

            Name = colName;
            Index = index;
            MappedTag = mappedTag;
            IsFilePath = isFileColumn;
        }
    }
}