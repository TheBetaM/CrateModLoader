using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_Metadata : ModStruct<GOD_File>
    {
        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains(@"frontend.god"))
            {
                LUA_Object obj = file.GetObject("Script", "LevelSetup");
                // Editing credits to add CML metadata
                if (obj != null)
                {
                    for (int i = 0; i < obj.Content.Count; i++)
                    {
                        if (obj.Content[i] == "screen.AddLine(\"\",0,\"\")")
                        {
                            obj.Content[i + 1] = "screen.AddLine(\"Crate Mod Loader " + ModLoaderGlobals.ProgramVersion + "\",0,\"\")";
                            obj.Content[i + 2] = "screen.AddLine(\"Seed: " + ModLoaderGlobals.RandomizerSeed + "\",0,\"\")";
                            break;
                        }
                    }
                }
            }
        }
    }
}
