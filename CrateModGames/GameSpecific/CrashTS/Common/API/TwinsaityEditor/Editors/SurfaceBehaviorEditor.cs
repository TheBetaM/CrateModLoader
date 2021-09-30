using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class SurfaceBehaviorEditor
    {

        private TwinsanityEditorForm twinsanityEditorForm;

        public SurfaceBehaviorEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public int IISIndex;
        public uint ItemId;
        public void UpdateTree(ref Twinsanity.SurfaceBehaviours SBs, uint Index)
        {
            SBTree.BeginUpdate();
            for (int i = 0; i <= SBs._Item.Length - 1; i++)
                SBTree.Nodes.Add("ID: " + SBs._Item[i].ID.ToString());
            SBTree.EndUpdate();
            IISIndex = (int)Index;
        }

        public void UpdateSB(int Index)
        {
            Twinsanity.SurfaceBehaviour SB = (Twinsanity.SurfaceBehaviour)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[5].Nodes[Index]));
            Flag11.Checked = (SB.Flag[0] & 1) >> 0 != 0;
            Flag12.Checked = (SB.Flag[0] & 2) >> 1 != 0;
            Flag13.Checked = (SB.Flag[0] & 4) >> 2 != 0;
            Flag14.Checked = (SB.Flag[0] & 8) >> 3 != 0;
            Flag15.Checked = (SB.Flag[0] & 16) >> 4 != 0;
            Flag16.Checked = (SB.Flag[0] & 32) >> 5 != 0;
            Flag17.Checked = (SB.Flag[0] & 64) >> 6 != 0;
            Flag18.Checked = (SB.Flag[0] & 128) >> 7 != 0;

            Flag21.Checked = (SB.Flag[1] & 1) >> 0 != 0;
            Flag22.Checked = (SB.Flag[1] & 2) >> 1 != 0;
            Flag23.Checked = (SB.Flag[1] & 4) >> 2 != 0;
            Flag24.Checked = (SB.Flag[1] & 8) >> 3 != 0;
            Flag25.Checked = (SB.Flag[1] & 16) >> 4 != 0;
            Flag26.Checked = (SB.Flag[1] & 32) >> 5 != 0;
            Flag27.Checked = (SB.Flag[1] & 64) >> 6 != 0;
            Flag28.Checked = (SB.Flag[1] & 128) >> 7 != 0;

            Flag31.Checked = (SB.Flag[2] & 1) >> 0 != 0;
            Flag32.Checked = (SB.Flag[2] & 2) >> 1 != 0;
            Flag33.Checked = (SB.Flag[2] & 4) >> 2 != 0;
            Flag34.Checked = (SB.Flag[2] & 8) >> 3 != 0;
            Flag35.Checked = (SB.Flag[2] & 16) >> 4 != 0;
            Flag36.Checked = (SB.Flag[2] & 32) >> 5 != 0;
            Flag37.Checked = (SB.Flag[2] & 64) >> 6 != 0;
            Flag38.Checked = (SB.Flag[2] & 128) >> 7 != 0;

            Flag41.Checked = (SB.Flag[3] & 1) >> 0 != 0;
            Flag42.Checked = (SB.Flag[3] & 2) >> 1 != 0;
            Flag43.Checked = (SB.Flag[3] & 4) >> 2 != 0;
            Flag44.Checked = (SB.Flag[3] & 8) >> 3 != 0;
            Flag45.Checked = (SB.Flag[3] & 16) >> 4 != 0;
            Flag46.Checked = (SB.Flag[3] & 32) >> 5 != 0;
            Flag47.Checked = (SB.Flag[3] & 64) >> 6 != 0;
            Flag48.Checked = (SB.Flag[3] & 128) >> 7 != 0;

            ID1Val.Text = SB.IDs[0].ToString();
            ID2Val.Text = SB.IDs[1].ToString();
            ID3Val.Text = SB.IDs[2].ToString();
            ID4Val.Text = SB.IDs[3].ToString();
            ID5Val.Text = SB.IDs[4].ToString();
            ID6Val.Text = SB.IDs[5].ToString();
            ID7Val.Text = SB.IDs[6].ToString();
            ID8Val.Text = SB.IDs[7].ToString();
            ID9Val.Text = SB.IDs[8].ToString();
            ID10Val.Text = SB.IDs[9].ToString();

            F11Val.Text = SB.Pos[0].X.ToString();
            F12Val.Text = SB.Pos[0].Y.ToString();
            F13Val.Text = SB.Pos[0].Z.ToString();
            F14Val.Text = SB.Pos[0].W.ToString();

            F21Val.Text = SB.Pos[1].X.ToString();
            F22Val.Text = SB.Pos[1].Y.ToString();
            F23Val.Text = SB.Pos[1].Z.ToString();
            F24Val.Text = SB.Pos[1].W.ToString();

            F31Val.Text = SB.Pos[2].X.ToString();
            F32Val.Text = SB.Pos[2].Y.ToString();
            F33Val.Text = SB.Pos[2].Z.ToString();
            F34Val.Text = SB.Pos[2].W.ToString();

            F41Val.Text = SB.Pos[3].X.ToString();
            F42Val.Text = SB.Pos[3].Y.ToString();
            F43Val.Text = SB.Pos[3].Z.ToString();
            F44Val.Text = SB.Pos[3].W.ToString();

            EI1Val.Text = SB.EndIn16[0].ToString();
            EI2Val.Text = SB.EndIn16[1].ToString();
            EI3Val.Text = SB.EndIn16[2].ToString();
            EI4Val.Text = SB.EndIn16[3].ToString();
            EI5Val.Text = SB.EndIn16[4].ToString();
            EI6Val.Text = SB.EndIn16[5].ToString();
            EI7Val.Text = SB.EndIn16[6].ToString();
            EI8Val.Text = SB.EndIn16[7].ToString();
            EI9Val.Text = SB.EndIn16[8].ToString();
            EI10Val.Text = SB.EndIn16[9].ToString();
            EI11Val.Text = SB.EndIn16[10].ToString();
            EI12Val.Text = SB.EndIn16[11].ToString();

            ItemId = SB.ID;
            this.Text = "ID: " + SB.ID.ToString();
        }

        public void ApplySB(int Index)
        {
            Twinsanity.SurfaceBehaviour SB = new Twinsanity.SurfaceBehaviour();
            SB.Flag[0] = 0;
            SB.Flag[1] = 0;
            SB.Flag[2] = 0;
            SB.Flag[3] = 0;

            SB.Flag[0] += (byte)(Flag11.Checked ? 1 : 0 << 0);
            SB.Flag[0] += (byte)(Flag12.Checked ? 1 : 0 << 1);
            SB.Flag[0] += (byte)(Flag13.Checked ? 1 : 0 << 2);
            SB.Flag[0] += (byte)(Flag14.Checked ? 1 : 0 << 3);
            SB.Flag[0] += (byte)(Flag15.Checked ? 1 : 0 << 4);
            SB.Flag[0] += (byte)(Flag16.Checked ? 1 : 0 << 5);
            SB.Flag[0] += (byte)(Flag17.Checked ? 1 : 0 << 6);
            SB.Flag[0] += (byte)(Flag18.Checked ? 1 : 0 << 7);

            SB.Flag[1] += (byte)(Flag21.Checked ? 1 : 0 << 0);
            SB.Flag[1] += (byte)(Flag22.Checked ? 1 : 0 << 1);
            SB.Flag[1] += (byte)(Flag23.Checked ? 1 : 0 << 2);
            SB.Flag[1] += (byte)(Flag24.Checked ? 1 : 0 << 3);
            SB.Flag[1] += (byte)(Flag25.Checked ? 1 : 0 << 4);
            SB.Flag[1] += (byte)(Flag26.Checked ? 1 : 0 << 5);
            SB.Flag[1] += (byte)(Flag27.Checked ? 1 : 0 << 6);
            SB.Flag[1] += (byte)(Flag28.Checked ? 1 : 0 << 7);

            SB.Flag[2] += (byte)(Flag31.Checked ? 1 : 0 << 0);
            SB.Flag[2] += (byte)(Flag32.Checked ? 1 : 0 << 1);
            SB.Flag[2] += (byte)(Flag33.Checked ? 1 : 0 << 2);
            SB.Flag[2] += (byte)(Flag34.Checked ? 1 : 0 << 3);
            SB.Flag[2] += (byte)(Flag35.Checked ? 1 : 0 << 4);
            SB.Flag[2] += (byte)(Flag36.Checked ? 1 : 0 << 5);
            SB.Flag[2] += (byte)(Flag37.Checked ? 1 : 0 << 6);
            SB.Flag[2] += (byte)(Flag38.Checked ? 1 : 0 << 7);

            SB.Flag[3] += (byte)(Flag41.Checked ? 1 : 0 << 0);
            SB.Flag[3] += (byte)(Flag42.Checked ? 1 : 0 << 1);
            SB.Flag[3] += (byte)(Flag43.Checked ? 1 : 0 << 2);
            SB.Flag[3] += (byte)(Flag44.Checked ? 1 : 0 << 3);
            SB.Flag[3] += (byte)(Flag45.Checked ? 1 : 0 << 4);
            SB.Flag[3] += (byte)(Flag46.Checked ? 1 : 0 << 5);
            SB.Flag[3] += (byte)(Flag47.Checked ? 1 : 0 << 6);
            SB.Flag[3] += (byte)(Flag48.Checked ? 1 : 0 << 7);

            SB.IDs[0] = UInt16.Parse(ID1Val.Text);
            SB.IDs[1] = UInt16.Parse(ID2Val.Text);
            SB.IDs[2] = UInt16.Parse(ID3Val.Text);
            SB.IDs[3] = UInt16.Parse(ID4Val.Text);
            SB.IDs[4] = UInt16.Parse(ID5Val.Text);
            SB.IDs[5] = UInt16.Parse(ID6Val.Text);
            SB.IDs[6] = UInt16.Parse(ID7Val.Text);
            SB.IDs[7] = UInt16.Parse(ID8Val.Text);
            SB.IDs[8] = UInt16.Parse(ID9Val.Text);
            SB.IDs[9] = UInt16.Parse(ID10Val.Text);

            SB.Pos[0].X = float.Parse(F11Val.Text);
            SB.Pos[0].Y = float.Parse(F12Val.Text);
            SB.Pos[0].Z = float.Parse(F13Val.Text);
            SB.Pos[0].W = float.Parse(F14Val.Text);

            SB.Pos[1].X = float.Parse(F21Val.Text);
            SB.Pos[1].Y = float.Parse(F22Val.Text);
            SB.Pos[1].Z = float.Parse(F23Val.Text);
            SB.Pos[1].W = float.Parse(F24Val.Text);

            SB.Pos[2].X = float.Parse(F31Val.Text);
            SB.Pos[2].Y = float.Parse(F32Val.Text);
            SB.Pos[2].Z = float.Parse(F33Val.Text);
            SB.Pos[2].W = float.Parse(F34Val.Text);

            SB.Pos[3].X = float.Parse(F41Val.Text);
            SB.Pos[3].Y = float.Parse(F42Val.Text);
            SB.Pos[3].Z = float.Parse(F43Val.Text);
            SB.Pos[3].W = float.Parse(F44Val.Text);

            SB.EndIn16[0] = UInt16.Parse(EI1Val.Text);
            SB.EndIn16[1] = UInt16.Parse(EI2Val.Text);
            SB.EndIn16[2] = UInt16.Parse(EI3Val.Text);
            SB.EndIn16[3] = UInt16.Parse(EI4Val.Text);
            SB.EndIn16[4] = UInt16.Parse(EI5Val.Text);
            SB.EndIn16[5] = UInt16.Parse(EI6Val.Text);
            SB.EndIn16[6] = UInt16.Parse(EI7Val.Text);
            SB.EndIn16[7] = UInt16.Parse(EI8Val.Text);
            SB.EndIn16[8] = UInt16.Parse(EI9Val.Text);
            SB.EndIn16[9] = UInt16.Parse(EI10Val.Text);
            SB.EndIn16[10] = UInt16.Parse(EI11Val.Text);
            SB.EndIn16[11] = UInt16.Parse(EI12Val.Text);

            SB.ID = ItemId;

            twinsanityEditorForm.LevelData.Put_Item(SB, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[5].Nodes[Index]));
        }

        private void SBTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateSB(SBTree.SelectedNode.Index);
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ApplySB(SBTree.SelectedNode.Index);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            UpdateSB(SBTree.SelectedNode.Index);
        }
    }
}
