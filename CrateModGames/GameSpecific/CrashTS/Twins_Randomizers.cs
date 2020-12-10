using System;
using System.Collections.Generic;
using System.Drawing;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public static class Twins_Randomizers
    {

        public static void RM_Randomize_Crates(TwinsFile RM_Archive, ChunkType chunkType, ref Random randState, ref List<uint> CrateReplaceList, ref List<uint> randCrateList)
        {
            List<ChunkType> BannedChunks = new List<ChunkType>()
            {
                ChunkType.School_Rooftop_BusChase,
            };
            if (BannedChunks.Contains(chunkType))
                return;

            randState = new Random((ModLoaderGlobals.RandomizerSeed + (int)chunkType) % int.MaxValue);
            List<uint> lifecrates = new List<uint>
            {
                (uint)ObjectID.EXTRALIFECRATE,
                (uint)ObjectID.EXTRALIFECRATECORTEX,
                (uint)ObjectID.EXTRALIFECRATENINA
            };
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int d = 0; d < CrateReplaceList.Count; d++)
                        {
                            if (instance.ObjectID == CrateReplaceList[d])
                            {
                                int target_item = randState.Next(0, randCrateList.Count);
                                if (randCrateList[target_item] == (int)ObjectID.EXTRALIFECRATE)
                                {
                                    int target_life = randState.Next(0, lifecrates.Count);
                                    target_item = (int)lifecrates[target_life];
                                }
                                else
                                {
                                    target_item = (int)randCrateList[target_item];
                                }

                                if (target_item == (int)ObjectID.AMMOCRATESMALL)
                                {
                                    instance.Flags = 0x4011E;
                                    instance.UnkI322 = new List<float>() { 1, 50, 10 };
                                }
                                else if (target_item == (int)ObjectID.BASICCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.EXTRALIFECRATE || target_item == (int)ObjectID.EXTRALIFECRATECORTEX || target_item == (int)ObjectID.EXTRALIFECRATENINA)
                                {
                                    instance.Flags = 0x81DE;
                                }
                                else if (target_item == (int)ObjectID.WOODENSPRINGCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.REINFORCEDWOODENCRATE)
                                {
                                    instance.Flags = 0xD91E;
                                }
                                else if (target_item == (int)ObjectID.AKUAKUCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.IRONCRATE)
                                {
                                    instance.Flags = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.IRONSPRINGCRATE)
                                {
                                    instance.Flags = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.MULTIPLEHITCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.SURPRISECRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.TNTCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.NITROCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }

                                instance.ObjectID = (ushort)target_item;
                                break;
                            }
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_Randomize_Gems(TwinsFile RM_Archive, ChunkType chunkType, ref List<uint> gemObjectList)
        {
            if (chunkType == ChunkType.Invalid)
            {
                Console.WriteLine("INVALID CHUNK FILE: " + RM_Archive.FileName);
                return;
            }

            // Part 1: Remove existing gems

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int d = 0; d < gemObjectList.Count; d++)
                        {
                            if (instance.ObjectID == gemObjectList[d])
                            {
                                instance.Pos.Y = instance.Pos.Y - 1000f; //todo: figure out how to get rid of them gracefully

                                /* Used this to generate vanilla gem locations instead of checking one-by-one
                                if (instance.ObjectID == (ushort)GemID.GEM_BLUE)
                                {
                                    Console.WriteLine("new TwinsGem(ChunkType." + chunkType + ",GemType.GEM_BLUE,new Vector3(" + instance.Pos.X + "f," + instance.Pos.Y + "f," + instance.Pos.Z + "f)),");
                                }
                                */

                                break;
                            }
                        }
                        instances.Records[i] = instance;
                    }
                }
            }

            // Part 2: Add new gems
            uint gem_section_id = (uint)RM_Sections.Instances1;
            if (!RM_Archive.ContainsItem(gem_section_id)) return;
            TwinsSection instances_group = RM_Archive.GetItem<TwinsSection>(gem_section_id);
            TwinsSection instances_section;
            if (instances_group.Records.Count > 0)
            {
                if (!instances_group.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) return;
                instances_section = instances_group.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
            }
            else
            {
                return;
            }


            for (int i = 0; i < Twins_Data.All_Gems.Count; i++)
            {
                if (Twins_Data.All_Gems[i].chunk == chunkType)
                {
                    Instance NewGem = new Instance();
                    NewGem.Pos = new Pos(Twins_Data.All_Gems[i].pos.X, Twins_Data.All_Gems[i].pos.Y, Twins_Data.All_Gems[i].pos.Z, 1f);
                    if (Twins_Data.All_Gems[i].type == GemType.GEM_BLUE)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_BLUE;
                    }
                    else if (Twins_Data.All_Gems[i].type == GemType.GEM_CLEAR)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_CLEAR;
                    }
                    else if (Twins_Data.All_Gems[i].type == GemType.GEM_GREEN)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_GREEN;
                    }
                    else if (Twins_Data.All_Gems[i].type == GemType.GEM_PURPLE)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_PURPLE;
                    }
                    else if (Twins_Data.All_Gems[i].type == GemType.GEM_RED)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_RED;
                    }
                    else if (Twins_Data.All_Gems[i].type == GemType.GEM_YELLOW)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_YELLOW;
                    }
                    NewGem.ID = (uint)instances_section.Records.Count;
                    NewGem.SomeNum1 = 10;
                    NewGem.SomeNum2 = 10;
                    NewGem.SomeNum3 = 10;
                    NewGem.AfterOID = uint.MaxValue;
                    NewGem.Flags = 0x1CE;
                    NewGem.UnkI322 = new List<float>() { 1 };
                    NewGem.UnkI323 = new List<uint>() { 0, 255, (uint)Twins_Data.All_Gems[i].type };

                    instances_section.Records.Add(NewGem);
                }
            }
        }

        public static void RM_Randomize_Music(TwinsFile RM_Archive, ref List<uint> musicTypes, ref List<uint> randMusicList)
        {
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (ushort)ObjectID.DJ)
                        {
                            uint sourceMusic = instance.UnkI323[0];
                            uint targetMusic = (uint)MusicID.TitleTheme;
                            for (int m = 0; m < musicTypes.Count; m++)
                            {
                                if (musicTypes[m] == sourceMusic)
                                {
                                    targetMusic = randMusicList[m];
                                }
                            }
                            instance.UnkI323 = new List<uint>() { targetMusic, 255, instance.UnkI323[2] };

                            break;
                        }
                        else if (instance.ObjectID == (ushort)ObjectID.DJ_TRIGGERABLE)
                        {
                            uint sourceMusic = instance.UnkI323[0];
                            uint targetMusic = (uint)MusicID.TitleTheme;
                            for (int m = 0; m < musicTypes.Count; m++)
                            {
                                if (musicTypes[m] == sourceMusic)
                                {
                                    targetMusic = randMusicList[m];
                                }
                            }
                            instance.UnkI323 = new List<uint>() { targetMusic, 255 };

                            break;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }

        public static void RM_Randomize_Enemies(TwinsFile RM_Archive, ChunkType chunkType, ref Random randState, ref List<ObjectID> EnemyReplaceList, ref List<ObjectID> EnemyInsertList)
        {
            List<ObjectID> importedObjects = new List<ObjectID>();
            bool EnemyFound = false;
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        for (int obj = 0; obj < EnemyReplaceList.Count; obj++)
                        {
                            if (instance.ObjectID == (uint)EnemyReplaceList[obj])
                            {
                                EnemyFound = true;
                            }
                        }
                        if (EnemyFound)
                        {
                            int targetPos = randState.Next(0, EnemyInsertList.Count);
                            ObjectID targetObjectID = EnemyInsertList[targetPos];
                            Twins_Data.ImportGameObject(ref RM_Archive, targetObjectID, ref importedObjects);
                            InstanceTemplate template = Twins_Data.GetInstanceTemplateByObjectID(targetObjectID);
                            if (template.ObjectID == 0 && template.Properties == 0) //&& instance.SomeNum1 == 0)
                            {
                                // For objects that are placed at runtime
                                template = new InstanceTemplate()
                                {
                                    ObjectID = (ushort)targetObjectID,
                                    InstancesNum = 10,
                                    PathsNum = 10,
                                    PositionsNum = 10,
                                    Properties = 0x188B2E,
                                    Flags = new List<uint>() { 10000 },
                                    FloatVars = new List<float>() { 1, 25, 1.4f, 15, 100, 0, 6, 6 },
                                    IntVars = new List<uint>() { 0, 0, 1 },
                                    InstanceIDs = new List<ushort>(),
                                    PathIDs = new List<ushort>(),
                                    PositionIDs = new List<ushort>(),
                                };
                            }
                            instance.ObjectID = template.ObjectID;
                            instance.SomeNum1 = template.InstancesNum;
                            instance.SomeNum2 = template.PathsNum;
                            instance.SomeNum3 = template.PositionsNum;
                            instance.Flags = template.Properties;
                            instance.UnkI321 = template.Flags;
                            instance.UnkI322 = template.FloatVars;
                            instance.UnkI323 = template.IntVars;
                            instance.InstanceIDs = new List<ushort>();
                            /*
                            if (template.InstanceIDs.Count > 0)
                            {
                                for (int a = 0; a < template.InstanceIDs.Count; a++)
                                {
                                    instance.InstanceIDs.Add(0);
                                }
                            }
                            */
                            instance.PathIDs = new List<ushort>();
                            /*
                            if (template.PathIDs.Count > 0)
                            {
                                for (int a = 0; a < template.PathIDs.Count; a++)
                                {
                                    instance.PathIDs.Add(0);
                                }
                            }
                            */
                            instance.PositionIDs = new List<ushort>();
                            /*
                            if (template.PositionIDs.Count > 0)
                            {
                                for (int a = 0; a < template.PositionIDs.Count; a++)
                                {
                                    instance.PositionIDs.Add(0);
                                }
                            }
                            */
                        }
                        instances.Records[i] = instance;
                        EnemyFound = false;
                    }
                }
            }
        }

        public static void RM_Randomize_CharacterInstanceStats(TwinsFile RM_Archive)
        {
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)ObjectID.CRASH)
                        {
                            // Crash mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Crash];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Crash];

                            //instance.UnkI322[(int)CharacterInstanceFloats.Static1] = 0; // 1

                        }
                        else if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            // Cortex mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Cortex];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Cortex];

                            //instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = 0.4f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            // Nina mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Nina];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Nina];

                            //instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = 1.75f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data_Characters.CharFloats_AirGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data_Characters.CharFloats_BaseGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data_Characters.CharFloats_BodyslamGravityForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data_Characters.CharFloats_BodyslamHangTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data_Characters.CharFloats_BodyslamUpwardForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data_Characters.CharFloats_CrawlSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data_Characters.CharFloats_CrawlTimeFromStand.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data_Characters.CharFloats_CrawlTimeToRun.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data_Characters.CharFloats_CrawlTimeToStand.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data_Characters.CharFloats_DoubleJumpArcUnk.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data_Characters.CharFloats_DoubleJumpHeight.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data_Characters.CharFloats_DoubleJumpUnk22.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data_Characters.CharFloats_FlyingKickForwardSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data_Characters.CharFloats_FlyingKickGravity.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data_Characters.CharFloats_FlyingKickHangTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data_Characters.CharFloats_GunButtonHoldTimeToStartCharging.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data_Characters.CharFloats_GunChargeTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenChargedShots.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data_Characters.CharFloats_GunTimeBetweenShots.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data_Characters.CharFloats_JumpAirSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data_Characters.CharFloats_JumpArcUnk18.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data_Characters.CharFloats_JumpArcUnk19.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data_Characters.CharFloats_JumpEdgeSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data_Characters.CharFloats_JumpHeight.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data_Characters.CharFloats_RadialBlastChargeTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data_Characters.CharFloats_RadialBlastTimeToStart.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data_Characters.CharFloats_RadialBlastUnk39.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data_Characters.CharFloats_RadialBlastUnk40.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data_Characters.CharFloats_RunSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data_Characters.CharFloats_SlideJumpUnk24.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data_Characters.CharFloats_SlideJumpUnk25.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data_Characters.CharFloats_SlideJumpUnk26.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data_Characters.CharFloats_SlideJumpUnk27.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data_Characters.CharFloats_SlideSlowdownTime.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data_Characters.CharFloats_SlideSlowdownTime2.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data_Characters.CharFloats_SlideSlowdownTime3.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data_Characters.CharFloats_SlideSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data_Characters.CharFloats_SlideUnk49.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data_Characters.CharFloats_SlideUnk50.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data_Characters.CharFloats_SpinDelay.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data_Characters.CharFloats_SpinLength.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data_Characters.CharFloats_SpinThrowForwardForce.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data_Characters.CharFloats_StrafingSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data_Characters.CharFloats_WalkSpeed.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data_Characters.CharFloats_WalkSpeedPercentage.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data_Characters.CharFloats_Static1.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data_Characters.CharFloats_Static15.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data_Characters.CharFloats_Static6.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data_Characters.CharFloats_Unk13.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data_Characters.CharFloats_Unk14.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data_Characters.CharFloats_Unk28.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data_Characters.CharFloats_Unk29.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data_Characters.CharFloats_Unk3.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data_Characters.CharFloats_Unk30.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data_Characters.CharFloats_Unk31.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data_Characters.CharFloats_Unk55.Value[(int)CharacterID.Mechabandicoot];
                            instance.UnkI323[2] = Twins_Data_Characters.CharInts_SpawnHealth.Value[(int)CharacterID.Mechabandicoot];

                            //instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }


        public static void RM_Randomize_PantsColor(TwinsFile RM_Archive, Color PantsColor)
        {
            if (RM_Archive.Type != TwinsFile.FileType.RM2) return;
            if (!RM_Archive.ContainsItem(11)) return;
            TwinsSection section = RM_Archive.GetItem<TwinsSection>(11);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Textures) && section.Records.Count > 0)
            {
                TwinsSection tex_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);

                foreach (TwinsItem item in tex_section.Records)
                {
                    if (item.ID == 0x0B75575C || item.ID == 0x0325A8A8)
                    {
                        Texture tex = (Texture)item;
                        for (int i = 0; i < tex.RawData.Length; i++)
                        {
                            if (tex.RawData[i].B > tex.RawData[i].R + 10 && tex.RawData[i].B > tex.RawData[i].G + 10)
                            {
                                float intensity = tex.RawData[i].B / 255f;
                                tex.RawData[i] = Color.FromArgb(tex.RawData[i].A, (int)(PantsColor.R * intensity), (int)(PantsColor.G * intensity), (int)(PantsColor.B * intensity));
                            }
                        }
                        tex.UpdateImageData();
                    }
                }

            }
        }

        public static List<DefaultEnums.SurfaceTypes> SurfacesToChange = new List<DefaultEnums.SurfaceTypes>()
        {
            DefaultEnums.SurfaceTypes.SURF_DEFAULT,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY_RIGID_ONLY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_SLIGHTLY_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_ICE,
            DefaultEnums.SurfaceTypes.SURF_ICE_LOW_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_GRASS,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_METAL,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_MUD,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_ROCK,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SAND,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SNOW,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_STONE_TILES,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_WOOD,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_METAL,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_ROCK,
            //DefaultEnums.SurfaceTypes.SURF_STICKY_SNOW,
        };

        public static List<DefaultEnums.SurfaceTypes> SurfacesToPlace = new List<DefaultEnums.SurfaceTypes>()
        {
            DefaultEnums.SurfaceTypes.SURF_DEFAULT,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY_RIGID_ONLY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_SLIGHTLY_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_ICE,
            DefaultEnums.SurfaceTypes.SURF_ICE_LOW_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_GRASS,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_METAL,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_MUD,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_ROCK,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SAND,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SNOW,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_STONE_TILES,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_WOOD,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_METAL,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_ROCK,
            //DefaultEnums.SurfaceTypes.SURF_STICKY_SNOW,
        };

        public static void RM_Randomize_Surfaces(TwinsFile RM_Archive, Dictionary<int, int> randList)
        {

            if (!RM_Archive.ContainsItem(9)) return;
            TwinsItem section = RM_Archive.GetItem<TwinsItem>(9);
            ColData colData = (ColData)section;

            for (int i = 0; i < colData.Tris.Count; i++)
            {
                if (randList.ContainsKey(colData.Tris[i].Surface))
                {
                    colData.Tris[i] = new ColData.ColTri()
                    {
                        Surface = randList[colData.Tris[i].Surface],
                        Vert1 = colData.Tris[i].Vert1,
                        Vert2 = colData.Tris[i].Vert2,
                        Vert3 = colData.Tris[i].Vert3,
                    };
                }
            }

        }

        public static List<ObjectID> CratesForWumpaToTurnInto = new List<ObjectID>()
        {
            ObjectID.AKUAKUCRATE,
            ObjectID.BASICCRATE,
            ObjectID.EXTRALIFECRATE,
            ObjectID.MULTIPLEHITCRATE,
            ObjectID.NITROCRATE,
            ObjectID.SURPRISECRATE,
            ObjectID.TNTCRATE,
            ObjectID.WOODENSPRINGCRATE,
        };

        public static void RM_WumpaIntoRandomCrates(TwinsFile RM_Archive, ChunkType chunkType, ref Random randState, bool isRandom)
        {
            List<ChunkType> BannedChunks = new List<ChunkType>()
            {
                ChunkType.AltEarth_Core_Throne,
                ChunkType.School_Rooftop_BusChase,
                ChunkType.School_Rooftop_Roof01,
                ChunkType.School_Rooftop_Roof02,
                ChunkType.School_Rooftop_Roof03,
                ChunkType.School_Rooftop_Roof04,
                ChunkType.School_Rooftop_Roof05,
                ChunkType.School_Rooftop_RoofCor1,
                ChunkType.School_Rooftop_RoofCor2,
            };
            if (BannedChunks.Contains(chunkType))
                return;

            List<uint> lifecrates = new List<uint>
            {
                (uint)ObjectID.EXTRALIFECRATE,
                (uint)ObjectID.EXTRALIFECRATECORTEX,
                (uint)ObjectID.EXTRALIFECRATENINA
            };
            randState = new Random((ModLoaderGlobals.RandomizerSeed + (int)chunkType) % int.MaxValue);
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];

                        if (instance.ObjectID == (ushort)ObjectID.REDWUMPA)
                        {
                            if (!isRandom || (isRandom && randState.Next(2) == 0))
                            {
                                int target_item = randState.Next(0, CratesForWumpaToTurnInto.Count);
                                if (CratesForWumpaToTurnInto[target_item] == ObjectID.EXTRALIFECRATE)
                                {
                                    int target_life = randState.Next(0, lifecrates.Count);
                                    target_item = (int)lifecrates[target_life];
                                }
                                else
                                {
                                    target_item = (int)CratesForWumpaToTurnInto[target_item];
                                }

                                instance.UnkI322 = new List<float>() { 1, 50, 0 };

                                if (target_item == (int)ObjectID.AMMOCRATESMALL)
                                {
                                    instance.Flags = 0x4011E;
                                    instance.UnkI322 = new List<float>() { 1, 50, 10 };
                                }
                                else if (target_item == (int)ObjectID.BASICCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.EXTRALIFECRATE || target_item == (int)ObjectID.EXTRALIFECRATECORTEX || target_item == (int)ObjectID.EXTRALIFECRATENINA)
                                {
                                    instance.Flags = 0x81DE;
                                }
                                else if (target_item == (int)ObjectID.WOODENSPRINGCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.REINFORCEDWOODENCRATE)
                                {
                                    instance.Flags = 0xD91E;
                                }
                                else if (target_item == (int)ObjectID.AKUAKUCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.IRONCRATE)
                                {
                                    instance.Flags = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.IRONSPRINGCRATE)
                                {
                                    instance.Flags = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.MULTIPLEHITCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.SURPRISECRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.TNTCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.NITROCRATE)
                                {
                                    instance.Flags = 0x811E;
                                }

                                instance.ObjectID = (ushort)target_item;
                            }
                        }

                        instances.Records[i] = instance;
                    }
                }
            }
        }

    }
}
