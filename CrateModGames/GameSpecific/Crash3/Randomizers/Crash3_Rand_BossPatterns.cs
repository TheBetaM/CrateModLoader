using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    //unfinished
    public class Crash3_Rand_BossPatterns : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            Crash3_Levels level = pair.LevelC3;
            bool isBackwards = false;
            if (!Crash3_Common.BossLevelsList.Contains(level))
            {
                return;
            }
            if (level == Crash3_Levels.B03_NTropy && !isBackwards)
            {
                return;
            }

            Entity CrashEntity = null;
            NewZoneEntry CrashZone = null;
            Entity WarpInEntity = null;
            Entity TropyEntity = null;
            NewZoneEntry TropyZone = null;

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    for (int e = 0; e < zonechunk.Entries.Count; e++)
                    {
                        if (zonechunk.Entries[e] is NewZoneEntry zone)
                        {

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                {

                                    if (level == Crash3_Levels.B03_NTropy && isBackwards)
                                    {

                                        if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                        {
                                            CrashEntity = zone.Entities[i];
                                            CrashZone = zone;
                                        }
                                        else if (WarpInEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 9)
                                        {
                                            WarpInEntity = zone.Entities[i];
                                        }
                                        else if (TropyEntity == null && zone.Entities[i].Type == 89 && zone.Entities[i].Subtype == 0)
                                        {
                                            TropyEntity = zone.Entities[i];
                                            TropyZone = zone;
                                        }

                                    }
                                    zone.Entities[i].Name = null;

                                    if (zone.Entities[i].Type == 68 && zone.Entities[i].Subtype == 0) // N.Gin
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = 35; a > -1; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
                                            {
                                                PosToRand.Add(a);
                                            }
                                            int count = PosToRand.Count;
                                            for (int a = 0; a < count; a++)
                                            {
                                                int r = rand.Next(PosToRand.Count);
                                                zone.Entities[i].Positions.Add(Path[PosToRand[r]]);
                                                PosToRand.RemoveAt(r);
                                            }

                                        }

                                        for (int a = 36; a < Path.Length; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
                                        }

                                    }
                                    else if (zone.Entities[i].Type == 94 && zone.Entities[i].Subtype == 0) // Cortex
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = 0; a < 18; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }

                                            for (int a = 45; a > 17; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }

                                            for (int a = 46; a < Path.Length; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            //todo
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
                                            {
                                                PosToRand.Add(a);
                                            }
                                            int count = PosToRand.Count;
                                            for (int a = 0; a < count; a++)
                                            {
                                                int r = rand.Next(PosToRand.Count);
                                                zone.Entities[i].Positions.Add(Path[PosToRand[r]]);
                                                PosToRand.RemoveAt(r);
                                            }

                                        }

                                    }
                                    else if (zone.Entities[i].Type == 94 && zone.Entities[i].Subtype == 5) // Uka
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = Path.Length - 1; a > -1; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            //todo
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
                                            {
                                                PosToRand.Add(a);
                                            }
                                            int count = PosToRand.Count;
                                            for (int a = 0; a < count; a++)
                                            {
                                                int r = rand.Next(PosToRand.Count);
                                                zone.Entities[i].Positions.Add(Path[PosToRand[r]]);
                                                PosToRand.RemoveAt(r);
                                            }

                                        }

                                    }
                                }
                            }

                        }
                    }
                }
            }

            if (level == Crash3_Levels.B03_NTropy && isBackwards)
            {

                EntityPosition CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
                EntityPosition TropyPos = new EntityPosition(TropyEntity.Positions[0].X, TropyEntity.Positions[0].Y, TropyEntity.Positions[0].Z);
                EntityPosition WarpInPos = new EntityPosition(WarpInEntity.Positions[0].X, WarpInEntity.Positions[0].Y, WarpInEntity.Positions[0].Z);

                EntityPosition BackSpawn = new EntityPosition(TropyEntity.Positions[0].X, CrashEntity.Positions[0].Y, (short)(TropyEntity.Positions[0].Z - 8000));
                EntityPosition FrontSpawn = new EntityPosition(CrashEntity.Positions[0].X, TropyEntity.Positions[0].Y, (short)(CrashEntity.Positions[0].Z + 7000));

                CrashEntity.Positions.RemoveAt(0);
                TropyEntity.Positions.RemoveAt(0);
                WarpInEntity.Positions.RemoveAt(0);

                CrashEntity.Positions.Add(BackSpawn);
                WarpInEntity.Positions.Add(BackSpawn);
                TropyEntity.Positions.Add(FrontSpawn);

                CrashEntity.Name = null;
                WarpInEntity.Name = null;

                int xoffset = BitConv.FromInt32(CrashZone.Layout, 0);
                int yoffset = BitConv.FromInt32(CrashZone.Layout, 4);
                int zoffset = BitConv.FromInt32(CrashZone.Layout, 8);

                pair.newnsd.Spawns[0].SpawnX = (xoffset + BackSpawn.X * 1) << 8;
                pair.newnsd.Spawns[0].SpawnY = (yoffset + BackSpawn.Y * 1) << 8;
                pair.newnsd.Spawns[0].SpawnZ = (zoffset + BackSpawn.Z * 1) << 8;
            }
        }
    }
}
