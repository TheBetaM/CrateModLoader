using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    // crashes...
    public class SonicRivals_Rand_Characters : ModStruct<GenericModStruct>
    {
        private Random rand;

        private bool isRivals2 = false;

        private List<string> Files = new List<string>()
        {
            "RACER_KNUCKLES_0.STR",
            "RACER_KNUCKLES_1.STR",
            "RACER_METALSONIC_0.STR",
            "RACER_METALSONIC_1.STR",
            "RACER_SHADOW_0.STR",
            "RACER_SHADOW_1.STR",
            "RACER_SILVER_0.STR",
            "RACER_SILVER_1.STR",
            "RACER_SONIC_0.STR",
            "RACER_SONIC_1.STR",

        };

        private List<string> Files2 = new List<string>()
        {
            "RACER_ESPIO_0.STR",
            "RACER_ESPIO_1.STR",
            "RACER_KNUCKLES_0.STR",
            "RACER_KNUCKLES_1.STR",
            "RACER_METALSONIC_0.STR",
            "RACER_METALSONIC_1.STR",
            "RACER_ROUGE_0.STR",
            "RACER_ROUGE_1.STR",
            "RACER_SHADOW_0.STR",
            "RACER_SHADOW_1.STR",
            "RACER_SILVER_0.STR",
            "RACER_SILVER_1.STR",
            "RACER_SONIC_0.STR",
            "RACER_SONIC_1.STR",
            "RACER_TAILS_0.STR",
            "RACER_TAILS_1.STR",
        };

        private List<int> Replacer = new List<int>();
        private List<int> Replacer2 = new List<int>();

        public override void BeforeModPass()
        {
            rand = GetRandom();

            List<int> RepList = new List<int>();
            for (int i = 0; i < Files.Count; i++)
            {
                RepList.Add(i);
            }
            Replacer = new List<int>();
            int r = 0;
            while (RepList.Count > 0)
            {
                r = rand.Next(RepList.Count);
                Replacer.Add(RepList[r]);
                RepList.RemoveAt(r);
            }

            List<int> RepList2 = new List<int>();
            for (int i = 0; i < Files2.Count; i++)
            {
                RepList2.Add(i);
            }
            Replacer2 = new List<int>();
            r = 0;
            while (RepList2.Count > 0)
            {
                r = rand.Next(RepList2.Count);
                Replacer2.Add(RepList2[r]);
                RepList2.RemoveAt(r);
            }
        }

        public override void ModPass(GenericModStruct mod)
        {
            if (File.Exists(mod.ExtractedPath + @"STREAMS\CARDSLARGE.STR"))
            {
                isRivals2 = true;
            }

            string path_extr = mod.ExtractedPath + @"STREAMS\";

            if (isRivals2)
            {
                for (int i = 0; i < Files2.Count; i++)
                {
                    string path1 = path_extr + Files2[i];
                    File.Move(path1, path1 + "1");
                }
                for (int i = 0; i < Files2.Count; i++)
                {
                    string oldpath = path_extr + Files2[i] + "1";
                    string newpath = path_extr + Files2[Replacer2[i]];
                    File.Move(oldpath, newpath);
                }
            }
            else
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    string path1 = path_extr + Files[i];
                    File.Move(path1, path1 + "1");
                }
                for (int i = 0; i < Files.Count; i++)
                {
                    string oldpath = path_extr + Files[i] + "1";
                    string newpath = path_extr + Files[Replacer[i]];
                    File.Move(oldpath, newpath);
                }
            }
            
        }
    }
}
