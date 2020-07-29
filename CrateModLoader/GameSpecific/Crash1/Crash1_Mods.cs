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
            Crash1_Levels.L05_Upstream, // todo: cycles not triggering reasonably
            Crash1_Levels.L06_RollingStones,
            //Crash1_Levels.L07_HogWild, // todo: vehicle stuff, level just ends??
            Crash1_Levels.L08_NativeFortress,
            Crash1_Levels.L09_UpTheCreek, // todo: cycles not triggering reasonably
            Crash1_Levels.L10_LostCity,
            Crash1_Levels.L11_TempleRuins, // todo: zone transitions
            //Crash1_Levels.L12_RoadToNowhere, // crashes on warping out
            Crash1_Levels.L13_BoulderDash, // todo: invisible wall
            //Crash1_Levels.L14_WholeHog, // todo: vehicle stuff, not enough chunk space
            Crash1_Levels.L15_SunsetVista, // todo: death trigger at first vertical section
            Crash1_Levels.L16_HeavyMachinery, // todo: cycles, crutches
            Crash1_Levels.L17_CortexPower, // todo: broken spawn
            Crash1_Levels.L18_GeneratorRoom, // todo: cycles
            Crash1_Levels.L19_ToxicWaste, // todo: broken spawn
            Crash1_Levels.L20_HighRoad,
            Crash1_Levels.L21_SlipperyClimb, // todo: broken spawn
            Crash1_Levels.L22_LightsOut,  // todo: turn off darkness
            Crash1_Levels.L23_FumblingInTheDark, // todo: turn off darkness
            Crash1_Levels.L24_JawsOfDarkness, // todo: zone transitions, crutches
            Crash1_Levels.L25_CastleMachinery, // todo: cycles, crutches
            Crash1_Levels.L26_TheLab,
            Crash1_Levels.L27_GreatHall, // todo: doesn't spawn
            Crash1_Levels.L28_StormyAscent, // unverified
        };

        static List<Crash1_Levels> ChaseLevelsList = new List<Crash1_Levels>()
        {
            Crash1_Levels.L04_Boulders,
            Crash1_Levels.L13_BoulderDash,
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
                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
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
                                            zone.Entities.RemoveAt(i);
                                            i--;
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
                                    for (int id = 0; id < 2; id++)
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

            CrashEntity.Positions.Add(WarpOutPos);
            WarpOutEntity.Positions.Add(CrashPos);

            short tempID = CrashEntity.ID;
            CrashEntity.ID = WarpOutEntity.ID;
            WarpOutEntity.ID = tempID;

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
                            }
                            else if (zone.EName == WarpOutZone.EName)
                            {
                                zone.Entities.Add(CrashEntity);
                            }
                        }
                    }
                }
            }

            nsd.StartZone = WarpOutZone.EID;
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
