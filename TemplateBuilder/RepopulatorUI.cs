using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repopulator;
using YamlDotNet.Serialization;

namespace TemplateBuilder
{
    public partial class RepopulatorUI : UserControl
    {
        public DicomRepopulatorOptions State;
        private const string StateFile = "RepopulatorUI.yaml";

        public DicomRepopulatorProcessor _populator;
        private const string HelpErrorThreshold =
            "The maximum number of errors in file processing / csv file reading before aborting the process";
        public const string HelpCopyToClipboard =
            "Copies the current log to the clipboard";
        public const string HelpFilePattern =
            "The search pattern to use to identify dicom files in the input directory, defaults to *.dcm.  Set to * if you lack file extensions";
        public const string HelpInputCsv = 
            "Csv file which contains anonymous values that you want to insert into dicom files.  Column headers must match dicom tags or you must provide ExtraMappings";
        public const string HelpExtraMappings =
            "Optional, allows you to have non dicom tag headers in your CSV file and still map them to 1+ dicom tags. e.g. \"MyFunkyHeader:SOPInstanceUID\"";
        public const string HelpInputFolder =
            "Directory containing one or more dicom files.  These files can be in subdirectories and do not have to have the extension .dcm (if you edit the Pattern)";
        public const string HelpIncludeSubFolders =
            "Check to search subdirectories of the input folder";
        public const string HelpFileNameColumn =
            "Optional, if your csv file includes a relative or absolute file path to images enter it here to avoid having to match based on SOP/Series/Study instance UID";
        public const string HelpOutputFolder =
            "The folder on disk that anonymised output images should be written to";

        public const string HelpDone =
            "The number of anonymous images succesfully written to the output folder";
        public const string HelpErrors =
            "The number of errors occuring during processing.  For example this can be an error resolving a CSV line or an error writting a tag / file to disk.";

        public const string HelpCulture
            = "The culture to use for parsing string values into dicom types e.g. dates, numbers";

        public const string HelpSubFolderColumn
            = "Optional, column in your CSV which provides the top level extraction folder under which all associated studies/series will go e.g. PatientID";

        public RepopulatorUI()
        {
            InitializeComponent();

            State = new DicomRepopulatorOptions();

            try
            {
                if (File.Exists("RepopulatorUI.yaml"))
                {
                    var des = new Deserializer();
                    State = des.Deserialize<DicomRepopulatorOptions>(File.ReadAllText(StateFile));

                    cbIncludeSubfolders.Checked = State.IncludeSubdirectories;
                    tbInputFolder.Text = State.InputFolder;
                    tbInputCsv.Text = State.InputCsv;
                    tbExtraMappings.Text = State.InputExtraMappings;
                    tbOutputFolder.Text = State.OutputFolder;
                    nThreads.Value = Math.Min(Math.Max(nThreads.Minimum,State.NumThreads),nThreads.Maximum);
                    nErrorThreshold.Value = Math.Min(Math.Max(nErrorThreshold.Minimum, State.ErrorThreshold),nErrorThreshold.Maximum);
                    tbFilePattern.Text = State.Pattern;
                    tbFilenameColumn.Text = State.FileNameColumn;
                    cbAnonymise.Checked = State.Anonymise;
                    cbDeleteAsYouGo.Checked = State.DeleteAsYouGo;

                    if (!string.IsNullOrWhiteSpace(State.CultureName))
                    {
                        tbCulture.Text = State.CultureName;
                        State.Culture = new CultureInfo(State.CultureName);
                    }
                    else
                        tbCulture.Text = State.Culture.DisplayName;

                    tbSubFolderColumn.Text = State.SubFolderColumn;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in RepopulatorUI.yaml", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tt = new ToolTip {InitialDelay = 0, AutoPopDelay = 32767, ShowAlways = true};
            tt.SetToolTip(btnInputFolder,HelpInputFolder);
            tt.SetToolTip(lblInputFolder,HelpInputFolder);

            tt.SetToolTip(btnInputCsv,HelpInputCsv);
            tt.SetToolTip(lblInputCsv,HelpInputCsv);

            tt.SetToolTip(btnExtraMappings,HelpExtraMappings);
            tt.SetToolTip(lblExtraMappings,HelpExtraMappings);

            tt.SetToolTip(cbIncludeSubfolders,HelpIncludeSubFolders);
            tt.SetToolTip(lblFileNameColumn,HelpFileNameColumn);
            tt.SetToolTip(lblFilePattern,HelpFilePattern);

            tt.SetToolTip(btnOutputFolder,HelpOutputFolder);
            tt.SetToolTip(lblOutputFolder,HelpOutputFolder);
            
            tt.SetToolTip(btnCopyToClipboard,HelpCopyToClipboard);

            tt.SetToolTip(lblErrorThreshold,HelpErrorThreshold);

            tt.SetToolTip(lblDone,HelpDone);
            tt.SetToolTip(lblErrors,HelpErrors);

            tt.SetToolTip(lblCulture,HelpCulture);

            tt.SetToolTip(lblSubFolder,HelpSubFolderColumn);
        }
        

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (sender == btnInputCsv)
                BrowseForFile(tbInputCsv, "Comma Separated File|*.csv");
            else if (sender == btnExtraMappings)
                BrowseForFile(tbExtraMappings, "Supplemental Mappings File|*.*");
            else if (sender == btnInputFolder)
                BrowseForFolder(tbInputFolder);
            else if(sender == btnOutputFolder)
                BrowseForFolder(tbOutputFolder);
        }

        private void BrowseForFolder(TextBox destinationTextBox)
        {
            using (var ofd = new FolderBrowserDialog())
                if (ofd.ShowDialog() == DialogResult.OK)
                    destinationTextBox.Text = ofd.SelectedPath;
        }

        private void BrowseForFile(TextBox destinationTextBox, string filter)
        {
            using (var ofd = new OpenFileDialog {CheckPathExists = true, Filter = filter, Multiselect = false})
            {
                try
                {
                    if (destinationTextBox.Text != null)
                        ofd.InitialDirectory = Path.GetDirectoryName(destinationTextBox.Text);
                }
                catch (Exception)
                {
                    //they typed something odd in there?
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                    destinationTextBox.Text = ofd.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            var task = new Task(() // lgtm[cs/local-not-disposed] - Tasks don't really need to be Disposed
                =>
                { using(_populator = new DicomRepopulatorProcessor())
                    _populator.Process(State);
                });

            task.ContinueWith(t =>
            {
                if(t.IsFaulted)
                    MessageBox.Show(UnpackException(t.Exception), "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnStart.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            
            task.Start();
        }

        private string UnpackException(AggregateException exception)
        {
            return string.Join(Environment.NewLine,exception.InnerExceptions.Select(e=>e.Message));
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if(sender == tbInputFolder)
                State.InputFolder = tbInputFolder.Text;

            if(sender == tbInputCsv)
                State.InputCsv = tbInputCsv.Text;

            if(sender == tbOutputFolder)
                State.OutputFolder = tbOutputFolder.Text;

            if (sender == tbExtraMappings)
                State.InputExtraMappings = tbExtraMappings.Text;

            SaveState();
        }

        private void SaveState()
        {
            try
            {
                var ser = new Serializer();
                File.WriteAllText(StateFile,ser.Serialize(State));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in RepopulatorUI.yaml", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbIncludeSubfolders_CheckedChanged(object sender, EventArgs e)
        {
            State.IncludeSubdirectories = cbIncludeSubfolders.Checked;
            SaveState();
        }

        private void tbPattern_TextChanged(object sender, EventArgs e)
        {
            State.Pattern = string.IsNullOrWhiteSpace(tbFilePattern.Text) ? DicomRepopulatorOptions.DefaultPattern:tbFilePattern.Text;
            SaveState();
        }

        private void nThreads_ValueChanged(object sender, EventArgs e)
        {
            State.NumThreads = (int) nThreads.Value;
            SaveState();
        }
        
        private void nErrorThreshold_ValueChanged(object sender, EventArgs e)
        {
            State.ErrorThreshold = (int) nErrorThreshold.Value;
            SaveState();
        }
        private void tbSubFolderColumn_TextChanged(object sender, EventArgs e)
        {
            State.SubFolderColumn = tbSubFolderColumn.Text;
            SaveState();
        }

        private void btnValidateCsv_Click(object sender, EventArgs e)
        {
            var mapping = new CsvToDicomTagMapping();
            bool built = mapping.BuildMap(State, out string log);

            MessageBox.Show(log,$"Validation { (built ? "Success" : "Failure" )}",MessageBoxButtons.OK,built ? MessageBoxIcon.Information: MessageBoxIcon.Error);
        }

        private void tbFilenameColumn_TextChanged(object sender, EventArgs e)
        {
            State.FileNameColumn = string.IsNullOrWhiteSpace(tbFilenameColumn.Text) ? DicomRepopulatorOptions.DefaultFileNameColumn : tbFilenameColumn.Text;
            SaveState();
        }

        private void cbAnonymise_CheckedChanged(object sender, EventArgs e)
        {
            State.Anonymise = cbAnonymise.Checked;
            SaveState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string log = _populator?.MemoryLogTarget?.Logs?.Last();

                if (log != lblProgress.Text)
                    lblProgress.Text = log;

                btnCopyToClipboard.Enabled = log != null;

                int nDone = _populator?.Done ?? 0;
                int nErrors = _populator?.Errors ?? 0;
                int nInput = _populator?.Input ?? 0;

                string done = $"{nDone:n0}";

                if(done != tbDone.Text)
                    tbDone.Text = done;

                string errors = $"{nErrors:n0}";

                if(errors != tbErrors.Text)
                    tbErrors.Text = errors;
                
                int processed = nDone + nErrors;
                
                progressBar1.Style = ProgressBarStyle.Continuous;

                if (nInput > 0)
                {
                    progressBar1.Maximum = nInput;
                    progressBar1.Value = Math.Min(processed,nInput);
                }
                
                
            }
            catch (Exception)
            {
                tbDone.Text = "-";
                tbErrors.Text = "-";
                lblProgress.Text = "-";
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
        }

        private void RepopulatorUI_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            var logs = _populator?.MemoryLogTarget?.Logs;
            if (logs != null)
                Clipboard.SetText(string.Join(Environment.NewLine, logs));
        }

        private void tbCulture_TextChanged(object sender, EventArgs e)
        {
            try
            {
                State.Culture = new CultureInfo(tbCulture.Text);
                State.CultureName = tbCulture.Text;
                tbCulture.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                tbCulture.ForeColor = Color.Red;
            }

            SaveState();

        }

        private void cbDeleteAsYouGo_CheckedChanged(object sender, EventArgs e)
        {
            State.DeleteAsYouGo = cbDeleteAsYouGo.Checked;
            SaveState();
        }
    }
}
