using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_TrackEntrances : ModStruct<GOD_File>
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

        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains(@"genericobjectives.god"))
            {
                for (int i = 0; i < 15; i++)
                {
                    LUA_Object lobj = file.GetObject("Objective", "StartRace" + CTTR_Data.TrackNamesSimple[i]);
                    if (lobj != null)
                    {
                        lobj.Content[lobj.Content.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                        lobj.Content[lobj.Content.Count - 1] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\",\"ReturnFromRace" + CTTR_Data.TrackNamesSimple[i] + "\")";
                    }
                }

                LUA_Object obj = file.GetObject("Objective", "StartRaceFromMidway2");
                if (obj != null)
                {
                    obj.Content[obj.Content.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces\")";
                }
                obj = file.GetObject("Objective", "StartRaceFromMidway3");
                if (obj != null)
                {
                    obj.Content[obj.Content.Count - 2] = "this.AddAction_StartRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\",\"ReturnFromMidwayRaces2\")";
                }
                obj = file.GetObject("Objective", "BuyRaceTicketWithTrack");
                if (obj != null)
                {
                    obj.Content[obj.Content.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[0]] + "\")";
                }
            }
            for (int obj = 0; obj < CTTR_Data.MissionObjectiveTypes.Length; obj++)
            {
                if (file.Name.Contains(CTTR_Data.MissionObjectiveTypes[obj] + ".god"))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        LUA_Object lobj = file.GetObject("Objective", "UnlockRace" + CTTR_Data.TrackNamesSimple[i]);
                        if (lobj != null)
                        {
                            lobj.Content[lobj.Content.Count - 2] = "this.AddAction_UnlockRace(\"" + CTTR_Data.TrackNames[randTracks[i]] + "\")";
                            lobj.Content[lobj.Content.Count - 1] = "this.AddAction_DisplayMessage(\"" + CTTR_Data.TrackGateNames[randTracks[i]] + "\",1.0,6.0)";
                        }
                    }
                }
            }
        }

    }
}
