namespace CrateModLoader
{
    partial class ModToolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModToolsForm));
            this.textBox_outputfolder = new System.Windows.Forms.TextBox();
            this.label_outputfolder = new System.Windows.Forms.Label();
            this.button_outputfolder_browse = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.comboBox_extractlayer = new System.Windows.Forms.ComboBox();
            this.label_extractlayer = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_modcratebuilder = new System.Windows.Forms.Button();
            this.label_processinfo = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label_layerinfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_outputfolder
            // 
            this.textBox_outputfolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_outputfolder.Location = new System.Drawing.Point(2, 67);
            this.textBox_outputfolder.Name = "textBox_outputfolder";
            this.textBox_outputfolder.Size = new System.Drawing.Size(300, 20);
            this.textBox_outputfolder.TabIndex = 0;
            // 
            // label_outputfolder
            // 
            this.label_outputfolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_outputfolder.Location = new System.Drawing.Point(2, 41);
            this.label_outputfolder.Name = "label_outputfolder";
            this.label_outputfolder.Size = new System.Drawing.Size(381, 22);
            this.label_outputfolder.TabIndex = 1;
            this.label_outputfolder.Text = "Extraction Output Folder";
            this.label_outputfolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_outputfolder_browse
            // 
            this.button_outputfolder_browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_outputfolder_browse.Location = new System.Drawing.Point(308, 66);
            this.button_outputfolder_browse.Name = "button_outputfolder_browse";
            this.button_outputfolder_browse.Size = new System.Drawing.Size(75, 22);
            this.button_outputfolder_browse.TabIndex = 2;
            this.button_outputfolder_browse.Text = "Browse...";
            this.button_outputfolder_browse.UseVisualStyleBackColor = true;
            // 
            // button_start
            // 
            this.button_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_start.Location = new System.Drawing.Point(247, 164);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(136, 25);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Start!";
            this.button_start.UseVisualStyleBackColor = true;
            // 
            // comboBox_extractlayer
            // 
            this.comboBox_extractlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_extractlayer.FormattingEnabled = true;
            this.comboBox_extractlayer.Location = new System.Drawing.Point(179, 107);
            this.comboBox_extractlayer.Name = "comboBox_extractlayer";
            this.comboBox_extractlayer.Size = new System.Drawing.Size(204, 21);
            this.comboBox_extractlayer.TabIndex = 4;
            // 
            // label_extractlayer
            // 
            this.label_extractlayer.Location = new System.Drawing.Point(2, 106);
            this.label_extractlayer.Name = "label_extractlayer";
            this.label_extractlayer.Size = new System.Drawing.Size(171, 21);
            this.label_extractlayer.TabIndex = 5;
            this.label_extractlayer.Text = "Extract Layer";
            this.label_extractlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(2, 165);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // button_modcratebuilder
            // 
            this.button_modcratebuilder.Enabled = false;
            this.button_modcratebuilder.Location = new System.Drawing.Point(5, 7);
            this.button_modcratebuilder.Name = "button_modcratebuilder";
            this.button_modcratebuilder.Size = new System.Drawing.Size(168, 23);
            this.button_modcratebuilder.TabIndex = 7;
            this.button_modcratebuilder.Text = "Mod Crate Builder";
            this.button_modcratebuilder.UseVisualStyleBackColor = true;
            // 
            // label_processinfo
            // 
            this.label_processinfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_processinfo.Location = new System.Drawing.Point(2, 192);
            this.label_processinfo.Name = "label_processinfo";
            this.label_processinfo.Size = new System.Drawing.Size(381, 31);
            this.label_processinfo.TabIndex = 8;
            this.label_processinfo.Text = "Process Info";
            this.label_processinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_layerinfo
            // 
            this.label_layerinfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_layerinfo.Location = new System.Drawing.Point(2, 131);
            this.label_layerinfo.Name = "label_layerinfo";
            this.label_layerinfo.Size = new System.Drawing.Size(381, 31);
            this.label_layerinfo.TabIndex = 9;
            this.label_layerinfo.Text = "Layer Info";
            this.label_layerinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ModToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 231);
            this.Controls.Add(this.label_layerinfo);
            this.Controls.Add(this.label_processinfo);
            this.Controls.Add(this.button_modcratebuilder);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_extractlayer);
            this.Controls.Add(this.comboBox_extractlayer);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_outputfolder_browse);
            this.Controls.Add(this.label_outputfolder);
            this.Controls.Add(this.textBox_outputfolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 270);
            this.Name = "ModToolsForm";
            this.Text = "Mod Tools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_outputfolder;
        private System.Windows.Forms.Label label_outputfolder;
        private System.Windows.Forms.Button button_outputfolder_browse;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.ComboBox comboBox_extractlayer;
        private System.Windows.Forms.Label label_extractlayer;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_modcratebuilder;
        private System.Windows.Forms.Label label_processinfo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label_layerinfo;
    }
}