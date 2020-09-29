using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    public partial class TextDisplayForm : Form
    {
        public enum TextDisplayType
        {
            Changelog = 0,
            Credits,
            Games,
        }

        private TextDisplayType DispType = TextDisplayType.Changelog;

        public TextDisplayForm(TextDisplayType type)
        {
            InitializeComponent();
            splitContainer1.IsSplitterFixed = true;

            DispType = type;
            UpdateText();
        }

        void UpdateText()
        {
            switch (DispType)
            {
                default:
                case TextDisplayType.Changelog:
                    Text = ModLoaderText.Changelog;
                    richTextBox1.Text = Properties.Resources.Changelog;
                    break;
                case TextDisplayType.Games:
                    Text = ModLoaderText.GamesFeatureList;
                    richTextBox1.Text = Properties.Resources.Games;
                    break;
                case TextDisplayType.Credits:
                    Text = ModLoaderText.ProgramCredits;
                    richTextBox1.Text = Properties.Resources.Readme;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextDisplayForm_Load(object sender, EventArgs e)
        {
            Owner.Enabled = false;
        }

        private void TextDisplayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
