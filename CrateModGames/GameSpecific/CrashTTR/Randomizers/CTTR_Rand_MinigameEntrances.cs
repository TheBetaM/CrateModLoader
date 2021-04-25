using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_MinigameEntrances : ModStruct<GOD_File>
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

        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains(@"genericobjectives.god"))
            {
                for (int i = 0; i < 8; i++)
                {
                    LUA_Object obj = file.GetObject("Objective", CTTR_Data.MinigameObjectiveTypes[i]);
                    if (obj != null)
                    {
                        obj.Content[obj.Content.Count - 3] = "this.AddAction_UnlockMiniGame(\"OFMiniGames/" + CTTR_Data.MinigameTypeNames[randMinigames[i]] + "\")";
                    }
                }
            }
        }

    }
}
