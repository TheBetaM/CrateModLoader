using System;
using System.Collections.Generic;
using System.Drawing;

namespace CrateModLoader
{
    public struct Game
    {
        /// <summary> Displayed name of the game. </summary>
        public string Name;
        /// <summary> Abbreviation for use in Mod Crates. </summary>
        public string ShortName;
        /// <summary> Console types to check for game detection. </summary>
        public List<ConsoleMode> Consoles;
        /// <summary> Detailed credit of the individual game's support. Leave as String.Empty if not applicable. </summary>
        public string API_Credit;
        /// <summary> Website link to the most relevant API. Leave as String.Empty if not applicable. </summary>
        public string API_Link;
        /// <summary> Display an icon or set to null to not display one. </summary>
        public Image Icon;
        /// <summary> Set to true to disable mod crates. </summary>
        public bool ModCratesDisabled;
        /// <summary> Text resource class used for mod menu property localization. </summary>
        public Type TextClass;
        /// <summary> Modder class (automatically set at runtime) </summary>
        public Type ModderClass;
        /// <summary> Dictionary of mod menu property categories. </summary>
        public Dictionary<int, string> PropertyCategories;
        /// <summary> Dictionary of region identifiers. </summary>
        public Dictionary<ConsoleMode, RegionCode[]> RegionID;
    }

    public struct RegionCode
    {
        public RegionType Region;
        public string Name;
        public string ExecName;
        public string CodeName;
        public int RegionNumber;
        public int VersionNumber;
    }
}
