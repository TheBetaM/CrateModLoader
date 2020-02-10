using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public sealed class Modder_Rayman3 : Modder
    {
        public Modder_Rayman3()
        {
            Game = new Game()
            {
                Name = "Rayman 3: Hoodlum Havoc",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.GCN,
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                    ConsoleMode.PC,
                },
                API_Credit = string.Empty, //"API based on Raymap by Droolie and Robin",
                API_Link = string.Empty,  //"https://github.com/byvar/raymap",
                Icon = Properties.Resources.icon_ray3,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = "SLUS_206.01",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_206.01",
                    CodeName = "SLUS_20601", },
                    new RegionCode() {
                    Name = "SLES_512.22",
                    Region = RegionType.PAL,
                    ExecName = "SLES_512.22",
                    CodeName = "SLES_51222", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GRHE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GRHP",
                    Region = RegionType.PAL },
                },
                RegionID_XBOX = new RegionCode[] {
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 3, },
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
                RegionID_PC = new RegionCode[] {
                    new RegionCode() {
                    Name = "Rayman3.exe",
                    Region = RegionType.Global },
                    new RegionCode() {
                    Name = "rayman3.exe",
                    Region = RegionType.Global },
                     new RegionCode() {
                    Name = "RAYMAN3.EXE",
                    Region = RegionType.Global },
                    new RegionCode() {
                    Name = "Rayman3.EXE",
                    Region = RegionType.Global },
                },
            };

            Options.Add(RandomizeLevelOrder, new ModOption("Randomize Level Order (All Levels)")); // todo
            Options.Add(RandomizeLevelOrderAlt, new ModOption("Randomize Level Order (Any amount of Levels)")); //todo: fix audio
            Options.Add(RandomizeOutfitColors, new ModOption("Randomize Outfit Visuals (GC Only)")); //todo
            Options.Add(NewGameNightmare, new ModOption("New Game Into 2D Nightmare (GC Only)"));
            Options.Add(RemoveIntroVideos, new ModOption("Remove Intro Video", true)); 

        }

        internal const int RandomizeLevelOrder = 0;
        internal const int RandomizeLevelOrderAlt = 1;
        internal const int RandomizeOutfitColors = 2;
        internal const int NewGameNightmare = 3;
        internal const int RemoveIntroVideos = 4;

        internal string basePath = "";

        public Random randState = new Random();

        public override void StartModProcess()
        {

            if (Program.ModProgram.isoType == ConsoleMode.PS2)
            {
                basePath = Program.ModProgram.extractedPath + @"DATABIN\";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.GCN)
            {
                basePath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\P-" + Program.ModProgram.ProductCode.Substring(0, 4) + @"\files\GAMEDATABIN\";
            }
            else if (Program.ModProgram.isoType  == ConsoleMode.XBOX)
            {
                basePath = Program.ModProgram.extractedPath + @"gamedatabin\";
            }
            else if (Program.ModProgram.isoType == ConsoleMode.PC)
            {
                basePath = Program.ModProgram.extractedPath + @"Gamedatabin\";
            }

            randState = new Random(Program.ModProgram.randoSeed);

            if (Options[RandomizeLevelOrder].Enabled)
            {
                Randomize_Level_Order(false);
            }
            else if (Options [RandomizeLevelOrderAlt].Enabled)
            {
                Randomize_Level_Order(true);
            }

            if (Options[RemoveIntroVideos].Enabled)
            {
                Remove_Intro_Videos();
            }

            if (Options[RandomizeOutfitColors].Enabled)
            {
                Randomize_Outfit_Colors();
            }

            if (Options[NewGameNightmare].Enabled)
            {
                Replace_Intro_With_Nightmare();
            }

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

        void Randomize_Level_Order(bool isAlt)
        {
            int minLevel = 0;
            int maxLevel = 45;
            int targetPos = 0;
            List<int> ValidLevels = new List<int>();
            List<int> LevelsReplacing = new List<int>();
            string sourceLevel, targetLevel;

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

            for (int i = minLevel; i <= maxLevel; i++)
            {
                targetLevel = LevelNames[i];
                if (Program.ModProgram.isoType == ConsoleMode.PS2)
                {
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + targetLevel + @"\"))
                {
                    Directory.Move(basePath + targetLevel + @"\", basePath + "level" + i + @"\");
                }
            }
            for (int i = minLevel; i <= maxLevel; i++)
            {
                sourceLevel = LevelNames[i];
                targetLevel = LevelNames[LevelsReplacing[i]];
                if (Program.ModProgram.isoType == ConsoleMode.PS2)
                {
                    sourceLevel = sourceLevel.ToUpper();
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + "level" + i + @"\"))
                {
                    Directory.Move(basePath + "level" + i + @"\", basePath + targetLevel + @"\");

                    if (Program.ModProgram.isoType == ConsoleMode.PS2)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".LVL;1", basePath + targetLevel + @"\" + targetLevel + ".LVL;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".PTR;1", basePath + targetLevel + @"\" + targetLevel + ".PTR;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".TBF;1", basePath + targetLevel + @"\" + targetLevel + ".TBF;1");

                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1", basePath + @"WORLD\SOUND\" + targetLevel + "1.BNH;1");
                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1", basePath + @"WORLD\SOUND\" + targetLevel + "1.HX2;1");
                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1", basePath + @"WORLD\SOUND\" + targetLevel + "1.SMT;1");
                        if (sourceLevel != targetLevel)
                        {
                            File.Move(basePath + @"WORLD\SOUND\" + sourceLevel + ".BNH;1", basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1");
                            File.Move(basePath + @"WORLD\SOUND\" + sourceLevel + ".HX2;1", basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1");
                            File.Move(basePath + @"WORLD\SOUND\" + sourceLevel + ".SMT;1", basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1");
                        }
                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + "1.BNH;1", basePath + @"WORLD\SOUND\" + sourceLevel + ".BNH;1");
                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + "1.HX2;1", basePath + @"WORLD\SOUND\" + sourceLevel + ".HX2;1");
                        File.Move(basePath + @"WORLD\SOUND\" + targetLevel + "1.SMT;1", basePath + @"WORLD\SOUND\" + sourceLevel + ".SMT;1");
                    }
                    else if (Program.ModProgram.isoType == ConsoleMode.GCN)
                    {
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
                        if (sourceLevel != targetLevel)
                        {
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".hxg", basePath + @"World\Sound\" + targetLevel + ".hxg");
                        }
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.bnh", basePath + @"World\Sound\" + sourceLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.hxg", basePath + @"World\Sound\" + sourceLevel + ".hxg");
                    }
                    else if (Program.ModProgram.isoType == ConsoleMode.XBOX)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bhf", basePath + targetLevel + @"\" + targetLevel + ".bhf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bif", basePath + targetLevel + @"\" + targetLevel + ".bif");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bsf", basePath + targetLevel + @"\" + targetLevel + ".bsf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".btf", basePath + targetLevel + @"\" + targetLevel + ".btf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bvf", basePath + targetLevel + @"\" + targetLevel + ".bvf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".spc", basePath + targetLevel + @"\" + targetLevel + ".spc");

                        File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + "1.bnh");
                        File.Move(basePath + @"World\Sound\" + targetLevel + ".hxx", basePath + @"World\Sound\" + targetLevel + "1.hxx");
                        if (sourceLevel != targetLevel)
                        {
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".hxx", basePath + @"World\Sound\" + targetLevel + ".hxx");
                        }
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.bnh", basePath + @"World\Sound\" + sourceLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.hxx", basePath + @"World\Sound\" + sourceLevel + ".hxx");
                    }
                    else if (Program.ModProgram.isoType == ConsoleMode.PC)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");

                        File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + "1.bnh");
                        File.Move(basePath + @"World\Sound\" + targetLevel + ".HXC", basePath + @"World\Sound\" + targetLevel + "1.HXC");
                        if (sourceLevel != targetLevel)
                        {
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                            File.Move(basePath + @"World\Sound\" + sourceLevel + ".HXC", basePath + @"World\Sound\" + targetLevel + ".HXC");
                        }
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.bnh", basePath + @"World\Sound\" + sourceLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + targetLevel + "1.HXC", basePath + @"World\Sound\" + sourceLevel + ".HXC");
                    }
                }
            }

        }

        void Randomize_Outfit_Colors()
        {
            if (File.Exists(basePath + @"fix.tpl"))
            {
                string args = "decode ";
                args += basePath + @"fix.tpl";

                Process wimgt = new Process();
                wimgt.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wimgt.exe";
                //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                wimgt.StartInfo.Arguments = args;
                wimgt.StartInfo.UseShellExecute = false;
                wimgt.StartInfo.RedirectStandardOutput = true;
                wimgt.StartInfo.CreateNoWindow = true;
                wimgt.Start();

                string outputMessage = wimgt.StandardOutput.ReadToEnd();
                //Console.WriteLine(outputMessage);

                wimgt.WaitForExit();

                File.Delete(basePath + @"fix.tpl");

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

                args = "encode ";
                args += basePath + @"fix.tpl.png";

                wimgt = new Process();
                wimgt.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "/Tools/wit/wimgt.exe";
                //wimgt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                wimgt.StartInfo.Arguments = args;
                wimgt.StartInfo.UseShellExecute = false;
                wimgt.StartInfo.RedirectStandardOutput = true;
                wimgt.StartInfo.CreateNoWindow = true;
                wimgt.Start();

                outputMessage = wimgt.StandardOutput.ReadToEnd();
                //Console.WriteLine(outputMessage);

                wimgt.WaitForExit();
            }
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

        public enum LevelID
        {
            Fairy1_Intro_10 = 0,
            Fairy2_Intro_15 = 1,
            Fairy3_Intro_17 = 2,
            Fairy4_Intro_20 = 3,
            Fairy5_Sk8_00 = 4,
            Forest1_Wood_11 = 5,
            Forest2_Wood_10 = 6,
            Forest3_Wood_19 = 7,
            Forest4_Wood_50 = 8,
            Forest5_Menu_10 = 9,
            Forest6_Sk8_10 = 10,
            Bog1_Swamp_60 = 11,
            Bog2_Swamp_82 = 12,
            Bog3_Swamp_81 = 13,
            Bog4_Swamp_83 = 14,
            Bog5_Swamp_50 = 15,
            Bog6_Swamp_51 = 16,
            LividDead1_Moor_00 = 17,
            LividDead2_Moor_30 = 18,
            LividDead3_Moor_60 = 19,
            LividDead4_Moor_19 = 20,
            LividDead5_Menu_20 = 21,
            LividDead6_Sk8_20 = 22,
            Desert1_Knaar_10 = 23,
            Desert2_Knaar_20 = 24,
            Desert3_Knaar_30 = 25,
            Desert4_Knaar_45 = 26,
            Desert5_Knaar_60 = 27,
            Desert6_Knaar_69 = 28,
            Desert7_Knaar_80 = 29,
            Desert8_Menu_30 = 30,
            Shortcut1_Flash_20 = 31,
            Shortcut2_Flash_30 = 32,
            Shortcut3_Flash_10 = 33,
            Summit1_Sea_10 = 34,
            Summit2_Mount_50 = 35,
            Summit3_Mount_4x = 36,
            Hoodlum1_Fact_40 = 37,
            Hoodlum2_Fact_50 = 38,
            Hoodlum3_Fact_55 = 39,
            Hoodlum4_Fact_34 = 40,
            Hoodlum5_Fact_22 = 41,
            Tower1_Tower_10 = 42,
            Tower2_Tower_20 = 43,
            Tower3_Tower_30 = 44,
            Tower4_Tower_40 = 45,
            Tower5_Lept_15 = 46,
        }

        public static string[] LevelNames = new string[]
        {
            "intro_10",
            "intro_15",
            "Intro_17",
            "intro_20",
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
