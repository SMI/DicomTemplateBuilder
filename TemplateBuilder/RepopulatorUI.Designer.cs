namespace TemplateBuilder
{
    partial class RepopulatorUI
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
            this.components = new System.ComponentModel.Container();
            this.btnInputFolder = new System.Windows.Forms.Button();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.cbIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.tbPattern = new System.Windows.Forms.TextBox();
            this.tbInputFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nThreads = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbInputCsv = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInputCsv = new System.Windows.Forms.Button();
            this.btnValidateCsv = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFilenameColumn = new System.Windows.Forms.TextBox();
            this.cbAnonymise = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbErrors = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExtraMappings = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbExtraMappings = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nThreads)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInputFolder
            // 
            this.btnInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputFolder.Location = new System.Drawing.Point(671, 28);
            this.btnInputFolder.Name = "btnInputFolder";
            this.btnInputFolder.Size = new System.Drawing.Size(67, 23);
            this.btnInputFolder.TabIndex = 1;
            this.btnInputFolder.Text = "Browse...";
            this.btnInputFolder.UseVisualStyleBackColor = true;
            this.btnInputFolder.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputFolder.Location = new System.Drawing.Point(671, 158);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(67, 23);
            this.btnOutputFolder.TabIndex = 10;
            this.btnOutputFolder.Text = "Browse";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbIncludeSubfolders
            // 
            this.cbIncludeSubfolders.AutoSize = true;
            this.cbIncludeSubfolders.Checked = true;
            this.cbIncludeSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncludeSubfolders.Location = new System.Drawing.Point(96, 56);
            this.cbIncludeSubfolders.Name = "cbIncludeSubfolders";
            this.cbIncludeSubfolders.Size = new System.Drawing.Size(120, 17);
            this.cbIncludeSubfolders.TabIndex = 2;
            this.cbIncludeSubfolders.Text = "Include Sub Folders";
            this.cbIncludeSubfolders.UseVisualStyleBackColor = true;
            this.cbIncludeSubfolders.CheckedChanged += new System.EventHandler(this.cbIncludeSubfolders_CheckedChanged);
            // 
            // tbPattern
            // 
            this.tbPattern.Location = new System.Drawing.Point(291, 56);
            this.tbPattern.Name = "tbPattern";
            this.tbPattern.Size = new System.Drawing.Size(100, 20);
            this.tbPattern.TabIndex = 4;
            this.tbPattern.Text = "*.dcm";
            this.tbPattern.TextChanged += new System.EventHandler(this.tbPattern_TextChanged);
            // 
            // tbInputFolder
            // 
            this.tbInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbInputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbInputFolder.Location = new System.Drawing.Point(88, 30);
            this.tbInputFolder.Name = "tbInputFolder";
            this.tbInputFolder.Size = new System.Drawing.Size(577, 20);
            this.tbInputFolder.TabIndex = 0;
            this.tbInputFolder.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input Folder:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output Folder:";
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbOutputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbOutputFolder.Location = new System.Drawing.Point(88, 160);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(577, 20);
            this.tbOutputFolder.TabIndex = 9;
            this.tbOutputFolder.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File Pattern:";
            // 
            // nThreads
            // 
            this.nThreads.Location = new System.Drawing.Point(452, 57);
            this.nThreads.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nThreads.Name = "nThreads";
            this.nThreads.Size = new System.Drawing.Size(111, 20);
            this.nThreads.TabIndex = 6;
            this.nThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nThreads.ValueChanged += new System.EventHandler(this.nThreads_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(397, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Threads:";
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(5, 211);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(58, 13);
            this.lblProgress.TabIndex = 9;
            this.lblProgress.Text = "lblProgress";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(219, 186);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(67, 23);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbInputCsv
            // 
            this.tbInputCsv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputCsv.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbInputCsv.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbInputCsv.Location = new System.Drawing.Point(88, 105);
            this.tbInputCsv.Name = "tbInputCsv";
            this.tbInputCsv.Size = new System.Drawing.Size(577, 20);
            this.tbInputCsv.TabIndex = 7;
            this.tbInputCsv.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Input Csv:";
            // 
            // btnInputCsv
            // 
            this.btnInputCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputCsv.Location = new System.Drawing.Point(671, 103);
            this.btnInputCsv.Name = "btnInputCsv";
            this.btnInputCsv.Size = new System.Drawing.Size(67, 23);
            this.btnInputCsv.TabIndex = 8;
            this.btnInputCsv.Text = "Browse...";
            this.btnInputCsv.UseVisualStyleBackColor = true;
            this.btnInputCsv.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnValidateCsv
            // 
            this.btnValidateCsv.Location = new System.Drawing.Point(88, 186);
            this.btnValidateCsv.Name = "btnValidateCsv";
            this.btnValidateCsv.Size = new System.Drawing.Size(125, 23);
            this.btnValidateCsv.TabIndex = 11;
            this.btnValidateCsv.Text = "Validate Csv Headers";
            this.btnValidateCsv.UseVisualStyleBackColor = true;
            this.btnValidateCsv.Click += new System.EventHandler(this.btnValidateCsv_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(93, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Filename Column";
            // 
            // tbFilenameColumn
            // 
            this.tbFilenameColumn.Location = new System.Drawing.Point(186, 79);
            this.tbFilenameColumn.Name = "tbFilenameColumn";
            this.tbFilenameColumn.Size = new System.Drawing.Size(205, 20);
            this.tbFilenameColumn.TabIndex = 4;
            this.tbFilenameColumn.TextChanged += new System.EventHandler(this.tbFilenameColumn_TextChanged);
            // 
            // cbAnonymise
            // 
            this.cbAnonymise.AutoSize = true;
            this.cbAnonymise.Location = new System.Drawing.Point(400, 81);
            this.cbAnonymise.Name = "cbAnonymise";
            this.cbAnonymise.Size = new System.Drawing.Size(207, 17);
            this.cbAnonymise.TabIndex = 13;
            this.cbAnonymise.Text = "Anonymise (with FoDicom Anonymizer)";
            this.cbAnonymise.UseVisualStyleBackColor = true;
            this.cbAnonymise.CheckedChanged += new System.EventHandler(this.cbAnonymise_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tbErrors);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tbDone);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 227);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 23);
            this.panel1.TabIndex = 14;
            // 
            // tbErrors
            // 
            this.tbErrors.Location = new System.Drawing.Point(264, 0);
            this.tbErrors.Name = "tbErrors";
            this.tbErrors.ReadOnly = true;
            this.tbErrors.Size = new System.Drawing.Size(166, 20);
            this.tbErrors.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(221, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Errors:";
            // 
            // tbDone
            // 
            this.tbDone.Location = new System.Drawing.Point(45, 0);
            this.tbDone.Name = "tbDone";
            this.tbDone.ReadOnly = true;
            this.tbDone.Size = new System.Drawing.Size(166, 20);
            this.tbDone.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Done:";
            // 
            // btnExtraMappings
            // 
            this.btnExtraMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtraMappings.Location = new System.Drawing.Point(671, 132);
            this.btnExtraMappings.Name = "btnExtraMappings";
            this.btnExtraMappings.Size = new System.Drawing.Size(67, 23);
            this.btnExtraMappings.TabIndex = 17;
            this.btnExtraMappings.Text = "Browse...";
            this.btnExtraMappings.UseVisualStyleBackColor = true;
            this.btnExtraMappings.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Extra Mappings:";
            // 
            // tbExtraMappings
            // 
            this.tbExtraMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraMappings.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbExtraMappings.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbExtraMappings.Location = new System.Drawing.Point(88, 134);
            this.tbExtraMappings.Name = "tbExtraMappings";
            this.tbExtraMappings.Size = new System.Drawing.Size(577, 20);
            this.tbExtraMappings.TabIndex = 16;
            this.tbExtraMappings.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // RepopulatorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExtraMappings);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbExtraMappings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbAnonymise);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnInputCsv);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.nThreads);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOutputFolder);
            this.Controls.Add(this.tbInputCsv);
            this.Controls.Add(this.tbInputFolder);
            this.Controls.Add(this.tbFilenameColumn);
            this.Controls.Add(this.tbPattern);
            this.Controls.Add(this.cbIncludeSubfolders);
            this.Controls.Add(this.btnValidateCsv);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.btnInputFolder);
            this.MinimumSize = new System.Drawing.Size(750, 250);
            this.Name = "RepopulatorUI";
            this.Size = new System.Drawing.Size(750, 250);
            this.Load += new System.EventHandler(this.RepopulatorUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nThreads)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.CheckBox cbIncludeSubfolders;
        private System.Windows.Forms.TextBox tbPattern;
        private System.Windows.Forms.TextBox tbInputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nThreads;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbInputCsv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnInputCsv;
        private System.Windows.Forms.Button btnValidateCsv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFilenameColumn;
        private System.Windows.Forms.CheckBox cbAnonymise;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbDone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExtraMappings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbExtraMappings;
        private System.Windows.Forms.TextBox tbErrors;
        private System.Windows.Forms.Label label9;
    }
}
