using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_TrackEntrances : ModStruct<string>
    {
        public override string Name => CTTR_Text.Rand_TrackEntrances;
        public override string Description => CTTR_Text.Rand_TrackEntrancesDesc;

        private List<int> randTracks;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randTracks = new List<int>();
            if (CTTR_Props_Main.Option_RandTrackEntrances.Enabled)
            {
                List<int> possibleTracks = new List<int>();

                for (int i = 0; i < 15; i++)
                {
                    possibleTracks.Add(i);
                }

                for (int i = 0; i < 15; i++)
                {
                    int targetTrack = possibleTracks[randState.Next(0, possibleTracks.Count)];
                    randTracks.Add(targetTrack);
                    possibleTracks.Remove(targetTrack);
                }
            }
        }

        public override void ModPass(string path_extr)
        {
            if (File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
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

                File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (File.Exists(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    string[] objective_lines = File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god");
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

                    File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_" + CTTR_Data.MissionObjectiveTypes[obj] + ".god", objective_lines);

                }
            }
        }

    }
}
