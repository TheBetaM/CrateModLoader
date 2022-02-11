using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class Crash3_RemoveBarriers : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC3 != Crash3_Levels.WarpRoom)
            {
                return;
            }

            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                    {
                        if (zone.Entities[i].Type == 26 && zone.Entities[i].Subtype == 2) //barrier
                        {
                            zone.Entities[i].Positions.Clear();
                            zone.Entities[i].Positions.Add(new EntityPosition(-12000, -12000, -12000));
                        }
                    }
                }
            }
        }
    }
}
