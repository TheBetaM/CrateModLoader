using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using CrateModLoader.GameSpecific.Crash1;
using CrateModLoader.Tools;

namespace CrateModLoader
{
    //CML Windows Forms Program variant
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        // If this isn't here it doesn't preload CrateModGames...
        private static Crash1_Levels Test = Crash1_Levels.L01_NSanityBeach;
        // If this isn't here it doesn't preload CrateModConsoles...
        private static PS2ImageMaker.ProgressState Test2 = PS2ImageMaker.ProgressState.FINISHED;

        [STAThread]
        static void Main(string[] args)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            ModLoaderGlobals.ModProgram = new ModLoader(assemblies);
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (ModLoaderForm ModProgramForm = new ModLoaderForm())
                {
                    Application.Run(ModProgramForm);
                }
            }
            else
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
                ModLoaderCLI.StartCLI(args);
            }
        }
    }
}
