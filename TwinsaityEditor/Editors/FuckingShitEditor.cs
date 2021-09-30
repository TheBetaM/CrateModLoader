using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class FuckingShitEditor
    {
        private int IISIndex;
        private uint ItemId;

        private TwinsanityEditorForm twinsanityEditorForm;

        public FuckingShitEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void UpdateTree(ref Twinsanity.FuckingShits FSs, uint Index)
        {
            FSTree.BeginUpdate();
            for (int i = 0; i <= FSs._Item.Length - 1; i++)
                FSTree.Nodes.Add("ID: " + FSs._Item[i].ID.ToString());
            FSTree.EndUpdate();
            IISIndex = (int)Index;
        }

        public void UpdateFS(int Index)
        {
            Twinsanity.FuckingShit FS = (Twinsanity.FuckingShit)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[2].Nodes[Index]));
            ID1Val.Text = FS.I16[0].ToString();
            ID2Val.Text = FS.I16[1].ToString();
            ID3Val.Text = FS.I16[2].ToString();
            ID4Val.Text = FS.I16[3].ToString();
            ID5Val.Text = FS.I16[4].ToString();
            ItemId = FS.ID;
            this.Text = "ID: " + FS.ID.ToString();
        }

        public void ApplyFS(int Index)
        {
            Twinsanity.FuckingShit FS = new Twinsanity.FuckingShit();
            FS.I16[0] = UInt16.Parse(ID1Val.Text);
            FS.I16[1] = UInt16.Parse(ID2Val.Text);
            FS.I16[2] = UInt16.Parse(ID3Val.Text);
            FS.I16[3] = UInt16.Parse(ID4Val.Text);
            FS.I16[4] = UInt16.Parse(ID5Val.Text);
            FS.ID = ItemId;
            twinsanityEditorForm.LevelData.Put_Item(FS, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[IISIndex].Nodes[2].Nodes[Index]));
        }

        private void FSTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateFS(FSTree.SelectedNode.Index);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UpdateFS(FSTree.SelectedNode.Index);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ApplyFS(FSTree.SelectedNode.Index);
        }
    }
}
