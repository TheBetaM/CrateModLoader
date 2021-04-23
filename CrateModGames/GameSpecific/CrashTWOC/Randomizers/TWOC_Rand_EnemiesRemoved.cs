using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    // todo: test
    public class TWOC_Rand_EnemiesRemoved : ModStruct<string>
    {
        public override string Name => "Random Enemies Removed";
        public override string Description => "Enemies are randomly removed in each level.";

        List<string> BannedEnemies = new List<string>()
            {
                "crystal",
                "clock",
                "probe",
                "crate gem",
                "green gem",
                "bonus gem",
                "flying clock",
                "super slam",
                "flying crystal",
                "flying crategem",
                "flying probe",
                "blue gem",
                "double jump",
                "red gem",
                "flying bonusgem",
                "tiptoe",
                "super spin",
                "sprint",
                "space cortex",
                "space crunch",
                "water crunch",
                "yellow gem",
                "purple gem",
                "bazooka",
                "coco",
                "space lo-lo",
                "earth crunch",
                "space rok-ko",
                "space wa-wa",
                "space py-ro",
                "atlas crunch",
                "turtle",

            };

        public override void ModPass(string extrPath)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            string Ext = ".AI";
            /*
            if (ConsolePipeline.Metadata.Console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
                Ext = Ext.ToLower();
            }
            else
            {
                Ext += ";1";
            }
            */

            for (int i = 0; i < TWOC_Common.LevelNames.Length; i++)
            {
                string path = extrPath + LevelsPathA + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + Ext;
                if (i > 24)
                {
                    path = extrPath + LevelsPathC + TWOC_Common.LevelNames[i] + @"\" + TWOC_Common.FileNames[i] + Ext;
                }
                if (File.Exists(path))
                {
                    TWOC_File_AI AIFile = new TWOC_File_AI(path, false);

                    for (int a = 0; a < AIFile.AI.Count; a++)
                    {
                        if (!BannedEnemies.Contains(AIFile.AI[a].Name) && rand.Next(2) == 0)
                        {
                            AIFile.AI.RemoveAt(a);
                            a--;
                        }
                    }

                    AIFile.Save(path);
                }
            }
        }
    }
}
