using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Twinsanity;
//Twinsanity API by NeoKesha
//Version number, seed and options are displayed in the Autosave Disabled screen accessible by starting a new game without saving or just disabling autosave.

namespace CrateModLoader
{
    class Modder_Twins
    {

        public string[] modOptions = { "Randomize Crate Types", "Randomize Individual Crates", "Randomize Gem Types",  };

        public bool Twins_Randomize_CrateTypes = false;
        public bool Twins_Randomize_AllCrates = false; // TODO
        public bool Twins_Randomize_GemTypes = false;
        private string bdPath = "";
        public Random randState = new Random();
        public List<uint> randCrateList = new List<uint>();

        public enum Twins_Options
        {
            RandomizeCrateTypes = 0,
            RandomizeAllCrates = 1,
            RandomizeGemTypes = 2,
        }

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)Twins_Options.RandomizeCrateTypes)
            {
                Twins_Randomize_CrateTypes = value;
            }
            else if (option == (int)Twins_Options.RandomizeAllCrates)
            {
                Twins_Randomize_AllCrates = value;
            }
            else if (option == (int)Twins_Options.RandomizeGemTypes)
            {
                Twins_Randomize_GemTypes = value;
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
            Directory.CreateDirectory(Program.ModProgram.extractedPath + "/cml_extr/");
            bdPath = Program.ModProgram.extractedPath + "/cml_extr/";
            BDArchive mainBD = new BDArchive();
            // Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            //File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");
            //mainBD.LoadArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH.BD");
            mainBD.ExtractOnce(bdPath, Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            //mainBD.Dispose();
            // This takes up too much memory for some reason?? And Dispose() doesn't work for it.

            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");

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
                mainArchive.LoadFile(bdPath + "/Startup/Default.rm2", TwinsFile.FileType.RM2);

                List<uint> crateList = new List<uint>();
                List<uint> posList = new List<uint>();

                if (Twins_Randomize_CrateTypes)
                {
                    crateList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                    crateList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
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
                    posList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                    posList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
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
                        mainArchive.GetItem<TwinsSection>((int)RM2_Sections.Code).GetItem<TwinsSection>((int)RM2_Code_Sections.Object).GetItem<TwinsItem>(posList[0]).ID = crateList[target_item];
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
                        mainArchive.GetItem<TwinsSection>((uint)RM2_Sections.Code).GetItem<TwinsSection>((int)RM2_Code_Sections.Object).GetItem<TwinsItem>(posList[0]).ID = crateList[target_item];
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
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                randCrateList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
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

                Twins_Edit_AllLevels = true;
            }

            if (Twins_Edit_AllLevels)
            {
                DirectoryInfo di = new DirectoryInfo(bdPath + "/Levels/");
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    Recursive_EditLevels(dir);
                }
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
            TwinsFile RM_Archive = new TwinsFile();
            RM_Archive.LoadFile(path, TwinsFile.FileType.RM2);

            int target_item = 0;
            bool cont = false;

            if (Twins_Randomize_AllCrates)
            {
                for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
                {
                    TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                    if (section.Records.Count > 0)
                    {
                        TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                        for (int i = 0; i < instances.Records.Count; ++i)
                        {
                            Instance instance = (Instance)instances.Records[i];
                            for (int d = 0; d < randCrateList.Count; d++)
                            {
                                if (!cont && instance.ObjectID == randCrateList[d])
                                {
                                    target_item = randState.Next(0, randCrateList.Count);
                                    instance.ObjectID = (ushort)randCrateList[target_item];
                                    cont = true;
                                }
                            }
                            instances.Records[i] = instance;
                            cont = false;
                        }
                    }
                }
            }
            
            RM_Archive.SaveFile(path);
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

        public void EndModProcess()
        {
            // Build BD
            BDArchive mainBD = new BDArchive();
            mainBD.CreateTable(bdPath);
            mainBD.SaveTable(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.SaveArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.Dispose();

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
    }
}
