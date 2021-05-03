using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    // todo: sort out FOV mod stuff
    public class Crash1_Rand_BackwardsLevels : ModStruct<NSF_Pair>
    {
        private bool isRandom;
        private Random rand;

        public static List<Crash1_Levels> BackwardsLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L01_NSanityBeach,
            Crash1_Levels.L02_JungleRollers,
            Crash1_Levels.L03_GreatGate,
            Crash1_Levels.L04_Boulders,
            Crash1_Levels.L05_Upstream,
            Crash1_Levels.L06_RollingStones,
            //Crash1_Levels.L07_HogWild, // too hard to include in the main option, uncomment to add it
            Crash1_Levels.L08_NativeFortress,

            Crash1_Levels.L09_UpTheCreek,
            Crash1_Levels.L10_LostCity,
            Crash1_Levels.L11_TempleRuins,
            Crash1_Levels.L12_RoadToNowhere,
            Crash1_Levels.L13_BoulderDash, // some zone jank, but it works
            //Crash1_Levels.L14_WholeHog, // too hard to include in the main option, uncomment to add it
            Crash1_Levels.L15_SunsetVista, // zone jank

            Crash1_Levels.L16_HeavyMachinery,
            Crash1_Levels.L17_CortexPower,
            Crash1_Levels.L18_GeneratorRoom,
            Crash1_Levels.L19_ToxicWaste,
            Crash1_Levels.L20_HighRoad,
            Crash1_Levels.L21_SlipperyClimb,
            Crash1_Levels.L22_LightsOut,
            Crash1_Levels.L23_FumblingInTheDark,
            Crash1_Levels.L24_JawsOfDarkness,
            Crash1_Levels.L25_CastleMachinery,
            Crash1_Levels.L26_TheLab,
            Crash1_Levels.L27_GreatHall,
            Crash1_Levels.L28_StormyAscent, // unverified

            //Crash1_Levels.B01_PapuPapu,
            Crash1_Levels.B02_RipperRoo,
            //Crash1_Levels.B03_KoalaKong,
            Crash1_Levels.B04_Pinstripe,
            //Crash1_Levels.B05_NBrio,
            Crash1_Levels.B06_Cortex,
        };

        public static List<Crash1_Levels> BackwardsCameraList = new List<Crash1_Levels>()
        {
            //Crash1_Levels.L01_NSanityBeach, // maybe
            Crash1_Levels.L02_JungleRollers, // full level
            //Crash1_Levels.L05_Upstream, // maybe
            Crash1_Levels.L06_RollingStones, // full level
            Crash1_Levels.L07_HogWild, // full level
            //Crash1_Levels.L09_UpTheCreek, // maybe
            //Crash1_Levels.L11_TempleRuins, // some sections?
            Crash1_Levels.L12_RoadToNowhere, // full level
            //Crash1_Levels.L14_WholeHog, // full level
            Crash1_Levels.L17_CortexPower, // full level
            //Crash1_Levels.L18_GeneratorRoom, // not needed?
            Crash1_Levels.L19_ToxicWaste, // full level
            Crash1_Levels.L20_HighRoad, // full level
            Crash1_Levels.L22_LightsOut, // full level
            Crash1_Levels.L23_FumblingInTheDark, // full level
            //Crash1_Levels.L24_JawsOfDarkness, // some sections?
            Crash1_Levels.L26_TheLab, // full level

            //Crash1_Levels.L03_GreatGate, // not needed
            //Crash1_Levels.L04_Boulders, // not needed
            //Crash1_Levels.L08_NativeFortress, // not needed
            //Crash1_Levels.L10_LostCity, // not needed
            //Crash1_Levels.L13_BoulderDash, // not needed
            //Crash1_Levels.L15_SunsetVista, // not needed
            //Crash1_Levels.L16_HeavyMachinery, // not needed
            //Crash1_Levels.L21_SlipperyClimb, // not needed
            //Crash1_Levels.L25_CastleMachinery, // not needed
            //Crash1_Levels.L27_GreatHall, // not needed
            //Crash1_Levels.L28_StormyAscent, // not needed
        };

        public Crash1_Rand_BackwardsLevels(bool isrand)
        {
            isRandom = Crash1_Props_Main.Option_RandBackwardsLevels.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            Crash1_Levels level = pair.LevelC1;
            if (!BackwardsLevelsList.Contains(level))
            {
                return;
            }
            if (isRandom && rand.Next(2) == 0)
            {
                return;
            }
            if (Crash1_Common.VehicleLevelsList.Contains(level))
            {
                //Mod_HogLevelsBackwards(nsf, nsd, level);
                return;
            }
            if (Crash1_Common.BossLevelsList.Contains(level))
            {
                //Mod_RandomizeBosses(nsf, nsd, level, rand, true);
                return;
            }
            if (!Crash1_Common.ChaseLevelsList.Contains(level))
            {
                //Mod_CameraFOV(nsf, rand, false);
            }

            OldEntity CrashEntity = null;
            OldZoneEntry CrashZone = null;
            OldEntity WarpOutEntity = null;
            OldZoneEntry WarpOutZone = null;
            bool DecoySpawn = true;

            bool FlipCamera = false;

            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                    {
                        CrashEntity = zone.Entities[i];
                        CrashZone = zone;
                        if (level != Crash1_Levels.L27_GreatHall)
                        {
                            zone.Entities.RemoveAt(i);
                            i--;
                            if (level == Crash1_Levels.L12_RoadToNowhere && DecoySpawn)
                            {
                                CrashEntity = null;
                                DecoySpawn = false;
                            }
                        }
                        else
                        {
                            zone.Entities[i].Positions.Clear();
                            zone.Entities[i].Positions.Add(new EntityPosition(476, 593, 324));
                        }
                    }
                    else if (WarpOutEntity == null && zone.Entities[i].Type == 32 && zone.Entities[i].Subtype == 1)
                    {
                        if (DecoySpawn && (level == Crash1_Levels.L13_BoulderDash || level == Crash1_Levels.L22_LightsOut))
                        {
                            DecoySpawn = false;
                        }
                        else
                        {
                            WarpOutEntity = zone.Entities[i];
                            WarpOutZone = zone;
                            if (level != Crash1_Levels.L27_GreatHall)
                            {
                                zone.Entities.RemoveAt(i);
                                i--;
                            }
                            else
                            {
                                zone.Entities[i].Positions.Clear();
                                zone.Entities[i].Positions.Add(new EntityPosition(476, 493, 1174));
                            }
                        }
                    }

                    if (zone.Entities.Count > 0)
                    {
                        if (Crash1_Common.ChaseLevelsList.Contains(level))
                        {
                            if (zone.Entities[i].Type == 22 && zone.Entities[i].Subtype == 12) //boulders
                            {
                                zone.Entities[i].Type = 3;
                                zone.Entities[i].Subtype = 16;
                                //zone.Entities[i].Flags = 196632;
                                zone.Entities[i].VecX = 0;
                                zone.Entities[i].VecY = 0;
                                zone.Entities[i].VecZ = 0;
                                if (zone.Entities[i].Positions.Count > 1)
                                {
                                    while (zone.Entities[i].Positions.Count > 1)
                                    {
                                        zone.Entities[i].Positions.RemoveAt(1);
                                    }
                                }
                            }
                        }
                    }
                }

                if (FlipCamera && BackwardsCameraList.Contains(level) && zone.Cameras.Count > 0)
                {
                    for (int i = 0; i < zone.Cameras.Count; i++)
                    {
                        for (int d = 0; d < zone.Cameras[i].Positions.Count; d++)
                        {
                            zone.Cameras[i].Positions[d] = new OldCameraPosition(zone.Cameras[i].Positions[d].X, zone.Cameras[i].Positions[d].Y, zone.Cameras[i].Positions[d].Z, zone.Cameras[i].Positions[d].XRot, (short)(zone.Cameras[i].Positions[d].YRot + 0x800), zone.Cameras[i].Positions[d].ZRot);
                        }
                    }
                }

                //crutch zone
                if (level == Crash1_Levels.L04_Boulders)
                {
                    if (zone.EName == "0t_eZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(580, 670, 780),
                                new EntityPosition(750, 950, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                }
                else if (level == Crash1_Levels.L05_Upstream)
                {
                    if (zone.EName == "0r_fZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(560, 220, 720),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0o_fZ")
                    {
                        int crutchID = 301;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(680, 220, 120),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0y_fZ")
                    {
                        int crutchID = 299;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(620, 310, 686),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0A_fZ")
                    {
                        EntityPosition[] GemPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(GemPath, 0);
                        zone.Entities[2].Positions.Clear();
                        for (int a = 0; a < GemPath.Length; a++)
                        {
                            int pos = (GemPath.Length - 1) - a;
                            GemPath[pos] = new EntityPosition((short)(GemPath[pos].X - 300), GemPath[pos].Y, (short)(GemPath[pos].Z - 20));
                            zone.Entities[2].Positions.Add(GemPath[pos]);
                        }
                    }
                }
                else if (level == Crash1_Levels.L06_RollingStones)
                {
                    if (zone.EName == "0I_lZ")
                    {
                        int crutchID = 299;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(550, 1100, 600),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                }
                else if (level == Crash1_Levels.L09_UpTheCreek)
                {
                    if (zone.EName == "0B_oZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(550, 250, 820),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0d_oZ")
                    {
                        int crutchID = 301;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(480, 250, 550),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0q_oZ")
                    {
                        int crutchID = 302;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(650, 200, 280),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }

                }
                else if (level == Crash1_Levels.L11_TempleRuins)
                {
                    if (zone.EName == "c4_sZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "c0_sZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(850, 700, 350),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "b0_sZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }

                }
                else if (level == Crash1_Levels.L16_HeavyMachinery)
                {
                    if (zone.EName == "i5_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[3].Positions.Count];
                        zone.Entities[3].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[3].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[3].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                        PlatPath = new EntityPosition[zone.Entities[4].Positions.Count];
                        zone.Entities[4].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[4].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[4].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }

                    }
                    else if (zone.EName == "i3_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[5].Positions.Count];
                        zone.Entities[5].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[5].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[5].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "i2_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                        PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "h7_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[2].Positions.Count];
                        zone.Entities[2].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[2].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[2].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "h2_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "g9_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "g1_6Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[3].Positions.Count];
                        zone.Entities[3].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[3].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[3].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }

                }
                else if (level == Crash1_Levels.L18_GeneratorRoom)
                {
                    if (zone.EName == "c2_5Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }

                    }
                    else if (zone.EName == "b4_5Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                        PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b5_5Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "c0_5Z")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                        PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b3_5Z")
                    {
                        int crutchID = 299;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(527, 1095, 505),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "a9_5Z")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(854, 545, 360),
                                new EntityPosition(1504, 705, 360),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "a8_5Z")
                    {
                        int crutchID = 302;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(570, 905, 360),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }

                }
                else if (level == Crash1_Levels.L24_JawsOfDarkness)
                {
                    if (zone.EName == "d9_tZ")
                    {
                        int crutchID = 298;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(400, 700, 450),
                                new EntityPosition(200, 820, 450),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "c9_tZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b3_tZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b4_tZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b9_tZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(850, 800, 1040),
                                new EntityPosition(1050, 900, 1040),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "s0_tZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }

                }
                else if (level == Crash1_Levels.L25_CastleMachinery)
                {
                    if (zone.EName == "e1_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "e0_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[1].Positions.Count];
                        zone.Entities[1].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[1].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[1].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "d7_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[3].Positions.Count];
                        zone.Entities[3].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[3].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[3].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "d6_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[2].Positions.Count];
                        zone.Entities[2].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[2].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[2].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "c0_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "b2_TZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                    else if (zone.EName == "c8_TZ")
                    {
                        zone.Entities[0].Positions.Clear();
                        //zone.Entities[0].Positions.Add(new EntityPosition(1168, 373, 190));
                        zone.Entities[0].Positions.Add(new EntityPosition(1068, 223, 190));
                    }
                    else if (zone.EName == "d3_TZ")
                    {
                        int crutchID = 250;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(850, 500, 180),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 3, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "d2_TZ")
                    {
                        int crutchID = 252;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(500, 110, 180),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "c6_TZ")
                    {
                        int crutchID = 255;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(170, 80, 180),
                                new EntityPosition(270, 1580, 180),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 1, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "b6_TZ")
                    {
                        int crutchID = 260;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(400, 1220, 150),
                                new EntityPosition(900, 720, 150),
                                new EntityPosition(480, 220, 150),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 3, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "b5_TZ")
                    {
                        int crutchID = 265;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(1100, 390, 150),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "b4_TZ")
                    {
                        int crutchID = 267;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(720, 280, 180),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "b3_TZ")
                    {
                        int crutchID = 269;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(1000, 250, 180),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "a5_TZ")
                    {
                        int crutchID = 275;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(270, 200, 200),
                                new EntityPosition(470, 1100, 200),
                                new EntityPosition(670, 1600, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 0, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "a4_TZ")
                    {

                        int crutchID = 280;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(470, 1100, 200),
                                new EntityPosition(270, 1600, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 0, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "a3_TZ")
                    {

                        int crutchID = 285;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(470, 1100, 200),
                                new EntityPosition(670, 1600, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 0, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "a2_TZ")
                    {
                        int crutchID = 290;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(470, 1100, 200),
                                new EntityPosition(270, 1600, 200),
                                new EntityPosition(270, 2000, 200),
                                new EntityPosition(270, 2500, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }

                        OldCameraNeighbor CamSet2 = new OldCameraNeighbor(1, 1, 0, 2);

                        zone.Cameras[0].NeighborCount++;
                        zone.Cameras[0].Neighbors[1] = CamSet2;
                    }
                    else if (zone.EName == "a1_TZ")
                    {
                        int crutchID = 295;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(770, -110, 200),
                                new EntityPosition(970, -110, 200),
                                new EntityPosition(1170, -110, 200),
                                new EntityPosition(770, -210, 200),
                                new EntityPosition(770, 500, 200),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                }
                else if (level == Crash1_Levels.L21_SlipperyClimb)
                {
                    if (zone.EName == "g3_KZ")
                    {
                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[0].Positions.Count];
                        zone.Entities[0].Positions.CopyTo(PlatPath, 0);
                        zone.Entities[0].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[0].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }
                    }
                }
                else if (level == Crash1_Levels.L13_BoulderDash)
                {
                    if (zone.EName == "0d_jZ")
                    {
                        int crutchID = 298;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(500, 520, 800),
                                new EntityPosition(500, 520, 910),
                                new EntityPosition(600, 520, 800),
                                new EntityPosition(600, 520, 910),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0u_jZ")
                    {
                        int crutchID = 302;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(450, 620, 950),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0B_jZ")
                    {
                        int crutchID = 295;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(300, 1100, -800),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "0a_jZ")
                    {
                        OldEntity gem = zone.Entities[0];

                        for (int a = 0; a < gem.Positions.Count; a++)
                        {
                            gem.Positions[a] = new EntityPosition((short)(gem.Positions[a].X - 50), gem.Positions[a].Y, (short)(gem.Positions[a].Z - 150)); // buggy platform >_<
                        }

                        CreateEntityGemPlatform(297, 5, 0, 0, 0, zone);

                        int pos = zone.Entities.Count - 1;
                        EntityPosition[] PlatPath = new EntityPosition[gem.Positions.Count];
                        gem.Positions.CopyTo(PlatPath, 0);
                        zone.Entities[pos].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[pos].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }

                        //zone.Cameras[0].Neighbors[1] = new OldCameraNeighbor(1, 2, 0, 1); // orig - 1 2 0 5
                    }
                    else if (zone.EName == "0b_jZ")
                    {
                        zone.Cameras[1].NeighborCount = 2;
                        zone.Cameras[1].Neighbors[0] = new OldCameraNeighbor(1, 0, 0, 2);
                        zone.Cameras[1].Neighbors[1] = new OldCameraNeighbor(2, 2, 0, 5);
                    }
                    else if (zone.EName == "1b_jZ")
                    {
                        zone.Cameras[0].NeighborCount = 2;
                        zone.Cameras[0].Neighbors[0] = new OldCameraNeighbor(1, 2, 1, 2);
                        zone.Cameras[0].Neighbors[1] = new OldCameraNeighbor(2, 0, 3, 1);
                    }
                }
                else if (level == Crash1_Levels.L22_LightsOut)
                {
                    if (zone.EName == "d5_EZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(475, 500, 950),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            CreateEntityMask((short)entID, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                    else if (zone.EName == "C8_EZ")
                    {
                        int crutchID = 299;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(524, -1100, 400),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            CreateEntityMask((short)entID, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                        OldEntity gem = zone.Entities[0];
                        CreateEntityGemPlatform(298, 5, 0, 0, 0, zone);

                        int pos = zone.Entities.Count - 1;
                        EntityPosition[] PlatPath = new EntityPosition[gem.Positions.Count];
                        gem.Positions.CopyTo(PlatPath, 0);
                        zone.Entities[pos].Positions.Clear();
                        for (int a = 0; a < PlatPath.Length; a++)
                        {
                            zone.Entities[pos].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                        }

                        // move THE ENTIRE ZONE because the transition doesn't work otherwise

                        int xoffset = BitConv.FromInt32(zone.Layout, 0);
                        int yoffset = BitConv.FromInt32(zone.Layout, 4);
                        int zoffset = BitConv.FromInt32(zone.Layout, 8);
                        int x2 = BitConv.FromInt32(zone.Layout, 12);
                        int y2 = BitConv.FromInt32(zone.Layout, 16);
                        int z2 = BitConv.FromInt32(zone.Layout, 20);
                        int xmax = (ushort)BitConv.FromInt16(zone.Layout, 0x1E);
                        int ymax = (ushort)BitConv.FromInt16(zone.Layout, 0x20);
                        int zmax = (ushort)BitConv.FromInt16(zone.Layout, 0x22);

                        short CamOffset = 2600;
                        short EntOffset = 700; //800
                        double ScaleMod = 1.6;

                        short offset = 0x20;
                        int value = (short)(ymax * ScaleMod);
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);

                        offset = 4;
                        value = yoffset - CamOffset;
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);

                        offset = 16;
                        value = (short)(y2 * ScaleMod);
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);
                        zone.Layout[offset + 2] = (byte)(value >> 8 * 2);
                        zone.Layout[offset + 3] = (byte)(value >> 8 * 3);


                        for (int i = 0; i < zone.Entities.Count; i++)
                        {
                            for (int a = 0; a < zone.Entities[i].Positions.Count; a++)
                            {
                                zone.Entities[i].Positions[a] = new EntityPosition(zone.Entities[i].Positions[a].X, (short)(zone.Entities[i].Positions[a].Y + EntOffset), zone.Entities[i].Positions[a].Z);
                            }
                        }

                        for (int i = 0; i < zone.Cameras[0].Positions.Count; i++)
                        {
                            zone.Cameras[0].Positions[i] = new OldCameraPosition(zone.Cameras[0].Positions[i].X, (short)(zone.Cameras[0].Positions[i].Y + CamOffset), zone.Cameras[0].Positions[i].Z, zone.Cameras[0].Positions[i].XRot, zone.Cameras[0].Positions[i].YRot, zone.Cameras[0].Positions[i].ZRot);
                        }


                    }
                }
                else if (level == Crash1_Levels.L23_FumblingInTheDark)
                {
                    if (zone.EName == "e0_GZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(475, 500, 650),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            CreateEntityMask((short)entID, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }
                }
                else if (level == Crash1_Levels.L15_SunsetVista)
                {
                    if (zone.EName == "n3_zZ")
                    {
                        // move THE ENTIRE ZONE because the transition doesn't work otherwise

                        int xoffset = BitConv.FromInt32(zone.Layout, 0);
                        int yoffset = BitConv.FromInt32(zone.Layout, 4);
                        int zoffset = BitConv.FromInt32(zone.Layout, 8);
                        int x2 = BitConv.FromInt32(zone.Layout, 12);
                        int y2 = BitConv.FromInt32(zone.Layout, 16);
                        int z2 = BitConv.FromInt32(zone.Layout, 20);
                        int xmax = (ushort)BitConv.FromInt16(zone.Layout, 0x1E);
                        int ymax = (ushort)BitConv.FromInt16(zone.Layout, 0x20);
                        int zmax = (ushort)BitConv.FromInt16(zone.Layout, 0x22);

                        short CamOffset = 2000;
                        short EntOffset = 505;
                        double ScaleMod = 1.25;

                        short offset = 0x20;
                        int value = (short)(ymax * ScaleMod);
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);

                        offset = 4;
                        value = yoffset - CamOffset;
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);

                        offset = 16;
                        value = (short)(y2 * ScaleMod);
                        zone.Layout[offset] = (byte)value;
                        zone.Layout[offset + 1] = (byte)(value >> 8);
                        zone.Layout[offset + 2] = (byte)(value >> 8 * 2);
                        zone.Layout[offset + 3] = (byte)(value >> 8 * 3);

                        for (int i = 0; i < zone.Entities.Count; i++)
                        {
                            zone.Entities[i].Positions[0] = new EntityPosition(zone.Entities[i].Positions[0].X, (short)(zone.Entities[i].Positions[0].Y + EntOffset), zone.Entities[i].Positions[0].Z);
                        }
                        for (int i = 0; i < zone.Cameras[0].Positions.Count; i++)
                        {
                            zone.Cameras[0].Positions[i] = new OldCameraPosition(zone.Cameras[0].Positions[i].X, (short)(zone.Cameras[0].Positions[i].Y + CamOffset), zone.Cameras[0].Positions[i].Z, zone.Cameras[0].Positions[i].XRot, zone.Cameras[0].Positions[i].YRot, zone.Cameras[0].Positions[i].ZRot);
                        }

                    }
                    else if (zone.EName == "h3_zZ")
                    {
                        int crutchID = 300;
                        EntityPosition[] crate_pos = new EntityPosition[]
                        {
                                new EntityPosition(800, 450, 900),
                        };
                        for (int id = 0; id < crate_pos.Length; id++)
                        {
                            int entID = id + crutchID;
                            Crash1_Common.CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, zone);
                        }
                    }

                }
                else if (level == Crash1_Levels.L28_StormyAscent)
                {
                    if (zone.EName == "h2_yZ")
                    {
                        int crutchID = 290;
                        CreateEntityStormyElevator((short)crutchID, 370, 619, 294, zone, 400);
                    }
                    else if (zone.EName == "h1_yZ")
                    {
                        zone.Entities.RemoveAt(2);
                        int crutchID = 291;
                        CreateEntityStormyElevator((short)crutchID, 160, 444, 294, zone, 400);
                    }
                    else if (zone.EName == "g5_yZ")
                    {
                        int crutchID = 292;
                        CreateEntityStormyElevator((short)crutchID, 1250, -130, 294, zone, 400);
                        crutchID = 293;
                        CreateEntityStormyElevator((short)crutchID, 320, 669, 294, zone, 400);
                    }
                    else if (zone.EName == "g4_yZ")
                    {
                        zone.Entities.RemoveAt(1);
                        int crutchID = 294;
                        CreateEntityStormyElevator((short)crutchID, 1350, 94, 294, zone, 400);
                    }
                    else if (zone.EName == "g1_yZ")
                    {
                        // we need space!!!
                        zone.Entities.RemoveAt(2);
                    }
                    else if (zone.EName == "f4_yZ")
                    {
                        // we need space!!!
                        zone.Entities.RemoveAt(1);
                    }
                    else if (zone.EName == "j1_yZ")
                    {
                        // we need space!!!
                        zone.Entities.RemoveAt(3);
                        zone.Entities.RemoveAt(1);
                    }
                    else if (zone.EName == "i2_yZ")
                    {
                        // we need space!!!
                        zone.Entities.RemoveAt(2);
                    }
                    else if (zone.EName == "i4_yZ")
                    {
                        // we need space!!!
                        zone.Entities.RemoveAt(2);
                    }
                }

            }

            if (level == Crash1_Levels.L27_GreatHall)
            {
                // a'ight imma head out
                // re: lol
                return;
            }

            EntityPosition CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
            EntityPosition WarpOutPos = new EntityPosition(WarpOutEntity.Positions[0].X, WarpOutEntity.Positions[0].Y, WarpOutEntity.Positions[0].Z);
            CrashEntity.Positions.RemoveAt(0);
            WarpOutEntity.Positions.RemoveAt(0);

            // fixes for some warp outs because crash has to land on it to work
            if (level == Crash1_Levels.L03_GreatGate)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 130), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L06_RollingStones)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 50), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L08_NativeFortress)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 130), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L12_RoadToNowhere)
            {
                CrashPos = new EntityPosition(550, 197, 1450);
            }
            else if (level == Crash1_Levels.L05_Upstream)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 510), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L09_UpTheCreek)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 125), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L18_GeneratorRoom)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 450), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L13_BoulderDash)
            {
                CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 230), CrashPos.Z);
            }
            else if (level == Crash1_Levels.L28_StormyAscent)
            {
                CrashPos = new EntityPosition((short)(CrashPos.X - 700), CrashPos.Y, CrashPos.Z);
            }
            else if (level == Crash1_Levels.L17_CortexPower || level == Crash1_Levels.L01_NSanityBeach)
            {
                //CrashEntity.Flags = 203416;
                CrashEntity.VecY = 0;
            }

            CrashEntity.Positions.Add(WarpOutPos);
            WarpOutEntity.Positions.Add(CrashPos);

            short tempID = CrashEntity.ID;
            CrashEntity.ID = WarpOutEntity.ID;
            WarpOutEntity.ID = tempID;

            pair.oldnsd.StartZone = WarpOutZone.EID;

            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                if (zone.EName == CrashZone.EName && level != Crash1_Levels.L12_RoadToNowhere)
                {
                    zone.Entities.Add(WarpOutEntity);
                }
                else if (zone.EName == WarpOutZone.EName)
                {
                    zone.Entities.Add(CrashEntity);
                }
                if (level == Crash1_Levels.L17_CortexPower && zone.EName == "c0_3Z")
                {
                    pair.oldnsd.StartZone = zone.EID;
                }
                if (level == Crash1_Levels.L21_SlipperyClimb && zone.EName == "i3_KZ")
                {
                    pair.oldnsd.StartZone = zone.EID;
                }
                if (level == Crash1_Levels.L19_ToxicWaste && zone.EName == "c6_7Z")
                {
                    pair.oldnsd.StartZone = zone.EID;
                }
                if (level == Crash1_Levels.L22_LightsOut && zone.EName == "d4_EZ")
                {
                    pair.oldnsd.StartZone = zone.EID;
                }
                if (level == Crash1_Levels.L12_RoadToNowhere && zone.EName == "a2_kZ")
                {
                    zone.Entities.Add(WarpOutEntity);
                }
            }
        }


        private void CreateEntityMask(short id, short x, short y, short z, OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0x18, 3, 0, id, 0, 0, 450, 3, 6, new List<EntityPosition>() { new EntityPosition(x, y, z) }, 0).Save());

            zone.Entities.Add(newentity);

        }

        private void CreateEntityGemPlatform(short id, int subtype, short x, short y, short z, OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0x18, 3, 0, id, 0, 0, 0, 58, (byte)subtype, new List<EntityPosition>() { new EntityPosition(x, y, z) }, 0).Save());

            zone.Entities.Add(newentity);
        }

        private void CreateEntityStormyElevator(short id, short x, short y, short z, OldZoneEntry zone, short height)
        {
            List<EntityPosition> path = new List<EntityPosition>() { new EntityPosition(x, y, z) };
            path.Add(new EntityPosition(x, (short)(y + (height * 0.05)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.1)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.2)), z));
            path.Add(new EntityPosition(x, (short)(y + (height * 0.3)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.4)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.5)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.6)), z));
            path.Add(new EntityPosition(x, (short)(y + (height * 0.7)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.8)), z));
            //path.Add(new EntityPosition(x, (short)(y + (height * 0.9)), z));
            path.Add(new EntityPosition(x, (short)(y + (height * 0.95)), z));
            path.Add(new EntityPosition(x, (short)(y + height), z));

            OldEntity newentity = OldEntity.Load(new OldEntity(0x18, 3, 0, id, 120, 0, 0, 11, 6, path, 0).Save());
            zone.Entities.Add(newentity);
        }

    }
}
