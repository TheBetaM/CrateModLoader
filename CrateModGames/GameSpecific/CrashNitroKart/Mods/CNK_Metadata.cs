using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Metadata : ModStruct<string>
    {
        public override void ModPass(string path_gob_extracted)
        {
            string[] csv_Credits = File.ReadAllLines(path_gob_extracted + "common/ui/eng/credits.csv");

            List<string> csv_Credits_LineList = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                csv_Credits_LineList.Add(csv_Credits[i]);
            }

            if (!csv_Credits[3].Contains(",0x") && !csv_Credits[3].Contains(", 0x") && !csv_Credits[3].Contains(",0x"))
            {
                csv_Credits_LineList.Add("Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + ",AlphaDance,1.25,C,4294950912,0,40");
                csv_Credits_LineList.Add("Seed: " + ModLoaderGlobals.RandomizerSeed + ",AlphaDance,1.25,C,4294950912,0,40");
                csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,4279304191,0,80");
            }
            else
            {
                csv_Credits_LineList.Add("Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                csv_Credits_LineList.Add("Seed: " + ModLoaderGlobals.RandomizerSeed + ",AlphaDance,1.25,C,0xFF10FFFF,0,40");
                csv_Credits_LineList.Add("Crash Nitro Kart,AlphaDance,1.25,C,0xFF10FFFF,0,80");
            }

            for (int i = 4; i < csv_Credits.Length; i++)
            {
                csv_Credits_LineList.Add(csv_Credits[i]);
            }

            csv_Credits = new string[csv_Credits_LineList.Count];
            for (int i = 0; i < csv_Credits_LineList.Count; i++)
            {
                csv_Credits[i] = csv_Credits_LineList[i];
            }

            File.WriteAllLines(path_gob_extracted + "common/ui/eng/credits.csv", csv_Credits);
        }
    }
}
