using System.Windows.Forms;
using System;

namespace TwinsaityEditor
{
    public partial class GameObjectEditor
    {

        private TwinsanityEditorForm twinsanityEditorForm;

        public GameObjectEditor(TwinsanityEditorForm TEF)
        {
            twinsanityEditorForm = TEF;
            InitializeComponent();
        }

        public void UpdateTree(ref Twinsanity.GameObjects GOs, int Index)
        {
            GOTree.BeginUpdate();
            for (int i = 0; i <= GOs._Item.Length - 1; i++)
            {
                Twinsanity.GameObject GO = (Twinsanity.GameObject)GOs._Item[i];
                GOTree.Nodes.Add("ID: " + GO.Name);
            }
            GOTree.EndUpdate();
        }
        public void UpdateGO(int index)
        {
            Twinsanity.GameObject GO = (Twinsanity.GameObject)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[index]));
            UnkList.Items.Clear();
            OGIList.Items.Clear();
            AnimList.Items.Clear();
            ScrList.Items.Clear();
            GOList.Items.Clear();
            SoundList.Items.Clear();
            SomeList.Items.Clear();
            FloatList.Items.Clear();
            IntList.Items.Clear();
            for (int i = 0; i <= GO.UnkI32.Length - 1; i++)
                UnkList.Items.Add(GO.UnkI32[i].ToString());
            for (int i = 0; i <= GO.OGI.Length - 1; i++)
                OGIList.Items.Add(GO.OGI[i].ToString());
            for (int i = 0; i <= GO.Animation.Length - 1; i++)
                AnimList.Items.Add(GO.Animation[i].ToString());
            for (int i = 0; i <= GO.Script.Length - 1; i++)
                ScrList.Items.Add(GO.Script[i].ToString());
            for (int i = 0; i <= GO._GameObject.Length - 1; i++)
                GOList.Items.Add(GO._GameObject[i].ToString());
            for (int i = 0; i <= GO.Sound.Length - 1; i++)
                SoundList.Items.Add(GO.Sound[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI321.Length - 1; i++)
                SomeList.Items.Add(GO.ParametersUnkI321[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI322.Length - 1; i++)
                FloatList.Items.Add(GO.ParametersUnkI322[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI323.Length - 1; i++)
                IntList.Items.Add(GO.ParametersUnkI323[i].ToString());
            IDVal.Text = GO.ID.ToString();
            NameVal.Text = GO.Name;
            Class1Val.Text = GO.Class1.ToString();
            Class2Val.Text = GO.Class2.ToString();
            Class3Val.Text = GO.Class3.ToString();
            ParamVal.Text = GO.ParametersUnkI32.ToString();
            this.Text = "ID: " + IDVal.Text;
        }
        public void ApplyGO(int index)
        {
            TwinsanityEditorForm twinsanityEditorForm = (TwinsanityEditorForm)ParentForm;
            Twinsanity.GameObject GO = (Twinsanity.GameObject)twinsanityEditorForm.LevelData.Get_Item(TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[index]));
            GO.UnkI32Number = UnkList.Items.Count;
            Array.Resize(ref GO.UnkI32, GO.UnkI32Number);
            GO.OGINumber = OGIList.Items.Count;
            Array.Resize(ref GO.OGI, GO.OGINumber);
            GO.AnimationNumber = AnimList.Items.Count;
            Array.Resize(ref GO.Animation, GO.AnimationNumber);
            GO.ScriptNumber = ScrList.Items.Count;
            Array.Resize(ref GO.Script, GO.ScriptNumber);
            GO.GameObjectNumber = GOList.Items.Count;
            Array.Resize(ref GO._GameObject, GO.GameObjectNumber);
            GO.SoundNumber = SoundList.Items.Count;
            Array.Resize(ref GO.Sound, GO.SoundNumber);
            GO.ParametersUnkI321Number = SomeList.Items.Count;
            Array.Resize(ref GO.ParametersUnkI321, GO.ParametersUnkI321Number);
            GO.ParametersUnkI322Number = FloatList.Items.Count;
            Array.Resize(ref GO.ParametersUnkI322, GO.ParametersUnkI322Number);
            GO.ParametersUnkI323Number = IntList.Items.Count;
            Array.Resize(ref GO.ParametersUnkI323, GO.ParametersUnkI323Number);

            for (int i = 0; i <= GO.UnkI32.Length - 1; i++)
                GO.UnkI32[i] = uint.Parse(UnkList.Items[i].ToString());
            for (int i = 0; i <= GO.OGI.Length - 1; i++)
                GO.OGI[i] = UInt16.Parse(OGIList.Items[i].ToString());
            for (int i = 0; i <= GO.Animation.Length - 1; i++)
                GO.Animation[i] = UInt16.Parse(AnimList.Items[i].ToString());
            for (int i = 0; i <= GO.Script.Length - 1; i++)
                GO.Script[i] = UInt16.Parse(ScrList.Items[i].ToString());
            for (int i = 0; i <= GO._GameObject.Length - 1; i++)
                GO._GameObject[i] = UInt16.Parse(GOList.Items[i].ToString());
            for (int i = 0; i <= GO.Sound.Length - 1; i++)
                GO.Sound[i] = UInt16.Parse(SoundList.Items[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI321.Length - 1; i++)
                GO.ParametersUnkI321[i] = uint.Parse(SomeList.Items[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI322.Length - 1; i++)
                GO.ParametersUnkI322[i] = float.Parse(FloatList.Items[i].ToString());
            for (int i = 0; i <= GO.ParametersUnkI323.Length - 1; i++)
                GO.ParametersUnkI323[i] = uint.Parse(IntList.Items[i].ToString());
            GO.ID = uint.Parse(IDVal.Text);
            GO.Name = NameVal.Text;
            GOTree.SelectedNode.Text = GO.Name;
            this.Text = "ID: " + IDVal.Text;
            GO.Class1 = uint.Parse(Class1Val.Text);
            GO.Class2 = uint.Parse(Class2Val.Text);
            GO.Class3 = uint.Parse(Class3Val.Text);
            GO.ParametersUnkI32 = uint.Parse(ParamVal.Text);
            GO.ParametersHeader =(uint)((GO.ParametersUnkI323Number << 16) + (GO.ParametersUnkI322Number << 8) + (GO.ParametersUnkI321Number));

            twinsanityEditorForm.LevelData.Put_Item(GO, TwinsanityEditorForm.CalculateIndexes(twinsanityEditorForm.TreeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[index]));
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            ApplyGO(GOTree.SelectedNode.Index);
        }

        private void Revert_Click(object sender, EventArgs e)
        {
            UpdateGO(GOTree.SelectedNode.Index);
        }

        private void GOTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateGO(GOTree.SelectedNode.Index);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (UnkList.SelectedIndex >= 0)
            {
                int i = UnkList.SelectedIndex;
                UnkList.Items.RemoveAt(UnkList.SelectedIndex);
                if (i >= UnkList.Items.Count)
                    i -= 1;
                if (UnkList.Items.Count > 0)
                    UnkList.SelectedIndex = i;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (OGIList.SelectedIndex >= 0)
            {
                int i = OGIList.SelectedIndex;
                OGIList.Items.RemoveAt(OGIList.SelectedIndex);
                if (i >= OGIList.Items.Count)
                    i -= 1;
                if (OGIList.Items.Count > 0)
                    OGIList.SelectedIndex = i;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (AnimList.SelectedIndex >= 0)
            {
                int i = AnimList.SelectedIndex;
                AnimList.Items.RemoveAt(AnimList.SelectedIndex);
                if (i >= AnimList.Items.Count)
                    i -= 1;
                if (AnimList.Items.Count > 0)
                    AnimList.SelectedIndex = i;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (ScrList.SelectedIndex >= 0)
            {
                int i = ScrList.SelectedIndex;
                ScrList.Items.RemoveAt(ScrList.SelectedIndex);
                if (i >= ScrList.Items.Count)
                    i -= 1;
                if (ScrList.Items.Count > 0)
                    ScrList.SelectedIndex = i;
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (GOList.SelectedIndex >= 0)
            {
                int i = GOList.SelectedIndex;
                GOList.Items.RemoveAt(GOList.SelectedIndex);
                if (i >= GOList.Items.Count)
                    i -= 1;
                if (GOList.Items.Count > 0)
                    GOList.SelectedIndex = i;
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (SoundList.SelectedIndex >= 0)
            {
                int i = SoundList.SelectedIndex;
                SoundList.Items.RemoveAt(SoundList.SelectedIndex);
                if (i >= SoundList.Items.Count)
                    i -= 1;
                if (SoundList.Items.Count > 0)
                    SoundList.SelectedIndex = i;
            }
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            if (SomeList.SelectedIndex >= 0)
            {
                int i = SomeList.SelectedIndex;
                SomeList.Items.RemoveAt(SomeList.SelectedIndex);
                if (i >= SomeList.Items.Count)
                    i -= 1;
                if (SomeList.Items.Count > 0)
                    SomeList.SelectedIndex = i;
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            if (FloatList.SelectedIndex >= 0)
            {
                int i = FloatList.SelectedIndex;
                FloatList.Items.RemoveAt(FloatList.SelectedIndex);
                if (i >= FloatList.Items.Count)
                    i -= 1;
                if (FloatList.Items.Count > 0)
                    FloatList.SelectedIndex = i;
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (IntList.SelectedIndex >= 0)
            {
                int i = IntList.SelectedIndex;
                IntList.Items.RemoveAt(IntList.SelectedIndex);
                if (i >= IntList.Items.Count)
                    i -= 1;
                if (IntList.Items.Count > 0)
                    IntList.SelectedIndex = i;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (UnkList.SelectedIndex >= 0)
                UnkList.Items[UnkList.SelectedIndex] = TextBox6.Text;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (OGIList.SelectedIndex >= 0)
                OGIList.Items[OGIList.SelectedIndex] = TextBox7.Text;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (AnimList.SelectedIndex >= 0)
                AnimList.Items[AnimList.SelectedIndex] = TextBox8.Text;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (ScrList.SelectedIndex >= 0)
                ScrList.Items[ScrList.SelectedIndex] = TextBox9.Text;
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (GOList.SelectedIndex >= 0)
                GOList.Items[GOList.SelectedIndex] = TextBox10.Text;
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (SoundList.SelectedIndex >= 0)
                SoundList.Items[SoundList.SelectedIndex] = TextBox11.Text;
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            if (SomeList.SelectedIndex >= 0)
                SomeList.Items[SomeList.SelectedIndex] = TextBox14.Text;
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            if (FloatList.SelectedIndex >= 0)
                FloatList.Items[FloatList.SelectedIndex] = TextBox13.Text;
        }
        private void Button19_Click(object sender, EventArgs e)
        {
            if (IntList.SelectedIndex >= 0)
                IntList.Items[IntList.SelectedIndex] = TextBox12.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UnkList.Items.Add(TextBox6.Text);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            OGIList.Items.Add(TextBox7.Text);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            AnimList.Items.Add(TextBox8.Text);
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            ScrList.Items.Add(TextBox9.Text);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            GOList.Items.Add(TextBox10.Text);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            SoundList.Items.Add(TextBox11.Text);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            IntList.Items.Add(TextBox12.Text);
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            FloatList.Items.Add(TextBox13.Text);
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            SomeList.Items.Add(TextBox14.Text);
        }

        private void GameObjectEditor_Load(object sender, EventArgs e)
        {
        }
    }
}
