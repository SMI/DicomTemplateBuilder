using System.Linq;
using FellowOakDicom;

namespace Repopulator.Matchers
{
    public class MatcherFactory
    {
        public IRepopulatorMatcher Create(CsvToDicomTagMapping map, DicomRepopulatorOptions options)
        {
            //We have a column that contains the exact location on disk of the image.  This is the best because it lets us stream the CSV
            if(map.FilenameColumn != null)
                return new FilePathMatcher(map,options);

            //We have no file path column so must do matching based on 'key'.  This will require loading entire CSV into memory which may break
            //for very large datasets
            if (map.TagColumns.Any(c =>
                c.TagsToPopulate.Contains(DicomTag.SOPInstanceUID) ||
                c.TagsToPopulate.Contains(DicomTag.SeriesInstanceUID) ||
                c.TagsToPopulate.Contains(DicomTag.StudyInstanceUID)))
                return new TagMatcher(map, options);

            return null;
        }
    }
}