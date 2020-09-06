namespace CrateModLoader
{
    partial class ModCrateMakerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModCrateMakerForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_author = new System.Windows.Forms.TextBox();
            this.textBox_version = new System.Windows.Forms.TextBox();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.label_author = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.label_description = new System.Windows.Forms.Label();
            this.label_version = new System.Windows.Forms.Label();
            this.label_icon = new System.Windows.Forms.Label();
            this.button_browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::CrateModLoader.Properties.Resources.cml_icon;
            this.pictureBox1.Location = new System.Drawing.Point(18, 169);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_save
            // 
            this.button_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_save.Location = new System.Drawing.Point(100, 251);
            this.button_save.Margin = new System.Windows.Forms.Padding(0);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(524, 30);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_name.Location = new System.Drawing.Point(100, 0);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(524, 20);
            this.textBox_name.TabIndex = 2;
            this.textBox_name.TextChanged += new System.EventHandler(this.textBox_name_TextChanged);
            // 
            // textBox_author
            // 
            this.textBox_author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_author.Location = new System.Drawing.Point(100, 23);
            this.textBox_author.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_author.Name = "textBox_author";
            this.textBox_author.Size = new System.Drawing.Size(524, 20);
            this.textBox_author.TabIndex = 3;
            this.textBox_author.TextChanged += new System.EventHandler(this.textBox_author_TextChanged);
            // 
            // textBox_version
            // 
            this.textBox_version.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_version.Location = new System.Drawing.Point(100, 46);
            this.textBox_version.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_version.Name = "textBox_version";
            this.textBox_version.Size = new System.Drawing.Size(524, 20);
            this.textBox_version.TabIndex = 4;
            this.textBox_version.TextChanged += new System.EventHandler(this.textBox_version_TextChanged);
            // 
            // textBox_description
            // 
            this.textBox_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_description.Location = new System.Drawing.Point(100, 69);
            this.textBox_description.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.tableLayoutPanel1.SetRowSpan(this.textBox_description, 4);
            this.textBox_description.Size = new System.Drawing.Size(524, 182);
            this.textBox_description.TabIndex = 5;
            this.textBox_description.TextChanged += new System.EventHandler(this.textBox_description_TextChanged);
            // 
            // label_author
            // 
            this.label_author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_author.Location = new System.Drawing.Point(3, 23);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(94, 23);
            this.label_author.TabIndex = 7;
            this.label_author.Text = "Author";
            this.label_author.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_name
            // 
            this.label_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_name.Location = new System.Drawing.Point(3, 0);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(94, 23);
            this.label_name.TabIndex = 8;
            this.label_name.Text = "Name";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_description
            // 
            this.label_description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_description.Location = new System.Drawing.Point(3, 69);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(94, 29);
            this.label_description.TabIndex = 9;
            this.label_description.Text = "Description";
            this.label_description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_version
            // 
            this.label_version.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_version.Location = new System.Drawing.Point(3, 46);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(94, 23);
            this.label_version.TabIndex = 10;
            this.label_version.Text = "Version";
            this.label_version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_icon
            // 
            this.label_icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_icon.Location = new System.Drawing.Point(3, 98);
            this.label_icon.Name = "label_icon";
            this.label_icon.Size = new System.Drawing.Size(94, 23);
            this.label_icon.TabIndex = 11;
            this.label_icon.Text = "Icon";
            this.label_icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_browse
            // 
            this.button_browse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_browse.Location = new System.Drawing.Point(0, 121);
            this.button_browse.Margin = new System.Windows.Forms.Padding(0);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(100, 30);
            this.button_browse.TabIndex = 12;
            this.button_browse.Text = "Browse...";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_cancel
            // 
            this.button_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_cancel.Location = new System.Drawing.Point(0, 251);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(0);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 30);
            this.button_cancel.TabIndex = 13;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label_name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_save, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.button_cancel, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBox_name, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_browse, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label_author, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_icon, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.textBox_author, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_description, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_description, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_version, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_version, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 281);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // ModCrateMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 281);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 320);
            this.Name = "ModCrateMakerForm";
            this.Text = "Simple Mod Crate Maker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateMakerForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateMakerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_author;
        private System.Windows.Forms.TextBox textBox_version;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.Label label_author;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Label label_icon;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}