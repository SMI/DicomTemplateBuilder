using AutocompleteMenuNS;
using BrightIdeasSoftware;
using Dicom;
using DicomTypeTranslation;
using DicomTypeTranslation.TableCreation;
using FAnsi.Discovery;
using FAnsi.Implementation;
using FAnsi.Implementations.MicrosoftSQL;
using ScintillaNET;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using DatabaseType = FAnsi.DatabaseType;

namespace TemplateBuilder
{
    public partial class Form1 : Form
    {
        private string _filename;
        private Scintilla _scintillaTemplate;
        private Scintilla _scintillaSql;

        public Form1()
        {
            InitializeComponent();

            
            _scintillaTemplate = new Scintilla(){Dock = DockStyle.Fill};
            _scintillaSql = new Scintilla(){Dock = DockStyle.Fill};

            _scintillaTemplate.AllowDrop = true;
            _scintillaTemplate.DragDrop += (sender, args) =>
            {
                var scintilla = (Scintilla) sender;
                if (args.Data.GetDataPresent(typeof(TagValueNode)))
                {
                    var text = string.Format(@"
  - ColumnName: {0}
    AllowNulls: true", Tag);

                    scintilla.InsertText(scintilla.CurrentPosition, "fish");
                }
            };
                

            ImplementationManager.Load<MicrosoftSQLImplementation>();

            var autoComplete = new AutocompleteMenu();

            autoComplete.AddItem(new AutocompleteItem("Tables"));
            autoComplete.AddItem(new AutocompleteItem("TableName"));
            autoComplete.AddItem(new AutocompleteItem("ColumnName"));
            autoComplete.AddItem(new AutocompleteItem("AllowNulls"));
            autoComplete.AddItem(new AutocompleteItem("IsPrimaryKey"));

            foreach (string keyword in DicomDictionary.Default.Select(e => e.Keyword).Distinct())
                autoComplete.AddItem(keyword);
            
            autoComplete.TargetControlWrapper = new ScintillaWrapper(_scintillaTemplate);

            tpEditor.Controls.Add(_scintillaTemplate);
            tpSql.Controls.Add(_scintillaSql);
            
        }

        private void olvDicoms_CanDrop(object sender, OlvDropEventArgs e)
        {
            var dataObject = (DataObject)e.DataObject;

            e.Effect = dataObject.ContainsFileDropList() ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void olvDicoms_Dropped(object sender, OlvDropEventArgs e)
        {
            var dataObject = (DataObject)e.DataObject;

            if(dataObject.ContainsFileDropList())
                foreach (string filename in dataObject.GetFileDropList())
                {
                    if(Path.GetExtension(filename) != ".dcm")
                        continue;
                    var fi = new FileInfo(filename);
                    olvDicoms.AddObject(fi);
                }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _filename = null;
            _scintillaTemplate.ClearAll();

            var c = new ImageTableTemplateCollection();
            c.DatabaseType = DatabaseType.MicrosoftSQLServer;

            _scintillaTemplate.Text = c.Serialize();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Imaging Template|*.it";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(ofd.FileName))
                {
                    _scintillaTemplate.Text = File.ReadAllText(ofd.FileName);
                    _filename = ofd.FileName;
                    Check();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!Check())
                return;

            if (_filename == null)
            {
                SaveAs();
                return;
            }

            if(!string.IsNullOrWhiteSpace(_filename))
                File.WriteAllText(_filename,_scintillaTemplate.Text);
        }

        private void SaveAs()
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Imaging Template|*.it";

            if (sfd.ShowDialog() == DialogResult.OK)
                _filename = sfd.FileName;
        }

        private bool Check()
        {
            try
            {
                var oldLine = _scintillaTemplate.CurrentPosition;
                var collection = ImageTableTemplateCollection.LoadFrom(_scintillaTemplate.Text);
                var text = collection.Serialize();
                _scintillaTemplate.Text = text;

                _scintillaTemplate.GotoPosition(oldLine);

                StringBuilder sb = new StringBuilder();

                var helper = ImplementationManager.GetImplementation(collection.DatabaseType).GetServerHelper();
                var server = new DiscoveredServer(helper.GetConnectionStringBuilder("localhost", "MyDatabase",null,null).ConnectionString,collection.DatabaseType);
                var db = server.ExpectDatabase("MyDatabase");

                tcDatagrids.Controls.Clear();

                foreach (ImageTableTemplate template in collection.Tables)
                {
                    TabPage tp = new TabPage(template.TableName);
                    tcDatagrids.Controls.Add(tp);

                    var dg = new DataGrid();
                    dg.Dock = DockStyle.Fill;
                    tp.Controls.Add(dg);
                    
                    sb.AppendLine(db.Helper.GetCreateTableSql(db, template.TableName, template.GetColumns(collection.DatabaseType), null, false));

                    if(olvDicoms.Objects != null)
                    {
                        DataTable dtAll = new DataTable();

                        foreach (var col in template.Columns)
                            dtAll.Columns.Add(col.ColumnName);

                        foreach (FileInfo fi in olvDicoms.Objects)
                        {
                            var dicom = DicomFile.Open(fi.FullName);

                            var dr = dtAll.Rows.Add();

                            foreach (DicomItem item in dicom.Dataset)
                            {
                                var colName = DicomTypeTranslaterReader.GetColumnNameForTag(item.Tag,false);
                                
                                if(dtAll.Columns.Contains(colName))
                                    dr[colName] = DicomTypeTranslaterReader.GetCSharpValue(dicom.Dataset, item);
                            }
                        }
                        
                        dg.DataSource = dtAll;
                    }
                }

                _scintillaSql.ReadOnly = false;
                _scintillaSql.Text = sb.ToString();
                _scintillaSql.ReadOnly = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void olvDicoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fi = olvDicoms.SelectedObject as FileInfo;

            olvFileTags.ClearObjects();
            
            if (fi != null)
            {
                try
                {
                    var dicom = DicomFile.Open(fi.FullName);

                    foreach (DicomItem item in dicom.Dataset)
                    {
                        var value = DicomTypeTranslaterReader.GetCSharpValue(dicom.Dataset, item);
                    
                        olvFileTags.AddObject(new TagValueNode(item.Tag, value));
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void olvDicoms_ItemActivate(object sender, EventArgs e)
        {
            if(olvDicoms.SelectedObject is FileInfo fi && !string.IsNullOrEmpty(fi.DirectoryName))
                Process.Start(fi.DirectoryName);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            olvFileTags.ModelFilter = new TextMatchFilter(olvFileTags,tbFilter.Text);
            olvFileTags.UseFiltering = !string.IsNullOrWhiteSpace(tbFilter.Text);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void findTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/HicServices/DicomTypeTranslation/tree/develop/Templates");
        }
    }

    internal class TagValueNode
    {
        public string Tag { get; set; }
        public object Value { get; set; }

        public TagValueNode(DicomTag tag, object value)
        {
            Tag = tag.DictionaryEntry.Keyword;
            Value = value;
        }

    }
}
