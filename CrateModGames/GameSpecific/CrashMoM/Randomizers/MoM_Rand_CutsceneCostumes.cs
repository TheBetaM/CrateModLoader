using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashMoM.Mods
{
    // these models aren't used in cutscenes
    public class MoM_Rand_CutsceneCostumes : ModStruct<GenericModStruct>
    {
        private Random rand;

        private List<string> Costumes = new List<string>()
        {
            "1a7615cb", //vanilla
            "9b4def7b", //parafox
            "979b7077", //ratcicle
            "892c5c87", //shurtle
            "3fb8f0d3", //skeleton
            "27360fd2", //spike
            "425f1e34", //var1
            "426d35b5", //var2
            "427b4d36", //var3
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

        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"default\";
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
