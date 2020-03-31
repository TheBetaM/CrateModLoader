using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrateModLoader
{
    public partial class ModCrateManagerForm : Form
    {
        public ModCrateManagerForm()
        {
            InitializeComponent();
            ModCrates.CheckedList_Mods = checkedListBox_mods;
            label_author.Text = "";
            label_desc.Text = "";
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_importmod_Click(object sender, EventArgs e)
        {
            //todo
        }

        private void ModCrateManagerForm_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void ModCrateManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;

            string CratesActive = "Mod Crates";
            if (ModCrates.ModsActive)
            {
                if (ModCrates.ModsActiveAmount > 0)
                {
                    CratesActive += " (" + ModCrates.ModsActiveAmount + " enabled)";
                }
            }

            Program.ModProgram.button_modCrateMenu.Text = CratesActive;
        }

        private void checkedListBox_mods_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            if (e.NewValue == CheckState.Checked)
            {
                ModCrates.UpdateModSelection(index, true);
            }
            else
            {
                ModCrates.UpdateModSelection(index, false);
            }
        }

        private void checkedListBox_mods_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index >= 0)
            {
                label_author.Text = "Author: " + ModCrates.SupportedMods[index].Author;
                label_desc.Text = "Description: " + ModCrates.SupportedMods[index].Desc;
            }
        }
    }
}
