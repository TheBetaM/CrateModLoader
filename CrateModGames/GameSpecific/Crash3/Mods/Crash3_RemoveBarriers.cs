using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class Crash3_RemoveBarriers : ModStruct<NSF_Pair>
    {
        public override string Name => Crash3_Text.Mod_RemoveWarpRoomWalls;
        public override string Description => Crash3_Text.Mod_RemoveWarpRoomWallsDesc;

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC3 != Crash3_Levels.WarpRoom)
            {
                return;
            }

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
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
        }
    }
}
