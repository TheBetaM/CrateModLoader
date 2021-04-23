using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_MinigameEntrances : ModStruct<string>
    {
        public override string Name => CTTR_Text.Rand_Minigames;
        public override string Description => CTTR_Text.Rand_MinigamesDesc;

        private List<int> randMinigames;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randMinigames = new List<int>();

            List<int> possibleMinigames = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                possibleMinigames.Add(i);
            }

            for (int i = 0; i < 8; i++)
            {
                int targetMinigame = possibleMinigames[randState.Next(0, possibleMinigames.Count)];
                randMinigames.Add(targetMinigame);
                possibleMinigames.Remove(targetMinigame);
            }
        }

        public override void ModPass(string path_extr)
        {
            if (File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
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

                File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
        }

    }
}
