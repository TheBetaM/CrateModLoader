using System;
using System.Collections.Generic;
using System.IO;

namespace Twinsanity
{
    public class BlendSkin : TwinsItem
    {
        public BlendSkin_Type0[] Models;
        public uint Bone_Count;

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Models.Length);
            writer.Write(Bone_Count);

            for (int sub = 0; sub < Models.Length; sub++)
            {
                writer.Write(Models[sub].SubModels.Length);
                writer.Write(Models[sub].MaterialID);
                for (int t = 0; t < Models[sub].SubModels.Length; t++)
                {
                    writer.Write(Models[sub].SubModels[t].Blob.Length);
                    writer.Write(Models[sub].SubModels[t].UnkInt);
                    writer.Write(Models[sub].SubModels[t].Blob);
                    writer.Write(Models[sub].SubModels[t].UnkData);

                    for (int b = 0; b < Bone_Count; b++)
                    {
                        writer.Write(Models[sub].SubModels[t].Bones[b].Blob.Length >> 4);
                        writer.Write(Models[sub].SubModels[t].Bones[b].UnkInt);
                        writer.Write(Models[sub].SubModels[t].Bones[b].Blob);
                    }
                }

            }

        }

        public override void Load(BinaryReader reader, int size)
        {
            long start_pos = reader.BaseStream.Position;

            uint SubModel_Count = reader.ReadUInt32();
            Bone_Count = reader.ReadUInt32();
            Models = new BlendSkin_Type0[SubModel_Count];

            for (int sub = 0; sub < SubModel_Count; sub++)
            {
                Models[sub] = new BlendSkin_Type0();
                uint Type1_Count = reader.ReadUInt32();
                Models[sub].MaterialID = reader.ReadUInt32();
                Models[sub].SubModels = new BlendSkin_Type1[Type1_Count];
                for (int t = 0; t < Type1_Count; t++)
                {
                    BlendSkin_Type1 Skin = new BlendSkin_Type1();
                    int BlobSize = reader.ReadInt32();
                    Skin.UnkInt = reader.ReadUInt32();
                    Skin.Blob = reader.ReadBytes(BlobSize);
                    Skin.UnkData = reader.ReadBytes(0xC);

                    Skin.Bones = new BlendSkin_Type2[Bone_Count];
                    for (int b = 0; b < Bone_Count; b++)
                    {
                        BlendSkin_Type2 BSkin = new BlendSkin_Type2();
                        int BSize = reader.ReadInt32();
                        BSkin.UnkInt = reader.ReadUInt32();
                        BSkin.Blob = reader.ReadBytes(BSize << 4);
                        Skin.Bones[b] = BSkin;
                    }

                    Models[sub].SubModels[t] = Skin;
                }
            }

            //Console.WriteLine("end pos: " + (reader.BaseStream.Position - start_pos) + " target: " + size);

        }

        protected override int GetSize()
        {
            int size = 8;
            for (int sub = 0; sub < Models.Length; sub++)
            {
                size += 8;
                for (int t = 0; t < Models[sub].SubModels.Length; t++)
                {
                    size += 8 + 0xC + Models[sub].SubModels[t].Blob.Length;
                    for (int b = 0; b < Bone_Count; b++)
                    {
                        size += 8 + Models[sub].SubModels[t].Bones[b].Blob.Length;
                    }
                }
            }

            return size;
        }

        public class BlendSkin_Type0
        {
            public uint MaterialID;
            public BlendSkin_Type1[] SubModels; // Type1_Count
        }

        public class BlendSkin_Type1
        {
            public uint UnkInt;
            public byte[] Blob; //blobSize
            public byte[] UnkData; //0xC
            public BlendSkin_Type2[] Bones; //Bone_Count
        }

        public class BlendSkin_Type2
        {
            public uint UnkInt;
            public byte[] Blob; //blobSize << 4
        }

    }
}