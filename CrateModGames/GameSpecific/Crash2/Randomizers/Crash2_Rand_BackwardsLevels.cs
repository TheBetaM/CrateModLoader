using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash2;

namespace CrateModLoader.GameSpecific.Crash2
{
    // todo: sort out boss randomizer stuff
    public class Crash2_Rand_BackwardsLevels : ModStruct<NSF_Pair>
    {
        public override string Name => Crash2_Text.Mod_BackwardsLevels;
        public override string Description => Crash2_Text.Mod_BackwardsLevelsDesc;

        private Random rand;
        private bool isRandom;

        private List<Crash2_Levels> BackwardsLevelsList = new List<Crash2_Levels>()
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

        private List<Crash2_Levels> BackwardsCameraList = new List<Crash2_Levels>()
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

        public Crash2_Rand_BackwardsLevels(bool isrand)
        {
            isRandom = isrand;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            Crash2_Levels level = pair.LevelC2;
            if (!BackwardsLevelsList.Contains(level))
            {
                return;
            }
            if (isRandom && rand.Next(2) == 0)
            {
                return;
            }
            if (Crash2_Common.BossLevelsList.Contains(level))
            {
                //Mod_RandomizeBosses(nsf, nsd, level, rand, true);
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

            if (Crash2_Common.ChaseLevelsList.Contains(level)) // bugged counter
            {
                Debug_DontMoveCounter = true;
            }

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            for (int i = 0; i < zone.Entities.Count; i++)
                            {

                                if (Crash2_Common.DarknessLevelsList.Contains(level))
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

                                    if (Crash2_Common.BoardLevelsList.Contains(level))
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

                                    if (Crash2_Common.ChaseLevelsList.Contains(level))
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

                                    if (Crash2_Common.BeeLevelsList.Contains(level)) // temporary until flipped camera is working
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
                                    Crash2_Common.CreateEntity(id, 34, 5, 854, 1149, 450, ref zone);
                                    Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, id);
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

            //Mod_VehicleLevelsOnFoot(nsf, nsd, level);

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

            foreach (Chunk chunk in pair.nsf.Chunks)
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
                                    Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, (int)BoxCounterEntity.ID);
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
                                    Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, id);
                                }
                                else if (zone.EName == "06_gZ")
                                {
                                    Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, id);
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

                foreach (Chunk chunk in pair.nsf.Chunks)
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

                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, 401);
                                    }
                                    else if (zone.EName == "b1_eZ")
                                    {
                                        zone.Entities.Add(ElevEnt);
                                        zone.EntityCount++;

                                        Crash2_Common.AddToDrawList(ref pair.nsf, ref zone, 400);
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

                pair.nsd.Spawns[0].ZoneEID = WarpOutZone.EID;
                pair.nsd.Spawns[0].SpawnX = (xoffset + WarpOutPos.X * 4) << 8;
                pair.nsd.Spawns[0].SpawnY = (yoffset + WarpOutPos.Y * 4) << 8;
                pair.nsd.Spawns[0].SpawnZ = (zoffset + WarpOutPos.Z * 4) << 8;
            }
        }

    }
}
