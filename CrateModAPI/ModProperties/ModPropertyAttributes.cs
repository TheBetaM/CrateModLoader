using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{


    /// <summary>
    /// Category tabs in the Mod Menu
    /// </summary>
    public class ModCategory : Attribute
    {
        public int ID { get; set; }

        public ModCategory(int CategoryID)
        {
            ID = CategoryID;
        }
    }

    /// <summary>
    /// Force the option order in the quick options list (does not affect execution order)
    /// </summary>
    public class ModListOrder : Attribute
    {
        public uint ID { get; set; }

        public ModListOrder(uint OrderID)
        {
            ID = OrderID;
        }
    }

    /// <summary>
    /// Add to not show the ModPropOption in the quick options in the main window
    /// </summary>
    public class ModMenuOnly : Attribute { }

    /// <summary>
    /// Force the property to only be accessible on set consoles
    /// </summary>
    public class ModAllowedConsoles : Attribute
    {
        public List<ConsoleMode> Allowed;

        public ModAllowedConsoles(params ConsoleMode[] args)
        {
            Allowed = new List<ConsoleMode>(args);
        }
    }

    /// <summary>
    /// Force the property to only be accessible on set regions
    /// </summary>
    public class ModAllowedRegions : Attribute
    {
        public List<RegionType> Allowed;

        public ModAllowedRegions(params RegionType[] args)
        {
            Allowed = new List<RegionType>(args);
        }
    }

    /// <summary>
    /// Hide property from all UI
    /// </summary>
    public class ModHidden : Attribute { }

    /// <summary>
    /// Changing the property executes a Mod
    /// </summary>
    public class ExecutesMods : Attribute
    {
        public List<Type> Mods;

        public ExecutesMods(params Type[] args)
        {
            Mods = new List<Type>(args);
        }
    }

    /// <summary>
    /// Property requires preload to be visible
    /// </summary>
    public class ModRequiresPreload : Attribute {  }

    /// <summary>
    /// Property can get preloaded using the Mod(s) that are linked to the property
    /// </summary>
    public class ModPreloadBonus : Attribute { }
}
