using System;

namespace TwinsaityEditor
{
    public partial class Search
    {
        public int SIndex;
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            SIndex = SearchID.SelectedIndex;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(System.Object sender, System.EventArgs e)
        {
            SIndex = SearchID.SelectedIndex;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
