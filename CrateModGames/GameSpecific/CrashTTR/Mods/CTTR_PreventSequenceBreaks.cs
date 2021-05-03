using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class CTTR_PreventSequenceBreaks : ModStruct<GOD_File>
    {
        public override void ModPass(GOD_File file)
        {
            if (file.Name.Contains("genericobjectives.god"))
            {
                List<LUA_Object> objs = file.GetObjects("Objective");
                if (objs.Count > 0)
                {
                    for (int x = 0; x < objs.Count; x++)
                    {
                        if (objs[x].ObjectName == "ChangeLevelMidwayToFairy")
                        {
                            objs[x].Content.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_fairy\",true)");
                        }
                        else if (objs[x].ObjectName == "ChangeLevelMidwayToDino")
                        {
                            objs[x].Content.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_dino\",true)");
                        }
                        else if (objs[x].ObjectName == "ChangeLevelMidwayToEgypt")
                        {
                            objs[x].Content.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_egypt\",true)");
                        }
                        else if (objs[x].ObjectName == "ChangeLevelMidwayToSolar")
                        {
                            objs[x].Content.Insert(2, "this.AddRequirement_CheckNamedFlag(\"GateUnlocked_solar\",true)");
                        }
                    }
                }
            }
            else if (file.Name.Contains("missionobjectives_fairy.god"))
            {
                LUA_Object obj = file.GetObject("Objective", "DinoKeyCollection");
                if (obj != null)
                {
                    obj.Content.Insert(2, "this.AddRequirement_CheckNamedFlag(\"WeenieUnlocked_fairy\",true)");
                }
            }

        }

    }
}
