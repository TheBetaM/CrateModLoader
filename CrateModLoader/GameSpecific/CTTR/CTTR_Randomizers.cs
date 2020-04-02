using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CTTR
{
    public static class CTTR_Randomizers
    {
        public static void Randomize_Characters(string path_extr, List<int> randChars)
        {
            /* TODO later, because it requires mission logic to unlock Crash/Cortex
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int characterList_Start = 0;
                int characterList_End = 0;
                List<string> DefaultUnlocks = new List<string>();
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "UnlockDefaults", ref characterList_Start, ref characterList_End, DefaultUnlocks))
                {
                    DefaultUnlocks.Clear();
                    DefaultUnlocks.Add("this.SetName(\"UnlockDefaults\")");
                    for (int i = 0; i < randChars.Count; i++)
                    {
                        DefaultUnlocks.Add("this.AddAction_UnlockCar(\"" + CTTR_Data.DriverNames[randChars[i]] + "\",1)");
                    }
                }
                CTTR_Data.LUA_SaveObject(LineList, "Objective", "UnlockDefaults", DefaultUnlocks);

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", startup_lines);

            }
            */
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash && System.IO.File.Exists(path_extr + @"design\permanent\skins.god"))
            {
                string[] skins_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\skins.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < skins_lines.Length; i++)
                {
                    LineList.Add(skins_lines[i]);
                }

                int skin_Start = 0;
                int skin_End = 0;
                List<string> SkinObj = new List<string>();
                if (CTTR_Data.LUA_LoadObject(LineList, "Skin", "CrashDefault", ref skin_Start, ref skin_End, SkinObj))
                {
                    for (int i = 0; i < SkinObj.Count; i++)
                    {
                        if (SkinObj[i] == "this.SetOnfootSkinFilename(\"crash_onfoot_model\")")
                        {
                            SkinObj[i] = "this.SetOnfootSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                        else if (SkinObj[i] == "this.SetSpinSkinFilename(\"crash_spin_model\")")
                        {
                            SkinObj[i] = "this.SetSpinSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                    }
                }
                CTTR_Data.LUA_SaveObject(LineList, "Skin", "CrashDefault", SkinObj);

                skins_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    skins_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\skins.god", skins_lines);

            }

            // Swapping idle animation for platforming character
            if (randChars[0] != (int)CTTR_Data.DriverID.Crash)
            {
                Pure3D.File targetCharAnim = new Pure3D.File();
                if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d"))
                {
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_animations.p3d");
                }
                else if (System.IO.File.Exists(path_extr + @"art\animation\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d"))
                {
                    targetCharAnim.Load(path_extr + @"art\animation\\" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_midway_animations.p3d");
                }
                else
                {
                    return;
                }

                Pure3D.Chunk targetIdleAnim;
                if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                {
                    targetIdleAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_idle");
                }
                else if (targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored") != null) // Nina doesn't have an idle animation
                {
                    Animation targetIdleAnimAnim;
                    targetIdleAnimAnim = targetCharAnim.RootChunk.GetChildByName<Animation>("onfoot_talk_bored");
                    targetIdleAnimAnim.Name = "onfoot_idle";
                    targetIdleAnim = (Pure3D.Chunk)targetIdleAnimAnim;
                }
                else
                {
                    return;
                }

                if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_animations.p3d"))
                {
                    Pure3D.File CrashOnfootAnim = new Pure3D.File();
                    CrashOnfootAnim.Load(path_extr + @"art\animation\crash_onfoot_animations.p3d");

                    if (CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        CrashOnfootAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }

                    CrashOnfootAnim.Save(path_extr + @"art\animation\crash_onfoot_animations1.p3d");
                    System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_animations.p3d");
                    System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_animations1.p3d", path_extr + @"art\animation\crash_onfoot_animations.p3d");

                }
                if (System.IO.File.Exists(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d"))
                {
                    Pure3D.File CrashOnfootMidwayAnim = new Pure3D.File();
                    CrashOnfootMidwayAnim.Load(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                    if (CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle") != null)
                    {
                        int animIndex = CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.GetChildIndexByName<Animation>("onfoot_idle");
                        CrashOnfootMidwayAnim.RootChunk.GetChildByName<Animation>("onfoot_idle").Parent.Children[animIndex] = targetIdleAnim;
                    }

                    CrashOnfootMidwayAnim.Save(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d");
                    System.IO.File.Delete(path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");
                    System.IO.File.Move(path_extr + @"art\animation\crash_onfoot_midway_animations1.p3d", path_extr + @"art\animation\crash_onfoot_midway_animations.p3d");

                }
            }

        }

        public static void Randomize_Hubs(string path_extr, List<int> randHubs, List<int> randGems)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    string targetHub = CTTR_Data.HubNamesSimple[i + 1];
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"" + CTTR_Data.HubNames[randHubs[i]] + "\",\"StartLocationFromMidway\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayTo" + targetHub, ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_ChangeLevel(\"onfoot_midway\",\"StartLocationFrom" + targetHub + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            /* TODO: Gem Key randomization?
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god");
                    List<string> LineList = new List<string>();
                    for (int i = 0; i < objective_lines.Length; i++)
                    {
                        LineList.Add(objective_lines[i]);
                    }

                    int List_Start = 0;
                    int List_End = 0;
                    List<string> ChangeHubObjective = new List<string>();
                    for (int i = 0; i < CTTR_Data.MissionObjectiveTypes.Length; i++)
                    {
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ref List_Start, ref List_End, ChangeHubObjective))
                        {
                            for (int a = 0; a < ChangeHubObjective.Count; a++)
                            {
                                if (ChangeHubObjective[a] == "this.AddAction_SetNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate\")")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Gate\")";
                                }
                                else if (ChangeHubObjective[a] == "this.AddRequirement_ObjectiveComplete(\"Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate\")")
                                {
                                    ChangeHubObjective[a] = "this.AddRequirement_ObjectiveComplete(\"Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Gate\")";
                                }
                            }
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", CTTR_Data.MissionObjectiveHubNamesSimple[i] + "KeyCollection", ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ref List_Start, ref List_End, ChangeHubObjective))
                        {
                            for (int a = 0; a < ChangeHubObjective.Count; a++)
                            {
                                if (ChangeHubObjective[a] == "this.AddRequirement_CheckNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddRequirement_CheckNamedFlag(\"KeyAcquired_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetNamedFlag(\"GateUnlocked_" + CTTR_Data.MissionObjectiveTypes[i] + "\",true)")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetNamedFlag(\"GateUnlocked_" + CTTR_Data.MissionObjectiveTypes[randGems[i]] + "\",true)";
                                }
                                else if (ChangeHubObjective[a] == "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Weenie\")")
                                {
                                    ChangeHubObjective[a] = "this.AddAction_SetRadarNavPoint(\"Nav" + CTTR_Data.MissionObjectiveHubNamesSimple[randGems[i]] + "Weenie\")";
                                }
                            }
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", "Unlock" + CTTR_Data.MissionObjectiveHubNamesSimple[i] + "Gate", ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                    }

                    objective_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        objective_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god", objective_lines);

                }
            }
            */
        }

        public static void Randomize_Tracks(string path_extr, List<int> randTracks)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 15; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                        ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\",\"ReturnFromRace" + CTTR_Data.TrackNamesSimple[i] + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i], ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRaceFromMidway2", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRaceFromMidway2", ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "StartRaceFromMidway3", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces2\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "StartRaceFromMidway3", ChangeHubObjective);
                }
                ChangeHubObjective.Clear();
                if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "BuyRaceTicketWithTrack", ref List_Start, ref List_End, ChangeHubObjective))
                {
                    ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\")";
                    CTTR_Data.LUA_SaveObject(LineList, "Objective", "BuyRaceTicketWithTrack", ChangeHubObjective);
                }
                ChangeHubObjective.Clear();

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god");
                    List<string> LineList = new List<string>();
                    for (int i = 0; i < objective_lines.Length; i++)
                    {
                        LineList.Add(objective_lines[i]);
                    }

                    int List_Start = 0;
                    int List_End = 0;
                    List<string> ChangeHubObjective = new List<string>();
                    for (int i = 0; i < 15; i++)
                    {
                        if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ref List_Start, ref List_End, ChangeHubObjective))
                        {
                            ChangeHubObjective[ChangeHubObjective.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                            ChangeHubObjective[ChangeHubObjective.Count - 1] = "this.AddAction_DisplayMessage(\"" + CTTR_Data.TrackGateNames[randTracks[i]] + "\",1.0,6.0)";
                            CTTR_Data.LUA_SaveObject(LineList, "Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i], ChangeHubObjective);
                        }
                        ChangeHubObjective.Clear();
                    }

                    objective_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        objective_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god", objective_lines);

                }
            }
        }

        public static void Randomize_Minigames(string path_extr, List<int> randMinigames)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective[ChangeHubObjective.Count - 3] = "this.AddAction_UnlockMiniGame(\"OFMiniGames/" + CTTR_Data.MinigameTypeNames[randMinigames[i]] + "\")";
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", CTTR_Data.MinigameObjectiveTypes[i], ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
        }

        public static void Randomize_Race_Laps(string path_extr, List<int> randLaps)
        {
            if (System.IO.File.Exists(path_extr + @"design\startup.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\startup.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int LevelListStart = 0;
                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i] == "function GetLevelList()")
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }
                LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + randLaps[0] + ",true},";
                LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + randLaps[1] + ",true},";
                LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + randLaps[2] + ",true},";
                LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + randLaps[3] + ",true},";
                LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + randLaps[4] + ",true},";
                LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + randLaps[5] + ",true},";
                LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + randLaps[6] + ",true},";
                LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + randLaps[7] + ",true},";
                LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + randLaps[8] + ",true},";
                LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + randLaps[9] + ",true},";
                LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + randLaps[10] + ",true},";
                LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + randLaps[11] + ",true},";
                LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + randLaps[12] + ",true},";
                LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + randLaps[13] + ",true},";
                LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + randLaps[14] + ",true},";

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

            }
        }

        public static void Randomize_Battle_KOs(string path_extr, List<int> randKOs)
        {
            if (System.IO.File.Exists(path_extr + @"design\startup.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\startup.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int LevelListStart = 0;
                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i] == "function GetLevelList()")
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }
                LineList[LevelListStart + 3] = "{\"adventure_arena\",ThemeAdventure,TypeBattle," + randKOs[0] + ",true},";
                LineList[LevelListStart + 8] = "{\"bonus1_arena\",ThemeFairy,TypeBattle," + randKOs[1] + ",true},";
                LineList[LevelListStart + 12] = "{\"dino_arena\",ThemeDino,TypeBattle," + randKOs[2] + ",true},";
                LineList[LevelListStart + 16] = "{\"egypt_arena\",ThemeEgypt,TypeBattle," + randKOs[3] + ",true},";

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

            }
        }


    }
}
