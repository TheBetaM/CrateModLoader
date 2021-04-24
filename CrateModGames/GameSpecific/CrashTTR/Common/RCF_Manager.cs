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

        public RCF_Manager(string path)
        {
            cachedRCF = new RCF();
            cachedRCF.OpenRCF(path);
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
            Recursive_CheckFiles(path_extr, source, path_extr);

            int error = 0;
            for (int i = 0; i < cachedRCF.Header.Files; i++)
            {
                if (cachedRCF.Header.T2File[cachedRCF.Header.T1File[i].Pos].External == "")
                {
                    error++;
                    Console.WriteLine("Missing external file: " + cachedRCF.Header.T2File[cachedRCF.Header.T1File[i].Pos].Name);
                }
            }
            if (error != 0)
            {
                throw new Exception($"External paths failed, crashed and burned {error} times!");
            }

            await cachedRCF.PackAsync(path + "1", path_extr);

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
            isBusy = false;
        }

        public void Pack(string path, string path_extr)
        {

            // set externals
            DirectoryInfo source = new DirectoryInfo(path_extr);
            Recursive_CheckFiles(path_extr, source, path_extr);

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

        public void Recursive_CheckFiles(string path_extr, DirectoryInfo di, string buffer)
        {
            string relativePath = buffer.Substring(path_extr.Length);
            //Console.WriteLine(relativePath);

            foreach (FileInfo file in di.EnumerateFiles())
            {
                for (int i = 0; i < cachedRCF.Header.T2File.Length; i++)
                {
                    if (cachedRCF.Header.T2File[i].Name == relativePath + @"\" + file.Name)
                    {
                        cachedRCF.Header.T2File[i].External = path_extr + relativePath + @"\" + file.Name;
                        //Console.WriteLine("external correct " + cachedRCF.Header.T2File[i].External);
                        break;
                    }
                    else if (relativePath == "" && cachedRCF.Header.T2File[i].Name == file.Name)
                    {
                        cachedRCF.Header.T2File[i].External = path_extr + relativePath + @"\" + file.Name;
                        //Console.WriteLine("external correct " + cachedRCF.Header.T2File[i].External);
                        break;
                    }
                }
            }

            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                string newbuffer = Path.Combine(buffer, dir.Name);
                Recursive_CheckFiles(path_extr, dir, newbuffer);
            }
        }


    }
}
