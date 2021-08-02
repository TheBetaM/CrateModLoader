using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_SwapLevels : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string extrPath = mod.ExtractedPath;
            ConsoleMode console = mod.Console;

            Random rand = GetRandom();

            TWOC_Levels Level1 = TWOC_Levels.L03_Bamboozled;
            TWOC_Levels Level2 = TWOC_Levels.L14_EskimoRoll;

            string LevelsPathA = @"LEVELS\A\";
            string LevelsPathC = @"LEVELS\C\";
            
            if (console != ConsoleMode.PS2)
            {
                LevelsPathA = LevelsPathA.ToLower();
                LevelsPathC = LevelsPathC.ToLower();
            }
            
            int maxLevel = TWOC_Common.LevelNames.Length;

            string LevelPathIn = LevelsPathA;
            string LevelPathOut = LevelsPathA;
            if ((int)Level1 > 24)
            {
                LevelPathIn = LevelsPathC;
            }
            if ((int)Level2 > 24)
            {
                LevelPathOut = LevelsPathC;
            }

            Directory.Move(extrPath + LevelPathIn + TWOC_Common.LevelNames[(int)Level1], extrPath + LevelPathIn + "LEVEL");
            Directory.Move(extrPath + LevelPathOut + TWOC_Common.LevelNames[(int)Level2], extrPath + LevelPathIn + TWOC_Common.LevelNames[(int)Level1]);
            Directory.Move(extrPath + LevelPathIn + "LEVEL", extrPath + LevelPathOut + TWOC_Common.LevelNames[(int)Level2]);

            DirectoryInfo di = new DirectoryInfo(extrPath + LevelPathOut + TWOC_Common.LevelNames[(int)Level2]);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Name.ToUpper().Contains(TWOC_Common.FileNames[(int)Level1]))
                {
                    file.MoveTo(di.FullName + @"\" + TWOC_Common.FileNames[(int)Level2] + file.Extension);
                }
            }

            di = new DirectoryInfo(extrPath + LevelPathIn + TWOC_Common.LevelNames[(int)Level1]);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Name.ToUpper().Contains(TWOC_Common.FileNames[(int)Level2]))
                {
                    file.MoveTo(di.FullName + @"\" + TWOC_Common.FileNames[(int)Level1] + file.Extension);
                }
            }
        }
    }
}
