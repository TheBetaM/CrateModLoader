using System;
using System.Collections.Generic;
using System.IO;
using RadcoreCementFile;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class RCF_Manager
    {
        public RCF cachedRCF;
        public bool isBusy = false;

        public RCF_Manager(Modder meta, string path)
        {
            cachedRCF = new RCF();
            cachedRCF.OpenRCF(path);
            meta.PassCount += (int)cachedRCF.Header.Files;
        }

        public void Extract(string path, string dest)
        {
            isBusy = true;
            Directory.CreateDirectory(dest);
            cachedRCF.ExtractRCF(dest);
            isBusy = false;
        }

        public async Task ExtractAsync(Modder meta, string path, string dest)
        {
            isBusy = true;
            Directory.CreateDirectory(dest);
            await cachedRCF.ExtractRCFAsync(meta, dest);
            isBusy = false;
        }

        public async Task PackAsync(string path, string path_extr)
        {
            isBusy = true;
            // set externals
            DirectoryInfo source = new DirectoryInfo(path_extr);
            Recursive_CheckFiles(path_extr);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await cachedRCF.PackAsync(path, path_extr);

            // Extraction cleanup
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                try
                {
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path_extr);
                }
                catch (IOException)
                {
                    Console.WriteLine("IO: couldn't remove folder + " + di.FullName);
                }
            }

            cachedRCF = null;
            isBusy = false;
        }

        public void Pack(string path, string path_extr)
        {

            // set externals
            DirectoryInfo source = new DirectoryInfo(path_extr);
            Recursive_CheckFiles(path_extr);

            cachedRCF.Pack(path + "1");

            // Extraction cleanup
            File.Delete(path);
            File.Move(path + "1", path);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                try
                {
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                    Directory.Delete(path_extr);
                }
                catch (IOException)
                {
                    Console.WriteLine("IO: couldn't remove folder + " + di.FullName);
                }
            }

            cachedRCF = null;
        }

        public void Recursive_CheckFiles(string path_extr)
        {
            for (int i = 0; i < cachedRCF.Header.T2File.Length; i++)
            {
                string name = cachedRCF.Header.T2File[i].Name;
                if (name.StartsWith(@"\") || name.StartsWith(@"/"))
                {
                    name = name.Substring(1);
                }
                string check = path_extr + cachedRCF.Header.T2File[i].Name;
                if (File.Exists(check))
                {
                    cachedRCF.Header.T2File[i].External = check;
                }
                else
                {
                    Console.WriteLine(path_extr);
                    Console.WriteLine(cachedRCF.Header.T2File[i].Name);
                    Console.WriteLine(check);
                    throw new Exception("External file not found for: " + cachedRCF.Header.T2File[i].Name);
                }
            }
        }

    }
}
