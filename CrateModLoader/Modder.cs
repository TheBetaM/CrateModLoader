using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using CrateModLoader.Resources.Text;
using CrateModLoader.ModProperties;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new modder class that inherits this abstract class.
     * 2. Fill its Game member with the appropriate info in the class constructor.
     * 3. Override StartModProcess (at least, there are more modding functions that can be overriden but are optional).
     * (optional) 4. Localize game title, API credit, and options using text resources.
     * (optional) 5. Create ModProperty variables for automatic Mod Menu setup.
     * 6. Done.
     * 
     */

    /// <summary>
    /// Abstract Modder class, inherited by all game modders
    /// </summary>
    public abstract class Modder
    {
        public Dictionary<int,ModOption> Options { get; } = new Dictionary<int,ModOption>();

        public List<ModPropertyBase> Props = new List<ModPropertyBase>();
        public Dictionary<int, string> PropCategories = new Dictionary<int, string>();

        public Modder()
        {
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
                            Props[Props.Count - 1].CodeName = field.Name;
                        }
                    }
                }
            }

        }

        public bool ModMenuEnabled => Props.Count > 0;

        // Use this instead of Options.Add!
        /// <summary>
        /// Adds an option to the dictionary if the region and console is allowed for it.
        /// </summary>
        /// <param name="id">Option ID in dictionary</param>
        /// <param name="option">Option constructor</param>
        public virtual void AddOption(int id, ModOption option)
        {
            if (option.AllowedConsoles.Count > 0 && !option.AllowedConsoles.Contains(ModLoaderGlobals.Console))
            {
                return;
            }
            if (option.AllowedRegions.Count > 0 && !option.AllowedRegions.Contains(ModLoaderGlobals.Region))
            {
                return;
            }
            Options.Add(id, option);
        }

        // Use this! Mostly for error handling and when options may be missing.
        /// <summary>
        /// Gets the enabled state of the mod option (false if option doesn't exist)
        /// </summary>
        /// <param name="id">Option ID in dictionary</param>
        public virtual bool GetOption(int id)
        {
            if (Options.ContainsKey(id) && Options[id].Enabled)
            {
                return true;
            }
            return false;
        }

        /// <summary> Hexadecimal display of which quick options were selected (automatically adjusts according the amount of quick options) - MSB is first option from the top </summary>
        public virtual string OptionsSelectedString
        {
            get
            {
                string str = string.Empty;
                if (Options != null && Options.Count > 0)
                {
                    for (int l = 0; l < (Options.Count + 31) / 32; ++l)
                    {
                        int val = 0;
                        for (int i = 0, s = Math.Min(32, Options.Count - l * 32); i < s; ++i)
                        {
                            if (Options.ContainsKey(l * 32 + i) && Options[l * 32 + i] is ModOption o)
                            {
                                if (o.Enabled)
                                    val |= 1 << (31 - i);
                            }
                        }
                        str += val.ToString("X08");
                    }
                }
                else
                {
                    str = "00000000";
                }
                return str;
            }
        }

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
                MessageBox.Show(ModLoaderText.ModMenuLoad_Error);
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

        public bool ModCratesManualInstall = false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.

        public abstract void StartModProcess();
        protected virtual void ModProcess() { }
        protected virtual void EndModProcess() { }

        public virtual void OpenModMenu()
        {
            MessageBox.Show(ModLoaderText.ModMenuMissingErrorPopup, ModLoaderText.ErrorPopupTitle, MessageBoxButtons.OK);
        }

        public Game Game { get; protected set; }
    }
}
