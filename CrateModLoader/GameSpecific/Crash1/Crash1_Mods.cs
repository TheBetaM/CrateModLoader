using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash1
{

    public enum Crash1_Levels
    {
        Unknown = -1,
        L01_NSanityBeach = 4,
        L02_JungleRollers = 5,
        L03_GreatGate = 9,
        L04_Boulders = 6,
        L05_Upstream = 7,
        L06_RollingStones = 12,
        L07_HogWild = 8,
        L08_NativeFortress = 15,
        L09_UpTheCreek = 14,
        L10_LostCity = 19,
        L11_TempleRuins = 16,
        L12_RoadToNowhere = 11,
        L13_BoulderDash = 10,
        L14_WholeHog = 18,
        L15_SunsetVista = 21,
        L16_HeavyMachinery = 2,
        L17_CortexPower = 0,
        L18_GeneratorRoom = 1,
        L19_ToxicWaste = 3,
        L20_HighRoad = 13,
        L21_SlipperyClimb = 26,
        L22_LightsOut = 22,
        L23_FumblingInTheDark = 24,
        L24_JawsOfDarkness = 17,
        L25_CastleMachinery = 27,
        L26_TheLab = 23,
        L27_GreatHall = 25,
        L28_StormyAscent = 20,
        B01_PapuPapu = 28,
        B02_RipperRoo = 29,
        B03_KoalaKong = 30,
        B04_Pinstripe = 31,
        B05_NBrio = 32,
        B06_Cortex = 33,
    }

    public static class Crash1_Mods
    {
        public enum CrateSubTypes
        {
            TNT = 0,
            Blank = 2,
            WoodSpring = 3,
            Checkpoint = 4,
            Iron = 5,
            Fruit = 6, //Multiple bounce
            IronSwitch = 7,
            Life = 8,
            Aku = 9,
            Pickup = 10,
            Pow = 11, //allegedly
            Outline = 13,
            IronSpring = 15,
            AutoPickup = 17,
            Nitro = 18,
            AutoBlank = 20,
            Blank2 = 21,
            Steel = 23,
            Slot = 25,
        }

        public static List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Outline, CrateSubTypes.Slot
        };
        public static List<CrateSubTypes> Crates_Wood = new List<CrateSubTypes>()
        {
            CrateSubTypes.Blank, //CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup
        };

        public static void Mod_RandomWoodCrates(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry)
                        {
                            OldZoneEntry zone = (OldZoneEntry)entry;
                            foreach (OldEntity ent in zone.Entities)
                            {
                                if (ent.Type == 34)
                                {
                                    if ((Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype)))
                                    {
                                        int entType = (int)Crates_Wood[rand.Next(Crates_Wood.Count)];
                                        ent.Subtype = (byte)entType;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void Mod_TurnCratesIntoWumpa(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry)
                        {
                            OldZoneEntry zone = (OldZoneEntry)entry;
                            foreach (OldEntity ent in zone.Entities)
                            {
                                if (ent.Type == 34)
                                {
                                    if ((Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype) || ent.Subtype == (int)CrateSubTypes.Checkpoint))
                                    {
                                        ent.Type = 3;
                                        ent.Subtype = 16;
                                        ent.Flags = 196632;
                                        ent.ModeA = 0;
                                        ent.ModeB = 0;
                                        ent.ModeC = 0;
                                        if (ent.Positions.Count > 1)
                                        {
                                            while (ent.Positions.Count > 1)
                                            {
                                                ent.Positions.RemoveAt(1);
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

        public static void Mod_CameraFOV(NSF nsf, Random rand, bool isRandom)
        {
            double FoV_Mod = 1.5d;
            if (isRandom)
            {
                FoV_Mod = rand.NextDouble() + 0.5d;
            }
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry zone)
                        {
                            foreach (OldCamera cam in zone.Cameras)
                            {
                                short newFOV = (short)Math.Floor(cam.Zoom * FoV_Mod);
                                cam.Zoom = newFOV;
                            }
                        }
                    }
                }
            }
        }

        static List<Crash1_Levels> BackwardsLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L01_NSanityBeach,
            Crash1_Levels.L02_JungleRollers,
            Crash1_Levels.L03_GreatGate, 
            Crash1_Levels.L04_Boulders,
            Crash1_Levels.L05_Upstream,
            Crash1_Levels.L06_RollingStones,
            //Crash1_Levels.L07_HogWild, // todo: vehicle stuff
            Crash1_Levels.L08_NativeFortress,

            Crash1_Levels.L09_UpTheCreek,
            Crash1_Levels.L10_LostCity,
            Crash1_Levels.L11_TempleRuins,
            //Crash1_Levels.L12_RoadToNowhere, // crashes on warping out
            //Crash1_Levels.L13_BoulderDash, // todo: invisible wall
            //Crash1_Levels.L14_WholeHog, // todo: vehicle stuff
            //Crash1_Levels.L15_SunsetVista, // todo: death trigger at first vertical section

            Crash1_Levels.L16_HeavyMachinery,
            Crash1_Levels.L17_CortexPower,
            Crash1_Levels.L18_GeneratorRoom,
            //Crash1_Levels.L19_ToxicWaste, // todo: broken spawn
            Crash1_Levels.L20_HighRoad,
            //Crash1_Levels.L21_SlipperyClimb, // todo: broken spawn
            //Crash1_Levels.L22_LightsOut,  // todo: turn off darkness
            //Crash1_Levels.L23_FumblingInTheDark, // todo: turn off darkness
            Crash1_Levels.L24_JawsOfDarkness,
            //Crash1_Levels.L25_CastleMachinery, // todo: cycles, crutches
            Crash1_Levels.L26_TheLab,
            Crash1_Levels.L27_GreatHall,
            Crash1_Levels.L28_StormyAscent, // unverified
        };

        static List<Crash1_Levels> ChaseLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L04_Boulders,
            Crash1_Levels.L13_BoulderDash,
        };
        static List<Crash1_Levels> VehicleLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L07_HogWild,
            Crash1_Levels.L14_WholeHog,
        };

        static List<Crash1_Levels> BackwardsCameraList = new List<Crash1_Levels>()
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

        public static void Mod_BackwardsLevels(NSF nsf, OldNSD nsd, Crash1_Levels level, bool isRandom, Random rand)
        {
            if (!BackwardsLevelsList.Contains(level))
            {
                return;
            }
            if (isRandom && rand.Next(2) == 0)
            {
                return;
            }

            OldEntity CrashEntity = null;
            OldZoneEntry CrashZone = null;
            OldEntity WarpOutEntity = null;
            OldZoneEntry WarpOutZone = null;
            bool DecoySpawn = true;

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                {
                                    if (!VehicleLevelsList.Contains(level))
                                    {
                                        if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                        {
                                            CrashEntity = zone.Entities[i];
                                            CrashZone = zone;
                                            if (!VehicleLevelsList.Contains(level) && level != Crash1_Levels.L27_GreatHall)
                                            {
                                                zone.Entities.RemoveAt(i);
                                                zone.EntityCount--;
                                                i--;
                                            }
                                            else if (level != Crash1_Levels.L27_GreatHall)
                                            {
                                                EntityPosition[] BoarPath = new EntityPosition[zone.Entities[i].Positions.Count];
                                                zone.Entities[i].Positions.CopyTo(BoarPath, 0);
                                                zone.Entities[i].Positions.Clear();
                                                for (int a = 0; a < BoarPath.Length; a++)
                                                {
                                                    zone.Entities[i].Positions.Add(BoarPath[(BoarPath.Length - 1) - a]);
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
                                                    zone.EntityCount--;
                                                    i--;
                                                }
                                                else
                                                {
                                                    zone.Entities[i].Positions.Clear();
                                                    zone.Entities[i].Positions.Add(new EntityPosition(476, 493, 1174));
                                                }
                                            }
                                        }
                                    }

                                    if (zone.Entities.Count > 0)
                                    {
                                        if (ChaseLevelsList.Contains(level))
                                        {
                                            if (zone.Entities[i].Type == 22) //boulders
                                            {
                                                zone.Entities[i].Type = 3;
                                                zone.Entities[i].Subtype = 16;
                                                zone.Entities[i].Flags = 196632;
                                                zone.Entities[i].ModeA = 0;
                                                zone.Entities[i].ModeB = 0;
                                                zone.Entities[i].ModeC = 0;
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                    int crutchID = 300;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(527, 1095, 505),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                else if (zone.EName == "a9_5Z")
                                {
                                    int crutchID = 301;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(854, 545, 360),
                                          new EntityPosition(1504, 705, 360),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                else if (zone.EName == "a8_5Z")
                                {
                                    int crutchID = 303;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(570, 905, 360),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }

                            }
                            else if (level == Crash1_Levels.L24_JawsOfDarkness)
                            {
                                if (zone.EName == "d9_tZ")
                                {
                                    int crutchID = 300;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(400, 700, 450),
                                          new EntityPosition(200, 820, 450),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                    int crutchID = 302;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(850, 800, 1040),
                                          new EntityPosition(1050, 900, 1040),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                else if (zone.EName == "d3_TZ")
                                {
                                    int crutchID = 300;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(780, 500, 130),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                else if (zone.EName == "d2_TZ")
                                {
                                    int crutchID = 301;
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                          new EntityPosition(650, 200, 140),
                                    };
                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchID;
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }

                            }

                        }
                    }
                }
            }

            if (level == Crash1_Levels.L27_GreatHall)
            {
                // a'ight imma head out
                return;
            }

            if (!VehicleLevelsList.Contains(level))
            {
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
                    CrashPos = new EntityPosition(CrashPos.X, (short)(CrashPos.Y - 390), (short)(CrashPos.Z + 200));
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
                else if (level == Crash1_Levels.L17_CortexPower || level == Crash1_Levels.L01_NSanityBeach)
                {
                    CrashEntity.Flags = 203416;
                    CrashEntity.ModeB = 0;
                }

                CrashEntity.Positions.Add(WarpOutPos);
                WarpOutEntity.Positions.Add(CrashPos);

                short tempID = CrashEntity.ID;
                CrashEntity.ID = WarpOutEntity.ID;
                WarpOutEntity.ID = tempID;

                nsd.StartZone = WarpOutZone.EID;

                foreach (Chunk chunk in nsf.Chunks)
                {
                    if (chunk is NormalChunk zonechunk)
                    {
                        foreach (Entry entry in zonechunk.Entries)
                        {
                            if (entry is OldZoneEntry zone)
                            {
                                if (zone.EName == CrashZone.EName)
                                {
                                    zone.Entities.Add(WarpOutEntity);
                                    zone.EntityCount++;
                                }
                                else if (zone.EName == WarpOutZone.EName)
                                {
                                    zone.Entities.Add(CrashEntity);
                                    zone.EntityCount++;
                                }
                                if (level == Crash1_Levels.L17_CortexPower && zone.EName == "c0_3Z")
                                {
                                    nsd.StartZone = zone.EID;
                                }
                            }
                        }
                    }
                }

                
            }
            else
            {

                WarpOutEntity.Positions.Clear();
                WarpOutEntity.Positions.Add(new EntityPosition(400, 300, 500));

                foreach (Chunk chunk in nsf.Chunks)
                {
                    if (chunk is NormalChunk zonechunk)
                    {
                        foreach (Entry entry in zonechunk.Entries)
                        {
                            if (entry is OldZoneEntry zone)
                            {
                                if (zone.EName == "0a_hZ")
                                {
                                    zone.Entities.Add(WarpOutEntity);
                                    zone.EntityCount++;
                                }
                                else if (zone.EName == "1O_hZ")
                                {
                                    nsd.StartZone = zone.EID;
                                }
                            }
                        }
                    }
                }
            }

        }

        static void CreateEntity(short id, int type, int subtype, short x, short y, short z, ref OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0, 0x00030018, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            newentity.ID = id;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = (byte)type;
            newentity.Subtype = (byte)subtype;

            newentity.Flags = 196632;
            newentity.ModeA = 0;
            newentity.ModeB = 0;
            newentity.ModeC = 0;

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

    }
}
