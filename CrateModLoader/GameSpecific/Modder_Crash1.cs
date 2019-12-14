using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Crash;
//Crash 1 API by chekwob and ManDude

namespace CrateModLoader
{
    public sealed class Modder_Crash1
    {
        public string gameName = "Crash 1";
        public string apiCredit = "API by chekwob and ManDude";
        public System.Drawing.Image gameIcon = null;
        public string[] modOptions = {
            "Randomize sound effects"
        };

        public bool RandomizeADIO = false;

        public enum ModOptions
        {
            RandomizeADIO = 0
        }

        public void OptionChanged(int option, bool value)
        {
            switch ((ModOptions)option)
            {
                case ModOptions.RandomizeADIO:
                    RandomizeADIO = value;
                    break;
            }
        }
        
        public void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();

            EndModProcess();
        }

        private void ModProcess()
        {
            Random rand = new Random(Program.ModProgram.randoSeed);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(Program.ModProgram.extractedPath);
            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs

            for (int i = 0; i < Math.Min(nsfs.Count, nsds.Count); ++i)
            {
                FileInfo nsfFile = nsfs[i];
                FileInfo nsdFile = nsds[i];
                if (Path.GetFileNameWithoutExtension(nsfFile.Name) != Path.GetFileNameWithoutExtension(nsdFile.Name))
                {
                    MessageBox.Show($"NSF/NSD file pair mismatch. First mismatch:\n\n{nsfFile.Name}\n{nsdFile.Name}");
                    return;
                }
                if (RandomizeADIO) Mod_RandomizeADIO(nsfFile, nsdFile, rand);
            }
        }

        private void AppendFileInfoDir(IList<FileInfo> nsfpaths, IList<FileInfo> nsdpaths, DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                AppendFileInfoDir(nsfpaths, nsdpaths, dir);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToUpper() == ".NSF") nsfpaths.Add(file);
                else if (file.Extension.ToUpper() == ".NSD") nsdpaths.Add(file);
            }
        }

        public void EndModProcess()
        {
            // ...or here
        }

        private void Mod_RandomizeADIO(FileInfo nsfFile, FileInfo nsdFile, Random rand)
        {
            // spidaphipahfahpajfa
        }
    }
}
