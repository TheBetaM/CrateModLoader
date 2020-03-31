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
            this.button_importmod = new System.Windows.Forms.Button();
            this.label_author = new System.Windows.Forms.Label();
            this.label_desc = new System.Windows.Forms.Label();
            this.pictureBox_ModIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // checkedListBox_mods
            // 
            this.checkedListBox_mods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox_mods.FormattingEnabled = true;
            this.checkedListBox_mods.Items.AddRange(new object[] {
            "Example Mod Name (v1.0)",
            "Example Mod Name 2 (v1.1)",
            "Example Mod Name 3 (v1.2)"});
            this.checkedListBox_mods.Location = new System.Drawing.Point(13, 13);
            this.checkedListBox_mods.Name = "checkedListBox_mods";
            this.checkedListBox_mods.Size = new System.Drawing.Size(359, 259);
            this.checkedListBox_mods.TabIndex = 0;
            this.checkedListBox_mods.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_mods_ItemCheck);
            this.checkedListBox_mods.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_mods_SelectedIndexChanged);
            // 
            // button_confirm
            // 
            this.button_confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_confirm.Location = new System.Drawing.Point(296, 326);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(75, 23);
            this.button_confirm.TabIndex = 1;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_importmod
            // 
            this.button_importmod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_importmod.Enabled = false;
            this.button_importmod.Location = new System.Drawing.Point(268, 285);
            this.button_importmod.Name = "button_importmod";
            this.button_importmod.Size = new System.Drawing.Size(103, 23);
            this.button_importmod.TabIndex = 2;
            this.button_importmod.Text = "Import Mod Crate";
            this.button_importmod.UseVisualStyleBackColor = true;
            this.button_importmod.Visible = false;
            this.button_importmod.Click += new System.EventHandler(this.button_importmod_Click);
            // 
            // label_author
            // 
            this.label_author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_author.AutoSize = true;
            this.label_author.Location = new System.Drawing.Point(86, 285);
            this.label_author.Name = "label_author";
            this.label_author.Size = new System.Drawing.Size(62, 13);
            this.label_author.TabIndex = 3;
            this.label_author.Text = "Mod Author";
            // 
            // label_desc
            // 
            this.label_desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_desc.Location = new System.Drawing.Point(86, 305);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(204, 44);
            this.label_desc.TabIndex = 4;
            this.label_desc.Text = "Mod Description";
            // 
            // pictureBox_ModIcon
            // 
            this.pictureBox_ModIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_ModIcon.Location = new System.Drawing.Point(16, 285);
            this.pictureBox_ModIcon.Name = "pictureBox_ModIcon";
            this.pictureBox_ModIcon.Size = new System.Drawing.Size(64, 64);
            this.pictureBox_ModIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_ModIcon.TabIndex = 5;
            this.pictureBox_ModIcon.TabStop = false;
            // 
            // ModCrateManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.pictureBox_ModIcon);
            this.Controls.Add(this.label_desc);
            this.Controls.Add(this.label_author);
            this.Controls.Add(this.button_importmod);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.checkedListBox_mods);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ModCrateManagerForm";
            this.ShowIcon = false;
            this.Text = "Mod Crate Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ModIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_mods;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_importmod;
        private System.Windows.Forms.Label label_author;
        private System.Windows.Forms.Label label_desc;
        private System.Windows.Forms.PictureBox pictureBox_ModIcon;
    }
}