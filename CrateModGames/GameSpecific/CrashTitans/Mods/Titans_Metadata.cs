using System;
using System.Collections.Generic;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_Metadata : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"default\";            
            bool skip = false;
            string fileName = path_extr + "package/cdd70a8c.p3d";
            File file = new File();
            try
            {
                file.Load(fileName);
            }
            catch
            {
                Console.WriteLine("Failed to load " + fileName);
                skip = true;
            }

            if (file.RootChunk.GetChildByName<FrontendTextBible>("frontend") != null)
            {
                foreach (Chunk chunk in file.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children)
                {
                    FrontendLanguage lang = (FrontendLanguage)chunk;
                    for (int i = 0; i < lang.TextStrings.Count; i++)
                    {
                        if (lang.TextStrings[i] == "RADICAL ENTERTAINMENT" || lang.TextStrings[i] == "DEVELOPED BY SUPERVILLAIN STUDIOS" || lang.TextStrings[i] == "SENIOR PRODUCER")
                        {
                            lang.TextStrings[i] = "Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + " Seed: " + ModLoaderGlobals.RandomizerSeed;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to find text");
            }

            if (!skip)
            {
                file.Save(fileName);
            }
        }
    }
}
