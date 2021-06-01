using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class WF_TestMod : ModStruct<XOM_File>
    {
        public override void ModPass(XOM_File file)
        {

            Console.WriteLine("Modding... " + Path.GetFileName(file.FileName));
            
            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is BuildingGlobalContainer bcont)
                {
                    //works
                    bcont.NumCratesToSpawnAtRefineryPerTurn = 5;
                }
                else if (file.Containers[i] is BuildingSpecificContainer bscont)
                {
                    if (file.Strings[bscont.ResourceName.RawValue] == "BrickifiedTower")
                    {
                        //works
                        bscont.BuildingHealth = 60;
                    }
                }
                else if (file.Containers[i] is WeaponDataCtr wcont)
                {
                    if (wcont.WeaponID == 0)
                    {
                        //works, but glitchy
                        //wcont.NumberOfShotsPerTurn = 2;
                    }
                }
                else if (file.Containers[i] is WormControlContainer wormcont)
                {
                    //works
                    wormcont.WalkSpeed = 2f;
                    wormcont.JumpDelay = 0.2f;
                }
                else if (file.Containers[i] is XColorResourceDetails colorcont)
                {
                    //works but doesn't affect the battle color
                    if (file.Strings[colorcont.NameKey.RawValue] == "AllianceColour1")
                    {
                        colorcont.R = 0;
                        colorcont.G = 255;
                        colorcont.B = 128;
                        colorcont.A = 255;
                    }
                }
                else if (file.Containers[i] is LevelDetails levcont)
                {
                    //works
                    if (file.Strings[levcont.LevelName.RawValue] == "Text.Level.Deathmatch1")
                    {
                        foreach (KeyValuePair<uint, string> pair in file.Strings)
                        {
                            if (pair.Value == "SS.DM2")
                            {
                                levcont.Image.RawValue = pair.Key;
                                break;
                            }
                        }
                    }
                }
                else if (file.Containers[i] is XFortsExportedData fortcont)
                {
                    //works
                    foreach (XFortsExportedData.BuildPoint Point in fortcont.BuildPoints)
                    {
                        if (file.Strings[Point.NameID.RawValue] == "buildHere0028")
                        {
                            Point.VictoryLocation.Value = true;
                        }
                        if (file.Strings[Point.NameID.RawValue] == "buildHere0029")
                        {
                            Point.VictoryLocation.Value = false;
                        }
                    }
                }

            }
            
        }

        /*
        public override void ModPass(LUA_File file)
        {
            if (file.Name != "DM01.lua" && file.Name != "DM01ps2.lua")
            {
                return;
            }

            LUA_Func init = file.GetFunction("InitialiseLevel()");

            for (int i = 0; i < init.Content.Count; i++)
            {
                if (init.Content[i].Contains("scheme.WormSelectMode"))
                {
                    init.Content[i] = "scheme.WormSelectMode = true";
                }
                if (init.Content[i].Contains("scheme.TurnTime"))
                {
                    init.Content[i] = "scheme.TurnTime = 1500";
                }
            }
        }
        */
    }
}
