using System.Collections.Generic;
using System.IO;
using System;

namespace Twinsanity
{
    public sealed class DynamicSceneryData : TwinsItem
    {

        public uint Header1;
        public List<DynamicSceneryModel> Models;

        public DynamicSceneryData()
        {

        }

        protected override int GetSize()
        {
            int count = 4 + 2;

            for (int i = 0; i < Models.Count; i++)
            {
                count += 4 + 4;
                if (Models[i].GI_Types.Count > 0)
                {
                    for (int g = 0; g < Models[i].GI_Types.Count; g++)
                    {
                        count += 0x16;
                        count += 4;
                        count += Models[i].GI_Types[g].unkBlob.Length;
                    }
                }
                count += 4 + 4 + 2;
                count += Models[i].dynBlob.Length;
                count += 1 + 4 + 32;
            }

            return count;
        }

        /// <summary>
        /// Write converted binary data to file.
        /// </summary>
        public override void Save(BinaryWriter writer)
        {
            writer.Write(Header1);
            writer.Write((ushort)Models.Count);

            for (int i = 0; i < Models.Count; i++)
            {
                writer.Write(Models[i].UnkInt1);
                writer.Write(Models[i].GI_Types.Count);
                for (int g = 0; g < Models[i].GI_Types.Count; g++)
                {
                    writer.Write(Models[i].GI_Types[g].Header);
                    writer.Write(Models[i].GI_Types[g].unkBlob.Length);
                    writer.Write(Models[i].GI_Types[g].unkBlob);
                }
                writer.Write(Models[i].unkInt2);
                writer.Write(Models[i].unkBlobSizePacked);
                writer.Write(Models[i].unkBlobSizeHelper);
                writer.Write(Models[i].dynBlob);
                writer.Write(Models[i].unkByte);
                writer.Write(Models[i].ModelID);
                writer.Write(Models[i].BoundingBoxVector1.X);
                writer.Write(Models[i].BoundingBoxVector1.Y);
                writer.Write(Models[i].BoundingBoxVector1.Z);
                writer.Write(Models[i].BoundingBoxVector1.W);
                writer.Write(Models[i].BoundingBoxVector2.X);
                writer.Write(Models[i].BoundingBoxVector2.Y);
                writer.Write(Models[i].BoundingBoxVector2.Z);
                writer.Write(Models[i].BoundingBoxVector2.W);
            }

        }

        public override void Load(BinaryReader reader, int size)
        {
            //long start_pos = reader.BaseStream.Position;

            Header1 = reader.ReadUInt32();
            ushort ModelCount = reader.ReadUInt16();
            Models = new List<DynamicSceneryModel>();

            if (ModelCount != 0)
            {
                for (int i = 0; i < ModelCount; i++)
                {
                    DynamicSceneryModel Model = new DynamicSceneryModel();
                    Model.GI_Types = new List<GI_Type3>();

                    Model.UnkInt1 = reader.ReadUInt32();
                    uint GI_amount = reader.ReadUInt32();
                    if (GI_amount != 0)
                    {
                        for (int g = 0; g < GI_amount; g++)
                        {
                            GI_Type3 git = new GI_Type3();
                            git.Header = reader.ReadBytes(0x16);
                            int gblobSize = reader.ReadInt32();
                            git.unkBlob = reader.ReadBytes(gblobSize);
                            Model.GI_Types.Add(git);
                        }
                    }
                    Model.unkInt2 = reader.ReadInt32();
                    Model.unkBlobSizePacked = reader.ReadInt32();
                    Model.unkBlobSizeHelper = reader.ReadInt16();

                    int blobSize = (Model.unkBlobSizePacked & 0x7F) * 0x8 +
                        (Model.unkBlobSizePacked >> 0x9 & 0x1FFC) +
                        (Model.unkBlobSizePacked >> 0x16) * Model.unkBlobSizeHelper * 0x4;

                    Model.dynBlob = reader.ReadBytes(blobSize);

                    Model.unkByte = reader.ReadByte();

                    Model.ModelID = reader.ReadUInt32();
                    Model.BoundingBoxVector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Model.BoundingBoxVector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Models.Add(Model);
                }
            }

            //reader.BaseStream.Position = start_pos;
            //Data = reader.ReadBytes(size);
            //DataSize = size;

            //Console.WriteLine("DySc end pos: " + (reader.BaseStream.Position - start_pos) + " target: " + size);
        }

        public class DynamicSceneryModel
        {
            public uint UnkInt1;
            public List<GI_Type3> GI_Types;
            public int unkInt2;

            public int unkBlobSizePacked;
            public short unkBlobSizeHelper;
            public byte[] dynBlob;

            public byte unkByte;
            public uint ModelID;
            public Pos BoundingBoxVector1;
            public Pos BoundingBoxVector2;

            //public byte[] BlobHeader; // 5
            //public Pos WorldPosition;
        }

        public class GI_Type3
        {
            public byte[] Header; //0x16
            public byte[] unkBlob; //blobSize
        }

    }
}
