using System.Collections.Generic;
using System.Drawing;

namespace CrateModLoader
{
    public struct Game
    {
        /// <summary> Displayed name of the game. </summary>
        public string Name;
        /// <summary> Console types to check for game detection. </summary>
        public List<ConsoleMode> Consoles;
        /// <summary> Detailed credit of the individual game's support. </summary>
        public string API_Credit;
        /// <summary> Display an icon or set to null to not display one. </summary>
        public Image Icon;
        /// <summary> Set to true to enable mod menu. </summary>
        public bool ModMenuEnabled;
        /// <summary> Set to true to enable mod crates. </summary>
        public bool ModCratesSupported;
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
    }

    public struct RegionCode
    {
        public string Name;
        public RegionType Region;
        public string ExecName;
        public string CodeName;
    }
}
