using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;
using Microsoft.VisualBasic.CompilerServices;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class OGIEditor
    {

        private TwinsanityEditorForm twinsanityEditorForm;

        public OGIEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public OGI OGI;
        private int T1Ind;
        private int T2Ind;
        private int T3Ind;
        private void AddGC_Click(object sender, EventArgs e)
        {
            this.OGI.GCNumber += 1;
            Array.Resize(ref this.OGI.GCI, this.OGI.GCNumber);
            this.OGI.GCI[(this.OGI.GCNumber - 1)].ID = byte.Parse(this.IDVal.Text);
            this.OGI.GCI[(this.OGI.GCNumber - 1)].GCID = uint.Parse(this.EditGCVal.Text);
            this.UpdateOGI(this.OGITree.SelectedNode.Index);
        }

        private void ApplyT2Rec_Click(object sender, EventArgs e)
        {
            this.OGI.T2[this.T2Ind] = this.ParseT2(this.OGI.T2[this.T2Ind]);
        }

        private void ApplyT3Rec_Click(object sender, EventArgs e)
        {
            this.OGI.T1[this.T1Ind] = this.ParseT1(this.OGI.T1[this.T1Ind]);
            this.OGI.T3[this.T1Ind] = this.ParseT3(this.OGI.T3[this.T1Ind]);
        }

        private void DeleteGC_Click(object sender, EventArgs e)
        {
            int num = (this.GCList.Items.Count - 2);
            int i = this.GCList.SelectedIndex;
            while ((i <= num))
            {
                this.OGI.GCI[i] = this.OGI.GCI[(i + 1)];
                i += 1;
            }
            this.OGI.GCNumber -= 1;
            Array.Resize(ref this.OGI.GCI, this.OGI.GCNumber);
            this.UpdateOGI(this.OGITree.SelectedNode.Index);
        }

        private void DelT2Rec_Click(object sender, EventArgs e)
        {
            if ((this.OGI.T2Number > 0))
            {
                int num = (this.OGI.T2Number - 2);
                int i = this.T2Ind;
                while ((i <= num))
                {
                    this.OGI.T2[i] = this.OGI.T2[(i + 1)];
                    i += 1;
                }
                this.OGI.T2Number -= 1;
                Array.Resize(ref this.OGI.T2, this.OGI.T2Number);
                this.UpdateT2();
            }
        }

        private void DelT3Rec_Click(object sender, EventArgs e)
        {
            if ((this.OGI.T1Number > 0))
            {
                int num = (this.OGI.T1Number - 2);
                int i = this.T1Ind;
                while ((i <= num))
                {
                    this.OGI.T1[i] = this.OGI.T1[(i + 1)];
                    this.OGI.T3[i] = this.OGI.T3[(i + 1)];
                    i += 1;
                }
                this.OGI.T1Number -= 1;
                Array.Resize(ref this.OGI.T1, this.OGI.T1Number);
                Array.Resize(ref this.OGI.T3, this.OGI.T1Number);
                this.UpdateT1();
                this.UpdateT3();
            }
        }


        private void EditGC_Click(object sender, EventArgs e)
        {
            this.OGI.GCI[this.GCList.SelectedIndex].ID = byte.Parse(this.IDVal.Text);
            this.OGI.GCI[this.GCList.SelectedIndex].GCID = uint.Parse(this.EditGCVal.Text);
            this.UpdateOGI(this.OGITree.SelectedNode.Index);
        }

        private void FlagsVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint num = uint.Parse(this.FlagsVal.Text);
                this.OGI.Flag1 = System.Convert.ToByte((0xFF & num));
                this.OGI.Flag2 = System.Convert.ToByte(((0xFF00 & num) >> 8));
                this.OGI.Flag3 = System.Convert.ToByte(((0xFF0000 & num) >> 0x10));
                this.OGI.Flag4 = System.Convert.ToByte(((-16777216 & num) >> 0x18));
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void GCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IDVal.Text = this.OGI.GCI[this.GCList.SelectedIndex].ID.ToString();
            this.EditGCVal.Text = this.OGI.GCI[this.GCList.SelectedIndex].GCID.ToString();
        }

        private void ID4Val_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint num = uint.Parse(this.ID4Val.Text);
                this.OGI.SomeInt321 = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void ID5Val_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint num = uint.Parse(this.ID5Val.Text);
                this.OGI.SomeInt322 = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void InsertT2Rec_Click(object sender, EventArgs e)
        {
            this.OGI.T2Number += 1;
            Array.Resize(ref this.OGI.T2, this.OGI.T2Number);
            int num = (this.OGI.T2Number - 2);
            int i = (this.T2Ind + 1);
            while ((i <= num))
            {
                this.OGI.T2[(i + 1)] = this.OGI.T2[i];
                i += 1;
            }
            this.OGI.T2[(this.T2Ind + 1)] = this.ParseT2(this.OGI.T2[(this.T2Ind + 1)]);
            T2Ind += 1;
            this.UpdateT2();
        }

        private void InsertT3Rec_Click(object sender, EventArgs e)
        {
            this.OGI.T1Number += 1;
            Array.Resize(ref this.OGI.T1, this.OGI.T1Number);
            Array.Resize(ref this.OGI.T3, this.OGI.T1Number);
            int num = (this.OGI.T1Number - 2);
            int i = (this.T1Ind + 1);
            while ((i <= num))
            {
                this.OGI.T1[(i + 1)] = this.OGI.T1[i];
                i += 1;
            }
            this.OGI.T1[(this.T1Ind + 1)] = this.ParseT1(this.OGI.T1[(this.T1Ind + 1)]);
            int num3 = (this.OGI.T1Number - 2);
            int j = (this.T3Ind + 1);
            while ((j <= num3))
            {
                this.OGI.T3[(j + 1)] = this.OGI.T3[j];
                j += 1;
            }
            this.OGI.T3[(this.T1Ind + 1)] = this.ParseT3(this.OGI.T3[(this.T1Ind + 1)]);
            T1Ind += 1;
            T3Ind += 1;
            this.UpdateT1();
            this.UpdateT3();
        }

        private void NextT1_Click(object sender, EventArgs e)
        {
            T1Ind += 1;
            T3Ind += 1;
            this.UpdateT1();
            this.UpdateT3();
        }

        private void NextT2_Click(object sender, EventArgs e)
        {
            T2Ind += 1;
            this.UpdateT2();
        }


        private void OGITree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.UpdateOGI(this.OGITree.SelectedNode.Index);
        }

        private Twinsanity.OGI.StructureType1 ParseT1(Twinsanity.OGI.StructureType1 T)
        {
            Twinsanity.OGI.StructureType1 type;
            try
            {
                Twinsanity.OGI.StructureType1 type2;
                string[][] strArray = new string[][] { };
                int index = 0;
                do
                {
                    strArray[index] = this.T1Edit.Lines[index].Split(' ');
                    index += 1;
                }
                while ((index <= 5));
                type2.Num1 = uint.Parse(strArray[0][0]);
                type2.Num2 = uint.Parse(strArray[0][1]);
                type2.Num3 = uint.Parse(strArray[0][2]);
                type2.Num4 = uint.Parse(strArray[0][3]);
                type2.Num5 = uint.Parse(strArray[0][4]);
                type2.Coordinate1.X = float.Parse(strArray[1][0]);
                type2.Coordinate1.Y = float.Parse(strArray[1][1]);
                type2.Coordinate1.Z = float.Parse(strArray[1][2]);
                type2.Coordinate1.W = float.Parse(strArray[1][3]);
                type2.Coordinate2.X = float.Parse(strArray[2][0]);
                type2.Coordinate2.Y = float.Parse(strArray[2][1]);
                type2.Coordinate2.Z = float.Parse(strArray[2][2]);
                type2.Coordinate2.W = float.Parse(strArray[2][3]);
                type2.Coordinate3.X = float.Parse(strArray[3][0]);
                type2.Coordinate3.Y = float.Parse(strArray[3][1]);
                type2.Coordinate3.Z = float.Parse(strArray[3][2]);
                type2.Coordinate3.W = float.Parse(strArray[3][3]);
                type2.Coordinate4.X = float.Parse(strArray[4][0]);
                type2.Coordinate4.Y = float.Parse(strArray[4][1]);
                type2.Coordinate4.Z = float.Parse(strArray[4][2]);
                type2.Coordinate4.W = float.Parse(strArray[4][3]);
                type2.Coordinate5.X = float.Parse(strArray[5][0]);
                type2.Coordinate5.Y = float.Parse(strArray[5][1]);
                type2.Coordinate5.Z = float.Parse(strArray[5][2]);
                type2.Coordinate5.W = float.Parse(strArray[5][3]);
                type = type2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("Parse Error", MsgBoxStyle.ApplicationModal, null);
                type = T;
                ProjectData.ClearProjectError();
            }
            return type;
        }

        private Twinsanity.OGI.StructureType2 ParseT2(Twinsanity.OGI.StructureType2 T)
        {
            Twinsanity.OGI.StructureType2 type;
            try
            {
                Twinsanity.OGI.StructureType2 type2;
                string[][] strArray = new string[][] { };
                int index = 0;
                do
                {
                    strArray[index] = this.T2Edit.Lines[index].Split(' ');
                    index += 1;
                }
                while ((index <= 4));
                type2.Num1 = uint.Parse(strArray[0][0]);
                type2.Num2 = uint.Parse(strArray[0][1]);
                type2.Coordinate1.X = float.Parse(strArray[1][0]);
                type2.Coordinate1.Y = float.Parse(strArray[1][1]);
                type2.Coordinate1.Z = float.Parse(strArray[1][2]);
                type2.Coordinate1.W = float.Parse(strArray[1][3]);
                type2.Coordinate2.X = float.Parse(strArray[2][0]);
                type2.Coordinate2.Y = float.Parse(strArray[2][1]);
                type2.Coordinate2.Z = float.Parse(strArray[2][2]);
                type2.Coordinate2.W = float.Parse(strArray[2][3]);
                type2.Coordinate3.X = float.Parse(strArray[3][0]);
                type2.Coordinate3.Y = float.Parse(strArray[3][1]);
                type2.Coordinate3.Z = float.Parse(strArray[3][2]);
                type2.Coordinate3.W = float.Parse(strArray[3][3]);
                type2.Coordinate4.X = float.Parse(strArray[4][0]);
                type2.Coordinate4.Y = float.Parse(strArray[4][1]);
                type2.Coordinate4.Z = float.Parse(strArray[4][2]);
                type2.Coordinate4.W = float.Parse(strArray[4][3]);
                type = type2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("Parse Error", MsgBoxStyle.ApplicationModal, null);
                type = T;
                ProjectData.ClearProjectError();
            }
            return type;
        }

        private Twinsanity.OGI.StructureType3 ParseT3(Twinsanity.OGI.StructureType3 T)
        {
            Twinsanity.OGI.StructureType3 type;
            try
            {
                Twinsanity.OGI.StructureType3 type2;
                string[][] strArray = new string[][] { };
                int index = 0;
                do
                {
                    strArray[index] = this.T3Edit.Lines[index].Split(' ');
                    index += 1;
                }
                while ((index <= 3));
                type2.Coordinate1.X = float.Parse(strArray[0][0]);
                type2.Coordinate1.Y = float.Parse(strArray[0][1]);
                type2.Coordinate1.Z = float.Parse(strArray[0][2]);
                type2.Coordinate1.W = float.Parse(strArray[0][3]);
                type2.Coordinate2.X = float.Parse(strArray[1][0]);
                type2.Coordinate2.Y = float.Parse(strArray[1][1]);
                type2.Coordinate2.Z = float.Parse(strArray[1][2]);
                type2.Coordinate2.W = float.Parse(strArray[1][3]);
                type2.Coordinate3.X = float.Parse(strArray[2][0]);
                type2.Coordinate3.Y = float.Parse(strArray[2][1]);
                type2.Coordinate3.Z = float.Parse(strArray[2][2]);
                type2.Coordinate3.W = float.Parse(strArray[2][3]);
                type2.Coordinate4.X = float.Parse(strArray[3][0]);
                type2.Coordinate4.Y = float.Parse(strArray[3][1]);
                type2.Coordinate4.Z = float.Parse(strArray[3][2]);
                type2.Coordinate4.W = float.Parse(strArray[3][3]);
                type = type2;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Interaction.MsgBox("Parse Error", MsgBoxStyle.ApplicationModal, null);
                type = T;
                ProjectData.ClearProjectError();
            }
            return type;
        }

        private void PrevT1_Click(object sender, EventArgs e)
        {
            T1Ind -= 1;
            T3Ind -= 1;
            this.UpdateT1();
            this.UpdateT3();
        }

        private void PrevT2_Click(object sender, EventArgs e)
        {
            T2Ind -= 1;
            this.UpdateT2();
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            this.UpdateOGI(this.OGITree.SelectedNode.Index);
        }

        private void T1IndVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.T1Ind = int.Parse(this.T1IndVal.Text);
                this.UpdateT1();
                this.UpdateT3();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void T2IndVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.T2Ind = int.Parse(this.T2IndVal.Text);
                this.UpdateT2();
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = uint.Parse(this.TextBox1.Text);
                this.OGI.ID = (uint)num;
                this.OGITree.SelectedNode.Text = (this.OGITree.SelectedNode.Index.ToString() + ") ID: " + this.OGI.ID.ToString());
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Unk1Val_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint num = uint.Parse(this.Unk1Val.Text);
                this.OGI.UnkI321 = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Unk2Val_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint num = uint.Parse(this.Unk2Val.Text);
                this.OGI.UnkI322 = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        public void UpdateOGI(int index)
        {
            this.OGI = (OGI)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[1].Nodes[3].Nodes[index]));
            uint Flag = (uint)(this.OGI.Flag1 + this.OGI.Flag2 * 256 + this.OGI.Flag3 * 65536 + this.OGI.Flag4 * 65536 * 256);
            this.FlagsVal.Text = Flag.ToString();
            this.Unk1Val.Text = this.OGI.UnkI321.ToString();
            this.Unk2Val.Text = this.OGI.UnkI322.ToString();
            this.Vect1XVal.Text = this.OGI.Coordinates1.X.ToString();
            this.Vect1YVal.Text = this.OGI.Coordinates1.Y.ToString();
            this.Vect1ZVal.Text = this.OGI.Coordinates1.Z.ToString();
            this.Vect1WVal.Text = this.OGI.Coordinates1.W.ToString();
            this.Vect2XVal.Text = this.OGI.Coordinates2.X.ToString();
            this.Vect2YVal.Text = this.OGI.Coordinates2.Y.ToString();
            this.Vect2ZVal.Text = this.OGI.Coordinates2.Z.ToString();
            this.Vect2WVal.Text = this.OGI.Coordinates2.W.ToString();
            this.ID4Val.Text = this.OGI.SomeInt321.ToString();
            this.ID5Val.Text = this.OGI.SomeInt322.ToString();
            this.T1Ind = 0;
            this.T2Ind = 0;
            this.T3Ind = 0;
            this.UpdateT1();
            this.UpdateT2();
            this.UpdateT3();
            this.GCList.Items.Clear();
            int num2 = (this.OGI.GCNumber - 1);
            int i = 0;
            while ((i <= num2))
            {
                this.GCList.Items.Add((this.OGI.GCI[i].ID.ToString() + ")" + this.OGI.GCI[i].GCID.ToString()));
                i += 1;
            }
            if ((this.GCList.Items.Count > 0))
                this.GCList.SelectedIndex = 0;
            this.TextBox1.Text = this.OGI.ID.ToString();
        }

        public void UpdateT1()
        {
            if ((this.OGI.T1Number > 0))
            {
                while ((this.T1Ind < 0))
                    this.T1Ind += this.OGI.T1Number;
                this.T1Ind = (this.T1Ind % this.OGI.T1Number);
                this.T1Edit.Text = this.OGI.T1[this.T1Ind].Num1.ToString() + " " + this.OGI.T1[this.T1Ind].Num2.ToString() + " " + this.OGI.T1[this.T1Ind].Num3.ToString() + " " + this.OGI.T1[this.T1Ind].Num4.ToString() + " " + this.OGI.T1[this.T1Ind].Num5.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T1Edit.Text += this.OGI.T1[this.T1Ind].Coordinate1.X.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate1.Y.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate1.Z.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate1.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T1Edit.Text += this.OGI.T1[this.T1Ind].Coordinate2.X.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate2.Y.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate2.Z.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate2.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T1Edit.Text += this.OGI.T1[this.T1Ind].Coordinate3.X.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate3.Y.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate3.Z.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate3.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T1Edit.Text += this.OGI.T1[this.T1Ind].Coordinate4.X.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate4.Y.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate4.Z.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate4.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T1Edit.Text += this.OGI.T1[this.T1Ind].Coordinate5.X.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate5.Y.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate5.Z.ToString() + " " + this.OGI.T1[this.T1Ind].Coordinate5.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
            }
            else
            {
                this.T1Edit.Text = "";
                int num = 0;
                do
                {
                    this.T1Edit.Text += Strings.ChrW(13) + Strings.ChrW(10);
                    num += 1;
                }
                while ((num <= 5));
                this.T1Ind = -1;
            }
            this.T1IndVal.Text = this.T1Ind.ToString();
        }

        public void UpdateT2()
        {
            if ((this.OGI.T2Number > 0))
            {
                while ((this.T2Ind < 0))
                    this.T2Ind += this.OGI.T2Number;
                this.T2Ind = (this.T2Ind % this.OGI.T2Number);
                this.T2Edit.Text = this.OGI.T2[this.T2Ind].Num1.ToString() + " " + this.OGI.T2[this.T2Ind].Num2.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T2Edit.Text += this.OGI.T2[this.T2Ind].Coordinate1.X.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate1.Y.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate1.Z.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate1.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T2Edit.Text += this.OGI.T2[this.T2Ind].Coordinate2.X.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate2.Y.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate2.Z.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate2.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T2Edit.Text += this.OGI.T2[this.T2Ind].Coordinate3.X.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate3.Y.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate3.Z.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate3.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T2Edit.Text += this.OGI.T2[this.T2Ind].Coordinate4.X.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate4.Y.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate4.Z.ToString() + " " + this.OGI.T2[this.T2Ind].Coordinate4.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
            }
            else
            {
                this.T2Edit.Text = "";
                int num = 0;
                do
                {
                    this.T2Edit.Text += Strings.ChrW(13) + Strings.ChrW(10);
                    num += 1;
                }
                while ((num <= 4));
                this.T2Ind = -1;
            }
            this.T2IndVal.Text = this.T2Ind.ToString();
        }

        public void UpdateT3()
        {
            if ((this.OGI.T1Number > 0))
            {
                while ((this.T3Ind < 0))
                    this.T3Ind += this.OGI.T1Number;
                this.T3Ind = (this.T3Ind % this.OGI.T1Number);
                this.T3Edit.Text = this.OGI.T3[this.T3Ind].Coordinate1.X.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate1.Y.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate1.Z.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate1.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T3Edit.Text += this.OGI.T3[this.T3Ind].Coordinate2.X.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate2.Y.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate2.Z.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate2.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T3Edit.Text += this.OGI.T3[this.T3Ind].Coordinate3.X.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate3.Y.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate3.Z.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate3.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
                this.T3Edit.Text += this.OGI.T3[this.T3Ind].Coordinate4.X.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate4.Y.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate4.Z.ToString() + " " + this.OGI.T3[this.T3Ind].Coordinate4.W.ToString() + Strings.ChrW(13) + Strings.ChrW(10);
            }
            else
            {
                this.T3Edit.Text = "";
                int num = 0;
                do
                {
                    this.T3Edit.Text += Strings.ChrW(13) + Strings.ChrW(10);
                    num += 1;
                }
                while ((num <= 3));
                this.T3Ind = -1;
            }
        }

        public void UpdateTree(ref OGIs OGIs, int Index)
        {
            this.OGITree.BeginUpdate();
            int num = (OGIs._Item.Length - 1);
            int i = 0;
            while ((i <= num))
            {
                OGI ogi = (OGI)OGIs._Item[i];
                this.OGITree.Nodes.Add((i.ToString() + ") ID: " + ogi.ID.ToString()));
                i += 1;
            }
            TextBox box1 = this.T1Edit;
            string[] lines = box1.Lines;
            Array.Resize<string>(ref lines, 6);
            box1.Lines = lines;
            TextBox box2 = this.T2Edit;
            lines = box2.Lines;
            Array.Resize<string>(ref lines, 5);
            box2.Lines = lines;
            TextBox box3 = this.T3Edit;
            lines = box3.Lines;
            Array.Resize<string>(ref lines, 4);
            box3.Lines = lines;
            this.OGITree.EndUpdate();
        }

        private void Vect1WVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect1WVal.Text);
                this.OGI.Coordinates1.W = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect1XVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect1XVal.Text);
                this.OGI.Coordinates1.X = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect1YVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect1YVal.Text);
                this.OGI.Coordinates1.Y = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect1ZVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect1ZVal.Text);
                this.OGI.Coordinates1.Z = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect2WVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect2WVal.Text);
                this.OGI.Coordinates2.W = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect2XVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect2XVal.Text);
                this.OGI.Coordinates2.X = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect2YVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect2YVal.Text);
                this.OGI.Coordinates2.Y = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Vect2ZVal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float num = float.Parse(this.Vect2ZVal.Text);
                this.OGI.Coordinates2.Z = num;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ArmatureSave.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream Obj = new System.IO.FileStream(ArmatureSave.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter Writer = new System.IO.StreamWriter(Obj);
                for (int i = 0; i <= OGI.T1Number - 1; i++)
                {
                    Writer.WriteLine("v " + OGI.T1[i].Coordinate2.X.ToString() + " " + OGI.T1[i].Coordinate2.Y.ToString() + " " + OGI.T1[i].Coordinate2.Z.ToString());
                    Writer.WriteLine("f " + i.ToString());
                }
                Writer.Flush();
                Writer.Close();
            }
        }
    }
}
