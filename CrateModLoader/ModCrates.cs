using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace CrateModLoader
{
    static class ModCrates
    {

        /* Plan for how Mod Crates are supposed to work:
         * They're .zip files:
         * with folders called "layer0", "layer1", "layer2" etc. 
         * Each layer corresponds to a data archive type that the files inside replace (or add?)
         * info.txt file with the mod's metadata
         * settings.txt file with the game specfic settings that can't be altered with mods
         * 
         * Layer 0 is where the base extracted files from a ROM are, so every game is supposed to support it
         */


        private const char Separator = '=';
        private const char CommentSymbol = '!';
        public static List<ModCrate> ModList;
        public static List<ModCrate> SupportedMods;
        public static bool ModsActive = false;
        public static int ModsActiveAmount = 0;

        public static CheckedListBox CheckedList_Mods;


        public static void PopulateModList()
        {
            CheckedList_Mods.Items.Clear();

            if (string.IsNullOrEmpty(Program.ModProgram.Modder.Game.ShortName))
            {
                Console.WriteLine("Target game is missing short name! Not loading anything.");
                return;
            }

            ModList = new List<ModCrate>();

            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/Mods/");
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".zip")
                {
                    LoadMetadata(file);
                }
            }

            if (ModList.Count <= 0)
            {
                // 404: Mods Not Found
                return;
            }

            SupportedMods = new List<ModCrate>();

            for (int i = 0; i < ModList.Count; i++)
            {
                if (ModList[i].TargetGame == Program.ModProgram.Modder.Game.ShortName)
                {
                    SupportedMods.Add(ModList[i]);
                }
            }

            for (int i = 0; i < SupportedMods.Count; i++)
            {
                string ListName = SupportedMods[i].Name;
                ListName += " ";
                ListName += SupportedMods[i].Version;
                CheckedList_Mods.Items.Add(ListName);
            }


        }

        public static void UpdateModSelection(int index, bool value)
        {
            SupportedMods[index].IsActivated = value;
            if (value)
            {
                ModsActive = true;
                ModsActiveAmount++;
            }
            else
            {
                ModsActive = false;
                ModsActiveAmount--;
                for (int i = 0; i < SupportedMods.Count; i++)
                {
                    if (SupportedMods[i].IsActivated)
                    {
                        ModsActive = true;
                        break;
                    }
                }
            }
        }

        public static void UpdateModList()
        {
            CheckedList_Mods.Items.Clear();

            if (string.IsNullOrEmpty(Program.ModProgram.Modder.Game.ShortName))
            {
                Console.WriteLine("Target game is missing short name! Not loading anything.");
                return;
            }

            if (SupportedMods.Count <= 0)
            {
                // 404: Mods Not Found
                return;
            }

            ModsActiveAmount = 0;
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                string ListName = SupportedMods[i].Name;
                ListName += " ";
                ListName += SupportedMods[i].Version;
                CheckedList_Mods.Items.Add(ListName);
                if (SupportedMods[i].IsActivated)
                {
                    ModsActiveAmount++;
                    CheckedList_Mods.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    CheckedList_Mods.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        // Load metadata from a .zip file
        public static void LoadMetadata(FileInfo file)
        {
            using (ZipArchive archive = ZipFile.OpenRead(file.FullName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) && entry.Name.ToLower() == "info.txt")
                    {
                        ModCrate NewCrate = new ModCrate();
                        using (StreamReader fileStream = new StreamReader(entry.Open(),true))
                        {
                            string line;
                            while ((line = fileStream.ReadLine()) != null)
                            {
                                if (line[0] != CommentSymbol) //reserved for comments
                                {
                                    string[] setting = line.Split(Separator);
                                    NewCrate.Meta[setting[0]] = setting[1];
                                }
                            }
                        }
                        if (NewCrate.Meta.ContainsKey("Name"))
                            NewCrate.Name = NewCrate.Meta["Name"];
                        if (NewCrate.Meta.ContainsKey("Description"))
                            NewCrate.Desc = NewCrate.Meta["Description"];
                        if (NewCrate.Meta.ContainsKey("Author"))
                            NewCrate.Author = NewCrate.Meta["Author"];
                        if (NewCrate.Meta.ContainsKey("Version"))
                            NewCrate.Version = NewCrate.Meta["Version"];
                        if (NewCrate.Meta.ContainsKey("ModLoaderVersion"))
                            NewCrate.CML_Version = NewCrate.Meta["ModLoaderVersion"];
                        if (NewCrate.Meta.ContainsKey("Game"))
                            NewCrate.TargetGame = NewCrate.Meta["Game"];

                        NewCrate.Path = file.FullName;

                        ModList.Add(NewCrate);
                    }
                }
            }
        }

        public static void ClearModLists()
        {
            ModList = new List<ModCrate>();
            SupportedMods = new List<ModCrate>();
            ModsActive = false;
            ModsActiveAmount = 0;
        }


    }

    class ModCrate
    {
        public Dictionary<string, string> Meta = new Dictionary<string, string>();
        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        public string Path;
        public string Name = "Unnamed Mod";
        public string Desc = "(No Description)";
        public string Author = "(Not credited)";
        public string Version = "v1.0";
        public string CML_Version = Program.ModProgram.releaseVersionString;
        public string TargetGame = "NoGame";
        public bool IsActivated = false;

        public bool[] LayersModded = new bool[1] { true };
    }
}
