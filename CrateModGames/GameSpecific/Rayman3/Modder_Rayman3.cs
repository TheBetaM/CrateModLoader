using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.Rayman3;

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
        public override Game Game => new Game()
        {
            Name = Rayman3_Text.GameTitle,
            ShortName = "Rayman3",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.GCN,
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                    //ConsoleMode.PC,
                },
            API_Credit = string.Empty,
            API_Link = string.Empty,
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
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
                [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "GRHE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GRHP",
                    Region = RegionType.PAL },
                },
                [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 3, },
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
                [ConsoleMode.PC] = new RegionCode[]
                {
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
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
                [(int)R3_ModProps.Textures_Loading] = "Textures - Loading",
                [(int)R3_ModProps.Textures_Menu] = "Textures - Menu",
                [(int)R3_ModProps.Textures_General] = "Textures - General",
            },
        };
        public override bool CanPreloadGame => true;

        public static ModPropOption Option_RandLevelOrderAll = new ModPropOption(Rayman3_Text.Rand_LevelOrder2, Rayman3_Text.Rand_LevelOrder2Desc)
        { Hidden = true, };
        public static ModPropOption Option_RandLevelOrder = new ModPropOption(Rayman3_Text.Rand_LevelOrder, Rayman3_Text.Rand_LevelOrderDesc);
        public static ModPropOption Option_RandOutfitColors = new ModPropOption(Rayman3_Text.Rand_OutfitColors, Rayman3_Text.Rand_OutfitColorsDesc)
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        public static ModPropOption Option_NewGameNightmare = new ModPropOption(Rayman3_Text.Mod_NewGameNightmare, Rayman3_Text.Mod_NewGameNightmareDesc)
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        public static ModPropOption Option_RemoveIntroVideos = new ModPropOption(1, Rayman3_Text.Mod_RemoveIntroVideo, Rayman3_Text.Mod_RemoveIntroVideoDesc)
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };

        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_Gear = new ModProp_TextureFile(false, "Loading - Gear", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_01 = new ModProp_TextureFile(false, "Loading - 01", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_02 = new ModProp_TextureFile(false, "Loading - 02", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_03 = new ModProp_TextureFile(false, "Loading - 03", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_04 = new ModProp_TextureFile(false, "Loading - 04", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_05 = new ModProp_TextureFile(false, "Loading - 05", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_06 = new ModProp_TextureFile(false, "Loading - 06", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_07 = new ModProp_TextureFile(false, "Loading - 07", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Loading)]
        public static ModProp_TextureFile Texture_Load_08 = new ModProp_TextureFile(false, "Loading - 08", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };

        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Overlay = new ModProp_TextureFile(false, "Pause Menu Overlay", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icons = new ModProp_TextureFile(false, "Pause Menu Sprites", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level1 = new ModProp_TextureFile(false, "Icon - The Fairy Council", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level2 = new ModProp_TextureFile(false, "Icon - Clearleaf Forest", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level3 = new ModProp_TextureFile(false, "Icon - The Bog of Murk", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level4 = new ModProp_TextureFile(false, "Icon - The Land of the Livid Dead", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level5 = new ModProp_TextureFile(false, "Icon - The Desert of the Knaaren", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level6 = new ModProp_TextureFile(false, "Icon - The Longest Shortcut", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level7 = new ModProp_TextureFile(false, "Icon - The Summit Beyond the Clouds", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level8 = new ModProp_TextureFile(false, "Icon - Hoodlum Headquarters", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Level9 = new ModProp_TextureFile(false, "Icon - The Tower of the Leptys", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Lum = new ModProp_TextureFile(false, "Icon - Black Lum", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Options = new ModProp_TextureFile(false, "Icon - Options", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Rayman = new ModProp_TextureFile(false, "Icon - Rayman", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Murfy = new ModProp_TextureFile(false, "Icon - Murfy", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Videos = new ModProp_TextureFile(false, "Icon - Videos", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Camera = new ModProp_TextureFile(false, "Icon - Camera", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icon_Plum = new ModProp_TextureFile(false, "Icon - Minigames", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icons_Videos = new ModProp_TextureFile(false, "Video Icons", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };
        [ModCategory((int)R3_ModProps.Textures_Menu)]
        public static ModProp_TextureFile Texture_Menu_Icons_Misc = new ModProp_TextureFile(false, "Misc. Icons", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.GCN }, };

        public Modder_Rayman3()
        {

        }

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

            if (Option_RandLevelOrderAll.Enabled)
            {
                Randomize_Level_Order(false);
            }
            else if (Option_RandLevelOrder.Enabled)
            {
                Randomize_Level_Order(true);
            }

            if (Option_RemoveIntroVideos.Enabled)
            {
                Remove_Intro_Videos();
            }

            Custom_Texture_Handle();

            if (Option_NewGameNightmare.Enabled)
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

        void Randomize_Level_Order(bool anyLevels)
        {
            int minLevel = 0;
            int maxLevel = 46;
            int targetPos = 0;
            List<int> ValidLevels = new List<int>();
            List<int> LevelsReplacing = new List<int>();
            string sourceLevel, targetLevel;

            if (anyLevels)
            {
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
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    LevelsReplacing.Add(-1);
                    if (i < maxLevel)
                    {
                        ValidLevels.Add(i);
                    }
                }
                int levelsLeft = 0;
                int currentPos = 0;
                while (levelsLeft <= maxLevel)
                {
                    if (ValidLevels.Count > 0)
                    {
                        targetPos = randState.Next(0, ValidLevels.Count);
                        LevelsReplacing[currentPos] = ValidLevels[targetPos];
                        //Console.WriteLine("Level " + levelsLeft + ": " + (LevelID)LevelsReplacing[currentPos]);
                        currentPos = ValidLevels[targetPos] + 1;
                        ValidLevels.RemoveAt(targetPos);
                    }
                    else
                    {
                        LevelsReplacing[currentPos] = maxLevel;
                    }

                    levelsLeft++;
                }

                //Console.WriteLine("");
                //for (int i = minLevel; i <= maxLevel; i++)
                //{
                //    Console.WriteLine("Order " + i + ": " + (LevelID)LevelsReplacing[i]);
                //}
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

                //Texture_Test.ResourceToFile(basePath + @"fix.tpl.mm56.png");

                if (Option_RandOutfitColors.Enabled)
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

                Texture_Menu_Overlay.ResourceToFile(basePath + @"menu.tpl.png");
                Texture_Menu_Icons.ResourceToFile(basePath + @"menu.tpl.mm1.png");
                Texture_Menu_Icon_Level1.ResourceToFile(basePath + @"menu.tpl.mm2.png");
                Texture_Menu_Icon_Level2.ResourceToFile(basePath + @"menu.tpl.mm3.png");
                Texture_Menu_Icon_Level3.ResourceToFile(basePath + @"menu.tpl.mm4.png");
                Texture_Menu_Icon_Level4.ResourceToFile(basePath + @"menu.tpl.mm5.png");
                Texture_Menu_Icon_Level5.ResourceToFile(basePath + @"menu.tpl.mm6.png");
                Texture_Menu_Icon_Level6.ResourceToFile(basePath + @"menu.tpl.mm7.png");
                Texture_Menu_Icon_Level7.ResourceToFile(basePath + @"menu.tpl.mm8.png");
                Texture_Menu_Icon_Level8.ResourceToFile(basePath + @"menu.tpl.mm9.png");
                Texture_Menu_Icon_Level9.ResourceToFile(basePath + @"menu.tpl.mm10.png");
                Texture_Menu_Icon_Lum.ResourceToFile(basePath + @"menu.tpl.mm11.png");
                Texture_Menu_Icon_Options.ResourceToFile(basePath + @"menu.tpl.mm12.png");
                Texture_Menu_Icon_Rayman.ResourceToFile(basePath + @"menu.tpl.mm13.png");
                Texture_Menu_Icon_Murfy.ResourceToFile(basePath + @"menu.tpl.mm14.png");
                Texture_Menu_Icon_Videos.ResourceToFile(basePath + @"menu.tpl.mm15.png");
                Texture_Menu_Icon_Camera.ResourceToFile(basePath + @"menu.tpl.mm16.png");
                Texture_Menu_Icon_Plum.ResourceToFile(basePath + @"menu.tpl.mm17.png");
                Texture_Menu_Icons_Videos.ResourceToFile(basePath + @"menu.tpl.mm18.png");
                Texture_Menu_Icons_Misc.ResourceToFile(basePath + @"menu.tpl.mm19.png");

                GCN_ImportTextures(basePath + @"menu.tpl.png");
            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                string lsbinpath = ConsolePipeline.ExtractedPath + @"LSBIN\";
                if (File.Exists(lsbinpath + @"lodmeca.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                    File.Delete(lsbinpath + @"lodmeca.tpl");
                    Texture_Load_Gear.ResourceToFile(lsbinpath + @"lodmeca.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodmeca.tpl.png");
                }
                if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodps2_01.tpl");
                    File.Delete(lsbinpath + @"lodps2_01.tpl");
                    Texture_Load_01.ResourceToFile(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_02.tpl");
                    File.Delete(lsbinpath + @"lodps2_02.tpl");
                    Texture_Load_02.ResourceToFile(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_03.tpl");
                    File.Delete(lsbinpath + @"lodps2_03.tpl");
                    Texture_Load_03.ResourceToFile(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_04.tpl");
                    File.Delete(lsbinpath + @"lodps2_04.tpl");
                    Texture_Load_04.ResourceToFile(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_05.tpl");
                    File.Delete(lsbinpath + @"lodps2_05.tpl");
                    Texture_Load_05.ResourceToFile(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_06.tpl");
                    File.Delete(lsbinpath + @"lodps2_06.tpl");
                    Texture_Load_06.ResourceToFile(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_07.tpl");
                    File.Delete(lsbinpath + @"lodps2_07.tpl");
                    Texture_Load_07.ResourceToFile(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_08.tpl");
                    File.Delete(lsbinpath + @"lodps2_08.tpl");
                    Texture_Load_08.ResourceToFile(lsbinpath + @"lodps2_08.tpl.png");
                    GCN_ImportTextures(lsbinpath + @"lodps2_08.tpl.png");
                }
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

                //Texture_Test.FileToResource(basePath + @"fix.tpl.mm56.png");

            }
            else if (File.Exists(basePath + @"FIX.TBF"))
            {
                //Use TBF Tool
            }

            if (File.Exists(basePath + @"menu.tpl"))
            {

                GCN_ExportTextures(basePath + @"menu.tpl");
                File.Delete(basePath + @"menu.tpl");

                Texture_Menu_Overlay.FileToResource(basePath + @"menu.tpl.png");
                Texture_Menu_Icons.FileToResource(basePath + @"menu.tpl.mm1.png");
                Texture_Menu_Icon_Level1.FileToResource(basePath + @"menu.tpl.mm2.png");
                Texture_Menu_Icon_Level2.FileToResource(basePath + @"menu.tpl.mm3.png");
                Texture_Menu_Icon_Level3.FileToResource(basePath + @"menu.tpl.mm4.png");
                Texture_Menu_Icon_Level4.FileToResource(basePath + @"menu.tpl.mm5.png");
                Texture_Menu_Icon_Level5.FileToResource(basePath + @"menu.tpl.mm6.png");
                Texture_Menu_Icon_Level6.FileToResource(basePath + @"menu.tpl.mm7.png");
                Texture_Menu_Icon_Level7.FileToResource(basePath + @"menu.tpl.mm8.png");
                Texture_Menu_Icon_Level8.FileToResource(basePath + @"menu.tpl.mm9.png");
                Texture_Menu_Icon_Level9.FileToResource(basePath + @"menu.tpl.mm10.png");
                Texture_Menu_Icon_Lum.FileToResource(basePath + @"menu.tpl.mm11.png");
                Texture_Menu_Icon_Options.FileToResource(basePath + @"menu.tpl.mm12.png");
                Texture_Menu_Icon_Rayman.FileToResource(basePath + @"menu.tpl.mm13.png");
                Texture_Menu_Icon_Murfy.FileToResource(basePath + @"menu.tpl.mm14.png");
                Texture_Menu_Icon_Videos.FileToResource(basePath + @"menu.tpl.mm15.png");
                Texture_Menu_Icon_Camera.FileToResource(basePath + @"menu.tpl.mm16.png");
                Texture_Menu_Icon_Plum.FileToResource(basePath + @"menu.tpl.mm17.png");
                Texture_Menu_Icons_Videos.FileToResource(basePath + @"menu.tpl.mm18.png");
                Texture_Menu_Icons_Misc.FileToResource(basePath + @"menu.tpl.mm19.png");

            }

            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                string lsbinpath = ConsolePipeline.ExtractedPath + @"LSBIN\";
                if (File.Exists(lsbinpath + @"lodmeca.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodmeca.tpl");
                    File.Delete(lsbinpath + @"lodmeca.tpl");
                    Texture_Load_Gear.FileToResource(lsbinpath + @"lodmeca.tpl.png");
                }
                if (File.Exists(lsbinpath + @"lodps2_01.tpl"))
                {
                    GCN_ExportTextures(lsbinpath + @"lodps2_01.tpl");
                    File.Delete(lsbinpath + @"lodps2_01.tpl");
                    Texture_Load_01.FileToResource(lsbinpath + @"lodps2_01.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_02.tpl");
                    File.Delete(lsbinpath + @"lodps2_02.tpl");
                    Texture_Load_02.FileToResource(lsbinpath + @"lodps2_02.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_03.tpl");
                    File.Delete(lsbinpath + @"lodps2_03.tpl");
                    Texture_Load_03.FileToResource(lsbinpath + @"lodps2_03.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_04.tpl");
                    File.Delete(lsbinpath + @"lodps2_04.tpl");
                    Texture_Load_04.FileToResource(lsbinpath + @"lodps2_04.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_05.tpl");
                    File.Delete(lsbinpath + @"lodps2_05.tpl");
                    Texture_Load_05.FileToResource(lsbinpath + @"lodps2_05.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_06.tpl");
                    File.Delete(lsbinpath + @"lodps2_06.tpl");
                    Texture_Load_06.FileToResource(lsbinpath + @"lodps2_06.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_07.tpl");
                    File.Delete(lsbinpath + @"lodps2_07.tpl");
                    Texture_Load_07.FileToResource(lsbinpath + @"lodps2_07.tpl.png");
                    GCN_ExportTextures(lsbinpath + @"lodps2_08.tpl");
                    File.Delete(lsbinpath + @"lodps2_08.tpl");
                    Texture_Load_08.FileToResource(lsbinpath + @"lodps2_08.tpl.png");
                }
            }
        }

        void GCN_ExportTextures(string path)
        {
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
