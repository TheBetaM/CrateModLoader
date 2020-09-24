using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrateModLoader.Resources.Text;

namespace CrateModLoader
{
    public partial class ModCrateMakerForm : Form
    {

        private ModCrate crate;
        private Modder mod;
        private Game Game;
        private string path;

        public ModCrateMakerForm(Modder inmod, Game ingame, string outpath)
        {
            InitializeComponent();

            mod = inmod;
            Game = ingame;
            path = outpath;

            button_save.Text = ModLoaderText.ModCrateMaker_Button_Save;
            button_browse.Text = ModLoaderText.ModCrateMaker_Button_Browse;
            button_cancel.Text = ModLoaderText.ModCrateMaker_Button_Cancel;
            label_name.Text = ModLoaderText.ModCrateMaker_Label_Name;
            label_author.Text = ModLoaderText.ModCrateMaker_Label_Author;
            label_description.Text = ModLoaderText.ModCrateMaker_Label_Description;
            label_version.Text = ModLoaderText.ModCrateMaker_Label_Version;
            label_icon.Text = ModLoaderText.ModCrateMaker_Label_Icon;
            Text = ModLoaderText.ModCrateMakerTitle;

            crate = new ModCrate();

            textBox_author.Text = crate.Author;
            textBox_description.Text = crate.Desc;
            textBox_name.Text = crate.Name;
            textBox_version.Text = crate.Version;

            crate.HasIcon = false;
            crate.TargetGame = Game.ShortName;
            crate.HasSettings = true;
            crate.IsFolder = false;


        }

        private void button_save_Click(object sender, EventArgs e)
        {
            mod.SaveSimpleCrateToFile(path, crate);

            Close();
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = string.Format("{0}|*.*", "All files");
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Bitmap img = new Bitmap(openFileDialog1.FileName);

                if (img != null)
                {
                    crate.HasIcon = true;
                    crate.IconPath = openFileDialog1.FileName;
                    //crate.Icon = img;
                    pictureBox1.Image = img;
                }

            }
        }

        private void textBox_name_TextChanged(object sender, EventArgs e)
        {
            crate.Name = textBox_name.Text;
        }

        private void textBox_author_TextChanged(object sender, EventArgs e)
        {
            crate.Author = textBox_author.Text;
        }

        private void textBox_version_TextChanged(object sender, EventArgs e)
        {
            crate.Version = textBox_version.Text;
        }

        private void textBox_description_TextChanged(object sender, EventArgs e)
        {
            crate.Desc = textBox_description.Text;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ModCrateMakerForm_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void ModCrateMakerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }
    }
}
