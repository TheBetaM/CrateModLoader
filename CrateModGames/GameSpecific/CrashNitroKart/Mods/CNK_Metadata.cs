using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Metadata : ModStruct<CSV>
    {
        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() == "credits.csv")
            {
                string col1 = "4294950912";

                if (file.Table[3][4].Contains("0x"))
                {
                    col1 = "0xFF10FFFF";
                }

                List<string> Meta1 = new List<string>()
                {
                    "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion,
                    "AlphaDance",
                    "1.25",
                    "C",
                    col1,
                    "0",
                    "40",
                };

                List<string> Meta2 = new List<string>()
                {
                    "Seed: " + ModLoaderGlobals.RandomizerSeed,
                    "AlphaDance",
                    "1.25",
                    "C",
                    col1,
                    "0",
                    "40",
                };

                file.Table.Insert(3, Meta2);
                file.Table.Insert(3, Meta1);
            }
        }
    }
}
