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
            this.SuspendLayout();
            // 
            // checkedListBox_mods
            // 
            this.checkedListBox_mods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox_mods.FormattingEnabled = true;
            this.checkedListBox_mods.Items.AddRange(new object[] {
            "Example Mod Name (C:/Path_To_Mod/modcrate.zip)"});
            this.checkedListBox_mods.Location = new System.Drawing.Point(13, 13);
            this.checkedListBox_mods.Name = "checkedListBox_mods";
            this.checkedListBox_mods.Size = new System.Drawing.Size(377, 259);
            this.checkedListBox_mods.TabIndex = 0;
            // 
            // button_confirm
            // 
            this.button_confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_confirm.Location = new System.Drawing.Point(314, 281);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(75, 23);
            this.button_confirm.TabIndex = 1;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_importmod
            // 
            this.button_importmod.Location = new System.Drawing.Point(13, 280);
            this.button_importmod.Name = "button_importmod";
            this.button_importmod.Size = new System.Drawing.Size(103, 23);
            this.button_importmod.TabIndex = 2;
            this.button_importmod.Text = "Import Mod Crate";
            this.button_importmod.UseVisualStyleBackColor = true;
            this.button_importmod.Click += new System.EventHandler(this.button_importmod_Click);
            // 
            // ModCrateManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 316);
            this.Controls.Add(this.button_importmod);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.checkedListBox_mods);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModCrateManagerForm";
            this.ShowIcon = false;
            this.Text = "Mod Crate Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModCrateManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.ModCrateManagerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_mods;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_importmod;
    }
}