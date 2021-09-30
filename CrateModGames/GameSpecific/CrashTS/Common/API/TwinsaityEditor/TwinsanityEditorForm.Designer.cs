using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

namespace TwinsaityEditor
{
    partial class TwinsanityEditorForm : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwinsanityEditorForm));
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.NewRM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ElfPatcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.E3ConverterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefresLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GeoDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GeoDataVisualiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearGeoDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripTextBox();
            this.ExportSingleOBJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportOBJLayeredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TriggerTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PSMWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MHWorkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WAVToTwinSNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenLevel = new System.Windows.Forms.OpenFileDialog();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Button14 = new System.Windows.Forms.Button();
            this.Button11 = new System.Windows.Forms.Button();
            this.Button10 = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.Button8 = new System.Windows.Forms.Button();
            this.Button7 = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Summary = new System.Windows.Forms.TextBox();
            this.SaveLevel = new System.Windows.Forms.SaveFileDialog();
            this.ExtractItem = new System.Windows.Forms.SaveFileDialog();
            this.ExtractBunch = new System.Windows.Forms.FolderBrowserDialog();
            this.OpenItem = new System.Windows.Forms.OpenFileDialog();
            this.AddItem = new System.Windows.Forms.OpenFileDialog();
            this.Button13 = new System.Windows.Forms.Button();
            this.E3Converter = new System.Windows.Forms.OpenFileDialog();
            this.ObjSingleSave = new System.Windows.Forms.SaveFileDialog();
            this.OpenObj = new System.Windows.Forms.OpenFileDialog();
            this.ImportGO = new System.Windows.Forms.FolderBrowserDialog();
            this.SaveWave = new System.Windows.Forms.SaveFileDialog();
            this.OpenWave = new System.Windows.Forms.OpenFileDialog();
            this.OpenMusic = new System.Windows.Forms.OpenFileDialog();
            this.MenuStrip1.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(879, 24);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ToolStripSeparator2,
            this.NewRM2ToolStripMenuItem,
            this.NewSM2ToolStripMenuItem,
            this.ToolStripSeparator1,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(122, 6);
            // 
            // NewRM2ToolStripMenuItem
            // 
            this.NewRM2ToolStripMenuItem.Name = "NewRM2ToolStripMenuItem";
            this.NewRM2ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.NewRM2ToolStripMenuItem.Text = "New RM2";
            this.NewRM2ToolStripMenuItem.Click += new System.EventHandler(this.NewRM2ToolStripMenuItem_Click);
            // 
            // NewSM2ToolStripMenuItem
            // 
            this.NewSM2ToolStripMenuItem.Name = "NewSM2ToolStripMenuItem";
            this.NewSM2ToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.NewSM2ToolStripMenuItem.Text = "New SM2";
            this.NewSM2ToolStripMenuItem.Click += new System.EventHandler(this.NewSM2ToolStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.SaveAsToolStripMenuItem.Text = "Save As...";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(122, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElfPatcherToolStripMenuItem,
            this.E3ConverterToolStripMenuItem,
            this.RefresLibraryToolStripMenuItem,
            this.GeoDataToolStripMenuItem,
            this.GraphicsToolStripMenuItem,
            this.SoundToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "Tools";
            // 
            // ElfPatcherToolStripMenuItem
            // 
            this.ElfPatcherToolStripMenuItem.Name = "ElfPatcherToolStripMenuItem";
            this.ElfPatcherToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ElfPatcherToolStripMenuItem.Text = "Elf Patcher";
            this.ElfPatcherToolStripMenuItem.Click += new System.EventHandler(this.ElfPatcherToolStripMenuItem_Click);
            // 
            // E3ConverterToolStripMenuItem
            // 
            this.E3ConverterToolStripMenuItem.Name = "E3ConverterToolStripMenuItem";
            this.E3ConverterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.E3ConverterToolStripMenuItem.Text = "E3 Converter";
            this.E3ConverterToolStripMenuItem.Click += new System.EventHandler(this.E3ConverterToolStripMenuItem_Click);
            // 
            // RefresLibraryToolStripMenuItem
            // 
            this.RefresLibraryToolStripMenuItem.Name = "RefresLibraryToolStripMenuItem";
            this.RefresLibraryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.RefresLibraryToolStripMenuItem.Text = "Refresh Library";
            this.RefresLibraryToolStripMenuItem.Click += new System.EventHandler(this.RefresLibraryToolStripMenuItem_Click);
            // 
            // GeoDataToolStripMenuItem
            // 
            this.GeoDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeoDataVisualiserToolStripMenuItem,
            this.ClearGeoDataToolStripMenuItem,
            this.AddLayerToolStripMenuItem,
            this.ToolStripMenuItem3,
            this.ExportSingleOBJToolStripMenuItem,
            this.ExportOBJLayeredToolStripMenuItem,
            this.TriggerTreeToolStripMenuItem});
            this.GeoDataToolStripMenuItem.Name = "GeoDataToolStripMenuItem";
            this.GeoDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.GeoDataToolStripMenuItem.Text = "GeoData";
            // 
            // GeoDataVisualiserToolStripMenuItem
            // 
            this.GeoDataVisualiserToolStripMenuItem.Name = "GeoDataVisualiserToolStripMenuItem";
            this.GeoDataVisualiserToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.GeoDataVisualiserToolStripMenuItem.Text = "GeoData Visualizer";
            this.GeoDataVisualiserToolStripMenuItem.Click += new System.EventHandler(this.GeoDataVisualiserToolStripMenuItem_Click_1);
            // 
            // ClearGeoDataToolStripMenuItem
            // 
            this.ClearGeoDataToolStripMenuItem.Name = "ClearGeoDataToolStripMenuItem";
            this.ClearGeoDataToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ClearGeoDataToolStripMenuItem.Text = "Clear GeoData";
            this.ClearGeoDataToolStripMenuItem.Click += new System.EventHandler(this.ClearGeoDataToolStripMenuItem_Click);
            // 
            // AddLayerToolStripMenuItem
            // 
            this.AddLayerToolStripMenuItem.Name = "AddLayerToolStripMenuItem";
            this.AddLayerToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.AddLayerToolStripMenuItem.Text = "Add Layer SB ID:";
            this.AddLayerToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(152, 23);
            this.ToolStripMenuItem3.Text = "0";
            // 
            // ExportSingleOBJToolStripMenuItem
            // 
            this.ExportSingleOBJToolStripMenuItem.Name = "ExportSingleOBJToolStripMenuItem";
            this.ExportSingleOBJToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ExportSingleOBJToolStripMenuItem.Text = "Export OBJ";
            this.ExportSingleOBJToolStripMenuItem.Click += new System.EventHandler(this.ExportSingleOBJToolStripMenuItem_Click);
            // 
            // ExportOBJLayeredToolStripMenuItem
            // 
            this.ExportOBJLayeredToolStripMenuItem.Name = "ExportOBJLayeredToolStripMenuItem";
            this.ExportOBJLayeredToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ExportOBJLayeredToolStripMenuItem.Text = "Export OBJ Layered";
            this.ExportOBJLayeredToolStripMenuItem.Click += new System.EventHandler(this.ExportOBJLayeredToolStripMenuItem_Click);
            // 
            // TriggerTreeToolStripMenuItem
            // 
            this.TriggerTreeToolStripMenuItem.Name = "TriggerTreeToolStripMenuItem";
            this.TriggerTreeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.TriggerTreeToolStripMenuItem.Text = "TriggerTree";
            this.TriggerTreeToolStripMenuItem.Click += new System.EventHandler(this.TriggerTreeToolStripMenuItem_Click);
            // 
            // GraphicsToolStripMenuItem
            // 
            this.GraphicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TexturesToolStripMenuItem,
            this.PSMWorkerToolStripMenuItem,
            this.ImportTextureToolStripMenuItem,
            this.ExportModelToolStripMenuItem,
            this.ImportModelToolStripMenuItem});
            this.GraphicsToolStripMenuItem.Name = "GraphicsToolStripMenuItem";
            this.GraphicsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.GraphicsToolStripMenuItem.Text = "Graphics";
            // 
            // TexturesToolStripMenuItem
            // 
            this.TexturesToolStripMenuItem.Name = "TexturesToolStripMenuItem";
            this.TexturesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.TexturesToolStripMenuItem.Text = "View Selected";
            this.TexturesToolStripMenuItem.Click += new System.EventHandler(this.TexturesToolStripMenuItem_Click);
            // 
            // PSMWorkerToolStripMenuItem
            // 
            this.PSMWorkerToolStripMenuItem.Name = "PSMWorkerToolStripMenuItem";
            this.PSMWorkerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PSMWorkerToolStripMenuItem.Text = "PSM Worker";
            this.PSMWorkerToolStripMenuItem.Click += new System.EventHandler(this.PSMWorkerToolStripMenuItem_Click);
            // 
            // ImportTextureToolStripMenuItem
            // 
            this.ImportTextureToolStripMenuItem.Name = "ImportTextureToolStripMenuItem";
            this.ImportTextureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ImportTextureToolStripMenuItem.Text = "Import Texture";
            this.ImportTextureToolStripMenuItem.Click += new System.EventHandler(this.ImportTextureToolStripMenuItem_Click);
            // 
            // ExportModelToolStripMenuItem
            // 
            this.ExportModelToolStripMenuItem.Name = "ExportModelToolStripMenuItem";
            this.ExportModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExportModelToolStripMenuItem.Text = "Export Model";
            this.ExportModelToolStripMenuItem.Click += new System.EventHandler(this.ExportModelToolStripMenuItem_Click);
            // 
            // ImportModelToolStripMenuItem
            // 
            this.ImportModelToolStripMenuItem.Name = "ImportModelToolStripMenuItem";
            this.ImportModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ImportModelToolStripMenuItem.Text = "Import Model";
            this.ImportModelToolStripMenuItem.Click += new System.EventHandler(this.ImportModelToolStripMenuItem_Click);
            // 
            // SoundToolStripMenuItem
            // 
            this.SoundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ManagerToolStripMenuItem,
            this.MHWorkerToolStripMenuItem,
            this.WAVToTwinSNDToolStripMenuItem,
            this.DebugToolStripMenuItem});
            this.SoundToolStripMenuItem.Name = "SoundToolStripMenuItem";
            this.SoundToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SoundToolStripMenuItem.Text = "Sound";
            // 
            // ManagerToolStripMenuItem
            // 
            this.ManagerToolStripMenuItem.Name = "ManagerToolStripMenuItem";
            this.ManagerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ManagerToolStripMenuItem.Text = "Play";
            // 
            // MHWorkerToolStripMenuItem
            // 
            this.MHWorkerToolStripMenuItem.Name = "MHWorkerToolStripMenuItem";
            this.MHWorkerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MHWorkerToolStripMenuItem.Text = "Convert to WAV";
            // 
            // WAVToTwinSNDToolStripMenuItem
            // 
            this.WAVToTwinSNDToolStripMenuItem.Name = "WAVToTwinSNDToolStripMenuItem";
            this.WAVToTwinSNDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.WAVToTwinSNDToolStripMenuItem.Text = "WAV to TwinSND";
            // 
            // DebugToolStripMenuItem
            // 
            this.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem";
            this.DebugToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DebugToolStripMenuItem.Text = "MH Worker";
            this.DebugToolStripMenuItem.Click += new System.EventHandler(this.DebugToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // OpenLevel
            // 
            this.OpenLevel.Filter = "RM2/RMX|*.rm*|SM2/SMX|*.sm*|Demo RM Files|*.rm*";
            // 
            // TreeView1
            // 
            this.TreeView1.Location = new System.Drawing.Point(13, 28);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(426, 375);
            this.TreeView1.TabIndex = 1;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Button14);
            this.GroupBox1.Controls.Add(this.Button11);
            this.GroupBox1.Controls.Add(this.Button10);
            this.GroupBox1.Controls.Add(this.Button9);
            this.GroupBox1.Controls.Add(this.Button8);
            this.GroupBox1.Controls.Add(this.Button7);
            this.GroupBox1.Controls.Add(this.Button6);
            this.GroupBox1.Controls.Add(this.Button5);
            this.GroupBox1.Controls.Add(this.Button4);
            this.GroupBox1.Controls.Add(this.Button3);
            this.GroupBox1.Controls.Add(this.Button2);
            this.GroupBox1.Controls.Add(this.Button1);
            this.GroupBox1.Location = new System.Drawing.Point(782, 27);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(85, 376);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Toolbar";
            // 
            // Button14
            // 
            this.Button14.Location = new System.Drawing.Point(4, 342);
            this.Button14.Name = "Button14";
            this.Button14.Size = new System.Drawing.Size(75, 23);
            this.Button14.TabIndex = 11;
            this.Button14.Text = "Scripting";
            this.Button14.UseVisualStyleBackColor = true;
            this.Button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // Button11
            // 
            this.Button11.Location = new System.Drawing.Point(4, 138);
            this.Button11.Name = "Button11";
            this.Button11.Size = new System.Drawing.Size(75, 23);
            this.Button11.TabIndex = 10;
            this.Button11.Text = "Extract";
            this.Button11.UseVisualStyleBackColor = true;
            this.Button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // Button10
            // 
            this.Button10.Location = new System.Drawing.Point(4, 79);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(75, 23);
            this.Button10.TabIndex = 9;
            this.Button10.Text = "Search";
            this.Button10.UseVisualStyleBackColor = true;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(4, 50);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(75, 23);
            this.Button9.TabIndex = 8;
            this.Button9.Text = "Export";
            this.Button9.UseVisualStyleBackColor = true;
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // Button8
            // 
            this.Button8.Location = new System.Drawing.Point(4, 313);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(75, 23);
            this.Button8.TabIndex = 7;
            this.Button8.Text = "Randomizer";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button7
            // 
            this.Button7.Location = new System.Drawing.Point(4, 284);
            this.Button7.Name = "Button7";
            this.Button7.Size = new System.Drawing.Size(75, 23);
            this.Button7.TabIndex = 6;
            this.Button7.Text = "Rep. Local";
            this.Button7.UseVisualStyleBackColor = true;
            this.Button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(4, 255);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(75, 23);
            this.Button6.TabIndex = 5;
            this.Button6.Text = "Import";
            this.Button6.UseVisualStyleBackColor = true;
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(4, 225);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(75, 23);
            this.Button5.TabIndex = 4;
            this.Button5.Text = "New";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(4, 196);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(75, 23);
            this.Button4.TabIndex = 3;
            this.Button4.Text = "Delete";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(4, 167);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(75, 23);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "Add";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(4, 108);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Replace";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Enabled = false;
            this.Button1.Location = new System.Drawing.Point(4, 19);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "HEXView";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Summary
            // 
            this.Summary.Location = new System.Drawing.Point(446, 28);
            this.Summary.MaxLength = 0;
            this.Summary.Multiline = true;
            this.Summary.Name = "Summary";
            this.Summary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Summary.Size = new System.Drawing.Size(330, 346);
            this.Summary.TabIndex = 3;
            this.Summary.WordWrap = false;
            // 
            // SaveLevel
            // 
            this.SaveLevel.Filter = "RM2|*.rm2|RMX|*.rmx|SM2|*.sm2|SMX|*.smx";
            // 
            // AddItem
            // 
            this.AddItem.Multiselect = true;
            // 
            // Button13
            // 
            this.Button13.Location = new System.Drawing.Point(446, 380);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(75, 23);
            this.Button13.TabIndex = 5;
            this.Button13.Text = "Edit";
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // E3Converter
            // 
            this.E3Converter.Filter = "E3GameObject|*.*|E3Instance|*.*|E3RM2|*.rm*|ReleaseGameObjec|*.*|ReleaseInstance|" +
    "*.*|ReleaseLevel|*.rm*";
            this.E3Converter.Multiselect = true;
            // 
            // ObjSingleSave
            // 
            this.ObjSingleSave.Filter = "Wavefront model(*.OBJ)|*.obj";
            // 
            // OpenObj
            // 
            this.OpenObj.Filter = "Wavefront model(*.OBJ)|*.obj";
            // 
            // SaveWave
            // 
            this.SaveWave.Filter = "WAVE|*.wav";
            // 
            // OpenWave
            // 
            this.OpenWave.Filter = "WAVE|*.wav";
            this.OpenWave.Multiselect = true;
            // 
            // OpenMusic
            // 
            this.OpenMusic.Filter = "Music Header (*.MH)|*.MH";
            // 
            // TwinsanityEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 408);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Summary);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.MenuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "TwinsanityEditorForm";
            this.Text = "Twinsaity Editor by Neo_Kesha";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenLevel;
        public System.Windows.Forms.TreeView TreeView1;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.TextBox Summary;
        private System.Windows.Forms.Button Button11;
        private System.Windows.Forms.Button Button10;
        private System.Windows.Forms.Button Button9;
        private System.Windows.Forms.Button Button8;
        private System.Windows.Forms.Button Button7;
        private System.Windows.Forms.Button Button6;
        private System.Windows.Forms.Button Button5;
        private System.Windows.Forms.Button Button4;
        private System.Windows.Forms.Button Button3;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.SaveFileDialog SaveLevel;
        private System.Windows.Forms.SaveFileDialog ExtractItem;
        private System.Windows.Forms.FolderBrowserDialog ExtractBunch;
        private System.Windows.Forms.OpenFileDialog OpenItem;
        private System.Windows.Forms.OpenFileDialog AddItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Button Button13;
        private System.Windows.Forms.Button Button14;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ElfPatcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem NewRM2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewSM2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem E3ConverterToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog E3Converter;
        private System.Windows.Forms.ToolStripMenuItem RefresLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GeoDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GeoDataVisualiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearGeoDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportSingleOBJToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog ObjSingleSave;
        private System.Windows.Forms.ToolStripMenuItem ExportOBJLayeredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddLayerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenObj;
        private System.Windows.Forms.ToolStripTextBox ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem GraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TexturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MHWorkerToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog ImportGO;
        private System.Windows.Forms.SaveFileDialog SaveWave;
        private System.Windows.Forms.ToolStripMenuItem WAVToTwinSNDToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenWave;
        private System.Windows.Forms.ToolStripMenuItem DebugToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenMusic;
        private System.Windows.Forms.ToolStripMenuItem TriggerTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PSMWorkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportModelToolStripMenuItem;
    }
}
