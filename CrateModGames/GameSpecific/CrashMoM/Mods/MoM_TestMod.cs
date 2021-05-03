using System;
using System.Collections.Generic;
using System.IO;
using Pure3D;
using Pure3D.Chunks;

namespace CrateModLoader.GameSpecific.CrashMoM.Mods
{
    public class MoM_TestMod : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string path_extr = mod.ExtractedPath + @"default\";

            string file1 = path_extr + "anim/1066d3f8.p3d";
            string file2 = path_extr + "anim/e7418dc5.p3d"; //orig
            System.IO.File.Move(file2, file1 + "1");
            System.IO.File.Move(file1, file2 + "1");
            System.IO.File.Move(file1 + "1", file1);
            System.IO.File.Move(file2 + "1", file2);
            

            /*
            bool skip = false;
            string fileName = path_extr + "package/cdd70a8c.p3d";
            Pure3D.File file = new Pure3D.File();
            try
            {
                file.Load(fileName);
            }
            catch
            {
                Console.WriteLine("Failed to load");
                skip = true;
            }

            if (file.RootChunk.GetChildByName<FrontendTextBible>("frontend") != null)
            {
                foreach (Chunk chunk in file.RootChunk.GetChildByName<FrontendTextBible>("frontend").Children)
                {
                    FrontendLanguage lang = (FrontendLanguage)chunk;
                    lang.TextStrings[45] = "Really long loading text for testing!";
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
            */
        }
    }
}
