using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace CrateModLoader
{
    public partial class ModLoaderForm : Form
    {
        public ModLoaderForm()
        {
            InitializeComponent();
            linkLabel2.Text = "Crate Mod Loader " + Program.ModProgram.releaseVersionString;
            label7.Text = "";
            linkLabel1.Text = "";
            label6.Text = "Waiting for input (1) ...";
            button3.Enabled = false;
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
            Program.ModProgram.textbox_input_path = textBox1;
            Program.ModProgram.textbox_output_path = textBox2;
            Program.ModProgram.textbox_rando_seed = numericUpDown1;
            Program.ModProgram.button_modMenu = button_openModMenu;
            Program.ModProgram.button_modCrateMenu = button_modCrateMenu;
            Program.ModProgram.checkbox_fromFolder = checkBox_loadFromFolder;
            Program.ModProgram.checkbox_toFolder = checkBox_saveToFolder;
            Program.ModProgram.asyncWorker = backgroundWorker1;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            Random rand = new Random();
            int Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            Program.ModProgram.randoSeed = Seed;

            openFileDialog1.FileName = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.ModProgram.inputDirectoryMode)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.ModProgram.OpenROM_Selection = OpenROM_SelectionType.Any;
                    Program.ModProgram.inputISOpath = folderBrowserDialog1.SelectedPath + @"\";
                    Program.ModProgram.CheckISO();
                    textBox1.Text = Program.ModProgram.inputISOpath;
                }
            }
            else
            {
                openFileDialog1.Filter = "PSX/PS2/PSP/GCN/WII/XBOX ROM (*.iso; *.bin; *.wbfs)|*.iso;*.bin;*.wbfs|All files (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.ModProgram.OpenROM_Selection = (OpenROM_SelectionType)openFileDialog1.FilterIndex;
                    Program.ModProgram.inputISOpath = openFileDialog1.FileName;
                    Program.ModProgram.CheckISO();
                    textBox1.Text = Program.ModProgram.inputISOpath;
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.ModProgram.outputDirectoryMode)
            {
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                {
                    Program.ModProgram.outputISOpath = folderBrowserDialog2.SelectedPath + @"\";
                    textBox2.Text = Program.ModProgram.outputISOpath;
                    Program.ModProgram.outputPathSet = true;
                    if (Program.ModProgram.loadedISO && Program.ModProgram.outputPathSet)
                    {
                        button3.Enabled = true;
                        label6.Text = "Ready!";
                    }
                    else
                    {
                        button3.Enabled = false;
                    }
                }
            }
            else
            {
                saveFileDialog1.Filter = "ISO (*.iso)|*.iso|BIN (*.bin)|*.bin|All files (*.*)|*.*";

                saveFileDialog1.FileName = "ModdedGame.iso";
                saveFileDialog1.ShowDialog();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.ModProgram.randoSeed = int.Parse(numericUpDown1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int Seed = rand.Next(0,int.MaxValue);
            numericUpDown1.Value = Seed;
            Program.ModProgram.randoSeed = Seed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Program.ModProgram.outputISOpath = saveFileDialog1.FileName;
            textBox2.Text = Program.ModProgram.outputISOpath;
            Program.ModProgram.outputPathSet = true;
            if (Program.ModProgram.loadedISO && Program.ModProgram.outputPathSet)
            {
                button3.Enabled = true;
                label6.Text = "Ready!";
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Program.ModProgram.outputISOpath = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.ModProgram.DisableInteraction();

            if (checkedListBox1.CheckedItems.Count <= 0 && ModCrates.ModsActiveAmount <= 0)
            {
                if (MessageBox.Show("No options specified - Output ROM will only display version info ingame if available. Proceed?", "No Options Selected", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.keepTempFiles = checkBox1.Checked;
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
                MessageBox.Show("Unable to open link.");
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
                            checkBox_loadFromFolder.Checked = true;
                            Program.ModProgram.UpdateInputSetting();
                            Program.ModProgram.inputISOpath = fileList[0] + @"\";
                        }
                        else
                        {
                            Program.ModProgram.inputDirectoryMode = false;
                            checkBox_loadFromFolder.Checked = false;
                            Program.ModProgram.UpdateInputSetting();
                            Program.ModProgram.inputISOpath = fileList[0];
                        }
                        Program.ModProgram.OpenROM_Selection = OpenROM_SelectionType.Any;
                        Program.ModProgram.CheckISO();
                        textBox1.Text = Program.ModProgram.inputISOpath;
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

        private void checkBox_loadFromFolder_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateInputSetting();
        }

        private void checkBox_saveToFolder_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateOutputSetting();
        }
    }
}
