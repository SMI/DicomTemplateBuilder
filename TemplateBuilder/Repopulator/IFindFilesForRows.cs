using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace TemplateBuilder.Repopulator
{
    public interface IRepopulator
    {
        /// <summary>
        /// Repopulates the given Dicom <paramref name="file"/> with the headers stored in the current <paramref name="row"/>
        /// using the indexes in the <paramref name="map"/> provided.
        /// </summary>
        /// <param name="file"></param>
        void Repopulate(FileInfo file, CsvToDicomTagMapping map, string[] row);
    }


    /// <summary>
    /// Interface for a strategy in which files on disk are enumerated and 
    /// </summary>
    public interface IFilesToRows : IRepopulator
    {
        void Initialize(CsvReader reader, CsvToDicomTagMapping mapping);
    }

    /// <summary>
    /// Interface for a strategy in which the Csv file rows are streamed one at a time and file(s)
    /// are returned that match the row.
    /// </summary>
    public interface IRowsToFiles:IRepopulator
    {
        string[] GetPathsFor(CsvReader reader, CsvToDicomTagMapping mapping);
    }
}
