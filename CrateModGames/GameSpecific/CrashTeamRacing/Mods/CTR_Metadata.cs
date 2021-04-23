using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CTRFramework.Lang;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class CTR_Metadata : ModStruct<string>
    {
        public override void ModPass(string path_extr)
        {
            LNG lng = new LNG(path_extr + @"lang\en.lng");
            string[] lang_lines = File.ReadAllLines(path_extr + @"lang\en.txt", Encoding.Default);
            for (int i = 0; i < lang_lines.Length; i++)
            {
                if (lang_lines[i].Contains("LOADING.."))
                {
                    lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                }
            }
            File.WriteAllLines(path_extr + @"lang\en.txt", lang_lines, Encoding.Default);
            lng.ConvertTXT(path_extr + @"lang\en.txt");
            File.Delete(path_extr + @"lang\en.txt");

            if (File.Exists(path_extr + @"lang\en2.lng"))
            {
                LNG lng1 = new LNG(path_extr + @"lang\en2.lng");
                string[] lang_lines1 = File.ReadAllLines(path_extr + @"lang\en2.txt", Encoding.Default);
                for (int i = 0; i < lang_lines1.Length; i++)
                {
                    if (lang_lines[i].Contains("LOADING.."))
                    {
                        lang_lines[i] = "CML " + ModLoaderGlobals.ProgramVersion + "|" + "SEED: " + ModLoaderGlobals.RandomizerSeed;
                    }
                }
                File.WriteAllLines(path_extr + @"lang\en2.txt", lang_lines1, Encoding.Default);
                lng1.ConvertTXT(path_extr + @"lang\en2.txt");
                File.Delete(path_extr + @"lang\en2.txt");
            }
        }
    }
}
