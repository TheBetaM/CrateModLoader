using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    public class SonicRivals_Rand_LevelOrder : ModStruct<GenericModStruct>
    {
        private Random rand;

        private List<string> Files = new List<string>()
        {
            "ZONE_1_ACT_1.STR",
            "ZONE_1_ACT_2.STR",
            "ZONE_2_ACT_1.STR",
            "ZONE_2_ACT_2.STR",
            "ZONE_3_ACT_1.STR",
            "ZONE_3_ACT_2.STR",
            "ZONE_4_ACT_1.STR",
            "ZONE_4_ACT_2.STR",
            "ZONE_5_ACT_1.STR",
            "ZONE_5_ACT_2.STR",
            "ZONE_6_ACT_1.STR",
            "ZONE_6_ACT_2.STR",
        };
        private List<string> FilesBoss = new List<string>()
        {
            "ZONE_1_ACT_3.STR",
            "ZONE_2_ACT_3.STR",
            "ZONE_4_ACT_3.STR",
            "ZONE_5_ACT_3.STR",
            "ZONE_6_ACT_3.STR",
        };

        private List<int> Replacer = new List<int>();
        private List<int> ReplacerBoss = new List<int>();

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

            RepList = new List<int>();
            for (int i = 0; i < FilesBoss.Count; i++)
            {
                RepList.Add(i);
            }
            ReplacerBoss = new List<int>();
            r = 0;
            while (RepList.Count > 0)
            {
                r = rand.Next(RepList.Count);
                ReplacerBoss.Add(RepList[r]);
                RepList.RemoveAt(r);
            }
        }

        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"STREAMS\";
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

            for (int i = 0; i < FilesBoss.Count; i++)
            {
                string path1 = path_extr + FilesBoss[i];
                File.Move(path1, path1 + "1");
            }
            for (int i = 0; i < FilesBoss.Count; i++)
            {
                string oldpath = path_extr + FilesBoss[i] + "1";
                string newpath = path_extr + FilesBoss[ReplacerBoss[i]];
                File.Move(oldpath, newpath);
            }
        }
    }
}
