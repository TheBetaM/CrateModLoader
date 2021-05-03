using System;
using System.Collections.Generic;

namespace CrateModLoader
{
    // Console types
    public enum ConsoleMode
    {
        Undefined = -1,
        PSP, // PlayStation Portable
        PS2, // PlayStation 2
        XBOX, // Xbox
        GCN, // Gamecube
        PS1, // PlayStation
        WII, // Wii
        XBOX360,  // Xbox 360
        PC, // PC CDROM/DVDROM, being considered
        DC, // Dreamcast, not supported yet
        PS3, // PlayStation 3, just for reference
        Android, // Android, being considered
        NDS, // DS, not supported yet
        N3DS, // 3DS, not supported yet
        N64, // N64, being considered
    }

    // Region types
    public enum RegionType
    {
        Undefined = -1,
        NTSC_U, // ex. North America
        PAL, // ex. Europe
        NTSC_J, // ex. Japan
        Global, // Region-free or region-less
    }

    public enum ModPass
    {
        Cache = 0,
        Mod,
        Preload,
    }
    public enum PipelinePass
    {
        Extract = 0,
        Build,
    }

    public static class ModLoaderGlobals
    {
        /// <summary> Global Randomizer Seed, can be modified during modding. Max length - 10 characters </summary>
        public static int RandomizerSeed = 0;

        /// <summary> String used to show which version of CML the modded game was built with. Should be under 10 characters. </summary>
        public const string ProgramVersion = "b1.4.0";
        public const uint ProgramVersionSimple = 7;
        /// <summary> Name of the folder used to hold game data during the modding process. Folder will be created in the program's directory. </summary>
        public const string TempName = "temp";
        /// <summary> Base directory of the program. </summary>
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary> Path to external tools' folder. Ends with '\' </summary>
        public static string ToolsPath = BaseDirectory + @"Tools\";
        /// <summary> Path to mods' folder. Ends with '\' </summary>
        public static string ModDirectory = BaseDirectory + @"Mods\";

        public static string ModAssetsFolderName = "modassets";

        //Common credits
        public static string Contributor_BetaM = "BetaM";
        public static string Contributor_ManDude = "ManDude";

    }

    public class CreditContributors
    {
        public List<string> Contributors;

        public CreditContributors(params string[] list)
        {
            Contributors = new List<string>(list);
        }

        public override string ToString()
        {
            string Credit = string.Empty;
            if (Contributors.Count > 0)
            {
                if (Contributors.Count > 1)
                {
                    for (int i = 0; i < Contributors.Count; i++)
                    {
                        if (i != Contributors.Count - 1)
                        {
                            Credit += Contributors[i];
                        }
                        else
                        {
                            Credit += Contributors[i] + ", ";
                        }
                    }
                }
                else
                {
                    Credit = Contributors[0];
                }
            }
            return Credit;
        }
    }
}
