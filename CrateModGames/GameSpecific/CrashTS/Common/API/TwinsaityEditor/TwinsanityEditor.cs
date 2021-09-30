using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace TwinsaityEditor
{
    internal static class TwinsanityEditor
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetDllDirectory(string path);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            int p = (int)Environment.OSVersion.Platform;
            if (p != 4 && p != 6 && p != 128)
            {
                //Thanks StackOverflow! http://stackoverflow.com/a/2594135/1122135
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                path = Path.Combine(path, IntPtr.Size == 8 ? "x64" : "x86");
                if (!SetDllDirectory(path))
                    throw new System.ComponentModel.Win32Exception();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }

    }
}
