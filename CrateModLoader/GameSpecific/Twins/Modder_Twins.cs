using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CrateModLoader.GameSpecific.Twins;
using Twinsanity;
//Twinsanity API by NeoKesha, Smartkin, ManDude and Marko (https://github.com/Smartkin/twinsanity-editor)
//Version number, seed and options are displayed in the Autosave Disabled screen accessible by starting a new game without saving or just disabling autosave.

namespace CrateModLoader
{
    class Modder_Twins
    {

        public string[] modOptions = { 
			"Randomize Regular Crates", 
			"Randomize Gem Locations",
            "Randomize Level Music",
            "Randomize Character Parameters",
			"Enable Flying Kick for Crash",
            "Enable Stomp Kick for Crash (Flying Kick variation)",
            "Enable Double Jump for Cortex",
            "Enable Double Jump for Nina",
			};

        public bool Twins_Randomize_CrateTypes = false; // TODO: Make this a toggle between CrateTypes/AllCrates in the mod menu?
        public bool Twins_Randomize_AllCrates = false;
		public bool Twins_Randomize_GemLocations = false;
        public bool Twins_Randomize_Enemies = false; // TODO
        public bool Twins_Randomize_PlayableChars = false; // TODO for a later version
        public bool Twins_Randomize_StartingChunk = false; // TODO, ExePatcher
        public bool Twins_Randomize_Music = false;
        public bool Twins_Randomize_CharacterParams = false; // TODO
		public bool Twins_Mod_PreventSequenceBreaks = false; // TODO
        public bool Twins_Mod_FlyingKick = false;
        public bool Twins_Mod_StompKick = false;
        public bool Twins_Mod_DoubleJump_Cortex = false;
        public bool Twins_Mod_DoubleJump_Nina = false;

        private string bdPath = "";
        public Random randState = new Random();
        public List<uint> CrateReplaceList = new List<uint>();
        public List<uint> randCrateList = new List<uint>();
        public List<uint> gemObjectList = new List<uint>();
        public List<uint> musicTypes = new List<uint>();
        public List<uint> randMusicList = new List<uint>();
        public bool[] levelEdited;

        public enum Twins_Options
        {
            RandomizeAllCrates = 0,
            RandomizeGemLocations = 1,
            RandomizeMusic = 2,
            RandomizeCharParams = 3,
            ModFlyingKick = 4,
			ModStompKick = 5,
            ModDoubleJumpCortex = 6,
            ModDoubleJumpNina = 7,
        }

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)Twins_Options.RandomizeGemLocations)
            {
                Twins_Randomize_GemLocations = value;
            }
            else if (option == (int)Twins_Options.RandomizeAllCrates)
            {
                Twins_Randomize_AllCrates = value;
            }
            else if (option == (int)Twins_Options.RandomizeMusic)
            {
                Twins_Randomize_Music = value;
            }
			else if (option == (int)Twins_Options.ModStompKick)
            {
                Twins_Mod_StompKick = value;
            }
            else if (option == (int)Twins_Options.ModFlyingKick)
            {
                Twins_Mod_FlyingKick = value;
            }
            else if (option == (int)Twins_Options.ModDoubleJumpCortex)
            {
                Twins_Mod_DoubleJump_Cortex = value;
            }
            else if (option == (int)Twins_Options.ModDoubleJumpNina)
            {
                Twins_Mod_DoubleJump_Nina = value;
            }
            else if (option == (int)Twins_Options.RandomizeCharParams)
            {
                Twins_Randomize_CharacterParams = value;
            }
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
        }

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
            RiggedModels = 4,
            Unknown1 = 5,
            GraphicsCompilationsExtra = 6,
            Terrains = 7,
            Unknown2 = 8,
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
            SE_Eng = 7,
            SE_Fre = 8,
            SE_Ger = 9,
            SE_Spa = 10,
            SE_Ita = 11,
            SE_Jpn = 12,
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

        public void StartModProcess()
        {
            // Extract BD
            bdPath = System.IO.Path.Combine(Program.ModProgram.extractedPath, "cml_extr/");
            Directory.CreateDirectory(bdPath);

            BDArchive.ExtractAll(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH"), bdPath);

            File.Delete(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH.BD"));
            File.Delete(System.IO.Path.Combine(Program.ModProgram.extractedPath, "CRASH6/CRASH.BH"));

            ModProcess();
        }

        public void ModProcess()
        {
            //Start Modding
            randState = new Random(Program.ModProgram.randoSeed);

            bool Twins_Edit_CodeText = true;
            bool Twins_Edit_AllLevels = false;

            if (Twins_Randomize_CrateTypes)
            {
                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm2", TwinsFile.FileType.RM2);

                List<uint> crateList = new List<uint>();
                List<uint> posList = new List<uint>();

                if (Twins_Randomize_CrateTypes)
                {
                    crateList.Add((uint)Twins_Data.ObjectID.BASICCRATE5);
                    //crateList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                    //crateList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                    crateList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                    crateList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                    crateList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                    crateList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                    //crateList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                    //crateList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_NINA);
                    crateList.Add((uint)Twins_Data.ObjectID.IRONCRATE7);
                    crateList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE7);
                    crateList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE1);
                    crateList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);

                    posList.Add((uint)Twins_Data.ObjectID.BASICCRATE5);
                    //posList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                    //posList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                    posList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                    posList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                    posList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                    posList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                    //posList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                    //posList.Add((uint)Twins_Data.ObjectID.EXTRA_LIFE_CRATE_NINA);
                    posList.Add((uint)Twins_Data.ObjectID.IRONCRATE7);
                    posList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE7);
                    posList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE1);
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
                }

                mainArchive.SaveFile(bdPath + "/Startup/Default.rm2");
            }
            if (Twins_Randomize_AllCrates)
            {
                //Importing ammo crate
                TwinsFile cortexlevelArchive = new TwinsFile();
                cortexlevelArchive.LoadFile(bdPath + @"Levels\school\Cortex\cogpa01.rm2", TwinsFile.FileType.RM2);

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

            if (Twins_Randomize_AllCrates)
            {

                // Crates to insert
                randCrateList = new List<uint>();
                randCrateList.Add((uint)Twins_Data.ObjectID.BASICCRATE5);
                //randCrateList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //randCrateList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.IRONCRATE7);
                randCrateList.Add((uint)Twins_Data.ObjectID.IRONSPRINGCRATE7);
                randCrateList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE1);
                randCrateList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);
                randCrateList.Add((uint)Twins_Data.ObjectID.AMMOCRATESMALL);

                // Crates to replace
                CrateReplaceList = new List<uint>();
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.BASICCRATE5);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.TNT_CRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.NITRO_CRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.WOODENSPRINGCRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.REINFORCEDWOODENCRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.AKUAKUCRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.IRON_CRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.IRON_SPRING_CRATE);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.MULTIPLEHITCRATE1);
                CrateReplaceList.Add((uint)Twins_Data.ObjectID.SURPRISECRATE);
                //CrateReplaceList.Add((uint)Twins_Data.ObjectID.AMMOCRATESMALL);

                Twins_Edit_AllLevels = true;
            }

            if (Twins_Randomize_GemLocations)
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

            if (Twins_Randomize_Music)
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

            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick || Twins_Mod_DoubleJump_Cortex || Twins_Mod_DoubleJump_Nina)
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

                RM_LoadScripts(ref mainArchiveLoad);
                RM_LoadObjects(ref mainArchiveLoad);

                Twins_Data.allScripts.Sort((x, y) => x.ID.CompareTo(y.ID));
                Twins_Data.allObjects.Sort((x, y) => x.ID.CompareTo(y.ID));

                /*
                List<string> test_scriptList = new List<string>();

                test_scriptList.Add("public enum ObjectID {");

                for (int i = 0; i < Twins_Data.allObjects.Count; i++)
                {
                    if (Twins_Data.allObjects[i].Name != null && Twins_Data.allObjects[i].Name != "")
                    {
                        test_scriptList.Add(Twins_Data.allObjects[i].Name + " = " + Twins_Data.allObjects[i].ID + ",");
                    }
                    else
                    {
                        test_scriptList.Add("Object" + Twins_Data.allObjects[i].ID + " = " + Twins_Data.allObjects[i].ID + ",");
                    }
                }

                test_scriptList.Add("};");

                File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "/Tools/AllScripts.txt", test_scriptList);
                */
                
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
            }

            if (Twins_Edit_CodeText)
            {
                string[] CodeText;
                if (Program.ModProgram.targetRegion == ModLoader.RegionType.NTSC_U)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/American.txt");
                }
                else if (Program.ModProgram.targetRegion == ModLoader.RegionType.PAL)
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/English.txt");
                }
                else
                {
                    CodeText = File.ReadAllLines(bdPath + "/Language/Code/Japanese.txt");
                }

                List<string> CodeText_LineList = new List<string>();
                for (int i = 0; i < CodeText.Length; i++)
                {
                    CodeText_LineList.Add(CodeText[i]);
                }

                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    if (CodeText_LineList[i] == "to enable autosave,~return to the pause menu~and re-save the game.") //todo: japanese equivalent
                    {
                        CodeText_LineList[i] = "to enable autosave,~return to the pause menu~and re-save the game.~crate mod loader " + Program.ModProgram.releaseVersionString + "~" + "seed: " + Program.ModProgram.randoSeed + "~" + "options: " + Program.ModProgram.optionsSelectedString + "";
                    }
                }

                CodeText = new string[CodeText_LineList.Count];
                for (int i = 0; i < CodeText_LineList.Count; i++)
                {
                    CodeText[i] = CodeText_LineList[i];
                }

                if (Program.ModProgram.targetRegion == ModLoader.RegionType.NTSC_U)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/American.txt", CodeText);
                }
                else if (Program.ModProgram.targetRegion == ModLoader.RegionType.PAL)
                {
                    File.WriteAllLines(bdPath + "/Language/Code/English.txt", CodeText);
                }
                else
                {
                    File.WriteAllLines(bdPath + "/Language/Code/Japanese.txt", CodeText);
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

            if (Twins_Randomize_AllCrates)
            {
                RM_Randomize_Crates(ref RM_Archive, ref chunkType);
            }
            if (Twins_Randomize_GemLocations)
            {
                RM_Randomize_Gems(ref RM_Archive, ref chunkType);
            }
            if (Twins_Randomize_Music)
            {
                RM_Randomize_Music(ref RM_Archive);
            }
            if (Twins_Mod_StompKick)
            {
                RM_CharacterObjectMod(ref RM_Archive);
            }
            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick || Twins_Mod_DoubleJump_Cortex || Twins_Mod_DoubleJump_Nina)
            {
                RM_CharacterMod(ref RM_Archive);
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

            RM_LoadScripts(ref RM_Archive);
            RM_LoadObjects(ref RM_Archive);
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

        public void EndModProcess()
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


        void RM_Randomize_Crates(ref TwinsFile RM_Archive, ref Twins_Data.ChunkType chunkType)
        {
            randState = new Random((Program.ModProgram.randoSeed + (int)chunkType) % int.MaxValue);
            int target_item = 0;
            int target_life = 0;
            List<uint> lifecrates = new List<uint>();
            lifecrates.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATE);
            lifecrates.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATECORTEX);
            lifecrates.Add((uint)Twins_Data.ObjectID.EXTRALIFECRATENINA);
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
                                target_item = randState.Next(0, randCrateList.Count);
                                if (randCrateList[target_item] == (int)Twins_Data.ObjectID.EXTRALIFECRATE)
                                {
                                    target_life = randState.Next(0, lifecrates.Count);
                                    target_item = (int)lifecrates[target_life];
                                }
                                else
                                {
                                    target_item = (int)randCrateList[target_item];
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

        void RM_Randomize_Gems(ref TwinsFile RM_Archive, ref Twins_Data.ChunkType chunkType)
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
                    NewGem.Pos = new Pos(Twins_Data.All_Gems[i].pos.X, Twins_Data.All_Gems[i].pos.Y, Twins_Data.All_Gems[i].pos.Z,1f);
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

        void RM_Randomize_Music(ref TwinsFile RM_Archive)
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
                        if (instance.ObjectID == 316) // DJ object
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

        void RM_LoadScripts(ref TwinsFile RM_Archive)
        {
            bool check = false;
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
                                check = false;
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
        void RM_LoadObjects(ref TwinsFile RM_Archive)
        {
            bool check = false;
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
                                check = false;
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

        void RM_CharacterObjectMod(ref TwinsFile RM_Archive)
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

                                if (Twins_Mod_StompKick)
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

        void RM_CharacterMod(ref TwinsFile RM_Archive)
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

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = 5;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = 10;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinLength] = 0.4f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SpinDelay] = 0.01f;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.JumpHeight] = 13;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RunSpeed] = 18;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideJumpUnk24] = 33;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = 30;

                            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = 0.15f;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = 50;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = 10;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static1] = 0; // 1
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk3] = 0; // 5.2
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static6] = 250; // 0
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk13] = 0; // 0.15
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk14] = 0; // 0.5
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Static15] = 0; // 1
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk28] = 0; // 0.05
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk29] = 0; // 0.4
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk30] = 0; // 0.05
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk31] = 0; // 0.05

                            break;
                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.CORTEX)
                        {
                            // Cortex mods

                            if (Twins_Mod_DoubleJump_Cortex)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamHangTime] = 0.4f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamUpwardForce] = 10;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.BodyslamGravityForce] = 400;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = 0.25f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunChargeTime] = 0.5f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenChargedShots] = 0.5f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.GunTimeBetweenShots] = 0.05f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.Unk55] = 0.5f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.RadialBlastChargeTime] = 0.1f;
                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.NINA)
                        {
                            // Nina mods

                            if (Twins_Mod_DoubleJump_Nina)
                            {
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                                instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;
                            }

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlSpeed] = 1.75f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeFromStand] = 0.1f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToStand] = 0.1f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.CrawlTimeToRun] = 0.1f;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSpeed] = 18;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime] = 0.15f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime2] = 0.2f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideSlowdownTime3] = 0.1f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk49] = 0.3f;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.SlideUnk50] = 0.3f;

                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = 5;
                        }
                        else if (instance.ObjectID == (uint)Twins_Data.ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

    }
}
