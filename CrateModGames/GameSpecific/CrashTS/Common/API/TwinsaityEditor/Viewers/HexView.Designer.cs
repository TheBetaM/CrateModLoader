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
    partial class HexView : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexView));
            this.HEX = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // HEX
            // 
            this.HEX.BackColor = System.Drawing.Color.White;
            this.HEX.Font = new System.Drawing.Font("Lucida Console", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(204));
            this.HEX.Location = new System.Drawing.Point(13, 13);
            this.HEX.Multiline = true;
            this.HEX.Name = "HEX";
            this.HEX.ReadOnly = true;
            this.HEX.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HEX.Size = new System.Drawing.Size(652, 363);
            this.HEX.TabIndex = 0;
            // 
            // HexView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 388);
            this.Controls.Add(this.HEX);
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.Name = "HexView";
            this.Text = "HexView";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox HEX;
    }
}
