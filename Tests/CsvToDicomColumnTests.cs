using System;
using FellowOakDicom;
using NUnit.Framework;
using Repopulator;

namespace Tests;

public class CsvToDicomColumnTests
{
    [Test]
    public void NegativeIndex()
    {
        var ex = Assert.Throws<ArgumentException>(static () => _=new CsvToDicomColumn("fish", -1, ColumnRole.FilePath));
        Assert.That(ex?.Message, Does.Contain("index cannot be negative"));
    }

    [Test]
    public void NoClearRole()
    {
        var ex = Assert.Throws<ArgumentException>(static () => _=new CsvToDicomColumn("fish", 0, ColumnRole.None));
        Assert.That(ex?.Message, Does.Contain("no clear role"));
    }

    [Test]
    public void TooManyRoles()
    {
        var ex = Assert.Throws<ArgumentException>(static () => _=new CsvToDicomColumn("fish", 0, ColumnRole.FilePath,DicomTag.ALineRate));
        Assert.That(ex?.Message, Does.Contain("has ambiguous role"));
    }

    [Test]
    public void SequenceTags()
    {
        var ex = Assert.Throws<ArgumentException>(static () => _=new CsvToDicomColumn("fish", 0, ColumnRole.None,DicomTag.AbstractPriorCodeSequence));
        Assert.That(ex?.Message, Does.Contain("Sequence tags are not supported (AbstractPriorCodeSequence)"));
    }
}