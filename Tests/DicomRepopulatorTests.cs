using System;
using System.IO;
using System.Threading;
using FellowOakDicom;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Repopulator;

namespace Tests;

[TestFixture]
public class DicomRepopulatorTests
{
    private readonly string _inputFileBase = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestInput");
    private readonly string _outputFileBase = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestOutput");
    private readonly string _seriesFilesBase = Path.Combine(TestContext.CurrentContext.TestDirectory, "MultipleSeriesTest");

    private const string IM_0001_0013_NAME = "IM_0001_0013.dcm";
    private const string IM_0001_0019_NAME = "IM_0001_0019.dcm";

    [SetUp]
    public void SetUp()
    {
        Directory.CreateDirectory(_inputFileBase);
        Directory.CreateDirectory(_outputFileBase);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_inputFileBase)) Directory.Delete(_inputFileBase, true);
        if (Directory.Exists(_outputFileBase)) Directory.Delete(_outputFileBase, true);
        if (Directory.Exists(_seriesFilesBase)) Directory.Delete(_seriesFilesBase, true);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Test_AbsolutePath_InCsv(bool deleteAsYouGo)
    {
        //Create a dicom file in the input dir /subdir/IM_0001_0013.dcm
        var inputDicom = CreateInputFile(TestData.Img013,
            Path.Combine("subdir", nameof(TestData.Img013) +".dcm"));

        //Create a CSV with the full path to the image
        var inputCsv = CreateInputCsvFile(
            $"""
             File,PatientID
             {inputDicom.FullName},ABC
             """);

        //run repopulator
        var outDir = AssertRunsSuccesfully(1,
            0,
            inputCsv,
            null,
            inputDicom.Directory.Parent,
            o =>
            {
                o.FileNameColumn = "File";
                o.DeleteAsYouGo = deleteAsYouGo;
            }
        );

        //anonymous image should appear in the subdirectory of the out dir
        var expectedOutFile = new FileInfo(Path.Combine(outDir.FullName, "subdir", nameof(TestData.Img013) + ".dcm"));
        FileAssert.Exists(expectedOutFile);

        Assert.That(File.Exists(inputDicom.FullName), Is.EqualTo(!deleteAsYouGo));
    }

    [TestCase("./subdir/IM-0001-0013.dcm")]
    [TestCase("subdir/IM-0001-0013.dcm")]
    [TestCase("./../Test_RelativePath_InCsv/subdir/IM-0001-0013.dcm")]
    public void Test_RelativePath_InCsv(string csvPath)
    {
        //Create a dicom file in the input dir /subdir/IM_0001_0013.dcm
        var inputDicom = TestData.Create(new FileInfo(Path.Combine(TestContext.CurrentContext.WorkDirectory,nameof(Test_RelativePath_InCsv),"subdir", Path.GetFileName(TestData.Img013))),TestData.Img013);

        //Create a CSV with the full path to the image
        var inputCsv = CreateInputCsvFile(
            $@"File,PatientID
{csvPath},ABC");

        //run repopulator
        var outDir = AssertRunsSuccesfully(1,
            0,
            inputCsv,
            null,
            inputDicom.Directory?.Parent, static o => o.FileNameColumn = "File");

        //anonymous image should appear in the subdirectory of the out dir
        var expectedOutFile = new FileInfo(Path.Combine(outDir.FullName, "subdir", Path.GetFileName(TestData.Img013)));
        FileAssert.Exists(expectedOutFile);
    }

    [Test]
    public void Test_StudyDateTag_GoodData()
    {
        var inputDicom = CreateInputFile(TestData.Img013, "mydicom.dcm");
        var inputCsv = CreateInputCsvFile(
            $"""
             RelativeFileArchiveURI,PatientID,StudyDate
             {inputDicom.FullName},ABC123,2001-01-01
             """);

        var outDir = AssertRunsSuccesfully(1,0,inputCsv,null,inputDicom.Directory);

        var fi = new FileInfo (Path.Combine(outDir.FullName,"mydicom.dcm"));
        FileAssert.Exists(fi);

        var file = DicomFile.Open(fi.FullName);
        Assert.Multiple(() =>
        {
            Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("ABC123"));
            Assert.That(file.Dataset.GetValue<DateTime>(DicomTag.StudyDate, 0), Is.EqualTo(new DateTime(2001, 1, 1)));
        });

    }

    [Ignore("This really shouldn't be passing! how do we validate that 'Lolz' is not a valid StudyDate for inserting rather than blindly writting it in?")]
    [Test]
    public void Test_StudyDateTag_BadData()
    {
        var inputDicom = CreateInputFile(TestData.Img013, "mydicom.dcm");
        var inputCsv = CreateInputCsvFile(
            $@"RelativeFileArchiveURI,PatientID,StudyDate
{inputDicom.FullName},ABC123,Lolz");

        //should be an error because Lolz is not a valid date!
        var outDir = AssertRunsSuccesfully(0,1,inputCsv,null,inputDicom.Directory);

        var fi = new FileInfo (Path.Combine(outDir.FullName,"mydicom.dcm"));
        FileAssert.Exists(fi);

        var file = DicomFile.Open(fi.FullName);
        Assert.Multiple(() =>
        {
            Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("ABC123"));
            Assert.That(file.Dataset.GetValue<DateTime>(DicomTag.StudyDate, 0), Is.EqualTo(new DateTime(2001, 1, 1)));
        });

    }

    [Test]
    public void SingleFileBasicOperationTest()
    {
        var inFile = CreateInputFile(TestData.Img013,nameof(TestData.Img013) +".dcm");

        var outDir = AssertRunsSuccesfully(1, 0,null,

            //Treat Csv column "ID" as a replacement for PatientID
            CreateExtraMappingsFile("ID:PatientID"), inFile.Directory,

            //Give it BasicTest.csv
            static o => o.InputCsv= Path.Combine(TestContext.CurrentContext.TestDirectory, "BasicTest.csv"));

        //Anonymous dicom image should exist
        var expectedFile = new FileInfo(Path.Combine(outDir.FullName, nameof(TestData.Img013) + ".dcm"));
        FileAssert.Exists(expectedFile);

        //it should have the patient ID from the csv
        var file = DicomFile.Open(expectedFile.FullName);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void KeyNotFirstColumn(bool runSecondaryAnon)
    {
        var inputDirPath = Path.Combine(_inputFileBase, "KeyNotFirstColumn");

        Directory.CreateDirectory(inputDirPath);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0013_NAME)), TestData.Img013);

        var outputDirPath = Path.Combine(_outputFileBase, "KeyNotFirstColumn");
        var expectedFile = Path.Combine(outputDirPath, IM_0001_0013_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "KeyNotFirstColumn.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            Anonymise = runSecondaryAnon,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID","sopid:SOPInstanceUID" ).FullName,
            NumThreads = 4
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile), Is.True, $"Expected output file {expectedFile} to exist");
        });

        var file = DicomFile.Open(expectedFile);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
    }

    [Test]
    public void DateRepopulation()
    {
        var inputDirPath = Path.Combine(_inputFileBase, "DateRepopulation");

        Directory.CreateDirectory(inputDirPath);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0013_NAME)), TestData.Img013);

        var outputDirPath = Path.Combine(_outputFileBase, "DateRepopulation");
        var expectedFile = Path.Combine(outputDirPath, IM_0001_0013_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "WithDate.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID", "Date:StudyDate" ).FullName,
            NumThreads = 4
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile), Is.True, $"Expected output file {expectedFile} to exist");
        });

        var file = DicomFile.Open(expectedFile);

        Assert.Multiple(() =>
        {
            Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
            Assert.That(file.Dataset.GetValue<string>(DicomTag.StudyDate, 0), Is.EqualTo("20180601"));
        });
    }

    [Test]
    public void OneCsvColumnToMultipleDicomTags()
    {
        var inputDirPath = Path.Combine(_inputFileBase, "OneCsvColumnToMultipleDicomTags");

        Directory.CreateDirectory(inputDirPath);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0013_NAME)), TestData.Img013);

        var outputDirPath = Path.Combine(_outputFileBase, "OneCsvColumnToMultipleDicomTags");
        var expectedFile = Path.Combine(outputDirPath, IM_0001_0013_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "WithDate.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID", "Date:StudyDate", "Date:SeriesDate" ).FullName,
            NumThreads = 1
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile), Is.True, $"Expected output file {expectedFile} to exist");
        });

        var file = DicomFile.Open(expectedFile);
        Assert.Multiple(() =>
        {
            Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
            Assert.That(file.Dataset.GetValue<string>(DicomTag.StudyDate, 0), Is.EqualTo("20180601"));
            Assert.That(file.Dataset.GetValue<string>(DicomTag.SeriesDate, 0), Is.EqualTo("20180601"));
        });
    }

    [Test]
    public void SpacesInCsvHeaderTest()
    {
        var inputDirPath = Path.Combine(_inputFileBase, "SpacesInCsvHeaderTest");

        Directory.CreateDirectory(inputDirPath);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0013_NAME)), TestData.Img013);

        var outputDirPath = Path.Combine(_outputFileBase, "SpacesInCsvHeaderTest");
        var expectedFile = Path.Combine(outputDirPath, IM_0001_0013_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "SpacesInCsvHeaderTest.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
            NumThreads = 1
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile), Is.True, $"Expected output file {expectedFile} to exist");
        });

        var file = DicomFile.Open(expectedFile);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
    }

    [Test]
    public void MultipleFilesSameSeriesTest()
    {
        var inputDirPath = Path.Combine(_inputFileBase, "MultipleFilesSameSeriesTest");

        Directory.CreateDirectory(inputDirPath);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0013_NAME)), TestData.Img013,true);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, IM_0001_0019_NAME)), TestData.Img013,false);

        var outputDirPath = Path.Combine(_outputFileBase, "MultipleFilesSameSeriesTest");
        var expectedFile1 = Path.Combine(outputDirPath, IM_0001_0013_NAME);
        var expectedFile2 = Path.Combine(outputDirPath, IM_0001_0019_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "BasicTest.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
            NumThreads = 1
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile1), Is.True, $"Expected output file {expectedFile1} to exist");
            Assert.That(File.Exists(expectedFile2), Is.True, $"Expected output file {expectedFile2} to exist");
        });

        var file = DicomFile.Open(expectedFile1);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));

        file = DicomFile.Open(expectedFile2);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void MultipleSeriesTest(bool useSubfolder)
    {
        var inputDirPath = Path.Combine(_seriesFilesBase, "TestInput");

        Directory.CreateDirectory(Path.Combine(inputDirPath, "Series1"));
        Directory.CreateDirectory(Path.Combine(inputDirPath, "Series2"));
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series1", IM_0001_0013_NAME)), TestData.Img013,true);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series1", IM_0001_0019_NAME)), TestData.Img013,false);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series2", IM_0001_0013_NAME)), TestData.Img019,true);
        TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series2", IM_0001_0019_NAME)), TestData.Img019,false);

        var outputDirPath = Path.Combine(_seriesFilesBase, "TestOutput");
        var expectedFile1 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID1/Series1" : "Series1", IM_0001_0013_NAME);
        var expectedFile2 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID1/Series1" : "Series1", IM_0001_0019_NAME);
        var expectedFile3 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID2/Series2" : "Series2", IM_0001_0013_NAME);
        var expectedFile4 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID2/Series2" : "Series2", IM_0001_0019_NAME);

        var options = new DicomRepopulatorOptions
        {
            InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "TwoSeriesCsv.csv"),
            InputFolder = inputDirPath,
            OutputFolder = outputDirPath,
            SubFolderColumn = useSubfolder ? "ID": null,
            InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
            NumThreads = 4
        };

        var result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(0));

            Assert.That(File.Exists(expectedFile1), Is.True, $"Expected output file {expectedFile1} to exist");
            Assert.That(File.Exists(expectedFile2), Is.True, $"Expected output file {expectedFile2} to exist");
            Assert.That(File.Exists(expectedFile3), Is.True, $"Expected output file {expectedFile3} to exist");
            Assert.That(File.Exists(expectedFile4), Is.True, $"Expected output file {expectedFile4} to exist");
        });

        var file = DicomFile.Open(expectedFile1);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));

        file = DicomFile.Open(expectedFile2);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID1"));

        file = DicomFile.Open(expectedFile3);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID2"));

        file = DicomFile.Open(expectedFile4);
        Assert.That(file.Dataset.GetValue<string>(DicomTag.PatientID, 0), Is.EqualTo("NewPatientID2"));
    }
    /// <summary>
    /// Writes the supplied string to "ExtraMappings.txt" in the test directory and returns the path to the file
    /// </summary>
    /// <param name="contents"></param>
    /// <returns></returns>
    private static FileInfo CreateExtraMappingsFile(params string[] contents)
    {
        return GenerateTextFile(contents, "ExtraMappings.txt");
    }

    /// <summary>
    /// Writes the supplied string to "Map.csv" in the test directory and returns the path to the file
    /// </summary>
    private static FileInfo CreateInputCsvFile(params string[] contents)
    {
        return GenerateTextFile(contents, "Map.csv");
    }

    private static FileInfo GenerateTextFile(string[] contents, string filename)
    {
        var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory,filename);
        File.WriteAllLines(filePath,contents);

        return new FileInfo(filePath);
    }

    /// <summary>
    /// Creates a new input directory with the name of the calling method (under <see cref="_inputFileBase"/>) then creates
    /// the given dicom file (<paramref name="testFile"/>) at <paramref name="filename"/> location (which can include subdirectories)
    /// </summary>
    /// <param name="testFile">The dicom file</param>
    /// <param name="filename">The filename to write out e.g. "my.dcm" or "mysubdir/my.dcm"</param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    private FileInfo CreateInputFile(string testFile,string filename,[System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
    {
        var inputDirPath = Path.Combine(_inputFileBase, memberName);

        Directory.CreateDirectory(inputDirPath);
        var toReturn = new FileInfo(Path.Combine(inputDirPath, filename));

        toReturn.Directory?.Create();

        return TestData.Create(toReturn, testFile);
    }
    /// <summary>
    /// Runs the <see cref="DicomRepopulatorProcessor"/> with the provided <paramref name="inputCsv"/> etc.  Asserts that there
    /// are no errors during the run and th
    /// </summary>
    /// <param name="expectedDone"></param>
    /// <param name="expectedErrors"></param>
    /// <param name="inputCsv"></param>
    /// <param name="inputExtraMapping"></param>
    /// <param name="inputDicomDirectory"></param>
    /// <param name="adjustOptions"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    private DirectoryInfo AssertRunsSuccesfully(int expectedDone, int expectedErrors, FileInfo inputCsv, FileInfo inputExtraMapping,
        DirectoryInfo inputDicomDirectory,
        Action<DicomRepopulatorOptions> adjustOptions = null,
        [System.Runtime.CompilerServices.CallerMemberName]
        string memberName = "")
    {
        var processor = GetProcessor(inputCsv,
            inputExtraMapping,
            inputDicomDirectory,
            adjustOptions,
            memberName,
            out var options,
            out var outputDirPath);

        var result = processor.Process(options,CancellationToken.None);
        Assert.That(result, Is.EqualTo(0));

        foreach (var log in processor.MemoryLogTarget.Logs)
            Console.WriteLine(log);

        Assert.Multiple(() =>
        {
            Assert.That(processor.Errors, Is.EqualTo(expectedErrors), "Expected error count was not correct");
            Assert.That(processor.Done, Is.EqualTo(expectedDone), "Expected success count was not correct");
        });

        return outputDirPath;
    }

    private DicomRepopulatorProcessor GetProcessor(FileInfo inputCsv, FileInfo inputExtraMapping,
        DirectoryInfo inputDicomDirectory, Action<DicomRepopulatorOptions> adjustOptions, string memberName,
        out DicomRepopulatorOptions options,
        out DirectoryInfo outputDirPath)
    {
        outputDirPath = new DirectoryInfo(Path.Combine(_outputFileBase, memberName));

        //delete old content
        if(Directory.Exists(outputDirPath.FullName))
            Directory.Delete(outputDirPath.FullName,true);

        Directory.CreateDirectory(outputDirPath.FullName);


        options = new DicomRepopulatorOptions
        {
            InputCsv = inputCsv?.FullName,
            InputFolder = inputDicomDirectory?.FullName,
            InputExtraMappings = inputExtraMapping?.FullName,
            OutputFolder = outputDirPath.FullName,
            NumThreads = 4
        };

        adjustOptions?.Invoke(options);

        return new DicomRepopulatorProcessor();
    }

}