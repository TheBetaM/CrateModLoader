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

        public static List<CrateSubTypes> Crates_ToRemove = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Blank,
            CrateSubTypes.Checkpoint,
        };

        public static void Rand_BoxCount(NSF nsf, Random rand, Crash2_Levels level)
        {
            List<Entity> willys = new List<Entity>();
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is ZoneEntry zone2)
                        {
                            foreach (Entity entity in zone2.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                            }
                        }
                        if (entry is NewZoneEntry zone3)
                        {
                            foreach (Entity entity in zone3.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                            }
                        }
                    }
                }
            }

            Dictionary<Crash2_Levels, int> minBoxCounts = new Dictionary<Crash2_Levels, int>()
            {
                [Crash2_Levels.L02_SnowGo] = 4,
                [Crash2_Levels.L05_CrashDash] = 8,
                [Crash2_Levels.L08_BearIt] = 10,
                [Crash2_Levels.L09_CrashCrush] = 18,
                [Crash2_Levels.L12_SewerOrLater] = 14,
                [Crash2_Levels.L13_BearDown] = 2,
                [Crash2_Levels.L15_UnBearable] = 18,
                [Crash2_Levels.L16_HanginOut] = 16,
                [Crash2_Levels.L18_ColdHardCrash] = 26,
                [Crash2_Levels.L25_SpacedOut] = 2,
                [Crash2_Levels.L26_TotallyBear] = 8,
            };

            foreach (Entity willy in willys)
            {
                if (willy.BoxCount.HasValue)
                {
                    int boxcount = willy.BoxCount.Value.ValueB;
                    if (minBoxCounts.ContainsKey(level))
                    {
                        boxcount = rand.Next(minBoxCounts[level], boxcount + 1);
                    }
                    else
                    {
                        boxcount = rand.Next(0, boxcount + 1);
                    }
                    willy.BoxCount = new EntitySetting(0, boxcount);
                }
            }
        }

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
                                    if ((CrateSubTypes)ent.Subtype == CrateSubTypes.Blank  || (CrateSubTypes)ent.Subtype == CrateSubTypes.Pickup || (CrateSubTypes)ent.Subtype == CrateSubTypes.WoodSpring)
                                    {
                                        int r = rand.Next(Crate_PossibleContents.Count);
                                        ent.Settings[0] = new EntitySetting(0, (int)Crate_PossibleContents[r]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public enum CrateParamFlags
        {
            Unk1 = 0,
            Unk2 = 1,
            Unk3 = 2,
            WaterFloat = 3,
            Outlined = 4,
            Unk4 = 5,
            NonSolid = 6, //can walk into to activate/destroy, can bounce off of it, jetski can't break it though
            Unk5 = 7,
            IceReflection = 8,
            SpaceFloat = 9,
            Unk6 = 10,
            Unk7 = 11,
            Unk8 = 12,
            Unk9 = 13,
            Unk10 = 14,
            Unk11 = 15,
        };

        public static void Mod_RandomCrateParams(NSF nsf, Random rand, Crash2_Levels level)
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
                                    if (ent.Settings.Count > 1)
                                    {
                                        int oldSet = ent.Settings[1].ValueB;

                                        int cratePreset = rand.Next(6);

                                        switch (cratePreset)
                                        {
                                            default:
                                                break;
                                            case 0:
                                                oldSet |= 1 << (int)CrateParamFlags.WaterFloat;
                                                break;
                                            case 1:
                                                oldSet |= 1 << (int)CrateParamFlags.SpaceFloat;
                                                break;
                                            case 2:
                                                oldSet |= 1 << (int)CrateParamFlags.WaterFloat;
                                                oldSet |= 1 << (int)CrateParamFlags.SpaceFloat;
                                                break;
                                        }

                                        ent.Settings[1] = new EntitySetting(0, oldSet);
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
                                        ent.AlternateID = null;
                                        ent.TimeTrialReward = null;
                                        ent.Victims.Clear();
                                        ent.BonusBoxCount = null;
                                        ent.BoxCount = null;
                                        ent.DDASection = null;
                                        ent.DDASettings = null;
                                        ent.ZMod = null;
                                        ent.OtherSettings = null;
                                        ent.Settings.Clear();
                                        ent.ExtraProperties.Clear();
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

        public static void Rand_CratesMissing(NSF nsf, Random rand)
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
                                    if (ent.Subtype != null && Crates_ToRemove.Contains((CrateSubTypes)ent.Subtype) && rand.Next(2) == 0)
                                    {
                                        ent.Type = 3;
                                        ent.Subtype = 16;
                                        ent.AlternateID = null;
                                        ent.TimeTrialReward = null;
                                        ent.Victims.Clear();
                                        ent.BonusBoxCount = null;
                                        ent.BoxCount = null;
                                        ent.DDASection = null;
                                        ent.DDASettings = null;
                                        ent.ZMod = null;
                                        ent.OtherSettings = null;
                                        ent.Settings.Clear();
                                        ent.ExtraProperties.Clear();
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

        public static void Rand_WoodenCrates(NSF nsf, Random rand, Crash2_Levels level)
        {
            List<CrateSubTypes> AvailableTypes = new List<CrateSubTypes>();

            List<CrateSubTypes> PossibleList = new List<CrateSubTypes>()
            {
                CrateSubTypes.Aku,
                CrateSubTypes.Blank,
                CrateSubTypes.Blank2,
                CrateSubTypes.Fruit,
                CrateSubTypes.Life,
                CrateSubTypes.Pickup,
                //CrateSubTypes.WoodSpring,
            };

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
                                if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                                {
                                    if (!AvailableTypes.Contains((CrateSubTypes)ent.Subtype))
                                    {
                                        AvailableTypes.Add((CrateSubTypes)ent.Subtype);
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
                        if (entry is ZoneEntry)
                        {
                            ZoneEntry zone = (ZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                                {
                                    ent.Subtype = (byte)AvailableTypes[rand.Next(AvailableTypes.Count)];
                                }
                            }
                        }
                    }
                }
            }

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
            //Crash2_Levels.L08_BearIt, // todo: camera stitching, bear stuff
            Crash2_Levels.L09_CrashCrush,  // todo: broken box counter
            //Crash2_Levels.L10_TheEelDeal, // todo: 1 section teleport

            Crash2_Levels.L11_PlantFood, // onfoot for now, todo: board stuff - sometimes crashes on death if too far away from spawn (board needs to be in loadlists?)
            Crash2_Levels.L12_SewerOrLater,
            //Crash2_Levels.L13_BearDown, // todo: camera stitching, bear stuff
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

            //Crash2_Levels.L26_TotallyBear, // todo: camera stitching, bear stuff
            Crash2_Levels.L27_TotallyFly,

            Crash2_Levels.B01_RipperRoo,
            Crash2_Levels.B02_KomodoBros,
            //Crash2_Levels.B03_TinyTiger,
            Crash2_Levels.B04_NGin,
            Crash2_Levels.B05_Cortex,
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
        static List<Crash2_Levels> BossLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.B01_RipperRoo,
            Crash2_Levels.B02_KomodoBros,
            Crash2_Levels.B03_TinyTiger,
            Crash2_Levels.B04_NGin,
            Crash2_Levels.B05_Cortex,
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
            if (BossLevelsList.Contains(level))
            {
                Mod_RandomizeBosses(nsf, nsd, level, rand, true);
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
            Entity SpaceLockEnt = null;
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

            /*
            if (BearLevelsList.Contains(level) && level != Crash2_Levels.L15_UnBearable) //temp forward testing
            {
                Debug_DontMoveCounter = true;
                Debug_DontMoveCrash = true;
            }
            */

            if (ChaseLevelsList.Contains(level)) // bugged counter
            {
                Debug_DontMoveCounter = true;
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

                                    /*
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
                                            SpaceLockEnt = zone.Entities[i];
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    */

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

            /*
            if (JetpackEnt != null)
            {
                EntityPosition JetpackPos = new EntityPosition(JetpackEnt.Positions[0].X, JetpackEnt.Positions[0].Y, JetpackEnt.Positions[0].Z);
                EntityPosition SpacepadPos = new EntityPosition(SpacepadEnt.Positions[0].X, SpacepadEnt.Positions[0].Y, SpacepadEnt.Positions[0].Z);
                JetpackEnt.Positions.RemoveAt(0);
                SpacepadEnt.Positions.RemoveAt(0);
                JetpackEnt.Positions.Add(SpacepadPos);
                SpacepadEnt.Positions.Add(JetpackPos);
                SpaceLockEnt.Positions[0] = new EntityPosition(12000, 12000, 12000);

                int tempboardID = (int)JetpackEnt.ID;
                JetpackEnt.ID = SpacepadEnt.ID;
                SpacepadEnt.ID = tempboardID;
            }
            */

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
                            if (zone.EName == WarpOutZone.EName)
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
                            
                            /*
                            if (JetpackEnt != null)
                            {
                                if (zone.EName == JetpackZone.EName)
                                {
                                    zone.Entities.Add(SpacepadEnt);
                                    zone.EntityCount++;
                                    AddToDrawList(ref nsf, ref zone, (int)SpacepadEnt.ID);
                                    RemoveFromDrawList(ref nsf, zone, (int)JetpackEnt.ID);
                                    RemoveFromDrawList(ref nsf, zone, (int)SpaceLockEnt.ID);
                                }
                                else if (zone.EName == SpacepadZone.EName)
                                {
                                    zone.Entities.Add(JetpackEnt);
                                    zone.EntityCount++;
                                    AddToDrawList(ref nsf, ref zone, (int)JetpackEnt.ID);
                                    AddToDrawList(ref nsf, ref zone, (int)SpaceLockEnt.ID);
                                    RemoveFromDrawList(ref nsf, zone, (int)SpacepadEnt.ID);
                                }
                            }
                            */
                            

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

        public static void Mod_RandomizeBosses(NSF nsf, NSD nsd, Crash2_Levels level, Random rand, bool isBackwards)
        {
            if (!BossLevelsList.Contains(level))
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

            foreach (Chunk chunk in nsf.Chunks)
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
                foreach (Chunk chunk in nsf.Chunks)
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

                                    nsd.Spawns[0].ZoneEID = SpawnZone;
                                    nsd.Spawns[0].SpawnX = (xoffset + CrashSpawn.X * 4) << 8;
                                    nsd.Spawns[0].SpawnY = (yoffset + CrashSpawn.Y * 4) << 8;
                                    nsd.Spawns[0].SpawnZ = (zoffset + CrashSpawn.Z * 4) << 8;
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

        static void RemoveFromDrawList(ref NSF nsf, ZoneEntry zone, int ID)
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

        class EntityTypePair
        {
            public int Type;
            public int Subtype;

            public EntityTypePair(int t, int s)
            {
                Type = t;
                Subtype = s;
            }
        }

        static List<EntityTypePair> EnemiesToRemove = new List<EntityTypePair>()
        {
            // Turtle Woods
            new EntityTypePair(20, 1), //armadillo
            new EntityTypePair(2, 0), //spiky turtle
            new EntityTypePair(6, 0), //dive bird
            // Snow Go
            new EntityTypePair(25, 1), //penguin pulse
            new EntityTypePair(25, 0), //penguin
            new EntityTypePair(24, 0), //seal
            new EntityTypePair(24, 1), //seal path
            new EntityTypePair(32, 0), //smasher
            new EntityTypePair(32, 1), //smasher constant
            new EntityTypePair(27, 0), //porcupine
            // Hang Eight
            new EntityTypePair(47, 5), //fish
            new EntityTypePair(47, 4), //mine float
            new EntityTypePair(38, 4), //evil plant
            //new EntityTypePair(47, 8), //whirlpool
            new EntityTypePair(47, 7), //mine path
            // The Pits
            new EntityTypePair(6, 2), //bird 3D
            new EntityTypePair(2, 5), //saw turtle
            // Crash Dash
            new EntityTypePair(39, 0), //mine
            new EntityTypePair(39, 1), //fence
            // Snow Biz
            new EntityTypePair(32, 3), //icicle
            new EntityTypePair(27, 1), //porcupine smart
            new EntityTypePair(32, 2), //roller
            new EntityTypePair(25, 2), //penguin path
            // Bear It
            new EntityTypePair(48, 2), //orca
            // The Eel Deal
            new EntityTypePair(28, 0), //rat
            new EntityTypePair(28, 1), //rat tunnel
            new EntityTypePair(13, 0), //scrubber
            new EntityTypePair(13, 6), //scrubber circle
            new EntityTypePair(12, 1), //fan
            new EntityTypePair(13, 8), //scrubber tunnel ring
            //new EntityTypePair(12, 8), //eel
            new EntityTypePair(12, 9), //barrel tunnel
            new EntityTypePair(21, 0), //mech floater
            new EntityTypePair(21, 3), //mech bob
            // Sewer Or Later
            new EntityTypePair(16, 0), //welder
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(13, 5), //scrubber path
            new EntityTypePair(12, 2), //fan break
            // Bear Down
            new EntityTypePair(48, 5), //lab lift
            // Road To Ruin
            new EntityTypePair(7, 1), //fireface watcher
            new EntityTypePair(28, 2), //possum path
            new EntityTypePair(7, 0), //fireface
            new EntityTypePair(10, 0), //monkey hop
            new EntityTypePair(28, 3), //lizard path
            new EntityTypePair(11, 0), //gorilla boulder
            // UnBearable
            new EntityTypePair(39, 8), //critter
            new EntityTypePair(20, 0), //armadillo
            new EntityTypePair(33, 0), //hunter
            // Hangin Out
            new EntityTypePair(13, 10), //scrubber tunnel
            new EntityTypePair(21, 4), //mech path
            new EntityTypePair(28, 6), //rat tunnel ring
            new EntityTypePair(21, 1), //mech hanger
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(21, 5), //mech path floater
            // Diggin It
            new EntityTypePair(38, 0), //spore plant
            new EntityTypePair(39, 7), //tiki
            new EntityTypePair(39, 4), //timed spark
            new EntityTypePair(39, 6), //timed spark
            //new EntityTypePair(18, 2), //hive
            new EntityTypePair(18, 0), //bee hive
            new EntityTypePair(45, 1), //labjack
            // Piston It Away
            new EntityTypePair(55, 1), //piston
            new EntityTypePair(55, 5), //robot walker
            new EntityTypePair(53, 0), //fred
            new EntityTypePair(55, 2), //pad
            //new EntityTypePair(55, 3), //gun
            //new EntityTypePair(55, 4), //gun down
            //new EntityTypePair(55, 0), //piston up
            new EntityTypePair(56, 0), //pusher
            // Rock It
            new EntityTypePair(35, 11), //space cable
            new EntityTypePair(35, 13), //space gun
            //new EntityTypePair(42, 0), //space lab
            new EntityTypePair(35, 15), //space ring
            // Night Fight
            new EntityTypePair(46, 0), //dragonfly
        };

        public static void Mod_RemoveEnemies(NSF nsf, Random rand, Crash2_Levels level, bool isRandom)
        {
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone && zone.EName != "H2_gZ") // piston it death route
                        {
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Subtype != null)
                                {
                                    foreach (EntityTypePair pair in EnemiesToRemove)
                                    {
                                        if (pair.Type == ent.Type && pair.Subtype == ent.Subtype)
                                        {
                                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                                            {
                                                ent.Type = 3;
                                                ent.Subtype = 16;
                                                ent.AlternateID = null;
                                                ent.TimeTrialReward = null;
                                                ent.Victims.Clear();
                                                ent.BonusBoxCount = null;
                                                ent.BoxCount = null;
                                                ent.DDASection = null;
                                                ent.DDASettings = null;
                                                ent.ZMod = null;
                                                ent.OtherSettings = null;
                                                ent.Settings.Clear();
                                                ent.ExtraProperties.Clear();
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

        static List<EntityTypePair> EnemiesToCrate = new List<EntityTypePair>()
        {
            // Turtle Woods
            new EntityTypePair(20, 1), //armadillo
            new EntityTypePair(2, 0), //spiky turtle
            new EntityTypePair(6, 0), //dive bird
            // Snow Go
            new EntityTypePair(25, 1), //penguin pulse
            new EntityTypePair(25, 0), //penguin
            new EntityTypePair(24, 0), //seal
            new EntityTypePair(24, 1), //seal path
            //new EntityTypePair(32, 0), //smasher
            //new EntityTypePair(32, 1), //smasher constant
            new EntityTypePair(27, 0), //porcupine
            // Hang Eight
            new EntityTypePair(47, 5), //fish
            new EntityTypePair(47, 4), //mine float
            //new EntityTypePair(38, 4), //evil plant
            //new EntityTypePair(47, 8), //whirlpool
            new EntityTypePair(47, 7), //mine path
            // The Pits
            new EntityTypePair(6, 2), //bird 3D
            new EntityTypePair(2, 5), //saw turtle
            // Crash Dash
            new EntityTypePair(39, 0), //mine
            new EntityTypePair(39, 1), //fence
            // Snow Biz
            //new EntityTypePair(32, 3), //icicle
            new EntityTypePair(27, 1), //porcupine smart
            new EntityTypePair(32, 2), //roller
            new EntityTypePair(25, 2), //penguin path
            // Bear It
            //new EntityTypePair(48, 2), //orca
            // The Eel Deal
            new EntityTypePair(28, 0), //rat
            new EntityTypePair(28, 1), //rat tunnel
            new EntityTypePair(13, 0), //scrubber
            new EntityTypePair(13, 6), //scrubber circle
            new EntityTypePair(12, 1), //fan
            new EntityTypePair(13, 8), //scrubber tunnel ring
            //new EntityTypePair(12, 8), //eel
            new EntityTypePair(12, 9), //barrel tunnel
            new EntityTypePair(21, 0), //mech floater
            new EntityTypePair(21, 3), //mech bob
            // Sewer Or Later
            //new EntityTypePair(16, 0), //welder
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(13, 5), //scrubber path
            new EntityTypePair(12, 2), //fan break
            // Bear Down
            new EntityTypePair(48, 5), //lab lift
            // Road To Ruin
            //new EntityTypePair(7, 1), //fireface watcher
            new EntityTypePair(28, 2), //possum path
            //new EntityTypePair(7, 0), //fireface
            new EntityTypePair(10, 0), //monkey hop
            new EntityTypePair(28, 3), //lizard path
            new EntityTypePair(11, 0), //gorilla boulder
            // UnBearable
            new EntityTypePair(39, 8), //critter
            new EntityTypePair(20, 0), //armadillo
            new EntityTypePair(33, 0), //hunter
            // Hangin Out
            new EntityTypePair(13, 10), //scrubber tunnel
            new EntityTypePair(21, 4), //mech path
            new EntityTypePair(28, 6), //rat tunnel ring
            new EntityTypePair(21, 1), //mech hanger
            new EntityTypePair(28, 4), //rat circle
            new EntityTypePair(21, 5), //mech path floater
            // Diggin It
            new EntityTypePair(38, 0), //spore plant
            new EntityTypePair(39, 7), //tiki
            new EntityTypePair(39, 4), //timed spark
            new EntityTypePair(39, 6), //timed spark
            //new EntityTypePair(18, 2), //hive
            new EntityTypePair(18, 0), //bee hive
            new EntityTypePair(45, 1), //labjack
            // Piston It Away
            //new EntityTypePair(55, 1), //piston
            new EntityTypePair(55, 5), //robot walker
            new EntityTypePair(53, 0), //fred
            new EntityTypePair(55, 2), //pad
            //new EntityTypePair(55, 3), //gun
            //new EntityTypePair(55, 4), //gun down
            //new EntityTypePair(55, 0), //piston up
            new EntityTypePair(56, 0), //pusher
            // Rock It
            new EntityTypePair(35, 11), //space cable
            new EntityTypePair(35, 13), //space gun
            //new EntityTypePair(42, 0), //space lab
            new EntityTypePair(35, 15), //space ring
            // Night Fight
            new EntityTypePair(46, 0), //dragonfly
        };

        public static void Mod_EnemyCrates(NSF nsf, Random rand, Crash2_Levels level, bool isRandom)
        {
            List<CrateSubTypes> AvailableTypes = new List<CrateSubTypes>();

            List<CrateSubTypes> PossibleList = new List<CrateSubTypes>()
            {
                CrateSubTypes.Aku,
                CrateSubTypes.Blank,
                CrateSubTypes.Fruit,
                CrateSubTypes.Life,
                CrateSubTypes.Pickup,
                CrateSubTypes.Nitro,
                //CrateSubTypes.Iron,
                //CrateSubTypes.IronSpring,
            };

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
                                if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                                {
                                    if (!AvailableTypes.Contains((CrateSubTypes)ent.Subtype))
                                    {
                                        AvailableTypes.Add((CrateSubTypes)ent.Subtype);
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
                        if (entry is ZoneEntry)
                        {
                            ZoneEntry zone = (ZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                ent.Name = null; // may need the extra space
                                if (ent.Type != null && ent.Subtype != null)
                                {
                                    foreach (EntityTypePair pair in EnemiesToCrate)
                                    {
                                        if (pair.Type == ent.Type && pair.Subtype == ent.Subtype)
                                        {
                                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                                            {
                                                ent.Type = 34;
                                                ent.Subtype = (int)AvailableTypes[rand.Next(AvailableTypes.Count)];
                                                ent.AlternateID = null;
                                                ent.TimeTrialReward = null;
                                                ent.Victims.Clear();
                                                ent.BonusBoxCount = null;
                                                ent.BoxCount = null;
                                                ent.DDASection = null;
                                                ent.DDASettings = null;
                                                ent.ZMod = null;
                                                ent.OtherSettings = null;
                                                ent.Settings.Clear();
                                                ent.Settings.Add(new EntitySetting(0, 0));
                                                ent.Settings.Add(new EntitySetting(0, 0));
                                                ent.Settings.Add(new EntitySetting(0, 0));
                                                ent.ExtraProperties.Clear();
                                                int randPos = rand.Next(ent.Positions.Count / 2);
                                                EntityPosition StartPos = ent.Positions[randPos];
                                                ent.Positions.Clear();
                                                ent.Positions.Add(StartPos);
                                            }
                                        }
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

        public static void Rand_EnemyPaths(NSF nsf, Random rand, Crash2_Levels level)
        {
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
                                if (ent.Type != null && ent.Subtype != null && ent.Positions.Count > 1)
                                {
                                    foreach (EntityTypePair pair in EnemiesToRemove)
                                    {
                                        if (pair.Type == ent.Type && pair.Subtype == ent.Subtype)
                                        {
                                            int startPos = rand.Next(ent.Positions.Count);
                                            bool reversed = false;
                                            if (rand.Next(2) == 0)
                                            {
                                                reversed = true;
                                            }
                                            List<EntityPosition> oldPath = new List<EntityPosition>(ent.Positions);
                                            ent.Positions.Clear();

                                            if (reversed)
                                            {
                                                for (int i = startPos; i > 0; i--)
                                                {
                                                    ent.Positions.Add(oldPath[i]);
                                                }
                                            }
                                            else
                                            {
                                                for (int i = startPos; i < oldPath.Count; i++)
                                                {
                                                    ent.Positions.Add(oldPath[i]);
                                                }
                                            }

                                            if (ent.Positions.Count != oldPath.Count)
                                            {
                                                if (reversed)
                                                {
                                                    for (int i = oldPath.Count - 1; i > startPos; i--)
                                                    {
                                                        ent.Positions.Add(oldPath[i]);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < startPos; i++)
                                                    {
                                                        ent.Positions.Add(oldPath[i]);
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

        public static void Mod_PantsColor(NSF nsf, SceneryColor color)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (ck is EntryChunk eck)
                {
                    foreach (Entry en in eck.Entries)
                    {
                        if (en is ModelEntry)
                        {
                            ModelEntry e = (ModelEntry)en;
                            if (e.EName.StartsWith("Cb") || e.EName.StartsWith("Cr") || e.EName.StartsWith("CR") || e.EName.StartsWith("Ch") || e.EName.StartsWith("CS") || e.EName.StartsWith("WiB"))
                            {
                                // don't paint the back texture and shoes!
                                List<int> TexturedTris = new List<int>();
                                for (int t = 0; t < e.Triangles.Count; ++t)
                                {
                                    if (e.Triangles[t].Tex > 0)
                                    {
                                        for (int i = 0; i < e.Triangles[t].Color.Length; i++)
                                        {
                                            TexturedTris.Add(e.Triangles[t].Color[i]);
                                        }
                                    }
                                }
                                
                                for (int i = 0; i < e.Colors.Count; ++i)
                                {
                                    if (e.Colors[i].Blue > 0 && e.Colors[i].Green < 110 && e.Colors[i].Red < 110 && !TexturedTris.Contains(i))
                                    {
                                        float intensity = e.Colors[i].Blue / 255f;
                                        e.Colors[i] = new SceneryColor((byte)(color.Red * intensity), (byte)(color.Green * intensity), (byte)(color.Blue * intensity), color.Extra);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Mod_Metadata(NSF nsf, NSD nsd, Crash2_Levels level, RegionType region)
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
                                if (region == RegionType.NTSC_U)
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
                                else if (region == RegionType.PAL)
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
                                    /*
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

                                            InsertStringsInByteArray(ref gool.Anims, i + 436, 11, new List<string>() {
                                            //ModLoaderGlobals.ProgramVersion.ToUpper(),
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
