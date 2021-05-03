using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_RemoveIntroVideo : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string basePath = Ray3_Common.GetDataPath(mod);
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
