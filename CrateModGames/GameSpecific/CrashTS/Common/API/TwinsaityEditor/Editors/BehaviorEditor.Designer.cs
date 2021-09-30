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
    partial class BehaviorEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BehaviorEditor));
            this.ZVal = new System.Windows.Forms.TextBox();
            this.YVal = new System.Windows.Forms.TextBox();
            this.XVal = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Revert = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.BehTree = new System.Windows.Forms.TreeView();
            this.NumVal = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.WVal = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ZVal
            // 
            this.ZVal.Location = new System.Drawing.Point(163, 76);
            this.ZVal.Name = "ZVal";
            this.ZVal.Size = new System.Drawing.Size(100, 20);
            this.ZVal.TabIndex = 17;
            // 
            // YVal
            // 
            this.YVal.Location = new System.Drawing.Point(163, 50);
            this.YVal.Name = "YVal";
            this.YVal.Size = new System.Drawing.Size(100, 20);
            this.YVal.TabIndex = 16;
            // 
            // XVal
            // 
            this.XVal.Location = new System.Drawing.Point(163, 24);
            this.XVal.Name = "XVal";
            this.XVal.Size = new System.Drawing.Size(100, 20);
            this.XVal.TabIndex = 15;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(139, 79);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(17, 13);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "Z:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(139, 53);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(17, 13);
            this.Label2.TabIndex = 13;
            this.Label2.Text = "Y:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(140, 27);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(17, 13);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "X:";
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(12, 194);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(121, 23);
            this.Revert.TabIndex = 11;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(12, 165);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(121, 23);
            this.Apply.TabIndex = 10;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // BehTree
            // 
            this.BehTree.Location = new System.Drawing.Point(12, 12);
            this.BehTree.Name = "BehTree";
            this.BehTree.Size = new System.Drawing.Size(121, 147);
            this.BehTree.TabIndex = 9;
            // 
            // NumVal
            // 
            this.NumVal.Location = new System.Drawing.Point(177, 125);
            this.NumVal.Name = "NumVal";
            this.NumVal.Size = new System.Drawing.Size(86, 20);
            this.NumVal.TabIndex = 19;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(139, 128);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(32, 13);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Num:";
            // 
            // WVal
            // 
            this.WVal.Location = new System.Drawing.Point(163, 99);
            this.WVal.Name = "WVal";
            this.WVal.Size = new System.Drawing.Size(100, 20);
            this.WVal.TabIndex = 21;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(139, 102);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(21, 13);
            this.Label5.TabIndex = 20;
            this.Label5.Text = "W:";
            // 
            // BehaviorEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
            this.Controls.Add(this.WVal);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.NumVal);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ZVal);
            this.Controls.Add(this.YVal);
            this.Controls.Add(this.XVal);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.BehTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BehaviorEditor";
            this.Text = "Behavior Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox ZVal;
        private TextBox YVal;
        private TextBox XVal;
        private Label Label3;
        private Label Label2;
        private Label Label1;
        private Button Revert;
        private Button Apply;
        public TreeView BehTree;
        private TextBox NumVal;
        private Label Label4;
        private TextBox WVal;
        private Label Label5;
    }
}
