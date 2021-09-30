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
    partial class FuckingShitEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuckingShitEditor));
            this.FSTree = new System.Windows.Forms.TreeView();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.ID1Val = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.ID2Val = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.ID3Val = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.ID4Val = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.ID5Val = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FSTree
            // 
            this.FSTree.Location = new System.Drawing.Point(12, 12);
            this.FSTree.Name = "FSTree";
            this.FSTree.Size = new System.Drawing.Size(156, 223);
            this.FSTree.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 241);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Revert";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(93, 241);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "Apply";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ID1Val
            // 
            this.ID1Val.Location = new System.Drawing.Point(208, 15);
            this.ID1Val.Name = "ID1Val";
            this.ID1Val.Size = new System.Drawing.Size(87, 20);
            this.ID1Val.TabIndex = 3;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(175, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(27, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "ID1:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(175, 44);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(27, 13);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "ID2:";
            // 
            // ID2Val
            // 
            this.ID2Val.Location = new System.Drawing.Point(208, 41);
            this.ID2Val.Name = "ID2Val";
            this.ID2Val.Size = new System.Drawing.Size(87, 20);
            this.ID2Val.TabIndex = 5;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(175, 70);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(27, 13);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "ID3:";
            // 
            // ID3Val
            // 
            this.ID3Val.Location = new System.Drawing.Point(208, 67);
            this.ID3Val.Name = "ID3Val";
            this.ID3Val.Size = new System.Drawing.Size(87, 20);
            this.ID3Val.TabIndex = 7;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(175, 96);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(27, 13);
            this.Label4.TabIndex = 10;
            this.Label4.Text = "ID4:";
            // 
            // ID4Val
            // 
            this.ID4Val.Location = new System.Drawing.Point(208, 93);
            this.ID4Val.Name = "ID4Val";
            this.ID4Val.Size = new System.Drawing.Size(87, 20);
            this.ID4Val.TabIndex = 9;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(175, 122);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(27, 13);
            this.Label5.TabIndex = 12;
            this.Label5.Text = "ID5:";
            // 
            // ID5Val
            // 
            this.ID5Val.Location = new System.Drawing.Point(208, 119);
            this.ID5Val.Name = "ID5Val";
            this.ID5Val.Size = new System.Drawing.Size(87, 20);
            this.ID5Val.TabIndex = 11;
            // 
            // FuckingShitEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 273);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ID5Val);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ID4Val);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.ID3Val);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ID2Val);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ID1Val);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.FSTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FuckingShitEditor";
            this.Text = "FuckingShit Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public TreeView FSTree;
        private Button Button1;
        private Button Button2;
        private TextBox ID1Val;
        private Label Label1;
        private Label Label2;
        private TextBox ID2Val;
        private Label Label3;
        private TextBox ID3Val;
        private Label Label4;
        private TextBox ID4Val;
        private Label Label5;
        private TextBox ID5Val;
    }
}
