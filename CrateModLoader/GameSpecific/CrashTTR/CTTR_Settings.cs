using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    static class CTTR_Settings
    {

        public static ModPropInt RaceLaps = new ModPropInt(3);

        public static void ParseSettings(string path_extr)
        {
            // example setting
            if (RaceLaps.HasChanged)
            {
                int Laps = RaceLaps.Value;
                if (System.IO.File.Exists(path_extr + @"design\startup.god"))
                {
                    string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\startup.god");
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
                    LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + Laps + ",true},";
                    LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + Laps + ",true},";

                    startup_lines = new string[LineList.Count];
                    for (int i = 0; i < LineList.Count; i++)
                    {
                        startup_lines[i] = LineList[i];
                    }

                    System.IO.File.WriteAllLines(path_extr + @"design\startup.god", startup_lines);

                }
            }
        }

    }
}
