using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: use modder's Random state, randomize per file
    public class TS_Rand_Enemies : ModStruct<ChunkInfoRM>
    {
        public override string Name => "Randomize Enemies (Soundless)";
        public override string Description => "";
        public override CreditContributors Contributors => new CreditContributors(ModLoaderGlobals.Contributor_BetaM);
        public override bool Hidden => true;
        public override bool NeedsCachePass => true;

        internal List<ObjectID> EnemyReplaceList = new List<ObjectID>();
        internal List<ObjectID> EnemyInsertList = new List<ObjectID>();
        private Random randState = new Random();

        public override void CachePass(ChunkInfoRM info)
        {
            // loaded default
            TwinsFile RM_Archive = info.File;
            ChunkType chunkType = info.Type;

            if (chunkType == ChunkType.Default)
            {
                EnemyReplaceList.Add(ObjectID.GLOBAL_MONKEY);
                EnemyReplaceList.Add(ObjectID.GLOBAL_CHICKEN);
                EnemyReplaceList.Add(ObjectID.GLOBAL_CRAB);
                EnemyReplaceList.Add(ObjectID.GLOBAL_SKUNK);
                //EnemyReplaceList.Add(ObjectID.EARTH_TRIBESMAN_SHIELDBEARER); // because it may softlock in JB
                EnemyReplaceList.Add(ObjectID.EARTH_TRIBESMAN);
                //EnemyReplaceList.Add(ObjectID.GLOBAL_BAT_DARKPURPLE); // because it may crash in JB
                EnemyReplaceList.Add(ObjectID.GLOBAL_BAT_ICE);
                EnemyReplaceList.Add(ObjectID.PIRANHAPLANT);
                EnemyReplaceList.Add(ObjectID.GLOBAL_PIG_WILDBOAR);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_INTERMEDIATE);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_DARKBROWN);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_DARKPURPLE);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_GREY);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_LIGHTBROWN);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_LIGHTPURPLE);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_MEDIUMBROWN);
                EnemyReplaceList.Add(ObjectID.GLOBAL_RAT_WHITE);
                EnemyReplaceList.Add(ObjectID.PENGUIN);
                EnemyReplaceList.Add(ObjectID.GLOBAL_CORTEX_CAMERABOT);
                //EnemyReplaceList.Add(ObjectID.RHINO_PIRATE); // maybe once the melee variation is figured out
                EnemyReplaceList.Add(ObjectID.SCHOOL_DOG);
                EnemyReplaceList.Add(ObjectID.GLOBAL_COCKROACH);
                EnemyReplaceList.Add(ObjectID.GLOBAL_BEETLE_DARKPURPLE);
                EnemyReplaceList.Add(ObjectID.GLOBAL_BEETLE_PROJECTILE);
                EnemyReplaceList.Add(ObjectID.SCHOOL_FROGENSTEIN);
                EnemyReplaceList.Add(ObjectID.SCHOOL_ZOMBOT);
                EnemyReplaceList.Add(ObjectID.DRONE_BASIC);
                EnemyReplaceList.Add(ObjectID.DRONE_DRILLER);
                EnemyReplaceList.Add(ObjectID.DRONE_FLAMER);
                EnemyReplaceList.Add(ObjectID.DRONE_FLYER);
                EnemyReplaceList.Add(ObjectID.DRONE_SOLDIER);
                EnemyReplaceList.Add(ObjectID.DRONE_BERSERKER);

                EnemyInsertList.Add(ObjectID.GLOBAL_CHICKEN); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_MONKEY); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_SKUNK); // works
                EnemyInsertList.Add(ObjectID.EARTH_TRIBESMAN_SHIELDBEARER); // works (but has 1 HP)
                EnemyInsertList.Add(ObjectID.EARTH_TRIBESMAN); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_BAT_DARKPURPLE); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_BAT_ICE); // works
                EnemyInsertList.Add(ObjectID.PIRANHAPLANT); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_RAT_INTERMEDIATE); // works
                EnemyInsertList.Add(ObjectID.PENGUIN); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_CORTEX_CAMERABOT); // works
                EnemyInsertList.Add(ObjectID.SCHOOL_DOG); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_COCKROACH); // works
                EnemyInsertList.Add(ObjectID.GLOBAL_BEETLE_PROJECTILE); // works
                EnemyInsertList.Add(ObjectID.SCHOOL_ZOMBOT); // works
                EnemyInsertList.Add(ObjectID.SCHOOL_FROGENSTEIN); // works
                EnemyInsertList.Add(ObjectID.DRONE_BASIC); // works, but only spawns when triggered
                EnemyInsertList.Add(ObjectID.DRONE_FLAMER); // works
                EnemyInsertList.Add(ObjectID.DRONE_BERSERKER); // works
                EnemyInsertList.Add(ObjectID.DRONE_FLYER); // works

                //EnemyInsertList.Add(ObjectID.DRONE_SOLDIER); // doesn't work (crashed on spawn)
                //EnemyInsertList.Add(ObjectID.DRONE_DRILLER); // doesn't work (crashed on spawn)
                //EnemyInsertList.Add(ObjectID.GLOBAL_PIG_WILDBOAR); // doesn't work (minor errors, crashed when switching chunk)

                //EnemyInsertList.Add(ObjectID.SCHOOL_JANITOR); // works, but lags the game and can't be defeated
                //EnemyInsertList.Add(ObjectID.RHINO_PIRATE); // works, but stands in place throwing barrels to the same spot
                //EnemyInsertList.Add(ObjectID.MINI_MON); // works with errors, just rushes you
                //EnemyInsertList.Add(ObjectID.GLOBAL_CRAB); // works with errors, only spawns near AIpositions?
                //EnemyInsertList.Add(ObjectID.GLOBAL_BEETLE_DARKPURPLE); // works, pretty much the same as GLOBAL_BEETLE_PROJECTILE?

                for (int i = 0; i < Twins_Data.cachedGameObjects.Count; i++)
                {
                    //soundless objects - temporary workaround because sound import/export is broken
                    if (Twins_Data.cachedGameObjects[i].mainObject.cSounds.Count > 0)
                    {
                        for (int a = 0; a < Twins_Data.cachedGameObjects[i].mainObject.cSounds.Count; a++)
                        {
                            Twins_Data.cachedGameObjects[i].mainObject.cSounds[a] = 20;
                        }
                    }
                    if (Twins_Data.cachedGameObjects[i].mainObject.Sounds.Count > 0)
                    {
                        for (int s = 0; s < Twins_Data.cachedGameObjects[i].mainObject.Sounds.Count; s++)
                        {
                            Twins_Data.cachedGameObjects[i].mainObject.Sounds[s] = 65535;
                        }
                    }
                    if (Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.GLOBAL_BAT_DARKPURPLE || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.SCHOOL_FROGENSTEIN || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.GLOBAL_SKUNK || Twins_Data.cachedGameObjects[i].mainObject.ID == (uint)ObjectID.SCHOOL_JANITOR)
                    {
                        Twins_Data.cachedGameObjects[i] = new CachedGameObject()
                        {
                            mainObject = Twins_Data.cachedGameObjects[i].mainObject,
                            instanceTemplate = new InstanceTemplate()
                            {
                                Properties = 0x188B2E, //Twins_Data.cachedGameObjects[i].instanceTemplate.Properties - (uint)Twins_Data.PropertyFlags.DisableObject,
                                Flags = Twins_Data.cachedGameObjects[i].instanceTemplate.Flags,
                                FloatVars = Twins_Data.cachedGameObjects[i].instanceTemplate.FloatVars,
                                InstanceIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.InstanceIDs,
                                InstancesNum = Twins_Data.cachedGameObjects[i].instanceTemplate.InstancesNum,
                                IntVars = Twins_Data.cachedGameObjects[i].instanceTemplate.IntVars,
                                ObjectID = Twins_Data.cachedGameObjects[i].instanceTemplate.ObjectID,
                                PathIDs = Twins_Data.cachedGameObjects[i].instanceTemplate.PathIDs,
                                PathsNum = Twins_Data.cachedGameObjects[i].instanceTemplate.PathsNum,
                                PositionIDs = new List<ushort>(), //Twins_Data.cachedGameObjects[i].instanceTemplate.PositionIDs,
                                PositionsNum = Twins_Data.cachedGameObjects[i].instanceTemplate.PositionsNum
                            },
                            list_blendskins = Twins_Data.cachedGameObjects[i].list_blendskins,
                            list_anims = Twins_Data.cachedGameObjects[i].list_anims,
                            list_skins = Twins_Data.cachedGameObjects[i].list_skins,
                            list_scriptpacks = Twins_Data.cachedGameObjects[i].list_scriptpacks,
                            list_materials = Twins_Data.cachedGameObjects[i].list_materials,
                            list_models = Twins_Data.cachedGameObjects[i].list_models,
                            list_rigidmodels = Twins_Data.cachedGameObjects[i].list_rigidmodels,
                            list_ogi = Twins_Data.cachedGameObjects[i].list_ogi,
                            list_scripts = Twins_Data.cachedGameObjects[i].list_scripts,
                            list_sounds = Twins_Data.cachedGameObjects[i].list_sounds,
                            list_sounds_english = Twins_Data.cachedGameObjects[i].list_sounds_english,
                            list_sounds_french = Twins_Data.cachedGameObjects[i].list_sounds_french,
                            list_sounds_german = Twins_Data.cachedGameObjects[i].list_sounds_german,
                            list_sounds_italian = Twins_Data.cachedGameObjects[i].list_sounds_italian,
                            list_sounds_spanish = Twins_Data.cachedGameObjects[i].list_sounds_spanish,
                            list_sounds_unused = Twins_Data.cachedGameObjects[i].list_sounds_unused,
                            list_subobjects = Twins_Data.cachedGameObjects[i].list_subobjects,
                            list_textures = Twins_Data.cachedGameObjects[i].list_textures
                        };
                    }
                }
            }
            else
            {
                List<ObjectID> ExportedObjects = new List<ObjectID>();
                if (chunkType == ChunkType.Earth_Hub_Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_MONKEY, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CHICKEN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CRAB, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Earth_Hub_HubB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_SKUNK, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.PIRANHAPLANT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BAT_DARKPURPLE, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Earth_Hub_HubA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.EARTH_TRIBESMAN_SHIELDBEARER, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Earth_Totem_L03Beach)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.EARTH_TRIBESMAN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_PIG_WILDBOAR, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Ice_HighSeas_GPA07)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_RAT_INTERMEDIATE, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Earth_Hub_HubD)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.MINI_MON, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Ice_IceClimb_BergExt)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.PENGUIN, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_CORTEX_CAMERABOT, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BAT_ICE, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.Ice_HighSeas_GPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.RHINO_PIRATE, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.School_Crash_CrGPA03)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_FROGENSTEIN, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.School_Crash_CrGPA04)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_DOG, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.School_Crash_CrLib)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_ZOMBOT, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.School_Boiler_Boiler_X)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_COCKROACH, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BEETLE_PROJECTILE, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.AltEarth_Core_CoreA)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_BERSERKER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_DRILLER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_SOLDIER, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.AltEarth_Core_CoreB)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_FLYER, ref ExportedObjects);
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_FLAMER, ref ExportedObjects);
                }
                else if (chunkType == ChunkType.School_Rooftop_BusChase)
                {
                    Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.DRONE_BASIC, ref ExportedObjects);
                }
                //else if (chunkType == ChunkType.School_Cortex_CoGPA03)
                //{
                //Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.SCHOOL_JANITOR, ref ExportedObjects);
                //}
                //else if (chunkType == ChunkType.School_Boiler_Boiler_2)
                //{
                //Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.GLOBAL_BEETLE_DARKPURPLE, ref ExportedObjects);
                //}
            }

        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;
            ChunkType chunkType = info.Type;

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
    }
}
