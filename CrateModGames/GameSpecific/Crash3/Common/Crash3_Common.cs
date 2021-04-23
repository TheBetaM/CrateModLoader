using System;
using System.Collections.Generic;
using Crash;

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

    public enum Crash3_CrateParamFlagsA
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

    public static class Crash3_Common
    {
        public static List<Crash3_Levels> JetskiLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L05_MakinWaves,
            Crash3_Levels.L18_TellNoTales,
            Crash3_Levels.L26_SkiCrazed,
            Crash3_Levels.L29_HotCoco,
        };

        public static List<Crash3_Levels> TigerLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L03_OrientExpress,
            Crash3_Levels.L10_MidnightRun,
        };

        public static List<Crash3_Levels> UnderwaterLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L02_UnderPressure,
            Crash3_Levels.L12_DeepTrouble,
        };

        public static List<Crash3_Levels> BikeLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L08_HogRide,
            Crash3_Levels.L14_RoadCrash,
            Crash3_Levels.L22_OrangeAsphalt,
            Crash3_Levels.L27_Area51,
        };

        public static List<Crash3_Levels> FlyingLevelsList = new List<Crash3_Levels>()
        {
            Crash3_Levels.L17_ByeByeBlimps,
            Crash3_Levels.L24_MadBombers,
            Crash3_Levels.L28_RingsOfPower,
        };

        public static List<Crash3_Levels> ChaseLevelsList = new List<Crash3_Levels>()
        {
            //Crash3_Levels.L04_BoneYard,
            Crash3_Levels.L11_DinoMight,
        };

        public static List<Crash3_Levels> BossLevelsList = new List<Crash3_Levels>()
        {
            //Crash3_Levels.B01_TinyTiger,
            //Crash3_Levels.B02_Dingodile,
            //Crash3_Levels.B03_NTropy,
            //Crash3_Levels.B04_NGin,
            Crash3_Levels.B05_Cortex,
        };

        public static string[] Crash3_LevelFileNames = new string[]
        {
            "0A",
            "0B",
            "0C",
            "0D",
            "0E",
            "0F",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "1A",
            "1B",
            "1C",
            "1D",
            "1E",
            "1F",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            //Bosses
            "06",
            "03",
            "04",
            "05",
            "07",
            //Other
            "02",
        };

        public static void AddToDrawList(ref NSF nsf, ref NewZoneEntry zone, int ID, bool debug = false)
        {
            AddToDrawListSingle(ref nsf, zone, ID, debug);
        }
        public static void AddToDrawListMulti(ref NSF nsf, ref NewZoneEntry zone, int ID)
        {

            AddToDrawListSingle(ref nsf, zone, ID, false);

            // doesn't do anything
            foreach (NewZoneEntry czone in GetNeighborZones(ref nsf, ref zone))
            {
                AddToDrawListSingle(ref nsf, czone, ID, false);
            }
        }
        public static void AddToDrawListSingle(ref NSF nsf, NewZoneEntry zone, int ID, bool debug)
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
        public static void AddToDrawListOneCam(ref NSF nsf, NewZoneEntry zone, int ID, int cam)
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
        public static void RemoveFromDrawLists(ref NSF nsf, NewZoneEntry zone, int ID)
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
        public static void RemoveFromDrawListsOneCam(ref NSF nsf, NewZoneEntry zone, int ID, int cam)
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
        public static int GetDrawListValue(NSF nsf, NewZoneEntry thiszone, int id)
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

        public static void CreateEntity(int id, int type, int subtype, short x, short y, short z, ref NewZoneEntry zone)
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

        public static List<NewZoneEntry> GetNeighborZones(ref NSF nsf, ref NewZoneEntry pzone)
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


    }
}
