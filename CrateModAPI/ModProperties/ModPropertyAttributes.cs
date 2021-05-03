using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{

    // Category tabs in the Mod Menu
    public class ModCategory : Attribute
    {
        public int ID { get; set; }

        public ModCategory(int CategoryID)
        {
            ID = CategoryID;
        }
    }

    // Add to not show the ModPropOption in the quick options in the main window
    public class ModMenuOnly : Attribute
    {

    }

    // Force the property to only be accessible on set consoles
    public class ModAllowedConsoles : Attribute
    {
        public List<ConsoleMode> Allowed;

        public ModAllowedConsoles(params ConsoleMode[] args)
        {
            Allowed = new List<ConsoleMode>(args);
        }
    }

    // Force the property to only be accessible on set regions
    public class ModAllowedRegions : Attribute
    {
        public List<RegionType> Allowed;

        public ModAllowedRegions(params RegionType[] args)
        {
            Allowed = new List<RegionType>(args);
        }
    }

    // Hide property from all UI
    public class ModHidden : Attribute
    {

    }

    // Changing the property executes a Mod
    public class ExecutesMods : Attribute
    {
        public List<Type> Mods;

        public ExecutesMods(params Type[] args)
        {
            Mods = new List<Type>(args);
        }
    }
}
