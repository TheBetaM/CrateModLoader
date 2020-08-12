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

namespace CrateModLoader
{
    public partial class ModMenuForm : Form
    {

        private Modder mod;

        public ModMenuForm(Modder modder)
        {
            InitializeComponent();

            mod = modder;

            int initOffset = 10;
            int offset = initOffset;

            // todo: some kind of tab name sorting from lowest ID to highest
            Dictionary<int, string> Pages = new Dictionary<int, string>();
            foreach (ModPropertyBase prop in mod.Props)
            {
                if (!Pages.ContainsKey(prop.Category))
                {
                    if (mod.PropCategories.ContainsKey(prop.Category))
                    {
                        Pages.Add(prop.Category, mod.PropCategories[prop.Category]);
                    }
                    else
                    {
                        Pages.Add(prop.Category, "Page " + (prop.Category + 1));
                    }
                }
            }

            tabControl1.TabPages.Clear();

            foreach (KeyValuePair<int, string> pair in Pages)
            {
                tabControl1.TabPages.Add(pair.Value);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].AutoScroll = true;

                foreach (ModPropertyBase prop in mod.Props)
                {
                    if (prop.Category == pair.Key)
                    {
                        prop.GenerateUI(tabControl1.TabPages[tabControl1.TabPages.Count - 1], ref offset);
                        offset += 20;
                    }
                }

                offset = initOffset;
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
                // todo: update UI
            }
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
