using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Start2DNightmare : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string basePath = Ray3_Common.GetDataPath(mod);
            string sourceLevel = "toudi_10";
            string targetLevel = "intro_10";
            string tempLevel = "intro_11";
            if (Directory.Exists(basePath + targetLevel + @"\"))
            {
                Directory.Move(basePath + targetLevel + @"\", basePath + tempLevel + @"\");
            }
            if (Directory.Exists(basePath + sourceLevel + @"\"))
            {
                Directory.Move(basePath + sourceLevel + @"\", basePath + targetLevel + @"\");

                File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Lvl.tpl", basePath + targetLevel + @"\" + targetLevel + "_Lvl.tpl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Trans.tpl", basePath + targetLevel + @"\" + targetLevel + "_Trans.tpl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.lvl", basePath + targetLevel + @"\" + targetLevel + "_vb.lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.ptr", basePath + targetLevel + @"\" + targetLevel + "_vb.ptr");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.lvl", basePath + targetLevel + @"\" + targetLevel + "kf.lvl");
                File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.ptr", basePath + targetLevel + @"\" + targetLevel + "kf.ptr");

                File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + "1.bnh");
                File.Move(basePath + @"World\Sound\" + targetLevel + ".hxg", basePath + @"World\Sound\" + targetLevel + "1.hxg");
                File.Move(basePath + @"World\Sound\" + sourceLevel + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                File.Move(basePath + @"World\Sound\" + sourceLevel + ".hxg", basePath + @"World\Sound\" + targetLevel + ".hxg");
                File.Move(basePath + @"World\Sound\" + targetLevel + "1.bnh", basePath + @"World\Sound\" + sourceLevel + ".bnh");
                File.Move(basePath + @"World\Sound\" + targetLevel + "1.hxg", basePath + @"World\Sound\" + sourceLevel + ".hxg");
            }
        }
    }
}
