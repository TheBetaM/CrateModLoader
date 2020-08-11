using System;
using System.Windows.Forms;

namespace CrateModLoader
{
    static class Program
    {
        public static ModLoader ModProgram;
        public static ModLoaderForm ModProgramForm;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ModProgram = new ModLoader();
                using (ModProgramForm = new ModLoaderForm())
                {
                    Application.Run(ModProgramForm);
                }
            }
            else
            {
                ModProgram = new ModLoader();
                //todo: commandline
            }
        }
    }
}
