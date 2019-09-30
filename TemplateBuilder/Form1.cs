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
using System.Windows.Forms;
using FAnsi.Implementations.MySql;
using FAnsi.Implementations.Oracle;
using DatabaseType = FAnsi.DatabaseType;

namespace TemplateBuilder
{
    public partial class Form1 : Form
    {
        private string _filename;
        private Scintilla _scintillaTemplate;
        private Scintilla _scintillaSql;
        bool _setupFinished = false;
        private ToolStripMenuItem miOpenDicoms;
        private ToolStripMenuItem miOpenTemplate;
        private ToolStripMenuItem miSaveTemplate;
        private ToolStripMenuItem miSaveAsTemplate;
        private ToolStripMenuItem miNewTemplate;

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
            ImplementationManager.Load<MySqlImplementation>();
            ImplementationManager.Load<OracleImplementation>();

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


            ddDatabaseType.ComboBox.DataSource = Enum.GetValues(typeof(DatabaseType));

            var menu = new ContextMenuStrip();
            miOpenDicoms = new ToolStripMenuItem("Open Dicom", null, (s, e) => OpenDicoms())
                {ShortcutKeys = Keys.Control | Keys.Shift | Keys.O};

            miNewTemplate =  new ToolStripMenuItem("New (empty) template",null,(s,e)=>NewTemplate()){ShortcutKeys = Keys.Control | Keys.N};
            miOpenTemplate = new ToolStripMenuItem("Open Template",null,(s,e)=>OpenTemplate()){ShortcutKeys = Keys.Control | Keys.O};
            miSaveTemplate = new ToolStripMenuItem("Save", null, (s, e) => Save()) {ShortcutKeys = Keys.Control | Keys.S};
            miSaveAsTemplate = new ToolStripMenuItem("Save As", null, (s, e) => SaveAs()){ShortcutKeys = Keys.Control | Keys.Shift | Keys.S};

            menu.Items.Add(miOpenDicoms);
            menu.Items.Add(miNewTemplate);
            menu.Items.Add(miOpenTemplate);
            menu.Items.Add(new ToolStripMenuItem("Navigate To Templates (Online)", null, (s, e) => GoToOnlineTemplates()));
            menu.Items.Add(miSaveTemplate);
            menu.Items.Add(miSaveAsTemplate);

            ContextMenuStrip = menu;
            
            _setupFinished = true;
            Check();
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

        private void NewTemplate()
        {
            _filename = null;
            _scintillaTemplate.ClearAll();

            var c = new ImageTableTemplateCollection();

            c.DatabaseType = DatabaseType.MicrosoftSQLServer;
            c.Tables.Add(new ImageTableTemplate
            {
                TableName = "MyTable",
                Columns = 
                new[]
            {
                new ImageColumnTemplate(DicomTag.SOPInstanceUID){AllowNulls = false,IsPrimaryKey = true},
                new ImageColumnTemplate(ImagingTableCreation.GetRelativeFileArchiveURIColumn(false,false))
            } });

            _scintillaTemplate.Text = c.Serialize();
        }


        private void OpenTemplate()
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

        private void Save()
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
            bool noTemplate = string.IsNullOrWhiteSpace(_scintillaTemplate.Text);

            btnAddDicom.Enabled = !noTemplate;
            miOpenDicoms.Enabled = !noTemplate;
            
            if (noTemplate)
                return false;
            
            try
            {
                var dbType = (DatabaseType) ddDatabaseType.ComboBox.SelectedItem;
                var oldLine = _scintillaTemplate.CurrentPosition;
                var collection = ImageTableTemplateCollection.LoadFrom(_scintillaTemplate.Text);
                var text = collection.Serialize();
                _scintillaTemplate.Text = text;

                _scintillaTemplate.GotoPosition(oldLine);

                StringBuilder sb = new StringBuilder();

                var helper = ImplementationManager.GetImplementation(collection.DatabaseType).GetServerHelper();
                var server = new DiscoveredServer(helper.GetConnectionStringBuilder("localhost", "MyDatabase",null,null).ConnectionString,dbType);
                var db = server.ExpectDatabase("MyDatabase");

                tcDatagrids.Controls.Clear();

                foreach (ImageTableTemplate template in collection.Tables)
                {
                    TabPage tp = new TabPage(template.TableName);
                    tcDatagrids.Controls.Add(tp);

                    var dg = new DataGrid();
                    dg.Dock = DockStyle.Fill;
                    tp.Controls.Add(dg);
                    
                    sb.AppendLine(db.Helper.GetCreateTableSql(db, template.TableName, template.GetColumns(dbType), null, false));

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
                                    dr[colName] = DicomTypeTranslater.Flatten(DicomTypeTranslaterReader.GetCSharpValue(dicom.Dataset, item));
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
            if(!_setupFinished)
                return;

            var fi = olvDicoms.SelectedObject as FileInfo;

            olvFileTags.ClearObjects();
            
            if (fi != null)
            {
                try
                {
                    var dicom = DicomFile.Open(fi.FullName);

                    foreach (DicomItem item in dicom.Dataset)
                    {
                        var value = DicomTypeTranslater.Flatten(DicomTypeTranslaterReader.GetCSharpValue(dicom.Dataset, item));
                    
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


        private void GoToOnlineTemplates()
        {
            Process.Start("https://github.com/HicServices/DicomTypeTranslation/tree/develop/Templates");
        }

        private void ddDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_setupFinished)
                Check();
        }

        private void openTemplate_Click(object sender, EventArgs e)
        {
            OpenTemplate();
        }

        private void btnAddDicom_Click(object sender, EventArgs e)
        {
            OpenDicoms();
        }


        private void OpenDicoms()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Dicom Files|*.dcm";
            ofd.Multiselect = true;

            
            

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _setupFinished = false;
                    olvDicoms.BeginUpdate();

                    foreach (var f in ofd.FileNames)
                    {
                        var fi = new FileInfo(f);
                        if (fi.Exists)
                            olvDicoms.AddObject(fi);
                    }
                }
                finally
                {
                    olvDicoms.EndUpdate();
                    _setupFinished = true;
                }
                
                Check();
            }

            
        }

        private void btnOnlineTemplates_Click(object sender, EventArgs e)
        {
            GoToOnlineTemplates();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewTemplate();
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
