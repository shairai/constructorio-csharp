namespace Sample_App
{
    partial class SampleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleForm));
            this.dataGridViewCSVData = new System.Windows.Forms.DataGridView();
            this.btnLoadCSV = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.openFileDialogCSV = new System.Windows.Forms.OpenFileDialog();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpUpload = new System.Windows.Forms.GroupBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.grpCSV = new System.Windows.Forms.GroupBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpKeys = new System.Windows.Forms.GroupBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtAPIToken = new System.Windows.Forms.TextBox();
            this.lblKeyAPI = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUploadUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCSVData)).BeginInit();
            this.grpUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.grpCSV.SuspendLayout();
            this.grpKeys.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCSVData
            // 
            this.dataGridViewCSVData.AllowUserToAddRows = false;
            this.dataGridViewCSVData.AllowUserToDeleteRows = false;
            this.dataGridViewCSVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCSVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCSVData.Location = new System.Drawing.Point(10, 141);
            this.dataGridViewCSVData.Name = "dataGridViewCSVData";
            this.dataGridViewCSVData.ReadOnly = true;
            this.dataGridViewCSVData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewCSVData.Size = new System.Drawing.Size(989, 284);
            this.dataGridViewCSVData.TabIndex = 0;
            // 
            // btnLoadCSV
            // 
            this.btnLoadCSV.Location = new System.Drawing.Point(168, 27);
            this.btnLoadCSV.Name = "btnLoadCSV";
            this.btnLoadCSV.Size = new System.Drawing.Size(75, 23);
            this.btnLoadCSV.TabIndex = 1;
            this.btnLoadCSV.Text = "Load CSV";
            this.btnLoadCSV.UseVisualStyleBackColor = true;
            this.btnLoadCSV.Click += new System.EventHandler(this.btnLoadCSV_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(223, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(87, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "Add";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // openFileDialogCSV
            // 
            this.openFileDialogCSV.FileName = "openFileDialog1";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(910, 431);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpUpload
            // 
            this.grpUpload.Controls.Add(this.btnUploadUpdate);
            this.grpUpload.Controls.Add(this.pictureBoxLoading);
            this.grpUpload.Controls.Add(this.label1);
            this.grpUpload.Controls.Add(this.comboBoxType);
            this.grpUpload.Controls.Add(this.btnUpload);
            this.grpUpload.Location = new System.Drawing.Point(617, 64);
            this.grpUpload.Name = "grpUpload";
            this.grpUpload.Size = new System.Drawing.Size(382, 71);
            this.grpUpload.TabIndex = 4;
            this.grpUpload.TabStop = false;
            this.grpUpload.Text = "3. Upload";
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxLoading.Image = global::Sample_App.Properties.Resources.ajax_loader;
            this.pictureBoxLoading.Location = new System.Drawing.Point(321, 13);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(47, 47);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoading.TabIndex = 16;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Section name";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Products",
            "Search Suggestions"});
            this.comboBoxType.Location = new System.Drawing.Point(11, 40);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(206, 21);
            this.comboBoxType.TabIndex = 3;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            this.comboBoxType.SelectedValueChanged += new System.EventHandler(this.comboBoxType_SelectedValueChanged);
            // 
            // grpCSV
            // 
            this.grpCSV.Controls.Add(this.txtFile);
            this.grpCSV.Controls.Add(this.label2);
            this.grpCSV.Controls.Add(this.btnLoadCSV);
            this.grpCSV.Location = new System.Drawing.Point(353, 64);
            this.grpCSV.Name = "grpCSV";
            this.grpCSV.Size = new System.Drawing.Size(258, 71);
            this.grpCSV.TabIndex = 5;
            this.grpCSV.TabStop = false;
            this.grpCSV.Text = "2. Choose CSV file";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(37, 28);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(123, 20);
            this.txtFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File";
            // 
            // grpKeys
            // 
            this.grpKeys.Controls.Add(this.btnVerify);
            this.grpKeys.Controls.Add(this.label3);
            this.grpKeys.Controls.Add(this.txtKey);
            this.grpKeys.Controls.Add(this.txtAPIToken);
            this.grpKeys.Controls.Add(this.lblKeyAPI);
            this.grpKeys.Location = new System.Drawing.Point(12, 64);
            this.grpKeys.Name = "grpKeys";
            this.grpKeys.Size = new System.Drawing.Size(335, 71);
            this.grpKeys.TabIndex = 6;
            this.grpKeys.TabStop = false;
            this.grpKeys.Text = "1. Enter keys";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(248, 28);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 4;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(89, 40);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(143, 20);
            this.txtKey.TabIndex = 2;
            this.txtKey.Text = "5tHZR5xflF6bNLgvpa60";
            // 
            // txtAPIToken
            // 
            this.txtAPIToken.Location = new System.Drawing.Point(89, 14);
            this.txtAPIToken.Name = "txtAPIToken";
            this.txtAPIToken.Size = new System.Drawing.Size(143, 20);
            this.txtAPIToken.TabIndex = 1;
            this.txtAPIToken.Text = "UuXqlIafeKvwop6DaRwP";
            // 
            // lblKeyAPI
            // 
            this.lblKeyAPI.AutoSize = true;
            this.lblKeyAPI.Location = new System.Drawing.Point(24, 16);
            this.lblKeyAPI.Name = "lblKeyAPI";
            this.lblKeyAPI.Size = new System.Drawing.Size(54, 13);
            this.lblKeyAPI.TabIndex = 0;
            this.lblKeyAPI.Text = "API token";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 41);
            this.panel1.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox1.Image = global::Sample_App.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(17, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(181, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // btnUploadUpdate
            // 
            this.btnUploadUpdate.Location = new System.Drawing.Point(223, 38);
            this.btnUploadUpdate.Name = "btnUploadUpdate";
            this.btnUploadUpdate.Size = new System.Drawing.Size(87, 23);
            this.btnUploadUpdate.TabIndex = 17;
            this.btnUploadUpdate.Text = "Add Or Update";
            this.btnUploadUpdate.UseVisualStyleBackColor = true;
            this.btnUploadUpdate.Click += new System.EventHandler(this.btnUploadUpdate_Click);
            // 
            // SampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 463);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpKeys);
            this.Controls.Add(this.grpCSV);
            this.Controls.Add(this.grpUpload);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridViewCSVData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SampleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCSVData)).EndInit();
            this.grpUpload.ResumeLayout(false);
            this.grpUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.grpCSV.ResumeLayout(false);
            this.grpCSV.PerformLayout();
            this.grpKeys.ResumeLayout(false);
            this.grpKeys.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCSVData;
        private System.Windows.Forms.Button btnLoadCSV;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialogCSV;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox grpUpload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.GroupBox grpCSV;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpKeys;
        private System.Windows.Forms.TextBox txtAPIToken;
        private System.Windows.Forms.Label lblKeyAPI;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Button btnUploadUpdate;
    }
}

