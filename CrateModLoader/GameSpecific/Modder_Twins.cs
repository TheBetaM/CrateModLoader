using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Twinsanity;
//Twinsanity API by NeoKesha

namespace CrateModLoader
{
    class Modder_Twins
    {
        public string gameName = "Twinsanity";
        public string apiCredit = "API by NeoKesha";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_twins;
        public string[] modOptions = { "Randomize Crate Types" };

        public bool Twins_Randomize_CrateTypes = false;
        private string bdPath = "";

        public enum Twins_Options
        {
            RandomizeCrateTypes = 0,
        }

        public void OptionChanged(int option, bool value)
        {
            if (option == (int)Twins_Options.RandomizeCrateTypes)
            {
                Twins_Randomize_CrateTypes = value;
            }
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
        public enum DefaultRM2_DefaultPOSITION
        {
            REDWUMPA = 0,
            HEALTH = 1,
            BASIC_CRATE = 2,
            NITRO_CRATE = 3,
            TNT_CRATE = 4,
            EXTRA_LIFE_CRATE = 5,
            WOODEN_SPRING_CRATE = 6,
            IRON_SPRING_CRATE = 7,
            IRON_CRATE = 8,
            IRON_SWITCH_CRATE = 9,
            SURPRISE_CRATE = 10,
            NITRO_SWITCH_CRATE = 11,
            MULTIPLE_HIT_CRATE = 12,
            REINFORCED_WOODEN_CRATE = 13,
            CEILING_CHI_CHI_GRASS = 14,
            WALL_CHI_CHI_GRASS = 15,
            EXTRA_LIFE = 16,
            CHECKPOINT_CRATE = 17,
            LEVEL_CRATE = 18,
            AKU_AKU_CRATE = 19,
            BREAKABLE_NITRO_SWITCH_CRATE = 20,
            GEM_RED = 21,
            GEM_GREEN = 22,
            GEM_BLUE = 23,
            GEM_PURPLE = 24,
            GEM_YELLOW = 25,
            GEM_CLEAR = 26,
            DETONATOR_CRATE = 27,
            CRYSTAL = 28,
            EXTRA_LIFE_CRATE_CORTEX = 29,
            EXTRA_LIFE_CRATE_NINA = 30,
            EXTRA_LIFE_CORTEX = 31,
            EXTRA_LIFE_NINA = 32,
        }

        public void StartModProcess()
        {
            Directory.CreateDirectory(Program.ModProgram.extractedPath + "/cml_extr/");
            bdPath = Program.ModProgram.extractedPath + "/cml_extr/";
            BDArchive mainBD = new BDArchive();
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");
            //mainBD.LoadArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH.BD");
            mainBD.ExtractOnce(bdPath, Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            //mainBD.Dispose();
            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");

            Random randState = new Random(Program.ModProgram.randoSeed);

            if (Twins_Randomize_CrateTypes)
            {
                RM2 mainArchive = new RM2();
                mainArchive.LoadRM2(bdPath + "/Startup/Default.rm2");

                List<uint> crateList = new List<uint>();
                crateList.Add((uint)DefaultRM2_DefaultIDs.BASIC_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.TNT_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.NITRO_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.WOODEN_SPRING_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.REINFORCED_WOODEN_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.AKU_AKU_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_CORTEX);
                crateList.Add((uint)DefaultRM2_DefaultIDs.EXTRA_LIFE_CRATE_NINA);
                crateList.Add((uint)DefaultRM2_DefaultIDs.IRON_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.IRON_SPRING_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.MULTIPLE_HIT_CRATE);
                crateList.Add((uint)DefaultRM2_DefaultIDs.SURPRISE_CRATE);

                List<int> posList = new List<int>();
                posList.Add((int)DefaultRM2_DefaultPOSITION.BASIC_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.TNT_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.NITRO_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.EXTRA_LIFE_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.WOODEN_SPRING_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.REINFORCED_WOODEN_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.AKU_AKU_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.EXTRA_LIFE_CRATE_CORTEX);
                posList.Add((int)DefaultRM2_DefaultPOSITION.EXTRA_LIFE_CRATE_NINA);
                posList.Add((int)DefaultRM2_DefaultPOSITION.IRON_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.IRON_SPRING_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.MULTIPLE_HIT_CRATE);
                posList.Add((int)DefaultRM2_DefaultPOSITION.SURPRISE_CRATE);

                int target_item = 0;

                while (posList.Count > 0)
                {
                    target_item = randState.Next(0, crateList.Count);
                    mainArchive.Item[1].Item[0].Item[posList[0]].ID = crateList[target_item];
                    posList.RemoveAt(0);
                    crateList.RemoveAt(target_item);
                }
                posList.Clear();
                crateList.Clear();

                //mainArchive.Item[1].Item[0].Item[3].ID = 5;
                //mainArchive.Item[1].Item[0].Item[4].ID = 4;

                mainArchive.Recalculate();
                mainArchive.Save(bdPath + "/Startup/Default.rm2");
            }

            mainBD = new BDArchive();
            mainBD.CreateTable(bdPath);
            mainBD.SaveTable(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.SaveArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.Dispose();

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

            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1");
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1");
        }
    }
}
