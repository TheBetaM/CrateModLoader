using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    // todo: split off backwards levels logic
    public class Crash1_Rand_BossPaths : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (!Crash1_Common.BossLevelsList.Contains(pair.LevelC1))
            {
                return;
            }

            bool isBackwards = false; // to remove

            List<List<EntityPosition>> CortexShots = new List<List<EntityPosition>>();

            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                    {
                        if (zone.Entities[i].Type == 39 && zone.Entities[i].Subtype == 0) // Ripper Roo TNT paths
                        {
                            // Big TNTs are hardcoded :(
                            // addendum: in RooOC

                            /*
                            EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                            zone.Entities[i].Positions.CopyTo(Path, 0);
                            zone.Entities[i].Positions.Clear();

                            for (int a = 0; a < 5; a++)
                            {
                                zone.Entities[i].Positions.Add(Path[a]);
                            }

                            if (isBackwards)
                            {
                                for (int a = 5; a < Path.Length - 3; a++)
                                {
                                    zone.Entities[i].Positions.Add(Path[Path.Length - a]);
                                }
                            }
                            else
                            {
                                int r = rand.Next(4);
                                switch (r)
                                {
                                    default:
                                    case 0:
                                        for (int a = 5; a < Path.Length - 3; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
                                        }
                                        break;
                                    case 1:
                                        for (int a = 5; a < Path.Length - 3; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[Path.Length - a]);
                                        }
                                        break;
                                    case 2:
                                        // todo sideways a
                                        for (int a = 5; a < Path.Length - 3; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
                                        }
                                        break;
                                    case 3:
                                        // todo sideways b
                                        for (int a = 5; a < Path.Length - 3; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[Path.Length - a]);
                                        }
                                        break;
                                }
                            }

                            for (int a = Path.Length - 3; a < Path.Length; a++)
                            {
                                zone.Entities[i].Positions.Add(Path[a]);
                            }
                            */

                        }
                        else if (zone.Entities[i].Type == 37 && zone.Entities[i].Subtype == 0) // Ripper Roo
                        {
                            EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                            zone.Entities[i].Positions.CopyTo(Path, 0);
                            zone.Entities[i].Positions.Clear();

                            for (int a = 0; a < 41; a++)
                            {
                                zone.Entities[i].Positions.Add(Path[a]);
                            }

                            if (isBackwards)
                            {
                                //41 - top left
                                zone.Entities[i].Positions.Add(Path[43]);
                                //42 - top middle
                                zone.Entities[i].Positions.Add(Path[42]);
                                //43 - top right
                                zone.Entities[i].Positions.Add(Path[41]);
                                //44 - middle left
                                zone.Entities[i].Positions.Add(Path[46]);
                                //45 - middle middle
                                zone.Entities[i].Positions.Add(Path[45]);
                                //46 - middle right
                                zone.Entities[i].Positions.Add(Path[44]);
                                //47 - bot left
                                zone.Entities[i].Positions.Add(Path[49]);
                                //48 - bot middle
                                zone.Entities[i].Positions.Add(Path[48]);
                                //49 - bot right
                                zone.Entities[i].Positions.Add(Path[47]);
                            }
                            else
                            {
                                List<int> PosToRand = new List<int>();
                                for (int a = 41; a < Path.Length - 1; a++)
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

                            zone.Entities[i].Positions.Add(Path[Path.Length - 1]);

                        }
                        else if (zone.Entities[i].Type == 15 && zone.Entities[i].Subtype == 0) // Pinstripe
                        {
                            EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                            zone.Entities[i].Positions.CopyTo(Path, 0);
                            zone.Entities[i].Positions.Clear();

                            /*
                                0 - middle
                                0-15 - middle-to-left
                            15-30 - left-to-middle
                            30-45 - middle-to-right
                            45-60 - right-to-middle
                            60-65 - back-to-right
                            65-95 - right-to-left
                            */

                            if (isBackwards)
                            {
                                zone.Entities[i].Positions.Add(Path[0]);

                                for (int a = 1; a < 16; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 30]);

                                for (int a = 1; a < 16; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 45]);

                                for (int a = 1; a < 16; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 0]);

                                for (int a = 1; a < 16; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 15]);

                                for (int a = 1; a < 5; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 60]);

                                for (int a = 0; a < 31; a++)
                                    zone.Entities[i].Positions.Add(Path[(30 - a) + 65]);

                            }
                            else
                            {
                                List<int> PosToRand = new List<int>();
                                for (int a = 0; a < 4; a++)
                                {
                                    PosToRand.Add(a);
                                }
                                int count = PosToRand.Count;
                                for (int c = 0; c < count; c++)
                                {
                                    int r = rand.Next(PosToRand.Count);
                                    if (PosToRand[r] == 0)
                                    {
                                        if (c == 0)
                                            zone.Entities[i].Positions.Add(Path[0]);

                                        for (int a = 1; a < 16; a++)
                                            zone.Entities[i].Positions.Add(Path[a + 0]);
                                    }
                                    else if (PosToRand[r] == 1)
                                    {
                                        if (c == 0)
                                            zone.Entities[i].Positions.Add(Path[15]);

                                        for (int a = 1; a < 16; a++)
                                            zone.Entities[i].Positions.Add(Path[a + 15]);
                                    }
                                    else if (PosToRand[r] == 2)
                                    {
                                        if (c == 0)
                                            zone.Entities[i].Positions.Add(Path[30]);

                                        for (int a = 1; a < 16; a++)
                                            zone.Entities[i].Positions.Add(Path[a + 30]);
                                    }
                                    else if (PosToRand[r] == 3)
                                    {
                                        if (c == 0)
                                            zone.Entities[i].Positions.Add(Path[45]);

                                        for (int a = 1; a < 16; a++)
                                            zone.Entities[i].Positions.Add(Path[a + 45]);
                                    }
                                    PosToRand.RemoveAt(r);
                                }

                                for (int a = 1; a < 5; a++)
                                    zone.Entities[i].Positions.Add(Path[a + 60]);

                                if (rand.Next(2) == 0)
                                {
                                    for (int a = 0; a < 31; a++)
                                        zone.Entities[i].Positions.Add(Path[(30 - a) + 65]);
                                }
                                else
                                {
                                    for (int a = 0; a < 31; a++)
                                        zone.Entities[i].Positions.Add(Path[a]);
                                }

                            }

                        }
                        else if (zone.Entities[i].Type == 49 && zone.Entities[i].Subtype == 0) // Cortex
                        {
                            if (isBackwards)
                            {
                                EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                zone.Entities[i].Positions.CopyTo(Path, 0);
                                zone.Entities[i].Positions.Clear();
                                for (int a = 0; a < Path.Length; a++)
                                {
                                    zone.Entities[i].Positions.Add(Path[(Path.Length - 1) - a]);
                                }
                            }
                        }
                        else if (zone.Entities[i].Type == 50 && zone.Entities[i].Subtype == 1) // Cortex projectile paths
                        {

                            EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                            zone.Entities[i].Positions.CopyTo(Path, 0);
                            zone.Entities[i].Positions.Clear();

                            if (isBackwards)
                            {
                                for (int a = 0; a < Path.Length; a++)
                                {
                                    zone.Entities[i].Positions.Add(Path[(Path.Length - 1) - a]);
                                }
                            }
                            else
                            {
                                List<EntityPosition> PPath = new List<EntityPosition>();
                                for (int a = 0; a < Path.Length; a++)
                                {
                                    PPath.Add(Path[a]);
                                }
                                CortexShots.Add(PPath);

                            }
                        }
                    }
                }

                if (!isBackwards)
                {
                    for (int i = 0; i < zone.Entities.Count; i++)
                    {
                        if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                        {
                            if (zone.Entities[i].Type == 50 && zone.Entities[i].Subtype == 1) // Cortex projectiles
                            {

                                int r = rand.Next(8);
                                switch (r)
                                {
                                    default:
                                    case 0:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[0][a]);
                                        }
                                        break;
                                    case 1:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[0][(CortexShots[0].Count - 1) - a]);
                                        }
                                        break;
                                    case 2:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[1][a]);
                                        }
                                        break;
                                    case 3:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[1][(CortexShots[1].Count - 1) - a]);
                                        }
                                        break;
                                    case 4:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[2][a]);
                                        }
                                        break;
                                    case 5:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[2][(CortexShots[2].Count - 1) - a]);
                                        }
                                        break;
                                    case 6:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[3][a]);
                                        }
                                        break;
                                    case 7:
                                        for (int a = 0; a < CortexShots[0].Count; a++)
                                        {
                                            zone.Entities[i].Positions.Add(CortexShots[3][(CortexShots[3].Count - 1) - a]);
                                        }
                                        break;
                                }


                            }
                        }
                    }
                }

                break;
            }
        }
    }
}
