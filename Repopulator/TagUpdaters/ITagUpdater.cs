using System;
using System.Collections.Generic;
using System.Text;

namespace Repopulator.TagUpdaters
{
    /// <summary>
    /// Strategy pattern interface for classes that update <see cref="Dicom.DicomTag"/> in a <see cref="Dicom.DicomDataset"/>
    /// with raw string values (handles translation from string to dicom types).
    /// </summary>
    interface ITagUpdater
    {
        void UpdateTags(RepopulatorJob job);
    }
}
