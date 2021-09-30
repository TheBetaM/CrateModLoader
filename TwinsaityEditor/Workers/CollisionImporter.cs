using System.Windows.Forms;
using System.Collections.Generic;
using Twinsanity;
using System.IO;
using System;

namespace TwinsaityEditor
{
    public partial class CollisionImporter : Form
    {
        private ColDataController controller;
        private List<ColModel> models = new List<ColModel>();
        private Dictionary<int, int> vertGroups;

        private int vertexCount, triCount, groupCount, triggerCount;

        public CollisionImporter(ColDataController c)
        {
            controller = c;
            InitializeComponent();
            comboBox1.TextChanged += comboBox1_TextChanged;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            int s;
            if (int.TryParse(comboBox1.Text.Split(' ')[0], out s))
            {
                ColModel model = models[listBox1.SelectedIndex];
                model.surface = s;
                models[listBox1.SelectedIndex] = model;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            vertexCount -= models[listBox1.SelectedIndex].vtx.Count;
            triCount -= models[listBox1.SelectedIndex].tris.Count;
            groupCount -= models[listBox1.SelectedIndex].groups.Count;
            models.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            if (listBox1.Items.Count == 0)
                button3.Enabled = label4.Enabled = false;
            else
                UpdateMainLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Wavefront OBJ (*.obj)|*.obj", Multiselect = true };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < ofd.FileNames.Length; ++i)
                {
                    vertGroups = new Dictionary<int, int>();
                    int grp = 0;
                    ColModel model = new ColModel { vtx = new List<Pos>(), groups = new List<ColData.GroupInfo>(), triIndices = new List<List<ColData.ColTri>>() };
                    StreamReader reader = new StreamReader(ofd.FileNames[i]);
                    while(!reader.EndOfStream)
                    {
                        string[] str = reader.ReadLine().Split(' ');
                        switch (str[0])
                        {
                            case "v":
                                model.vtx.Add(new Pos(-float.Parse(str[1].Replace(".", ".")), float.Parse(str[2].Replace(".", ".")), float.Parse(str[3].Replace(".", ".")), 1));
                                break;
                            case "f":
                                int v1 = int.Parse(str[1].Split('/')[0]) - 1;
                                int v2 = int.Parse(str[2].Split('/')[0]) - 1;
                                int v3 = int.Parse(str[3].Split('/')[0]) - 1;
                                if (vertGroups.TryGetValue(v1, out grp) || vertGroups.TryGetValue(v2, out grp) || vertGroups.TryGetValue(v3, out grp))
                                {
                                    if (model.groups[grp].Size >= numericUpDown1.Value)
                                    {
                                        DiscardGroup(grp);
                                        grp = model.groups.Count;
                                        model.groups.Add(new ColData.GroupInfo());
                                        model.triIndices.Add(new List<ColData.ColTri>());
                                    }
                                    if (!vertGroups.ContainsKey(v1))
                                        vertGroups.Add(v1, grp);
                                    if (!vertGroups.ContainsKey(v2))
                                        vertGroups.Add(v2, grp);
                                    if (!vertGroups.ContainsKey(v3))
                                        vertGroups.Add(v3, grp);
                                }
                                else
                                {
                                    grp = model.groups.Count;
                                    model.groups.Add(new ColData.GroupInfo());
                                    model.triIndices.Add(new List<ColData.ColTri>());
                                    vertGroups.Add(v1, grp);
                                    vertGroups.Add(v2, grp);
                                    vertGroups.Add(v3, grp);
                                }
                                model.triIndices[grp].Add(new ColData.ColTri { Vert1 = v1, Vert2 = v2, Vert3 = v3 });
                                break;
                        }
                    }
                    reader.Close();
                    //GenerateGroups();
                    CheckMergeGroups(ref model);
                    GenerateGroupOff(ref model);
                    if (model.groups.Count == 0)
                        continue;
                    listBox1.Items.Add(ofd.FileNames[i]);
                    models.Add(model);
                    vertexCount += model.vtx.Count;
                    triCount += model.tris.Count;
                    groupCount += model.groups.Count;
                }
                button3.Enabled = label4.Enabled = true;
                UpdateMainLabel();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //generate
            controller.MainFile.CloseRMViewer();
            controller.Data.Vertices.Clear();
            controller.Data.Tris.Clear();
            controller.Data.Groups.Clear();
            int vertex_off = 0, poly_off = 0;
            for (int i = 0; i < models.Count; ++i)
            {
                controller.Data.Vertices.AddRange(models[i].vtx);
                for (int j = 0; j < models[i].tris.Count; ++j)
                {
                    controller.Data.Tris.Add(new ColData.ColTri { Vert1 = models[i].tris[j].Vert1 + vertex_off,
                        Vert2 = models[i].tris[j].Vert2 + vertex_off,
                        Vert3 = models[i].tris[j].Vert3 + vertex_off,
                        Surface = models[i].surface });
                }
                vertex_off += models[i].vtx.Count;
                for (int j = 0; j < models[i].groups.Count; ++j)
                {
                    controller.Data.Groups.Add(new ColData.GroupInfo
                    {
                        Offset = (uint)(models[i].groups[j].Offset + poly_off),
                        Size = models[i].groups[j].Size
                    });
                }
                poly_off += models[i].tris.Count;
            }
            ColData.Trigger[] triggers = new ColData.Trigger[controller.Data.Groups.Count * 2 - 1];
            TreeView TriggerTree = new TreeView();
            TriggerTree.Nodes.Add("E", "0");
            int x = (int)Math.Truncate(Math.Log(controller.Data.Groups.Count, 2));
            for (int i = 0; i < x; ++i)
                ExpandLevel(TriggerTree.Nodes[0]);
            ExpandEngings(TriggerTree, (uint)(controller.Data.Groups.Count - Math.Pow(2, x)));
            int temp = 1;
            CalcIDs(TriggerTree.Nodes[0], ref temp);
            Tree2Trigger(TriggerTree.Nodes[0], triggers);
            TriggerRecalculate(triggers, controller.Data.Groups, controller.Data.Tris, controller.Data.Vertices, 0);
            controller.Data.Triggers = new List<ColData.Trigger>(triggers);
            controller.UpdateText();
            Close();
        }

        private void DoubleExpand(TreeNode Node)
        {
            Node.Nodes.Add("E", "Node");
            Node.Nodes.Add("E", "Node");
        }

        private void ExpandLevel(TreeNode Root)
        {
            TreeNode[] Nodes = Root.Nodes.Find("E", true);
            if (Root.Name == "E")
            {
                Array.Resize(ref Nodes, Nodes.Length + 1);
                Nodes[Nodes.Length - 1] = Root;
            }
            for (int i = 0; i <= Nodes.Length - 1; i++)
            {
                Nodes[i].Name = "P";
                DoubleExpand(Nodes[i]);
            }
        }

        private void ExpandEngings(TreeView Tree, uint d)
        {
            TreeNode[] Nodes = Tree.Nodes.Find("E", true);
            for (int i = 0; i < d; ++i)
            {
                Nodes[i].Name = "P";
                DoubleExpand(Nodes[i]);
            }
        }

        private void CalcIDs(TreeNode Node, ref int temp)
        {
            if (Node.Name == "P")
            {
                uint x = (uint)Node.Nodes[0].GetNodeCount(true);
                Node.Nodes[0].Text = (int.Parse(Node.Text) + 1).ToString();
                Node.Nodes[1].Text = (int.Parse(Node.Nodes[0].Text) + x + 1).ToString();
                CalcIDs(Node.Nodes[0], ref temp);
                CalcIDs(Node.Nodes[1], ref temp);
            }
            else if (Node.Name == "E")
            {
                Node.Nodes.Add("Ptr", temp.ToString());
                temp += 1;
            }
        }

        private void Tree2Trigger(TreeNode Node, ColData.Trigger[] Triggers)
        {
            if (Node.Name == "P")
            {
                Triggers[int.Parse(Node.Text)].Flag1 = int.Parse(Node.Nodes[0].Text);
                Triggers[int.Parse(Node.Text)].Flag2 = int.Parse(Node.Nodes[1].Text);
                Tree2Trigger(Node.Nodes[0], Triggers);
                Tree2Trigger(Node.Nodes[1], Triggers);
            }
            else if (Node.Name == "E")
            {
                Triggers[int.Parse(Node.Text)].Flag1 = -int.Parse(Node.Nodes[0].Text);
                Triggers[int.Parse(Node.Text)].Flag2 = -int.Parse(Node.Nodes[0].Text);
            }
        }

        private void TriggerRecalculate(ColData.Trigger[] Triggers, List<ColData.GroupInfo> Groups, List<ColData.ColTri> Indexes, List<Pos> Vertexes, int index)
        {
            if (Triggers[index].Flag1 >= 0)
            {
                float pad = 0;
                TriggerRecalculate(Triggers, Groups, Indexes, Vertexes, Triggers[index].Flag1);
                TriggerRecalculate(Triggers, Groups, Indexes, Vertexes, Triggers[index].Flag2);
                Triggers[index].X1 = Math.Min(Triggers[Triggers[index].Flag1].X1, Triggers[Triggers[index].Flag2].X1) - pad;
                Triggers[index].Y1 = Math.Min(Triggers[Triggers[index].Flag1].Y1, Triggers[Triggers[index].Flag2].Y1) - pad;
                Triggers[index].Z1 = Math.Min(Triggers[Triggers[index].Flag1].Z1, Triggers[Triggers[index].Flag2].Z1) - pad;
                Triggers[index].X2 = Math.Max(Triggers[Triggers[index].Flag1].X2, Triggers[Triggers[index].Flag2].X2) + pad;
                Triggers[index].Y2 = Math.Max(Triggers[Triggers[index].Flag1].Y2, Triggers[Triggers[index].Flag2].Y2) + pad;
                Triggers[index].Z2 = Math.Max(Triggers[Triggers[index].Flag1].Z2, Triggers[Triggers[index].Flag2].Z2) + pad;
            }
            else
            {
                float x1 = 0f, x2 = 0f, y1 = 0f, y2 = 0f, z1 = 0f, z2 = 0f;
                float pad = 0;
                for (int i = (int)Groups[-Triggers[index].Flag1 - 1].Offset; i < Groups[-Triggers[index].Flag1 - 1].Offset + Groups[-Triggers[index].Flag1 - 1].Size; ++i)
                {
                    Pos a = Vertexes[Indexes[i].Vert1], b = Vertexes[Indexes[i].Vert2], c = Vertexes[Indexes[i].Vert3];
                    if (x1 == 0)
                        x1 = Math.Min(a.X, Math.Min(b.X, c.X)) - pad;
                    else
                        x1 = Math.Min(x1, Math.Min(a.X, Math.Min(b.X, c.X))) - pad;
                    if (y1 == 0)
                        y1 = Math.Min(a.Y, Math.Min(b.Y, c.Y)) - pad;
                    else
                        y1 = Math.Min(y1, Math.Min(a.Y, Math.Min(b.Y, c.Y))) - pad;
                    if (z1 == 0)
                        z1 = Math.Min(a.Z, Math.Min(b.Z, c.Z)) - pad;
                    else
                        z1 = Math.Min(z1, Math.Min(a.Z, Math.Min(b.Z, c.Z))) - pad;
                    if (x2 == 0)
                        x2 = Math.Max(a.X, Math.Max(b.X, c.X)) + pad;
                    else
                        x2 = Math.Max(x2, Math.Max(a.X, Math.Max(b.X, c.X))) + pad;
                    if (y2 == 0)
                        y2 = Math.Max(a.Y, Math.Max(b.Y, c.Y)) + pad;
                    else
                        y2 = Math.Max(y2, Math.Max(a.Y, Math.Max(b.Y, c.Y))) + pad;
                    if (z2 == 0)
                        z2 = Math.Max(a.Z, Math.Max(b.Z, c.Z));
                    else
                        z2 = Math.Max(z2, Math.Max(a.Z, Math.Max(b.Z, c.Z))) + pad;
                }
                Triggers[index].X1 = x1;
                Triggers[index].Y1 = y1;
                Triggers[index].Z1 = z1;
                Triggers[index].X2 = x2;
                Triggers[index].Y2 = y2;
                Triggers[index].Z2 = z2;
            }
        }

        private struct ColModel
        {
            public List<Pos> vtx;
            public List<ColData.ColTri> tris;
            public List<ColData.GroupInfo> groups;
            public List<List<ColData.ColTri>> triIndices;
            public int surface;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(groupBox1.Enabled = button2.Enabled = (listBox1.SelectedIndex != -1))) return;

            if (models[listBox1.SelectedIndex].surface >= comboBox1.Items.Count)
                comboBox1.Text = models[listBox1.SelectedIndex].surface.ToString();
            else
                comboBox1.Text = comboBox1.GetItemText(models[listBox1.SelectedIndex].surface);
            label3.Text = $"Vertex Count: {models[listBox1.SelectedIndex].vtx.Count} Triangle Count: {models[listBox1.SelectedIndex].tris.Count}"
                + Environment.NewLine + $"Group Count: {models[listBox1.SelectedIndex].groups.Count}";
            UpdateMainLabel();
        }

        private void UpdateMainLabel()
        {
            label4.Text = $"Trigger Count: {groupCount * 2}{Environment.NewLine}" +
                $"Group Count: {groupCount}{Environment.NewLine}" +
                $"Triangle Count: {triCount}{Environment.NewLine}" +
                $"Vertex Count: {vertexCount}";
        }

        private void DiscardGroup(int grp)
        {
            var dic = new Dictionary<int, int>(vertGroups);
            foreach (var i in dic)
                if (i.Value == grp)
                    vertGroups.Remove(i.Key);
        }

        private void GenerateGroupOff(ref ColModel model)
        {
            model.tris = new List<ColData.ColTri>();
            uint off = 0;
            for (int i = 0; i < model.groups.Count; ++i)
            {
                ColData.GroupInfo grp = model.groups[i];
                grp.Offset = off;
                grp.Size = (uint)model.triIndices[i].Count;
                off += grp.Size;
                model.tris.AddRange(model.triIndices[i]);
                model.groups[i] = grp;
            }
        }

        private void CheckMergeGroups(ref ColModel model)
        {
            HashSet<int> groups_to_delete = new HashSet<int>();
            for (int i = 0; i < model.groups.Count; ++i)
            {
                if (groups_to_delete.Contains(i)) continue;
                HashSet<int> verts = new HashSet<int>();
                for (int j = 0; j < model.triIndices[i].Count; ++j)
                {
                    verts.Add(model.triIndices[i][j].Vert1);
                    verts.Add(model.triIndices[i][j].Vert2);
                    verts.Add(model.triIndices[i][j].Vert3);
                }
                for (int j = i + 1; j < model.groups.Count; ++j)
                {
                    if ((model.triIndices[i].Count + model.triIndices[j].Count) > numericUpDown1.Value) continue;
                    for (int k = 0; k < model.triIndices[j].Count; ++k)
                    {
                        if (verts.Contains(model.triIndices[j][k].Vert1) || verts.Contains(model.triIndices[j][k].Vert3) || verts.Contains(model.triIndices[j][k].Vert3))
                        {
                            model.triIndices[i].AddRange(model.triIndices[j]);
                            groups_to_delete.Add(j);
                            break;
                        }
                    }
                }
            }
            List<int> delete_list = new List<int>(groups_to_delete);
            delete_list.Sort();
            for (int i = delete_list.Count - 1; i >= 0; --i)
            {
                model.groups.RemoveAt(delete_list[i]);
                model.triIndices.RemoveAt(delete_list[i]);
            }
        }
    }
}
