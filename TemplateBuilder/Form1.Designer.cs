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
            this.tcDatagrids = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOnlineTemplates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ddDatabaseType = new System.Windows.Forms.ToolStripComboBox();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miNew = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenDicoms = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAsTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindowYaml = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindowSql = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindowFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.miWindowTable = new System.Windows.Forms.ToolStripMenuItem();
            this.miRepopulator = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            // 
            // olvName
            // 
            this.olvName.AspectName = "Name";
            this.olvName.FillsFreeSpace = true;
            this.olvName.Text = "File";
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
            this.btnOnlineTemplates,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.ddDatabaseType});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1205, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
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
            this.ddDatabaseType.Name = "ddDatabaseType";
            this.ddDatabaseType.Size = new System.Drawing.Size(160, 25);
            this.ddDatabaseType.SelectedIndexChanged += new System.EventHandler(this.ddDatabaseType_SelectedIndexChanged);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Location = new System.Drawing.Point(636, 147);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(200, 100);
            this.dockPanel1.TabIndex = 11;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1205, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.miSaveTemplate,
            this.miSaveAsTemplate});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // miNew
            // 
            this.miNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewTemplate});
            this.miNew.Name = "miNew";
            this.miNew.Size = new System.Drawing.Size(195, 22);
            this.miNew.Text = "New";
            // 
            // miNewTemplate
            // 
            this.miNewTemplate.Name = "miNewTemplate";
            this.miNewTemplate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNewTemplate.Size = new System.Drawing.Size(166, 22);
            this.miNewTemplate.Text = "Template";
            // 
            // miOpen
            // 
            this.miOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenTemplate,
            this.miOpenDicoms});
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(195, 22);
            this.miOpen.Text = "Open";
            // 
            // miOpenTemplate
            // 
            this.miOpenTemplate.Name = "miOpenTemplate";
            this.miOpenTemplate.Size = new System.Drawing.Size(180, 22);
            this.miOpenTemplate.Text = "Template..";
            this.miOpenTemplate.Click += new System.EventHandler(this.openTemplate_Click);
            // 
            // miOpenDicoms
            // 
            this.miOpenDicoms.Name = "miOpenDicoms";
            this.miOpenDicoms.Size = new System.Drawing.Size(180, 22);
            this.miOpenDicoms.Text = "Dicom File(s)...";
            this.miOpenDicoms.Click += new System.EventHandler(this.btnAddDicom_Click);
            // 
            // miSaveTemplate
            // 
            this.miSaveTemplate.Image = ((System.Drawing.Image)(resources.GetObject("miSaveTemplate.Image")));
            this.miSaveTemplate.Name = "miSaveTemplate";
            this.miSaveTemplate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSaveTemplate.Size = new System.Drawing.Size(195, 22);
            this.miSaveTemplate.Text = "Save";
            this.miSaveTemplate.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // miSaveAsTemplate
            // 
            this.miSaveAsTemplate.Name = "miSaveAsTemplate";
            this.miSaveAsTemplate.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.miSaveAsTemplate.Size = new System.Drawing.Size(195, 22);
            this.miSaveAsTemplate.Text = "Save As...";
            this.miSaveAsTemplate.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miWindowYaml,
            this.miWindowSql,
            this.miWindowFiles,
            this.miWindowTable,
            this.miRepopulator});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // miWindowYaml
            // 
            this.miWindowYaml.Name = "miWindowYaml";
            this.miWindowYaml.Size = new System.Drawing.Size(180, 22);
            this.miWindowYaml.Text = "Template Yaml";
            this.miWindowYaml.Click += new System.EventHandler(this.WindowClicked);
            // 
            // miWindowSql
            // 
            this.miWindowSql.Name = "miWindowSql";
            this.miWindowSql.Size = new System.Drawing.Size(180, 22);
            this.miWindowSql.Text = "Template Sql";
            this.miWindowSql.Click += new System.EventHandler(this.WindowClicked);
            // 
            // miWindowFiles
            // 
            this.miWindowFiles.Name = "miWindowFiles";
            this.miWindowFiles.Size = new System.Drawing.Size(180, 22);
            this.miWindowFiles.Text = "Dicom Files";
            this.miWindowFiles.Click += new System.EventHandler(this.WindowClicked);
            // 
            // miWindowTable
            // 
            this.miWindowTable.Name = "miWindowTable";
            this.miWindowTable.Size = new System.Drawing.Size(180, 22);
            this.miWindowTable.Text = "Table View";
            this.miWindowTable.Click += new System.EventHandler(this.WindowClicked);
            // 
            // miRepopulator
            // 
            this.miRepopulator.Name = "miRepopulator";
            this.miRepopulator.Size = new System.Drawing.Size(180, 22);
            this.miRepopulator.Text = "Tag Populator";
            this.miRepopulator.Click += new System.EventHandler(this.miRepopulator_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 844);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.olvDicoms);
            this.Controls.Add(this.tcDatagrids);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ObjectListView olvDicoms;
        private OLVColumn olvName;
        private System.Windows.Forms.TabControl tcDatagrids;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox ddDatabaseType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnOnlineTemplates;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripMenuItem miOpenTemplate;
        private System.Windows.Forms.ToolStripMenuItem miOpenDicoms;
        private System.Windows.Forms.ToolStripMenuItem miSaveTemplate;
        private System.Windows.Forms.ToolStripMenuItem miSaveAsTemplate;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miNewTemplate;
        private System.Windows.Forms.ToolStripMenuItem miWindowYaml;
        private System.Windows.Forms.ToolStripMenuItem miWindowSql;
        private System.Windows.Forms.ToolStripMenuItem miWindowFiles;
        private System.Windows.Forms.ToolStripMenuItem miWindowTable;
        private System.Windows.Forms.ToolStripMenuItem miRepopulator;
    }
}

