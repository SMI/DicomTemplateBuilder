using System.Globalization;
using FellowOakDicom;
using DicomTypeTranslation;
using TypeGuesser;

namespace Repopulator.TagUpdaters
{
    public abstract class TagUpdater : ITagUpdater
    {
        public virtual void UpdateTags(RepopulatorJob job)
        {
            foreach (var col in job.Map.TagColumns)
                foreach (DicomTag dicomTag in col.TagsToPopulate)
                    UpdateTag(dicomTag, job.File.Dataset, job.Cells[col.Index]);
        }

        protected abstract void UpdateTag(DicomTag dicomTag, DicomDataset dataset, string cellValue);
    }

    /// <summary>
    /// Updater which uses FAnsi <see cref="TypeDeciderFactory"/> to translate the string value into the c sharp
    /// type (e.g. DateTime) before writing to dicom.  Types that do not map to FAnsi or that fail parsing are written
    /// as raw string values
    /// </summary>
    class ParseStringsUpdater : TagUpdater
    {
        private TypeDeciderFactory _factory;

        public ParseStringsUpdater(CultureInfo culture)
        {
            _factory = new(culture);
        }

        protected override void UpdateTag(DicomTag dicomTag, DicomDataset dataset, string cellValue)
        {
            var cSharpType = DicomTypeTranslater.GetNaturalTypeForVr(dicomTag.DictionaryEntry.ValueRepresentations,
                dicomTag.DictionaryEntry.ValueMultiplicity)?.CSharpType;

            object writeValue = cellValue;

            //if it's a supported type e.g. DateTime parse it
            if (cSharpType != null && _factory.IsSupported(cSharpType))
                if (_factory.Dictionary[cSharpType].IsAcceptableAsType(cellValue, Ignore.Me ))
                    writeValue = _factory.Dictionary[cSharpType].Parse(cellValue);

            dataset.Remove(dicomTag);
            DicomTypeTranslaterWriter.SetDicomTag(dataset,dicomTag,writeValue);
        }
        private class Ignore : IDataTypeSize
        {
            private static IDataTypeSize _instance = new Ignore();

            public DecimalSize Size { get; set; } = new();
            public int? Width { get; set; }
            public bool Unicode { get; set; }
            public static IDataTypeSize Me => _instance;
        }
    }


    /// <summary>
    /// Updater which calls <see cref="DicomDataset.AddOrUpdate(Dicom.DicomItem[])"/> directly with the string value.  This
    /// can result in successful writes that break on read attempts in downstream components
    /// </summary>
    class NaiveUpdater : TagUpdater
    {
        protected override void UpdateTag(DicomTag dicomTag, DicomDataset dataset, string cellValue)
        {
            dataset.AddOrUpdate(dicomTag, cellValue);
        }
    }
}