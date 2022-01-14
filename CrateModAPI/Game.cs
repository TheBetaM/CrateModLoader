using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class Game
    {
        /// <summary> Displayed name of the game. </summary>
        public abstract string Name { get; }
        /// <summary> Abbreviation for use in Mod Crates. </summary>
        public abstract string ShortName { get; }
        /// <summary> Console types to check for game detection. </summary>
        public abstract List<ConsoleMode> Consoles { get; }
        /// <summary> Detailed credit of the individual game's support. Leave as String.Empty if not applicable. </summary>
        public virtual string API_Credit => string.Empty;
        /// <summary> Website link to the most relevant API. Leave as String.Empty if not applicable. </summary>
        public virtual string API_Link => string.Empty;
        /// <summary> Set to true to disable mod crates. </summary>
        public virtual bool ModCratesDisabled => false;
        /// <summary> Set to true to enable Level Editor. </summary>
        public virtual bool EnableLevelEditor => false;
        /// <summary> Text resource class used for mod menu property localization. </summary>
        public virtual Type TextClass => null;
        /// <summary> Modder class (automatically set at runtime) </summary>
        public virtual Type ModderClass { get; }
        /// <summary> Dictionary of mod menu property categories. </summary>
        public virtual Dictionary<int, string> PropertyCategories => null;
        /// <summary> Dictionary of region identifiers. </summary>
        public abstract Dictionary<ConsoleMode, RegionCode[]> RegionID { get; }
        /// <summary> For PS1/PS2 CD games that work better with PSX2ISO than MKPSXISO </summary>
        public virtual bool UseLegacyMethod => false;
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
