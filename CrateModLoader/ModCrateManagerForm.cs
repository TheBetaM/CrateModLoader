using System;
using System.Windows.Forms;
using CrateModLoader.Resources.Text;

namespace CrateModLoader
{
    public partial class ModCrateManagerForm : Form
    {
        public ModCrateManagerForm()
        {
            InitializeComponent();

            checkedListBox_mods.Items.Clear();

            ModCrates.PopulateModList();

            if (ModCrates.SupportedMods.Count > 0)
            {
                for (int i = 0; i < ModCrates.SupportedMods.Count; i++)
                {
                    string ListName = ModCrates.SupportedMods[i].Name;
                    ListName += " ";
                    ListName += ModCrates.SupportedMods[i].Version;

                    /*
                    uint ModLoaderVer;
                    if (uint.TryParse(SupportedMods[i].CML_Version, out ModLoaderVer))
                    {
                        if (ModLoaderVer != ModLoaderGlobals.ProgramVersionSimple)
                        {
                            ListName += " ";
                            ListName += "(*)";
                        }
                    }
                    else
                    {
                        ListName += " ";
                        ListName += "(*)";
                    }
                    */

                    checkedListBox_mods.Items.Add(ListName);
                    if (ModCrates.SupportedMods[i].IsActivated)
                    {
                        checkedListBox_mods.SetItemCheckState(i, CheckState.Checked);
                    }
                    else
                    {
                        checkedListBox_mods.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
            }

            label_author.Text = "";
            label_desc.Text = "";
            button_confirm.Text = ModLoaderText.ModCrateManagerConfirmButton;
            Text = ModLoaderText.ModCrateManagerTitle;
            pictureBox_ModIcon.Image = null;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            Close();
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

            string CratesActive = ModLoaderText.ModCratesButton;
            if (ModCrates.ModsActiveAmount > 0)
            {
                CratesActive += $" ({ ModCrates.ModsActiveAmount }x)";
            }

            ModLoaderGlobals.ModProgram.button_modCrateMenu.Text = CratesActive;
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
                label_author.Text = ModLoaderText.ModCrateManagerAuthorText + " " + ModCrates.SupportedMods[index].Author;
                label_desc.Text = ModCrates.SupportedMods[index].Desc;
                if (ModCrates.SupportedMods[index].Icon == null)
                {
                    pictureBox_ModIcon.Image = Properties.Resources.cml_icon;
                }
                else
                {
                    pictureBox_ModIcon.Image = ModCrates.SupportedMods[index].Icon;
                }
            }
        }
    }
}
