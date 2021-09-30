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
    partial class GameObjectEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameObjectEditor));
            this.Revert = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.TriggerID = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.GOTree = new System.Windows.Forms.TreeView();
            this.Label2 = new System.Windows.Forms.Label();
            this.IDVal = new System.Windows.Forms.TextBox();
            this.NameVal = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Class1Val = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Class2Val = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Class3Val = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.UnkList = new System.Windows.Forms.ListBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.TextBox6 = new System.Windows.Forms.TextBox();
            this.TextBox7 = new System.Windows.Forms.TextBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Label8 = new System.Windows.Forms.Label();
            this.OGIList = new System.Windows.Forms.ListBox();
            this.TextBox8 = new System.Windows.Forms.TextBox();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.AnimList = new System.Windows.Forms.ListBox();
            this.TextBox9 = new System.Windows.Forms.TextBox();
            this.Button10 = new System.Windows.Forms.Button();
            this.Button11 = new System.Windows.Forms.Button();
            this.Button12 = new System.Windows.Forms.Button();
            this.Label10 = new System.Windows.Forms.Label();
            this.ScrList = new System.Windows.Forms.ListBox();
            this.TextBox10 = new System.Windows.Forms.TextBox();
            this.Button13 = new System.Windows.Forms.Button();
            this.Button14 = new System.Windows.Forms.Button();
            this.Button15 = new System.Windows.Forms.Button();
            this.Label11 = new System.Windows.Forms.Label();
            this.GOList = new System.Windows.Forms.ListBox();
            this.TextBox11 = new System.Windows.Forms.TextBox();
            this.Button16 = new System.Windows.Forms.Button();
            this.Button17 = new System.Windows.Forms.Button();
            this.Button18 = new System.Windows.Forms.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.SoundList = new System.Windows.Forms.ListBox();
            this.TextBox12 = new System.Windows.Forms.TextBox();
            this.Button19 = new System.Windows.Forms.Button();
            this.Button20 = new System.Windows.Forms.Button();
            this.Button21 = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.IntList = new System.Windows.Forms.ListBox();
            this.TextBox13 = new System.Windows.Forms.TextBox();
            this.Button22 = new System.Windows.Forms.Button();
            this.Button23 = new System.Windows.Forms.Button();
            this.Button24 = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.FloatList = new System.Windows.Forms.ListBox();
            this.TextBox14 = new System.Windows.Forms.TextBox();
            this.Button25 = new System.Windows.Forms.Button();
            this.Button26 = new System.Windows.Forms.Button();
            this.Button27 = new System.Windows.Forms.Button();
            this.Label15 = new System.Windows.Forms.Label();
            this.SomeList = new System.Windows.Forms.ListBox();
            this.ParamVal = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(12, 310);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(127, 23);
            this.Revert.TabIndex = 46;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(12, 281);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(127, 23);
            this.Apply.TabIndex = 45;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // TriggerID
            // 
            this.TriggerID.Location = new System.Drawing.Point(192, -31);
            this.TriggerID.Name = "TriggerID";
            this.TriggerID.Size = new System.Drawing.Size(62, 20);
            this.TriggerID.TabIndex = 44;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(165, -28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(21, 13);
            this.Label1.TabIndex = 43;
            this.Label1.Text = "ID:";
            // 
            // GOTree
            // 
            this.GOTree.Location = new System.Drawing.Point(12, 12);
            this.GOTree.Name = "GOTree";
            this.GOTree.Size = new System.Drawing.Size(127, 262);
            this.GOTree.TabIndex = 42;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(146, 13);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(21, 13);
            this.Label2.TabIndex = 47;
            this.Label2.Text = "ID:";
            // 
            // IDVal
            // 
            this.IDVal.Location = new System.Drawing.Point(190, 10);
            this.IDVal.Name = "IDVal";
            this.IDVal.Size = new System.Drawing.Size(81, 20);
            this.IDVal.TabIndex = 48;
            // 
            // NameVal
            // 
            this.NameVal.Location = new System.Drawing.Point(190, 36);
            this.NameVal.Name = "NameVal";
            this.NameVal.Size = new System.Drawing.Size(81, 20);
            this.NameVal.TabIndex = 50;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(146, 39);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(38, 13);
            this.Label3.TabIndex = 49;
            this.Label3.Text = "Name:";
            // 
            // Class1Val
            // 
            this.Class1Val.Location = new System.Drawing.Point(190, 62);
            this.Class1Val.Name = "Class1Val";
            this.Class1Val.Size = new System.Drawing.Size(81, 20);
            this.Class1Val.TabIndex = 52;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(146, 65);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(41, 13);
            this.Label4.TabIndex = 51;
            this.Label4.Text = "Class1:";
            // 
            // Class2Val
            // 
            this.Class2Val.Location = new System.Drawing.Point(190, 88);
            this.Class2Val.Name = "Class2Val";
            this.Class2Val.Size = new System.Drawing.Size(81, 20);
            this.Class2Val.TabIndex = 54;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(146, 91);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(41, 13);
            this.Label5.TabIndex = 53;
            this.Label5.Text = "Class2:";
            // 
            // Class3Val
            // 
            this.Class3Val.Location = new System.Drawing.Point(190, 114);
            this.Class3Val.Name = "Class3Val";
            this.Class3Val.Size = new System.Drawing.Size(81, 20);
            this.Class3Val.TabIndex = 56;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(146, 117);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(41, 13);
            this.Label6.TabIndex = 55;
            this.Label6.Text = "Class3:";
            // 
            // UnkList
            // 
            this.UnkList.FormattingEnabled = true;
            this.UnkList.Location = new System.Drawing.Point(277, 25);
            this.UnkList.Name = "UnkList";
            this.UnkList.Size = new System.Drawing.Size(93, 95);
            this.UnkList.TabIndex = 57;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(277, 9);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(71, 13);
            this.Label7.TabIndex = 58;
            this.Label7.Text = "UnknownShit";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(376, 64);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(46, 23);
            this.Button1.TabIndex = 59;
            this.Button1.Text = "Add";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(376, 93);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(46, 23);
            this.Button2.TabIndex = 60;
            this.Button2.Text = "Delete";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(277, 124);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(46, 23);
            this.Button3.TabIndex = 61;
            this.Button3.Text = "Edit";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new System.Drawing.Point(329, 127);
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new System.Drawing.Size(93, 20);
            this.TextBox6.TabIndex = 62;
            // 
            // TextBox7
            // 
            this.TextBox7.Location = new System.Drawing.Point(480, 127);
            this.TextBox7.Name = "TextBox7";
            this.TextBox7.Size = new System.Drawing.Size(93, 20);
            this.TextBox7.TabIndex = 68;
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(428, 124);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(46, 23);
            this.Button4.TabIndex = 67;
            this.Button4.Text = "Edit";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(527, 93);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(46, 23);
            this.Button5.TabIndex = 66;
            this.Button5.Text = "Delete";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(527, 64);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(46, 23);
            this.Button6.TabIndex = 65;
            this.Button6.Text = "Add";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(428, 9);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(26, 13);
            this.Label8.TabIndex = 64;
            this.Label8.Text = "OGI";
            // 
            // OGIList
            // 
            this.OGIList.FormattingEnabled = true;
            this.OGIList.Location = new System.Drawing.Point(428, 25);
            this.OGIList.Name = "OGIList";
            this.OGIList.Size = new System.Drawing.Size(93, 95);
            this.OGIList.TabIndex = 63;
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new System.Drawing.Point(631, 127);
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new System.Drawing.Size(93, 20);
            this.TextBox8.TabIndex = 74;
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(579, 124);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(46, 23);
            this.Button7.TabIndex = 73;
            this.Button7.Text = "Edit";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button8
            // 
            this.Button8.Location = new System.Drawing.Point(678, 93);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(46, 23);
            this.Button8.TabIndex = 72;
            this.Button8.Text = "Delete";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(678, 64);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(46, 23);
            this.Button9.TabIndex = 71;
            this.Button9.Text = "Add";
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(579, 9);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(53, 13);
            this.Label9.TabIndex = 70;
            this.Label9.Text = "Animation";
            // 
            // AnimList
            // 
            this.AnimList.FormattingEnabled = true;
            this.AnimList.Location = new System.Drawing.Point(579, 25);
            this.AnimList.Name = "AnimList";
            this.AnimList.Size = new System.Drawing.Size(93, 95);
            this.AnimList.TabIndex = 69;
            // 
            // TextBox9
            // 
            this.TextBox9.Location = new System.Drawing.Point(329, 281);
            this.TextBox9.Name = "TextBox9";
            this.TextBox9.Size = new System.Drawing.Size(93, 20);
            this.TextBox9.TabIndex = 80;
            // 
            // Button10
            // 
            this.Button10.Location = new System.Drawing.Point(277, 278);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(46, 23);
            this.Button10.TabIndex = 79;
            this.Button10.Text = "Edit";
            this.Button10.UseVisualStyleBackColor = true;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button11
            // 
            this.Button11.Location = new System.Drawing.Point(376, 247);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(46, 23);
            this.Button11.TabIndex = 78;
            this.Button11.Text = "Delete";
            this.Button11.UseVisualStyleBackColor = true;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button12
            // 
            this.Button12.Location = new System.Drawing.Point(376, 218);
            this.Button12.Name = "Button12";
            this.Button12.Size = new System.Drawing.Size(46, 23);
            this.Button12.TabIndex = 77;
            this.Button12.Text = "Add";
            this.Button12.UseVisualStyleBackColor = true;
            this.Button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(277, 163);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(39, 13);
            this.Label10.TabIndex = 76;
            this.Label10.Text = "Scripts";
            // 
            // ScrList
            // 
            this.ScrList.FormattingEnabled = true;
            this.ScrList.Location = new System.Drawing.Point(277, 179);
            this.ScrList.Name = "ScrList";
            this.ScrList.Size = new System.Drawing.Size(93, 95);
            this.ScrList.TabIndex = 75;
            // 
            // TextBox10
            // 
            this.TextBox10.Location = new System.Drawing.Point(483, 281);
            this.TextBox10.Name = "TextBox10";
            this.TextBox10.Size = new System.Drawing.Size(93, 20);
            this.TextBox10.TabIndex = 86;
            // 
            // Button13
            // 
            this.Button13.Location = new System.Drawing.Point(431, 278);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(46, 23);
            this.Button13.TabIndex = 85;
            this.Button13.Text = "Edit";
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Button14
            // 
            this.Button14.Location = new System.Drawing.Point(530, 247);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(46, 23);
            this.Button14.TabIndex = 84;
            this.Button14.Text = "Delete";
            this.Button14.UseVisualStyleBackColor = true;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button15
            // 
            this.Button15.Location = new System.Drawing.Point(530, 218);
            this.Button15.Name = "Button15";
            this.Button15.Size = new System.Drawing.Size(46, 23);
            this.Button15.TabIndex = 83;
            this.Button15.Text = "Add";
            this.Button15.UseVisualStyleBackColor = true;
            this.Button15.Click += new System.EventHandler(this.Button15_Click);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(431, 163);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(71, 13);
            this.Label11.TabIndex = 82;
            this.Label11.Text = "GameObjects";
            // 
            // GOList
            // 
            this.GOList.FormattingEnabled = true;
            this.GOList.Location = new System.Drawing.Point(431, 179);
            this.GOList.Name = "GOList";
            this.GOList.Size = new System.Drawing.Size(93, 95);
            this.GOList.TabIndex = 81;
            // 
            // TextBox11
            // 
            this.TextBox11.Location = new System.Drawing.Point(634, 281);
            this.TextBox11.Name = "TextBox11";
            this.TextBox11.Size = new System.Drawing.Size(93, 20);
            this.TextBox11.TabIndex = 92;
            // 
            // Button16
            // 
            this.Button16.Location = new System.Drawing.Point(582, 278);
            this.Button16.Name = "Button16";
            this.Button16.Size = new System.Drawing.Size(46, 23);
            this.Button16.TabIndex = 91;
            this.Button16.Text = "Edit";
            this.Button16.UseVisualStyleBackColor = true;
            this.Button16.Click += new System.EventHandler(this.Button16_Click);
            // 
            // Button17
            // 
            this.Button17.Location = new System.Drawing.Point(681, 247);
            this.Button17.Name = "Button17";
            this.Button17.Size = new System.Drawing.Size(46, 23);
            this.Button17.TabIndex = 90;
            this.Button17.Text = "Delete";
            this.Button17.UseVisualStyleBackColor = true;
            this.Button17.Click += new System.EventHandler(this.Button17_Click);
            // 
            // Button18
            // 
            this.Button18.Location = new System.Drawing.Point(681, 218);
            this.Button18.Name = "Button18";
            this.Button18.Size = new System.Drawing.Size(46, 23);
            this.Button18.TabIndex = 89;
            this.Button18.Text = "Add";
            this.Button18.UseVisualStyleBackColor = true;
            this.Button18.Click += new System.EventHandler(this.Button18_Click);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(582, 163);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(43, 13);
            this.Label12.TabIndex = 88;
            this.Label12.Text = "Sounds";
            // 
            // SoundList
            // 
            this.SoundList.FormattingEnabled = true;
            this.SoundList.Location = new System.Drawing.Point(582, 179);
            this.SoundList.Name = "SoundList";
            this.SoundList.Size = new System.Drawing.Size(93, 95);
            this.SoundList.TabIndex = 87;
            // 
            // TextBox12
            // 
            this.TextBox12.Location = new System.Drawing.Point(637, 427);
            this.TextBox12.Name = "TextBox12";
            this.TextBox12.Size = new System.Drawing.Size(93, 20);
            this.TextBox12.TabIndex = 110;
            // 
            // Button19
            // 
            this.Button19.Location = new System.Drawing.Point(585, 424);
            this.Button19.Name = "Button19";
            this.Button19.Size = new System.Drawing.Size(46, 23);
            this.Button19.TabIndex = 109;
            this.Button19.Text = "Edit";
            this.Button19.UseVisualStyleBackColor = true;
            this.Button19.Click += new System.EventHandler(this.Button19_Click);
            // 
            // Button20
            // 
            this.Button20.Location = new System.Drawing.Point(684, 393);
            this.Button20.Name = "Button20";
            this.Button20.Size = new System.Drawing.Size(46, 23);
            this.Button20.TabIndex = 108;
            this.Button20.Text = "Delete";
            this.Button20.UseVisualStyleBackColor = true;
            this.Button20.Click += new System.EventHandler(this.Button20_Click);
            // 
            // Button21
            // 
            this.Button21.Location = new System.Drawing.Point(684, 364);
            this.Button21.Name = "Button21";
            this.Button21.Size = new System.Drawing.Size(46, 23);
            this.Button21.TabIndex = 107;
            this.Button21.Text = "Add";
            this.Button21.UseVisualStyleBackColor = true;
            this.Button21.Click += new System.EventHandler(this.Button21_Click);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(585, 309);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(45, 13);
            this.Label13.TabIndex = 106;
            this.Label13.Text = "Integers";
            // 
            // IntList
            // 
            this.IntList.FormattingEnabled = true;
            this.IntList.Location = new System.Drawing.Point(585, 325);
            this.IntList.Name = "IntList";
            this.IntList.Size = new System.Drawing.Size(93, 95);
            this.IntList.TabIndex = 105;
            // 
            // TextBox13
            // 
            this.TextBox13.Location = new System.Drawing.Point(486, 427);
            this.TextBox13.Name = "TextBox13";
            this.TextBox13.Size = new System.Drawing.Size(93, 20);
            this.TextBox13.TabIndex = 104;
            // 
            // Button22
            // 
            this.Button22.Location = new System.Drawing.Point(434, 424);
            this.Button22.Name = "Button22";
            this.Button22.Size = new System.Drawing.Size(46, 23);
            this.Button22.TabIndex = 103;
            this.Button22.Text = "Edit";
            this.Button22.UseVisualStyleBackColor = true;
            this.Button22.Click += new System.EventHandler(this.Button22_Click);
            // 
            // Button23
            // 
            this.Button23.Location = new System.Drawing.Point(533, 393);
            this.Button23.Name = "Button23";
            this.Button23.Size = new System.Drawing.Size(46, 23);
            this.Button23.TabIndex = 102;
            this.Button23.Text = "Delete";
            this.Button23.UseVisualStyleBackColor = true;
            this.Button23.Click += new System.EventHandler(this.Button23_Click);
            // 
            // Button24
            // 
            this.Button24.Location = new System.Drawing.Point(533, 364);
            this.Button24.Name = "Button24";
            this.Button24.Size = new System.Drawing.Size(46, 23);
            this.Button24.TabIndex = 101;
            this.Button24.Text = "Add";
            this.Button24.UseVisualStyleBackColor = true;
            this.Button24.Click += new System.EventHandler(this.Button24_Click);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(434, 309);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(35, 13);
            this.Label14.TabIndex = 100;
            this.Label14.Text = "Floats";
            // 
            // FloatList
            // 
            this.FloatList.FormattingEnabled = true;
            this.FloatList.Location = new System.Drawing.Point(434, 325);
            this.FloatList.Name = "FloatList";
            this.FloatList.Size = new System.Drawing.Size(93, 95);
            this.FloatList.TabIndex = 99;
            // 
            // TextBox14
            // 
            this.TextBox14.Location = new System.Drawing.Point(332, 427);
            this.TextBox14.Name = "TextBox14";
            this.TextBox14.Size = new System.Drawing.Size(93, 20);
            this.TextBox14.TabIndex = 98;
            // 
            // Button25
            // 
            this.Button25.Location = new System.Drawing.Point(280, 424);
            this.Button25.Name = "Button25";
            this.Button25.Size = new System.Drawing.Size(46, 23);
            this.Button25.TabIndex = 97;
            this.Button25.Text = "Edit";
            this.Button25.UseVisualStyleBackColor = true;
            this.Button25.Click += new System.EventHandler(this.Button25_Click);
            // 
            // Button26
            // 
            this.Button26.Location = new System.Drawing.Point(379, 393);
            this.Button26.Name = "Button26";
            this.Button26.Size = new System.Drawing.Size(46, 23);
            this.Button26.TabIndex = 96;
            this.Button26.Text = "Delete";
            this.Button26.UseVisualStyleBackColor = true;
            this.Button26.Click += new System.EventHandler(this.Button26_Click);
            // 
            // Button27
            // 
            this.Button27.Location = new System.Drawing.Point(379, 364);
            this.Button27.Name = "Button27";
            this.Button27.Size = new System.Drawing.Size(46, 23);
            this.Button27.TabIndex = 95;
            this.Button27.Text = "Add";
            this.Button27.UseVisualStyleBackColor = true;
            this.Button27.Click += new System.EventHandler(this.Button27_Click);
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(280, 309);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(66, 13);
            this.Label15.TabIndex = 94;
            this.Label15.Text = "SomeValues";
            // 
            // SomeList
            // 
            this.SomeList.FormattingEnabled = true;
            this.SomeList.Location = new System.Drawing.Point(280, 325);
            this.SomeList.Name = "SomeList";
            this.SomeList.Size = new System.Drawing.Size(93, 95);
            this.SomeList.TabIndex = 93;
            // 
            // ParamVal
            // 
            this.ParamVal.Location = new System.Drawing.Point(190, 140);
            this.ParamVal.Name = "ParamVal";
            this.ParamVal.Size = new System.Drawing.Size(81, 20);
            this.ParamVal.TabIndex = 112;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(146, 143);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(40, 13);
            this.Label16.TabIndex = 111;
            this.Label16.Text = "Param:";
            // 
            // GameObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 454);
            this.Controls.Add(this.ParamVal);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.TextBox12);
            this.Controls.Add(this.Button19);
            this.Controls.Add(this.Button20);
            this.Controls.Add(this.Button21);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.IntList);
            this.Controls.Add(this.TextBox13);
            this.Controls.Add(this.Button22);
            this.Controls.Add(this.Button23);
            this.Controls.Add(this.Button24);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.FloatList);
            this.Controls.Add(this.TextBox14);
            this.Controls.Add(this.Button25);
            this.Controls.Add(this.Button26);
            this.Controls.Add(this.Button27);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.SomeList);
            this.Controls.Add(this.TextBox11);
            this.Controls.Add(this.Button16);
            this.Controls.Add(this.Button17);
            this.Controls.Add(this.Button18);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.SoundList);
            this.Controls.Add(this.TextBox10);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Button14);
            this.Controls.Add(this.Button15);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.GOList);
            this.Controls.Add(this.TextBox9);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.Button11);
            this.Controls.Add(this.Button12);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.ScrList);
            this.Controls.Add(this.TextBox8);
            this.Controls.Add(this.Button7);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Button9);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.AnimList);
            this.Controls.Add(this.TextBox7);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.OGIList);
            this.Controls.Add(this.TextBox6);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.UnkList);
            this.Controls.Add(this.Class3Val);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Class2Val);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Class1Val);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.NameVal);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.IDVal);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.TriggerID);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.GOTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameObjectEditor";
            this.Text = "GameObject Editor";
            this.Load += new System.EventHandler(this.GameObjectEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button Revert;
        private Button Apply;
        private TextBox TriggerID;
        private Label Label1;
        public TreeView GOTree;
        private Label Label2;
        private TextBox IDVal;
        private TextBox NameVal;
        private Label Label3;
        private TextBox Class1Val;
        private Label Label4;
        private TextBox Class2Val;
        private Label Label5;
        private TextBox Class3Val;
        private Label Label6;
        private ListBox UnkList;
        private Label Label7;
        private Button Button1;
        private Button Button2;
        private Button Button3;
        private TextBox TextBox6;
        private TextBox TextBox7;
        private Button Button4;
        private Button Button5;
        private Button Button6;
        private Label Label8;
        private ListBox OGIList;
        private TextBox TextBox8;
        private Button Button7;
        private Button Button8;
        private Button Button9;
        private Label Label9;
        private ListBox AnimList;
        private TextBox TextBox9;
        private Button Button10;
        private Button Button11;
        private Button Button12;
        private Label Label10;
        private ListBox ScrList;
        private TextBox TextBox10;
        private Button Button13;
        private Button Button14;
        private Button Button15;
        private Label Label11;
        private ListBox GOList;
        private TextBox TextBox11;
        private Button Button16;
        private Button Button17;
        private Button Button18;
        private Label Label12;
        private ListBox SoundList;
        private TextBox TextBox12;
        private Button Button19;
        private Button Button20;
        private Button Button21;
        private Label Label13;
        private ListBox IntList;
        private TextBox TextBox13;
        private Button Button22;
        private Button Button23;
        private Button Button24;
        private Label Label14;
        private ListBox FloatList;
        private TextBox TextBox14;
        private Button Button25;
        private Button Button26;
        private Button Button27;
        private Label Label15;
        private ListBox SomeList;
        private TextBox ParamVal;
        private Label Label16;
    }
}
