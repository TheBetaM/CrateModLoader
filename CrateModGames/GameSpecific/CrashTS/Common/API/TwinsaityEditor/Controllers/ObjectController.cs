using System;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public class ObjectController : ItemController
    {
        public new GameObject Data { get; set; }

        public ObjectController(MainForm topform, GameObject item) : base (topform, item)
        {
            Data = item;
            AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            string obj_name = Data.Name;
            obj_name = Utils.TextUtils.TruncateObjectName(obj_name, (ushort)Data.ID, "*", "");
            return $"{obj_name} [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();
            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add($"Name: {Data.Name}");
            text.Add($"Unknown bitfield: 0x{Data.UnkBitfield:X}");
            text.Add($"Object type: {Data.UnkBitfield >> 0x14 & 0xFF}");
            for (int i = 0; i < Data.ScriptSlots.Count; ++i)
            {
                var slotName = "Reserved";
                var slotAmt = Data.ScriptSlots[i];
                switch(i)
                {
                    case 0:
                        {
                            slotName = "Pairs(OGI/Animation)";
                        }
                        break;
                    case 1:
                        {
                            slotName = "Scripts";
                        }
                        break;
                    case 2:
                        {
                            slotName = "Objects";
                        }
                        break;
                    case 3:
                        {
                            slotName = "UInt32";
                        }
                        break;
                    case 4:
                        {
                            slotName = "Sounds";
                        }
                        break;
                }
                text.Add($"{slotName} script slots: {slotAmt}");
            }

            text.Add($"");
            text.Add($"Reference Data");
            text.Add($"");

            text.Add($"UnknownInt32Count: {Data.UI32.Count}");
            for (int i = 0; i < Data.UI32.Count; ++i)
            {
                var u32 = Data.UI32[i];
                ushort script = (ushort)((u32 >> 0xA) & 0x3FFF);
                string scriptLine = Data.UI32[i].ToString("X");
                scriptLine += " Packed script: " + script.ToString();
                if (Enum.IsDefined(typeof(DefaultEnums.ScriptID), script))
                {
                    scriptLine += " " + (DefaultEnums.ScriptID)script;
                }
                text.Add(scriptLine);
            }

            text.Add($"OGICount: {Data.OGIs.Count}");
            for (int i = 0; i < Data.OGIs.Count; ++i)
                text.Add(Data.OGIs[i].ToString());

            text.Add($"AnimCount: {Data.Anims.Count}");
            for (int i = 0; i < Data.Anims.Count; ++i)
                text.Add(Data.Anims[i].ToString());

            text.Add($"ScriptCount: {Data.Scripts.Count}");
            for (int i = 0; i < Data.Scripts.Count; ++i)
            {
                string script_line = "#" + i.ToString() + ": ";
                if (Enum.IsDefined(typeof(DefaultEnums.GameObjectScriptOrder), i))
                {
                    script_line += "(" + (DefaultEnums.GameObjectScriptOrder)i + ") ";
                }
                if (Data.Scripts[i] != 65535)
                {
                    script_line += Data.Scripts[i].ToString();
                }
                if (Enum.IsDefined(typeof(DefaultEnums.ScriptID), Data.Scripts[i]))
                {
                    script_line += " " + (DefaultEnums.ScriptID)Data.Scripts[i];
                }
                text.Add(script_line);
            }

            text.Add($"ObjectCount: {Data.Objects.Count}");
            for (int i = 0; i < Data.Objects.Count; ++i)
                text.Add(Data.Objects[i].ToString());

            text.Add($"SoundCount: {Data.Sounds.Count}");
            for (int i = 0; i < Data.Sounds.Count; ++i)
                text.Add(Data.Sounds[i].ToString());

            text.Add($"");
            text.Add($"Preload Data");
            text.Add($"");

            text.Add($"ObjectCount: {Data.cObjects.Count}");
            for (int i = 0; i < Data.cObjects.Count; ++i)
                text.Add(Data.cObjects[i].ToString());

            text.Add($"OGICount: {Data.cOGIs.Count}");
            for (int i = 0; i < Data.cOGIs.Count; ++i)
                text.Add(Data.cOGIs[i].ToString());

            text.Add($"AnimCount: {Data.cAnims.Count}");
            for (int i = 0; i < Data.cAnims.Count; ++i)
                text.Add(Data.cAnims[i].ToString());

            text.Add($"CMCount: {Data.cCM.Count}");
            for (int i = 0; i < Data.cCM.Count; ++i)
                text.Add(Data.cCM[i].ToString());

            text.Add($"ScriptCount: {Data.cScripts.Count}");
            for (int i = 0; i < Data.cScripts.Count; ++i)
            {
                string script_line = "#" + i.ToString() + ": ";
                if (Data.cScripts[i] != 65535)
                {
                    script_line += Data.cScripts[i].ToString();
                }
                if (Enum.IsDefined(typeof(DefaultEnums.ScriptID), Data.cScripts[i]))
                {
                    script_line += " " + (DefaultEnums.ScriptID)Data.cScripts[i];
                }
                text.Add(script_line);
            }

            text.Add($"UnkCount: {Data.cUnk.Count}");
            for (int i = 0; i < Data.cUnk.Count; ++i)
                text.Add(Data.cUnk[i].ToString());

            text.Add($"SoundCount: {Data.cSounds.Count}");
            for (int i = 0; i < Data.cSounds.Count; ++i)
                text.Add(Data.cSounds[i].ToString());

            text.Add($"Commands amount: {Data.scriptCommandsAmount}");
            if (Data.scriptCommandsAmount > 0)
            {
                var command = Data.scriptCommand;
                do
                {
                    if (Enum.IsDefined(typeof(DefaultEnums.CommandID), command.VTableIndex))
                    {
                        text.Add($"{(DefaultEnums.CommandID)command.VTableIndex}: {command.VTableIndex}");
                    }
                    else
                    {
                        text.Add($"{command.VTableIndex}");
                    }
                    command = command.nextCommand;
                } while (command != null);
            }

            TextPrev = text.ToArray();
        }
        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor(this);
        }
    }
}
