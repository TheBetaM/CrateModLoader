using System;
using System.Collections.Generic;
using Twinsanity;
using System.Drawing;

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

        public static void RM_CharacterMod_ClassicSlideJump(TwinsFile RM_Archive)
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
                            if (instance.UnkI321.Count > 8)
                            {
                                instance.UnkI321[(int)CharacterInstanceFlags.SlideJumpRotationSpeed] = 0x10000;
                            }
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
                                script_section.Records.Add(Twins_Data.allScripts[(uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT]);
                            }
                            if (!script_section.ContainsItem((uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT))
                            {
                                script_section.Records.Add(Twins_Data.allScripts[(uint)ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT]);
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

        public static void RM_Mod_SyncScripts(TwinsFile RM_Archive, ChunkType chunk)
        {
            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);

                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        List<uint> scripts = new List<uint>();
                        for (int i = 0; i < script_section.Records.Count; i++)
                        {
                            scripts.Add(script_section.Records[i].ID);
                            script_section.RemoveItem(script_section.Records[i].ID);
                        }
                        for (int i = 0; i < scripts.Count; i++)
                        {
                            script_section.Records.Add(Twins_Data.allScripts[scripts[i]]);
                        }
                    }
                }
            }
        }

        public static void Script_Mod_SkipCutscenes()
        {
            foreach (KeyValuePair<uint, Script> scr in Twins_Data.allScripts)
            {
                if (scr.Value.Main != null && scr.Value.Main.scriptState1 != null)
                {
                    switch (scr.Key)
                    {
                        case (uint)ScriptID.COM_CAVERN_CUTSCENE_A:
                            scr.Value.Main.unkInt2 = 12;
                            break;
                        case (uint)ScriptID.COM_CAVERN_CUTSCENE_C:
                            scr.Value.Main.unkInt2 = 31;
                            break;
                        case (uint)ScriptID.COM_CAVERN_CUTSCENE_L02D_A:
                            scr.Value.Main.unkInt2 = 7;
                            break;
                        case (uint)ScriptID.COM_SENTRY_TRIBESMAN_ZOOM_TO:
                            //scr.Value.Main.unkInt2 = 5;
                            break;
                        case (uint)ScriptID.COM_EARTH_HUB_CUTSCENE_DIRECTOR_ACTIVATED:
                            scr.Value.Main.unkInt2 = 14;
                            break;
                        case (uint)ScriptID.COM_BLIMP_DEPART_CUTSCENE_DIRECTOR_ACTIVATED:
                            scr.Value.Main.unkInt2 = 3;
                            break;
                        case (uint)ScriptID.COM_CLASSROOM_CHAOS_CUTSCENE_L08C:
                            scr.Value.Main.unkInt2 = 3;
                            break;
                        case (uint)ScriptID.COM_DORM_ROOM_CUTSCENE_H03B:
                            scr.Value.Main.unkInt2 = 1;
                            break;
                        case (uint)ScriptID.COM_SCHOOL_HUB_TO_ICE_HUB_CS:
                            scr.Value.Main.unkInt2 = 10;
                            break;
                        case (uint)ScriptID.COM_ALTEARTH_ROCKSLIDE_CUTSCENE_H04B:
                            scr.Value.Main.unkInt2 = 3;
                            break;
                        case (uint)ScriptID.COM_HENCHMANIA_BOSSFIGHT_CUTSCENE_INTRO:
                            scr.Value.Main.unkInt2 = 13;
                            break;
                    }
                    Script.MainScript.ScriptState state = scr.Value.Main.scriptState1;
                    while (state != null)
                    {
                        switch (state.scriptIndexOrSlot)
                        {
                            default:
                                break;
                            case (short)ScriptID.COM_TOTEM_CUTSCENE_A_TAKE_2:
                            case (short)ScriptID.COM_TOTEM_CUTSCENE_B:
                            case (short)ScriptID.COM_TOTEM_CUTSCENE_C:
                            case (short)ScriptID.COM_TOTEM_CUTSCENE_D:
                            case (short)ScriptID.COM_TOTEM_CUTSCENE_E:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_TOTEM_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_BEACH_TRAINING_CS:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_CUTSCENE_L01B:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_A:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_B:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_C:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_D:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_E:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_F:
                            case (short)ScriptID.COM_TRAINING_CUTSCENE_G:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_TRAINING_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_ANGRY_SKUNK_CUSCENE_MAIN:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_PARTY_ARENA_CUTSCENE_H01D:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_DOCAMOK_BEAR_CUTSCENE:
                            case (short)ScriptID.COM_DOCAMOK_BEEHIVE_CORTEX_CS:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_TOTEM_TIED_CUTSCENE_DIRECTOR_ACTIVATED:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_TIKI_MON_CUTSCENE_ENTER:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_TIKI_MON_CUTSCENE_SKIP_ENTER;
                                break;
                            case (short)ScriptID.COM_ICELAB_CUTSCENE_L04A:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_ICELAB_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_ICE_PENGUIN_CUTSCENE_DIRECTOR_LO4B:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_ICE_PENGUIN_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_UKAUKA_DEFEATED_CUTSCENE_L04D:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_UKAUKA_DEFEATED_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_ICELABINT_CUTSCENE_H02B:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_ICELABINT_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_PSYCHOTRON_CUTSCENE_A:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_PSYCHOTRON_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_SLIPSLIDE_CUTSCENE_H02G:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_SLIPSLIDE_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_MOULIN_CUTSCENE_L05B:
                            case (short)ScriptID.COM_CHICKEN_CUTSCENE_L05A:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_DINGODILE_HUT_CS:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_SLIPSLIDE_DINGODILE_HUT_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_WALRUS_CUTSCENE_L06E:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_WALRUS_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_HENCHMANIA_CUTSCENE_B02A:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_HENCHMANIA_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_HENCHMANIA_BOSSFIGHT_CUTSCENE_INTRO:
                                //state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_BR_CORTEX_PIPE_CS:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_BOSS_DINGODILE_INTRO_CUTSCENE:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_DINGODILE_INTRO_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_DINGODILE_BOSSFIGHT_DIRECTOR_END_CUTSCENE:
                                state.scriptIndexOrSlot = (short)ScriptID.COM_DINGODILE_END_CUTSCENE_SKIP;
                                break;
                            case (short)ScriptID.COM_CUTSCENE_H03C:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_BELL_TOWER_CUTSCENE_B03A:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_ALTEARTH_EVILCRASH_CUDDLE_CUTSCENE_DIRECTOR_H04A:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_EVILCRASH_HOUSE_CS_H04C:
                                state.scriptIndexOrSlot = -1;
                                break;
                            case (short)ScriptID.COM_COREINTRO_CS_DIR_H04E:
                                state.scriptIndexOrSlot = -1;
                                break;

                        }

                        state = state.nextState;
                    }
                }
            }
        }

        public static void SM_Rand_WorldPalette(TwinsFile SM_Archive, Modder_Twins.ColorSwizzleData Swiz)
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
                                float maxVal = Math.Max(model.SubModels[i].Groups[g].VData[v].R, model.SubModels[i].Groups[g].VData[v].G);
                                maxVal = Math.Max(maxVal, model.SubModels[i].Groups[g].VData[v].B);
                                maxVal = maxVal / 255f;

                                int r = model.SubModels[i].Groups[g].VData[v].R;
                                int gr = model.SubModels[i].Groups[g].VData[v].G;
                                int b = model.SubModels[i].Groups[g].VData[v].B;

                                model.SubModels[i].Groups[g].VData[v].R = (byte)((Swiz.r_r * r + Swiz.r_g * gr + Swiz.r_b * b) / Swiz.r_s);
                                model.SubModels[i].Groups[g].VData[v].G = (byte)((Swiz.g_r * r + Swiz.g_g * gr + Swiz.g_b * b) / Swiz.g_s);
                                model.SubModels[i].Groups[g].VData[v].B = (byte)((Swiz.b_r * r + Swiz.b_g * gr + Swiz.b_b * b) / Swiz.b_s);
                            }
                        }
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
                TwinsSection tex_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);

                foreach (TwinsItem item in tex_section.Records)
                {
                    Texture tex = (Texture)item;
                    for (int i = 0; i < tex.RawData.Length; i++)
                    {
                        int maxVal = Math.Max(tex.RawData[i].R, tex.RawData[i].G);
                        maxVal = Math.Max(maxVal, tex.RawData[i].B);
                        tex.RawData[i] = Color.FromArgb(tex.RawData[i].A, maxVal, maxVal, maxVal);
                    }
                    tex.UpdateImageData();
                }

            }

            SceneryData scenery = (SceneryData)SM_Archive.GetItem<TwinsItem>(0);
            if (scenery.LightsAmbient.Count > 0)
            {
                foreach (SceneryData.LightAmbient light in scenery.LightsAmbient)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsDirectional.Count > 0)
            {
                foreach (SceneryData.LightDirectional light in scenery.LightsDirectional)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsPoint.Count > 0)
            {
                foreach (SceneryData.LightPoint light in scenery.LightsPoint)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsNegative.Count > 0)
            {
                foreach (SceneryData.LightNegative light in scenery.LightsNegative)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
        }

        public static void SM_Mod_GreyscaleDimension(TwinsFile SM_Archive, ChunkType chunk)
        {
            List<ChunkType> AllowedChunks = new List<ChunkType>()
            {
                ChunkType.AltEarth_Core_AftTreas,
                ChunkType.AltEarth_Core_CoreA,
                ChunkType.AltEarth_Core_CoreB,
                ChunkType.AltEarth_Core_CoreC,
                ChunkType.AltEarth_Core_CoreD,
                ChunkType.AltEarth_Core_PreTreas,
                ChunkType.AltEarth_Core_Throne,
                ChunkType.AltEarth_Core_Treasure,
                ChunkType.AltEarth_Hub_AltA,
                ChunkType.AltEarth_Hub_AltDoc,
                ChunkType.AltEarth_Hub_AltDoc_B,
                ChunkType.AltEarth_Hub_AltDoc_C,
                ChunkType.AltEarth_Hub_AltTunl,
                ChunkType.AltEarth_Hub_AlwaysOn,
                ChunkType.AltEarth_Hub_CoreEnt,
                ChunkType.AltEarth_Hub_SlipJoin,
                ChunkType.AltEarth_Lab_AltLabIn,
                ChunkType.AltEarth_Lab_LabExt,
                ChunkType.AltEarth_Lab_Psycho,
                ChunkType.AltEarth_Lab_PTCorr,
                ChunkType.AltEarth_Lab_PTExit,
                ChunkType.AltEarth_RockSlid_L10ChasA,
                ChunkType.AltEarth_RockSlid_L10ChasB,
                ChunkType.AltEarth_RockSlid_L10End,
                ChunkType.AltEarth_RockSlid_L10Roids,
                ChunkType.AltEarth_RockSlid_L10Start,
            };

            if (AllowedChunks.Contains(chunk))
            {
                SM_Mod_GreyscaleWorld(SM_Archive);
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
