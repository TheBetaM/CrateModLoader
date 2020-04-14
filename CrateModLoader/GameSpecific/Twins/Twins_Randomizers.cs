using System;
using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.Twins
{
    public static class Twins_Randomizers
    {

        public static void RM_Randomize_Crates(TwinsFile RM_Archive, ChunkType chunkType, ref Random randState, ref List<uint> CrateReplaceList, ref List<uint> randCrateList)
        {
            randState = new Random((Program.ModProgram.randoSeed + (int)chunkType) % int.MaxValue);
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
                                    instance.UnkI32 = 0x4011E;
                                    instance.UnkI322 = new List<float>() { 1, 50, 10 };
                                }
                                else if (target_item == (int)ObjectID.BASICCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.EXTRALIFECRATE || target_item == (int)ObjectID.EXTRALIFECRATECORTEX || target_item == (int)ObjectID.EXTRALIFECRATENINA)
                                {
                                    instance.UnkI32 = 0x81DE;
                                }
                                else if (target_item == (int)ObjectID.WOODENSPRINGCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.REINFORCEDWOODENCRATE)
                                {
                                    instance.UnkI32 = 0xD91E;
                                }
                                else if (target_item == (int)ObjectID.AKUAKUCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.IRONCRATE)
                                {
                                    instance.UnkI32 = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.IRONSPRINGCRATE)
                                {
                                    instance.UnkI32 = 0x7D1E;
                                }
                                else if (target_item == (int)ObjectID.MULTIPLEHITCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.SURPRISECRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.TNTCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
                                }
                                else if (target_item == (int)ObjectID.NITROCRATE)
                                {
                                    instance.UnkI32 = 0x811E;
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
                    NewGem.UnkI32 = 0x1CE;
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
                            instance.UnkI32 = template.Properties;
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

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)CharacterID.Crash];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)CharacterID.Crash];
                            instance.UnkI323[2] = Twins_Data.CharInts_SpawnHealth[(int)CharacterID.Crash];

                            //instance.UnkI322[(int)CharacterInstanceFloats.Static1] = 0; // 1

                        }
                        else if (instance.ObjectID == (uint)ObjectID.CORTEX)
                        {
                            // Cortex mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)CharacterID.Cortex];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)CharacterID.Cortex];
                            instance.UnkI323[2] = Twins_Data.CharInts_SpawnHealth[(int)CharacterID.Cortex];

                            //instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = 0.4f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.NINA)
                        {
                            // Nina mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)CharacterID.Nina];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)CharacterID.Nina];
                            instance.UnkI323[2] = Twins_Data.CharInts_SpawnHealth[(int)CharacterID.Nina];

                            //instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = 1.75f;

                        }
                        else if (instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            // Mechabandicoot mods

                            instance.UnkI322[(int)CharacterInstanceFloats.AirGravity] = Twins_Data.CharFloats_AirGravity[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BaseGravity] = Twins_Data.CharFloats_BaseGravity[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamGravityForce] = Twins_Data.CharFloats_BodyslamGravityForce[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamHangTime] = Twins_Data.CharFloats_BodyslamHangTime[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.BodyslamUpwardForce] = Twins_Data.CharFloats_BodyslamUpwardForce[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlSpeed] = Twins_Data.CharFloats_CrawlSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeFromStand] = Twins_Data.CharFloats_CrawlTimeFromStand[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToRun] = Twins_Data.CharFloats_CrawlTimeToRun[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.CrawlTimeToStand] = Twins_Data.CharFloats_CrawlTimeToStand[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpArcUnk] = Twins_Data.CharFloats_DoubleJumpArcUnk[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpHeight] = Twins_Data.CharFloats_DoubleJumpHeight[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.DoubleJumpUnk22] = Twins_Data.CharFloats_DoubleJumpUnk22[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickForwardSpeed] = Twins_Data.CharFloats_FlyingKickForwardSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickGravity] = Twins_Data.CharFloats_FlyingKickGravity[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.FlyingKickHangTime] = Twins_Data.CharFloats_FlyingKickHangTime[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunButtonHoldTimeToStartCharging] = Twins_Data.CharFloats_GunButtonHoldTimeToStartCharging[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunChargeTime] = Twins_Data.CharFloats_GunChargeTime[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenChargedShots] = Twins_Data.CharFloats_GunTimeBetweenChargedShots[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.GunTimeBetweenShots] = Twins_Data.CharFloats_GunTimeBetweenShots[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpAirSpeed] = Twins_Data.CharFloats_JumpAirSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk18] = Twins_Data.CharFloats_JumpArcUnk18[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpArcUnk19] = Twins_Data.CharFloats_JumpArcUnk19[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpEdgeSpeed] = Twins_Data.CharFloats_JumpEdgeSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.JumpHeight] = Twins_Data.CharFloats_JumpHeight[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastChargeTime] = Twins_Data.CharFloats_RadialBlastChargeTime[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastTimeToStart] = Twins_Data.CharFloats_RadialBlastTimeToStart[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk39] = Twins_Data.CharFloats_RadialBlastUnk39[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RadialBlastUnk40] = Twins_Data.CharFloats_RadialBlastUnk40[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.RunSpeed] = Twins_Data.CharFloats_RunSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk24] = Twins_Data.CharFloats_SlideJumpUnk24[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk25] = Twins_Data.CharFloats_SlideJumpUnk25[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk26] = Twins_Data.CharFloats_SlideJumpUnk26[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideJumpUnk27] = Twins_Data.CharFloats_SlideJumpUnk27[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime] = Twins_Data.CharFloats_SlideSlowdownTime[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime2] = Twins_Data.CharFloats_SlideSlowdownTime2[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSlowdownTime3] = Twins_Data.CharFloats_SlideSlowdownTime3[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideSpeed] = Twins_Data.CharFloats_SlideSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk49] = Twins_Data.CharFloats_SlideUnk49[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SlideUnk50] = Twins_Data.CharFloats_SlideUnk50[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinDelay] = Twins_Data.CharFloats_SpinDelay[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinLength] = Twins_Data.CharFloats_SpinLength[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.SpinThrowForwardForce] = Twins_Data.CharFloats_SpinThrowForwardForce[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = Twins_Data.CharFloats_StrafingSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeed] = Twins_Data.CharFloats_WalkSpeed[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.WalkSpeedPercentage] = Twins_Data.CharFloats_WalkSpeedPercentage[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static1] = Twins_Data.CharFloats_Static1[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static15] = Twins_Data.CharFloats_Static15[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Static6] = Twins_Data.CharFloats_Static6[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk13] = Twins_Data.CharFloats_Unk13[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk14] = Twins_Data.CharFloats_Unk14[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk28] = Twins_Data.CharFloats_Unk28[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk29] = Twins_Data.CharFloats_Unk29[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk3] = Twins_Data.CharFloats_Unk3[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk30] = Twins_Data.CharFloats_Unk30[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk31] = Twins_Data.CharFloats_Unk31[(int)CharacterID.Mechabandicoot];
                            instance.UnkI322[(int)CharacterInstanceFloats.Unk55] = Twins_Data.CharFloats_Unk55[(int)CharacterID.Mechabandicoot];
                            instance.UnkI323[2] = Twins_Data.CharInts_SpawnHealth[(int)CharacterID.Mechabandicoot];

                            //instance.UnkI322[(int)CharacterInstanceFloats.StrafingSpeed] = 10;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }




    }
}
