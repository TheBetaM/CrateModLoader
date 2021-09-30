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
    partial class SurfaceBehaviorEditor : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SurfaceBehaviorEditor));
            this.Revert = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.SBTree = new System.Windows.Forms.TreeView();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Flag18 = new System.Windows.Forms.CheckBox();
            this.Flag17 = new System.Windows.Forms.CheckBox();
            this.Flag16 = new System.Windows.Forms.CheckBox();
            this.Flag15 = new System.Windows.Forms.CheckBox();
            this.Flag14 = new System.Windows.Forms.CheckBox();
            this.Flag13 = new System.Windows.Forms.CheckBox();
            this.Flag12 = new System.Windows.Forms.CheckBox();
            this.Flag11 = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Flag28 = new System.Windows.Forms.CheckBox();
            this.Flag27 = new System.Windows.Forms.CheckBox();
            this.Flag26 = new System.Windows.Forms.CheckBox();
            this.Flag25 = new System.Windows.Forms.CheckBox();
            this.Flag24 = new System.Windows.Forms.CheckBox();
            this.Flag23 = new System.Windows.Forms.CheckBox();
            this.Flag22 = new System.Windows.Forms.CheckBox();
            this.Flag21 = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Flag38 = new System.Windows.Forms.CheckBox();
            this.Flag37 = new System.Windows.Forms.CheckBox();
            this.Flag36 = new System.Windows.Forms.CheckBox();
            this.Flag35 = new System.Windows.Forms.CheckBox();
            this.Flag34 = new System.Windows.Forms.CheckBox();
            this.Flag33 = new System.Windows.Forms.CheckBox();
            this.Flag32 = new System.Windows.Forms.CheckBox();
            this.Flag31 = new System.Windows.Forms.CheckBox();
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
            this.Label6 = new System.Windows.Forms.Label();
            this.ID10Val = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.ID9Val = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.ID8Val = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.ID7Val = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.ID6Val = new System.Windows.Forms.TextBox();
            this.F11Val = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.F12Val = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.F13Val = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.F14Val = new System.Windows.Forms.TextBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.F41Val = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.F44Val = new System.Windows.Forms.TextBox();
            this.F42Val = new System.Windows.Forms.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.Label26 = new System.Windows.Forms.Label();
            this.F43Val = new System.Windows.Forms.TextBox();
            this.F31Val = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.F34Val = new System.Windows.Forms.TextBox();
            this.F32Val = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.F33Val = new System.Windows.Forms.TextBox();
            this.F21Val = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.F24Val = new System.Windows.Forms.TextBox();
            this.F22Val = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label18 = new System.Windows.Forms.Label();
            this.F23Val = new System.Windows.Forms.TextBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.EI1Val = new System.Windows.Forms.TextBox();
            this.EI2Val = new System.Windows.Forms.TextBox();
            this.Label28 = new System.Windows.Forms.Label();
            this.EI3Val = new System.Windows.Forms.TextBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.EI6Val = new System.Windows.Forms.TextBox();
            this.Label30 = new System.Windows.Forms.Label();
            this.EI5Val = new System.Windows.Forms.TextBox();
            this.Label31 = new System.Windows.Forms.Label();
            this.EI4Val = new System.Windows.Forms.TextBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.EI12Val = new System.Windows.Forms.TextBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.EI11Val = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.EI10Val = new System.Windows.Forms.TextBox();
            this.Label35 = new System.Windows.Forms.Label();
            this.EI9Val = new System.Windows.Forms.TextBox();
            this.Label36 = new System.Windows.Forms.Label();
            this.EI8Val = new System.Windows.Forms.TextBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.EI7Val = new System.Windows.Forms.TextBox();
            this.Label38 = new System.Windows.Forms.Label();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.Flag48 = new System.Windows.Forms.CheckBox();
            this.Flag47 = new System.Windows.Forms.CheckBox();
            this.Flag46 = new System.Windows.Forms.CheckBox();
            this.Flag45 = new System.Windows.Forms.CheckBox();
            this.Flag44 = new System.Windows.Forms.CheckBox();
            this.Flag43 = new System.Windows.Forms.CheckBox();
            this.Flag42 = new System.Windows.Forms.CheckBox();
            this.Flag41 = new System.Windows.Forms.CheckBox();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Revert
            // 
            this.Revert.Location = new System.Drawing.Point(12, 232);
            this.Revert.Name = "Revert";
            this.Revert.Size = new System.Drawing.Size(121, 23);
            this.Revert.TabIndex = 14;
            this.Revert.Text = "Revert";
            this.Revert.UseVisualStyleBackColor = true;
            this.Revert.Click += new System.EventHandler(this.Revert_Click);
            // 
            // Apply
            // 
            this.Apply.Location = new System.Drawing.Point(12, 203);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(121, 23);
            this.Apply.TabIndex = 13;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // SBTree
            // 
            this.SBTree.Location = new System.Drawing.Point(12, 12);
            this.SBTree.Name = "SBTree";
            this.SBTree.Size = new System.Drawing.Size(121, 185);
            this.SBTree.TabIndex = 12;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Flag18);
            this.GroupBox1.Controls.Add(this.Flag17);
            this.GroupBox1.Controls.Add(this.Flag16);
            this.GroupBox1.Controls.Add(this.Flag15);
            this.GroupBox1.Controls.Add(this.Flag14);
            this.GroupBox1.Controls.Add(this.Flag13);
            this.GroupBox1.Controls.Add(this.Flag12);
            this.GroupBox1.Controls.Add(this.Flag11);
            this.GroupBox1.Location = new System.Drawing.Point(139, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(80, 202);
            this.GroupBox1.TabIndex = 15;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Flags1";
            // 
            // Flag18
            // 
            this.Flag18.AutoSize = true;
            this.Flag18.Location = new System.Drawing.Point(6, 180);
            this.Flag18.Name = "Flag18";
            this.Flag18.Size = new System.Drawing.Size(69, 17);
            this.Flag18.TabIndex = 7;
            this.Flag18.Text = "SlipMetal";
            this.Flag18.UseVisualStyleBackColor = true;
            // 
            // Flag17
            // 
            this.Flag17.AutoSize = true;
            this.Flag17.Location = new System.Drawing.Point(6, 157);
            this.Flag17.Name = "Flag17";
            this.Flag17.Size = new System.Drawing.Size(53, 17);
            this.Flag17.TabIndex = 6;
            this.Flag17.Text = "Grass";
            this.Flag17.UseVisualStyleBackColor = true;
            // 
            // Flag16
            // 
            this.Flag16.AutoSize = true;
            this.Flag16.Location = new System.Drawing.Point(6, 134);
            this.Flag16.Name = "Flag16";
            this.Flag16.Size = new System.Drawing.Size(71, 17);
            this.Flag16.TabIndex = 5;
            this.Flag16.Text = "FallDeath";
            this.Flag16.UseVisualStyleBackColor = true;
            // 
            // Flag15
            // 
            this.Flag15.AutoSize = true;
            this.Flag15.Location = new System.Drawing.Point(6, 111);
            this.Flag15.Name = "Flag15";
            this.Flag15.Size = new System.Drawing.Size(72, 17);
            this.Flag15.TabIndex = 4;
            this.Flag15.Text = "InstDeath";
            this.Flag15.UseVisualStyleBackColor = true;
            // 
            // Flag14
            // 
            this.Flag14.AutoSize = true;
            this.Flag14.Location = new System.Drawing.Point(6, 88);
            this.Flag14.Name = "Flag14";
            this.Flag14.Size = new System.Drawing.Size(50, 17);
            this.Flag14.TabIndex = 3;
            this.Flag14.Text = "Lava";
            this.Flag14.UseVisualStyleBackColor = true;
            // 
            // Flag13
            // 
            this.Flag13.AutoSize = true;
            this.Flag13.Location = new System.Drawing.Point(6, 65);
            this.Flag13.Name = "Flag13";
            this.Flag13.Size = new System.Drawing.Size(71, 17);
            this.Flag13.TabIndex = 2;
            this.Flag13.Text = "SlipperyH";
            this.Flag13.UseVisualStyleBackColor = true;
            // 
            // Flag12
            // 
            this.Flag12.AutoSize = true;
            this.Flag12.Location = new System.Drawing.Point(6, 42);
            this.Flag12.Name = "Flag12";
            this.Flag12.Size = new System.Drawing.Size(69, 17);
            this.Flag12.TabIndex = 1;
            this.Flag12.Text = "SlipperyL";
            this.Flag12.UseVisualStyleBackColor = true;
            // 
            // Flag11
            // 
            this.Flag11.AutoSize = true;
            this.Flag11.Location = new System.Drawing.Point(6, 19);
            this.Flag11.Name = "Flag11";
            this.Flag11.Size = new System.Drawing.Size(60, 17);
            this.Flag11.TabIndex = 0;
            this.Flag11.Text = "Default";
            this.Flag11.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.Flag28);
            this.GroupBox2.Controls.Add(this.Flag27);
            this.GroupBox2.Controls.Add(this.Flag26);
            this.GroupBox2.Controls.Add(this.Flag25);
            this.GroupBox2.Controls.Add(this.Flag24);
            this.GroupBox2.Controls.Add(this.Flag23);
            this.GroupBox2.Controls.Add(this.Flag22);
            this.GroupBox2.Controls.Add(this.Flag21);
            this.GroupBox2.Location = new System.Drawing.Point(225, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(80, 202);
            this.GroupBox2.TabIndex = 16;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Flags2";
            // 
            // Flag28
            // 
            this.Flag28.AutoSize = true;
            this.Flag28.Location = new System.Drawing.Point(6, 180);
            this.Flag28.Name = "Flag28";
            this.Flag28.Size = new System.Drawing.Size(53, 17);
            this.Flag28.TabIndex = 7;
            this.Flag28.Text = "Snow";
            this.Flag28.UseVisualStyleBackColor = true;
            // 
            // Flag27
            // 
            this.Flag27.AutoSize = true;
            this.Flag27.Location = new System.Drawing.Point(6, 157);
            this.Flag27.Name = "Flag27";
            this.Flag27.Size = new System.Drawing.Size(69, 17);
            this.Flag27.TabIndex = 6;
            this.Flag27.Text = "SlipRock";
            this.Flag27.UseVisualStyleBackColor = true;
            // 
            // Flag26
            // 
            this.Flag26.AutoSize = true;
            this.Flag26.Location = new System.Drawing.Point(6, 134);
            this.Flag26.Name = "Flag26";
            this.Flag26.Size = new System.Drawing.Size(52, 17);
            this.Flag26.TabIndex = 5;
            this.Flag26.Text = "Rock";
            this.Flag26.UseVisualStyleBackColor = true;
            // 
            // Flag25
            // 
            this.Flag25.AutoSize = true;
            this.Flag25.Location = new System.Drawing.Point(6, 111);
            this.Flag25.Name = "Flag25";
            this.Flag25.Size = new System.Drawing.Size(55, 17);
            this.Flag25.TabIndex = 4;
            this.Flag25.Text = "Water";
            this.Flag25.UseVisualStyleBackColor = true;
            // 
            // Flag24
            // 
            this.Flag24.AutoSize = true;
            this.Flag24.Location = new System.Drawing.Point(6, 88);
            this.Flag24.Name = "Flag24";
            this.Flag24.Size = new System.Drawing.Size(47, 17);
            this.Flag24.TabIndex = 3;
            this.Flag24.Text = "Mud";
            this.Flag24.UseVisualStyleBackColor = true;
            // 
            // Flag23
            // 
            this.Flag23.AutoSize = true;
            this.Flag23.Location = new System.Drawing.Point(6, 65);
            this.Flag23.Name = "Flag23";
            this.Flag23.Size = new System.Drawing.Size(51, 17);
            this.Flag23.TabIndex = 2;
            this.Flag23.Text = "Sand";
            this.Flag23.UseVisualStyleBackColor = true;
            // 
            // Flag22
            // 
            this.Flag22.AutoSize = true;
            this.Flag22.Location = new System.Drawing.Point(6, 42);
            this.Flag22.Name = "Flag22";
            this.Flag22.Size = new System.Drawing.Size(52, 17);
            this.Flag22.TabIndex = 1;
            this.Flag22.Text = "Metal";
            this.Flag22.UseVisualStyleBackColor = true;
            // 
            // Flag21
            // 
            this.Flag21.AutoSize = true;
            this.Flag21.Location = new System.Drawing.Point(6, 19);
            this.Flag21.Name = "Flag21";
            this.Flag21.Size = new System.Drawing.Size(55, 17);
            this.Flag21.TabIndex = 0;
            this.Flag21.Text = "Wood";
            this.Flag21.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.Flag38);
            this.GroupBox3.Controls.Add(this.Flag37);
            this.GroupBox3.Controls.Add(this.Flag36);
            this.GroupBox3.Controls.Add(this.Flag35);
            this.GroupBox3.Controls.Add(this.Flag34);
            this.GroupBox3.Controls.Add(this.Flag33);
            this.GroupBox3.Controls.Add(this.Flag32);
            this.GroupBox3.Controls.Add(this.Flag31);
            this.GroupBox3.Location = new System.Drawing.Point(311, 12);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(80, 202);
            this.GroupBox3.TabIndex = 17;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Flags3";
            // 
            // Flag38
            // 
            this.Flag38.AutoSize = true;
            this.Flag38.Location = new System.Drawing.Point(6, 180);
            this.Flag38.Name = "Flag38";
            this.Flag38.Size = new System.Drawing.Size(71, 17);
            this.Flag38.TabIndex = 7;
            this.Flag38.Text = "Drowning";
            this.Flag38.UseVisualStyleBackColor = true;
            // 
            // Flag37
            // 
            this.Flag37.AutoSize = true;
            this.Flag37.Location = new System.Drawing.Point(6, 157);
            this.Flag37.Name = "Flag37";
            this.Flag37.Size = new System.Drawing.Size(64, 17);
            this.Flag37.TabIndex = 6;
            this.Flag37.Text = "SlipIceL";
            this.Flag37.UseVisualStyleBackColor = true;
            // 
            // Flag36
            // 
            this.Flag36.AutoSize = true;
            this.Flag36.Location = new System.Drawing.Point(6, 134);
            this.Flag36.Name = "Flag36";
            this.Flag36.Size = new System.Drawing.Size(70, 17);
            this.Flag36.TabIndex = 5;
            this.Flag36.Text = "StneTiles";
            this.Flag36.UseVisualStyleBackColor = true;
            // 
            // Flag35
            // 
            this.Flag35.AutoSize = true;
            this.Flag35.Location = new System.Drawing.Point(6, 111);
            this.Flag35.Name = "Flag35";
            this.Flag35.Size = new System.Drawing.Size(74, 17);
            this.Flag35.TabIndex = 4;
            this.Flag35.Text = "CamBlock";
            this.Flag35.UseVisualStyleBackColor = true;
            // 
            // Flag34
            // 
            this.Flag34.AutoSize = true;
            this.Flag34.Location = new System.Drawing.Point(6, 88);
            this.Flag34.Name = "Flag34";
            this.Flag34.Size = new System.Drawing.Size(70, 17);
            this.Flag34.TabIndex = 3;
            this.Flag34.Text = "HackRail";
            this.Flag34.UseVisualStyleBackColor = true;
            // 
            // Flag33
            // 
            this.Flag33.AutoSize = true;
            this.Flag33.Location = new System.Drawing.Point(6, 65);
            this.Flag33.Name = "Flag33";
            this.Flag33.Size = new System.Drawing.Size(73, 17);
            this.Flag33.TabIndex = 2;
            this.Flag33.Text = "GlassWall";
            this.Flag33.UseVisualStyleBackColor = true;
            // 
            // Flag32
            // 
            this.Flag32.AutoSize = true;
            this.Flag32.Location = new System.Drawing.Point(6, 42);
            this.Flag32.Name = "Flag32";
            this.Flag32.Size = new System.Drawing.Size(41, 17);
            this.Flag32.TabIndex = 1;
            this.Flag32.Text = "Ice";
            this.Flag32.UseVisualStyleBackColor = true;
            // 
            // Flag31
            // 
            this.Flag31.AutoSize = true;
            this.Flag31.Location = new System.Drawing.Point(6, 19);
            this.Flag31.Name = "Flag31";
            this.Flag31.Size = new System.Drawing.Size(75, 17);
            this.Flag31.TabIndex = 0;
            this.Flag31.Text = "StckSnow";
            this.Flag31.UseVisualStyleBackColor = true;
            // 
            // ID1Val
            // 
            this.ID1Val.Location = new System.Drawing.Point(516, 9);
            this.ID1Val.Name = "ID1Val";
            this.ID1Val.Size = new System.Drawing.Size(58, 20);
            this.ID1Val.TabIndex = 18;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(483, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(27, 13);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "ID1:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(483, 38);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(27, 13);
            this.Label2.TabIndex = 21;
            this.Label2.Text = "ID2:";
            // 
            // ID2Val
            // 
            this.ID2Val.Location = new System.Drawing.Point(516, 35);
            this.ID2Val.Name = "ID2Val";
            this.ID2Val.Size = new System.Drawing.Size(58, 20);
            this.ID2Val.TabIndex = 20;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(483, 64);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(27, 13);
            this.Label3.TabIndex = 23;
            this.Label3.Text = "ID3:";
            // 
            // ID3Val
            // 
            this.ID3Val.Location = new System.Drawing.Point(516, 61);
            this.ID3Val.Name = "ID3Val";
            this.ID3Val.Size = new System.Drawing.Size(58, 20);
            this.ID3Val.TabIndex = 22;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(483, 90);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(27, 13);
            this.Label4.TabIndex = 25;
            this.Label4.Text = "ID4:";
            // 
            // ID4Val
            // 
            this.ID4Val.Location = new System.Drawing.Point(516, 87);
            this.ID4Val.Name = "ID4Val";
            this.ID4Val.Size = new System.Drawing.Size(58, 20);
            this.ID4Val.TabIndex = 24;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(483, 116);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(27, 13);
            this.Label5.TabIndex = 27;
            this.Label5.Text = "ID5:";
            // 
            // ID5Val
            // 
            this.ID5Val.Location = new System.Drawing.Point(516, 113);
            this.ID5Val.Name = "ID5Val";
            this.ID5Val.Size = new System.Drawing.Size(58, 20);
            this.ID5Val.TabIndex = 26;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(575, 116);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(33, 13);
            this.Label6.TabIndex = 37;
            this.Label6.Text = "ID10:";
            // 
            // ID10Val
            // 
            this.ID10Val.Location = new System.Drawing.Point(608, 113);
            this.ID10Val.Name = "ID10Val";
            this.ID10Val.Size = new System.Drawing.Size(58, 20);
            this.ID10Val.TabIndex = 36;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(575, 90);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(27, 13);
            this.Label7.TabIndex = 35;
            this.Label7.Text = "ID9:";
            // 
            // ID9Val
            // 
            this.ID9Val.Location = new System.Drawing.Point(608, 87);
            this.ID9Val.Name = "ID9Val";
            this.ID9Val.Size = new System.Drawing.Size(58, 20);
            this.ID9Val.TabIndex = 34;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(575, 64);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(27, 13);
            this.Label8.TabIndex = 33;
            this.Label8.Text = "ID8:";
            // 
            // ID8Val
            // 
            this.ID8Val.Location = new System.Drawing.Point(608, 61);
            this.ID8Val.Name = "ID8Val";
            this.ID8Val.Size = new System.Drawing.Size(58, 20);
            this.ID8Val.TabIndex = 32;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(575, 38);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(27, 13);
            this.Label9.TabIndex = 31;
            this.Label9.Text = "ID7:";
            // 
            // ID7Val
            // 
            this.ID7Val.Location = new System.Drawing.Point(608, 35);
            this.ID7Val.Name = "ID7Val";
            this.ID7Val.Size = new System.Drawing.Size(58, 20);
            this.ID7Val.TabIndex = 30;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(575, 12);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(27, 13);
            this.Label10.TabIndex = 29;
            this.Label10.Text = "ID6:";
            // 
            // ID6Val
            // 
            this.ID6Val.Location = new System.Drawing.Point(608, 9);
            this.ID6Val.Name = "ID6Val";
            this.ID6Val.Size = new System.Drawing.Size(58, 20);
            this.ID6Val.TabIndex = 28;
            // 
            // F11Val
            // 
            this.F11Val.Location = new System.Drawing.Point(36, 16);
            this.F11Val.Name = "F11Val";
            this.F11Val.Size = new System.Drawing.Size(58, 20);
            this.F11Val.TabIndex = 38;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(8, 19);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(22, 13);
            this.Label11.TabIndex = 39;
            this.Label11.Text = "F1:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(8, 41);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(22, 13);
            this.Label12.TabIndex = 41;
            this.Label12.Text = "F2:";
            // 
            // F12Val
            // 
            this.F12Val.Location = new System.Drawing.Point(36, 38);
            this.F12Val.Name = "F12Val";
            this.F12Val.Size = new System.Drawing.Size(58, 20);
            this.F12Val.TabIndex = 40;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(8, 64);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(22, 13);
            this.Label13.TabIndex = 43;
            this.Label13.Text = "F3:";
            // 
            // F13Val
            // 
            this.F13Val.Location = new System.Drawing.Point(36, 61);
            this.F13Val.Name = "F13Val";
            this.F13Val.Size = new System.Drawing.Size(58, 20);
            this.F13Val.TabIndex = 42;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(8, 88);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(22, 13);
            this.Label14.TabIndex = 45;
            this.Label14.Text = "F4:";
            // 
            // F14Val
            // 
            this.F14Val.Location = new System.Drawing.Point(36, 85);
            this.F14Val.Name = "F14Val";
            this.F14Val.Size = new System.Drawing.Size(58, 20);
            this.F14Val.TabIndex = 44;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.F41Val);
            this.GroupBox4.Controls.Add(this.Label23);
            this.GroupBox4.Controls.Add(this.Label24);
            this.GroupBox4.Controls.Add(this.F44Val);
            this.GroupBox4.Controls.Add(this.F42Val);
            this.GroupBox4.Controls.Add(this.Label25);
            this.GroupBox4.Controls.Add(this.Label26);
            this.GroupBox4.Controls.Add(this.F43Val);
            this.GroupBox4.Controls.Add(this.F31Val);
            this.GroupBox4.Controls.Add(this.Label19);
            this.GroupBox4.Controls.Add(this.Label20);
            this.GroupBox4.Controls.Add(this.F34Val);
            this.GroupBox4.Controls.Add(this.F32Val);
            this.GroupBox4.Controls.Add(this.Label21);
            this.GroupBox4.Controls.Add(this.Label22);
            this.GroupBox4.Controls.Add(this.F33Val);
            this.GroupBox4.Controls.Add(this.F21Val);
            this.GroupBox4.Controls.Add(this.Label15);
            this.GroupBox4.Controls.Add(this.Label16);
            this.GroupBox4.Controls.Add(this.F24Val);
            this.GroupBox4.Controls.Add(this.F22Val);
            this.GroupBox4.Controls.Add(this.Label17);
            this.GroupBox4.Controls.Add(this.Label18);
            this.GroupBox4.Controls.Add(this.F23Val);
            this.GroupBox4.Controls.Add(this.F11Val);
            this.GroupBox4.Controls.Add(this.Label14);
            this.GroupBox4.Controls.Add(this.Label11);
            this.GroupBox4.Controls.Add(this.F14Val);
            this.GroupBox4.Controls.Add(this.F12Val);
            this.GroupBox4.Controls.Add(this.Label13);
            this.GroupBox4.Controls.Add(this.Label12);
            this.GroupBox4.Controls.Add(this.F13Val);
            this.GroupBox4.Location = new System.Drawing.Point(672, 9);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(200, 214);
            this.GroupBox4.TabIndex = 46;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Physics";
            // 
            // F41Val
            // 
            this.F41Val.Location = new System.Drawing.Point(136, 114);
            this.F41Val.Name = "F41Val";
            this.F41Val.Size = new System.Drawing.Size(58, 20);
            this.F41Val.TabIndex = 62;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(108, 186);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(28, 13);
            this.Label23.TabIndex = 69;
            this.Label23.Text = "F16:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(108, 117);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(28, 13);
            this.Label24.TabIndex = 63;
            this.Label24.Text = "F13:";
            // 
            // F44Val
            // 
            this.F44Val.Location = new System.Drawing.Point(136, 183);
            this.F44Val.Name = "F44Val";
            this.F44Val.Size = new System.Drawing.Size(58, 20);
            this.F44Val.TabIndex = 68;
            // 
            // F42Val
            // 
            this.F42Val.Location = new System.Drawing.Point(136, 136);
            this.F42Val.Name = "F42Val";
            this.F42Val.Size = new System.Drawing.Size(58, 20);
            this.F42Val.TabIndex = 64;
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(108, 162);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(28, 13);
            this.Label25.TabIndex = 67;
            this.Label25.Text = "F15:";
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(108, 140);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(28, 13);
            this.Label26.TabIndex = 65;
            this.Label26.Text = "F14:";
            // 
            // F43Val
            // 
            this.F43Val.Location = new System.Drawing.Point(136, 159);
            this.F43Val.Name = "F43Val";
            this.F43Val.Size = new System.Drawing.Size(58, 20);
            this.F43Val.TabIndex = 66;
            // 
            // F31Val
            // 
            this.F31Val.Location = new System.Drawing.Point(36, 111);
            this.F31Val.Name = "F31Val";
            this.F31Val.Size = new System.Drawing.Size(58, 20);
            this.F31Val.TabIndex = 54;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(8, 183);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(22, 13);
            this.Label19.TabIndex = 61;
            this.Label19.Text = "F8:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(8, 114);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(22, 13);
            this.Label20.TabIndex = 55;
            this.Label20.Text = "F5:";
            // 
            // F34Val
            // 
            this.F34Val.Location = new System.Drawing.Point(36, 180);
            this.F34Val.Name = "F34Val";
            this.F34Val.Size = new System.Drawing.Size(58, 20);
            this.F34Val.TabIndex = 60;
            // 
            // F32Val
            // 
            this.F32Val.Location = new System.Drawing.Point(36, 133);
            this.F32Val.Name = "F32Val";
            this.F32Val.Size = new System.Drawing.Size(58, 20);
            this.F32Val.TabIndex = 56;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(8, 159);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(22, 13);
            this.Label21.TabIndex = 59;
            this.Label21.Text = "F7:";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(8, 136);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(22, 13);
            this.Label22.TabIndex = 57;
            this.Label22.Text = "F6:";
            // 
            // F33Val
            // 
            this.F33Val.Location = new System.Drawing.Point(36, 156);
            this.F33Val.Name = "F33Val";
            this.F33Val.Size = new System.Drawing.Size(58, 20);
            this.F33Val.TabIndex = 58;
            // 
            // F21Val
            // 
            this.F21Val.Location = new System.Drawing.Point(136, 16);
            this.F21Val.Name = "F21Val";
            this.F21Val.Size = new System.Drawing.Size(58, 20);
            this.F21Val.TabIndex = 46;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(108, 88);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(28, 13);
            this.Label15.TabIndex = 53;
            this.Label15.Text = "F12:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(108, 19);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(22, 13);
            this.Label16.TabIndex = 47;
            this.Label16.Text = "F9:";
            // 
            // F24Val
            // 
            this.F24Val.Location = new System.Drawing.Point(136, 85);
            this.F24Val.Name = "F24Val";
            this.F24Val.Size = new System.Drawing.Size(58, 20);
            this.F24Val.TabIndex = 52;
            // 
            // F22Val
            // 
            this.F22Val.Location = new System.Drawing.Point(136, 38);
            this.F22Val.Name = "F22Val";
            this.F22Val.Size = new System.Drawing.Size(58, 20);
            this.F22Val.TabIndex = 48;
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(108, 64);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(28, 13);
            this.Label17.TabIndex = 51;
            this.Label17.Text = "F11:";
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(108, 41);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(28, 13);
            this.Label18.TabIndex = 49;
            this.Label18.Text = "F10:";
            // 
            // F23Val
            // 
            this.F23Val.Location = new System.Drawing.Point(136, 61);
            this.F23Val.Name = "F23Val";
            this.F23Val.Size = new System.Drawing.Size(58, 20);
            this.F23Val.TabIndex = 50;
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(484, 145);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(26, 13);
            this.Label27.TabIndex = 47;
            this.Label27.Text = "EI1:";
            // 
            // EI1Val
            // 
            this.EI1Val.Location = new System.Drawing.Point(516, 141);
            this.EI1Val.Name = "EI1Val";
            this.EI1Val.Size = new System.Drawing.Size(58, 20);
            this.EI1Val.TabIndex = 48;
            // 
            // EI2Val
            // 
            this.EI2Val.Location = new System.Drawing.Point(516, 165);
            this.EI2Val.Name = "EI2Val";
            this.EI2Val.Size = new System.Drawing.Size(58, 20);
            this.EI2Val.TabIndex = 50;
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(484, 169);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(26, 13);
            this.Label28.TabIndex = 49;
            this.Label28.Text = "EI2:";
            // 
            // EI3Val
            // 
            this.EI3Val.Location = new System.Drawing.Point(516, 189);
            this.EI3Val.Name = "EI3Val";
            this.EI3Val.Size = new System.Drawing.Size(58, 20);
            this.EI3Val.TabIndex = 52;
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(484, 193);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(26, 13);
            this.Label29.TabIndex = 51;
            this.Label29.Text = "EI3:";
            // 
            // EI6Val
            // 
            this.EI6Val.Location = new System.Drawing.Point(516, 263);
            this.EI6Val.Name = "EI6Val";
            this.EI6Val.Size = new System.Drawing.Size(58, 20);
            this.EI6Val.TabIndex = 58;
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Location = new System.Drawing.Point(484, 267);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(26, 13);
            this.Label30.TabIndex = 57;
            this.Label30.Text = "EI6:";
            // 
            // EI5Val
            // 
            this.EI5Val.Location = new System.Drawing.Point(516, 239);
            this.EI5Val.Name = "EI5Val";
            this.EI5Val.Size = new System.Drawing.Size(58, 20);
            this.EI5Val.TabIndex = 56;
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(484, 243);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(26, 13);
            this.Label31.TabIndex = 55;
            this.Label31.Text = "EI5:";
            // 
            // EI4Val
            // 
            this.EI4Val.Location = new System.Drawing.Point(516, 215);
            this.EI4Val.Name = "EI4Val";
            this.EI4Val.Size = new System.Drawing.Size(58, 20);
            this.EI4Val.TabIndex = 54;
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(484, 219);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(26, 13);
            this.Label32.TabIndex = 53;
            this.Label32.Text = "EI4:";
            // 
            // EI12Val
            // 
            this.EI12Val.Location = new System.Drawing.Point(608, 263);
            this.EI12Val.Name = "EI12Val";
            this.EI12Val.Size = new System.Drawing.Size(58, 20);
            this.EI12Val.TabIndex = 70;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(576, 267);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(32, 13);
            this.Label33.TabIndex = 69;
            this.Label33.Text = "EI12:";
            // 
            // EI11Val
            // 
            this.EI11Val.Location = new System.Drawing.Point(608, 239);
            this.EI11Val.Name = "EI11Val";
            this.EI11Val.Size = new System.Drawing.Size(58, 20);
            this.EI11Val.TabIndex = 68;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(576, 243);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(32, 13);
            this.Label34.TabIndex = 67;
            this.Label34.Text = "EI11:";
            // 
            // EI10Val
            // 
            this.EI10Val.Location = new System.Drawing.Point(608, 215);
            this.EI10Val.Name = "EI10Val";
            this.EI10Val.Size = new System.Drawing.Size(58, 20);
            this.EI10Val.TabIndex = 66;
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(576, 219);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(32, 13);
            this.Label35.TabIndex = 65;
            this.Label35.Text = "EI10:";
            // 
            // EI9Val
            // 
            this.EI9Val.Location = new System.Drawing.Point(608, 189);
            this.EI9Val.Name = "EI9Val";
            this.EI9Val.Size = new System.Drawing.Size(58, 20);
            this.EI9Val.TabIndex = 64;
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(576, 193);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(26, 13);
            this.Label36.TabIndex = 63;
            this.Label36.Text = "EI9:";
            // 
            // EI8Val
            // 
            this.EI8Val.Location = new System.Drawing.Point(608, 165);
            this.EI8Val.Name = "EI8Val";
            this.EI8Val.Size = new System.Drawing.Size(58, 20);
            this.EI8Val.TabIndex = 62;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Location = new System.Drawing.Point(576, 169);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(26, 13);
            this.Label37.TabIndex = 61;
            this.Label37.Text = "EI8:";
            // 
            // EI7Val
            // 
            this.EI7Val.Location = new System.Drawing.Point(608, 141);
            this.EI7Val.Name = "EI7Val";
            this.EI7Val.Size = new System.Drawing.Size(58, 20);
            this.EI7Val.TabIndex = 60;
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(576, 145);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(26, 13);
            this.Label38.TabIndex = 59;
            this.Label38.Text = "EI7:";
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.Flag48);
            this.GroupBox5.Controls.Add(this.Flag47);
            this.GroupBox5.Controls.Add(this.Flag46);
            this.GroupBox5.Controls.Add(this.Flag45);
            this.GroupBox5.Controls.Add(this.Flag44);
            this.GroupBox5.Controls.Add(this.Flag43);
            this.GroupBox5.Controls.Add(this.Flag42);
            this.GroupBox5.Controls.Add(this.Flag41);
            this.GroupBox5.Location = new System.Drawing.Point(397, 12);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(80, 202);
            this.GroupBox5.TabIndex = 18;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Flags4";
            // 
            // Flag48
            // 
            this.Flag48.AutoSize = true;
            this.Flag48.Location = new System.Drawing.Point(6, 180);
            this.Flag48.Name = "Flag48";
            this.Flag48.Size = new System.Drawing.Size(46, 17);
            this.Flag48.TabIndex = 7;
            this.Flag48.Text = "Unk";
            this.Flag48.UseVisualStyleBackColor = true;
            // 
            // Flag47
            // 
            this.Flag47.AutoSize = true;
            this.Flag47.Location = new System.Drawing.Point(6, 157);
            this.Flag47.Name = "Flag47";
            this.Flag47.Size = new System.Drawing.Size(46, 17);
            this.Flag47.TabIndex = 6;
            this.Flag47.Text = "Unk";
            this.Flag47.UseVisualStyleBackColor = true;
            // 
            // Flag46
            // 
            this.Flag46.AutoSize = true;
            this.Flag46.Location = new System.Drawing.Point(6, 134);
            this.Flag46.Name = "Flag46";
            this.Flag46.Size = new System.Drawing.Size(46, 17);
            this.Flag46.TabIndex = 5;
            this.Flag46.Text = "Unk";
            this.Flag46.UseVisualStyleBackColor = true;
            // 
            // Flag45
            // 
            this.Flag45.AutoSize = true;
            this.Flag45.Location = new System.Drawing.Point(6, 111);
            this.Flag45.Name = "Flag45";
            this.Flag45.Size = new System.Drawing.Size(46, 17);
            this.Flag45.TabIndex = 4;
            this.Flag45.Text = "Unk";
            this.Flag45.UseVisualStyleBackColor = true;
            // 
            // Flag44
            // 
            this.Flag44.AutoSize = true;
            this.Flag44.Location = new System.Drawing.Point(6, 88);
            this.Flag44.Name = "Flag44";
            this.Flag44.Size = new System.Drawing.Size(63, 17);
            this.Flag44.TabIndex = 3;
            this.Flag44.Text = "BlockAI";
            this.Flag44.UseVisualStyleBackColor = true;
            // 
            // Flag43
            // 
            this.Flag43.AutoSize = true;
            this.Flag43.Location = new System.Drawing.Point(6, 65);
            this.Flag43.Name = "Flag43";
            this.Flag43.Size = new System.Drawing.Size(82, 17);
            this.Flag43.TabIndex = 2;
            this.Flag43.Text = "ElectrDeath";
            this.Flag43.UseVisualStyleBackColor = true;
            // 
            // Flag42
            // 
            this.Flag42.AutoSize = true;
            this.Flag42.Location = new System.Drawing.Point(6, 42);
            this.Flag42.Name = "Flag42";
            this.Flag42.Size = new System.Drawing.Size(73, 17);
            this.Flag42.TabIndex = 1;
            this.Flag42.Text = "RigidSlipp";
            this.Flag42.UseVisualStyleBackColor = true;
            // 
            // Flag41
            // 
            this.Flag41.AutoSize = true;
            this.Flag41.Location = new System.Drawing.Point(6, 19);
            this.Flag41.Name = "Flag41";
            this.Flag41.Size = new System.Drawing.Size(66, 17);
            this.Flag41.TabIndex = 0;
            this.Flag41.Text = "ChBlock";
            this.Flag41.UseVisualStyleBackColor = true;
            // 
            // SurfaceBehaviorEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 297);
            this.Controls.Add(this.GroupBox5);
            this.Controls.Add(this.EI12Val);
            this.Controls.Add(this.Label33);
            this.Controls.Add(this.EI11Val);
            this.Controls.Add(this.Label34);
            this.Controls.Add(this.EI10Val);
            this.Controls.Add(this.Label35);
            this.Controls.Add(this.EI9Val);
            this.Controls.Add(this.Label36);
            this.Controls.Add(this.EI8Val);
            this.Controls.Add(this.Label37);
            this.Controls.Add(this.EI7Val);
            this.Controls.Add(this.Label38);
            this.Controls.Add(this.EI6Val);
            this.Controls.Add(this.Label30);
            this.Controls.Add(this.EI5Val);
            this.Controls.Add(this.Label31);
            this.Controls.Add(this.EI4Val);
            this.Controls.Add(this.Label32);
            this.Controls.Add(this.EI3Val);
            this.Controls.Add(this.Label29);
            this.Controls.Add(this.EI2Val);
            this.Controls.Add(this.Label28);
            this.Controls.Add(this.EI1Val);
            this.Controls.Add(this.Label27);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.ID10Val);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.ID9Val);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.ID8Val);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.ID7Val);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.ID6Val);
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
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Revert);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.SBTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SurfaceBehaviorEditor";
            this.Text = "SurfaceBehavior Editor";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button Revert;
        private Button Apply;
        public TreeView SBTree;
        private GroupBox GroupBox1;
        private CheckBox Flag18;
        private CheckBox Flag17;
        private CheckBox Flag16;
        private CheckBox Flag15;
        private CheckBox Flag14;
        private CheckBox Flag13;
        private CheckBox Flag12;
        private CheckBox Flag11;
        private GroupBox GroupBox2;
        private CheckBox Flag28;
        private CheckBox Flag27;
        private CheckBox Flag26;
        private CheckBox Flag25;
        private CheckBox Flag24;
        private CheckBox Flag23;
        private CheckBox Flag22;
        private CheckBox Flag21;
        private GroupBox GroupBox3;
        private CheckBox Flag38;
        private CheckBox Flag37;
        private CheckBox Flag36;
        private CheckBox Flag35;
        private CheckBox Flag34;
        private CheckBox Flag33;
        private CheckBox Flag32;
        private CheckBox Flag31;
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
        private Label Label6;
        private TextBox ID10Val;
        private Label Label7;
        private TextBox ID9Val;
        private Label Label8;
        private TextBox ID8Val;
        private Label Label9;
        private TextBox ID7Val;
        private Label Label10;
        private TextBox ID6Val;
        private TextBox F11Val;
        private Label Label11;
        private Label Label12;
        private TextBox F12Val;
        private Label Label13;
        private TextBox F13Val;
        private Label Label14;
        private TextBox F14Val;
        private GroupBox GroupBox4;
        private TextBox F41Val;
        private Label Label23;
        private Label Label24;
        private TextBox F44Val;
        private TextBox F42Val;
        private Label Label25;
        private Label Label26;
        private TextBox F43Val;
        private TextBox F31Val;
        private Label Label19;
        private Label Label20;
        private TextBox F34Val;
        private TextBox F32Val;
        private Label Label21;
        private Label Label22;
        private TextBox F33Val;
        private TextBox F21Val;
        private Label Label15;
        private Label Label16;
        private TextBox F24Val;
        private TextBox F22Val;
        private Label Label17;
        private Label Label18;
        private TextBox F23Val;
        private Label Label27;
        private TextBox EI1Val;
        private TextBox EI2Val;
        private Label Label28;
        private TextBox EI3Val;
        private Label Label29;
        private TextBox EI6Val;
        private Label Label30;
        private TextBox EI5Val;
        private Label Label31;
        private TextBox EI4Val;
        private Label Label32;
        private TextBox EI12Val;
        private Label Label33;
        private TextBox EI11Val;
        private Label Label34;
        private TextBox EI10Val;
        private Label Label35;
        private TextBox EI9Val;
        private Label Label36;
        private TextBox EI8Val;
        private Label Label37;
        private TextBox EI7Val;
        private Label Label38;
        private GroupBox GroupBox5;
        private CheckBox Flag48;
        private CheckBox Flag47;
        private CheckBox Flag46;
        private CheckBox Flag45;
        private CheckBox Flag44;
        private CheckBox Flag43;
        private CheckBox Flag42;
        private CheckBox Flag41;
    }
}
