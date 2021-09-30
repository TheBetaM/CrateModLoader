using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class BehaviorEditor
    {
        private int IISIndex;
        private uint ItemId;

        private TwinsanityEditorForm twinsanityEditorForm;
        public BehaviorEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void UpdateTree(ref Twinsanity.Behaviors BHs, uint Index)
        {
            BehTree.BeginUpdate();
            for (int i = 0; i <= BHs._Item.Length - 1; i++)
                BehTree.Nodes.Add("ID: " + BHs._Item[i].ID.ToString());
            BehTree.EndUpdate();
            IISIndex = (int)Index;
        }

        public void UpdateBeh(int Index)
        {
            Twinsanity.Behavior Beh = (Twinsanity.Behavior)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[1].Nodes[Index]));
            XVal.Text = Beh.Cord.X.ToString();
            YVal.Text = Beh.Cord.Y.ToString();
            ZVal.Text = Beh.Cord.Z.ToString();
            WVal.Text = Beh.Cord.W.ToString();
            NumVal.Text = Beh.Num.ToString();
            ItemId = Beh.ID;
            this.Text = "ID: " + Beh.ID.ToString();
        }

        public void ApplyBeh(int Index)
        {
            Twinsanity.Behavior Beh = new Twinsanity.Behavior();
            Beh.Cord.X = float.Parse(XVal.Text);
            Beh.Cord.Y = float.Parse(YVal.Text);
            Beh.Cord.Z = float.Parse(ZVal.Text);
            Beh.Cord.W = float.Parse(WVal.Text);
            Beh.Num = UInt16.Parse(NumVal.Text);
            Beh.ID = ItemId;
            twinsanityEditorForm.LevelData.Put_Item(Beh, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[1].Nodes[Index]));
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ApplyBeh(BehTree.SelectedNode.Index);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            UpdateBeh(BehTree.SelectedNode.Index);
        }

        private void BehTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateBeh(BehTree.SelectedNode.Index);
        }
    }
}
