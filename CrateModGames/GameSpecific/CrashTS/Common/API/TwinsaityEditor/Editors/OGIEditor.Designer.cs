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
    partial class OGIEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OGIEditor));
            this.OGITree = new System.Windows.Forms.TreeView();
            this.Label1 = new System.Windows.Forms.Label();
            this.FlagsVal = new System.Windows.Forms.TextBox();
            this.Unk1Val = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Unk2Val = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Vect1XVal = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Vect1YVal = new System.Windows.Forms.TextBox();
            this.Vect1ZVal = new System.Windows.Forms.TextBox();
            this.Vect2ZVal = new System.Windows.Forms.TextBox();
            this.Vect2YVal = new System.Windows.Forms.TextBox();
            this.Vect2XVal = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Vect1WVal = new System.Windows.Forms.TextBox();
            this.Vect2WVal = new System.Windows.Forms.TextBox();
            this.T1Edit = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.PrevT1 = new System.Windows.Forms.Button();
            this.NextT1 = new System.Windows.Forms.Button();
            this.T1IndVal = new System.Windows.Forms.TextBox();
            this.ApplyT2Rec = new System.Windows.Forms.Button();
            this.InsertT2Rec = new System.Windows.Forms.Button();
            this.DelT2Rec = new System.Windows.Forms.Button();
            this.T2IndVal = new System.Windows.Forms.TextBox();
            this.NextT2 = new System.Windows.Forms.Button();
            this.PrevT2 = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.T2Edit = new System.Windows.Forms.TextBox();
            this.ApplyT3Rec = new System.Windows.Forms.Button();
            this.InsertT3Rec = new System.Windows.Forms.Button();
            this.DelT3Rec = new System.Windows.Forms.Button();
            this.T3Edit = new System.Windows.Forms.TextBox();
            this.GCList = new System.Windows.Forms.ListBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.AddGC = new System.Windows.Forms.Button();
            this.DeleteGC = new System.Windows.Forms.Button();
            this.EditGC = new System.Windows.Forms.Button();
            this.EditGCVal = new System.Windows.Forms.TextBox();
            this.IDVal = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.ID4Val = new System.Windows.Forms.TextBox();
            this.ID5Val = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.ArmatureSave = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // OGITree
            // 
            this.OGITree.Location = new System.Drawing.Point(12, 12);
            this.OGITree.Name = "OGITree";
            this.OGITree.Size = new System.Drawing.Size(127, 262);
            this.OGITree.TabIndex = 47;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(146, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(35, 13);
            this.Label1.TabIndex = 50;
            this.Label1.Text = "Flags:";
            // 
            // FlagsVal
            // 
            this.FlagsVal.Location = new System.Drawing.Point(187, 10);
            this.FlagsVal.Name = "FlagsVal";
            this.FlagsVal.Size = new System.Drawing.Size(100, 20);
            this.FlagsVal.TabIndex = 51;
            this.FlagsVal.TextChanged += new System.EventHandler(this.FlagsVal_TextChanged);
            // 
            // Unk1Val
            // 
            this.Unk1Val.Location = new System.Drawing.Point(187, 36);
            this.Unk1Val.Name = "Unk1Val";
            this.Unk1Val.Size = new System.Drawing.Size(100, 20);
            this.Unk1Val.TabIndex = 53;
            this.Unk1Val.TextChanged += new System.EventHandler(this.Unk1Val_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(146, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(36, 13);
            this.Label2.TabIndex = 52;
            this.Label2.Text = "Unk1:";
            // 
            // Unk2Val
            // 
            this.Unk2Val.Location = new System.Drawing.Point(187, 62);
            this.Unk2Val.Name = "Unk2Val";
            this.Unk2Val.Size = new System.Drawing.Size(100, 20);
            this.Unk2Val.TabIndex = 55;
            this.Unk2Val.TextChanged += new System.EventHandler(this.Unk2Val_TextChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(146, 65);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(36, 13);
            this.Label3.TabIndex = 54;
            this.Label3.Text = "Unk2:";
            // 
            // Vect1XVal
            // 
            this.Vect1XVal.Location = new System.Drawing.Point(334, 10);
            this.Vect1XVal.Name = "Vect1XVal";
            this.Vect1XVal.Size = new System.Drawing.Size(65, 20);
            this.Vect1XVal.TabIndex = 57;
            this.Vect1XVal.TextChanged += new System.EventHandler(this.Vect1XVal_TextChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(293, 13);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(38, 13);
            this.Label4.TabIndex = 56;
            this.Label4.Text = "Vect1:";
            // 
            // Vect1YVal
            // 
            this.Vect1YVal.Location = new System.Drawing.Point(405, 10);
            this.Vect1YVal.Name = "Vect1YVal";
            this.Vect1YVal.Size = new System.Drawing.Size(65, 20);
            this.Vect1YVal.TabIndex = 58;
            this.Vect1YVal.TextChanged += new System.EventHandler(this.Vect1YVal_TextChanged);
            // 
            // Vect1ZVal
            // 
            this.Vect1ZVal.Location = new System.Drawing.Point(476, 10);
            this.Vect1ZVal.Name = "Vect1ZVal";
            this.Vect1ZVal.Size = new System.Drawing.Size(65, 20);
            this.Vect1ZVal.TabIndex = 59;
            this.Vect1ZVal.TextChanged += new System.EventHandler(this.Vect1ZVal_TextChanged);
            // 
            // Vect2ZVal
            // 
            this.Vect2ZVal.Location = new System.Drawing.Point(476, 39);
            this.Vect2ZVal.Name = "Vect2ZVal";
            this.Vect2ZVal.Size = new System.Drawing.Size(65, 20);
            this.Vect2ZVal.TabIndex = 63;
            this.Vect2ZVal.TextChanged += new System.EventHandler(this.Vect2ZVal_TextChanged);
            // 
            // Vect2YVal
            // 
            this.Vect2YVal.Location = new System.Drawing.Point(405, 39);
            this.Vect2YVal.Name = "Vect2YVal";
            this.Vect2YVal.Size = new System.Drawing.Size(65, 20);
            this.Vect2YVal.TabIndex = 62;
            this.Vect2YVal.TextChanged += new System.EventHandler(this.Vect2YVal_TextChanged);
            // 
            // Vect2XVal
            // 
            this.Vect2XVal.Location = new System.Drawing.Point(334, 39);
            this.Vect2XVal.Name = "Vect2XVal";
            this.Vect2XVal.Size = new System.Drawing.Size(65, 20);
            this.Vect2XVal.TabIndex = 61;
            this.Vect2XVal.TextChanged += new System.EventHandler(this.Vect2XVal_TextChanged);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(293, 42);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(38, 13);
            this.Label5.TabIndex = 60;
            this.Label5.Text = "Vect2:";
            // 
            // Vect1WVal
            // 
            this.Vect1WVal.Location = new System.Drawing.Point(547, 10);
            this.Vect1WVal.Name = "Vect1WVal";
            this.Vect1WVal.Size = new System.Drawing.Size(65, 20);
            this.Vect1WVal.TabIndex = 64;
            this.Vect1WVal.TextChanged += new System.EventHandler(this.Vect1WVal_TextChanged);
            // 
            // Vect2WVal
            // 
            this.Vect2WVal.Location = new System.Drawing.Point(547, 39);
            this.Vect2WVal.Name = "Vect2WVal";
            this.Vect2WVal.Size = new System.Drawing.Size(65, 20);
            this.Vect2WVal.TabIndex = 65;
            this.Vect2WVal.TextChanged += new System.EventHandler(this.Vect2WVal_TextChanged);
            // 
            // T1Edit
            // 
            this.T1Edit.Location = new System.Drawing.Point(149, 124);
            this.T1Edit.Multiline = true;
            this.T1Edit.Name = "T1Edit";
            this.T1Edit.Size = new System.Drawing.Size(233, 102);
            this.T1Edit.TabIndex = 66;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(149, 105);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(23, 13);
            this.Label6.TabIndex = 67;
            this.Label6.Text = "T1:";
            // 
            // PrevT1
            // 
            this.PrevT1.Location = new System.Drawing.Point(178, 100);
            this.PrevT1.Name = "PrevT1";
            this.PrevT1.Size = new System.Drawing.Size(27, 23);
            this.PrevT1.TabIndex = 68;
            this.PrevT1.Text = "<<";
            this.PrevT1.UseVisualStyleBackColor = true;
            this.PrevT1.Click += new System.EventHandler(this.PrevT1_Click);
            // 
            // NextT1
            // 
            this.NextT1.Location = new System.Drawing.Point(256, 100);
            this.NextT1.Name = "NextT1";
            this.NextT1.Size = new System.Drawing.Size(31, 23);
            this.NextT1.TabIndex = 69;
            this.NextT1.Text = ">>";
            this.NextT1.UseVisualStyleBackColor = true;
            this.NextT1.Click += new System.EventHandler(this.NextT1_Click);
            // 
            // T1IndVal
            // 
            this.T1IndVal.Location = new System.Drawing.Point(211, 102);
            this.T1IndVal.Name = "T1IndVal";
            this.T1IndVal.Size = new System.Drawing.Size(39, 20);
            this.T1IndVal.TabIndex = 70;
            this.T1IndVal.TextChanged += new System.EventHandler(this.T1IndVal_TextChanged);
            // 
            // ApplyT2Rec
            // 
            this.ApplyT2Rec.Location = new System.Drawing.Point(563, 232);
            this.ApplyT2Rec.Name = "ApplyT2Rec";
            this.ApplyT2Rec.Size = new System.Drawing.Size(75, 23);
            this.ApplyT2Rec.TabIndex = 81;
            this.ApplyT2Rec.Text = "Apply";
            this.ApplyT2Rec.UseVisualStyleBackColor = true;
            this.ApplyT2Rec.Click += new System.EventHandler(this.ApplyT2Rec_Click);
            // 
            // InsertT2Rec
            // 
            this.InsertT2Rec.Location = new System.Drawing.Point(482, 232);
            this.InsertT2Rec.Name = "InsertT2Rec";
            this.InsertT2Rec.Size = new System.Drawing.Size(75, 23);
            this.InsertT2Rec.TabIndex = 80;
            this.InsertT2Rec.Text = "InsertNew";
            this.InsertT2Rec.UseVisualStyleBackColor = true;
            this.InsertT2Rec.Click += new System.EventHandler(this.InsertT2Rec_Click);
            // 
            // DelT2Rec
            // 
            this.DelT2Rec.Location = new System.Drawing.Point(401, 232);
            this.DelT2Rec.Name = "DelT2Rec";
            this.DelT2Rec.Size = new System.Drawing.Size(75, 23);
            this.DelT2Rec.TabIndex = 79;
            this.DelT2Rec.Text = "Delete";
            this.DelT2Rec.UseVisualStyleBackColor = true;
            this.DelT2Rec.Click += new System.EventHandler(this.DelT2Rec_Click);
            // 
            // T2IndVal
            // 
            this.T2IndVal.Location = new System.Drawing.Point(467, 102);
            this.T2IndVal.Name = "T2IndVal";
            this.T2IndVal.Size = new System.Drawing.Size(39, 20);
            this.T2IndVal.TabIndex = 78;
            this.T2IndVal.TextChanged += new System.EventHandler(this.T2IndVal_TextChanged);
            // 
            // NextT2
            // 
            this.NextT2.Location = new System.Drawing.Point(512, 100);
            this.NextT2.Name = "NextT2";
            this.NextT2.Size = new System.Drawing.Size(31, 23);
            this.NextT2.TabIndex = 77;
            this.NextT2.Text = ">>";
            this.NextT2.UseVisualStyleBackColor = true;
            this.NextT2.Click += new System.EventHandler(this.NextT2_Click);
            // 
            // PrevT2
            // 
            this.PrevT2.Location = new System.Drawing.Point(434, 100);
            this.PrevT2.Name = "PrevT2";
            this.PrevT2.Size = new System.Drawing.Size(27, 23);
            this.PrevT2.TabIndex = 76;
            this.PrevT2.Text = "<<";
            this.PrevT2.UseVisualStyleBackColor = true;
            this.PrevT2.Click += new System.EventHandler(this.PrevT2_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(405, 105);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(23, 13);
            this.Label7.TabIndex = 75;
            this.Label7.Text = "T2:";
            // 
            // T2Edit
            // 
            this.T2Edit.Location = new System.Drawing.Point(405, 124);
            this.T2Edit.Multiline = true;
            this.T2Edit.Name = "T2Edit";
            this.T2Edit.Size = new System.Drawing.Size(233, 102);
            this.T2Edit.TabIndex = 74;
            // 
            // ApplyT3Rec
            // 
            this.ApplyT3Rec.Location = new System.Drawing.Point(311, 349);
            this.ApplyT3Rec.Name = "ApplyT3Rec";
            this.ApplyT3Rec.Size = new System.Drawing.Size(75, 23);
            this.ApplyT3Rec.TabIndex = 89;
            this.ApplyT3Rec.Text = "Apply";
            this.ApplyT3Rec.UseVisualStyleBackColor = true;
            this.ApplyT3Rec.Click += new System.EventHandler(this.ApplyT3Rec_Click);
            // 
            // InsertT3Rec
            // 
            this.InsertT3Rec.Location = new System.Drawing.Point(230, 349);
            this.InsertT3Rec.Name = "InsertT3Rec";
            this.InsertT3Rec.Size = new System.Drawing.Size(75, 23);
            this.InsertT3Rec.TabIndex = 88;
            this.InsertT3Rec.Text = "InsertNew";
            this.InsertT3Rec.UseVisualStyleBackColor = true;
            this.InsertT3Rec.Click += new System.EventHandler(this.InsertT3Rec_Click);
            // 
            // DelT3Rec
            // 
            this.DelT3Rec.Location = new System.Drawing.Point(149, 349);
            this.DelT3Rec.Name = "DelT3Rec";
            this.DelT3Rec.Size = new System.Drawing.Size(75, 23);
            this.DelT3Rec.TabIndex = 87;
            this.DelT3Rec.Text = "Delete";
            this.DelT3Rec.UseVisualStyleBackColor = true;
            this.DelT3Rec.Click += new System.EventHandler(this.DelT3Rec_Click);
            // 
            // T3Edit
            // 
            this.T3Edit.Location = new System.Drawing.Point(149, 232);
            this.T3Edit.Multiline = true;
            this.T3Edit.Name = "T3Edit";
            this.T3Edit.Size = new System.Drawing.Size(233, 111);
            this.T3Edit.TabIndex = 82;
            // 
            // GCList
            // 
            this.GCList.FormattingEnabled = true;
            this.GCList.Location = new System.Drawing.Point(644, 27);
            this.GCList.Name = "GCList";
            this.GCList.Size = new System.Drawing.Size(216, 316);
            this.GCList.TabIndex = 90;
            this.GCList.SelectedIndexChanged += new System.EventHandler(this.GCList_SelectedIndexChanged);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(642, 8);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(44, 13);
            this.Label9.TabIndex = 91;
            this.Label9.Text = "GC List:";
            // 
            // AddGC
            // 
            this.AddGC.Location = new System.Drawing.Point(645, 344);
            this.AddGC.Name = "AddGC";
            this.AddGC.Size = new System.Drawing.Size(42, 23);
            this.AddGC.TabIndex = 92;
            this.AddGC.Text = "Add";
            this.AddGC.UseVisualStyleBackColor = true;
            this.AddGC.Click += new System.EventHandler(this.AddGC_Click);
            // 
            // DeleteGC
            // 
            this.DeleteGC.Location = new System.Drawing.Point(819, 344);
            this.DeleteGC.Name = "DeleteGC";
            this.DeleteGC.Size = new System.Drawing.Size(42, 23);
            this.DeleteGC.TabIndex = 93;
            this.DeleteGC.Text = "Del";
            this.DeleteGC.UseVisualStyleBackColor = true;
            this.DeleteGC.Click += new System.EventHandler(this.DeleteGC_Click);
            // 
            // EditGC
            // 
            this.EditGC.Location = new System.Drawing.Point(693, 344);
            this.EditGC.Name = "EditGC";
            this.EditGC.Size = new System.Drawing.Size(42, 23);
            this.EditGC.TabIndex = 94;
            this.EditGC.Text = "Edit";
            this.EditGC.UseVisualStyleBackColor = true;
            this.EditGC.Click += new System.EventHandler(this.EditGC_Click);
            // 
            // EditGCVal
            // 
            this.EditGCVal.Location = new System.Drawing.Point(766, 347);
            this.EditGCVal.Name = "EditGCVal";
            this.EditGCVal.Size = new System.Drawing.Size(47, 20);
            this.EditGCVal.TabIndex = 95;
            // 
            // IDVal
            // 
            this.IDVal.Location = new System.Drawing.Point(742, 347);
            this.IDVal.Name = "IDVal";
            this.IDVal.Size = new System.Drawing.Size(18, 20);
            this.IDVal.TabIndex = 96;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(39, 280);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(100, 20);
            this.TextBox1.TabIndex = 97;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(12, 283);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(21, 13);
            this.Label8.TabIndex = 98;
            this.Label8.Text = "ID:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(304, 69);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(27, 13);
            this.Label10.TabIndex = 99;
            this.Label10.Text = "ID4:";
            // 
            // ID4Val
            // 
            this.ID4Val.Location = new System.Drawing.Point(334, 65);
            this.ID4Val.Name = "ID4Val";
            this.ID4Val.Size = new System.Drawing.Size(100, 20);
            this.ID4Val.TabIndex = 100;
            this.ID4Val.TextChanged += new System.EventHandler(this.ID4Val_TextChanged);
            // 
            // ID5Val
            // 
            this.ID5Val.Location = new System.Drawing.Point(476, 62);
            this.ID5Val.Name = "ID5Val";
            this.ID5Val.Size = new System.Drawing.Size(100, 20);
            this.ID5Val.TabIndex = 102;
            this.ID5Val.TextChanged += new System.EventHandler(this.ID5Val_TextChanged);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(446, 66);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(27, 13);
            this.Label11.TabIndex = 101;
            this.Label11.Text = "ID5:";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(392, 349);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(165, 23);
            this.Button1.TabIndex = 103;
            this.Button1.Text = "Export Armature";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ArmatureSave
            // 
            this.ArmatureSave.Filter = "Wavefront model(*.OBJ)|*.obj";
            // 
            // OGIEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 375);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.ID5Val);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.ID4Val);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.IDVal);
            this.Controls.Add(this.EditGCVal);
            this.Controls.Add(this.EditGC);
            this.Controls.Add(this.DeleteGC);
            this.Controls.Add(this.AddGC);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.GCList);
            this.Controls.Add(this.ApplyT3Rec);
            this.Controls.Add(this.InsertT3Rec);
            this.Controls.Add(this.DelT3Rec);
            this.Controls.Add(this.T3Edit);
            this.Controls.Add(this.ApplyT2Rec);
            this.Controls.Add(this.InsertT2Rec);
            this.Controls.Add(this.DelT2Rec);
            this.Controls.Add(this.T2IndVal);
            this.Controls.Add(this.NextT2);
            this.Controls.Add(this.PrevT2);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.T2Edit);
            this.Controls.Add(this.T1IndVal);
            this.Controls.Add(this.NextT1);
            this.Controls.Add(this.PrevT1);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.T1Edit);
            this.Controls.Add(this.Vect2WVal);
            this.Controls.Add(this.Vect1WVal);
            this.Controls.Add(this.Vect2ZVal);
            this.Controls.Add(this.Vect2YVal);
            this.Controls.Add(this.Vect2XVal);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Vect1ZVal);
            this.Controls.Add(this.Vect1YVal);
            this.Controls.Add(this.Vect1XVal);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Unk2Val);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Unk1Val);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.FlagsVal);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.OGITree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OGIEditor";
            this.Text = "OGI Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public TreeView OGITree;
        private Label Label1;
        private TextBox FlagsVal;
        private TextBox Unk1Val;
        private Label Label2;
        private TextBox Unk2Val;
        private Label Label3;
        private TextBox Vect1XVal;
        private Label Label4;
        private TextBox Vect1YVal;
        private TextBox Vect1ZVal;
        private TextBox Vect2ZVal;
        private TextBox Vect2YVal;
        private TextBox Vect2XVal;
        private Label Label5;
        private TextBox Vect1WVal;
        private TextBox Vect2WVal;
        private TextBox T1Edit;
        private Label Label6;
        private Button PrevT1;
        private Button NextT1;
        private TextBox T1IndVal;
        private Button ApplyT2Rec;
        private Button InsertT2Rec;
        private Button DelT2Rec;
        private TextBox T2IndVal;
        private Button NextT2;
        private Button PrevT2;
        private Label Label7;
        private TextBox T2Edit;
        private Button ApplyT3Rec;
        private Button InsertT3Rec;
        private Button DelT3Rec;
        private TextBox T3Edit;
        private ListBox GCList;
        private Label Label9;
        private Button AddGC;
        private Button DeleteGC;
        private Button EditGC;
        private TextBox EditGCVal;
        private TextBox IDVal;
        private TextBox TextBox1;
        private Label Label8;
        private Label Label10;
        private TextBox ID4Val;
        private TextBox ID5Val;
        private Label Label11;
        private Button Button1;
        private SaveFileDialog ArmatureSave;
    }
}
