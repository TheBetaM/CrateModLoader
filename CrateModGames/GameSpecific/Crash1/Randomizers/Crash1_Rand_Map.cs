using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    public class Crash1_Rand_Map : ModStruct<NSF_Pair>
    {
        public override string Name => "Randomize Level Order";
        public override string Description => "Shuffle the order of which levels you enter. The Cortex boss is still the last level to play.";

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC1 != Crash1_Levels.MapMainMenu)
            {
                return;
            }

            int LevelCount = 31;

            List<int> LevelsToReplace = new List<int>();
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < LevelCount + 1; i++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsToReplaceNoKeys = new List<int>(LevelsToReplace);
            LevelsToReplaceNoKeys.Remove(25); // jaws
            if (pair.region == RegionType.NTSC_J)
                LevelsToReplaceNoKeys.Remove(23); //sunset
            else
                LevelsToReplaceNoKeys.Remove(15); //sunset

            // key unlock level 1
            int rk = rand.Next(LevelsToReplaceNoKeys.Count);
            LevelsRand.Add(LevelsToReplaceNoKeys[rk]);
            LevelsToReplace.Remove(LevelsToReplaceNoKeys[rk]);
            LevelsToReplaceNoKeys.RemoveAt(rk);

            // key unlock level 2
            rk = rand.Next(LevelsToReplaceNoKeys.Count);
            LevelsRand.Add(LevelsToReplaceNoKeys[rk]);
            LevelsToReplace.Remove(LevelsToReplaceNoKeys[rk]);
            LevelsToReplaceNoKeys.RemoveAt(rk);

            for (int i = 0; i < LevelCount - 1; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Insert(0, LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }

            List<int> OrigValues = new List<int>();
            List<int> OrigValues_LevelName = new List<int>();

            List<int> DontSwap = new List<int>()
            {
                30, //cortex
                32, //dupe lights out
            };

            GOOLEntry map = pair.nsf.GetEntry<GOOLEntry>("IsldC");
            if (map != null)
            {
                for (int i = 0; i < LevelCount + 3; i++)
                {
                    if (!DontSwap.Contains(i))
                    {
                        OrigValues.Add(map.Instructions[37 + (i * 9)].Value);
                        OrigValues_LevelName.Add(map.Instructions[41 + (i * 9)].Value);
                    }
                }

                for (int i = 0; i < LevelCount + 4; i++)
                {
                    if (i != 30 && i != 32 && i != 34)
                    {
                        if (i > 30)
                        {
                            if (i > 32)
                            {
                                map.Instructions[37 + (i * 9)].Value = OrigValues[LevelsRand[i - 2]];
                                map.Instructions[41 + (i * 9)].Value = OrigValues_LevelName[LevelsRand[i - 2]];
                            }
                            else
                            {
                                map.Instructions[37 + (i * 9)].Value = OrigValues[LevelsRand[i - 1]];
                                map.Instructions[41 + (i * 9)].Value = OrigValues_LevelName[LevelsRand[i - 1]];
                            }
                        }
                        else
                        {
                            map.Instructions[37 + (i * 9)].Value = OrigValues[LevelsRand[i]];
                            map.Instructions[41 + (i * 9)].Value = OrigValues_LevelName[LevelsRand[i]];
                        }
                    }
                    else
                    {
                        // walking backwards in split paths sets the level ID differently for a second
                        if (i == 32)
                        {
                            map.Instructions[37 + (i * 9)].Value = OrigValues[LevelsRand[24]];
                            map.Instructions[41 + (i * 9)].Value = OrigValues_LevelName[LevelsRand[24]];
                        }
                        else if (i == 34)
                        {
                            map.Instructions[37 + (i * 9)].Value = OrigValues[LevelsRand[14]];
                            map.Instructions[41 + (i * 9)].Value = OrigValues_LevelName[LevelsRand[14]];
                        }
                    }
                }
            }
        }
    }
}
