using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_BattleKOs : ModStruct<string>
    {
        public override string Name => "Randomize Battle KO's";

        private List<int> randKOs;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randKOs = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                randKOs.Add(randState.Next(5, 20));
            }
        }

        public override void ModPass(string path_extr)
        {
            if (File.Exists(path_extr + @"design\startup.god"))
            {
                string[] startup_lines = File.ReadAllLines(path_extr + @"design\startup.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int LevelListStart = 0;
                for (int i = 0; i < LineList.Count; i++)
                {
                    if (LineList[i] == "function GetLevelList()")
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }
                LineList[LevelListStart + 3] = "{\"adventure_arena\",ThemeAdventure,TypeBattle," + randKOs[0] + ",true},";
                LineList[LevelListStart + 8] = "{\"bonus1_arena\",ThemeFairy,TypeBattle," + randKOs[1] + ",true},";
                LineList[LevelListStart + 12] = "{\"dino_arena\",ThemeDino,TypeBattle," + randKOs[2] + ",true},";
                LineList[LevelListStart + 16] = "{\"egypt_arena\",ThemeEgypt,TypeBattle," + randKOs[3] + ",true},";

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

            }
        }

    }
}
