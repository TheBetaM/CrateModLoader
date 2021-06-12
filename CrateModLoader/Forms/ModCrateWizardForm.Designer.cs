namespace CrateModLoader.Forms
{
    partial class ModCrateWizardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCrateWizardForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DataThing.dat");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DataFolder", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("LooseFile.xml");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Root", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ReplaceFileDelta = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button_RestoreFile = new System.Windows.Forms.Button();
            this.button_RemoveFolder = new System.Windows.Forms.Button();
            this.button_RemoveFile = new System.Windows.Forms.Button();
            this.button_AddFile = new System.Windows.Forms.Button();
            this.treeView_files = new System.Windows.Forms.TreeView();
            this.button_SaveAs = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_ExportFile = new System.Windows.Forms.Button();
            this.button_AddFolder = new System.Windows.Forms.Button();
            this.button_ReplaceFile = new System.Windows.Forms.Button();
            this.button_ModMenu = new System.Windows.Forms.Button();
            this.comboBox_Layers = new System.Windows.Forms.ComboBox();
            this.button_EditInfo = new System.Windows.Forms.Button();
            this.button_LevelEditor = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button_ReplaceFileDelta, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_RestoreFile, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_RemoveFolder, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.button_RemoveFile, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.button_AddFile, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.treeView_files, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_SaveAs, 4, 9);
            this.tableLayoutPanel1.Controls.Add(this.button_Cancel, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.button_ExportFile, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_AddFolder, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.button_ReplaceFile, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_ModMenu, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Layers, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_EditInfo, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_LevelEditor, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 453);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_ReplaceFileDelta
            // 
            this.button_ReplaceFileDelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ReplaceFileDelta.ImageKey = "file-diff";
            this.button_ReplaceFileDelta.ImageList = this.imageList1;
            this.button_ReplaceFileDelta.Location = new System.Drawing.Point(96, 173);
            this.button_ReplaceFileDelta.Margin = new System.Windows.Forms.Padding(0);
            this.button_ReplaceFileDelta.Name = "button_ReplaceFileDelta";
            this.button_ReplaceFileDelta.Size = new System.Drawing.Size(48, 48);
            this.button_ReplaceFileDelta.TabIndex = 14;
            this.button_ReplaceFileDelta.UseVisualStyleBackColor = true;
            this.button_ReplaceFileDelta.Click += new System.EventHandler(this.button_ReplaceFileDelta_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "CML");
            this.imageList1.Images.SetKeyName(1, "file");
            this.imageList1.Images.SetKeyName(2, "file-alert");
            this.imageList1.Images.SetKeyName(3, "file-check");
            this.imageList1.Images.SetKeyName(4, "file-code");
            this.imageList1.Images.SetKeyName(5, "file-download");
            this.imageList1.Images.SetKeyName(6, "file-export");
            this.imageList1.Images.SetKeyName(7, "file-info");
            this.imageList1.Images.SetKeyName(8, "file-music");
            this.imageList1.Images.SetKeyName(9, "file-off");
            this.imageList1.Images.SetKeyName(10, "files");
            this.imageList1.Images.SetKeyName(11, "file-search");
            this.imageList1.Images.SetKeyName(12, "file-text");
            this.imageList1.Images.SetKeyName(13, "file-upload");
            this.imageList1.Images.SetKeyName(14, "file-x");
            this.imageList1.Images.SetKeyName(15, "file-zip");
            this.imageList1.Images.SetKeyName(16, "folder");
            this.imageList1.Images.SetKeyName(17, "folders");
            this.imageList1.Images.SetKeyName(18, "folder-x");
            this.imageList1.Images.SetKeyName(19, "photo");
            this.imageList1.Images.SetKeyName(20, "world");
            this.imageList1.Images.SetKeyName(21, "file-plus");
            this.imageList1.Images.SetKeyName(22, "file-minus");
            this.imageList1.Images.SetKeyName(23, "file-diff");
            this.imageList1.Images.SetKeyName(24, "folder-add");
            this.imageList1.Images.SetKeyName(25, "folder-minus");
            this.imageList1.Images.SetKeyName(26, "file-import");
            // 
            // button_RestoreFile
            // 
            this.button_RestoreFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RestoreFile.ImageKey = "file-x";
            this.button_RestoreFile.ImageList = this.imageList1;
            this.button_RestoreFile.Location = new System.Drawing.Point(48, 221);
            this.button_RestoreFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_RestoreFile.Name = "button_RestoreFile";
            this.button_RestoreFile.Size = new System.Drawing.Size(48, 48);
            this.button_RestoreFile.TabIndex = 13;
            this.button_RestoreFile.UseVisualStyleBackColor = true;
            this.button_RestoreFile.Click += new System.EventHandler(this.button_RestoreFile_Click);
            // 
            // button_RemoveFolder
            // 
            this.button_RemoveFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RemoveFolder.ImageKey = "folder-minus";
            this.button_RemoveFolder.ImageList = this.imageList1;
            this.button_RemoveFolder.Location = new System.Drawing.Point(96, 317);
            this.button_RemoveFolder.Margin = new System.Windows.Forms.Padding(0);
            this.button_RemoveFolder.Name = "button_RemoveFolder";
            this.button_RemoveFolder.Size = new System.Drawing.Size(48, 48);
            this.button_RemoveFolder.TabIndex = 11;
            this.button_RemoveFolder.UseVisualStyleBackColor = true;
            this.button_RemoveFolder.Click += new System.EventHandler(this.button_RemoveFolder_Click);
            // 
            // button_RemoveFile
            // 
            this.button_RemoveFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RemoveFile.ImageKey = "file-minus";
            this.button_RemoveFile.ImageList = this.imageList1;
            this.button_RemoveFile.Location = new System.Drawing.Point(48, 317);
            this.button_RemoveFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_RemoveFile.Name = "button_RemoveFile";
            this.button_RemoveFile.Size = new System.Drawing.Size(48, 48);
            this.button_RemoveFile.TabIndex = 10;
            this.button_RemoveFile.UseVisualStyleBackColor = true;
            this.button_RemoveFile.Click += new System.EventHandler(this.button_RemoveFile_Click);
            // 
            // button_AddFile
            // 
            this.button_AddFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_AddFile.ImageKey = "file-plus";
            this.button_AddFile.ImageList = this.imageList1;
            this.button_AddFile.Location = new System.Drawing.Point(48, 269);
            this.button_AddFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_AddFile.Name = "button_AddFile";
            this.button_AddFile.Size = new System.Drawing.Size(48, 48);
            this.button_AddFile.TabIndex = 6;
            this.button_AddFile.UseVisualStyleBackColor = true;
            this.button_AddFile.Click += new System.EventHandler(this.button_AddFile_Click);
            // 
            // treeView_files
            // 
            this.treeView_files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_files.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeView_files.ImageIndex = 0;
            this.treeView_files.ImageList = this.imageList1;
            this.treeView_files.Location = new System.Drawing.Point(192, 28);
            this.treeView_files.Margin = new System.Windows.Forms.Padding(0);
            this.treeView_files.Name = "treeView_files";
            treeNode1.ImageKey = "file";
            treeNode1.Name = "Node2";
            treeNode1.Text = "DataThing.dat";
            treeNode2.ImageKey = "folder";
            treeNode2.Name = "Node1";
            treeNode2.Text = "DataFolder";
            treeNode3.ImageKey = "file-text";
            treeNode3.Name = "Node3";
            treeNode3.Text = "LooseFile.xml";
            treeNode4.ImageKey = "file-zip";
            treeNode4.Name = "Node0";
            treeNode4.StateImageKey = "(none)";
            treeNode4.Text = "Root";
            this.treeView_files.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.tableLayoutPanel1.SetRowSpan(this.treeView_files, 8);
            this.treeView_files.SelectedImageIndex = 0;
            this.treeView_files.Size = new System.Drawing.Size(615, 385);
            this.treeView_files.TabIndex = 0;
            // 
            // button_SaveAs
            // 
            this.button_SaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_SaveAs.Location = new System.Drawing.Point(192, 413);
            this.button_SaveAs.Margin = new System.Windows.Forms.Padding(0);
            this.button_SaveAs.Name = "button_SaveAs";
            this.button_SaveAs.Size = new System.Drawing.Size(615, 40);
            this.button_SaveAs.TabIndex = 1;
            this.button_SaveAs.Text = "Save as...";
            this.button_SaveAs.UseVisualStyleBackColor = true;
            this.button_SaveAs.Click += new System.EventHandler(this.button_SaveAs_Click);
            // 
            // button_Cancel
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_Cancel, 4);
            this.button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Cancel.Location = new System.Drawing.Point(0, 413);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(0);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(192, 40);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_ExportFile
            // 
            this.button_ExportFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ExportFile.ImageKey = "file-download";
            this.button_ExportFile.ImageList = this.imageList1;
            this.button_ExportFile.Location = new System.Drawing.Point(96, 221);
            this.button_ExportFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_ExportFile.Name = "button_ExportFile";
            this.button_ExportFile.Size = new System.Drawing.Size(48, 48);
            this.button_ExportFile.TabIndex = 4;
            this.button_ExportFile.UseVisualStyleBackColor = true;
            this.button_ExportFile.Click += new System.EventHandler(this.button_ExportFile_Click);
            // 
            // button_AddFolder
            // 
            this.button_AddFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_AddFolder.ImageKey = "folder-add";
            this.button_AddFolder.ImageList = this.imageList1;
            this.button_AddFolder.Location = new System.Drawing.Point(96, 269);
            this.button_AddFolder.Margin = new System.Windows.Forms.Padding(0);
            this.button_AddFolder.Name = "button_AddFolder";
            this.button_AddFolder.Size = new System.Drawing.Size(48, 48);
            this.button_AddFolder.TabIndex = 3;
            this.button_AddFolder.UseVisualStyleBackColor = true;
            this.button_AddFolder.Click += new System.EventHandler(this.button_AddFolder_Click);
            // 
            // button_ReplaceFile
            // 
            this.button_ReplaceFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ReplaceFile.ImageKey = "file-import";
            this.button_ReplaceFile.ImageList = this.imageList1;
            this.button_ReplaceFile.Location = new System.Drawing.Point(48, 173);
            this.button_ReplaceFile.Margin = new System.Windows.Forms.Padding(0);
            this.button_ReplaceFile.Name = "button_ReplaceFile";
            this.button_ReplaceFile.Size = new System.Drawing.Size(48, 48);
            this.button_ReplaceFile.TabIndex = 5;
            this.button_ReplaceFile.UseVisualStyleBackColor = true;
            this.button_ReplaceFile.Click += new System.EventHandler(this.button_ReplaceFile_Click);
            // 
            // button_ModMenu
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_ModMenu, 2);
            this.button_ModMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ModMenu.Location = new System.Drawing.Point(48, 28);
            this.button_ModMenu.Margin = new System.Windows.Forms.Padding(0);
            this.button_ModMenu.Name = "button_ModMenu";
            this.button_ModMenu.Size = new System.Drawing.Size(96, 49);
            this.button_ModMenu.TabIndex = 7;
            this.button_ModMenu.Text = "Mod Menu";
            this.button_ModMenu.UseVisualStyleBackColor = true;
            this.button_ModMenu.Click += new System.EventHandler(this.button_ModMenu_Click);
            // 
            // comboBox_Layers
            // 
            this.comboBox_Layers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Layers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox_Layers.FormattingEnabled = true;
            this.comboBox_Layers.Items.AddRange(new object[] {
            "Layer 0",
            "Layer 1"});
            this.comboBox_Layers.Location = new System.Drawing.Point(192, 0);
            this.comboBox_Layers.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_Layers.Name = "comboBox_Layers";
            this.comboBox_Layers.Size = new System.Drawing.Size(615, 28);
            this.comboBox_Layers.TabIndex = 8;
            this.comboBox_Layers.Text = "Layer 0: Extracted files";
            this.comboBox_Layers.SelectedIndexChanged += new System.EventHandler(this.comboBox_Layers_SelectedIndexChanged);
            // 
            // button_EditInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_EditInfo, 2);
            this.button_EditInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_EditInfo.Location = new System.Drawing.Point(48, 125);
            this.button_EditInfo.Margin = new System.Windows.Forms.Padding(0);
            this.button_EditInfo.Name = "button_EditInfo";
            this.button_EditInfo.Size = new System.Drawing.Size(96, 48);
            this.button_EditInfo.TabIndex = 9;
            this.button_EditInfo.Text = "Edit Crate Info";
            this.button_EditInfo.UseVisualStyleBackColor = true;
            this.button_EditInfo.Click += new System.EventHandler(this.button_EditInfo_Click);
            // 
            // button_LevelEditor
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_LevelEditor, 2);
            this.button_LevelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LevelEditor.Enabled = false;
            this.button_LevelEditor.Location = new System.Drawing.Point(48, 77);
            this.button_LevelEditor.Margin = new System.Windows.Forms.Padding(0);
            this.button_LevelEditor.Name = "button_LevelEditor";
            this.button_LevelEditor.Size = new System.Drawing.Size(96, 48);
            this.button_LevelEditor.TabIndex = 12;
            this.button_LevelEditor.Text = "Level Editor";
            this.button_LevelEditor.UseVisualStyleBackColor = true;
            this.button_LevelEditor.Click += new System.EventHandler(this.button_LevelEditor_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ModCrateWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 453);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ModCrateWizardForm";
            this.Text = "Mod Crate Wizard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateWizardForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateWizardForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button_SaveAs;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_AddFolder;
        private System.Windows.Forms.Button button_ExportFile;
        private System.Windows.Forms.Button button_AddFile;
        private System.Windows.Forms.Button button_ReplaceFile;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_ModMenu;
        private System.Windows.Forms.ComboBox comboBox_Layers;
        private System.Windows.Forms.TreeView treeView_files;
        private System.Windows.Forms.Button button_EditInfo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_RemoveFolder;
        private System.Windows.Forms.Button button_RemoveFile;
        private System.Windows.Forms.Button button_LevelEditor;
        private System.Windows.Forms.Button button_RestoreFile;
        private System.Windows.Forms.Button button_ReplaceFileDelta;
    }
}