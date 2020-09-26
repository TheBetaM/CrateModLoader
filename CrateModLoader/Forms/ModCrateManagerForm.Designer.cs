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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox_mods
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.checkedListBox_mods, 2);
            this.checkedListBox_mods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox_mods.FormattingEnabled = true;
            this.checkedListBox_mods.Items.AddRange(new object[] {
            "Example Mod Name (v1.0)",
            "Example Mod Name 2 (v1.1)",
            "Example Mod Name 3 (v1.2)"});
            this.checkedListBox_mods.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox_mods.Margin = new System.Windows.Forms.Padding(0);
            this.checkedListBox_mods.Name = "checkedListBox_mods";
            this.checkedListBox_mods.Size = new System.Drawing.Size(384, 264);
            this.checkedListBox_mods.TabIndex = 0;
            this.checkedListBox_mods.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_mods_ItemCheck);
            this.checkedListBox_mods.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_mods_SelectedIndexChanged);
            // 
            // button_confirm
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_confirm, 2);
            this.button_confirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_confirm.Location = new System.Drawing.Point(0, 331);
            this.button_confirm.Margin = new System.Windows.Forms.Padding(0);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(384, 30);
            this.button_confirm.TabIndex = 1;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // label_author
            // 
            this.label_author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_author.Location = new System.Drawing.Point(70, 264);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(311, 20);
            this.label_author.TabIndex = 3;
            this.label_author.Text = "Mod Author";
            this.label_author.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_desc
            // 
            this.label_desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_desc.Location = new System.Drawing.Point(70, 284);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(311, 47);
            this.label_desc.TabIndex = 4;
            this.label_desc.Text = "Mod Description";
            // 
            // pictureBox_ModIcon
            // 
            this.pictureBox_ModIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ModIcon.InitialImage = null;
            this.pictureBox_ModIcon.Location = new System.Drawing.Point(3, 267);
            this.pictureBox_ModIcon.Name = "pictureBox_ModIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox_ModIcon, 2);
            this.pictureBox_ModIcon.Size = new System.Drawing.Size(61, 61);
            this.pictureBox_ModIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_ModIcon.TabIndex = 5;
            this.pictureBox_ModIcon.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox_mods, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_confirm, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_desc, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ModIcon, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_author, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 361);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // ModCrateManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
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
    }
}