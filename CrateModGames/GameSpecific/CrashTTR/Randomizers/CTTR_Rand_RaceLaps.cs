using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Rand_RaceLaps : ModStruct<GOD_File>
    {
        private List<int> randLaps;
        private bool isSet;

        public CTTR_Rand_RaceLaps()
        {
            isSet = CTTR_Props_Main.Option_RandRaceLaps.Enabled;
        }

        public override void BeforeModPass()
        {
            if (isSet)
            {
                return;
            }
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randLaps = new List<int>();
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

        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains(@"startup.god"))
            {

                LUA_Object obj = file.GetObject("Script", "StartUp");

                int LevelListStart = 0;
                for (int i = 0; i < obj.Content.Count; i++)
                {
                    if (obj.Content[i].StartsWith("function GetLevelList()"))
                    {
                        LevelListStart = i + 2;
                        break;
                    }
                }

                if (isSet)
                {
                    obj.Content[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[0] + ",true},";
                    obj.Content[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[1] + ",true},";
                    obj.Content[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[2] + ",true},";
                    obj.Content[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[3] + ",true},";
                    obj.Content[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[4] + ",true},";
                    obj.Content[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[5] + ",true},";
                    obj.Content[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[6] + ",true},";
                    obj.Content[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[7] + ",true},";
                    obj.Content[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[8] + ",true},";
                    obj.Content[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[9] + ",true},";
                    obj.Content[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[10] + ",true},";
                    obj.Content[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[11] + ",true},";
                    obj.Content[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[12] + ",true},";
                    obj.Content[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[13] + ",true},";
                    obj.Content[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + CTTR_Props_Misc.RaceLaps.Value[14] + ",true},";
                }
                else
                {
                    obj.Content[LevelListStart] = "{\"adventure1\",ThemeAdventure,TypeRace," + randLaps[0] + ",true},";
                    obj.Content[LevelListStart + 1] = "{\"adventure2\",ThemeAdventure,TypeRace," + randLaps[1] + ",true},";
                    obj.Content[LevelListStart + 2] = "{\"adventure3\",ThemeAdventure,TypeRace," + randLaps[2] + ",true},";
                    obj.Content[LevelListStart + 4] = "{\"fairy1\",ThemeFairy,TypeRace," + randLaps[3] + ",true},";
                    obj.Content[LevelListStart + 5] = "{\"fairy2\",ThemeFairy,TypeRace," + randLaps[4] + ",true},";
                    obj.Content[LevelListStart + 6] = "{\"fairy3\",ThemeFairy,TypeRace," + randLaps[5] + ",true},";
                    obj.Content[LevelListStart + 9] = "{\"dino1\",ThemeDino,TypeRace," + randLaps[6] + ",true},";
                    obj.Content[LevelListStart + 10] = "{\"dino2\",ThemeDino,TypeRace," + randLaps[7] + ",true},";
                    obj.Content[LevelListStart + 11] = "{\"dino3\",ThemeDino,TypeRace," + randLaps[8] + ",true},";
                    obj.Content[LevelListStart + 13] = "{\"egypt1\",ThemeEgypt,TypeRace," + randLaps[9] + ",true},";
                    obj.Content[LevelListStart + 14] = "{\"egypt2\",ThemeEgypt,TypeRace," + randLaps[10] + ",true},";
                    obj.Content[LevelListStart + 15] = "{\"egypt3\",ThemeEgypt,TypeRace," + randLaps[11] + ",true},";
                    obj.Content[LevelListStart + 17] = "{\"solar1\",ThemeSolar,TypeRace," + randLaps[12] + ",true},";
                    obj.Content[LevelListStart + 18] = "{\"solar2\",ThemeSolar,TypeRace," + randLaps[13] + ",true},";
                    obj.Content[LevelListStart + 19] = "{\"solar3\",ThemeSolar,TypeRace," + randLaps[14] + ",true},";
                }

            }
        }

    }
}
