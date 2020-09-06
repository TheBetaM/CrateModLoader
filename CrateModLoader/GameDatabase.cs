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
        /// <summary> Set to true to enable mod crates. </summary>
        public bool ModCratesSupported;
        /// <summary> Text resource class used for mod menu property localization. </summary>
        public Type TextClass;
        /// <summary> Dictionary of mod menu property categories. </summary>
        public Dictionary<int, string> PropertyCategories;
        /// <summary> List of region identifiers for PS1 games. </summary>
        public RegionCode[] RegionID_PS1;
        /// <summary> List of region identifiers for PS2 games. </summary>
        public RegionCode[] RegionID_PS2;
        /// <summary> List of region identifiers for PSP games. </summary>
        public RegionCode[] RegionID_PSP;
        /// <summary> List of region identifiers for GCN games. </summary>
        public RegionCode[] RegionID_GCN;
        /// <summary> List of region identifiers for WII games. </summary>
        public RegionCode[] RegionID_WII;
        /// <summary> List of region identifiers for XBOX games. </summary>
        public RegionCode[] RegionID_XBOX;
        /// <summary> List of region identifiers for XBOX 360 games. </summary>
        public RegionCode[] RegionID_XBOX360;
        /// <summary> List of region identifiers for PC games. </summary>
        public RegionCode[] RegionID_PC;
        /// <summary> List of region identifiers for DC games. </summary>
        public RegionCode[] RegionID_DC;
        /// <summary> List of region identifiers for NDS games. </summary>
        public RegionCode[] RegionID_NDS;
        /// <summary> List of region identifiers for 3DS games. </summary>
        public RegionCode[] RegionID_N3DS;
    }

    public struct RegionCode
    {
        public string Name;
        public RegionType Region;
        public string ExecName;
        public string CodeName;
        public int RegionNumber;
        public int VersionNumber;
    }
}
