namespace TemplateBuilder
{
    partial class DicomFileTagsUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.olvFileTags = new BrightIdeasSoftware.ObjectListView();
            this.olvTag = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pTags = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Filter:";
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(41, 263);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(398, 20);
            this.tbFilter.TabIndex = 8;
            this.tbFilter.TextChanged += new System.EventHandler(this.tbFilter_TextChanged);
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
            this.olvFileTags.FullRowSelect = true;
            this.olvFileTags.HideSelection = false;
            this.olvFileTags.IsSimpleDragSource = true;
            this.olvFileTags.IsSimpleDropSink = true;
            this.olvFileTags.Location = new System.Drawing.Point(3, 3);
            this.olvFileTags.Name = "olvFileTags";
            this.olvFileTags.ShowGroups = false;
            this.olvFileTags.Size = new System.Drawing.Size(436, 255);
            this.olvFileTags.TabIndex = 6;
            this.olvFileTags.UseCompatibleStateImageBehavior = false;
            this.olvFileTags.View = System.Windows.Forms.View.Details;
            this.olvFileTags.ItemActivate += new System.EventHandler(this.olvFileTags_ItemActivate);
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
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(442, 289);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pTags
            // 
            this.pTags.Controls.Add(this.olvFileTags);
            this.pTags.Controls.Add(this.label3);
            this.pTags.Controls.Add(this.tbFilter);
            this.pTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTags.Location = new System.Drawing.Point(0, 0);
            this.pTags.Name = "pTags";
            this.pTags.Size = new System.Drawing.Size(442, 285);
            this.pTags.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pTags);
            this.splitContainer1.Size = new System.Drawing.Size(442, 578);
            this.splitContainer1.SplitterDistance = 289;
            this.splitContainer1.TabIndex = 13;
            // 
            // DicomFileTagsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DicomFileTagsUI";
            this.Size = new System.Drawing.Size(442, 578);
            ((System.ComponentModel.ISupportInitialize)(this.olvFileTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pTags.ResumeLayout(false);
            this.pTags.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilter;
        private BrightIdeasSoftware.ObjectListView olvFileTags;
        private BrightIdeasSoftware.OLVColumn olvTag;
        private BrightIdeasSoftware.OLVColumn olvValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pTags;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
