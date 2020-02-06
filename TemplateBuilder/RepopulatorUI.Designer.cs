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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepopulatorUI));
            this.btnInputFolder = new System.Windows.Forms.Button();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.cbIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.tbFilePattern = new System.Windows.Forms.TextBox();
            this.tbInputFolder = new System.Windows.Forms.TextBox();
            this.lblInputFolder = new System.Windows.Forms.Label();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.tbOutputFolder = new System.Windows.Forms.TextBox();
            this.lblFilePattern = new System.Windows.Forms.Label();
            this.nThreads = new System.Windows.Forms.NumericUpDown();
            this.lblThreads = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbInputCsv = new System.Windows.Forms.TextBox();
            this.lblInputCsv = new System.Windows.Forms.Label();
            this.btnInputCsv = new System.Windows.Forms.Button();
            this.btnValidateCsv = new System.Windows.Forms.Button();
            this.lblFileNameColumn = new System.Windows.Forms.Label();
            this.tbFilenameColumn = new System.Windows.Forms.TextBox();
            this.cbAnonymise = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbErrors = new System.Windows.Forms.TextBox();
            this.lblErrors = new System.Windows.Forms.Label();
            this.tbDone = new System.Windows.Forms.TextBox();
            this.lblDone = new System.Windows.Forms.Label();
            this.btnExtraMappings = new System.Windows.Forms.Button();
            this.lblExtraMappings = new System.Windows.Forms.Label();
            this.tbExtraMappings = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.nErrorThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblErrorThreshold = new System.Windows.Forms.Label();
            this.lblCulture = new System.Windows.Forms.Label();
            this.tbCulture = new System.Windows.Forms.TextBox();
            this.lblSubFolder = new System.Windows.Forms.Label();
            this.tbSubFolderColumn = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nThreads)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nErrorThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInputFolder
            // 
            this.btnInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputFolder.Location = new System.Drawing.Point(673, 1);
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
            this.btnOutputFolder.Location = new System.Drawing.Point(673, 131);
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
            this.cbIncludeSubfolders.Location = new System.Drawing.Point(98, 29);
            this.cbIncludeSubfolders.Name = "cbIncludeSubfolders";
            this.cbIncludeSubfolders.Size = new System.Drawing.Size(120, 17);
            this.cbIncludeSubfolders.TabIndex = 2;
            this.cbIncludeSubfolders.Text = "Include Sub Folders";
            this.cbIncludeSubfolders.UseVisualStyleBackColor = true;
            this.cbIncludeSubfolders.CheckedChanged += new System.EventHandler(this.cbIncludeSubfolders_CheckedChanged);
            // 
            // tbFilePattern
            // 
            this.tbFilePattern.Location = new System.Drawing.Point(293, 29);
            this.tbFilePattern.Name = "tbFilePattern";
            this.tbFilePattern.Size = new System.Drawing.Size(100, 20);
            this.tbFilePattern.TabIndex = 4;
            this.tbFilePattern.Text = "*.dcm";
            this.tbFilePattern.TextChanged += new System.EventHandler(this.tbPattern_TextChanged);
            // 
            // tbInputFolder
            // 
            this.tbInputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbInputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbInputFolder.Location = new System.Drawing.Point(90, 3);
            this.tbInputFolder.Name = "tbInputFolder";
            this.tbInputFolder.Size = new System.Drawing.Size(577, 20);
            this.tbInputFolder.TabIndex = 0;
            this.tbInputFolder.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // lblInputFolder
            // 
            this.lblInputFolder.AutoSize = true;
            this.lblInputFolder.Location = new System.Drawing.Point(12, 6);
            this.lblInputFolder.Name = "lblInputFolder";
            this.lblInputFolder.Size = new System.Drawing.Size(66, 13);
            this.lblInputFolder.TabIndex = 4;
            this.lblInputFolder.Text = "Input Folder:";
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(10, 136);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(74, 13);
            this.lblOutputFolder.TabIndex = 4;
            this.lblOutputFolder.Text = "Output Folder:";
            // 
            // tbOutputFolder
            // 
            this.tbOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbOutputFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbOutputFolder.Location = new System.Drawing.Point(90, 133);
            this.tbOutputFolder.Name = "tbOutputFolder";
            this.tbOutputFolder.Size = new System.Drawing.Size(577, 20);
            this.tbOutputFolder.TabIndex = 9;
            this.tbOutputFolder.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // lblFilePattern
            // 
            this.lblFilePattern.AutoSize = true;
            this.lblFilePattern.Location = new System.Drawing.Point(224, 32);
            this.lblFilePattern.Name = "lblFilePattern";
            this.lblFilePattern.Size = new System.Drawing.Size(63, 13);
            this.lblFilePattern.TabIndex = 3;
            this.lblFilePattern.Text = "File Pattern:";
            // 
            // nThreads
            // 
            this.nThreads.Location = new System.Drawing.Point(454, 30);
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
            this.nThreads.Size = new System.Drawing.Size(54, 20);
            this.nThreads.TabIndex = 6;
            this.nThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nThreads.ValueChanged += new System.EventHandler(this.nThreads_ValueChanged);
            // 
            // lblThreads
            // 
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(399, 32);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(49, 13);
            this.lblThreads.TabIndex = 5;
            this.lblThreads.Text = "Threads:";
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(31, 184);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(58, 13);
            this.lblProgress.TabIndex = 9;
            this.lblProgress.Text = "lblProgress";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(221, 159);
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
            this.tbInputCsv.Location = new System.Drawing.Point(90, 78);
            this.tbInputCsv.Name = "tbInputCsv";
            this.tbInputCsv.Size = new System.Drawing.Size(577, 20);
            this.tbInputCsv.TabIndex = 7;
            this.tbInputCsv.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // lblInputCsv
            // 
            this.lblInputCsv.AutoSize = true;
            this.lblInputCsv.Location = new System.Drawing.Point(12, 81);
            this.lblInputCsv.Name = "lblInputCsv";
            this.lblInputCsv.Size = new System.Drawing.Size(55, 13);
            this.lblInputCsv.TabIndex = 4;
            this.lblInputCsv.Text = "Input Csv:";
            // 
            // btnInputCsv
            // 
            this.btnInputCsv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputCsv.Location = new System.Drawing.Point(673, 76);
            this.btnInputCsv.Name = "btnInputCsv";
            this.btnInputCsv.Size = new System.Drawing.Size(67, 23);
            this.btnInputCsv.TabIndex = 8;
            this.btnInputCsv.Text = "Browse...";
            this.btnInputCsv.UseVisualStyleBackColor = true;
            this.btnInputCsv.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnValidateCsv
            // 
            this.btnValidateCsv.Location = new System.Drawing.Point(90, 159);
            this.btnValidateCsv.Name = "btnValidateCsv";
            this.btnValidateCsv.Size = new System.Drawing.Size(125, 23);
            this.btnValidateCsv.TabIndex = 11;
            this.btnValidateCsv.Text = "Validate Csv Headers";
            this.btnValidateCsv.UseVisualStyleBackColor = true;
            this.btnValidateCsv.Click += new System.EventHandler(this.btnValidateCsv_Click);
            // 
            // lblFileNameColumn
            // 
            this.lblFileNameColumn.AutoSize = true;
            this.lblFileNameColumn.Location = new System.Drawing.Point(95, 55);
            this.lblFileNameColumn.Name = "lblFileNameColumn";
            this.lblFileNameColumn.Size = new System.Drawing.Size(87, 13);
            this.lblFileNameColumn.TabIndex = 12;
            this.lblFileNameColumn.Text = "Filename Column";
            // 
            // tbFilenameColumn
            // 
            this.tbFilenameColumn.Location = new System.Drawing.Point(188, 52);
            this.tbFilenameColumn.Name = "tbFilenameColumn";
            this.tbFilenameColumn.Size = new System.Drawing.Size(205, 20);
            this.tbFilenameColumn.TabIndex = 4;
            this.tbFilenameColumn.TextChanged += new System.EventHandler(this.tbFilenameColumn_TextChanged);
            // 
            // cbAnonymise
            // 
            this.cbAnonymise.AutoSize = true;
            this.cbAnonymise.Location = new System.Drawing.Point(402, 54);
            this.cbAnonymise.Name = "cbAnonymise";
            this.cbAnonymise.Size = new System.Drawing.Size(207, 17);
            this.cbAnonymise.TabIndex = 13;
            this.cbAnonymise.Text = "Anonymise (with FoDicom Anonymizer)";
            this.cbAnonymise.UseVisualStyleBackColor = true;
            this.cbAnonymise.CheckedChanged += new System.EventHandler(this.cbAnonymise_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tbErrors);
            this.panel1.Controls.Add(this.lblErrors);
            this.panel1.Controls.Add(this.tbDone);
            this.panel1.Controls.Add(this.lblDone);
            this.panel1.Location = new System.Drawing.Point(3, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 23);
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
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Location = new System.Drawing.Point(221, 3);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(37, 13);
            this.lblErrors.TabIndex = 11;
            this.lblErrors.Text = "Errors:";
            // 
            // tbDone
            // 
            this.tbDone.Location = new System.Drawing.Point(45, 0);
            this.tbDone.Name = "tbDone";
            this.tbDone.ReadOnly = true;
            this.tbDone.Size = new System.Drawing.Size(166, 20);
            this.tbDone.TabIndex = 10;
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(3, 3);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(36, 13);
            this.lblDone.TabIndex = 9;
            this.lblDone.Text = "Done:";
            // 
            // btnExtraMappings
            // 
            this.btnExtraMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtraMappings.Location = new System.Drawing.Point(673, 105);
            this.btnExtraMappings.Name = "btnExtraMappings";
            this.btnExtraMappings.Size = new System.Drawing.Size(67, 23);
            this.btnExtraMappings.TabIndex = 17;
            this.btnExtraMappings.Text = "Browse...";
            this.btnExtraMappings.UseVisualStyleBackColor = true;
            this.btnExtraMappings.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblExtraMappings
            // 
            this.lblExtraMappings.AutoSize = true;
            this.lblExtraMappings.Location = new System.Drawing.Point(6, 110);
            this.lblExtraMappings.Name = "lblExtraMappings";
            this.lblExtraMappings.Size = new System.Drawing.Size(83, 13);
            this.lblExtraMappings.TabIndex = 15;
            this.lblExtraMappings.Text = "Extra Mappings:";
            // 
            // tbExtraMappings
            // 
            this.tbExtraMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraMappings.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbExtraMappings.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbExtraMappings.Location = new System.Drawing.Point(90, 107);
            this.tbExtraMappings.Name = "tbExtraMappings";
            this.tbExtraMappings.Size = new System.Drawing.Size(577, 20);
            this.tbExtraMappings.TabIndex = 16;
            this.tbExtraMappings.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 204);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(747, 10);
            this.progressBar1.TabIndex = 18;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyToClipboard.Image")));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(3, 177);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(25, 24);
            this.btnCopyToClipboard.TabIndex = 19;
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // nErrorThreshold
            // 
            this.nErrorThreshold.Location = new System.Drawing.Point(602, 28);
            this.nErrorThreshold.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.nErrorThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nErrorThreshold.Name = "nErrorThreshold";
            this.nErrorThreshold.Size = new System.Drawing.Size(86, 20);
            this.nErrorThreshold.TabIndex = 21;
            this.nErrorThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nErrorThreshold.ValueChanged += new System.EventHandler(this.nErrorThreshold_ValueChanged);
            // 
            // lblErrorThreshold
            // 
            this.lblErrorThreshold.AutoSize = true;
            this.lblErrorThreshold.Location = new System.Drawing.Point(514, 32);
            this.lblErrorThreshold.Name = "lblErrorThreshold";
            this.lblErrorThreshold.Size = new System.Drawing.Size(82, 13);
            this.lblErrorThreshold.TabIndex = 20;
            this.lblErrorThreshold.Text = "Error Threshold:";
            // 
            // lblCulture
            // 
            this.lblCulture.AutoSize = true;
            this.lblCulture.Location = new System.Drawing.Point(522, 165);
            this.lblCulture.Name = "lblCulture";
            this.lblCulture.Size = new System.Drawing.Size(43, 13);
            this.lblCulture.TabIndex = 22;
            this.lblCulture.Text = "Culture:";
            // 
            // tbCulture
            // 
            this.tbCulture.Location = new System.Drawing.Point(567, 162);
            this.tbCulture.Name = "tbCulture";
            this.tbCulture.Size = new System.Drawing.Size(100, 20);
            this.tbCulture.TabIndex = 23;
            this.tbCulture.TextChanged += new System.EventHandler(this.tbCulture_TextChanged);
            // 
            // lblSubFolder
            // 
            this.lblSubFolder.AutoSize = true;
            this.lblSubFolder.Location = new System.Drawing.Point(294, 165);
            this.lblSubFolder.Name = "lblSubFolder";
            this.lblSubFolder.Size = new System.Drawing.Size(99, 13);
            this.lblSubFolder.TabIndex = 24;
            this.lblSubFolder.Text = "Subfolder (Column):";
            // 
            // tbSubFolderColumn
            // 
            this.tbSubFolderColumn.Location = new System.Drawing.Point(399, 162);
            this.tbSubFolderColumn.Name = "tbSubFolderColumn";
            this.tbSubFolderColumn.Size = new System.Drawing.Size(117, 20);
            this.tbSubFolderColumn.TabIndex = 25;
            this.tbSubFolderColumn.TextChanged += new System.EventHandler(this.tbSubFolderColumn_TextChanged);
            // 
            // RepopulatorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tbSubFolderColumn);
            this.Controls.Add(this.lblSubFolder);
            this.Controls.Add(this.tbCulture);
            this.Controls.Add(this.lblCulture);
            this.Controls.Add(this.nErrorThreshold);
            this.Controls.Add(this.lblErrorThreshold);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnExtraMappings);
            this.Controls.Add(this.lblExtraMappings);
            this.Controls.Add(this.tbExtraMappings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbAnonymise);
            this.Controls.Add(this.lblFileNameColumn);
            this.Controls.Add(this.btnInputCsv);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.nThreads);
            this.Controls.Add(this.lblThreads);
            this.Controls.Add(this.lblFilePattern);
            this.Controls.Add(this.lblInputCsv);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.lblInputFolder);
            this.Controls.Add(this.tbOutputFolder);
            this.Controls.Add(this.tbInputCsv);
            this.Controls.Add(this.tbInputFolder);
            this.Controls.Add(this.tbFilenameColumn);
            this.Controls.Add(this.tbFilePattern);
            this.Controls.Add(this.cbIncludeSubfolders);
            this.Controls.Add(this.btnValidateCsv);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.btnInputFolder);
            this.MinimumSize = new System.Drawing.Size(750, 240);
            this.Name = "RepopulatorUI";
            this.Size = new System.Drawing.Size(750, 240);
            this.Load += new System.EventHandler(this.RepopulatorUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nThreads)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nErrorThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInputFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.CheckBox cbIncludeSubfolders;
        private System.Windows.Forms.TextBox tbFilePattern;
        private System.Windows.Forms.TextBox tbInputFolder;
        private System.Windows.Forms.Label lblInputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox tbOutputFolder;
        private System.Windows.Forms.Label lblFilePattern;
        private System.Windows.Forms.NumericUpDown nThreads;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbInputCsv;
        private System.Windows.Forms.Label lblInputCsv;
        private System.Windows.Forms.Button btnInputCsv;
        private System.Windows.Forms.Button btnValidateCsv;
        private System.Windows.Forms.Label lblFileNameColumn;
        private System.Windows.Forms.TextBox tbFilenameColumn;
        private System.Windows.Forms.CheckBox cbAnonymise;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbDone;
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.Button btnExtraMappings;
        private System.Windows.Forms.Label lblExtraMappings;
        private System.Windows.Forms.TextBox tbExtraMappings;
        private System.Windows.Forms.TextBox tbErrors;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.NumericUpDown nErrorThreshold;
        private System.Windows.Forms.Label lblErrorThreshold;
        private System.Windows.Forms.Label lblCulture;
        private System.Windows.Forms.TextBox tbCulture;
        private System.Windows.Forms.Label lblSubFolder;
        private System.Windows.Forms.TextBox tbSubFolderColumn;
    }
}
