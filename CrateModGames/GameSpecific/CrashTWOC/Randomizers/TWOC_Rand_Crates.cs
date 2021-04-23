using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    // todo: test
    public class TWOC_Rand_Crates : ModStruct<string>
    {
        public override string Name => "Randomize Wooden Crates";
        public override string Description => "The types of wooden crates are randomized.";

        public List<TWOC_File_CRT.CrateType> CratesToChange = new List<TWOC_File_CRT.CrateType>()
        {
            TWOC_File_CRT.CrateType.Aku,
            TWOC_File_CRT.CrateType.Blank,
            //TWOC_File_CRT.CrateType.Bounce,
            TWOC_File_CRT.CrateType.Fruit,
            TWOC_File_CRT.CrateType.Invisibility,
            TWOC_File_CRT.CrateType.Life,
            TWOC_File_CRT.CrateType.Pickup,
            TWOC_File_CRT.CrateType.Proximity,
            //TWOC_File_CRT.CrateType.TNT,
        };
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

                    foreach (TWOC_File_CRT.CrateGroup Group in CrateFile.CrateGroups)
                    {
                        foreach (TWOC_File_CRT.Crate Crate in Group.Crates)
                        {
                            if (CratesToChange.Contains(Crate.Type))
                            {
                                int r = rand.Next(CratesToInsert.Count);
                                Crate.Type = CratesToInsert[r];
                            }
                        }
                    }

                    CrateFile.Save(path);
                }
            }
        }
    }
}
