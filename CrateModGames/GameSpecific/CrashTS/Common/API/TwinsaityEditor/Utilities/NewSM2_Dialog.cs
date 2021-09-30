using System;

namespace TwinsaityEditor
{
    public partial class NewSM2_Dialog
    {
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void Dialog1_Load(object sender, EventArgs e)
        {
        }
    }
}
