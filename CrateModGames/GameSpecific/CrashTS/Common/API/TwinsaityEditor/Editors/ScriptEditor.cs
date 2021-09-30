using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Twinsanity;
using System.ComponentModel.Design;

namespace TwinsaityEditor
{
    public partial class ScriptEditor : Form
    {
        private SectionController controller;
        private Script script;

        private Script.HeaderScript selectedHeaderScript;
        private Script.MainScript selectedMainScript;
        private Script.MainScript.SupportType1 selectedType1;
        private Script.MainScript.ScriptStateBody selectedType2;
        private Script.MainScript.ScriptCondition selectedType3;
        private Script.MainScript.ScriptCommand selectedType4;
        private Script.MainScript.ScriptState selectedLinked;
        private FileController File { get; set; }
        private TwinsFile FileData { get => File.Data; }
        private Func<Script, bool> scriptPredicate;
        private List<int> scriptIndices = new List<int>();
        private bool ignoreChange = false;
        public ScriptEditor(SectionController c)
        {
            File = c.MainFile;
            controller = c;
            Text = $"Instance Editor (Section {c.Data.Parent.ID})";
            scriptPredicate = s => { return s.Name.Contains(scriptNameFilter.Text) && s.Main != null; };
            InitializeComponent();
            PopulateList(scriptPredicate);
            UpdatePanels();
            PopulateCommandList();
        }

        private void ScriptEditor_Load(object sender, EventArgs e)
        {
            filterSelection.SelectedIndex = 0;
        }
        private void PopulateCommandList()
        {
            // Populate with current script command knowledge
            for (ushort i = 0; i < Script.MainScript.ScriptCommand.ScriptCommandTableSize; ++i)
            {
                if (Enum.IsDefined(typeof(DefaultEnums.CommandID), i))
                {
                    cbCommandIndex.Items.Add(((DefaultEnums.CommandID)i).ToString());
                } else
                {
                    cbCommandIndex.Items.Add("Unexisting/Unknown " + i.ToString());
                }
            }
        }
        private void PopulateList()
        {
            PopulateList(s => true);
        }
        private void PopulateList(Func<Script, bool> predicate)
        {
            scriptListBox.BeginUpdate();
            scriptListBox.Items.Clear();
            scriptIndices.Clear();
            var index = 0;
            foreach (Script i in controller.Data.Records)
            {
                if (predicate.Invoke(i))
                {
                    scriptIndices.Add(index);
                    scriptListBox.Items.Add(GenTextForList(i));
                }
                ++index;
            }
            scriptListBox.EndUpdate();
        }
        private string GenTextForList(Script script)
        {
            if (script.script != null && script.script.Length > 0) // warn if there are leftovers
            {
                return $"(!)ID {script.ID} {(script.Name == string.Empty ? string.Empty : $" - {script.Name}")}";
            }
            else
            {
                return $"ID {script.ID} {(script.Name == string.Empty ? string.Empty : $" - {script.Name}")}";
            }
        }
        private void BuildTree()
        {
            scriptTree.BeginUpdate();
            scriptTree.Nodes.Clear();
            if (null != script)
            {
                scriptTree.Nodes.Add(script.Name);
                if (script.Header != null)
                {
                    scriptTree.TopNode.Nodes.Add("Header Script").Tag = script.Header;
                }
                if (script.Main != null)
                {
                    TreeNode mainScriptNode = scriptTree.TopNode.Nodes.Add("Main Script - State: " + script.Main.unkInt2);
                    mainScriptNode.Tag = script.Main;
                    Script.MainScript mainScript = script.Main;
                    Script.MainScript.ScriptState ptr = mainScript.scriptState1;
                    while (ptr != null)
                    {
                        AddLinked(mainScriptNode, ptr);
                        ptr = ptr.nextState;
                    }
                }
                scriptTree.Nodes[0].ExpandAll();
                scriptTree.Nodes[0].EnsureVisible();
                scriptTree.EndUpdate();
            }
        }
        private void AddLinked(TreeNode parent, Script.MainScript.ScriptState ptr)
        {
            string Name = $"State {parent.Nodes.Count}";
            if (ptr.type1 != null)
            {
                Name += $" + Header";
            }
            if (ptr.scriptIndexOrSlot != -1)
            {
                if (ptr.IsSlot)
                {
                    Name += $" - Object Script #{ptr.scriptIndexOrSlot}";
                }
                else
                {
                    if (Enum.IsDefined(typeof(DefaultEnums.ScriptID), (ushort)ptr.scriptIndexOrSlot))
                    {
                        Name += $" - ID: {ptr.scriptIndexOrSlot} {(DefaultEnums.ScriptID)(ushort)ptr.scriptIndexOrSlot}";
                    }
                    else
                    {
                        Name += $" - Script {ptr.scriptIndexOrSlot}";
                    }
                }
            }
            TreeNode node = parent.Nodes.Add(Name);
            node.Tag = ptr;
            if (null != ptr.type1)
            {
                //AddType1(node, ptr.type1);
            }
            Script.MainScript.ScriptStateBody ptrType2 = ptr.scriptStateBody;
            while (ptrType2 != null)
            {
                AddType2(node, ptrType2);
                ptrType2 = ptrType2.nextScriptStateBody;
            }
        }
        private void AddType1(TreeNode parent, Script.MainScript.SupportType1 ptr)
        {
            TreeNode node = parent.Nodes.Add($"Header");
            node.Tag = ptr;
        }
        private void AddType2(TreeNode parent, Script.MainScript.ScriptStateBody ptr)
        {
            TreeNode node = parent.Nodes.Add($"To State: {ptr.scriptStateListIndex}");
            node.Tag = ptr;
            if (null != ptr.condition)
            {
                AddType3(node, ptr.condition);
            }
            if (!ptr.IsEnabled)
            {
                node.Text = string.Format("{1} {0}", node.Text, "(OFF)");
            }
            Script.MainScript.ScriptCommand ptrType4 = ptr.command;
            while (ptrType4 != null)
            {
                AddType4(node, ptrType4);
                ptrType4 = ptrType4.nextCommand;
            }
        }
        private void AddType3(TreeNode parent, Script.MainScript.ScriptCondition ptr)
        {
            string Name = $"Condition {ptr.VTableIndex}";
            bool IsDefined = false;
            if (Enum.IsDefined(typeof(DefaultEnums.ConditionID), ptr.VTableIndex))
            {
                Name = ((DefaultEnums.ConditionID)ptr.VTableIndex).ToString();
                IsDefined = true;
            }
            if (ptr.NotGate)
            {
                Name = "NOT " + Name;
            }

            parent.Text = string.Format("{1} - {0}", parent.Text, Name);
            /*
            TreeNode node = parent.Nodes.Add(Name);
            node.Tag = ptr;
            if (!IsDefined)
            {
                node.ForeColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
            }
            */
            
        }
        private void AddType4(TreeNode parent, Script.MainScript.ScriptCommand ptr)
        {
            string Name = $"Command {ptr.VTableIndex}";
            bool IsDefined = false;
            if (Enum.IsDefined(typeof(DefaultEnums.CommandID), ptr.VTableIndex))
            {
                Name = ((DefaultEnums.CommandID)ptr.VTableIndex).ToString();
                IsDefined = true;
            }

            TreeNode node = parent.Nodes.Add(Name);
            node.Tag = ptr;
            if (!ptr.isValidBits())
            {
                node.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }
            else if (!IsDefined)
            {
                node.ForeColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
            }
        }
        private void UpdatePanels()
        {
            panelHeader.Visible = false;
            panelMain.Visible = false;
            panelType1.Visible = false;
            panelType2.Visible = false;
            panelType3.Visible = false;
            panelType4.Visible = false;
            panelLinked.Visible = false;
            panelGeneral.Visible = false;
            if (null != scriptTree.SelectedNode)
            {
                Object tag = scriptTree.SelectedNode.Tag;
                if (tag == null)
                {
                    panelGeneral.Visible = true;
                    UpdateGeneralPanel();
                }
                if (tag is Script.HeaderScript)
                {
                    panelHeader.Visible = true;
                    selectedHeaderScript = (Script.HeaderScript)tag;
                    UpdateHeaderPanel();
                }
                if (tag is Script.MainScript)
                {
                    panelMain.Visible = true;
                    selectedMainScript = (Script.MainScript)tag;
                    UpdateMainPanel();
                }
                if (tag is Script.MainScript.SupportType1)
                {
                    panelType1.Visible = true;
                    selectedType1 = (Script.MainScript.SupportType1)tag;
                    UpdateType1Panel();
                }
                if (tag is Script.MainScript.ScriptStateBody)
                {
                    panelType2.Visible = true;
                    selectedType2 = (Script.MainScript.ScriptStateBody)tag;
                    UpdateType2Panel();

                    ignoreChange = true;
                    if (selectedType2.condition != null)
                    {
                        //panelType2.Visible = false;
                        checkBox_type2_cond_toggle.Checked = true;
                        panelType3.Visible = true;
                        selectedType3 = selectedType2.condition;
                        UpdateType3Panel();
                    }
                    else
                    {
                        checkBox_type2_cond_toggle.Checked = false;
                        panelType3.Visible = false;
                    }
                    ignoreChange = false;
                    
                }
                if (tag is Script.MainScript.ScriptCondition)
                {
                    panelType3.Visible = true;
                    selectedType3 = (Script.MainScript.ScriptCondition)tag;
                    UpdateType3Panel();
                }
                if (tag is Script.MainScript.ScriptCommand)
                {
                    panelType4.Visible = true;
                    selectedType4 = (Script.MainScript.ScriptCommand)tag;
                    UpdateType4Panel();
                }
                if (tag is Script.MainScript.ScriptState)
                {
                    panelLinked.Visible = true;
                    selectedLinked = (Script.MainScript.ScriptState)tag;
                    UpdateLinkedPanel();

                    ignoreChange = true;
                    if (selectedLinked.type1 != null)
                    {
                        checkBox_state_header_toggle.Checked = true;
                        panelType1.Visible = true;
                        selectedType1 = selectedLinked.type1;
                        UpdateType1Panel();
                    }
                    else
                    {
                        checkBox_state_header_toggle.Checked = false;
                        panelType1.Visible = false;
                    }
                    ignoreChange = false;
                }
                UpdateNodeName();
            }
        }

        private void UpdateHeaderPanel()
        {
            headerSubScripts.Items.Clear();
            foreach (Script.HeaderScript.UnkIntPairs pair in selectedHeaderScript.pairs)
            {
                headerSubScripts.Items.Add(pair);
            }
            if (headerSubScripts.Items.Count > 0)
            {
                headerSubScripts.SelectedIndex = 0;
            }
        }
        private void headerSubScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (headerSubScripts.SelectedItem != null)
            {
                Script.HeaderScript.UnkIntPairs pair = selectedHeaderScript.pairs[headerSubScripts.SelectedIndex];
                headerSubscriptID.Text = (pair.mainScriptIndex - 1).ToString();
                headerSubscriptArg.Text = pair.unkInt2.ToString();
            }
        }
        private void headerSubscriptID_TextChanged(object sender, EventArgs e)
        {
            if (headerSubScripts.SelectedItem != null)
            {
                Script.HeaderScript.UnkIntPairs pair = selectedHeaderScript.pairs[headerSubScripts.SelectedIndex];
                TextBox textBox = (TextBox)sender;
                int val = pair.mainScriptIndex;
                if (int.TryParse(textBox.Text, out val))
                {
                    textBox.BackColor = Color.White;
                    pair.mainScriptIndex = val + 1;
                    headerSubScripts.SelectedItem = pair;
                    headerSubScripts.Text = headerSubScripts.SelectedItem.ToString();
                }
                else
                {
                    textBox.BackColor = Color.Red;
                }

            }
        }

        private void headerSubscriptArg_TextChanged(object sender, EventArgs e)
        {
            if (headerSubScripts.SelectedItem != null)
            {
                Script.HeaderScript.UnkIntPairs pair = selectedHeaderScript.pairs[headerSubScripts.SelectedIndex];
                TextBox textBox = (TextBox)sender;
                UInt32 val = pair.unkInt2;
                if (UInt32.TryParse(textBox.Text, out val))
                {
                    textBox.BackColor = Color.White;
                    pair.unkInt2 = val;
                    headerSubScripts.SelectedItem = pair;
                    headerSubScripts.Text = headerSubScripts.SelectedItem.ToString();
                }
                else
                {
                    textBox.BackColor = Color.Red;
                }

            }
        }
        private void UpdateMainPanel()
        {
            mainName.Text = selectedMainScript.name;
            mainLinkedCnt.Text = selectedMainScript.StatesAmount.ToString();
            mainUnk.Text = selectedMainScript.unkInt2.ToString();
            mainLinkedPos.Text = "0";
            UpdateNodeName();
        }
        private void mainName_TextChanged(object sender, EventArgs e)
        {
            selectedMainScript.name = ((TextBox)sender).Text;
            scriptTree.TopNode.Text = selectedMainScript.name;
            //scriptListBox.Items[scriptListBox.SelectedIndex] = GenTextForList(script); Fuck this shit for being unstable piss that destroys my will to live
        }

        private void mainUnk_TextChanged(object sender, EventArgs e)
        {
            int val = selectedMainScript.unkInt2;
            if (int.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedMainScript.unkInt2 = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            UpdateNodeName();
        }

        private void mainLinkedPos_TextChanged(object sender, EventArgs e)
        {

        }

        private void mainAddLinked_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(mainLinkedPos.Text, out val))
            {
                if (selectedMainScript.AddLinkedScript(val))
                {
                    TreeNode mainNode = scriptTree.SelectedNode;
                    mainNode.Nodes.Clear();
                    Script.MainScript.ScriptState ptr = selectedMainScript.scriptState1;
                    while (ptr != null)
                    {
                        AddLinked(mainNode, ptr);
                        ptr = ptr.nextState;
                    }
                    UpdateMainPanel();
                }
            }
        }

        private void mainDelLinked_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(mainLinkedPos.Text, out val))
            {
                if (selectedMainScript.DeleteLinkedScript(val))
                {
                    TreeNode mainNode = scriptTree.SelectedNode;
                    mainNode.Nodes.Clear();
                    Script.MainScript.ScriptState ptr = selectedMainScript.scriptState1;
                    while (ptr != null)
                    {
                        AddLinked(mainNode, ptr);
                        ptr = ptr.nextState;
                    }
                    UpdateMainPanel();
                }
            }
        }
        bool blockType1IndexChanged = false;
        private void UpdateType1Panel()
        {
            type1UnkByte1.Text = selectedType1.unkByte1.ToString();
            type1UnkByte2.Text = selectedType1.unkByte2.ToString();
            type1UnkShort.Text = selectedType1.unkUShort1.ToString();
            type1UnkInt.Text = selectedType1.unkInt1.ToString();
            blockType1IndexChanged = true;
            UpdateType1Bytes();
            UpdateType1Floats();
            blockType1IndexChanged = false;
            UpdateNodeName();
        }
        private void UpdateType1Bytes()
        {
            type1Bytes.BeginUpdate();
            type1Bytes.Items.Clear();
            int i = 0;
            foreach (Byte b in selectedType1.bytes)
            {
                if (b == 255)
                {
                    type1Bytes.Items.Add($"{i:000}: N/A");
                }
                else if (b > 127)
                {
                    type1Bytes.Items.Add($"{i:000}: Inst. Float #{b - 128}");
                }
                else
                {
                    type1Bytes.Items.Add($"{i:000}: {b}");
                }
                
                ++i;
            }
            type1Bytes.EndUpdate();
        }
        private void UpdateType1Floats()
        {
            type1Floats.BeginUpdate();
            type1Floats.Items.Clear();
            int i = 0;
            foreach (Single f in selectedType1.floats)
            {
                type1Floats.Items.Add($"{i:000}: {f}");
                ++i;
            }
            type1Floats.EndUpdate();
        }
        private string GetTextFromArray(Byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Byte b in array)
            {
                builder.Append($"{b:X2} ");
            }
            return builder.ToString();
        }
        private void type1UnkByte1_TextChanged(object sender, EventArgs e)
        {
            Byte val = selectedType1.unkByte1;
            if (Byte.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType1.unkByte1 = val;
                blockType1IndexChanged = true;
                UpdateType1Bytes();
                blockType1IndexChanged = false;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            if (selectedType1.isValidArraySize())
            {
                type1Warning.Visible = false;
            }
            else
            {
                type1Warning.Visible = true;

            }
        }

        private void type1UnkByte2_TextChanged(object sender, EventArgs e)
        {
            Byte val = selectedType1.unkByte2;
            if (Byte.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType1.unkByte2 = val;
                blockType1IndexChanged = true;
                UpdateType1Floats();
                blockType1IndexChanged = false;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
            if (selectedType1.isValidArraySize())
            {
                type1Warning.Visible = false;
            }
            else
            {
                type1Warning.Visible = true;

            }
        }

        private void type1UnkShort_TextChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType1.unkUShort1;
            if (UInt16.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType1.unkUShort1 = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
        }

        private void type1UnkInt_TextChanged(object sender, EventArgs e)
        {
            int val = selectedType1.unkInt1;
            if (int.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType1.unkInt1 = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
            }
        }

        private void type1Array_TextChanged(object sender, EventArgs e)
        {

        }

        private void type1Bytes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!blockType1IndexChanged)
            {
                ListBox list = (ListBox)sender;
                if (list.SelectedItem != null)
                {
                    type1Byte.Text = selectedType1.bytes[list.SelectedIndex].ToString();
                }
            }
        }

        private void type1Floats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!blockType1IndexChanged)
            {
                ListBox list = (ListBox)sender;
                if (list.SelectedItem != null)
                {
                    type1Float.Text = selectedType1.floats[list.SelectedIndex].ToString();
                }
            }
        }
        private void UpdateType2Panel()
        {
            type2Bitfield.Text = Convert.ToString(selectedType2.bitfield, 16);
            type2Slot.Text = selectedType2.scriptStateListIndex.ToString();
            type2TransitionEnabled.Checked = (selectedType2.bitfield & 0x400) != 0;
            UpdateNodeName();
        }
        private void type2Bitfield_TextChanged(object sender, EventArgs e)
        {
            int val = selectedType2.bitfield;
            if (int.TryParse(((TextBox)sender).Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType2.bitfield = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            if (selectedType2.isBitFieldValid())
            {
                type2BitfieldWarning.Visible = false;
            }
            else
            {
                type2BitfieldWarning.Visible = true;
            }
        }

        private void type2Slot_TextChanged(object sender, EventArgs e)
        {
            int val = selectedType2.scriptStateListIndex;
            if (int.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType2.scriptStateListIndex = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            UpdateNodeName();
        }

        private void type2CreateType3_Click(object sender, EventArgs e)
        {
            if (selectedType2.CreateCondition())
            {
                TreeNode node = scriptTree.SelectedNode;
                node.Nodes.Clear();
                if (selectedType2.condition != null)
                {
                    AddType3(node, selectedType2.condition);
                }
                Script.MainScript.ScriptCommand ptr = selectedType2.command;
                while (ptr != null)
                {
                    AddType4(node, ptr);
                    ptr = ptr.nextCommand;
                }
                UpdateType2Panel();
            }
        }

        private void type2DeleteType3_Click(object sender, EventArgs e)
        {
            if (selectedType2.DeleteCondition())
            {
                TreeNode node = scriptTree.SelectedNode;
                node.Nodes.Clear();
                if (selectedType2.condition != null)
                {
                    AddType3(node, selectedType2.condition);
                }
                Script.MainScript.ScriptCommand ptr = selectedType2.command;
                while (ptr != null)
                {
                    AddType4(node, ptr);
                    ptr = ptr.nextCommand;
                }
                UpdateType2Panel();
            }
        }
        private void type2TransitionEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                selectedType2.bitfield |= 0x400;
            }
            else
            {
                selectedType2.bitfield &= ~0x400;
            }
            type2Bitfield.Text = Convert.ToString(selectedType2.bitfield, 16);
            UpdateNodeName();
        }

        private void type2SelectedType4Pos_TextChanged(object sender, EventArgs e)
        {

        }

        private void type2AddType4_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(type2SelectedType4Pos.Text, out val))
            {
                if (selectedType2.AddCommand(val))
                {
                    TreeNode node = scriptTree.SelectedNode;
                    node.Nodes.Clear();
                    if (selectedType2.condition != null)
                    {
                        AddType3(node, selectedType2.condition);
                    }
                    Script.MainScript.ScriptCommand ptr = selectedType2.command;
                    while (ptr != null)
                    {
                        AddType4(node, ptr);
                        ptr = ptr.nextCommand;
                    }
                    UpdateType2Panel();
                }
            }
        }

        private void type2DeleteType4_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(type2SelectedType4Pos.Text, out val))
            {
                if (selectedType2.DeleteCommand(val))
                {
                    TreeNode node = scriptTree.SelectedNode;
                    node.Nodes.Clear();
                    if (selectedType2.condition != null)
                    {
                        AddType3(node, selectedType2.condition);
                    }
                    Script.MainScript.ScriptCommand ptr = selectedType2.command;
                    while (ptr != null)
                    {
                        AddType4(node, ptr);
                        ptr = ptr.nextCommand;
                    }
                    UpdateType2Panel();
                }
            }
        }
        private void UpdateType3Panel()
        {
            type3VTable.Text = selectedType3.VTableIndex.ToString();
            type3UnkShort.Text = selectedType3.UnkData.ToString();
            type3CbNotGate.Checked = selectedType3.NotGate;
            type3X.Text = selectedType3.X.ToString(CultureInfo.InvariantCulture);
            type3Y.Text = selectedType3.Y.ToString(CultureInfo.InvariantCulture);
            type3Z.Text = selectedType3.Z.ToString(CultureInfo.InvariantCulture);
            UpdateNodeName();
        }
        private void type3VTable_TextChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType3.VTableIndex;
            if (UInt16.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType3.VTableIndex = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            UpdateNodeName();
        }

        private void type3UnkShort_TextChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType3.UnkData;
            if (UInt16.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType3.UnkData = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
        }

        private void type3X_TextChanged(object sender, EventArgs e)
        {
            Single val = selectedType3.X;
            if (Single.TryParse(((TextBox)sender).Text, NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType3.X = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
        }

        private void type3Y_TextChanged(object sender, EventArgs e)
        {
            Single val = selectedType3.Y;
            if (Single.TryParse(((TextBox)sender).Text, NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType3.Y = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
        }

        private void type3Z_TextChanged(object sender, EventArgs e)
        {
            Single val = selectedType3.Z;
            if (Single.TryParse(((TextBox)sender).Text, NumberStyles.Float, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType3.Z = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
        }
        private void UpdateType4Panel()
        {
            cbCommandIndex.SelectedItem = cbCommandIndex.Items[selectedType4.VTableIndex];
            type4BitField.Text = selectedType4.UnkShort.ToString("X4");
            type4Arguments.BeginUpdate();
            type4Arguments.Items.Clear();
            int i = 0;
            foreach (UInt32 arg in selectedType4.arguments)
            {
                type4Arguments.Items.Add($"{i:000}: {arg:X8}");
                ++i;
            }
            type4Arguments.EndUpdate();
            if (selectedType4.arguments.Count > 0)
            {
                ignoreUpdate = 0;
                type4Arguments.SelectedIndex = 0;
            }
            UpdateNodeName();
        }
        private void type1Byte_TextChanged(object sender, EventArgs e)
        {
            if (type1Bytes.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Byte val = 0;
                if (Byte.TryParse(text, out val))
                {
                    selectedType1.bytes[type1Bytes.SelectedIndex] = val;
                    ((TextBox)sender).BackColor = Color.White;
                    int index = type1Bytes.SelectedIndex;
                    blockType1IndexChanged = true;
                    if (val == 255)
                    {
                        type1Bytes.Items[index] = $"{index:000}: N/A";
                    }
                    else if (val > 127)
                    {
                        type1Bytes.Items[index] = $"{index:000}: Inst. Float #{selectedType1.bytes[index] - 128}";
                    }
                    else
                    {
                        type1Bytes.Items[index] = $"{index:000}: {selectedType1.bytes[index]}";
                    }
                    
                    type1Bytes.SelectedIndex = index;
                    blockType1IndexChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type1Float_TextChanged(object sender, EventArgs e)
        {
            if (type1Floats.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Single val = 0;
                if (Single.TryParse(text, out val))
                {
                    selectedType1.floats[type1Floats.SelectedIndex] = val;
                    ((TextBox)sender).BackColor = Color.White;
                    int index = type1Floats.SelectedIndex;
                    blockType1IndexChanged = true;
                    type1Floats.Items[index] = $"{index:000}: {selectedType1.floats[index]}";
                    type1Floats.SelectedIndex = index;
                    blockType1IndexChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }
        private void type4VTableIndex_TextChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType4.VTableIndex;
            if (UInt16.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType4.VTableIndex = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            type4ExpectedLength.Text = $"Arguments: {selectedType4.GetExpectedSize() / 4}";
            if (selectedType4.isValidBits())
            {
                type4Warning.Visible = false;
            }
            else
            {
                type4Warning.Visible = true;

            }
            UpdateNodeName();
        }

        private void type4BitField_TextChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType4.UnkShort;
            if (UInt16.TryParse(((TextBox)sender).Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedType4.UnkShort = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            if (selectedType4.isValidBits())
            {
                type4Warning.Visible = false;
            }
            else
            {
                type4Warning.Visible = true;

            }
        }

        private void type4Array_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                string[] strs = ((TextBox)sender).Text.Trim(' ').Split(' ');
                Byte[] byteArray = new Byte[strs.Length];
                int i = 0;
                foreach (string str in strs)
                {
                    Byte val = 0;
                    if (Byte.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                    {
                        ((TextBox)sender).BackColor = Color.White;
                        byteArray[i] = val;
                    }
                    else
                    {
                        ((TextBox)sender).BackColor = Color.Red;
                        return;
                    }
                    ++i;
                }
                //selectedType4.byteArray = byteArray;
            }
            else
            {
                //selectedType4.byteArray = new byte[0];
            }

            if (selectedType4.isValidBits())
            {
                type4Warning.Visible = false;
            }
            else
            {
                type4Warning.Visible = true;

            }
        }
        private void type4Arguments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreUpdate == 0)
            {
                ListBox listBox = (ListBox)sender;
                if (listBox.SelectedItem != null)
                {
                    UpdateArgRepresentations(selectedType4.arguments[listBox.SelectedIndex]);
                }
            }
        }
        private void UpdateArgRepresentations(UInt32 val)
        {
            if (ignoreUpdate != 0 && type4Arguments.SelectedIndex >= 0)
            {
                int index = type4Arguments.SelectedIndex;
                type4Arguments.Items[index] = $"{index:000}: {val:X8}";
                type4Arguments.SelectedIndex = index;
            }
            if (ignoreUpdate != 1) type4ArgHEX.Text = val.ToString("X8");
            if (ignoreUpdate != 2) type4ArgInt32.Text = val.ToString();
            if (ignoreUpdate != 3) type4ArgFloat.Text = (BitConverter.ToSingle(BitConverter.GetBytes(val), 0)).ToString();
            if (ignoreUpdate != 4) type4ArgInt16_1.Text = (val & 0xFFFF).ToString();
            if (ignoreUpdate != 5) type4ArgInt16_2.Text = ((val & 0xFFFF0000) >> 16).ToString();
            if (ignoreUpdate != 6) type4ArgByte1.Text = ((val & 0xFF) >> 0).ToString();
            if (ignoreUpdate != 7) type4ArgByte2.Text = ((val & 0xFF00) >> 8).ToString();
            if (ignoreUpdate != 8) type4ArgByte3.Text = ((val & 0xFF0000) >> 16).ToString();
            if (ignoreUpdate != 9) type4ArgByte4.Text = ((val & 0xFF000000) >> 24).ToString();
            if (ignoreUpdate != 10) type4ArgSignedInt32.Text = ((int)val).ToString();
            if (ignoreUpdate != 11) type4ArgSignedInt16_1.Text = ((Int16)(val & 0xFFFF)).ToString();
            if (ignoreUpdate != 12) type4ArgSignedInt16_2.Text = ((Int16)((val & 0xFFFF0000) >> 16)).ToString();
            if (ignoreUpdate != 13) type4ArgBinary.Text = Convert.ToString(val,2).PadLeft(32,'0');
        }
        private bool stopChanged = false;
        private int ignoreUpdate = 0;
        private void type4ArgHEX_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                UInt32 val = 0;
                if (UInt32.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = val;
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 1;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                } else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgInt32_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                UInt32 val = 0;
                if (UInt32.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = val;
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 2;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgFloat_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Single val = 0;
                if (Single.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = BitConverter.ToUInt32(BitConverter.GetBytes(val), 0);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 3;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgInt16_1_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                UInt16 val = 0;
                if (UInt16.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] =  (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFF0000) | (UInt32)(val & 0xFFFF);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 4;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgInt16_2_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                UInt16 val = 0;
                if (UInt16.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (UInt32)(val << 16) | (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFF);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 5;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgByte1_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Byte val = 0;
                if (Byte.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFFFF00) | (UInt32)((val & 0xFF) << 0);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 6;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgByte2_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Byte val = 0;
                if (Byte.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFF00FF) | (UInt32)((val & 0xFF) << 8);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 7;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgByte3_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Byte val = 0;
                if (Byte.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFF00FFFF) | (UInt32)((val & 0xFF) << 16);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 8;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgByte4_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Byte val = 0;
                if (Byte.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (selectedType4.arguments[type4Arguments.SelectedIndex] & 0x00FFFFFF) | (UInt32)((val & 0xFF) << 24);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 9;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }
        private void type4ArgSignedInt32_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                int val = 0;
                if (int.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (UInt32)val;
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 2;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 10;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgSignedInt16_1_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Int16 val = 0;
                if (Int16.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFF0000) | (UInt32)((UInt16)val & 0xFFFF);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 11;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }

        private void type4ArgSignedInt16_2_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                string text = ((TextBox)sender).Text;
                Int16 val = 0;
                if (Int16.TryParse(text, out val))
                {
                    selectedType4.arguments[type4Arguments.SelectedIndex] = (UInt32)((UInt16)val << 16) | (selectedType4.arguments[type4Arguments.SelectedIndex] & 0xFFFF);
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 12;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }
        private void type4ArgBinary_TextChanged(object sender, EventArgs e)
        {
            if (type4Arguments.SelectedIndex >= 0 && !stopChanged)
            {
                try
                {
                    string text = ((TextBox)sender).Text;
                    UInt32 val = Convert.ToUInt32(text, 2);
                    selectedType4.arguments[type4Arguments.SelectedIndex] = val;
                    ((TextBox)sender).BackColor = Color.White;
                    stopChanged = true;
                    ignoreUpdate = 13;
                    UpdateArgRepresentations(selectedType4.arguments[type4Arguments.SelectedIndex]);
                    ignoreUpdate = 0;
                    stopChanged = false;
                } 
                catch
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
            }
        }
        private void UpdateLinkedPanel()
        {
            linkedBitField.Text = selectedLinked.bitfield.ToString("X4");
            linkedSlotIndex.Text = selectedLinked.scriptIndexOrSlot.ToString();
            checkBox_localScriptSlot.Checked = selectedLinked.IsSlot;
            UpdateNodeName();
        }
        private void linkedBitField_TextChanged(object sender, EventArgs e)
        {
            Int16 val = selectedLinked.bitfield;
            if (Int16.TryParse(((TextBox)sender).Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedLinked.bitfield = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            if (selectedLinked.isValidBits())
            {
                linkedWarning.Visible = false;
            }
            else
            {
                linkedWarning.Visible = true;
            }
        }

        private void linkedSlotIndex_TextChanged(object sender, EventArgs e)
        {
            Int16 val = selectedLinked.scriptIndexOrSlot;
            if (Int16.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                selectedLinked.scriptIndexOrSlot = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
            UpdateNodeName();
        }

        private void linkedCreateType1_Click(object sender, EventArgs e)
        {
            if (selectedLinked.CreateType1())
            {
                TreeNode node = scriptTree.SelectedNode;
                node.Nodes.Clear();
                if (selectedLinked.type1 != null)
                {
                    AddType1(node, selectedLinked.type1);
                }
                Script.MainScript.ScriptStateBody ptr = selectedLinked.scriptStateBody;
                while (ptr != null)
                {
                    AddType2(node, ptr);
                    ptr = ptr.nextScriptStateBody;
                }
                UpdateLinkedPanel();
            }
        }

        private void linkedDeleteType1_Click(object sender, EventArgs e)
        {
            if (selectedLinked.DeleteType1())
            {
                TreeNode node = scriptTree.SelectedNode;
                node.Nodes.Clear();
                if (selectedLinked.type1 != null)
                {
                    AddType1(node, selectedLinked.type1);
                }
                Script.MainScript.ScriptStateBody ptr = selectedLinked.scriptStateBody;
                while (ptr != null)
                {
                    AddType2(node, ptr);
                    ptr = ptr.nextScriptStateBody;
                }
                UpdateLinkedPanel();
            }
        }

        private void linkedCreateType2_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(linkedType2Pos.Text, out val))
            {
                if (selectedLinked.AddScriptStateBody(val))
                {
                    TreeNode node = scriptTree.SelectedNode;
                    node.Nodes.Clear();
                    if (selectedLinked.type1 != null)
                    {
                        AddType1(node, selectedLinked.type1);
                    }
                    Script.MainScript.ScriptStateBody ptr = selectedLinked.scriptStateBody;
                    while (ptr != null)
                    {
                        AddType2(node, ptr);
                        ptr = ptr.nextScriptStateBody;
                    }
                    UpdateLinkedPanel();
                }
            }
        }

        private void linkedDeleteType2_Click(object sender, EventArgs e)
        {
            int val = 0;
            if (int.TryParse(linkedType2Pos.Text, out val))
            {
                if (selectedLinked.DeleteScriptStateBody(val))
                {
                    TreeNode node = scriptTree.SelectedNode;
                    node.Nodes.Clear();
                    if (selectedLinked.type1 != null)
                    {
                        AddType1(node, selectedLinked.type1);
                    }
                    Script.MainScript.ScriptStateBody ptr = selectedLinked.scriptStateBody;
                    while (ptr != null)
                    {
                        AddType2(node, ptr);
                        ptr = ptr.nextScriptStateBody;
                    }
                    UpdateLinkedPanel();
                }
            }
        }
        private void UpdateGeneralPanel()
        {
            generalId.Text = script.ID.ToString();
            generalArray.Text = GetTextFromArray(script.script);
            if (script.script.Length == 0)
            {
                generalWarning.Visible = false;
            }
            else
            {
                generalWarning.Visible = true;
            }
        }
        private void generalArray_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                string[] strs = ((TextBox)sender).Text.Trim(' ').Split(' ');
                Byte[] byteArray = new Byte[strs.Length];
                int i = 0;
                foreach (string str in strs)
                {
                    Byte val = 0;
                    if (Byte.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                    {
                        ((TextBox)sender).BackColor = Color.White;
                        byteArray[i] = val;
                    }
                    else
                    {
                        ((TextBox)sender).BackColor = Color.Red;
                        return;
                    }
                    ++i;
                }
                script.script = byteArray;
            }
            else
            {
                script.script = new Byte[0];
            }
            if (script.script.Length == 0)
            {
                generalWarning.Visible = false;
            }
            else
            {
                generalWarning.Visible = true;
            }
        }
        private void generalId_TextChanged_1(object sender, EventArgs e)
        {
            UInt32 val = 0;
            if (UInt32.TryParse(((TextBox)sender).Text, out val))
            {
                ((TextBox)sender).BackColor = Color.White;
                script.ID = val;
            }
            else
            {
                ((TextBox)sender).BackColor = Color.Red;
                return;
            }
        }
        private void scriptListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != scriptListBox.SelectedItem)
            {
                File.SelectItem((Script)controller.Data.Records[scriptIndices[scriptListBox.SelectedIndex]]);
                script = (Script)File.SelectedItem;
            }
            else
            {
                File.SelectItem(null);
                script = null;
            }
            BuildTree();
            UpdatePanels();
        }

        private void scriptTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdatePanels();
        }

        private void panelLinked_Paint(object sender, PaintEventArgs e)
        {

        }

        private void scriptNameFilter_TextChanged(object sender, EventArgs e)
        {
            PopulateList(scriptPredicate);
        }

        private void filterSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_showHeaderScripts.Checked)
            {
                switch (filterSelection.SelectedIndex)
                {
                    case 0:
                        scriptPredicate = s =>
                        {
                            return s.Name.ToUpper().Contains(scriptNameFilter.Text.ToUpper());
                        };
                        break;
                    case 1:
                        scriptPredicate = s =>
                        {
                            return scriptNameFilter.TextLength == 0 ||
                                    (scriptNameFilter.Text.All(c => { return char.IsDigit(c); }) &&
                                    s.ID == int.Parse(scriptNameFilter.Text));
                        };
                        break;
                }
            }
            else
            {
                switch (filterSelection.SelectedIndex)
                {
                    case 0:
                        scriptPredicate = s =>
                        {
                            return s.Name.ToUpper().Contains(scriptNameFilter.Text.ToUpper()) && s.Main != null;
                        };
                        break;
                    case 1:
                        scriptPredicate = s =>
                        {
                            return scriptNameFilter.TextLength == 0 ||
                                    (scriptNameFilter.Text.All(c => { return char.IsDigit(c); }) &&
                                    s.ID == int.Parse(scriptNameFilter.Text)) &&
                                    s.Main != null;
                        };
                        break;
                }
            }
        }

        private void deleteScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_i = scriptListBox.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.RemoveItem(script.ID);
            scriptListBox.BeginUpdate();
            scriptListBox.Items.RemoveAt(sel_i);
            if (sel_i >= scriptListBox.Items.Count) sel_i = scriptListBox.Items.Count - 1;
            scriptListBox.SelectedIndex = sel_i;
            scriptListBox.EndUpdate();
            controller.UpdateTextBox();
        }

        private void createScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ushort maxid = (ushort)controller.Data.RecordIDs.Select(p => p.Key).Max();
            ushort id1 = Math.Max((ushort)(8191), maxid);
            ++id1;
            id1 += (ushort)(id1 % 2);
            ushort id2 = id1;
            ++id2;
            Script newScriptHeader = new Script();
            newScriptHeader.Header = new Script.HeaderScript((int)id2);
            newScriptHeader.ID = id1;
            newScriptHeader.Name = "Header Script";
            newScriptHeader.flag = 1;
            controller.Data.AddItem(id1, newScriptHeader);
            scriptIndices.Add(id1);
            ((MainForm)Tag).GenTreeNode(newScriptHeader, controller);

            script = newScriptHeader;
            scriptListBox.Items.Add(GenTextForList(newScriptHeader));
            controller.UpdateText();
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[newScriptHeader.ID]].Tag).UpdateText();
            

            Script newScriptMain = new Script();
            newScriptMain.Main = new Script.MainScript();
            newScriptMain.ID = id2;
            newScriptMain.Name = "New Script";
            controller.Data.AddItem(id2, newScriptMain);
            scriptIndices.Add(id2);
            ((MainForm)Tag).GenTreeNode(newScriptMain, controller);

            scriptListBox.Items.Add(GenTextForList(newScriptMain));
            
            controller.UpdateText();
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[newScriptMain.ID]].Tag).UpdateText();
            PopulateList();
            scriptListBox.SelectedIndex = scriptListBox.Items.Count - 1;
        }

        private void panelType4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox_showHeaderScripts_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_showHeaderScripts.Checked)
            {
                filterSelection_SelectedIndexChanged(null, null);
            }
            else
            {
                switch (filterSelection.SelectedIndex)
                {
                    case 0:
                        scriptPredicate = s =>
                        {
                            return s.Name.ToUpper().Contains(scriptNameFilter.Text.ToUpper()) && s.Main != null;
                        };
                        break;
                    case 1:
                        scriptPredicate = s =>
                        {
                            return scriptNameFilter.TextLength == 0 ||
                                    (scriptNameFilter.Text.All(c => { return char.IsDigit(c); }) &&
                                    s.ID == int.Parse(scriptNameFilter.Text)) &&
                                    s.Main != null;
                        };
                        break;
                }
            }
            scriptNameFilter_TextChanged(null, null);
        }

        private void checkBox_localScriptSlot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_localScriptSlot.Checked)
            {
                selectedLinked.IsSlot = true;
            }
            else
            {
                selectedLinked.IsSlot = false;
            }
            if (sender != this && sender != null)
            {
                UpdateLinkedPanel();
            }
        }

        private void checkBox_type2_cond_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreChange)
            {
                ignoreChange = false;
                return;
            }
            if (selectedType2.condition != null)
            {
                selectedType2.DeleteCondition();
                selectedType3 = null;
                panelType3.Visible = false;
            }
            else
            {
                selectedType2.CreateCondition();
                selectedType3 = selectedType2.condition;
                UpdateType3Panel();
                panelType3.Visible = true;
            }
            UpdateType2Panel();
        }

        private void checkBox_state_header_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreChange)
            {
                ignoreChange = false;
                return;
            }
            if (selectedLinked.type1 != null)
            {
                selectedLinked.DeleteType1();
                selectedType1 = null;
                panelType1.Visible = false;
            }
            else
            {
                selectedLinked.CreateType1();
                selectedType1 = selectedLinked.type1;
                UpdateType1Panel();
                panelType1.Visible = true;
            }
            UpdateLinkedPanel();
        }

        void UpdateNodeName()
        {
            if (null != scriptTree.SelectedNode)
            {
                Object tag = scriptTree.SelectedNode.Tag;
                TreeNode node = scriptTree.SelectedNode;
                if (tag is Script.MainScript)
                {
                    scriptTree.SelectedNode.Text = "Main Script - State: " + script.Main.unkInt2;
                }
                if (tag is Script.MainScript.ScriptStateBody)
                {
                    string Name = $"To State: {selectedType2.scriptStateListIndex}";

                    if (null != selectedType2.condition)
                    {
                        if (Enum.IsDefined(typeof(DefaultEnums.ConditionID), selectedType2.condition.VTableIndex))
                        {
                            if (selectedType2.condition.NotGate)
                            {
                                Name = string.Format("NOT {1} - {0}", Name, ((DefaultEnums.ConditionID)selectedType2.condition.VTableIndex).ToString());
                            }
                            else
                            {
                                Name = string.Format("{1} - {0}", Name, ((DefaultEnums.ConditionID)selectedType2.condition.VTableIndex).ToString());
                            }
                        }
                        else
                        {
                            if (selectedType2.condition.NotGate)
                            {
                                Name = string.Format("NOT {1} - {0}", Name, $"Condition {selectedType2.condition.VTableIndex}");
                            }
                            else
                            {
                                Name = string.Format("{1} - {0}", Name, $"Condition {selectedType2.condition.VTableIndex}");
                            }
                        }
                    }
                    if (!selectedType2.IsEnabled)
                    {
                        Name = string.Format("{1} {0}", Name, "(OFF)");
                    }

                    node.Text = Name;
                }
                if (tag is Script.MainScript.ScriptCommand)
                {
                    string Name = $"Command {selectedType4.VTableIndex}";
                    bool IsDefined = false;
                    if (Enum.IsDefined(typeof(DefaultEnums.CommandID), selectedType4.VTableIndex))
                    {
                        Name = ((DefaultEnums.CommandID)selectedType4.VTableIndex).ToString();
                        IsDefined = true;
                    }

                    if (!selectedType4.isValidBits())
                    {
                        node.ForeColor = Color.FromKnownColor(KnownColor.Red);
                    }
                    else if (!IsDefined)
                    {
                        node.ForeColor = Color.FromKnownColor(KnownColor.ControlDarkDark);
                    }
                    else
                    {
                        node.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    }
                    node.Text = Name;
                }
                if (tag is Script.MainScript.ScriptState)
                {
                    TreeNode parentNode = node.Parent;
                    int index = 0;
                    for (int n = 0; n < parentNode.Nodes.Count; n++)
                    {
                        if (parentNode.Nodes[n].Text == node.Text)
                        {
                            index = n;
                        }
                    }

                    string Name = $"State {index}";
                    if (selectedLinked.type1 != null)
                    {
                        Name += $" + Header";
                    }
                    if (selectedLinked.scriptIndexOrSlot != -1)
                    {
                        if (selectedLinked.IsSlot)
                        {
                            Name += $" - Object Script #{selectedLinked.scriptIndexOrSlot}";
                        }
                        else
                        {
                            if (Enum.IsDefined(typeof(DefaultEnums.ScriptID), (ushort)selectedLinked.scriptIndexOrSlot))
                            {
                                Name += $" - ID: {selectedLinked.scriptIndexOrSlot} {(DefaultEnums.ScriptID)(ushort)selectedLinked.scriptIndexOrSlot}";
                            }
                            else
                            {
                                Name += $" - Script {selectedLinked.scriptIndexOrSlot}";
                            }
                        }
                    }

                    node.Text = Name;
                }
            }
        }

        private void cbCommandIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            UInt16 val = selectedType4.VTableIndex;
            if (UInt16.TryParse(((ComboBox)sender).SelectedIndex.ToString(), out val))
            {
                ((ComboBox)sender).BackColor = Color.White;
                selectedType4.VTableIndex = val;
            }
            else
            {
                ((ComboBox)sender).BackColor = Color.Red;
                return;
            }
            type4ExpectedLength.Text = $"Arguments: {selectedType4.GetExpectedSize() / 4}";
            if (selectedType4.isValidBits())
            {
                type4Warning.Visible = false;
            }
            else
            {
                type4Warning.Visible = true;

            }
            UpdateNodeName();
            UpdateType4Panel();
        }

        private void cbNotGate_CheckedChanged(object sender, EventArgs e)
        {
            selectedType3.NotGate = type3CbNotGate.Checked;
            UpdateNodeName();
        }
    }
}
