namespace TwinsaityEditor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonRM2Viewer = new System.Windows.Forms.Button();
            this.buttonMHTool = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonBHTool = new System.Windows.Forms.Button();
            this.buttonISOTool = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonEXETool = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1090, 483);
            this.splitContainer1.SplitterDistance = 545;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(150, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(395, 483);
            this.treeView1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.DimGray;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel2.Controls.Add(this.buttonSaveAs);
            this.splitContainer2.Panel2.Controls.Add(this.buttonOpen);
            this.splitContainer2.Panel2.Controls.Add(this.buttonRM2Viewer);
            this.splitContainer2.Panel2.Controls.Add(this.buttonMHTool);
            this.splitContainer2.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer2.Panel2.Controls.Add(this.buttonBHTool);
            this.splitContainer2.Panel2.Controls.Add(this.buttonISOTool);
            this.splitContainer2.Panel2.Controls.Add(this.buttonAbout);
            this.splitContainer2.Panel2.Controls.Add(this.buttonEXETool);
            this.splitContainer2.Size = new System.Drawing.Size(150, 483);
            this.splitContainer2.SplitterDistance = 80;
            this.splitContainer2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(82, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "v0.60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Twinsanity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(55, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Editor";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 44);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.BackColor = System.Drawing.Color.Silver;
            this.buttonSaveAs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSaveAs.FlatAppearance.BorderSize = 0;
            this.buttonSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveAs.Location = new System.Drawing.Point(3, 91);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(144, 38);
            this.buttonSaveAs.TabIndex = 8;
            this.buttonSaveAs.Text = "Save As...";
            this.buttonSaveAs.UseVisualStyleBackColor = false;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.BackColor = System.Drawing.Color.Silver;
            this.buttonOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpen.FlatAppearance.BorderSize = 0;
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(3, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(144, 38);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open File";
            this.buttonOpen.UseVisualStyleBackColor = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonRM2Viewer
            // 
            this.buttonRM2Viewer.BackColor = System.Drawing.Color.Silver;
            this.buttonRM2Viewer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRM2Viewer.FlatAppearance.BorderSize = 0;
            this.buttonRM2Viewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRM2Viewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRM2Viewer.Location = new System.Drawing.Point(3, 135);
            this.buttonRM2Viewer.Name = "buttonRM2Viewer";
            this.buttonRM2Viewer.Size = new System.Drawing.Size(144, 38);
            this.buttonRM2Viewer.TabIndex = 2;
            this.buttonRM2Viewer.Text = "Viewer";
            this.buttonRM2Viewer.UseVisualStyleBackColor = false;
            this.buttonRM2Viewer.Click += new System.EventHandler(this.buttonRM2Viewer_Click);
            // 
            // buttonMHTool
            // 
            this.buttonMHTool.BackColor = System.Drawing.Color.Silver;
            this.buttonMHTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMHTool.FlatAppearance.BorderSize = 0;
            this.buttonMHTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMHTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMHTool.Location = new System.Drawing.Point(3, 267);
            this.buttonMHTool.Name = "buttonMHTool";
            this.buttonMHTool.Size = new System.Drawing.Size(144, 38);
            this.buttonMHTool.TabIndex = 3;
            this.buttonMHTool.Text = "MH/MB Tool";
            this.buttonMHTool.UseVisualStyleBackColor = false;
            this.buttonMHTool.Click += new System.EventHandler(this.buttonMHTool_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Silver;
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(3, 47);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(144, 38);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save File";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonBHTool
            // 
            this.buttonBHTool.BackColor = System.Drawing.Color.Silver;
            this.buttonBHTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBHTool.FlatAppearance.BorderSize = 0;
            this.buttonBHTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBHTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBHTool.Location = new System.Drawing.Point(3, 179);
            this.buttonBHTool.Name = "buttonBHTool";
            this.buttonBHTool.Size = new System.Drawing.Size(144, 38);
            this.buttonBHTool.TabIndex = 4;
            this.buttonBHTool.Text = "BH/BD Tool";
            this.buttonBHTool.UseVisualStyleBackColor = false;
            this.buttonBHTool.Click += new System.EventHandler(this.buttonBHTool_Click);
            // 
            // buttonISOTool
            // 
            this.buttonISOTool.BackColor = System.Drawing.Color.Silver;
            this.buttonISOTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonISOTool.FlatAppearance.BorderSize = 0;
            this.buttonISOTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonISOTool.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonISOTool.Location = new System.Drawing.Point(3, 223);
            this.buttonISOTool.Name = "buttonISOTool";
            this.buttonISOTool.Size = new System.Drawing.Size(144, 38);
            this.buttonISOTool.TabIndex = 5;
            this.buttonISOTool.Text = "Image Maker";
            this.buttonISOTool.UseVisualStyleBackColor = false;
            this.buttonISOTool.Click += new System.EventHandler(this.buttonISOTool_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.BackColor = System.Drawing.Color.Silver;
            this.buttonAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAbout.FlatAppearance.BorderSize = 0;
            this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbout.Location = new System.Drawing.Point(3, 355);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(144, 38);
            this.buttonAbout.TabIndex = 7;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = false;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonEXETool
            // 
            this.buttonEXETool.BackColor = System.Drawing.Color.Silver;
            this.buttonEXETool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEXETool.FlatAppearance.BorderSize = 0;
            this.buttonEXETool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEXETool.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEXETool.Location = new System.Drawing.Point(3, 311);
            this.buttonEXETool.Name = "buttonEXETool";
            this.buttonEXETool.Size = new System.Drawing.Size(144, 38);
            this.buttonEXETool.TabIndex = 6;
            this.buttonEXETool.Text = "EXE Patcher";
            this.buttonEXETool.UseVisualStyleBackColor = false;
            this.buttonEXETool.Click += new System.EventHandler(this.buttonEXETool_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(541, 483);
            this.textBox1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 483);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(911, 522);
            this.Name = "MainForm";
            this.Text = "Twinsaity Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonEXETool;
        private System.Windows.Forms.Button buttonISOTool;
        private System.Windows.Forms.Button buttonBHTool;
        private System.Windows.Forms.Button buttonMHTool;
        private System.Windows.Forms.Button buttonRM2Viewer;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}