using Microsoft.VisualBasic;
using System;

namespace TwinsaityEditor
{
    public partial class TwoIDAsker
    {
        public uint ID1, ID2;
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                ID1 = uint.Parse(TextBox1.Text);
                ID2 = uint.Parse(TextBox2.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Cannot convert to uint");
            }
        }

        private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
