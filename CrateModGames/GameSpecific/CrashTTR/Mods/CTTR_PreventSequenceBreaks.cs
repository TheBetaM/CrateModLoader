using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_PreventSequenceBreaks : ModStruct<string>
    {
        public override string Name => CTTR_Text.Mod_PreventSkips;
        public override string Description => CTTR_Text.Mod_PreventSkipsDesc;

        public override void ModPass(string path_extr)
        {
            if (File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] objective_lines = File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
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

                File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", objective_lines);

            }
            if (File.Exists(path_extr + @"design\permanent\missionobjectives_fairy.god"))
            {
                string[] objective_lines = File.ReadAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god");
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

                File.WriteAllLines(path_extr + @"design\permanent\missionobjectives_fairy.god", objective_lines);

            }
        }

    }
}
