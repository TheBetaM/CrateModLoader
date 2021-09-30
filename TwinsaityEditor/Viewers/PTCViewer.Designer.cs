using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections;
using System;

namespace TwinsaityEditor
{
    partial class PTCViewer : System.Windows.Forms.Form
    {

        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                    components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private System.ComponentModel.IContainer components;


        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PTCViewer));
            this.GlControl1 = new OpenTK.GLControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.SavePNG = new System.Windows.Forms.SaveFileDialog();
            this.btnPrevTexture = new System.Windows.Forms.Button();
            this.btnNextTexture = new System.Windows.Forms.Button();
            this.lblTextureIndex = new System.Windows.Forms.Label();
            this.cbViewFullPSM = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // GlControl1
            // 
            this.GlControl1.BackColor = System.Drawing.Color.Black;
            this.GlControl1.Location = new System.Drawing.Point(13, 13);
            this.GlControl1.Name = "GlControl1";
            this.GlControl1.Size = new System.Drawing.Size(636, 640);
            this.GlControl1.TabIndex = 0;
            this.GlControl1.VSync = false;
            this.GlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl1_Paint);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(655, 83);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SavePNG
            // 
            this.SavePNG.Filter = "PNG Image (*.png)|*.png";
            // 
            // btnPrevTexture
            // 
            this.btnPrevTexture.Location = new System.Drawing.Point(655, 12);
            this.btnPrevTexture.Name = "btnPrevTexture";
            this.btnPrevTexture.Size = new System.Drawing.Size(79, 23);
            this.btnPrevTexture.TabIndex = 2;
            this.btnPrevTexture.Text = "Prev";
            this.btnPrevTexture.UseVisualStyleBackColor = true;
            this.btnPrevTexture.Click += new System.EventHandler(this.btnPrevTexture_Click);
            // 
            // btnNextTexture
            // 
            this.btnNextTexture.Location = new System.Drawing.Point(655, 54);
            this.btnNextTexture.Name = "btnNextTexture";
            this.btnNextTexture.Size = new System.Drawing.Size(79, 23);
            this.btnNextTexture.TabIndex = 3;
            this.btnNextTexture.Text = "Next";
            this.btnNextTexture.UseVisualStyleBackColor = true;
            this.btnNextTexture.Click += new System.EventHandler(this.btnNextTexture_Click);
            // 
            // lblTextureIndex
            // 
            this.lblTextureIndex.AutoSize = true;
            this.lblTextureIndex.Location = new System.Drawing.Point(683, 38);
            this.lblTextureIndex.Name = "lblTextureIndex";
            this.lblTextureIndex.Size = new System.Drawing.Size(24, 13);
            this.lblTextureIndex.TabIndex = 4;
            this.lblTextureIndex.Text = "0/0";
            this.lblTextureIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbViewFullPSM
            // 
            this.cbViewFullPSM.AutoSize = true;
            this.cbViewFullPSM.Enabled = false;
            this.cbViewFullPSM.Location = new System.Drawing.Point(655, 113);
            this.cbViewFullPSM.Name = "cbViewFullPSM";
            this.cbViewFullPSM.Size = new System.Drawing.Size(91, 17);
            this.cbViewFullPSM.TabIndex = 5;
            this.cbViewFullPSM.Text = "View full PSM";
            this.cbViewFullPSM.UseVisualStyleBackColor = true;
            this.cbViewFullPSM.CheckedChanged += new System.EventHandler(this.cbViewFullPSM_CheckedChanged);
            // 
            // PTCViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 665);
            this.Controls.Add(this.cbViewFullPSM);
            this.Controls.Add(this.lblTextureIndex);
            this.Controls.Add(this.btnNextTexture);
            this.Controls.Add(this.btnPrevTexture);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.GlControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PTCViewer";
            this.Text = "PTC Viewer";
            this.Load += new System.EventHandler(this.TextureViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Panel Panel1;
        private OpenTK.GLControl GlControl1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog SavePNG;
        private Button btnPrevTexture;
        private Button btnNextTexture;
        private Label lblTextureIndex;
        private CheckBox cbViewFullPSM;
    }
}
