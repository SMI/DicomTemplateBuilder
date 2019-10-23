using System;
using System.IO;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace TemplateBuilder.Repopulator
{
    public partial class RepopulatorUI : UserControl
    {
        public RepopulatorUIState State;
        private const string StateFile = "RepopulatorUI.yaml";
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
                    nThreads.Value = State.NumThreads;
                    tbPattern.Text = State.Pattern;
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
    }
}
