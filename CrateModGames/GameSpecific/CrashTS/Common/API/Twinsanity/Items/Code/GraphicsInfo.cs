using System;
using System.IO;

namespace Twinsanity
{
    public class GraphicsInfo : TwinsItem
    {

        public GI_Type0[] ModelIDs { get; set; }
        public uint SkinID { get; set; }
        public uint BlendSkinID { get; set; }
        public Pos Coord1 { get; set; } // Bounding box?
        public Pos Coord2 { get; set; } // Bounding box?
        public GI_Type1[] Type1 { get; set; }
        public GI_Type2[] Type2 { get; set; }
        public GI_Type3[] Type3 { get; set; }
        public GI_CollisionData[] CollisionData { get; set; }

        public byte[] HeaderVars;
        public byte[] CollisionDataRelated;

        public override void Save(BinaryWriter writer)
        {
            writer.Write(HeaderVars);
            writer.Write(Coord1.X);
            writer.Write(Coord1.Y);
            writer.Write(Coord1.Z);
            writer.Write(Coord1.W);
            writer.Write(Coord2.X);
            writer.Write(Coord2.Y);
            writer.Write(Coord2.Z);
            writer.Write(Coord2.W);

            if (Type1.Length > 0)
            {
                for (int i = 0; i < Type1.Length; i++)
                {
                    for (int a = 0; a < Type1[i].Numbers.Length; a++)
                    {
                        writer.Write(Type1[i].Numbers[a]);
                    }
                    for (int a = 0; a < Type1[i].Matrix.Length; a++)
                    {
                        writer.Write(Type1[i].Matrix[a].X);
                        writer.Write(Type1[i].Matrix[a].Y);
                        writer.Write(Type1[i].Matrix[a].Z);
                        writer.Write(Type1[i].Matrix[a].W);
                    }
                }
            }

            if (Type2.Length > 0)
            {
                for (int i = 0; i < Type2.Length; i++)
                {
                    for (int a = 0; a < Type2[i].Numbers.Length; a++)
                    {
                        writer.Write(Type2[i].Numbers[a]);
                    }
                    for (int a = 0; a < Type2[i].Matrix.Length; a++)
                    {
                        writer.Write(Type2[i].Matrix[a].X);
                        writer.Write(Type2[i].Matrix[a].Y);
                        writer.Write(Type2[i].Matrix[a].Z);
                        writer.Write(Type2[i].Matrix[a].W);
                    }
                }
            }

            if (ModelIDs.Length > 0)
            {
                for (int i = 0; i < ModelIDs.Length; i++)
                {
                    writer.Write((byte)ModelIDs[i].ID);
                }
                for (int i = 0; i < ModelIDs.Length; i++)
                {
                    writer.Write(ModelIDs[i].ModelID);
                }
            }

            if (Type1.Length > 0)
            {
                for (int a = 0; a < Type3.Length; a++)
                {
                    for (int i = 0; i < Type3[a].Matrix.Length; i++)
                    {
                        writer.Write(Type3[a].Matrix[i].X);
                        writer.Write(Type3[a].Matrix[i].Y);
                        writer.Write(Type3[a].Matrix[i].Z);
                        writer.Write(Type3[a].Matrix[i].W);
                    }
                }
            }

            writer.Write(SkinID);
            writer.Write(BlendSkinID);

            if (CollisionData.Length > 0)
            {
                for (int a = 0; a < CollisionData.Length; a++)
                {
                    for (var i = 0; i < 11; ++i)
                    {
                        writer.Write(CollisionData[a].Header[i]);
                    }
                    writer.Write(CollisionData[a].collisionDataBlob.Length);
                    writer.Write(CollisionData[a].collisionDataBlob);
                }
            }

            writer.Write(CollisionDataRelated);

        }

        public override void Load(BinaryReader reader, int size)
        {
            long pre_pos = reader.BaseStream.Position;

            HeaderVars = new byte[0x10];
            HeaderVars = reader.ReadBytes(0x10);

            uint Type1_Size = HeaderVars[0];
            uint Type2_Size = HeaderVars[1];
            uint Model_Size = HeaderVars[5];
            uint SkinFlag = HeaderVars[6];
            uint BlendSkinFlag = HeaderVars[7];
            int Type4_Size = HeaderVars[8];

            Coord1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Coord2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            if (Type1_Size > 0)
            {
                Type1 = new GI_Type1[Type1_Size];
                for (int i = 0; i < Type1_Size; i++)
                {
                    Pos[] Type1_Matrix = new Pos[5];
                    uint[] Type1_Numbers = new uint[5];
                    for (int a = 0; a < Type1_Numbers.Length; a++)
                    {
                        Type1_Numbers[a] = reader.ReadUInt32();
                    }
                    Type1_Matrix[0] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Type1_Matrix[1] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Type1_Matrix[2] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Type1_Matrix[3] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Type1_Matrix[4] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    GI_Type1 temp_Type1 = new GI_Type1() { Matrix = Type1_Matrix, Numbers = Type1_Numbers };
                    Type1[i] = temp_Type1;
                }
            }
            else
            {
                Type1 = new GI_Type1[0];
            }

            if (Type2_Size > 0)
            {
                Type2 = new GI_Type2[Type2_Size];
                for (int i = 0; i < Type2_Size; i++)
                {
                    Pos[] Type2_Matrix = new Pos[4];
                    uint[] Type2_Numbers = new uint[2];
                    Type2_Numbers[0] = reader.ReadUInt32();
                    Type2_Numbers[1] = reader.ReadUInt32();
                    for (int a = 0; a < Type2_Matrix.Length; a++)
                    {
                        Type2_Matrix[a] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    }
                    GI_Type2 temp_Type2 = new GI_Type2() { Matrix = Type2_Matrix, Numbers = Type2_Numbers };
                    Type2[i] = temp_Type2;
                }
            }
            else
            {
                Type2 = new GI_Type2[0];
            }

            if (Model_Size > 0)
            {
                ModelIDs = new GI_Type0[Model_Size];
                uint[] IDs = new uint[Model_Size];
                uint[] IDs_m = new uint[Model_Size];
                for (int i = 0; i < Model_Size; i++)
                {
                    IDs[i] = reader.ReadByte();
                }
                for (int i = 0; i < Model_Size; i++)
                {
                    IDs_m[i] = reader.ReadUInt32();
                }
                for (int i = 0; i < Model_Size; i++)
                {
                    GI_Type0 Type0 = new GI_Type0() { ID = IDs[i], ModelID = IDs_m[i] };
                    ModelIDs[i] = Type0;
                }

            }
            else
            {
                ModelIDs = new GI_Type0[0];
            }

            if (Type1_Size > 0)
            {
                Type3 = new GI_Type3[Type1_Size];
                for (int a = 0; a < Type1_Size; a++)
                {
                    Pos[] Type3_Matrix = new Pos[4];
                    for (int i = 0; i < Type3_Matrix.Length; i++)
                    {
                        Type3_Matrix[i] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    }
                    Type3[a] = new GI_Type3() { Matrix = Type3_Matrix };
                }
            }
            else
            {
                Type3 = new GI_Type3[0];
            }

            SkinID = reader.ReadUInt32();

            BlendSkinID = reader.ReadUInt32();

            if (Type4_Size > 0)
            {
                CollisionData = new GI_CollisionData[Type4_Size];
                for (int a = 0; a < Type4_Size; a++)
                {
                    ushort[] header = new ushort[11];
                    for (var i = 0; i < 11; ++i)
                    {
                        header[i] = reader.ReadUInt16();
                    }
                    int blobSize = reader.ReadInt32();
                    byte[] Blob = reader.ReadBytes(blobSize);
                    CollisionData[a] = new GI_CollisionData() { Header = header, collisionDataBlob = Blob };
                    using (MemoryStream memoryStream = new MemoryStream(Blob))
                    {
                        using (BinaryReader blobReader = new BinaryReader(memoryStream))
                        {
                            var firstBlockElemAmt = header[0];
                            var secondBlockElemAmt = header[1];
                            var thirdBlockElemAmt = header[2];
                            var fourthBlockElemAmt = header[3];
                            var fifthBlockElemAmt = header[4];
                            var sixthBlockElemAmt = header[10] - header[9];
                            var seventhBlockElemAmt = blobSize - header[10];
                            var secondBlockPos = header[5];
                            var thirdBlockPos = header[6];
                            var fourthBlockPos = header[7];
                            var fifthBlockPos = header[8];
                            var sixthBlockPos = header[9];
                            var seventhBlockPos = header[10];
                            CollisionData[a].UnkVectors1 = new TwinsVector4[firstBlockElemAmt];
                            for (var i = 0; i < firstBlockElemAmt; ++i)
                            {
                                CollisionData[a].UnkVectors1[i] = new TwinsVector4();
                                CollisionData[a].UnkVectors1[i].Load(blobReader, 16);
                            }
                        }
                    }
                }
            }
            else
            {
                CollisionData = new GI_CollisionData[0];
            }

            CollisionDataRelated = reader.ReadBytes(Type4_Size);

        }

        protected override int GetSize()
        {
            int count = 0x10 + 16 + 16 + 4 + 4 + CollisionDataRelated.Length;

            if (Type1.Length > 0)
            {
                for (int i = 0; i < Type1.Length; i++)
                {
                    count += Type1[i].Numbers.Length * 4;
                    count += Type1[i].Matrix.Length * 16;
                }
            }

            if (Type2.Length > 0)
            {
                for (int i = 0; i < Type2.Length; i++)
                {
                    count += Type2[i].Numbers.Length * 4;
                    count += Type2[i].Matrix.Length * 16;
                }
            }

            if (ModelIDs.Length > 0)
            {
                count += ModelIDs.Length * 5;
            }

            if (Type3.Length > 0)
            {
                for (int i = 0; i < Type3.Length; i++)
                {
                    count += Type3[i].Matrix.Length * 16;
                }
            }

            if (CollisionData.Length > 0)
            {
                for (int i = 0; i < CollisionData.Length; i++)
                {
                    count += CollisionData[i].collisionDataBlob.Length + 4 + 0x16;
                }
            }

            return count;
        }

        public struct GI_Type0
        {
            public uint ID;
            public uint ModelID;
        }

        public struct GI_Type1
        {
            public uint[] Numbers; // 5
            public Pos[] Matrix; // 5
        }

        public struct GI_Type2
        {
            public uint[] Numbers; // 2
            public Pos[] Matrix; // 4
        }

        public struct GI_Type3
        {
            public Pos[] Matrix; // 4
        }

        public class GI_CollisionData
        {
            public ushort[] Header; //0x16
            public byte[] collisionDataBlob; //blobSize
            public TwinsVector4[] UnkVectors1;
        }
    }
}
