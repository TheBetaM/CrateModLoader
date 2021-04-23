using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    // todo: test
    public class TWOC_Rand_WumpaIntoCrates : ModStruct<string>
    {
        public override string Name => "Random Wumpa Are Random Crates";
        public override string Description => "Wumpas are randomly turned into crates in each level. The box counter is adjusted accordingly.";
        public override bool Hidden => true;

        public List<TWOC_File_CRT.CrateType> CratesToInsert = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
        };
        List<TWOC_File_CRT.CrateType> TimeCrates = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Time1,
            TWOC_File_CRT.CrateType.Time2,
            TWOC_File_CRT.CrateType.Time3,
        };

        public override void ModPass(string extrPath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string ExtWMP = ".WMP";
            string ExtCRT = ".CRT";
            /*
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                ExtWMP = ExtWMP.ToLower();
                ExtCRT = ExtCRT.ToLower();
            }
            else
            {
                ExtCRT += ";1";
                ExtWMP += ";1";
            }
            */

            for (int i = 0; i < TWOC_Common.LevelNames.Length - 5; i++)
            {
                string path = extrPath + LevelsPathA + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + ExtWMP;
                if (i > 24)
                {
                    path = extrPath + LevelsPathC + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + ExtWMP;
                }
                if (File.Exists(path))
                {
                    TWOC_File_WMP WumpaFile = new TWOC_File_WMP(path, false);

                    List<TWOC_Vector3> WumpaPos = new List<TWOC_Vector3>();

                    for (int w = 0; w < WumpaFile.Wumpas.Count; w++)
                    {
                        if (rand.Next(0, 5) == 0)
                        {
                            WumpaPos.Add(WumpaFile.Wumpas[w]);
                            WumpaFile.Wumpas.RemoveAt(w);
                            w--;
                        }
                    }

                    WumpaFile.Save(path);

                    path = extrPath + LevelsPathA + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + ExtCRT;
                    if (i > 24)
                    {
                        path = extrPath + LevelsPathC + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + ExtCRT;
                    }

                    if (File.Exists(path))
                    {
                        TWOC_File_CRT CrateFile = new TWOC_File_CRT(path, false);

                        ushort CrateCount = CrateFile.GetCrateCount();

                        for (int w = 0; w < WumpaPos.Count; w++)
                        {
                            TWOC_File_CRT.CrateGroup Group = new TWOC_File_CRT.CrateGroup();
                            Group.Crates = new List<TWOC_File_CRT.Crate>();
                            TWOC_File_CRT.Crate BaseCrate = new TWOC_File_CRT.Crate();
                            BaseCrate.Pos = new TWOC_Vector3(WumpaPos[w].X, WumpaPos[w].Y, WumpaPos[w].Z);
                            Group.ID = CrateCount;
                            CrateCount++;
                            Group.unkFlags = 0; // ??
                            if (rand.Next(0, 2) == 0)
                                Group.Rot = new TWOC_Vector3((float)rand.NextDouble() * 180f, 0, 0);
                            else
                                Group.Rot = new TWOC_Vector3((float)rand.NextDouble() * -180f, 0, 0);

                            BaseCrate.unkFlags = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
                            int r = rand.Next(CratesToInsert.Count);
                            BaseCrate.Type = CratesToInsert[r];
                            if (rand.Next(0, 3) == 0)
                            {
                                r = rand.Next(TimeCrates.Count);
                                BaseCrate.TypeTT = TimeCrates[r];
                            }
                            else
                            {
                                BaseCrate.TypeTT = BaseCrate.Type;
                            }
                            BaseCrate.Type3 = TWOC_File_CRT.CrateType.NULL;
                            BaseCrate.Type4 = TWOC_File_CRT.CrateType.NULL;
                            BaseCrate.unkFlags2 = new byte[14] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, };
                            Group.Crates.Add(BaseCrate);
                            CrateFile.CrateGroups.Add(Group);
                        }

                        CrateFile.Save(path);
                    }
                }
            }
        }
    }
}
