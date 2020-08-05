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
        WarpRoom = 32,
        WarpRoom2 = 33,
        WarpRoom3 = 34,
        WarpRoom4 = 35,
        WarpRoom5 = 36,
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

        public static void Mod_RandomCrateContents(NSF nsf, Random rand)
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
                                    if ((CrateSubTypes)ent.Subtype == CrateSubTypes.Blank || (CrateSubTypes)ent.Subtype == CrateSubTypes.Pickup || (CrateSubTypes)ent.Subtype == CrateSubTypes.WoodSpring)
                                    {
                                        int r = rand.Next(Crate_PossibleContents.Count);
                                        ent.Settings[0] = new EntitySetting(ent.Settings[0].ValueA, (int)Crate_PossibleContents[r]);
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
                FoV_Mod = (rand.NextDouble() / 2d) + 0.75d;
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
            Crash2_Levels.L01_TurtleWoods,
            //Crash2_Levels.L02_SnowGo, // todo: 2 section teleports
            Crash2_Levels.L03_HangEight, // onfoot for now, todo: board stuff - freezes at the second jetski (board needs to be in loadlists?)
            Crash2_Levels.L04_ThePits,
            Crash2_Levels.L05_CrashDash, // todo: broken box counter

            //Crash2_Levels.L06_SnowBiz, // todo: 2 section teleports
            Crash2_Levels.L07_AirCrash, // onfoot for now, todo: board stuff - too many objects?
            Crash2_Levels.L08_BearIt, // todo: camera stitching, bear stuff
            Crash2_Levels.L09_CrashCrush,  // todo: broken box counter
            //Crash2_Levels.L10_TheEelDeal, // todo: 1 section teleport

            Crash2_Levels.L11_PlantFood, // onfoot for now, todo: board stuff - out of space on PAL (maybe not anymore), sometimes crashes on death if too far away from spawn (board needs to be in loadlists?)
            Crash2_Levels.L12_SewerOrLater,
            Crash2_Levels.L13_BearDown, // todo: camera stitching, bear stuff
            Crash2_Levels.L14_RoadToRuin,
            Crash2_Levels.L15_UnBearable, // todo: broken box counter

            //Crash2_Levels.L16_HanginOut, // todo: 2 section teleports
            Crash2_Levels.L17_DigginIt,
            //Crash2_Levels.L18_ColdHardCrash, // todo: 2 section teleports
            Crash2_Levels.L19_Ruination,
            Crash2_Levels.L20_BeeHaving,

            Crash2_Levels.L21_PistonItAway,
            //Crash2_Levels.L22_RockIt, // todo: crashes on jetpack pickup, because of lack of alarms?
            Crash2_Levels.L23_NightFight,
            //Crash2_Levels.L24_PackAttack, // todo: crashes on jetpack pickup, because of lack of alarms?
            Crash2_Levels.L25_SpacedOut,

            Crash2_Levels.L26_TotallyBear, // todo: camera stitching, bear stuff
            Crash2_Levels.L27_TotallyFly
        };

        static List<Crash2_Levels> ChaseLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L05_CrashDash, 
            Crash2_Levels.L09_CrashCrush, 
            Crash2_Levels.L15_UnBearable,
        };
        static List<Crash2_Levels> BoardLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L03_HangEight,
            Crash2_Levels.L07_AirCrash,
            Crash2_Levels.L11_PlantFood,
        };
        static List<Crash2_Levels> SpaceLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L22_RockIt,
            Crash2_Levels.L24_PackAttack,
        };
        static List<Crash2_Levels> DarknessLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L23_NightFight,
            Crash2_Levels.L27_TotallyFly,
        };
        static List<Crash2_Levels> BeeLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L20_BeeHaving,
            Crash2_Levels.L17_DigginIt,
        };
        static List<Crash2_Levels> BearLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L08_BearIt,
            Crash2_Levels.L13_BearDown,
            Crash2_Levels.L15_UnBearable,
            Crash2_Levels.L26_TotallyBear,
        };

        static List<Crash2_Levels> BackwardsCameraList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L01_TurtleWoods, // full level
            //Crash2_Levels.L02_SnowGo, // 2 sections
            Crash2_Levels.L03_HangEight, // full level
            Crash2_Levels.L04_ThePits, // full level
            //Crash2_Levels.L05_CrashDash, // not needed
            //Crash2_Levels.L06_SnowBiz, // 2 sections
            Crash2_Levels.L07_AirCrash, // full level
            Crash2_Levels.L08_BearIt, // full level
            //Crash2_Levels.L09_CrashCrush, // not needed
            Crash2_Levels.L10_TheEelDeal, // full level
            Crash2_Levels.L11_PlantFood, // full level
            Crash2_Levels.L12_SewerOrLater, // full level
            Crash2_Levels.L13_BearDown, // full level
            //Crash2_Levels.L14_RoadToRuin, // some sections?
            //Crash2_Levels.L15_UnBearable, // not needed
            Crash2_Levels.L16_HanginOut, // full level
            Crash2_Levels.L17_DigginIt, // full level
            //Crash2_Levels.L18_ColdHardCrash, // 2 sections
            //Crash2_Levels.L19_Ruination, // some sections?
            Crash2_Levels.L20_BeeHaving, // full levvel
            //Crash2_Levels.L21_PistonItAway, // not needed
            Crash2_Levels.L22_RockIt, // full level
            Crash2_Levels.L23_NightFight, // full level
            Crash2_Levels.L24_PackAttack, // full level
            //Crash2_Levels.L25_SpacedOut, // not needed
            Crash2_Levels.L26_TotallyBear, // full level
            Crash2_Levels.L27_TotallyFly // full level
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
            Entity EmptyEntity = null;
            Entity BoxCounterEntity = null;
            ZoneEntry BoxCounterZone = null;
            List<ZoneEntry> BoardLaunchZones = new List<ZoneEntry>();
            List<Entity> BoardEnts = new List<Entity>();
            List<ZoneEntry> BoardDropoffZones = new List<ZoneEntry>();
            List<Entity> BoardDropEnts = new List<Entity>();
            Entity JetpackEnt = null;
            Entity SpacepadEnt = null;
            ZoneEntry JetpackZone = null;
            ZoneEntry SpacepadZone = null;
            Entity ElevEnt = null;
            Entity ElevCatchEnt = null;
            ZoneEntry ElevCatchZone = null;
            ZoneEntry ElevZone = null;

            bool CameraFlip = false;

            bool Debug_DontMoveCounter = false;
            bool Debug_DontMoveCrash = false;

            if (BearLevelsList.Contains(level) && level != Crash2_Levels.L15_UnBearable) //temp forward testing
            {
                Debug_DontMoveCounter = true;
                Debug_DontMoveCrash = true;
            }

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

                                if (DarknessLevelsList.Contains(level))
                                {
                                    //design workaround: disable darkness
                                    if (zone.Entities[i].ExtraProperties.ContainsKey(0x185))
                                    {
                                        EntityUInt32Property property = (EntityUInt32Property)zone.Entities[i].ExtraProperties[0x185];
                                        foreach (EntityPropertyRow<uint> row in property.Rows)
                                        {
                                            if (row.Values.Count >= 1)
                                                row.Values[0] &= ~4U;
                                        }
                                    }
                                }

                                //save some chunk space
                                zone.Entities[i].Name = null;

                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        if (EmptyEntity == null)
                                            EmptyEntity = zone.Entities[2];
                                        if (!Debug_DontMoveCrash)
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        
                                    }
                                    else if (WarpOutEntity == null && zone.Entities[i].Type == 1 && zone.Entities[i].Subtype == 1)
                                    {
                                        WarpOutEntity = zone.Entities[i];
                                        WarpOutZone = zone;
                                        if (!Debug_DontMoveCrash)
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    else if (BoxCounterEntity == null && zone.Entities[i].Type == 4 && zone.Entities[i].Subtype == 17)
                                    {
                                        BoxCounterEntity = zone.Entities[i];
                                        BoxCounterZone = zone;
                                        if (!Debug_DontMoveCounter)
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }

                                    if (BoardLevelsList.Contains(level))
                                    {

                                        /*
                                        if (zone.Entities[i].Type == 47 && zone.Entities[i].Subtype == 2) // Board launch
                                        {
                                            BoardEnts.Add(zone.Entities[i]);
                                            BoardLaunchZones.Add(zone);
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        else if (zone.Entities[i].Type == 47 && zone.Entities[i].Subtype == 3) // Board dropoff
                                        {
                                            BoardDropEnts.Add(zone.Entities[i]);
                                            BoardDropoffZones.Add(zone);
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        */
                                    }

                                    if (SpaceLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 35 && zone.Entities[i].Subtype == 1) // Jetpack
                                        {
                                            JetpackEnt = zone.Entities[i];
                                            JetpackZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        else if (zone.Entities[i].Type == 35 && zone.Entities[i].Subtype == 8) // Spacepad
                                        {
                                            SpacepadEnt = zone.Entities[i];
                                            SpacepadZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        else if (zone.Entities[i].Type == 35 && zone.Entities[i].Subtype == 6) // Spacelock
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                    }

                                    if (ChaseLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 41 && zone.Entities[i].Subtype == 0) // Boulder
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 39 && zone.Entities[i].Subtype == 5) // Boulder door
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 41 && zone.Entities[i].Subtype == 2) // Papa bear
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 48 && zone.Entities[i].Subtype == 0) // Bear
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 48 && zone.Entities[i].Subtype == 9) // Secret bear
                                        {
                                            //zone.Entities.RemoveAt(i);
                                            //zone.Entities.Insert(i, EmptyEntity);
                                            //i--;
                                            zone.Entities[i].Type = 1;
                                            zone.Entities[i].Subtype = 9;
                                            zone.Entities[i].Settings.Add(new EntitySetting(0, 0));
                                            zone.Entities[i].Settings.Add(new EntitySetting(0, 0));
                                        }
                                    }

                                    if (BeeLevelsList.Contains(level)) // temporary until flipped camera is working
                                    {
                                        if (zone.Entities[i].Type == 18 && zone.Entities[i].Subtype == 0) // Bee hive
                                        {
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
                                        }
                                    }

                                    if (level == Crash2_Levels.L02_SnowGo)
                                    {
                                        if (ElevEnt == null && zone.Entities[i].Type == 9 && zone.Entities[i].Subtype == 6) // Elevator
                                        {
                                            ElevEnt = zone.Entities[i];
                                            ElevZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        else if (ElevCatchEnt == null && zone.Entities[i].Type == 9 && zone.Entities[i].Subtype == 32) // Elevator catch
                                        {
                                            ElevCatchEnt = zone.Entities[i];
                                            ElevCatchZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                }
                                else
                                {
                                    if (CameraFlip && BackwardsCameraList.Contains(level) && zone.Entities[i].CameraIndex != null && zone.Entities[i].CameraSubIndex != null)
                                    {
                                        if (zone.Entities[i].CameraSubIndex == 1)
                                        {
                                            EntityPosition[] Angles = new EntityPosition[zone.Entities[i].Positions.Count];
                                            zone.Entities[i].Positions.CopyTo(Angles, 0);
                                            for (int a = 0; a < Angles.Length; a++)
                                            {
                                                Angles[a] = new EntityPosition(Angles[a].X, (short)(Angles[a].Y + 2000), Angles[a].Z);
                                            }
                                            zone.Entities[i].Positions.Clear();
                                            for (int a = 0; a < Angles.Length; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Angles[a]);
                                            }
                                        }
                                        else if (zone.Entities[i].CameraSubIndex == 0)
                                        {
                                            
                                            if (zone.Entities[i].DrawListA != null)
                                            {
                                                
                                                for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                                                {
                                                    //zone.Entities[i].DrawListA.Rows[a].MetaValue = Math.Max((short)(zone.Entities[i].DrawListA.Rows[a].MetaValue - 5), (short)0);
                                                    //zone.Entities[i].DrawListA.Rows[a].MetaValue = Math.Min((short)(zone.Entities[i].DrawListA.Rows[a].MetaValue + 8), (short)(zone.Entities[i].Positions.Count - 1));
                                                    zone.Entities[i].DrawListA.Rows[a].MetaValue = Math.Max((short)(zone.Entities[i].DrawListA.Rows[a].MetaValue - 10), (short)0);
                                                }
                                                
                                                //zone.Entities[i].DrawListA.Rows.Reverse();
                                            }
                                            if (zone.Entities[i].DrawListB != null)
                                            {
                                                
                                                for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                                                {
                                                    //zone.Entities[i].DrawListB.Rows[a].MetaValue = Math.Max((short)(zone.Entities[i].DrawListB.Rows[a].MetaValue - 5), (short)0);
                                                    //zone.Entities[i].DrawListB.Rows[a].MetaValue = Math.Min((short)(zone.Entities[i].DrawListB.Rows[a].MetaValue + 8), (short)(zone.Entities[i].Positions.Count - 1));
                                                    zone.Entities[i].DrawListB.Rows[a].MetaValue = Math.Min((short)(zone.Entities[i].DrawListB.Rows[a].MetaValue + 10), (short)(zone.Entities[i].Positions.Count - 1));
                                                }
                                                
                                                //zone.Entities[i].DrawListB.Rows.Reverse();
                                            }
                                            
                                            // switching positions gives some results, but breaks the level, so it's not the right approach
                                            /*
                                            if (zone.Entities[i].Positions.Count > 0)
                                            {
                                                List<EntityPosition> campos = new List<EntityPosition>();
                                                for (int a = 0; a < zone.Entities[i].Positions.Count; a++)
                                                {
                                                    campos.Add(zone.Entities[i].Positions[a]);
                                                }
                                                campos.Reverse();
                                                zone.Entities[i].Positions.Clear();
                                                for (int a = 0; a < campos.Count; a++)
                                                {
                                                    zone.Entities[i].Positions.Add(campos[a]);
                                                }
                                            }
                                            if (zone.Entities[i].DrawListA != null)
                                            {
                                                List<short> MetaValues = new List<short>();
                                                for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                                                {
                                                    MetaValues.Add((short)zone.Entities[i].DrawListA.Rows[a].MetaValue);
                                                }
                                                for (int a = 1; a < zone.Entities[i].DrawListA.Rows.Count + 1; a++)
                                                {
                                                    zone.Entities[i].DrawListA.Rows[a - 1].MetaValue = (short)((zone.Entities[i].Positions.Count - 1)  - MetaValues[a - 1]);
                                                }
                                            }
                                            if (zone.Entities[i].DrawListB != null)
                                            {
                                                List<short> MetaValues = new List<short>();
                                                for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                                                {
                                                    MetaValues.Add((short)zone.Entities[i].DrawListB.Rows[a].MetaValue);
                                                }
                                                for (int a = 1; a < zone.Entities[i].DrawListB.Rows.Count + 1; a++)
                                                {
                                                    zone.Entities[i].DrawListB.Rows[a - 1].MetaValue = (short)((zone.Entities[i].Positions.Count - 1) - MetaValues[a - 1]);
                                                }
                                            }
                                            if (zone.Entities[i].LoadListA != null)
                                            {
                                                List<short> MetaValues = new List<short>();
                                                for (int a = 0; a < zone.Entities[i].LoadListA.Rows.Count; a++)
                                                {
                                                    MetaValues.Add((short)zone.Entities[i].LoadListA.Rows[a].MetaValue);
                                                }
                                                for (int a = 1; a < zone.Entities[i].LoadListA.Rows.Count + 1; a++)
                                                {
                                                    zone.Entities[i].LoadListA.Rows[a - 1].MetaValue = (short)((zone.Entities[i].Positions.Count - 1) - MetaValues[a - 1]);
                                                }
                                            }
                                            if (zone.Entities[i].LoadListB != null)
                                            {
                                                List<short> MetaValues = new List<short>();
                                                for (int a = 0; a < zone.Entities[i].LoadListB.Rows.Count; a++)
                                                {
                                                    MetaValues.Add((short)zone.Entities[i].LoadListB.Rows[a].MetaValue);
                                                }
                                                for (int a = 1; a < zone.Entities[i].LoadListB.Rows.Count + 1; a++)
                                                {
                                                    zone.Entities[i].LoadListB.Rows[a - 1].MetaValue = (short)((zone.Entities[i].Positions.Count - 1) - MetaValues[a - 1]);
                                                }
                                            }
                                            */
                                        }
                                    }
                                }
                            }

                            //Crutch zone
                            if (level == Crash2_Levels.L21_PistonItAway)
                            {
                                //int entid = (int)(i | (id << 8) | ((zone.Entities.IndexOf(id) - BitConv.FromInt32(zone.Header, 0x188)) << 24));
                                //int entid = id >> 8 & 0xFFFF; // the other way around

                                if (zone.EName == "05_gZ")
                                {
                                    int id = 400;
                                    CreateEntity(id, 34, 5, 854, 1149, 450, ref zone);
                                    AddToDrawList(ref nsf, ref zone, id);
                                }
                            }
                            else if (level == Crash2_Levels.L12_SewerOrLater)
                            {
                                if (zone.EName == "b4_aZ")
                                {
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (zone.Entities[i].ID == 79) // blades
                                        {
                                            zone.Entities[i].Type = 3;
                                            zone.Entities[i].Subtype = 16;
                                            zone.Entities[i].Settings.Clear();
                                        }
                                    }
                                }
                            }
                            /*
                            else if (level == Crash2_Levels.L11_PlantFood)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "28_xZ")
                                {
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (zone.Entities[i].ID == 161 || zone.Entities[i].ID == 162) // plants near the end
                                        {
                                            zone.Entities[i].Type = 3;
                                            zone.Entities[i].Subtype = 16;
                                            zone.Entities[i].Settings.Clear();
                                        }
                                    }
                                }
                                else if (zone.EName == "27_xZ")
                                {
                                    //EntityPosition center = new EntityPosition(685, 350, 420);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(685, 350, 480),
                                        new EntityPosition(685, 350, 420),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "19_xZ")
                                {
                                    //EntityPosition center = new EntityPosition(700, 350, 180);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(700, 350, 180),
                                        new EntityPosition(700, 350, 120),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 2;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "05_xZ")
                                {
                                    //EntityPosition center = new EntityPosition(680, 350, 720);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(680, 350, 780),
                                        new EntityPosition(680, 350, 720),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 4;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }

                            }
                            else if (level == Crash2_Levels.L03_HangEight)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "30_pZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(400, 430, 280),
                                        new EntityPosition(400, 490, 380),
                                        new EntityPosition(400, 550, 480),
                                        new EntityPosition(400, 610, 580),
                                    };

                                    for (int id = 0; id < 4; id++)
                                    {
                                        int entID = id + crutchboxID;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "24_pZ")
                                {
                                    //EntityPosition center = new EntityPosition(635, 400, 620);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(635, 400, 620),
                                        new EntityPosition(635, 400, 570),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 4;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "08_pZ")
                                {
                                    //EntityPosition center = new EntityPosition(625, 300, 580);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(625, 300, 580),
                                        new EntityPosition(625, 300, 530),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 6;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }

                            }
                            else if (level == Crash2_Levels.L07_AirCrash)
                            {
                                int crutchboxID = 500;
                                if (zone.EName == "31_wZ")
                                {

                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(440, 360, 210),
                                        new EntityPosition(440, 420, 310),
                                        new EntityPosition(440, 480, 410),
                                        new EntityPosition(440, 540, 510),
                                    };

                                    
                                    for (int id = 0; id < 1; id++)
                                    {
                                        int entID = id + crutchboxID;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                    
                                }
                                if (zone.EName == "29_wZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(1300, 360, 1370),
                                        new EntityPosition(1300, 420, 1270),
                                        new EntityPosition(1300, 480, 1170),
                                        new EntityPosition(1300, 540, 1070),
                                    };

                                    for (int id = 0; id < 4; id++)
                                    {
                                        int entID = id + crutchboxID + 4;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "15_wZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(370, 360, 1120),
                                        new EntityPosition(370, 420, 1020),
                                        new EntityPosition(370, 480, 920),
                                        new EntityPosition(370, 540, 820),
                                    };

                                    for (int id = 0; id < 4; id++)
                                    {
                                        int entID = id + crutchboxID + 8;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "24_wZ")
                                {
                                    //EntityPosition center = new EntityPosition(720, 350, 80);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(720, 350, 80),
                                        new EntityPosition(720, 350, 0),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 12;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                                else if (zone.EName == "09_wZ")
                                {
                                    //EntityPosition center = new EntityPosition(680, 350, 1050);
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                    {
                                        new EntityPosition(680, 350, 1050),
                                        new EntityPosition(680, 350, 990),
                                    };

                                    for (int id = 0; id < 2; id++)
                                    {
                                        int entID = id + crutchboxID + 14;

                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }

                            }
                            */
                        }
                        else if (entry is SLSTEntry sortlist)
                        {
                            if (BackwardsCameraList.Contains(level))
                            {
                                /*
                                List<SLSTDelta> deltas = new List<SLSTDelta>();
                                for (int a = 0; a < sortlist.Deltas.Count; a++)
                                {
                                    deltas.Add(sortlist.Deltas[a]);
                                }
                                sortlist.Deltas.Clear();
                                for (int a = 0; a < deltas.Count; a++)
                                {
                                    sortlist.Deltas.Add(deltas[a]);
                                }
                                */
                                /* softlock
                                foreach (SLSTDelta delta in sortlist.Deltas)
                                {
                                    List<short> AddNodes = new List<short>();
                                    List<short> RemoveNodes = new List<short>();
                                    for (int a = 0; a < delta.AddNodes.Count; a++)
                                    {
                                        AddNodes.Add(delta.AddNodes[a]);
                                    }
                                    for (int a = 0; a < delta.RemoveNodes.Count; a++)
                                    {
                                        RemoveNodes.Add(delta.RemoveNodes[a]);
                                    }
                                    delta.AddNodes.Clear();
                                    delta.RemoveNodes.Clear();
                                    for (int a = 0; a < RemoveNodes.Count; a++)
                                    {
                                        delta.AddNodes.Add(RemoveNodes[a]);
                                    }
                                    for (int a = 0; a < AddNodes.Count; a++)
                                    {
                                        delta.RemoveNodes.Add(AddNodes[a]);
                                    }
                                }
                                */

                            }
                        }
                    }
                }
            }

            Mod_VehicleLevelsOnFoot(nsf, nsd, level);

            EntityPosition CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
            EntityPosition WarpOutPos = new EntityPosition(WarpOutEntity.Positions[0].X, WarpOutEntity.Positions[0].Y, WarpOutEntity.Positions[0].Z);

            if (level == Crash2_Levels.L12_SewerOrLater) // normal spawn is out of bounds
            {
                WarpOutPos = new EntityPosition(BoxCounterEntity.Positions[0].X, BoxCounterEntity.Positions[0].Y, BoxCounterEntity.Positions[0].Z);
            }

            if (!Debug_DontMoveCrash)
            {
                CrashEntity.Positions.RemoveAt(0);
                WarpOutEntity.Positions.RemoveAt(0);

                CrashEntity.Positions.Add(WarpOutPos);
                WarpOutEntity.Positions.Add(CrashPos);
            }

            if (!Debug_DontMoveCounter)
            {
                BoxCounterEntity.Positions.RemoveAt(0);
                BoxCounterEntity.Positions.Add(CrashPos);
            }

            //saving some chunk space
            CrashEntity.Name = null;
            WarpOutEntity.Name = null;
            BoxCounterEntity.Name = null;

            if (!Debug_DontMoveCrash)
            {
                //ID switch to fix drawlists?
                int tempID = (int)CrashEntity.ID;
                CrashEntity.ID = WarpOutEntity.ID;
                WarpOutEntity.ID = tempID;
            }

            /*
            if (BoardEnts.Count > 0)
            {
                for (int i = 0; i < BoardEnts.Count; i++)
                {
                    EntityPosition LaunchPos = new EntityPosition(BoardEnts[i].Positions[0].X, BoardEnts[i].Positions[0].Y, BoardEnts[i].Positions[0].Z);
                    EntityPosition DropoffPos = new EntityPosition(BoardDropEnts[i].Positions[0].X, BoardDropEnts[i].Positions[0].Y, BoardDropEnts[i].Positions[0].Z);
                    BoardEnts[i].Positions.RemoveAt(0);
                    BoardDropEnts[i].Positions.RemoveAt(0);
                    BoardEnts[i].Positions.Add(DropoffPos);
                    BoardDropEnts[i].Positions.Add(LaunchPos);

                    int tempboardID = (int)BoardEnts[i].ID;
                    BoardEnts[i].ID = BoardDropEnts[i].ID;
                    BoardDropEnts[i].ID = tempboardID;
                }
            }
            */

            if (JetpackEnt != null)
            {
                EntityPosition JetpackPos = new EntityPosition(JetpackEnt.Positions[0].X, JetpackEnt.Positions[0].Y, JetpackEnt.Positions[0].Z);
                EntityPosition SpacepadPos = new EntityPosition(SpacepadEnt.Positions[0].X, SpacepadEnt.Positions[0].Y, SpacepadEnt.Positions[0].Z);
                JetpackEnt.Positions.RemoveAt(0);
                SpacepadEnt.Positions.RemoveAt(0);
                JetpackEnt.Positions.Add(SpacepadPos);
                SpacepadEnt.Positions.Add(JetpackPos);

                int tempboardID = (int)JetpackEnt.ID;
                JetpackEnt.ID = SpacepadEnt.ID;
                SpacepadEnt.ID = tempboardID;
            }

            if (ElevEnt != null)
            {
                List<EntityPosition> Pos1 = new List<EntityPosition>(ElevEnt.Positions);
                List<EntityPosition> Pos2 = new List<EntityPosition>(ElevCatchEnt.Positions);
                Pos1.Reverse();
                Pos2.Reverse();
                ElevEnt.Positions.Clear();
                ElevCatchEnt.Positions.Clear();
                for (int i = 0; i < Pos1.Count; i++)
                {
                    ElevCatchEnt.Positions.Add(Pos1[i]);
                }
                for (int i = 0; i < Pos2.Count; i++)
                {
                    ElevEnt.Positions.Add(Pos2[i]);
                }

                int tempboardID = (int)ElevEnt.ID;
                ElevEnt.ID = ElevCatchEnt.ID;
                ElevCatchEnt.ID = tempboardID;
            }

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
                                if (!Debug_DontMoveCrash)
                                {
                                    zone.Entities.Add(WarpOutEntity);
                                    zone.EntityCount++;
                                }

                                if (!Debug_DontMoveCounter)
                                {
                                    zone.Entities.Add(BoxCounterEntity);
                                    zone.EntityCount++;
                                    AddToDrawList(ref nsf, ref zone, (int)BoxCounterEntity.ID);
                                }
                            }
                            else if (zone.EName == WarpOutZone.EName)
                            {
                                if (!Debug_DontMoveCrash)
                                {
                                    zone.Entities.Add(CrashEntity);
                                    zone.EntityCount++;
                                }
                            }
                            /*
                            if (BoardEnts.Count > 0)
                            {
                                for (int i = 0; i < BoardEnts.Count; i++)
                                {
                                    if (zone.EName == BoardLaunchZones[i].EName)
                                    {
                                        zone.Entities.Add(BoardDropEnts[i]);
                                        zone.EntityCount++;
                                    }
                                    else if (zone.EName == BoardDropoffZones[i].EName)
                                    {
                                        zone.Entities.Add(BoardEnts[i]);
                                        zone.EntityCount++;
                                    }
                                }
                            }
                            */
                            if (JetpackEnt != null)
                            {
                                if (zone.EName == JetpackZone.EName)
                                {
                                    zone.Entities.Add(SpacepadEnt);
                                    zone.EntityCount++;
                                }
                                else if (zone.EName == SpacepadZone.EName)
                                {
                                    zone.Entities.Add(JetpackEnt);
                                    zone.EntityCount++;
                                }
                            }

                            if (level == Crash2_Levels.L02_SnowGo)
                            {
                                if (zone.EName == ElevZone.EName)
                                {
                                    zone.Entities.Add(ElevCatchEnt);
                                    zone.EntityCount++;
                                }
                                else if (zone.EName == ElevCatchZone.EName)
                                {
                                    zone.Entities.Add(ElevEnt);
                                    zone.EntityCount++;
                                }

                            }

                            //Crutch zone 2
                            if (level == Crash2_Levels.L21_PistonItAway)
                            {
                                int id = 400;
                                if (zone.EName == "04_gZ")
                                {
                                    AddToDrawList(ref nsf, ref zone, id);
                                }
                                else if (zone.EName == "06_gZ")
                                {
                                    AddToDrawList(ref nsf, ref zone, id);
                                }
                            }
                                /*
                                else if (level == Crash2_Levels.L11_PlantFood)
                                {
                                    int id = 400;
                                    if (zone.EName == "26_xZ")
                                    {
                                        for (int i = id + 0; i < id + 2; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "28_xZ")
                                    {
                                        for (int i = id + 0; i < id + 2; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "18_xZ")
                                    {
                                        for (int i = id + 2; i < id + 4; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "20_xZ")
                                    {
                                        for (int i = id + 2; i < id + 4; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "04_xZ")
                                    {
                                        for (int i = id + 4; i < id + 6; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "06_xZ")
                                    {
                                        for (int i = id + 4; i < id + 6; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                }
                                else if (level == Crash2_Levels.L03_HangEight)
                                {
                                    int id = 400;
                                    if (zone.EName == "29_pZ")
                                    {
                                        for (int i = id + 0; i < id + 4; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "31_pZ")
                                    {
                                        for (int i = id + 0; i < id + 4; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "23_pZ")
                                    {
                                        for (int i = id + 4; i < id + 6; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "25_pZ")
                                    {
                                        for (int i = id + 4; i < id + 6; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "07_pZ")
                                    {
                                        for (int i = id + 6; i < id + 8; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "09_pZ")
                                    {
                                        for (int i = id + 6; i < id + 8; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                }
                                else if (level == Crash2_Levels.L07_AirCrash)
                                {
                                    int id = 500;
                                    if (zone.EName == "30_wZ")
                                    {

                                        for (int i = id + 4; i < id + 8; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }

                                    }
                                    else if (zone.EName == "28_wZ")
                                    {
                                        for (int i = id + 4; i < id + 8; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "14_wZ")
                                    {
                                        for (int i = id + 8; i < id + 12; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "16_wZ")
                                    {
                                        for (int i = id + 8; i < id + 12; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "25_wZ")
                                    {
                                        for (int i = id + 12; i < id + 14; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "23_wZ")
                                    {
                                        for (int i = id + 12; i < id + 14; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "08_wZ")
                                    {
                                        for (int i = id + 14; i < id + 16; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                    else if (zone.EName == "10_wZ")
                                    {
                                        for (int i = id + 14; i < id + 16; i++)
                                        {
                                            AddToDrawList(ref nsf, ref zone, i);
                                        }
                                    }
                                }
                                */
                            }
                    }
                }
            }

            if (level == Crash2_Levels.L02_SnowGo)
            {

                List<EntityPosition> Pos1 = new List<EntityPosition>(ElevEnt.Positions);
                List<EntityPosition> Pos2 = new List<EntityPosition>(ElevCatchEnt.Positions);
                Pos1.Reverse();
                Pos2.Reverse();
                ElevEnt.Positions.Clear();
                ElevCatchEnt.Positions.Clear();
                for (int i = 0; i < Pos1.Count; i++)
                {
                    ElevCatchEnt.Positions.Add(new EntityPosition(500, (short)(Pos2[i].Y + 190), 500));
                }
                for (int i = 0; i < Pos2.Count; i++)
                {
                    ElevEnt.Positions.Add(new EntityPosition(689, Pos2[i].Y, 555));
                }
                ElevEnt.ID = 400;
                ElevCatchEnt.ID = 401;
                ElevEnt.Settings[2] = new EntitySetting(0, 401);

                foreach (Chunk chunk in nsf.Chunks)
                {
                    if (chunk is NormalChunk zonechunk)
                    {
                        foreach (Entry entry in zonechunk.Entries)
                        {
                            if (entry is ZoneEntry zone)
                            {
                                if (level == Crash2_Levels.L02_SnowGo)
                                {
                                    if (zone.EName == "1n_eZ")
                                    {
                                        zone.Entities.Add(ElevCatchEnt);
                                        zone.EntityCount++;

                                        AddToDrawList(ref nsf, ref zone, 401);
                                    }
                                    else if (zone.EName == "b1_eZ")
                                    {
                                        zone.Entities.Add(ElevEnt);
                                        zone.EntityCount++;

                                        AddToDrawList(ref nsf, ref zone, 400);
                                    }

                                }
                            }
                        }
                    }
                }
            }

            if (!Debug_DontMoveCrash)
            {
                int xoffset = BitConv.FromInt32(WarpOutZone.Layout, 0);
                int yoffset = BitConv.FromInt32(WarpOutZone.Layout, 4);
                int zoffset = BitConv.FromInt32(WarpOutZone.Layout, 8);

                nsd.Spawns[0].ZoneEID = WarpOutZone.EID;
                nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 4) << 8;
                nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 4) << 8;
                nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 4) << 8;
            }
        }

        public static void Mod_RandomizeWarpRoom(NSF nsf, NSD nsd, Crash2_Levels level, Random rand)
        {
            if (level != Crash2_Levels.WarpRoom && level != Crash2_Levels.WarpRoom2 && level != Crash2_Levels.WarpRoom3 && level != Crash2_Levels.WarpRoom4 && level != Crash2_Levels.WarpRoom5)
            {
                return;
            }

            // spawn 0 - load wall
            // spawn 3 - new game, intro & turtle woods spawn
            // spawn 4 - load game spawn

            List<NSDSpawnPoint> OrigSpawns = new List<NSDSpawnPoint>();
            List<NSDSpawnPoint> SpawnsToRand = new List<NSDSpawnPoint>();
            for (int i = 0; i < nsd.Spawns.Count; i++)
            {
                //Console.WriteLine("ID:" + i + " Zone: " + nsd.Spawns[i].ZoneEID);
                OrigSpawns.Add(nsd.Spawns[i]);
                if (i != 4 && i != 0 && i < 27)
                {
                    SpawnsToRand.Add(nsd.Spawns[i]);
                }
            }
            int spawnCount = SpawnsToRand.Count;
            List<NSDSpawnPoint> RandSpawns = new List<NSDSpawnPoint>();
            for (int i = 0; i < OrigSpawns.Count; i++)
            {
                if (i != 4 && i != 0 && i < 27)
                {
                    int r = rand.Next(SpawnsToRand.Count);
                    RandSpawns.Add(SpawnsToRand[r]);
                    SpawnsToRand.RemoveAt(r);
                }
                else
                {
                    RandSpawns.Add(null);
                }
            }
            nsd.Spawns.Clear();
            for (int i = 0; i < OrigSpawns.Count; i++)
            {
                
                if (RandSpawns[i] != null)
                {
                    nsd.Spawns.Add(RandSpawns[i]);
                }
                else
                {
                    if (i == 4)
                    {
                        nsd.Spawns.Add(RandSpawns[3]);
                    }
                    else
                    {
                        nsd.Spawns.Add(OrigSpawns[i]);
                    }
                }
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
            

        }


        public static void Mod_VehicleLevelsOnFoot(NSF nsf, NSD nsd, Crash2_Levels level)
        {
            if (!BearLevelsList.Contains(level) && !BoardLevelsList.Contains(level))
            {
                return;
            }

            //todo: jetpack

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
                                if (zone.Entities[i].Type == 48 && zone.Entities[i].Subtype == 0) // Bear
                                {
                                    zone.Entities.RemoveAt(i);
                                    zone.EntityCount--;
                                    i--;
                                    /*
                                    EntityPosition[] BearPath = new EntityPosition[zone.Entities[i].Positions.Count];
                                    zone.Entities[i].Positions.CopyTo(BearPath, 0);
                                    zone.Entities[i].Positions.Clear();
                                    for (int a = 0; a < BearPath.Length; a++)
                                    {
                                        zone.Entities[i].Positions.Add(BearPath[(BearPath.Length - 1) - a]);
                                    }
                                    */
                                }
                                else if (zone.Entities[i].Type == 34 && zone.Entities[i].Subtype == 4) // Checkpoint fix
                                {
                                    zone.Entities[i].Settings[1] = new EntitySetting(0, 0);
                                }
                                if (zone.Entities.Count > 0 && zone.Entities[i].CameraIndex != null && zone.Entities[i].CameraSubIndex == 0 && zone.Entities[i].Neighbors.RowCount == 1)
                                {
                                    //doesn't do anything

                                    zone.Entities[i].Neighbors.Rows.Insert(0, new EntityPropertyRow<uint>());
                                    zone.Entities[i].Neighbors.Rows[0].MetaValue = 0;
                                    int neighborindex = 0;
                                    int neighborsettingindex = 0;

                                    int camflag = 2;
                                    int camIndex = 0;
                                    int camZone = 2;
                                    int camLink = 1;

                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values.Add(0);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                    zone.Entities[i].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);
                                    
                                }
                            }

                            if (BoardLevelsList.Contains(level))
                            {
                                for (int a = 0x24; a < zone.Layout.Length; a += 2)
                                {
                                    short node = BitConv.FromInt16(zone.Layout, a);
                                    if (node == 0x35)
                                    {
                                        //bear level water => solid
                                        BitConv.ToInt16(zone.Layout, a, 3);
                                    }
                                    else if (node == 0x129)
                                    {
                                        //water level water => solid
                                        BitConv.ToInt16(zone.Layout, a, 3);
                                    }
                                }
                            }

                            if (level == Crash2_Levels.L08_BearIt)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "61_tZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 400, 300),
                                        new EntityPosition(1000, 550, 600),
                                        new EntityPosition(1000, 700, 900),
                                        new EntityPosition(1000, 800, 1200),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L13_BearDown)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "69_yZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 100, 800),
                                        new EntityPosition(1000, 250, 1100),
                                        new EntityPosition(1000, 400, 1400),
                                        new EntityPosition(1000, 550, 1700),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L26_TotallyBear)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "64_BZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        new EntityPosition(1000, 250, 700),
                                        new EntityPosition(1000, 400, 1000),
                                        new EntityPosition(1000, 550, 1300),
                                        new EntityPosition(1000, 700, 1600),
                                        new EntityPosition(1000, 850, 1900),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L15_UnBearable)
                            {
                                int crutchboxID = 400;
                                if (zone.EName == "46_nZ")
                                {
                                    EntityPosition[] crate_pos = new EntityPosition[]
                                     {
                                        //new EntityPosition(870, 150, 450),
                                        new EntityPosition(870, 150, 250),
                                        //new EntityPosition(870, 150, 50),
                                        new EntityPosition(870, 150, -150),
                                     };

                                    for (int id = 0; id < crate_pos.Length; id++)
                                    {
                                        int entID = id + crutchboxID;
                                        CreateEntity(entID, 34, 5, crate_pos[id].X, crate_pos[id].Y, crate_pos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, entID);
                                    }
                                }
                            }

                        }
                    }
                }
            }

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            if (level == Crash2_Levels.L08_BearIt)
                            {
                                int id = 400;
                                if (zone.EName == "60_tZ")
                                {
                                    for (int i = id + 0; i < id + 4; i++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L13_BearDown)
                            {
                                int id = 400;
                                if (zone.EName == "68_yZ")
                                {
                                    for (int i = id + 0; i < id + 4; i++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L26_TotallyBear)
                            {
                                int id = 400;
                                if (zone.EName == "63_BZ")
                                {
                                    for (int i = id + 0; i < id + 5; i++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, i);
                                    }
                                }
                            }
                            else if (level == Crash2_Levels.L15_UnBearable)
                            {
                                int id = 400;
                                if (zone.EName == "45_nZ" || zone.EName == "47_nZ")
                                {
                                    for (int i = id + 0; i < id + 2; i++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, i);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        //omg why is this so convoluted for just the id ;_;
        static int GetDrawListValue(NSF nsf, ZoneEntry thiszone, int id)
        {
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            foreach (Entity otherentity in zone.Entities)
                            {
                                if (otherentity.ID.HasValue && otherentity.ID.Value == id)
                                {
                                    for (int i = 0; i < thiszone.ZoneCount; ++i)
                                    {
                                        if (entry.EID == BitConv.FromInt32(thiszone.Header, 0x194 + i * 4))
                                        {
                                            return (int)(i | (otherentity.ID << 8) | ((zone.Entities.IndexOf(otherentity) - BitConv.FromInt32(zone.Header, 0x188)) << 24));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }

        static void AddToDrawList(ref NSF nsf, ref ZoneEntry zone, int ID)
        {

            int BoxEntID = GetDrawListValue(nsf, zone, ID);

            for (int i = 0; i < zone.Entities.Count; i++)
            {
                if (zone.Entities[i].DrawListB != null)
                {
                    short LowPos = short.MaxValue;
                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListB.Rows[a].MetaValue < LowPos)
                        {
                            LowPos = (short)zone.Entities[i].DrawListB.Rows[a].MetaValue;
                        }
                    }

                    if (LowPos > 0)
                    {
                        zone.Entities[i].DrawListB.Rows.Add(new EntityPropertyRow<int>());
                        zone.Entities[i].DrawListB.Rows[zone.Entities[i].DrawListB.Rows.Count - 1].MetaValue = 0;
                        LowPos = 0;
                    }

                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
                        if (!zone.Entities[i].DrawListB.Rows[a].Values.Contains(BoxEntID) && zone.Entities[i].DrawListB.Rows[a].MetaValue == LowPos)
                        {
                            zone.Entities[i].DrawListB.Rows[a].Values.Add(BoxEntID);
                        }
                    }
                }
                if (zone.Entities[i].DrawListA != null)
                {
                    short MaxPos = -1;
                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListA.Rows[a].MetaValue > MaxPos)
                        {
                            MaxPos = (short)zone.Entities[i].DrawListA.Rows[a].MetaValue;
                        }
                    }

                    if (MaxPos < zone.Entities[i].Positions.Count - 1)
                    {
                        zone.Entities[i].DrawListA.Rows.Add(new EntityPropertyRow<int>());
                        zone.Entities[i].DrawListA.Rows[zone.Entities[i].DrawListA.Rows.Count - 1].MetaValue = (short)(zone.Entities[i].Positions.Count - 1);
                        MaxPos = (short)(zone.Entities[i].Positions.Count - 1);
                    }

                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (!zone.Entities[i].DrawListA.Rows[a].Values.Contains(BoxEntID) && zone.Entities[i].DrawListA.Rows[a].MetaValue == MaxPos)
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Add(BoxEntID);
                        }
                    }
                }
            }
        }

        static void RemoveFromDrawList(ref NSF nsf, ref ZoneEntry zone, int ID)
        {

            int BoxEntID = GetDrawListValue(nsf, zone, ID);

            for (int i = 0; i < zone.Entities.Count; i++)
            {
                if (zone.Entities[i].DrawListB != null)
                {
                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListB.Rows[a].Values.Contains(BoxEntID))
                        {
                            zone.Entities[i].DrawListB.Rows[a].Values.Remove(BoxEntID);
                        }
                    }
                }
                if (zone.Entities[i].DrawListA != null)
                {

                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListA.Rows[a].Values.Contains(BoxEntID))
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Remove(BoxEntID);
                        }
                    }
                }
            }
        }

        static void CreateEntity(int id, int type, int subtype, short x, short y, short z, ref ZoneEntry zone)
        {
            Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
            newentity.ID = id;
            newentity.AlternateID = null;
            newentity.Name = null;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = type;
            newentity.Subtype = subtype;
            newentity.DDASection = null;
            newentity.DDASettings = null;
            newentity.Settings.Clear();
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        public static void Mod_Metadata(NSF nsf, NSD nsd, Crash2_Levels level)
        {
            if (level != Crash2_Levels.WarpRoom && level != Crash2_Levels.WarpRoom2 && level != Crash2_Levels.WarpRoom3 && level != Crash2_Levels.WarpRoom4 && level != Crash2_Levels.WarpRoom5)
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
                            if (gool.EName == "DispC")
                            {
                                if (ModLoaderGlobals.Region == RegionType.NTSC_U)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("RESUME"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i, 17, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            seed,
                                        });
                                        }
                                    }
                                }
                                else if (ModLoaderGlobals.Region == RegionType.PAL)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("RESUME"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i, 45, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            "OPTIONEN",
                                            "OPCIONES",
                                            "OPZIONI",
                                            seed,
                                        });
                                        }
                                    }
                                }
                                else
                                {
                                    /* one word off? NTSC-J broken atm anyway
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("UNKNOWN"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i + 423, 23, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(),
                                            seed,
                                        });
                                        }
                                    }
                                    */
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
