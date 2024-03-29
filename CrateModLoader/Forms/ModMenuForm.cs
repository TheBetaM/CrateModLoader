﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CrateModLoader.ModProperties;
using CrateModLoader.ModProperties.GUI;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    public partial class ModMenuForm : Form
    {

        private Modder mod;
        private Game Game;
        private ModLoaderForm parentForm = null;
        private List<ModPropertyGUI_Base> PropGUIs;

        private ModPropertyGUI_Base GetExtension(object prop)
        {
            if (prop is ModPropBool)
                return new ModPropBoolGUI((ModPropBool)prop);
            else if (prop is ModPropFloat)
                return new ModPropFloatGUI((ModPropFloat)prop);
            else if (prop is ModPropFloatArray)
                return new ModPropFloatArrayGUI((ModPropFloatArray)prop);
            else if (prop is ModPropInt)
                return new ModPropIntGUI((ModPropInt)prop);
            else if (prop is ModPropNamedFloatArray)
                return new ModPropNamedFloatArrayGUI((ModPropNamedFloatArray)prop);
            else if (prop is ModPropNamedFloatArray2)
                return new ModPropNamedFloatArray2GUI((ModPropNamedFloatArray2)prop);
            else if (prop is ModPropNamedUIntArray)
                return new ModPropNamedUIntArrayGUI((ModPropNamedUIntArray)prop);
            else if (prop is ModPropOption)
                return new ModPropOptionGUI((ModPropOption)prop);
            else if (prop is ModPropString)
                return new ModPropStringGUI((ModPropString)prop);
            else if (prop is ModPropUInt)
                return new ModPropUIntGUI((ModPropUInt)prop);
            else if (prop is ModPropExternalResourceBase)
                return new ModPropExternalResourceGUI((ModPropExternalResourceBase)prop);
            else if (prop is ModPropColor)
                return new ModPropColorGUI((ModPropColor)prop);
            else
                return null;
        }

        public ModMenuForm(ModLoaderForm parent, Modder modder, Game g)
        {
            InitializeComponent();

            button1.Text = ModLoaderText.ModMenu_Button_Confirm;
            button2.Text = ModLoaderText.ModMenu_Button_SaveAs;
            button3.Text = ModLoaderText.ModMenu_Button_Load;
            button4.Text = ModLoaderText.ModMenu_Button_ResetToDefault;
            button5.Text = ModLoaderText.ModMenu_Button_Publish;
            button_modbit.Text = ModLoaderText.ModMenu_Label_ModBit;
            Text = ModLoaderText.ModMenuTitle;

            mod = modder;
            Game = g;
            parentForm = parent;

            /*
            if (parentForm == null)
            {
                button2.Enabled = false;
            }
            */

            //todo: generate Mod Bit

            GenerateUI();
            
        }

        void GenerateUI()
        {
            PropGUIs = new List<ModPropertyGUI_Base>();
            label1.Text = "";

            int initOffset = 0;
            int offset = 0;
            int initRowSize = 25;

            int itemsPerPage = 45; // to reduce lag

            SortedDictionary<int, string> Pages = new SortedDictionary<int, string>();
            foreach (ModPropertyBase prop in mod.Props)
            {
                if (!Pages.ContainsKey((int)prop.Category))
                {
                    if (Game.PropertyCategories != null && Game.PropertyCategories.ContainsKey((int)prop.Category))
                    {
                        Pages.Add((int)prop.Category, Game.PropertyCategories[(int)prop.Category]);
                    }
                    else
                    {
                        Pages.Add((int)prop.Category, string.Format("{0} {1}", ModLoaderText.ModMenuPage, prop.Category + 1));
                    }
                }
            }

            tabControl1.TabPages.Clear();

            int curItem = 0;
            int curPage = 1;
            TableLayoutPanel tableLayout = null;
            ConsoleMode Console = mod.ConsolePipeline.Metadata.Console;
            RegionType Region = mod.GameRegion.Region;

            foreach (KeyValuePair<int, string> pair in Pages)
            {
                tabControl1.TabPages.Add(pair.Value);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = true; //auto scroll causes massive cpu usage and choppy visuals with lots of elements

                curItem = 0;
                curPage = 1;
                tableLayout = null;

                foreach (ModPropertyBase propbase in mod.Props)
                {
                    if (!propbase.Allowed(Console, Region))
                    {
                        continue;
                    }
                    if (curItem == itemsPerPage)
                    {
                        if (curPage == 1)
                        {
                            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Text = string.Format("{0} {1} {2}", tabControl1.TabPages[tabControl1.TabPages.Count - 1].Text, ModLoaderText.ModMenuPage, curPage);
                        }
                        curPage++;
                        tabControl1.TabPages.Add(string.Format("{0} {1} {2}", pair.Value, ModLoaderText.ModMenuPage, curPage));
                        tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = true;

                        curItem = 0;
                        offset = initOffset;
                    }
                    if (propbase.Category == pair.Key)
                    {
                        if (curItem == 0)
                        {
                            if (tableLayout != null)
                            {
                                int newSize = -initRowSize;
                                foreach (RowStyle row in tableLayout.RowStyles)
                                {
                                    newSize += (int)row.Height;
                                }
                                tableLayout.Size = new Size(tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width, newSize);
                            }

                            tableLayout = new TableLayoutPanel();
                            tableLayout.Parent = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                            //tableLayout.Dock = DockStyle.Fill;

                            tableLayout.Location = new Point(0, 0);
                            tableLayout.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                            tableLayout.AutoSize = false;
                            tableLayout.Size = new Size(tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width, 300);

                            tableLayout.RowCount = 1;
                            tableLayout.ColumnCount = 1;
                            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, initRowSize));

                        }

                        //ModPropertyGUI_Base prop = new ModPropertyGUI<ModPropertyBase>(propbase);
                        ModPropertyGUI_Base prop = GetExtension(propbase);
                        prop.GenerateUI(tableLayout, ref offset);
                        tableLayout.RowCount++;
                        tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, initRowSize));
                        offset += 1;
                        curItem++;
                        PropGUIs.Add(prop);
                    }
                }

                if (tableLayout != null)
                {
                    int newSize = -initRowSize;
                    foreach (RowStyle row in tableLayout.RowStyles)
                    {
                        newSize += (int)row.Height;
                    }
                    tableLayout.Size = new Size(tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width, newSize);
                }

                offset = initOffset;
            }
        }

        void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (sender is TabPage page)
            {
                page.VerticalScroll.Value = e.NewValue;
            }
            else if (sender is VScrollBar vs)
            {
                TabPage tpage = (TabPage)vs.Parent;
                tpage.VerticalScroll.Value = e.NewValue;
            }
        }

        public void UpdateDescLabel(string desc)
        {
            label1.Text = desc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;

            if (parentForm != null)
            {
                parentForm.UpdateOptionList();

                bool HasChanged = false;

                foreach (ModPropertyBase prop in mod.Props)
                {
                    if (prop.HasChanged)
                    {
                        HasChanged = true;
                        break;
                    }
                }

                parentForm.ModProgram.UpdateModMenuChangedState(HasChanged);
            }
        }

        private void ModMenuForm_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ModPropertyBase prop in mod.Props)
            {
                prop.ResetToDefault();
            }
            foreach (ModPropertyGUI_Base prop in PropGUIs)
            {
                prop.UpdateUI();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = ModLoaderGlobals.ModDirectory;
            openFileDialog1.Filter = string.Format("{0} (*.zip; *.txt)|*.zip;*.txt", ModLoaderText.ModMenuLoad_FileTypes);
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ModCrates.LoadSettingsFromFile(mod, openFileDialog1.FileName);

                foreach (ModPropertyGUI_Base prop in PropGUIs)
                {
                    prop.UpdateUI();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = ModLoaderGlobals.ModDirectory;
            saveFileDialog1.Filter = string.Format("{0} (*.zip)|*.zip|{1} (*.txt)|*.txt|{2} (*.txt)|*.txt", ModLoaderText.ModMenuSaveAs_ModCrate, ModLoaderText.ModMenuSaveAs_SettingFile, ModLoaderText.ModMenuSaveAs_SettingFileFull);
            saveFileDialog1.FileName = "";


            bool HasChanged = false;

            foreach (ModPropertyBase prop in mod.Props)
            {
                if (prop.HasChanged)
                {
                    HasChanged = true;
                    break;
                }
            }

            if (!HasChanged)
            {
                MessageBox.Show(ModLoaderText.ModMenuSaveAs_NoSettingsChanged);
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FilterIndex == 1)
                {
                    ModCrateMakerForm modMenu = new ModCrateMakerForm(mod, Game, saveFileDialog1.FileName);

                    modMenu.Owner = this;
                    modMenu.Show();
                }
                else if (saveFileDialog1.FilterIndex == 2)
                {
                    ModCrates.SaveSettingsToFile(mod, saveFileDialog1.FileName, false);
                }
                else if (saveFileDialog1.FilterIndex == 3)
                {
                    ModCrates.SaveSettingsToFile(mod, saveFileDialog1.FileName, true);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // publish button
        }

        private void button_copymodbit_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox_modbit.Text, TextDataFormat.Text);
        }

        private void button_modbit_Click(object sender, EventArgs e)
        {
            // todo: input Mod Bit
        }
    }
}
