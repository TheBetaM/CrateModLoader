using System;
using System.Windows.Forms;

namespace CrateModLoader
{
    static class Program
    {
        public static ModLoader ModProgram;
        public static ModLoaderForm ModProgramForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (ModProgramForm = new ModLoaderForm())
            {
                ModProgram = new ModLoader();
                Application.Run(ModProgramForm);
            }
        }
    }
}
