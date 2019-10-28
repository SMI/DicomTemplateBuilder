using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repopulator.Matchers
{
    public abstract class RepopulatorMatcher : IRepopulatorMatcher, IDisposable
    {
        protected DicomRepopulatorOptions Options { get; }
        protected CsvToDicomTagMapping Map { get; }
        public RepopulatorMatcher(CsvToDicomTagMapping map, DicomRepopulatorOptions options)
        {
            Options = options;
            Map = map;

            if(!Map.IsBuilt)
                throw new ArgumentException("Map has not been built yet");

            if(string.IsNullOrWhiteSpace(options.InputFolder))
                throw new ArgumentException("InputFolder has not been set");

        }

        protected IEnumerable<string> GetFileList()
        {
            return Directory.GetFiles(Options.InputFolder, Options.Pattern,
                Options.IncludeSubdirectories? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        public abstract RepopulatorJob Next();

        public virtual int GetInputFileCount()
        {
            return GetFileList().Count();
        }

        public abstract void Dispose();
    }
}