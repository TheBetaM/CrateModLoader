using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinsaityEditor.Properties;
using WK.Libraries.BetterFolderBrowserNS;

namespace TwinsaityEditor.Workers
{
    public partial class ImageMaker : Form
    {

        private enum GenerationState
        {
            Idle,
            PackBD,
            ImageGeneration,
        }

        private GenerationState gen_state = GenerationState.Idle;
        private BDExplorer BDExplorer = null;

        public ImageMaker()
        {
            InitializeComponent();
            Show();
        }

        private void ImageMaker_Load(object sender, EventArgs e)
        {
            BDExplorer = new BDExplorer();
            BDExplorer.Hide();
            tbOutputPath.Enabled = false;
            tbTwinsanityPath.Enabled = false;
            tspbGenerationProgress.Maximum = 100;
            if (Directory.Exists(Settings.Default.TwinsUnpackedPath))
            {
                tbTwinsanityPath.Text = Settings.Default.TwinsUnpackedPath;
            }
            tbImageName.Text = Settings.Default.ImageName;
            if (Directory.Exists(Settings.Default.ImageOutputPath))
            {
                tbOutputPath.Text = Settings.Default.ImageOutputPath;
            }
            if (Directory.Exists(Settings.Default.PCSX2Path))
            {
                tbPcsx2Path.Text = Settings.Default.PCSX2Path;
            }
        }

        private void btnSelectTwinsPath_Click(object sender, EventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = Settings.Default.TwinsUnpackedPath,
                Title = "Select Twinsanity folder"
            })
            {
                if (bfb.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.TwinsUnpackedPath = bfb.SelectedFolder;
                    tbTwinsanityPath.Text = bfb.SelectedFolder;
                }
            }
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = Settings.Default.ImageOutputPath,
                Title = "Select Twinsanity folder"
            })
            {
                if (bfb.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.ImageOutputPath = bfb.SelectedFolder;
                    tbOutputPath.Text = bfb.SelectedFolder;
                }
            }
        }

        private void tbImageName_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ImageName = tbImageName.Text;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DisableInterface();
            if (cbPackAndCopy.Checked)
            {
                gen_state = GenerationState.PackBD;
                tsslblStatus.Text = "Status: Generating archives";
                PackArchives();
            }
            else
            {
                ArchivesPacked();
            }
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gen_state == GenerationState.PackBD) return;

            var progress = Externals.PS2ImageMaker.PS2ImageMaker.PollProgress();
            tspbGenerationProgress.Value = (int)(progress.ProgressPercentage * 100);
            switch (progress.ProgressS)
            {
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.FAILED:
                    timer1.Enabled = false;
                    EnableInterface();
                    tsslblStatus.Text = "Status: Failed";
                    tspbGenerationProgress.Value = 0;
                    break;
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.ENUM_FILES:
                    tsslblStatus.Text = "Status: Enumerating files in directory";
                    break;
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.WRITE_SECTORS:
                    tsslblStatus.Text = "Status: Writing sectors";
                    break;
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.WRITE_FILES:
                    tsslblStatus.Text = "Status: Writing file " + progress.File;
                    break;
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.WRITE_END:
                    tsslblStatus.Text = "Status: Writing special sectors";
                    break;
                case Externals.PS2ImageMaker.PS2ImageMaker.ProgressState.FINISHED:
                    timer1.Enabled = false;
                    EnableInterface();
                    tsslblStatus.Text = "Status: Finished";
                    tspbGenerationProgress.Value = 0;
                    if (cbOpenOutPath.Checked)
                    { // Open output path on finish
                        Process.Start("explorer.exe", tbOutputPath.Text);
                    }
                    if (cbRun.Checked)
                    { // Open output path on finish
                        string PCSXFolder = Settings.Default.PCSX2Path;
                        string[] files = System.IO.Directory.GetFiles(PCSXFolder, "pcsx2.exe");
                        if (files.Length > 0)
                        {
                            Process.Start(files[0], $"\"{Path.Combine(tbOutputPath.Text, $"{Settings.Default.ImageName}.iso")}\"");
                        }
                    }
                    break;
            }
        }

        internal void EnableInterface()
        {
            btnGenerate.Enabled = true;
            cbOpenOutPath.Enabled = true;
            cbPackAndCopy.Enabled = true;
            cbRun.Enabled = true;
            btnOutputPath.Enabled = true;
            btnSelectTwinsPath.Enabled = true;
            tbImageName.Enabled = true;
        }

        internal void DisableInterface()
        {
            btnGenerate.Enabled = false;
            cbOpenOutPath.Enabled = false;
            cbPackAndCopy.Enabled = false;
            cbRun.Enabled = false;
            btnOutputPath.Enabled = false;
            btnSelectTwinsPath.Enabled = false;
            tbImageName.Enabled = false;
        }

        internal void PackArchives()
        {
            var result = BDExplorer.PackBDArchives(tbTwinsanityPath.Text + "\\" + "Crash6", ArchivesPacked);
            if (!result)
            {
                timer1.Enabled = false;
                EnableInterface();
                tsslblStatus.Text = "Status: Failed";
                tspbGenerationProgress.Value = 0;
            }
        }

        internal void ArchivesPacked()
        {
            gen_state = GenerationState.ImageGeneration;
            var progress = Externals.PS2ImageMaker.PS2ImageMaker.StartPacking(tbTwinsanityPath.Text, tbOutputPath.Text + "\\" + tbImageName.Text + ".iso");
            tspbGenerationProgress.Value = (int)(progress.ProgressPercentage * 100);
        }

        private void btnPcsx2Path_Click(object sender, EventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = Settings.Default.PCSX2Path,
                Title = "Select PCSX2 folder"
            })
            {
                if (bfb.ShowDialog() == DialogResult.OK)
                {
                    string[] files = System.IO.Directory.GetFiles(bfb.SelectedFolder, "pcsx2.exe");
                    if (files.Length == 0)
                    {
                        MessageBox.Show("PCSX2.EXE not found!", "PCSX2 not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Settings.Default.PCSX2Path = bfb.SelectedFolder;
                        tbPcsx2Path.Text = bfb.SelectedFolder;
                    }
                }
            }
        }
    }
}
