using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class InstanceEditor : Form
    {
        private const string angleFormat = "{0:0.000}º";
        private readonly SectionController controller;
        private Instance ins;
        private InstanceFlagsEditor flagsEditor;
        
        private FileController File { get; set; }
        private TwinsFile FileData => File.Data;

        internal Stack<bool> dirty = new Stack<bool>();
        internal bool Dirty => dirty.Count > 0 && dirty.Peek();

        public InstanceEditor(SectionController c)
        {
            File = c.MainFile;
            controller = c;
            InitializeComponent();
            Text = $"Instance Editor (Section {c.Data.Parent.ID})";
            PopulateList();
            comboBox1.TextChanged += comboBox1_TextChanged;
            FormClosed += InstanceEditor_FormClosed;

            numFlags.Controls.RemoveAt(0);
            numFlags.Controls[0].Width = numFlags.Width - 4;
        }

        private void InstanceEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.SelectItem(null);
            if (flagsEditor != null)
            {
                flagsEditor.Close();
            }
        }

        private void PopulateList()
        {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            foreach (Instance i in controller.Data.Records)
            {
                listBox1.Items.Add(GenTextForList(i));
            }
            listBox1.EndUpdate();
            comboBox1.Items.Clear();
            var s_dic = new SortedDictionary<uint, int>(FileData.GetItem<TwinsSection>(10).GetItem<TwinsSection>(0).RecordIDs);
            foreach (var i in s_dic)
            {
                string obj_name = File.GetObjectName(i.Key);
                obj_name = Utils.TextUtils.TruncateObjectName(obj_name, (ushort)i.Key, "*", "");
                comboBox1.Items.Add($"{i.Key} - {obj_name}");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            this.SuspendDrawing();

            File.SelectItem((Instance)controller.Data.Records[listBox1.SelectedIndex]);
            ins = (Instance)File.SelectedItem;
            tabControl1.Enabled = true;
            dirty.Push(true);
            Control cur = splitContainer1.ActiveControl;
            var tabIndex = tabControl1.SelectedIndex;
            tabControl1.SelectedIndex = -1;
            tabControl1.SelectedIndex = tabIndex; // yes, this is intentional! don't judge me
            splitContainer1.ActiveControl = cur;
            if (flagsEditor != null)
            {
                flagsEditor.Instance = ins;
            }

            dirty.Push(false);

            this.ResumeDrawing();
        }

        private void GetXRot(bool slider, bool num)
        {
            dirty.Push(true);
            if (slider)
                trackBar1.Value = ins.RotX;
            if (num)
                numericUpDown6.Value = ins.RotX;
            label6.Text = string.Format(angleFormat, ins.RotX / (float)(ushort.MaxValue + 1) * 360f);
            dirty.Push(false);
        }

        private void GetYRot(bool slider, bool num)
        {
            dirty.Push(true);
            if (slider)
                trackBar2.Value = ins.RotY;
            if (num)
                numericUpDown7.Value = ins.RotY;
            label7.Text = string.Format(angleFormat, ins.RotY / (float)(ushort.MaxValue + 1) * 360f);
            dirty.Push(false);
        }

        private void GetZRot(bool slider, bool num)
        {
            dirty.Push(true);
            if (slider)
                trackBar3.Value = ins.RotZ;
            if (num)
                numericUpDown8.Value = ins.RotZ;
            label9.Text = string.Format(angleFormat, ins.RotZ / (float)(ushort.MaxValue + 1) * 360f);
            dirty.Push(false);
        }

        private string GenTextForList(Instance instance)
        {
            string obj_name = File.GetObjectName(instance.ObjectID);
            obj_name = Utils.TextUtils.TruncateObjectName(obj_name, instance.ObjectID, "*", "");
            return $"ID {instance.ID} {(obj_name == string.Empty ? string.Empty : $" - {obj_name}")}";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            if (ushort.TryParse(comboBox1.Text.Split(new char[] { ' ' }, 2)[0], out ushort oid))
            {
                ins.ObjectID = oid;
                listBox1.Items[listBox1.SelectedIndex] = GenTextForList(ins);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.Pos.X = (float)numericUpDown2.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.Pos.Y = (float)numericUpDown3.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.Pos.Z = (float)numericUpDown4.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.Pos.W = (float)numericUpDown5.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ins.RotX = (ushort)trackBar1.Value;
            GetXRot(false, true);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ins.RotY = (ushort)trackBar2.Value;
            GetYRot(false, true);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            ins.RotZ = (ushort)trackBar3.Value;
            GetZRot(false, true);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.RotX = (ushort)numericUpDown6.Value;
            GetXRot(true, false);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.COMRotX = (ushort)numericUpDown13.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.RotY = (ushort)numericUpDown7.Value;
            GetYRot(true, false);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.COMRotY = (ushort)numericUpDown14.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.SomeNum1 = (int)numericUpDown9.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.SomeNum2 = (int)numericUpDown10.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.SomeNum3 = (int)numericUpDown11.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.InstanceIDs.Clear();
            for (int i = 0; i < textBox2.Lines.Length; ++i)
            {
                if (ushort.TryParse(textBox2.Lines[i], out ushort v))
                    ins.InstanceIDs.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.PositionIDs.Clear();
            for (int i = 0; i < textBox3.Lines.Length; ++i)
            {
                if (ushort.TryParse(textBox3.Lines[i], out ushort v))
                    ins.PositionIDs.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.PathIDs.Clear();
            for (int i = 0; i < textBox4.Lines.Length; ++i)
            {
                if (ushort.TryParse(textBox4.Lines[i], out ushort v))
                    ins.PathIDs.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.UnkI321.Clear();
            for (int i = 0; i < textBox7.Lines.Length; ++i)
            {
                if (uint.TryParse(textBox7.Lines[i], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out uint v))
                    ins.UnkI321.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.UnkI322.Clear();
            for (int i = 0; i < textBox6.Lines.Length; ++i)
            {
                if (float.TryParse(textBox6.Lines[i], out float v))
                    ins.UnkI322.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.UnkI323.Clear();
            for (int i = 0; i < textBox5.Lines.Length; ++i)
            {
                if (uint.TryParse(textBox5.Lines[i], out uint v))
                    ins.UnkI323.Add(v);
            }
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controller.Data.RecordIDs.Count >= ushort.MaxValue) return;
            uint id;
            for (id = 0; id < uint.MaxValue; ++id)
            {
                if (!controller.Data.ContainsItem(id))
                    break;
            }
            Instance new_ins = new Instance { ID = id, RefList = -1, ScriptID = -1, Pos = new Pos(0, 0, 0, 1), SomeNum1 = 10, SomeNum2 = 10, SomeNum3 = 10, Flags = 0x1CE,
                UnkI322 = new List<float>() { 1 },
                UnkI323 = new List<uint>() { 0, 0 } };
            controller.Data.AddItem(id, new_ins);
            ((MainForm)Tag).GenTreeNode(new_ins, controller);
            ins = new_ins;
            listBox1.Items.Add(GenTextForList(ins));
            controller.UpdateText();
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_i = listBox1.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.RemoveItem(ins.ID);
            listBox1.BeginUpdate();
            listBox1.Items.RemoveAt(sel_i);
            for (int i = 0; i < controller.Data.Records.Count; ++i)
            {
                bool update_text = false;
                Instance new_ins = (Instance)controller.Data.Records[i];
                if (new_ins.ID != i)
                {
                    controller.ChangeID(new_ins.ID, (uint)i);
                    listBox1.Items[i] = GenTextForList(new_ins);
                    ((Controller)controller.Node.Nodes[i].Tag).UpdateName();
                    update_text = true;
                }
                for (int j = 0; j < new_ins.InstanceIDs.Count; ++j)
                {
                    if (new_ins.InstanceIDs[j] > sel_i)
                    {
                        update_text = true;
                        --new_ins.InstanceIDs[j];
                    }
                    else if (new_ins.InstanceIDs[j] == sel_i)
                    {
                        update_text = true;
                        new_ins.InstanceIDs.RemoveAt(j);
                        --j;
                    }
                }
                if (update_text)
                    ((Controller)controller.Node.Nodes[i].Tag).UpdateTextBox();
                update_text = false;
            }

            var trig_sec_c = (SectionController)controller.Node.Parent.Nodes[7].Tag;

            foreach (TreeNode node in trig_sec_c.Node.Nodes)
            {
                bool update_text = false;
                Trigger tr = ((TriggerController)node.Tag).Data;
                for (int j = 0; j < tr.Instances.Count; ++j)
                {
                    if (tr.Instances[j] > sel_i)
                    {
                        update_text = true;
                        tr.Instances[j] -= 1;
                    }
                    else if (tr.Instances[j] == sel_i)
                    {
                        update_text = true;
                        tr.Instances.RemoveAt(j);
                        --j;
                    }
                    else
                        continue;
                }
                if (update_text)
                    ((Controller)node.Tag).UpdateTextBox();
            }
            trig_sec_c.UpdateTextBox();
            if (sel_i >= listBox1.Items.Count) sel_i = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = sel_i;
            listBox1.EndUpdate();
            if (listBox1.Items.Count == 0)
                tabControl1.Enabled = false;
            controller.UpdateTextBox();
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.RotZ = (ushort)numericUpDown8.Value;
            GetZRot(true, false);
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
            File.RMViewer_LoadInstances();
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.COMRotZ = (ushort)numericUpDown15.Value;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void button_PosFromCam_Click(object sender, EventArgs e)
        {
            Pos currentPos = File.RMViewer_GetPos(ins.Pos);
            ins.Pos.X = currentPos.X;
            ins.Pos.Y = currentPos.Y;
            ins.Pos.Z = currentPos.Z;
            numericUpDown2.Value = (decimal)ins.Pos.X;
            numericUpDown3.Value = (decimal)ins.Pos.Y;
            numericUpDown4.Value = (decimal)ins.Pos.Z;
            numericUpDown5.Value = (decimal)ins.Pos.W;
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateTextBox();
            File.RMViewer_LoadInstances();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_i = listBox1.SelectedIndex;
            if (sel_i == -1)
                return;

            Instance last_inst = ins;

            if (controller.Data.RecordIDs.Count >= ushort.MaxValue) return;
            uint id;
            for (id = 0; id < uint.MaxValue; ++id)
            {
                if (!controller.Data.ContainsItem(id))
                    break;
            }
            Instance new_ins = new Instance
            {
                ID = id,
                RefList = -1,
                ScriptID = -1,
                Pos = new Pos(last_inst.Pos.X,last_inst.Pos.Y + 1f,last_inst.Pos.Z, 1),
                RotX = last_inst.RotX,
                RotY = last_inst.RotY,
                RotZ = last_inst.RotZ,
                COMRotX = last_inst.COMRotX,
                COMRotY = last_inst.COMRotY,
                COMRotZ = last_inst.COMRotZ,
                SomeNum1 = last_inst.SomeNum1,
                SomeNum2 = last_inst.SomeNum2,
                SomeNum3 = last_inst.SomeNum3,
                Flags = last_inst.Flags,
                UnkI321 = last_inst.UnkI321,
                UnkI322 = last_inst.UnkI322,
                UnkI323 = last_inst.UnkI323,
                ObjectID = last_inst.ObjectID,
                InstanceIDs = last_inst.InstanceIDs,
                PathIDs = last_inst.PathIDs,
                PositionIDs = last_inst.PositionIDs,
            };
            controller.Data.AddItem(id, new_ins);
            ((MainForm)Tag).GenTreeNode(new_ins, controller);
            ins = new_ins;
            listBox1.Items.Add(GenTextForList(ins));
            controller.UpdateText();
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[ins.ID]].Tag).UpdateText();
        }

        private void btnEditInstanceFlags_Click(object sender, EventArgs e)
        {
            if (flagsEditor == null || flagsEditor.IsDisposed)
            {
                flagsEditor = new InstanceFlagsEditor(ins, numFlags);
            }
            flagsEditor.Show();
        }

        private void numScript_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.ScriptID = (short)numScript.Value;
        }

        private void numRefList_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.RefList = (short)numRefList.Value;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            string obj_name = File.GetObjectName(ins.ObjectID);
            obj_name = Utils.TextUtils.TruncateObjectName(obj_name, ins.ObjectID, "*", "");
            comboBox1.Text = ins.ObjectID.ToString() + ((obj_name == string.Empty) ? string.Empty : $" - {obj_name}");
            numericUpDown12.Value = ins.ID;
            numericUpDown2.Value = (decimal)ins.Pos.X;
            numericUpDown3.Value = (decimal)ins.Pos.Y;
            numericUpDown4.Value = (decimal)ins.Pos.Z;
            numericUpDown5.Value = (decimal)ins.Pos.W;
            numFlags.Value = ins.Flags;
            numericUpDown13.Value = ins.COMRotX;
            numericUpDown14.Value = ins.COMRotY;
            numericUpDown15.Value = ins.COMRotZ;
            GetXRot(true, true); GetYRot(true, true); GetZRot(true, true);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            numScript.Value = ins.ScriptID;
            numRefList.Value = ins.RefList;

            numericUpDown9.Value = ins.SomeNum1;
            numericUpDown10.Value = ins.SomeNum2;
            numericUpDown11.Value = ins.SomeNum3;

            string[] lines = new string[ins.InstanceIDs.Count];
            for (int i = 0; i < ins.InstanceIDs.Count; ++i)
                lines[i] = ins.InstanceIDs[i].ToString();
            textBox2.Lines = lines;
            lines = new string[ins.PositionIDs.Count];
            for (int i = 0; i < ins.PositionIDs.Count; ++i)
                lines[i] = ins.PositionIDs[i].ToString();
            textBox3.Lines = lines;
            lines = new string[ins.PathIDs.Count];
            for (int i = 0; i < ins.PathIDs.Count; ++i)
                lines[i] = ins.PathIDs[i].ToString();
            textBox4.Lines = lines;

            lines = new string[ins.UnkI321.Count];
            for (int i = 0; i < ins.UnkI321.Count; ++i)
                lines[i] = ins.UnkI321[i].ToString("X");
            textBox7.Lines = lines;
            lines = new string[ins.UnkI322.Count];
            for (int i = 0; i < ins.UnkI322.Count; ++i)
                lines[i] = ins.UnkI322[i].ToString();
            textBox6.Lines = lines;
            lines = new string[ins.UnkI323.Count];
            for (int i = 0; i < ins.UnkI323.Count; ++i)
                lines[i] = ins.UnkI323[i].ToString();
            textBox5.Lines = lines;
        }

        private void numFlags_ValueChanged(object sender, EventArgs e)
        {
            if (Dirty) return;
            ins.Flags = (uint)numFlags.Value;
            flagsEditor?.UpdateCheckBoxes();
        }
    }
}
