using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_RaceLaps : ModStruct<string>
    {
        public override string Name => CTTR_Text.Rand_RaceLaps;
        public override string Description => CTTR_Text.Rand_RaceLapsDesc;

        private List<int> randLaps;
        private bool isSet;

        public CTTR_Rand_RaceLaps(bool set)
        {
            isSet = set;
        }

        public override void BeforeModPass()
        {
            if (isSet)
            {
                return;
            }
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randLaps = new List<int>();
            if (CTTR_Props_Main.Option_RandRaceLaps.Enabled)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (i == 12) // Rings of Uranus
                    {
                        randLaps.Add(randState.Next(3, 13));
                    }
                    else
                    {
                        randLaps.Add(randState.Next(1, 7));
                    }
                }
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
                if (isSet)
                {
                    LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[0] + ",true},";
                    LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[1] + ",true},";
                    LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[2] + ",true},";
                    LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[3] + ",true},";
                    LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[4] + ",true},";
                    LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[5] + ",true},";
                    LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[6] + ",true},";
                    LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[7] + ",true},";
                    LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[8] + ",true},";
                    LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[9] + ",true},";
                    LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[10] + ",true},";
                    LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[11] + ",true},";
                    LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[12] + ",true},";
                    LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[13] + ",true},";
                    LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[14] + ",true},";
                }
                else
                {
                    LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + randLaps[0] + ",true},";
                    LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + randLaps[1] + ",true},";
                    LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + randLaps[2] + ",true},";
                    LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + randLaps[3] + ",true},";
                    LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + randLaps[4] + ",true},";
                    LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + randLaps[5] + ",true},";
                    LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + randLaps[6] + ",true},";
                    LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + randLaps[7] + ",true},";
                    LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + randLaps[8] + ",true},";
                    LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + randLaps[9] + ",true},";
                    LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + randLaps[10] + ",true},";
                    LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + randLaps[11] + ",true},";
                    LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + randLaps[12] + ",true},";
                    LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + randLaps[13] + ",true},";
                    LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + randLaps[14] + ",true},";
                }

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
