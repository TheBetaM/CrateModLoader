using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.Sonic2Rivals
{
    public class SonicRivals2_Rand_LevelOrder : ModStruct<GenericModStruct>
    {
        private Random rand;

        private List<string> Files = new List<string>()
        {
            "R2_ZONE_1_ACT_1.STR",
            "R2_ZONE_1_ACT_2.STR",
            "R2_ZONE_1_ACT_3.STR",
            "R2_ZONE_2_ACT_1.STR",
            "R2_ZONE_2_ACT_2.STR",
            "R2_ZONE_2_ACT_3.STR",
            "R2_ZONE_3_ACT_1.STR",
            "R2_ZONE_3_ACT_2.STR",
            "R2_ZONE_3_ACT_3.STR",
            "R2_ZONE_4_ACT_1.STR",
            "R2_ZONE_4_ACT_2.STR",
            "R2_ZONE_4_ACT_3.STR",
            "R2_ZONE_5_ACT_1.STR",
            "R2_ZONE_5_ACT_2.STR",
            "R2_ZONE_5_ACT_3.STR",
            "R2_ZONE_6_ACT_1.STR",
            "R2_ZONE_6_ACT_2.STR",
            "R2_ZONE_6_ACT_3.STR",
        };
        private List<string> FilesBoss = new List<string>()
        {
            "R2_ZONE_1_ACT_4.STR",
            "R2_ZONE_2_ACT_4.STR",
            "R2_ZONE_3_ACT_4.STR",
            "R2_ZONE_4_ACT_4.STR",
            "R2_ZONE_5_ACT_4.STR",
            "R2_ZONE_6_ACT_4.STR",
        };
        private List<string> FilesLaps = new List<string>()
        {
            "R2_ZONE_1_LAPS.STR",
            "R2_ZONE_2_LAPS.STR",
            "R2_ZONE_3_LAPS.STR",
            "R2_ZONE_4_LAPS.STR",
            "R2_ZONE_5_LAPS.STR",
            "R2_ZONE_6_LAPS.STR",
        };

        private List<int> Replacer = new List<int>();
        private List<int> ReplacerBoss = new List<int>();
        private List<int> ReplacerLaps = new List<int>();

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

            RepList = new List<int>();
            for (int i = 0; i < FilesLaps.Count; i++)
            {
                RepList.Add(i);
            }
            ReplacerLaps = new List<int>();
            r = 0;
            while (RepList.Count > 0)
            {
                r = rand.Next(RepList.Count);
                ReplacerLaps.Add(RepList[r]);
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

            /*
            for (int i = 0; i < FilesLaps.Count; i++)
            {
                string path1 = path_extr + FilesLaps[i];
                File.Move(path1, path1 + "1");
            }
            for (int i = 0; i < FilesLaps.Count; i++)
            {
                string oldpath = path_extr + FilesLaps[i] + "1";
                string newpath = path_extr + FilesLaps[ReplacerLaps[i]];
                File.Move(oldpath, newpath);
            }
            */
        }
    }
}
