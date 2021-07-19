using System;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using CrateModAPI.Resources.Text;
using CrateModLoader.ModProperties;
using CrateModLoader.Forms.LevelEditor;

namespace CrateModLoader
{
    public partial class ModLoaderForm : Form
    {
        public ModLoader ModProgram;
        public ModCrateManagerBox ModCrateBox;

        public ModLoaderForm(ModLoader Program)
        {
            ModProgram = Program;

            InitializeComponent();
            linkLabel_programTitle.Text = ModLoaderText.ProgramTitle + " " + ModLoaderGlobals.ProgramVersion;
            label_gameType.Text = "";
            linkLabel_apiCredit.Text = "";
            label_processText.Text = ModLoaderText.Step1Text;
            button_startProcess.Enabled = false;
            linkLabel_optionDesc.Text = "";
            linkLabel_optionDesc.Enabled = true;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;
            Text = ModLoaderText.ProgramTitle;
            textBox_inputPath.Text = ModLoaderText.InputInstruction;
            textBox_outputPath.Text = ModLoaderText.OutputInstruction;
            button_preloadGame.Text = ModLoaderText.Button_PreloadGame;

            ModCrateBox = new ModCrateManagerBox(ModProgram);
            ModCrateBox.Dock = DockStyle.Fill;
            panel_modCrateManager.Controls.Add(ModCrateBox);
            ModCrateBox.Visible = ModCrateBox.Enabled = false;

            //button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            //button_modTools.Text = "Level Editor";
            button_openModMenu.Text = ModLoaderText.ModMenuButton;
            button_randomizeSeed.Text = ModLoaderText.RandomizeSeedButton + " →";
            button_browseInput.Text = ModLoaderText.InputBrowse;
            button_browseOutput.Text = ModLoaderText.OutputBrowse;
            button_startProcess.Text = ModLoaderText.StartProcessButton;

            Size = new Size(MinimumSize.Width, MinimumSize.Height);

            toolTip1.SetToolTip(linkLabel_programTitle, ModLoaderText.Tooltip_Label_Version);
            toolTip1.SetToolTip(linkLabel_apiCredit, ModLoaderText.Tooltip_Label_API);
            toolTip1.SetToolTip(numericUpDown1, ModLoaderText.Tooltip_Numeric_Seed);
            toolTip1.SetToolTip(button_preloadGame, ModLoaderText.Tooltip_PreloadGame);
            //toolTip1.SetToolTip(button_modCrateMenu, ModLoaderText.Tooltip_Button_ModCrates);
            toolTip1.SetToolTip(button_openModMenu, ModLoaderText.Tooltip_Button_ModMenu);
            //toolTip1.SetToolTip(button_modTools, ModLoaderText.Tooltip_Button_ModTools);
            toolTip1.SetToolTip(button_randomizeSeed, ModLoaderText.Tooltip_Button_RandomizeSeed);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            Random rand = new Random();
            int Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;
            ModProgram.RandomizerSeedBase = Seed;

            openFileDialog1.FileName = string.Empty;

            ModProgram.ProcessMessageChanged += UpdateProcessText;
            ModProgram.InteractionEnable += EnableInteraction;
            ModProgram.InteractionDisable += DisableInteraction;
            ModProgram.ProcessProgressChanged += UpdateProcessProgress;
            ModProgram.ProcessFinished += Notify_ProcessFinished;
            ModProgram.ModCratesUpdated += UpdateModCrateChangedState;
            ModProgram.ResetGameEvent += ResetGameSpecific;
            ModProgram.SetProcessStartAllow += SetProcessStartAllowed;
            ModProgram.ModMenuUpdated += UpdateModMenuChangeState;
            ModProgram.LayoutChangeUnsupported += SetLayoutUnsupportedGame;
            ModProgram.LayoutChangeSupported += SetLayoutSupportedGame;
            ModProgram.ErrorMessage += DisplayError;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (ModProgram.inputDirectoryMode)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.InputPath = folderBrowserDialog1.SelectedPath + @"\";
                    ModProgram.DetectGame(ModLoaderGlobals.InputPath);
                    textBox_inputPath.Text = ModLoaderGlobals.InputPath;
                }
            }
            else
            {
                
            }
            */

            //openFileDialog1.Filter = "PSX/PS2/PSP/GCN/WII/XBOX/360 ROM (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|All files (*.*)|*.*";
            openFileDialog1.Filter = ModLoaderText.InputDialogTypeAuto + " (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ModProgram.InputPath = openFileDialog1.FileName;
                ModProgram.DetectGame(ModProgram.InputPath);
                textBox_inputPath.Text = ModProgram.InputPath;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            if (ModProgram.outputDirectoryMode)
            {
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.OutputPath = folderBrowserDialog2.SelectedPath + @"\";
                    textBox_outputPath.Text = ModLoaderGlobals.OutputPath;
                    ModProgram.OutputPathSet = true;
                    if (ModProgram.GameDetected)
                    {
                        button_startProcess.Enabled = true;
                        label_processText.Text = ModLoaderText.ProcessReady;
                    }
                    else
                    {
                        button_startProcess.Enabled = false;
                    }
                }
            }
            else
            {
                
            }
            */

            switch (ModProgram.Pipeline.Metadata.Console)
            {
                default:
                    saveFileDialog1.Filter = "ISO (*.iso)|*.iso|BIN (*.bin)|*.bin|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
                    saveFileDialog1.FileName = ModLoaderText.DefaultOutputFileName + ".iso";
                    break;
                case ConsoleMode.N3DS:
                    saveFileDialog1.Filter = "3DS (*.3ds)|*.3ds|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";
                    saveFileDialog1.FileName = ModLoaderText.DefaultOutputFileName + ".3ds";
                    break;
            }
            
            saveFileDialog1.ShowDialog();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ModLoaderGlobals.RandomizerSeed = int.Parse(numericUpDown1.Text);
            ModProgram.RandomizerSeedBase = ModLoaderGlobals.RandomizerSeed;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int Seed = rand.Next(0,int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;
            ModProgram.RandomizerSeedBase = Seed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ModProgram.OutputPath = saveFileDialog1.FileName;
            textBox_outputPath.Text = ModProgram.OutputPath;
            bool ready = ModProgram.Pipeline != null && !ModProgram.HasProcessFinished;
            button_startProcess.Enabled = ready;
            if (ready)
            {
                UpdateProcessText(ModLoaderText.ProcessReady);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ModProgram.OutputPath = textBox_outputPath.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count <= 0 && ModProgram.ModsActiveAmount <= 0 && !button_openModMenu.Text.StartsWith("*"))
            {
                if (MessageBox.Show(ModLoaderText.NoOptionsSelectedPopup, ModLoaderText.NoOptionsSelectedTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ModProgram.StartProcess(false);
                }
            }
            else
            {
                ModProgram.StartProcess(false);
            }
        }

        private void button_openModMenu_Click(object sender, EventArgs e)
        {
            // Individual Game Mod Menu
            // Detailed settings UI for mod properties
            // Automatically generated for any ModProperty in the modder class' namespace

            ModMenuForm modMenu = new ModMenuForm(this, ModProgram.Modder, ModProgram.Game);

            modMenu.Owner = this;
            modMenu.Show();

            //ModProgram.Modder.OpenModMenu();
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
                        linkLabel_optionDesc.Text = option.Description;
                        linkLabel_optionDesc.Visible = panel_desc.Visible = !string.IsNullOrEmpty(option.Description);
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
                linkLabel_apiCredit.LinkVisited = true;
                if (ModProgram.Modder != null && !string.IsNullOrWhiteSpace(ModProgram.Game.API_Credit) && !string.IsNullOrWhiteSpace(ModProgram.Game.API_Link))
                {
                    Process.Start(ModProgram.Game.API_Link);
                }
            }
            catch
            {
                MessageBox.Show(ModLoaderText.LinkOpenFail);
            }
        }

        private void ModLoaderForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (fileList.Length == 1)
                {
                    textBox_inputPath.Text = "";
                    if (Directory.Exists(fileList[0]))
                    {
                        ModProgram.InputPath = fileList[0] + @"\";
                    }
                    else
                    {
                        ModProgram.InputPath = fileList[0];
                    }
                    ModProgram.ResetGameSpecific(true);
                    UpdateProcessText("Detecting...");
                    Update();
                    ModProgram.DetectGame(ModProgram.InputPath);
                    textBox_inputPath.Text = ModProgram.InputPath;
                }
            }
            catch
            {

            }
        }

        private void ModLoaderForm_DragEnter(object sender, DragEventArgs e)
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel_programTitle.LinkVisited = true;
            Process.Start("https://github.com/TheBetaM/CrateModLoader");
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
            textBox_inputPath.Text = "";
            ModProgram.InputPath = "";
            ModProgram.ResetGameSpecific(true);
            UpdateProcessText(ModLoaderText.Step1Text);
            button1_Click(null, null);
        }

        private void toolStripMenuItem_loadFolder_Click(object sender, EventArgs e)
        {
            textBox_inputPath.Text = "";
            ModProgram.InputPath = "";
            ModProgram.ResetGameSpecific(true);
            UpdateProcessText(ModLoaderText.Step1Text);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ModProgram.InputPath = folderBrowserDialog1.SelectedPath + @"\";
                ModProgram.DetectGame(ModProgram.InputPath);
                textBox_inputPath.Text = ModProgram.InputPath;
            }
        }

        private void toolStripMenuItem_saveROM_Click(object sender, EventArgs e)
        {
            textBox_outputPath.Text = "";
            ModProgram.OutputPath = "";
            button_startProcess.Enabled = false;
            button2_Click(null, null);
        }

        private void toolStripMenuItem_saveFolder_Click(object sender, EventArgs e)
        {
            textBox_outputPath.Text = "";
            ModProgram.OutputPath = "";
            button_startProcess.Enabled = false;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                ModProgram.OutputPath = folderBrowserDialog2.SelectedPath + @"\";
                textBox_outputPath.Text = ModProgram.OutputPath;

                bool ready = ModProgram.Pipeline != null && !ModProgram.HasProcessFinished;
                button_startProcess.Enabled = ready;
                if (ready)
                {
                    UpdateProcessText(ModLoaderText.ProcessReady);
                }
            }
        }

        // UI-specific functions
        public void AdjustSize(int height)
        {
            if (Size.Height < height)
            {
                //Size = new Size(mMinimumSize.Width, height + 300);
                Size = new Size(MinimumSize.Width, height);
            }
            //MinimumSize = new Size(MinimumSize.Width, height);
            if (Size.Height > height)
            {
                Size = new Size(MinimumSize.Width, height);
            }
        }

        public void DisableInteraction(object sender, EventArgs e)
        {
            button_startProcess.Enabled = false;
            checkedListBox1.Enabled = false;
            button_browseInput.Enabled = false;
            button_browseOutput.Enabled = false;
            button_randomizeSeed.Enabled = false;
            button_LevelEditor.Enabled = false;
            textBox_outputPath.ReadOnly = true;
            numericUpDown1.ReadOnly = true;
            numericUpDown1.Enabled = false;
            button_openModMenu.Enabled = false;
            //button_modCrateMenu.Enabled = false;
            linkLabel_apiCredit.Enabled = false;
            linkLabel_programTitle.Enabled = false;
            //button_modTools.Enabled = false;
            button_preloadGame.Enabled = false;
            menuStrip1.Enabled = false;
            ModCrateBox.Enabled = false;
            DragDrop -= ModLoaderForm_DragDrop;
            DragEnter -= ModLoaderForm_DragEnter;
        }
        public void EnableInteraction(object sender, EventArgs e)
        {
            if (ModProgram.OutputPath != string.Empty && !ModProgram.HasProcessFinished)
            {
                button_startProcess.Enabled = true;
            }
            else
            {
                button_startProcess.Enabled = false;
            }
            checkedListBox1.Enabled = true;
            button_browseInput.Enabled = true;
            button_browseOutput.Enabled = true;
            button_randomizeSeed.Enabled = true;
            
            textBox_outputPath.ReadOnly = false;
            numericUpDown1.ReadOnly = false;
            numericUpDown1.Enabled = true;
            //button_modCrateMenu.Enabled = true;
            menuStrip1.Enabled = true;
            ModCrateBox.Enabled = true;
            DragDrop += ModLoaderForm_DragDrop;
            DragEnter += ModLoaderForm_DragEnter;

            if (ModProgram.Modder != null)
            {
                button_openModMenu.Enabled = ModProgram.Modder.ModMenuEnabled;
                button_LevelEditor.Enabled = false;//ModProgram.Game.EnableLevelEditor;
            }
            else
            {
                button_openModMenu.Enabled = button_LevelEditor.Enabled = false;
            }
            //button_modTools.Enabled = false;//true;

            if (!ModProgram.GamePreloaded)
                //ModProgram.Modder.CanPreloadGame && (ModProgram.Modder.PreloadConsoles == null || ModProgram.Modder.PreloadConsoles.Contains(ModProgram.Pipeline.Metadata.Console)))
            {
                button_preloadGame.Enabled = true;
            }
            else
            {
                button_preloadGame.Enabled = false;
            }

            if (ModProgram.Modder != null && !string.IsNullOrWhiteSpace(ModProgram.Game.API_Link))
            {
                linkLabel_apiCredit.Enabled = true;
            }
            linkLabel_programTitle.Enabled = true;
        }

        void ResetGameSpecific(object sender, EventValueArgs<bool> e)
        {
            bool ClearGameText = e.Value;
            //button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            button_openModMenu.Text = ModLoaderText.ModMenuButton;

            button_startProcess.Enabled = false;

            button_openModMenu.Enabled = button_openModMenu.Visible = false;
            //button_modCrateMenu.Enabled = button_modCrateMenu.Visible = false;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = false;//button_modTools.Visible = button_modTools.Enabled = false;
            numericUpDown1.Enabled = numericUpDown1.Visible = false;
            button_preloadGame.Visible = true;
            button_preloadGame.Enabled = !ModProgram.GamePreloaded;
            ModCrateBox.Visible = ModCrateBox.Enabled = false;

            linkLabel_apiCredit.Text = string.Empty;
            linkLabel_apiCredit.LinkVisited = false;
            if (ClearGameText)
            {
                label_gameType.Text = string.Empty;
            }

            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;

            checkedListBox1.Visible = checkedListBox1.Enabled = false;

            int Height = 500;
            AdjustSize(Height);
        }

        void SetLayoutUnsupportedGame(object sender, EventValueArgs<string> e)
        {
            string cons_mod = e.Value;
            //button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            checkedListBox1.Items.Clear();
            button_openModMenu.Visible = true;
            button_openModMenu.Enabled = false;
            //button_modCrateMenu.Enabled = button_modCrateMenu.Visible = true;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = button_openModMenu.Visible = button_openModMenu.Enabled = button_LevelEditor.Enabled = button_LevelEditor.Visible = false;// button_modTools.Visible = false;
            numericUpDown1.Enabled = numericUpDown1.Visible = false;
            //button_modTools.Enabled = false;
            button_preloadGame.Visible = true;
            button_preloadGame.Enabled = !ModProgram.GamePreloaded;
            ModCrateBox.Visible = ModCrateBox.Enabled = true;
            ModCrateBox.PopulateList();

            label_gameType.Text = ModLoaderText.UnsupportedGameTitle + " (" + cons_mod + ")";
            linkLabel_apiCredit.Text = string.Empty;
            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;

            int height = 500;//295 + 45 + (checkedListBox1.Items.Count * 17);
            checkedListBox1.Visible = checkedListBox1.Enabled = checkedListBox1.Items.Count > 0;
            AdjustSize(height);
        }
        public void SetLayoutSupportedGame(object sender, EventGameDetails e)
        {
            Game game = e.Game;
            string cons_mod = e.Console;
            string region_mod = e.Region;
            //button_modCrateMenu.Text = ModLoaderText.ModCratesButton;
            checkedListBox1.Items.Clear();

            button_openModMenu.Visible = true;
            button_openModMenu.Enabled = ModProgram.Modder.ModMenuEnabled;
            button_LevelEditor.Visible = false; //true;
            button_LevelEditor.Enabled = false; //ModProgram.Game.EnableLevelEditor;
            //button_modCrateMenu.Visible = true;
            //button_modCrateMenu.Enabled = !game.ModCratesDisabled;
            button_randomizeSeed.Enabled = button_randomizeSeed.Visible = true; //button_modTools.Visible = button_downloadMods.Visible = true;
            ModCrateBox.Visible = ModCrateBox.Enabled = true;
            ModCrateBox.PopulateList();

            if (!ModProgram.GamePreloaded)
              //&& ModProgram.Modder.CanPreloadGame && (ModProgram.Modder.PreloadConsoles == null || ModProgram.Modder.PreloadConsoles.Contains(ModProgram.Pipeline.Metadata.Console)))
            {
                button_preloadGame.Enabled = true;
            }
            else
            {
                button_preloadGame.Enabled = false;
            }

            numericUpDown1.Enabled = numericUpDown1.Visible = true;
            linkLabel_optionDesc.Text = string.Empty;
            linkLabel_optionDesc.Visible = false;
            panel_desc.Visible = false;
            //button_modTools.Enabled = false;//true;

            if (string.IsNullOrWhiteSpace(region_mod))
            {
                label_gameType.Text = string.Format("{0}\n({1})", game.Name, cons_mod);
            }
            else
            {
                label_gameType.Text = string.Format("{0}\n({1} {2})", game.Name, region_mod, cons_mod);
            }

            if (!string.IsNullOrWhiteSpace(game.API_Credit))
            {
                linkLabel_apiCredit.Text = game.API_Credit;
                if (!string.IsNullOrWhiteSpace(game.API_Link))
                {
                    linkLabel_apiCredit.Enabled = true;
                }
                else
                {
                    linkLabel_apiCredit.Enabled = false;
                }
            }
            else
            {
                linkLabel_apiCredit.Text = ModLoaderText.GameHasNoAPI;
                linkLabel_apiCredit.Enabled = false;
            }

            if (ModProgram.Modder.Props.Count > 0)
            {
                Dictionary<uint, int> PropOrder = new Dictionary<uint, int>();
                uint maxIndex = 0;

                for (int i = 0; i < ModProgram.Modder.Props.Count; i++)
                {
                    ModPropertyBase prop = ModProgram.Modder.Props[i];
                    if (prop is ModPropOption option && option.Allowed(ModProgram.Pipeline.Metadata.Console, ModProgram.Modder.GameRegion.Region) && !option.ModMenuOnly)
                    {
                        if (option.ListIndex != null)
                        {
                            PropOrder.Add((uint)option.ListIndex, i);
                            if ((uint)option.ListIndex > maxIndex)
                            {
                                maxIndex = (uint)option.ListIndex;
                            }
                        }
                    }
                }

                if (PropOrder.Count > 0)
                {
                    uint iter = 0;
                    while (iter < maxIndex + 1)
                    {
                        if (PropOrder.ContainsKey(iter))
                        {
                            ModPropOption option = (ModPropOption)ModProgram.Modder.Props[PropOrder[iter]];
                            checkedListBox1.Items.Add(option, option.Value != 0);
                        }
                        iter++;
                    }
                }

                foreach (var prop in ModProgram.Modder.Props)
                {
                    if (prop is ModPropOption option && option.Allowed(ModProgram.Pipeline.Metadata.Console, ModProgram.Modder.GameRegion.Region) && !option.ModMenuOnly)
                    {
                        if (option.ListIndex == null)
                        {
                            checkedListBox1.Items.Add(option, option.Value != 0);
                        }
                    }
                }
            }

            int height = 500;//295 + 45 + (checkedListBox1.Items.Count * 17);
            checkedListBox1.Visible = checkedListBox1.Enabled = checkedListBox1.Items.Count > 0;
            AdjustSize(height);
        }

        void UpdateProcessText(string msg)
        {
            label_processText.Text = msg;
        }
        void UpdateProcessText(object sender, EventValueArgs<string> e)
        {
            label_processText.Text = e.Value;
        }

        void UpdateProcessProgress(object sender, EventValueArgs<int> e)
        {
            progressBar1.Value = e.Value;
        }

        public void SetProcessStartAllowed(object sender, EventValueArgs<bool> e)
        {
            bool allow = e.Value;
            if (ModProgram.OutputPath == "")
            {
                UpdateProcessText(ModLoaderText.Step2Text);
                allow = false;
            }
            if (ModProgram.HasProcessFinished)
            {
                allow = false;
            }
            button_startProcess.Enabled = allow;
            if (allow)
            {
                UpdateProcessText(ModLoaderText.ProcessReady);
            }
        }

        public void UpdateModMenuChangeState(object sender, EventValueArgs<bool> e)
        {
            bool changed = e.Value;
            string name = ModLoaderText.ModMenuButton;
            if (ModProgram.GamePreloaded)
            {
                name += "+";
            }
            if (changed)
            {
                name = "*" + name;
            }
            button_openModMenu.Text = name;
        }

        void UpdateModCrateChangedState(object sender, EventArgs e)
        {
            string CratesActive = ModLoaderText.ModCratesButton;
            int ModsActive = ModProgram.ModsActiveAmount;
            if (ModsActive > 0)
            {
                CratesActive += $" ({ ModsActive }x)";
            }

            //button_modCrateMenu.Text = CratesActive;
        }

        [DllImport("user32.dll")]
        public static extern int FlashWindow(IntPtr Hwnd, bool Revert);

        void Notify_ProcessFinished(object sender, EventArgs e)
        {
            FlashWindow(Handle, false);
            SystemSounds.Beep.Play();
        }

        private void button_downloadMods_Click(object sender, EventArgs e)
        {
            if (!ModProgram.GamePreloaded)
            {
                if (MessageBox.Show(ModLoaderText.Popup_PreloadGame, ModLoaderText.Button_PreloadGame, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ModProgram.StartProcess(true);
                }
            }
        }

        private void checkedListBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            CheckedListBox c = sender as CheckedListBox;
            int index = c.IndexFromPoint(e.Location);
            if (index < 0) return;
            if (index != c.SelectedIndex)
                c.SelectedIndex = index;

            if (c.Items[index] is ModPropOption option)
            {
                linkLabel_optionDesc.Text = option.Description;
                linkLabel_optionDesc.Visible = panel_desc.Visible = !string.IsNullOrEmpty(option.Description);
            }
        }

        public void DisplayError(object sender, EventValueArgs<string> e)
        {
            MessageBox.Show(e.Value);
        }

        private void button_LevelEditor_Click(object sender, EventArgs e)
        {
            if (!ModProgram.GamePreloaded)
            {
                MessageBox.Show("You must preload the game to open the Level Editor.");
                return;
            }

            LevelEditor Editor = new LevelEditor(ModProgram);
            Editor.Owner = this;
            Editor.Show();
            return;
        }
    }
}
