namespace TwinsaityEditor.Workers
{
    partial class ImageMaker
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTwinsanityPath = new System.Windows.Forms.TextBox();
            this.tbImageName = new System.Windows.Forms.TextBox();
            this.tbOutputPath = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSelectTwinsPath = new System.Windows.Forms.Button();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbGenerationProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbOpenOutPath = new System.Windows.Forms.CheckBox();
            this.cbPackAndCopy = new System.Windows.Forms.CheckBox();
            this.btnPcsx2Path = new System.Windows.Forms.Button();
            this.tbPcsx2Path = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRun = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Twinsanity path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Image name:";
            // 
            // tbTwinsanityPath
            // 
            this.tbTwinsanityPath.Location = new System.Drawing.Point(103, 10);
            this.tbTwinsanityPath.Name = "tbTwinsanityPath";
            this.tbTwinsanityPath.ReadOnly = true;
            this.tbTwinsanityPath.Size = new System.Drawing.Size(339, 20);
            this.tbTwinsanityPath.TabIndex = 3;
            // 
            // tbImageName
            // 
            this.tbImageName.Location = new System.Drawing.Point(103, 36);
            this.tbImageName.MaxLength = 200;
            this.tbImageName.Name = "tbImageName";
            this.tbImageName.Size = new System.Drawing.Size(339, 20);
            this.tbImageName.TabIndex = 4;
            this.tbImageName.TextChanged += new System.EventHandler(this.tbImageName_TextChanged);
            // 
            // tbOutputPath
            // 
            this.tbOutputPath.Location = new System.Drawing.Point(103, 62);
            this.tbOutputPath.Name = "tbOutputPath";
            this.tbOutputPath.ReadOnly = true;
            this.tbOutputPath.Size = new System.Drawing.Size(339, 20);
            this.tbOutputPath.TabIndex = 5;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(13, 91);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSelectTwinsPath
            // 
            this.btnSelectTwinsPath.Location = new System.Drawing.Point(448, 8);
            this.btnSelectTwinsPath.Name = "btnSelectTwinsPath";
            this.btnSelectTwinsPath.Size = new System.Drawing.Size(28, 23);
            this.btnSelectTwinsPath.TabIndex = 7;
            this.btnSelectTwinsPath.Text = "...";
            this.btnSelectTwinsPath.UseVisualStyleBackColor = true;
            this.btnSelectTwinsPath.Click += new System.EventHandler(this.btnSelectTwinsPath_Click);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(448, 60);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(28, 23);
            this.btnOutputPath.TabIndex = 8;
            this.btnOutputPath.Text = "...";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblStatus,
            this.tspbGenerationProgress});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(103, 92);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(373, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslblStatus
            // 
            this.tsslblStatus.Name = "tsslblStatus";
            this.tsslblStatus.Size = new System.Drawing.Size(42, 17);
            this.tsslblStatus.Text = "Status:";
            // 
            // tspbGenerationProgress
            // 
            this.tspbGenerationProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspbGenerationProgress.Name = "tspbGenerationProgress";
            this.tspbGenerationProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbOpenOutPath
            // 
            this.cbOpenOutPath.AutoSize = true;
            this.cbOpenOutPath.Location = new System.Drawing.Point(13, 121);
            this.cbOpenOutPath.Name = "cbOpenOutPath";
            this.cbOpenOutPath.Size = new System.Drawing.Size(184, 17);
            this.cbOpenOutPath.TabIndex = 10;
            this.cbOpenOutPath.Text = "Open image path after generating";
            this.cbOpenOutPath.UseVisualStyleBackColor = true;
            // 
            // cbPackAndCopy
            // 
            this.cbPackAndCopy.AutoSize = true;
            this.cbPackAndCopy.Location = new System.Drawing.Point(241, 121);
            this.cbPackAndCopy.Name = "cbPackAndCopy";
            this.cbPackAndCopy.Size = new System.Drawing.Size(225, 17);
            this.cbPackAndCopy.TabIndex = 11;
            this.cbPackAndCopy.Text = "Pack and copy BD/BH to Twinsanity path";
            this.cbPackAndCopy.UseVisualStyleBackColor = true;
            // 
            // btnPcsx2Path
            // 
            this.btnPcsx2Path.Location = new System.Drawing.Point(448, 144);
            this.btnPcsx2Path.Name = "btnPcsx2Path";
            this.btnPcsx2Path.Size = new System.Drawing.Size(28, 23);
            this.btnPcsx2Path.TabIndex = 14;
            this.btnPcsx2Path.Text = "...";
            this.btnPcsx2Path.UseVisualStyleBackColor = true;
            this.btnPcsx2Path.Click += new System.EventHandler(this.btnPcsx2Path_Click);
            // 
            // tbPcsx2Path
            // 
            this.tbPcsx2Path.Location = new System.Drawing.Point(206, 146);
            this.tbPcsx2Path.Name = "tbPcsx2Path";
            this.tbPcsx2Path.ReadOnly = true;
            this.tbPcsx2Path.Size = new System.Drawing.Size(236, 20);
            this.tbPcsx2Path.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "PCSX2 Path:";
            // 
            // cbRun
            // 
            this.cbRun.AutoSize = true;
            this.cbRun.Location = new System.Drawing.Point(12, 148);
            this.cbRun.Name = "cbRun";
            this.cbRun.Size = new System.Drawing.Size(109, 17);
            this.cbRun.TabIndex = 15;
            this.cbRun.Text = "Run after finished";
            this.cbRun.UseVisualStyleBackColor = true;
            // 
            // ImageMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 177);
            this.Controls.Add(this.cbRun);
            this.Controls.Add(this.btnPcsx2Path);
            this.Controls.Add(this.tbPcsx2Path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPackAndCopy);
            this.Controls.Add(this.cbOpenOutPath);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.btnSelectTwinsPath);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbOutputPath);
            this.Controls.Add(this.tbImageName);
            this.Controls.Add(this.tbTwinsanityPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageMaker";
            this.Text = "Image maker";
            this.Load += new System.EventHandler(this.ImageMaker_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbTwinsanityPath;
        private System.Windows.Forms.TextBox tbImageName;
        private System.Windows.Forms.TextBox tbOutputPath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSelectTwinsPath;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblStatus;
        private System.Windows.Forms.ToolStripProgressBar tspbGenerationProgress;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbOpenOutPath;
        private System.Windows.Forms.CheckBox cbPackAndCopy;
        private System.Windows.Forms.Button btnPcsx2Path;
        private System.Windows.Forms.TextBox tbPcsx2Path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbRun;
    }
}