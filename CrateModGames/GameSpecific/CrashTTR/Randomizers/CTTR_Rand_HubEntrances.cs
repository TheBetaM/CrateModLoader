using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    // unfinsihed, needs more logic
    public class CTTR_Rand_HubEntrances : ModStruct<GOD_File>
    {
        private List<int> randHubs;
        private List<int> randGems;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randHubs = new List<int>();
            randGems = new List<int>();
            /*
            if (GetOption(RandomizeHubs))
            {
                List<int> possibleHubs = new List<int>();

                for (int i = 1; i < 6; i++)
                {
                    possibleHubs.Add(i);
                }

                for (int i = 0; i < 5; i++)
                {
                    int targetHub = possibleHubs[randState.Next(0, possibleHubs.Count)];
                    randHubs.Add(targetHub);
                    possibleHubs.Remove(targetHub);
                }
                List<int> possibleGems = new List<int>();

                possibleGems.Add(0);
                possibleGems.Add(1);
                possibleGems.Add(2);
                possibleGems.Add(4);
                possibleGems.Add(5);
                for (int i = 0; i < 5; i++)
                {
                    int targetGem = possibleGems[randState.Next(0, possibleGems.Count)];
                    randGems.Add(targetGem);
                    possibleGems.Remove(targetGem);
                    if (i == 2)
                    {
                        randGems.Add(3);
                    }
                }
            }
            */
        }

        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains(@"genericobjectives.god"))
            {
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    string targetHub = CTTR_Data.HubNamesSimple[i + 1];

                    LUA_Object obj = file.GetObject("Objective", "ChangeLevelMidwayTo" + targetHub);
                    if (obj != null)
                    {
                        obj.Content[obj.Content.Count - 3] = "this.AddAction_ChangeLevel(\"" + CTTR_Data.HubNames[randHubs[i]] + "\",\"StartLocationFromMidway\")";
                    }
                    obj = file.GetObject("Objective", "ChangeLevel" + CTTR_Data.HubNamesSimple[randHubs[i]] + "ToMidway");
                    if (obj != null)
                    {
                        obj.Content[obj.Content.Count - 3] = "this.AddAction_ChangeLevel(\"onfoot_midway\",\"StartLocationFrom" + targetHub + "\")";
                    }
                }
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

    }
}
