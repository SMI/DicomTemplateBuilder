using System;

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

        public abstract RepopulatorJob Next();
        public abstract void Dispose();
    }
}