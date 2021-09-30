using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class GCEditor
    {

        private TwinsanityEditorForm twinsanityEditorForm;

        public GCEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void UpdateTree(ref Twinsanity.GCs GCs)
        {
            GCTree.BeginUpdate();
            for (int i = 0; i <= GCs._Item.Length - 1; i++)
                GCTree.Nodes.Add("ID: " + GCs._Item[i].ID.ToString());
            GCTree.EndUpdate();
        }
        public void UpdateGC(int Index)
        {
            Twinsanity.GC GC = (Twinsanity.GC)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[Index]));
            GCID.Text = GC.ID.ToString();
            Model.Text = GC.Model.ToString();
            Materials.Items.Clear();
            for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                Materials.Items.Add(GC.Material[i]);
            if (Materials.Items.Count > 0)
                UpdateMaterial((uint)Materials.Items[0]);
            else
                UpdateMaterial(0);
            this.Text = "ID: " + GC.ID.ToString();
        }
        public void UpdateMaterial(uint ID)
        {
            Twinsanity.Materials MTRLs = (Twinsanity.Materials)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1]));
            Twinsanity.Material MTRL = null;
            for (int i = 0; i <= MTRLs._Item.Length - 1; i++)
            {
                if (MTRLs._Item[i].ID == ID)
                {
                    MTRL = (Twinsanity.Material)MTRLs._Item[i];
                    break;
                }
            }
            if (!(MTRL == null))
                Label2.Text = MTRL.Name;
            else
                Label2.Text = "Material is undefined";
        }
        public void ApplyGC(int Index)
        {
            Twinsanity.GC GC = (Twinsanity.GC)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[Index]));
            GC.ID = uint.Parse(GCID.Text);
            GC.Model = uint.Parse(Model.Text);
            GC.MaterialNumber = Materials.Items.Count;
            Array.Resize(ref GC.Material, GC.MaterialNumber);
            for (int i = 0; i <= GC.MaterialNumber - 1; i++)
                GC.Material[i] = (uint)Materials.Items[i];
            twinsanityEditorForm.LevelData.Put_Item(GC, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[3].Nodes[Index]));
        }

        private void GCTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateGC(GCTree.SelectedNode.Index);
        }

        private void Materials_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(Materials.SelectedIndex == -1))
                UpdateMaterial((uint)Materials.Items[Materials.SelectedIndex]);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            UpdateGC(GCTree.SelectedNode.Index);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Materials.Items.Add(uint.Parse(MaterialVal.Text));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Materials.Items[Materials.SelectedIndex] = uint.Parse(MaterialVal.Text);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Materials.Items.RemoveAt(Materials.SelectedIndex);
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ApplyGC(GCTree.SelectedNode.Index);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!(Materials.SelectedIndex == -1))
            {
                twinsanityEditorForm.context.viewerTexture.Hide();
                Twinsanity.Materials MATs = (Twinsanity.Materials)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1]));
                Twinsanity.Material MAT = new Twinsanity.Material();
                int I;
                for (I = 0; I <= MATs._Item.Length - 1; I++)
                {
                    if (MATs._Item[I].ID == (uint)Materials.SelectedItem)
                    {
                        MAT = (Twinsanity.Material)MATs._Item[I];
                        break;
                    }
                }
                uint TexID = MAT.Texture;
                twinsanityEditorForm.context.viewerTexture.Textures = (Twinsanity.Textures)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[0]));
                twinsanityEditorForm.context.viewerTexture.Materials = MATs;
                twinsanityEditorForm.context.viewerTexture.Mat = true;
                twinsanityEditorForm.context.viewerTexture.CurTex = (uint)I;
                twinsanityEditorForm.context.viewerTexture.Show();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Twinsanity.GCs GCs = (Twinsanity.GCs)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[3]));
            Twinsanity.GC GC = new Twinsanity.GC();
            for (int i = 0; i <= GCs._Item.Length - 1; i++)
            {
                if (GCs._Item[i].ID == uint.Parse(GCTree.SelectedNode.Text.Remove(0, 4)))
                {
                    GC = (Twinsanity.GC)GCs._Item[i];
                    break;
                }
            }
            Twinsanity.Texture[] Texture = new Twinsanity.Texture[] { };
            Twinsanity.Materials Materials = (Twinsanity.Materials)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[1]));
            Twinsanity.Textures Textures = (Twinsanity.Textures)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[0]));
            Twinsanity.Models Models = (Twinsanity.Models)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[2]));
            Twinsanity.Material[] MAT = new Twinsanity.Material[] { };
            for (int i = 0; i <= GC.MaterialNumber - 1; i++)
            {
                for (int j = 0; j <= Materials._Item.Length - 1; j++)
                {
                    if (GC.Material[i] == Materials._Item[j].ID)
                    {
                        Array.Resize(ref MAT, MAT.Length + 1);
                        MAT[MAT.Length - 1] = (Twinsanity.Material)Materials._Item[j];
                        break;
                    }
                }
            }
            for (int i = 0; i <= GC.MaterialNumber - 1; i++)
            {
                for (int j = 0; j <= Textures._Item.Length - 1; j++)
                {
                    if (MAT[i].Texture == Textures._Item[j].ID)
                    {
                        Array.Resize(ref Texture, Texture.Length + 1);
                        Texture[Texture.Length - 1] = (Twinsanity.Texture)Textures._Item[j];
                        break;
                    }
                }
            }
            for (int i = 0; i <= Models._Item.Length - 1; i++)
            {
                if (Models._Item[i].ID == GC.Model)
                {
                    twinsanityEditorForm.context.viewerModel.Model = (Twinsanity.Model)Models._Item[i];
                    break;
                }
            }
            Array.Resize(ref twinsanityEditorForm.context.viewerModel.TwinTextures, Texture.Length);
            Texture.CopyTo(twinsanityEditorForm.context.viewerModel.TwinTextures, 0);
            twinsanityEditorForm.context.viewerModel.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Twinsanity.Models MDLs = (Twinsanity.Models)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[0].Nodes[2]));
            Twinsanity.Model MDL = new Twinsanity.Model();
            for (int i = 0; i <= MDLs._Item.Length - 1; i++)
            {
                if (MDLs._Item[i].ID == uint.Parse(Model.Text))
                {
                    MDL = (Twinsanity.Model)MDLs._Item[i];
                    break;
                }
            }
            twinsanityEditorForm.context.viewerModel.Model = MDL;
            twinsanityEditorForm.context.viewerModel.Show();
        }

        private void GCEditor_Load(object sender, EventArgs e)
        {
        }
    }
}
