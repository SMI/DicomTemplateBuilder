using System.IO;
using NUnit.Framework;

namespace Tests;

public sealed class TestData
{
    // Paths to the test DICOM files relative to TestContext.CurrentContext.TestDirectory
    private const string TEST_DATA_DIR = "TestData";
    public static string IMG_013 = Path.Combine(TEST_DATA_DIR, "IM-0001-0013.dcm");
    public static string IMG_019 = Path.Combine(TEST_DATA_DIR, "IM-0001-0019.dcm");

    /// <summary>
    /// Creates the test image <see cref="IMG_013"/> in the file location specified
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="testFile">The test file to create, should be a static member of this class.  Defaults to <see cref="IMG_013"/></param>
    /// <param name="cleanDirectory">True to delete any existing files in the directory</param>
    /// <returns></returns>
    public static FileInfo Create(FileInfo fileInfo, string testFile=null,bool cleanDirectory = true)
    {
        var from = Path.Combine(TestContext.CurrentContext.TestDirectory, testFile??IMG_013);
            
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