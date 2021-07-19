using BrightIdeasSoftware;
using Dicom;
using DicomTypeTranslation;
using DicomTypeTranslation.TableCreation;
using FAnsi.Discovery;
using FAnsi.Implementation;
using FAnsi.Implementations.MicrosoftSQL;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dicom.Imaging;
using FAnsi.Implementations.MySql;
using FAnsi.Implementations.Oracle;
using FAnsi.Implementations.PostgreSql;
using WeifenLuo.WinFormsUI.Docking;
using DatabaseType = FAnsi.DatabaseType;
using System.Runtime.InteropServices;

namespace TemplateBuilder
{
    public partial class Form1 : Form
    {
        private string _filename;
        private Scintilla _scintillaTemplate;
        private Scintilla _scintillaSql;
        bool _setupFinished = false;


        DockContent dcDicoms = new DockContent {HideOnClose = true};
        DockContent dcSql = new DockContent {HideOnClose = true};
        DockContent dcYaml = new DockContent {HideOnClose = true};
        DockContent dcTable = new DockContent {HideOnClose = true};
        
        public Dictionary<DockContent,DockState> DefaultDockLocations { get; set; }

        public Form1()
        {
            InitializeComponent();


            _scintillaTemplate = new Scintilla {AllowDrop = true};
            _scintillaSql = new Scintilla();

            ImplementationManager.Load<MicrosoftSQLImplementation>();
            ImplementationManager.Load<MySqlImplementation>();
            ImplementationManager.Load<OracleImplementation>();
            ImplementationManager.Load<PostgreSqlImplementation>();
            
            ImageManager.SetImplementation(WinFormsImageManager.Instance);

            autoComplete = new List<string>();

            autoComplete.Add("Tables");
            autoComplete.Add("TableName");
            autoComplete.Add("ColumnName");
            autoComplete.Add("AllowNulls");
            autoComplete.Add("IsPrimaryKey");

            foreach (string keyword in DicomDictionary.Default.Select(e => e.Keyword).Distinct())
                autoComplete.Add(keyword);
                     
            ddDatabaseType.ComboBox.DataSource = Enum.GetValues(typeof(DatabaseType));

            var menu = new ContextMenuStrip();
            menu.Items.Add(new ToolStripMenuItem("Navigate To Templates (Online)", null, (s, e) => GoToOnlineTemplates()));

            ContextMenuStrip = menu;

            DefaultDockLocations = new Dictionary<DockContent, DockState>
            {
                {dcTable, DockState.DockBottom},
                {dcDicoms, DockState.DockRight},
                {dcSql, DockState.Document},
                {dcYaml, DockState.Document},
            };


            IsMdiContainer = true;

            dockPanel1.Dock = DockStyle.Fill;
            
            olvDicoms.Dock = DockStyle.Fill;
            dcDicoms.Controls.Add(olvDicoms);
            dcDicoms.TabText = "Dicom Files";

            dockPanel1.Theme = new VS2015BlueTheme();

            dcDicoms.Show(dockPanel1,DefaultDockLocations[dcDicoms]);
            
            
            dcYaml.Controls.Add(_scintillaTemplate);
            _scintillaTemplate.Dock = DockStyle.Fill;
            _scintillaTemplate.CharAdded += _scintillaTemplate_CharAdded;

            dcYaml.TabText = "Template (yaml)";
            dcYaml.Show(dockPanel1, DefaultDockLocations[dcYaml]);

            dcSql.Controls.Add(_scintillaSql);
            _scintillaSql.Dock = DockStyle.Fill;
            dcSql.TabText = "Template (sql)";
            dcSql.Show(dockPanel1, DefaultDockLocations[dcSql]);

            dcTable.Controls.Add(tcDatagrids);
            dcTable.TabText = "Tag Table(s)";
            tcDatagrids.Dock = DockStyle.Fill;
            dcTable.Show(dockPanel1,DefaultDockLocations[dcTable]);

            _scintillaTemplate.DragEnter += Scintilla_OnDragEnter;
            _scintillaTemplate.DragDrop += Scintilla_OnDragDrop;

            _setupFinished = true;
            Check();
        }

        private void _scintillaTemplate_CharAdded(object sender, CharAddedEventArgs e)
        {
            var scintilla = (Scintilla)sender;

            // Find the word start
            var currentPos = scintilla.CurrentPosition;
            var wordStartPos = scintilla.WordStartPosition(currentPos, false);

            // Display the autocompletion list
            var lenEntered = currentPos - wordStartPos;
            if (lenEntered > 0)
            {
                if (!scintilla.AutoCActive)
                    scintilla.AutoCShow(lenEntered, string.Join(' ',autoComplete.OrderBy(a=>a).ToArray()));
            }
        
        }

        private void Scintilla_OnDragEnter(object sender, DragEventArgs dragEventArgs)
        {
            if (!(dragEventArgs.Data is OLVDataObject olv)) return;
            if(olv.ModelObjects.OfType<TagValueNode>().Any())
                dragEventArgs.Effect = DragDropEffects.Copy;
        }
        private void Scintilla_OnDragDrop(object sender, DragEventArgs dragEventArgs)
        {
            //point they are dragged over
            var editor = (Scintilla) sender;

            if (editor.ReadOnly)
                return;

            TagValueNode[] nodes = null; 

            if (dragEventArgs.Data is OLVDataObject olv)
                nodes = olv.ModelObjects.OfType<TagValueNode>().ToArray();

            if(nodes == null || nodes.Length == 0)
                return;

            var clientPoint = editor.PointToClient(new Point(dragEventArgs.X, dragEventArgs.Y));
            //get where the mouse is hovering over
            int pos = editor.CharPositionFromPoint(clientPoint.X, clientPoint.Y);
            
            //if it has a Form give it focus
            var form = editor.FindForm();

            if(form != null)
            {
                form.Activate();
                editor.Focus();
            }
            
            editor.InsertText(pos,string.Join(Environment.NewLine,nodes.Select(n=>n.GetTemplateYaml())));
        }

        private void olvDicoms_CanDrop(object sender, OlvDropEventArgs e)
        {
            var dataObject = (DataObject)e.DataObject;

            e.Effect = dataObject.ContainsFileDropList() ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void olvDicoms_Dropped(object sender, OlvDropEventArgs e)
        {
            var dataObject = (DataObject)e.DataObject;

            if (!dataObject.ContainsFileDropList()) return;
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

            var c = new ImageTableTemplateCollection {DatabaseType = DatabaseType.MicrosoftSQLServer};

            c.Tables.Add(
                new ImageTableTemplate { TableName = "StudyTable", Columns =  
                new[] {
                        new ImageColumnTemplate(DicomTag.StudyInstanceUID){AllowNulls = false,IsPrimaryKey = true},
                        new ImageColumnTemplate(DicomTag.StudyDescription){AllowNulls = true},
                        new ImageColumnTemplate(DicomTag.StudyDate){AllowNulls = true}
                      }
                });

            c.Tables.Add(
                new ImageTableTemplate
                {
                    TableName = "SeriesTable",
                    Columns =
                new[] {
                        new ImageColumnTemplate(DicomTag.StudyInstanceUID){AllowNulls = false },
                        new ImageColumnTemplate(DicomTag.SeriesInstanceUID){AllowNulls = false,IsPrimaryKey = true},
                        new ImageColumnTemplate(DicomTag.SeriesDescription){AllowNulls = true},
                        new ImageColumnTemplate(DicomTag.SeriesDate){AllowNulls = true}
                      }
                });

            c.Tables.Add(
                new ImageTableTemplate
                {
                    TableName = "ImageTable",
                    Columns =
                new[] {
                        new ImageColumnTemplate(DicomTag.SeriesInstanceUID){AllowNulls = false},
                        new ImageColumnTemplate(DicomTag.SOPInstanceUID){AllowNulls = false,IsPrimaryKey = true},
                        new ImageColumnTemplate(DicomTag.ImageType){AllowNulls = true},
                        new ImageColumnTemplate(ImagingTableCreation.GetRelativeFileArchiveURIColumn(false,false))
                      }
                });

            _scintillaTemplate.Text = c.Serialize();

            
            dcYaml.Show(dockPanel1, DefaultDockLocations[dcYaml]);
        }


        private void OpenTemplate()
        {
            using (var ofd = new OpenFileDialog { Filter = "Imaging Template|*.it" })
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(ofd.FileName)) return;
                    _scintillaTemplate.Text = File.ReadAllText(ofd.FileName);
                    _filename = ofd.FileName;
                    Check();
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
            using (var sfd = new SaveFileDialog { Filter = "Imaging Template|*.it" })
                if (sfd.ShowDialog() == DialogResult.OK)
                    _filename = sfd.FileName;
        }

        private bool Check()
        {
            bool noTemplate = string.IsNullOrWhiteSpace(_scintillaTemplate.Text);
            
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

                var helper = ImplementationManager.GetImplementation(dbType).GetServerHelper();
                var server = new DiscoveredServer(helper.GetConnectionStringBuilder("localhost", null,null,null).ConnectionString,dbType);
                var db = server.ExpectDatabase("MyDatabase");

                tcDatagrids.Controls.Clear();

                foreach (ImageTableTemplate template in collection.Tables)
                {
                    TabPage tp = new TabPage(template.TableName);
                    tcDatagrids.Controls.Add(tp);

                    var dg = new DataGridView {Dock = DockStyle.Fill};
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


        private void olvDicoms_ItemActivate(object sender, EventArgs e)
        {
            if (!(olvDicoms.SelectedObject is FileInfo fi))
                return;

            var ui = new DicomFileTagsUI(fi) {Dock = DockStyle.Fill};
            var dc = new DockContent {TabText = fi.Name};
            dockcontents.Add(dc);
            dc.Controls.Add(ui);
            dc.Show(dockPanel1,DockState.DockLeft);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        

        private void GoToOnlineTemplates()
        {
            OpenBrowser("https://github.com/HicServices/DicomTypeTranslation/tree/develop/Templates");
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
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
            using (var ofd = new OpenFileDialog
            {
                Filter = "Dicom Files|*.dcm",
                Multiselect = true
            })
            {
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
        
        private void WindowClicked(object sender, EventArgs e)
        {
            DockContent dc = null;

            if (sender == miWindowTable)
                dc = dcTable;
            if (sender == miWindowFiles)
                dc = dcDicoms;
            if (sender == miWindowSql)
                dc = dcSql;
            if (sender == miWindowYaml)
                dc = dcYaml;

            if(dc != null)
             if (dc.Visible)
                dc.Activate();
             else
                 dc.Show(dockPanel1,DefaultDockLocations[dc]);
        }

        private void miRepopulator_Click(object sender, EventArgs e)
        {
            var ui = new RepopulatorUI {Dock = DockStyle.Fill};

            var dc = new DockContent {Height = ui.MinimumSize.Height, TabText = "Repopulator"};
            dockcontents.Add(dc);
            dc.Controls.Add(ui);
            dc.Show(dockPanel1,DockState.Document);
        }
        
        private void miNewTemplate_Click(object sender, EventArgs e)
        {
            NewTemplate();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        public string GetTemplateYaml()
        {
            return $@"
  - ColumnName: {Tag}
    AllowNulls: true";
        }

    }
}
