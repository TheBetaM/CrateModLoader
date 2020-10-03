﻿using System;
using System.Collections.Generic;
using System.Reflection;

// Global variables to be accessed in Modders
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

    public static class ModLoaderGlobals
    {

        /// <summary> Global access to the main ModLoader class </summary>
        public static ModLoader ModProgram;

        /// <summary> Global Randomizer Seed, can be modified during modding. Max length - 10 characters </summary>
        public static int RandomizerSeed = 0;

        /// <summary> Do not access this, UI related. </summary>
        public static int RandomizerSeedBase = 0;

        /// <summary> String used to show which version of CML the modded game was built with. Should be under 10 characters. </summary>
        public static string ProgramVersion = "v1.3.0";
        public static uint ProgramVersionSimple = 6;

        /// <summary> Console of the currently loaded ROM. </summary>
        public static ConsoleMode Console = ConsoleMode.Undefined;

        /// <summary> Region of the currently loaded ROM. </summary>
        public static RegionType Region = RegionType.Undefined;

        /// <summary> Path to the input ROM or directory. Set by drag/drop or system browser. Displayed in the upmost text box. </summary>
        public static string InputPath = "";

        /// <summary> Path to the output ROM or directory. Set by system browser. Displayed in the second-to-upmost text box. </summary>
        public static string OutputPath = "";

        /// <summary> Name of the folder used to hold game data during the modding process. Folder will be created in the program's directory. </summary>
        public static string TempName = "temp";

        /// <summary> Base directory of the program. </summary>
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary> Full path to the extracted files' folder. Differs based on console, but always points to the same game data. Ends with '\' </summary>
        public static string ExtractedPath
        {
            get
            {
                return BaseDirectory + ProcessPath;
            }
        }

        /// <summary> Relative path to the extracted files' folder that starts with the temp folder's name. Ends with '\' </summary>
        public static string ProcessPath
        {
            get
            {
                switch (Console)
                {
                    default:
                        return TempName + @"\";
                    case ConsoleMode.PS1:
                    case ConsoleMode.PS2:
                    case ConsoleMode.XBOX:
                    case ConsoleMode.XBOX360:
                        return TempName + @"\";
                    case ConsoleMode.GCN:
                        return TempName + @"\P-" + ProductCode.Substring(0, 4) + @"\files\";
                    case ConsoleMode.WII:
                        return TempName + @"\DATA\files\";
                    case ConsoleMode.PSP:
                        return TempName + @"\PSP_GAME\USRDIR\";
                }
            }
        }

        /// <summary> Executable file name from the detected RegionCode struct of the currently loaded ROM. ex. "SLUS_209.09" </summary>
        public static string ExecutableName = "";

        /// <summary> Label of the input ISO image. Used only for building the ISO image. </summary>
        public static string ISO_Label = "";

        /// <summary> Path to external tools' folder. Ends with '\' </summary>
        public static string ToolsPath = BaseDirectory + @"Tools\";

        /// <summary> Path to mods' folder. Ends with '\' </summary>
        public static string ModDirectory = BaseDirectory + @"Mods\";

        /// <summary> Partial path to the extracted files. Use ExtractedPath instead!! </summary>
        public static string TempPath
        {
            get
            {
                switch (Console)
                {
                    default:
                        return BaseDirectory + TempName + @"\";
                    case ConsoleMode.PS1:
                    case ConsoleMode.PS2:
                    case ConsoleMode.XBOX:
                    case ConsoleMode.XBOX360:
                    case ConsoleMode.PSP:
                        return BaseDirectory + TempName + @"\";
                    case ConsoleMode.GCN:
                    case ConsoleMode.WII:
                        return BaseDirectory + TempName;
                }
            }
        }

        public static bool KeepTempFiles = false;

        /// <summary> Product code used as a folder name after extracting (for GC). Usually no need to access this - use ExtractedPath instead! </summary>
        public static string ProductCode = "";

        /// <summary> Supported game list automatically populated on boot.  </summary>
        public static Dictionary<Game, Assembly> SupportedGames;

        /// <summary> Supported pipeline list automatically populated on boot. </summary>
        public static Dictionary<ModPipelineInfo, Type> SupportedConsoles;

    }
}
