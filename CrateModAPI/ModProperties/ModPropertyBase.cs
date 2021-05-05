using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    public abstract class ModPropertyBase
    {

        public string Name;

        public string Description;

        /// <summary> The property's name in code </summary>
        public string CodeName;

        /// <summary> UI category tab ID </summary>
        public int? Category = null;

        /// <summary> True if value changed from default </summary>
        public bool HasChanged = false;

        /// <summary> True if meant to be hidden from UI </summary>
        public bool Hidden = false;

        /// <summary> True if it requires to be preloaded from game data to be visible and editable. Does not enforce preload for "dry" property use! </summary>
        public bool RequiresPreload = false;

        /// <summary> True if preload extends functionality </summary>
        public bool PreloadBonus = false;

        /// <summary> (for ModPropOption) True if it doesn't show up in the quick options in the main window </summary>
        public bool ModMenuOnly = false;

        public List<Type> TargetMods = null;

        /// <summary> Region list for the property to be allowed for </summary>
        public List<RegionType> AllowedRegions { get; set; }

        /// <summary> Console list for the property to be allowed for </summary>
        public List<ConsoleMode> AllowedConsoles { get; set; }

        /// <summary> Appends input string with a serialized version of the property. </summary>
        public abstract void Serialize(ref string input);

        /// <summary> Changes the property's value by parsing the input string. </summary>
        public abstract void DeSerialize(string input, ModCrate crate);

        /// <summary> Resets the property's value to its default state. </summary>
        public abstract void ResetToDefault();

        /// <summary> Force the option order in the quick options list (does not affect execution order) </summary>
        public uint? ListIndex = null; 

        public bool Allowed(ConsoleMode Console, RegionType Region)
        {
            if (Hidden)
                return false;
            if (AllowedRegions != null && AllowedRegions.Count > 0 && !AllowedRegions.Contains(Region))
                return false;
            if (AllowedConsoles != null && AllowedConsoles.Count > 0 && !AllowedConsoles.Contains(Console))
                return false;

            return true;
        }

        public virtual void ValueChange(object sender, EventArgs e)
        {
            HasChanged = true;
        }
    }
}
