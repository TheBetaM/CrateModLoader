using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CTRFramework.Lang;
using CTRFramework.Shared;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class CTR_Metadata : ModStruct<string>
    {
        // todo improve this
        public override void ModPass(string path_extr)
        {
            LNG lng = LNG.FromFile(path_extr + @"lang\en.lng");
            lng.Export(path_extr + @"lang\en.txt");

            string[] lang_lines = File.ReadAllLines(path_extr + @"lang\en.txt", Encoding.Default);
            for (int i = 0; i < lang_lines.Length; i++)
            {
                if (lang_lines[i].Contains("LOADING.."))
                {
                    lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                }
            }
            File.WriteAllLines(path_extr + @"lang\en.txt", lang_lines, Encoding.Default);

            LNG after = LNG.FromText(File.ReadAllLines(path_extr + @"lang\en.txt", Encoding.Default), true);
            File.Delete(path_extr + @"lang\en.txt");
            after.Save(path_extr + @"lang\en.lng");

            if (File.Exists(path_extr + @"lang\en2.lng"))
            {
                LNG lng1 = LNG.FromFile(path_extr + @"lang\en2.lng");
                lng1.Export(path_extr + @"lang\en2.txt");

                string[] lang_lines1 = File.ReadAllLines(path_extr + @"lang\en2.txt", Encoding.Default);
                for (int i = 0; i < lang_lines1.Length; i++)
                {
                    if (lang_lines[i].Contains("LOADING.."))
                    {
                        lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                    }
                }
                File.WriteAllLines(path_extr + @"lang\en2.txt", lang_lines1, Encoding.Default);

                LNG after1 = LNG.FromText(File.ReadAllLines(path_extr + @"lang\en2.txt", Encoding.Default), true);
                File.Delete(path_extr + @"lang\en2.txt");
                after1.Save(path_extr + @"lang\en2.lng");
            }
            
        }
    }
}
