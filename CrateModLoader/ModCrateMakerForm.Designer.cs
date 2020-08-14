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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CrateModLoader.Properties.Resources.cml_icon;
            this.pictureBox1.Location = new System.Drawing.Point(26, 161);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Location = new System.Drawing.Point(120, 235);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(505, 27);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_name.Location = new System.Drawing.Point(120, 9);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(504, 20);
            this.textBox_name.TabIndex = 2;
            this.textBox_name.TextChanged += new System.EventHandler(this.textBox_name_TextChanged);
            // 
            // textBox_author
            // 
            this.textBox_author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_author.Location = new System.Drawing.Point(120, 35);
            this.textBox_author.Name = "textBox_author";
            this.textBox_author.Size = new System.Drawing.Size(504, 20);
            this.textBox_author.TabIndex = 3;
            this.textBox_author.TextChanged += new System.EventHandler(this.textBox_author_TextChanged);
            // 
            // textBox_version
            // 
            this.textBox_version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_version.Location = new System.Drawing.Point(120, 61);
            this.textBox_version.Name = "textBox_version";
            this.textBox_version.Size = new System.Drawing.Size(504, 20);
            this.textBox_version.TabIndex = 4;
            this.textBox_version.TextChanged += new System.EventHandler(this.textBox_version_TextChanged);
            // 
            // textBox_description
            // 
            this.textBox_description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_description.Location = new System.Drawing.Point(120, 87);
            this.textBox_description.Multiline = true;
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(504, 141);
            this.textBox_description.TabIndex = 5;
            this.textBox_description.TextChanged += new System.EventHandler(this.textBox_description_TextChanged);
            // 
            // label_author
            // 
            this.label_author.Location = new System.Drawing.Point(12, 38);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(100, 13);
            this.label_author.TabIndex = 7;
            this.label_author.Text = "Author";
            this.label_author.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_name
            // 
            this.label_name.Location = new System.Drawing.Point(12, 12);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(100, 13);
            this.label_name.TabIndex = 8;
            this.label_name.Text = "Name";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_description
            // 
            this.label_description.Location = new System.Drawing.Point(12, 90);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(100, 13);
            this.label_description.TabIndex = 9;
            this.label_description.Text = "Description";
            this.label_description.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_version
            // 
            this.label_version.Location = new System.Drawing.Point(12, 64);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(100, 13);
            this.label_version.TabIndex = 10;
            this.label_version.Text = "Version";
            this.label_version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_icon
            // 
            this.label_icon.Location = new System.Drawing.Point(10, 116);
            this.label_icon.Name = "label_icon";
            this.label_icon.Size = new System.Drawing.Size(100, 13);
            this.label_icon.TabIndex = 11;
            this.label_icon.Text = "Icon";
            this.label_icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(10, 132);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(100, 23);
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
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.Location = new System.Drawing.Point(0, 235);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(111, 27);
            this.button_cancel.TabIndex = 13;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // ModCrateMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 261);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.label_icon);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.label_author);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.textBox_version);
            this.Controls.Add(this.textBox_author);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 300);
            this.Name = "ModCrateMakerForm";
            this.Text = "Mod Crate Maker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateMakerForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateMakerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}