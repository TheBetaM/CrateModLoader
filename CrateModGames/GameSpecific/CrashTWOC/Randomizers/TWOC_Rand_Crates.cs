using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_Rand_Crates : ModStruct<TWOC_File_CRT>
    {
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

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(TWOC_File_CRT CrateFile)
        {
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
        }
    }
}
