using BrightIdeasSoftware;

namespace TemplateBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.olvDicoms = new BrightIdeasSoftware.ObjectListView();
            this.olvName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.olvFileTags = new BrightIdeasSoftware.ObjectListView();
            this.olvTag = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tcDatagrids = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddDicom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenTemplate = new System.Windows.Forms.ToolStripButton();
            this.btnOnlineTemplates = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnNewTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ddDatabaseType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.templateyamlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pFileTags = new System.Windows.Forms.Panel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.pFileTags.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvDicoms
            // 
            this.olvDicoms.AllColumns.Add(this.olvName);
            this.olvDicoms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvDicoms.CellEditUseWholeCell = false;
            this.olvDicoms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvName});
            this.olvDicoms.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvDicoms.FullRowSelect = true;
            this.olvDicoms.HideSelection = false;
            this.olvDicoms.IsSimpleDropSink = true;
            this.olvDicoms.Location = new System.Drawing.Point(131, 128);
            this.olvDicoms.Name = "olvDicoms";
            this.olvDicoms.ShowGroups = false;
            this.olvDicoms.Size = new System.Drawing.Size(372, 225);
            this.olvDicoms.TabIndex = 1;
            this.olvDicoms.UseCompatibleStateImageBehavior = false;
            this.olvDicoms.View = System.Windows.Forms.View.Details;
            this.olvDicoms.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.olvDicoms_CanDrop);
            this.olvDicoms.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.olvDicoms_Dropped);
            this.olvDicoms.ItemActivate += new System.EventHandler(this.olvDicoms_ItemActivate);
            this.olvDicoms.SelectedIndexChanged += new System.EventHandler(this.olvDicoms_SelectedIndexChanged);
            // 
            // olvName
            // 
            this.olvName.AspectName = "Name";
            this.olvName.FillsFreeSpace = true;
            this.olvName.Text = "File";
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(41, 230);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(387, 20);
            this.tbFilter.TabIndex = 5;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter:";
            // 
            // olvFileTags
            // 
            this.olvFileTags.AllColumns.Add(this.olvTag);
            this.olvFileTags.AllColumns.Add(this.olvValue);
            this.olvFileTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvFileTags.CellEditUseWholeCell = false;
            this.olvFileTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTag,
            this.olvValue});
            this.olvFileTags.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFileTags.HideSelection = false;
            this.olvFileTags.IsSimpleDragSource = true;
            this.olvFileTags.IsSimpleDropSink = true;
            this.olvFileTags.Location = new System.Drawing.Point(0, 0);
            this.olvFileTags.Name = "olvFileTags";
            this.olvFileTags.ShowGroups = false;
            this.olvFileTags.Size = new System.Drawing.Size(431, 224);
            this.olvFileTags.TabIndex = 1;
            this.olvFileTags.UseCompatibleStateImageBehavior = false;
            this.olvFileTags.View = System.Windows.Forms.View.Details;
            this.olvFileTags.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.olvDicoms_CanDrop);
            this.olvFileTags.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.olvDicoms_Dropped);
            // 
            // olvTag
            // 
            this.olvTag.AspectName = "Tag";
            this.olvTag.Text = "Tag";
            this.olvTag.Width = 100;
            // 
            // olvValue
            // 
            this.olvValue.AspectName = "Value";
            this.olvValue.FillsFreeSpace = true;
            this.olvValue.Text = "Value";
            this.olvValue.Width = 100;
            // 
            // tcDatagrids
            // 
            this.tcDatagrids.Location = new System.Drawing.Point(60, 413);
            this.tcDatagrids.Name = "tcDatagrids";
            this.tcDatagrids.SelectedIndex = 0;
            this.tcDatagrids.Size = new System.Drawing.Size(1205, 333);
            this.tcDatagrids.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddDicom,
            this.toolStripSeparator2,
            this.btnOpenTemplate,
            this.btnOnlineTemplates,
            this.btnSave,
            this.btnSaveAs,
            this.btnNewTemplate,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.ddDatabaseType,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1205, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddDicom
            // 
            this.btnAddDicom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddDicom.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDicom.Image")));
            this.btnAddDicom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddDicom.Name = "btnAddDicom";
            this.btnAddDicom.Size = new System.Drawing.Size(23, 22);
            this.btnAddDicom.Text = "Open Dicom Files";
            this.btnAddDicom.Click += new System.EventHandler(this.btnAddDicom_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOpenTemplate
            // 
            this.btnOpenTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTemplate.Image")));
            this.btnOpenTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenTemplate.Name = "btnOpenTemplate";
            this.btnOpenTemplate.Size = new System.Drawing.Size(23, 22);
            this.btnOpenTemplate.Text = "Open Template";
            this.btnOpenTemplate.Click += new System.EventHandler(this.openTemplate_Click);
            // 
            // btnOnlineTemplates
            // 
            this.btnOnlineTemplates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOnlineTemplates.Image = ((System.Drawing.Image)(resources.GetObject("btnOnlineTemplates.Image")));
            this.btnOnlineTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOnlineTemplates.Name = "btnOnlineTemplates";
            this.btnOnlineTemplates.Size = new System.Drawing.Size(23, 22);
            this.btnOnlineTemplates.Text = "Go to online templates";
            this.btnOnlineTemplates.Click += new System.EventHandler(this.btnOnlineTemplates_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnNewTemplate
            // 
            this.btnNewTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTemplate.Image")));
            this.btnNewTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewTemplate.Name = "btnNewTemplate";
            this.btnNewTemplate.Size = new System.Drawing.Size(23, 22);
            this.btnNewTemplate.Text = "New (empty) template";
            this.btnNewTemplate.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel1.Text = "Database Type:";
            // 
            // ddDatabaseType
            // 
            this.ddDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddDatabaseType.Name = "ddDatabaseType";
            this.ddDatabaseType.Size = new System.Drawing.Size(121, 25);
            this.ddDatabaseType.SelectedIndexChanged += new System.EventHandler(this.ddDatabaseType_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateyamlToolStripMenuItem,
            this.templateSqlToolStripMenuItem,
            this.fileListToolStripMenuItem,
            this.dataGridToolStripMenuItem,
            this.pixelViewerToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // templateyamlToolStripMenuItem
            // 
            this.templateyamlToolStripMenuItem.Name = "templateyamlToolStripMenuItem";
            this.templateyamlToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.templateyamlToolStripMenuItem.Text = "Template (yaml)";
            // 
            // templateSqlToolStripMenuItem
            // 
            this.templateSqlToolStripMenuItem.Name = "templateSqlToolStripMenuItem";
            this.templateSqlToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.templateSqlToolStripMenuItem.Text = "Template (sql)";
            // 
            // fileListToolStripMenuItem
            // 
            this.fileListToolStripMenuItem.Name = "fileListToolStripMenuItem";
            this.fileListToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.fileListToolStripMenuItem.Text = "Dicom File List";
            this.fileListToolStripMenuItem.Click += new System.EventHandler(this.fileListToolStripMenuItem_Click);
            // 
            // dataGridToolStripMenuItem
            // 
            this.dataGridToolStripMenuItem.Name = "dataGridToolStripMenuItem";
            this.dataGridToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.dataGridToolStripMenuItem.Text = "Table Viewer";
            this.dataGridToolStripMenuItem.Click += new System.EventHandler(this.dataGridToolStripMenuItem_Click);
            // 
            // pixelViewerToolStripMenuItem
            // 
            this.pixelViewerToolStripMenuItem.Name = "pixelViewerToolStripMenuItem";
            this.pixelViewerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.pixelViewerToolStripMenuItem.Text = "Pixel Viewer";
            // 
            // pFileTags
            // 
            this.pFileTags.Controls.Add(this.label3);
            this.pFileTags.Controls.Add(this.tbFilter);
            this.pFileTags.Controls.Add(this.olvFileTags);
            this.pFileTags.Location = new System.Drawing.Point(567, 114);
            this.pFileTags.Name = "pFileTags";
            this.pFileTags.Size = new System.Drawing.Size(431, 253);
            this.pFileTags.TabIndex = 10;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Location = new System.Drawing.Point(12, 28);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(200, 100);
            this.dockPanel1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 844);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.olvDicoms);
            this.Controls.Add(this.pFileTags);
            this.Controls.Add(this.tcDatagrids);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pFileTags.ResumeLayout(false);
            this.pFileTags.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ObjectListView olvDicoms;
        private OLVColumn olvName;
        private System.Windows.Forms.TabControl tcDatagrids;
        private ObjectListView olvFileTags;
        private OLVColumn olvTag;
        private OLVColumn olvValue;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddDicom;
        private System.Windows.Forms.ToolStripComboBox ddDatabaseType;
        private System.Windows.Forms.ToolStripButton btnOpenTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOnlineTemplates;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripButton btnNewTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem templateyamlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelViewerToolStripMenuItem;
        private System.Windows.Forms.Panel pFileTags;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
    }
}

