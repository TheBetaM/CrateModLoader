using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using CrateModLoader.Resources.Text;
using CrateModLoader.Tools;

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
            button3.Enabled = false;
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
            button4.Text = ModLoaderText.RandomizeSeedButton;
            button1.Text = ModLoaderText.InputBrowse;
            button2.Text = ModLoaderText.OutputBrowse;
            button3.Text = ModLoaderText.StartProcessButton;

            Size = new System.Drawing.Size(MinimumSize.Width, MinimumSize.Height);

            toolTip1.SetToolTip(linkLabel2, ModLoaderText.Tooltip_Label_Version);
            toolTip1.SetToolTip(linkLabel1, ModLoaderText.Tooltip_Label_API);
            toolTip1.SetToolTip(numericUpDown1, ModLoaderText.Tooltip_Numeric_Seed);
            toolTip1.SetToolTip(button_downloadMods, ModLoaderText.Tooltip_Button_DownloadMods);
            toolTip1.SetToolTip(button_modCrateMenu, ModLoaderText.Tooltip_Button_ModCrates);
            toolTip1.SetToolTip(button_openModMenu, ModLoaderText.Tooltip_Button_ModMenu);
            toolTip1.SetToolTip(button_modTools, ModLoaderText.Tooltip_Button_ModTools);
            toolTip1.SetToolTip(button4, ModLoaderText.Tooltip_Button_RandomizeSeed);

            Program.ModProgram.panel_optionDesc = panel_desc;
            Program.ModProgram.text_optionDescLabel = linkLabel_optionDesc;
            Program.ModProgram.processText = label6;
            Program.ModProgram.progressBar = progressBar1;
            Program.ModProgram.startButton = button3;
            Program.ModProgram.text_gameType = label7;
            Program.ModProgram.text_titleLabel = linkLabel2;
            Program.ModProgram.text_apiLabel = linkLabel1;
            Program.ModProgram.list_modOptions = checkedListBox1;
            Program.ModProgram.main_form = this;
            Program.ModProgram.image_gameIcon = pictureBox1;
            Program.ModProgram.button_browse1 = button1;
            Program.ModProgram.button_browse2 = button2;
            Program.ModProgram.button_randomize = button4;
            Program.ModProgram.button_modTools = button_modTools;
            Program.ModProgram.button_downloadMods = button_downloadMods;
            Program.ModProgram.textbox_input_path = textBox1;
            Program.ModProgram.textbox_output_path = textBox2;
            Program.ModProgram.textbox_rando_seed = numericUpDown1;
            Program.ModProgram.button_modMenu = button_openModMenu;
            Program.ModProgram.button_modCrateMenu = button_modCrateMenu;
            Program.ModProgram.asyncWorker = backgroundWorker1;
            Program.ModProgram.asyncTimer = timer1;
            timer1.Enabled = false;
            timer1.Interval = 500;
            Program.ModProgram.formHandle = this.Handle;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            Random rand = new Random();
            int Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;

            openFileDialog1.FileName = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.ModProgram.inputDirectoryMode)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.ModProgram.OpenROM_Selection = ModLoader.OpenROM_SelectionType.Any;
                    ModLoaderGlobals.InputPath = folderBrowserDialog1.SelectedPath + @"\";
                    Program.ModProgram.CheckISO();
                    textBox1.Text = ModLoaderGlobals.InputPath;
                }
            }
            else
            {
                //openFileDialog1.Filter = "PSX/PS2/PSP/GCN/WII/XBOX/360 ROM (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|All files (*.*)|*.*";
                openFileDialog1.Filter = ModLoaderText.InputDialogTypeAuto + " (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|" + ModLoaderText.OutputDialogTypeAllFiles + " (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.ModProgram.OpenROM_Selection = (ModLoader.OpenROM_SelectionType)openFileDialog1.FilterIndex;
                    ModLoaderGlobals.InputPath = openFileDialog1.FileName;
                    Program.ModProgram.CheckISO();
                    textBox1.Text = ModLoaderGlobals.InputPath;
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.ModProgram.outputDirectoryMode)
            {
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                {
                    ModLoaderGlobals.OutputPath = folderBrowserDialog2.SelectedPath + @"\";
                    textBox2.Text = ModLoaderGlobals.OutputPath;
                    Program.ModProgram.outputPathSet = true;
                    if (Program.ModProgram.loadedISO && Program.ModProgram.outputPathSet)
                    {
                        button3.Enabled = true;
                        label6.Text = ModLoaderText.ProcessReady;
                    }
                    else
                    {
                        button3.Enabled = false;
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int Seed = rand.Next(0,int.MaxValue);
            numericUpDown1.Value = Seed;
            ModLoaderGlobals.RandomizerSeed = Seed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ModLoaderGlobals.OutputPath = saveFileDialog1.FileName;
            textBox2.Text = ModLoaderGlobals.OutputPath;
            Program.ModProgram.outputPathSet = true;
            if (Program.ModProgram.loadedISO && Program.ModProgram.outputPathSet)
            {
                button3.Enabled = true;
                label6.Text = ModLoaderText.ProcessReady;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ModLoaderGlobals.OutputPath = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.ModProgram.DisableInteraction();

            if (checkedListBox1.CheckedItems.Count <= 0 && ModCrates.ModsActiveAmount <= 0 && !button_openModMenu.Text.EndsWith("*"))
            {
                if (MessageBox.Show(ModLoaderText.NoOptionsSelectedPopup, ModLoaderText.NoOptionsSelectedTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Program.ModProgram.StartButtonPressed();
                }
                else
                {
                    Program.ModProgram.EnableInteraction();
                }
            }
            else
            {
                Program.ModProgram.StartButtonPressed();
            }
        }

        private void button_modCrateMenu_Click(object sender, EventArgs e)
        {
            Program.ModProgram.OpenModCrateManager();
        }

        private void button_openModMenu_Click(object sender, EventArgs e)
        {
            Program.ModProgram.OpenModMenu();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox c = sender as CheckedListBox;
            for (int i = 0; i < c.Items.Count; ++i)
            {
                if (c.Items[i] is ModOption o)
                {
                    o.Enabled = c.GetItemChecked(i);
                    if (c.SelectedIndex == i)
                    {
                        if (!string.IsNullOrEmpty(o.Description))
                        {
                            linkLabel_optionDesc.Text = o.Description;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Program.ModProgram.API_Link_Clicked();
            }
            catch
            {
                MessageBox.Show(ModLoaderText.LinkOpenFail);
            }
        }

        private void ModLoaderForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!Program.ModProgram.processActive)
            {
                try
                {
                    string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    if (fileList.Length == 1)
                    {
                        if (Directory.Exists(fileList[0]))
                        {
                            Program.ModProgram.inputDirectoryMode = true;
                            Program.ModProgram.UpdateInputSetting(true);
                            ModLoaderGlobals.InputPath = fileList[0] + @"\";
                        }
                        else
                        {
                            Program.ModProgram.inputDirectoryMode = false;
                            Program.ModProgram.UpdateInputSetting(false);
                            ModLoaderGlobals.InputPath = fileList[0];
                        }
                        Program.ModProgram.OpenROM_Selection = ModLoader.OpenROM_SelectionType.Any;
                        Program.ModProgram.CheckISO();
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
            if (!Program.ModProgram.processActive)
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
                Program.ModProgram.Async_BuildFinished();
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
            Program.ModProgram.ShowText_Credits();
        }

        private void toolStripMenuItem_showGames_Click(object sender, EventArgs e)
        {
            Program.ModProgram.ShowText_Games();
        }

        private void toolStripMenuItem_showChangelog_Click(object sender, EventArgs e)
        {
            Program.ModProgram.ShowText_Changelog();
        }

        private void toolStripMenuItem_loadROM_Click(object sender, EventArgs e)
        {
            Program.ModProgram.inputDirectoryMode = false;
            Program.ModProgram.UpdateInputSetting(false);
            button1_Click(null, null);
        }

        private void toolStripMenuItem_loadFolder_Click(object sender, EventArgs e)
        {
            Program.ModProgram.inputDirectoryMode = true;
            Program.ModProgram.UpdateInputSetting(true);
            button1_Click(null, null);
        }

        private void toolStripMenuItem_saveROM_Click(object sender, EventArgs e)
        {
            Program.ModProgram.outputDirectoryMode = false;
            Program.ModProgram.UpdateOutputSetting(false);
            button2_Click(null, null);
        }

        private void toolStripMenuItem_saveFolder_Click(object sender, EventArgs e)
        {
            Program.ModProgram.outputDirectoryMode = true;
            Program.ModProgram.UpdateOutputSetting(true);
            button2_Click(null, null);
        }

        private void toolStripMenuItem_keepTempFiles_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.keepTempFiles = toolStripMenuItem_keepTempFiles.Checked;
        }
    }
}
