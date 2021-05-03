using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: randomize per file, test
    public class TS_Rand_Crates : ModStruct<ChunkInfoRM>
    {
        public override bool NeedsCachePass => true;

        internal List<uint> CrateReplaceList = new List<uint>();
        internal List<uint> randCrateList = new List<uint>();

        private List<ObjectID> exports;

        public override void BeforeCachePass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);

            if (TS_Props_Main.Option_RandCrates.Value == 1)
            {

                // Crates to insert
                randCrateList = new List<uint>();
                randCrateList.Add((uint)ObjectID.BASICCRATE);
                //randCrateList.Add((uint)ObjectID.TNT_CRATE);
                //randCrateList.Add((uint)ObjectID.NITRO_CRATE);
                randCrateList.Add((uint)ObjectID.EXTRALIFECRATE);
                randCrateList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                randCrateList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                randCrateList.Add((uint)ObjectID.AKUAKUCRATE);
                randCrateList.Add((uint)ObjectID.IRONCRATE);
                randCrateList.Add((uint)ObjectID.IRONSPRINGCRATE);
                randCrateList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                randCrateList.Add((uint)ObjectID.SURPRISECRATE);
                randCrateList.Add((uint)ObjectID.AMMOCRATESMALL);

                // Crates to replace
                CrateReplaceList = new List<uint>();
                CrateReplaceList.Add((uint)ObjectID.BASICCRATE);
                //CrateReplaceList.Add((uint)ObjectID.TNT_CRATE);
                //CrateReplaceList.Add((uint)ObjectID.NITRO_CRATE);
                CrateReplaceList.Add((uint)ObjectID.EXTRALIFECRATE);
                CrateReplaceList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                CrateReplaceList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                CrateReplaceList.Add((uint)ObjectID.AKUAKUCRATE);
                //CrateReplaceList.Add((uint)ObjectID.IRON_CRATE);
                //CrateReplaceList.Add((uint)ObjectID.IRON_SPRING_CRATE);
                CrateReplaceList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                CrateReplaceList.Add((uint)ObjectID.SURPRISECRATE);
                //CrateReplaceList.Add((uint)ObjectID.AMMOCRATESMALL);

            }

        }

        public override void CachePass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;
            ChunkType chunk = info.Type;
            Random randState = new Random();

            if (chunk == ChunkType.Default)
            {
                if (TS_Props_Main.Option_RandCrates.Value == 2)
                {
                    List<uint> crateList = new List<uint>();
                    List<uint> posList = new List<uint>();

                    crateList.Add((uint)ObjectID.BASICCRATE);
                    //crateList.Add((uint)ObjectID.TNT_CRATE);
                    //crateList.Add((uint)ObjectID.NITRO_CRATE);
                    crateList.Add((uint)ObjectID.EXTRALIFECRATE);
                    crateList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                    crateList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                    crateList.Add((uint)ObjectID.AKUAKUCRATE);
                    //crateList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                    //crateList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_NINA);
                    //crateList.Add((uint)ObjectID.IRONCRATE);
                    //crateList.Add((uint)ObjectID.IRONSPRINGCRATE);
                    crateList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                    crateList.Add((uint)ObjectID.SURPRISECRATE);

                    posList.Add((uint)ObjectID.BASICCRATE);
                    //posList.Add((uint)ObjectID.TNT_CRATE);
                    //posList.Add((uint)ObjectID.NITRO_CRATE);
                    posList.Add((uint)ObjectID.EXTRALIFECRATE);
                    posList.Add((uint)ObjectID.WOODENSPRINGCRATE);
                    posList.Add((uint)ObjectID.REINFORCEDWOODENCRATE);
                    posList.Add((uint)ObjectID.AKUAKUCRATE);
                    //posList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_CORTEX);
                    //posList.Add((uint)ObjectID.EXTRA_LIFE_CRATE_NINA);
                    //posList.Add((uint)ObjectID.IRONCRATE);
                    //posList.Add((uint)ObjectID.IRONSPRINGCRATE);
                    posList.Add((uint)ObjectID.MULTIPLEHITCRATE);
                    posList.Add((uint)ObjectID.SURPRISECRATE);

                    int target_item = 0;

                    while (posList.Count > 0)
                    {
                        target_item = randState.Next(0, crateList.Count);
                        TwinsSection objectdata = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code).GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
                        if (objectdata.ContainsItem(posList[0]))
                            objectdata.GetItem<TwinsItem>(posList[0]).ID = crateList[target_item];
                        posList.RemoveAt(0);
                        crateList.RemoveAt(target_item);
                    }
                    posList.Clear();
                    crateList.Clear();
                }
            }
            else if (chunk == ChunkType.School_Cortex_CoGPA01)
            {
                exports = new List<ObjectID>();
                Twins_Data.ExportGameObject(ref RM_Archive, ObjectID.AMMOCRATESMALL, ref exports);

                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnPhysicsCollision] = 65535;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnTouch] = 65535;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnTrigger] = (ushort)ScriptID.HEAD_COM_GENERIC_CRATE_TRIGGER_NEXT;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnDamage] = (ushort)ScriptID.HEAD_COM_AMMO_CRATE_SMALL_TOUCHED;
                Twins_Data.cachedGameObjects[0].mainObject.Scripts[(ushort)GameObjectScriptOrder.OnLand] = (ushort)ScriptID.HEAD_COM_BASIC_CRATE_LANDED_ON;
            }
            
            
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;
            ChunkType chunkType = info.Type;

            if (chunkType == ChunkType.Default)
            {
                if (TS_Props_Main.Option_RandCrates.Value == 1)
                {
                    //Importing ammo crate
                    Twins_Data.ImportGameObject(ref RM_Archive, ObjectID.AMMOCRATESMALL, ref exports);
                    exports.Clear();
                }
            }

            List<ChunkType> BannedChunks = new List<ChunkType>()
            {
                ChunkType.School_Rooftop_BusChase,
            };
            if (BannedChunks.Contains(chunkType))
                return;

            Random randState = new Random((ModLoaderGlobals.RandomizerSeed + (int)chunkType) % int.MaxValue);
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
    }
}
