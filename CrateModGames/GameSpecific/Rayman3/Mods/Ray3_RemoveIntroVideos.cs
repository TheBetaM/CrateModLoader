using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_RemoveIntroVideo : ModStruct<Rayman3_GenericMod>
    {
        public override void ModPass(Rayman3_GenericMod mod)
        {
            string basePath = mod.mainPath;
            if (Directory.Exists(basePath + "videos"))
            {
                if (File.Exists(basePath + @"videos\trailer.h4m"))
                {
                    File.Delete(basePath + @"videos\trailer.h4m");
                }
            }
        }
    }
}
