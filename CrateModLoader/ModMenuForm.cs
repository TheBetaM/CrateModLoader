using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrateModLoader.ModProperties;
using CrateModLoader.Resources.Text;

namespace CrateModLoader
{
    public partial class ModMenuForm : Form
    {

        private Modder mod;

        public ModMenuForm(Modder modder)
        {
            InitializeComponent();

            mod = modder;

            GenerateUI();
            
        }

        void GenerateUI()
        {
            label1.Text = "";

            int initOffset = 10;
            int offset = initOffset;

            int itemsPerPage = 40; // to reduce lag

            SortedDictionary<int, string> Pages = new SortedDictionary<int, string>();
            foreach (ModPropertyBase prop in mod.Props)
            {
                if (!Pages.ContainsKey((int)prop.Category))
                {
                    if (mod.PropCategories.ContainsKey((int)prop.Category))
                    {
                        Pages.Add((int)prop.Category, mod.PropCategories[(int)prop.Category]);
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

            foreach (KeyValuePair<int, string> pair in Pages)
            {
                tabControl1.TabPages.Add(pair.Value);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = true; //causes massive cpu usage and choppy visuals with lots of elements
                //tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = false;

                curItem = 0;
                curPage = 1;

                foreach (ModPropertyBase prop in mod.Props)
                {
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
                    if (prop.Category == pair.Key)
                    {
                        prop.GenerateUI(tabControl1.TabPages[tabControl1.TabPages.Count - 1], ref offset);
                        offset += 25;
                        curItem++;
                    }
                }
                
                /*
                VScrollBar vscroll = new VScrollBar();
                vscroll.Parent = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                vscroll.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                vscroll.Height = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Height;
                vscroll.Location = new Point(tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width - 10, 0);
                vscroll.Scroll += VScroll_Scroll;
                vscroll.Minimum = 0;
                vscroll.Maximum = offset + initOffset;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Scroll += VScroll_Scroll;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].VerticalScroll.Minimum = 0;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].VerticalScroll.Maximum = offset + initOffset;

                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Height = offset + initOffset;
                */

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

            bool HasChanged = false;

            foreach (ModPropertyBase prop in mod.Props)
            {
                if (prop.HasChanged)
                {
                    HasChanged = true;
                    break;
                }
            }

            if (HasChanged)
            {
                Program.ModProgram.button_modMenu.Text = ModLoaderText.ModMenuButton + "*";
            }
            else
            {
                Program.ModProgram.button_modMenu.Text = ModLoaderText.ModMenuButton;
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
            // todo: maybe update UI instead at some point
            GenerateUI();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = string.Format("{0} (*.zip; *.txt)|*.zip;*.txt", ModLoaderText.ModMenuLoad_FileTypes);
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + ModLoaderGlobals.ModDirectory;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mod.LoadSettingsFromFile(openFileDialog1.FileName);

                GenerateUI();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = string.Format("{0} (*.txt)|*.txt|{1} (*.zip)|*.zip|{2} (*.txt)|*.txt", ModLoaderText.ModMenuSaveAs_SettingFile, ModLoaderText.ModMenuSaveAs_ModCrate, ModLoaderText.ModMenuSaveAs_SettingFileFull);
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + ModLoaderGlobals.ModDirectory;
            saveFileDialog1.FileName = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FilterIndex == 1)
                {
                    mod.SaveSettingsToFile(saveFileDialog1.FileName, false);
                }
                else if (saveFileDialog1.FilterIndex == 2)
                {
                    // todo: Mod Crate maker
                    MessageBox.Show("Mod Crate maker not yet implemented!");
                }
                else if (saveFileDialog1.FilterIndex == 3)
                {
                    mod.SaveSettingsToFile(saveFileDialog1.FileName, true);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // publish button
        }
    }
}
