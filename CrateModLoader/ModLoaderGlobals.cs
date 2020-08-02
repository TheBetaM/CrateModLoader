using System;

// Global variables to be accessed in Modders
namespace CrateModLoader
{
    // Console types
    public enum ConsoleMode
    {
        Undefined = -1,
        PSP = 0, // PlayStation Portable
        PS2 = 1, // PlayStation 2
        XBOX = 2, // Xbox
        GCN = 3, // Gamecube
        PS1 = 4, // PlayStation
        WII = 5, // Wii
        XBOX360 = 6,  // Xbox 360
        PC = 7, // PC CDROM/DVDROM, being considered
        DC = 8, // Dreamcast, not supported yet
        PS3 = 9, // PlayStation 3, just for reference
        Android = 10, // Android, being considered
        NDS = 11, // DS, not supported yet
        N3DS = 12, // 3DS, not supported yet
        N64 = 13, // N64, being considered
    }

    // Region types
    public enum RegionType
    {
        Undefined = -1,
        NTSC_U = 0, // ex. North America
        PAL = 1, // ex. Europe
        NTSC_J = 2, // ex. Japan
        Global = 3, // Region-free or region-less
    }

    public static class ModLoaderGlobals
    {

        /// <summary> Global Randomizer Seed, can be modified during modding. Max length - 10 characters </summary>
        public static int RandomizerSeed = 0;

        /// <summary> String used to show which version of CML the modded game was built with. Should be under 10 characters. </summary>
        public static string ProgramVersion = "v1.2.0";

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

        /// <summary> Full path to the extracted files' folder. Differs based on console, but always points to the same game data. Ends with '\' </summary>
        public static string ExtractedPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + ProcessPath;
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
        public static string ToolsPath = AppDomain.CurrentDomain.BaseDirectory + @"\Tools\";

        /// <summary> Path to mods' folder. Ends with '\' </summary>
        public static string ModDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\Mods\";

        /// <summary> Partial path to the extracted files. Use ExtractedPath instead!! </summary>
        public static string TempPath
        {
            get
            {
                switch (Console)
                {
                    default:
                        return AppDomain.CurrentDomain.BaseDirectory + TempName + @"\";
                    case ConsoleMode.PS1:
                    case ConsoleMode.PS2:
                    case ConsoleMode.XBOX:
                    case ConsoleMode.XBOX360:
                    case ConsoleMode.PSP:
                        return AppDomain.CurrentDomain.BaseDirectory + TempName + @"\";
                    case ConsoleMode.GCN:
                    case ConsoleMode.WII:
                        return AppDomain.CurrentDomain.BaseDirectory + TempName;
                }
            }
        }

        /// <summary> Product code used as a folder name after extracting (for GC). Usually no need to access this - use ExtractedPath instead! </summary>
        public static string ProductCode = "";

    }
}
