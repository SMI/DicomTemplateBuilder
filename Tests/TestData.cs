using System.IO;
using NUnit.Framework;

namespace Tests;

public sealed class TestData
{
    // Paths to the test DICOM files relative to TestContext.CurrentContext.TestDirectory
    private const string HelpTestDataDir = "TestData";
    public static readonly string Img013 = Path.Combine(HelpTestDataDir, "IM-0001-0013.dcm");
    internal static readonly string Img019 = Path.Combine(HelpTestDataDir, "IM-0001-0019.dcm");

    /// <summary>
    /// Creates the test image <see cref="Img013"/> in the file location specified
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="testFile">The test file to create, should be a static member of this class.  Defaults to <see cref="Img013"/></param>
    /// <param name="cleanDirectory">True to delete any existing files in the directory</param>
    /// <returns></returns>
    internal static FileInfo Create(FileInfo fileInfo, string testFile=null,bool cleanDirectory = true)
    {
        var from = Path.Combine(TestContext.CurrentContext.TestDirectory, testFile??Img013);

        if(fileInfo.Directory != null)
            if(!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            else
            {
                if (cleanDirectory)
                {
                    foreach(var f in fileInfo.Directory.GetFiles("*",SearchOption.AllDirectories))
                        f.Delete();

                    foreach(var d in fileInfo.Directory.EnumerateDirectories())
                        d.Delete();
                }

            }

        File.Copy(from,fileInfo.FullName,true);

        return fileInfo;
    }

}