namespace TwinsaityEditor.Workers
{
    partial class TextureImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureImport));
            this.ofdSelect = new System.Windows.Forms.OpenFileDialog();
            this.btnSelector = new System.Windows.Forms.Button();
            this.cbFormats = new System.Windows.Forms.ComboBox();
            this.lbFormat = new System.Windows.Forms.Label();
            this.listImages = new System.Windows.Forms.ListView();
            this.lbImageList = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.pbImport = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // ofdSelect
            // 
            this.ofdSelect.Filter = "PNG|*.png";
            this.ofdSelect.Multiselect = true;
            this.ofdSelect.Title = "Select images to import";
            // 
            // btnSelector
            // 
            this.btnSelector.Location = new System.Drawing.Point(12, 163);
            this.btnSelector.Name = "btnSelector";
            this.btnSelector.Size = new System.Drawing.Size(86, 23);
            this.btnSelector.TabIndex = 0;
            this.btnSelector.Text = "Select images";
            this.btnSelector.UseVisualStyleBackColor = true;
            this.btnSelector.Click += new System.EventHandler(this.btnSelector_Click);
            // 
            // cbFormats
            // 
            this.cbFormats.FormattingEnabled = true;
            this.cbFormats.Items.AddRange(new object[] {
            "128x256 ",
            "128x128 ",
            "128x128 mip",
            "128x64 (N/A)",
            "128x64 mip",
            "128x32 (N/A)",
            "128x32 mip",
            "64x64 ",
            "64x64 mip",
            "64x32 (N/A)",
            "64x32 mip",
            "32x64 ",
            "32x64 mip",
            "32x32 (N/A)",
            "32x32 mip",
            "32x16 (N/A)",
            "32x16 mip",
            "32x8 ",
            "16x16 (N/A)",
            "16x16 mip"});
            this.cbFormats.Location = new System.Drawing.Point(86, 6);
            this.cbFormats.Name = "cbFormats";
            this.cbFormats.Size = new System.Drawing.Size(121, 21);
            this.cbFormats.TabIndex = 1;
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(9, 9);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(71, 13);
            this.lbFormat.TabIndex = 2;
            this.lbFormat.Text = "Import format:";
            // 
            // listImages
            // 
            this.listImages.Location = new System.Drawing.Point(12, 60);
            this.listImages.Name = "listImages";
            this.listImages.Size = new System.Drawing.Size(194, 97);
            this.listImages.TabIndex = 3;
            this.listImages.UseCompatibleStateImageBehavior = false;
            // 
            // lbImageList
            // 
            this.lbImageList.AutoSize = true;
            this.lbImageList.Location = new System.Drawing.Point(67, 44);
            this.lbImageList.Name = "lbImageList";
            this.lbImageList.Size = new System.Drawing.Size(85, 13);
            this.lbImageList.TabIndex = 4;
            this.lbImageList.Text = "Selected images";
            this.lbImageList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(120, 163);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(86, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pbImport
            // 
            this.pbImport.Location = new System.Drawing.Point(12, 192);
            this.pbImport.Name = "pbImport";
            this.pbImport.Size = new System.Drawing.Size(194, 22);
            this.pbImport.TabIndex = 6;
            this.pbImport.Visible = false;
            // 
            // TextureImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 226);
            this.Controls.Add(this.pbImport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lbImageList);
            this.Controls.Add(this.listImages);
            this.Controls.Add(this.lbFormat);
            this.Controls.Add(this.cbFormats);
            this.Controls.Add(this.btnSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextureImport";
            this.Text = "Texture Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdSelect;
        private System.Windows.Forms.Button btnSelector;
        private System.Windows.Forms.ComboBox cbFormats;
        private System.Windows.Forms.Label lbFormat;
        private System.Windows.Forms.ListView listImages;
        private System.Windows.Forms.Label lbImageList;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar pbImport;
    }
}