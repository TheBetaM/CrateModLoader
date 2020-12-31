using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash3
{

    public enum Crash3_Levels
    {
        Unknown = -1,
        L01_ToadVillage = 1,
        L02_UnderPressure = 4,
        L03_OrientExpress = 0,
        L04_BoneYard = 2,
        L05_MakinWaves = 15,
        L06_GeeWiz = 5,
        L07_HangEmHigh = 12,
        L08_HogRide = 11,
        L09_TombTime = 8,
        L10_MidnightRun = 7,
        L11_DinoMight = 6,
        L12_DeepTrouble = 18,
        L13_HighTime = 16,
        L14_RoadCrash = 10,
        L15_DoubleHeader = 19,
        L16_Sphynxinator = 20,
        L17_ByeByeBlimps = 9,
        L18_TellNoTales = 3,
        L19_FutureFrenzy = 17,
        L20_TombWader = 14,
        L21_GoneTomorrow = 25,
        L22_OrangeAsphalt = 22,
        L23_FlamingPassion = 24,
        L24_MadBombers = 13,
        L25_BugLite = 26,
        L26_SkiCrazed = 23,
        L27_Area51 = 27,
        L28_RingsOfPower = 21,
        L29_HotCoco = 29,
        L30_EggipusRex = 28,
        B01_TinyTiger = 30,
        B02_Dingodile = 31,
        B03_NTropy = 32,
        B04_NGin = 33,
        B05_Cortex = 34,
        WarpRoom = 35,
    }

    public static class Crash3_Mods
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
            Clock = 28,
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

        public static void Rand_BoxCount(NSF nsf, Random rand, Crash3_Levels level)
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

            Dictionary<Crash3_Levels, int> minBoxCounts = new Dictionary<Crash3_Levels, int>()
            {
                [Crash3_Levels.L02_UnderPressure] = 20,
                [Crash3_Levels.L04_BoneYard] = 27,
                [Crash3_Levels.L06_GeeWiz] = 1,
                [Crash3_Levels.L07_HangEmHigh] = 6,
                [Crash3_Levels.L09_TombTime] = 6,
                [Crash3_Levels.L11_DinoMight] = 22,
                [Crash3_Levels.L12_DeepTrouble] = 10,
                [Crash3_Levels.L13_HighTime] = 4,
                [Crash3_Levels.L16_Sphynxinator] = 20,
                [Crash3_Levels.L17_ByeByeBlimps] = 1,
                [Crash3_Levels.L19_FutureFrenzy] = 8,
                [Crash3_Levels.L21_GoneTomorrow] = 6,
                [Crash3_Levels.L24_MadBombers] = 1,
                [Crash3_Levels.L25_BugLite] = 9,
                [Crash3_Levels.L26_SkiCrazed] = 21,
                [Crash3_Levels.L28_RingsOfPower] = 1,
                [Crash3_Levels.L29_HotCoco] = 18,
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

        public static void Mod_AllWoodCrates(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if ((CrateSubTypes)ent.Subtype == CrateSubTypes.Blank || (CrateSubTypes)ent.Subtype == CrateSubTypes.Pickup || (CrateSubTypes)ent.Subtype == CrateSubTypes.WoodSpring)
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

        public enum CrateParamFlagsA
        {
            Invisible = 0,
            GhostVisible = 1,
            AutoCollect = 2,
            LowZ1 = 3,
            LowZ2 = 4,
            NoGhost = 5,
            GhostBreak = 6,
            NoAbove = 7,
        };

        public enum CrateParamFlagsB
        {
            NoTrigger = 0,
            BonusContinue = 1,
            LowZ3 = 2,
            Wave = 3,
            Ghost = 4,
            NoBottom = 5,
            NoSides = 6,  //can walk into to activate/destroy, can bounce off of it, jetski can't break it though
            NoTop = 7,
            Reflection = 8,
            Space = 9,
            NitroNoHop = 10,
            NoBelow = 11,
            NitroSolid = 12,
            NoHit = 13,
            Unk10 = 14,
            Unk11 = 15,
            Unk12 = 16,
            Unk13 = 17,
            Unk14 = 18,
            Unk15 = 19,
        };

        public static void Mod_RandomCrateParams(NSF nsf, Random rand, Crash3_Levels level)
        {
            List<int> foundFlags = new List<int>();

            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Settings.Count > 1 && ent.Subtype != (int)CrateSubTypes.Slot && ent.Subtype != (int)CrateSubTypes.Clock)
                                    {
                                        byte SetA = ent.Settings[1].ValueA;
                                        int SetB = ent.Settings[1].ValueB;

                                        int cratePreset = rand.Next(6);

                                        switch (cratePreset)
                                        {
                                            default:
                                                break;
                                            case 0:
                                                SetB |= 1 << (int)CrateParamFlagsB.Wave;
                                                break;
                                            case 1:
                                                SetB |= 1 << (int)CrateParamFlagsB.Space;
                                                break;
                                            case 2:
                                                SetB |= 1 << (int)CrateParamFlagsB.NitroNoHop;
                                                break;
                                        }

                                        ent.Settings[1] = new EntitySetting(SetA, SetB);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void Mod_InvisibleCrates(NSF nsf, Random rand, Crash3_Levels level, bool isRandom)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Settings.Count > 1)
                                    {
                                        if (!isRandom || (isRandom && rand.Next(2) == 0))
                                        {
                                            byte SetA = ent.Settings[1].ValueA;
                                            int SetB = ent.Settings[1].ValueB;

                                            SetA |= 1 << (int)CrateParamFlagsA.Invisible;

                                            ent.Settings[1] = new EntitySetting(SetA, SetB);
                                        }
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Subtype != null && (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype) 
                                        || ent.Subtype == (int)CrateSubTypes.Checkpoint || ent.Subtype == (int)CrateSubTypes.Iron))
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
                                        ent.Scaling = 0;
                                        ent.Settings.Clear();
                                        ent.Settings.Add(new EntitySetting(0, 0));
                                        ent.ExtraProperties.Clear();
                                    }
                                }
                                else if (ent.Type != null && ent.Type == 8 && ent.Subtype == 1) // kite
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
                                    ent.Scaling = 0;
                                    ent.Settings.Clear();
                                    ent.Settings.Add(new EntitySetting(0, 0));
                                    ent.ExtraProperties.Clear();
                                }
                                else if (ent.Type != null && ent.Type == 0 && ent.Subtype == 0)
                                {
                                    if (ent.BoxCount != null)
                                    {
                                        ent.BoxCount = new EntitySetting(0, 0);
                                    }
                                    if (ent.BonusBoxCount != null)
                                    {
                                        ent.BonusBoxCount = new EntitySetting(0, 0);
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

        public static List<CrateSubTypes> Crates_ToRemove = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Blank,
            CrateSubTypes.Checkpoint,
        };

        public static void Rand_CratesMissing(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
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
                                        ent.Scaling = 0;
                                        ent.Settings.Clear();
                                        ent.Settings.Add(new EntitySetting(0, 0));
                                        ent.ExtraProperties.Clear();
                                    }
                                }
                                else if (ent.Type != null && ent.Type == 8 && ent.Subtype == 1 && rand.Next(2) == 0) // kite
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
                                    ent.Scaling = 0;
                                    ent.Settings.Clear();
                                    ent.Settings.Add(new EntitySetting(0, 0));
                                    ent.ExtraProperties.Clear();
                                }
                                else if (ent.Type != null && ent.Type == 0 && ent.Subtype == 0)
                                {
                                    if (ent.BoxCount != null)
                                    {
                                        ent.BoxCount = new EntitySetting(0, 0);
                                    }
                                    if (ent.BonusBoxCount != null)
                                    {
                                        ent.BonusBoxCount = new EntitySetting(0, 0);
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
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

        static List<Crash3_Levels> BackwardsLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L01_ToadVillage,
            Crash3_Levels.L02_UnderPressure,
            //Crash3_Levels.L03_OrientExpress, // todo: tiger stuff (onfoot is unstable)
            //Crash3_Levels.L04_BoneYard, // todo: exit has trouble appearing, stability problems?, out of space on PAL and NTSC-J
            //Crash3_Levels.L05_MakinWaves, // todo: warpout/box counter/clock doesn't appear

            Crash3_Levels.L06_GeeWiz, // todo: laggy start, exit problems if clock is there?
            Crash3_Levels.L07_HangEmHigh,
            //Crash3_Levels.L08_HogRide, // todo: vehicle stuff
            Crash3_Levels.L09_TombTime,
            //Crash3_Levels.L10_MidnightRun, // unverified, todo: camera stitching, tiger stuff (onfoot is unstable)

            //Crash3_Levels.L11_DinoMight, // todo: exit has trouble appearing, stability problems?
            Crash3_Levels.L12_DeepTrouble, // exit problems if clock is there?
            Crash3_Levels.L13_HighTime,
            //Crash3_Levels.L14_RoadCrash, // todo: vehicle stuff
            Crash3_Levels.L15_DoubleHeader, // exit problems if clock is there?

            Crash3_Levels.L16_Sphynxinator, // todo: adding clock crashes
            Crash3_Levels.L17_ByeByeBlimps,
            //Crash3_Levels.L18_TellNoTales, // exit having trouble spawning
            Crash3_Levels.L19_FutureFrenzy,
            Crash3_Levels.L20_TombWader, 

            Crash3_Levels.L21_GoneTomorrow, // todo: adding clock crashes
            //Crash3_Levels.L22_OrangeAsphalt, // todo: vehicle stuff
            Crash3_Levels.L23_FlamingPassion,
            Crash3_Levels.L24_MadBombers,
            Crash3_Levels.L25_BugLite,

            //Crash3_Levels.L26_SkiCrazed, // todo: warpout/box counter/clock doesn't appear, spawning randomly doesn't work
            //Crash3_Levels.L27_Area51, // todo: vehicle stuff
            Crash3_Levels.L28_RingsOfPower,
            //Crash3_Levels.L29_HotCoco, // probably not
            //Crash3_Levels.L30_EggipusRex, // warpout doesn't appear

            //Crash3_Levels.B01_TinyTiger,
            //Crash3_Levels.B02_Dingodile,
            //Crash3_Levels.B03_NTropy, // doesn't really work
            //Crash3_Levels.B04_NGin,
            Crash3_Levels.B05_Cortex,

        };

        static List<Crash3_Levels> JetskiLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L05_MakinWaves,
            Crash3_Levels.L18_TellNoTales,
            Crash3_Levels.L26_SkiCrazed,
            Crash3_Levels.L29_HotCoco,
        };

        static List<Crash3_Levels> TigerLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L03_OrientExpress,
            Crash3_Levels.L10_MidnightRun,
        };

        static List<Crash3_Levels> UnderwaterLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L02_UnderPressure,
            Crash3_Levels.L12_DeepTrouble,
        };

        static List<Crash3_Levels> BikeLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L08_HogRide,
            Crash3_Levels.L14_RoadCrash,
            Crash3_Levels.L22_OrangeAsphalt,
            Crash3_Levels.L27_Area51,
        };

        static List<Crash3_Levels> FlyingLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L17_ByeByeBlimps,
            Crash3_Levels.L24_MadBombers,
            Crash3_Levels.L28_RingsOfPower,
        };

        static List<Crash3_Levels> ChaseLevelsList = new List<Crash3_Levels>()
        {
            //Crash3_Levels.L04_BoneYard,
            Crash3_Levels.L11_DinoMight,
        };
        
        static List<Crash3_Levels> BossLevelsList = new List<Crash3_Levels>()
        {
            //Crash3_Levels.B01_TinyTiger,
            //Crash3_Levels.B02_Dingodile,
            //Crash3_Levels.B03_NTropy,
            //Crash3_Levels.B04_NGin,
            Crash3_Levels.B05_Cortex,
        };

        static List<Crash3_Levels> BackwardsCameraList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L01_ToadVillage, // full level
            //Crash3_Levels.L04_BoneYard, // 1 section
            Crash3_Levels.L06_GeeWiz, // full level
            //Crash3_Levels.L07_HangEmHigh, // some sections?
            Crash3_Levels.L09_TombTime, // full level
            //Crash3_Levels.L11_DinoMight, // some sections
            //Crash3_Levels.L13_HighTime, // some sections?
            Crash3_Levels.L15_DoubleHeader, // full level
            Crash3_Levels.L16_Sphynxinator, // full level
            //Crash3_Levels.L19_FutueFrenzy, // 1 section
            Crash3_Levels.L20_TombWader, // full level
            //Crash3_Levels.L21_GoneTomorrow, // 2 sections
            //Crash3_Levels.L23_FlamingPassion, // some sections?
            Crash3_Levels.L25_BugLite, // full level
            //Crash3_Levels.L30_EggipusRex, // not needed

            Crash3_Levels.L03_OrientExpress, // full level
            Crash3_Levels.L10_MidnightRun, // full level

            Crash3_Levels.L08_HogRide, // full level
            Crash3_Levels.L14_RoadCrash, // full level
            Crash3_Levels.L22_OrangeAsphalt, // full level
            Crash3_Levels.L27_Area51, // full level

            //Crash3_Levels.L02_UnderPressure, // not needed
            //Crash3_Levels.L12_DeepTrouble, // not needed
            //Crash3_Levels.L28_RingsOfPower, // not needed
            //Crash3_Levels.L05_MakinWaves, // not needed
            //Crash3_Levels.L18_TellNoTales, // not needed
            //Crash3_Levels.L26_SkiCrazed, // not needed
            //Crash3_Levels.L17_ByeByeBlimps, // not needed
            //Crash3_Levels.L24_MadBombers, // not needed
            //Crash3_Levels.L29_HotCoco, // not needed
            
        };

        public static void Mod_BackwardsLevels(NSF nsf, NewNSD nsd, Crash3_Levels level, bool isRandom, Random rand)
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
            if (FlyingLevelsList.Contains(level))
            {
                Mod_RandomizeFlyingLevels(nsf, nsd, level, rand, true);
                return;
            }

            Entity CrashEntity = null;
            NewZoneEntry CrashZone = null;
            Entity WarpOutEntity = null;
            NewZoneEntry WarpOutZone = null;
            Entity WarpInEntity = null;
            NewZoneEntry WarpInZone = null;
            Entity GemEntity = null;
            NewZoneEntry GemZone = null;

            Entity ClockEntity = null;
            NewZoneEntry ClockZone = null;
            Entity BoxCounterEntity = null;
            NewZoneEntry BoxCounterZone = null;

            EntityPosition bufferPos = new EntityPosition(0,0,0);

            bool IgnoreFirstEnd = false;
            if (level == Crash3_Levels.L12_DeepTrouble || level == Crash3_Levels.L13_HighTime)
            {
                IgnoreFirstEnd = true;
            }
            bool IgnoreFirstCounter = false;
            if (level == Crash3_Levels.L12_DeepTrouble)
            {
                IgnoreFirstCounter = true;
            }
            bool CameraFlip = false;

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {

                                if (i < zone.Entities.Count)
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
                                            /*
                                            EntityVictimProperty SAngles = (EntityVictimProperty)zone.Entities[i].ExtraProperties[305];
                                            for (int a = 0; a < SAngles.Rows.Count; a++)
                                            {
                                                SAngles.Rows[a].Values[0] = new EntityVictim((short)(SAngles.Rows[a].Values[0].VictimID - 1000));
                                                SAngles.Rows[a].Values[1] = new EntityVictim((short)(SAngles.Rows[a].Values[1].VictimID - 1000));
                                                SAngles.Rows[a].Values[2] = new EntityVictim((short)(-SAngles.Rows[a].Values[2].VictimID));
                                            }
                                            zone.Entities[i].ExtraProperties[305] = SAngles;
                                            */
                                        }
                                    }
                                }

                                //save some chunk space
                                zone.Entities[i].Name = null;

                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null && !FlyingLevelsList.Contains(level) && !BikeLevelsList.Contains(level))
                                {

                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        if (!JetskiLevelsList.Contains(level))
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    if (WarpOutEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 8) // subtype 8: warpout, subtype 9: warpin
                                    {
                                        if (IgnoreFirstEnd)
                                        {
                                            IgnoreFirstEnd = false;
                                        }
                                        else
                                        {
                                            WarpOutEntity = zone.Entities[i];
                                            WarpOutZone = zone;
                                            if (!JetskiLevelsList.Contains(level))
                                            {
                                                zone.Entities.RemoveAt(i);
                                                i--;
                                                zone.EntityCount--;
                                            }
                                        }
                                    }
                                    if (WarpInEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 9)
                                    {
                                        WarpInEntity = zone.Entities[i];
                                        WarpInZone = zone;
                                        if (!JetskiLevelsList.Contains(level))
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    if (ClockEntity == null && zone.Entities[i].Type == 34 && zone.Entities[i].Subtype == 28)
                                    {
                                        
                                        ClockEntity = zone.Entities[i];
                                        ClockZone = zone;
                                        if (level != Crash3_Levels.L30_EggipusRex && !JetskiLevelsList.Contains(level))
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                        
                                    }
                                    if (GemEntity == null && zone.Entities[i].Type == 3 && zone.Entities[i].Subtype == 25 && level == Crash3_Levels.L30_EggipusRex)
                                    {
                                        GemEntity = zone.Entities[i];
                                        GemZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
                                        zone.EntityCount--;
                                    }
                                    if (BoxCounterEntity == null && zone.Entities[i].Type == 4 && zone.Entities[i].Subtype == 17)
                                    {
                                        if (IgnoreFirstCounter)
                                        {
                                            IgnoreFirstCounter = false;
                                        }
                                        else
                                        {
                                            BoxCounterEntity = zone.Entities[i];
                                            BoxCounterZone = zone;
                                            if (!JetskiLevelsList.Contains(level))
                                            {
                                                zone.Entities.RemoveAt(i);
                                                i--;
                                                zone.EntityCount--;
                                            }
                                        }
                                    }

                                    /*
                                    if (ChaseLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 17 && (zone.Entities[i].Subtype == 2 || zone.Entities[i].Subtype == 0))
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    */

                                }
                            }

                            if (BikeLevelsList.Contains(level))
                            {

                                for (int e = 0; e < zone.Entities.Count; e++)
                                {
                                    if (zone.Entities[e].CameraSubIndex != null && zone.Entities[e].CameraSubIndex == 1)
                                    {
                                        EntityPosition[] Angles = new EntityPosition[zone.Entities[e].Positions.Count];
                                        zone.Entities[e].Positions.CopyTo(Angles, 0);
                                        for (int a = 0; a < Angles.Length; a++)
                                        {
                                            Angles[a] = new EntityPosition(Angles[a].X, (short)(Angles[a].Y + 4000), Angles[a].Z);
                                        }
                                        zone.Entities[e].Positions.Clear();
                                        for (int a = 0; a < Angles.Length; a++)
                                        {
                                            zone.Entities[e].Positions.Add(Angles[a]);
                                        }
                                        
                                        EntityVictimProperty SAngles = (EntityVictimProperty)zone.Entities[e].ExtraProperties[305];
                                        for (int a = 0; a < SAngles.Rows.Count; a++)
                                        {
                                            SAngles.Rows[a].Values[0] = new EntityVictim((short)(SAngles.Rows[a].Values[0].VictimID + 1000));
                                            SAngles.Rows[a].Values[1] = new EntityVictim((short)(SAngles.Rows[a].Values[1].VictimID + 1000));
                                            //SAngles.Rows[a].Values[2] = new EntityVictim((short)(-SAngles.Rows[a].Values[2].VictimID));
                                        }
                                        zone.Entities[e].ExtraProperties[305] = SAngles;
                                    }

                                    if (zone.Entities[e].Type != null)
                                    {

                                        if (zone.Entities[e].Type == 0 && zone.Entities[e].Subtype == 0) //crash
                                        {
                                            List<EntityPosition> Pos1 = new List<EntityPosition>(zone.Entities[e].Positions);
                                            Pos1.Reverse();
                                            zone.Entities[e].Positions.Clear();
                                            for (int a = 0; a < Pos1.Count; a++)
                                            {
                                                zone.Entities[e].Positions.Add(Pos1[a]);
                                            }

                                            //int xoffset = BitConv.FromInt32(WarpOutZone.Layout, 0);
                                            //int yoffset = BitConv.FromInt32(WarpOutZone.Layout, 4);
                                            //int zoffset = BitConv.FromInt32(WarpOutZone.Layout, 8);

                                            //nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                                            //nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                                            //nsd.Spawns[0].SpawnZ = (zoffset + zone.Entities[e].Positions[0].Z * 1) << 8;

                                            //EntityInt32Property prop = (EntityInt32Property)zone.Entities[e].ExtraProperties[664];
                                            //prop.Rows[0].Values[0] = 1512; //512 orig
                                        }


                                        if (zone.Entities[e].Type == 50 && zone.Entities[e].Subtype == 2) // hotrod rail
                                        {
                                            List<EntityPosition> Pos1 = new List<EntityPosition>(zone.Entities[e].Positions);
                                            Pos1.Reverse();
                                            zone.Entities[e].Positions.Clear();
                                            for (int a = 0; a < Pos1.Count; a++)
                                            {
                                                zone.Entities[e].Positions.Add(Pos1[a]);
                                            }
                                            zone.Entities[e].Settings[0] = new EntitySetting(0, 0);
                                        }
                                        if (zone.Entities[e].Type == 50 && zone.Entities[e].Subtype == 0) //hotrod
                                        {
                                            List<EntityPosition> Pos1 = new List<EntityPosition>(zone.Entities[e].Positions);
                                            Pos1.Reverse();
                                            zone.Entities[e].Positions.Clear();
                                            for (int a = 0; a < Pos1.Count; a++)
                                            {
                                                zone.Entities[e].Positions.Add(Pos1[a]);
                                            }
                                        }

                                    }

                                }

                                /*
                                if (zone.EName == "00_kZ")
                                {
                                    EntityPosition TargetSpawn = new EntityPosition(750, 100, 100);

                                    int xoffset = BitConv.FromInt32(zone.Layout, 0);
                                    int yoffset = BitConv.FromInt32(zone.Layout, 4);
                                    int zoffset = BitConv.FromInt32(zone.Layout, 8);

                                    nsd.Spawns[0].SpawnX = (xoffset + TargetSpawn.X * 3) << 8;
                                    nsd.Spawns[0].SpawnY = (yoffset + TargetSpawn.Y * 3) << 8;
                                    nsd.Spawns[0].SpawnZ = (zoffset + TargetSpawn.Z * 3) << 8;

                                    zone.Entities[0].DrawListA.Rows[1].Values.Clear();

                                    AddToDrawList(ref nsf, ref zone, 117); //warpin
                                }
                                */


                            }

                            if (level == Crash3_Levels.L21_GoneTomorrow)
                            {

                                if (zone.EName == "15_zZ")
                                {
                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {

                                        if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 0 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;

                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 1;
                                            int camZone = 0;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                            zone.Entities[e].Neighbors.Rows.Reverse();

                                            EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                            vic.Rows[0].Values[0] = new EntityVictim(1); // enables the connection

                                            // 2. elevator stuff
                                            EntityUInt32Property prop3 = new EntityUInt32Property();
                                            prop3.Rows.Add(new EntityPropertyRow<uint>());
                                            prop3.Rows[0].MetaValue = 0;
                                            prop3.Rows[0].Values.Add(134414336);
                                            prop3.Rows[0].Values.Add(16);
                                            prop3.Rows[0].Values.Add(0);
                                            prop3.Rows[0].Values.Add(0);

                                            zone.Entities[e].ExtraProperties.Add(424, prop3);

                                        }
                                        else if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 1 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;

                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 0;
                                            int camZone = 2;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                            zone.Entities[e].Neighbors.Rows.Reverse();

                                            EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                            vic.Rows[0].Values[0] = new EntityVictim(1); // enables the connection

                                        }

                                    }
                                }

                                if (zone.EName == "27_zZ")
                                {
                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {

                                        if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 1 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;

                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 0;
                                            int camZone = 0;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                            zone.Entities[e].Neighbors.Rows.Reverse();

                                            EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                            vic.Rows[0].Values[0] = new EntityVictim(1); // enables the connection

                                        }
                                        else if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 0 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                            vic.Rows[1].Values[0] = new EntityVictim(1); // enables the connection
                                        }
                                    }
                                }
                                if (zone.EName == "27_zZ" || zone.EName == "13_zZ")
                                {
                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {
                                        if (zone.Entities[e].Type != null && zone.Entities[e].Type == 9 && zone.Entities[e].Subtype == 6)
                                        {
                                            EntityPosition[] PlatPath = new EntityPosition[zone.Entities[e].Positions.Count];
                                            zone.Entities[e].Positions.CopyTo(PlatPath, 0);
                                            zone.Entities[e].Positions.Clear();
                                            for (int a = 0; a < PlatPath.Length; a++)
                                            {
                                                zone.Entities[e].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                                            }
                                        }
                                    }
                                }
                                if (zone.EName == "28_zZ")
                                {

                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {
                                        
                                        if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 0 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            //zone.Entities[e].CameraIndex = 2;
                                            /*
                                            if (zone.Entities[e].Neighbors != null)
                                            {
                                                int camIndex = 1;
                                                int camZone = 0;
                                                zone.Entities[e].Neighbors.Rows[0].Values[0] &= 0xFFFF00FF;
                                                zone.Entities[e].Neighbors.Rows[0].Values[0] |= (uint)((byte)camIndex << 8);
                                                zone.Entities[e].Neighbors.Rows[0].Values[0] &= 0xFF00FFFF;
                                                zone.Entities[e].Neighbors.Rows[0].Values[0] |= (uint)((byte)camZone << 8);
                                            }
                                            */

                                            // 1. must have proper neighbors
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;

                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 2;
                                            int camZone = 0;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                            zone.Entities[e].Neighbors.Rows.Reverse();
                                           
                                            //EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                            //vic.Rows[0].Values[0] = new EntityVictim(1); // does nothing?
                                            //EntityUInt32Property prop2 = (EntityUInt32Property)zone.Entities[e].ExtraProperties[354];
                                            //prop2.Rows[0].Values[0] = 55041917; // does nothing?

                                            // 2. elevator stuff
                                            EntityUInt32Property prop3 = new EntityUInt32Property();
                                            prop3.Rows.Add(new EntityPropertyRow<uint>());
                                            prop3.Rows[0].MetaValue = 0;
                                            prop3.Rows[0].Values.Add(134414336);
                                            prop3.Rows[0].Values.Add(16);
                                            prop3.Rows[0].Values.Add(0);
                                            prop3.Rows[0].Values.Add(0);

                                            zone.Entities[e].ExtraProperties.Add(424, prop3);

                                            /*
                                            Console.WriteLine("BCam ID: " + e + " Index: " + zone.Entities[e].CameraIndex + " Subindex: " + zone.Entities[e].CameraSubIndex);

                                            foreach (KeyValuePair<short, EntityProperty> pair in zone.Entities[e].ExtraProperties)
                                            {
                                                if (pair.Value is EntityInt32Property p1)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Int32 RowCount: " + p1.RowCount + " ");
                                                    foreach (EntityPropertyRow<int> row in p1.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (int v in row.Values)
                                                        {
                                                            Console.Write(" Int32 " + v);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUInt32Property p2)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " UInt32 RowCount: " + p2.RowCount + " ");
                                                    foreach (EntityPropertyRow<uint> row in p2.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (uint v in row.Values)
                                                        {
                                                            Console.Write(" UInt32 " + v);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntitySettingProperty p3)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Setting RowCount: " + p3.RowCount + " ");
                                                    foreach (EntityPropertyRow<EntitySetting> row in p3.Rows)
                                                    {
                                                        //Console.Write("Row " + row.MetaValue);
                                                        foreach (EntitySetting v in row.Values)
                                                        {
                                                            //Console.Write(" EntSetting A " + v.ValueA + " B " + v.ValueB + " Int " + v.ValueInt);
                                                        }
                                                        //Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityVictimProperty p4)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " RowCount: " + p4.RowCount + " ");
                                                    foreach (EntityPropertyRow<EntityVictim> row in p4.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (EntityVictim v in row.Values)
                                                        {
                                                            Console.Write(" Victim " + v.VictimID);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUInt8Property p5)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " UInt8 RowCount: " + p5.RowCount + " ");
                                                    foreach (EntityPropertyRow<byte> row in p5.Rows)
                                                    {
                                                        //Console.Write("Row " + row.MetaValue + " ValCount " + row.Values.Count);
                                                        foreach (byte v in row.Values)
                                                        {
                                                            //Console.Write(" UInt8 " + v);
                                                        }
                                                        //Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUnknownProperty p6)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Unk RowCount: " + p6.RowCount + " ");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Type: " + pair.Value);
                                                }
                                            }

                                            Console.WriteLine(" ");
                                            */

                                        }
                                    }
                                }

                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                for (int e = 0; e < zone.Entities.Count; e++)
                                {
                                    if (zone.Entities[e].Type != null && zone.Entities[e].Type == 10 && zone.Entities[e].Subtype == 0)
                                    {
                                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[e].Positions.Count];
                                        zone.Entities[e].Positions.CopyTo(PlatPath, 0);
                                        zone.Entities[e].Positions.Clear();
                                        if (zone.Entities[e].ID == 10)
                                        {
                                            for (int a = 0; a < PlatPath.Length; a++)
                                            {
                                                zone.Entities[e].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                                            }
                                        }
                                        else
                                        {
                                            for (int a = 21; a < PlatPath.Length; a++)
                                            {
                                                zone.Entities[e].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                                            }
                                        }
                                    }
                                }

                                if (zone.EName == "46_aZ")
                                {
                                    List<EntityPosition> EntPos = new List<EntityPosition>()
                                    {
                                        new EntityPosition(3500, 200, -600),
                                        new EntityPosition(3500, 800, 400),
                                        new EntityPosition(3500, 1400, 1400),
                                        new EntityPosition(3500, 2000, 2400),
                                        new EntityPosition(3500, 2600, 3400),
                                        new EntityPosition(3500, 3200, 4400),
                                    };

                                    for (int id = 0; id < EntPos.Count; id++)
                                    {
                                        int EntID = id + 500;
                                        CreateEntity(EntID, 34, 2, EntPos[id].X, EntPos[id].Y, EntPos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, EntID);
                                    }
                                }
                            }
                            else if (level == Crash3_Levels.L10_MidnightRun)
                            {
                                for (int e = 0; e < zone.Entities.Count; e++)
                                {
                                    if (zone.Entities[e].Type != null && zone.Entities[e].Type == 10 && zone.Entities[e].Subtype == 0)
                                    {
                                        EntityPosition[] PlatPath = new EntityPosition[zone.Entities[e].Positions.Count];
                                        zone.Entities[e].Positions.CopyTo(PlatPath, 0);
                                        zone.Entities[e].Positions.Clear();
                                        for (int a = 0; a < PlatPath.Length; a++)
                                        {
                                            zone.Entities[e].Positions.Add(PlatPath[(PlatPath.Length - 1) - a]);
                                        }
                                    }
                                    /*
                                    if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 0 && zone.Entities[e].CameraSubIndex == 0)
                                    {
                                        if (zone.Entities[e].Neighbors.RowCount == 1 && zone.Entities[e].Neighbors.Rows[0].Values.Count < 2)
                                        {
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;
                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 0;
                                            int camZone = 0;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);
                                        }
                                        
                                    }
                                    */
                                }

                                if (zone.EName == "48_hZ")
                                {

                                    List<EntityPosition> EntPos = new List<EntityPosition>()
                                    {
                                        new EntityPosition(3500,  800, 3400),
                                        new EntityPosition(3500, 1400, 4400),
                                        new EntityPosition(3500, 2000, 5400),
                                        new EntityPosition(3500, 2600, 6400),
                                        new EntityPosition(3500, 3200, 7400),
                                        new EntityPosition(3500, 4000, 8400),
                                    };

                                    for (int id = 0; id < EntPos.Count; id++)
                                    {
                                        int EntID = id + 500;
                                        CreateEntity(EntID, 34, 2, EntPos[id].X, EntPos[id].Y, EntPos[id].Z, ref zone);
                                        AddToDrawList(ref nsf, ref zone, EntID);
                                    }
                                }
                                if (zone.EName != "49_hZ" && zone.EName != "00_hZ")
                                {

                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {
                                        if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraSubIndex == 0 && zone.Entities[e].Neighbors != null && zone.Entities[e].Neighbors.RowCount == 1)
                                        {
                                            
                                            zone.Entities[e].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                            zone.Entities[e].Neighbors.Rows[zone.Entities[e].Neighbors.RowCount - 1].MetaValue = 0;

                                            int neighborindex = zone.Entities[e].Neighbors.RowCount - 1;
                                            int neighborsettingindex = 0;

                                            int camflag = 2;
                                            int camIndex = 0;
                                            int camZone = 2;
                                            int camLink = 1;

                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values.Add(0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                            zone.Entities[e].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);

                                            zone.Entities[e].Neighbors.Rows.Reverse();

                                            if (zone.Entities[e].ExtraProperties.ContainsKey(374))
                                            {
                                                EntityVictimProperty vic = (EntityVictimProperty)zone.Entities[e].ExtraProperties[374];
                                                vic.Rows[0].Values[0] = new EntityVictim(1);
                                            }
                                            
                                            /*
                                            Console.WriteLine(zone.EName + " zone count:" + zone.ZoneCount);
                                            Console.WriteLine(zone.EName + " Cam ID: " + e + " Index: " + zone.Entities[e].CameraIndex + " Subindex: " + zone.Entities[e].CameraSubIndex);

                                            foreach (KeyValuePair<short, EntityProperty> pair in zone.Entities[e].ExtraProperties)
                                            {
                                                if (pair.Value is EntityInt32Property p1)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Int32 RowCount: " + p1.RowCount + " ");
                                                    foreach (EntityPropertyRow<int> row in p1.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (int v in row.Values)
                                                        {
                                                            Console.Write(" Int32 " + v);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUInt32Property p2)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " UInt32 RowCount: " + p2.RowCount + " ");
                                                    foreach (EntityPropertyRow<uint> row in p2.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (uint v in row.Values)
                                                        {
                                                            Console.Write(" UInt32 " + v);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntitySettingProperty p3)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Setting RowCount: " + p3.RowCount + " ");
                                                    foreach (EntityPropertyRow<EntitySetting> row in p3.Rows)
                                                    {
                                                        //Console.Write("Row " + row.MetaValue);
                                                        foreach (EntitySetting v in row.Values)
                                                        {
                                                            //Console.Write(" EntSetting A " + v.ValueA + " B " + v.ValueB + " Int " + v.ValueInt);
                                                        }
                                                        //Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityVictimProperty p4)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " RowCount: " + p4.RowCount + " ");
                                                    foreach (EntityPropertyRow<EntityVictim> row in p4.Rows)
                                                    {
                                                        Console.Write("Row " + row.MetaValue);
                                                        foreach (EntityVictim v in row.Values)
                                                        {
                                                            Console.Write(" Victim " + v.VictimID);
                                                        }
                                                        Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUInt8Property p5)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " UInt8 RowCount: " + p5.RowCount + " ");
                                                    foreach (EntityPropertyRow<byte> row in p5.Rows)
                                                    {
                                                        //Console.Write("Row " + row.MetaValue + " ValCount " + row.Values.Count);
                                                        foreach (byte v in row.Values)
                                                        {
                                                            //Console.Write(" UInt8 " + v);
                                                        }
                                                        //Console.WriteLine(" ");
                                                    }
                                                }
                                                else if (pair.Value is EntityUnknownProperty p6)
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Unk RowCount: " + p6.RowCount + " ");
                                                    Console.Write("Data: ");
                                                    foreach (byte b in p6.Data)
                                                    {
                                                        Console.Write(b.ToString());
                                                    }
                                                    Console.WriteLine(" ");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Type: " + pair.Key + " Type: " + pair.Value);
                                                }
                                            }

                                            Console.WriteLine(" ");
                                            */
                                            

                                        }
                                    }
                                }
                            }

                            if (level == Crash3_Levels.L11_DinoMight)
                            {
                                if (zone.EName == "43_gZ")
                                {
                                    int id = 500;
                                    int id1 = 501;
                                    CreateEntity(id,  34, 5, 2600, 2000, 2500, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 2950, 1350, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "32_gZ")
                                {
                                    int id = 502;
                                    int id1 = 503;
                                    CreateEntity(id, 34, 5, 2600, 1000, 600, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 1950, -100, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L04_BoneYard)
                            {
                                if (zone.EName == "01_cZ")
                                {

                                    int id = 504;
                                    int id1 = 505;
                                    CreateEntity(id, 34, 5, 2900, 1200, 3000, ref zone);
                                    CreateEntity(id1, 34, 5, 2900, 2200, 2550, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "06_cZ")
                                {

                                    int id = 506;
                                    int id1 = 507;
                                    CreateEntity(id, 34, 5, 2600, 1100, 2000, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 1900, 1350, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "14_cZ")
                                {

                                    int id = 509;
                                    int id1 = 510;
                                    CreateEntity(id, 34, 5, 1800, 1150, 700, ref zone);
                                    CreateEntity(id1, 34, 5, 1800, 1950, 0, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "34_cZ")
                                {

                                    int id = 511;
                                    int id1 = 512;
                                    CreateEntity(id, 34, 5, 2600, 1100, 500, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 1800,  0, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "44_cZ")
                                {

                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {
                                        if (zone.Entities[e].CameraIndex != null && zone.Entities[e].CameraIndex == 0 && zone.Entities[e].CameraSubIndex == 0)
                                        {
                                            zone.Entities[e].Neighbors.Rows.RemoveAt(1);
                                        }
                                    }

                                    nsd.Spawns[0].Camera = 0;

                                    int id = 500;
                                    int id1 = 501;
                                    CreateEntity(id, 34,  5, 2400, 1400, 1500, ref zone);
                                    CreateEntity(id1, 34, 5, 2400, 2400, 950, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L26_SkiCrazed)
                            {
                                if (zone.EName == "03_xZ")
                                {
                                    for (int e = 0; e < zone.Entities.Count; e++)
                                    {
                                        if (zone.Entities[e].ID != null && (zone.Entities[e].ID == 190 || zone.Entities[e].ID == 171 || zone.Entities[e].ID == 189))
                                        {
                                            zone.Entities[e].Positions[0] = new EntityPosition((short)(zone.Entities[e].Positions[0].X + 2000), zone.Entities[e].Positions[0].Y, zone.Entities[e].Positions[0].Z);
                                        }
                                    }
                                }
                            }

                            if (level == Crash3_Levels.L02_UnderPressure)
                            {
                                if (zone.EName == "12_eZ") // move the sub
                                {
                                    int pos = zone.Entities.Count - 1;
                                    zone.Entities[pos].Positions[0] = new EntityPosition(zone.Entities[pos].Positions[0].X, 3800, zone.Entities[pos].Positions[0].Z);
                                }
                            }
                            else if (level == Crash3_Levels.L09_TombTime)
                            {
                                if (zone.EName == "b0_iZ") // move the ! box
                                {
                                    int pos = zone.Entities.Count - 4;
                                    zone.Entities[pos].Positions[0] = new EntityPosition(zone.Entities[pos].Positions[0].X, 2150, -12500);
                                }
                            }
                        }
                    }
                }
            }

            EntityPosition CrashPos = new EntityPosition();
            EntityPosition WarpInPos = new EntityPosition();
            EntityPosition WarpOutPos = new EntityPosition();
            bool FlipCrashAndWarpOut = false;

            if (WarpOutEntity != null)
            {
                CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
                WarpOutPos = new EntityPosition(WarpOutEntity.Positions[0].X, WarpOutEntity.Positions[0].Y, WarpOutEntity.Positions[0].Z);
                if (WarpInEntity != null)
                {
                    WarpInPos = new EntityPosition(WarpInEntity.Positions[0].X, WarpInEntity.Positions[0].Y, WarpInEntity.Positions[0].Z);
                }
                CrashEntity.Positions.RemoveAt(0);
                WarpOutEntity.Positions.RemoveAt(0);
                WarpInEntity.Positions.RemoveAt(0);
            }

            //sometimes the spawn is too low or too far
            if (level == Crash3_Levels.L01_ToadVillage)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 2200), (short)(WarpOutPos.Z + 2000));
            }
            else if (level == Crash3_Levels.L03_OrientExpress)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 2600), (short)(WarpOutPos.Z + 2000));
                CrashEntity.BoxCount = new EntitySetting(0, CrashEntity.BoxCount.Value.ValueB + 6); //crutch boxes
            }
            else if (level == Crash3_Levels.L07_HangEmHigh)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 1500));
            }
            else if (level == Crash3_Levels.L10_MidnightRun)
            {
                CrashEntity.BoxCount = new EntitySetting(0, CrashEntity.BoxCount.Value.ValueB + 6); //crutch boxes
            }
            else if (level == Crash3_Levels.L15_DoubleHeader)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 2000));
            }
            else if (level == Crash3_Levels.L16_Sphynxinator)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 3000));
                CrashPos = new EntityPosition(CrashPos.X, CrashPos.Y, (short)(CrashPos.Z + 5900));
            }
            else if (level == Crash3_Levels.L21_GoneTomorrow)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, WarpOutPos.Y, (short)(WarpOutPos.Z + 1500));
            }
            else if (level == Crash3_Levels.L23_FlamingPassion)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 2000));
            }
            else if (level == Crash3_Levels.L30_EggipusRex)
            {
                CrashPos = new EntityPosition((short)(CrashPos.X - 2000), CrashPos.Y, CrashPos.Z);
                ClockEntity.Positions[0] = new EntityPosition((short)(ClockEntity.Positions[0].X + 2000), ClockEntity.Positions[0].Y, ClockEntity.Positions[0].Z);
            }
            if (level == Crash3_Levels.L09_TombTime || level == Crash3_Levels.L30_EggipusRex || level == Crash3_Levels.L18_TellNoTales) //|| level == Crash3_Levels.L05_MakinWaves) // || level == Crash3_Levels.L04_BoneYard)
            {
                FlipCrashAndWarpOut = true;
            }

            if (WarpOutEntity != null)
            {
                CrashEntity.Positions.Add(WarpOutPos);
                WarpOutEntity.Positions.Add(CrashPos);
                WarpInEntity.Positions.Add(WarpOutPos);
            }

            if (ClockEntity != null && BoxCounterEntity != null)
            {
                
                int tempclockID = (int)ClockEntity.ID;
                int tempcounterID = (int)BoxCounterEntity.ID;
                int tempclockScale = (int)ClockEntity.Scaling;
                int tempcounterScale = (int)BoxCounterEntity.Scaling;
                if (level == Crash3_Levels.L09_TombTime)
                {
                    ClockEntity.ID = tempcounterID;
                    BoxCounterEntity.ID = tempclockID;
                }
                ClockEntity.Scaling = tempcounterScale;
                BoxCounterEntity.Scaling = tempclockScale;

                ClockEntity.Settings[0] = new EntitySetting(0, 0);
                ClockEntity.Settings[1] = new EntitySetting(0, 0);
                ClockEntity.Settings[2] = new EntitySetting(0, 0);

                ClockEntity.Name = null;
                BoxCounterEntity.Name = null;

                EntityPosition ClockPos = new EntityPosition(ClockEntity.Positions[0].X, ClockEntity.Positions[0].Y, ClockEntity.Positions[0].Z);
                EntityPosition BoxCountPos = new EntityPosition(BoxCounterEntity.Positions[0].X, BoxCounterEntity.Positions[0].Y, BoxCounterEntity.Positions[0].Z);
                ClockEntity.Positions.RemoveAt(0);
                BoxCounterEntity.Positions.RemoveAt(0);

                if (level == Crash3_Levels.L23_FlamingPassion)
                {
                    BoxCountPos = new EntityPosition((short)(BoxCountPos.X + 300), BoxCountPos.Y, (short)(BoxCountPos.Z + 1600));
                }
                else if (level == Crash3_Levels.L15_DoubleHeader)
                {
                    BoxCountPos = new EntityPosition((short)(BoxCountPos.X + 1000), BoxCountPos.Y, (short)(BoxCountPos.Z + 4200));
                }
                else if (level == Crash3_Levels.L06_GeeWiz)
                {
                    BoxCountPos = new EntityPosition(BoxCountPos.X, BoxCountPos.Y, (short)(BoxCountPos.Z + 500));
                }
                else if (level == Crash3_Levels.L21_GoneTomorrow)
                {
                    //BoxCountPos = new EntityPosition(7500, 1500, 100);
                }

                ClockEntity.Positions.Add(BoxCountPos);
                BoxCounterEntity.Positions.Add(ClockPos);
            }
            if (level == Crash3_Levels.L30_EggipusRex)
            {
                int tempclockID = (int)ClockEntity.ID;
                int tempcounterID = (int)GemEntity.ID;
                int tempclockScale = (int)ClockEntity.Scaling;
                int tempcounterScale = (int)GemEntity.Scaling;
                ClockEntity.ID = tempcounterID;
                GemEntity.ID = tempclockID;
                ClockEntity.Scaling = tempcounterScale;
                GemEntity.Scaling = tempclockScale;

                ClockEntity.Settings[0] = new EntitySetting(0, 0);
                ClockEntity.Settings[1] = new EntitySetting(0, 0);
                ClockEntity.Settings[2] = new EntitySetting(0, 0);

                ClockEntity.Name = null;
                GemEntity.Name = null;

                EntityPosition ClockPos = new EntityPosition(ClockEntity.Positions[0].X, ClockEntity.Positions[0].Y, ClockEntity.Positions[0].Z);
                EntityPosition GemPos = new EntityPosition(GemEntity.Positions[0].X, GemEntity.Positions[0].Y, GemEntity.Positions[0].Z);
                ClockEntity.Positions.RemoveAt(0);
                GemEntity.Positions.RemoveAt(0);
                ClockEntity.Positions.Add(GemPos);
                GemEntity.Positions.Add(ClockPos);
            }

            if (WarpOutEntity != null)
            {
                int CrashScale = (int)CrashEntity.Scaling;
                int WarpInScale = (int)WarpInEntity.Scaling;
                int WarpOutScale = (int)WarpOutEntity.Scaling;

                CrashEntity.Scaling = WarpOutScale;
                WarpInEntity.Scaling = WarpOutScale;
                WarpOutEntity.Scaling = CrashScale;
                if (FlipCrashAndWarpOut)
                {
                    int tempID = (int)CrashEntity.ID;
                    CrashEntity.ID = WarpOutEntity.ID;
                    WarpOutEntity.ID = tempID;
                }
                else
                {
                    if (level != Crash3_Levels.L03_OrientExpress && level != Crash3_Levels.L10_MidnightRun && level != Crash3_Levels.L05_MakinWaves && level != Crash3_Levels.L04_BoneYard)
                    {
                        int tempID1 = (int)WarpInEntity.ID;
                        WarpInEntity.ID = WarpOutEntity.ID;
                        WarpOutEntity.ID = tempID1;
                    }
                }

                CrashEntity.Name = null;
                WarpOutEntity.Name = null;
                WarpInEntity.Name = null;

                //settings are not needed to function, they're mostly delays and pushes before the first spawn
                WarpInEntity.Settings.Clear();
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 0));
                WarpInEntity.Settings.Add(new EntitySetting(0, 32));

                nsd.Spawns[0].ZoneEID = WarpOutZone.EID;
            }

            bool Spawned_WarpOut = false;
            bool Spawned_Crash = false;
            bool Spawned_Counter = false;
            bool Spawned_Clock = false;

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            if (CrashEntity != null && WarpOutEntity != null && zone.EName == CrashZone.EName && !Spawned_WarpOut)
                            {
                                if (level == Crash3_Levels.L02_UnderPressure || level == Crash3_Levels.L30_EggipusRex)
                                {
                                    AddToDrawList(ref nsf, ref zone, (int)WarpOutEntity.ID);
                                }
                                if (JetskiLevelsList.Contains(level))
                                {
                                    if (level == Crash3_Levels.L18_TellNoTales)
                                    {
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 0);
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 1);
                                        RemoveFromDrawLists(ref nsf, zone, (int)BoxCounterEntity.ID);
                                        //AddToDrawListOneCam(ref nsf, zone, (int)BoxCounterEntity.ID, 0); // makes warpout not appear during first visit...
                                        //AddToDrawListOneCam(ref nsf, zone, (int)BoxCounterEntity.ID, 1);
                                        //RemoveFromDrawListsOneCam(ref nsf, zone, (int)WarpInEntity.ID, 0);
                                        //RemoveFromDrawListsOneCam(ref nsf, zone, (int)WarpInEntity.ID, 1);
                                        //RemoveFromDrawListsOneCam(ref nsf, zone, (int)CrashEntity.ID, 0);
                                        //RemoveFromDrawListsOneCam(ref nsf, zone, (int)CrashEntity.ID, 1);
                                        Spawned_WarpOut = true;
                                    }
                                    else if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 0); //todo
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 1); //todo
                                        Spawned_WarpOut = true;
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        //AddToDrawList(ref nsf, ref zone, (int)WarpOutEntity.ID); //nothing
                                        //AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 6); //todo
                                        //AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 7); //todo
                                        //Spawned_WarpOut = true;
                                    }
                                }
                                else
                                {
                                    zone.Entities.Add(WarpOutEntity);
                                    zone.EntityCount++;
                                    Spawned_WarpOut = true;
                                }
                            }
                            if (WarpOutEntity != null && CrashEntity != null && zone.EName == WarpOutZone.EName && !Spawned_Crash)
                            {
                                if (!JetskiLevelsList.Contains(level))
                                {
                                    zone.Entities.Add(CrashEntity);
                                    zone.Entities.Add(WarpInEntity);
                                    zone.EntityCount++;
                                    zone.EntityCount++;
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpOutEntity.ID);
                                    if (BoxCounterEntity != null)
                                    {
                                        RemoveFromDrawLists(ref nsf, zone, (int)BoxCounterEntity.ID);
                                    }
                                }
                                if (FlipCrashAndWarpOut)
                                {
                                    if (level == Crash3_Levels.L18_TellNoTales)
                                    {
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpInEntity.ID, 8);
                                    }
                                    else
                                    {
                                        AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);
                                    }
                                }
                                else
                                {
                                    if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpInEntity.ID, 9);
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        // randomly doesn't work
                                        AddToDrawListOneCam(ref nsf, zone, (int)CrashEntity.ID, 5); 
                                        AddToDrawListOneCam(ref nsf, zone, (int)WarpInEntity.ID, 5);
                                    }
                                    else
                                    {
                                        if (level == Crash3_Levels.L11_DinoMight || level == Crash3_Levels.L04_BoneYard)
                                        {
                                            AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);
                                        }
                                        AddToDrawList(ref nsf, ref zone, (int)CrashEntity.ID);
                                    }
                                }
                                
                                Spawned_Crash = true;
                            }
                            if (CrashEntity != null && WarpOutEntity != null && WarpInEntity != null && zone.EName == WarpInZone.EName && !Spawned_WarpOut)
                            {
                                if (level == Crash3_Levels.L05_MakinWaves)
                                {
                                    //AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 0); //todo (or maybe in crashzone?)
                                    //AddToDrawListOneCam(ref nsf, zone, (int)WarpOutEntity.ID, 1); //todo
                                    //Spawned_WarpOut = true;
                                }
                                if (!JetskiLevelsList.Contains(level))
                                {
                                    RemoveFromDrawLists(ref nsf, zone, (int)ClockEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpInEntity.ID);
                                }
                            }
                            if (ClockZone != null && BoxCounterEntity != null && ClockZone.EName == zone.EName && !Spawned_Counter)
                            {
                                if (!JetskiLevelsList.Contains(level))
                                {
                                    RemoveFromDrawLists(ref nsf, zone, (int)ClockEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpInEntity.ID);
                                }
                                if (!JetskiLevelsList.Contains(level) && level != Crash3_Levels.L20_TombWader)
                                {
                                    zone.Entities.Add(BoxCounterEntity);
                                    zone.EntityCount++;
                                    Spawned_Counter = true;
                                }
                                else
                                {
                                    if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        //AddToDrawListOneCam(ref nsf, zone, (int)BoxCounterEntity.ID, 0); //todo
                                        //AddToDrawList(ref nsf, ref zone, (int)BoxCounterEntity.ID);
                                        //Spawned_Counter = true;
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        //AddToDrawListOneCam(ref nsf, zone, (int)BoxCounterEntity.ID, 0); //todo
                                        //Spawned_Counter = true;
                                    }
                                }
                            }
                            if (ClockZone != null && BoxCounterEntity != null && BoxCounterZone.EName == zone.EName && !Spawned_Clock)
                            {

                                if (level == Crash3_Levels.L18_TellNoTales)
                                {
                                    AddToDrawListOneCam(ref nsf, zone, (int)ClockEntity.ID, 8);
                                    Spawned_Clock = true;
                                }
                                else if (level == Crash3_Levels.L05_MakinWaves)
                                {
                                    //AddToDrawListOneCam(ref nsf, zone, (int)ClockEntity.ID, 8);
                                    //AddToDrawListOneCam(ref nsf, zone, (int)ClockEntity.ID, 9);
                                    Spawned_Clock = true;
                                }
                                else if (level == Crash3_Levels.L26_SkiCrazed)
                                {
                                    //AddToDrawListOneCam(ref nsf, zone, (int)ClockEntity.ID, 5);
                                    Spawned_Clock = true;
                                }
                                else 
                                {
                                    zone.Entities.Add(ClockEntity);
                                    zone.EntityCount++;
                                    if (level != Crash3_Levels.L16_Sphynxinator && level != Crash3_Levels.L21_GoneTomorrow) //clock crashes when added to the drawlist
                                    {
                                        AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                                    }
                                    if (BoxCounterEntity != null)
                                    {
                                        RemoveFromDrawLists(ref nsf, zone, (int)BoxCounterEntity.ID);
                                    }
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpOutEntity.ID);
                                    Spawned_Clock = true;
                                }

                                
                            }
                            if (ClockZone != null && GemZone != null && level == Crash3_Levels.L30_EggipusRex && !Spawned_Counter && zone.EName == ClockZone.EName)
                            {
                                zone.Entities.Add(GemEntity);
                                zone.EntityCount++;
                                //AddToDrawList(ref nsf, ref zone, (int)WarpOutEntity.ID);
                                AddToDrawList(ref nsf, ref zone, (int)GemEntity.ID); 
                                Spawned_Counter = true;
                            }
                            if (ClockZone != null && GemZone != null && level == Crash3_Levels.L30_EggipusRex && !Spawned_Clock && zone.EName == GemZone.EName)
                            {
                                zone.Entities.Add(ClockEntity);
                                zone.EntityCount++;
                                AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                                Spawned_Clock = true;
                            }

                            if (level == Crash3_Levels.L20_TombWader && !Spawned_Counter)
                            {
                                if (zone.EName == "02_oZ")
                                {
                                    BoxCounterEntity.Positions[0] = new EntityPosition(2000, 2500, 500);
                                    zone.Entities.Add(BoxCounterEntity);
                                    zone.EntityCount++;
                                    AddToDrawList(ref nsf, ref zone, (int)BoxCounterEntity.ID);
                                    Spawned_Counter = true;
                                }
                            }

                            if (level == Crash3_Levels.L25_BugLite)
                            {
                                if (zone.EName == "43_AZ")
                                {
                                    int id = 500;
                                    CreateEntityFireFly(id, 5412, 1283, -5299, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                }
                            }
                            else if (level == Crash3_Levels.L11_DinoMight)
                            {
                                if (zone.EName == "44_gZ" || zone.EName == "42_gZ")
                                {
                                    int id = 500;
                                    int id1 = 501;

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L04_BoneYard)
                            {
                                if (zone.EName == "43_cZ")
                                {
                                    int id = 500;
                                    int id1 = 501;

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                                else if (zone.EName == "01_cZ" || zone.EName == "02_cZ")
                                {
                                    RemoveFromDrawLists(ref nsf, zone, (int)ClockEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpInEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)CrashEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)BoxCounterEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, 376);
                                    RemoveFromDrawLists(ref nsf, zone, 283);
                                    RemoveFromDrawLists(ref nsf, zone, 366);
                                    RemoveFromDrawLists(ref nsf, zone, 379);
                                    RemoveFromDrawLists(ref nsf, zone, 380);
                                    RemoveFromDrawLists(ref nsf, zone, 382);
                                    RemoveFromDrawLists(ref nsf, zone, 367);
                                    RemoveFromDrawLists(ref nsf, zone, 368);
                                    RemoveFromDrawLists(ref nsf, zone, 369);
                                    RemoveFromDrawLists(ref nsf, zone, 370);
                                    RemoveFromDrawLists(ref nsf, zone, 371);
                                    RemoveFromDrawLists(ref nsf, zone, 372);
                                    RemoveFromDrawLists(ref nsf, zone, 353);
                                    RemoveFromDrawLists(ref nsf, zone, 354);
                                    RemoveFromDrawLists(ref nsf, zone, 355);
                                    RemoveFromDrawLists(ref nsf, zone, 356);
                                    AddToDrawList(ref nsf, ref zone, (int)WarpOutEntity.ID);
                                }
                                else if (zone.EName == "03_cZ")
                                {
                                    RemoveFromDrawLists(ref nsf, zone, (int)ClockEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)WarpInEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)CrashEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, (int)BoxCounterEntity.ID);
                                    RemoveFromDrawLists(ref nsf, zone, 376);
                                    RemoveFromDrawLists(ref nsf, zone, 283);
                                    RemoveFromDrawLists(ref nsf, zone, 366);
                                    RemoveFromDrawLists(ref nsf, zone, 379);
                                    RemoveFromDrawLists(ref nsf, zone, 380);
                                    RemoveFromDrawLists(ref nsf, zone, 382);
                                    RemoveFromDrawLists(ref nsf, zone, 367);
                                    RemoveFromDrawLists(ref nsf, zone, 368);
                                    RemoveFromDrawLists(ref nsf, zone, 369);
                                    RemoveFromDrawLists(ref nsf, zone, 370);
                                    RemoveFromDrawLists(ref nsf, zone, 371);
                                    RemoveFromDrawLists(ref nsf, zone, 372);
                                    RemoveFromDrawLists(ref nsf, zone, 353);
                                    RemoveFromDrawLists(ref nsf, zone, 354);
                                    RemoveFromDrawLists(ref nsf, zone, 355);
                                    RemoveFromDrawLists(ref nsf, zone, 356);
                                }
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                if (zone.EName == "47_aZ" || zone.EName == "45_aZ")
                                {
                                    for (int id = 500; id < 506; id++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, id);
                                    }
                                    AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);
                                }
                            }
                            else if (level == Crash3_Levels.L10_MidnightRun)
                            {
                                if (zone.EName == "47_hZ" || zone.EName == "49_hZ")
                                {
                                    for (int id = 500; id < 506; id++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, id);
                                    }
                                    AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);
                                }
                            }

                            //Clock zone
                            if (level == Crash3_Levels.L01_ToadVillage)
                            {
                                if (zone.EName == "26_bZ")
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L02_UnderPressure)
                            {
                                if (zone.EName == "29_eZ")
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                if (zone.EName == "47_aZ")
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L07_HangEmHigh)
                            {
                                if (zone.EName == "26_mZ")
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L15_DoubleHeader)
                            {
                                if (zone.EName == "35_tZ")
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L06_GeeWiz) //lags
                            {
                                if (zone.EName == "36_fZ")
                                {
                                    AddToDrawList(ref nsf, ref zone, (int)ClockEntity.ID);
                                    // removing swallups, butterflies due to lag
                                    RemoveFromDrawLists(ref nsf, zone, 185);
                                    RemoveFromDrawLists(ref nsf, zone, 192);
                                    RemoveFromDrawLists(ref nsf, zone, 193);
                                }
                                else if (zone.EName == "35_fZ")
                                {
                                    RemoveFromDrawLists(ref nsf, zone, 185);
                                    RemoveFromDrawLists(ref nsf, zone, 192);
                                    RemoveFromDrawLists(ref nsf, zone, 193);
                                }
                            }
                            

                        }
                    }
                }
            }

            if (WarpOutEntity != null)
            {
                int xoffset = BitConv.FromInt32(WarpOutZone.Layout, 0);
                int yoffset = BitConv.FromInt32(WarpOutZone.Layout, 4);
                int zoffset = BitConv.FromInt32(WarpOutZone.Layout, 8);

                if (level == Crash3_Levels.L05_MakinWaves)
                {
                    nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                    nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                    nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 1) << 8;
                }
                else if (JetskiLevelsList.Contains(level))
                {
                    nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 2) << 8;
                    nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 2) << 8;
                    nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 2) << 8;
                }
                else
                {
                    nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                    nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                    nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 1) << 8;
                }
            }

        }

        static Dictionary<Crash3_Levels, int> LevelValues = new Dictionary<Crash3_Levels, int>()
        {
            //[Crash3_Levels.WarpRoom] = 0x02,
            [Crash3_Levels.B02_Dingodile] = 0x03,
            [Crash3_Levels.B03_NTropy] = 0x04,
            [Crash3_Levels.B04_NGin] = 0x05,
            [Crash3_Levels.B01_TinyTiger] = 0x06,
            [Crash3_Levels.B05_Cortex] = 0x07,
            [Crash3_Levels.L03_OrientExpress] = 0x0A,
            [Crash3_Levels.L01_ToadVillage] = 0x0B,
            [Crash3_Levels.L04_BoneYard] = 0x0C,
            [Crash3_Levels.L18_TellNoTales] = 0x0D,
            [Crash3_Levels.L02_UnderPressure] = 0x0E,
            [Crash3_Levels.L06_GeeWiz] = 0x0F,
            [Crash3_Levels.L11_DinoMight] = 0x10,
            [Crash3_Levels.L10_MidnightRun] = 0x11,
            [Crash3_Levels.L09_TombTime] = 0x12,
            [Crash3_Levels.L17_ByeByeBlimps] = 0x13,
            [Crash3_Levels.L14_RoadCrash] = 0x14,
            [Crash3_Levels.L08_HogRide] = 0x15,
            [Crash3_Levels.L07_HangEmHigh] = 0x16,
            [Crash3_Levels.L24_MadBombers] = 0x17,
            [Crash3_Levels.L20_TombWader] = 0x18,
            [Crash3_Levels.L05_MakinWaves] = 0x19,
            [Crash3_Levels.L13_HighTime] = 0x1A,
            [Crash3_Levels.L19_FutureFrenzy] = 0x1B,
            [Crash3_Levels.L12_DeepTrouble] = 0x1C,
            [Crash3_Levels.L15_DoubleHeader] = 0x1D,
            [Crash3_Levels.L16_Sphynxinator] = 0x1E,
            [Crash3_Levels.L28_RingsOfPower] = 0x1F,
            [Crash3_Levels.L22_OrangeAsphalt] = 0x20,
            [Crash3_Levels.L26_SkiCrazed] = 0x21,
            [Crash3_Levels.L23_FlamingPassion] = 0x22,
            [Crash3_Levels.L21_GoneTomorrow] = 0x23,
            [Crash3_Levels.L25_BugLite] = 0x24,
            [Crash3_Levels.L27_Area51] = 0x25,
            [Crash3_Levels.L30_EggipusRex] = 0x26,
            [Crash3_Levels.L29_HotCoco] = 0x27,
        };
        static Dictionary<int, Crash3_Levels> ValuesToLevel = new Dictionary<int, Crash3_Levels>()
        {
            //[Crash3_Levels.WarpRoom] = 0x02,
            [0x03] = Crash3_Levels.B02_Dingodile,
            [0x04] = Crash3_Levels.B03_NTropy,
            [0x05] = Crash3_Levels.B04_NGin,
            [0x06] = Crash3_Levels.B01_TinyTiger,
            [0x07] = Crash3_Levels.B05_Cortex,
            [0x0A] = Crash3_Levels.L03_OrientExpress,
            [0x0B] = Crash3_Levels.L01_ToadVillage,
            [0x0C] = Crash3_Levels.L04_BoneYard,
            [0x0D] = Crash3_Levels.L18_TellNoTales,
            [0x0E] = Crash3_Levels.L02_UnderPressure,
            [0x0F] = Crash3_Levels.L06_GeeWiz,
            [0x10] = Crash3_Levels.L11_DinoMight,
            [0x11] = Crash3_Levels.L10_MidnightRun,
            [0x12] = Crash3_Levels.L09_TombTime,
            [0x13] = Crash3_Levels.L17_ByeByeBlimps,
            [0x14] = Crash3_Levels.L14_RoadCrash,
            [0x15] = Crash3_Levels.L08_HogRide,
            [0x16] = Crash3_Levels.L07_HangEmHigh,
            [0x17] = Crash3_Levels.L24_MadBombers,
            [0x18] = Crash3_Levels.L20_TombWader,
            [0x19] = Crash3_Levels.L05_MakinWaves,
            [0x1A] = Crash3_Levels.L13_HighTime,
            [0x1B] = Crash3_Levels.L19_FutureFrenzy,
            [0x1C] = Crash3_Levels.L12_DeepTrouble,
            [0x1D] = Crash3_Levels.L15_DoubleHeader,
            [0x1E] = Crash3_Levels.L16_Sphynxinator,
            [0x1F] = Crash3_Levels.L28_RingsOfPower,
            [0x20] = Crash3_Levels.L22_OrangeAsphalt,
            [0x21] = Crash3_Levels.L26_SkiCrazed,
            [0x22] = Crash3_Levels.L23_FlamingPassion,
            [0x23] = Crash3_Levels.L21_GoneTomorrow,
            [0x24] = Crash3_Levels.L25_BugLite,
            [0x25] = Crash3_Levels.L27_Area51,
            [0x26] = Crash3_Levels.L30_EggipusRex,
            [0x27] = Crash3_Levels.L29_HotCoco,
        };
        static List<Crash3_Levels> AllLevels = new List<Crash3_Levels>()
        {
            //Crash3_Levels.B02_Dingodile,
            //Crash3_Levels.B03_NTropy,
            //Crash3_Levels.B04_NGin,
            //Crash3_Levels.B01_TinyTiger,
            //Crash3_Levels.B05_Cortex,
            Crash3_Levels.L03_OrientExpress,
            Crash3_Levels.L01_ToadVillage,
            Crash3_Levels.L04_BoneYard,
            Crash3_Levels.L18_TellNoTales,
            Crash3_Levels.L02_UnderPressure,
            Crash3_Levels.L06_GeeWiz,
            Crash3_Levels.L11_DinoMight,
            Crash3_Levels.L10_MidnightRun,
            Crash3_Levels.L09_TombTime,
            Crash3_Levels.L17_ByeByeBlimps,
            Crash3_Levels.L14_RoadCrash,
            Crash3_Levels.L08_HogRide,
            //Crash3_Levels.L07_HangEmHigh,
            Crash3_Levels.L24_MadBombers,
            Crash3_Levels.L20_TombWader,
            Crash3_Levels.L05_MakinWaves,
            Crash3_Levels.L13_HighTime,
            //Crash3_Levels.L19_FutureFrenzy,
            Crash3_Levels.L12_DeepTrouble,
            Crash3_Levels.L15_DoubleHeader,
            Crash3_Levels.L16_Sphynxinator,
            //Crash3_Levels.L28_RingsOfPower,
            Crash3_Levels.L22_OrangeAsphalt,
            //Crash3_Levels.L26_SkiCrazed,
            Crash3_Levels.L23_FlamingPassion,
            Crash3_Levels.L21_GoneTomorrow,
            Crash3_Levels.L25_BugLite,
            //Crash3_Levels.L27_Area51,
            //Crash3_Levels.L30_EggipusRex,
            //Crash3_Levels.L29_HotCoco,
        };

        public static void Mod_RandomizeWarpRoom(NSF nsf, NewNSD nsd, Crash3_Levels level, Random rand)
        {
            if (level != Crash3_Levels.WarpRoom)
            {
                return;
            }

            int LevelCount = 35;

            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < LevelCount; i ++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < LevelCount; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Add(LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }

            List<Crash3_Levels> LevelsReplace = new List<Crash3_Levels>(AllLevels);
            List<Crash3_Levels> LevelsRandom = new List<Crash3_Levels>();
            for (int i = 0; i < AllLevels.Count; i++)
            {
                int r = rand.Next(LevelsReplace.Count);
                LevelsRandom.Add(LevelsReplace[r]);
                LevelsReplace.RemoveAt(r);
            }

            List<int> OrigValues = new List<int>();
            List<int> OrigValues_Names = new List<int>();

            int CortexID = 30;
            /*
            GOOLEntry warp = nsf.GetEntry<GOOLEntry>("WStOC");
            if (warp != null)
            {
                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        OrigValues.Add(warp.Instructions[4 + (i * 3)].Value);
                    }
                }

                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        if (i > CortexID)
                        {
                            warp.Instructions[4 + (i * 3)].Value = OrigValues[LevelsRand[i - 1]];
                        }
                        else
                        {
                            warp.Instructions[4 + (i * 3)].Value = OrigValues[LevelsRand[i]];
                        }
                    }
                }
            }
            */

            

            Dictionary<int, Crash3_Levels> SubtypeToLevel = new Dictionary<int, Crash3_Levels>();
            Dictionary<int, Crash3_Levels> SubtypeToLevelRand = new Dictionary<int, Crash3_Levels>();

            // Button Level
            foreach (NewZoneEntry zone in nsf.GetEntries<NewZoneEntry>())
            {
                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                    {
                        if (zone.Entities[i].Type == 73 && (int)zone.Entities[i].Subtype < 35) // button
                        {

                            int origSet = zone.Entities[i].Settings[0].ValueB;
                            Crash3_Levels origLevel = ValuesToLevel[origSet];
                            SubtypeToLevel.Add((int)zone.Entities[i].Subtype, origLevel);

                            if (AllLevels.Contains(origLevel))
                            {
                                int targetPos = AllLevels.IndexOf(origLevel);
                                Crash3_Levels targetLevel = LevelsRandom[targetPos];
                                int targetSet = LevelValues[targetLevel];
                                zone.Entities[i].Settings[0] = new EntitySetting(0, targetSet);
                                SubtypeToLevelRand.Add((int)zone.Entities[i].Subtype, targetLevel);
                            }
                            else
                            {
                                SubtypeToLevelRand.Add((int)zone.Entities[i].Subtype, Crash3_Levels.Unknown);
                            }

                            //zone.Entities[i].Subtype = LevelsRand[(int)zone.Entities[i].Subtype]; // buttons don't appear if it's not the right warp room
                        }
                    }
                }
            }

            // Button Visuals
            
            List<int> OrigValues_ButtonVis1 = new List<int>();
            List<int> OrigValues_ButtonVis2 = new List<int>();
            List<int> OrigValues_ButtonVis3 = new List<int>();
            List<int> OrigValues_ButtonVis4 = new List<int>();

            GOOLEntry warp2 = nsf.GetEntry<GOOLEntry>("ButOC");
            if (warp2 != null)
            {
                for (int i = 0; i < LevelCount; i++)
                {
                    OrigValues_ButtonVis1.Add(warp2.Instructions[11 + (i * 8)].Value);
                    OrigValues_ButtonVis2.Add(warp2.Instructions[12 + (i * 8)].Value);
                    OrigValues_ButtonVis3.Add(warp2.Instructions[13 + (i * 8)].Value);
                    OrigValues_ButtonVis4.Add(warp2.Instructions[14 + (i * 8)].Value);
                }

                foreach (KeyValuePair<int, Crash3_Levels> pair in SubtypeToLevel)
                {
                    if (pair.Value != SubtypeToLevelRand[pair.Key] && SubtypeToLevelRand[pair.Key] != Crash3_Levels.Unknown)
                    {
                        int targetPos = 0;
                        foreach (KeyValuePair<int, Crash3_Levels> pair2 in SubtypeToLevelRand)
                        {
                            if (pair2.Value == pair.Value)
                            {
                                targetPos = pair2.Key;
                            }
                        }

                        warp2.Instructions[11 + (targetPos * 8)].Value = OrigValues_ButtonVis1[pair.Key];
                        warp2.Instructions[12 + (targetPos * 8)].Value = OrigValues_ButtonVis2[pair.Key];
                        warp2.Instructions[13 + (targetPos * 8)].Value = OrigValues_ButtonVis3[pair.Key];
                        warp2.Instructions[14 + (targetPos * 8)].Value = OrigValues_ButtonVis4[pair.Key];
                    }
                }
            }
            

        }

        public static void Mod_RemoveBarriers(NSF nsf, Crash3_Levels level)
        {
            if (level != Crash3_Levels.WarpRoom)
            {
                return;
            }

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (zone.Entities[i].Type == 26 && zone.Entities[i].Subtype == 2) //barrier
                                    {
                                        zone.Entities[i].Positions.Clear();
                                        zone.Entities[i].Positions.Add(new EntityPosition(-12000, -12000, -12000));
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public static void Rand_WoodenCrates(NSF nsf, Random rand, Crash3_Levels level)
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
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
            // Toad Village
            new EntityTypePair(11, 1), //goat
            new EntityTypePair(11, 2), //knight
            new EntityTypePair(11, 6), //log
            new EntityTypePair(11, 7), //fence
            new EntityTypePair(11, 0), //frog
            // Under Pressure
            new EntityTypePair(2, 1), //shark
            new EntityTypePair(23, 0), //mine
            new EntityTypePair(2, 2), //eel
            new EntityTypePair(2, 0), //pufferfish
            new EntityTypePair(2, 3), //paddle
            // Orient Express
            new EntityTypePair(8, 4), //stone assistant
            //new EntityTypePair(7, 0), //assistant
            new EntityTypePair(6, 0), //dragon
            new EntityTypePair(8, 0), //barrel
            // Bone Yard
            new EntityTypePair(13, 0), //grass
            new EntityTypePair(16, 2), //tery
            new EntityTypePair(19, 0), //swamp assistant
            // Makin Waves
            new EntityTypePair(42, 0), //jet mine
            new EntityTypePair(46, 0), //boat guy
            new EntityTypePair(42, 1), //jet mine circle
            new EntityTypePair(15, 16), //ship
            new EntityTypePair(15, 18), //ship
            // Gee Wiz
            new EntityTypePair(25, 0), //wizard
            // Hang Em High
            new EntityTypePair(61, 0), //guard
            new EntityTypePair(59, 0), //carpet guy
            new EntityTypePair(58, 0), //scorpion
            new EntityTypePair(65, 0), //vase guy
            // Tomb Time
            new EntityTypePair(37, 2), //cobra
            new EntityTypePair(37, 0), //croc
            new EntityTypePair(41, 0), //flamer
            //new EntityTypePair(39, 0), //vase
            new EntityTypePair(38, 0), //switch guy
            new EntityTypePair(37, 4), //spear
            // Midnight Run
            new EntityTypePair(31, 0), //runner
            // Dino Might
            new EntityTypePair(21, 0), //crash fish
            // Deep Trouble
            new EntityTypePair(70, 0), //laser
            // High Time
            new EntityTypePair(74, 0), //thrower
            new EntityTypePair(60, 0), //knife guy
            // Double Header
            new EntityTypePair(81, 0), //giant
            // Sphynxinator
            new EntityTypePair(57, 0), //sarcophagus
            // Tell No Tales
            new EntityTypePair(49, 0), //crows nest
            new EntityTypePair(47, 0), //shark
            // Future Frenzy
            new EntityTypePair(77, 6), //future fence
            new EntityTypePair(79, 0), //jumper
            new EntityTypePair(82, 0), //ufo
            new EntityTypePair(83, 0), //spinner
            // Tomb Wader
            new EntityTypePair(71, 0), //scarab
            new EntityTypePair(76, 0), //jump gator
            new EntityTypePair(69, 0), //pusher wave
            new EntityTypePair(75, 0), //shield guy
            // Gone Tomorrow
            new EntityTypePair(82, 0), //bazooka robot
            new EntityTypePair(77, 10), //future fence 2D
            // Eggipus Rex
            new EntityTypePair(16, 9), //ptery
        };

        public static void Mod_RemoveEnemies(NSF nsf, Random rand, Crash3_Levels level, bool isRandom)
        {
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone && zone.EName != "77_gZ") // eggipus entrance
                        {
                            foreach (Entity ent in zone.Entities)
                            {
                                ent.Name = null; // may need the extra space
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
                                                ent.Scaling = 0;
                                                ent.Settings.Clear();
                                                ent.Settings.Add(new EntitySetting(0, 0));
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
            // Toad Village
            new EntityTypePair(11, 1), //goat
            new EntityTypePair(11, 2), //knight
            new EntityTypePair(11, 6), //log
            new EntityTypePair(11, 7), //fence
            new EntityTypePair(11, 0), //frog
            // Under Pressure
            new EntityTypePair(2, 1), //shark
            new EntityTypePair(23, 0), //mine
            //new EntityTypePair(2, 2), //eel
            new EntityTypePair(2, 0), //pufferfish
            new EntityTypePair(2, 3), //paddle
            // Orient Express
            new EntityTypePair(8, 4), //stone assistant
            //new EntityTypePair(7, 0), //assistant
            //new EntityTypePair(6, 0), //dragon
            new EntityTypePair(8, 0), //barrel
            // Bone Yard
            //new EntityTypePair(13, 0), //grass
            new EntityTypePair(16, 2), //tery
            new EntityTypePair(19, 0), //swamp assistant
            // Makin Waves
            //new EntityTypePair(42, 0), //jet mine
            //new EntityTypePair(46, 0), //boat guy
            //new EntityTypePair(42, 1), //jet mine circle
            //new EntityTypePair(15, 16), //ship
            //new EntityTypePair(15, 18), //ship
            // Gee Wiz
            new EntityTypePair(25, 0), //wizard
            // Hang Em High
            new EntityTypePair(61, 0), //guard
            //new EntityTypePair(59, 0), //carpet guy
            //new EntityTypePair(58, 0), //scorpion
            new EntityTypePair(65, 0), //vase guy
            // Tomb Time
            new EntityTypePair(37, 2), //cobra
            //new EntityTypePair(37, 0), //croc
            new EntityTypePair(41, 0), //flamer
            //new EntityTypePair(39, 0), //vase
            new EntityTypePair(38, 0), //switch guy
            new EntityTypePair(37, 4), //spear
            // Midnight Run
            new EntityTypePair(31, 0), //runner
            // Dino Might
            new EntityTypePair(21, 0), //crash fish
            // Deep Trouble
            //new EntityTypePair(70, 0), //laser
            // High Time
            //new EntityTypePair(74, 0), //thrower
            new EntityTypePair(60, 0), //knife guy
            // Double Header
            new EntityTypePair(81, 0), //giant
            // Sphynxinator
            new EntityTypePair(57, 0), //sarcophagus
            // Tell No Tales
            //new EntityTypePair(49, 0), //crows nest
            //new EntityTypePair(47, 0), //shark
            // Future Frenzy
            new EntityTypePair(77, 6), //future fence
            new EntityTypePair(79, 0), //jumper
            new EntityTypePair(82, 0), //ufo
            new EntityTypePair(83, 0), //spinner
            // Tomb Wader
            new EntityTypePair(71, 0), //scarab
            //new EntityTypePair(76, 0), //jump gator
            //new EntityTypePair(69, 0), //pusher wave
            new EntityTypePair(75, 0), //shield guy
            // Gone Tomorrow
            new EntityTypePair(82, 0), //bazooka robot
            new EntityTypePair(77, 10), //future fence 2D
            // Eggipus Rex
            //new EntityTypePair(16, 9), //ptery
        };


        public static void Mod_EnemyCrates(NSF nsf, Random rand, Crash3_Levels level, bool isRandom)
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
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
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
                        if (entry is NewZoneEntry zone && zone.EName != "77_gZ") // eggipus entrance
                        {
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
                                                //ent.Scaling = 0;
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

        public static void Mod_RandomizeBosses(NSF nsf, NewNSD nsd, Crash3_Levels level, Random rand, bool isBackwards)
        {
            if (!BossLevelsList.Contains(level))
            {
                return;
            }
            if (level == Crash3_Levels.B03_NTropy && !isBackwards)
            {
                return;
            }

            Entity CrashEntity = null;
            NewZoneEntry CrashZone = null;
            Entity WarpInEntity = null;
            Entity TropyEntity = null;
            NewZoneEntry TropyZone = null;

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    for (int e = 0; e < zonechunk.Entries.Count; e++)
                    {
                        if (zonechunk.Entries[e] is NewZoneEntry zone)
                        {

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (zone.Entities.Count > 0 && i < zone.Entities.Count)
                                {

                                    if (level == Crash3_Levels.B03_NTropy && isBackwards)
                                    {
                                        
                                        if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                        {
                                            CrashEntity = zone.Entities[i];
                                            CrashZone = zone;
                                        }
                                        else if (WarpInEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 9)
                                        {
                                            WarpInEntity = zone.Entities[i];
                                        }
                                        else if (TropyEntity == null && zone.Entities[i].Type == 89 && zone.Entities[i].Subtype == 0)
                                        {
                                            TropyEntity = zone.Entities[i];
                                            TropyZone = zone;
                                        }
                                        
                                    }
                                    zone.Entities[i].Name = null;

                                    if (zone.Entities[i].Type == 68 && zone.Entities[i].Subtype == 0) // N.Gin
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = 35; a > -1; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
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

                                        for (int a = 36; a < Path.Length; a++)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
                                        }

                                    }
                                    else if (zone.Entities[i].Type == 94 && zone.Entities[i].Subtype == 0) // Cortex
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = 0; a < 18; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }

                                            for (int a = 45; a > 17; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }

                                            for (int a = 46; a < Path.Length; a++)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            //todo
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
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

                                    }
                                    else if (zone.Entities[i].Type == 94 && zone.Entities[i].Subtype == 5) // Uka
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        if (isBackwards)
                                        {
                                            for (int a = Path.Length - 1; a > -1; a--)
                                            {
                                                zone.Entities[i].Positions.Add(Path[a]);
                                            }
                                        }
                                        else
                                        {
                                            //todo
                                            List<int> PosToRand = new List<int>();
                                            for (int a = 0; a < 36; a++)
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

                                    }
                                }
                            }

                        }
                    }
                }
            }

            if (level == Crash3_Levels.B03_NTropy && isBackwards)
            {

                EntityPosition CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
                EntityPosition TropyPos = new EntityPosition(TropyEntity.Positions[0].X, TropyEntity.Positions[0].Y, TropyEntity.Positions[0].Z);
                EntityPosition WarpInPos = new EntityPosition(WarpInEntity.Positions[0].X, WarpInEntity.Positions[0].Y, WarpInEntity.Positions[0].Z);

                EntityPosition BackSpawn = new EntityPosition(TropyEntity.Positions[0].X, CrashEntity.Positions[0].Y, (short)(TropyEntity.Positions[0].Z - 8000));
                EntityPosition FrontSpawn = new EntityPosition(CrashEntity.Positions[0].X, TropyEntity.Positions[0].Y, (short)(CrashEntity.Positions[0].Z + 7000));

                CrashEntity.Positions.RemoveAt(0);
                TropyEntity.Positions.RemoveAt(0);
                WarpInEntity.Positions.RemoveAt(0);

                CrashEntity.Positions.Add(BackSpawn);
                WarpInEntity.Positions.Add(BackSpawn);
                TropyEntity.Positions.Add(FrontSpawn);

                CrashEntity.Name = null;
                WarpInEntity.Name = null;

                int xoffset = BitConv.FromInt32(CrashZone.Layout, 0);
                int yoffset = BitConv.FromInt32(CrashZone.Layout, 4);
                int zoffset = BitConv.FromInt32(CrashZone.Layout, 8);

                nsd.Spawns[0].SpawnX = (xoffset + BackSpawn.X * 1) << 8;
                nsd.Spawns[0].SpawnY = (yoffset + BackSpawn.Y * 1) << 8;
                nsd.Spawns[0].SpawnZ = (zoffset + BackSpawn.Z * 1) << 8;
            }
            

        }

        static void AddToDrawList(ref NSF nsf, ref NewZoneEntry zone, int ID, bool debug = false)
        {
            AddToDrawListSingle(ref nsf, zone, ID, debug);
        }
        static void AddToDrawListMulti(ref NSF nsf, ref NewZoneEntry zone, int ID)
        {

            AddToDrawListSingle(ref nsf, zone, ID, false);
            
            // doesn't do anything
            foreach (NewZoneEntry czone in GetNeighborZones(ref nsf, ref zone))
            {
                AddToDrawListSingle(ref nsf, czone, ID, false);
            }
        }
        static void AddToDrawListSingle(ref NSF nsf, NewZoneEntry zone, int ID, bool debug)
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
                        if (zone.Entities[i].DrawListB.Rows[a].Values.Contains(BoxEntID))
                        {
                            zone.Entities[i].DrawListB.Rows[a].Values.Remove(BoxEntID);
                        }
                        if (zone.Entities[i].DrawListB.Rows[a].MetaValue == LowPos)
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
                        if (zone.Entities[i].DrawListA.Rows[a].Values.Contains(BoxEntID))
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Remove(BoxEntID);
                        }
                        if (zone.Entities[i].DrawListA.Rows[a].MetaValue == MaxPos)
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Add(BoxEntID);
                        }
                    }
                }
            }
        }
        static void AddToDrawListOneCam(ref NSF nsf, NewZoneEntry zone, int ID, int cam)
        {

            int BoxEntID = GetDrawListValue(nsf, zone, ID);

            for (int i = 0; i < zone.Entities.Count; i++)
            {
                if (zone.Entities[i].CameraIndex == cam)
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
                            if (zone.Entities[i].DrawListB.Rows[a].Values.Contains(BoxEntID))
                            {
                                zone.Entities[i].DrawListB.Rows[a].Values.Remove(BoxEntID);
                            }
                            if (zone.Entities[i].DrawListB.Rows[a].MetaValue == LowPos)
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
                            if (zone.Entities[i].DrawListA.Rows[a].Values.Contains(BoxEntID))
                            {
                                zone.Entities[i].DrawListA.Rows[a].Values.Remove(BoxEntID);
                            }
                            if (zone.Entities[i].DrawListA.Rows[a].MetaValue == MaxPos)
                            {
                                zone.Entities[i].DrawListA.Rows[a].Values.Add(BoxEntID);
                            }
                        }
                    }
                }
            }
        }
        static void RemoveFromDrawLists(ref NSF nsf, NewZoneEntry zone, int ID)
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
        static void RemoveFromDrawListsOneCam(ref NSF nsf, NewZoneEntry zone, int ID, int cam)
        {
            int BoxEntID = GetDrawListValue(nsf, zone, ID);

            for (int i = 0; i < zone.Entities.Count; i++)
            {
                if (zone.Entities[i].CameraIndex != null && zone.Entities[i].CameraIndex == cam)
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
        }

        //omg why is this so convoluted for just the id ;_;
        static int GetDrawListValue(NSF nsf, NewZoneEntry thiszone, int id)
        {
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
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

        static void CreateEntity(int id, int type, int subtype, short x, short y, short z, ref NewZoneEntry zone)
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
            newentity.Scaling = 0;
            newentity.ZMod = null;
            newentity.Settings.Clear();
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 32768));
            newentity.Settings.Add(new EntitySetting(0, 0));

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static void CreateEntityFruitBox(int id, int type, int subtype, short x, short y, short z, ref NewZoneEntry zone)
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
            newentity.Scaling = 0;
            newentity.ZMod = null;
            newentity.Settings.Clear();
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(4, 224));
            newentity.Settings.Add(new EntitySetting(0, 0));

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static void CreateEntityFireFly(int id, short x, short y, short z, ref NewZoneEntry zone)
        {
            Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
            newentity.ID = id;
            newentity.AlternateID = null;
            newentity.Name = null;
            newentity.Positions.Clear();
            newentity.Positions.Add(new EntityPosition(x, y, z));
            newentity.Type = 93;
            newentity.Subtype = 1;
            newentity.DDASection = null;
            newentity.DDASettings = null;
            newentity.Scaling = 0;
            newentity.ZMod = null;
            newentity.Settings.Clear();
            newentity.Settings.Add(new EntitySetting(224, 1));
            newentity.Settings.Add(new EntitySetting(255, -1));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(20, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));
            newentity.Settings.Add(new EntitySetting(0, 0));

            zone.Entities.Add(newentity);
            zone.EntityCount++;

        }

        static List<NewZoneEntry> GetNeighborZones(ref NSF nsf, ref NewZoneEntry pzone)
        {
            List<NewZoneEntry> zones = new List<NewZoneEntry>();
            List<int> zoneIDs = new List<int>();

            for (int i = 0; i < pzone.ZoneCount; ++i)
            {
                zoneIDs.Add(BitConv.FromInt32(pzone.Header, 0x194 + i * 4));
            }

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            if (zoneIDs.Contains(zone.EID))
                            {
                                zones.Add(zone);
                            }
                        }
                    }
                }
            }

            return zones;
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
                            if (e.EName.StartsWith("Cb") || e.EName.StartsWith("Cr") || e.EName.StartsWith("CR") || e.EName.StartsWith("Ch") || e.EName.StartsWith("CS") || e.EName.StartsWith("WiB")
                                 || e.EName.StartsWith("Cd") || e.EName.StartsWith("Cgb") || e.EName.StartsWith("CM"))
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

        public static void Mod_AshedCrates(NSF nsf, Random rand, bool isRandom)
        {
            foreach (ModelEntry model in nsf.GetEntries<ModelEntry>())
            {
                if (model.EName.StartsWith("B") && (model.EName.EndsWith("10G") || model.EName.EndsWith("20G") || model.EName.EndsWith("30G") || model.EName.EndsWith("40G")))
                {
                    if (!isRandom || (isRandom && rand.Next(2) == 0))
                    {
                        for (int i = 0; i < model.Colors.Count; ++i)
                        {
                            model.Colors[i] = new SceneryColor(0, 0, 0, model.Colors[i].Extra);
                        }
                    }
                }
            }
        }

        public static void Mod_Metadata(NSF nsf, NewNSD nsd, Crash3_Levels level, RegionType region)
        {
            if (level != Crash3_Levels.WarpRoom)
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

                                            InsertStringsInByteArray(ref gool.Anims, i, 27, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(), 
                                            seed,
                                            "QUIT" 
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
                                        if (s.Contains("GET ALL"))
                                        {
                                            string seed = ModLoaderGlobals.RandomizerSeed.ToString();
                                            if (seed.Length < 10)
                                            {
                                                while (seed.Length < 10)
                                                {
                                                    seed += " ";
                                                }
                                            }

                                            InsertStringsInByteArray(ref gool.Anims, i - 103, 20, new List<string>() {
                                            ModLoaderGlobals.ProgramVersion.ToUpper(),
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

        public static void Mod_RandomizeFlyingLevels(NSF nsf, NewNSD nsd, Crash3_Levels level, Random rand, bool isBackwards)
        {
            if (!FlyingLevelsList.Contains(level))
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

            foreach (Chunk chunk in nsf.Chunks)
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
