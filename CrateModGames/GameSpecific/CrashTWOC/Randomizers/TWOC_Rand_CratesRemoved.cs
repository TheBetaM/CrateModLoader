using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    // todo: test
    public class TWOC_Rand_CratesRemoved : ModStruct<string>
    {
        public override string Name => "Random Crates Removed";
        public override string Description => "Wooden crates are randomly removed in each level. The box counter is adjusted accordingly.";

        public List<TWOC_File_CRT.CrateType> CratesToRemove = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            //TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
            //TWOC_File_CRT.CrateType.Nitro,
            TWOC_File_CRT.CrateType.Reinforced,
            TWOC_File_CRT.CrateType.Checkpoint,
            TWOC_File_CRT.CrateType.Slot,
        };

        public override void ModPass(string extrPath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".CRT";
            /*
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }
            */

            for (int i = 0; i < TWOC_Common.LevelNames.Length; i++)
            {
                string path = extrPath + LevelsPathA + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + Ext;
                if (i > 24)
                {
                    path = extrPath + LevelsPathC + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_CRT CrateFile = new TWOC_File_CRT(path, false);

                    for (int g = 0; g < CrateFile.CrateGroups.Count; g++)
                    {
                        for (int c = 0; c < CrateFile.CrateGroups[g].Crates.Count; c++)
                        {
                            if (CratesToRemove.Contains(CrateFile.CrateGroups[g].Crates[c].Type) && rand.Next(2) == 0)
                            {
                                CrateFile.CrateGroups[g].Crates.RemoveAt(c);
                                c--;
                            }
                        }
                        if (CrateFile.CrateGroups[g].Crates.Count == 0)
                        {
                            CrateFile.CrateGroups.RemoveAt(g);
                            g--;
                        }
                    }

                    CrateFile.Save(path);
                }
            }
        }
    }
}
