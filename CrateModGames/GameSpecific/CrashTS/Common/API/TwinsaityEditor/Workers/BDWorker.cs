using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;

namespace TwinsaityEditor
{
    public partial class BDWorker
    {
        private Twinsanity.BDArchive Archive = new Twinsanity.BDArchive();
        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                TextBox2.Text = Archive.FormatPath(Browse.SelectedPath);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("Extract: Archive folder - WHAT extract. Content folder - to what place extract" + Strings.Chr(13) + Strings.Chr(10) + "Pack: Content folder - WHAT pack. Arhive folde - place to save new archive");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Text = "BDWorker Processing... Extracting...";
            Archive.ExtractOnce(TextBox1.Text, TextBox2.Text, TextBox3.Text);
            Application.DoEvents();
            this.Text = "BDWorker";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Text = "BDWorker Processing... Creating new table...";
            Archive.CreateTable(TextBox1.Text);
            Application.DoEvents();
            this.Text = "BDWorker Processing... Saving table...";
            Archive.SaveTable(TextBox2.Text, TextBox3.Text);
            Application.DoEvents();
            this.Text = "BDWorker Processing... Saving archive...";
            Archive.SaveArchive(TextBox2.Text, TextBox3.Text);
            Application.DoEvents();
            this.Text = "BDWorker Processing... Disposing resourses...";
            Application.DoEvents();
            Archive.Dispose();
            this.Text = "BDWorker";
        }

        public ref FolderBrowserDialog GetFolderBrowser()
        {
            return ref Browse;
        }
    }
}
