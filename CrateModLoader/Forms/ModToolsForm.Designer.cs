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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_outputfolder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_outputfolder, 2);
            this.textBox_outputfolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_outputfolder.Location = new System.Drawing.Point(0, 52);
            this.textBox_outputfolder.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_outputfolder.Name = "textBox_outputfolder";
            this.textBox_outputfolder.Size = new System.Drawing.Size(307, 20);
            this.textBox_outputfolder.TabIndex = 0;
            // 
            // label_outputfolder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_outputfolder, 3);
            this.label_outputfolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_outputfolder.Location = new System.Drawing.Point(3, 30);
            this.label_outputfolder.Name = "label_outputfolder";
            this.label_outputfolder.Size = new System.Drawing.Size(378, 22);
            this.label_outputfolder.TabIndex = 1;
            this.label_outputfolder.Text = "Extraction Output Folder";
            this.label_outputfolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_outputfolder_browse
            // 
            this.button_outputfolder_browse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_outputfolder_browse.Location = new System.Drawing.Point(307, 52);
            this.button_outputfolder_browse.Margin = new System.Windows.Forms.Padding(0);
            this.button_outputfolder_browse.Name = "button_outputfolder_browse";
            this.button_outputfolder_browse.Size = new System.Drawing.Size(77, 22);
            this.button_outputfolder_browse.TabIndex = 2;
            this.button_outputfolder_browse.Text = "Browse...";
            this.button_outputfolder_browse.UseVisualStyleBackColor = true;
            // 
            // button_start
            // 
            this.button_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_start.Location = new System.Drawing.Point(307, 125);
            this.button_start.Margin = new System.Windows.Forms.Padding(0);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(77, 30);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Start!";
            this.button_start.UseVisualStyleBackColor = true;
            // 
            // comboBox_extractlayer
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.comboBox_extractlayer, 2);
            this.comboBox_extractlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_extractlayer.FormattingEnabled = true;
            this.comboBox_extractlayer.Location = new System.Drawing.Point(115, 74);
            this.comboBox_extractlayer.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox_extractlayer.Name = "comboBox_extractlayer";
            this.comboBox_extractlayer.Size = new System.Drawing.Size(269, 21);
            this.comboBox_extractlayer.TabIndex = 4;
            // 
            // label_extractlayer
            // 
            this.label_extractlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_extractlayer.Location = new System.Drawing.Point(3, 74);
            this.label_extractlayer.Name = "label_extractlayer";
            this.label_extractlayer.Size = new System.Drawing.Size(109, 22);
            this.label_extractlayer.TabIndex = 5;
            this.label_extractlayer.Text = "Extract Layer";
            this.label_extractlayer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // progressBar1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBar1, 2);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 125);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(307, 30);
            this.progressBar1.TabIndex = 6;
            // 
            // button_modcratebuilder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_modcratebuilder, 3);
            this.button_modcratebuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_modcratebuilder.Enabled = false;
            this.button_modcratebuilder.Location = new System.Drawing.Point(0, 0);
            this.button_modcratebuilder.Margin = new System.Windows.Forms.Padding(0);
            this.button_modcratebuilder.Name = "button_modcratebuilder";
            this.button_modcratebuilder.Size = new System.Drawing.Size(384, 30);
            this.button_modcratebuilder.TabIndex = 7;
            this.button_modcratebuilder.Text = "Mod Crate Builder";
            this.button_modcratebuilder.UseVisualStyleBackColor = true;
            // 
            // label_processinfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_processinfo, 3);
            this.label_processinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_processinfo.Location = new System.Drawing.Point(3, 155);
            this.label_processinfo.Name = "label_processinfo";
            this.label_processinfo.Size = new System.Drawing.Size(378, 30);
            this.label_processinfo.TabIndex = 8;
            this.label_processinfo.Text = "Process Info";
            this.label_processinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_layerinfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label_layerinfo, 3);
            this.label_layerinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_layerinfo.Location = new System.Drawing.Point(3, 96);
            this.label_layerinfo.Name = "label_layerinfo";
            this.label_layerinfo.Size = new System.Drawing.Size(378, 29);
            this.label_layerinfo.TabIndex = 9;
            this.label_layerinfo.Text = "Layer Info";
            this.label_layerinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button_modcratebuilder, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_processinfo, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label_layerinfo, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_outputfolder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_outputfolder, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_extractlayer, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_extractlayer, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_outputfolder_browse, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_start, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 185);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // ModToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 185);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 224);
            this.Name = "ModToolsForm";
            this.Text = "Mod Tools";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}