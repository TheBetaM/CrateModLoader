using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace TwinsaityEditor
{
    public partial class IDAsker
    {
        public uint ID;
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                ID = uint.Parse(TextBox1.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Cannot convert to uint");
            }
        }

        public ref TextBox Get_IDBox()
        {
            return ref TextBox1;
        }
    }
}
