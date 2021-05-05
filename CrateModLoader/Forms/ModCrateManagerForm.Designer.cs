namespace CrateModLoader
{
    partial class ModCrateManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCrateManagerForm));
            this.checkedListBox_mods = new System.Windows.Forms.CheckedListBox();
            this.button_confirm = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox_mods
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.checkedListBox_mods, 5);
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
            this.checkedListBox_mods.Size = new System.Drawing.Size(421, 232);
            this.checkedListBox_mods.TabIndex = 0;
            this.checkedListBox_mods.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_mods_ItemCheck);
            this.checkedListBox_mods.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_mods_SelectedIndexChanged);
            this.checkedListBox_mods.MouseMove += new System.Windows.Forms.MouseEventHandler(this.checkedListBox_mods_MouseMove);
            // 
            // button_confirm
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_confirm, 7);
            this.button_confirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_confirm.Location = new System.Drawing.Point(0, 329);
            this.button_confirm.Margin = new System.Windows.Forms.Padding(0);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(458, 32);
            this.button_confirm.TabIndex = 1;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // label_author
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_author, 4);
            this.label_author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_author.Location = new System.Drawing.Point(70, 262);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(385, 20);
            this.label_author.TabIndex = 3;
            this.label_author.Text = "Mod Author";
            this.label_author.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_desc
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_desc, 4);
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.Location = new System.Drawing.Point(70, 282);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(385, 47);
            this.label_desc.TabIndex = 4;
            this.label_desc.Text = "Mod Description";
            // 
            // pictureBox_ModIcon
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox_ModIcon, 2);
            this.pictureBox_ModIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ModIcon.InitialImage = null;
            this.pictureBox_ModIcon.Location = new System.Drawing.Point(3, 265);
            this.pictureBox_ModIcon.Name = "pictureBox_ModIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox_ModIcon, 2);
            this.pictureBox_ModIcon.Size = new System.Drawing.Size(61, 61);
            this.pictureBox_ModIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_ModIcon.TabIndex = 5;
            this.pictureBox_ModIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox_mods, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_confirm, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label_desc, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ModIcon, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_author, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_ToTop, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_MoveUp, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_MoveDown, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_ToBottom, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.button_ImportCrate, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_CreateCrate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_DeleteCrate, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_DownloadCrates, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(458, 361);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // button_ToTop
            // 
            this.button_ToTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ToTop.Location = new System.Drawing.Point(0, 30);
            this.button_ToTop.Margin = new System.Windows.Forms.Padding(0);
            this.button_ToTop.Name = "button_ToTop";
            this.button_ToTop.Size = new System.Drawing.Size(37, 58);
            this.button_ToTop.TabIndex = 6;
            this.button_ToTop.Text = "⭱";
            this.button_ToTop.UseVisualStyleBackColor = true;
            this.button_ToTop.Click += new System.EventHandler(this.button_ToTop_Click);
            // 
            // button_MoveUp
            // 
            this.button_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_MoveUp.Location = new System.Drawing.Point(0, 88);
            this.button_MoveUp.Margin = new System.Windows.Forms.Padding(0);
            this.button_MoveUp.Name = "button_MoveUp";
            this.button_MoveUp.Size = new System.Drawing.Size(37, 58);
            this.button_MoveUp.TabIndex = 7;
            this.button_MoveUp.Text = "↑";
            this.button_MoveUp.UseVisualStyleBackColor = true;
            this.button_MoveUp.Click += new System.EventHandler(this.button_MoveUp_Click);
            // 
            // button_MoveDown
            // 
            this.button_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_MoveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_MoveDown.Location = new System.Drawing.Point(0, 146);
            this.button_MoveDown.Margin = new System.Windows.Forms.Padding(0);
            this.button_MoveDown.Name = "button_MoveDown";
            this.button_MoveDown.Size = new System.Drawing.Size(37, 58);
            this.button_MoveDown.TabIndex = 8;
            this.button_MoveDown.Text = "↓";
            this.button_MoveDown.UseVisualStyleBackColor = true;
            this.button_MoveDown.Click += new System.EventHandler(this.button_MoveDown_Click);
            // 
            // button_ToBottom
            // 
            this.button_ToBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ToBottom.Location = new System.Drawing.Point(0, 204);
            this.button_ToBottom.Margin = new System.Windows.Forms.Padding(0);
            this.button_ToBottom.Name = "button_ToBottom";
            this.button_ToBottom.Size = new System.Drawing.Size(37, 58);
            this.button_ToBottom.TabIndex = 9;
            this.button_ToBottom.Text = "⭳";
            this.button_ToBottom.UseVisualStyleBackColor = true;
            this.button_ToBottom.Click += new System.EventHandler(this.button_ToBottom_Click);
            // 
            // button_ImportCrate
            // 
            this.button_ImportCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ImportCrate.Enabled = false;
            this.button_ImportCrate.Location = new System.Drawing.Point(67, 0);
            this.button_ImportCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_ImportCrate.Name = "button_ImportCrate";
            this.button_ImportCrate.Size = new System.Drawing.Size(97, 30);
            this.button_ImportCrate.TabIndex = 10;
            this.button_ImportCrate.Text = "Import...";
            this.button_ImportCrate.UseVisualStyleBackColor = true;
            this.button_ImportCrate.Click += new System.EventHandler(this.button_ImportCrate_Click);
            // 
            // button_CreateCrate
            // 
            this.button_CreateCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_CreateCrate.Enabled = false;
            this.button_CreateCrate.Location = new System.Drawing.Point(164, 0);
            this.button_CreateCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_CreateCrate.Name = "button_CreateCrate";
            this.button_CreateCrate.Size = new System.Drawing.Size(97, 30);
            this.button_CreateCrate.TabIndex = 11;
            this.button_CreateCrate.Text = "Create...";
            this.button_CreateCrate.UseVisualStyleBackColor = true;
            this.button_CreateCrate.Click += new System.EventHandler(this.button_CreateCrate_Click);
            // 
            // button_DeleteCrate
            // 
            this.button_DeleteCrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DeleteCrate.Enabled = false;
            this.button_DeleteCrate.Location = new System.Drawing.Point(261, 0);
            this.button_DeleteCrate.Margin = new System.Windows.Forms.Padding(0);
            this.button_DeleteCrate.Name = "button_DeleteCrate";
            this.button_DeleteCrate.Size = new System.Drawing.Size(97, 30);
            this.button_DeleteCrate.TabIndex = 12;
            this.button_DeleteCrate.Text = "Delete";
            this.button_DeleteCrate.UseVisualStyleBackColor = true;
            this.button_DeleteCrate.Click += new System.EventHandler(this.button_DeleteCrate_Click);
            // 
            // button_DownloadCrates
            // 
            this.button_DownloadCrates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_DownloadCrates.Location = new System.Drawing.Point(358, 0);
            this.button_DownloadCrates.Margin = new System.Windows.Forms.Padding(0);
            this.button_DownloadCrates.Name = "button_DownloadCrates";
            this.button_DownloadCrates.Size = new System.Drawing.Size(100, 30);
            this.button_DownloadCrates.TabIndex = 13;
            this.button_DownloadCrates.Text = "Download";
            this.button_DownloadCrates.UseVisualStyleBackColor = true;
            this.button_DownloadCrates.Click += new System.EventHandler(this.button_DownloadCrates_Click);
            // 
            // ModCrateManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ModCrateManagerForm";
            this.Text = "Mod Crate Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_mods;
        private System.Windows.Forms.Button button_confirm;
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
    }
}