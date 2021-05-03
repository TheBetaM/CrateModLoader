using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //todo xbox scripts, needs testing
    public class TS_SwitchCharacters : ModStruct<ChunkInfoRM>
    {
        public override bool NeedsCachePass => true;

        private Script StrafeLeft = null;
        private Script StrafeRight = null;
        private Script HeadStrafeLeft = null;
        private Script HeadStrafeRight = null;

        public override void CachePass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (StrafeLeft == null)
            {

                int scriptVer = 0;
                //if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
                //{
                //    scriptVer = 1;
                //}

                if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
                {
                    TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                    if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                    {
                        TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                        if (script_section.Records.Count > 0)
                        {
                            if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT))
                            {
                                Script scr = script_section.GetItem<Script>((int)ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT);
                                StrafeLeft = scr;
                                Script scr2 = script_section.GetItem<Script>((int)ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT);
                                StrafeRight = scr2;
                                Script scr3 = script_section.GetItem<Script>((int)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT);
                                HeadStrafeLeft = scr;
                                Script scr4 = script_section.GetItem<Script>((int)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT);
                                HeadStrafeRight = scr2;

                                Script.MainScript.ScriptCommand SwitchToCrashCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                                    arguments = new List<uint>() { 0xCDCDDFEF, 0 },
                                };
                                Script.MainScript.ScriptCommand SwitchToCortexCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                                    arguments = new List<uint>() { 0xCDCDDFEF, 1 },
                                };
                                Script.MainScript.ScriptCommand SwitchToNinaCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                                    arguments = new List<uint>() { 0xCDCDDFEF, 3 },
                                };
                                Script.MainScript.ScriptCommand SwitchToMechaCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                                    arguments = new List<uint>() { 0xCDCDDF6F, 6 },
                                };
                                Script.MainScript.ScriptCommand ExitVehicleCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.ExitVehicleMode,
                                    arguments = new List<uint>() { 0 },
                                };
                                Script.MainScript.ScriptCommand DetachCortexCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)562,
                                    arguments = new List<uint>() { },
                                };
                                Script.MainScript.ScriptCommand PlayerModeSoloCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.SetPlayerMode,
                                    arguments = new List<uint>() { 1, 0, 6 },
                                };

                                /*
                                Script.MainScript.ScriptCommand TestCommand = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.PlayCredits,
                                    arguments = new List<uint>() { },
                                };
                                Script.MainScript.ScriptCommand TestCommand2 = new Script.MainScript.ScriptCommand(scriptVer)
                                {
                                    VTableIndex = (ushort)DefaultEnums.CommandID.PlayCredits,
                                    arguments = new List<uint>() { },
                                };
                                StrafeLeft.Main.scriptState1.scriptStateBody.command = TestCommand;
                                StrafeRight.Main.scriptState1.scriptStateBody.command = TestCommand2;
                                */

                                StrafeLeft.Main.scriptState1.scriptStateBody.command = SwitchToCrashCommand;
                                StrafeRight.Main.scriptState1.scriptStateBody.AddCommand(1);
                                StrafeRight.Main.scriptState1.scriptStateBody.AddCommand(2);
                                StrafeRight.Main.scriptState1.scriptStateBody.command = ExitVehicleCommand;
                                StrafeRight.Main.scriptState1.scriptStateBody.command.internalIndex = (StrafeRight.Main.scriptState1.scriptStateBody.command.internalIndex & 0xffff) | (int)((0x0100 << 16) & 0xffff0000);
                                StrafeRight.Main.scriptState1.scriptStateBody.command.nextCommand = DetachCortexCommand;
                                StrafeRight.Main.scriptState1.scriptStateBody.command.nextCommand.internalIndex = (StrafeRight.Main.scriptState1.scriptStateBody.command.nextCommand.internalIndex & 0xffff) | (int)((0x0100 << 16) & 0xffff0000);
                                StrafeRight.Main.scriptState1.scriptStateBody.command.nextCommand.nextCommand = SwitchToCortexCommand;
                            }
                        }
                    }
                }
            }
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);

                bool containsCharacter = false;
                if (code_section.ContainsItem((uint)RM_Code_Sections.Object))
                {
                    TwinsSection obj_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
                    if (obj_section.Records.Count > 0)
                    {
                        for (int obj = 0; obj < obj_section.Records.Count; obj++)
                        {
                            if (obj_section.Records[obj].ID == (uint)ObjectID.CRASH)
                            {
                                GameObject gameObj = (GameObject)obj_section.Records[obj];
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                obj_section.Records[obj] = gameObj;
                                containsCharacter = true;
                            }
                            else if (obj_section.Records[obj].ID == (uint)ObjectID.CORTEX)
                            {
                                GameObject gameObj = (GameObject)obj_section.Records[obj];
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                obj_section.Records[obj] = gameObj;
                                containsCharacter = true;
                            }
                            else if (obj_section.Records[obj].ID == (uint)ObjectID.NINA)
                            {
                                GameObject gameObj = (GameObject)obj_section.Records[obj];
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                obj_section.Records[obj] = gameObj;
                                containsCharacter = true;
                            }
                        }
                    }
                }

                if (containsCharacter)
                {
                    if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                    {
                        TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                        if (script_section.Records.Count > 0)
                        {
                            if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT))
                            {
                                script_section.RemoveItem((uint)ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT);
                            }
                            if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT))
                            {
                                script_section.RemoveItem((uint)ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT);
                            }
                            if (!script_section.ContainsItem((uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT))
                            {
                                script_section.Records.Add(HeadStrafeLeft);
                            }
                            if (!script_section.ContainsItem((uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT))
                            {
                                script_section.Records.Add(HeadStrafeRight);
                            }
                            script_section.Records.Add(StrafeLeft);
                            script_section.Records.Add(StrafeRight);
                        }
                    }
                }
            }

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)ObjectID.CRASH)
                        {
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 0.1f;
                        }
                        else if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 0.1f;
                        }
                        else if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 0.1f;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
