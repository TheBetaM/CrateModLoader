using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CrateModLoader
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main(string[] args)
        {
            ModLoaderGlobals.ModProgram = new ModLoader();
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
