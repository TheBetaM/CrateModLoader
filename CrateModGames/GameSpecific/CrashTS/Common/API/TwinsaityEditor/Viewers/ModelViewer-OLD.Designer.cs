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
    partial class ModelViewer : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelViewer));
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.View = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(13, 361);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(44, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "<<";
            this.Button1.UseVisualStyleBackColor = true;
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(112, 361);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(44, 23);
            this.Button2.TabIndex = 0;
            this.Button2.Text = ">>";
            this.Button2.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(63, 366);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(25, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "0\\0";
            // 
            // View
            // 
            this.View.BackColor = System.Drawing.Color.Black;
            this.View.Location = new System.Drawing.Point(13, 13);
            this.View.Name = "View";
            this.View.Size = new System.Drawing.Size(524, 336);
            this.View.TabIndex = 3;
            this.View.VSync = false;
            this.View.Paint += new System.Windows.Forms.PaintEventHandler(this.GeoDataVis_Paint);
            // 
            // ModelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 361);
            this.Controls.Add(this.View);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModelViewer";
            this.Text = "ModelViewer";
            this.Load += new System.EventHandler(this.ModelViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Label Label1;
        private OpenTK.GLControl View;
    }
}
