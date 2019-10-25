using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace TemplateBuilder.Repopulator
{
    public partial class RepopulatorUI : UserControl
    {
        public RepopulatorUIState State;
        private const string StateFile = "RepopulatorUI.yaml";

        public DicomRepopulatorProcessor _populator;

        public RepopulatorUI()
        {
            InitializeComponent();

            State = new RepopulatorUIState();

            try
            {
                if (File.Exists("RepopulatorUI.yaml"))
                {
                    var des = new Deserializer();
                    State = des.Deserialize<RepopulatorUIState>(File.ReadAllText(StateFile));

                    cbIncludeSubfolders.Checked = State.IncludeSubdirectories;
                    tbInputFolder.Text = State.InputFolder;
                    tbInputCsv.Text = State.InputCsv;
                    tbOutputFolder.Text = State.OutputFolder;
                    nThreads.Value = Math.Min(Math.Max(nThreads.Minimum,State.NumThreads),nThreads.Maximum);
                    tbPattern.Text = State.Pattern;
                    tbFilenameColumn.Text = State.FileNameColumn;
                    cbAnonymise.Checked = State.Anonymise;
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (sender == btnInputCsv)
            {
                var ofd = new OpenFileDialog();
                ofd.CheckPathExists = true;

                try
                {
                    if (tbInputCsv.Text != null)
                        ofd.InitialDirectory = Path.GetDirectoryName(tbInputCsv.Text);
                }
                catch (Exception)
                {
                    //they typed something odd in there?
                }

                ofd.Filter = "Comma Separated File|*.csv";
                ofd.Multiselect = false;

                if(ofd.ShowDialog()==DialogResult.OK)
                    tbInputCsv.Text = ofd.FileName;
            }
            else
            {
                var ofd = new FolderBrowserDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var tb = sender == btnInputFolder ? tbInputFolder : tbOutputFolder;
                    tb.Text = ofd.SelectedPath;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _populator = new DicomRepopulatorProcessor();
                _populator.Process(State);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if(sender == tbInputFolder)
                State.InputFolder = tbInputFolder.Text;

            if(sender == tbInputCsv)
                State.InputCsv = tbInputCsv.Text;

            if(sender == tbOutputFolder)
                State.OutputFolder = tbOutputFolder.Text;


            SaveState();
        }

        private void SaveState()
        {
            try
            {
                var ser = new Serializer();
                File.WriteAllText(StateFile,ser.Serialize(State));
            }
            catch (Exception)
            {
                
            }
        }

        private void cbIncludeSubfolders_CheckedChanged(object sender, EventArgs e)
        {
            State.IncludeSubdirectories = cbIncludeSubfolders.Checked;
            SaveState();
        }

        private void tbPattern_TextChanged(object sender, EventArgs e)
        {
            State.Pattern = tbPattern.Text;
            SaveState();
        }

        private void nThreads_ValueChanged(object sender, EventArgs e)
        {
            State.NumThreads = (int) nThreads.Value;
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
            State.FileNameColumn = tbFilenameColumn.Text;
            SaveState();
        }

        private void cbAnonymise_CheckedChanged(object sender, EventArgs e)
        {
            State.Anonymise = cbAnonymise.Checked;
            SaveState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string log = _populator?.MemoryLogTarget?.Logs?.Last();

            if (log != lblProgress.Text)
                lblProgress.Text = log;

            string done = (_populator?.Done ?? 0).ToString("{0:n0}");

            if(done != tbDone.Text)
                tbDone.Text = done;
        }
    }
}
