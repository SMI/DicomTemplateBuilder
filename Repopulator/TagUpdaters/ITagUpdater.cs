namespace Repopulator.TagUpdaters
{
    /// <summary>
    /// Strategy pattern interface for classes that update <see cref="FellowOakDicom.DicomTag"/> in a <see cref="FellowOakDicom.DicomDataset"/>
    /// with raw string values (handles translation from string to dicom types).
    /// </summary>
    interface ITagUpdater
    {
        void UpdateTags(RepopulatorJob job);
    }
}
