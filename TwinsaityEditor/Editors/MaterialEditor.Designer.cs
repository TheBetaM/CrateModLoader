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
    partial class MaterialEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialEditor));
            this.Revert = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.MtlTree = new System.Windows.Forms.TreeView();
            this.TextureID = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.MtlID = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.MtlName = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(12, 258);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(121, 23);
            this.Revert.TabIndex = 5;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(12, 229);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(121, 23);
            this.Apply.TabIndex = 4;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // MtlTree
            // 
            this.MtlTree.Location = new System.Drawing.Point(12, 12);
            this.MtlTree.Name = "MtlTree";
            this.MtlTree.Size = new System.Drawing.Size(121, 211);
            this.MtlTree.TabIndex = 3;
            // 
            // TextureID
            // 
            this.TextureID.Location = new System.Drawing.Point(139, 75);
            this.TextureID.Name = "TextureID";
            this.TextureID.Size = new System.Drawing.Size(100, 20);
            this.TextureID.TabIndex = 7;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(136, 59);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(59, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "TexturelID:";
            // 
            // MtlID
            // 
            this.MtlID.Location = new System.Drawing.Point(139, 36);
            this.MtlID.Name = "MtlID";
            this.MtlID.Size = new System.Drawing.Size(100, 20);
            this.MtlID.TabIndex = 9;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(136, 20);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(58, 13);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "MaterialID:";
            // 
            // MtlName
            // 
            this.MtlName.Location = new System.Drawing.Point(139, 118);
            this.MtlName.Name = "MtlName";
            this.MtlName.Size = new System.Drawing.Size(100, 20);
            this.MtlName.TabIndex = 11;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(136, 102);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(38, 13);
            this.Label3.TabIndex = 10;
            this.Label3.Text = "Name:";
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 297);
            this.Controls.Add(this.MtlName);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.MtlID);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.TextureID);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.MtlTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialEditor";
            this.Text = "Material Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button Revert;
        private Button Apply;
        public TreeView MtlTree;
        private TextBox TextureID;
        private Label Label1;
        private TextBox MtlID;
        private Label Label2;
        private TextBox MtlName;
        private Label Label3;
    }
}
