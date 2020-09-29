using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    [ModCategory(1)]
    static class CTTR_Settings
    {

        public static string[] TrackNames = Enum.GetNames(typeof(TrackID));

        public static ModPropNamedUIntArray RaceLaps = new ModPropNamedUIntArray(new uint[] 
        {
            3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 9, 3, 3
        }, TrackNames);

        public static void ParseSettings(string path_extr)
        {
            // example setting
            if (RaceLaps.HasChanged)
            {
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
                    LineList[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + RaceLaps.Value[0] + ",true},";
                    LineList[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + RaceLaps.Value[1] + ",true},";
                    LineList[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + RaceLaps.Value[2] + ",true},";
                    LineList[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + RaceLaps.Value[3] + ",true},";
                    LineList[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + RaceLaps.Value[4] + ",true},";
                    LineList[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + RaceLaps.Value[5] + ",true},";
                    LineList[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + RaceLaps.Value[6] + ",true},";
                    LineList[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + RaceLaps.Value[7] + ",true},";
                    LineList[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + RaceLaps.Value[8] + ",true},";
                    LineList[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + RaceLaps.Value[9] + ",true},";
                    LineList[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + RaceLaps.Value[10] + ",true},";
                    LineList[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + RaceLaps.Value[11] + ",true},";
                    LineList[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + RaceLaps.Value[12] + ",true},";
                    LineList[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + RaceLaps.Value[13] + ",true},";
                    LineList[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + RaceLaps.Value[14] + ",true},";

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
