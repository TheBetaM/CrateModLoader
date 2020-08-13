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
            int initOffset = 10;
            int offset = initOffset;

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

            foreach (KeyValuePair<int, string> pair in Pages)
            {
                tabControl1.TabPages.Add(pair.Value);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = true; //causes massive cpu usage and choppy visuals with lots of elements
                //tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = false;

                foreach (ModPropertyBase prop in mod.Props)
                {
                    if (prop.Category == pair.Key)
                    {
                        prop.GenerateUI(tabControl1.TabPages[tabControl1.TabPages.Count - 1], ref offset);
                        offset += 25;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
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
            // todo: load from text file / mod crate
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // todo: save to text file / mod crate
        }

    }
}
