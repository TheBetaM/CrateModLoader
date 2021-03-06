﻿using System;
using System.Collections.Generic;
using System.IO;
using RadcoreCementFile;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public static class RCF_Manager
    {

        // To prevent high memory usage, either clear cachedRCF or Pack after being done
        public static RCF cachedRCF;

        public static void Extract(string path, string dest)
        {
            cachedRCF = new RCF();
            cachedRCF.OpenRCF(path);
            Directory.CreateDirectory(dest);
            cachedRCF.ExtractRCF(dest);
        }

        public static void Pack(string path, string path_extr)
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

        static void Recursive_CheckFiles(string path_extr, DirectoryInfo di, string buffer)
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
