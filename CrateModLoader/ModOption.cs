using System.Collections.Generic;

namespace CrateModLoader
{
    public class ModOption
    {
        /// <summary>
        /// Mod option allowed on all consoles and regions.
        /// </summary>
        /// <param name="name">Option name</param>
        /// <param name="enabled">Default enabled state, defaults to not enabled</param>
        public ModOption(string name, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
            AllowedRegions = new List<RegionType>();
            AllowedConsoles = new List<ConsoleMode>();
        }
        /// <summary>
        /// Mod option allowed only on listed regions.
        /// </summary>
        /// <param name="name">Option name</param>
        /// <param name="regions">List of regions where the option will appear</param>
        /// <param name="enabled">Default enabled state, defaults to not enabled</param>
        public ModOption(string name, List<RegionType> regions, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
            AllowedRegions = regions;
            AllowedConsoles = new List<ConsoleMode>();
        }
        /// <summary>
        /// Mod option allowed only on listed consoles.
        /// </summary>
        /// <param name="name">Option name</param>
        /// <param name="consoles">List of consoles where the option will appear</param>
        /// <param name="enabled">Default enabled state, defaults to not enabled</param>
        public ModOption(string name, List<ConsoleMode> consoles, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
            AllowedRegions = new List<RegionType>();
            AllowedConsoles = consoles;
        }
        /// <summary>
        /// Mod option allowed only on listed consoles and regions.
        /// </summary>
        /// <param name="name">Option name</param>
        /// <param name="regions">List of regions where the option will appear</param>
        /// <param name="consoles">List of consoles where the option will appear</param>
        /// <param name="enabled">Default enabled state, defaults to not enabled</param>
        public ModOption(string name, List<RegionType> regions, List<ConsoleMode> consoles, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
            AllowedRegions = regions;
            AllowedConsoles = consoles;
        }

        /// <summary>
        /// Option name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Current enabled state, please use Modder.GetOption to get state instead of this directly!
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// List of regions where the option will appear
        /// </summary>
        public List<RegionType> AllowedRegions { get; }
        /// <summary>
        /// List of consoles where the option will appear
        /// </summary>
        public List<ConsoleMode> AllowedConsoles { get; }

        public override string ToString() => Name;
    }
}
