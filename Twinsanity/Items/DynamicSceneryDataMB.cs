using System.Collections.Generic;
using System.IO;
using System;

namespace Twinsanity
{
    public sealed class DynamicSceneryDataMB : TwinsItem
    {

        public uint Header1;
        public List<DynamicSceneryModel> Models;
        private int DataSize;

        public DynamicSceneryDataMB()
        {

        }

        protected override int GetSize()
        {
            return DataSize;
        }

        /// <summary>
        /// Write converted binary data to file.
        /// </summary>
        public override void Save(BinaryWriter writer)
        {
            writer.Write(Data);
        }

        public override void Load(BinaryReader reader, int size)
        {
            long start_pos = reader.BaseStream.Position;

            Header1 = reader.ReadUInt32();
            uint ModelCount = reader.ReadUInt32();
            Models = new List<DynamicSceneryModel>();

            reader.BaseStream.Position = start_pos;
            Data = reader.ReadBytes(size);
            DataSize = size;

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
