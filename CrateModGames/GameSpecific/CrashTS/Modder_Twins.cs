using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Twinsanity;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTS;
//Twinsanity API by NeoKesha, Smartkin, ManDude and Marko (https://github.com/Smartkin/twinsanity-editor)
/* 
 * Mod Layers:
 * 1: Extracted BD/BH archive files (PS2 only, same as layer 0 on XBOX)
 */

namespace CrateModLoader.GameSpecific.CrashTS
{

    public enum RM_Sections
    {
        Graphics = 11,
        Code = 10,
        Particles = 8,
        ColData = 9,
        Instances1 = 0,
        Instances2 = 1,
        Instances3 = 2,
        Instances4 = 3,
        Instances5 = 4,
        Instances6 = 5,
        Instances7 = 6,
        Instances8 = 7,
    }
    public enum RM_Graphics_Sections
    {
        Textures = 0,
        Materials = 1,
        Models = 2,
        RigidModels = 3,
        Skin = 4,
        BlendSkin = 5,
        Meshes = 6,
        LodModels = 7,
        Skydome = 8,
    }
    public enum RM_Code_Sections
    {
        Object = 0,
        Script = 1,
        Animation = 2,
        OGI = 3,
        CodeModel = 4,
        Unknown = 5,
        SE = 6,
        // also used for japanese
        SE_Eng = 7,
        SE_Fre = 8,
        SE_Ger = 9,
        SE_Spa = 10,
        SE_Ita = 11,
        SE_Unused = 12,
    }
    public enum RM_Instance_Sections
    {
        UnknownInstance = 0,
        AIPosition = 1,
        AIPath = 2,
        Position = 3,
        Path = 4,
        CollisionSurface = 5,
        ObjectInstance = 6,
        Trigger = 7,
        Camera = 8,
    }

    public enum ModProps : int
    {
        Options = 0,
        Misc = 1,
        Character = 2,
    }

    public sealed class Modder_Twins : Modder
    {

        public override Game Game => new Game()
        {
            Name = Twins_Text.GameTitle,
            ShortName = "CrashTS",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                },
            API_Credit = Twins_Text.API_Credit,
            API_Link = "https://github.com/Smartkin/twinsanity-editor",
            TextClass = typeof(Twins_Text),
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_209.09",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"SLES_525.68",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"SLPM_658.01",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                },
                [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7,
                    ExecName = "default.xbe" },
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.PAL,
                    RegionNumber = 4,
                    ExecName = "default.xbe" },
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [(int)ModProps.Options] = "Options",
                [(int)ModProps.Misc] = "Misc.",
                [(int)ModProps.Character] = "Character",
            }
        };

        public static ModPropOption Option_RandCrates = new ModPropOption(Twins_Text.Rand_Crates, Twins_Text.Rand_CratesDesc)
        {
            Items = new List<string>() {
                "Off", "All Crates", "Types Only", },
            ItemsDesc = new List<string>() {
                "", "Individual crates are randomized.", "Only crate types are switched around.", },
        };
        public static ModPropOption Option_RandGemLocations = new ModPropOption(Twins_Text.Rand_GemLocations, Twins_Text.Rand_GemLocationsDesc);
        public static ModPropOption Option_RandCharacterParams = new ModPropOption(Twins_Text.Rand_CharParams, Twins_Text.Rand_CharParamsDesc);
        public static ModPropOption Option_FlyingKick = new ModPropOption(Twins_Text.Mod_FlyingKick, Twins_Text.Mod_FlyingKickDesc);
        public static ModPropOption Option_StompKick = new ModPropOption(Twins_Text.Mod_StompKick, Twins_Text.Mod_StompKickDesc);
        public static ModPropOption Option_DoubleJumpCortex = new ModPropOption(Twins_Text.Mod_CortexDoubleJump, Twins_Text.Mod_CortexDoubleJumpDesc);
        public static ModPropOption Option_DoubleJumpNina = new ModPropOption(Twins_Text.Mod_NinaDoubleJump, Twins_Text.Mod_NinaDoubleJumpDesc);
        public static ModPropOption Option_UnusedEnemies = new ModPropOption(Twins_Text.Mod_UnusedEnemies, Twins_Text.Mod_UnusedEnemiesDesc);
        public static ModPropOption Option_SwitchCharacters = new ModPropOption(Twins_Text.Mod_SwitchCharacters, Twins_Text.Mod_SwitchCharactersDesc);
        public static ModPropOption Option_ClassicHealth = new ModPropOption(Twins_Text.Mod_ClassicHealth, Twins_Text.Mod_ClassicHealthDesc);
        public static ModPropOption Option_ClassicExplosions = new ModPropOption(Twins_Text.Mod_ClassicExplosionDaamge, Twins_Text.Mod_ClassicExplosionDamageDesc);
        public static ModPropOption Option_UnlockedCamera = new ModPropOption(Twins_Text.Mod_UnlockedCamera, Twins_Text.Mod_UnlockedCameraDesc);
        public static ModPropOption Option_ClassicBossHealth = new ModPropOption("Classic Boss Health", "Start boss fights with 2 masks.") // TODO
        { Hidden = true, };
        public static ModPropOption Option_SkipCutscenes = new ModPropOption("Skip Cutscenes", "Skips all non-video cutscenes after entering.") // TODO
        { Hidden = true, };
        public static ModPropOption Option_ClassicCrates = new ModPropOption(Twins_Text.Mod_ClassicCratePersistence, Twins_Text.Mod_ClassicCratePersistenceDesc) // TODO
        { Hidden = true, };

        public static ModPropOption Option_RandomizeMusic = new ModPropOption(Twins_Text.Rand_Music, Twins_Text.Rand_MusicDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(Twins_Text.Rand_WorldPalette, Twins_Text.Rand_WorldPaletteDesc);
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(Twins_Text.Mod_GreyscaleWorld, Twins_Text.Mod_GreyscaleWorldDesc) // todo: textures
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(Twins_Text.Mod_UntexturedWorld, Twins_Text.Mod_UntexturedWorldDesc);
        public static ModPropOption Option_RandPantsColor = new ModPropOption(Twins_Text.Rand_PantsColor, Twins_Text.Rnad_PantsColorDesc) { Hidden = true, }; // doesn't work yet

        [ModCategory((int)ModProps.Misc)]
        public static ModPropNamedFloatArray Prop_PantsColor = new ModPropNamedFloatArray(new float[3] { 0, 0, 1f }, new string[] { "Red", "Green", "Blue" }, Twins_Text.Prop_PantsColor, Twins_Text.Prop_PantsColorDesc)
        { Hidden = true };

        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") // TODO
        { Hidden = true, };
        public static ModPropOption Option_RandEnemies = new ModPropOption("Randomize Enemies (Soundless)", "") // not stable enough
        { Hidden = true, };
        public static ModPropOption Option_RandStartingChunk = new ModPropOption("Randomize Starting Chunk", "") // TODO
        { Hidden = true, };

        public Modder_Twins()
        {

        }

        internal string bdPath = "";
        internal string extensionMod = "2";
        internal TwinsFile.FileType rmType = TwinsFile.FileType.RM2;
        internal TwinsFile.FileType smType = TwinsFile.FileType.SM2;
        internal Random randState = new Random();
        internal List<uint> CrateReplaceList = new List<uint>();
        internal List<uint> randCrateList = new List<uint>();
        internal List<uint> gemObjectList = new List<uint>();
        internal List<uint> musicTypes = new List<uint>();
        internal List<uint> randMusicList = new List<uint>();
        internal List<ObjectID> EnemyReplaceList = new List<ObjectID>();
        internal List<ObjectID> EnemyInsertList = new List<ObjectID>();
        internal bool[] levelEdited;
        internal bool[] sceneryEdited;
        internal bool Edit_AllCharacters = false;
        internal Script StrafeLeft = null;
        internal Script StrafeRight = null;
        internal Script GenericCrateExplode = null;
        

        public override void StartModProcess()
        {

            // Extract BD (PS2 only)
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                bdPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "cml_extr/");
                Directory.CreateDirectory(bdPath);

                BDArchive.ExtractAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

                File.Delete(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH.BD"));
                File.Delete(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH.BH"));
                extensionMod = "2";
                rmType = TwinsFile.FileType.RM2;
                smType = TwinsFile.FileType.SM2;
            }
            else
            {
                bdPath = ConsolePipeline.ExtractedPath;
                extensionMod = "x";
                rmType = TwinsFile.FileType.RMX;
                smType = TwinsFile.FileType.SMX;
            }

            ModProcess();
        }

        void ModProcess()
        {
            //Start Modding
            randState = new Random(ModLoaderGlobals.RandomizerSeed);
            
            Twins_Settings.PatchEXE(ConsolePipeline.Metadata.Console, GameRegion.Region, ConsolePipeline.ExtractedPath, GameRegion.ExecName);

            ModCrates.InstallLayerMods(bdPath, 1);

            bool Twins_Edit_CodeText = true;
            bool Twins_Edit_AllLevels = false;
            bool Twins_Edit_AllScenery = false;

            Edit_AllCharacters = false;

            foreach (ModPropertyBase prop in Props)
            {
                if (prop.HasChanged)
                {
                    if (prop.Category == (int)ModProps.Character)
                    {
                        Twins_Edit_AllLevels = true;
                        Edit_AllCharacters = true;
                    }
                }
            }

            if (Option_GreyscaleWorld.Enabled || Option_UntexturedWorld.Enabled || Option_RandWorldPalette.Enabled)
            {
                Twins_Edit_AllScenery = true;
            }

            if (Option_RandCrates.Value == 2)
            {
                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm" + extensionMod, rmType);

                List<uint> crateList = new List<uint>();
                List<uint> posList = new List<uint>();

                crateList.Add((uint)ObjectID.BASICCRATE);
                //crateList.Add((uint)ObjectID.TNT_CRATE);
                //crateList.Add((uint)ObjectID.NITRO_CRATE);
                crateList.Add((uint)ObjectID.EXTRALIFECRATE);
                crateList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                crateList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                crateList.Add((uint)ObjectID.AKUAKUCRATE);
                //crateList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                //crateList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_NINA);
                //crateList.Add((uint)ObjectID.IRONCRATE);
                //crateList.Add((uint)ObjectID.IRONSPRINGCRATE);
                crateList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                crateList.Add((uint)ObjectID.SURPRISECRATE);

                posList.Add((uint)ObjectID.BASICCRATE);
                //posList.Add((uint)ObjectID.TNT_CRATE);
                //posList.Add((uint)ObjectID.NITRO_CRATE);
                posList.Add((uint)ObjectID.EXTRALIFECRATE);
                posList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                posList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                posList.Add((uint)ObjectID.AKUAKUCRATE);
                //posList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                //posList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_NINA);
                //posList.Add((uint)ObjectID.IRONCRATE);
                //posList.Add((uint)ObjectID.IRONSPRINGCRATE);
                posList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                posList.Add((uint)ObjectID.SURPRISECRATE);

                int target_item = 0;

                while (posList.Count > 0)
                {
                    target_item = randState.Next(0, crateList.Count);
                    TwinsSection objectdata = mainArchive.GetItem<TwinsSection>((uint)RM_Sections.Code).GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
                    if (objectdata.ContainsItem(posList[0]))
                        objectdata.GetItem<TwinsItem>(posList[0]).ID = crateList[target_item];
                    posList.RemoveAt(0);
                    crateList.RemoveAt(target_item);
                }
                posList.Clear();
                crateList.Clear();

                mainArchive.SaveFile(bdPath + "/Startup/Default.rm" + extensionMod);
            }
            if (Option_RandCrates.Value == 1)
            {
                //Importing ammo crate
                TwinsFile cortexlevelArchive = new TwinsFile();
                cortexlevelArchive.LoadFile(bdPath + @"Levels\school\Cortex\cogpa01.rm" + extensionMod, rmType);

                List<ObjectID> exportList = new List<ObjectID>();
                Twins_Data.ExportGameObject(ref cortexlevelArchive, ObjectID.AMMOCRATESMALL,ref exportList);
                exportList.Clear();

                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnPhysicsCollision] = 65535;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnTouch] = 65535;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnTrigger] = (ushort)ScriptID.HEAD_COM_GENERIC_CRATE_TRIGGER_NEXT;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnDamage] = (ushort)ScriptID.HEAD_COM_AMMO_CRATE_SMALL_TOUCHED;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnLand] = (ushort)ScriptID.HEAD_COM_BASIC_CRATE_LANDED_ON;

                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm" + extensionMod, rmType);

                Twins_Data.ImportGameObject(ref mainArchive, ObjectID.AMMOCRATESMALL,ref exportList);
                exportList.Clear();

                mainArchive.SaveFile(bdPath + "/Startup/Default.rm" + extensionMod);
            }

            if (Option_RandCrates.Value == 1)
            {

                // Crates to insert
                randCrateList = new List<uint>();
                randCrateList.Add((uint)ObjectID.BASICCRATE);
                //randCrateList.Add((uint)ObjectID.TNT_CRATE);
                //randCrateList.Add((uint)ObjectID.NITRO_CRATE);
                randCrateList.Add((uint)ObjectID.EXTRALIFECRATE);
                randCrateList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                randCrateList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                randCrateList.Add((uint)ObjectID.AKUAKUCRATE);
                randCrateList.Add((uint)ObjectID.IRONCRATE);
                randCrateList.Add((uint)ObjectID.IRONSPRINGCRATE);
                randCrateList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                randCrateList.Add((uint)ObjectID.SURPRISECRATE);
                randCrateList.Add((uint)ObjectID.AMMOCRATESMALL);

                // Crates to replace
                CrateReplaceList = new List<uint>();
                CrateReplaceList.Add((uint)ObjectID.BASICCRATE);
                //CrateReplaceList.Add((uint)ObjectID.TNT_CRATE);
                //CrateReplaceList.Add((uint)ObjectID.NITRO_CRATE);
                CrateReplaceList.Add((uint)ObjectID.EXTRALIFECRATE);
                CrateReplaceList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                CrateReplaceList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                CrateReplaceList.Add((uint)ObjectID.AKUAKUCRATE);
                //CrateReplaceList.Add((uint)ObjectID.IRON_CRATE);
                //CrateReplaceList.Add((uint)ObjectID.IRON_SPRING_CRATE);
                CrateReplaceList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                CrateReplaceList.Add((uint)ObjectID.SURPRISECRATE);
                //CrateReplaceList.Add((uint)ObjectID.AMMOCRATESMALL);

                Twins_Edit_AllLevels = true;
            }

            if (Option_RandGemLocations.Enabled)
            {
                Twins_Data.Twins_Randomize_Gems(ref randState);

                gemObjectList = new List<uint>();
                gemObjectList.Add((uint)ObjectID.GEM_BLUE);
                gemObjectList.Add((uint)ObjectID.GEM_CLEAR);
                gemObjectList.Add((uint)ObjectID.GEM_GREEN);
                gemObjectList.Add((uint)ObjectID.GEM_PURPLE);
                gemObjectList.Add((uint)ObjectID.GEM_RED);
                gemObjectList.Add((uint)ObjectID.GEM_YELLOW);

                Twins_Edit_AllLevels = true;
            }

            if (Option_RandomizeMusic.Enabled)
            {
                List<uint> temp_musicList = new List<uint>();

                musicTypes.Add((uint)MusicID.Academy);
                musicTypes.Add((uint)MusicID.AcademyNoLaugh);
                musicTypes.Add((uint)MusicID.AltLab);
                musicTypes.Add((uint)MusicID.AntAgony);
                musicTypes.Add((uint)MusicID.BeeChase);
                musicTypes.Add((uint)MusicID.Boiler);
                musicTypes.Add((uint)MusicID.BoilerUnused);
                musicTypes.Add((uint)MusicID.BossAmberly);
                musicTypes.Add((uint)MusicID.BossDingodile);
                musicTypes.Add((uint)MusicID.BossNGin);
                musicTypes.Add((uint)MusicID.BossTikimon);
                musicTypes.Add((uint)MusicID.BossTwins);
                musicTypes.Add((uint)MusicID.BossUka);
                musicTypes.Add((uint)MusicID.BP);
                musicTypes.Add((uint)MusicID.Cavern);
                musicTypes.Add((uint)MusicID.ClassroomCortex);
                musicTypes.Add((uint)MusicID.ClassroomCrash);
                musicTypes.Add((uint)MusicID.Henchmania);
                musicTypes.Add((uint)MusicID.Hijinks);
                musicTypes.Add((uint)MusicID.IcebergLab);
                musicTypes.Add((uint)MusicID.IcebergLabFast);
                musicTypes.Add((uint)MusicID.IceClimb);
                musicTypes.Add((uint)MusicID.MechaBandicoot);
                musicTypes.Add((uint)MusicID.Rockslide);
                musicTypes.Add((uint)MusicID.Rooftop);
                musicTypes.Add((uint)MusicID.SlipSlide);
                musicTypes.Add((uint)MusicID.TitleTheme);
                musicTypes.Add((uint)MusicID.TotemRiver);
                musicTypes.Add((uint)MusicID.TwinsanityIsland);
                musicTypes.Add((uint)MusicID.WalrusChase);
                musicTypes.Add((uint)MusicID.WormChase);
                int targetPos = 0;

                for (int i = 0; i < musicTypes.Count; i++)
                {
                    temp_musicList.Add(musicTypes[i]);
                }
                while (temp_musicList.Count > 0)
                {
                    targetPos = randState.Next(0, temp_musicList.Count);
                    randMusicList.Add(temp_musicList[targetPos]);
                    temp_musicList.RemoveAt(targetPos);
                }

                Twins_Edit_AllLevels = true;
            }

            if (Option_FlyingKick.Enabled || Option_StompKick.Enabled || Option_DoubleJumpCortex.Enabled || Option_DoubleJumpNina.Enabled 
                || Option_SwitchCharacters.Enabled || Option_ClassicExplosions.Enabled || Option_RandEnemies.Enabled)
            {
                Twins_Edit_AllLevels = true;

                levelEdited = new bool[140];

                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_LoadLevels(di);
                }

                TwinsFile mainArchiveLoad = new TwinsFile();
                mainArchiveLoad.LoadFile(bdPath + @"Startup\Default.rm" + extensionMod, rmType);

                RM_LoadScripts(mainArchiveLoad);
                RM_LoadObjects(mainArchiveLoad);

                Twins_Data.allScripts.Sort((x, y) => x.ID.CompareTo(y.ID));
                Twins_Data.allObjects.Sort((x, y) => x.ID.CompareTo(y.ID));

                int scriptVer = 0;
                if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
                {
                    scriptVer = 1;
                }

                if (Option_SwitchCharacters.Enabled)
                {
                    StrafeLeft = Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT);
                    StrafeRight = Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT);

                    Script.MainScript.ScriptCommand SwitchToCrashCommand = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                        arguments = new List<uint>() { 0xCDCDDFEF, 0 },
                    };
                    Script.MainScript.ScriptCommand SwitchToCortexCommand = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                        arguments = new List<uint>() { 0xCDCDDFEF, 1 },
                    };
                    Script.MainScript.ScriptCommand SwitchToNinaCommand = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                        arguments = new List<uint>() { 0xCDCDDFEF, 3 },
                    };
                    Script.MainScript.ScriptCommand SwitchToMechaCommand = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter,
                        arguments = new List<uint>() { 0xCDCDDF6F, 6 },
                    };
                    /*
                    Script.MainScript.ScriptCommand TestCommand = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = 517,
                        arguments = new List<uint>() {  },
                    };
                    Script.MainScript.ScriptCommand TestCommand2 = new Script.MainScript.ScriptCommand(scriptVer)
                    {
                        VTableIndex = 519,
                        arguments = new List<uint>() {  },
                    };
                    */

                    StrafeLeft.Main.scriptState1.scriptStateBody.command = SwitchToCrashCommand;
                    StrafeRight.Main.scriptState1.scriptStateBody.command = SwitchToCortexCommand;

                }

                if (Option_ClassicExplosions.Enabled)
                {
                    GenericCrateExplode = Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CRATE_EXPLODE);

                    Script.MainScript.ScriptState state = GenericCrateExplode.Main.scriptState1;
                    for (int s = 0; s < 3; s++)
                    {
                        state = state.nextState;
                    }
                    Script.MainScript.ScriptCommand command = state.scriptStateBody.command;
                    while (command.VTableIndex != (ushort)DefaultEnums.CommandID.CreateDamageZone)
                    {
                        command = command.nextCommand;
                    }
                    command.arguments[7] = 8;

                }
                
                
                if (Option_RandEnemies.Enabled)
                {
                    EnemyReplaceList.Add(ObjectID.GLOBAL_MONKEY);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_CHICKEN);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_CRAB);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_SKUNK);
                    //EnemyReplaceList.Add(ObjectID.EARTH_TRIBESMAN_SHIELDBEARER); // because it may softlock in JB
                    EnemyReplaceList.Add(ObjectID.EARTH_TRIBESMAN);
                    //EnemyReplaceList.Add(ObjectID.GLOBAL_BAT_DARKPURPLE); // because it may crash in JB
                    EnemyReplaceList.Add(ObjectID.GLOBAL_BAT_ICE);
                    EnemyReplaceList.Add(ObjectID.PIRANHAPLANT);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_PIG_WILDBOAR);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_INTERMEDIATE);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_DARKBROWN);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_DARKPURPLE);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_GREY);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_LIGHTBROWN);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_LIGHTPURPLE);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_MEDIUMBROWN);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_WHITE);
                    EnemyReplaceList.Add(ObjectID.PENGUIN);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_CORTEX_CAMERABOT);
                    //EnemyReplaceList.Add(ObjectID.RHINO_PIRATE); // maybe once the melee variation is figured out
                    EnemyReplaceList.Add(ObjectID.SCHOOL_DOG);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_COCKROACH);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_BEETLE_DARKPURPLE);
                    EnemyReplaceList.Add(ObjectID.GLOBAL_BEETLE_PROJECTILE);
                    EnemyReplaceList.Add(ObjectID.SCHOOL_FROGENSTEIN);
                    EnemyReplaceList.Add(ObjectID.SCHOOL_ZOMBOT);
                    EnemyReplaceList.Add(ObjectID.DRONE_BASIC);
                    EnemyReplaceList.Add(ObjectID.DRONE_DRILLER);
                    EnemyReplaceList.Add(ObjectID.DRONE_FLAMER);
                    EnemyReplaceList.Add(ObjectID.DRONE_FLYER);
                    EnemyReplaceList.Add(ObjectID.DRONE_SOLDIER);
                    EnemyReplaceList.Add(ObjectID.DRONE_BERSERKER);

                    EnemyInsertList.Add(ObjectID.GLOBAL_CHICKEN); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_MONKEY); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_SKUNK); // works
                    EnemyInsertList.Add(ObjectID.EARTH_TRIBESMAN_SHIELDBEARER); // works (but has 1 HP)
                    EnemyInsertList.Add(ObjectID.EARTH_TRIBESMAN); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_BAT_DARKPURPLE); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_BAT_ICE); // works
                    EnemyInsertList.Add(ObjectID.PIRANHAPLANT); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_RAT_INTERMEDIATE); // works
                    EnemyInsertList.Add(ObjectID.PENGUIN); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_CORTEX_CAMERABOT); // works
                    EnemyInsertList.Add(ObjectID.SCHOOL_DOG); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_COCKROACH); // works
                    EnemyInsertList.Add(ObjectID.GLOBAL_BEETLE_PROJECTILE); // works
                    EnemyInsertList.Add(ObjectID.SCHOOL_ZOMBOT); // works
                    EnemyInsertList.Add(ObjectID.SCHOOL_FROGENSTEIN); // works
                    EnemyInsertList.Add(ObjectID.DRONE_BASIC); // works, but only spawns when triggered
                    EnemyInsertList.Add(ObjectID.DRONE_FLAMER); // works
                    EnemyInsertList.Add(ObjectID.DRONE_BERSERKER); // works
                    EnemyInsertList.Add(ObjectID.DRONE_FLYER); // works

                    //EnemyInsertList.Add(ObjectID.DRONE_SOLDIER); // doesn't work (crashed on spawn)
                    //EnemyInsertList.Add(ObjectID.DRONE_DRILLER); // doesn't work (crashed on spawn)
                    //EnemyInsertList.Add(ObjectID.GLOBAL_PIG_WILDBOAR); // doesn't work (minor errors, crashed when switching chunk)

                    //EnemyInsertList.Add(ObjectID.SCHOOL_JANITOR); // works, but lags the game and can't be defeated
                    //EnemyInsertList.Add(ObjectID.RHINO_PIRATE); // works, but stands in place throwing barrels to the same spot
                    //EnemyInsertList.Add(ObjectID.MINI_MON); // works with errors, just rushes you
                    //EnemyInsertList.Add(ObjectID.GLOBAL_CRAB); // works with errors, only spawns near AIpositions?
                    //EnemyInsertList.Add(ObjectID.GLOBAL_BEETLE_DARKPURPLE); // works, pretty much the same as GLOBAL_BEETLE_PROJECTILE?

                    for (int i = 0; i < Twins_Data.cachedGameObjects.Count; i++)
                    {
                        //soundless objects - temporary workaround because sound import/export is broken
                        if (Twins_Data.cachedGameObjects[i].mainObject.cSounds.Count > 0)
                        {
                            for (int a = 0; a < Twins_Data.cachedGameObjects[i].mainObject.cSounds.Count; a++)
                            {
                                Twins_Data.cachedGameObjects[i].mainObject.cSounds[a] = 20 ;
                            }
                        }
                        if (Twins_Data.cachedGameObjects[i].mainObject.Sounds.Count > 0)
                        {
                            for (int s = 0; s < Twins_Data.cachedGameObjects[i].mainObject.Sounds.Count; s++)
                            {
                                Twins_Data.cachedGameObjects[i].mainObject.Sounds[s] = 65535;
                            }
                        }
                        if (Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.GLOBAL_BAT_DARKPURPLE || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.SCHOOL_FROGENSTEIN || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.GLOBAL_SKUNK || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.SCHOOL_JANITOR)
                        {
                            Twins_Data.cachedGameObjects[i] = new CachedGameObject()
                            {
                                mainObject = Twins_Data.cachedGameObjects[i].mainObject,
                                instanceTemplate = new InstanceTemplate()
                                {
                                    Properties = 0x188B2E, //Twins_Data.cachedGameObjects[i].instanceTemplate.Properties - (uint)Twins_Data.PropertyFlags.DisableObject,
                                    Flags = Twins_Data.cachedGameObjects[i].instanceTemplate.Flags,
                                    FloatVars = Twins_Data.cachedGameObjects[i].instanceTemplate.FloatVars,
                                    InstanceIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.InstanceIDs,
                                    InstancesNum = Twins_Data.cachedGameObjects[i].instanceTemplate.InstancesNum,
                                    IntVars = Twins_Data.cachedGameObjects[i].instanceTemplate.IntVars,
                                    ObjectID = Twins_Data.cachedGameObjects[i].instanceTemplate.ObjectID,
                                    PathIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.PathIDs,
                                    PathsNum = Twins_Data.cachedGameObjects[i].instanceTemplate.PathsNum,
                                    PositionIDs = new List<ushort>(), //Twins_Data.cachedGameObjects[i].instanceTemplate.PositionIDs,
                                    PositionsNum = Twins_Data.cachedGameObjects[i].instanceTemplate.PositionsNum
                                },
                                list_blendskins = Twins_Data.cachedGameObjects[i].list_blendskins,
                                list_anims = Twins_Data.cachedGameObjects[i].list_anims,
                                list_skins = Twins_Data.cachedGameObjects[i].list_skins,
                                list_scriptpacks = Twins_Data.cachedGameObjects[i].list_scriptpacks,
                                list_materials = Twins_Data.cachedGameObjects[i].list_materials,
                                list_models = Twins_Data.cachedGameObjects[i].list_models,
                                list_rigidmodels = Twins_Data.cachedGameObjects[i].list_rigidmodels,
                                list_ogi = Twins_Data.cachedGameObjects[i].list_ogi,
                                list_scripts = Twins_Data.cachedGameObjects[i].list_scripts,
                                list_sounds = Twins_Data.cachedGameObjects[i].list_sounds,
                                list_sounds_english = Twins_Data.cachedGameObjects[i].list_sounds_english,
                                list_sounds_french = Twins_Data.cachedGameObjects[i].list_sounds_french,
                                list_sounds_german = Twins_Data.cachedGameObjects[i].list_sounds_german,
                                list_sounds_italian = Twins_Data.cachedGameObjects[i].list_sounds_italian,
                                list_sounds_spanish = Twins_Data.cachedGameObjects[i].list_sounds_spanish,
                                list_sounds_unused = Twins_Data.cachedGameObjects[i].list_sounds_unused,
                                list_subobjects = Twins_Data.cachedGameObjects[i].list_subobjects,
                                list_textures = Twins_Data.cachedGameObjects[i].list_textures
                            };
                        }
                    }
                }
                
            }
            

            if (Option_UnusedEnemies.Enabled || Option_RandPantsColor.Enabled || Option_UnlockedCamera.Enabled)
            {
                Twins_Edit_AllLevels = true;
            }
            if (Option_RandCharacterParams.Enabled)
            {
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Crash, ref randState);
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Cortex, ref randState);
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Nina, ref randState);
                Twins_Edit_AllLevels = true;
            }

            if (Twins_Edit_AllLevels)
            {
                levelEdited = new bool[140];

                RM_EditDefault();

                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_EditLevels(di);
                }

                Twins_Data.allScripts.Clear();
                Twins_Data.allObjects.Clear();
                Twins_Data.cachedGameObjects.Clear();
            }

            if (Twins_Edit_AllScenery)
            {
                sceneryEdited = new bool[140];

                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_EditScenery(di);
                }
            }

            if (Twins_Edit_CodeText)
            {
                string[] CodeText;
                if (GameRegion.Region == RegionType.NTSC_U)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/American.txt", Encoding.Default);
                }
                else if (GameRegion.Region == RegionType.PAL)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/English.txt", Encoding.Default);
                }
                else
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/Japanese.txt", Encoding.Default);
                }

                List<string> CodeText_LineList = new List<string>();
                for (int i = 0; i < CodeText.Length; i++)
                {
                    CodeText_LineList.Add(CodeText[i]);
                }

                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    if (CodeText_LineList[i] == "to enable autosave,~return to the pause menu~and re-save the game.")
                    {
                        CodeText_LineList[i] = "to enable autosave,~return to the pause menu~and re-save the game.~crate mod loader " + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "seed: " + ModLoaderGlobals.RandomizerSeed + "";
                    }
                    else if (CodeText_LineList[i] == "autosave disabled")
                    {
                        CodeText_LineList[i] = "autosave disabled~";
                    }
                    else if (i == 39 && GameRegion.Region == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~";
                    }
                    else if (i == 40 && GameRegion.Region == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~" + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "" + ModLoaderGlobals.RandomizerSeed + "";
                    }
                }

                CodeText = new string[CodeText_LineList.Count];
                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    CodeText[i] = CodeText_LineList[i];
                }

                if (GameRegion.Region == RegionType.NTSC_U)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/American.txt", CodeText, Encoding.Default);
                }
                else if (GameRegion.Region == RegionType.PAL)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/English.txt", CodeText, Encoding.Default);
                }
                else
                {
                    File.WriteAllLines(bdPath + "/Language/Code/Japanese.txt", CodeText, Encoding.Default);
                }
            }

            EndModProcess();
        }

        void RM_EditDefault()
        {
            TwinsFile RM_Archive = new TwinsFile();
            RM_Archive.LoadFile(bdPath + @"Startup\Default.rm" + extensionMod, rmType);

            if (Option_ClassicExplosions.Enabled)
                Twins_Mods.RM_Mod_ClassicExplosions(RM_Archive, GenericCrateExplode);

            RM_Archive.SaveFile(bdPath + @"Startup\Default.rm" + extensionMod);
        }

        void RM_EditLevel(string path)
        {
            string mainPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, @"cml_extr\");
            if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                mainPath = ConsolePipeline.ExtractedPath;
            }
            ChunkType chunkType = Twins_Data.ChunkPathToType(path, mainPath, extensionMod);
            if (chunkType != ChunkType.Invalid)
            {
                if (levelEdited[(int)chunkType])
                {
                    return;
                }
                else
                {
                    levelEdited[(int)chunkType] = true;
                }
            }

            //Console.WriteLine(chunkType);

            TwinsFile RM_Archive = new TwinsFile();
            RM_Archive.LoadFile(path, rmType);

            if (Option_RandCrates.Value == 1)
                Twins_Randomizers.RM_Randomize_Crates(RM_Archive, chunkType, ref randState, ref CrateReplaceList, ref randCrateList);
            if (Option_RandGemLocations.Enabled)
                Twins_Randomizers.RM_Randomize_Gems(RM_Archive, chunkType, ref gemObjectList);
            if (Option_RandomizeMusic.Enabled)
                Twins_Randomizers.RM_Randomize_Music(RM_Archive, ref musicTypes, ref randMusicList);
            if (Option_RandCharacterParams.Enabled || Edit_AllCharacters)
                Twins_Randomizers.RM_Randomize_CharacterInstanceStats(RM_Archive);
            if (Option_RandEnemies.Enabled)
                Twins_Randomizers.RM_Randomize_Enemies(RM_Archive, chunkType, ref randState, ref EnemyReplaceList, ref EnemyInsertList);
            if (Option_RandPantsColor.Enabled)
                Twins_Randomizers.RM_Randomize_PantsColor(RM_Archive, Color.Green);


            if (Option_StompKick.Enabled)
                Twins_Mods.RM_CharacterObjectMod(RM_Archive);
            if (Option_FlyingKick.Enabled || Option_StompKick.Enabled)
                Twins_Mods.RM_CharacterMod_EnableFlyingKick(RM_Archive);
            if (Option_DoubleJumpCortex.Enabled)
                Twins_Mods.RM_CharacterMod_DoubleJumpCortex(RM_Archive);
            if (Option_DoubleJumpNina.Enabled)
                Twins_Mods.RM_CharacterMod_DoubleJumpNina(RM_Archive);
            if (Option_UnusedEnemies.Enabled)
                Twins_Mods.RM_EnableUnusedEnemies(RM_Archive);
            if (Option_SwitchCharacters.Enabled)
                Twins_Mods.RM_SwitchCharactersMod(RM_Archive, StrafeLeft, StrafeRight);
            if (Option_ClassicHealth.Enabled)
                Twins_Mods.RM_Mod_ClassicHealth(RM_Archive);
            if (Option_ClassicExplosions.Enabled)
                Twins_Mods.RM_Mod_ClassicExplosions(RM_Archive, GenericCrateExplode);
            if (Option_UnlockedCamera.Enabled)
                Twins_Mods.RM_Mod_UnlockedCamera(RM_Archive, chunkType);

            RM_Archive.SaveFile(path);
        }
        
        void RM_LoadLevel(string path)
        {
            string mainPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, @"cml_extr\");
            if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                mainPath = ConsolePipeline.ExtractedPath;
            }
            ChunkType chunkType = Twins_Data.ChunkPathToType(path, mainPath, extensionMod);
            if (chunkType != ChunkType.Invalid)
            {
                if (levelEdited[(int)chunkType])
                {
                    return;
                }
                else
                {
                    levelEdited[(int)chunkType] = true;
                }
            }

            TwinsFile RM_Archive = new TwinsFile();
            RM_Archive.LoadFile(path, rmType);

            RM_LoadScripts(RM_Archive);
            RM_LoadObjects(RM_Archive);


            /*
            if (GetOption(RandomizeEnemies))
            {
                List<ObjectID> ExportedObjects = new List<ObjectID>();
                if (chunkType == Twins_Data.ChunkType.Earth_Hub_Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_MONKEY, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CHICKEN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CRAB, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_SKUNK, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.PIRANHAPLANT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BAT_DARKPURPLE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.EARTH_TRIBESMAN_SHIELDBEARER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Totem_L03Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.EARTH_TRIBESMAN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_PIG_WILDBOAR, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_HighSeas_GPA07)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_RAT_INTERMEDIATE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubD)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.MINI_MON, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_IceClimb_BergExt)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.PENGUIN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CORTEX_CAMERABOT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BAT_ICE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_HighSeas_GPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.RHINO_PIRATE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrGPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_FROGENSTEIN, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrGPA04)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_DOG, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrLib)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_ZOMBOT, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Boiler_Boiler_X)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_COCKROACH, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BEETLE_PROJECTILE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.AltEarth_Core_CoreA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_BERSERKER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_DRILLER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_SOLDIER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.AltEarth_Core_CoreB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_FLYER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_FLAMER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Rooftop_BusChase)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_BASIC, ref ExportedObjects);
                }
                //else if (chunkType == Twins_Data.ChunkType.School_Cortex_CoGPA03)
                //{
                    //Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_JANITOR, ref ExportedObjects);
                //}
                //else if (chunkType == Twins_Data.ChunkType.School_Boiler_Boiler_2)
                //{
                    //Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BEETLE_DARKPURPLE, ref ExportedObjects);
                //}
            }
            */

        }

        void SM_EditLevel(string path)
        {
            string mainPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, @"cml_extr\");
            if (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX)
            {
                mainPath = ConsolePipeline.ExtractedPath;
            }
            ChunkType chunkType = Twins_Data.SceneryPathToType(path, mainPath, extensionMod);
            if (chunkType != ChunkType.Invalid)
            {
                if (sceneryEdited[(int)chunkType])
                {
                    return;
                }
                else
                {
                    sceneryEdited[(int)chunkType] = true;
                }
            }

            //Console.WriteLine(chunkType);

            TwinsFile SM_Archive = new TwinsFile();
            SM_Archive.LoadFile(path, smType);

            if (Option_RandWorldPalette.Enabled)
                Twins_Mods.SM_Rand_WorldPalette(SM_Archive, new ColorSwizzleData(randState));
            if (Option_GreyscaleWorld.Enabled)
                Twins_Mods.SM_Mod_GreyscaleWorld(SM_Archive);
            if (Option_UntexturedWorld.Enabled)
                Twins_Mods.SM_Mod_UntexturedWorld(SM_Archive);

            SM_Archive.SaveFile(path);
        }

        void Recursive_EditLevels(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    if (file.Extension.ToLower() == ".rm2" || file.Extension.ToLower() == ".rmx")
                    {
                        RM_EditLevel(file.FullName);
                    }
                }
                Recursive_EditLevels(dir);
            }
        }
        void Recursive_LoadLevels(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    if (file.Extension.ToLower() == ".rm2" || file.Extension.ToLower() == ".rmx")
                    {
                        RM_LoadLevel(file.FullName);
                    }
                }
                Recursive_LoadLevels(dir);
            }
        }
        void Recursive_EditScenery(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    if (file.Extension.ToLower() == ".sm2" || file.Extension.ToLower() == ".smx")
                    {
                        SM_EditLevel(file.FullName);
                    }
                }
                Recursive_EditScenery(dir);
            }
        }

        void EndModProcess()
        {
            CrateReplaceList.Clear();
            randCrateList.Clear();
            gemObjectList.Clear();
            musicTypes.Clear();
            randMusicList.Clear();

            // Build BD
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PS2)
            {
                BDArchive.CompileAll(System.IO.Path.Combine(ConsolePipeline.ExtractedPath, "CRASH6/CRASH"), bdPath);

                // Get rid of extracted files
                if (Directory.Exists(bdPath))
                {
                    DirectoryInfo di = new DirectoryInfo(bdPath);

                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }

                    Directory.Delete(bdPath);
                }
            }
        }
        
        void RM_LoadScripts(TwinsFile RM_Archive)
        {
            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        for (int i = 0; i < script_section.Records.Count; ++i)
                        {
                            Script scr = (Script)script_section.Records[i];
                            if (Twins_Data.allScripts.Count > 0)
                            {
                                bool check = false;
                                for (int d = 0; d < Twins_Data.allScripts.Count; d++)
                                {
                                    if (Twins_Data.allScripts[d].ID == scr.ID)
                                    {
                                        check = true;
                                    }
                                }
                                if (!check)
                                {
                                    Twins_Data.allScripts.Add(scr);
                                }
                            }
                            else
                            {
                                Twins_Data.allScripts.Add(scr);
                            }
                        }
                    }
                }
            }
        }
        
        void RM_LoadObjects(TwinsFile RM_Archive)
        {
            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                if (code_section.ContainsItem((uint)RM_Code_Sections.Object))
                {
                    TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
                    if (object_section.Records.Count > 0)
                    {
                        for (int i = 0; i < object_section.Records.Count; ++i)
                        {
                            GameObject scr = (GameObject)object_section.Records[i];
                            string[] nameFix = scr.Name.Split('|');
                            scr.Name = nameFix[nameFix.Length - 1];
                            scr.Name = scr.Name.Replace("act_", "");
                            if (Twins_Data.allObjects.Count > 0)
                            {
                                bool check = false;
                                for (int d = 0; d < Twins_Data.allObjects.Count; d++)
                                {
                                    if (Twins_Data.allObjects[d].ID == scr.ID)
                                    {
                                        check = true;
                                    }
                                }
                                if (!check)
                                {
                                    Twins_Data.allObjects.Add(scr);
                                }
                            }
                            else
                            {
                                Twins_Data.allObjects.Add(scr);
                            }
                        }
                    }
                }
            }
        }

        public class ColorSwizzleData
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

    }
}
