namespace CrateModLoader
{
    partial class ModCrateManagerBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCrateManagerBox));
            this.checkedListBox_mods = new System.Windows.Forms.CheckedListBox();
            this.label_author = new System.Windows.Forms.Label();
            this.label_desc = new System.Windows.Forms.Label();
            this.pictureBox_ModIcon = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ToTop = new System.Windows.Forms.Button();
            this.button_MoveUp = new System.Windows.Forms.Button();
            this.button_MoveDown = new System.Windows.Forms.Button();
            this.button_ToBottom = new System.Windows.Forms.Button();
            this.button_ImportCrate = new System.Windows.Forms.Button();
            this.button_CreateCrate = new System.Windows.Forms.Button();
            this.button_DeleteCrate = new System.Windows.Forms.Button();
            this.button_DownloadCrates = new System.Windows.Forms.Button();
            this.button_EditCrate = new System.Windows.Forms.Button();
            this.button_RefreshCrates = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip_ImportCrate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_FromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FromFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_DownloadMods = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.download_site_BT = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip_ImportCrate.SuspendLayout();
            this.contextMenuStrip_DownloadMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox_mods
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.checkedListBox_mods, 6);
            this.checkedListBox_mods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox_mods.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkedListBox_mods.FormattingEnabled = true;
            this.checkedListBox_mods.Items.AddRange(new object[] {
            "Example Mod Name (v1.0)",
            "Example Mod Name 2 (v1.1)",
            "Example Mod Name 3 (v1.2)"});
            this.checkedListBox_mods.Location = new System.Drawing.Point(37, 30);
            this.checkedListBox_mods.Margin = new System.Windows.Forms.Padding(0);
            this.checkedListBox_mods.Name = "checkedListBox_mods";
            this.tableLayoutPanel1.SetRowSpan(this.checkedListBox_mods, 4);
            this.checkedListBox_mods.Size = new System.Drawing.Size(437, 300);
            this.checkedListBox_mods.TabIndex = 0;
            this.checkedListBox_mods.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_mods_ItemCheck);
            this.checkedListBox_mods.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_mods_SelectedIndexChanged);
            this.checkedListBox_mods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.checkedListBox_mods_MouseMove);
            // 
            // label_author
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_author, 5);
            this.label_author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_author.Location = new System.Drawing.Point(70, 330);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(401, 20);
            this.label_author.TabIndex = 3;
            this.label_author.Text = "Mod Author";
            this.label_author.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_desc
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_desc, 5);
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.Location = new System.Drawing.Point(70, 350);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(401, 50);
            this.label_desc.TabIndex = 4;
            this.label_desc.Text = "Mod Description";
            // 
            // pictureBox_ModIcon
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox_ModIcon, 2);
            this.pictureBox_ModIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ModIcon.InitialImage = null;
            this.pictureBox_ModIcon.Location = new System.Drawing.Point(0, 330);
            this.pictureBox_ModIcon.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ModIcon.Name = "pictureBox_ModIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox_ModIcon, 2);
            this.pictureBox_ModIcon.Size = new System.Drawing.Size(67, 70);
            this.pictureBox_ModIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_ModIcon.TabIndex = 5;
            this.pictureBox_ModIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox_mods, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_desc, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ModIcon, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_author, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_ToTop, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_MoveUp, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_MoveDown, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_ToBottom, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_ImportCrate, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_CreateCrate, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_DeleteCrate, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_DownloadCrates, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_EditCrate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_RefreshCrates, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(474, 400);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // button_ToTop
            // 
            this.button_ToTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ToTop.Location = new System.Drawing.Point(0, 30);
            this.button_ToTop.Margin = new System.Windows.Forms.Padding(0);
            this.button_ToTop.Name = "button_ToTop";
            this.button_ToTop.Size = new System.Drawing.Size(37, 75);
            this.button_ToTop.TabIndex = 6;
            this.button_ToTop.Text = "⭱";
            this.button_ToTop.UseVisualStyleBackColor = true;
            this.button_ToTop.Click += new System.EventHandler(this.button_ToTop_Click);
            // 
            // button_MoveUp
            // 
            this.button_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_MoveUp.Location = new System.Drawing.Point(0, 105);
            this.button_MoveUp.Margin = new System.Windows.Forms.Padding(0);
            this.button_MoveUp.Name = "button_MoveUp";
            this.button_MoveUp.Size = new System.Drawing.Size(37, 75);
            this.button_MoveUp.TabIndex = 7;
            this.button_MoveUp.Text = "↑";
            this.button_MoveUp.UseVisualStyleBackColor = true;
            this.button_MoveUp.Click += new System.EventHandler(this.button_MoveUp_Click);
            // 
            // button_MoveDown
            // 
            this.button_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_MoveDown.Location = new System.Drawing.Point(0, 180);
            this.button_MoveDown.Margin = new System.Windows.Forms.Padding(0);
            this.button_MoveDown.Name = "button_MoveDown";
            this.button_MoveDown.Size = new System.Drawing.Size(37, 75);
            this.button_MoveDown.TabIndex = 8;
            this.button_MoveDown.Text = "↓";
            this.button_MoveDown.UseVisualStyleBackColor = true;
            this.button_MoveDown.Click += new System.EventHandler(this.button_MoveDown_Click);
            // 
            // button_ToBottom
            // 
            this.button_ToBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ToBottom.Location = new System.Drawing.Point(0, 255);
            this.button_ToBottom.Margin = new System.Windows.Forms.Padding(0);
            this.button_ToBottom.Name = "button_ToBottom";
            this.button_ToBottom.Size = new System.Drawing.Size(37, 75);
            this.button_ToBottom.TabIndex = 9;
            this.button_ToBottom.Text = "⭳";
            this.button_ToBottom.UseVisualStyleBackColor = true;
            this.button_ToBottom.Click += new System.EventHandler(this.button_ToBottom_Click);
            // 
            // button_ImportCrate
            // 
            this.button_ImportCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ImportCrate.ImageKey = "file-plus";
            this.button_ImportCrate.ImageList = this.imageList1;
            this.button_ImportCrate.Location = new System.Drawing.Point(229, 0);
            this.button_ImportCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_ImportCrate.Name = "button_ImportCrate";
            this.button_ImportCrate.Size = new System.Drawing.Size(81, 30);
            this.button_ImportCrate.TabIndex = 10;
            this.button_ImportCrate.UseVisualStyleBackColor = true;
            this.button_ImportCrate.Click += new System.EventHandler(this.button_ImportCrate_Click);
            // 
            // button_CreateCrate
            // 
            this.button_CreateCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_CreateCrate.ImageKey = "(none)";
            this.button_CreateCrate.ImageList = this.imageList1;
            this.button_CreateCrate.Location = new System.Drawing.Point(67, 0);
            this.button_CreateCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_CreateCrate.Name = "button_CreateCrate";
            this.button_CreateCrate.Size = new System.Drawing.Size(81, 30);
            this.button_CreateCrate.TabIndex = 11;
            this.button_CreateCrate.Text = "Create";
            this.button_CreateCrate.UseVisualStyleBackColor = true;
            this.button_CreateCrate.Click += new System.EventHandler(this.button_CreateCrate_Click);
            // 
            // button_DeleteCrate
            // 
            this.button_DeleteCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DeleteCrate.ImageKey = "file-minus";
            this.button_DeleteCrate.ImageList = this.imageList1;
            this.button_DeleteCrate.Location = new System.Drawing.Point(310, 0);
            this.button_DeleteCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_DeleteCrate.Name = "button_DeleteCrate";
            this.button_DeleteCrate.Size = new System.Drawing.Size(81, 30);
            this.button_DeleteCrate.TabIndex = 12;
            this.button_DeleteCrate.UseVisualStyleBackColor = true;
            this.button_DeleteCrate.Click += new System.EventHandler(this.button_DeleteCrate_Click);
            // 
            // button_DownloadCrates
            // 
            this.button_DownloadCrates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DownloadCrates.ImageKey = "world";
            this.button_DownloadCrates.ImageList = this.imageList1;
            this.button_DownloadCrates.Location = new System.Drawing.Point(391, 0);
            this.button_DownloadCrates.Margin = new System.Windows.Forms.Padding(0);
            this.button_DownloadCrates.Name = "button_DownloadCrates";
            this.button_DownloadCrates.Size = new System.Drawing.Size(83, 30);
            this.button_DownloadCrates.TabIndex = 13;
            this.button_DownloadCrates.UseVisualStyleBackColor = true;
            this.button_DownloadCrates.Click += new System.EventHandler(this.button_DownloadCrates_Click);
            // 
            // button_EditCrate
            // 
            this.button_EditCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_EditCrate.Location = new System.Drawing.Point(148, 0);
            this.button_EditCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_EditCrate.Name = "button_EditCrate";
            this.button_EditCrate.Size = new System.Drawing.Size(81, 30);
            this.button_EditCrate.TabIndex = 14;
            this.button_EditCrate.Text = "Edit";
            this.button_EditCrate.UseVisualStyleBackColor = true;
            this.button_EditCrate.Click += new System.EventHandler(this.button_EditCrate_Click);
            // 
            // button_RefreshCrates
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_RefreshCrates, 2);
            this.button_RefreshCrates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RefreshCrates.ImageKey = "refresh";
            this.button_RefreshCrates.ImageList = this.imageList1;
            this.button_RefreshCrates.Location = new System.Drawing.Point(0, 0);
            this.button_RefreshCrates.Margin = new System.Windows.Forms.Padding(0);
            this.button_RefreshCrates.Name = "button_RefreshCrates";
            this.button_RefreshCrates.Size = new System.Drawing.Size(67, 30);
            this.button_RefreshCrates.TabIndex = 15;
            this.button_RefreshCrates.UseVisualStyleBackColor = true;
            this.button_RefreshCrates.Click += new System.EventHandler(this.button_RefreshCrates_Click);
            // 
            // contextMenuStrip_ImportCrate
            // 
            this.contextMenuStrip_ImportCrate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_FromFile,
            this.toolStripMenuItem_FromFolder});
            this.contextMenuStrip_ImportCrate.Name = "contextMenuStrip_ImportCrate";
            this.contextMenuStrip_ImportCrate.Size = new System.Drawing.Size(161, 48);
            // 
            // toolStripMenuItem_FromFile
            // 
            this.toolStripMenuItem_FromFile.Name = "toolStripMenuItem_FromFile";
            this.toolStripMenuItem_FromFile.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_FromFile.Text = "Import File (.zip)";
            this.toolStripMenuItem_FromFile.Click += new System.EventHandler(this.toolStripMenuItem_FromFile_Click);
            // 
            // toolStripMenuItem_FromFolder
            // 
            this.toolStripMenuItem_FromFolder.Name = "toolStripMenuItem_FromFolder";
            this.toolStripMenuItem_FromFolder.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_FromFolder.Text = "Import Folder";
            this.toolStripMenuItem_FromFolder.Click += new System.EventHandler(this.toolStripMenuItem_FromFolder_Click);
            // 
            // contextMenuStrip_DownloadMods
            // 
            this.contextMenuStrip_DownloadMods.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.download_site_BT});
            this.contextMenuStrip_DownloadMods.Name = "contextMenuStrip_DownloadMods";
            this.contextMenuStrip_DownloadMods.Size = new System.Drawing.Size(198, 26);
            // 
            // download_site_BT
            // 
            this.download_site_BT.Name = "download_site_BT";
            this.download_site_BT.Size = new System.Drawing.Size(197, 22);
            this.download_site_BT.Text = "Visit Beyond Twinsanity";
            this.download_site_BT.Click += new System.EventHandler(this.download_site_BT_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CML");
            this.imageList1.Images.SetKeyName(1, "refresh");
            this.imageList1.Images.SetKeyName(2, "file-plus");
            this.imageList1.Images.SetKeyName(3, "file-minus");
            this.imageList1.Images.SetKeyName(4, "world");
            // 
            // ModCrateManagerBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ModCrateManagerBox";
            this.Size = new System.Drawing.Size(474, 400);
            this.Load += new System.EventHandler(this.ModCrateManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip_ImportCrate.ResumeLayout(false);
            this.contextMenuStrip_DownloadMods.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_mods;
        private System.Windows.Forms.Label label_author;
        private System.Windows.Forms.Label label_desc;
        private System.Windows.Forms.PictureBox pictureBox_ModIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_ToTop;
        private System.Windows.Forms.Button button_MoveUp;
        private System.Windows.Forms.Button button_MoveDown;
        private System.Windows.Forms.Button button_ToBottom;
        private System.Windows.Forms.Button button_ImportCrate;
        private System.Windows.Forms.Button button_CreateCrate;
        private System.Windows.Forms.Button button_DeleteCrate;
        private System.Windows.Forms.Button button_DownloadCrates;
        private System.Windows.Forms.Button button_EditCrate;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ImportCrate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FromFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FromFolder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_DownloadMods;
        private System.Windows.Forms.ToolStripMenuItem download_site_BT;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_RefreshCrates;
        private System.Windows.Forms.ImageList imageList1;
    }
}