using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash2
{

    public enum Crash2_Levels
    {
        Unknown = -1,
        L01_TurtleWoods = 17,
        L02_SnowGo = 3,
        L03_HangEight = 13,
        L04_ThePits = 18,
        L05_CrashDash = 12,
        L06_SnowBiz = 6,
        L07_AirCrash = 19,
        L08_BearIt = 16,
        L09_CrashCrush = 15,
        L10_TheEelDeal = 22,
        L11_PlantFood = 20,
        L12_SewerOrLater = 0,
        L13_BearDown = 21,
        L14_RoadToRuin = 10,
        L15_UnBearable = 11,
        L16_HanginOut = 2,
        L17_DigginIt = 9,
        L18_ColdHardCrash = 8,
        L19_Ruination = 4,
        L20_BeeHaving = 23,
        L21_PistonItAway = 5,
        L22_RockIt = 7,
        L23_NightFight = 1,
        L24_PackAttack = 14,
        L25_SpacedOut = 25,
        L26_TotallyBear = 24,
        L27_TotallyFly = 26,
        B01_RipperRoo = 27,
        B02_KomodoBros = 28,
        B03_TinyTiger = 29,
        B04_NGin = 30,
        B05_Cortex = 31,
    }

    public static class Crash2_Mods
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
        }

        public static List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Outline,
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
                        if (entry is ZoneEntry)
                        {
                            ZoneEntry zone = (ZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Subtype != null && (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype)))
                                    {
                                        int entType = (int)Crates_Wood[rand.Next(Crates_Wood.Count)];
                                        ent.Subtype = entType;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CrashTri_Common.Fix_Detonator(nsf);
            CrashTri_Common.Fix_BoxCount(nsf);

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
                        if (entry is ZoneEntry)
                        {
                            ZoneEntry zone = (ZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Subtype != null && (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype) || ent.Subtype == (int)CrateSubTypes.Checkpoint))
                                    {
                                        ent.Type = 3;
                                        ent.Subtype = 16;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CrashTri_Common.Fix_Detonator(nsf);
            CrashTri_Common.Fix_BoxCount(nsf);
        }

        public static void Mod_CameraFOV(NSF nsf, Random rand, bool isRandom)
        {
            double FoV_Mod = 0.8d;
            if (isRandom)
            {
                FoV_Mod = (rand.NextDouble() / 3d) + 0.75d;
            }
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry)
                        {
                            ZoneEntry zone = (ZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.FOV != null && ent.FOV.RowCount > 0)
                                {
                                    for (int i = 0; i < ent.FOV.Rows.Count; i++)
                                    {
                                        for (int d = 0; d < ent.FOV.Rows[i].Values.Count; d++)
                                        {
                                            int newFOV = (int)Math.Floor(ent.FOV.Rows[i].Values[d].VictimID * FoV_Mod);
                                            ent.FOV.Rows[i].Values[d] = new EntityVictim((short)newFOV);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        static List<Crash2_Levels> BackwardsLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L01_TurtleWoods, Crash2_Levels.L04_ThePits, Crash2_Levels.L14_RoadToRuin, Crash2_Levels.L17_DigginIt, Crash2_Levels.L19_Ruination, Crash2_Levels.L20_BeeHaving, Crash2_Levels.L21_PistonItAway, Crash2_Levels.L25_SpacedOut, Crash2_Levels.L27_TotallyFly
        };

        public static void Mod_BackwardsLevels(NSF nsf, NSD nsd, Crash2_Levels level, bool isRandom, Random rand)
        {
            if (!BackwardsLevelsList.Contains(level))
            {
                return;
            }
            if (isRandom && rand.Next(2) == 0)
            {
                return;
            }

            Entity CrashEntity = null;
            ZoneEntry CrashZone = null;
            Entity WarpOutEntity = null;
            ZoneEntry WarpOutZone = null;
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
                                    }
                                    else if (zone.Entities[i].Type == 1 && zone.Entities[i].Subtype == 1)
                                    {
                                        WarpOutEntity = zone.Entities[i];
                                        WarpOutZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
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
            CrashEntity.Positions.Add(WarpOutPos);
            WarpOutEntity.Positions.Add(CrashPos);

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
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

            int xoffset = BitConv.FromInt32(WarpOutZone.Layout, 0);
            int yoffset = BitConv.FromInt32(WarpOutZone.Layout, 4);
            int zoffset = BitConv.FromInt32(WarpOutZone.Layout, 8);

            nsd.Spawns[0].ZoneEID = WarpOutZone.EID;
            nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 4) << 8;
            nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 4) << 8;
            nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 4) << 8;
        }

    }
}
