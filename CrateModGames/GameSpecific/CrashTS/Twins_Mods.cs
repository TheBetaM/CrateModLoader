using System;
using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public static class Twins_Mods
    {

        public static void RM_EnableUnusedEnemies(TwinsFile RM_Archive)
        {
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
                        if (instance.ObjectID == (uint)ObjectID.GLOBAL_BAT_DARKPURPLE)
                        {
                            if (instance.Flags > (uint)PropertyFlags.DisableObject)
                            {
                                instance.Flags -= (uint)PropertyFlags.DisableObject;
                            }
                        }
                        else if (instance.ObjectID == (uint)ObjectID.SCHOOL_FROGENSTEIN)
                        {
                            instance.Flags = 0x188B2E;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_CharacterObjectMod(TwinsFile RM_Archive)
        {
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
        }

        public static void RM_CharacterMod_EnableFlyingKick(TwinsFile RM_Archive)
        {
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

        public static void RM_CharacterMod_DoubleJumpCortex(TwinsFile RM_Archive)
        {
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
                        if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            // Cortex mods
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = 16;
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_CharacterMod_DoubleJumpNina(TwinsFile RM_Archive)
        {
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
                        if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            // Nina mods
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = 16;
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        //example
        public static void RM_CharacterMod(TwinsFile RM_Archive)
        {
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

                            //instance.UnkI322[(int)CharacterInstanceFloats.Static1] = 0; // 1

                        }
                        else if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            // Cortex mods

                            //instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = 0.4f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            // Nina mods

                            //instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = 1.75f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods

                            //instance.UnkI322[(int).CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_SwitchCharactersMod(TwinsFile RM_Archive, Script StrafeLeft, Script StrafeRight)
        {
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
                                script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT));
                            }
                            if (!script_section.ContainsItem((uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT))
                            {
                                script_section.Records.Add(Twins_Data.GetScriptByID(ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT));
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

        public static void RM_Mod_ClassicHealth(TwinsFile RM_Archive)
        {
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
                        if (instance.ObjectID == (uint)ObjectID.CRASH || instance.ObjectID == (uint)ObjectID.CORTEX || 
                            instance.ObjectID == (uint)ObjectID.NINA || instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            instance.UnkI323[2] = 1;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_Mod_ClassicExplosions(TwinsFile RM_Archive, Script GenericCrateExplode)
        {
            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);

                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CRATE_EXPLODE))
                        {
                            script_section.RemoveItem((uint)ScriptID.COM_GENERIC_CRATE_EXPLODE);
                            script_section.Records.Add(GenericCrateExplode);
                        }
                    }
                }
            }

        }

        public static void RM_Mod_UnlockedCamera(TwinsFile RM_Archive, ChunkType chunk)
        {
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.Camera)) continue;
                    TwinsSection cameras = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.Camera);
                    for (int i = 0; i < cameras.Records.Count; ++i)
                    {
                        Camera cam = (Camera)cameras.Records[i];
                        cam.Enabled = 0;
                        cameras.Records[i] = cam;
                    }
                }
            }
        }

        public static void SM_Mod_GreyscaleWorld(TwinsFile SM_Archive)
        {
            if (SM_Archive.Type != TwinsFile.FileType.SM2) return;
            if (!SM_Archive.ContainsItem(6)) return;
            TwinsSection section = SM_Archive.GetItem<TwinsSection>(6);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Models) && section.Records.Count > 0)
            {
                TwinsSection model_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Models);

                foreach (TwinsItem item in model_section.Records)
                {
                    Model model = (Model)item;
                    for (int i = 0; i < model.SubModels.Count; i++)
                    {
                        for (int g = 0; g < model.SubModels[i].Groups.Count; g++)
                        {
                            for (int v = 0; v < model.SubModels[i].Groups[g].VData.Length; v++)
                            {
                                int maxVal = Math.Max(model.SubModels[i].Groups[g].VData[v].R, model.SubModels[i].Groups[g].VData[v].G);
                                maxVal = Math.Max(maxVal, model.SubModels[i].Groups[g].VData[v].B);
                                model.SubModels[i].Groups[g].VData[v].R = (byte)maxVal;
                                model.SubModels[i].Groups[g].VData[v].G = (byte)maxVal;
                                model.SubModels[i].Groups[g].VData[v].B = (byte)maxVal;
                            }
                        }
                    }
                }

            }
            if (section.ContainsItem((uint)RM_Graphics_Sections.Textures) && section.Records.Count > 0)
            {
                //todo
                TwinsSection tex_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);

                foreach (TwinsItem item in tex_section.Records)
                {
                    Texture tex = (Texture)item;
                    for (int i = 0; i < tex.RawData.Length; i++)
                    {
                        //tex.RawData[i] = new System.Drawing.Color();
                    }
                }

            }
        }

        public static void SM_Mod_UntexturedWorld(TwinsFile SM_Archive)
        {
            if (!SM_Archive.ContainsItem(6)) return;
            TwinsSection section = SM_Archive.GetItem<TwinsSection>(6);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Materials) && section.Records.Count > 0)
            {
                TwinsSection mat_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Materials);

                foreach (TwinsItem item in mat_section.Records)
                {
                    Material mat = (Material)item;
                    for (int i = 0; i < mat.Shaders.Count; i++)
                    {
                        mat.Shaders[i].TextureId = 0;
                        mat.Shaders[i].TxtMapping = TwinsShader.TextureMapping.OFF;
                    }
                }

            }
        }

    }
}
