using System;
using Dicom;
using NUnit.Framework;
using Repopulator;

namespace Tests
{
    public class CsvToDicomColumnTests
    {
        [Test]
        public void NegativeIndex()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", -1, true));
            StringAssert.Contains("index cannot be negative",ex.Message);
        }

        [Test]
        public void NoClearRole()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false));
            StringAssert.Contains(ex.Message,"no clear role");
        }

        [Test]
        public void TooManyRoles()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, true,DicomTag.ALineRate));
            StringAssert.Contains(ex.Message,"has ambiguous role");
        }

        [Test]
        public void SequenceTags()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false,DicomTag.AbstractPriorCodeSequence));
            StringAssert.Contains(ex.Message,"Sequence tags are not supported (AbstractPriorCodeSequence)");
        }
    }
}
