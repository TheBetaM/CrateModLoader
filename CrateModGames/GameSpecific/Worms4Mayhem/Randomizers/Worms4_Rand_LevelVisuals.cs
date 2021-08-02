using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.Worms4
{
    public class W4_Rand_LevelVisuals : ModStruct<XOM_File>
    {
        private Random rand;

        private List<string> TimesOfDay = new List<string>()
        {
            "DAY",
            "NIGHT",
            "EVENING",
        };

        private List<string> LevelThemes = new List<string>()
        {
            "ARABIAN",
            "BUILDING",
            "CAMELOT",
            "PREHISTORIC",
            "WILDWEST",
        };
        private List<string> LevelThemes_Materials = new List<string>()
        {
            @"ThemeArabian\ThemeArabian.txt",
            @"ThemeBuilding\ThemeBuilding.txt",
            @"ThemeCamelot\ThemeCamelot.txt",
            @"ThemePrehistoric\ThemePrehistoric.txt",
            @"ThemeWildwest\ThemeWildwest.txt",
        };

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(XOM_File file)
        {
            string level = Path.GetFileName(file.FileName);
            if (!Worms4_Common.Levels_All.Contains(level)) return;

            int rand_theme = rand.Next(LevelThemes.Count);
            int rand_time = rand.Next(TimesOfDay.Count);

            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is XStringResourceDetails cont)
                {
                    if (file.Strings[(int)cont.NameKey.Value] == "Databank.TimeOfDay")
                    {
                        file.Strings[(int)cont.ValueKey.Value] = TimesOfDay[rand_time];
                    }
                    if (file.Strings[(int)cont.NameKey.Value] == "Databank.Theme")
                    {
                        file.Strings[(int)cont.ValueKey.Value] = LevelThemes[rand_theme];
                    }
                    if (file.Strings[(int)cont.NameKey.Value] == "Databank.MaterialFile")
                    {
                        file.Strings[(int)cont.ValueKey.Value] = LevelThemes_Materials[rand_theme];
                    }
                }
            }
        }
    }
}
