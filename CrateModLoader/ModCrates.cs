using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace CrateModLoader
{
    static class ModCrates
    {

        /* Plan for how Mod Crates are supposed to work:
         * They're .zip files (or unzipped folders):
         * with folders called "layer0", "layer1", "layer2" etc. 
         * Each layer corresponds to a data archive type that the files inside replace (or add?) (game-specific, except for layer0)
         * modcrateinfo.txt file with the mod's metadata
         * (optional) modcratesettings.txt file with the game specfic settings that can't be altered with mods
         * (optional) modcrateicon.png icon of the mod
         * 
         * Layer 0 is where the base extracted files from a ROM are, so every game is supposed to support it
         */


        public const char Separator = '=';
        public const char CommentSymbol = '!';
        public const string LayerFolderName = "layer";
        public static List<ModCrate> ModList;
        public static List<ModCrate> SupportedMods;
        public static int ModsActiveAmount
        {
            get
            {
                int amount = 0;
                foreach (var mod in SupportedMods)
                {
                    if (mod.IsActivated)
                        ++amount;
                }
                return amount;
            }
        }

        public static CheckedListBox CheckedList_Mods;

        public static void PopulateModList()
        {
            CheckedList_Mods.Items.Clear();

            bool SupportAll = false;
            if (Program.ModProgram.Modder == null)
            {
                SupportAll = true;
            }
            else if (string.IsNullOrEmpty(Program.ModProgram.Modder.Game.ShortName))
            {
                Console.WriteLine("Target game is missing short name!");
                SupportAll = true;
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
            if (di.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    LoadMetadata(dir);
                }
            }

            if (ModList.Count <= 0)
            {
                // 404: Mods Not Found
                return;
            }

            SupportedMods = new List<ModCrate>();

            if (SupportAll)
            {
                for (int i = 0; i < ModList.Count; i++)
                {
                    if (ModList[i].TargetGame == "NoGame")
                    {
                        SupportedMods.Add(ModList[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ModList.Count; i++)
                {
                    if (ModList[i].TargetGame == Program.ModProgram.Modder.Game.ShortName || ModList[i].TargetGame == "All")
                    {
                        SupportedMods.Add(ModList[i]);
                    }
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
        }

        public static void UpdateModList()
        {
            CheckedList_Mods.Items.Clear();

            if (SupportedMods.Count <= 0)
            {
                // 404: Mods Not Found
                return;
            }

            for (int i = 0; i < SupportedMods.Count; i++)
            {
                string ListName = SupportedMods[i].Name;
                ListName += " ";
                ListName += SupportedMods[i].Version;
                CheckedList_Mods.Items.Add(ListName);
                if (SupportedMods[i].IsActivated)
                {
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
            ModCrate NewCrate = new ModCrate();
            bool HasInfo = false;
            bool HasSettings = false;
            int MaxLayer = 0;
            List<int> ModdedLayers = new List<int>();

            using (ZipArchive archive = ZipFile.OpenRead(file.FullName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        if (entry.Name.ToLower() == "modcrateinfo.txt")
                        {
                            using (StreamReader fileStream = new StreamReader(entry.Open(), true))
                            {
                                string line;
                                while ((line = fileStream.ReadLine()) != null)
                                {
                                    if (line[0] != CommentSymbol) //reserved for comments
                                    {
                                        string[] setting = line.Split(Separator);
                                        NewCrate.Meta[setting[0]] = setting[1];
                                        HasInfo = true;
                                    }
                                }
                            }
                        }
                        else if (entry.Name.ToLower() == "modcratesettings.txt")
                        {
                            using (StreamReader fileStream = new StreamReader(entry.Open(), true))
                            {
                                string line;
                                while ((line = fileStream.ReadLine()) != null)
                                {
                                    if (line[0] != CommentSymbol) //reserved for comments
                                    {
                                        string[] setting = line.Split(Separator);
                                        NewCrate.Settings[setting[0]] = setting[1];
                                        HasSettings = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (entry.Name.ToLower() == "modcrateicon.png")
                    {
                        NewCrate.Icon = Image.FromStream(entry.Open());
                    }
                    if (entry.FullName.Split('/')[0].Substring(0, LayerFolderName.Length).ToLower() == LayerFolderName)
                    {
                        int Layer = int.Parse(entry.FullName.Split('/')[0].Substring(LayerFolderName.Length));
                        if (!ModdedLayers.Contains(Layer))
                        {
                            MaxLayer = Math.Max(MaxLayer, Layer);
                            ModdedLayers.Add(Layer);
                        }
                    }
                }
            }

            if (!HasInfo)
            {
                Console.WriteLine("Mod Crate has no info.txt file!");
                return;
            }

            if (ModdedLayers.Count > 0)
            {
                NewCrate.LayersModded = new bool[MaxLayer + 1];
                for (int i = 0; i < ModdedLayers.Count; i++)
                {
                    NewCrate.LayersModded[ModdedLayers[i]] = true;
                }
            }
            else
            {
                NewCrate.LayersModded = new bool[1] { false };
            }

            if (HasSettings)
            {
                NewCrate.HasSettings = true;
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
        public static void LoadMetadata(DirectoryInfo dir)
        {
            ModCrate NewCrate = new ModCrate();
            bool HasInfo = false;
            bool HasSettings = false;
            int MaxLayer = 0;
            List<int> ModdedLayers = new List<int>();

            foreach (FileInfo file in dir.EnumerateFiles())
            {
                if (file.Name.ToLower() == "modcrateinfo.txt")
                {
                    using (StreamReader fileStream = new StreamReader(file.Open(FileMode.Open), true))
                    {
                        string line;
                        while ((line = fileStream.ReadLine()) != null)
                        {
                            if (line[0] != CommentSymbol) //reserved for comments
                            {
                                string[] setting = line.Split(Separator);
                                NewCrate.Meta[setting[0]] = setting[1];
                                HasInfo = true;
                            }
                        }
                    }
                }
                else if (file.Name.ToLower() == "modcratesettings.txt")
                {
                    using (StreamReader fileStream = new StreamReader(file.Open(FileMode.Open), true))
                    {
                        string line;
                        while ((line = fileStream.ReadLine()) != null)
                        {
                            if (line[0] != CommentSymbol) //reserved for comments
                            {
                                string[] setting = line.Split(Separator);
                                NewCrate.Settings[setting[0]] = setting[1];
                                HasSettings = true;
                            }
                        }
                    }
                }
                else if (file.Name.ToLower() == "modcrateicon.png")
                {
                    NewCrate.Icon = Image.FromFile(file.FullName);
                }
            }
            if (dir.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    if (di.Name.Substring(0, LayerFolderName.Length).ToLower() == LayerFolderName)
                    {
                        int Layer = int.Parse(di.Name.Substring(LayerFolderName.Length));
                        if (!ModdedLayers.Contains(Layer))
                        {
                            MaxLayer = Math.Max(MaxLayer, Layer);
                            ModdedLayers.Add(Layer);
                        }
                    }
                }
            }

            if (!HasInfo)
            {
                Console.WriteLine("Mod Crate has no info.txt file!");
                return;
            }

            NewCrate.IsFolder = true;

            if (ModdedLayers.Count > 0)
            {
                NewCrate.LayersModded = new bool[MaxLayer + 1];
                for (int i = 0; i < ModdedLayers.Count; i++)
                {
                    NewCrate.LayersModded[ModdedLayers[i]] = true;
                }
            }
            else
            {
                NewCrate.LayersModded = new bool[1] { false };
            }

            if (HasSettings)
            {
                NewCrate.HasSettings = true;
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

            NewCrate.Path = dir.FullName;

            ModList.Add(NewCrate);
        }

        public static void ClearModLists()
        {
            ModList = new List<ModCrate>();
            SupportedMods = new List<ModCrate>();
        }

        // Use this to handle settings checks. This will only return the first detected instance of a setting in any enabled mod.
        public static string GetSetting(string property)
        {
            if (ModsActiveAmount <= 0)
            {
                return string.Empty;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].HasSettings)
                {
                    if (SupportedMods[i].Settings.ContainsKey(property))
                    {
                        return SupportedMods[i].Settings[property];
                    }
                }
            }
            return string.Empty;
        }
        public static int GetIntSetting(string property)
        {
            if (ModsActiveAmount <= 0)
            {
                return -1;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].HasSettings)
                {
                    if (SupportedMods[i].Settings.ContainsKey(property))
                    {
                        return int.Parse(SupportedMods[i].Settings[property]);
                    }
                }
            }
            return -1;
        }
        public static float GetFloatSetting(string property)
        {
            if (ModsActiveAmount <= 0)
            {
                return -1f;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].HasSettings)
                {
                    if (SupportedMods[i].Settings.ContainsKey(property))
                    {
                        return float.Parse(SupportedMods[i].Settings[property]);
                    }
                }
            }
            return -1f;
        }
        public static bool HasSetting(string property)
        {
            if (ModsActiveAmount <= 0)
            {
                return false;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].HasSettings)
                {
                    if (SupportedMods[i].Settings.ContainsKey(property))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static void InstallLayerMods(string basePath, int layer)
        {
            if (ModsActiveAmount <= 0)
            {
                return;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].LayersModded[layer])
                {
                    if (!SupportedMods[i].IsFolder)
                    {
                        InstallLayerMod(SupportedMods[i], basePath, layer);
                    }
                    else
                    {
                        InstallLayerModFolder(SupportedMods[i], basePath, layer);
                    }
                }
            }
        }
        public static void InstallLayerMod(ModCrate Crate, string basePath, int layer)
        {
            using (ZipArchive archive = ZipFile.OpenRead(Crate.Path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.Split('/').Length > 1 && entry.FullName[entry.FullName.Length - 1] != '/' && entry.FullName.Split('/')[1].Length > 0 && entry.FullName.Split('/')[0].Substring(0, LayerFolderName.Length + layer.ToString().Length).ToLower() == LayerFolderName + layer)
                    {
                        Directory.CreateDirectory(basePath + entry.FullName.Substring(LayerFolderName.Length + layer.ToString().Length + 1, entry.FullName.Length - entry.Name.Length));
                        entry.ExtractToFile(basePath + entry.FullName.Substring(LayerFolderName.Length + layer.ToString().Length), true);
                    }
                }
            }
        }
        public static void InstallLayerModFolder(ModCrate Crate, string basePath, int layer)
        {
            DirectoryInfo dest = new DirectoryInfo(basePath);
            DirectoryInfo source = new DirectoryInfo(Crate.Path + @"\" + LayerFolderName + layer);
            Recursive_CopyFiles(basePath, source, dest, "");
        }
        static void Recursive_CopyFiles(string basePath, DirectoryInfo di, DirectoryInfo dest, string buffer)
        {
            string mainbuffer = buffer + @"\";
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                buffer = Path.Combine(mainbuffer, dir.Name);
                string tempFolder = dest.FullName + buffer + @"\";
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }
                foreach (FileInfo file in dir.EnumerateFiles())
                {
                    string relativePath = Path.Combine(dest.FullName, buffer + @"\" + file.Name);
                    File.Copy(file.FullName, basePath + relativePath, true);
                }
                Recursive_CopyFiles(basePath, dir, dest, buffer);
            }
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
        public bool HasSettings = false;
        public bool IsFolder = false;
        public Image Icon = null;

        public bool[] LayersModded = new bool[1] { false };
    }
}
