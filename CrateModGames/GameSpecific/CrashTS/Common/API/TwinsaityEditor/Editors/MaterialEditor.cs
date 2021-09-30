using System;

namespace TwinsaityEditor
{
    public partial class MaterialEditor
    {
        private TwinsanityEditorForm twinsanityEditorForm;

        public MaterialEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void UpdateTree(ref Twinsanity.Materials MTLs)
        {
            MtlTree.BeginUpdate();
            for (int i = 0; i <= MTLs._Item.Length - 1; i++)
                MtlTree.Nodes.Add("ID: " + MTLs._Item[i].ID.ToString());
            MtlTree.EndUpdate();
        }
        public void UpdateMtl(int Index)
        {
            Twinsanity.Material Mtl = (Twinsanity.Material)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[Index]));
            TextureID.Text = Mtl.Texture.ToString();
            MtlID.Text = Mtl.ID.ToString();
            MtlName.Text = Mtl.Name;
            this.Text = "ID: " + Mtl.ID.ToString();
        }
        public void ApplyMtl(int Index)
        {
            Twinsanity.Material Mtl = (Twinsanity.Material)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[Index]));
            Mtl.ID = uint.Parse(MtlID.Text);
            Mtl.Texture = uint.Parse(TextureID.Text);
            Mtl.Name = MtlName.Text;
            twinsanityEditorForm.LevelData.Put_Item(Mtl, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[Index]));
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ApplyMtl(MtlTree.SelectedNode.Index);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            UpdateMtl(MtlTree.SelectedNode.Index);
        }
    }
}
