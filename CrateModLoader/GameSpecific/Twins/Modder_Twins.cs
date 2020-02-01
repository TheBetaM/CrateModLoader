using CrateModLoader.GameSpecific.Twins;
using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
//Twinsanity API by NeoKesha, Smartkin, ManDude and Marko (https://github.com/Smartkin/twinsanity-editor)
//Version number, seed and options are displayed in the Autosave Disabled screen accessible by starting a new game without saving or just disabling autosave.

namespace CrateModLoader
{
    public sealed class Modder_Twins : Modder
    {
        internal const int RandomizeAllCrates       = 0;
        internal const int RandomizeCrateTypes      = 10;
        internal const int RandomizeGemLocations    = 1;
        internal const int RandomizeEnemies         = 2;
        internal const int RandomizeMusic           = 3;
        internal const int RandomizeCharParams      = 4;
        internal const int ModFlyingKick            = 5;
        internal const int ModStompKick             = 6;
        internal const int ModDoubleJumpCortex      = 7;
        internal const int ModDoubleJumpNina        = 8;
        internal const int ModEnableUnusedEnemies   = 9;

        public Modder_Twins()
        {
            Game = new Game()
            {
                Name = "Crash Twinsanity",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                },
                API_Credit = "API by NeoKesha, Smartkin, ManDude and Marko",
                Icon = Properties.Resources.icon_twins,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_209.09;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_525.68;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_658.01;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                },
                RegionID_XBOX = new RegionCode[] {
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
            };

            Options.Add(RandomizeCrateTypes, new ModOption("Randomize Crate Types")); // TODO: Make this a toggle between CrateTypes/AllCrates in the mod menu?
            Options.Add(RandomizeAllCrates, new ModOption("Randomize Individual Crates"));
            Options.Add(RandomizeGemLocations, new ModOption("Randomize Gem Locations"));
            //Options.Add(RandomizeEnemies, new ModOption("Randomize Enemies")); // TODO
            Options.Add(RandomizeMusic, new ModOption("Randomize Level Music"));
            Options.Add(RandomizeCharParams, new ModOption("Randomize Character Parameters"));
            Options.Add(ModFlyingKick, new ModOption("Enable Flying Kick for Crash (Jump + Circle)"));
            Options.Add(ModStompKick, new ModOption("Enable Stomp Kick for Crash (Flying Kick variation)"));
            Options.Add(ModDoubleJumpCortex, new ModOption("Enable Double Jump for Cortex"));
            Options.Add(ModDoubleJumpNina, new ModOption("Enable Double Jump for Nina"));
            Options.Add(ModEnableUnusedEnemies, new ModOption("Enable Some Unused Enemies")); // TODO: frogensteins, ants in coreEnt
        }

        internal string bdPath = "";
        internal Random randState = new Random();
        internal List<uint> CrateReplaceList = new List<uint>();
        internal List<uint> randCrateList = new List<uint>();
        internal List<uint> gemObjectList = new List<uint>();
        internal List<uint> musicTypes = new List<uint>();
        internal List<uint> randMusicList = new List<uint>();
        internal List<Twins_Data.ObjectID> EnemyReplaceList = new List<Twins_Data.ObjectID>();
        internal List<Twins_Data.ObjectID> EnemyInsertList = new List<Twins_Data.ObjectID>();
        internal bool[] levelEdited;

        private enum RM2_Sections
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
        public enum RM2_Graphics_Sections
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
        public enum RM2_Code_Sections
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
        public enum RM2_Instance_Sections
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

        public override void StartModProcess()
        {
            if (Program.ModProgram.isoType == ConsoleMode.XBOX)
            {
                // No need to extract BD/BH on Xbox, but RMX/SMX level files are not working in the API at the moment
                return;
            }

            // Extract BD
            bdPath = System.IO.Path.Combine(Program.ModProgram.extractedPath, "cml_extr/");
            Directory.CreateDirectory(bdPath);

            BDArchive.ExtractAll(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH"), bdPath);

            File.Delete(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH.BD"));
            File.Delete(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH.BH"));

            ModProcess();
        }

        protected override void ModProcess()
        {
            //Start Modding
            randState = new Random(Program.ModProgram.randoSeed);

            bool Twins_Edit_CodeText = true;
            bool Twins_Edit_AllLevels = false;

            if (Options[RandomizeCrateTypes].Enabled)
            {
                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm2", TwinsFile.FileType.RM2);

                List<uint> crateList = new List<uint>();
                List<uint> posList = new List<uint>();

                crateList.Add((uint)Twins_Data.ObjectID.BASICCRATE);
                //crateList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //crateList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                crateList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                crateList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                crateList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                crateList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                //crateList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                //crateList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_NINA);
                crateList.Add((uint)Twins_Data.ObjectID.IRONCRATE);
                crateList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE);
                crateList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE);
                crateList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);

                posList.Add((uint)Twins_Data.ObjectID.BASICCRATE);
                //posList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //posList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                posList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                posList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                posList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                posList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                //posList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                //posList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_NINA);
                //posList.Add((uint)Twins_Data.ObjectID.IRONCRATE);
                //posList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE);
                posList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE);
                posList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);

                int target_item = 0;

                while (posList.Count > 0)
                {
                    target_item = randState.Next(0, crateList.Count);
                    TwinsSection objectdata = mainArchive.GetItem<TwinsSection>((uint)RM2_Sections.Code).GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
                    if (objectdata.ContainsItem(posList[0]))
                        objectdata.GetItem<TwinsItem>(posList[0]).ID = crateList[target_item];
                    posList.RemoveAt(0);
                    crateList.RemoveAt(target_item);
                }
                posList.Clear();
                crateList.Clear();

                mainArchive.SaveFile(bdPath + "/Startup/Default.rm2");
            }
            if (Options[RandomizeAllCrates].Enabled)
            {
                //Importing ammo crate
                TwinsFile cortexlevelArchive = new TwinsFile();
                cortexlevelArchive.LoadFile(bdPath + @"Levels\school\Cortex\cogpa01.rm2", TwinsFile.FileType.RM2);

                // when exporting is done
                //List<Twins_Data.ObjectID> exportList = new List<Twins_Data.ObjectID>();
                //Twins_Data.ExportGameObject(ref cortexlevelArchive, Twins_Data.ObjectID.AMMOCRATESMALL,ref exportList);
                //exportList.Clear();

                List<GameObject> import_GObj = new List<GameObject>();
                List<Texture> import_Tex = new List<Texture>();
                List<Material> import_Mat = new List<Material>();
                List<Mesh> import_Mesh = new List<Mesh>();
                List<Model> import_Mdl = new List<Model>();
                List<Script> import_Scr = new List<Script>();
                List<TwinsItem> import_OGI = new List<TwinsItem>();

                TwinsSection gfx_section = cortexlevelArchive.GetItem<TwinsSection>((uint)RM2_Sections.Graphics);
                TwinsSection code_section = cortexlevelArchive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
                TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
                TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
                TwinsSection ogi_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.OGI);
                TwinsSection tex_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Textures);
                TwinsSection mat_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Materials);
                TwinsSection mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Meshes);
                TwinsSection mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Models);
                for (int i = 0; i < script_section.Records.Count; ++i)
                {
                    Script scr = (Script)script_section.Records[i];
                    if (scr.ID == (uint)Twins_Data.ScriptID.HEAD_COM_AMMO_CRATE_SMALL_TOUCHED)
                    {
                        import_Scr.Add(scr);
                    }
                    else if (scr.ID == (uint)Twins_Data.ScriptID.COM_AMMO_CRATE_SMALL_TOUCHED)
                    {
                        import_Scr.Add(scr);
                    }
                }
                for (int i = 0; i < object_section.Records.Count; ++i)
                {
                    GameObject obj = (GameObject)object_section.Records[i];
                    if (obj.ID == (uint)Twins_Data.ObjectID.AMMOCRATESMALL)
                    {
                        obj.Scripts[(uint)Twins_Data.GameObjectScriptOrder.OnPhysicsCollision] = 65535;
                        obj.Scripts[(uint)Twins_Data.GameObjectScriptOrder.OnTouch] = 65535;
                        obj.Scripts[(uint)Twins_Data.GameObjectScriptOrder.OnDamage] = (ushort)Twins_Data.ScriptID.HEAD_COM_AMMO_CRATE_SMALL_TOUCHED;
                        obj.Scripts[(uint)Twins_Data.GameObjectScriptOrder.OnLand] = (ushort)Twins_Data.ScriptID.HEAD_COM_BASIC_CRATE_LANDED_ON;
                        import_GObj.Add(obj);
                    }
                }
                for (int i = 0; i < ogi_section.Records.Count; ++i)
                {
                    TwinsItem obj = (TwinsItem)ogi_section.Records[i];
                    if (obj.ID == 1012 || obj.ID == 1013)
                    {
                        import_OGI.Add(obj);
                    }
                }
                for (int i = 0; i < tex_section.Records.Count; ++i)
                {
                    Texture obj = (Texture)tex_section.Records[i];
                    if (obj.ID == 579096643 || obj.ID == 1337357917)
                    {
                        import_Tex.Add(obj);
                    }
                }
                for (int i = 0; i < mat_section.Records.Count; ++i)
                {
                    Material obj = (Material)mat_section.Records[i];
                    if (obj.ID == 3145594139 || obj.ID == 2974101469 || obj.ID == 755441073 || obj.ID == 2631436731)
                    {
                        import_Mat.Add(obj);
                    }
                }
                for (int i = 0; i < mesh_section.Records.Count; ++i)
                {
                    Mesh obj = (Mesh)mesh_section.Records[i];
                    if (obj.ID == 4014807021 || obj.ID == 847180949 || obj.ID == 1222385729 || obj.ID == 1597590509 || obj.ID == 1972795289 || obj.ID == 2348000069)
                    {
                        import_Mesh.Add(obj);
                    }
                    else if (obj.ID == 2723204849 || obj.ID == 3098409629 || obj.ID == 3473614409 || obj.ID == 3848819189 || obj.ID == 4224023969)
                    {
                        import_Mesh.Add(obj);
                    }
                }
                for (int i = 0; i < mdl_section.Records.Count; ++i)
                {
                    Model obj = (Model)mdl_section.Records[i];
                    if (obj.ID == 2727310987 || obj.ID == 991942702 || obj.ID == 1367147482 || obj.ID == 1742352262 || obj.ID == 2117557042 || obj.ID == 2492761822)
                    {
                        import_Mdl.Add(obj);
                    }
                    else if (obj.ID == 2867966602 || obj.ID == 3243171382 || obj.ID == 3618376162 || obj.ID == 3993580942 || obj.ID == 73818426)
                    {
                        import_Mdl.Add(obj);
                    }
                }

                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm2", TwinsFile.FileType.RM2);

                // when importing is done
                //Twins_Data.ImportGameObject(ref mainArchive, Twins_Data.ObjectID.AMMOCRATESMALL,ref exportList);
                //exportList.Clear();

                gfx_section = mainArchive.GetItem<TwinsSection>((uint)RM2_Sections.Graphics);
                code_section = mainArchive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
                object_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
                script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
                ogi_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.OGI);
                tex_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Textures);
                mat_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Materials);
                mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Meshes);
                mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Models);

                for (int i = 0; i < import_GObj.Count; i++)
                {
                    object_section.Records.Add(import_GObj[i]);
                }
                for (int i = 0; i < import_Mat.Count; i++)
                {
                    mat_section.Records.Add(import_Mat[i]);
                }
                for (int i = 0; i < import_Mdl.Count; i++)
                {
                    mdl_section.Records.Add(import_Mdl[i]);
                }
                for (int i = 0; i < import_Mesh.Count; i++)
                {
                    mesh_section.Records.Add(import_Mesh[i]);
                }
                for (int i = 0; i < import_Scr.Count; i++)
                {
                    script_section.Records.Add(import_Scr[i]);
                }
                for (int i = 0; i < import_Tex.Count; i++)
                {
                    tex_section.Records.Add(import_Tex[i]);
                }
                for (int i = 0; i < import_OGI.Count; i++)
                {
                    ogi_section.Records.Add(import_OGI[i]);
                }

                mainArchive.SaveFile(bdPath + "/Startup/Default.rm2");
            }

            if (Options[RandomizeAllCrates].Enabled)
            {

                // Crates to insert
                randCrateList = new List<uint>();
                randCrateList.Add((uint)Twins_Data.ObjectID.BASICCRATE);
                //randCrateList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //randCrateList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.IRONCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.AMMOCRATESMALL);

                // Crates to replace
                CrateReplaceList = new List<uint>();
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.BASICCRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.IRON_CRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.IRON_SPRING_CRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.AMMOCRATESMALL);

                Twins_Edit_AllLevels = true;
            }

            if (Options[RandomizeGemLocations].Enabled)
            {
                Twins_Data.Twins_Randomize_Gems(ref randState);

                gemObjectList = new List<uint>();
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_BLUE);
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_CLEAR);
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_GREEN);
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_PURPLE);
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_RED);
                gemObjectList.Add((uint)Twins_Data.ObjectID.GEM_YELLOW);

                Twins_Edit_AllLevels = true;
            }

            if (Options[RandomizeMusic].Enabled)
            {
                List<uint> temp_musicList = new List<uint>();

                musicTypes.Add((uint)Twins_Data.MusicID.Academy);
                musicTypes.Add((uint)Twins_Data.MusicID.AcademyNoLaugh);
                musicTypes.Add((uint)Twins_Data.MusicID.AltLab);
                musicTypes.Add((uint)Twins_Data.MusicID.AntAgony);
                musicTypes.Add((uint)Twins_Data.MusicID.BeeChase);
                musicTypes.Add((uint)Twins_Data.MusicID.Boiler);
                musicTypes.Add((uint)Twins_Data.MusicID.BoilerUnused);
                musicTypes.Add((uint)Twins_Data.MusicID.BossAmberly);
                musicTypes.Add((uint)Twins_Data.MusicID.BossDingodile);
                musicTypes.Add((uint)Twins_Data.MusicID.BossNGin);
                musicTypes.Add((uint)Twins_Data.MusicID.BossTikimon);
                musicTypes.Add((uint)Twins_Data.MusicID.BossTwins);
                musicTypes.Add((uint)Twins_Data.MusicID.BossUka);
                musicTypes.Add((uint)Twins_Data.MusicID.BP);
                musicTypes.Add((uint)Twins_Data.MusicID.Cavern);
                musicTypes.Add((uint)Twins_Data.MusicID.ClassroomCortex);
                musicTypes.Add((uint)Twins_Data.MusicID.ClassroomCrash);
                musicTypes.Add((uint)Twins_Data.MusicID.Henchmania);
                musicTypes.Add((uint)Twins_Data.MusicID.Hijinks);
                musicTypes.Add((uint)Twins_Data.MusicID.IcebergLab);
                musicTypes.Add((uint)Twins_Data.MusicID.IcebergLabFast);
                musicTypes.Add((uint)Twins_Data.MusicID.IceClimb);
                musicTypes.Add((uint)Twins_Data.MusicID.MechaBandicoot);
                musicTypes.Add((uint)Twins_Data.MusicID.Rockslide);
                musicTypes.Add((uint)Twins_Data.MusicID.Rooftop);
                musicTypes.Add((uint)Twins_Data.MusicID.SlipSlide);
                musicTypes.Add((uint)Twins_Data.MusicID.TitleTheme);
                musicTypes.Add((uint)Twins_Data.MusicID.TotemRiver);
                musicTypes.Add((uint)Twins_Data.MusicID.TwinsanityIsland);
                musicTypes.Add((uint)Twins_Data.MusicID.WalrusChase);
                musicTypes.Add((uint)Twins_Data.MusicID.WormChase);
                int targetPos = 0;

                for (int i = 0; i < musicTypes.Count; i++)
                {
                    temp_musicList.Add(musicTypes[i]);
                }
                while (musicTypes.Count > 0)
                {
                    targetPos = randState.Next(0, musicTypes.Count);
                    randMusicList.Add(musicTypes[targetPos]);
                    musicTypes.RemoveAt(targetPos);
                }

                musicTypes.Clear();
                for (int i = 0; i < temp_musicList.Count; i++)
                {
                    musicTypes.Add(temp_musicList[i]);
                }

                Twins_Edit_AllLevels = true;
            }

            if (Options[ModFlyingKick].Enabled || Options[ModStompKick].Enabled || Options[ModDoubleJumpCortex].Enabled || Options[ModDoubleJumpNina].Enabled) // || Options[RandomizeEnemies].Enabled)
            {
                Twins_Edit_AllLevels = true;

                levelEdited = new bool[140];

                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_LoadLevels(di);
                }

                TwinsFile mainArchiveLoad = new TwinsFile();
                mainArchiveLoad.LoadFile(bdPath + @"Startup\Default.rm2", TwinsFile.FileType.RM2);

                RM_LoadScripts(mainArchiveLoad);
                RM_LoadObjects(mainArchiveLoad);

                Twins_Data.allScripts.Sort((x, y) => x.ID.CompareTo(y.ID));
                Twins_Data.allObjects.Sort((x, y) => x.ID.CompareTo(y.ID));

                /*
                if (Options[RandomizeEnemies].Enabled)
                {
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_MONKEY);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_CHICKEN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_CRAB);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_SKUNK);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.EARTH_TRIBESMAN_SHIELDBEARER);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.EARTH_TRIBESMAN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_BAT_DARKPURPLE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_BAT_ICE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.PIRANHAPLANT);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_PIG_WILDBOAR);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_INTERMEDIATE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_DARKBROWN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_DARKPURPLE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_GREY);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_LIGHTBROWN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_LIGHTPURPLE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_MEDIUMBROWN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_RAT_WHITE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.PENGUIN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_CORTEX_CAMERABOT);
                    //EnemyReplaceList.Add(Twins_Data.ObjectID.RHINO_PIRATE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.SCHOOL_DOG);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_COCKROACH);
                    //EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_BEETLE_DARKPURPLE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.GLOBAL_BEETLE_PROJECTILE);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.SCHOOL_FROGENSTEIN);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.SCHOOL_ZOMBOT);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_BASIC);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_DRILLER);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_FLAMER);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_FLYER);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_SOLDIER);
                    EnemyReplaceList.Add(Twins_Data.ObjectID.DRONE_BERSERKER);

                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_MONKEY);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_CHICKEN);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_CRAB);
                    //EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_SKUNK); // todo: skunk requires 2 positions? training skunk 1 position
                    EnemyInsertList.Add(Twins_Data.ObjectID.EARTH_TRIBESMAN_SHIELDBEARER);
                    EnemyInsertList.Add(Twins_Data.ObjectID.EARTH_TRIBESMAN);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_BAT_DARKPURPLE);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_BAT_ICE);
                    EnemyInsertList.Add(Twins_Data.ObjectID.PIRANHAPLANT);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_PIG_WILDBOAR);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_RAT_INTERMEDIATE);
                    //EnemyInsertList.Add(Twins_Data.ObjectID.MINI_MON); //todo: missing instance values for a generic one
                    EnemyInsertList.Add(Twins_Data.ObjectID.PENGUIN);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_CORTEX_CAMERABOT);
                    //EnemyInsertList.Add(Twins_Data.ObjectID.RHINO_PIRATE); //todo: missing instance values for a generic one
                    EnemyInsertList.Add(Twins_Data.ObjectID.SCHOOL_DOG);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_COCKROACH);
                    //EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_BEETLE_DARKPURPLE);
                    EnemyInsertList.Add(Twins_Data.ObjectID.GLOBAL_BEETLE_PROJECTILE);
                    //EnemyInsertList.Add(Twins_Data.ObjectID.SCHOOL_FROGENSTEIN); // todo: enable frogensteins
                    EnemyInsertList.Add(Twins_Data.ObjectID.SCHOOL_ZOMBOT);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_BASIC);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_DRILLER);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_FLAMER);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_FLYER);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_SOLDIER);
                    EnemyInsertList.Add(Twins_Data.ObjectID.DRONE_BERSERKER);

                    for (int i = 0; i < Twins_Data.cachedGameObjects.Count; i++)
                    {
                        if (Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)Twins_Data.ObjectID.GLOBAL_BAT_DARKPURPLE)
                        {
                            if (Twins_Data.cachedGameObjects[i].instanceTemplate.Properties > (uint)Twins_Data.PropertyFlags.DisableObject)
                            {
                                Twins_Data.cachedGameObjects[i] = new CachedGameObject()
                                {
                                    mainObject = Twins_Data.cachedGameObjects[i].mainObject,
                                    instanceTemplate = new InstanceTemplate()
                                    {
                                        Properties = Twins_Data.cachedGameObjects[i].instanceTemplate.Properties - (uint)Twins_Data.PropertyFlags.DisableObject,
                                        Flags = Twins_Data.cachedGameObjects[i].instanceTemplate.Flags,
                                        FloatVars = Twins_Data.cachedGameObjects[i].instanceTemplate.FloatVars,
                                        InstanceIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.InstanceIDs,
                                        InstancesNum = Twins_Data.cachedGameObjects[i].instanceTemplate.InstancesNum,
                                        IntVars = Twins_Data.cachedGameObjects[i].instanceTemplate.IntVars,
                                        ObjectID = Twins_Data.cachedGameObjects[i].instanceTemplate.ObjectID,
                                        PathIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.PathIDs,
                                        PathsNum = Twins_Data.cachedGameObjects[i].instanceTemplate.PathsNum,
                                        PositionIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.PositionIDs,
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
                }
                */

            }

            if (Options[ModEnableUnusedEnemies].Enabled)
            {
                Twins_Edit_AllLevels = true;
            }
            if (Options[RandomizeCharParams].Enabled)
            {
                Twins_Data.Twins_Randomize_Character((int)Twins_Data.CharacterID.Crash, ref randState);
                Twins_Data.Twins_Randomize_Character((int)Twins_Data.CharacterID.Cortex, ref randState);
                Twins_Data.Twins_Randomize_Character((int)Twins_Data.CharacterID.Nina, ref randState);
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
                if (Program.ModProgram.targetRegion == RegionType.NTSC_U)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/American.txt");
                }
                else if (Program.ModProgram.targetRegion == RegionType.PAL)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/English.txt");
                }
                else
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/Japanese.txt", System.Text.Encoding.Default);
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
                        CodeText_LineList[i] = "to enable autosave,~return to the pause menu~and re-save the game.~crate mod loader " + Program.ModProgram.releaseVersionString.ToLower() + "~" + "seed: " + Program.ModProgram.randoSeed + "~" + "options: " + Program.ModProgram.optionsSelectedString.ToLower() + "";
                    }
                    else if (CodeText_LineList[i] == "autosave disabled")
                    {
                        CodeText_LineList[i] = "autosave disabled~";
                    }
                    else if (i == 39 && Program.ModProgram.targetRegion == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~";
                    }
                    else if (i == 40 && Program.ModProgram.targetRegion == RegionType.NTSC_J)
                    {
                        CodeText_LineList[i] += "~" + Program.ModProgram.releaseVersionString.ToLower() + "~" + "" + Program.ModProgram.randoSeed + "~" + "" + Program.ModProgram.optionsSelectedString.ToLower() + "";
                    }
                }

                CodeText = new string[CodeText_LineList.Count];
                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    CodeText[i] = CodeText_LineList[i];
                }

                if (Program.ModProgram.targetRegion == RegionType.NTSC_U)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/American.txt", CodeText);
                }
                else if (Program.ModProgram.targetRegion == RegionType.PAL)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/English.txt", CodeText);
                }
                else
                {
                    File.WriteAllLines(bdPath + "/Language/Code/Japanese.txt", CodeText, System.Text.Encoding.Default);
                }
            }

            EndModProcess();
        }


        void RM_EditLevel(string path)
        {
            Twins_Data.ChunkType chunkType = Twins_Data.ChunkPathToType(path, System.IO.Path.Combine(Program.ModProgram.extractedPath, @"cml_extr\"));
            if (chunkType != Twins_Data.ChunkType.Invalid)
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
            RM_Archive.LoadFile(path, TwinsFile.FileType.RM2);

            if (Options[RandomizeAllCrates].Enabled)
            {
                RM_Randomize_Crates(RM_Archive, chunkType);
            }
            if (Options[RandomizeGemLocations].Enabled)
            {
                RM_Randomize_Gems(RM_Archive, chunkType);
            }
            if (Options[RandomizeMusic].Enabled)
            {
                RM_Randomize_Music(RM_Archive);
            }
            /*
            if (Options[RandomizeEnemies].Enabled)
            {
                RM_Randomize_Enemies(RM_Archive);
            }
            */
            if (Options[ModStompKick].Enabled)
            {
                RM_CharacterObjectMod(RM_Archive);
            }
            if (Options[ModFlyingKick].Enabled || Options[ModStompKick].Enabled || Options[ModDoubleJumpNina].Enabled || Options[ModDoubleJumpCortex].Enabled || Options[RandomizeCharParams].Enabled)
            {
                RM_CharacterMod(RM_Archive);
            }
            if (Options[ModEnableUnusedEnemies].Enabled)
            {
                RM_EnableUnusedEnemies(RM_Archive);
            }

            RM_Archive.SaveFile(path);
        }
        
        void RM_LoadLevel(string path)
        {
            Twins_Data.ChunkType chunkType = Twins_Data.ChunkPathToType(path, System.IO.Path.Combine(Program.ModProgram.extractedPath, @"cml_extr\"));
            if (chunkType != Twins_Data.ChunkType.Invalid)
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
            RM_Archive.LoadFile(path, TwinsFile.FileType.RM2);

            RM_LoadScripts(RM_Archive);
            RM_LoadObjects(RM_Archive);

            /*
            if (Options[RandomizeEnemies].Enabled)
            {
                List<Twins_Data.ObjectID> ExportedObjects = new List<Twins_Data.ObjectID>();
                if (chunkType == Twins_Data.ChunkType.Earth_Hub_Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_MONKEY, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_CHICKEN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_CRAB, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_SKUNK, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.PIRANHAPLANT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_BAT_DARKPURPLE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.EARTH_TRIBESMAN_SHIELDBEARER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Totem_L03Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.EARTH_TRIBESMAN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_PIG_WILDBOAR, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_HighSeas_GPA07)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_RAT_INTERMEDIATE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Earth_Hub_HubD)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.MINI_MON, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_IceClimb_BergExt)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.PENGUIN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_CORTEX_CAMERABOT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_BAT_ICE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.Ice_HighSeas_GPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.RHINO_PIRATE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrGPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.SCHOOL_FROGENSTEIN, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrGPA04)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.SCHOOL_DOG, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Crash_CrLib)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.SCHOOL_ZOMBOT, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Boiler_Boiler_X)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_COCKROACH, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.GLOBAL_BEETLE_PROJECTILE, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.AltEarth_Core_CoreA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_BERSERKER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_DRILLER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_SOLDIER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.AltEarth_Core_CoreB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_FLYER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_FLAMER, ref ExportedObjects);
                }
                else if (chunkType == Twins_Data.ChunkType.School_Rooftop_BusChase)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.DRONE_BASIC, ref ExportedObjects);
                }
                //Twins_Data.ExportGameObject(ref RM_Archive, Twins_Data.ObjectID.SCHOOL_JANITOR, ref ExportedObjects);
            }
            */
        }

        void Recursive_EditLevels(DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    if (file.Extension == ".rm2" || file.Extension == ".RM2")
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
                    if (file.Extension == ".rm2" || file.Extension == ".RM2")
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
            BDArchive.CompileAll(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH"), bdPath);

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

            // Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1");
            //File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1");
        }


        void RM_Randomize_Crates(TwinsFile RM_Archive, Twins_Data.ChunkType chunkType)
        {
            randState = new Random((Program.ModProgram.randoSeed + (int)chunkType) % int.MaxValue);
            List<uint> lifecrates = new List<uint>
            {
                (uint)Twins_Data.ObjectID.EXTRALIFECRATE,
                (uint)Twins_Data.ObjectID.EXTRALIFECRATECORTEX,
                (uint)Twins_Data.ObjectID.EXTRALIFECRATENINA
            };
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int d = 0; d < CrateReplaceList.Count; d++)
                        {
                            if (instance.ObjectID == CrateReplaceList[d])
                            {
                                int target_item = randState.Next(0, randCrateList.Count);
                                if (randCrateList[target_item] == (int)Twins_Data.ObjectID.EXTRALIFECRATE)
                                {
                                    int target_life = randState.Next(0, lifecrates.Count);
                                    target_item = (int)lifecrates[target_life];
                                }
                                else
                                {
                                    target_item = (int)randCrateList[target_item];
                                }

                                if (target_item == (int)Twins_Data.ObjectID.AMMOCRATESMALL)
                                {
                                    instance.UnkI32 = 0x4011E;
                                    instance.UnkI322 = new List<float>() { 1, 50, 10 };
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.BASICCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.EXTRALIFECRATE || target_item == (int)Twins_Data.ObjectID.EXTRALIFECRATECORTEX || target_item == (int)Twins_Data.ObjectID.EXTRALIFECRATENINA)
                                {
                                    instance.UnkI32 = 0x81DE;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.WOODENSPRINGCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.REINFORCEDWOODENCRATE)
                                {
                                    instance.UnkI32 = 0xD91E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.AKUAKUCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.IRONCRATE)
                                {
                                    instance.UnkI32 = 0x7D1E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.IRONSPRINGCRATE)
                                {
                                    instance.UnkI32 = 0x7D1E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.MULTIPLEHITCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.SURPRISECRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.TNTCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)Twins_Data.ObjectID.NITROCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }

                                instance.ObjectID = (ushort)target_item;
                                break;
                            }
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        void RM_Randomize_Gems(TwinsFile RM_Archive, Twins_Data.ChunkType chunkType)
        {
            if (chunkType == Twins_Data.ChunkType.Invalid)
            {
                Console.WriteLine("INVALID CHUNK FILE: " + RM_Archive.FileName);
                return;
            }

            // Part 1: Remove existing gems

            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int d = 0; d < gemObjectList.Count; d++)
                        {
                            if (instance.ObjectID == gemObjectList[d])
                            {
                                instance.Pos.Y = instance.Pos.Y - 1000f; //todo: figure out how to get rid of them gracefully

                                /* Used this to generate vanilla gem locations instead of checking one-by-one
                                if (instance.ObjectID == (ushort)Twins_Data.GemID.GEM_BLUE)
                                {
                                    Console.WriteLine("new TwinsGem(ChunkType." + chunkType + ",GemType.GEM_BLUE,new Vector3(" + instance.Pos.X + "f," + instance.Pos.Y + "f," + instance.Pos.Z + "f)),");
                                }
                                */

                                break;
                            }
                        }
                        instances.Records[i] = instance;
                    }
                }
            }

            // Part 2: Add new gems
            uint gem_section_id = (uint)RM2_Sections.Instances1;
            if (!RM_Archive.ContainsItem(gem_section_id)) return;
            TwinsSection instances_group = RM_Archive.GetItem<TwinsSection>(gem_section_id);
            TwinsSection instances_section;
            if (instances_group.Records.Count > 0)
            {
                if (!instances_group.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) return;
                instances_section = instances_group.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
            }
            else
            {
                return;
            }


            for (int i = 0; i < Twins_Data.All_Gems.Count; i++)
            {
                if (Twins_Data.All_Gems[i].chunk == chunkType)
                {
                    Instance NewGem = new Instance();
                    NewGem.Pos = new Pos(Twins_Data.All_Gems[i].pos.X, Twins_Data.All_Gems[i].pos.Y, Twins_Data.All_Gems[i].pos.Z, 1f);
                    if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_BLUE)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_BLUE;
                    }
                    else if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_CLEAR)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_CLEAR;
                    }
                    else if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_GREEN)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_GREEN;
                    }
                    else if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_PURPLE)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_PURPLE;
                    }
                    else if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_RED)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_RED;
                    }
                    else if (Twins_Data.All_Gems[i].type == Twins_Data.GemType.GEM_YELLOW)
                    {
                        NewGem.ObjectID = (ushort)Twins_Data.GemID.GEM_YELLOW;
                    }
                    NewGem.ID = (uint)instances_section.Records.Count;
                    NewGem.SomeNum1 = 10;
                    NewGem.SomeNum2 = 10;
                    NewGem.SomeNum3 = 10;
                    NewGem.AfterOID = uint.MaxValue;
                    NewGem.UnkI32 = 0x1CE;
                    NewGem.UnkI322 = new List<float>() { 1 };
                    NewGem.UnkI323 = new List<uint>() { 0, 255, (uint)Twins_Data.All_Gems[i].type };

                    instances_section.Records.Add(NewGem);
                }
            }
        }

        void RM_Randomize_Music(TwinsFile RM_Archive)
        {
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (ushort)Twins_Data.ObjectID.DJ)
                        {
                            uint sourceMusic = instance.UnkI323[0];
                            for (int m = 0; m < musicTypes.Count; m++)
                            {
                                if (musicTypes[m] == sourceMusic)
                                {
                                    sourceMusic = randMusicList[m];
                                }
                            }
                            instance.UnkI323 = new List<uint>() { sourceMusic, 255, instance.UnkI323[2] };

                            break;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        void RM_LoadScripts(TwinsFile RM_Archive)
        {
            if (RM_Archive.ContainsItem((uint)RM2_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
                if (code_section.ContainsItem((uint)RM2_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
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
            if (RM_Archive.ContainsItem((uint)RM2_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
                if (code_section.ContainsItem((uint)RM2_Code_Sections.Object))
                {
                    TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
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

        void RM_CharacterObjectMod(TwinsFile RM_Archive)
        {
            if (RM_Archive.ContainsItem((uint)RM2_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
                /*
                if (code_section.ContainsItem((uint)RM2_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        script_section.Records.Add(Twins_Data.GetScriptByID(Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(Twins_Data.ScriptID.COM_GENERIC_CHARACTER_STRAFE_LEFT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT));
                        script_section.Records.Add(Twins_Data.GetScriptByID(Twins_Data.ScriptID.COM_GENERIC_CHARACTER_STRAFE_RIGHT));
                    }
                }
                */
                if (code_section.ContainsItem((uint)RM2_Code_Sections.Object))
                {
                    TwinsSection obj_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
                    if (obj_section.Records.Count > 0)
                    {
                        for (int obj = 0; obj < obj_section.Records.Count; obj++)
                        {
                            if (obj_section.Records[obj].ID == (uint)Twins_Data.ObjectID.CRASH)
                            {
                                GameObject gameObj = (GameObject)obj_section.Records[obj];
                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                if (Options[ModStompKick].Enabled)
                                {
                                    gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnFlyingKick] = (ushort)Twins_Data.ScriptID.HEAD_COM_CRASH_STOMP_KICK;
                                    gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnFlyingKickLand] = (ushort)Twins_Data.ScriptID.HEAD_COM_CRASH_STOMP_KICK_LAND;
                                }

                                obj_section.Records[obj] = gameObj;
                            }
                            else if (obj_section.Records[obj].ID == (uint)Twins_Data.ObjectID.CORTEX)
                            {
                                //GameObject gameObj = (GameObject)obj_section.Records[obj];

                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.Unk35] = (ushort)Twins_Data.ScriptID.HEAD_COM_CORTEX_RECOIL;

                                //obj_section.Records[obj] = gameObj;
                            }
                            else if (obj_section.Records[obj].ID == (uint)Twins_Data.ObjectID.NINA)
                            {
                                //GameObject gameObj = (GameObject)obj_section.Records[obj];
                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnStrafeLeft] = (ushort)Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_LEFT;
                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnStrafeRight] = (ushort)Twins_Data.ScriptID.HEAD_COM_GENERIC_CHARACTER_STRAFE_RIGHT;

                                //gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnThrowPunch] = (ushort)Twins_Data.ScriptID.HEAD_COM_NINA_ENTER_VEHICLE;

                                //obj_section.Records[obj] = gameObj;
                            }
                        }
                    }
                }
            }
        }

        void RM_CharacterMod(TwinsFile RM_Archive)
        {
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)Twins_Data.ObjectID.CRASH)
                        {
                            // Crash mods

                            if (Options[RandomizeCharParams].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)Twins_Data.CharacterID.Crash];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)Twins_Data.CharacterID.Crash];
                            }

                            if (Options[ModFlyingKick].Enabled || Options[ModStompKick].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = 0.15f;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = 50;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = 10;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = 0; // 1

                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.CORTEX)
                        {
                            // Cortex mods

                            if (Options[RandomizeCharParams].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)Twins_Data.CharacterID.Cortex];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)Twins_Data.CharacterID.Cortex];
                            }

                            if (Options[ModDoubleJumpCortex].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = 0.4f;

                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.NINA)
                        {
                            // Nina mods

                            if (Options[RandomizeCharParams].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)Twins_Data.CharacterID.Nina];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)Twins_Data.CharacterID.Nina];
                            }

                            if (Options[ModDoubleJumpNina].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = 1.75f;

                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods

                            if (Options[RandomizeCharParams].Enabled)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)Twins_Data.CharacterID.Mechabandicoot];
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)Twins_Data.CharacterID.Mechabandicoot];
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        void RM_EnableUnusedEnemies(TwinsFile RM_Archive)
        {
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)Twins_Data.ObjectID.GLOBAL_BAT_DARKPURPLE)
                        {
                            if (instance.UnkI32 > (uint)Twins_Data.PropertyFlags.DisableObject)
                            {
                                instance.UnkI32 -= (uint)Twins_Data.PropertyFlags.DisableObject;
                            }
                        }
                        //todo: frogensteins, drones in coreent
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        void RM_Randomize_Enemies(TwinsFile RM_Archive)
        {
            List<Twins_Data.ObjectID> importedObjects = new List<Twins_Data.ObjectID>();
            bool EnemyFound = false;
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int obj = 0; obj < EnemyReplaceList.Count; obj++)
                        {
                            if (instance.ObjectID == (uint)EnemyReplaceList[obj])
                            {
                                EnemyFound = true;
                            }
                        }
                        if (EnemyFound)
                        {
                            int targetPos = randState.Next(0, EnemyInsertList.Count);
                            Twins_Data.ObjectID targetObjectID = EnemyInsertList[targetPos];
                            Twins_Data.ImportGameObject(ref RM_Archive, targetObjectID, ref importedObjects);
                            InstanceTemplate template = Twins_Data.GetInstanceTemplateByObjectID(targetObjectID);
                            if (template.ObjectID == 0 && instance.SomeNum1 == 0)
                            {
                                // For objects that are placed at runtime
                                template = new InstanceTemplate()
                                {
                                    ObjectID = (ushort)targetObjectID,
                                    InstancesNum = 10,
                                    PathsNum = 10,
                                    PositionsNum = 10,
                                    Properties = 0x8B2E,
                                    Flags = new List<uint>() { 10000 },
                                    FloatVars = new List<float>() { 1, 25, 1.4f, 15, 100, 0, 6, 6 },
                                    IntVars = new List<uint>() { 0, 0, 1 },
                                    InstanceIDs = new List<ushort>(),
                                    PathIDs = new List<ushort>(),
                                    PositionIDs = new List<ushort>(),
                                };
                            }
                            instance.ObjectID = template.ObjectID;
                            instance.SomeNum1 = template.InstancesNum;
                            instance.SomeNum2 = template.PathsNum;
                            instance.SomeNum3 = template.PositionsNum;
                            instance.UnkI32 = template.Properties;
                            instance.UnkI321 = template.Flags;
                            instance.UnkI322 = template.FloatVars;
                            instance.UnkI323 = template.IntVars;
                            // todo: figure out if these are needed and how to avoid them (procedurally generate?)
                            instance.InstanceIDs = new List<ushort>();
                            if (template.InstanceIDs.Count > 0)
                            {
                                for (int a = 0; a < template.InstanceIDs.Count; a++)
                                {
                                    instance.InstanceIDs.Add(0);
                                }
                            }
                            instance.PathIDs = new List<ushort>();
                            if (template.PathIDs.Count > 0)
                            {
                                for (int a = 0; a < template.PathIDs.Count; a++)
                                {
                                    instance.PathIDs.Add(0);
                                }
                            }
                            instance.PositionIDs = new List<ushort>();
                            if (template.PositionIDs.Count > 0)
                            {
                                for (int a = 0; a < template.PositionIDs.Count; a++)
                                {
                                    instance.PositionIDs.Add(0);
                                }
                            }
                        }
                        instances.Records[i] = instance;
                        EnemyFound = false;
                    }
                }
            }
        }

    }
}
