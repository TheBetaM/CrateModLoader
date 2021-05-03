using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    // todo: sort out FOV mod stuff
    public class Crash1_BackwardsHogLevels : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            if (!Crash1_Common.VehicleLevelsList.Contains(pair.LevelC1))
            {
                return;
            }

            OldEntity CrashEntity = null;
            OldZoneEntry CrashZone = null;
            OldEntity WarpOutEntity = null;
            OldZoneEntry WarpOutZone = null;
            int SpawnEID = 0;
            //Mod_CameraFOV(nsf, new Random(), false);

            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldCamera cam in zone.Cameras)
                {
                    short newFOV = (short)Math.Floor(cam.Zoom * 2d); // 1.5
                    cam.Zoom = newFOV;
                }

                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                    {
                        EntityPosition[] BoarPath = new EntityPosition[zone.Entities[i].Positions.Count];
                        //BoarPath[0] = zone.Entities[i].Positions[0];
                        zone.Entities[i].Positions.CopyTo(BoarPath, 0);
                        zone.Entities[i].Positions.Clear();
                        //zone.Entities[i].Positions.Add(new EntityPosition(550, 1054, -200));
                        //zone.Entities[i].Positions.Add(BoarPath[0]);
                        //zone.Entities[i].Positions[0] = new EntityPosition((short)(BoarPath[0].X + 50), (short)(BoarPath[0].Y + 50), (short)(BoarPath[0].Z + 50));

                        /*
                        if (isReverse)
                        {
                            zone.Entities[i].Positions.Add(new EntityPosition(550, 1014, -200));
                        }
                        else
                        {
                            zone.Entities[i].Positions.Add(new EntityPosition(420, 400, 500));
                        }
                        */

                        CrashEntity = zone.Entities[i];
                        CrashZone = zone;

                        //EntityPosition tempPos = BoarPath[(BoarPath.Length - 1) - 15];
                        //zone.Entities[i].Positions.Add(new EntityPosition(tempPos.X,(short)(tempPos.Y + 100),tempPos.Z));

                        bool firstSpawn = true;

                        for (int a = BoarPath.Length - 8; a > -1; a--)
                        {
                            if (firstSpawn)
                            {
                                zone.Entities[i].Positions.Add(new EntityPosition(BoarPath[a].X, (short)(BoarPath[a].Y + 3000), BoarPath[a].Z));
                                firstSpawn = false;
                                a--;
                            }
                            zone.Entities[i].Positions.Add(BoarPath[a]);
                        }

                        /*
                        for (int a = 19; a > -1; a--)
                        {
                            zone.Entities[i].Positions.Add(BoarPath[a]);
                        }
                        */


                        /*
                        zone.Entities.RemoveAt(i);
                        i--;
                        */

                        //CrashEntity.Flags = 196632;

                    }
                    else if (WarpOutEntity == null && zone.Entities[i].Type == 32 && zone.Entities[i].Subtype == 1)
                    {
                        WarpOutEntity = zone.Entities[i];
                        WarpOutZone = zone;
                        zone.Entities.RemoveAt(i);
                        i--;
                    }
                    else if (zone.Entities[i].Type == 34 && zone.Entities[i].Subtype == (int)CrateSubTypes.Checkpoint)
                    {
                        zone.Entities[i].Subtype = (int)CrateSubTypes.Blank; // TODO : spawn position is dictated by WillC for hog levels
                    }
                }

                if (pair.LevelC1 == Crash1_Levels.L07_HogWild)
                {
                    if (zone.EName == "s0_hZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(-2445, 5775, 32689),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 2, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    if (zone.EName == "1M_hZ")
                    {
                        SpawnEID = zone.EID;

                        zone.Cameras[0].NeighborCount = 1;
                    }
                    else if (zone.EName != "s0_hZ" && zone.Cameras.Count > 0)
                    {
                        zone.Cameras[0].NeighborCount = 2;
                        zone.Cameras[0].Neighbors[1] = new OldCameraNeighbor(1, 1, 0, 2);
                    }
                }
                else
                {
                    if (zone.EName == "s0_uZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(-2445, 5775, 32689),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 2, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0y_uZ")
                    {
                        int crutchID = 301;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(533, 352, 320),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            CreateEntityBoarBounce((short)entID, 51, 3, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    if (zone.EName == "1M_uZ")
                    {
                        SpawnEID = zone.EID;

                        zone.Cameras[0].NeighborCount = 1;
                    }
                    else if (zone.EName != "s0_uZ" && zone.Cameras.Count > 0)
                    {
                        zone.Cameras[0].NeighborCount = 2;
                        zone.Cameras[0].Neighbors[1] = new OldCameraNeighbor(1, 1, 0, 2);
                    }
                }
            }

            /*
            int cacheCount = 0;
            for (int i = 0; i < Cache_Entry.Count; i++)
            {
                if (!CacheChecks[i])
                {
                    cacheCount++;
                }
            }

            if (cacheCount > 0)
            {
                int curEntry = 0;
                if (CacheChecks[curEntry])
                {
                    while (CacheChecks[curEntry] && curEntry < Cache_Entry.Count)
                    {
                        curEntry++;
                    }
                }

                while (curEntry < Cache_Entry.Count)
                {
                    NormalChunk newchunk = new NormalChunk();
                    newchunk.Entries.Add(Cache_Entry[curEntry]);
                    nsf.Chunks.Add(newchunk);
                    curEntry++;
                    if (curEntry < Cache_Entry.Count && CacheChecks[curEntry])
                    {
                        while (CacheChecks[curEntry] && curEntry < Cache_Entry.Count)
                        {
                            curEntry++;
                        }
                    }
                }
            }
            */

            WarpOutEntity.Positions.Clear();
            if (pair.LevelC1 == Crash1_Levels.L07_HogWild)
            {
                WarpOutEntity.Positions.Add(new EntityPosition(400, 300, 500));
            }
            else
            {
                WarpOutEntity.Positions.Add(new EntityPosition(300, 450, 800));
            }


            List<int> LinkedZones = new List<int>();

            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                if (pair.LevelC1 == Crash1_Levels.L07_HogWild)
                {
                    if (zone.EName == "0a_hZ")
                    {
                        zone.Entities.Add(WarpOutEntity);
                        //orig start zone is s0_hZ / s0_uZ, it has to stay that way
                        //nsd.StartZone = zone.EID;
                        //zone.Entities.Add(CrashEntity);
                        //zone.EntityCount++;
                    }
                    if (zone.EName == "s0_hZ")
                    {
                        //BitConv.FromInt32(zone.Header, 532 + 1 * 4);
                        //BitConv.ToInt32(zone.Header, 532 + 1 * 4, nsd.StartZone);

                        /*
                        int linkedzoneentrycount = BitConv.FromInt32(zone.Header, 528);
                        Console.WriteLine("Linked zones: " + linkedzoneentrycount);
                        for (int i = 0; i < linkedzoneentrycount; i++)
                        {
                            LinkedZones.Add(BitConv.FromInt32(zone.Header, 532 + i * 4));
                        }
                        */
                        BitConv.ToInt32(zone.Header, 532 + 1 * 4, SpawnEID);
                    }
                }
                else
                {
                    if (zone.EName == "0g_uZ") // or 0f?
                    {
                        zone.Entities.Add(WarpOutEntity);
                    }
                    if (zone.EName == "s0_uZ")
                    {
                        BitConv.ToInt32(zone.Header, 532 + 1 * 4, SpawnEID);
                    }
                }

            }

            /*
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry zone)
                        {
                            for (int i = 0; i < LinkedZones.Count; i++)
                            {
                                if (LinkedZones[i] == zone.EID)
                                {
                                    Console.WriteLine("Link " + i + ": " + zone.EName);
                                }
                            }
                        }
                    }
                }
            }
            */
        }


        private void CreateEntityBoarBounce(short id, int type, int subtype, short x, short y, short z, OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0x18, 3, 0, id, 0, 0, 0, (byte)type, (byte)subtype, new List<EntityPosition>() { new EntityPosition(x, y, z) }, 0).Save());

            zone.Entities.Add(newentity);

        }

    }
}
