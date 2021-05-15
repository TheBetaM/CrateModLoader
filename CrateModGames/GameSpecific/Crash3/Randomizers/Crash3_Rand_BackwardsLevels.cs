using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    //test cortex, flying lvls
    public class Crash3_Rand_BackwardsLevels : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;

        private List<Crash3_Levels> BackwardsLevelsList = new List<Crash3_Levels>()
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


        private List<Crash3_Levels> BackwardsCameraList = new List<Crash3_Levels>()
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


        public Crash3_Rand_BackwardsLevels()
        {
            isRandom = Crash3_Props_Main.Option_RandBackwardsLevels.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            Crash3_Levels level = pair.LevelC3;

            if (!BackwardsLevelsList.Contains(level))
            {
                return;
            }
            if (isRandom && rand.Next(2) == 0)
            {
                return;
            }
            if (Crash3_Common.BossLevelsList.Contains(level))
            {
                BossPass(pair);
                return;
            }
            if (Crash3_Common.FlyingLevelsList.Contains(level))
            {
                FLyingLevelPass(pair);
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

            EntityPosition bufferPos = new EntityPosition(0, 0, 0);

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

            foreach (Chunk chunk in pair.nsf.Chunks)
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

                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null && !Crash3_Common.FlyingLevelsList.Contains(level) && !Crash3_Common.BikeLevelsList.Contains(level))
                                {

                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        if (!Crash3_Common.JetskiLevelsList.Contains(level))
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
                                            if (!Crash3_Common.JetskiLevelsList.Contains(level))
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
                                        if (!Crash3_Common.JetskiLevelsList.Contains(level))
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
                                        if (level != Crash3_Levels.L30_EggipusRex && !Crash3_Common.JetskiLevelsList.Contains(level))
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
                                            if (!Crash3_Common.JetskiLevelsList.Contains(level))
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

                            if (Crash3_Common.BikeLevelsList.Contains(level))
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
                                        Crash3_Common.CreateEntity(EntID, 34, 2, EntPos[id].X, EntPos[id].Y, EntPos[id].Z, ref zone);
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, EntID);
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
                                        Crash3_Common.CreateEntity(EntID, 34, 2, EntPos[id].X, EntPos[id].Y, EntPos[id].Z, ref zone);
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, EntID);
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
                                    Crash3_Common.CreateEntity(id, 34, 5, 2600, 2000, 2500, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2600, 2950, 1350, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                                else if (zone.EName == "32_gZ")
                                {
                                    int id = 502;
                                    int id1 = 503;
                                    Crash3_Common.CreateEntity(id, 34, 5, 2600, 1000, 600, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2600, 1950, -100, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L04_BoneYard)
                            {
                                if (zone.EName == "01_cZ")
                                {

                                    int id = 504;
                                    int id1 = 505;
                                    Crash3_Common.CreateEntity(id, 34, 5, 2900, 1200, 3000, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2900, 2200, 2550, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                                else if (zone.EName == "06_cZ")
                                {

                                    int id = 506;
                                    int id1 = 507;
                                    Crash3_Common.CreateEntity(id, 34, 5, 2600, 1100, 2000, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2600, 1900, 1350, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                                else if (zone.EName == "14_cZ")
                                {

                                    int id = 509;
                                    int id1 = 510;
                                    Crash3_Common.CreateEntity(id, 34, 5, 1800, 1150, 700, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 1800, 1950, 0, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                                else if (zone.EName == "34_cZ")
                                {

                                    int id = 511;
                                    int id1 = 512;
                                    Crash3_Common.CreateEntity(id, 34, 5, 2600, 1100, 500, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2600, 1800, 0, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
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

                                    pair.newnsd.Spawns[0].Camera = 0;

                                    int id = 500;
                                    int id1 = 501;
                                    Crash3_Common.CreateEntity(id, 34, 5, 2400, 1400, 1500, ref zone);
                                    Crash3_Common.CreateEntity(id1, 34, 5, 2400, 2400, 950, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
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

                pair.newnsd.Spawns[0].ZoneEID = WarpOutZone.EID;
            }

            bool Spawned_WarpOut = false;
            bool Spawned_Crash = false;
            bool Spawned_Counter = false;
            bool Spawned_Clock = false;

            foreach (Chunk chunk in pair.nsf.Chunks)
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
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpOutEntity.ID);
                                }
                                if (Crash3_Common.JetskiLevelsList.Contains(level))
                                {
                                    if (level == Crash3_Levels.L18_TellNoTales)
                                    {
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 0);
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 1);
                                        Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)BoxCounterEntity.ID);
                                        //AddToDrawListOneCam(ref pair.nsf, zone, (int)BoxCounterEntity.ID, 0); // makes warpout not appear during first visit...
                                        //AddToDrawListOneCam(ref pair.nsf, zone, (int)BoxCounterEntity.ID, 1);
                                        //RemoveFromDrawListsOneCam(ref pair.nsf, zone, (int)WarpInEntity.ID, 0);
                                        //RemoveFromDrawListsOneCam(ref pair.nsf, zone, (int)WarpInEntity.ID, 1);
                                        //RemoveFromDrawListsOneCam(ref pair.nsf, zone, (int)CrashEntity.ID, 0);
                                        //RemoveFromDrawListsOneCam(ref pair.nsf, zone, (int)CrashEntity.ID, 1);
                                        Spawned_WarpOut = true;
                                    }
                                    else if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 0); //todo
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 1); //todo
                                        Spawned_WarpOut = true;
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        //AddToDrawList(ref pair.nsf, ref zone, (int)WarpOutEntity.ID); //nothing
                                        //AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 6); //todo
                                        //AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 7); //todo
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
                                if (!Crash3_Common.JetskiLevelsList.Contains(level))
                                {
                                    zone.Entities.Add(CrashEntity);
                                    zone.Entities.Add(WarpInEntity);
                                    zone.EntityCount++;
                                    zone.EntityCount++;
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpOutEntity.ID);
                                    if (BoxCounterEntity != null)
                                    {
                                        Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)BoxCounterEntity.ID);
                                    }
                                }
                                if (FlipCrashAndWarpOut)
                                {
                                    if (level == Crash3_Levels.L18_TellNoTales)
                                    {
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpInEntity.ID, 8);
                                    }
                                    else
                                    {
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpInEntity.ID);
                                    }
                                }
                                else
                                {
                                    if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpInEntity.ID, 9);
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        // randomly doesn't work
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)CrashEntity.ID, 5);
                                        Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpInEntity.ID, 5);
                                    }
                                    else
                                    {
                                        if (level == Crash3_Levels.L11_DinoMight || level == Crash3_Levels.L04_BoneYard)
                                        {
                                            Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpInEntity.ID);
                                        }
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)CrashEntity.ID);
                                    }
                                }

                                Spawned_Crash = true;
                            }
                            if (CrashEntity != null && WarpOutEntity != null && WarpInEntity != null && zone.EName == WarpInZone.EName && !Spawned_WarpOut)
                            {
                                if (level == Crash3_Levels.L05_MakinWaves)
                                {
                                    //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 0); //todo (or maybe in crashzone?)
                                    //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)WarpOutEntity.ID, 1); //todo
                                    //Spawned_WarpOut = true;
                                }
                                if (!Crash3_Common.JetskiLevelsList.Contains(level))
                                {
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)ClockEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpInEntity.ID);
                                }
                            }
                            if (ClockZone != null && BoxCounterEntity != null && ClockZone.EName == zone.EName && !Spawned_Counter)
                            {
                                if (!Crash3_Common.JetskiLevelsList.Contains(level))
                                {
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)ClockEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpInEntity.ID);
                                }
                                if (!Crash3_Common.JetskiLevelsList.Contains(level) && level != Crash3_Levels.L20_TombWader)
                                {
                                    zone.Entities.Add(BoxCounterEntity);
                                    zone.EntityCount++;
                                    Spawned_Counter = true;
                                }
                                else
                                {
                                    if (level == Crash3_Levels.L05_MakinWaves)
                                    {
                                        //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)BoxCounterEntity.ID, 0); //todo
                                        //Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)BoxCounterEntity.ID);
                                        //Spawned_Counter = true;
                                    }
                                    else if (level == Crash3_Levels.L26_SkiCrazed)
                                    {
                                        //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)BoxCounterEntity.ID, 0); //todo
                                        //Spawned_Counter = true;
                                    }
                                }
                            }
                            if (ClockZone != null && BoxCounterEntity != null && BoxCounterZone.EName == zone.EName && !Spawned_Clock)
                            {

                                if (level == Crash3_Levels.L18_TellNoTales)
                                {
                                    Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)ClockEntity.ID, 8);
                                    Spawned_Clock = true;
                                }
                                else if (level == Crash3_Levels.L05_MakinWaves)
                                {
                                    //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)ClockEntity.ID, 8);
                                    //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)ClockEntity.ID, 9);
                                    Spawned_Clock = true;
                                }
                                else if (level == Crash3_Levels.L26_SkiCrazed)
                                {
                                    //Crash3_Common.AddToDrawListOneCam(ref pair.nsf, zone, (int)ClockEntity.ID, 5);
                                    Spawned_Clock = true;
                                }
                                else
                                {
                                    zone.Entities.Add(ClockEntity);
                                    zone.EntityCount++;
                                    if (level != Crash3_Levels.L16_Sphynxinator && level != Crash3_Levels.L21_GoneTomorrow) //clock crashes when added to the drawlist
                                    {
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                                    }
                                    if (BoxCounterEntity != null)
                                    {
                                        Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)BoxCounterEntity.ID);
                                    }
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpOutEntity.ID);
                                    Spawned_Clock = true;
                                }


                            }
                            if (ClockZone != null && GemZone != null && level == Crash3_Levels.L30_EggipusRex && !Spawned_Counter && zone.EName == ClockZone.EName)
                            {
                                zone.Entities.Add(GemEntity);
                                zone.EntityCount++;
                                //Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpOutEntity.ID);
                                Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)GemEntity.ID);
                                Spawned_Counter = true;
                            }
                            if (ClockZone != null && GemZone != null && level == Crash3_Levels.L30_EggipusRex && !Spawned_Clock && zone.EName == GemZone.EName)
                            {
                                zone.Entities.Add(ClockEntity);
                                zone.EntityCount++;
                                Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                                Spawned_Clock = true;
                            }

                            if (level == Crash3_Levels.L20_TombWader && !Spawned_Counter)
                            {
                                if (zone.EName == "02_oZ")
                                {
                                    BoxCounterEntity.Positions[0] = new EntityPosition(2000, 2500, 500);
                                    zone.Entities.Add(BoxCounterEntity);
                                    zone.EntityCount++;
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)BoxCounterEntity.ID);
                                    Spawned_Counter = true;
                                }
                            }

                            if (level == Crash3_Levels.L25_BugLite)
                            {
                                if (zone.EName == "43_AZ")
                                {
                                    int id = 500;
                                    CreateEntityFireFly(id, 5412, 1283, -5299, ref zone);

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                }
                            }
                            else if (level == Crash3_Levels.L11_DinoMight)
                            {
                                if (zone.EName == "44_gZ" || zone.EName == "42_gZ")
                                {
                                    int id = 500;
                                    int id1 = 501;

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                            }
                            else if (level == Crash3_Levels.L04_BoneYard)
                            {
                                if (zone.EName == "43_cZ")
                                {
                                    int id = 500;
                                    int id1 = 501;

                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id1);
                                }
                                else if (zone.EName == "01_cZ" || zone.EName == "02_cZ")
                                {
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)ClockEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpInEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)CrashEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)BoxCounterEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 376);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 283);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 366);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 379);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 380);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 382);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 367);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 368);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 369);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 370);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 371);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 372);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 353);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 354);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 355);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 356);
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpOutEntity.ID);
                                }
                                else if (zone.EName == "03_cZ")
                                {
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)ClockEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)WarpInEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)CrashEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, (int)BoxCounterEntity.ID);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 376);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 283);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 366);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 379);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 380);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 382);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 367);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 368);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 369);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 370);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 371);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 372);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 353);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 354);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 355);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 356);
                                }
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                if (zone.EName == "47_aZ" || zone.EName == "45_aZ")
                                {
                                    for (int id = 500; id < 506; id++)
                                    {
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    }
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpInEntity.ID);
                                }
                            }
                            else if (level == Crash3_Levels.L10_MidnightRun)
                            {
                                if (zone.EName == "47_hZ" || zone.EName == "49_hZ")
                                {
                                    for (int id = 500; id < 506; id++)
                                    {
                                        Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                    }
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)WarpInEntity.ID);
                                }
                            }

                            //Clock zone
                            if (level == Crash3_Levels.L01_ToadVillage)
                            {
                                if (zone.EName == "26_bZ")
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L02_UnderPressure)
                            {
                                if (zone.EName == "29_eZ")
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L03_OrientExpress)
                            {
                                if (zone.EName == "47_aZ")
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L07_HangEmHigh)
                            {
                                if (zone.EName == "26_mZ")
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L15_DoubleHeader)
                            {
                                if (zone.EName == "35_tZ")
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                            }
                            else if (level == Crash3_Levels.L06_GeeWiz) //lags
                            {
                                if (zone.EName == "36_fZ")
                                {
                                    Crash3_Common.AddToDrawList(ref pair.nsf, ref zone, (int)ClockEntity.ID);
                                    // removing swallups, butterflies due to lag
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 185);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 192);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 193);
                                }
                                else if (zone.EName == "35_fZ")
                                {
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 185);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 192);
                                    Crash3_Common.RemoveFromDrawLists(ref pair.nsf, zone, 193);
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
                    pair.newnsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                    pair.newnsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                    pair.newnsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 1) << 8;
                }
                else if (Crash3_Common.JetskiLevelsList.Contains(level))
                {
                    pair.newnsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 2) << 8;
                    pair.newnsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 2) << 8;
                    pair.newnsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 2) << 8;
                }
                else
                {
                    pair.newnsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 1) << 8;
                    pair.newnsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 1) << 8;
                    pair.newnsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 1) << 8;
                }
            }

        }

        public void FLyingLevelPass(NSF_Pair pair)
        {
            Crash3_Levels level = pair.LevelC3;
            if (!Crash3_Common.FlyingLevelsList.Contains(level))
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

            foreach (Chunk chunk in pair.nsf.Chunks)
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
                                            short X = ent.Positions[0].X;
                                            short Y = ent.Positions[0].Y;
                                            short Z = (short)(-ent.Positions[0].Z);
                                            ent.Positions.Clear();
                                            ent.Positions.Add(new EntityPosition(X, Y, Z));

                                        }
                                    }
                                    else if (ent.Type != null && ent.Type == 0 && ent.Subtype == 0)
                                    {
                                        //WarpOutPos = ent.Positions[0]; // maybe randomize crash's spawn?
                                    }
                                }
                                else if (level == Crash3_Levels.L24_MadBombers)
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
                                else if (level == Crash3_Levels.L28_RingsOfPower)
                                {
                                    if (ent.Type == 87 && ent.Subtype == 0) //ring
                                    {
                                        int RingID = ent.Settings[0].ValueA;
                                        ent.Settings[0] = new EntitySetting((byte)(29 - RingID), 0);
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

        public void BossPass(NSF_Pair pair)
        {
            Crash3_Levels level = pair.LevelC3;
            if (!Crash3_Common.BossLevelsList.Contains(level))
            {
                return;
            }

            Entity CrashEntity = null;
            NewZoneEntry CrashZone = null;
            Entity WarpInEntity = null;
            Entity TropyEntity = null;
            NewZoneEntry TropyZone = null;

            foreach (Chunk chunk in pair.nsf.Chunks)
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

                                    if (level == Crash3_Levels.B03_NTropy)
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

                                        for (int a = 35; a > -1; a--)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
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
                                    else if (zone.Entities[i].Type == 94 && zone.Entities[i].Subtype == 5) // Uka
                                    {
                                        EntityPosition[] Path = new EntityPosition[zone.Entities[i].Positions.Count];
                                        zone.Entities[i].Positions.CopyTo(Path, 0);
                                        zone.Entities[i].Positions.Clear();

                                        for (int a = Path.Length - 1; a > -1; a--)
                                        {
                                            zone.Entities[i].Positions.Add(Path[a]);
                                        }

                                    }
                                }
                            }

                        }
                    }
                }
            }

            if (level == Crash3_Levels.B03_NTropy)
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

                pair.newnsd.Spawns[0].SpawnX = (xoffset + BackSpawn.X * 1) << 8;
                pair.newnsd.Spawns[0].SpawnY = (yoffset + BackSpawn.Y * 1) << 8;
                pair.newnsd.Spawns[0].SpawnZ = (zoffset + BackSpawn.Z * 1) << 8;
            }
        }

        private void CreateEntityFruitBox(int id, int type, int subtype, short x, short y, short z, ref NewZoneEntry zone)
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

        private void CreateEntityFireFly(int id, short x, short y, short z, ref NewZoneEntry zone)
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


    }
}
