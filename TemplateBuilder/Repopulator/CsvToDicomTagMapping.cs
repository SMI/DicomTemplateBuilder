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
        public Dictionary<string,DicomTag> Map = new Dictionary<string, DicomTag>(StringComparer.CurrentCultureIgnoreCase);

        public bool BuildMap(RepopulatorUIState state, out string log)
        {
            Map.Clear();

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
                        foreach (var header in reader.Context.HeaderRecord)
                        {
                            if (GetKeyDicomTagAndColumnName(header, out Tuple<string, DicomTag> match))
                            {
                                Map.Add(match.Item1,match.Item2);
                                sb.AppendLine($"Validated header ''{header}''");
                            }
                            else
                                sb.AppendLine($"Could not determine tag for '{header}'");
                        }
                    }

                    sb.AppendLine($"Found {Map.Count} valid mappings");
                }
            }
            catch (Exception e)
            {
                sb.AppendLine(e.ToString());
                log = sb.ToString();
                return false;
            }

            log = sb.ToString();
            return Map.Count > 0;
        }

        /// <summary>
        /// Get the key DicomTag and associated CSV column name.
        /// </summary>
        public static bool GetKeyDicomTagAndColumnName(string columnName, out Tuple<string,DicomTag> match)
        {
            string[] split = columnName.Split(':');
            if (split.Length == 1)
            {
                var found = DicomDictionary.Default.SingleOrDefault(entry => string.Equals(entry.Keyword ,columnName,StringComparison.CurrentCultureIgnoreCase));

                if (found != null)
                {
                    match = Tuple.Create(columnName, found.Tag);
                    return true;
                }

                match = default;
                return false;
            }
            
            if(split.Length != 2 || split[0].Length == 0 || split[1].Length == 0)
            {
                match = default;
                return false;
            }
            string dicomTagString = split[1];

            // Check the DICOM tag is a valid DICOM tag
            DicomDictionaryEntry prasedDictEntry =
                DicomDictionary.Default.SingleOrDefault(entry =>  string.Equals(entry.Keyword ,dicomTagString,StringComparison.CurrentCultureIgnoreCase));

            if (prasedDictEntry == null)
            {
                match = default;
                return false;
            }

            match = Tuple.Create(columnName, prasedDictEntry.Tag);
            return true;
        }
    }
}
