using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Metadata : ModStruct<string>
    {
        public override void ModPass(string basePath)
        {
            string path_extr = basePath + @"cml_extr\";
            if (File.Exists(path_extr + @"design\levels\common\frontend.god"))
            {

                string[] frontend_lines = File.ReadAllLines(path_extr + @"design\levels\common\frontend.god");

                // Editing credits to add CML metadata
                for (int i = 0; i < frontend_lines.Length; i++)
                {
                    if (frontend_lines[i] == "screen.AddLine(\"\",0,\"\")")
                    {
                        frontend_lines[i + 1] = "screen.AddLine(\"Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + "\",0,\"\")";
                        frontend_lines[i + 2] = "screen.AddLine(\"Seed: " + ModLoaderGlobals.RandomizerSeed + "\",0,\"\")";
                        frontend_lines[i + 3] = "screen.AddLine(\"\",0,\"\")";
                        frontend_lines[i + 4] = "screen.AddLineSpecial(\"creditscttr\",0,104,104,255,1.2,true)";
                        break;
                    }
                }

                File.WriteAllLines(path_extr + @"design\levels\common\frontend.god", frontend_lines);
            }
        }
    }
}
