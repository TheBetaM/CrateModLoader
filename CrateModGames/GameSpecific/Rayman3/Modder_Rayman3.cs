using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public enum R3_ModProps : int
    {
        Options = 0,
        Textures_General,
        Textures_Menu,
        Textures_Loading,
    }

    public sealed class Modder_Rayman3 : Modder
    {
        public override bool CanPreloadGame => true;
        public override List<ConsoleMode> PreloadConsoles => new List<ConsoleMode>() { ConsoleMode.GCN, };

        public Modder_Rayman3() { }

        internal string basePath = "";

        public Random randState = new Random();

        public override void StartModProcess()
        {

            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                basePath = ConsolePipeline.ExtractedPath + @"DATABIN\";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                basePath = ConsolePipeline.ExtractedPath + @"GAMEDATABIN\";
            }
            else if (ConsolePipeline.Metadata.Console  == ConsoleMode.XBOX)
            {
                basePath = ConsolePipeline.ExtractedPath + @"gamedatabin\";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PC)
            {
                basePath = ConsolePipeline.ExtractedPath + @"Gamedatabin\";
            }

            randState = new Random(ModLoaderGlobals.RandomizerSeed);

            if (Rayman3_Props_Main.Option_RandLevelOrderAll.Enabled)
            {
                Randomize_Level_Order(false);
            }
            else if (Rayman3_Props_Main.Option_RandLevelOrder.Enabled)
            {
                Randomize_Level_Order(true);
            }

            if (Rayman3_Props_Main.Option_RemoveIntroVideos.Enabled)
            {
                Remove_Intro_Videos();
            }

            Custom_Texture_Handle();

            if (Rayman3_Props_Main.Option_RandCopterColors.Enabled)
                Rand_Copter_Colors(randState);
            if (Rayman3_Props_Main.Option_RandHUDColors.Enabled)
                Rand_HUD_Colors(randState);
            if (Rayman3_Props_Main.Option_RandOutfitColors.Enabled)
                Rand_Outfit_Colors(randState);

            if (Rayman3_Props_Main.Option_RandWorldColors.Enabled)
            {
                DirectoryInfo di = new DirectoryInfo(basePath);
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    Recursive_WorldColor(dir, randState);
                }
            }

            if (Rayman3_Props_Main.Option_NewGameNightmare.Enabled)
            {
                Replace_Intro_With_Nightmare();
            }

            Recursive_PostCleanup(new DirectoryInfo(ConsolePipeline.ExtractedPath));

            Add_CMl_Metadata();
        }

        void Add_CMl_Metadata()
        {
            //todo: fix.lvl, texts on PS2
        }

        void Replace_Intro_With_Nightmare()
        {
            string sourceLevel = "toudi_10";
            string targetLevel = "intro_10";
            string tempLevel = "intro_11";
            if (Directory.Exists(basePath + targetLevel + @"\"))
            {
                Directory.Move(basePath + targetLevel + @"\", basePath + tempLevel + @"\");
            }
            if (Directory.Exists(basePath + sourceLevel + @"\"))
            {
                Directory.Move(basePath + sourceLevel + @"\", basePath + targetLevel + @"\");

                File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Lvl.tpl", basePath + targetLevel + @"\" + targetLevel + "_Lvl.tpl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Trans.tpl", basePath + targetLevel + @"\" + targetLevel + "_Trans.tpl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.lvl", basePath + targetLevel + @"\" + targetLevel + "_vb.lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.ptr", basePath + targetLevel + @"\" + targetLevel + "_vb.ptr");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.lvl", basePath + targetLevel + @"\" + targetLevel + "kf.lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.ptr", basePath + targetLevel + @"\" + targetLevel + "kf.ptr");

                File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + "1.bnh");
                File.Move(basePath + @"World\Sound\" + targetLevel + ".hxg", basePath + @"World\Sound\" + targetLevel + "1.hxg");
                File.Move(basePath + @"World\Sound\" + sourceLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                File.Move(basePath + @"World\Sound\" + sourceLevel + ".hxg", basePath + @"World\Sound\" + targetLevel + ".hxg");
                File.Move(basePath + @"World\Sound\" + targetLevel + "1.bnh", basePath + @"World\Sound\" + sourceLevel + ".bnh");
                File.Move(basePath + @"World\Sound\" + targetLevel + "1.hxg", basePath + @"World\Sound\" + sourceLevel + ".hxg");
            }
        }

        void Randomize_Level_Order(bool anyLevels)
        {
            int minLevel = 1;
            int maxLevel = 46; //46
            int targetPos = 0;
            List<int> ValidLevels = new List<int>();
            List<int> LevelsReplacing = new List<int>();
            string sourceLevel, targetLevel;

            if (anyLevels)
            {
                minLevel = 0;
                maxLevel = 46;
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    ValidLevels.Add(i);
                }
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    targetPos = randState.Next(0, ValidLevels.Count);
                    LevelsReplacing.Add(ValidLevels[targetPos]);
                    ValidLevels.RemoveAt(targetPos);
                }
            }
            else
            {
                LevelsReplacing.Add(0);
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    LevelsReplacing.Add(-1);
                }
                List<int> HubSections = new List<int>()
                {
                    5,
                    6,
                    6,
                    6,
                    8,
                    3,
                    3,
                    5,
                    4,
                };
                int currentPos = 1;
                int lastLevel = 0;
                int currentSection = 0;

                while (currentSection < HubSections.Count)
                {

                    int firstLevel = minLevel;
                    if (currentSection > 0)
                    {
                        for (int i = 0; i < currentSection; i++)
                        {
                            firstLevel += HubSections[i];
                        }
                    }
                    currentPos = firstLevel;
                    int endLevel = firstLevel + HubSections[currentSection] - 1;
                    for (int i = firstLevel; i < endLevel; i++)
                    {
                        ValidLevels.Add(i);
                    }
                    while (ValidLevels.Count > 0)
                    {
                        targetPos = randState.Next(0, ValidLevels.Count);
                        LevelsReplacing[currentPos] = ValidLevels[targetPos];
                        Console.WriteLine("Level " + currentPos + ": " + (LevelID)LevelsReplacing[currentPos] + " (" + LevelsReplacing[currentPos] + ")");
                        currentPos = ValidLevels[targetPos] + 1;
                        //currentPos++;
                        lastLevel = ValidLevels[targetPos];
                        ValidLevels.RemoveAt(targetPos);
                    }
                    currentSection++;

                    currentPos = lastLevel + 1;
                    LevelsReplacing[currentPos] = endLevel;
                    Console.WriteLine("Level " + currentPos + ": " + (LevelID)LevelsReplacing[currentPos] + " (" + LevelsReplacing[currentPos] + ")");
                }

                //currentPos = lastLevel + 1;
                
                Console.WriteLine("");
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    Console.WriteLine("Order " + i + ": " + (LevelID)LevelsReplacing[i]);
                }
                
            }

            for (int i = minLevel; i <= maxLevel; i++)
            {
                targetLevel = LevelNames[i];
                if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
                {
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + targetLevel + @"\"))
                {
                    Directory.Move(basePath + targetLevel + @"\", basePath + "level" + i + @"\");
                }
                if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
                {
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1", basePath + @"WORLD\SOUND\" + "level" + i + ".BNH;1");
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1", basePath + @"WORLD\SOUND\" + "level" + i + ".HX2;1");
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1", basePath + @"WORLD\SOUND\" + "level" + i + ".SMT;1");
                }
                else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".hxg", basePath + @"World\Sound\" + "level" + i + ".hxg");
                }
                else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".hxx", basePath + @"World\Sound\" + "level" + i + ".hxx");
                }
                else if (ConsolePipeline.Metadata.Console == ConsoleMode.PC)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".HXC", basePath + @"World\Sound\" + "level" + i + ".HXC");
                }
            }
            for (int i = minLevel; i <= maxLevel; i++)
            {
                sourceLevel = LevelNames[i];
                targetLevel = LevelNames[LevelsReplacing[i]];
                if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
                {
                    sourceLevel = sourceLevel.ToUpper();
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + "level" + i + @"\"))
                {
                    Directory.Move(basePath + "level" + i + @"\", basePath + targetLevel + @"\");

                    if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".LVL;1", basePath + targetLevel + @"\" + targetLevel + ".LVL;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".PTR;1", basePath + targetLevel + @"\" + targetLevel + ".PTR;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".TBF;1", basePath + targetLevel + @"\" + targetLevel + ".TBF;1");

                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".BNH;1", basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1");
                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".HX2;1", basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1");
                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".SMT;1", basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1");
                    }
                    else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Lvl.tpl", basePath + targetLevel + @"\" + targetLevel + "_Lvl.tpl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Trans.tpl", basePath + targetLevel + @"\" + targetLevel + "_Trans.tpl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.lvl", basePath + targetLevel + @"\" + targetLevel + "_vb.lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.ptr", basePath + targetLevel + @"\" + targetLevel + "_vb.ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.lvl", basePath + targetLevel + @"\" + targetLevel + "kf.lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.ptr", basePath + targetLevel + @"\" + targetLevel + "kf.ptr");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".hxg", basePath + @"World\Sound\" + targetLevel + ".hxg");
                    }
                    else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bhf", basePath + targetLevel + @"\" + targetLevel + ".bhf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bif", basePath + targetLevel + @"\" + targetLevel + ".bif");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bsf", basePath + targetLevel + @"\" + targetLevel + ".bsf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".btf", basePath + targetLevel + @"\" + targetLevel + ".btf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bvf", basePath + targetLevel + @"\" + targetLevel + ".bvf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".spc", basePath + targetLevel + @"\" + targetLevel + ".spc");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".hxx", basePath + @"World\Sound\" + targetLevel + ".hxx");
                    }
                    else if (ConsolePipeline.Metadata.Console == ConsoleMode.PC)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".HXC", basePath + @"World\Sound\" + targetLevel + ".HXC");
                    }
                }
            }

        }

        void Custom_Texture_Handle()
        {
            if (File.Exists(basePath + @"fix.tpl"))
            {
                GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                Rayman3_Textures_General.Texture_General_Fist_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm4.png");
                Rayman3_Textures_General.Texture_General_Outfit_ThrottleCopter.ResourceToFile(basePath + @"fix.tpl.mm6.png");
                Rayman3_Textures_General.Texture_General_Outfit_Normal.ResourceToFile(basePath + @"fix.tpl.mm7.png");
                Rayman3_Textures_General.Texture_General_Outfit_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm8.png");
                Rayman3_Textures_General.Texture_General_Outfit_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm9.png");
                Rayman3_Textures_General.Texture_General_Outfit_Vortex.ResourceToFile(basePath + @"fix.tpl.mm10.png");
                Rayman3_Textures_General.Texture_General_Outfit_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm11.png");
                Rayman3_Textures_General.Texture_General_Fist_Vortex.ResourceToFile(basePath + @"fix.tpl.mm13.png");
                Rayman3_Textures_General.Texture_General_Copter_Normal.ResourceToFile(basePath + @"fix.tpl.mm15.png");
                Rayman3_Textures_General.Texture_General_Copter_LockJaw.ResourceToFile(basePath + @"fix.tpl.mm17.png");
                Rayman3_Textures_General.Texture_General_Copter_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm16.png");
                Rayman3_Textures_General.Texture_General_Copter_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm18.png");
                Rayman3_Textures_General.Texture_General_Copter_Vortex.ResourceToFile(basePath + @"fix.tpl.mm19.png");
                Rayman3_Textures_General.Texture_General_Fist_ShockRocket.ResourceToFile(basePath + @"fix.tpl.mm20.png");
                Rayman3_Textures_General.Texture_General_Copter_ThrottleCopter.ResourceToFile(basePath + @"fix.tpl.mm21.png");
                Rayman3_Textures_General.Texture_General_Fist_HeavyMetalFist.ResourceToFile(basePath + @"fix.tpl.mm22.png");
                Rayman3_Textures_General.Texture_General_RedLum01.ResourceToFile(basePath + @"fix.tpl.mm24.png");
                Rayman3_Textures_General.Texture_General_RedLum02.ResourceToFile(basePath + @"fix.tpl.mm25.png");
                Rayman3_Textures_General.Texture_General_RedLum03.ResourceToFile(basePath + @"fix.tpl.mm26.png");
                Rayman3_Textures_General.Texture_General_RedLum04.ResourceToFile(basePath + @"fix.tpl.mm23.png");
                Rayman3_Textures_General.Texture_General_GameOver.ResourceToFile(basePath + @"fix.tpl.mm27.png");
                Rayman3_Textures_General.Texture_General_HUDElements.ResourceToFile(basePath + @"fix.tpl.mm29.png");
                Rayman3_Textures_General.Texture_General_AimingNear.ResourceToFile(basePath + @"fix.tpl.mm39.png");
                Rayman3_Textures_General.Texture_General_AimingArrow01.ResourceToFile(basePath + @"fix.tpl.mm40.png");
                Rayman3_Textures_General.Texture_General_AimingArrow02.ResourceToFile(basePath + @"fix.tpl.mm41.png");
                Rayman3_Textures_General.Texture_General_AimingArrow03.ResourceToFile(basePath + @"fix.tpl.mm42.png");
                Rayman3_Textures_General.Texture_General_AimingFar.ResourceToFile(basePath + @"fix.tpl.mm43.png");
                Rayman3_Textures_General.Texture_General_ResultStarOff.ResourceToFile(basePath + @"fix.tpl.mm46.png");
                Rayman3_Textures_General.Texture_General_GradientBG.ResourceToFile(basePath + @"fix.tpl.mm47.png");
                Rayman3_Textures_General.Texture_General_ResultMurfy.ResourceToFile(basePath + @"fix.tpl.mm48.png");
                Rayman3_Textures_General.Texture_General_ResultStarOn.ResourceToFile(basePath + @"fix.tpl.mm49.png");
                Rayman3_Textures_General.Texture_General_TeensieUnlocked.ResourceToFile(basePath + @"fix.tpl.mm53.png");
                Rayman3_Textures_General.Texture_General_TeensieLocked.ResourceToFile(basePath + @"fix.tpl.mm54.png");
                Rayman3_Textures_General.Texture_General_TeensieBG.ResourceToFile(basePath + @"fix.tpl.mm55.png");
                Rayman3_Textures_General.Texture_General_ScoreDisplay.ResourceToFile(basePath + @"fix.tpl.mm56.png");
                Rayman3_Textures_General.Texture_General_Arrow.ResourceToFile(basePath + @"fix.tpl.mm62.png");
                Rayman3_Textures_General.Texture_General_Damage01.ResourceToFile(basePath + @"fix.tpl.mm64.png");
                Rayman3_Textures_General.Texture_General_Damage02.ResourceToFile(basePath + @"fix.tpl.mm66.png");
                Rayman3_Textures_General.Texture_General_Damage03.ResourceToFile(basePath + @"fix.tpl.mm68.png");
                Rayman3_Textures_General.Texture_General_Damage04.ResourceToFile(basePath + @"fix.tpl.mm70.png");
                Rayman3_Textures_General.Texture_General_ComboScores.ResourceToFile(basePath + @"fix.tpl.mm71.png");
                Rayman3_Textures_General.Texture_General_Popup01.ResourceToFile(basePath + @"fix.tpl.mm72.png");
                Rayman3_Textures_General.Texture_General_Popup02.ResourceToFile(basePath + @"fix.tpl.mm73.png");
                Rayman3_Textures_General.Texture_General_Popup03.ResourceToFile(basePath + @"fix.tpl.mm74.png");
                Rayman3_Textures_General.Texture_General_Font.ResourceToFile(basePath + @"fix.tpl.png");

                if (Rayman3_Props_Main.Option_RandOutfitColors.Enabled)
                {
                    //Shuffle outfit textures
                    string[] outfitFiles = new string[] { "fix.tpl.mm7.png", "fix.tpl.mm8.png", "fix.tpl.mm9.png", "fix.tpl.mm10.png", "fix.tpl.mm11.png" };
                    File.Move(basePath + outfitFiles[0], basePath + "outfit0.png");
                    File.Move(basePath + outfitFiles[1], basePath + "outfit1.png");
                    File.Move(basePath + outfitFiles[2], basePath + "outfit2.png");
                    File.Move(basePath + outfitFiles[3], basePath + "outfit3.png");
                    File.Move(basePath + outfitFiles[4], basePath + "outfit4.png");
                    string[] copterFiles = new string[] { "fix.tpl.mm15.png", "fix.tpl.mm18.png", "fix.tpl.mm16.png", "fix.tpl.mm19.png", "fix.tpl.mm17.png" };
                    File.Move(basePath + copterFiles[0], basePath + "copter0.png");
                    File.Move(basePath + copterFiles[1], basePath + "copter1.png");
                    File.Move(basePath + copterFiles[2], basePath + "copter2.png");
                    File.Move(basePath + copterFiles[3], basePath + "copter3.png");
                    File.Move(basePath + copterFiles[4], basePath + "copter4.png");

                    List<int> RandList = new List<int>();
                    for (int i = 0; i < 5; i++)
                    {
                        RandList.Add(i);
                    }
                    int targetpos = 0;

                    for (int i = 0; i < 5; i++)
                    {
                        targetpos = randState.Next(0, RandList.Count);
                        File.Move(basePath + "outfit" + RandList[targetpos] + ".png", basePath + outfitFiles[i]);
                        File.Move(basePath + "copter" + RandList[targetpos] + ".png", basePath + copterFiles[i]);
                        RandList.RemoveAt(targetpos);
                    }
                }



                GCN_ImportTextures(basePath + @"fix.tpl.png");
            }
            else if (File.Exists(basePath + @"FIX.TBF"))
            {
                //Use TBF Tool
            }

            if (File.Exists(basePath + @"menu.tpl"))
            {

                GCN_ExportTextures(basePath + @"menu.tpl");
                File.Delete(basePath + @"menu.tpl");

                Rayman3_Textures_Menu.Texture_Menu_Overlay.ResourceToFile(basePath + @"menu.tpl.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons.ResourceToFile(basePath + @"menu.tpl.mm1.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level1.ResourceToFile(basePath + @"menu.tpl.mm2.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level2.ResourceToFile(basePath + @"menu.tpl.mm3.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level3.ResourceToFile(basePath + @"menu.tpl.mm4.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level4.ResourceToFile(basePath + @"menu.tpl.mm5.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level5.ResourceToFile(basePath + @"menu.tpl.mm6.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level6.ResourceToFile(basePath + @"menu.tpl.mm7.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level7.ResourceToFile(basePath + @"menu.tpl.mm8.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level8.ResourceToFile(basePath + @"menu.tpl.mm9.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level9.ResourceToFile(basePath + @"menu.tpl.mm10.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Lum.ResourceToFile(basePath + @"menu.tpl.mm11.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Options.ResourceToFile(basePath + @"menu.tpl.mm12.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Rayman.ResourceToFile(basePath + @"menu.tpl.mm13.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Murfy.ResourceToFile(basePath + @"menu.tpl.mm14.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Videos.ResourceToFile(basePath + @"menu.tpl.mm15.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Camera.ResourceToFile(basePath + @"menu.tpl.mm16.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Plum.ResourceToFile(basePath + @"menu.tpl.mm17.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Videos.ResourceToFile(basePath + @"menu.tpl.mm18.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Misc.ResourceToFile(basePath + @"menu.tpl.mm19.png");

                GCN_ImportTextures(basePath + @"menu.tpl.png");
            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                string lsbinpath = ConsolePipeline.ExtractedPath + @"LSBIN\";
                if (File.Exists(lsbinpath + @"lodmeca.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                    File.Delete(lsbinpath + @"lodmeca.tpl");
                    Rayman3_Textures_Loading.Texture_Load_Gear.ResourceToFile(lsbinpath + @"lodmeca.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodmeca.tpl.png");
                }
                if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodps2_01.tpl");
                    File.Delete(lsbinpath + @"lodps2_01.tpl");
                    Rayman3_Textures_Loading.Texture_Load_01.ResourceToFile(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_02.tpl");
                    File.Delete(lsbinpath + @"lodps2_02.tpl");
                    Rayman3_Textures_Loading.Texture_Load_02.ResourceToFile(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_03.tpl");
                    File.Delete(lsbinpath + @"lodps2_03.tpl");
                    Rayman3_Textures_Loading.Texture_Load_03.ResourceToFile(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_04.tpl");
                    File.Delete(lsbinpath + @"lodps2_04.tpl");
                    Rayman3_Textures_Loading.Texture_Load_04.ResourceToFile(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_05.tpl");
                    File.Delete(lsbinpath + @"lodps2_05.tpl");
                    Rayman3_Textures_Loading.Texture_Load_05.ResourceToFile(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_06.tpl");
                    File.Delete(lsbinpath + @"lodps2_06.tpl");
                    Rayman3_Textures_Loading.Texture_Load_06.ResourceToFile(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_07.tpl");
                    File.Delete(lsbinpath + @"lodps2_07.tpl");
                    Rayman3_Textures_Loading.Texture_Load_07.ResourceToFile(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_08.tpl");
                    File.Delete(lsbinpath + @"lodps2_08.tpl");
                    Rayman3_Textures_Loading.Texture_Load_08.ResourceToFile(lsbinpath + @"lodps2_08.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_08.tpl.png");
                }
            }
        }

        void Rand_HUD_Colors(Random rand)
        {
            if (File.Exists(basePath + @"fix.tpl"))
            {
                GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<List<string>> HUDTex = new List<List<string>>()
                {
                    new List<string>() {
                        "fix.tpl.mm27",
                    },
                    new List<string>() {
                        "fix.tpl.mm29",
                    },
                    new List<string>() {
                        "fix.tpl.mm39",
                        "fix.tpl.mm40",
                        "fix.tpl.mm41",
                        "fix.tpl.mm42",
                    },
                    new List<string>() {
                        "fix.tpl.mm43",
                    },
                    new List<string>() {
                        "fix.tpl.mm47",
                    },
                    new List<string>() {
                        "fix.tpl.mm46",
                        "fix.tpl.mm49",
                        "fix.tpl.mm53",
                        "fix.tpl.mm54",
                        "fix.tpl.mm55",
                    },
                    new List<string>() {
                        "fix.tpl.mm56",
                    },
                    new List<string>() {
                        "fix.tpl.mm71",
                    },
                    new List<string>() {
                        "fix.tpl.mm72",
                        "fix.tpl.mm73",
                        "fix.tpl.mm74",
                    },
                };

                for (int i = 0; i < HUDTex.Count; i++)
                {
                    //Color targetColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    for (int t = 0; t < HUDTex[i].Count; t++)
                    {
                        Recolor_Texture_File(basePath + HUDTex[i][t] + ".png", Swiz);
                    }
                }

                GCN_ImportTextures(basePath + @"fix.tpl.png");
            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                string lsbinpath = ConsolePipeline.ExtractedPath + @"LSBIN\";
                if (File.Exists(lsbinpath + @"lodmeca.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                    File.Delete(lsbinpath + @"lodmeca.tpl");

                    //Color GearColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Recolor_Texture_File(lsbinpath + @"lodmeca.tpl.png", Swiz);

                    GCN_ImportTextures(lsbinpath + @"lodmeca.tpl.png");
                }
                if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
                {
                    for (int i = 1; i < 9; i++)
                    {
                        //Color LoadColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                        ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                        string filePath = lsbinpath + @"lodps2_0" + i + ".tpl";
                        string pngPath = lsbinpath + @"lodps2_0" + i + ".tpl.png";

                        GCN_ExportTextures(filePath);
                        File.Delete(filePath);

                        Recolor_Texture_File(pngPath, Swiz);

                        GCN_ImportTextures(pngPath);
                    }
                }
            }
        }

        void Rand_Copter_Colors(Random rand)
        {
            if (File.Exists(basePath + @"fix.tpl"))
            {
                GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<string> CopterTex = new List<string>()
                {
                    "fix.tpl.mm15",
                    "fix.tpl.mm16",
                    "fix.tpl.mm17",
                    "fix.tpl.mm18",
                    "fix.tpl.mm19",
                };

                foreach (string fileName in CopterTex)
                {
                    //Color CopterColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Recolor_Texture_File(basePath + fileName + ".png", Swiz);
                }

                GCN_ImportTextures(basePath + @"fix.tpl.png");
            }
        }

        void Rand_Outfit_Colors(Random rand)
        {
            if (File.Exists(basePath + @"fix.tpl"))
            {
                GCN_ExportTextures(basePath + @"fix.tpl");

                File.Delete(basePath + @"fix.tpl");

                List<string> OutfitTex = new List<string>()
                {
                    "fix.tpl.mm4",
                    "fix.tpl.mm6",
                    "fix.tpl.mm7",
                    "fix.tpl.mm8",
                    "fix.tpl.mm9",
                    "fix.tpl.mm10",
                    "fix.tpl.mm11",
                    "fix.tpl.mm13",
                    "fix.tpl.mm20",
                    "fix.tpl.mm21",
                    "fix.tpl.mm22",
                    "fix.tpl.mm57",
                    "fix.tpl.mm58",
                    "fix.tpl.mm59",
                    "fix.tpl.mm60",
                    "fix.tpl.mm61",
                    "fix.tpl.mm63",
                };

                foreach (string fileName in OutfitTex)
                {
                    //Color OutfitColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Recolor_Texture_File(basePath + fileName + ".png", Swiz);
                }

                GCN_ImportTextures(basePath + @"fix.tpl.png");
            }
        }

        void Rand_World_Colors(DirectoryInfo di, string filePath, Random rand)
        {
            GCN_ExportTextures(filePath, true);

            File.Delete(filePath);

            DirectoryInfo dir = new DirectoryInfo(di.FullName);

            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToLower().Contains("png"))
                {
                    //Color CopterColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    ColorSwizzleData Swiz = new ColorSwizzleData(rand);

                    Recolor_Texture_File(file.FullName, Swiz);
                }
                
            }

            GCN_ImportTextures(filePath + ".png");
        }

        void Recursive_WorldColor(DirectoryInfo di, Random rand)
        {
            foreach(DirectoryInfo dir in di.GetDirectories())
            {
                Recursive_WorldColor(dir, rand);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToLower().Contains("tpl"))
                {
                    Rand_World_Colors(di, file.FullName, rand);
                }
            }
        }


        void Recolor_Texture_File(string filePath, ColorSwizzleData Swiz)
        {
            Bitmap temp = new Bitmap(filePath);
            Bitmap tex = new Bitmap(temp);
            temp.Dispose();
            File.Delete(filePath);

            for (int x = 0; x < tex.Width; x++)
            {
                for (int y = 0; y < tex.Height; y++)
                {
                    Color sourceColor = tex.GetPixel(x, y);
                    float intensity = Math.Max(sourceColor.R, sourceColor.G);
                    intensity = Math.Max(intensity, sourceColor.B);
                    intensity = intensity / 255f;

                    int r = sourceColor.R;
                    int g = sourceColor.G;
                    int b = sourceColor.B;

                    Color targetColor = Color.FromArgb(tex.GetPixel(x, y).A,
                        (int)((Swiz.r_r * r + Swiz.r_g * g + Swiz.r_b * b) / Swiz.r_s),
                        (int)((Swiz.g_r * r + Swiz.g_g * g + Swiz.g_b * b) / Swiz.g_s),
                        (int)((Swiz.b_r * r + Swiz.b_g * g + Swiz.b_b * b) / Swiz.b_s));

                    tex.SetPixel(x, y, targetColor);
                }
            }

            tex.Save(filePath);
            tex.Dispose();
        }


        void Remove_Intro_Videos()
        {
            if (Directory.Exists(basePath + "videos"))
            {
                if (File.Exists(basePath + @"videos\trailer.h4m"))
                {
                    File.Delete(basePath + @"videos\trailer.h4m");
                }
            }
        }

        void Recursive_PostCleanup(DirectoryInfo di)
        {
            foreach(DirectoryInfo dir in di.GetDirectories())
            {
                Recursive_PostCleanup(dir);
                PostCleanup(di);
            }
        }

        void PostCleanup(DirectoryInfo di)
        {
            foreach(FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToLower().Contains("png"))
                {
                    file.Delete();
                }
            }
        }


        public override void StartPreload()
        {
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                basePath = ConsolePipeline.ExtractedPath + @"DATABIN\";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                basePath = ConsolePipeline.ExtractedPath + @"GAMEDATABIN\";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                basePath = ConsolePipeline.ExtractedPath + @"gamedatabin\";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PC)
            {
                basePath = ConsolePipeline.ExtractedPath + @"Gamedatabin\";
            }

            if (File.Exists(basePath + @"fix.tpl"))
            {

                GCN_ExportTextures(basePath + @"fix.tpl");
                File.Delete(basePath + @"fix.tpl");

                Rayman3_Textures_General.Texture_General_Fist_LockJaw.FileToResource(basePath + @"fix.tpl.mm4.png");
                Rayman3_Textures_General.Texture_General_Outfit_ThrottleCopter.FileToResource(basePath + @"fix.tpl.mm6.png");
                Rayman3_Textures_General.Texture_General_Outfit_Normal.FileToResource(basePath + @"fix.tpl.mm7.png");
                Rayman3_Textures_General.Texture_General_Outfit_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm8.png");
                Rayman3_Textures_General.Texture_General_Outfit_LockJaw.FileToResource(basePath + @"fix.tpl.mm9.png");
                Rayman3_Textures_General.Texture_General_Outfit_Vortex.FileToResource(basePath + @"fix.tpl.mm10.png");
                Rayman3_Textures_General.Texture_General_Outfit_ShockRocket.FileToResource(basePath + @"fix.tpl.mm11.png");
                Rayman3_Textures_General.Texture_General_Fist_Vortex.FileToResource(basePath + @"fix.tpl.mm13.png");
                Rayman3_Textures_General.Texture_General_Copter_Normal.FileToResource(basePath + @"fix.tpl.mm15.png");
                Rayman3_Textures_General.Texture_General_Copter_LockJaw.FileToResource(basePath + @"fix.tpl.mm17.png");
                Rayman3_Textures_General.Texture_General_Copter_ShockRocket.FileToResource(basePath + @"fix.tpl.mm16.png");
                Rayman3_Textures_General.Texture_General_Copter_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm18.png");
                Rayman3_Textures_General.Texture_General_Copter_Vortex.FileToResource(basePath + @"fix.tpl.mm19.png");
                Rayman3_Textures_General.Texture_General_Fist_ShockRocket.FileToResource(basePath + @"fix.tpl.mm20.png");
                Rayman3_Textures_General.Texture_General_Copter_ThrottleCopter.FileToResource(basePath + @"fix.tpl.mm21.png");
                Rayman3_Textures_General.Texture_General_Fist_HeavyMetalFist.FileToResource(basePath + @"fix.tpl.mm22.png");
                Rayman3_Textures_General.Texture_General_RedLum01.FileToResource(basePath + @"fix.tpl.mm24.png");
                Rayman3_Textures_General.Texture_General_RedLum02.FileToResource(basePath + @"fix.tpl.mm25.png");
                Rayman3_Textures_General.Texture_General_RedLum03.FileToResource(basePath + @"fix.tpl.mm26.png");
                Rayman3_Textures_General.Texture_General_RedLum04.FileToResource(basePath + @"fix.tpl.mm23.png");
                Rayman3_Textures_General.Texture_General_GameOver.FileToResource(basePath + @"fix.tpl.mm27.png");
                Rayman3_Textures_General.Texture_General_HUDElements.FileToResource(basePath + @"fix.tpl.mm29.png");
                Rayman3_Textures_General.Texture_General_AimingNear.FileToResource(basePath + @"fix.tpl.mm39.png");
                Rayman3_Textures_General.Texture_General_AimingArrow01.FileToResource(basePath + @"fix.tpl.mm40.png");
                Rayman3_Textures_General.Texture_General_AimingArrow02.FileToResource(basePath + @"fix.tpl.mm41.png");
                Rayman3_Textures_General.Texture_General_AimingArrow03.FileToResource(basePath + @"fix.tpl.mm42.png");
                Rayman3_Textures_General.Texture_General_AimingFar.FileToResource(basePath + @"fix.tpl.mm43.png");
                Rayman3_Textures_General.Texture_General_ResultStarOff.FileToResource(basePath + @"fix.tpl.mm46.png");
                Rayman3_Textures_General.Texture_General_GradientBG.FileToResource(basePath + @"fix.tpl.mm47.png");
                Rayman3_Textures_General.Texture_General_ResultMurfy.FileToResource(basePath + @"fix.tpl.mm48.png");
                Rayman3_Textures_General.Texture_General_ResultStarOn.FileToResource(basePath + @"fix.tpl.mm49.png");
                Rayman3_Textures_General.Texture_General_TeensieUnlocked.FileToResource(basePath + @"fix.tpl.mm53.png");
                Rayman3_Textures_General.Texture_General_TeensieLocked.FileToResource(basePath + @"fix.tpl.mm54.png");
                Rayman3_Textures_General.Texture_General_TeensieBG.FileToResource(basePath + @"fix.tpl.mm55.png");
                Rayman3_Textures_General.Texture_General_ScoreDisplay.FileToResource(basePath + @"fix.tpl.mm56.png");
                Rayman3_Textures_General.Texture_General_Arrow.FileToResource(basePath + @"fix.tpl.mm62.png");
                Rayman3_Textures_General.Texture_General_Damage01.FileToResource(basePath + @"fix.tpl.mm64.png");
                Rayman3_Textures_General.Texture_General_Damage02.FileToResource(basePath + @"fix.tpl.mm66.png");
                Rayman3_Textures_General.Texture_General_Damage03.FileToResource(basePath + @"fix.tpl.mm68.png");
                Rayman3_Textures_General.Texture_General_Damage04.FileToResource(basePath + @"fix.tpl.mm70.png");
                Rayman3_Textures_General.Texture_General_ComboScores.FileToResource(basePath + @"fix.tpl.mm71.png");
                Rayman3_Textures_General.Texture_General_Popup01.FileToResource(basePath + @"fix.tpl.mm72.png");
                Rayman3_Textures_General.Texture_General_Popup02.FileToResource(basePath + @"fix.tpl.mm73.png");
                Rayman3_Textures_General.Texture_General_Popup03.FileToResource(basePath + @"fix.tpl.mm74.png");
                Rayman3_Textures_General.Texture_General_Font.FileToResource(basePath + @"fix.tpl.png");

            }
            else if (File.Exists(basePath + @"FIX.TBF"))
            {
                //Use TBF Tool
            }

            if (File.Exists(basePath + @"menu.tpl"))
            {

                GCN_ExportTextures(basePath + @"menu.tpl");
                File.Delete(basePath + @"menu.tpl");

                Rayman3_Textures_Menu.Texture_Menu_Overlay.FileToResource(basePath + @"menu.tpl.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons.FileToResource(basePath + @"menu.tpl.mm1.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level1.FileToResource(basePath + @"menu.tpl.mm2.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level2.FileToResource(basePath + @"menu.tpl.mm3.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level3.FileToResource(basePath + @"menu.tpl.mm4.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level4.FileToResource(basePath + @"menu.tpl.mm5.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level5.FileToResource(basePath + @"menu.tpl.mm6.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level6.FileToResource(basePath + @"menu.tpl.mm7.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level7.FileToResource(basePath + @"menu.tpl.mm8.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level8.FileToResource(basePath + @"menu.tpl.mm9.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Level9.FileToResource(basePath + @"menu.tpl.mm10.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Lum.FileToResource(basePath + @"menu.tpl.mm11.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Options.FileToResource(basePath + @"menu.tpl.mm12.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Rayman.FileToResource(basePath + @"menu.tpl.mm13.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Murfy.FileToResource(basePath + @"menu.tpl.mm14.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Videos.FileToResource(basePath + @"menu.tpl.mm15.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Camera.FileToResource(basePath + @"menu.tpl.mm16.png");
                Rayman3_Textures_Menu.Texture_Menu_Icon_Plum.FileToResource(basePath + @"menu.tpl.mm17.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Videos.FileToResource(basePath + @"menu.tpl.mm18.png");
                Rayman3_Textures_Menu.Texture_Menu_Icons_Misc.FileToResource(basePath + @"menu.tpl.mm19.png");

            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                string lsbinpath = ConsolePipeline.ExtractedPath + @"LSBIN\";
                if (File.Exists(lsbinpath + @"lodmeca.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                    File.Delete(lsbinpath + @"lodmeca.tpl");
                    Rayman3_Textures_Loading.Texture_Load_Gear.FileToResource(lsbinpath + @"lodmeca.tpl.png");
                }
                if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodps2_01.tpl");
                    File.Delete(lsbinpath + @"lodps2_01.tpl");
                    Rayman3_Textures_Loading.Texture_Load_01.FileToResource(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_02.tpl");
                    File.Delete(lsbinpath + @"lodps2_02.tpl");
                    Rayman3_Textures_Loading.Texture_Load_02.FileToResource(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_03.tpl");
                    File.Delete(lsbinpath + @"lodps2_03.tpl");
                    Rayman3_Textures_Loading.Texture_Load_03.FileToResource(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_04.tpl");
                    File.Delete(lsbinpath + @"lodps2_04.tpl");
                    Rayman3_Textures_Loading.Texture_Load_04.FileToResource(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_05.tpl");
                    File.Delete(lsbinpath + @"lodps2_05.tpl");
                    Rayman3_Textures_Loading.Texture_Load_05.FileToResource(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_06.tpl");
                    File.Delete(lsbinpath + @"lodps2_06.tpl");
                    Rayman3_Textures_Loading.Texture_Load_06.FileToResource(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_07.tpl");
                    File.Delete(lsbinpath + @"lodps2_07.tpl");
                    Rayman3_Textures_Loading.Texture_Load_07.FileToResource(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_08.tpl");
                    File.Delete(lsbinpath + @"lodps2_08.tpl");
                    Rayman3_Textures_Loading.Texture_Load_08.FileToResource(lsbinpath + @"lodps2_08.tpl.png");
                }
            }
        }

        void GCN_ExportTextures(string path, bool noCleanup = false)
        {
            if (!noCleanup)
            {
                Recursive_PostCleanup(new DirectoryInfo(ConsolePipeline.ExtractedPath));
            }

            string args = "decode ";
            args += path;

            Process wimgt = new Process();
            wimgt.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wimgt.exe";
            //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            wimgt.StartInfo.Arguments = args;
            wimgt.StartInfo.UseShellExecute = false;
            wimgt.StartInfo.RedirectStandardOutput = true;
            wimgt.StartInfo.CreateNoWindow = true;
            wimgt.Start();

            string outputMessage = wimgt.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            wimgt.WaitForExit();
        }
        void GCN_ImportTextures(string path)
        {
            string args = "encode ";
            args += path;

            Process wimgt = new Process();
            wimgt.StartInfo.FileName = ModLoaderGlobals.ToolsPath + @"wit\wimgt.exe";
            //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            wimgt.StartInfo.Arguments = args;
            wimgt.StartInfo.UseShellExecute = false;
            wimgt.StartInfo.RedirectStandardOutput = true;
            wimgt.StartInfo.CreateNoWindow = true;
            wimgt.Start();

            string outputMessage = wimgt.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            wimgt.WaitForExit();
        }

        class ColorSwizzleData
        {
            public int r_r;
            public int r_g;
            public int r_b;
            public int r_s;
            public int g_r;
            public int g_g;
            public int g_b;
            public int g_s;
            public int b_r;
            public int b_g;
            public int b_b;
            public int b_s;

            public ColorSwizzleData(Random rand)
            {
                r_r = rand.Next(2);
                r_g = rand.Next(2);
                r_b = rand.Next(2);
                r_s = r_r + r_g + r_b;
                g_r = rand.Next(2);
                g_g = rand.Next(2);
                g_b = rand.Next(2);
                g_s = g_r + g_g + g_b;
                b_r = rand.Next(2);
                b_g = rand.Next(2);
                b_b = rand.Next(2);
                b_s = b_r + b_g + b_b;

                if (r_s == 0) r_s = 1;
                if (g_s == 0) g_s = 1;
                if (b_s == 0) b_s = 1;
            }
        }

        public enum LevelID
        {
            Invalid = -1,
            Fairy1_Intro_10 = 0,
            Fairy2_Intro_15 = 1,
            Fairy3_Intro_17 = 2,
            Fairy4_Intro_20 = 3,
            Fairy5_Menu_00 = 4,
            Fairy6_Sk8_00 = 5,
            Forest1_Wood_11 = 6,
            Forest2_Wood_10 = 7,
            Forest3_Wood_19 = 8,
            Forest4_Wood_50 = 9,
            Forest5_Menu_10 = 10,
            Forest6_Sk8_10 = 11,
            Bog1_Swamp_60 = 12,
            Bog2_Swamp_82 = 13,
            Bog3_Swamp_81 = 14,
            Bog4_Swamp_83 = 15,
            Bog5_Swamp_50 = 16,
            Bog6_Swamp_51 = 17,
            LividDead1_Moor_00 = 18,
            LividDead2_Moor_30 = 19,
            LividDead3_Moor_60 = 20,
            LividDead4_Moor_19 = 21,
            LividDead5_Menu_20 = 22,
            LividDead6_Sk8_20 = 23,
            Desert1_Knaar_10 = 24,
            Desert2_Knaar_20 = 25,
            Desert3_Knaar_30 = 26,
            Desert4_Knaar_45 = 27,
            Desert5_Knaar_60 = 28,
            Desert6_Knaar_69 = 29,
            Desert7_Knaar_70 = 30,
            Desert8_Menu_30 = 31,
            Shortcut1_Flash_20 = 32,
            Shortcut2_Flash_30 = 33,
            Shortcut3_Flash_10 = 34,
            Summit1_Sea_10 = 35,
            Summit2_Mount_50 = 36,
            Summit3_Mount_4x = 37,
            Hoodlum1_Fact_40 = 38,
            Hoodlum2_Fact_50 = 39,
            Hoodlum3_Fact_55 = 40,
            Hoodlum4_Fact_34 = 41,
            Hoodlum5_Fact_22 = 42,
            Tower1_Tower_10 = 43,
            Tower2_Tower_20 = 44,
            Tower3_Tower_30 = 45,
            Tower4_Tower_40 = 46,
            Tower5_Lept_15 = 47,
        }

        public static string[] LevelNames = new string[]
        {
            "intro_10",
            "intro_15",
            "Intro_17",
            "intro_20",
            "menu_00",
            "sk8_00",
            "wood_11",
            "Wood_10",
            "Wood_19",
            "Wood_50",
            "menu_10",
            "Sk8_10",
            "Swamp_60",
            "Swamp_82",
            "Swamp_81",
            "swamp_83",
            "Swamp_50",
            "Swamp_51",
            "Moor_00",
            "Moor_30",
            "moor_60",
            "moor_19",
            "menu_20",
            "Sk8_20",
            "Knaar_10",
            "Knaar_20",
            "Knaar_30",
            "Knaar_45",
            "Knaar_60",
            "Knaar_69",
            "Knaar_70",
            "menu_30",
            "Flash_20",
            "Flash_30",
            "flash_10",
            "Sea_10",
            "mount_50",
            "mount_4x",
            "Fact_40",
            "Fact_50",
            "Fact_55",
            "fact_34",
            "Fact_22",
            "Tower_10",
            "Tower_20",
            "Tower_30",
            "Tower_40",
            "lept_15",
        };

    }
}
