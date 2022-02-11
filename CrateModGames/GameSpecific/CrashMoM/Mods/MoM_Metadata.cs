using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashMoM
{
    public class MoM_Metadata : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"default\";
            // Proof of concept mod replacing credits text
            string[] credits_lines = File.ReadAllLines(path_extr + @"script\CreditsList.txt");

            List<string> credits_LineList = new List<string>();
            credits_LineList.Add(credits_lines[0]);

            credits_LineList.Add("false        \"Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + "\"                 false           false");
            credits_LineList.Add("false        \"Seed: " + ModLoaderGlobals.RandomizerSeed + "\"                 false           false");

            for (int i = 1; i < credits_lines.Length; i++)
            {
                credits_LineList.Add(credits_lines[i]);
            }

            credits_lines = new string[credits_LineList.Count];
            for (int i = 0; i < credits_LineList.Count; i++)
            {
                credits_lines[i] = credits_LineList[i];
            }
            File.WriteAllLines(path_extr + @"script\CreditsList.txt", credits_lines);
        }
    }
}
