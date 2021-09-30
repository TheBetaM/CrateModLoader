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
    partial class PSMWorker : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSMWorker));
            this.GlControl1 = new OpenTK.GLControl();
            this.Button1 = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button5 = new System.Windows.Forms.Button();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GlControl1
            // 
            this.GlControl1.BackColor = System.Drawing.Color.Black;
            this.GlControl1.Location = new System.Drawing.Point(13, 13);
            this.GlControl1.Name = "GlControl1";
            this.GlControl1.Size = new System.Drawing.Size(512, 512);
            this.GlControl1.TabIndex = 0;
            this.GlControl1.VSync = false;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(6, 19);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Load";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Button7);
            this.GroupBox1.Controls.Add(this.Button6);
            this.GroupBox1.Controls.Add(this.ComboBox1);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Button5);
            this.GroupBox1.Controls.Add(this.CheckBox2);
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Controls.Add(this.Button4);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.Button3);
            this.GroupBox1.Controls.Add(this.Button2);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Location = new System.Drawing.Point(531, 13);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(147, 513);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Actions";
            this.GroupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(18, 253);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(111, 13);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "Please, use 8-bit PNG";
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(6, 217);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(75, 23);
            this.Button7.TabIndex = 12;
            this.Button7.Text = "Save Native";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(7, 188);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(134, 23);
            this.Button6.TabIndex = 11;
            this.Button6.Text = "Replace 512x512";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // ComboBox1
            // 
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(50, 161);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(91, 21);
            this.ComboBox1.TabIndex = 10;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 164);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(36, 13);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Index:";
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(7, 131);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(134, 23);
            this.Button5.TabIndex = 8;
            this.Button5.Text = "Replace Segment";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // CheckBox2
            // 
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Location = new System.Drawing.Point(7, 107);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(44, 17);
            this.CheckBox2.TabIndex = 7;
            this.CheckBox2.Text = "BW";
            this.CheckBox2.UseVisualStyleBackColor = true;
            this.CheckBox2.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(87, 19);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(54, 17);
            this.CheckBox1.TabIndex = 6;
            this.CheckBox1.Text = "Demo";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(7, 78);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(75, 23);
            this.Button4.TabIndex = 5;
            this.Button4.Text = "Save";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(62, 54);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(25, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "0\\0";
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(104, 49);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(37, 23);
            this.Button3.TabIndex = 3;
            this.Button3.Text = ">>";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(7, 49);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(37, 23);
            this.Button2.TabIndex = 2;
            this.Button2.Text = "<<";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "PSM Image (*.psm)|*.psm|PTC Image (*.ptc)|*.ptc|PSF Font (*.psf)|*.psf";
            this.OpenFileDialog1.Multiselect = true;
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.Filter = "PNG Image (*.png)|*.png";
            // 
            // OpenFileDialog2
            // 
            this.OpenFileDialog2.Filter = "PNG Image | *.png";
            // 
            // PSMWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 538);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.GlControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PSMWorker";
            this.Text = "PSMWorker";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        
        private OpenTK.GLControl GlControl1;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.CheckBox CheckBox1;
        private System.Windows.Forms.CheckBox CheckBox2;
        private ComboBox ComboBox1;
        private Label Label2;
        private Button Button5;
        private OpenFileDialog OpenFileDialog2;
        private Button Button6;
        private Button Button7;
        private SaveFileDialog SaveFileDialog2;
        private Label Label3;
    }
}
