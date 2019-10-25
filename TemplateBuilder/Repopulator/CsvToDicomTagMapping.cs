using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using Dicom;
using DicomTypeTranslation;

namespace TemplateBuilder.Repopulator
{
    class CsvToDicomTagMapping
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

        /// <summary>
        /// The column of the CSV which records the file path to the image(s) being processed.  These should be expressed
        /// relatively (i.e. not absolute path names)
        /// </summary>
        public CsvToDicomColumn FilenameColumn;
        
        /// <summary>
        /// Columns which contain dicom tag values.  This is not all the columns in the CSV.  It does not include <see cref="FilenameColumn"/>
        /// or any columns which could not be mapped;
        /// </summary>
        public List<CsvToDicomColumn> TagColumns = new List<CsvToDicomColumn>();

        /// <summary>
        /// Clears current column mappings
        /// </summary>
        public void Clear()
        {
            FilenameColumn = null;
            TagColumns.Clear();
        }

        /// <summary>
        /// Reads the headers from the CSV specified in <paramref name="state"/> and builds <see cref="TagColumns"/> and <see cref="FilenameColumn"/>.
        /// Returns true if the headers constitute a valid set (at least 1 and <see cref="FilenameColumn"/> found).
        /// </summary>
        /// <param name="state"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool BuildMap(RepopulatorUIState state, out string log)
        {
            Clear();

            StringBuilder sb  = new StringBuilder();
            try
            {
                using (var reader = new CsvReader(state.CsvFileInfo.OpenText()))
                {
                    reader.Configuration.TrimOptions = TrimOptions.Trim;
                    reader.Read();
                    var couldReadHeader = reader.ReadHeader();

                    sb.AppendLine("Could Read Header:" + couldReadHeader);

                    if (couldReadHeader)
                    {
                        for (var index = 0; index < reader.Context.HeaderRecord.Length; index++)
                        {
                            var header = reader.Context.HeaderRecord[index];
                            var match = GetKeyDicomTagAndColumnName(state, header, index);

                            if (match != null)
                            {
                                if(match.IsFilePath)
                                    if (FilenameColumn != null)
                                        throw new Exception("There are 2+ FilenameColumn in the CSV");
                                    else
                                        FilenameColumn = match;
                                else
                                {
                                    if(TagColumns.Any(c=>c.MappedTag == match.MappedTag))
                                        throw new Exception($"There are 2+ columns that both contain DicomTag '{match.MappedTag}'");

                                    TagColumns.Add(match);
                                }
                                
                                sb.AppendLine($"Validated header ''{header}''");
                            }
                            else
                                sb.AppendLine($"Could not determine tag for '{header}'");
                        }
                    }

                    sb.AppendLine($"Found {TagColumns.Count} valid mappings");
                    sb.AppendLine($"FilenameColumn is: {FilenameColumn?.Name ?? "Not Set"}");
                }
            }
            catch (Exception e)
            {
                sb.AppendLine(e.ToString());
                log = sb.ToString();
                return false;
            }

            log = sb.ToString();
            return TagColumns.Count > 0 && FilenameColumn != null;
        }

        /// <summary>
        /// Get the key DicomTag and associated CSV column name.
        /// </summary>
        public CsvToDicomColumn GetKeyDicomTagAndColumnName(RepopulatorUIState state, string columnName,int index)
        {
            if(columnName.Equals(state.FileNameColumn,StringComparison.CurrentCultureIgnoreCase))
                return new CsvToDicomColumn(columnName,index,null,true);

            string[] split = columnName.Split(':');
            if (split.Length == 1)
            {
                var found = DicomDictionary.Default.SingleOrDefault(entry => string.Equals(entry.Keyword ,columnName,StringComparison.CurrentCultureIgnoreCase));
                return found != null ? new CsvToDicomColumn(columnName,index,found.Tag,false) : null;
            }
            
            if(split.Length != 2 || split[0].Length == 0 || split[1].Length == 0)
                return null;
            
            string dicomTagString = split[1];

            // Check the DICOM tag is a valid DICOM tag
            var found2 =
                DicomDictionary.Default.SingleOrDefault(entry =>  string.Equals(entry.Keyword ,dicomTagString,StringComparison.CurrentCultureIgnoreCase));

            return found2 != null ? new CsvToDicomColumn(columnName, index, found2.Tag, false) : null;
        }
    }
}
