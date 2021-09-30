using System.Windows.Forms;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class PathEditor : Form
    {
        private SectionController controller;
        private Path path;

        private FileController File { get; set; }
        private TwinsFile FileData { get => File.Data; }
        private Controller CurPathCont { get => (Controller)controller.Node.Nodes[controller.Data.RecordIDs[path.ID]].Tag; }

        private bool ignore_value_change;
        private int pos_i, par_i;

        public PathEditor(SectionController c)
        {
            File = c.MainFile;
            controller = c;
            InitializeComponent();
            Text = $"Path Editor (Section {c.Data.Parent.ID})";
            PopulateList();
            FormClosed += PathEditor_FormClosed;
        }

        private void PathEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.SelectItem(null);
        }

        private void PopulateList()
        {
            listBox1.Items.Clear();
            foreach (Path i in controller.Data.Records)
            {
                listBox1.Items.Add($"ID {i.ID}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            this.SuspendDrawing();

            path = (Path)controller.Data.Records[listBox1.SelectedIndex];
            pos_i = par_i = 0;
            File.SelectItem(path, pos_i);
            splitContainer1.Panel2.Enabled = true;

            ignore_value_change = true;
            numericUpDown5.Value = path.ID;
            ignore_value_change = false;
            numericUpDown6.Maximum = path.Positions.Count > 0 ? path.Positions.Count : 1;
            numericUpDown9.Maximum = path.Params.Count > 0 ? path.Params.Count : 1;
            UpdatePosition();
            UpdateParam();

            this.ResumeDrawing();
        }

        private void UpdatePosition()
        {
            ignore_value_change = true;
            if (path.Positions.Count > 0)
            {
                label5.Text = $"/ {path.Positions.Count}";
                label5.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = true;
                numericUpDown6.Enabled = true;
                button2.Enabled = true;
                numericUpDown1.Value = (decimal)path.Positions[pos_i].X;
                numericUpDown2.Value = (decimal)path.Positions[pos_i].Y;
                numericUpDown3.Value = (decimal)path.Positions[pos_i].Z;
                numericUpDown4.Value = (decimal)path.Positions[pos_i].W;
                numericUpDown6.Value = pos_i + 1;
            }
            else
            {
                label5.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown6.Enabled = false;
                button2.Enabled = false;
            }
            ignore_value_change = false;
        }

        private void UpdateParam()
        {
            ignore_value_change = true;
            if (path.Params.Count > 0)
            {
                label8.Text = $"/ {path.Params.Count}";
                label8.Enabled = true;
                numericUpDown7.Enabled = true;
                numericUpDown8.Enabled = true;
                numericUpDown9.Enabled = true;
                button3.Enabled = true;
                numericUpDown7.Value = (decimal)path.Params[par_i].P1;
                numericUpDown8.Value = (decimal)path.Params[par_i].P2;
                numericUpDown9.Value = par_i + 1;
            }
            else
            {
                label8.Enabled = false;
                numericUpDown7.Enabled = false;
                numericUpDown8.Enabled = false;
                numericUpDown9.Enabled = false;
                button3.Enabled = false;
            }
            ignore_value_change = false;
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Positions[pos_i].X = (float)numericUpDown1.Value;
            CurPathCont.UpdateTextBox();
        }

        private void addToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (controller.Data.RecordIDs.Count >= ushort.MaxValue) return;
            uint id;
            for (id = 0; id < uint.MaxValue; ++id)
            {
                if (!controller.Data.ContainsItem(id))
                    break;
            }
            Path new_path = new Path { ID = id, Positions = new List<Pos>(), Params = new List<Path.PathParam>() };
            controller.Data.AddItem(id, new_path);
            ((MainForm)Tag).GenTreeNode(new_path, controller);
            path = new_path;
            listBox1.Items.Add($"ID {path.ID}");
            controller.UpdateTextBox();
            CurPathCont.UpdateText();
        }

        private void removeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var sel_i = listBox1.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.RemoveItem(path.ID);
            listBox1.BeginUpdate();
            listBox1.Items.RemoveAt(sel_i);
            for (int i = 0; i < controller.Data.Records.Count; ++i)
            {
                Path new_pth = (Path)controller.Data.Records[i];
                if (new_pth.ID != i)
                {
                    controller.ChangeID(new_pth.ID, (uint)i);
                    listBox1.Items[i] = $"ID {i}";
                    ((Controller)controller.Node.Nodes[i].Tag).UpdateText();
                }
            }
            if (sel_i >= listBox1.Items.Count) sel_i = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = sel_i;
            listBox1.EndUpdate();
            if (listBox1.Items.Count == 0)
                splitContainer1.Panel2.Enabled = false;
            controller.UpdateTextBox();
        }

        private void numericUpDown2_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Positions[pos_i].Y = (float)numericUpDown2.Value;
            CurPathCont.UpdateTextBox();
        }

        private void numericUpDown3_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Positions[pos_i].Z = (float)numericUpDown3.Value;
            CurPathCont.UpdateTextBox();
        }

        private void numericUpDown4_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Positions[pos_i].W = (float)numericUpDown4.Value;
            CurPathCont.UpdateTextBox();
        }

        private void numericUpDown7_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Params[par_i].P1 = (float)numericUpDown7.Value;
            CurPathCont.UpdateTextBox();
        }

        private void numericUpDown8_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            path.Params[par_i].P2 = (float)numericUpDown8.Value;
            CurPathCont.UpdateTextBox();
        }

        private void numericUpDown6_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            pos_i = (int)numericUpDown6.Value - 1;
            File.SelectItem(path, pos_i);
            UpdatePosition();
        }

        private void numericUpDown9_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            par_i = (int)numericUpDown9.Value - 1;
            UpdateParam();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            path.Positions.Add(new Pos((float)numericUpDown1.Value, (float)numericUpDown2.Value, (float)numericUpDown3.Value, (float)numericUpDown4.Value));
            label5.Text = $"/ {path.Positions.Count}";
            numericUpDown6.Maximum = path.Positions.Count;
            if (path.Positions.Count == 1)
            {
                pos_i = 0;
                UpdatePosition();
            }
            controller.UpdateTextBox();
            CurPathCont.UpdateTextBox();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            path.Params.Add(new Path.PathParam() { P1 = (float)numericUpDown7.Value, P2 = (float)numericUpDown8.Value } );
            label8.Text = $"/ {path.Params.Count}";
            numericUpDown9.Maximum = path.Params.Count;
            if (path.Params.Count == 1)
            {
                par_i = 0;
                UpdateParam();
            }
            controller.UpdateTextBox();
            CurPathCont.UpdateTextBox();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            path.Params.RemoveAt(par_i);
            label8.Text = $"/ {path.Params.Count}";
            numericUpDown9.Maximum = path.Params.Count > 0 ? path.Params.Count : 1;
            UpdateParam();
            controller.UpdateTextBox();
            CurPathCont.UpdateTextBox();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            path.Positions.RemoveAt(pos_i);
            label5.Text = $"/ {path.Positions.Count}";
            numericUpDown6.Maximum = path.Positions.Count > 0 ? path.Positions.Count : 1;
            UpdatePosition();
            controller.UpdateTextBox();
            CurPathCont.UpdateTextBox();
        }
    }
}
