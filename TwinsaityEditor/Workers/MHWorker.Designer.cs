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
    partial class MHWorker : System.Windows.Forms.Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MHWorker));
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.Label10 = new System.Windows.Forms.Label();
            this.Button9 = new System.Windows.Forms.Button();
            this.UpdateBar = new System.Windows.Forms.Timer(this.components);
            this.SaveWAV = new System.Windows.Forms.SaveFileDialog();
            this.OpenWAV = new System.Windows.Forms.OpenFileDialog();
            this.SaveMBH = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeView1
            // 
            this.TreeView1.Location = new System.Drawing.Point(13, 26);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(187, 224);
            this.TreeView1.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(240, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(37, 13);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Type: ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(244, 26);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(33, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Size: ";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(236, 39);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 13);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Offset: ";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(203, 52);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(74, 13);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "Sample Rate: ";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(243, 65);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(34, 13);
            this.Label5.TabIndex = 5;
            this.Label5.Text = "Skip: ";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(12, 9);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(49, 13);
            this.Label6.TabIndex = 6;
            this.Label6.Text = "Sounds: ";
            // 
            // TrackBar1
            // 
            this.TrackBar1.Location = new System.Drawing.Point(207, 95);
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(238, 45);
            this.TrackBar1.TabIndex = 7;
            this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(207, 132);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(28, 13);
            this.Label7.TabIndex = 8;
            this.Label7.Text = "0:00";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(417, 132);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(28, 13);
            this.Label8.TabIndex = 9;
            this.Label8.Text = "0:00";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(313, 132);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(28, 13);
            this.Label9.TabIndex = 10;
            this.Label9.Text = "0:00";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(270, 157);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(54, 23);
            this.Button1.TabIndex = 11;
            this.Button1.Text = "Play";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(330, 157);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(54, 23);
            this.Button2.TabIndex = 12;
            this.Button2.Text = "Pause";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(390, 157);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(54, 23);
            this.Button3.TabIndex = 13;
            this.Button3.Text = "Stop";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(210, 157);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(54, 23);
            this.Button4.TabIndex = 14;
            this.Button4.Text = "Load";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(210, 186);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(54, 23);
            this.Button5.TabIndex = 15;
            this.Button5.Text = "Add";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(269, 186);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(55, 23);
            this.Button6.TabIndex = 16;
            this.Button6.Text = "Replace";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(330, 186);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(53, 23);
            this.Button7.TabIndex = 17;
            this.Button7.Text = "Delete";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Location = new System.Drawing.Point(210, 215);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(114, 23);
            this.Button8.TabIndex = 18;
            this.Button8.Text = "Repack MB Archive";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(231, 79);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(46, 13);
            this.Label10.TabIndex = 19;
            this.Label10.Text = "Loaded:";
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(391, 186);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(54, 23);
            this.Button9.TabIndex = 20;
            this.Button9.Text = "Convert";
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // UpdateBar
            // 
            this.UpdateBar.Enabled = true;
            this.UpdateBar.Interval = 1;
            this.UpdateBar.Tick += new System.EventHandler(this.UpdateBar_Tick);
            // 
            // SaveWAV
            // 
            this.SaveWAV.Filter = "WAVE (*.wav)|*.wav";
            // 
            // OpenWAV
            // 
            this.OpenWAV.Filter = "WAVE (*.wav)|*.wav";
            // 
            // MHWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 262);
            this.Controls.Add(this.Button9);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TreeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MHWorker";
            this.Text = "MHWorker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MHWorker_FormClosed_1);
            this.Load += new System.EventHandler(this.MHWorker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.Button Button5;
        private System.Windows.Forms.Button Button6;
        private System.Windows.Forms.Button Button7;
        private System.Windows.Forms.Button Button8;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Button Button9;
        private System.Windows.Forms.Timer UpdateBar;
        private System.Windows.Forms.SaveFileDialog SaveWAV;
        private System.Windows.Forms.OpenFileDialog OpenWAV;
        private System.Windows.Forms.SaveFileDialog SaveMBH;
    }
}
