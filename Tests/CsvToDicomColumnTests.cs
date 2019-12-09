using System;
using Dicom;
using NUnit.Framework;
using Repopulator;
using Repopulator.Matchers;

namespace Tests
{
    public class CsvToDicomColumnTests
    {
        [Test]
        public void NegativeIndex()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", -1, true));
            StringAssert.Contains(ex.Message,"index cannot be negative");
        }

        [Test]
        public void NoClearRole()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false));
            StringAssert.Contains("no clear role",ex.Message);
        }

        [Test]
        public void TooManyRoles()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, true,DicomTag.ALineRate));
            StringAssert.Contains("has ambiguous role",ex.Message);
        }

        [Test]
        public void SequenceTags()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false,DicomTag.AbstractPriorCodeSequence));
            StringAssert.Contains("Sequence tags are not supported (AbstractPriorCodeSequence)",ex.Message);
        }
    }
}
