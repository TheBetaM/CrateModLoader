using System;
using System.Collections.Generic;
using Crash;

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

    public enum Crash2_CrateParamFlagsA
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
    };

    public static class Crash2_Common
    {
        public static List<Crash2_Levels> ChaseLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L05_CrashDash,
            Crash2_Levels.L09_CrashCrush,
            Crash2_Levels.L15_UnBearable,
        };
        public static List<Crash2_Levels> BoardLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L03_HangEight,
            Crash2_Levels.L07_AirCrash,
            Crash2_Levels.L11_PlantFood,
        };
        public static List<Crash2_Levels> SpaceLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L22_RockIt,
            Crash2_Levels.L24_PackAttack,
        };
        public static List<Crash2_Levels> DarknessLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L23_NightFight,
            Crash2_Levels.L27_TotallyFly,
        };
        public static List<Crash2_Levels> BeeLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L20_BeeHaving,
            Crash2_Levels.L17_DigginIt,
        };
        public static List<Crash2_Levels> BearLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.L08_BearIt,
            Crash2_Levels.L13_BearDown,
            Crash2_Levels.L15_UnBearable,
            Crash2_Levels.L26_TotallyBear,
        };
        public static List<Crash2_Levels> BossLevelsList = new List<Crash2_Levels>()
        {
            Crash2_Levels.B01_RipperRoo,
            Crash2_Levels.B02_KomodoBros,
            Crash2_Levels.B03_TinyTiger,
            Crash2_Levels.B04_NGin,
            Crash2_Levels.B05_Cortex,
        };

        public static string[] Crash2_LevelFileNames = new string[]
        {
            "0A",
            "0C",
            "0D",
            "0E",
            "0F",
            "10",
            "11",
            "12",
            "13",
            "15",
            "16",
            "17",
            "18",
            "19",
            "1A",
            "1B",
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
            "08",
            "03",
            "09",
            "07",
            //Other
            "02",
            "2D",
            "2E",
            "2F",
            "30",
        };

        public static void CreateEntity(int id, int type, int subtype, short x, short y, short z, ref ZoneEntry zone)
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

        //omg why is this so convoluted for just the id ;_;
        public static int GetDrawListValue(NSF nsf, ZoneEntry thiszone, int id)
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


        public static void AddToDrawList(ref NSF nsf, ref ZoneEntry zone, int ID)
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

        public static void RemoveFromDrawList(ref NSF nsf, ZoneEntry zone, int ID)
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
    }
}
