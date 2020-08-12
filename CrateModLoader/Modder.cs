﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using CrateModLoader.Resources.Text;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new modder class that inherits this abstract class.
     * 2. Fill its Game member with the appropriate info in the class constructor.
     * 3. Override StartModProcess (at least, there are more modding functions that can be overriden but are optional).
     * (optionally) 4. Localize game title, API credit, and options using text resources.
     * 5. Done.
     * 
     */

    /// <summary>
    /// Abstract Modder class, inherited by all game modders
    /// </summary>
    public abstract class Modder
    {
        public Dictionary<int,ModOption> Options { get; } = new Dictionary<int,ModOption>();

        public List<ModProperty> Props = new List<ModProperty>();

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
                        if (field.FieldType == typeof(ModProperty))
                        {
                            Props.Add((ModProperty)field.GetValue(null));
                        }
                    }
                }
            }

        }

        public bool ModMenuEnabled
        {
            get
            {
                return Props.Count > 0;
            }
        }

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
