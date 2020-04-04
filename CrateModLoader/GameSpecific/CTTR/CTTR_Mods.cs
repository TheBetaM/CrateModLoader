using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CTTR
{
    public static class CTTR_Mods
    {
        public static void Mod_PreventSequenceBreaks(string path_extr)
        {
            if (System.IO.File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToFairy", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToDino", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_dino\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToDino", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_egypt\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToEgypt", ChangeHubObjective);
                    }
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_solar\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "ChangeLevelMidwayToSolar", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            if (System.IO.File.Exists(path_extr + @"design\permanent\missionobjectives_fairy.god"))
            {
                string[] objective_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < objective_lines.Length; i++)
                {
                    LineList.Add(objective_lines[i]);
                }

                int List_Start = 0;
                int List_End = 0;
                List<string> ChangeHubObjective = new List<string>();
                for (int i = 0; i < 8; i++)
                {
                    if (CTTR_Data.LUA_LoadObject(LineList, "Objective", "DinoKeyCollection", ref List_Start, ref List_End, ChangeHubObjective))
                    {
                        ChangeHubObjective.Insert(2, "this.AddRequirement_CheckNamedFlag(\"WeenieUnlocked_fairy\",true)");
                        CTTR_Data.LUA_SaveObject(LineList, "Objective", "DinoKeyCollection", ChangeHubObjective);
                    }
                    ChangeHubObjective.Clear();
                }

                objective_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    objective_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god", objective_lines);

            }
        }

        public static void Mod_EditCredits(string basePath)
        {
            string path_extr = basePath + @"cml_extr\";
            if (System.IO.File.Exists(path_extr + @"design\levels\common\frontend.god"))
            {

                string[] frontend_lines = System.IO.File.ReadAllLines(path_extr + @"design\levels\common\frontend.god");

                // Editing credits to add CML metadata
                for (int i = 0; i < frontend_lines.Length; i++)
                {
                    if (frontend_lines[i] == "screen.AddLine(\"\",0,\"\")")
                    {
                        frontend_lines[i + 1] = "screen.AddLine(\"Crate Mod Loader " + Program.ModProgram.releaseVersionString + "\",0,\"\")";
                        frontend_lines[i + 2] = "screen.AddLine(\"Seed: " + Program.ModProgram.randoSeed + "\",0,\"\")";
                        frontend_lines[i + 3] = "screen.AddLine(\"Options: " + Program.ModProgram.optionsSelectedString + "\",0,\"\")";
                        frontend_lines[i + 4] = "screen.AddLineSpecial(\"creditscttr\",0,104,104,255,1.2,true)";
                        break;
                    }
                }

                System.IO.File.WriteAllLines(path_extr + @"design\levels\common\frontend.god", frontend_lines);
            }
        }
    }
}
