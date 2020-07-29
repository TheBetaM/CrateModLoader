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
        L19_FutueFrenzy = 17,
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
            //Crash3_Levels.L02_UnderPressure,
            //Crash3_Levels.L03_OrientExpress, // todo: tiger stuff
            //Crash3_Levels.L04_BoneYard, // todo: remove chase, area transitions
            //Crash3_Levels.L05_MakinWaves,
            //Crash3_Levels.L06_GeeWiz,
            //Crash3_Levels.L07_HangEmHigh,
            //Crash3_Levels.L08_HogRide, // todo: vehicle stuff
            //Crash3_Levels.L09_TombTime,
            //Crash3_Levels.L10_MidnightRun, //todo: tiger stuff
            //Crash3_Levels.L11_DinoMight, // todo: remove chase, area transitions
            //Crash3_Levels.L12_DeepTrouble,
            //Crash3_Levels.L13_HighTime,
            //Crash3_Levels.L14_RoadCrash, // todo: vehicle stuff
            //Crash3_Levels.L15_DoubleHeader,
            //Crash3_Levels.L16_Sphynxinator,
            //Crash3_Levels.L17_ByeByeBlimps, // probably not
            //Crash3_Levels.L18_TellNoTales,
            //Crash3_Levels.L19_FutueFrenzy, // todo: zone transitions
            //Crash3_Levels.L20_TombWader,
            //Crash3_Levels.L21_GoneTomorrow, // todo: zone transitions
            //Crash3_Levels.L22_OrangeAsphalt, // todo: vehicle stuff
            //Crash3_Levels.L23_FlamingPassion,
            //Crash3_Levels.L24_MadBombers, // probably not
            //Crash3_Levels.L25_BugLite,
            //Crash3_Levels.L26_SkiCrazed,
            //Crash3_Levels.L27_Area51, // todo: vehicle stuff
            //Crash3_Levels.L28_RingsOfPower, // todo: vehicle stuff
            //Crash3_Levels.L29_HotCoco, // probably not
            //Crash3_Levels.L30_EggipusRex,
        };

        static List<Crash3_Levels> ChaseLevelsList = new List<Crash3_Levels>()
        {
            
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
                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        if (EmptyEntity == null)
                                            EmptyEntity = zone.Entities[2];
                                        i--;
                                    }
                                    else if (zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 8) // subtype 8: warpout, subtype 9: warpin
                                    {
                                        WarpOutEntity = zone.Entities[i];
                                        WarpOutZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
                                    }
                                    else if (zone.Entities[i].Type == 30 && zone.Entities[i].Subtype == 9)
                                    {
                                        WarpInEntity = zone.Entities[i];
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
            WarpInEntity.Positions.RemoveAt(0);
            CrashEntity.Positions.Add(WarpOutPos);
            WarpOutEntity.Positions.Add(CrashPos);
            WarpInEntity.Positions.Add(WarpOutPos);

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry zone)
                        {
                            if (zone.EName == CrashZone.EName)
                            {
                                zone.Entities.Add(WarpOutEntity);
                            }
                            else if (zone.EName == WarpOutZone.EName)
                            {
                                zone.Entities.Add(CrashEntity);
                                zone.Entities.Add(WarpInEntity);
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
