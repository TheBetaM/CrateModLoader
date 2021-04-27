using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_Rand_CostumeVisuals : ModStruct<string>
    {
        public override string Name => "Randomize Costume Visuals";
        public override string Description => "Shuffles Crash's playable costume visuals.";

        private Random rand;

        private List<string> Costumes = new List<string>()
        {
            "b06ecd92", //vanilla
            "9a6320ed",
            "14dca1cc",
            "7947835b",
            "292e19f5",
            "bf4c98de",
            //"344a15f9", // hulk
            "55200ca5",
            "52511f40",
            "7ed57722",
            "5a0e7b87",
            "2f22d61e",
            "5ad87557",
            "7c63dfcb",
            "2319828d",
            "29ac4a2e",
            "8ee6e47a",
            "5c6700f3",
            "55915af9",
            "5cd26ea1",
            "62cfa6e9",
        };

        private List<int> Replacer = new List<int>();

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);

            List<int> RepList = new List<int>();
            for (int i = 0; i < Costumes.Count; i++)
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

        public override void ModPass(string path_extr)
        {
            for (int i = 0; i < Costumes.Count; i++)
            {
                string path1 = path_extr + "package/" + Costumes[i] + ".p3d";
                File.Move(path1, path1 + "1");
            }
            for (int i = 0; i < Costumes.Count; i++)
            {
                string oldpath = path_extr + "package/" + Costumes[i] + ".p3d1";
                string newpath = path_extr + "package/" + Costumes[Replacer[i]] + ".p3d";
                File.Move(oldpath, newpath);
            }
        }
    }
}
