using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash2.Mods
{
    //unfinished
    public class Crash2_VehicleLevelsOnFoot : ModStruct<NSF_Pair>
    {
        public override string Name => "Vehicle Levels On Foot";

        public override void ModPass(NSF_Pair pair)
        {
            if (!Crash2_Common.BearLevelsList.Contains(pair.LevelC2) && !Crash2_Common.BoardLevelsList.Contains(pair.LevelC2))
            {
                return;
            }

            //todo: jetpack

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities[i].Type == 48 && zone.Entities[i].Subtype == 0) // Bear
                                {
                                    zone.Entities.RemoveAt(i);
                                    zone.EntityCount--;
                                    i--;
                                    /*
                                    EntityPosition[] BearPath = new EntityPosition[zone.Entities[i].Positions.Count];
                                    zone.Entities[i].Positions.CopyTo(BearPath, 0);
                                    zone.Entities[i].Positions.Clear();
                                    for (int a = 0; a < BearPath.Length; a++)
                                    {
                                        zone.Entities[i].Positions.Add(BearPath[(BearPath.Length - 1) - a]);
                                    }
                                    */
                                }
                                else if (zone.Entities[i].Type == 34 && zone.Entities[i].Subtype == 4) // Checkpoint fix
                                {
                                    zone.Entities[i].Settings[1] = new EntitySetting(0, 0);
                                }
                                if (zone.Entities.Count > 0 && zone.Entities[i].CameraIndex != null && zone.Entities[i].CameraSubIndex == 0 && zone.Entities[i].Neighbors.RowCount == 1)
                                {
                                    //doesn't do anything

                                    zone.Entities[i].Neighbors.Rows.Insert(0, new EntityPropertyRow<uint>());
                                    zone.Entities[i].Neighbors.Rows[0].MetaValue = 0;
                                    int neighborindex = 0;
                                    int neighborsettingindex = 0;

                                    int camflag = 2;
                                    int camIndex = 0;
                                    int camZone = 2;
                                    int camLink = 1;

                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values.Add(0);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                }
                            }

                            if (Crash2_Common.BoardLevelsList.Contains(pair.LevelC2))
                            {
                                for (int a = 0x24; a < zone.Layout.Length; a += 2)
                                {
                                    short node = BitConv.FromInt16(zone.Layout, a);
                                    if (node == 0x35)
                                    {
                                        //bear level water => solid
                                        BitConv.ToInt16(zone.Layout, a, 3);
                                    }
                                    else if (node == 0x129)
                                    {
                                        //water level water => solid
                                        BitConv.ToInt16(zone.Layout, a, 3);
                                    }
                                }
                            }

                            if (pair.LevelC2 == Crash2_Levels.L08_BearIt)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "61_tZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 400, 300),
                                        new EntityPosition(1000, 550, 600),
                                        new EntityPosition(1000, 700, 900),
                                        new EntityPosition(1000, 800, 1200),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        Crash2_Common.CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L13_BearDown)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "69_yZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 100, 800),
                                        new EntityPosition(1000, 250, 1100),
                                        new EntityPosition(1000, 400, 1400),
                                        new EntityPosition(1000, 550, 1700),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        Crash2_Common.CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L26_TotallyBear)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "64_BZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 250, 700),
                                        new EntityPosition(1000, 400, 1000),
                                        new EntityPosition(1000, 550, 1300),
                                        new EntityPosition(1000, 700, 1600),
                                        new EntityPosition(1000, 850, 1900),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        Crash2_Common.CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L15_UnBearable)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "46_nZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        //new EntityPosition(870, 150, 450),
                                        new EntityPosition(870, 150, 250),
                                        //new EntityPosition(870, 150, 50),
                                        new EntityPosition(870, 150, -150),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        Crash2_Common.CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, entID);
                                    }
                                }
                            }

                        }
                    }
                }
            }

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            if (pair.LevelC2 == Crash2_Levels.L08_BearIt)
                            {
                                int id = 400;
                                if (zone.EName == "60_tZ")
                                {
                                    for (int i = id + 0; i < id + 4; i++)
                                    {
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L13_BearDown)
                            {
                                int id = 400;
                                if (zone.EName == "68_yZ")
                                {
                                    for (int i = id + 0; i < id + 4; i++)
                                    {
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L26_TotallyBear)
                            {
                                int id = 400;
                                if (zone.EName == "63_BZ")
                                {
                                    for (int i = id + 0; i < id + 5; i++)
                                    {
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (pair.LevelC2 == Crash2_Levels.L15_UnBearable)
                            {
                                int id = 400;
                                if (zone.EName == "45_nZ" || zone.EName == "47_nZ")
                                {
                                    for (int i = id + 0; i < id + 2; i++)
                                    {
                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, i);
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
