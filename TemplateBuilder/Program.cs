using System;
using System.Windows.Forms;
using System.Runtime.Versioning;

namespace TemplateBuilder;

[SupportedOSPlatform("windows7.0")]
static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.ThreadException += Application_ThreadException;
        Application.Run(new Form1());
    }

    private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        if(e.Exception.Message.Equals("Value cannot be null. (Parameter 'owningItem')"))
            return;

        throw e.Exception;
    }
}