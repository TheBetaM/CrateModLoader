using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Ray3_Rand_LevelOrder : ModStruct<GenericModStruct>
    {
        public bool anyLevels = false;

        public Ray3_Rand_LevelOrder()
        {
            anyLevels = Rayman3_Props_Main.Option_RandLevelOrder.Enabled;
        }

        public override void ModPass(GenericModStruct mod)
        {
            string basePath = Ray3_Common.GetDataPath(mod);
            ConsoleMode console = mod.Console;
            Random randState = GetRandom();
            int minLevel = 1;
            int maxLevel = 46; //46
            int targetPos = 0;
            List<int> ValidLevels = new List<int>();
            List<int> LevelsReplacing = new List<int>();
            string sourceLevel, targetLevel;

            if (anyLevels)
            {
                minLevel = 0;
                maxLevel = 46;
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    ValidLevels.Add(i);
                }
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    targetPos = randState.Next(0, ValidLevels.Count);
                    LevelsReplacing.Add(ValidLevels[targetPos]);
                    ValidLevels.RemoveAt(targetPos);
                }
            }
            else
            {
                LevelsReplacing.Add(0);
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    LevelsReplacing.Add(-1);
                }
                List<int> HubSections = new List<int>()
                {
                    5,
                    6,
                    6,
                    6,
                    8,
                    3,
                    3,
                    5,
                    4,
                };
                int currentPos = 1;
                int lastLevel = 0;
                int currentSection = 0;

                while (currentSection < HubSections.Count)
                {

                    int firstLevel = minLevel;
                    if (currentSection > 0)
                    {
                        for (int i = 0; i < currentSection; i++)
                        {
                            firstLevel += HubSections[i];
                        }
                    }
                    currentPos = firstLevel;
                    int endLevel = firstLevel + HubSections[currentSection] - 1;
                    for (int i = firstLevel; i < endLevel; i++)
                    {
                        ValidLevels.Add(i);
                    }
                    while (ValidLevels.Count > 0)
                    {
                        targetPos = randState.Next(0, ValidLevels.Count);
                        LevelsReplacing[currentPos] = ValidLevels[targetPos];
                        Console.WriteLine("Level " + currentPos + ": " + (LevelID)LevelsReplacing[currentPos] + " (" + LevelsReplacing[currentPos] + ")");
                        currentPos = ValidLevels[targetPos] + 1;
                        //currentPos++;
                        lastLevel = ValidLevels[targetPos];
                        ValidLevels.RemoveAt(targetPos);
                    }
                    currentSection++;

                    currentPos = lastLevel + 1;
                    LevelsReplacing[currentPos] = endLevel;
                    Console.WriteLine("Level " + currentPos + ": " + (LevelID)LevelsReplacing[currentPos] + " (" + LevelsReplacing[currentPos] + ")");
                }

                //currentPos = lastLevel + 1;

                Console.WriteLine("");
                for (int i = minLevel; i <= maxLevel; i++)
                {
                    Console.WriteLine("Order " + i + ": " + (LevelID)LevelsReplacing[i]);
                }

            }

            for (int i = minLevel; i <= maxLevel; i++)
            {
                targetLevel = Ray3_Common.LevelNames[i];
                if (console == ConsoleMode.PS2)
                {
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + targetLevel + @"\"))
                {
                    Directory.Move(basePath + targetLevel + @"\", basePath + "level" + i + @"\");
                }
                if (console == ConsoleMode.PS2)
                {
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1", basePath + @"WORLD\SOUND\" + "level" + i + ".BNH;1");
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1", basePath + @"WORLD\SOUND\" + "level" + i + ".HX2;1");
                    File.Move(basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1", basePath + @"WORLD\SOUND\" + "level" + i + ".SMT;1");
                }
                else if (console == ConsoleMode.GCN)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".hxg", basePath + @"World\Sound\" + "level" + i + ".hxg");
                }
                else if (console == ConsoleMode.XBOX)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".hxx", basePath + @"World\Sound\" + "level" + i + ".hxx");
                }
                else if (console == ConsoleMode.PC)
                {
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".bnh", basePath + @"World\Sound\" + "level" + i + ".bnh");
                    File.Move(basePath + @"World\Sound\" + targetLevel + ".HXC", basePath + @"World\Sound\" + "level" + i + ".HXC");
                }
            }
            for (int i = minLevel; i <= maxLevel; i++)
            {
                sourceLevel = Ray3_Common.LevelNames[i];
                targetLevel = Ray3_Common.LevelNames[LevelsReplacing[i]];
                if (console == ConsoleMode.PS2)
                {
                    sourceLevel = sourceLevel.ToUpper();
                    targetLevel = targetLevel.ToUpper();
                }
                if (Directory.Exists(basePath + "level" + i + @"\"))
                {
                    Directory.Move(basePath + "level" + i + @"\", basePath + targetLevel + @"\");

                    if (console == ConsoleMode.PS2)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".LVL;1", basePath + targetLevel + @"\" + targetLevel + ".LVL;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".PTR;1", basePath + targetLevel + @"\" + targetLevel + ".PTR;1");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".TBF;1", basePath + targetLevel + @"\" + targetLevel + ".TBF;1");

                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".BNH;1", basePath + @"WORLD\SOUND\" + targetLevel + ".BNH;1");
                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".HX2;1", basePath + @"WORLD\SOUND\" + targetLevel + ".HX2;1");
                        File.Move(basePath + @"WORLD\SOUND\" + "level" + i + ".SMT;1", basePath + @"WORLD\SOUND\" + targetLevel + ".SMT;1");
                    }
                    else if (console == ConsoleMode.GCN)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Lvl.tpl", basePath + targetLevel + @"\" + targetLevel + "_Lvl.tpl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_Trans.tpl", basePath + targetLevel + @"\" + targetLevel + "_Trans.tpl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.lvl", basePath + targetLevel + @"\" + targetLevel + "_vb.lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "_vb.ptr", basePath + targetLevel + @"\" + targetLevel + "_vb.ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.lvl", basePath + targetLevel + @"\" + targetLevel + "kf.lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + "kf.ptr", basePath + targetLevel + @"\" + targetLevel + "kf.ptr");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".hxg", basePath + @"World\Sound\" + targetLevel + ".hxg");
                    }
                    else if (console == ConsoleMode.XBOX)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bhf", basePath + targetLevel + @"\" + targetLevel + ".bhf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bif", basePath + targetLevel + @"\" + targetLevel + ".bif");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bsf", basePath + targetLevel + @"\" + targetLevel + ".bsf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".btf", basePath + targetLevel + @"\" + targetLevel + ".btf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".bvf", basePath + targetLevel + @"\" + targetLevel + ".bvf");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".spc", basePath + targetLevel + @"\" + targetLevel + ".spc");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".hxx", basePath + @"World\Sound\" + targetLevel + ".hxx");
                    }
                    else if (console == ConsoleMode.PC)
                    {
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".lvl", basePath + targetLevel + @"\" + targetLevel + ".lvl");
                        File.Move(basePath + targetLevel + @"\" + sourceLevel + ".ptr", basePath + targetLevel + @"\" + targetLevel + ".ptr");

                        File.Move(basePath + @"World\Sound\" + "level" + i + ".bnh", basePath + @"World\Sound\" + targetLevel + ".bnh");
                        File.Move(basePath + @"World\Sound\" + "level" + i + ".HXC", basePath + @"World\Sound\" + targetLevel + ".HXC");
                    }
                }
            }
        }
    }
}
