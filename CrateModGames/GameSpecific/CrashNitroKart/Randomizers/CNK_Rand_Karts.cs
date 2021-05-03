using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_Karts : ModStruct<GenericModStruct>
    {
        private Random randState;

        public override void BeforeModPass()
        {
            randState = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(GenericModStruct mod)
        {
            string path = mod.ExtractedPath + @"/ASSETS/";
            ConsoleMode console = mod.Console;

            //Replace model files
            string modelpath = path + "";

            if (console == ConsoleMode.PS2)
            {
                modelpath += "ps2/gfx/karts/";
            }
            else if (console == ConsoleMode.XBOX)
            {
                modelpath += "xbox/gfx/karts/";
            }
            else
            {
                modelpath += "gcn/gfx/karts/";
            }

            List<int> kartList = new List<int>();
            List<int> kartList_rand = new List<int>();
            string[] KartTypes = new string[] { "crash", "ncortex", "noxide", "ntrance", "boss_krunk", "boss_nash", "boss_norm_b", "boss_norm_l", "boss_geary", "boss_velo", "boss" };

            //Boss karts crash the game
            for (int i = 0; i < 4; i++)
            {
                kartList.Add(i);
                File.Move(modelpath + KartTypes[i] + ".igb", modelpath + "Kart" + i + ".igb");
            }

            for (int i = 0; i < 4; i++)
            {
                int target_id = randState.Next(0, kartList.Count);
                kartList_rand.Add(kartList[target_id]);
                kartList.RemoveAt(target_id);
            }

            for (int i = 0; i < 4; i++)
            {
                File.Move(modelpath + "Kart" + kartList_rand[i] + ".igb", modelpath + KartTypes[i] + ".igb");
            }
        }

    }
}
