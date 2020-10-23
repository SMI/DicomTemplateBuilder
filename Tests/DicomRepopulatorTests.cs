using System;
using System.IO;
using System.Threading;
using Dicom;
using NUnit.Framework;
using Repopulator;

namespace Tests
{
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
            var inputDicom = CreateInputFile(TestData.IMG_013,
                Path.Combine("subdir", nameof(TestData.IMG_013) +".dcm"));
            
            //Create a CSV with the full path to the image
            var inputCsv = CreateInputCsvFile(
                $@"File,PatientID
{inputDicom.FullName},ABC");

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
            var expectedOutFile = new FileInfo(Path.Combine(outDir.FullName, "subdir", nameof(TestData.IMG_013) + ".dcm"));
            FileAssert.Exists(expectedOutFile);

            Assert.AreEqual(!deleteAsYouGo,File.Exists(inputDicom.FullName));
        }

        [TestCase("./subdir/IM-0001-0013.dcm")]
        [TestCase("subdir/IM-0001-0013.dcm")]
        [TestCase("./../Test_RelativePath_InCsv/subdir/IM-0001-0013.dcm")]
        public void Test_RelativePath_InCsv(string csvPath)
        {
            //Create a dicom file in the input dir /subdir/IM_0001_0013.dcm
            var inputDicom = TestData.Create(new FileInfo(Path.Combine(TestContext.CurrentContext.WorkDirectory,nameof(Test_RelativePath_InCsv),"subdir", Path.GetFileName(TestData.IMG_013))),TestData.IMG_013);
            
            //Create a CSV with the full path to the image
            var inputCsv = CreateInputCsvFile(
                $@"File,PatientID
{csvPath},ABC");

            //run repopulator
            var outDir = AssertRunsSuccesfully(1,
                0,
                inputCsv,
                null,
                inputDicom.Directory.Parent,
                o => o.FileNameColumn = "File");
            
            //anonymous image should appear in the subdirectory of the out dir
            var expectedOutFile = new FileInfo(Path.Combine(outDir.FullName, "subdir", Path.GetFileName(TestData.IMG_013)));
            FileAssert.Exists(expectedOutFile);
        }

        [Test]
        public void Test_StudyDateTag_GoodData()
        {
            var inputDicom = CreateInputFile(TestData.IMG_013, "mydicom.dcm");
            var inputCsv = CreateInputCsvFile(
                $@"RelativeFileArchiveURI,PatientID,StudyDate
{inputDicom.FullName},ABC123,2001-01-01");

            var outDir = AssertRunsSuccesfully(1,0,inputCsv,null,inputDicom.Directory);
            
            var fi = new FileInfo (Path.Combine(outDir.FullName,"mydicom.dcm"));
            FileAssert.Exists(fi);

            DicomFile file = DicomFile.Open(fi.FullName);
            Assert.AreEqual("ABC123", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
            Assert.AreEqual(new DateTime(2001,1,1), file.Dataset.GetValue<DateTime>(DicomTag.StudyDate, 0));

        }

        [Ignore("This really shouldn't be passing! how do we validate that 'Lolz' is not a valid StudyDate for inserting rather than blindly writting it in?")]
        [Test]
        public void Test_StudyDateTag_BadData()
        {
            var inputDicom = CreateInputFile(TestData.IMG_013, "mydicom.dcm");
            var inputCsv = CreateInputCsvFile(
                $@"RelativeFileArchiveURI,PatientID,StudyDate
{inputDicom.FullName},ABC123,Lolz");

            //should be an error because Lolz is not a valid date!
            var outDir = AssertRunsSuccesfully(0,1,inputCsv,null,inputDicom.Directory);
            
            var fi = new FileInfo (Path.Combine(outDir.FullName,"mydicom.dcm"));
            FileAssert.Exists(fi);

            DicomFile file = DicomFile.Open(fi.FullName);
            Assert.AreEqual("ABC123", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
            Assert.AreEqual(new DateTime(2001,1,1), file.Dataset.GetValue<DateTime>(DicomTag.StudyDate, 0));

        }

        [Test]
        public void SingleFileBasicOperationTest()
        {
            var inFile = CreateInputFile(TestData.IMG_013,nameof(TestData.IMG_013) +".dcm");

            var outDir = AssertRunsSuccesfully(1, 0,null, 
                
                //Treat Csv column "ID" as a replacement for PatientID
                CreateExtraMappingsFile("ID:PatientID"), inFile.Directory,
                
                //Give it BasicTest.csv 
                o => o.InputCsv= Path.Combine(TestContext.CurrentContext.TestDirectory, "BasicTest.csv"));

            //Anonymous dicom image should exist
            var expectedFile = new FileInfo(Path.Combine(outDir.FullName, nameof(TestData.IMG_013) + ".dcm"));
            FileAssert.Exists(expectedFile);

            //it should have the patient ID from the csv
            DicomFile file = DicomFile.Open(expectedFile.FullName);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void KeyNotFirstColumn(bool runSecondaryAnon)
        {
            string inputDirPath = Path.Combine(_inputFileBase, "KeyNotFirstColumn");
            const string testFileName = IM_0001_0013_NAME;

            Directory.CreateDirectory(inputDirPath);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName)), TestData.IMG_013);

            string outputDirPath = Path.Combine(_outputFileBase, "KeyNotFirstColumn");
            string expectedFile = Path.Combine(outputDirPath, testFileName);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "KeyNotFirstColumn.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                Anonymise = runSecondaryAnon,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID","sopid:SOPInstanceUID" ).FullName,
                NumThreads = 4
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile), "Expected output file {0} to exist", expectedFile);

            DicomFile file = DicomFile.Open(expectedFile);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
        }

        [Test]
        public void DateRepopulation()
        {
            string inputDirPath = Path.Combine(_inputFileBase, "DateRepopulation");
            const string testFileName = IM_0001_0013_NAME;

            Directory.CreateDirectory(inputDirPath);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName)), TestData.IMG_013);

            string outputDirPath = Path.Combine(_outputFileBase, "DateRepopulation");
            string expectedFile = Path.Combine(outputDirPath, testFileName);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "WithDate.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID", "Date:StudyDate" ).FullName,
                NumThreads = 4
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile), "Expected output file {0} to exist", expectedFile);

            DicomFile file = DicomFile.Open(expectedFile);

            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
            Assert.AreEqual("20180601", file.Dataset.GetValue<string>(DicomTag.StudyDate, 0));
        }

        [Test]
        public void OneCsvColumnToMultipleDicomTags()
        {
            string inputDirPath = Path.Combine(_inputFileBase, "OneCsvColumnToMultipleDicomTags");
            const string testFileName = IM_0001_0013_NAME;

            Directory.CreateDirectory(inputDirPath);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName)), TestData.IMG_013);

            string outputDirPath = Path.Combine(_outputFileBase, "OneCsvColumnToMultipleDicomTags");
            string expectedFile = Path.Combine(outputDirPath, testFileName);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "WithDate.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID", "Date:StudyDate", "Date:SeriesDate" ).FullName,
                NumThreads = 1
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile), "Expected output file {0} to exist", expectedFile);

            DicomFile file = DicomFile.Open(expectedFile);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
            Assert.AreEqual("20180601", file.Dataset.GetValue<string>(DicomTag.StudyDate, 0));
            Assert.AreEqual("20180601", file.Dataset.GetValue<string>(DicomTag.SeriesDate, 0));
        }

        [Test]
        public void SpacesInCsvHeaderTest()
        {
            string inputDirPath = Path.Combine(_inputFileBase, "SpacesInCsvHeaderTest");
            const string testFileName = IM_0001_0013_NAME;

            Directory.CreateDirectory(inputDirPath);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName)), TestData.IMG_013);

            string outputDirPath = Path.Combine(_outputFileBase, "SpacesInCsvHeaderTest");
            string expectedFile = Path.Combine(outputDirPath, testFileName);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "SpacesInCsvHeaderTest.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
                NumThreads = 1
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile), "Expected output file {0} to exist", expectedFile);

            DicomFile file = DicomFile.Open(expectedFile);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
        }

        [Test]
        public void MultipleFilesSameSeriesTest()
        {
            string inputDirPath = Path.Combine(_inputFileBase, "MultipleFilesSameSeriesTest");
            const string testFileName1 = IM_0001_0013_NAME;
            const string testFileName2 = IM_0001_0019_NAME;

            Directory.CreateDirectory(inputDirPath);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName1)), TestData.IMG_013,true);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, testFileName2)), TestData.IMG_013,false);

            string outputDirPath = Path.Combine(_outputFileBase, "MultipleFilesSameSeriesTest");
            string expectedFile1 = Path.Combine(outputDirPath, testFileName1);
            string expectedFile2 = Path.Combine(outputDirPath, testFileName2);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "BasicTest.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
                NumThreads = 1
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile1), "Expected output file {0} to exist", expectedFile1);
            Assert.True(File.Exists(expectedFile2), "Expected output file {0} to exist", expectedFile2);

            DicomFile file = DicomFile.Open(expectedFile1);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));

            file = DicomFile.Open(expectedFile2);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MultipleSeriesTest(bool useSubfolder)
        {
            string inputDirPath = Path.Combine(_seriesFilesBase, "TestInput");
            const string testFileName1 = IM_0001_0013_NAME;
            const string testFileName2 = IM_0001_0019_NAME;

            Directory.CreateDirectory(Path.Combine(inputDirPath, "Series1"));
            Directory.CreateDirectory(Path.Combine(inputDirPath, "Series2"));
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series1", testFileName1)), TestData.IMG_013,true);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series1", testFileName2)), TestData.IMG_013,false);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series2", testFileName1)), TestData.IMG_019,true);
            TestData.Create(new FileInfo(Path.Combine(inputDirPath, "Series2", testFileName2)), TestData.IMG_019,false);

            string outputDirPath = Path.Combine(_seriesFilesBase, "TestOutput");
            string expectedFile1 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID1/Series1" : "Series1", testFileName1);
            string expectedFile2 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID1/Series1" : "Series1", testFileName2);
            string expectedFile3 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID2/Series2" : "Series2", testFileName1);
            string expectedFile4 = Path.Combine(outputDirPath, useSubfolder? "NewPatientID2/Series2" : "Series2", testFileName2);

            var options = new DicomRepopulatorOptions
            {
                InputCsv = Path.Combine(TestContext.CurrentContext.TestDirectory, "TwoSeriesCsv.csv"),
                InputFolder = inputDirPath,
                OutputFolder = outputDirPath,
                SubFolderColumn = useSubfolder ? "ID": null,
                InputExtraMappings = CreateExtraMappingsFile( "ID:PatientID" ).FullName,
                NumThreads = 4
            };

            int result = new DicomRepopulatorProcessor().Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);

            Assert.True(File.Exists(expectedFile1), "Expected output file {0} to exist", expectedFile1);
            Assert.True(File.Exists(expectedFile2), "Expected output file {0} to exist", expectedFile2);
            Assert.True(File.Exists(expectedFile3), "Expected output file {0} to exist", expectedFile3);
            Assert.True(File.Exists(expectedFile4), "Expected output file {0} to exist", expectedFile4);

            DicomFile file = DicomFile.Open(expectedFile1);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));

            file = DicomFile.Open(expectedFile2);
            Assert.AreEqual("NewPatientID1", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));

            file = DicomFile.Open(expectedFile3);
            Assert.AreEqual("NewPatientID2", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));

            file = DicomFile.Open(expectedFile4);
            Assert.AreEqual("NewPatientID2", file.Dataset.GetValue<string>(DicomTag.PatientID, 0));
        }
        /// <summary>
        /// Writes the supplied string to "ExtraMappings.txt" in the test directory and returns the path to the file
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        private FileInfo CreateExtraMappingsFile(params string[] contents)
        {
            return GenerateTextFile(contents, "ExtraMappings.txt");
        }
        
        /// <summary>
        /// Writes the supplied string to "Map.csv" in the test directory and returns the path to the file
        /// </summary>
        private FileInfo CreateInputCsvFile(params string[] contents)
        {
            return GenerateTextFile(contents, "Map.csv");
        }

        private FileInfo GenerateTextFile(string[] contents, string filename)
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
            string inputDirPath = Path.Combine(_inputFileBase, memberName);
            
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
                out DicomRepopulatorOptions options,
                out DirectoryInfo outputDirPath);
            
            int result = processor.Process(options,CancellationToken.None);
            Assert.AreEqual(0, result);
            
            foreach (var log in processor.MemoryLogTarget.Logs)
                Console.WriteLine(log);

            Assert.AreEqual(expectedErrors,processor.Errors,"Expected error count was not correct");
            Assert.AreEqual(expectedDone,processor.Done, "Expected success count was not correct");

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
}
