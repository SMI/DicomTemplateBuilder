using System;
using FellowOakDicom;
using NUnit.Framework;
using Repopulator;

namespace Tests
{
    public class CsvToDicomColumnTests
    {
        [Test]
        public void NegativeIndex()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", -1, ColumnRole.FilePath));
            StringAssert.Contains(ex.Message,"index cannot be negative");
        }

        [Test]
        public void NoClearRole()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, ColumnRole.None));
            StringAssert.Contains("no clear role",ex.Message);
        }

        [Test]
        public void TooManyRoles()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, ColumnRole.FilePath,DicomTag.ALineRate));
            StringAssert.Contains("has ambiguous role",ex.Message);
        }

        [Test]
        public void SequenceTags()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CsvToDicomColumn("fish", 0, ColumnRole.None,DicomTag.AbstractPriorCodeSequence));
            StringAssert.Contains("Sequence tags are not supported (AbstractPriorCodeSequence)",ex.Message);
        }
    }
}
