namespace Sample_App__Add_Modify_Delete__
{
    partial class frmSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSample));
            this.grpKeys = new System.Windows.Forms.GroupBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtAPI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpUpload = new System.Windows.Forms.GroupBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.dataGridViewData = new System.Windows.Forms.DataGridView();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.new_item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autocomplete_section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suggested_score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keywords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.image_url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkOptional = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpKeys.SuspendLayout();
            this.grpUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpKeys
            // 
            this.grpKeys.Controls.Add(this.btnVerify);
            this.grpKeys.Controls.Add(this.label1);
            this.grpKeys.Controls.Add(this.txtKey);
            this.grpKeys.Controls.Add(this.txtAPI);
            this.grpKeys.Controls.Add(this.label2);
            this.grpKeys.Location = new System.Drawing.Point(12, 59);
            this.grpKeys.Name = "grpKeys";
            this.grpKeys.Size = new System.Drawing.Size(326, 71);
            this.grpKeys.TabIndex = 8;
            this.grpKeys.TabStop = false;
            this.grpKeys.Text = "1. Enter keys";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(233, 27);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 5;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Key";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(70, 40);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(143, 20);
            this.txtKey.TabIndex = 2;
            this.txtKey.Text = "5tHZR5xflF6bNLgvpa60";
            // 
            // txtAPI
            // 
            this.txtAPI.Location = new System.Drawing.Point(70, 14);
            this.txtAPI.Name = "txtAPI";
            this.txtAPI.Size = new System.Drawing.Size(143, 20);
            this.txtAPI.TabIndex = 1;
            this.txtAPI.Text = "UuXqlIafeKvwop6DaRwP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "API token";
            // 
            // grpUpload
            // 
            this.grpUpload.Controls.Add(this.comboBoxType);
            this.grpUpload.Location = new System.Drawing.Point(344, 59);
            this.grpUpload.Name = "grpUpload";
            this.grpUpload.Size = new System.Drawing.Size(174, 71);
            this.grpUpload.TabIndex = 9;
            this.grpUpload.TabStop = false;
            this.grpUpload.Text = "2. Section";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Products",
            "Search Suggestions"});
            this.comboBoxType.Location = new System.Drawing.Point(15, 27);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(143, 21);
            this.comboBoxType.TabIndex = 3;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // dataGridViewData
            // 
            this.dataGridViewData.AllowUserToAddRows = false;
            this.dataGridViewData.AllowUserToDeleteRows = false;
            this.dataGridViewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_name,
            this.new_item_name,
            this.autocomplete_section,
            this.suggested_score,
            this.keywords,
            this.url,
            this.image_url,
            this.description,
            this.id});
            this.dataGridViewData.Location = new System.Drawing.Point(12, 136);
            this.dataGridViewData.Name = "dataGridViewData";
            this.dataGridViewData.Size = new System.Drawing.Size(1044, 203);
            this.dataGridViewData.TabIndex = 10;
            this.dataGridViewData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // item_name
            // 
            this.item_name.HeaderText = "Item Name ( The name of the item, as it will appear in the autocomplete suggestio" +
    "ns ) Requred";
            this.item_name.Name = "item_name";
            // 
            // new_item_name
            // 
            this.new_item_name.HeaderText = "New Name (When modifying an existing item you can give it a new name)";
            this.new_item_name.Name = "new_item_name";
            this.new_item_name.Visible = false;
            // 
            // autocomplete_section
            // 
            this.autocomplete_section.HeaderText = resources.GetString("autocomplete_section.HeaderText");
            this.autocomplete_section.Name = "autocomplete_section";
            this.autocomplete_section.ReadOnly = true;
            this.autocomplete_section.Width = 140;
            // 
            // suggested_score
            // 
            this.suggested_score.HeaderText = resources.GetString("suggested_score.HeaderText");
            this.suggested_score.Name = "suggested_score";
            this.suggested_score.Width = 140;
            // 
            // keywords
            // 
            this.keywords.HeaderText = "Keywords ( An array of keywords for this item. Keywords are useful if you want a " +
    "product name to appear when a user enters a searchterm that isn’t in the product" +
    " name iteslf. )";
            this.keywords.Name = "keywords";
            this.keywords.Width = 140;
            // 
            // url
            // 
            this.url.HeaderText = "Url ( A URL to directly send the user after selecting the item )";
            this.url.Name = "url";
            // 
            // image_url
            // 
            this.image_url.HeaderText = "Image Url ( A URL that points to an image you’d like displayed next to some item " +
    "(only applicable when url is supplied) ";
            this.image_url.Name = "image_url";
            this.image_url.Width = 140;
            // 
            // description
            // 
            this.description.HeaderText = "Description ( A description for some item (only applicable when url is supplied )" +
    " ";
            this.description.Name = "description";
            // 
            // id
            // 
            this.id.HeaderText = "Id ( An arbitrary ID you would like associated with this item. You can use this f" +
    "ield to store your own IDs of the items to more easily access them in other API " +
    "calls )";
            this.id.Name = "id";
            this.id.Width = 140;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxLoading);
            this.groupBox1.Controls.Add(this.btnProcess);
            this.groupBox1.Controls.Add(this.comboBoxAction);
            this.groupBox1.Location = new System.Drawing.Point(700, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 71);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "4. Action";
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxLoading.Image = global::Sample_App__Add_Modify_Delete__.Properties.Resources.ajax_loader;
            this.pictureBoxLoading.Location = new System.Drawing.Point(261, 16);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(47, 47);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLoading.TabIndex = 15;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(168, 29);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Items.AddRange(new object[] {
            "Add",
            "Modify ( name of item )",
            "Delete"});
            this.comboBoxAction.Location = new System.Drawing.Point(12, 29);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(141, 21);
            this.comboBoxAction.TabIndex = 3;
            this.comboBoxAction.SelectedIndexChanged += new System.EventHandler(this.comboBoxAction_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(981, 345);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1070, 41);
            this.panel1.TabIndex = 14;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(181, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Checked = true;
            this.chkRequired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRequired.Location = new System.Drawing.Point(11, 22);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 4;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            this.chkRequired.CheckedChanged += new System.EventHandler(this.chkRequired_CheckedChanged);
            // 
            // chkOptional
            // 
            this.chkOptional.AutoSize = true;
            this.chkOptional.Location = new System.Drawing.Point(11, 41);
            this.chkOptional.Name = "chkOptional";
            this.chkOptional.Size = new System.Drawing.Size(65, 17);
            this.chkOptional.TabIndex = 5;
            this.chkOptional.Text = "Optional";
            this.chkOptional.UseVisualStyleBackColor = true;
            this.chkOptional.CheckedChanged += new System.EventHandler(this.chkOptional_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkRequired);
            this.groupBox2.Controls.Add(this.chkOptional);
            this.groupBox2.Location = new System.Drawing.Point(524, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 71);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "3. Parameters";
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewData);
            this.Controls.Add(this.grpUpload);
            this.Controls.Add(this.grpKeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSample";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adds an item, modifies it, and then deletes it";
            this.grpKeys.ResumeLayout(false);
            this.grpKeys.PerformLayout();
            this.grpUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpKeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtAPI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpUpload;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.DataGridView dataGridViewData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.CheckBox chkOptional;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn new_item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn autocomplete_section;
        private System.Windows.Forms.DataGridViewTextBoxColumn suggested_score;
        private System.Windows.Forms.DataGridViewTextBoxColumn keywords;
        private System.Windows.Forms.DataGridViewTextBoxColumn url;
        private System.Windows.Forms.DataGridViewTextBoxColumn image_url;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}

