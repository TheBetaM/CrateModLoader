using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_Rand_CratesRemoved : ModStruct<TWOC_File_CRT>
    {
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

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(TWOC_File_CRT CrateFile)
        {
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
        }
    }
}
