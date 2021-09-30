using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class TerrainEditor
    {
        private TwinsanityEditorForm twinsanityEditorForm;

        public TerrainEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }


        private uint ItemId;
        public void UpdateTree(ref Twinsanity.Terrains Ts)
        {
            TerTree.BeginUpdate();
            for (int i = 0; i <= Ts._Item.Length - 1; i++)
                TerTree.Nodes.Add("ID: " + Ts._Item[i].ID.ToString());
            TerTree.EndUpdate();
        }

        public void UpdatePos(int Index)
        {
            Twinsanity.Terrain T = (Twinsanity.Terrain)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[7].Nodes[Index]));
            NumVal.Text = T.Num.ToString();
            K1Val.Text = T.K[0].ToString();
            if (T.Num >= 2)
                K2Val.Text = T.K[1].ToString();
            else
                K2Val.Text = "";
            if (T.Num >= 3)
                K3Val.Text = T.K[2].ToString();
            else
                K3Val.Text = "";
            if (T.Num >= 4)
                K4Val.Text = T.K[3].ToString();
            else
                K4Val.Text = "";
            ID1Val.Text = T.IDS[3].ToString();
            ID2Val.Text = T.IDS[2].ToString();
            ID3Val.Text = T.IDS[1].ToString();
            ID4Val.Text = T.IDS[0].ToString();
            IDVal.Text = T.ID.ToString();
            this.Text = "ID: " + T.ID.ToString();
        }

        public void ApplyPos(int Index)
        {
            Twinsanity.Terrain T = new Twinsanity.Terrain();
            T.Num = uint.Parse(NumVal.Text);
            Array.Resize(ref T.K, (int)T.Num);
            T.K[0] = uint.Parse(K1Val.Text);
            if (T.Num >= 2)
                T.K[1] = uint.Parse(K2Val.Text);
            if (T.Num >= 3)
                T.K[2] = uint.Parse(K3Val.Text);
            if (T.Num >= 4)
                T.K[3] = uint.Parse(K4Val.Text);
            T.IDS[0] = uint.Parse(ID4Val.Text);
            T.IDS[1] = uint.Parse(ID3Val.Text);
            T.IDS[2] = uint.Parse(ID2Val.Text);
            T.IDS[3] = uint.Parse(ID1Val.Text);
            T.ID = uint.Parse(IDVal.Text);
            twinsanityEditorForm.LevelData.Put_Item(T, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[7].Nodes[Index]));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ApplyPos(TerTree.SelectedNode.Index);
        }

        private void TerTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdatePos(TerTree.SelectedNode.Index);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            UpdatePos(TerTree.SelectedNode.Index);
        }

        private void TerrainEditor_Load(object sender, EventArgs e)
        {
        }
    }
}
