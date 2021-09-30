using System;

namespace TwinsaityEditor
{
    public partial class LibManager
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
        private TwinsanityEditorForm.Libr[] Library;
        private void LibManager_Load(object sender, EventArgs e)
        {
        }
    }
}
