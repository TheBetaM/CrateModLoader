﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using CrateModLoader.Resources.Text;
using System.Globalization;

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
        public const string InfoFileName = "modcrateinfo.txt";
        public const string SettingsFileName = "modcratesettings.txt";
        public const string IconFileName = "modcrateicon.png";
        public const string UnsupportedGameShortName = "NoGame";
        public const string AllGamesShortName = "All";
        public const string Prop_Name = "Name";
        public const string Prop_Desc = "Description";
        public const string Prop_Author = "Author";
        public const string Prop_Version = "Version";
        public const string Prop_CML_Version = "ModLoaderVersion";
        public const string Prop_Game = "Game";

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
                Console.WriteLine("WARN: Target game is missing short name!");
                SupportAll = true;
            }

            ModList = new List<ModCrate>();

            DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.ModDirectory);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".zip")
                {
                    try
                    {
                        LoadMetadata(file);
                    }
                    catch
                    {
                        MessageBox.Show(ModLoaderText.ModCrateErrorPopup + " " + file.FullName);
                    }
                }
            }
            if (di.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        LoadMetadata(dir);
                    }
                    catch
                    {
                        MessageBox.Show(ModLoaderText.ModCrateErrorPopup + " " + dir.FullName);
                    }
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
                    if (ModList[i].TargetGame == UnsupportedGameShortName || ModList[i].TargetGame == AllGamesShortName)
                    {
                        SupportedMods.Add(ModList[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ModList.Count; i++)
                {
                    if (ModList[i].TargetGame == Program.ModProgram.Modder.Game.ShortName || ModList[i].TargetGame == AllGamesShortName)
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
                        if (entry.Name.ToLower() == InfoFileName)
                        {
                            if (entry.Name != entry.FullName)
                            {
                                NewCrate.NestedPath = entry.FullName.Substring(0, entry.FullName.Length - entry.Name.Length);
                            }
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
                        else if (entry.Name.ToLower() == SettingsFileName)
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
                    else if (entry.Name.ToLower() == IconFileName)
                    {
                        NewCrate.Icon = Image.FromStream(entry.Open());
                    }
                }
                if (HasInfo)
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (NewCrate.NestedPath != "")
                        {
                            if (entry.FullName.Length > NewCrate.NestedPath.Length && entry.FullName.Substring(NewCrate.NestedPath.Length, LayerFolderName.Length).ToLower() == LayerFolderName)
                            {
                                string NestedNumber = entry.FullName.Substring(NewCrate.NestedPath.Length + LayerFolderName.Length);
                                int Layer = int.Parse(NestedNumber.Split('/')[0]);
                                if (!ModdedLayers.Contains(Layer))
                                {
                                    MaxLayer = Math.Max(MaxLayer, Layer);
                                    ModdedLayers.Add(Layer);
                                }
                            }
                        }
                        else
                        {
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
                }
            }

            if (!HasInfo)
            {
                Console.WriteLine("WARN: Mod Crate has no info.txt file! Ignored.");
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

            if (NewCrate.Meta.ContainsKey(Prop_Name))
                NewCrate.Name = NewCrate.Meta[Prop_Name];
            if (NewCrate.Meta.ContainsKey(Prop_Desc))
                NewCrate.Desc = NewCrate.Meta[Prop_Desc];
            if (NewCrate.Meta.ContainsKey(Prop_Author))
                NewCrate.Author = NewCrate.Meta[Prop_Author];
            if (NewCrate.Meta.ContainsKey(Prop_Version))
                NewCrate.Version = NewCrate.Meta[Prop_Version];
            if (NewCrate.Meta.ContainsKey(Prop_CML_Version))
                NewCrate.CML_Version = NewCrate.Meta[Prop_CML_Version];
            if (NewCrate.Meta.ContainsKey(Prop_Game))
                NewCrate.TargetGame = NewCrate.Meta[Prop_Game];
            if (NewCrate.Meta.ContainsKey(Prop_Name + "-" + CultureInfo.CurrentCulture.Name))
                NewCrate.Name = NewCrate.Meta[Prop_Name + "-" + CultureInfo.CurrentCulture.Name];
            if (NewCrate.Meta.ContainsKey(Prop_Desc + "-" + CultureInfo.CurrentCulture.Name))
                NewCrate.Desc = NewCrate.Meta[Prop_Desc + "-" + CultureInfo.CurrentCulture.Name];

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
                if (file.Name.ToLower() == InfoFileName)
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
                else if (file.Name.ToLower() == SettingsFileName)
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
                else if (file.Name.ToLower() == IconFileName)
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
                Console.WriteLine("WARN: Mod Crate has no info.txt file! Ignored.");
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

            if (NewCrate.Meta.ContainsKey(Prop_Name))
                NewCrate.Name = NewCrate.Meta[Prop_Name];
            if (NewCrate.Meta.ContainsKey(Prop_Desc))
                NewCrate.Desc = NewCrate.Meta[Prop_Desc];
            if (NewCrate.Meta.ContainsKey(Prop_Author))
                NewCrate.Author = NewCrate.Meta[Prop_Author];
            if (NewCrate.Meta.ContainsKey(Prop_Version))
                NewCrate.Version = NewCrate.Meta[Prop_Version];
            if (NewCrate.Meta.ContainsKey(Prop_CML_Version))
                NewCrate.CML_Version = NewCrate.Meta[Prop_CML_Version];
            if (NewCrate.Meta.ContainsKey(Prop_Game))
                NewCrate.TargetGame = NewCrate.Meta[Prop_Game];
            if (NewCrate.Meta.ContainsKey(Prop_Name + "-" + CultureInfo.CurrentCulture.Name))
                NewCrate.Name = NewCrate.Meta[Prop_Name + "-" + CultureInfo.CurrentCulture.Name];
            if (NewCrate.Meta.ContainsKey(Prop_Desc + "-" + CultureInfo.CurrentCulture.Name))
                NewCrate.Desc = NewCrate.Meta[Prop_Desc + "-" + CultureInfo.CurrentCulture.Name];

            NewCrate.Path = dir.FullName;

            ModList.Add(NewCrate);
        }

        public static void ClearModLists()
        {
            ModList = new List<ModCrate>();
            SupportedMods = new List<ModCrate>();
        }

        /// <summary>
        /// Installs all active mods of the specified layer in the specified path
        /// </summary>
        public static void InstallLayerMods(string basePath, int layer)
        {
            if (ModsActiveAmount <= 0)
            {
                return;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].LayersModded.Length > layer && SupportedMods[i].LayersModded[layer])
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
                    if (entry.FullName.Length > Crate.NestedPath.Length + LayerFolderName.Length + layer.ToString().Length + 1 && entry.FullName[entry.FullName.Length - 1] != '/')
                    {
                        string AdjustedName = entry.FullName.Substring(Crate.NestedPath.Length);
                        if (AdjustedName.Split('/').Length > 1 && AdjustedName.Split('/')[1].Length > 0)
                        {
                            if (AdjustedName.Split('/')[0].Substring(0, LayerFolderName.Length + layer.ToString().Length).ToLower() == LayerFolderName + layer)
                            {
                                int PathLen = Crate.NestedPath.Length + LayerFolderName.Length + layer.ToString().Length + 1;
                                string NewDir = entry.FullName.Substring(PathLen, entry.FullName.Length - PathLen - entry.Name.Length);
                                string extrPath = entry.FullName.Substring(Crate.NestedPath.Length + LayerFolderName.Length + layer.ToString().Length);
                                Directory.CreateDirectory(basePath + NewDir);
                                entry.ExtractToFile(basePath + extrPath, true);
                            }
                        }
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
                Recursive_CopyFiles(basePath, dir, dest, buffer);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                string relativePath = Path.Combine(dest.FullName, mainbuffer + @"\" + file.Name);
                File.Copy(file.FullName, basePath + relativePath, true);
            }
        }

        /// <summary>
        /// For checking if a layer is modded
        /// </summary>
        public static bool HasLayerModsActive(int layer)
        {
            if (ModsActiveAmount <= 0)
            {
                return false;
            }
            for (int i = 0; i < SupportedMods.Count; i++)
            {
                if (SupportedMods[i].IsActivated && SupportedMods[i].LayersModded.Length > layer && SupportedMods[i].LayersModded[layer])
                {
                    return true;
                }
            }
            return false;
        }

    }

    public class ModCrate
    {
        public Dictionary<string, string> Meta = new Dictionary<string, string>();
        public Dictionary<string, string> Settings = new Dictionary<string, string>();
        public string Path;
        public string Name = "Unnamed Mod";
        public string Desc = "(No Description)";
        public string Author = "(Not credited)";
        public string Version = "v1.0";
        public string CML_Version = ModLoaderGlobals.ProgramVersionSimple.ToString();
        public string TargetGame = ModCrates.AllGamesShortName;
        public bool IsActivated = false;
        public bool HasSettings = false;
        public bool IsFolder = false;
        public Image Icon = null;
        // A workaround for nested folders
        public string NestedPath = "";

        public bool[] LayersModded = new bool[1] { false };
    }
}
