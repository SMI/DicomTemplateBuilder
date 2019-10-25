using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace TemplateBuilder.Repopulator
{
    public interface IFindFilesForRows
    {
        string[] GetPathsFor(CsvReader reader, CsvToDicomTagMapping mapping);
    }
}
