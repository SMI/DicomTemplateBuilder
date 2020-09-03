using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Dicom;
using Repopulator.Matchers;

namespace Repopulator
{
    public class CsvToDicomTagMapping
    {
        /// <summary>
        /// The column of the CSV which records the file path to the image(s) being processed.  These should be expressed
        /// relatively (i.e. not absolute path names)
        /// </summary>
        public CsvToDicomColumn FilenameColumn;


        /// <summary>
        /// The column of the CSV which records.  The column should be a top level subfolder under which to create files e.g. PatientID
        /// </summary>
        public CsvToDicomColumn SubFolderColumn;
        
        /// <summary>
        /// Columns which contain dicom tag values.  This is not all the columns in the CSV.  It does not include <see cref="FilenameColumn"/>
        /// or any columns which could not be mapped;
        /// </summary>
        public List<CsvToDicomColumn> TagColumns = new List<CsvToDicomColumn>();

        /// <summary>
        /// The file that was read during <see cref="BuildMap"/>
        /// </summary>
        public FileInfo CsvFile { get; private set; }

        /// <summary>
        /// Clears current column mappings
        /// </summary>
        public void Clear()
        {
            FilenameColumn = null;
            CsvFile = null;
            TagColumns.Clear();
            IsBuilt = false;
        }

        /// <summary>
        /// True if the class has been built yet
        /// </summary>
        public bool IsBuilt { get; private set; }

        /// <summary>
        /// Reads the headers from the CSV specified in <paramref name="options"/> and builds <see cref="TagColumns"/> and <see cref="FilenameColumn"/>.
        /// Returns true if the headers constitute a valid set (at least 1 and <see cref="FilenameColumn"/> found).
        /// </summary>
        /// <param name="options"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool BuildMap(DicomRepopulatorOptions options, out string log)
        {
            Clear();

            StringBuilder sb  = new StringBuilder();
            
            //how we will tie CSV rows to files
            IRepopulatorMatcher matcher = null;

            try
            {
                var extraMappings = GetExtraMappings(options);

                CsvFile = options.CsvFileInfo;

                using (var reader = new CsvReader(CsvFile.OpenText(), System.Globalization.CultureInfo.CurrentCulture))
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
                            var match = GetKeyDicomTagAndColumnName(options, header, index,extraMappings);

                            if (match != null)
                            {
                                switch (match.Role)
                                {
                                    case ColumnRole.FilePath when FilenameColumn != null:
                                        throw new Exception("There are 2+ FilenameColumn in the CSV");
                                    case ColumnRole.FilePath:
                                        FilenameColumn = match;
                                        break;
                                    case ColumnRole.SubFolder when SubFolderColumn != null:
                                        throw new Exception("There are 2+ SubFolderColumn in the CSV");
                                    case ColumnRole.SubFolder:
                                        SubFolderColumn = match;
                                        break;
                                }

                                if(TagColumns.Any(c=>c.TagsToPopulate.Intersect(match.TagsToPopulate).Any()))
                                    throw new Exception($"There are 2+ columns that both populate for one of the DicomTag(s) '{string.Join(",",match.TagsToPopulate)}'");

                                TagColumns.Add(match);
                            
                                
                                sb.AppendLine($"Validated header '{header}'");
                            }
                            else
                                sb.AppendLine($"Could not determine tag for '{header}'");
                        }
                    }

                    sb.AppendLine($"Found {TagColumns.Count} valid mappings");
                    sb.AppendLine($"FilenameColumn is: {FilenameColumn?.Name ?? "Not Set"}");
                }

                IsBuilt = true;
                
                var matcherFactory = new MatcherFactory();
                using(matcher = matcherFactory.Create(this,options))
                    sb.AppendLine($"Matching Strategy is: { matcher?.ToString() ?? "No Strategy Found"}");
            }
            catch (Exception e)
            {
                sb.AppendLine(e.ToString());
                log = sb.ToString();
                return false;
            }
            
            log = sb.ToString();


            return TagColumns.Count > 0 && matcher != null;
        }

        

        private Dictionary<string, HashSet<DicomTag>> GetExtraMappings(DicomRepopulatorOptions state)
        {
            Dictionary<string, HashSet<DicomTag>> toReturn = new Dictionary<string, HashSet<DicomTag>>(StringComparer.CurrentCultureIgnoreCase);

            if(string.IsNullOrWhiteSpace(state.InputExtraMappings))
                return null;

            var extraMappingsFile = state.ExtraMappings;

            int lineNumber = 0;
            foreach (string[] pair in File.ReadAllLines(extraMappingsFile.FullName).Select(l => l.Split(new []{':'},StringSplitOptions.RemoveEmptyEntries)))
            {
                lineNumber++;
                
                //ignore blank lines
                if(pair.Length == 0)
                    continue;

                if(pair.Length != 2)
                    throw new Exception($"Bad line in extra mappings file (line number {lineNumber}).  Line did not match expected format 'ColumnName:TagName'");


                var found = DicomDictionary.Default.SingleOrDefault(entry => string.Equals(entry.Keyword ,pair[1],StringComparison.CurrentCultureIgnoreCase));

                if (found == null)
                    throw new Exception(
                        $"Bad tag '{pair[1]}' on line number {lineNumber} of ExtraMappings file '{extraMappingsFile.FullName}'. It is not a valid DicomTag name");

                if(!toReturn.ContainsKey(pair[0]))
                    toReturn.Add(pair[0],new HashSet<DicomTag>());

                toReturn[pair[0]].Add(found.Tag);
            }

            return toReturn;
        }

        /// <summary>
        /// Creates a mapping between a single CSV file column and one or more <see cref="DicomTag"/>
        /// </summary>
        public CsvToDicomColumn GetKeyDicomTagAndColumnName(DicomRepopulatorOptions state, string columnName,int index,Dictionary<string,HashSet<DicomTag>> extraMappings)
        {
            CsvToDicomColumn toReturn = null;
            if(columnName.Equals(state.FileNameColumn,StringComparison.CurrentCultureIgnoreCase))
                toReturn = new CsvToDicomColumn(columnName,index,ColumnRole.FilePath);
            
            if(columnName.Equals(state.SubFolderColumn, StringComparison.CurrentCultureIgnoreCase))
                toReturn = new CsvToDicomColumn(columnName,index,ColumnRole.SubFolder);

            var found = DicomDictionary.Default.SingleOrDefault(entry => string.Equals(entry.Keyword ,columnName,StringComparison.CurrentCultureIgnoreCase));

            if(found != null)
                if (toReturn == null)
                    toReturn = new CsvToDicomColumn(columnName,index,ColumnRole.None,found.Tag); 
                else
                    toReturn.TagsToPopulate.Add(found.Tag); //it's a file path AND a tag! ok...
                

            if (extraMappings != null && extraMappings.ContainsKey(columnName))
                if(toReturn == null)
                    toReturn = new CsvToDicomColumn(columnName,index,ColumnRole.None,extraMappings[columnName].ToArray());
                else
                    toReturn.TagsToPopulate.UnionWith(extraMappings[columnName]);

            return toReturn;
        }
    }
}
