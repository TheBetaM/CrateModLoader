using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashMoM.Mods
{
    public class MoM_Rand_CostumeVisuals : ModStruct<string>
    {
        public override string Name => "Randomize Costume Visuals";
        public override string Description => "Shuffles Crash's playable costume visuals.";

        private Random rand;

        private List<string> Costumes = new List<string>()
        {
            "b06ecd92", //vanilla
            "7ed57722", //parafox
            "2f22d61e", //ratcicle
            "29ac4a2e", //shurtle
            "8ee6e47a", //skeleton
            "55915af9", //spike
            "55b4ea5b", //var1
            "55b4ea5c", //var2
            "55b4ea5d", //var3
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
