using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //unfinished
    public class TS_SkipCutscenes : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Mod_SkipCutscenes;
        public override string Description => Twins_Text.Mod_SkipCutscenesDesc;
        public override bool Hidden => true;
        public override bool NeedsCachePass => true;

        private Dictionary<uint, Script> allScripts;

        public override void CachePass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        for (int i = 0; i < script_section.Records.Count; ++i)
                        {
                            Script scr = (Script)script_section.Records[i];
                            if (allScripts.Count > 0)
                            {
                                if (!allScripts.ContainsKey(scr.ID))
                                {
                                    allScripts.Add(scr.ID, scr);
                                }
                            }
                            else
                            {
                                allScripts.Add(scr.ID, scr);
                            }
                        }
                    }
                }
            }
        }

        public override void BeforeModPass()
        {
            foreach (KeyValuePair<uint, Script> scr in allScripts)
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

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

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
                            script_section.Records.Add(allScripts[scripts[i]]);
                        }
                    }
                }
            }
        }
    }
}
