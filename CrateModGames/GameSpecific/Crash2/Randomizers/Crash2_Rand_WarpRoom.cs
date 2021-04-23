using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash2;

namespace CrateModLoader.GameSpecific.Crash2
{
    //unfinished
    public class Crash2_Rand_WarpRoom : ModStruct<NSF_Pair>
    {
        public override string Name => "Randomize Level Order";

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC2 != Crash2_Levels.WarpRoom)
            {
                return;
            }

            int LevelCount = 35;

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

            int CortexID = 30;

            GOOLEntry warp = pair.nsf.GetEntry<GOOLEntry>("ButOC");
            if (warp != null)
            {
                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        OrigValues.Add(warp.Instructions[10 + (i * 8)].Value);
                    }
                }

                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        if (i > CortexID)
                        {
                            warp.Instructions[10 + (i * 8)].Value = OrigValues[LevelsRand[i - 1]];
                        }
                        else
                        {
                            warp.Instructions[10 + (i * 8)].Value = OrigValues[LevelsRand[i]];
                        }
                    }
                }
            }
        }
    }
}
