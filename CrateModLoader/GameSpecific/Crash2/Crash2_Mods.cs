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
            //Crash2_Levels.L03_HangEight, // todo: one fence wall, secret spawn?
            Crash2_Levels.L04_ThePits,
            Crash2_Levels.L05_CrashDash,

            //Crash2_Levels.L06_SnowBiz, // todo: 2 section teleports
            //Crash2_Levels.L07_AirCrash, // todo: 3 fence walls, secret spawn?
            //Crash2_Levels.L08_BearIt, // todo: bear stuff
            Crash2_Levels.L09_CrashCrush,
            //Crash2_Levels.L10_TheEelDeal, // todo: 1 section teleport

            //Crash2_Levels.L11_PlantFood, // todo: board dropoff in the wrong direction, design: requires a mask to beat
            //Crash2_Levels.L12_SewerOrLater, // todo: death on spawn, out of bounds spawn zone?
            //Crash2_Levels.L13_BearDown, // todo: bear stuff
            Crash2_Levels.L14_RoadToRuin,
            Crash2_Levels.L15_UnBearable, // had to remove secret exit

            //Crash2_Levels.L16_HanginOut, // todo: 2 section teleports
            Crash2_Levels.L17_DigginIt, // unverified to be beatable
            //Crash2_Levels.L18_ColdHardCrash, // todo: 2 section teleports
            Crash2_Levels.L19_Ruination,
            Crash2_Levels.L20_BeeHaving, // unverified to be beatable

            Crash2_Levels.L21_PistonItAway,
            //Crash2_Levels.L22_RockIt, // todo: crashes on jetpack pickup, because of lack of alarms?
            Crash2_Levels.L23_NightFight,
            //Crash2_Levels.L24_PackAttack, // todo: crashes on jetpack pickup, because of lack of alarms?
            Crash2_Levels.L25_SpacedOut,

            //Crash2_Levels.L26_TotallyBear, // todo: bear stuff, probably won't be possible
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
            List<ZoneEntry> BoardLaunchZones = new List<ZoneEntry>();
            List<Entity> BoardEnts = new List<Entity>();
            List<ZoneEntry> BoardDropoffZones = new List<ZoneEntry>();
            List<Entity> BoardDropEnts = new List<Entity>();
            Entity JetpackEnt = null;
            Entity SpacepadEnt = null;
            ZoneEntry JetpackZone = null;
            ZoneEntry SpacepadZone = null;
            Entity MainEnt = null;

            bool CameraFlip = true;

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            MainEnt = null;
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {
                                if (MainEnt == null && zone.Entities[i].DrawListA != null)
                                {
                                    MainEnt = zone.Entities[i];
                                }

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

                                if (i < zone.Entities.Count && zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                                {
                                    if (CrashEntity == null && zone.Entities[i].Type == 0 && zone.Entities[i].Subtype == 0)
                                    {
                                        CrashEntity = zone.Entities[i];
                                        CrashZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        if (EmptyEntity == null)
                                            EmptyEntity = zone.Entities[2];
                                        i--;
                                    }
                                    else if (WarpOutEntity == null && zone.Entities[i].Type == 1 && zone.Entities[i].Subtype == 1)
                                    {
                                        WarpOutEntity = zone.Entities[i];
                                        WarpOutZone = zone;
                                        zone.Entities.RemoveAt(i);
                                        i--;
                                    }
                                    else if (BoxCounterEntity == null && zone.Entities[i].Type == 4 && zone.Entities[i].Subtype == 17)
                                    {
                                        BoxCounterEntity = zone.Entities[i];
                                        //zone.Entities.RemoveAt(i);
                                        //i--;
                                    }

                                    if (BoardLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 47 && zone.Entities[i].Subtype == 2) // Board launch
                                        {
                                            BoardEnts.Add(zone.Entities[i]);
                                            BoardLaunchZones.Add(zone);
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 47 && zone.Entities[i].Subtype == 3) // Board dropoff
                                        {
                                            BoardDropEnts.Add(zone.Entities[i]);
                                            BoardDropoffZones.Add(zone);
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                        }
                                    }

                                    if (SpaceLevelsList.Contains(level))
                                    {
                                        if (zone.Entities[i].Type == 35 && zone.Entities[i].Subtype == 1) // Jetpack
                                        {
                                            JetpackEnt = zone.Entities[i];
                                            JetpackZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
                                        }
                                        else if (zone.Entities[i].Type == 35 && zone.Entities[i].Subtype == 8) // Spacepad
                                        {
                                            SpacepadEnt = zone.Entities[i];
                                            SpacepadZone = zone;
                                            zone.Entities.RemoveAt(i);
                                            i--;
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
                                            zone.Entities.RemoveAt(i);
                                            zone.Entities.Insert(i, EmptyEntity);
                                            i--;
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
                                                    zone.Entities[i].DrawListA.Rows[a].MetaValue = Math.Min((short)(zone.Entities[i].DrawListA.Rows[a].MetaValue + 8), (short)(zone.Entities[i].Positions.Count - 1));
                                                }
                                                
                                                //zone.Entities[i].DrawListA.Rows.Reverse();
                                            }
                                            if (zone.Entities[i].DrawListB != null)
                                            {
                                                
                                                for (int a = 0; a < zone.Entities[i].DrawListB.Rows.Count; a++)
                                                {
                                                    //zone.Entities[i].DrawListB.Rows[a].MetaValue = Math.Max((short)(zone.Entities[i].DrawListB.Rows[a].MetaValue - 5), (short)0);
                                                    zone.Entities[i].DrawListB.Rows[a].MetaValue = Math.Min((short)(zone.Entities[i].DrawListB.Rows[a].MetaValue + 8), (short)(zone.Entities[i].Positions.Count - 1));
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
                                            */
                                            

                                        }
                                    }
                                }
                            }

                            //Crutch zone
                            if (level == Crash2_Levels.L21_PistonItAway)
                            {
                                if (zone.EName == "05_gZ")
                                {

                                    int id = 10;
                                    foreach (Chunk chunk1 in nsf.Chunks)
                                    {
                                        if (chunk1 is EntryChunk entrychunk)
                                        {
                                            foreach (Entry entry1 in entrychunk.Entries)
                                            {
                                                if (entry1 is ZoneEntry zone1)
                                                {
                                                    foreach (Entity otherentity in zone1.Entities)
                                                    {
                                                        if (otherentity.ID != null && id < otherentity.ID)
                                                        {
                                                            id = (int)otherentity.ID + 1;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    Entity newentity = Entity.Load(new Entity(new Dictionary<short, EntityProperty>()).Save());
                                    newentity.ID = id;
                                    newentity.AlternateID = id;
                                    newentity.Name = "obj_box_iron#2Dnew";
                                    newentity.Positions.Clear();
                                    newentity.Positions.Add(new EntityPosition(854,1149,450));
                                    newentity.Type = 34;
                                    newentity.Subtype = 5;
                                    newentity.DDASection = null;
                                    newentity.DDASettings = null;
                                    newentity.Settings.Clear();
                                    newentity.Settings.Add(new EntitySetting(0, 0));
                                    newentity.Settings.Add(new EntitySetting(0, 16));
                                    newentity.Settings.Add(new EntitySetting(0, 0));
                                    zone.Entities.Add(newentity);
                                    zone.EntityCount++;

                                    if (MainEnt != null)
                                    {
                                        MainEnt.DrawListA.Rows[1].Values.Add(id);
                                        MainEnt.DrawListA.Rows[0].Values.Add(id);
                                    }
                                }
                            }

                        }
                        else if (entry is ModelEntry model)
                        {
                            if (BoardLevelsList.Contains(level))
                            {
                                if (model.EName == "Ra1kG") // ramp
                                {
                                    
                                }
                            }
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

            EntityPosition CrashPos = new EntityPosition(CrashEntity.Positions[0].X, CrashEntity.Positions[0].Y, CrashEntity.Positions[0].Z);
            EntityPosition WarpOutPos = new EntityPosition(WarpOutEntity.Positions[0].X, WarpOutEntity.Positions[0].Y, WarpOutEntity.Positions[0].Z);
            CrashEntity.Positions.RemoveAt(0);
            WarpOutEntity.Positions.RemoveAt(0);
            //BoxCounterEntity.Positions.RemoveAt(0);
            CrashEntity.Positions.Add(WarpOutPos);
            WarpOutEntity.Positions.Add(CrashPos);
            //BoxCounterEntity.Positions.Add(CrashPos);

            //ID switch to fix drawlists?
            int tempID = (int)CrashEntity.ID;
            WarpOutEntity.ID = CrashEntity.ID;
            CrashEntity.ID = tempID;


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
                }
            }

            if (JetpackEnt != null)
            {
                EntityPosition JetpackPos = new EntityPosition(JetpackEnt.Positions[0].X, JetpackEnt.Positions[0].Y, JetpackEnt.Positions[0].Z);
                EntityPosition SpacepadPos = new EntityPosition(SpacepadEnt.Positions[0].X, SpacepadEnt.Positions[0].Y, SpacepadEnt.Positions[0].Z);
                JetpackEnt.Positions.RemoveAt(0);
                SpacepadEnt.Positions.RemoveAt(0);
                JetpackEnt.Positions.Add(SpacepadPos);
                SpacepadEnt.Positions.Add(JetpackPos);
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
                                zone.Entities.Add(WarpOutEntity);
                                //zone.Entities.Add(BoxCounterEntity); // not enough space in crash crush
                            }
                            else if (zone.EName == WarpOutZone.EName)
                            {
                                zone.Entities.Add(CrashEntity);
                            }
                            if (BoardEnts.Count > 0)
                            {
                                for (int i = 0; i < BoardEnts.Count; i++)
                                {
                                    if (zone.EName == BoardLaunchZones[i].EName)
                                    {
                                        zone.Entities.Add(BoardDropEnts[i]);
                                    }
                                    else if (zone.EName == BoardDropoffZones[i].EName)
                                    {
                                        zone.Entities.Add(BoardEnts[i]);
                                    }
                                }
                            }
                            if (JetpackEnt != null)
                            {
                                if (zone.EName == JetpackZone.EName)
                                {
                                    zone.Entities.Add(SpacepadEnt);
                                }
                                else if (zone.EName == SpacepadZone.EName)
                                {
                                    zone.Entities.Add(JetpackEnt);
                                }
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
