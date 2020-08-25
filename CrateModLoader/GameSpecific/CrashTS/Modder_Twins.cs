using CrateModLoader.GameSpecific.CrashTS;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
//Twinsanity API by NeoKesha, Smartkin, ManDude and Marko (https://github.com/Smartkin/twinsanity-editor)
//Version number, seed and options are displayed in the Autosave Disabled screen accessible by starting a new game without saving or just disabling autosave.
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
        Meshes = 2,
        Models = 3,
        ArmatureModel = 4,
        ActorModel = 5,
        StaticModel = 6,
        Terrains = 7,
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
        Misc = 0,
        Character = 1,
    }

    public sealed class Modder_Twins : Modder
    {
        internal const int RandomizeAllCrates       = 0;
        internal const int RandomizeCrateTypes      = 10;
        internal const int RandomizeGemLocations    = 1;
        internal const int RandomizeEnemies         = 2;
        internal const int RandomizeMusic           = 3;
        internal const int RandomizeCharParams      = 4;
        internal const int RandomizeStartingChunk   = 11;
        internal const int ModFlyingKick            = 5;
        internal const int ModStompKick             = 6;
        internal const int ModDoubleJumpCortex      = 7;
        internal const int ModDoubleJumpNina        = 8;
        internal const int ModEnableUnusedEnemies   = 9;
        internal const int ModSwitchCharacters = 12;

        public Modder_Twins()
        {
            Game = new Game()
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
                Icon = Properties.Resources.icon_crashts,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
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
                RegionID_XBOX = new RegionCode[] {
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
            };

            PropCategories.Add((int)ModProps.Misc, "Misc.");
            PropCategories.Add((int)ModProps.Character, "Character");

            AddOption(RandomizeCrateTypes, new ModOption(Twins_Text.Rand_CrateTypes, Twins_Text.Rand_CrateTypesDesc)); // TODO: Make this a toggle between CrateTypes/AllCrates in the mod menu?
            AddOption(RandomizeAllCrates, new ModOption(Twins_Text.Rand_Crates, Twins_Text.Rand_CratesDesc));
            AddOption(RandomizeGemLocations, new ModOption(Twins_Text.Rand_GemLocations, Twins_Text.Rand_GemLocationsDesc));
            //AddOption(RandomizeEnemies, new ModOption("Randomize Enemies (Soundless)")); // not stable enough
            AddOption(RandomizeMusic, new ModOption(Twins_Text.Rand_Music, Twins_Text.Rand_MusicDesc));
            AddOption(RandomizeCharParams, new ModOption(Twins_Text.Rand_CharParams, Twins_Text.Rand_CharParamsDesc));
            //AddOption(RandomizeStartingChunk, new ModOption("Randomize Starting Chunk")); // TODO
            AddOption(ModFlyingKick, new ModOption(Twins_Text.Mod_FlyingKick, Twins_Text.Mod_FlyingKickDesc));
            AddOption(ModStompKick, new ModOption(Twins_Text.Mod_StompKick, Twins_Text.Mod_StompKickDesc));
            AddOption(ModDoubleJumpCortex, new ModOption(Twins_Text.Mod_CortexDoubleJump, Twins_Text.Mod_CortexDoubleJumpDesc));
            AddOption(ModDoubleJumpNina, new ModOption(Twins_Text.Mod_NinaDoubleJump, Twins_Text.Mod_NinaDoubleJumpDesc));
            AddOption(ModEnableUnusedEnemies, new ModOption(Twins_Text.Mod_UnusedEnemies, Twins_Text.Mod_UnusedEnemiesDesc));
            AddOption(ModSwitchCharacters, new ModOption(Twins_Text.Mod_SwitchCharacters, Twins_Text.Mod_SwitchCharactersDesc));

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
        internal bool Edit_AllCharacters = false;
        internal Script StrafeLeft = null;
        internal Script StrafeRight = null;
        

        public override void StartModProcess()
        {

            // Extract BD (PS2 only)
            if (ModLoaderGlobals.Console == ConsoleMode.PS2)
            {
                bdPath = System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, "cml_extr/");
                Directory.CreateDirectory(bdPath);

                BDArchive.ExtractAll(System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, "CRASH6/CRASH"), bdPath);

                File.Delete(System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, "CRASH6/CRASH.BD"));
                File.Delete(System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, "CRASH6/CRASH.BH"));
                extensionMod = "2";
                rmType = TwinsFile.FileType.RM2;
                smType = TwinsFile.FileType.SM2;
            }
            else
            {
                bdPath = ModLoaderGlobals.ExtractedPath;
                extensionMod = "x";
                rmType = TwinsFile.FileType.RMX;
                smType = TwinsFile.FileType.SMX;
            }

            ModProcess();
        }

        protected override void ModProcess()
        {
            //Start Modding
            randState = new Random(ModLoaderGlobals.RandomizerSeed);
            
            Twins_Settings.PatchEXE();

            ModCrates.InstallLayerMods(bdPath, 1);

            bool Twins_Edit_CodeText = true;
            bool Twins_Edit_AllLevels = false;

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

            if (GetOption(RandomizeCrateTypes))
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
            if (GetOption(RandomizeAllCrates))
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

            if (GetOption(RandomizeAllCrates))
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

            if (GetOption(RandomizeGemLocations))
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

            if (GetOption(RandomizeMusic))
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

            if (GetOption(ModFlyingKick) || GetOption(ModStompKick) || GetOption(ModDoubleJumpCortex) || GetOption(ModDoubleJumpNina) || GetOption(ModSwitchCharacters))// || GetOption(RandomizeEnemies))
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


                if (GetOption(ModSwitchCharacters))
                {
                    StrafeLeft = Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT);
                    StrafeRight = Twins_Data.GetScriptByID(ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT);

                    Script.MainScript.ScriptCommand SwitchToCrashCommand = new Script.MainScript.ScriptCommand();
                    Script.MainScript.ScriptCommand SwitchToCortexCommand = new Script.MainScript.ScriptCommand();
                    Script.MainScript.ScriptCommand SwitchToNinaCommand = new Script.MainScript.ScriptCommand();
                    Script.MainScript.ScriptCommand SwitchToMechaCommand = new Script.MainScript.ScriptCommand();
                    SwitchToCrashCommand.VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter;
                    SwitchToCortexCommand.VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter;
                    SwitchToNinaCommand.VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter;
                    SwitchToMechaCommand.VTableIndex = (ushort)DefaultEnums.CommandID.SwitchCharacter;
                    SwitchToCrashCommand.arguments[1] = 0;
                    SwitchToCortexCommand.arguments[1] = 1;
                    SwitchToNinaCommand.arguments[1] = 3;
                    SwitchToMechaCommand.arguments[1] = 6;

                    StrafeLeft.Main.scriptState1.scriptStateBody.command = SwitchToCrashCommand;
                    StrafeRight.Main.scriptState1.scriptStateBody.command = SwitchToCortexCommand;

                }
                
                /*
                if (GetOption(RandomizeEnemies))
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
                        if (Twins_Data.cachedGameObjects[i].mainObject.cSounds.Length > 0)
                        {
                            for (int a = 0; a < Twins_Data.cachedGameObjects[i].mainObject.cSounds.Length; a++)
                            {
                                Twins_Data.cachedGameObjects[i].mainObject.cSounds[a] = 20 ;
                            }
                        }
                        if (Twins_Data.cachedGameObjects[i].mainObject.Sounds.Length > 0)
                        {
                            for (int s = 0; s < Twins_Data.cachedGameObjects[i].mainObject.Sounds.Length; s++)
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
                                list_actormodels = Twins_Data.cachedGameObjects[i].list_actormodels,
                                list_anims = Twins_Data.cachedGameObjects[i].list_anims,
                                list_armaturemodels = Twins_Data.cachedGameObjects[i].list_armaturemodels,
                                list_codemodels = Twins_Data.cachedGameObjects[i].list_codemodels,
                                list_materials = Twins_Data.cachedGameObjects[i].list_materials,
                                list_meshes = Twins_Data.cachedGameObjects[i].list_meshes,
                                list_models = Twins_Data.cachedGameObjects[i].list_models,
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
                */
            }
            

            if (GetOption(ModEnableUnusedEnemies))
            {
                Twins_Edit_AllLevels = true;
            }
            if (GetOption(RandomizeCharParams))
            {
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Crash, ref randState);
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Cortex, ref randState);
                Twins_Data_Characters.Twins_Randomize_Character((int)CharacterID.Nina, ref randState);
                Twins_Edit_AllLevels = true;
            }

            if (Twins_Edit_AllLevels)
            {
                levelEdited = new bool[140];

                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_EditLevels(di);
                }

                Twins_Data.allScripts.Clear();
                Twins_Data.allObjects.Clear();
                Twins_Data.cachedGameObjects.Clear();
            }

            if (Twins_Edit_CodeText)
            {
                string[] CodeText;
                if (ModLoaderGlobals.Region == RegionType.NTSC_U)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/American.txt", Encoding.Default);
                }
                else if (ModLoaderGlobals.Region == RegionType.PAL)
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
                        CodeText_LineList[i] = "to enable autosave,~return to the pause menu~and re-save the game.~crate mod loader " + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "seed: " + ModLoaderGlobals.RandomizerSeed + "~" + "options: " + OptionsSelectedString.ToLower() + "";
                    }
                    else if (CodeText_LineList[i] == "autosave disabled")
                    {
                        CodeText_LineList[i] = "autosave disabled~";
                    }
                    else if (i == 39 && ModLoaderGlobals.Region == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~";
                    }
                    else if (i == 40 && ModLoaderGlobals.Region == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~" + ModLoaderGlobals.ProgramVersion.ToLower() + "~" + "" + ModLoaderGlobals.RandomizerSeed + "~" + "" + OptionsSelectedString.ToLower() + "";
                    }
                }

                CodeText = new string[CodeText_LineList.Count];
                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    CodeText[i] = CodeText_LineList[i];
                }

                if (ModLoaderGlobals.Region == RegionType.NTSC_U)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/American.txt", CodeText, Encoding.Default);
                }
                else if (ModLoaderGlobals.Region == RegionType.PAL)
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


        void RM_EditLevel(string path)
        {
            string mainPath = System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, @"cml_extr\");
            if (ModLoaderGlobals.Console == ConsoleMode.XBOX)
            {
                mainPath = ModLoaderGlobals.ExtractedPath;
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

            if (GetOption(RandomizeAllCrates))
                Twins_Randomizers.RM_Randomize_Crates(RM_Archive, chunkType, ref randState, ref CrateReplaceList, ref randCrateList);
            if (GetOption(RandomizeGemLocations))
                Twins_Randomizers.RM_Randomize_Gems(RM_Archive, chunkType, ref gemObjectList);
            if (GetOption(RandomizeMusic))
                Twins_Randomizers.RM_Randomize_Music(RM_Archive, ref musicTypes, ref randMusicList);
            if (GetOption(RandomizeCharParams) || Edit_AllCharacters)
                Twins_Randomizers.RM_Randomize_CharacterInstanceStats(RM_Archive);

            /*
            if (GetOption(RandomizeEnemies))
                Twins_Randomizers.RM_Randomize_Enemies(RM_Archive, chunkType, ref randState, ref EnemyReplaceList, ref EnemyInsertList);
            */

            if (GetOption(ModStompKick))
                Twins_Mods.RM_CharacterObjectMod(RM_Archive);
            if (GetOption(ModFlyingKick) || GetOption(ModStompKick))
                Twins_Mods.RM_CharacterMod_EnableFlyingKick(RM_Archive);
            if (GetOption(ModDoubleJumpCortex))
                Twins_Mods.RM_CharacterMod_DoubleJumpCortex(RM_Archive);
            if (GetOption(ModDoubleJumpNina))
                Twins_Mods.RM_CharacterMod_DoubleJumpNina(RM_Archive);
            if (GetOption(ModEnableUnusedEnemies))
                Twins_Mods.RM_EnableUnusedEnemies(RM_Archive);

            if (GetOption(ModSwitchCharacters))
            {
                Twins_Mods.RM_SwitchCharactersMod(RM_Archive, StrafeLeft, StrafeRight);
            }

            RM_Archive.SaveFile(path);
        }
        
        void RM_LoadLevel(string path)
        {
            string mainPath = System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, @"cml_extr\");
            if (ModLoaderGlobals.Console == ConsoleMode.XBOX)
            {
                mainPath = ModLoaderGlobals.ExtractedPath;
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

        protected override void EndModProcess()
        {
            CrateReplaceList.Clear();
            randCrateList.Clear();
            gemObjectList.Clear();
            musicTypes.Clear();
            randMusicList.Clear();

            // Build BD
            if (ModLoaderGlobals.Console == ConsoleMode.PS2)
            {
                BDArchive.CompileAll(System.IO.Path.Combine(ModLoaderGlobals.ExtractedPath, "CRASH6/CRASH"), bdPath);

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

    }
}
