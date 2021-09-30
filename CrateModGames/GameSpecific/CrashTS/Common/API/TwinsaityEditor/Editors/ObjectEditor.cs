using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Twinsanity;
using TwinsaityEditor.Utils;
using System.IO;

namespace TwinsaityEditor
{
    public partial class ObjectEditor : Form
    {
        private SectionController controller;
        private GameObject gameObject;
        private FileController File { get; set; }
        private TwinsFile FileData { get => File.Data; }

        private ListManipulatorUInt16 ScriptsManipulator;
        private ListManipulatorUInt16 ObjectsManipulator;
        private ListManipulatorUInt16 AnimationsManipulator;
        private ListManipulatorUInt16 OGIManipulator;
        private ListManipulatorUInt16 SoundManipulator;
        private ListManipulatorUInt16 ParamsManipulator;

        private ListManipulatorUInt16 cScriptsManipulator;
        private ListManipulatorUInt16 cObjectsManipulator;
        private ListManipulatorUInt16 cAnimationsManipulator;
        private ListManipulatorUInt16 cOGIManipulator;
        private ListManipulatorUInt16 cSoundManipulator;
        private ListManipulatorUInt16 cCmManipulator;

        private ListManipulatorUInt32 instFlagsManipulator;
        private ListManipulatorSingle instFloatsManipulator;
        private ListManipulatorUInt32 instIntergersManipulator;
        private ListManipulatorUInt16 unk4Manipulator;
        public ObjectEditor(SectionController c)
        {
            File = c.MainFile;
            controller = c;
            Text = $"Instance Editor (Section {c.Data.Parent.ID})";
            InitializeComponent();
            ScriptsManipulator = new ListManipulatorUInt16(scriptsAdd, scriptsRemove, scriptsSet, scriptsUp, scriptsDown, scriptsListBox, scrtiptIdSource);
            ObjectsManipulator = new ListManipulatorUInt16(objectsAdd, objectsRemove, objectsSet, objectsUp, objectsDown, objectsListBox, objectIdSource);
            OGIManipulator = new ListManipulatorUInt16(ogiAdd, ogiRemove, ogiSet, ogiUp, ogiDown, ogiListBox, ogiIdSource);
            AnimationsManipulator = new ListManipulatorUInt16(animationsAdd, animationsRemove, animationsSet, animationsUp, animationsDown, animationsListBox, animationIdSource);
            SoundManipulator = new ListManipulatorUInt16(soundsAdd, soundsRemove, soundsSet, soundsUp, soundsDown, soundsListBox, soundIdSource);
            ParamsManipulator = new ListManipulatorUInt16(paramsAdd, paramsRemove, paramsSet, paramsUp, paramsDown, paramsListBox, paramSource);

            cScriptsManipulator = new ListManipulatorUInt16(cscriptsAdd, cscriptsRemove, cscriptsSet, cscriptsUp, cscriptsDown, cscriptsList, cscriptIdSource);
            cObjectsManipulator = new ListManipulatorUInt16(cobjectAdd, cobjectRemove, cobjectSet, cobjectUp, cobjectDown, cobjectList, cobjectIdSource);
            cOGIManipulator = new ListManipulatorUInt16(cogiAdd, cogiRemove, cogiSet, cogiUp, cogiDown, cogiList, cogiIdSource);
            cAnimationsManipulator = new ListManipulatorUInt16(canimationAdd, canimationRemove, canimationSet, canimationUp, canimationDown, canimationsList, canimationIdSource);
            cSoundManipulator = new ListManipulatorUInt16(csoundAdd, csoundRemove, csoundSet, csoundUp, csoundDown, csoundsList, csoundIdSource);
            cCmManipulator = new ListManipulatorUInt16(ccmAdd, ccmRemove, ccmSet, ccmUp, ccmDown, ccmList, ccmIdSource);

            instFlagsManipulator = new ListManipulatorUInt32(unk1Add, unk1Remove, unk1Set, unk1Up, unk1Down, instFlagsList, unk1Source);
            instFloatsManipulator = new ListManipulatorSingle(unk2Add, unk2Remove, unk2Set, unk2Up, unk2Down, instFloatsList, unk2Source);
            instIntergersManipulator = new ListManipulatorUInt32(unk3Add, unk3Remove, unk3Set, unk3Up, unk3Down, instIntegersList, unk3Source);
            unk4Manipulator = new ListManipulatorUInt16(unk4Add, unk4Remove, unk4Set, unk4Up, unk4Down, unk4List, unk4Source);
            PopulateList();
            SetRepresentationConversions();
        }
        public void PopulateList()
        {
            objectList.BeginUpdate();
            objectList.Items.Clear();
            foreach (GameObject i in controller.Data.Records)
            {
                objectList.Items.Add(GenTextForList(i));
            }
            objectList.EndUpdate();

            // Populate with current script command knowledge
            for (ushort i = 0; i < Script.MainScript.ScriptCommand.ScriptCommandTableSize; ++i)
            {
                if (Enum.IsDefined(typeof(DefaultEnums.CommandID), i))
                {
                    cbVTableIndexes.Items.Add(((DefaultEnums.CommandID)i).ToString());
                }
                else
                {
                    cbVTableIndexes.Items.Add("Unexisting/Unknown " + i.ToString());
                }
            }
        }
        private void PopulateObjectCommandList()
        {
            var command = gameObject.scriptCommand;
            commandsList.Items.Clear();
            while (command != null)
            {
                if (Enum.IsDefined(typeof(DefaultEnums.CommandID), command.VTableIndex))
                {
                    commandsList.Items.Add((DefaultEnums.CommandID)command.VTableIndex);
                }
                else
                {
                    commandsList.Items.Add($"Command {command.VTableIndex}");
                }
                command = command.nextCommand;
            }
        }
        private string GenTextForList(GameObject gameObject)
        {
            return $"ID {gameObject.ID} {(gameObject.Name == string.Empty ? string.Empty : $" - {gameObject.Name}")}";
        }
        private void ObjectEditor_Load(object sender, EventArgs e)
        {

        }

        private void objectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != objectList.SelectedItem)
            {
                File.SelectItem((GameObject)controller.Data.Records[objectList.SelectedIndex]);
                gameObject = (GameObject)File.SelectedItem;
                InitLists();
            }
            else
            {
                File.SelectItem(null);
                gameObject = null;
            }
        }
        private void InitLists()
        {
            ScriptsManipulator.SetSource(gameObject.Scripts);
            ScriptsManipulator.PopulateList();
            ObjectsManipulator.SetSource(gameObject.Objects);
            ObjectsManipulator.PopulateList();
            OGIManipulator.SetSource(gameObject.OGIs);
            OGIManipulator.PopulateList();
            AnimationsManipulator.SetSource(gameObject.Anims);
            AnimationsManipulator.PopulateList();
            SoundManipulator.SetSource(gameObject.Sounds);
            SoundManipulator.PopulateList();
            ParamsManipulator.SetSource(gameObject.scriptParams);
            ParamsManipulator.PopulateList();

            cScriptsManipulator.SetSource(gameObject.cScripts);
            cScriptsManipulator.PopulateList();
            cObjectsManipulator.SetSource(gameObject.cObjects);
            cObjectsManipulator.PopulateList();
            cOGIManipulator.SetSource(gameObject.cOGIs);
            cOGIManipulator.PopulateList();
            cAnimationsManipulator.SetSource(gameObject.cAnims);
            cAnimationsManipulator.PopulateList();
            cSoundManipulator.SetSource(gameObject.cSounds);
            cSoundManipulator.PopulateList();
            cCmManipulator.SetSource(gameObject.cCM);
            cCmManipulator.PopulateList();

            instFlagsManipulator.SetSource(gameObject.instFlagsList);
            instFlagsManipulator.PopulateList();
            instFloatsManipulator.SetSource(gameObject.instFloatsList);
            instFloatsManipulator.PopulateList();
            instIntergersManipulator.SetSource(gameObject.instIntegerList);
            instIntergersManipulator.PopulateList();
            unk4Manipulator.SetSource(gameObject.cUnk);
            unk4Manipulator.PopulateList();

            nameSource.Text = gameObject.Name;
            objectId.Text = Convert.ToString(gameObject.ID, 10);

            PopulateObjectCommandList();
        }

        private void nameSource_TextChanged(object sender, EventArgs e)
        {
            if (gameObject != null)
            {
                gameObject.Name = ((TextBox)sender).Text;
                //objectList.Items[objectList.SelectedIndex] = gameObject.Name; //for the sake of not breaking anything - don't uncomment. And the deal is not in NPE. At all.
            }
        }

        private void flagSource_TextChanged(object sender, EventArgs e)
        {
            if (gameObject != null)
            {
                UInt32 val = 0;
                if (UInt32.TryParse(((TextBox)sender).Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out val))
                {
                    gameObject.flag = val;
                    ((TextBox)sender).BackColor = Color.White;
                } else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }
                
            }
        }

        private void objectId_TextChanged(object sender, EventArgs e)
        {
            if (gameObject != null)
            {
                UInt32 val = 0;
                if (UInt32.TryParse(((TextBox)sender).Text,  out val))
                {
                    gameObject.ID = val;
                    ((TextBox)sender).BackColor = Color.White;
                }
                else
                {
                    ((TextBox)sender).BackColor = Color.Red;
                }

            }
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel_i = objectList.SelectedIndex;
            if (sel_i == -1)
                return;
            controller.RemoveItem(gameObject.ID);
            objectList.BeginUpdate();
            objectList.Items.RemoveAt(sel_i);
            if (sel_i >= objectList.Items.Count) sel_i = objectList.Items.Count - 1;
            objectList.SelectedIndex = sel_i;
            objectList.EndUpdate();
            controller.UpdateTextBox();
        }

        private void createObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ushort maxid = (ushort)controller.Data.RecordIDs.Select(p => p.Key).Max();
            ushort id = Math.Max((ushort)(8192), maxid);
            ++id;
            GameObject newGameObject = new GameObject();
            newGameObject.ID = id;
            newGameObject.Name = "New Game Object";
            controller.Data.AddItem(id, newGameObject);
            ((MainForm)Tag).GenTreeNode(newGameObject, controller);
            gameObject = newGameObject;
            int i = objectList.Items.Add(GenTextForList(newGameObject));
            objectList.SelectedIndex = i;
            controller.UpdateText();
            ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[newGameObject.ID]].Tag).UpdateText();
        }

        private void duplicateObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gameObject != null)
            {
                ushort maxid = (ushort)controller.Data.RecordIDs.Select(p => p.Key).Max();
                ushort id = Math.Max((ushort)(8192), maxid);
                ++id;
                GameObject newGameObject = new GameObject();
                using (MemoryStream stream = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    gameObject.Save(writer);
                    reader.BaseStream.Position = 0;
                    newGameObject.Load(reader, (int)reader.BaseStream.Length);
                }
                newGameObject.ID = id;
                controller.Data.AddItem(id, newGameObject);
                ((MainForm)Tag).GenTreeNode(newGameObject, controller);
                gameObject = newGameObject;
                int i = objectList.Items.Add(GenTextForList(newGameObject));
                objectList.SelectedIndex = i;
                controller.UpdateText();
                ((Controller)controller.Node.Nodes[controller.Data.RecordIDs[newGameObject.ID]].Tag).UpdateText();
            }
        }

        private void commandsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipUpdate || commandsList.SelectedIndex == -1) return;
            
            var command = gameObject.scriptCommands[commandsList.SelectedIndex];
            cbVTableIndexes.SelectedIndex = command.VTableIndex;
            UpdateCommandFields(command);
        }
        private void ClearRepresentation()
        {
            skipUpdate = true;
            tbHEXRepres.Text = "";
            tbFloatRepres.Text = "";
            tbInt32Repres.Text = "";
            tbUInt32Repres.Text = "";
            tbInt16_1Repres.Text = "";
            tbInt16_2Repres.Text = "";
            tbUint16_1Repres.Text = "";
            tbUInt16_2Repres.Text = "";
            tbByte1Repres.Text = "";
            tbByte2Repres.Text = "";
            tbByte3Repres.Text = "";
            tbByte4Repres.Text = "";
            tbBinaryRepres.Text = "";
            skipUpdate = false;
        }
        private void UpdateRepresentation(uint val, object sender)
        {
            // Imagine C# not having a proper way to temporarily and properly disabling Text firing TextChanged event
            skipUpdate = true;
            if (tbHEXRepres != sender) tbHEXRepres.Text = val.ToString("X8");
            if (tbUInt32Repres != sender)  tbUInt32Repres.Text = val.ToString();
            if (tbFloatRepres != sender)  tbFloatRepres.Text = (BitConverter.ToSingle(BitConverter.GetBytes(val), 0)).ToString();
            if (tbUint16_1Repres != sender)  tbUint16_1Repres.Text = (val & 0xFFFF).ToString();
            if (tbUInt16_2Repres != sender)  tbUInt16_2Repres.Text = ((val & 0xFFFF0000) >> 16).ToString();
            if (tbByte1Repres != sender)  tbByte1Repres.Text = ((val & 0xFF) >> 0).ToString();
            if (tbByte2Repres != sender)  tbByte2Repres.Text = ((val & 0xFF00) >> 8).ToString();
            if (tbByte3Repres != sender)  tbByte3Repres.Text = ((val & 0xFF0000) >> 16).ToString();
            if (tbByte4Repres != sender)  tbByte4Repres.Text = ((val & 0xFF000000) >> 24).ToString();
            if (tbInt32Repres != sender)  tbInt32Repres.Text = ((int)val).ToString();
            if (tbInt16_1Repres != sender)  tbInt16_1Repres.Text = ((Int16)(val & 0xFFFF)).ToString();
            if (tbInt16_2Repres != sender)  tbInt16_2Repres.Text = ((Int16)((val & 0xFFFF0000) >> 16)).ToString();
            if (tbBinaryRepres != sender)  tbBinaryRepres.Text = Convert.ToString(val, 2).PadLeft(32, '0');
            if (sender != lbCommandArguments)
            {
                for (var i = 0; i < gameObject.scriptCommands[commandsList.SelectedIndex].arguments.Count; ++i)
                {
                    var selIndex = lbCommandArguments.SelectedIndex;
                    lbCommandArguments.Items[i] = $"{i:D3}: 0x{gameObject.scriptCommands[commandsList.SelectedIndex].arguments[i]:X8}";
                    lbCommandArguments.SelectedIndex = selIndex;
                }
            }
            skipUpdate = false;
        }
        private struct ArgParseResult
        {
            public uint val;
            public bool success;
        }
        private struct ScriptArgumentParser
        {
            public Func<string, ArgParseResult> parser;
            public uint mask;
            public int shiftAmount;
        }
        private void SetRepresentationConversions()
        {
            tbHEXRepres.Tag = new ScriptArgumentParser
            {
                parser = new Func<string, ArgParseResult>(str =>
                {
                    var result = new ArgParseResult();
                    result.success = UInt32.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result.val);
                    return result;
                }),
                mask = 0xFFFFFFFF,
                shiftAmount = 0
            };
            tbInt32Repres.Tag = new ScriptArgumentParser
            {
                parser = new Func<string, ArgParseResult>(str =>
                {
                    var result = new ArgParseResult();
                    var value = (int)result.val;
                    result.success = int.TryParse(str, out value);
                    result.val = (UInt32)value;
                    return result;
                }),
                mask = 0xFFFFFFFF,
                shiftAmount = 0
            };
            tbUInt32Repres.Tag = new ScriptArgumentParser
            {
                parser = new Func<string, ArgParseResult>(str =>
                {
                    var result = new ArgParseResult();
                    result.success = UInt32.TryParse(str, out result.val);
                    return result;
                }),
                mask = 0xFFFFFFFF,
                shiftAmount = 0
            };
            tbFloatRepres.Tag = new ScriptArgumentParser
            {
                parser = new Func<string, ArgParseResult>(str =>
                {
                    var result = new ArgParseResult();
                    var value = (Single)result.val;
                    result.success = Single.TryParse(str, out value);
                    result.val = (UInt32)value;
                    return result;
                }),
                mask = 0xFFFFFFFF,
                shiftAmount = 0
            };
            var int16_parser = new Func<string, ArgParseResult>(str =>
            {
                var result = new ArgParseResult();
                var value = (Int16)result.val;
                result.success = Int16.TryParse(str, out value);
                result.val = (UInt32)value;
                return result;
            });
            tbInt16_1Repres.Tag = new ScriptArgumentParser
            {
                parser = int16_parser,
                mask = 0xFFFF0000,
                shiftAmount = 0
            };
            tbInt16_2Repres.Tag = new ScriptArgumentParser
            {
                parser = int16_parser,
                mask = 0x0000FFFF,
                shiftAmount = 16
            };
            var uint16_parser = new Func<string, ArgParseResult>(str =>
            {
                var result = new ArgParseResult();
                var value = (UInt16)result.val;
                result.success = UInt16.TryParse(str, out value);
                result.val = (UInt32)value;
                return result;
            });
            tbUint16_1Repres.Tag = new ScriptArgumentParser
            {
                parser = uint16_parser,
                mask = 0xFFFF0000,
                shiftAmount = 16
            };
            tbUInt16_2Repres.Tag = new ScriptArgumentParser
            {
                parser = uint16_parser,
                mask = 0x0000FFFF,
                shiftAmount = 16
            };
            var byteParser = new Func<string, ArgParseResult>(str =>
            {
                var result = new ArgParseResult();
                var value = (Byte)result.val;
                result.success = Byte.TryParse(str, out value);
                result.val = (UInt32)value;
                return result;
            });
            tbByte1Repres.Tag = new ScriptArgumentParser
            {
                parser = byteParser,
                mask = 0xFFFFFF00,
                shiftAmount = 0
            };
            tbByte2Repres.Tag = new ScriptArgumentParser
            {
                parser = byteParser,
                mask = 0xFFFF00FF,
                shiftAmount = 8
            };
            tbByte3Repres.Tag = new ScriptArgumentParser
            {
                parser = byteParser,
                mask = 0xFF00FFFF,
                shiftAmount = 16
            };
            tbByte4Repres.Tag = new ScriptArgumentParser
            {
                parser = byteParser,
                mask = 0x00FFFFFF,
                shiftAmount = 24
            };
            tbBinaryRepres.Tag = new ScriptArgumentParser
            {
                parser = new Func<string, ArgParseResult>(str =>
                {
                    var result = new ArgParseResult
                    {
                        success = true
                    };
                    try
                    {
                        result.val = Convert.ToUInt32(str, 2);
                    }
                    catch
                    {
                        result.success = false;
                    }
                    return result;
                }),
                mask = 0xFFFFFFFF,
                shiftAmount = 0
            };
        }
        private bool skipUpdate = false;
        private void textbox_RepresentationTextChanged(object sender, EventArgs e)
        {
            if (skipUpdate) return;
            TextBox tb = (TextBox)sender;
            var parser = (ScriptArgumentParser)tb.Tag;
            var parseResult = parser.parser(tb.Text);
            if (parseResult.success)
            {
                tb.BackColor = Color.White;
                var outVal = gameObject.scriptCommands[commandsList.SelectedIndex].arguments[lbCommandArguments.SelectedIndex];
                outVal = (outVal & parser.mask) | (parseResult.val << parser.shiftAmount);
                gameObject.scriptCommands[commandsList.SelectedIndex].arguments[lbCommandArguments.SelectedIndex] = outVal;
                UpdateRepresentation(outVal, sender);
            }
            else
            {
                tb.BackColor = Color.Red;
            }
        }
        private void lbCommandArguments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipUpdate) return;
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem != null)
            {
                UpdateRepresentation(gameObject.scriptCommands[commandsList.SelectedIndex].arguments[lb.SelectedIndex], sender);
            }
        }

        private void cbVTableIndexes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var command = gameObject.scriptCommands[commandsList.SelectedIndex];
            UInt16 val = command.VTableIndex;
            if (UInt16.TryParse(((ComboBox)sender).SelectedIndex.ToString(), out val))
            {
                ((ComboBox)sender).BackColor = Color.White;
                command.VTableIndex = val;
            }
            else
            {
                ((ComboBox)sender).BackColor = Color.Red;
                return;
            }
            UpdateCommandName(command);
        }
        private void UpdateCommandName(Script.MainScript.ScriptCommand command)
        {
            var str = $"Command {command.VTableIndex}";
            if (Enum.IsDefined(typeof(DefaultEnums.CommandID), command.VTableIndex))
            {
                str = ((DefaultEnums.CommandID)command.VTableIndex).ToString();
            }
            var selIndex = commandsList.SelectedIndex;
            skipUpdate = true;
            commandsList.Items[commandsList.SelectedIndex] = str;
            skipUpdate = false;
            commandsList.SelectedIndex = selIndex;
            UpdateCommandFields(command);
        }
        private void UpdateCommandFields(Script.MainScript.ScriptCommand command)
        {
            ClearRepresentation();
            lblArguments.Text = $"Arguments: {command.arguments.Count}";
            lbCommandArguments.Items.Clear();
            for (var i = 0; i < command.arguments.Count; ++i)
            {
                lbCommandArguments.Items.Add($"{i:D3}: 0x{command.arguments[i]:X8}");
            }
            tbBitfield.Text = command.UnkShort.ToString("X4");
            tbCommandPosition.Text = "";
        }
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            UInt32 position;
            if (UInt32.TryParse(tbCommandPosition.Text, out position))
            {
                if (position > gameObject.scriptCommandsAmount)
                {
                    position = (UInt32)gameObject.scriptCommandsAmount;
                }
            }
            var newCom = new Script.MainScript.ScriptCommand(gameObject.scriptGameVersion);
            if (position == 0)
            {
                if (gameObject.scriptCommandsAmount > 1)
                {
                    newCom.nextCommand = gameObject.scriptCommands[(int)position];
                    newCom.internalIndex |= 0x1000000;
                }
                gameObject.scriptCommand = newCom;
            }
            else if (position != (UInt32)gameObject.scriptCommandsAmount)
            {
                var prevCom = gameObject.scriptCommands[(int)(position - 1)];
                newCom.nextCommand = prevCom.nextCommand;
                prevCom.nextCommand = newCom;
                newCom.internalIndex |= 0x1000000;
            }
            else
            {
                var prevCom = gameObject.scriptCommands[(int)(position - 1)];
                prevCom.nextCommand = newCom;
                prevCom.internalIndex |= 0x1000000;
            }
            gameObject.scriptCommands.Insert((int)position, newCom);
            gameObject.scriptCommandsAmount++;
            PopulateObjectCommandList();
        }

        private void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            if (commandsList.SelectedItem != null)
            {
                if (gameObject.scriptCommandsAmount > 1 && commandsList.SelectedIndex == gameObject.scriptCommandsAmount - 1)
                {
                    var prevCom = gameObject.scriptCommands[commandsList.SelectedIndex - 1];
                    prevCom.nextCommand = null;
                    prevCom.internalIndex &= 0x70FFFFFF;
                }
                else if (commandsList.SelectedIndex != 0)
                {
                    var remCom = gameObject.scriptCommands[commandsList.SelectedIndex];
                    var prevCom = gameObject.scriptCommands[commandsList.SelectedIndex - 1];
                    prevCom.nextCommand = remCom.nextCommand;
                }
                else
                { // If first command is being removed, set new starting command
                    if (gameObject.scriptCommandsAmount > 1)
                    {
                        gameObject.scriptCommand = gameObject.scriptCommands[1];
                    }
                    else
                    {
                        gameObject.scriptCommand = null;
                    }
                }
                gameObject.scriptCommands.RemoveAt(commandsList.SelectedIndex);
                gameObject.scriptCommandsAmount--;
                ClearRepresentation();
                lbCommandArguments.Items.Clear();
                lblArguments.Text = "Arguments: 0";
                PopulateObjectCommandList();
            }
        }
    }
}
