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
            this.button_browseInput = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_browseOutput = new System.Windows.Forms.Button();
            this.button_startProcess = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button_randomizeSeed = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button_openModMenu = new System.Windows.Forms.Button();
            this.button_modCrateMenu = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel_optionDesc = new System.Windows.Forms.LinkLabel();
            this.panel_desc = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_modTools = new System.Windows.Forms.Button();
            this.button_downloadMods = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_loadROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_loadFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_saveROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_saveFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_keepTempFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_language = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_showCredits = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_showGames = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_showChangelog = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel_desc.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Nothing"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 0);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(464, 130);
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
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(348, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "1. Click \"Browse\" to select the game.";
            // 
            // button_browseInput
            // 
            this.button_browseInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_browseInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_browseInput.Location = new System.Drawing.Point(348, 0);
            this.button_browseInput.Margin = new System.Windows.Forms.Padding(0);
            this.button_browseInput.Name = "button_browseInput";
            this.button_browseInput.Size = new System.Drawing.Size(116, 23);
            this.button_browseInput.TabIndex = 2;
            this.button_browseInput.Text = "(1) Browse...";
            this.button_browseInput.UseVisualStyleBackColor = true;
            this.button_browseInput.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 46);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(464, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox2.Location = new System.Drawing.Point(0, 23);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(348, 22);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "2. Click \"Browse\" to set the output path.";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button_browseOutput
            // 
            this.button_browseOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_browseOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_browseOutput.Location = new System.Drawing.Point(348, 23);
            this.button_browseOutput.Margin = new System.Windows.Forms.Padding(0);
            this.button_browseOutput.Name = "button_browseOutput";
            this.button_browseOutput.Size = new System.Drawing.Size(116, 23);
            this.button_browseOutput.TabIndex = 7;
            this.button_browseOutput.Text = "(2) Browse...";
            this.button_browseOutput.UseVisualStyleBackColor = true;
            this.button_browseOutput.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_startProcess
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.button_startProcess, 2);
            this.button_startProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_startProcess.Enabled = false;
            this.button_startProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_startProcess.Location = new System.Drawing.Point(0, 69);
            this.button_startProcess.Margin = new System.Windows.Forms.Padding(0);
            this.button_startProcess.Name = "button_startProcess";
            this.button_startProcess.Size = new System.Drawing.Size(464, 23);
            this.button_startProcess.TabIndex = 11;
            this.button_startProcess.Text = "Start!";
            this.button_startProcess.UseVisualStyleBackColor = true;
            this.button_startProcess.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.SystemColors.Window;
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDown1.Location = new System.Drawing.Point(309, 27);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(155, 24);
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
            // button_randomizeSeed
            // 
            this.button_randomizeSeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_randomizeSeed.Enabled = false;
            this.button_randomizeSeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_randomizeSeed.Location = new System.Drawing.Point(154, 26);
            this.button_randomizeSeed.Margin = new System.Windows.Forms.Padding(0);
            this.button_randomizeSeed.Name = "button_randomizeSeed";
            this.button_randomizeSeed.Size = new System.Drawing.Size(154, 26);
            this.button_randomizeSeed.TabIndex = 13;
            this.button_randomizeSeed.Text = "Randomize Seed";
            this.toolTip1.SetToolTip(this.button_randomizeSeed, "Randomize the seed to the right of this button.");
            this.button_randomizeSeed.UseVisualStyleBackColor = true;
            this.button_randomizeSeed.Visible = false;
            this.button_randomizeSeed.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(458, 22);
            this.label6.TabIndex = 15;
            this.label6.Text = "ProgressInfo The process info may be very long, but only if there\'s an error. Tex" +
    "t overflow is rare.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_openModMenu
            // 
            this.button_openModMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_openModMenu.Enabled = false;
            this.button_openModMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_openModMenu.Location = new System.Drawing.Point(308, 0);
            this.button_openModMenu.Margin = new System.Windows.Forms.Padding(0);
            this.button_openModMenu.Name = "button_openModMenu";
            this.button_openModMenu.Size = new System.Drawing.Size(156, 26);
            this.button_openModMenu.TabIndex = 19;
            this.button_openModMenu.Text = "Mod Menu";
            this.toolTip1.SetToolTip(this.button_openModMenu, "Open the Mod Menu of this specific game.");
            this.button_openModMenu.UseVisualStyleBackColor = true;
            this.button_openModMenu.Visible = false;
            this.button_openModMenu.Click += new System.EventHandler(this.button_openModMenu_Click);
            // 
            // button_modCrateMenu
            // 
            this.button_modCrateMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_modCrateMenu.Enabled = false;
            this.button_modCrateMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_modCrateMenu.Location = new System.Drawing.Point(0, 0);
            this.button_modCrateMenu.Margin = new System.Windows.Forms.Padding(0);
            this.button_modCrateMenu.Name = "button_modCrateMenu";
            this.button_modCrateMenu.Size = new System.Drawing.Size(154, 26);
            this.button_modCrateMenu.TabIndex = 20;
            this.button_modCrateMenu.Text = "Mod Crates";
            this.toolTip1.SetToolTip(this.button_modCrateMenu, "Manage Mod Crates compatible with this game. They must be in the \"Mods\" folder ne" +
        "ar this application.");
            this.button_modCrateMenu.UseVisualStyleBackColor = true;
            this.button_modCrateMenu.Visible = false;
            this.button_modCrateMenu.Click += new System.EventHandler(this.button_modCrateMenu_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.SetColumnSpan(this.label7, 2);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(3, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(458, 33);
            this.label7.TabIndex = 28;
            this.label7.Text = "Game Name\r\n(Region Console)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.SetColumnSpan(this.linkLabel1, 2);
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(3, 55);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(458, 21);
            this.linkLabel1.TabIndex = 29;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "API Credit Text which is very long probably";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.linkLabel1, "Click this to visit the relevant website of this game\'s mod support.");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.Enabled = false;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel2.Location = new System.Drawing.Point(260, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(206, 22);
            this.linkLabel2.TabIndex = 30;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Crate Mod Loader v1.0.0";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.linkLabel2, "Click this to visit the website of this tool.");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel_optionDesc
            // 
            this.linkLabel_optionDesc.AutoSize = true;
            this.linkLabel_optionDesc.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel_optionDesc.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabel_optionDesc.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel_optionDesc.Enabled = false;
            this.linkLabel_optionDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.linkLabel_optionDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel_optionDesc.LinkArea = new System.Windows.Forms.LinkArea(33203, 0);
            this.linkLabel_optionDesc.Location = new System.Drawing.Point(0, 0);
            this.linkLabel_optionDesc.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel_optionDesc.Name = "linkLabel_optionDesc";
            this.linkLabel_optionDesc.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.linkLabel_optionDesc.Size = new System.Drawing.Size(1080, 19);
            this.linkLabel_optionDesc.TabIndex = 34;
            this.linkLabel_optionDesc.Text = resources.GetString("linkLabel_optionDesc.Text");
            this.linkLabel_optionDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel_optionDesc.UseCompatibleTextRendering = true;
            // 
            // panel_desc
            // 
            this.panel_desc.AutoScroll = true;
            this.panel_desc.BackColor = System.Drawing.Color.Transparent;
            this.panel_desc.Controls.Add(this.linkLabel_optionDesc);
            this.panel_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_desc.Location = new System.Drawing.Point(3, 130);
            this.panel_desc.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel_desc.Name = "panel_desc";
            this.panel_desc.Size = new System.Drawing.Size(464, 38);
            this.panel_desc.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(0, 195);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(464, 52);
            this.panel2.TabIndex = 36;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_randomizeSeed, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_modTools, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_downloadMods, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_modCrateMenu, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_openModMenu, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 52);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // button_modTools
            // 
            this.button_modTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_modTools.Enabled = false;
            this.button_modTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_modTools.Location = new System.Drawing.Point(154, 0);
            this.button_modTools.Margin = new System.Windows.Forms.Padding(0);
            this.button_modTools.Name = "button_modTools";
            this.button_modTools.Size = new System.Drawing.Size(154, 26);
            this.button_modTools.TabIndex = 21;
            this.button_modTools.Text = "Mod Tools";
            this.button_modTools.UseVisualStyleBackColor = true;
            this.button_modTools.Visible = false;
            // 
            // button_downloadMods
            // 
            this.button_downloadMods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_downloadMods.Enabled = false;
            this.button_downloadMods.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_downloadMods.Location = new System.Drawing.Point(0, 26);
            this.button_downloadMods.Margin = new System.Windows.Forms.Padding(0);
            this.button_downloadMods.Name = "button_downloadMods";
            this.button_downloadMods.Size = new System.Drawing.Size(154, 26);
            this.button_downloadMods.TabIndex = 22;
            this.button_downloadMods.Text = "Download Mods";
            this.button_downloadMods.UseVisualStyleBackColor = true;
            this.button_downloadMods.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(464, 25);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_loadROM,
            this.toolStripMenuItem_loadFolder,
            this.toolStripMenuItem_saveROM,
            this.toolStripMenuItem_saveFolder});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(39, 21);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem_loadROM
            // 
            this.toolStripMenuItem_loadROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_loadROM.Name = "toolStripMenuItem_loadROM";
            this.toolStripMenuItem_loadROM.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem_loadROM.Text = "Load ROM...";
            this.toolStripMenuItem_loadROM.Click += new System.EventHandler(this.toolStripMenuItem_loadROM_Click);
            // 
            // toolStripMenuItem_loadFolder
            // 
            this.toolStripMenuItem_loadFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_loadFolder.Name = "toolStripMenuItem_loadFolder";
            this.toolStripMenuItem_loadFolder.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem_loadFolder.Text = "Load Folder...";
            this.toolStripMenuItem_loadFolder.Click += new System.EventHandler(this.toolStripMenuItem_loadFolder_Click);
            // 
            // toolStripMenuItem_saveROM
            // 
            this.toolStripMenuItem_saveROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_saveROM.Name = "toolStripMenuItem_saveROM";
            this.toolStripMenuItem_saveROM.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem_saveROM.Text = "Save as ROM...";
            this.toolStripMenuItem_saveROM.Click += new System.EventHandler(this.toolStripMenuItem_saveROM_Click);
            // 
            // toolStripMenuItem_saveFolder
            // 
            this.toolStripMenuItem_saveFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_saveFolder.Name = "toolStripMenuItem_saveFolder";
            this.toolStripMenuItem_saveFolder.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem_saveFolder.Text = "Save as Folder...";
            this.toolStripMenuItem_saveFolder.Click += new System.EventHandler(this.toolStripMenuItem_saveFolder_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_keepTempFiles,
            this.toolStripMenuItem_language});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(88, 21);
            this.toolStripMenuItem3.Text = "Preferences";
            // 
            // toolStripMenuItem_keepTempFiles
            // 
            this.toolStripMenuItem_keepTempFiles.CheckOnClick = true;
            this.toolStripMenuItem_keepTempFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_keepTempFiles.Enabled = false;
            this.toolStripMenuItem_keepTempFiles.Name = "toolStripMenuItem_keepTempFiles";
            this.toolStripMenuItem_keepTempFiles.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItem_keepTempFiles.Text = "Keep Extracted Files";
            this.toolStripMenuItem_keepTempFiles.Visible = false;
            this.toolStripMenuItem_keepTempFiles.CheckedChanged += new System.EventHandler(this.toolStripMenuItem_keepTempFiles_CheckedChanged);
            // 
            // toolStripMenuItem_language
            // 
            this.toolStripMenuItem_language.Enabled = false;
            this.toolStripMenuItem_language.Name = "toolStripMenuItem_language";
            this.toolStripMenuItem_language.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItem_language.Text = "Language";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_showCredits,
            this.toolStripMenuItem_showGames,
            this.toolStripMenuItem_showChangelog});
            this.toolStripMenuItem2.Image = global::CrateModLoader.Properties.Resources.cml_icon;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(71, 21);
            this.toolStripMenuItem2.Text = "About";
            // 
            // toolStripMenuItem_showCredits
            // 
            this.toolStripMenuItem_showCredits.Image = global::CrateModLoader.Properties.Resources.cml_icon;
            this.toolStripMenuItem_showCredits.Name = "toolStripMenuItem_showCredits";
            this.toolStripMenuItem_showCredits.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_showCredits.Text = "About";
            this.toolStripMenuItem_showCredits.Click += new System.EventHandler(this.toolStripMenuItem_showCredits_Click);
            // 
            // toolStripMenuItem_showGames
            // 
            this.toolStripMenuItem_showGames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_showGames.Name = "toolStripMenuItem_showGames";
            this.toolStripMenuItem_showGames.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_showGames.Text = "Games";
            this.toolStripMenuItem_showGames.Click += new System.EventHandler(this.toolStripMenuItem_showGames_Click);
            // 
            // toolStripMenuItem_showChangelog
            // 
            this.toolStripMenuItem_showChangelog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem_showChangelog.Name = "toolStripMenuItem_showChangelog";
            this.toolStripMenuItem_showChangelog.Size = new System.Drawing.Size(139, 22);
            this.toolStripMenuItem_showChangelog.Text = "Changelog";
            this.toolStripMenuItem_showChangelog.Click += new System.EventHandler(this.toolStripMenuItem_showChangelog_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.checkedListBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel_desc, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 250);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(464, 168);
            this.tableLayoutPanel2.TabIndex = 42;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel3.Controls.Add(this.linkLabel1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 117);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(464, 76);
            this.tableLayoutPanel3.TabIndex = 43;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.button_startProcess, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.progressBar1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBox2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.button_browseInput, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.button_browseOutput, 1, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(464, 92);
            this.tableLayoutPanel4.TabIndex = 44;
            // 
            // ModLoaderForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(464, 419);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(480, 180);
            this.Name = "ModLoaderForm";
            this.Text = "Crate Mod Loader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ModLoaderForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ModLoaderForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel_desc.ResumeLayout(false);
            this.panel_desc.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_browseInput;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_browseOutput;
        private System.Windows.Forms.Button button_startProcess;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button_randomizeSeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_openModMenu;
        private System.Windows.Forms.Button button_modCrateMenu;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel_optionDesc;
        private System.Windows.Forms.Panel panel_desc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_downloadMods;
        private System.Windows.Forms.Button button_modTools;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_loadROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_loadFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_saveROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_saveFolder;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_showCredits;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_showGames;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_showChangelog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_keepTempFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_language;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}

