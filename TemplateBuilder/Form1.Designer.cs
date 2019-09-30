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
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpEditor = new System.Windows.Forms.TabPage();
            this.tpSql = new System.Windows.Forms.TabPage();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.olvFileTags = new BrightIdeasSoftware.ObjectListView();
            this.olvTag = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tcDatagrids = new System.Windows.Forms.TabControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
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
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.olvDicoms.Location = new System.Drawing.Point(6, 22);
            this.olvDicoms.Name = "olvDicoms";
            this.olvDicoms.ShowGroups = false;
            this.olvDicoms.Size = new System.Drawing.Size(354, 187);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Example Dicom Files (Drop Files Here)";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbFilter);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.olvFileTags);
            this.splitContainer1.Panel2.Controls.Add(this.olvDicoms);
            this.splitContainer1.Size = new System.Drawing.Size(1205, 482);
            this.splitContainer1.SplitterDistance = 838;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpEditor);
            this.tabControl1.Controls.Add(this.tpSql);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(838, 482);
            this.tabControl1.TabIndex = 0;
            // 
            // tpEditor
            // 
            this.tpEditor.Location = new System.Drawing.Point(4, 22);
            this.tpEditor.Name = "tpEditor";
            this.tpEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditor.Size = new System.Drawing.Size(830, 456);
            this.tpEditor.TabIndex = 0;
            this.tpEditor.Text = "Template";
            this.tpEditor.UseVisualStyleBackColor = true;
            // 
            // tpSql
            // 
            this.tpSql.Location = new System.Drawing.Point(4, 22);
            this.tpSql.Name = "tpSql";
            this.tpSql.Padding = new System.Windows.Forms.Padding(3);
            this.tpSql.Size = new System.Drawing.Size(830, 456);
            this.tpSql.TabIndex = 1;
            this.tpSql.Text = "Sql";
            this.tpSql.UseVisualStyleBackColor = true;
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(44, 458);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(310, 20);
            this.tbFilter.TabIndex = 5;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 461);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selected File Tags";
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
            this.olvFileTags.Location = new System.Drawing.Point(9, 228);
            this.olvFileTags.Name = "olvFileTags";
            this.olvFileTags.ShowGroups = false;
            this.olvFileTags.Size = new System.Drawing.Size(354, 224);
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
            this.tcDatagrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDatagrids.Location = new System.Drawing.Point(0, 0);
            this.tcDatagrids.Name = "tcDatagrids";
            this.tcDatagrids.SelectedIndex = 0;
            this.tcDatagrids.Size = new System.Drawing.Size(1205, 333);
            this.tcDatagrids.TabIndex = 7;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tcDatagrids);
            this.splitContainer2.Size = new System.Drawing.Size(1205, 819);
            this.splitContainer2.SplitterDistance = 482;
            this.splitContainer2.TabIndex = 8;
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
            this.ddDatabaseType});
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 844);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvDicoms)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ObjectListView olvDicoms;
        private OLVColumn olvName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpEditor;
        private System.Windows.Forms.TabPage tpSql;
        private System.Windows.Forms.TabControl tcDatagrids;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
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
    }
}

