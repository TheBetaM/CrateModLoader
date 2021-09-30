using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class ColDataEditor : Form
    {
        private ColData col;

        public ColDataEditor(ColData col)
        {
            this.col = col;
            InitializeComponent();
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.SplitterDistance = (int)(splitContainer1.Width * 1F);
            splitContainer1.SizeChanged += SplitContainer1_SizeChanged;
            PopulateTree(treeView1);
        }

        private void SplitContainer1_SizeChanged(object sender, System.EventArgs e)
        {
            splitContainer1.SplitterDistance = (int)(splitContainer1.Width * 1F);
        }

        void PopulateTree(TreeView trv)
        {
            trv.BeginUpdate();
            trv.Nodes.Clear();
            TreeNode new_node = new TreeNode("Root Node");
            AddTriggerNode(new_node, 0);
            new_node.ExpandAll();
            trv.Nodes.Add(new_node);
            trv.EndUpdate();
        }

        int AddTriggerNode(TreeNode node, int index)
        {
            if (index >= col.Triggers.Count) return -1; //error!
            TreeNode new_node = new TreeNode($"Sub-Node ({index})");
            node.Nodes.Add(new_node);
            int min_node = col.Triggers[index].Flag1, max_node = col.Triggers[index].Flag2;
            for (index = min_node; index <= max_node && index < col.Triggers.Count; ++index)
            {
                if (col.Triggers[index].Flag1 < 0 && col.Triggers[index].Flag2 < 0) //leaf node
                {
                    new_node.Nodes.Add($"Leaf Node ({index}): Group {-col.Triggers[index].Flag1}");
                }
                else
                {
                    index = AddTriggerNode(new_node, index) - 1;
                }
            }
            return index;
        }
    }
}
