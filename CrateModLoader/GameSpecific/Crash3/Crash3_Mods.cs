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
                                        if (ent.Settings.Count > 0)
                                        {
                                            while (ent.Settings.Count > 0)
                                            {
                                                ent.Settings.RemoveAt(0);
                                            }
                                            ent.Settings.Add(new EntitySetting(0, 0));
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
            //Crash3_Levels.L04_BoneYard, // unverified, todo: camera stitching, crutches
            Crash3_Levels.L06_GeeWiz, 
            Crash3_Levels.L07_HangEmHigh,
            Crash3_Levels.L09_TombTime, 
            //Crash3_Levels.L11_DinoMight, // unverified, todo: freezes around the yellow gem platform
            Crash3_Levels.L13_HighTime,
            Crash3_Levels.L15_DoubleHeader,
            Crash3_Levels.L16_Sphynxinator, 
            Crash3_Levels.L19_FutureFrenzy, 
            Crash3_Levels.L20_TombWader,
            Crash3_Levels.L21_GoneTomorrow, // unverified, todo: camera stitching
            Crash3_Levels.L23_FlamingPassion,
            Crash3_Levels.L25_BugLite, // todo: darkness
            Crash3_Levels.L30_EggipusRex, // todo: warpout doesn't appear

            Crash3_Levels.L02_UnderPressure,
            Crash3_Levels.L12_DeepTrouble,

            //Crash3_Levels.L05_MakinWaves, // chunk space
            Crash3_Levels.L18_TellNoTales, // unverified, todo: not spawning yet
            //Crash3_Levels.L26_SkiCrazed, // chunk space

            //Crash3_Levels.L03_OrientExpress, // todo: tiger stuff (unstable - game may crash if jumped on bouncepad that turns you into crash, softlock on checkpoint respawn)
            //Crash3_Levels.L10_MidnightRun, // unverified, todo: camera stitching, tiger stuff
            Crash3_Levels.L28_RingsOfPower, // maybe move spawn/clock?

            //Crash3_Levels.L08_HogRide, // todo: vehicle stuff
            //Crash3_Levels.L14_RoadCrash, // todo: vehicle stuff
            //Crash3_Levels.L22_OrangeAsphalt, // todo: vehicle stuff
            //Crash3_Levels.L27_Area51, // todo: vehicle stuff
            //Crash3_Levels.L17_ByeByeBlimps, // probably not
            //Crash3_Levels.L24_MadBombers, // probably not
            //Crash3_Levels.L29_HotCoco, // probably not

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

            Entity CrashEntity = null;
            NewZoneEntry CrashZone = null;
            Entity WarpOutEntity = null;
            NewZoneEntry WarpOutZone = null;
            Entity EmptyEntity = null;
            Entity WarpInEntity = null;

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
                                        }
                                    }
                                }

                                if (level == Crash3_Levels.L28_RingsOfPower)
                                {
                                    if (zone.Entities[i].Type == 87 && zone.Entities[i].Subtype == 0) //ring
                                    {
                                        int RingID = zone.Entities[i].Settings[0].ValueA;
                                        zone.Entities[i].Settings[0] = new EntitySetting((byte)(29 - RingID), 0);
                                    }
                                }

                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null && !FlyingLevelsList.Contains(level) && !BikeLevelsList.Contains(level))
                                {

                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        if (EmptyEntity == null)
                                            EmptyEntity = zone.Entities[2];
                                        i--;
                                        zone.EntityCount--;
                                    }
                                    else if (WarpOutEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 8) // subtype 8: warpout, subtype 9: warpin
                                    {
                                        if (IgnoreFirstEnd)
                                        {
                                            IgnoreFirstEnd = false;
                                        }
                                        else
                                        {
                                            WarpOutEntity = zone.Entities[i];
                                            WarpOutZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    else if (WarpInEntity == null && zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 9)
                                    {
                                        WarpInEntity = zone.Entities[i];
                                        zone.Entities.RemoveAt(i);
                                        i--;
                                        zone.EntityCount--;
                                    }
                                    else if (ClockEntity == null && zone.Entities[i].Type == 34 && zone.Entities[i].Subtype == 28)
                                    {
                                        ClockEntity = zone.Entities[i];
                                        ClockZone = zone;
                                        if (level != Crash3_Levels.L30_EggipusRex)
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }
                                    else if (BoxCounterEntity == null && zone.Entities[i].Type == 4 && zone.Entities[i].Subtype == 17)
                                    {
                                        if (IgnoreFirstCounter)
                                        {
                                            IgnoreFirstCounter = false;
                                        }
                                        else
                                        {
                                            BoxCounterEntity = zone.Entities[i];
                                            BoxCounterZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }

                                    if (ChaseLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 17 && (zone.Entities[i].Subtype == 2 || zone.Entities[i].Subtype == 0))
                                        {
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                            zone.EntityCount--;
                                        }
                                    }

                                }
                            }

                            if (level == Crash3_Levels.L13_HighTime)
                            {
                                if (zone.EName == "22_qZ")
                                {
                                    // save some chunk space
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                        {
                                            if (zone.Entities[i].Type == 54 && zone.Entities[i].Subtype == 4) // no back-backtracking!
                                            {
                                                zone.Entities.RemoveAt(i);
                                                i--;
                                                zone.EntityCount--;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (level == Crash3_Levels.L16_Sphynxinator)
                            {
                                if (zone.EName == "50_uZ")
                                {
                                    // save some chunk space
                                    for (int i = zone.Entities[1].Positions.Count - 3; i > 0; i--)
                                    {
                                        zone.Entities[1].Positions.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                            else if (level == Crash3_Levels.L15_DoubleHeader)
                            {
                                if (zone.EName == "34_tZ")
                                {
                                    //save some chunk space
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                        {
                                            if (zone.Entities[i].Type == 95 && zone.Entities[i].Subtype == 4) // all swallups must go!
                                            {
                                                zone.Entities[i].Type = 3;
                                                zone.Entities[i].Subtype = 16;
                                                zone.Entities[i].AlternateID = null;
                                                zone.Entities[i].TimeTrialReward = null;
                                                zone.Entities[i].Victims.Clear();
                                                zone.Entities[i].BonusBoxCount = null;
                                                zone.Entities[i].BoxCount = null;
                                                zone.Entities[i].DDASection = null;
                                                zone.Entities[i].DDASettings = null;
                                                zone.Entities[i].ZMod = null;
                                                zone.Entities[i].OtherSettings = null;
                                                if (zone.Entities[i].Positions.Count > 1)
                                                {
                                                    for (int p = 1; p < zone.Entities[i].Positions.Count; p++)
                                                    {
                                                        zone.Entities[i].Positions.RemoveAt(1);
                                                    }
                                                }
                                                if (zone.Entities[i].Settings.Count > 0)
                                                {
                                                    while (zone.Entities[i].Settings.Count > 0)
                                                    {
                                                        zone.Entities[i].Settings.RemoveAt(0);
                                                    }
                                                    zone.Entities[i].Settings.Add(new EntitySetting(0, 0));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (level == Crash3_Levels.L21_GoneTomorrow)
                            {
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
                                /* none of this works
                                if (zone.EName == "28_zZ")
                                {
                                    int camID = 0;
                                    zone.Entities[camID].Neighbors = new EntityUInt32Property();
                                    zone.Entities[camID].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                    zone.Entities[camID].Neighbors.Rows[zone.Entities[camID].Neighbors.RowCount - 1].MetaValue = 0;
                                    int neighborindex = zone.Entities[camID].Neighbors.RowCount - 1;
                                    int neighborsettingindex = 0;

                                    int camflag = 1;
                                    int camIndex = 1;
                                    int camZone = 0;
                                    int camLink = 2;

                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values.Add(0);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);
                                }
                                else if (zone.EName == "27_zZ")
                                {
                                    
                                    int camID = 0;
                                    zone.Entities[camID].Neighbors = new EntityUInt32Property();
                                    zone.Entities[camID].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                    zone.Entities[camID].Neighbors.Rows[zone.Entities[camID].Neighbors.RowCount - 1].MetaValue = 0;
                                    int neighborindex = zone.Entities[camID].Neighbors.RowCount - 1;
                                    int neighborsettingindex = 0;

                                    int camflag = 2;
                                    int camIndex = 0;
                                    int camZone = 1;
                                    int camLink = 1;

                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values.Add(0);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                    zone.Entities[camID].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);
                                    
                                }
                                */
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                for (int e = 0; e < zone.Entities.Count; e++)
                                {
                                    if (zone.Entities[e].Type != null && zone.Entities[e].Type == 48 && zone.Entities[e].Subtype == 0)
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

                                if (zone.EName == "46_aZ")
                                {
                                    //save some chunk space
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                        {
                                            if (zone.Entities[i].Type == 95 && zone.Entities[i].Subtype == 4) // all swallups must go!
                                            {
                                                zone.Entities[i].Type = 3;
                                                zone.Entities[i].Subtype = 16;
                                                zone.Entities[i].AlternateID = null;
                                                zone.Entities[i].TimeTrialReward = null;
                                                zone.Entities[i].Victims.Clear();
                                                zone.Entities[i].BonusBoxCount = null;
                                                zone.Entities[i].BoxCount = null;
                                                zone.Entities[i].DDASection = null;
                                                zone.Entities[i].DDASettings = null;
                                                zone.Entities[i].ZMod = null;
                                                zone.Entities[i].OtherSettings = null;
                                                if (zone.Entities[i].Positions.Count > 1)
                                                {
                                                    for (int p = 1; p < zone.Entities[i].Positions.Count; p++)
                                                    {
                                                        zone.Entities[i].Positions.RemoveAt(1);
                                                    }
                                                }
                                                if (zone.Entities[i].Settings.Count > 0)
                                                {
                                                    while (zone.Entities[i].Settings.Count > 0)
                                                    {
                                                        zone.Entities[i].Settings.RemoveAt(0);
                                                    }
                                                    zone.Entities[i].Settings.Add(new EntitySetting(0, 0));
                                                }
                                            }
                                        }
                                    }

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
                                    if (zone.Entities[e].Type != null && zone.Entities[e].Type == 48 && zone.Entities[e].Subtype == 0)
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
                            }

                            if (level == Crash3_Levels.L11_DinoMight)
                            {
                                if (zone.EName == "43_gZ")
                                {
                                    int id = 500;
                                    int id1 = 501;
                                    CreateEntity(id,  34, 5, 2600, 2400, 2500, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 3050, 1350, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L04_BoneYard)
                            {
                                if (zone.EName == "43_cZ")
                                {
                                    for (int i = 0; i < zone.Entities.Count; i++)
                                    {
                                        if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                        {
                                            if (zone.Entities[i].Type == 95 && zone.Entities[i].Subtype == 6) // no butterfly, smoke
                                            {
                                                zone.Entities.RemoveAt(i);
                                                i--;
                                                zone.EntityCount--;
                                            }
                                            else if (zone.Entities[i].Type == 16 && zone.Entities[i].Subtype == 3) // no butterfly, smoke
                                            {
                                                zone.Entities.RemoveAt(i);
                                                i--;
                                                zone.EntityCount--;
                                            }
                                        }
                                    }
                                }
                                if (zone.EName == "44_cZ")
                                {

                                    //camera pls, why don't you do something
                                    zone.Entities[3].Neighbors = new EntityUInt32Property();
                                    zone.Entities[3].Neighbors.Rows.Add(new EntityPropertyRow<uint>());
                                    zone.Entities[3].Neighbors.Rows[zone.Entities[3].Neighbors.RowCount - 1].MetaValue = 5;
                                    int neighborindex = zone.Entities[3].Neighbors.RowCount - 1;
                                    int neighborsettingindex = 0;

                                    int camflag = 2;
                                    int camIndex = 0;
                                    int camZone = 1;
                                    int camLink = 1;

                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values.Add(0);
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFFFF00;
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camflag << 0);
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFFFF00FF;
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camIndex << 8);
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0xFF00FFFF;
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camZone << 16);
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] &= 0x00FFFFFF;
                                    zone.Entities[3].Neighbors.Rows[neighborindex].Values[neighborsettingindex] |= (uint)((byte)camLink << 24);
                                    
                                    int id = 500;
                                    int id1 = 501;
                                    CreateEntity(id, 34, 5, 2600, 1400, 2500, ref zone);
                                    CreateEntity(id1, 34, 5, 2600, 2450, 1750, ref zone);

                                    AddToDrawList(ref nsf, ref zone, id);
                                    AddToDrawList(ref nsf, ref zone, id1);
                                }
                            }

                        }
                    }
                }
            }

            EntityPosition CrashPos = new EntityPosition();
            EntityPosition WarpOutPos = new EntityPosition();

            if (WarpOutEntity != null)
            {
                CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
                WarpOutPos = new EntityPosition(WarpOutEntity.Positions[0].X, WarpOutEntity.Positions[0].Y, WarpOutEntity.Positions[0].Z);
                CrashEntity.Positions.RemoveAt(0);
                WarpOutEntity.Positions.RemoveAt(0);
                WarpInEntity.Positions.RemoveAt(0);
            }

            //sometimes the spawn is too low or too far
            if (level == Crash3_Levels.L01_ToadVillage)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 2200), (short)(WarpOutPos.Z + 2000)); // WarpOutPos.Z);
            }
            else if (level == Crash3_Levels.L16_Sphynxinator)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 3000));
            }
            else if (level == Crash3_Levels.L07_HangEmHigh)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 1500)); // WarpOutPos.Z); 
            }
            else if (level == Crash3_Levels.L15_DoubleHeader)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 2000)); // WarpOutPos.Z);
            }
            else if (level == Crash3_Levels.L23_FlamingPassion)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 1600), (short)(WarpOutPos.Z + 2000)); // WarpOutPos.Z);
            }
            else if (level == Crash3_Levels.L21_GoneTomorrow)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, WarpOutPos.Y, (short)(WarpOutPos.Z + 1500)); // WarpOutPos.Z); 
            }
            else if (level == Crash3_Levels.L03_OrientExpress)
            {
                WarpOutPos = new EntityPosition(WarpOutPos.X, (short)(WarpOutPos.Y + 2600), (short)(WarpOutPos.Z + 2000)); // WarpOutPos.Z); 
                CrashEntity.BoxCount = new EntitySetting(0, CrashEntity.BoxCount.Value.ValueB + 6); //crutch boxes
            }
            else if (level == Crash3_Levels.L10_MidnightRun)
            {
                CrashEntity.BoxCount = new EntitySetting(0, CrashEntity.BoxCount.Value.ValueB + 6); //crutch boxes
            }
            else if (level == Crash3_Levels.L18_TellNoTales)
            {
                //WarpOutPos = new EntityPosition((short)(WarpOutPos.X + 3000), WarpOutPos.Y, WarpOutPos.Z);
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
                ClockEntity.ID = BoxCounterEntity.ID;
                BoxCounterEntity.ID = tempclockID;

                EntityPosition ClockPos = new EntityPosition(ClockEntity.Positions[0].X, ClockEntity.Positions[0].Y, ClockEntity.Positions[0].Z);
                EntityPosition BoxCountPos = new EntityPosition(BoxCounterEntity.Positions[0].X, BoxCounterEntity.Positions[0].Y, BoxCounterEntity.Positions[0].Z);
                ClockEntity.Positions.RemoveAt(0);
                BoxCounterEntity.Positions.RemoveAt(0);
                ClockEntity.Positions.Add(BoxCountPos);
                BoxCounterEntity.Positions.Add(ClockPos);
            }

            if (WarpOutEntity != null)
            {
                int tempID = (int)CrashEntity.ID;
                CrashEntity.ID = WarpOutEntity.ID;
                WarpOutEntity.ID = tempID;

                //not needed to function, they're mostly delays and pushes before the first spawn
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

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            if (CrashEntity != null && WarpOutEntity != null && zone.EName == CrashZone.EName)
                            {
                                zone.Entities.Add(WarpOutEntity);
                                //zone.Entities[0].DrawListA.Rows[0].Values.Add((int)WarpOutEntity.ID);
                                //zone.Entities[0].DrawListB.Rows[0].Values.Add((int)WarpOutEntity.ID);
                                zone.EntityCount++;
                            }
                            else if (WarpOutEntity != null && CrashEntity != null && zone.EName == WarpOutZone.EName)
                            {
                                zone.Entities.Add(CrashEntity);
                                zone.Entities.Add(WarpInEntity);
                                //zone.Entities[0].DrawListA.Rows[0].Values.Add((int)CrashEntity.ID);
                                //zone.Entities[0].DrawListB.Rows[0].Values.Add((int)CrashEntity.ID);

                                AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);

                                //zone.Entities[0].DrawListA.Rows[0].Values.Add(BoxEntID);
                                //zone.Entities[0].DrawListB.Rows[0].Values.Add(BoxEntID);
                                zone.EntityCount++;
                                zone.EntityCount++;
                            }
                            else if (ClockZone != null && BoxCounterEntity != null && ClockZone.EName == zone.EName)
                            {
                                zone.Entities.Add(BoxCounterEntity);
                                zone.EntityCount++;
                            }
                            else if (ClockZone != null && BoxCounterEntity != null && BoxCounterZone.EName == zone.EName)
                            {
                                zone.Entities.Add(ClockEntity);
                                zone.EntityCount++;
                            }

                            
                            if (level == Crash3_Levels.L11_DinoMight)
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
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                if (zone.EName == "47_aZ" || zone.EName == "45_aZ")
                                {
                                    for (int id = 500; id < 506; id++)
                                    {
                                        AddToDrawList(ref nsf, ref zone, id);
                                    }
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
                                }
                            }

                            if (level != Crash3_Levels.L18_TellNoTales && !FlyingLevelsList.Contains(level) && !BikeLevelsList.Contains(level))
                            {
                                if (WarpOutZone.EName.Substring(2, 3) == zone.EName.Substring(2, 3))
                                {
                                    int warpNumber = 0;
                                    int thisNumber = -10;
                                    bool OutZone = int.TryParse(WarpOutZone.EName.Substring(0, 2), out warpNumber);
                                    bool InZone = int.TryParse(zone.EName.Substring(0, 2), out thisNumber);

                                    if ((OutZone && InZone) && (Math.Abs(warpNumber - thisNumber) <= 1 && warpNumber != thisNumber))
                                    {
                                        AddToDrawList(ref nsf, ref zone, (int)WarpInEntity.ID);
                                    }
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

                nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 1) << 8;

                if (JetskiLevelsList.Contains(level))
                {
                    nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 2) << 8;
                    nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 2) << 8;
                    nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 2) << 8;
                }
            }

            //nsd.Spawns[0].Camera = 0;

            /*
            int spawnX = (xoffset + CrashPos.X * 1) << 8;
            int spawnY = (yoffset + CrashPos.Y * 1) << 8;
            int spawnZ = (zoffset + CrashPos.Z * 1) << 8;

            int spawnX1 = (xoffset + CrashPos.X * 4) << 8;
            int spawnY1 = (yoffset + CrashPos.Y * 4) << 8;
            int spawnZ1 = (zoffset + CrashPos.Z * 4) << 8;

            int oldX = nsd.Spawns[0].SpawnX >> 8;
            int oldY = nsd.Spawns[0].SpawnY >> 8;
            int oldZ = nsd.Spawns[0].SpawnZ >> 8;

            Console.WriteLine("Orig X: " + nsd.Spawns[0].SpawnX);
            Console.WriteLine("Orig Y: " + nsd.Spawns[0].SpawnY);
            Console.WriteLine("Orig Z: " + nsd.Spawns[0].SpawnZ);

            Console.WriteLine(" New X: " + spawnX);
            Console.WriteLine(" New Y: " + spawnY);
            Console.WriteLine(" New Z: " + spawnZ);

            Console.WriteLine(" Old X: " + oldX);
            Console.WriteLine(" Old Y: " + oldY);
            Console.WriteLine(" Old Z: " + oldZ);

            Console.WriteLine("Orig X: " + Convert.ToString(nsd.Spawns[0].SpawnX, toBase: 2));
            Console.WriteLine("Orig Y: " + Convert.ToString(nsd.Spawns[0].SpawnY, toBase: 2));
            Console.WriteLine("Orig Z: " + Convert.ToString(nsd.Spawns[0].SpawnZ, toBase: 2));

            Console.WriteLine(" New X: " + Convert.ToString(spawnX, toBase: 2));
            Console.WriteLine(" New Y: " + Convert.ToString(spawnY, toBase: 2));
            Console.WriteLine(" New Z: " + Convert.ToString(spawnZ, toBase: 2));

            Console.WriteLine("New1 X: " + Convert.ToString(spawnX1, toBase: 2));
            Console.WriteLine("New1 Y: " + Convert.ToString(spawnY1, toBase: 2));
            Console.WriteLine("New1 Z: " + Convert.ToString(spawnZ1, toBase: 2));

            Console.WriteLine("Do they match?");
            */

        }

        public static void Mod_RandomizeWRButtons(NSF nsf, NewNSD nsd, Crash3_Levels level, Random rand)
        {
            if (level != Crash3_Levels.WarpRoom)
            {
                return;
            }

            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < 35; i ++)
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

            List<int> LevelsIgnore = new List<int>();
            LevelsIgnore.Add(7);
            LevelsIgnore.Add(8);
            LevelsIgnore.Add(9);

            List<int> LevelsToReplace1 = new List<int>();
            for (int i = 3; i < 38; i++)
            {
                if (!LevelsIgnore.Contains(i))
                    LevelsToReplace1.Add(i);
            }
            int MaxLevelCount = LevelsToReplace1.Count;

            List<int> LevelsRand1 = new List<int>();
            for (int i = 0; i < MaxLevelCount; i++)
            {
                int r = rand.Next(LevelsToReplace1.Count);
                LevelsRand1.Add(LevelsToReplace1[r]);
                LevelsToReplace1.RemoveAt(r);
            }
            for (int i = 3; i < 38; i++)
            {
                if (!LevelsIgnore.Contains(i))
                    LevelsToReplace1.Add(i);
            }

            List<int> BarReplace = new List<int>();
            for (int i = 2; i < 6; i++)
            {
                BarReplace.Add(i);
            }
            List<int> BarRand = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int r = rand.Next(BarReplace.Count);
                BarRand.Add(BarReplace[r]);
                BarReplace.RemoveAt(r);
            }
            int BarOne = rand.Next(5);
            int BarID = 0;

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
                                    if (zone.Entities[i].Type == 73 && (int)zone.Entities[i].Subtype < 35) // button
                                    {
                                        
                                        for (int a = 0; a < LevelsToReplace1.Count; a++)
                                        {
                                            if (LevelsToReplace1[a] == zone.Entities[i].Settings[0].ValueB)
                                            {
                                                zone.Entities[i].Settings[0] = new EntitySetting(0, LevelsRand1[a]);
                                                //Console.WriteLine(zone.Entities[i].Name + ": " + LevelsRand1[a]);
                                                break;
                                            }
                                        }
                                        
                                        //zone.Entities[i].Subtype = LevelsRand[(int)zone.Entities[i].Subtype]; // buttons don't appear if it's not the right warp room
                                    }
                                    else if (zone.Entities[i].Type == 26 && zone.Entities[i].Subtype == 2) //barrier
                                    {
                                        int target = BarRand[(int)zone.Entities[i].Settings[1].ValueA - 2];
                                        if (BarOne + 1 == (int)zone.Entities[i].Settings[1].ValueA)
                                        {
                                            zone.Entities[i].Positions.Clear();
                                            zone.Entities[i].Positions.Add(new EntityPosition(3000, 1969, 630));
                                            zone.Entities[i].Settings[0] = new EntitySetting(10, 0);

                                            BarID = (int)zone.Entities[i].ID;
                                            zone.Entities[i].ID = 85;
                                        }
                                        zone.Entities[i].Settings[1] = new EntitySetting((byte)BarRand[(int)zone.Entities[i].Settings[1].ValueA - 2], 0);
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
                        if (entry is NewZoneEntry zone && BarID != 0)
                        {
                            if (zone.EName == "00_2Z")
                            {
                                AddToDrawList(ref nsf, ref zone, BarID);
                            }

                            for (int i = 0; i < zone.Entities.Count; i++)
                            {

                                if (zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        zone.Entities[i].ID = BarID;
                                    }
                                }

                            }
                        }
                    }
                }
            }

        }

        static void AddToDrawList(ref NSF nsf, ref NewZoneEntry zone, int ID)
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
                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
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
                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListA.Rows[a].MetaValue == MaxPos)
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Add(BoxEntID);
                        }
                    }
                }
            }
        }
        static void AddToDrawListRev(ref NSF nsf, ref NewZoneEntry zone, int ID)
        {
            int BoxEntID = GetDrawListValue(nsf, zone, ID);

            for (int i = 0; i < zone.Entities.Count; i++)
            {
                if (zone.Entities[i].DrawListA != null)
                {
                    short LowPos = short.MaxValue;
                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListA.Rows[a].MetaValue < LowPos)
                        {
                            LowPos = (short)zone.Entities[i].DrawListA.Rows[a].MetaValue;
                        }
                    }
                    for (int a = 0; a < zone.Entities[i].DrawListA.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListA.Rows[a].MetaValue == LowPos)
                        {
                            zone.Entities[i].DrawListA.Rows[a].Values.Add(BoxEntID);
                        }
                    }
                }
                if (zone.Entities[i].DrawListB != null)
                {
                    short MaxPos = -1;
                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListB.Rows[a].MetaValue > MaxPos)
                        {
                            MaxPos = (short)zone.Entities[i].DrawListB.Rows[a].MetaValue;
                        }
                    }
                    for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                    {
                        if (zone.Entities[i].DrawListB.Rows[a].MetaValue == MaxPos)
                        {
                            zone.Entities[i].DrawListB.Rows[a].Values.Add(BoxEntID);
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
            newentity.Name = "cml";
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
            newentity.Name = "cml";
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

        public static void Mod_Metadata(NSF nsf, NewNSD nsd, Crash3_Levels level)
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
                                if (ModLoaderGlobals.Region != RegionType.NTSC_J)
                                {
                                    for (int i = gool.Anims.Length - 11; i > 0; i--)
                                    {
                                        string s = System.Text.Encoding.Default.GetString(gool.Anims, i, 10);
                                        if (s.Contains("RESUME"))
                                        {
                                            InsertStringsInByteArray(ref gool.Anims, i, 27, new List<string>() {
                                            "CML " + ModLoaderGlobals.ProgramVersion.ToUpper(), 
                                            ModLoaderGlobals.RandomizerSeed.ToString(),
                                            "QUIT" 
                                        });
                                        }
                                    }
                                }
                                else
                                {
                                    // "WARP ROOM" ?
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
