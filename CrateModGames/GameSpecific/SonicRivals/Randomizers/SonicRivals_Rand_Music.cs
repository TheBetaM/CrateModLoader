using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    public class SonicRivals_Rand_Music : ModStruct<GenericModStruct>
    {
        private Random rand;

        private List<string> Files = new List<string>()
        {
            "LEVELMUSIC_Z1A1.AT3",
            "LEVELMUSIC_Z1A2.AT3",
            "LEVELMUSIC_Z1A3.AT3",
            "LEVELMUSIC_Z2A1.AT3",
            "LEVELMUSIC_Z2A2.AT3",
            "LEVELMUSIC_Z2A3.AT3",
            "LEVELMUSIC_Z3A1.AT3",
            "LEVELMUSIC_Z3A2.AT3",
            "LEVELMUSIC_Z3A3.AT3",
            "LEVELMUSIC_Z4A1.AT3",
            "LEVELMUSIC_Z4A2.AT3",
            "LEVELMUSIC_Z4A3.AT3",
            "LEVELMUSIC_Z5A1.AT3",
            "LEVELMUSIC_Z5A2.AT3",
            "LEVELMUSIC_Z5A3.AT3",
            "LEVELMUSIC_Z6A1.AT3",
            "LEVELMUSIC_Z6A2.AT3",
            "LEVELMUSIC_Z6A3.AT3",
        };

        private List<int> Replacer = new List<int>();

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
        }

        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"SOUNDS\MUSIC\";
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
