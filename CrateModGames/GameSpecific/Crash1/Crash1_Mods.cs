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
        MapMainMenu = 34,
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

        public enum CrateContentTypes : short
        {
            Wumpa_1 = 0,
            Wumpa_10 = 77,
            Wumpa_9 = 79,
            Wumpa_8 = 81,
            Wumpa_7 = 83,
            Wumpa_6 = 85,
            Wumpa_5 = 87,
            Wumpa_4 = 89,
            Wumpa_3 = 91,
            Wumpa_2 = 93,
            Wumpa_1_Anim = 96,
            Life = 97,
            Rand1 = 100,
            Rand2 = 101,
            Mask = 102,
            Token_Cortex = 103,
            Token_Brio = 104,
            Token_Tawna = 105,
        }

        public static List<CrateContentTypes> Crate_PossibleContents = new List<CrateContentTypes>()
        {
            CrateContentTypes.Wumpa_1,
            CrateContentTypes.Wumpa_2,
            CrateContentTypes.Wumpa_3,
            CrateContentTypes.Wumpa_4,
            CrateContentTypes.Wumpa_5,
            CrateContentTypes.Wumpa_6,
            CrateContentTypes.Wumpa_7,
            CrateContentTypes.Wumpa_8,
            CrateContentTypes.Wumpa_9,
            CrateContentTypes.Wumpa_10,
            CrateContentTypes.Wumpa_1_Anim,
            CrateContentTypes.Life,
            CrateContentTypes.Mask,
        };

        public static List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Outline, CrateSubTypes.Slot
        };
        public static List<CrateSubTypes> Crates_Wood = new List<CrateSubTypes>()
        {
            CrateSubTypes.Blank, //CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup
        };

        public static void Mod_RandomWoodCrates(NSF nsf, Random rand, Crash1_Levels level)
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
                                        if ((level == Crash1_Levels.L22_LightsOut || level == Crash1_Levels.L23_FumblingInTheDark) && ent.Subtype == (int)CrateSubTypes.Aku)
                                        {

                                        }
                                        else
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

        }

        public static void Mod_RandomCrateContents(NSF nsf, Random rand, Crash1_Levels level)
        {
            if (VehicleLevelsList.Contains(level))
            {
                return; // mask crashes oops
            }
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
                                    if ((CrateSubTypes)ent.Subtype == CrateSubTypes.Blank || (CrateSubTypes)ent.Subtype == CrateSubTypes.Pickup || (CrateSubTypes)ent.Subtype == CrateSubTypes.WoodSpring)
                                    {
                                        if (ent.ModeA != (short)CrateContentTypes.Token_Brio && ent.ModeA != (short)CrateContentTypes.Token_Cortex && ent.ModeA != (short)CrateContentTypes.Token_Tawna)
                                        {
                                            int r = rand.Next(Crate_PossibleContents.Count);
                                            ent.ModeA = (short)Crate_PossibleContents[r];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void Mod_TurnCratesIntoWumpa(NSF nsf, Random rand, Crash1_Levels level)
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
                            if (zone.EName != "c0_3Z" && zone.EName != "d3_TZ" && zone.EName != "d4_TZ") //cortex power ending, castle machinery last crates
                            {
                                foreach (OldEntity ent in zone.Entities)
                                {
                                    if (ent.Type == 34)
                                    {
                                        if ((Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype)
                                            || ent.Subtype == (int)CrateSubTypes.Checkpoint || ent.Subtype == (int)CrateSubTypes.Iron))
                                        {
                                            if ((level == Crash1_Levels.L22_LightsOut || level == Crash1_Levels.L23_FumblingInTheDark) && ent.Subtype == (int)CrateSubTypes.Aku)
                                            {

                                            }
                                            else
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
        static List<Crash1_Levels> BossLevelsList = new List<Crash1_Levels>()
        {
            //Crash1_Levels.B01_PapuPapu,
            Crash1_Levels.B02_RipperRoo,
            //Crash1_Levels.B03_KoalaKong,
            Crash1_Levels.B04_Pinstripe,
            //Crash1_Levels.B05_NBrio,
            Crash1_Levels.B06_Cortex,
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
            if (VehicleLevelsList.Contains(level))
            {
                Mod_HogLevelsBackwards(nsf, nsd, level);
                return;
            }
            if (BossLevelsList.Contains(level))
            {
                Mod_RandomizeBosses(nsf, nsd, level, rand, true);
                return;
            }

            OldEntity CrashEntity = null;
            OldZoneEntry CrashZone = null;
            OldEntity WarpOutEntity = null;
            OldZoneEntry WarpOutZone = null;
            bool DecoySpawn = true;

            bool FlipCamera = false;

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
                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        if (level != Crash1_Levels.L27_GreatHall)
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.EntityCount--;
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

                                    if (zone.Entities.Count > 0)
                                    {
                                        if (ChaseLevelsList.Contains(level))
                                        {
                                            if (zone.Entities[i].Type == 22 && zone.Entities[i].Subtype == 12) //boulders
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

                            if (FlipCamera && BackwardsCameraList.Contains(level) && zone.Cameras.Count > 0)
                            {
                                for (int i = 0; i < zone.Cameras.Count; i++)
                                {
                                    for (int d = 0; d < zone.Cameras[i].Positions.Count; d++)
                                    {
                                        zone.Cameras[i].Positions[d] = new OldCameraPosition(zone.Cameras[i].Positions[d].X, zone.Cameras[i].Positions[d].Y, zone.Cameras[i].Positions[d].Z, zone.Cameras[i].Positions[d].XRot, (short)(zone.Cameras[i].Positions[d].YRot + 2000), zone.Cameras[i].Positions[d].ZRot);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                    int crutchID = 299;
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
                                    int crutchID = 300;
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
                                    int crutchID = 302;
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
                                    int crutchID = 298;
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
                                    int crutchID = 300;
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntitySpring((short)entID, 14, 0, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntity((short)entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                else if (zone.EName == "0a_jZ")
                                {
                                    OldEntity gem = zone.Entities[0];

                                    for (int a = 0; a < gem.Positions.Count; a++)
                                    {
                                        gem.Positions[a] = new EntityPosition((short)(gem.Positions[a].X - 50), gem.Positions[a].Y, (short)(gem.Positions[a].Z - 150)); // buggy platform >_<
                                    }

                                    CreateEntityGemPlatform(297, 5, 0, 0, 0, ref zone);

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
                                        CreateEntityMask((short)entID, 3, 6, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntityMask((short)entID, 3, 6, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                    OldEntity gem = zone.Entities[0];
                                    CreateEntityGemPlatform(298, 5, 0, 0, 0, ref zone);

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
                                        CreateEntityMask((short)entID, 3, 6, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                            if (zone.EName == CrashZone.EName && level != Crash1_Levels.L12_RoadToNowhere)
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
                            if (level == Crash1_Levels.L21_SlipperyClimb && zone.EName == "i3_KZ")
                            {
                                nsd.StartZone = zone.EID;
                            }
                            if (level == Crash1_Levels.L19_ToxicWaste && zone.EName == "c6_7Z")
                            {
                                nsd.StartZone = zone.EID;
                            }
                            if (level == Crash1_Levels.L22_LightsOut && zone.EName == "d4_EZ")
                            {
                                nsd.StartZone = zone.EID;
                            }
                            if (level == Crash1_Levels.L12_RoadToNowhere && zone.EName == "a2_kZ")
                            {
                                zone.Entities.Add(WarpOutEntity);
                                zone.EntityCount++;
                            }
                        }
                    }
                }
            }

        }

        public static Entry Cache_Standard_WillC = null;
        public static List<Entry> Cache_Entry;

        public static void Cache_NormalCrashData(NSF nsf, OldNSD nsd, Crash1_Levels level)
        {
            if (level != Crash1_Levels.L05_Upstream)
            {
                return;
            }

            Cache_Entry = new List<Entry>();

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is GOOLEntry gool)
                        {
                            if (gool.EName == "WillC")
                            {
                                Cache_Standard_WillC = entry;
                            }
                        }
                        else if (entry is OldAnimationEntry anim)
                        {
                            if (anim.EName.StartsWith("Wi") || anim.EName.StartsWith("WW") || anim.EName.StartsWith("WD"))
                            {
                                Cache_Entry.Add(entry);
                            }
                        }
                        else if (entry is OldModelEntry model)
                        {
                            if (model.EName.StartsWith("Wi") || model.EName.StartsWith("WW") || model.EName.StartsWith("WD"))
                            {
                                Cache_Entry.Add(entry);
                            }
                        }
                    }
                }
            }
        }

        public static void Mod_HogLevelsBackwards(NSF nsf, OldNSD nsd, Crash1_Levels level)
        {
            if (!VehicleLevelsList.Contains(level))
            {
                return;
            }

            OldEntity CrashEntity = null;
            OldZoneEntry CrashZone = null;
            OldEntity WarpOutEntity = null;
            OldZoneEntry WarpOutZone = null;
            int SpawnEID = 0;

            /*
            List<bool> CacheChecks = new List<bool>();
            for (int i = 0; i < Cache_Entry.Count; i++)
            {
                CacheChecks.Add(false);
            }
            */

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    for (int e = 0; e < zonechunk.Entries.Count; e++)
                    {
                        /*
                        if (zonechunk.Entries[e] is GOOLEntry gool)
                        {
                            if (gool.EName == "WillC")
                            {
                                zonechunk.Entries[e] = Cache_Standard_WillC;
                            }
                        }
                        if (zonechunk.Entries[e] is OldAnimationEntry anim)
                        {
                            for (int i = 0; i < Cache_Entry.Count; i++)
                            {
                                if (anim.EName == Cache_Entry[i].EName)
                                {
                                    CacheChecks[i] = true;
                                }
                            }
                        }
                        else if (zonechunk.Entries[e] is OldModelEntry model)
                        {
                            for (int i = 0; i < Cache_Entry.Count; i++)
                            {
                                if (model.EName == Cache_Entry[i].EName)
                                {
                                    CacheChecks[i] = true;
                                }
                            }
                        }
                        */
                        if (zonechunk.Entries[e] is OldZoneEntry zone)
                        {
                            foreach (OldCamera cam in zone.Cameras)
                            {
                                short newFOV = (short)Math.Floor(cam.Zoom * 2d); // 1.5
                                cam.Zoom = newFOV;
                            }

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
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
                                        zone.EntityCount--;
                                        i--;
                                        */

                                        //CrashEntity.Flags = 196632;

                                    }
                                    else if (WarpOutEntity == null && zone.Entities[i].Type == 32 && zone.Entities[i].Subtype == 1)
                                    {
                                        WarpOutEntity = zone.Entities[i];
                                        WarpOutZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        zone.EntityCount--;
                                        i--;
                                    }


                                }
                            }

                            if (level == Crash1_Levels.L07_HogWild)
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
                                        CreateEntity((short)entID, 34, 2, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                if (zone.EName == "1M_hZ")
                                {
                                    SpawnEID = zone.EID;

                                    zone.Cameras[0].NeighborCount = 1;
                                }
                                else if (zone.EName != "s0_hZ" && zone.CameraCount > 0)
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
                                        CreateEntity((short)entID, 34, 2, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
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
                                        CreateEntityBoarBounce((short)entID, 51, 3, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                    }
                                }
                                if (zone.EName == "1M_uZ")
                                {
                                    SpawnEID = zone.EID;

                                    zone.Cameras[0].NeighborCount = 1;
                                }
                                else if (zone.EName != "s0_uZ" && zone.CameraCount > 0)
                                {
                                    zone.Cameras[0].NeighborCount = 2;
                                    zone.Cameras[0].Neighbors[1] = new OldCameraNeighbor(1, 1, 0, 2);
                                }
                            }

                        }
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
            if (level == Crash1_Levels.L07_HogWild)
            {
                WarpOutEntity.Positions.Add(new EntityPosition(400, 300, 500));
            }
            else
            {
                WarpOutEntity.Positions.Add(new EntityPosition(300, 450, 800)); 
            }
            

            List<int> LinkedZones = new List<int>();

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is OldZoneEntry zone)
                        {
                            if (level == Crash1_Levels.L07_HogWild)
                            {
                                if (zone.EName == "0a_hZ")
                                {
                                    zone.Entities.Add(WarpOutEntity);
                                    zone.EntityCount++;
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
                                    zone.EntityCount++;
                                }
                                if (zone.EName == "s0_uZ")
                                {
                                    BitConv.ToInt32(zone.Header, 532 + 1 * 4, SpawnEID);
                                }
                            }
                            
                        }
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

        public static void Mod_RandomizeBosses(NSF nsf, OldNSD nsd, Crash1_Levels level, Random rand, bool isBackwards)
        {
            if (!BossLevelsList.Contains(level))
            {
                return;
            }

            List<List<EntityPosition>> CortexShots = new List<List<EntityPosition>>();

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    for (int e = 0; e < zonechunk.Entries.Count; e++)
                    {
                        if (zonechunk.Entries[e] is OldZoneEntry zone)
                        {

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                {
                                    if (zone.Entities[i].Type == 39 && zone.Entities[i].Subtype == 0) // Ripper Roo TNT paths
                                    {
                                        // Big TNTs are hardcoded :(

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
                                    else if (zone.Entities[i].Type == 50 && zone.Entities[i].Subtype == 1) // Cortex projectiles
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

        static void CreateEntityMask(short id, int type, int subtype, short x, short y, short z, ref OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0, 0x00030018, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            newentity.ID = id;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = (byte)type;
            newentity.Subtype = (byte)subtype;

            newentity.Flags = 196633;
            newentity.ModeA = 0;
            newentity.ModeB = 0;
            newentity.ModeC = 450;

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static void CreateEntitySpring(short id, int type, int subtype, short x, short y, short z, ref OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0, 0x00030018, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            newentity.ID = id;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = (byte)type;
            newentity.Subtype = (byte)subtype;

            newentity.Flags = 196616;
            newentity.ModeA = 0;
            newentity.ModeB = 0;
            newentity.ModeC = 0;

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static void CreateEntityBoarBounce(short id, int type, int subtype, short x, short y, short z, ref OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0, 0x00030018, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            newentity.ID = id;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = (byte)type;
            newentity.Subtype = (byte)subtype;

            newentity.Flags = 196616;
            newentity.ModeA = 0;
            newentity.ModeB = 0;
            newentity.ModeC = 0;

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static void CreateEntityGemPlatform(short id, int subtype, short x, short y, short z, ref OldZoneEntry zone)
        {
            OldEntity newentity = OldEntity.Load(new OldEntity(0, 0x00030018, id, 0, 0, 0, 0, 0, new List<EntityPosition>() { new EntityPosition(0, 0, 0) }, 0).Save());
            newentity.ID = id;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = 58;
            newentity.Subtype = (byte)subtype;

            newentity.Flags = 196608;
            newentity.ModeA = 0;
            newentity.ModeB = 0;
            newentity.ModeC = 0;

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        public static void Mod_RandomizeMap(NSF nsf, NSD nsd, Crash1_Levels level, Random rand)
        {
            if (level != Crash1_Levels.MapMainMenu)
            {
                return;
            }

            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < 35; i++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < 35; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Add(LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }

            
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is MapEntry zone)
                        {
                            if (zone.EName == "3MapP" || zone.EName == "2MapP" || zone.EName == "1MapP")
                            {
                                for (int i = 0; i < zone.Entities.Count; i++)
                                {
                                    if (zone.Entities[i].Type == 44)
                                    {

                                    }
                                }
                            }
                            
                        }
                    }
                }
            }

        }

        public static void Mod_PantsColor(NSF nsf, OldSceneryColor color)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (ck is EntryChunk eck)
                {
                    foreach (Entry en in eck.Entries)
                    {
                        if (en is OldModelEntry)
                        {
                            OldModelEntry e = (OldModelEntry)en;
                            if (e.EName.StartsWith("Wi"))
                            {
                                // this does nothing...
                                for (int i = 0; i < e.Structs.Count; ++i)
                                {
                                    if (e.Structs[i] is OldSceneryColor col)
                                    {
                                        if (col.B > 0 && col.G < 110 && col.R < 110)
                                        {
                                            float intensity = col.B / 255f;
                                            e.Structs[i] = new OldSceneryColor((byte)(color.R * intensity), (byte)(color.G * intensity), (byte)(color.B * intensity), col.N);
                                        }
                                    }
                                    else if (e.Structs[i] is OldModelTexture tex)
                                    {
                                        if (tex.B > 0 && tex.G < 110 && tex.R < 110)
                                        {
                                            float intensity = tex.B / 255f;
                                            e.Structs[i] = new OldModelTexture(tex.UVIndex, tex.ClutX, tex.ClutY, tex.XOffU, tex.YOffU, tex.ColorMode, tex.BlendMode, tex.Segment,
                                                (byte)(color.R * intensity), (byte)(color.G * intensity), (byte)(color.B * intensity), 
                                                tex.N, tex.EID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Mod_Metadata(NSF nsf, OldNSD nsd, Crash1_Levels level, RegionType region)
        {
            if (level != Crash1_Levels.MapMainMenu)
            {
                return;
            }

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is GOOLEntry gool)
                        {
                            if (gool.EName == "GamOC" || gool.EName == "IsldC")
                            {
                                if (region == RegionType.NTSC_U || region == RegionType.PAL)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("PASSWORD"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i, 29, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            "SEED: " + seed,
                                        });
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("TEST SAVE"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i - 15, 27, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            "SEED: " + seed,
                                        });
                                        }
                                    }
                                }
                                
                            }

                        }
                    }
                }
            }
        }

        static void InsertStringsInByteArray(ref byte[] array, int index, int len, List<string> str)
        {
            int word = 0;
            int letter = 0;
            for (int i = index; i < index + len; i++)
            {
                array[i] = (byte)str[word][letter];
                letter++;
                if (letter >= str[word].Length)
                {
                    letter = 0;
                    word++;
                    i++;
                    array[i] = (byte)0;
                    if (word >= str.Count)
                    {
                        i = index + len;
                    }
                }
            }
        }

    }
}
