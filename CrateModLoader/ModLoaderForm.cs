using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CrateModLoader
{
    public partial class ModLoaderForm : Form
    {
        public ModLoaderForm()
        {
            InitializeComponent();
            label4.Text = "Crate Mod Loader " + Program.ModProgram.releaseVersionString;
            label5.Text = "";
            label7.Text = "";
            label6.Text = "Waiting for input..";
            button3.Enabled = false;
            Program.ModProgram.processText = label6;
            Program.ModProgram.progressBar = progressBar1;
            Program.ModProgram.startButton = button3;
            Program.ModProgram.text_gameType = label7;
            Program.ModProgram.text_optionsLabel = label5;
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
            Program.ModProgram.button_radio_FromFolder = radioButton_FromFolder;
            Program.ModProgram.button_radio_FromROM = radioButton_FromROM;
            Program.ModProgram.button_radio_ToFolder = radioButton_ToFolder;
            Program.ModProgram.button_radio_ToROM = radioButton_ToROM;
            Program.ModProgram.asyncWorker = backgroundWorker1;
            radioButton_FromROM.Checked = true;
            radioButton_ToROM.Checked = true;

            toolTip1.SetToolTip(radioButton_ToFolder, "Not supported by PS1/PS2 software");

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
                //openFileDialog1.Filter = "PS2/PSP ISO (*.iso)|*.iso|GC ISO (*.iso)|*.iso|PSX BIN (*.bin)|*.bin|WII ISO (*.iso)|*.iso|PSX/PS2 Directory (system.cnf)|*.cnf|PSP Directory (umd_data.bin)|*.bin|GC/WII Directory (opening.bnr)|*.bnr|XBOX Directory (*.xbe)|*.xbe|360 Directory (*.xex)|*.xex|All files (*.*)|*.*";
                openFileDialog1.Filter = "PSX/PS2/PSP ROM (*.iso; *.bin)|*.iso;*.bin|GC/WII ROM (*.iso; *.wbfs)|*.iso;*.wbfs|All files (*.*)|*.*";

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

            if (checkedListBox1.CheckedItems.Count <= 0)
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
            //Program.ModProgram.OpenModCrateManager();
            MessageBox.Show("Coming in a future release!", "Not available yet", MessageBoxButtons.OK);
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

        private void radioButton_FromROM_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateInputSetting();
        }

        private void radioButton_FromFolder_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateInputSetting();
        }

        private void radioButton_ToROM_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateOutputSetting();
        }

        private void radioButton_ToFolder_CheckedChanged(object sender, EventArgs e)
        {
            Program.ModProgram.UpdateOutputSetting();
        }
    }
}
