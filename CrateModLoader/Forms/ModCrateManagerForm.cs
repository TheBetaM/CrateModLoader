using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    public partial class ModCrateManagerForm : Form
    {
        public ModLoader ModProgram;

        public ModCrateManagerForm(ModLoader Program)
        {
            ModProgram = Program;

            InitializeComponent();

            checkedListBox_mods.Items.Clear();

            string ShortName = ModCrates.UnsupportedGameShortName;
            if (ModProgram.Modder != null)
            {
                ShortName = ModProgram.Game.ShortName;
            }

            ModCrates.PopulateModList(ModProgram.SupportedMods, ModProgram.Modder != null, ShortName);

            if (ModProgram.SupportedMods.Count > 0)
            {
                for (int i = 0; i < ModProgram.SupportedMods.Count; i++)
                {
                    string ListName = ModProgram.SupportedMods[i].Name;
                    ListName += " ";
                    ListName += ModProgram.SupportedMods[i].Version;

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
                    if (ModProgram.SupportedMods[i].IsActivated)
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

            ModProgram.UpdateModCrateChangedState();
        }

        private void checkedListBox_mods_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            if (e.NewValue == CheckState.Checked)
            {
                ModCrates.UpdateModSelection(ModProgram.SupportedMods, index, true);
            }
            else
            {
                ModCrates.UpdateModSelection(ModProgram.SupportedMods, index, false);
            }
        }

        private void checkedListBox_mods_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index >= 0)
            {
                label_author.Text = ModLoaderText.ModCrateManagerAuthorText + " " + ModProgram.SupportedMods[index].Author;
                label_desc.Text = ModProgram.SupportedMods[index].Desc;
                pictureBox_ModIcon.Image = Properties.Resources.cml_icon;
                if (ModProgram.SupportedMods[index].HasIcon)
                {
                    if (ModProgram.SupportedMods[index].IsFolder)
                    {
                        pictureBox_ModIcon.Image = Image.FromFile(Path.Combine(ModProgram.SupportedMods[index].Path, ModCrates.IconFileName));
                    }
                    else
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(ModProgram.SupportedMods[index].Path))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.Name.ToLower() == ModCrates.IconFileName)
                                {
                                    pictureBox_ModIcon.Image = Image.FromStream(entry.Open());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
