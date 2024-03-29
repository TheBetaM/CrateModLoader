﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using CrateModAPI.Resources.Text;
using CrateModLoader.Tools.IO;
using CrateModLoader.Forms;

namespace CrateModLoader
{
    public partial class ModCrateManagerBox : UserControl
    {
        public ModLoader ModProgram;
        private bool ignoreChange = false;

        public ModCrateManagerBox(ModLoader Program)
        {
            ModProgram = Program;

            InitializeComponent();

            checkedListBox_mods.Items.Clear();
            label_author.Text = "";
            label_desc.Text = "";
            pictureBox_ModIcon.Image = null;

            toolTip1.SetToolTip(button_CreateCrate, "Create a Mod Crate for this game");
            toolTip1.SetToolTip(button_DeleteCrate, "Delete the selected Mod Crate and its files");
            toolTip1.SetToolTip(button_EditCrate, "Edit the selected Mod Crate");
            toolTip1.SetToolTip(button_ImportCrate, "Import a Mod Crate from a file or folder");
            toolTip1.SetToolTip(button_RefreshCrates, "Refresh and check available Mod Crates in your Mods directory");
            toolTip1.SetToolTip(button_ToTop, "Move the selected Mod Crate to be the first one installed.");
            toolTip1.SetToolTip(button_ToBottom, "Move the selected Mod Crate to be the last one installed.");
            toolTip1.SetToolTip(button_MoveUp, "Move the selected Mod Crate's install order earlier.");
            toolTip1.SetToolTip(button_MoveDown, "Move the selected Mod Crate's install order later.");

            //PopulateList();
        }

        public void PopulateList()
        {
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
            //button_confirm.Text = ModLoaderText.ModCrateManagerConfirmButton;
            Text = ModLoaderText.ModCrateManagerTitle;
            pictureBox_ModIcon.Image = null;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            //Close();
            ModProgram.UpdateModCrateChangedState();
        }

        private void button_importmod_Click(object sender, EventArgs e)
        {
            //todo
        }

        private void ModCrateManagerForm_Load(object sender, EventArgs e)
        {
            //Owner.Enabled = false;
        }

        private void ModCrateManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Owner.Enabled = true;

            ModProgram.UpdateModCrateChangedState();
        }

        private void checkedListBox_mods_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            if (index < 0) return;
            if (ignoreChange) return;
            if (e.NewValue == CheckState.Checked)
            {
                if (ModProgram.SupportedMods[index].HasScripts)
                {
                    if (MessageBox.Show("Warning: This mod contains executable scripts. Only activate scripts from sources you trust. Do you still want to activate this mod?", "Warning: Mod contains scripts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        ModCrates.UpdateModSelection(ModProgram.SupportedMods, index, true);
                    }
                }
                else
                {
                    ModCrates.UpdateModSelection(ModProgram.SupportedMods, index, true);
                }
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
            /* Not a good experience managing multiple crates.
            CheckedListBox c = sender as CheckedListBox;
            int index = c.IndexFromPoint(e.Location);
            if (index < 0) return;

            if (c.SelectedIndex != index)
            {
                c.SelectedIndex = index;
            }

            UpdateInfoBox(index);
            */
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
                    Image temp = Image.FromFile(Path.Combine(ModProgram.SupportedMods[index].Path, ModCrates.IconFileName));
                    Image disp = new Bitmap(temp);
                    temp.Dispose();
                    pictureBox_ModIcon.Image = disp;
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

            // broken
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
            if (contextMenuStrip_ImportCrate.Visible)
            {
                contextMenuStrip_ImportCrate.Hide();
            }
            else
            {
                contextMenuStrip_ImportCrate.Show(button_ImportCrate, new Point(0, button_ImportCrate.Height));
            }
        }

        private void button_DeleteCrate_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;

            if (MessageBox.Show("Are you sure you want to delete " + ModProgram.SupportedMods[index].Name + " and all its files?", "Delete Mod Crate?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ModCrates.DeleteModCrate(ModProgram.SupportedMods[index]);
                ModProgram.SupportedMods.RemoveAt(index);
                checkedListBox_mods.Items.RemoveAt(index);
                checkedListBox_mods.SelectedIndex = -1;
            }
        }

        private void toolStripMenuItem_FromFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Mod Crate (*.zip)|*.zip|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportModCrate(openFileDialog1.FileName, false);
            }
        }

        private void toolStripMenuItem_FromFolder_Click(object sender, EventArgs e)
        {
            // copy folder and verify that it's a mod crate for this game
            // then add it to the listbox and supportedmods list
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ImportModCrate(folderBrowserDialog1.SelectedPath + @"\", true);
            }
        }

        void ImportModCrate(string Path, bool FolderMode)
        {
            ModCrate TestCrate = null;
            string newPath = "";

            if (FolderMode)
            {
                DirectoryInfo origCrate = new DirectoryInfo(Path);
                TestCrate = ModCrates.LoadMetadata(origCrate);
                newPath = ModLoaderGlobals.ModDirectory + origCrate.Name + @"\";
                if (Directory.Exists(newPath))
                {
                    ModProgram.InvokeError("A folder with this name already exists!");
                    return;
                }
            }
            else
            {
                FileInfo origCrate = new FileInfo(Path);
                TestCrate = ModCrates.LoadMetadata(origCrate);

                newPath = ModLoaderGlobals.ModDirectory + origCrate.Name;
                if (File.Exists(newPath))
                {
                    ModProgram.InvokeError("A file with this name already exists!");
                    return;
                }
            }

            if (TestCrate == null)
            {
                ModProgram.InvokeError("Failed to import Mod Crate!");
                return;
            }

            if (FolderMode)
            {
                Directory.CreateDirectory(newPath);
                string pathparent = newPath;
                DirectoryInfo di = new DirectoryInfo(Path);
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Directory.CreateDirectory(pathparent + dir.Name);
                    foreach (FileInfo file in dir.EnumerateFiles())
                    {
                        file.CopyTo(pathparent + dir.Name + @"\" + file.Name);
                    }
                    IO_Common.Recursive_CopyFiles(dir, pathparent + dir.Name + @"\");
                }
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.CopyTo(pathparent + file.Name);
                }
            }
            else
            {
                File.Copy(Path, newPath);
            }

            PopulateList();

            if ((ModProgram.Game != null && TestCrate.TargetGame == ModProgram.Game.ShortName) ||
                (TestCrate.TargetGame == ModCrates.AllGamesShortName) ||
                (ModProgram.Game == null && TestCrate.TargetGame == ModCrates.UnsupportedGameShortName))
            {
                //ModProgram.SupportedMods.Add(NewCrate);
            }
            else
            {
                ModProgram.InvokeError("The Mod Crate has been imported, but it's not meant for the currently loaded game, so it will not appear on the list.");
            }
        }

        private void button_RefreshCrates_Click(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void button_CreateCrate_Click(object sender, EventArgs e)
        {
            if (ModProgram.HasProcessFinished)
            {
                ModProgram.InvokeError("The game ROM must be reloaded.");
                return;
            }
            if (!ModProgram.GamePreloaded)
            {
                ModProgram.InvokeError("The game must be preloaded to be able to create or edit Mod Crates.");
                return;
            }

            ModCrateWizardForm editMenu = new ModCrateWizardForm(ModProgram);

            editMenu.Owner = this.ParentForm;
            editMenu.Show();
        }

        private void button_EditCrate_Click(object sender, EventArgs e)
        {
            int index = checkedListBox_mods.SelectedIndex;
            if (index < 0) return;

            if (ModProgram.HasProcessFinished)
            {
                ModProgram.InvokeError("The game ROM must be reloaded.");
                return;
            }
            if (!ModProgram.GamePreloaded)
            {
                ModProgram.InvokeError("The game must be preloaded to be able to create or edit Mod Crates.");
                return;
            }

            ModCrateWizardForm editMenu = new ModCrateWizardForm(ModProgram, ModProgram.SupportedMods[index]);

            editMenu.Owner = this.ParentForm;
            editMenu.Show();
        }

    }
}
