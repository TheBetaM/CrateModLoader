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
    partial class TerrainEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerrainEditor));
            this.TerTree = new System.Windows.Forms.TreeView();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.NumVal = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.K1Val = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.ID1Val = new System.Windows.Forms.TextBox();
            this.ID2Val = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.K2Val = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.ID3Val = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.K3Val = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.ID4Val = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.K4Val = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.IDVal = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TerTree
            // 
            this.TerTree.Location = new System.Drawing.Point(13, 13);
            this.TerTree.Name = "TerTree";
            this.TerTree.Size = new System.Drawing.Size(121, 161);
            this.TerTree.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(13, 181);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(121, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Apply";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(13, 210);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(121, 23);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "Revert";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // NumVal
            // 
            this.NumVal.Location = new System.Drawing.Point(308, 19);
            this.NumVal.Name = "NumVal";
            this.NumVal.Size = new System.Drawing.Size(100, 20);
            this.NumVal.TabIndex = 3;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(270, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(32, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Num:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(144, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(17, 13);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "K:";
            // 
            // K1Val
            // 
            this.K1Val.Location = new System.Drawing.Point(167, 45);
            this.K1Val.Name = "K1Val";
            this.K1Val.Size = new System.Drawing.Size(100, 20);
            this.K1Val.TabIndex = 6;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(274, 48);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(21, 13);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "ID:";
            // 
            // ID1Val
            // 
            this.ID1Val.Location = new System.Drawing.Point(301, 45);
            this.ID1Val.Name = "ID1Val";
            this.ID1Val.Size = new System.Drawing.Size(107, 20);
            this.ID1Val.TabIndex = 8;
            // 
            // ID2Val
            // 
            this.ID2Val.Location = new System.Drawing.Point(301, 71);
            this.ID2Val.Name = "ID2Val";
            this.ID2Val.Size = new System.Drawing.Size(107, 20);
            this.ID2Val.TabIndex = 12;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(274, 74);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(21, 13);
            this.Label4.TabIndex = 11;
            this.Label4.Text = "ID:";
            // 
            // K2Val
            // 
            this.K2Val.Location = new System.Drawing.Point(167, 71);
            this.K2Val.Name = "K2Val";
            this.K2Val.Size = new System.Drawing.Size(100, 20);
            this.K2Val.TabIndex = 10;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(144, 74);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(17, 13);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "K:";
            // 
            // ID3Val
            // 
            this.ID3Val.Location = new System.Drawing.Point(301, 97);
            this.ID3Val.Name = "ID3Val";
            this.ID3Val.Size = new System.Drawing.Size(107, 20);
            this.ID3Val.TabIndex = 16;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(274, 100);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(21, 13);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "ID:";
            // 
            // K3Val
            // 
            this.K3Val.Location = new System.Drawing.Point(167, 97);
            this.K3Val.Name = "K3Val";
            this.K3Val.Size = new System.Drawing.Size(100, 20);
            this.K3Val.TabIndex = 14;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(144, 100);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(17, 13);
            this.Label7.TabIndex = 13;
            this.Label7.Text = "K:";
            // 
            // ID4Val
            // 
            this.ID4Val.Location = new System.Drawing.Point(301, 123);
            this.ID4Val.Name = "ID4Val";
            this.ID4Val.Size = new System.Drawing.Size(107, 20);
            this.ID4Val.TabIndex = 20;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(274, 126);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(21, 13);
            this.Label8.TabIndex = 19;
            this.Label8.Text = "ID:";
            // 
            // K4Val
            // 
            this.K4Val.Location = new System.Drawing.Point(167, 123);
            this.K4Val.Name = "K4Val";
            this.K4Val.Size = new System.Drawing.Size(100, 20);
            this.K4Val.TabIndex = 18;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(144, 126);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(17, 13);
            this.Label9.TabIndex = 17;
            this.Label9.Text = "K:";
            // 
            // IDVal
            // 
            this.IDVal.Location = new System.Drawing.Point(167, 19);
            this.IDVal.Name = "IDVal";
            this.IDVal.Size = new System.Drawing.Size(100, 20);
            this.IDVal.TabIndex = 21;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(140, 22);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(21, 13);
            this.Label10.TabIndex = 22;
            this.Label10.Text = "ID:";
            // 
            // TerrainEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 243);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.IDVal);
            this.Controls.Add(this.ID4Val);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.K4Val);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.ID3Val);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.K3Val);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.ID2Val);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.K2Val);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ID1Val);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.K1Val);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.NumVal);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TerTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TerrainEditor";
            this.Text = "Terrain Editor";
            this.Load += new System.EventHandler(this.TerrainEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public TreeView TerTree;
        private Button Button1;
        private Button Button2;
        private TextBox NumVal;
        private Label Label1;
        private Label Label2;
        private TextBox K1Val;
        private Label Label3;
        private TextBox ID1Val;
        private TextBox ID2Val;
        private Label Label4;
        private TextBox K2Val;
        private Label Label5;
        private TextBox ID3Val;
        private Label Label6;
        private TextBox K3Val;
        private Label Label7;
        private TextBox ID4Val;
        private Label Label8;
        private TextBox K4Val;
        private Label Label9;
        private TextBox IDVal;
        private Label Label10;
    }
}
