using System;
using System.Collections.Generic;
using System.Text;
using CrateModAPI.Resources.Text;

namespace CrateModLoader
{
    static class ModLoaderCLI
    {

        public static void StartCLI(string [] args)
        {
            Console.WriteLine();
            Console.Write(ModLoaderText.ProgramTitle);
            Console.Write(" ");
            Console.Write(ModLoaderGlobals.ProgramVersion);
            Console.WriteLine("Press Enter to exit.");
            Console.WriteLine();
            Console.ReadLine();
        }


    }
}
