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
    [global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    partial class SceneryEditor : System.Windows.Forms.Form
    {

        // Form overrides dispose to clean up the component list.
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

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SceneryEditor));
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button4 = new System.Windows.Forms.Button();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(12, 297);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(121, 23);
            this.Button2.TabIndex = 4;
            this.Button2.Text = "Revert";
            this.Button2.UseVisualStyleBackColor = true;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 268);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(121, 23);
            this.Button1.TabIndex = 3;
            this.Button1.Text = "Apply";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(12, 326);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(120, 23);
            this.Button3.TabIndex = 5;
            this.Button3.Text = "View";
            this.Button3.UseVisualStyleBackColor = true;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(56, 10);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(100, 20);
            this.TextBox1.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(38, 13);
            this.Label1.TabIndex = 7;
            this.Label1.Text = "Name:";
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(162, 8);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(48, 23);
            this.Button4.TabIndex = 8;
            this.Button4.Text = "<<";
            this.Button4.UseVisualStyleBackColor = true;
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(217, 10);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(67, 20);
            this.TextBox2.TabIndex = 9;
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(290, 8);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(48, 23);
            this.Button5.TabIndex = 10;
            this.Button5.Text = ">>";
            this.Button5.UseVisualStyleBackColor = true;
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(35, 40);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(175, 20);
            this.TextBox3.TabIndex = 11;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(17, 13);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "A:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 69);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(17, 13);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "B:";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(35, 66);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(175, 20);
            this.TextBox4.TabIndex = 14;
            // 
            // SceneryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 360);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SceneryEditor";
            this.Text = "Scenery Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button Button2;
        private Button Button1;
        private Button Button3;
        private TextBox TextBox1;
        private Label Label1;
        private Button Button4;
        private TextBox TextBox2;
        private Button Button5;
        private TextBox TextBox3;
        private Label Label2;
        private Label Label3;
        private TextBox TextBox4;
    }
}
