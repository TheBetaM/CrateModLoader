using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CTTR
{
    public static class CTTR_Mods
    {
        public static void Mod_PreventSequenceBreaks(string path_extr)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToDino", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_dino\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToDino", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_egypt\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_solar\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_fairy.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "DinoKeyCollection", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"WeenieUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "DinoKeyCollection", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god", objective_lines);

            }
        }

        public static void Mod_EditCredits(string basePath)
        {
            string path_extr = basePath + @"cml_extr\";
            if (System.IO.File.Exists(path_extr + @"design\levels\common\frontend.god"))
            {

                string[] frontend_lines = System.IO.File.ReadAllLines(path_extr + @"design\levels\common\frontend.god");

                // Editing credits to add CML metadata
                for (int i = 0; i < frontend_lines.Length; i++)
                {
                    if (frontend_lines[i] == "screen.AddLine(\"\",0,\"\")")
                    {
                        frontend_lines[i + 1] = "screen.AddLine(\"Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + "\",0,\"\")";
                        frontend_lines[i + 2] = "screen.AddLine(\"Seed: " + ModLoaderGlobals.RandomizerSeed + "\",0,\"\")";
                        frontend_lines[i + 3] = "screen.AddLine(\"Options: " + Program.ModProgram.Modder.OptionsSelectedString + "\",0,\"\")";
                        frontend_lines[i + 4] = "screen.AddLineSpecial(\"creditscttr\",0,104,104,255,1.2,true)";
                        break;
                    }
                }

                System.IO.File.WriteAllLines(path_extr + @"design\levels\common\frontend.god", frontend_lines);
            }
        }

        public static void TestMod_1(string path_extr)
        {
            string[] files = {
                @"art\frontend\frontend.p3d",
                @"art\frontend\frontend_pal.p3d",
                @"art\frontend\frontend_japan.p3d",
                @"art\frontend\loading.p3d",
                @"art\frontend\loading_pal.p3d",
                @"art\frontend\loading_japan.p3d",
                @"art\frontend\onfoot.p3d",
                @"art\frontend\onfoot_pal.p3d",
                @"art\frontend\onfoot_japan.p3d",
                @"art\frontend\bootup.p3d",
                @"art\frontend\bootup_pal.p3d",
                @"art\frontend\bootup_japan.p3d",
                @"art\frontend\arena.p3d",
                @"art\frontend\arena_pal.p3d",
                @"art\frontend\arena_japan.p3d",
                @"art\frontend\driving.p3d",
                @"art\frontend\driving_pal.p3d",
                @"art\frontend\driving_japan.p3d",
                @"art\frontend\minigame.p3d",
                @"art\frontend\minigame_pal.p3d",
                @"art\frontend\minigame_japan.p3d",
                @"art\frontend\stunt.p3d",
                @"art\frontend\stunt_pal.p3d",
                @"art\frontend\stunt_japan.p3d",
            };
            for (int i = 0; i < files.Length; i++)
            {
                TestMod_Text(path_extr, files[i]);
            }
        }
        public static void TestMod_Text(string path_extr, string file_name)
        {

            Pure3D.File targetFile = new Pure3D.File();
            if (System.IO.File.Exists(path_extr + file_name))
            {
                targetFile.Load(path_extr + file_name);
            }
            else
            {
                return;
            }

            int chunkPos;
            if (targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading") != null)
            {
                chunkPos = targetFile.RootChunk.GetChildIndexByName<FrontendLanguage>("loading");
                FrontendLanguage lang = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[0];
                lang.TextStrings[1] = "Really long loading text for testing!";
                lang.TextStrings[2] = "Really long loading text for testing!!";
                lang.TextStrings[3] = "Really long loading text for testing!!!";
                lang.TextStrings[4] = "Really long loading text for testing!!!!";
                if (targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children.Count > 1)
                {
                    FrontendLanguage lang1 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[1];
                    lang1.TextStrings[1] = "Really long loading text for testing!";
                    lang1.TextStrings[2] = "Really long loading text for testing!!";
                    lang1.TextStrings[3] = "Really long loading text for testing!!!";
                    lang1.TextStrings[4] = "Really long loading text for testing!!!!";
                    FrontendLanguage lang2 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[2];
                    lang2.TextStrings[1] = "Really long loading text for testing!";
                    lang2.TextStrings[2] = "Really long loading text for testing!!";
                    lang2.TextStrings[3] = "Really long loading text for testing!!!";
                    lang2.TextStrings[4] = "Really long loading text for testing!!!!";
                    FrontendLanguage lang3 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[3];
                    lang3.TextStrings[1] = "Really long loading text for testing!";
                    lang3.TextStrings[2] = "Really long loading text for testing!!";
                    lang3.TextStrings[3] = "Really long loading text for testing!!!";
                    lang3.TextStrings[4] = "Really long loading text for testing!!!!";
                    FrontendLanguage lang4 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[4];
                    lang4.TextStrings[1] = "Really long loading text for testing!";
                    lang4.TextStrings[2] = "Really long loading text for testing!!";
                    lang4.TextStrings[3] = "Really long loading text for testing!!!";
                    lang4.TextStrings[4] = "Really long loading text for testing!!!!";
                    FrontendLanguage lang5 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("loading").Children[5];
                    lang5.TextStrings[1] = "Really long loading text for testing!";
                    lang5.TextStrings[2] = "Really long loading text for testing!!";
                    lang5.TextStrings[3] = "Really long loading text for testing!!!";
                    lang5.TextStrings[4] = "Really long loading text for testing!!!!";
                }
            }

            if (targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend") != null)
            {
                chunkPos = targetFile.RootChunk.GetChildIndexByName<FrontendLanguage>("frontend");
                FrontendLanguage lang6 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[0];
                lang6.TextStrings[171] = "Really long loading text for testing!";
                if (targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children.Count > 1)
                {
                    FrontendLanguage lang7 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[1];
                    lang7.TextStrings[171] = "Really long loading text for testing!";
                    FrontendLanguage lang8 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[2];
                    lang8.TextStrings[171] = "Really long loading text for testing!";
                    FrontendLanguage lang9 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[3];
                    lang9.TextStrings[171] = "Really long loading text for testing!";
                    FrontendLanguage lang11 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[4];
                    lang11.TextStrings[171] = "Really long loading text for testing!";
                    FrontendLanguage lang12 = (FrontendLanguage)targetFile.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children[5];
                    lang12.TextStrings[171] = "Really long loading text for testing!";
                }
            }

            targetFile.Save(path_extr + file_name + "1");
            System.IO.File.Delete(path_extr + file_name);
            System.IO.File.Move(path_extr + file_name + "1", path_extr + file_name);

        }
    }
}
