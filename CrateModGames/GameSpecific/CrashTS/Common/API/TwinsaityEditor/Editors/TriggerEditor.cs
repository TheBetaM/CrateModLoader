using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class TriggerEditor : Form
    {
        private SectionController controller;
        private Trigger trigger;

        private FileController File { get; set; }
        private TwinsFile FileData { get => File.Data; }
        private Controller CurCont { get => (Controller)controller.Node.Nodes[controller.Data.RecordIDs[trigger.ID]].Tag; }

        private bool ignore_value_change;

        public TriggerEditor(SectionController c)
        {
            File = c.MainFile;
            controller = c;
            InitializeComponent();
            Text = $"Trigger Editor (Section {c.Data.Parent.ID})";
            PopulateList();
            FormClosed += TriggerEditor_FormClosed;
        }

        private void TriggerEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.SelectItem(null);
        }

        private void PopulateList()
        {
            listBox1.Items.Clear();
            foreach (Trigger i in controller.Data.Records)
            {
                listBox1.Items.Add($"ID {i.ID}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            this.SuspendDrawing();

            trigger = (Trigger)controller.Data.Records[listBox1.SelectedIndex];
            File.SelectItem(trigger);
            splitContainer1.Panel2.Enabled = true;

            ignore_value_change = true;

            numericUpDown1.Value = trigger.Header;
            numericUpDown2.Value = trigger.Enabled;
            numericUpDown3.Value = (decimal)trigger.SomeFloat;
            numericUpDown4.Value = trigger.SectionHead;
            numericUpDown5.Value = trigger.ID;
            numericUpDown6.Value = trigger.Arg1;
            numericUpDown7.Value = trigger.Arg2;
            numericUpDown8.Value = trigger.Arg3;
            numericUpDown9.Value = trigger.Arg4;
            numericUpDown10.Value = (decimal)trigger.Coords[1].X;
            numericUpDown11.Value = (decimal)trigger.Coords[1].Y;
            numericUpDown12.Value = (decimal)trigger.Coords[1].Z;
            numericUpDown13.Value = (decimal)trigger.Coords[1].W;
            numericUpDown14.Value = (decimal)trigger.Coords[2].X;
            numericUpDown15.Value = (decimal)trigger.Coords[2].Y;
            numericUpDown16.Value = (decimal)trigger.Coords[2].Z;
            numericUpDown17.Value = (decimal)trigger.Coords[2].W;
            double angle = Math.Acos(trigger.Coords[0].W);
            double angle_modify = Math.Sin(angle);
            if (angle_modify == 0)
            {
                numericUpDown18.Value = 0;
                numericUpDown19.Value = 0;
                numericUpDown20.Value = 0;
                numericUpDown21.Value = 0;
            }
            else
            {
                numericUpDown18.Value = (decimal)(trigger.Coords[0].X / angle_modify);
                numericUpDown19.Value = (decimal)(trigger.Coords[0].Y / angle_modify);
                numericUpDown20.Value = (decimal)(trigger.Coords[0].Z / angle_modify);
                numericUpDown21.Value = (decimal)angle * 2;
            }

            var lines = new string[trigger.Instances.Count];
            for (int i = 0; i < trigger.Instances.Count; ++i)
            {
                lines[i] = trigger.Instances[i].ToString();
            }
            textBox1.Lines = lines;

            ignore_value_change = false;

            this.ResumeDrawing();
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
            Trigger new_trigger = new Trigger { ID = id, Parent = controller.Data, Instances = new List<ushort>(), Coords = new Pos[] { new Pos(0, 0, 0, 1), new Pos(0, 0, 0, 1), new Pos(0, 0, 0, 1) } };
            controller.Data.AddItem(id, new_trigger);
            ((MainForm)Tag).GenTreeNode(new_trigger, controller);
            trigger = new_trigger;
            listBox1.Items.Add($"ID {trigger.ID}");
            controller.UpdateTextBox();
            CurCont.UpdateText();
        }

        private void removeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var sel_i = listBox1.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.RemoveItem(trigger.ID);
            listBox1.BeginUpdate();
            listBox1.Items.RemoveAt(sel_i);
            for (int i = 0; i < controller.Data.Records.Count; ++i)
            {
                Trigger new_trg = (Trigger)controller.Data.Records[i];
                if (new_trg.ID != i)
                {
                    controller.ChangeID(new_trg.ID, (uint)i);
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

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Header = (uint)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Enabled = (uint)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.SomeFloat = (float)numericUpDown3.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown4_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.SectionHead = (uint)numericUpDown4.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Arg1 = (ushort)numericUpDown6.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown7_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Arg2 = (ushort)numericUpDown7.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown8_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Arg3 = (ushort)numericUpDown8.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown9_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Arg4 = (ushort)numericUpDown9.Value;
            CurCont.UpdateTextBox();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Instances.Clear();
            for (int i = 0; i < textBox1.Lines.Length; ++i)
            {
                if (ushort.TryParse(textBox1.Lines[i], out ushort v))
                {
                    if (File.GetInstanceID(controller.Data.Parent.ID, v) != null)
                        trigger.Instances.Add(v);
                }
            }
            controller.UpdateTextBox();
            CurCont.UpdateTextBox();
        }

        private void numericUpDown10_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[1].X = (float)numericUpDown10.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown11_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[1].Y = (float)numericUpDown11.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown12_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[1].Z = (float)numericUpDown12.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown13_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[1].W = (float)numericUpDown13.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown14_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[2].X = (float)numericUpDown14.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown15_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[2].Y = (float)numericUpDown15.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown16_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[2].Z = (float)numericUpDown16.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown17_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[2].W = (float)numericUpDown17.Value;
            CurCont.UpdateTextBox();
        }

        private void numericUpDown18_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[0].X = (float)((float)numericUpDown18.Value * Math.Sin((float)numericUpDown21.Value * 2));
            CurCont.UpdateTextBox();
        }

        private void numericUpDown19_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[0].Y = (float)((float)numericUpDown19.Value * Math.Sin((float)numericUpDown21.Value * 2));
            CurCont.UpdateTextBox();
        }

        private void numericUpDown20_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[0].Z = (float)((float)numericUpDown20.Value * Math.Sin((float)numericUpDown21.Value * 2));
            CurCont.UpdateTextBox();
        }

        private void numericUpDown21_ValueChanged(object sender, System.EventArgs e)
        {
            if (ignore_value_change) return;
            trigger.Coords[0].W = (float)Math.Cos((float)numericUpDown21.Value / 2);
            trigger.Coords[0].X = (float)((float)numericUpDown18.Value * Math.Sin((float)numericUpDown21.Value * 2));
            trigger.Coords[0].Y = (float)((float)numericUpDown19.Value * Math.Sin((float)numericUpDown21.Value * 2));
            trigger.Coords[0].Z = (float)((float)numericUpDown20.Value * Math.Sin((float)numericUpDown21.Value * 2));
            CurCont.UpdateTextBox();
        }
    }
}
