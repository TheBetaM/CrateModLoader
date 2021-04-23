using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_Rand_EpisodeOrder : ModStruct<string>
    {
        public override string Name => "Randomize Episode Order";
        public override bool Hidden => true;

        List<string> EpisodeFolderNames = new List<string>()
        {
            "L1_E1",
            "L1_E2",
            "L1_E3",
            "L1_E4",
            "L2_E1",
            "L2_E2",
            "L2_E4",
            "L3_E1",
            "L3_E1A",
            "L3_E1B",
            "L3_E2",
            "L3_E3",
            "L3_E4",
            "L4_E1",
            "L4_E2",
            "L4_E3",
            "L4_E4",
            "L5_E1",
            "L5_E2",
            "L5_E3",
        };

        public override void ModPass(string path_extr)
        {
            // unfinished
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
            List<int> LevelsToRand = new List<int>();
            for (int i = 0; i < EpisodeFolderNames.Count; i++)
            {
                LevelsToRand.Add(i);
                Directory.Move(path_extr + @"levels\" + EpisodeFolderNames[i], path_extr + @"levels\" + "level" + i);
            }

            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < EpisodeFolderNames.Count; i++)
            {
                int r = rand.Next(LevelsToRand.Count);
                LevelsRand.Add(LevelsToRand[r]);
                LevelsToRand.RemoveAt(r);
            }

            for (int i = 0; i < EpisodeFolderNames.Count; i++)
            {
                Directory.Move(path_extr + @"levels\" + "level" + i, path_extr + @"levels\" + EpisodeFolderNames[LevelsRand[i]]);
            }
        }
    }
}
