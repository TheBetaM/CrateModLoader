using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    //todo add mod prop for manual editing
    public class CTTR_Rand_BattleKOs : ModStruct<GOD_File>
    {
        public override string Name => "Randomize Battle KO's";

        private List<int> randKOs;
        private bool isSet = false;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randKOs = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                randKOs.Add(randState.Next(5, 25));
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
                    obj.Content[LevelListStart + 3] = "{\"adventure_arena\",ThemeAdventure,TypeBattle," + randKOs[0] + ",true},";
                    obj.Content[LevelListStart + 8] = "{\"bonus1_arena\",ThemeFairy,TypeBattle," + randKOs[1] + ",true},";
                    obj.Content[LevelListStart + 12] = "{\"dino_arena\",ThemeDino,TypeBattle," + randKOs[2] + ",true},";
                    obj.Content[LevelListStart + 16] = "{\"egypt_arena\",ThemeEgypt,TypeBattle," + randKOs[3] + ",true},";
                }
                else
                {
                    obj.Content[LevelListStart + 3] = "{\"adventure_arena\",ThemeAdventure,TypeBattle," + randKOs[0] + ",true},";
                    obj.Content[LevelListStart + 8] = "{\"bonus1_arena\",ThemeFairy,TypeBattle," + randKOs[1] + ",true},";
                    obj.Content[LevelListStart + 12] = "{\"dino_arena\",ThemeDino,TypeBattle," + randKOs[2] + ",true},";
                    obj.Content[LevelListStart + 16] = "{\"egypt_arena\",ThemeEgypt,TypeBattle," + randKOs[3] + ",true},";
                }

            }
        }

    }
}
