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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label5.Text = "";
            label6.Text = "Waiting for input ISO...";
            label7.Text = "";
            button3.Enabled = false;
            Program.RandoProgram.processText = label6;
            Program.RandoProgram.progressBar = progressBar1;
            Program.RandoProgram.startButton = button3;
            Program.RandoProgram.text_gameType = label7;
            Program.RandoProgram.text_optionsLabel = label5;
            Program.RandoProgram.list_modOptions = checkedListBox1;
            Program.RandoProgram.main_form = this;
            Program.RandoProgram.image_gameIcon = pictureBox1;

            progressBar1.Minimum = 1;
            progressBar1.Maximum = 4;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            Random rand = new Random();
            long Seed = rand.Next(0, int.MaxValue);
            numericUpDown1.Value = Seed;
            Program.RandoProgram.randoSeed = Seed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog1.Filter = "ISO (*.iso)|*.iso|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";

            openFileDialog1.ShowDialog();
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

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.RandoProgram.randoSeed = long.Parse(numericUpDown1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            long Seed = rand.Next(0,int.MaxValue);
            numericUpDown1.Value = Seed;
            Program.RandoProgram.randoSeed = Seed;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Program.RandoProgram.outputISOpath = saveFileDialog1.FileName;
            textBox2.Text = Program.RandoProgram.outputISOpath;
            Program.RandoProgram.outputPathSet = true;
            if (Program.RandoProgram.loadedISO && Program.RandoProgram.outputPathSet)
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
            Program.RandoProgram.outputISOpath = textBox2.Text;
        }


        public enum CTTR_Options
        {
            RandomizeHubs = 0,
            RandomizeTracks = 1,
            RandomizeMinigames = 2,
            RandomizeMissions = 3,
            AddUnusedCutscenes = 4,
            PreventSequenceBreaks = 5,
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.RandoProgram.targetGame == Randomizer.GameType.CTTR)
            {
                Program.RandoProgram.CTTR_rand_hubs = checkedListBox1.GetItemChecked((int)CTTR_Options.RandomizeHubs);
                Program.RandoProgram.CTTR_rand_tracks = checkedListBox1.GetItemChecked((int)CTTR_Options.RandomizeTracks);
                Program.RandoProgram.CTTR_rand_minigames = checkedListBox1.GetItemChecked((int)CTTR_Options.RandomizeMinigames);
                Program.RandoProgram.CTTR_rand_missions = checkedListBox1.GetItemChecked((int)CTTR_Options.RandomizeMissions);
                Program.RandoProgram.CTTR_add_unused_cutscenes = checkedListBox1.GetItemChecked((int)CTTR_Options.AddUnusedCutscenes);
                Program.RandoProgram.CTTR_add_sequence_break_checks = checkedListBox1.GetItemChecked((int)CTTR_Options.PreventSequenceBreaks);
            }
            checkedListBox1.ClearSelected();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Program.RandoProgram.inputISOpath = openFileDialog1.FileName;
            textBox1.Text = Program.RandoProgram.inputISOpath;
            Program.RandoProgram.CheckISO();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            if (checkedListBox1.CheckedItems.Count <= 0)
            {
                DialogResult dialogResult = MessageBox.Show("No options specified - Output ISO will be exactly the same as the input! Proceed?", "No Options", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Program.RandoProgram.StartButtonPressed();
                }
                else if (dialogResult == DialogResult.No)
                {
                    button3.Enabled = true;
                }
            }
            else
            {
                Program.RandoProgram.StartButtonPressed();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.RandoProgram.keepTempFiles = checkBox1.Checked;
        }
    }
}
