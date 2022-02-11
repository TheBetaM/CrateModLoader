using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    // todo: seed, options, props
    static class ModLoaderCLI
    {

        public static string[] HelpText = new string[]
        {
            "Crate Mod Loader:",
            "  Takes the input game, extracts it, modifies the files and then rebuilds it into a modded game.",
            "Usage:",
            "  CrateModLoader.exe \"[Input path]\" \"[Output path]\" [options]",
            "Options:",
            "  -help               Displays this help message.",
            "  -q                  Silences the console output as much as possible.",
            //"  -s 123456789        Sets the randomizer seed to the specified value.",
            //"  -o 1;2;3            Enables the 1st, 2nd and 3rd quick menu options of the game, if supported and available. (the rest become disabled)",
            //"  -p \"Example=2.5\"  Sets the property with the specified code name to the given value.",
            //"                      To learn the code names, check the source code or the Mod Menu (save settings as Full Settings file).",
            "  -c \"[Path]\"       Installs a Mod Crate from the specified path into the game if suppoted and available.",

        };
        public static string StartupEmptyText = "Start with -help to learn commandline usage.";
        public static string Error_InvalidArgs = "Invalid arguments.";
        public static string Error_InputNotFound = "Input file not found.";

        public static bool QuietMode = false;

        public static void StartCLI(ModLoader ModProgram, string [] args)
        {
            List<string> arglist = new List<string>(args);
            QuietMode = arglist.Contains("-q");

            if (!QuietMode)
            {
                Console.WriteLine();
                Console.Write(ModLoaderText.ProgramTitle);
                Console.Write(" ");
                Console.Write(ModLoaderGlobals.ProgramVersion);
                Console.WriteLine();
                Console.WriteLine();
            }

            switch (args.Length)
            {
                case 0:
                    PopMessage(StartupEmptyText);
                    break;
                case 1:
                    switch (args[0])
                    {
                        default:
                            PopMessage(Error_InvalidArgs);
                            break;
                        case "-help":
                        case "-h":
                        case "--help":
                            foreach (string text in HelpText)
                            {
                                PopMessage(text);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }

            if (!File.Exists(arglist[0]))
            {
                PopMessage(Error_InputNotFound);
                return;
            }

            for (int i = 0; i < arglist.Count; i++)
            {
                
            }

            
        }

        public static void PopMessage(string text)
        {
            if (!QuietMode)
            {
                Console.WriteLine(text);
            }
        }


    }
}
