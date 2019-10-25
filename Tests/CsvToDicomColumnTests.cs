using System;
using Dicom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateBuilder.Repopulator;

namespace Tests
{
    [TestClass]
    public class CsvToDicomColumnTests
    {
        [TestMethod]
        public void NegativeIndex()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new CsvToDicomColumn("fish", -1, true));
            StringAssert.Contains("index cannot be negative",ex.Message);
        }

        [TestMethod]
        public void NoClearRole()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false));
            StringAssert.Contains(ex.Message,"no clear role");
        }

        [TestMethod]
        public void TooManyRoles()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new CsvToDicomColumn("fish", 0, true,DicomTag.ALineRate));
            StringAssert.Contains(ex.Message,"has ambiguous role");
        }

        [TestMethod]
        public void SequenceTags()
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => new CsvToDicomColumn("fish", 0, false,DicomTag.AbstractPriorCodeSequence));
            StringAssert.Contains(ex.Message,"Sequence tags are not supported (AbstractPriorCodeSequence)");
        }
    }
}
