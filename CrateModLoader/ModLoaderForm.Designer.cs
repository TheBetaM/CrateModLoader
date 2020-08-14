namespace CrateModLoader
{
    partial class ModLoaderForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModLoaderForm));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_openModMenu = new System.Windows.Forms.Button();
            this.button_modCrateMenu = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.checkBox_loadFromFolder = new System.Windows.Forms.CheckBox();
            this.checkBox_saveToFolder = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel_optionDesc = new System.Windows.Forms.LinkLabel();
            this.panel_desc = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_downloadMods = new System.Windows.Forms.Button();
            this.button_modTools = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel_desc.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Nothing"});
            this.checkedListBox1.Location = new System.Drawing.Point(0, 250);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(464, 120);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.Visible = false;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(308, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "1. Click \"Browse\" to select the game or drag & drop it here.";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(380, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 24);
            this.button1.TabIndex = 2;
            this.button1.Text = "(1) Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(308, 24);
            this.progressBar1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(0, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(308, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "2. Click \"Browse\" to choose the output path.";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(380, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 24);
            this.button2.TabIndex = 7;
            this.button2.Text = "(2) Browse...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(310, 89);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 26);
            this.button3.TabIndex = 11;
            this.button3.Text = "Start!";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDown1.Location = new System.Drawing.Point(228, 26);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(235, 24);
            this.numericUpDown1.TabIndex = 12;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericUpDown1, "The seed of all randomizers for this game.");
            this.numericUpDown1.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(0, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(228, 26);
            this.button4.TabIndex = 13;
            this.button4.Text = "Randomize Seed";
            this.toolTip1.SetToolTip(this.button4, "Randomize the seed to the right of this button.");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(6, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(459, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "ProgressInfo The process info may be very long, but only if there\'s an error. Tex" +
    "t overflow is rare.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(384, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Keep Files";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // button_openModMenu
            // 
            this.button_openModMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_openModMenu.Enabled = false;
            this.button_openModMenu.Location = new System.Drawing.Point(155, 0);
            this.button_openModMenu.Name = "button_openModMenu";
            this.button_openModMenu.Size = new System.Drawing.Size(155, 26);
            this.button_openModMenu.TabIndex = 19;
            this.button_openModMenu.Text = "Mod Menu";
            this.toolTip1.SetToolTip(this.button_openModMenu, "Open the Mod Menu of this specific game.");
            this.button_openModMenu.UseVisualStyleBackColor = true;
            this.button_openModMenu.Visible = false;
            this.button_openModMenu.Click += new System.EventHandler(this.button_openModMenu_Click);
            // 
            // button_modCrateMenu
            // 
            this.button_modCrateMenu.Enabled = false;
            this.button_modCrateMenu.Location = new System.Drawing.Point(0, 0);
            this.button_modCrateMenu.Name = "button_modCrateMenu";
            this.button_modCrateMenu.Size = new System.Drawing.Size(155, 26);
            this.button_modCrateMenu.TabIndex = 20;
            this.button_modCrateMenu.Text = "Mod Crates";
            this.toolTip1.SetToolTip(this.button_modCrateMenu, "Manage Mod Crates compatible with this game. They must be in the \"Mods\" folder ne" +
        "ar this application.");
            this.button_modCrateMenu.UseVisualStyleBackColor = true;
            this.button_modCrateMenu.Visible = false;
            this.button_modCrateMenu.Click += new System.EventHandler(this.button_modCrateMenu_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(10, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(438, 26);
            this.label7.TabIndex = 28;
            this.label7.Text = "Game Name\r\n(Region Console)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(10, 172);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(443, 18);
            this.linkLabel1.TabIndex = 29;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "API Credit Text which is very long probably";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.linkLabel1, "Click this to visit the relevant website of this game\'s mod support.");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel2.Location = new System.Drawing.Point(36, 11);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(148, 13);
            this.linkLabel2.TabIndex = 30;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Crate Mod Loader v1.0.0";
            this.toolTip1.SetToolTip(this.linkLabel2, "Click this to visit the website of this tool.");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // checkBox_loadFromFolder
            // 
            this.checkBox_loadFromFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_loadFromFolder.AutoSize = true;
            this.checkBox_loadFromFolder.Location = new System.Drawing.Point(310, 38);
            this.checkBox_loadFromFolder.Name = "checkBox_loadFromFolder";
            this.checkBox_loadFromFolder.Size = new System.Drawing.Size(70, 17);
            this.checkBox_loadFromFolder.TabIndex = 31;
            this.checkBox_loadFromFolder.Text = "As Folder";
            this.toolTip1.SetToolTip(this.checkBox_loadFromFolder, "Load the game files from a folder instead of a ROM.");
            this.checkBox_loadFromFolder.UseVisualStyleBackColor = true;
            this.checkBox_loadFromFolder.CheckedChanged += new System.EventHandler(this.checkBox_loadFromFolder_CheckedChanged);
            // 
            // checkBox_saveToFolder
            // 
            this.checkBox_saveToFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_saveToFolder.AutoSize = true;
            this.checkBox_saveToFolder.Location = new System.Drawing.Point(310, 66);
            this.checkBox_saveToFolder.Name = "checkBox_saveToFolder";
            this.checkBox_saveToFolder.Size = new System.Drawing.Size(71, 17);
            this.checkBox_saveToFolder.TabIndex = 32;
            this.checkBox_saveToFolder.Text = "To Folder";
            this.toolTip1.SetToolTip(this.checkBox_saveToFolder, "Copy resulting modded files to a folder. Not supported by PS1/PS2 emulators.");
            this.checkBox_saveToFolder.UseVisualStyleBackColor = true;
            this.checkBox_saveToFolder.CheckedChanged += new System.EventHandler(this.checkBox_saveToFolder_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(389, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 70);
            this.panel1.TabIndex = 33;
            // 
            // linkLabel_optionDesc
            // 
            this.linkLabel_optionDesc.AutoSize = true;
            this.linkLabel_optionDesc.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel_optionDesc.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel_optionDesc.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel_optionDesc.Enabled = false;
            this.linkLabel_optionDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel_optionDesc.LinkArea = new System.Windows.Forms.LinkArea(33203, 0);
            this.linkLabel_optionDesc.Location = new System.Drawing.Point(0, 0);
            this.linkLabel_optionDesc.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel_optionDesc.Name = "linkLabel_optionDesc";
            this.linkLabel_optionDesc.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.linkLabel_optionDesc.Size = new System.Drawing.Size(991, 17);
            this.linkLabel_optionDesc.TabIndex = 34;
            this.linkLabel_optionDesc.Text = resources.GetString("linkLabel_optionDesc.Text");
            this.linkLabel_optionDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel_optionDesc.UseCompatibleTextRendering = true;
            // 
            // panel_desc
            // 
            this.panel_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_desc.AutoScroll = true;
            this.panel_desc.BackColor = System.Drawing.Color.Transparent;
            this.panel_desc.Controls.Add(this.linkLabel_optionDesc);
            this.panel_desc.Location = new System.Drawing.Point(0, 379);
            this.panel_desc.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel_desc.Name = "panel_desc";
            this.panel_desc.Size = new System.Drawing.Size(464, 38);
            this.panel_desc.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button_downloadMods);
            this.panel2.Controls.Add(this.button_modTools);
            this.panel2.Controls.Add(this.button_modCrateMenu);
            this.panel2.Controls.Add(this.button_openModMenu);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Location = new System.Drawing.Point(0, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 52);
            this.panel2.TabIndex = 36;
            // 
            // button_downloadMods
            // 
            this.button_downloadMods.Enabled = false;
            this.button_downloadMods.Location = new System.Drawing.Point(0, 60);
            this.button_downloadMods.Name = "button_downloadMods";
            this.button_downloadMods.Size = new System.Drawing.Size(155, 26);
            this.button_downloadMods.TabIndex = 22;
            this.button_downloadMods.Text = "Download Mods";
            this.button_downloadMods.UseVisualStyleBackColor = true;
            this.button_downloadMods.Visible = false;
            // 
            // button_modTools
            // 
            this.button_modTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_modTools.Enabled = false;
            this.button_modTools.Location = new System.Drawing.Point(310, 0);
            this.button_modTools.Name = "button_modTools";
            this.button_modTools.Size = new System.Drawing.Size(155, 26);
            this.button_modTools.TabIndex = 21;
            this.button_modTools.Text = "Mod Tools";
            this.button_modTools.UseVisualStyleBackColor = true;
            this.button_modTools.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CrateModLoader.Properties.Resources.cml_icon;
            this.pictureBox2.InitialImage = global::CrateModLoader.Properties.Resources.cml_icon;
            this.pictureBox2.Location = new System.Drawing.Point(1, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            // 
            // ModLoaderForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(464, 419);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_desc);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox_saveToFolder);
            this.Controls.Add(this.checkBox_loadFromFolder);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 180);
            this.Name = "ModLoaderForm";
            this.Text = "Crate Mod Loader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModLoaderForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModLoaderForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel_desc.ResumeLayout(false);
            this.panel_desc.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_openModMenu;
        private System.Windows.Forms.Button button_modCrateMenu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox checkBox_loadFromFolder;
        private System.Windows.Forms.CheckBox checkBox_saveToFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel_optionDesc;
        private System.Windows.Forms.Panel panel_desc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_downloadMods;
        private System.Windows.Forms.Button button_modTools;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

