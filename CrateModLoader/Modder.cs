using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using CrateModLoader.Resources.Text;
using CrateModLoader.ModProperties;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new modder class that inherits this abstract class. (ensure that the modder's namespace is CrateModLoader.GameSpecific.??? to avoid bugs)
     * 2. Override its Game member with the appropriate info in the getter. 
     * 3. Override StartModProcess (at least, there are more modding functions that can be overriden but are optional).
     * (optional) 4. Localize game title, API credit, and options using text resources.
     * (optional) 5. Create ModProperty variables for automatic Mod Menu setup.
     * (optional) 6. Add ModPropOption variables for quick options in the main window.
     * 7. Done.
     * 
     */

    /// <summary>
    /// Abstract Modder class, inherited by all game modders
    /// </summary>
    public abstract class Modder
    {

        public List<ModPropertyBase> Props = new List<ModPropertyBase>();

        public Modder()
        {
            // moved to ModLoader.cs because of localization being loaded by constructor, also for speed I guess
            //PopulateProperties();
        }

        public void PopulateProperties()
        {
            Props.Clear();

            // Populate property list automatically from namespace
            Assembly asm = Assembly.GetExecutingAssembly();

            string nameSpace = GetType().Namespace;

            foreach (Type type in asm.GetTypes())
            {
                if (!string.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains(GetType().Namespace))
                {
                    foreach (FieldInfo field in type.GetFields())
                    {
                        if (field.FieldType.IsSubclassOf(typeof(ModPropertyBase)))
                        {
                            Props.Add((ModPropertyBase)field.GetValue(null));

                            Props[Props.Count - 1].ResetToDefault();

                            if (Props[Props.Count - 1].Category == null)
                            {
                                ModCategory chunkAttr = (ModCategory)field.GetCustomAttribute(typeof(ModCategory), false);
                                if (chunkAttr != null)
                                {
                                    Props[Props.Count - 1].Category = chunkAttr.ID;
                                }
                                else
                                {
                                    ModCategory typeAttr = (ModCategory)field.DeclaringType.GetCustomAttribute(typeof(ModCategory), false);
                                    if (typeAttr != null)
                                    {
                                        Props[Props.Count - 1].Category = typeAttr.ID;
                                    }
                                    else
                                    {
                                        Props[Props.Count - 1].Category = 0;
                                    }
                                }
                            }

                            if (string.IsNullOrWhiteSpace(Props[Props.Count - 1].Name))
                            {
                                Props[Props.Count - 1].Name = field.Name;
                            }
                            if (Game.TextClass != null)
                            {
                                foreach (MethodInfo text in Game.TextClass.GetRuntimeMethods())
                                {
                                    if (text.Name == "get_" + field.Name)
                                    {
                                        Props[Props.Count - 1].Name = (string)text.Invoke(null, null);
                                    }
                                    else if (text.Name == "get_" + field.Name + "Desc")
                                    {
                                        Props[Props.Count - 1].Description = (string)text.Invoke(null, null);
                                    }
                                }
                            }
                            Props[Props.Count - 1].CodeName = field.Name;
                        }
                    }
                }
            }
        }

        public bool ModMenuEnabled => Props.Count > 0;

        public void InstallCrateSettings()
        {
            if (!ModMenuEnabled)
                return;

            for (int mod = 0; mod < ModCrates.SupportedMods.Count; mod++)
            {
                if (ModCrates.SupportedMods[mod].IsActivated && ModCrates.SupportedMods[mod].HasSettings)
                {
                    foreach (ModPropertyBase prop in Props)
                    {
                        if (ModCrates.SupportedMods[mod].Settings.ContainsKey(prop.CodeName) && !prop.HasChanged) // Manual mod menu changes override mod crates
                        {
                            prop.DeSerialize(ModCrates.SupportedMods[mod].Settings[prop.CodeName]);
                            prop.HasChanged = true;
                        }
                    }
                }
            }
        }

        public void LoadSettingsFromFile(string path)
        {
            FileInfo file = new FileInfo(path);

            bool isModCrate = file.Extension.ToLower() == ".zip";

            Dictionary<string, string> Settings = new Dictionary<string, string>();

            //zip handling
            if (isModCrate)
            {
                using (ZipArchive archive = ZipFile.OpenRead(file.FullName))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            if (entry.Name.ToLower() == ModCrates.SettingsFileName)
                            {
                                using (StreamReader fileStream = new StreamReader(entry.Open(), true))
                                {
                                    string line;
                                    while ((line = fileStream.ReadLine()) != null)
                                    {
                                        if (line[0] != ModCrates.CommentSymbol)
                                        {
                                            string[] setting = line.Split(ModCrates.Separator);
                                            Settings[setting[0]] = setting[1];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (StreamReader fileStream = new StreamReader(path, true))
                {
                    string line;
                    while ((line = fileStream.ReadLine()) != null)
                    {
                        if (line[0] != ModCrates.CommentSymbol)
                        {
                            string[] setting = line.Split(ModCrates.Separator);
                            if (setting.Length > 1)
                            {
                                Settings[setting[0]] = setting[1];
                            }
                        }
                    }
                }
            }

            if (Settings.Count == 0)
            {
                //MessageBox.Show(ModLoaderText.ModMenuLoad_Error);
                return;
            }

            foreach (ModPropertyBase prop in Props)
            {
                if (Settings.ContainsKey(prop.CodeName))
                {
                    prop.DeSerialize(Settings[prop.CodeName]);
                    prop.HasChanged = true;
                }
            }

        }

        public void SaveSettingsToFile(string path, bool fullSettings)
        {

            List<string> LineList = new List<string>();

            LineList.Add(string.Format("{0} {1} {2} {3}", ModCrates.CommentSymbol, ModLoaderText.ProgramTitle, ModLoaderGlobals.ProgramVersion, "Auto-Generated Settings File"));

            foreach (ModPropertyBase prop in Props)
            {
                if (fullSettings || (!fullSettings && prop.HasChanged))
                {
                    string text = "";
                    prop.Serialize(ref text);
                    LineList.Add(text);
                }
            }

            File.WriteAllLines(path, LineList);
        }

        public void SaveSimpleCrateToFile(string path, ModCrate crate)
        {
            List<string> LineList_Info = new List<string>();

            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Name, ModCrates.Separator, crate.Name));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Desc, ModCrates.Separator, crate.Desc));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Author, ModCrates.Separator, crate.Author));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Version, ModCrates.Separator, crate.Version));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_CML_Version, ModCrates.Separator, crate.CML_Version));
            LineList_Info.Add(string.Format("{0}{1}{2}", ModCrates.Prop_Game, ModCrates.Separator, crate.TargetGame));

            File.WriteAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.InfoFileName), LineList_Info);

            SaveSettingsToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.SettingsFileName), false);

            if (crate.Icon != null)
            {
                crate.Icon.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.IconFileName));
            }

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (ZipArchive zip = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.InfoFileName), ModCrates.InfoFileName);
                    zip.CreateEntryFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.SettingsFileName), ModCrates.SettingsFileName);
                    if (crate.Icon != null)
                    {
                        zip.CreateEntryFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.IconFileName), ModCrates.IconFileName);
                    }
                }
            }

            //cleanup
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.InfoFileName));
            File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.SettingsFileName));
            if (crate.Icon != null)
            {
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModCrates.IconFileName));
            }

        }

        public bool ModCratesManualInstall = false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.

        public abstract void StartModProcess();
        protected virtual void ModProcess() { }
        protected virtual void EndModProcess() { }

        public virtual void OpenModMenu() { }

        public abstract Game Game { get; }

    }
}
