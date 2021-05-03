using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    // todo sort out backwards
    // unfinished
    public class Crash3_Rand_FlyingLevels : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            bool isBackwards = false;
            Crash3_Levels level = pair.LevelC3;
            if (!Crash3_Common.FlyingLevelsList.Contains(level))
            {
                return;
            }

            List<int> TypesToRand = new List<int>()
            {
                0, 1, //4, 
            };

            // randomize zep/balloon count? will need to update draw lists...

            NewZoneEntry MainZone = null;
            EntityPosition SpawnPos = new EntityPosition(0, 0, 0);

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                ent.Name = null;

                                if (level == Crash3_Levels.L17_ByeByeBlimps)
                                {
                                    MainZone = zone;
                                    if (ent.Type != null && ent.Type == 36)
                                    {
                                        if (ent.Subtype == 0 || ent.Subtype == 1) // zep and loons
                                        {
                                            short Min_X = -25823;
                                            short Max_X = 29792;
                                            short Min_Y = 7400;
                                            short Max_Y = 14025;
                                            short Min_Z = -29589;
                                            short Max_Z = 26432;

                                            if (isBackwards)
                                            {
                                                short X = ent.Positions[0].X;
                                                short Y = ent.Positions[0].Y;
                                                short Z = (short)(-ent.Positions[0].Z);
                                                ent.Positions.Clear();
                                                ent.Positions.Add(new EntityPosition(X, Y, Z));
                                            }
                                            else
                                            {
                                                ent.Positions.Clear();
                                                short X = (short)rand.Next(Min_X, Max_X);
                                                short Y = (short)rand.Next(Min_Y, Max_Y);
                                                short Z = (short)rand.Next(Min_Z, Max_Z);
                                                ent.Positions.Add(new EntityPosition(X, Y, Z));
                                                //todo: loon types
                                            }

                                        }
                                    }
                                    else if (ent.Type != null && ent.Type == 0 && ent.Subtype == 0)
                                    {
                                        //WarpOutPos = ent.Positions[0]; // maybe randomize crash's spawn?
                                    }
                                }
                                else if (level == Crash3_Levels.L24_MadBombers)
                                {
                                    if (isBackwards)
                                    {
                                        if (ent.Type == 55 && ent.Subtype == 0) //reverse paths
                                        {
                                            List<EntityPosition> Pos1 = new List<EntityPosition>(ent.Positions);
                                            Pos1.Reverse();
                                            ent.Positions.Clear();
                                            for (int a = 0; a < Pos1.Count; a++)
                                            {
                                                ent.Positions.Add(Pos1[a]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ent.Type == 55 && ent.Subtype == 0) //reverse paths, sometimes
                                        {
                                            if (rand.Next(2) == 0)
                                            {
                                                List<EntityPosition> Pos1 = new List<EntityPosition>(ent.Positions);
                                                Pos1.Reverse();
                                                ent.Positions.Clear();
                                                for (int a = 0; a < Pos1.Count; a++)
                                                {
                                                    ent.Positions.Add(Pos1[a]);
                                                }
                                            }
                                        }
                                        //todo: loon types
                                    }
                                }
                                else if (level == Crash3_Levels.L28_RingsOfPower)
                                {
                                    if (isBackwards)
                                    {
                                        if (ent.Type == 87 && ent.Subtype == 0) //ring
                                        {
                                            int RingID = ent.Settings[0].ValueA;
                                            ent.Settings[0] = new EntitySetting((byte)(29 - RingID), 0);
                                        }
                                    }
                                    else
                                    {
                                        //todo
                                        //todo: loon types
                                    }
                                }


                            }
                        }
                    }
                }
            }

            /*
            int xoffset = BitConv.FromInt32(WarpOutZone.Layout, 0);
            int yoffset = BitConv.FromInt32(WarpOutZone.Layout, 4);
            int zoffset = BitConv.FromInt32(WarpOutZone.Layout, 8);

            nsd.Spawns[0].SpawnX = (xoffset + (short)(WarpOutPos.X * 1.5)) << 8;
            nsd.Spawns[0].SpawnY = (yoffset + (short)(WarpOutPos.Y * 1.5)) << 8;
            nsd.Spawns[0].SpawnZ = (zoffset + (short)(WarpOutPos.Z * 1.5)) << 8;
            */

        }
    }
}
