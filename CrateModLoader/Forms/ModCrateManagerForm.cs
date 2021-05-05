using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    public partial class ModCrateManagerForm : Form
    {
        public ModLoader ModProgram;
        private bool ignoreChange = false;

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

            ModCrates.PopulateModList(ModProgram, ModProgram.SupportedMods, ModProgram.Modder != null, ShortName);

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
            if (index < 0) return;
            if (ignoreChange) return;
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
            CheckedListBox c = sender as CheckedListBox;
            int index = c.SelectedIndex;
            if (index < 0) return;

            UpdateInfoBox(index);
        }

        private void checkedListBox_mods_MouseMove(object sender, MouseEventArgs e)
        {
            CheckedListBox c = sender as CheckedListBox;
            int index = c.IndexFromPoint(e.Location);
            if (index < 0) return;

            if (c.SelectedIndex != index)
            {
                c.SelectedIndex = index;
            }

            UpdateInfoBox(index);
        }

        void UpdateInfoBox(int index)
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

        private void button_ToTop_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;

            ignoreChange = true;
            bool check = checkedListBox_mods.GetItemChecked(index);
            checkedListBox_mods.Items.Insert(0, checkedListBox_mods.Items[index]);
            checkedListBox_mods.SetItemChecked(0, check);
            checkedListBox_mods.Items.RemoveAt(index + 1);
            ModProgram.SupportedMods.Insert(0, ModProgram.SupportedMods[index]);
            ModProgram.SupportedMods.RemoveAt(index + 1);
            ignoreChange = false;
            checkedListBox_mods.SelectedIndex = 0;
        }

        private void button_MoveUp_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;
            if (index - 1 < 0) return;

            ignoreChange = true;
            bool check = checkedListBox_mods.GetItemChecked(index);
            checkedListBox_mods.Items.Insert(index - 1, checkedListBox_mods.Items[index]);
            checkedListBox_mods.SetItemChecked(index - 1, check);
            checkedListBox_mods.Items.RemoveAt(index + 1);
            ModProgram.SupportedMods.Insert(index - 1, ModProgram.SupportedMods[index]);
            ModProgram.SupportedMods.RemoveAt(index + 1);
            ignoreChange = false;
            checkedListBox_mods.SelectedIndex = index - 1;
        }

        private void button_MoveDown_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;
            if (index + 1 >= checkedListBox_mods.Items.Count) return;

            ignoreChange = true;
            bool check = checkedListBox_mods.GetItemChecked(index);
            checkedListBox_mods.Items.Insert(index + 1, checkedListBox_mods.Items[index]);
            checkedListBox_mods.SetItemChecked(index + 1, check);
            checkedListBox_mods.Items.RemoveAt(index);
            ModProgram.SupportedMods.Insert(index + 1, ModProgram.SupportedMods[index]);
            ModProgram.SupportedMods.RemoveAt(index);
            ignoreChange = false;
            checkedListBox_mods.SelectedIndex = index + 1;
        }

        private void button_ToBottom_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;

            ignoreChange = true;
            checkedListBox_mods.Items.Add(checkedListBox_mods.Items[index], checkedListBox_mods.GetItemChecked(index));
            checkedListBox_mods.Items.RemoveAt(index);
            ModProgram.SupportedMods.Add(ModProgram.SupportedMods[index]);
            ModProgram.SupportedMods.RemoveAt(index);
            ignoreChange = false;
            checkedListBox_mods.SelectedIndex = checkedListBox_mods.Items.Count - 1;
        }

        private void button_ImportCrate_Click(object sender, EventArgs e)
        {

        }

        private void button_CreateCrate_Click(object sender, EventArgs e)
        {

        }

        private void button_DeleteCrate_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;
        }

        private void button_DownloadCrates_Click(object sender, EventArgs e)
        {
            Process.Start(ModLoaderGlobals.ModCratesDownloadLink);
        }
    }
}
