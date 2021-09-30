using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace TwinsaityEditor
{
    public partial class InstanceScripting
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("POINT|X|Y|Z|ID - Places object ID in coordinates (X;Y;Z)" + Strings.Chr(13) + Strings.Chr(10) + "FILL|X|Y|Z|W|H|L|StX|StY|StZ|ID - Fills area from (X;Y;Z) to (X+W;Y+H;Z+L) with step (StX;StY;StZ) with object ID" + Strings.Chr(13) + Strings.Chr(10) + "SET|VAR|VAL - Set parameter VAR to VAL. Parameters saves until you restore they to default by SET|CLEAR. You can copy setup from another instance by SET|COPY|INST_ID. List of VAR|VAL:" + Strings.Chr(13) + Strings.Chr(10) + "SET|ROTX|VAL - Set rotation. Same for Y and Z. SET|ROT|VALX|VALY|VALZ - Set rotation for three axis." + Strings.Chr(13) + Strings.Chr(10) + "SET|SOME1|N|VAL1|..|VALN - Set something1 size to N and set all elements for it. Same for 2 and 3." + Strings.Chr(13) + Strings.Chr(10) + "SET|PARAMHEAD|VAL and SET|PARAMNUMBER|VAL. PARAMETERHEAD is XX YY ZZ 00 int Little endian value, where XX - Length of Param1 array, YY - Param2 Length and ZZ - Param3 Length" + Strings.Chr(13) + Strings.Chr(10) + "SET|PARAM1|N|VAL1|..|VALN - Set parameters1 size to N and set all elements for it. Same for 2 and 3.");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (OpenTXT.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream File = new System.IO.FileStream(OpenTXT.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader Reader = new System.IO.StreamReader(File);
                TextBox1.Text = Reader.ReadToEnd();
                File.Close();
            }
        }

        public ref TextBox Get_ScriptBox()
        {
            return ref TextBox1;
        }
    }
}
