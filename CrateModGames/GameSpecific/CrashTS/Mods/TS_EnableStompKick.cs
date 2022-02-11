using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_EnableStompKick : ModStruct<ChunkInfoRM>
    {
        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                /*
                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT));
                    }
                }
                */
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
                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                //if (Options[ModStompKick].Enabled)
                                //{
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnFlyingKick] = (ushort)ScriptID.HEAD_COM_CRASH_STOMP_KICK;
                                gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnFlyingKickLand] = (ushort)ScriptID.HEAD_COM_CRASH_STOMP_KICK_LAND;
                                //}

                                obj_section.Records[obj] = gameObj;
                            }
                            else if (obj_section.Records[obj].ID == (uint)ObjectID.CORTEX)
                            {
                                //GameObject gameObj = (GameObject)obj_section.Records[obj];

                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.Unk35] = (ushort)ScriptID.HEAD_COM_CORTEX_RECOIL;

                                //obj_section.Records[obj] = gameObj;
                            }
                            else if (obj_section.Records[obj].ID == (uint)ObjectID.NINA)
                            {
                                //GameObject gameObj = (GameObject)obj_section.Records[obj];
                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                //gameObj.Scripts[(int)CharacterGameObjectScriptOrder.OnThrowPunch] = (ushort)ScriptID.HEAD_COM_NINA_ENTER_VEHICLE;

                                //obj_section.Records[obj] = gameObj;
                            }
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
                            // Crash mods
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = 0.15f;
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = 50;
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
