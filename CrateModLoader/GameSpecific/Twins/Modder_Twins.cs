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
			"Enable Flying Kick for Crash",
            "Enable Stomp Kick for Crash (Flying Kick variation)"
			};

        public bool Twins_Randomize_CrateTypes = false; // TODO: Make this a toggle between CrateTypes/AllCrates in the mod menu?
        public bool Twins_Randomize_AllCrates = false; // TODO: Set seed based per chunk/instance ID to prevent version inconsistency
        public bool Twins_Randomize_GemTypes = false; // TODO: Change instance variable, or maybe just scrap this
		public bool Twins_Randomize_GemLocations = false;
        public bool Twins_Randomize_Enemies = false; // TODO
        public bool Twins_Randomize_PlayableChars = false; // TODO
        public bool Twins_Randomize_StartingChunk = false; // TODO, ExePatcher
        public bool Twins_Randomize_Music = false;
        public bool Twins_Randomize_BossPatterns = false;// TODO
		public bool Twins_Mod_PreventSequenceBreaks = false; // TODO
        public bool Twins_Mod_FlyingKick = false;
        public bool Twins_Mod_StompKick = false;
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
            ModFlyingKick = 3,
			ModStompKick = 4,
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
            Models = 2,
            GraphicsCompilations = 3,
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
        public enum DefaultRM2_DefaultIDs
        {
            REDWUMPA = 1,
            HEALTH = 2,
            BASIC_CRATE = 3,
            NITRO_CRATE = 4,
            TNT_CRATE = 5,
            EXTRA_LIFE_CRATE = 12,
            WOODEN_SPRING_CRATE = 13,
            IRON_SPRING_CRATE = 14,
            IRON_CRATE = 15,
            IRON_SWITCH_CRATE = 16,
            SURPRISE_CRATE = 17,
            NITRO_SWITCH_CRATE = 18,
            MULTIPLE_HIT_CRATE = 19,
            REINFORCED_WOODEN_CRATE = 20,
            CEILING_CHI_CHI_GRASS = 33,
            WALL_CHI_CHI_GRASS = 85,
            EXTRA_LIFE = 190,
            CHECKPOINT_CRATE = 266,
            LEVEL_CRATE = 268,
            AKU_AKU_CRATE = 297,
            BREAKABLE_NITRO_SWITCH_CRATE = 584,
            GEM_RED = 771,
            GEM_GREEN = 772,
            GEM_BLUE = 773,
            GEM_PURPLE = 775,
            GEM_YELLOW = 776,
            GEM_CLEAR = 777,
            DETONATOR_CRATE = 802,
            CRYSTAL = 878,
            EXTRA_LIFE_CRATE_CORTEX = 1137,
            EXTRA_LIFE_CRATE_NINA = 1138,
            EXTRA_LIFE_CORTEX = 1139,
            EXTRA_LIFE_NINA = 1140,
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

            if (Twins_Randomize_CrateTypes || Twins_Randomize_GemTypes)
            {
                TwinsFile mainArchive = new TwinsFile();
                mainArchive.LoadFile(bdPath + @"Startup\Default.rm2", TwinsFile.FileType.RM2);

                List<uint> crateList = new List<uint>();
                List<uint> posList = new List<uint>();

                if (Twins_Randomize_CrateTypes)
                {
                    crateList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                    //crateList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                    //crateList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.WOODEN_SPRING_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.REINFORCED_WOODEN_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.AKU_AKU_CRATE);
                    //crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_CORTEX);
                   // crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_NINA);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.IRON_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.IRON_SPRING_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.MULTIPLE_HIT_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.SURPRISE_CRATE);

                    posList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                    //posList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                    //posList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.WOODEN_SPRING_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.REINFORCED_WOODEN_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.AKU_AKU_CRATE);
                    //posList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_CORTEX);
                    //posList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_NINA);
                    posList.Add((uint)DefaultRM2_DefaultIDs.IRON_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.IRON_SPRING_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.MULTIPLE_HIT_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.SURPRISE_CRATE);

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
                if (Twins_Randomize_GemTypes)
                {
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_BLUE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_CLEAR);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_GREEN);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_PURPLE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_RED);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.GEM_YELLOW);

                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_BLUE);
                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_CLEAR);
                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_GREEN);
                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_PURPLE);
                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_RED);
                    posList.Add((int)DefaultRM2_DefaultIDs.GEM_YELLOW);

                    int target_item = 0;

                    while (posList.Count > 0)
                    {
                        target_item = randState.Next(0, crateList.Count);
                        TwinsSection objectdata = mainArchive.GetItem<TwinsSection>((uint)RM2_Sections.Code).GetItem<TwinsSection>((int)RM2_Code_Sections.Object);
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
                randCrateList = new List<uint>();
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                //randCrateList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                //randCrateList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.WOODEN_SPRING_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.REINFORCED_WOODEN_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.AKU_AKU_CRATE);
                //randCrateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_CORTEX); Maybe try weighted random or when life crate is chosen choose from those three
                //randCrateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_NINA);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.IRON_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.IRON_SPRING_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.MULTIPLE_HIT_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.SURPRISE_CRATE);

                CrateReplaceList = new List<uint>();
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                //CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                //CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.WOODEN_SPRING_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.REINFORCED_WOODEN_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.AKU_AKU_CRATE);
                //CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.IRON_CRATE);
                //CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.IRON_SPRING_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.MULTIPLE_HIT_CRATE);
                CrateReplaceList.Add((uint)DefaultRM2_DefaultIDs.SURPRISE_CRATE);

                Twins_Edit_AllLevels = true;
            }

            if (Twins_Randomize_GemLocations)
            {
                Twins_Data.Twins_Randomize_Gems(ref randState);

                gemObjectList = new List<uint>();
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_BLUE);
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_CLEAR);
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_GREEN);
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_PURPLE);
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_RED);
                gemObjectList.Add((uint)DefaultRM2_DefaultIDs.GEM_YELLOW);

                Twins_Edit_AllLevels = true;
            }

            if (Twins_Randomize_Music)
            {
                List<uint> temp_musicList = new List<uint>();
                musicTypes.Clear();
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

            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick)
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
                RM_Randomize_Crates(ref RM_Archive);
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
            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick)
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

            if (Twins_Mod_FlyingKick || Twins_Mod_StompKick)
            {
                RM_LoadScripts(ref RM_Archive);
                RM_LoadObjects(ref RM_Archive);
            }
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


        void RM_Randomize_Crates(ref TwinsFile RM_Archive)
        {
            int target_item = 0;
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
                                instance.ObjectID = (ushort)randCrateList[target_item];
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
                                gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnFlyingKick] = (ushort)Twins_Data.ScriptID.HEAD_COM_CRASH_STOMP_KICK;
                                gameObj.Scripts[(int)Twins_Data.CharacterGameObjectScriptOrder.OnFlyingKickLand] = (ushort)Twins_Data.ScriptID.HEAD_COM_CRASH_STOMP_KICK_LAND;

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
                        if (instance.ObjectID == 0)
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

                            instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickHangTime] = 0.15f;
                            instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickForwardSpeed] = 50;
                            instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.FlyingKickGravity] = 10;

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
                        else if (instance.ObjectID == 74)
                        {
                            // Cortex mods
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;

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
                        else if (instance.ObjectID == 347)
                        {
                            // Nina mods
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpHeight] = 16;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpUnk22] = 64;
                            //instance.UnkI322[(int)Twins_Data.CharacterInstanceFloats.DoubleJumpArcUnk] = 72.951f;

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
                        else if (instance.ObjectID == 379)
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
