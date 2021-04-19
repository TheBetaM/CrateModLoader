using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: unfinished, use modder's Random state, randomize per file
    public class TS_Rand_WumpaIntoCrates : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Rand_WumpaIntoCrates;
        public override string Description => Twins_Text.Rand_WumpaIntoCratesDesc;
        public override bool Hidden => true;

        private bool isRandom = true;

        private List<ObjectID> CratesForWumpaToTurnInto = new List<ObjectID>()
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


        public override void BeforeModPass()
        {
            isRandom = true;
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;
            ChunkType chunkType = info.Type;
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
            Random randState = new Random((ModLoaderGlobals.RandomizerSeed + (int)chunkType) % int.MaxValue);
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
