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
    public partial class ModLoaderForm : Form
    {
        public ModLoaderForm()
        {
            InitializeComponent();
            label5.Text = "";
            label6.Text = "Waiting for input ISO...";
            label7.Text = "";
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
            Program.ModProgram.textbox_output_path = textBox2;
            Program.ModProgram.textbox_rando_seed = numericUpDown1;
            Program.ModProgram.button_modMenu = button_openModMenu;
            Program.ModProgram.button_modCrateMenu = button_modCrateMenu;

            progressBar1.Minimum = 1;
            progressBar1.Maximum = 4;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            Random rand = new Random();
            int Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            Program.ModProgram.randoSeed = Seed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog1.Filter = "PS2/PSP ISO (*.iso)|*.iso|GC ISO (*.iso)|*.iso|PSX BIN (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Program.ModProgram.inputISOpath = openFileDialog1.FileName;
                Program.ModProgram.CheckISO();
                textBox1.Text = Program.ModProgram.inputISOpath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog1.Filter = "ISO (*.iso)|*.iso|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.FileName = "CTTR_Randomized.iso";
            saveFileDialog1.ShowDialog();
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Program.ModProgram.targetGame)
            {
                case ModLoader.GameType.CTTR:
                case ModLoader.GameType.Twins:
                case ModLoader.GameType.CNK:
                case ModLoader.GameType.Titans:
                case ModLoader.GameType.MoM:
                case ModLoader.GameType.Crash1:
                case ModLoader.GameType.Crash2:
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        Program.ModProgram.OptionChanged(i, checkedListBox1.GetItemChecked(i));
                    }
                    break;
            }
            checkedListBox1.ClearSelected();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.ModProgram.DisableInteraction();

            if (checkedListBox1.CheckedItems.Count <= 0)
            {
                DialogResult dialogResult = MessageBox.Show("No options specified - Output ISO will be exactly the same as the input! Proceed?", "No Options Selected", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Program.ModProgram.StartButtonPressed();
                }
                else if (dialogResult == DialogResult.No)
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
            // TODO: Mod Crate Manager Window: 
            // Either a checkbox list of .zip files in a mod directory OR
            // A list with a button that lets you manually add .zip files
        }

        private void button_openModMenu_Click(object sender, EventArgs e)
        {
            // TODO: Individual Game Mod Menu
            // Detailed settings UI for some games
            // Set availability in ModProgram.SetGameType
        }
    }
}
