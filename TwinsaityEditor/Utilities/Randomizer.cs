using Microsoft.VisualBasic;
using System;

namespace TwinsaityEditor
{
    public partial class Randomizer
    {
        public int StartIndex, EndIndex, Cycles;
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                StartIndex = int.Parse(TextBox1.Text);
                EndIndex = int.Parse(TextBox2.Text);
                Cycles = int.Parse(TextBox3.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Cannot conver to numbers");
            }
        }

        private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
