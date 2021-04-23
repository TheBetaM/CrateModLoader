using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash2;

namespace CrateModLoader.GameSpecific.Crash2
{
    //todo sort out backwards
    public class Crash2_Rand_BossPaths : ModStruct<NSF_Pair>
    {
        public override string Name => Crash2_Text.Rand_BossLevels;
        public override string Description => Crash2_Text.Rand_BossLevelsDesc;

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            Crash2_Levels level = pair.LevelC2;
            bool isBackwards = false;
            if (!Crash2_Common.BossLevelsList.Contains(level))
            {
                return;
            }
            if (level == Crash2_Levels.B05_Cortex && !isBackwards)
            {
                return;
            }
            if (level == Crash2_Levels.B03_TinyTiger && isBackwards)
            {
                return;
            }

            List<EntityPosition> TinyPlats = new List<EntityPosition>();
            EntityPosition TinyInit = new EntityPosition();
            List<List<EntityPosition>> RocketPaths = new List<List<EntityPosition>>();
            int SpawnZone = 0;
            EntityPosition CrashSpawn = new EntityPosition();

            bool CameraFlip = true;

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    for (int e = 0; e < zonechunk.Entries.Count; e++)
                    {
                        if (zonechunk.Entries[e] is ZoneEntry zone)
                        {

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                {

                                    if (level == Crash2_Levels.B05_Cortex && CameraFlip && zone.Entities[i].CameraIndex != null && zone.Entities[i].CameraSubIndex != null)
                                    {
                                        if (zone.Entities[i].CameraSubIndex == 1)
                                        {
                                            EntityPosition[] Angles = new EntityPosition[zone.Entities[i].Positions.Count];
                                            zone.Entities[i].Positions.CopyTo(Angles, 0);
                                            for (int a = 0; a < Angles.Length; a++)
                                            {
                                                Angles[a] = new EntityPosition(3800, (short)(Angles[a].Y + 2000), Angles[a].Z);
                                            }
                                            zone.Entities[i].Positions.Clear();
                                            for (int a = 0; a < Angles.Length; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Angles[a]);
                                            }
                                        }
                                    }

                                    if (zone.Entities[i].Type == 19 && zone.Entities[i].Subtype == 0) // Ripper Roo
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = Path.Length - 2; a > -1; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {

                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < Path.Length - 1; a++)
                                            {
                                                PosToRand.Add(a);
                                            }
                                            List<int> PosRand = new List<int>();

                                            /* if you freely randomize the tiles the explosion hitbox bugs out
                                            int count = PosToRand.Count;
                                            for (int a = 0; a < count; a++)
                                            {
                                                int r = rand.Next(PosToRand.Count);
                                                zone.Entities[i].Positions.Add(Path[PosToRand[r]]);
                                                PosToRand.RemoveAt(r);
                                            }
                                            */

                                            int iter = rand.Next(1, 5);

                                            while (iter > 0)
                                            {
                                                if (rand.Next(2) == 0)
                                                {
                                                    //horizontal flip
                                                    for (int a = 0; a < 8; a++)
                                                    {
                                                        for (int b = 7; b > -1; b--)
                                                        {
                                                            PosRand.Add(PosToRand[(a * 8) + b]);
                                                        }
                                                    }
                                                    PosToRand.Clear();
                                                    for (int a = 0; a < PosRand.Count; a++)
                                                    {
                                                        PosToRand.Add(PosRand[a]);
                                                    }
                                                    PosRand.Clear();
                                                }
                                                if (rand.Next(2) == 0)
                                                {
                                                    //vertical flip
                                                    for (int a = 7; a > -1; a--)
                                                    {
                                                        for (int b = 0; b < 8; b++)
                                                        {
                                                            PosRand.Add(PosToRand[(a * 8) + b]);
                                                        }
                                                    }
                                                    PosToRand.Clear();
                                                    for (int a = 0; a < PosRand.Count; a++)
                                                    {
                                                        PosToRand.Add(PosRand[a]);
                                                    }
                                                    PosRand.Clear();
                                                }
                                                if (rand.Next(2) == 0)
                                                {
                                                    //rotate 90 degrees clockwise
                                                    for (int a = 0; a < 8; a++)
                                                    {
                                                        for (int b = 0; b < 8; b++)
                                                        {
                                                            PosRand.Add(PosToRand[a + (b * 8)]);
                                                        }
                                                    }
                                                    PosToRand.Clear();
                                                    for (int a = 0; a < PosRand.Count; a++)
                                                    {
                                                        PosToRand.Add(PosRand[a]);
                                                    }
                                                    PosRand.Clear();
                                                }
                                                if (rand.Next(2) == 0)
                                                {
                                                    //rotate 90 degrees counter-clockwise
                                                    for (int a = 0; a < 8; a++)
                                                    {
                                                        for (int b = 7; b > -1; b--)
                                                        {
                                                            PosRand.Add(PosToRand[a + (b * 8)]);
                                                        }
                                                    }
                                                    PosToRand.Clear();
                                                    for (int a = 0; a < PosRand.Count; a++)
                                                    {
                                                        PosToRand.Add(PosRand[a]);
                                                    }
                                                    PosRand.Clear();
                                                }


                                                iter--;
                                            }

                                            for (int a = 0; a < PosToRand.Count; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Path[PosToRand[a]]);
                                            }

                                        }

                                        zone.Entities[i].Positions.Add(Path[Path.Length - 1]);

                                    }
                                    else if (zone.Entities[i].Type == 54 && zone.Entities[i].Subtype == 3) // Komodo radius marker
                                    {
                                        EntityPosition Path = new EntityPosition(zone.Entities[i].Positions[0].X, zone.Entities[i].Positions[0].Y, zone.Entities[i].Positions[0].Z);
                                        zone.Entities[i].Positions.Clear();
                                        short targetZ = 220;

                                        if (isBackwards)
                                        {
                                            targetZ = 1670; // doesn't seem to do much but might as well
                                        }
                                        else
                                        {
                                            targetZ = (short)rand.Next(100, 520);
                                        }

                                        Path = new EntityPosition(Path.X, Path.Y, targetZ);
                                        zone.Entities[i].Positions.Add(Path);
                                    }
                                    else if (zone.Entities[i].Type == 43 && zone.Entities[i].Subtype == 0) // Tiny platform
                                    {
                                        if (zone.Entities[i].ID != 107) //spawn
                                        {
                                            TinyPlats.Add(zone.Entities[i].Positions[0]);
                                            zone.Entities[i].Positions.Clear();
                                        }
                                    }
                                    else if (zone.Entities[i].Type == 44 && zone.Entities[i].Subtype == 0) // Tiny
                                    {
                                        TinyInit = zone.Entities[i].Positions[zone.Entities[i].Positions.Count - 1];
                                        //zone.Entities[i].Positions.Clear();
                                    }
                                    else if (zone.Entities[i].Type == 58 && zone.Entities[i].Subtype == 10) // N.Gin Rocket Spawners
                                    {
                                        List<EntityPosition> RPath = new List<EntityPosition>();
                                        for (int a = 0; a < zone.Entities[i].Positions.Count; a++)
                                        {
                                            RPath.Add(zone.Entities[i].Positions[a]);
                                        }
                                        RocketPaths.Add(RPath);
                                        zone.Entities[i].Positions.Clear();
                                    }
                                    else if (zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0 && level == Crash2_Levels.B05_Cortex) // Crash
                                    {

                                        List<EntityPosition> Pos1 = new List<EntityPosition>(zone.Entities[i].Positions);
                                        Pos1.Reverse();
                                        zone.Entities[i].Positions.Clear();
                                        for (int a = 0; a < Pos1.Count - 10; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Pos1[a]);
                                        }

                                        //CrashSpawn = zone.Entities[i].Positions[0];
                                    }
                                    else if (zone.Entities[i].Type == 22 && zone.Entities[i].Subtype == 0 && level == Crash2_Levels.B05_Cortex) // Cortex
                                    {
                                        List<EntityPosition> Pos1 = new List<EntityPosition>(zone.Entities[i].Positions);
                                        Pos1.Reverse();
                                        zone.Entities[i].Positions.Clear();
                                        for (int a = 0; a < 10; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Pos1[0]);
                                        }
                                        for (int a = 0; a < Pos1.Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Pos1[a]);
                                        }
                                    }
                                    else if (zone.Entities[i].Type == 58 && zone.Entities[i].Subtype == 0) // N.Gin
                                    {
                                        //todo: 2900 positions!!
                                    }
                                }
                            }

                            if (level == Crash2_Levels.B03_TinyTiger)
                            {

                                //short min_x = 747;
                                //short max_x = 1647 + 1;
                                short y = TinyPlats[0].Y;
                                //short min_z = 725;
                                //short max_z = 1625 + 1;
                                List<EntityPosition> NewPos = new List<EntityPosition>();
                                for (int i = 0; i < 8; i++)
                                {
                                    short x = (short)rand.Next(-100, 100);
                                    short z = (short)rand.Next(-100, 100);
                                    NewPos.Add(new EntityPosition((short)(TinyPlats[i].X + x), y, (short)(TinyPlats[i].Z + z)));
                                }
                                int iter = 0;

                                for (int i = 0; i < zone.Entities.Count; i++)
                                {
                                    if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                    {
                                        if (zone.Entities[i].Type == 43 && zone.Entities[i].Subtype == 0) // Tiny platform
                                        {
                                            if (zone.Entities[i].ID != 107) //spawn
                                            {
                                                zone.Entities[i].Positions.Add(NewPos[iter]);
                                                iter++;
                                            }
                                        }
                                        else if (zone.Entities[i].Type == 44 && zone.Entities[i].Subtype == 0) // Tiny
                                        {
                                            for (int a = 0; a < 10; a++)
                                            {
                                                if (a != 7)
                                                {
                                                    for (int b = 0; b < TinyPlats.Count; b++)
                                                    {
                                                        if (TinyPlats[b].X == zone.Entities[i].Positions[a].X && TinyPlats[b].Z == zone.Entities[i].Positions[a].Z)
                                                        {
                                                            zone.Entities[i].Positions[a] = NewPos[b];
                                                            b = TinyPlats.Count;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.B04_NGin)
                            {

                                int iter = 0;

                                List<int> RocketToRand = new List<int>();
                                for (int i = 0; i < RocketPaths.Count; i++)
                                {
                                    RocketToRand.Add(i);
                                }
                                List<int> RocketRand = new List<int>();
                                if (!isBackwards)
                                {
                                    for (int i = 0; i < RocketPaths.Count; i++)
                                    {
                                        if (i == 0 || i == 7)
                                        {
                                            RocketRand.Add(i);
                                        }
                                        else
                                        {
                                            int r = rand.Next(RocketToRand.Count);
                                            RocketRand.Add(RocketToRand[r]);
                                            RocketToRand.RemoveAt(r);
                                        }
                                    }
                                }

                                for (int i = 0; i < zone.Entities.Count; i++)
                                {
                                    if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                    {
                                        if (zone.Entities[i].Type == 58 && zone.Entities[i].Subtype == 10) // N.Gin Rocket Spawners
                                        {
                                            if (iter == 0 || iter == 7)
                                            {
                                                for (int a = 0; a < RocketPaths[iter].Count; a++)
                                                {
                                                    zone.Entities[i].Positions.Add(RocketPaths[iter][a]);
                                                }
                                            }
                                            else
                                            {
                                                // 0 - shoot out left
                                                // 1-6 - topdown rockets from left to right
                                                // 7 - shoot out right
                                                // 8-19 - sideways rockets, interchangeably shot from left-right

                                                if (isBackwards)
                                                {
                                                    if (iter >= 1 && iter <= 6)
                                                    {
                                                        int target = 7 - iter;
                                                        for (int a = 0; a < RocketPaths[target].Count; a++)
                                                        {
                                                            zone.Entities[i].Positions.Add(RocketPaths[target][a]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        int target = iter;
                                                        if (iter % 2 == 0)
                                                        {
                                                            target = iter + 1;
                                                        }
                                                        else
                                                        {
                                                            target = iter - 1;
                                                        }
                                                        for (int a = 0; a < RocketPaths[target].Count; a++)
                                                        {
                                                            zone.Entities[i].Positions.Add(RocketPaths[target][a]);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    int target = RocketRand[iter];
                                                    for (int a = 0; a < RocketPaths[target].Count; a++)
                                                    {
                                                        zone.Entities[i].Positions.Add(RocketPaths[target][a]);
                                                    }
                                                }

                                            }
                                            iter++;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            if (level == Crash2_Levels.B05_Cortex)
            {
                foreach (Chunk chunk in pair.nsf.Chunks)
                {
                    if (chunk is NormalChunk zonechunk)
                    {
                        for (int e = 0; e < zonechunk.Entries.Count; e++)
                        {
                            if (zonechunk.Entries[e] is ZoneEntry zone)
                            {

                                if (zone.EName == "33_7Z")
                                {
                                    SpawnZone = zone.EID;

                                    int xoffset = BitConv.FromInt32(zone.Layout, 0);
                                    int yoffset = BitConv.FromInt32(zone.Layout, 4);
                                    int zoffset = BitConv.FromInt32(zone.Layout, 8);

                                    CrashSpawn = new EntityPosition(748, 750, 880);

                                    pair.nsd.Spawns[0].ZoneEID = SpawnZone;
                                    pair.nsd.Spawns[0].SpawnX = (xoffset + CrashSpawn.X * 4) << 8;
                                    pair.nsd.Spawns[0].SpawnY = (yoffset + CrashSpawn.Y * 4) << 8;
                                    pair.nsd.Spawns[0].SpawnZ = (zoffset + CrashSpawn.Z * 4) << 8;
                                }

                                /*
                                if (linkedzones.Contains(zone.EID))
                                {
                                    Console.WriteLine("zone " + linkedzones.IndexOf(zone.EID) + ": " + zone.EName);
                                }
                                if (zone.EID == nsd.Spawns[0].ZoneEID)
                                {
                                    Console.WriteLine("spawnzone: " + zone.EName);
                                }
                                */

                            }
                        }
                    }
                }
            }

        }
    }
}
