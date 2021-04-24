using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    // todo: test
    public class TWOC_Rand_LevelOrder : ModStruct<TWOC_GenericMod>
    {
        public override string Name => "Randomize Level Order";
        public override bool Hidden => true;

        public override void ModPass(TWOC_GenericMod mod)
        {
            string extrPath = mod.mainPath;
            ConsoleMode console = mod.console;

            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            
            if (console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
            }
            
            int maxLevel = TWOC_Common.LevelNames.Length; // - 5;

            List<TWOC_Levels> LevelsNoRand = new List<TWOC_Levels>()
            {
                TWOC_Levels.L03_Bamboozled,
                TWOC_Levels.L14_EskimoRoll,
                TWOC_Levels.L23_MedievalMadness,
                TWOC_Levels.L29_SolarBowler,

                TWOC_Levels.L07_SeaShellShenanigans,
                TWOC_Levels.L10_H2OhNo,
                TWOC_Levels.L19_CoralCanyon,

                TWOC_Levels.L02_TornadoAlley,
                TWOC_Levels.L09_ThatSinkingFeeling,
                TWOC_Levels.L18_Crashteroids,
                TWOC_Levels.L24_CrateBallsOfFire,
                TWOC_Levels.L28_IceStationBandicoot,

                TWOC_Levels.L13_SmokeyAndTheBandicoot,
                TWOC_Levels.L27_GhostTown,

                TWOC_Levels.B01_Earth,
                TWOC_Levels.B02_Water,
                TWOC_Levels.B03_Fire,
                TWOC_Levels.B04_Air,
                TWOC_Levels.B05_Cortex,
            };

            List<int> LevelsToRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                if (i < 25)
                {
                    Directory.Move(extrPath + LevelsPathA + TWOC_Common.LevelNames[i], extrPath + LevelsPathA + "LEVEL" + i);
                }
                else
                {
                    Directory.Move(extrPath + LevelsPathC + TWOC_Common.LevelNames[i], extrPath + LevelsPathC + "LEVEL" + i);
                }

                if (!LevelsNoRand.Contains((TWOC_Levels)i))
                {
                    LevelsToRand.Add(i);
                }
            }

            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < maxLevel; i++)
            {
                if (LevelsNoRand.Contains((TWOC_Levels)i))
                {
                    LevelsRand.Add(i);
                }
                else
                {
                    int r = rand.Next(LevelsToRand.Count);
                    LevelsRand.Add(LevelsToRand[r]);
                    LevelsToRand.RemoveAt(r);
                }
            }

            for (int i = 0; i < maxLevel; i++)
            {
                string LevelPathIn = LevelsPathA;
                string LevelPathOut = LevelsPathA;
                if (i > 24)
                {
                    LevelPathIn = LevelsPathC;
                }
                if (LevelsRand[i] > 24)
                {
                    LevelPathOut = LevelsPathC;
                }

                Directory.Move(extrPath + LevelPathIn + "LEVEL" + i, extrPath + LevelPathOut + TWOC_Common.LevelNames[LevelsRand[i]]);

                if (i != LevelsRand[i])
                {
                    DirectoryInfo di = new DirectoryInfo(extrPath + LevelPathOut + TWOC_Common.LevelNames[LevelsRand[i]]);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        if (file.Name.ToUpper().Contains(TWOC_Common.FileNames[i]))
                        {
                            file.MoveTo(di.FullName + @"\" + TWOC_Common.FileNames[LevelsRand[i]] + file.Extension);
                        }
                    }
                }
            }
        }
    }
}
