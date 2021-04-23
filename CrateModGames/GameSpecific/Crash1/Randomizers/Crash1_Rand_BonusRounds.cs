using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    //unfinished
    public class Crash1_Rand_BonusRounds : ModStruct<NSF_Pair>
    {
        public override string Name => "Randomize Bonus Rounds";

        private Random rand;

        private Dictionary<Crash1_Levels, BonusLevels[]> ValidBonuses = new Dictionary<Crash1_Levels, BonusLevels[]>()
        {
            [Crash1_Levels.Bonus_TawnaShort] = new BonusLevels[] { },
        };


        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (!Crash1_Common.BonusLevelsList.Contains(pair.LevelC1)) return;

            int LevelCount = 22;

            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < LevelCount; i++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < LevelCount; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Add(LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }

            List<int> OrigValues = new List<int>();

            GOOLEntry map = pair.nsf.GetEntry<GOOLEntry>("BonoC");
            if (map != null)
            {
                for (int i = 0; i < LevelCount; i++)
                {
                    OrigValues.Add(map.Instructions[6097 + (i * 4)].Value);
                }

                for (int i = 0; i < LevelCount; i++)
                {
                    map.Instructions[6097 + (i * 4)].Value = OrigValues[LevelsRand[i]];
                }
            }
        }
    }
}
