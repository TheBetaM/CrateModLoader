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
    partial class GCEditor : System.Windows.Forms.Form
    {

        // Форма переопределяет dispose для очистки списка компонентов.
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

        // Является обязательной для конструктора форм Windows Forms
        private System.ComponentModel.IContainer components;

        // Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
        // Для ее изменения используйте конструктор форм Windows Form.  
        // Не изменяйте ее в редакторе исходного кода.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCEditor));
            this.GCTree = new System.Windows.Forms.TreeView();
            this.Apply = new System.Windows.Forms.Button();
            this.Revert = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Model = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Materials = new System.Windows.Forms.ListBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.MaterialVal = new System.Windows.Forms.TextBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.GCID = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GCTree
            // 
            this.GCTree.Location = new System.Drawing.Point(13, 13);
            this.GCTree.Name = "GCTree";
            this.GCTree.Size = new System.Drawing.Size(121, 244);
            this.GCTree.TabIndex = 0;
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(16, 263);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(121, 23);
            this.Apply.TabIndex = 1;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(16, 292);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(121, 23);
            this.Revert.TabIndex = 2;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(137, 46);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(50, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "ModelID:";
            // 
            // Model
            // 
            this.Model.Location = new System.Drawing.Point(140, 62);
            this.Model.Name = "Model";
            this.Model.Size = new System.Drawing.Size(100, 20);
            this.Model.TabIndex = 4;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(140, 89);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(44, 13);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Material";
            // 
            // Materials
            // 
            this.Materials.FormattingEnabled = true;
            this.Materials.Location = new System.Drawing.Point(140, 105);
            this.Materials.Name = "Materials";
            this.Materials.Size = new System.Drawing.Size(100, 95);
            this.Materials.TabIndex = 6;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(143, 206);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(41, 23);
            this.Button1.TabIndex = 7;
            this.Button1.Text = "Add";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(195, 206);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(41, 23);
            this.Button2.TabIndex = 8;
            this.Button2.Text = "Edit";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(143, 263);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(97, 23);
            this.Button3.TabIndex = 9;
            this.Button3.Text = "Delete";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // MaterialVal
            // 
            this.MaterialVal.Location = new System.Drawing.Point(140, 237);
            this.MaterialVal.Name = "MaterialVal";
            this.MaterialVal.Size = new System.Drawing.Size(100, 20);
            this.MaterialVal.TabIndex = 10;
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(246, 59);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(49, 24);
            this.Button4.TabIndex = 11;
            this.Button4.Text = "View";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(246, 205);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(49, 23);
            this.Button5.TabIndex = 12;
            this.Button5.Text = "View";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(143, 292);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(97, 23);
            this.Button6.TabIndex = 13;
            this.Button6.Text = "View";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // GCID
            // 
            this.GCID.Location = new System.Drawing.Point(140, 23);
            this.GCID.Name = "GCID";
            this.GCID.Size = new System.Drawing.Size(100, 20);
            this.GCID.TabIndex = 15;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(137, 7);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(36, 13);
            this.Label3.TabIndex = 14;
            this.Label3.Text = "GCID:";
            // 
            // GCEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 329);
            this.Controls.Add(this.GCID);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.MaterialVal);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Materials);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Model);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.GCTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GCEditor";
            this.Text = "GC Editor";
            this.Load += new System.EventHandler(this.GCEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public System.Windows.Forms.TreeView GCTree;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Revert;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox Model;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.ListBox Materials;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.TextBox MaterialVal;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.Button Button5;
        private System.Windows.Forms.Button Button6;
        private TextBox GCID;
        private Label Label3;
    }
}
