using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using CrateModLoader.Resources.Text;
using CrateModLoader.Tools;
using CrateModLoader.ModProperties;

namespace CrateModLoader
{
    public partial class ModLoaderForm : Form
    {
        public ModLoaderForm()
        {
            InitializeComponent();
            linkLabel2.Text = ModLoaderText.ProgramTitle + " " + ModLoaderGlobals.ProgramVersion;
            label7.Text = "";
            linkLabel1.Text = "";
            label6.Text = ModLoaderText.Step1Text;
            button_startProcess.Enabled = false;
            linkLabel_optionDesc.Text = "";
            linkLabel_optionDesc.Enabled = true;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;
            Text = ModLoaderText.ProgramTitle;
            textBox1.Text = ModLoaderText.InputInstruction;
            textBox2.Text = ModLoaderText.OutputInstruction;
            button_downloadMods.Text = ModLoaderText.Button_DownloadMods;
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            button_modTools.Text = ModLoaderText.Button_ModTools;
            button_openModMenu.Text = ModLoaderText.ModMenuButton;
            button_randomizeSeed.Text = ModLoaderText.RandomizeSeedButton;
            button_browseInput.Text = ModLoaderText.InputBrowse;
            button_browseOutput.Text = ModLoaderText.OutputBrowse;
            button_startProcess.Text = ModLoaderText.StartProcessButton;

            Size = new Size(MinimumSize.Width, MinimumSize.Height);

            toolTip1.SetToolTip(linkLabel2, ModLoaderText.Tooltip_Label_Version);
            toolTip1.SetToolTip(linkLabel1, ModLoaderText.Tooltip_Label_API);
            toolTip1.SetToolTip(numericUpDown1, ModLoaderText.Tooltip_Numeric_Seed);
            toolTip1.SetToolTip(button_downloadMods, ModLoaderText.Tooltip_Button_DownloadMods);
            toolTip1.SetToolTip(button_modCrateMenu, ModLoaderText.Tooltip_Button_ModCrates);
            toolTip1.SetToolTip(button_openModMenu, ModLoaderText.Tooltip_Button_ModMenu);
            toolTip1.SetToolTip(button_modTools, ModLoaderText.Tooltip_Button_ModTools);
            toolTip1.SetToolTip(button_randomizeSeed, ModLoaderText.Tooltip_Button_RandomizeSeed);

            timer1.Enabled = false;
            timer1.Interval = 500;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            Random rand = new Random();
            int Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;
            ModLoaderGlobals.RandomizerSeedBase = Seed;

            openFileDialog1.FileName = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ModLoaderGlobals.ModProgram.inputDirectoryMode)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.ModProgram.OpenROM_Selection = ModLoader.OpenROM_SelectionType.Any;
                    ModLoaderGlobals.InputPath = folderBrowserDialog1.SelectedPath + @"\";
                    ModLoaderGlobals.ModProgram.CheckISO();
                    textBox1.Text = ModLoaderGlobals.InputPath;
                }
            }
            else
            {
                //openFileDialog1.Filter = "PSX/PS2/PSP/GCN/WII/XBOX/360 ROM (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|All files (*.*)|*.*";
                openFileDialog1.Filter = ModLoaderText.InputDialogTypeAuto + " (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.ModProgram.OpenROM_Selection = (ModLoader.OpenROM_SelectionType)openFileDialog1.FilterIndex;
                    ModLoaderGlobals.InputPath = openFileDialog1.FileName;
                    ModLoaderGlobals.ModProgram.CheckISO();
                    textBox1.Text = ModLoaderGlobals.InputPath;
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ModLoaderGlobals.ModProgram.outputDirectoryMode)
            {
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.OutputPath = folderBrowserDialog2.SelectedPath + @"\";
                    textBox2.Text = ModLoaderGlobals.OutputPath;
                    ModLoaderGlobals.ModProgram.outputPathSet = true;
                    if (ModLoaderGlobals.ModProgram.loadedISO && ModLoaderGlobals.ModProgram.outputPathSet)
                    {
                        button_startProcess.Enabled = true;
                        label6.Text = ModLoaderText.ProcessReady;
                    }
                    else
                    {
                        button_startProcess.Enabled = false;
                    }
                }
            }
            else
            {
                saveFileDialog1.Filter = "ISO (*.iso)|*.iso|BIN (*.bin)|*.bin|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

                saveFileDialog1.FileName = ModLoaderText.DefaultOutputFileName + ".iso";
                saveFileDialog1.ShowDialog();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ModLoaderGlobals.RandomizerSeed = int.Parse(numericUpDown1.Text);
            ModLoaderGlobals.RandomizerSeedBase = ModLoaderGlobals.RandomizerSeed;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int Seed = rand.Next(0,int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;
            ModLoaderGlobals.RandomizerSeedBase = Seed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ModLoaderGlobals.OutputPath = saveFileDialog1.FileName;
            textBox2.Text = ModLoaderGlobals.OutputPath;
            ModLoaderGlobals.ModProgram.outputPathSet = true;
            if (ModLoaderGlobals.ModProgram.loadedISO && ModLoaderGlobals.ModProgram.outputPathSet)
            {
                button_startProcess.Enabled = true;
                label6.Text = ModLoaderText.ProcessReady;
            }
            else
            {
                button_startProcess.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ModLoaderGlobals.OutputPath = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModLoaderGlobals.ModProgram.DisableInteraction();

            if (checkedListBox1.CheckedItems.Count <= 0 && ModCrates.ModsActiveAmount <= 0 && !button_openModMenu.Text.EndsWith("*"))
            {
                if (MessageBox.Show(ModLoaderText.NoOptionsSelectedPopup, ModLoaderText.NoOptionsSelectedTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ModLoaderGlobals.ModProgram.StartButtonPressed();
                }
                else
                {
                    ModLoaderGlobals.ModProgram.EnableInteraction();
                }
            }
            else
            {
                ModLoaderGlobals.ModProgram.StartButtonPressed();
            }
        }

        private void button_modCrateMenu_Click(object sender, EventArgs e)
        {
            // Mod Crate Manager Window: 
            // Either a checkbox list of .zip files in a mod directory OR
            // A list with a button that lets you manually add .zip files
            // Set availability in the respective modder's Game struct (ModCratesSupported variable) 

            ModCrateManagerForm modCrateManagerMenu = new ModCrateManagerForm();

            modCrateManagerMenu.Owner = this;
            modCrateManagerMenu.Show();
        }

        private void button_openModMenu_Click(object sender, EventArgs e)
        {
            // Individual Game Mod Menu
            // Detailed settings UI for mod properties
            // Automatically generated for any ModProperty in the modder class' namespace

            ModMenuForm modMenu = new ModMenuForm(this, ModLoaderGlobals.ModProgram.Modder, ModLoaderGlobals.ModProgram.Game);

            modMenu.Owner = this;
            modMenu.Show();

            //ModLoaderGlobals.ModProgram.Modder.OpenModMenu();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox c = sender as CheckedListBox;
            for (int i = 0; i < c.Items.Count; ++i)
            {
                if (c.Items[i] is ModPropOption option)
                {
                    if (c.SelectedIndex == i)
                    {
                        if (c.GetItemChecked(i) == false)
                        {
                            option.Value = 0;
                        }
                        else
                        {
                            option.Value = 1;
                        }
                        option.HasChanged = true;
                        if (!string.IsNullOrEmpty(option.Description))
                        {
                            linkLabel_optionDesc.Text = option.Description;
                            linkLabel_optionDesc.Visible = true;
                            panel_desc.Visible = true;
                        }
                        else
                        {
                            linkLabel_optionDesc.Text = string.Empty;
                            linkLabel_optionDesc.Visible = false;
                            panel_desc.Visible = false;
                        }
                    }
                }
            }
        }
        public void UpdateOptionList()
        {
            CheckedListBox c = checkedListBox1;
            c.SelectedIndex = -1;
            for (int i = 0; i < c.Items.Count; ++i)
            {
                if (c.Items[i] is ModPropOption option)
                {
                    if (option.Value == 0)
                    {
                        c.SetItemChecked(i, false);
                    }
                    else
                    {
                        c.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel1.LinkVisited = true;
                ModLoaderGlobals.ModProgram.API_Link_Clicked();
            }
            catch
            {
                MessageBox.Show(ModLoaderText.LinkOpenFail);
            }
        }

        private void ModLoaderForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!ModLoaderGlobals.ModProgram.processActive)
            {
                try
                {
                    string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    if (fileList.Length == 1)
                    {
                        textBox1.Text = "";
                        if (Directory.Exists(fileList[0]))
                        {
                            ModLoaderGlobals.ModProgram.inputDirectoryMode = true;
                            ModLoaderGlobals.ModProgram.UpdateInputSetting(true);
                            ModLoaderGlobals.InputPath = fileList[0] + @"\";
                        }
                        else
                        {
                            ModLoaderGlobals.ModProgram.inputDirectoryMode = false;
                            ModLoaderGlobals.ModProgram.UpdateInputSetting(false);
                            ModLoaderGlobals.InputPath = fileList[0];
                        }
                        ModLoaderGlobals.ModProgram.OpenROM_Selection = ModLoader.OpenROM_SelectionType.Any;
                        ModLoaderGlobals.ModProgram.CheckISO();
                        textBox1.Text = ModLoaderGlobals.InputPath;
                    }
                }
                catch
                {

                }
            }
        }

        private void ModLoaderForm_DragEnter(object sender, DragEventArgs e)
        {
            if (!ModLoaderGlobals.ModProgram.processActive)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            Process.Start("https://github.com/TheBetaM/CrateModLoader");
        }

        private bool IO_Delay = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IO_Delay)
            {
                timer1.Enabled = false;
                ModLoaderGlobals.ModProgram.Async_BuildFinished();
                return;
            }
            var progress = PS2ImageMaker.PollProgress();
            //Progress.Value = (int)(progress.ProgressPercentage * 100);
            switch (progress.ProgressS)
            {
                default:
                    break;
                case PS2ImageMaker.ProgressState.FAILED:
                    IO_Delay = true;
                    Console.WriteLine("Error: PS2 Image Build failed!");
                    break;
                case PS2ImageMaker.ProgressState.FINISHED:
                    IO_Delay = true;
                    break;
            }
        }

        private void toolStripMenuItem_showCredits_Click(object sender, EventArgs e)
        {
            TextDisplayForm textForm = new TextDisplayForm(TextDisplayForm.TextDisplayType.Credits);

            textForm.Owner = this;
            textForm.Show();
        }

        private void toolStripMenuItem_showGames_Click(object sender, EventArgs e)
        {
            TextDisplayForm textForm = new TextDisplayForm(TextDisplayForm.TextDisplayType.Games);

            textForm.Owner = this;
            textForm.Show();
        }

        private void toolStripMenuItem_showChangelog_Click(object sender, EventArgs e)
        {
            TextDisplayForm textForm = new TextDisplayForm(TextDisplayForm.TextDisplayType.Changelog);

            textForm.Owner = this;
            textForm.Show();
        }

        private void toolStripMenuItem_loadROM_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            ModLoaderGlobals.ModProgram.inputDirectoryMode = false;
            ModLoaderGlobals.ModProgram.UpdateInputSetting(false);
            button1_Click(null, null);
        }

        private void toolStripMenuItem_loadFolder_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            ModLoaderGlobals.ModProgram.inputDirectoryMode = true;
            ModLoaderGlobals.ModProgram.UpdateInputSetting(true);
            button1_Click(null, null);
        }

        private void toolStripMenuItem_saveROM_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            button_startProcess.Enabled = false;
            ModLoaderGlobals.ModProgram.outputDirectoryMode = false;
            ModLoaderGlobals.ModProgram.UpdateOutputSetting(false);
            button2_Click(null, null);
        }

        private void toolStripMenuItem_saveFolder_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            button_startProcess.Enabled = false;
            ModLoaderGlobals.ModProgram.outputDirectoryMode = true;
            ModLoaderGlobals.ModProgram.UpdateOutputSetting(true);
            button2_Click(null, null);
        }

        private void toolStripMenuItem_keepTempFiles_CheckedChanged(object sender, EventArgs e)
        {
            ModLoaderGlobals.ModProgram.keepTempFiles = toolStripMenuItem_keepTempFiles.Checked;
        }

        // UI-specific functions
        public void AdjustSize(int height)
        {
            if (Size.Height < height)
            {
                //Size = new Size(mMinimumSize.Width, height + 300);
                Size = new Size(MinimumSize.Width, height);
            }
            MinimumSize = new Size(MinimumSize.Width, height);
            if (Size.Height > height)
            {
                Size = new Size(MinimumSize.Width, height);
            }
        }

        public void DisableInteraction()
        {
            button_startProcess.Enabled = false;
            checkedListBox1.Enabled = false;
            button_browseInput.Enabled = false;
            button_browseOutput.Enabled = false;
            button_randomizeSeed.Enabled = false;
            textBox2.ReadOnly = true;
            numericUpDown1.ReadOnly = true;
            numericUpDown1.Enabled = false;
            button_openModMenu.Enabled = false;
            button_modCrateMenu.Enabled = false;
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            button_modTools.Enabled = false;
            button_downloadMods.Enabled = false;
        }
        public void EnableInteraction()
        {
            button_startProcess.Enabled = true;
            checkedListBox1.Enabled = true;
            button_browseInput.Enabled = true;
            button_browseOutput.Enabled = true;
            button_randomizeSeed.Enabled = true;
            textBox2.ReadOnly = false;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Enabled = true;
            button_modCrateMenu.Enabled = true;

            if (ModLoaderGlobals.ModProgram.Modder != null)
            {
                button_openModMenu.Enabled = ModLoaderGlobals.ModProgram.Modder.ModMenuEnabled;
            }
            button_modTools.Enabled = true;
            //button_downloadMods.Enabled = true;

            if (ModLoaderGlobals.ModProgram.Modder != null && !string.IsNullOrWhiteSpace(ModLoaderGlobals.ModProgram.Game.API_Link))
            {
                linkLabel1.Enabled = true;
            }
            linkLabel2.Enabled = true;
        }

        public void ResetGameSpecific(bool ClearGameText = false, bool ExtendedWindow = false)
        {
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            button_openModMenu.Text = ModLoaderText.ModMenuButton;

            button_startProcess.Enabled = false;

            button_openModMenu.Enabled = button_openModMenu.Visible = false;
            button_modCrateMenu.Enabled = button_modCrateMenu.Visible = false;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = button_modTools.Visible = button_modTools.Enabled = button_downloadMods.Enabled = button_downloadMods.Visible = false;
            numericUpDown1.Enabled = numericUpDown1.Visible = false;

            linkLabel1.Text = string.Empty;
            linkLabel1.LinkVisited = false;
            if (ClearGameText)
            {
                label7.Text = string.Empty;
            }

            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;

            checkedListBox1.Visible = checkedListBox1.Enabled = false;

            int Height = 188;
            if (ExtendedWindow)
            {
                Height = 220;
            }

            AdjustSize(Height);
        }

        public void SetLayoutUnsupportedGame(string cons_mod)
        {
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            checkedListBox1.Items.Clear();
            button_openModMenu.Visible = true;
            button_openModMenu.Enabled = false;
            button_modCrateMenu.Enabled = button_modCrateMenu.Visible = button_modTools.Visible = true;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = button_openModMenu.Visible = button_openModMenu.Enabled = button_downloadMods.Enabled = button_downloadMods.Visible = false;
            numericUpDown1.Enabled = numericUpDown1.Visible = false;
            button_modTools.Enabled = true;

            label7.Text = ModLoaderText.UnsupportedGameTitle + " (" + cons_mod + ")";
            linkLabel1.Text = string.Empty;
            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;

            int height = 295 + 45 + (checkedListBox1.Items.Count * 17);
            checkedListBox1.Visible = checkedListBox1.Enabled = checkedListBox1.Items.Count > 0;
            AdjustSize(height);
        }
        public void SetLayoutSupportedGame(Game game, string cons_mod, string region_mod)
        {
            button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            checkedListBox1.Items.Clear();
            button_openModMenu.Visible = true;
            button_openModMenu.Enabled = ModLoaderGlobals.ModProgram.Modder.ModMenuEnabled;
            button_modCrateMenu.Visible = true;
            button_modCrateMenu.Enabled = !game.ModCratesDisabled;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = button_modTools.Visible = button_downloadMods.Visible = true;
            numericUpDown1.Enabled = numericUpDown1.Visible = true;
            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;
            button_modTools.Enabled = true;

            if (string.IsNullOrWhiteSpace(region_mod))
            {
                label7.Text = string.Format("{0}\n({1})", game.Name, cons_mod);
            }
            else
            {
                label7.Text = string.Format("{0}\n({1} {2})", game.Name, region_mod, cons_mod);
            }

            if (!string.IsNullOrWhiteSpace(game.API_Credit))
            {
                linkLabel1.Text = game.API_Credit;
                if (!string.IsNullOrWhiteSpace(game.API_Link))
                {
                    linkLabel1.Enabled = true;
                }
                else
                {
                    linkLabel1.Enabled = false;
                }
            }
            else
            {
                linkLabel1.Text = ModLoaderText.GameHasNoAPI;
                linkLabel1.Enabled = false;
            }

            if (ModLoaderGlobals.ModProgram.Modder.Props.Count > 0)
            {
                foreach (var prop in ModLoaderGlobals.ModProgram.Modder.Props)
                {
                    if (prop is ModPropOption option && option.Allowed())
                    {
                        checkedListBox1.Items.Add(option, option.Value != 0);
                    }
                }
            }

            int height = 295 + 45 + (checkedListBox1.Items.Count * 17);
            checkedListBox1.Visible = checkedListBox1.Enabled = checkedListBox1.Items.Count > 0;
            AdjustSize(height);
        }

        public void UpdateGameTitleText(string txt)
        {
            label7.Text = txt;
        }
        public void UpdateProcessText(string txt)
        {
            label6.Text = txt;
        }

        public void UpdateProcessProgress(int value)
        {
            progressBar1.Value = value;
        }
        public void ResetProcessProgress()
        {
            progressBar1.Value = progressBar1.Minimum;
        }

        public void SetProcessStartAllowed(bool allow)
        {
            button_startProcess.Enabled = allow;
        }

        public void UpdateModMenuChangeState(bool changed)
        {
            if (changed)
            {
                button_openModMenu.Text = ModLoaderText.ModMenuButton + "*";
            }
            else
            {
                button_openModMenu.Text = ModLoaderText.ModMenuButton;
            }
        }

        public void UpdateModCrateChangedState()
        {
            string CratesActive = ModLoaderText.ModCratesButton;
            if (ModCrates.ModsActiveAmount > 0)
            {
                CratesActive += $" ({ ModCrates.ModsActiveAmount }x)";
            }

            button_modCrateMenu.Text = CratesActive;
        }

        public void StartAsyncTimer()
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        [DllImport("user32.dll")]
        public static extern int FlashWindow(IntPtr Hwnd, bool Revert);

        public void Notify_ProcessFinished()
        {
            FlashWindow(Handle, false);
            SystemSounds.Beep.Play();
        }


    }
}
