using System;
using System.Collections.Generic;

namespace Twinsanity
{
    public class Model : BaseItem
    {
        public new string NodeName = "Model";
        public int SubModels;
        public _SubModel[] SubModel;

        private List<_Group> _Groups = new List<_Group>();

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            SubModels = BSReader.ReadInt32();
            SubModel = new _SubModel[SubModels];

            for (int i = 0; i < SubModels; i++)
            {
                _SubModel withBlock = SubModel[i];
                withBlock.Vertexes = BSReader.ReadInt32();
                withBlock.BlockSize = BSReader.ReadUInt32();
                withBlock.k = BSReader.ReadUInt16();
                withBlock.c = BSReader.ReadUInt16();
                int cnt, offset;
                cnt = 0;
                offset = (int)ByteStream.Position;
                withBlock.Null1 = BSReader.ReadUInt32();
                withBlock.Something = BSReader.ReadUInt32();
                withBlock.Null2 = BSReader.ReadUInt32();
                while ((cnt < withBlock.Vertexes) && (ByteStream.Position < offset + withBlock.BlockSize))
                {
                    var withBlock1 = new _Group();
                    Array.Resize(ref withBlock1.UV, 0);
                    Array.Resize(ref withBlock1.Shit, 0);
                    withBlock1.SomeNum1 = BSReader.ReadUInt32();
                    withBlock1.Vertexes = BSReader.ReadByte();
                    cnt += withBlock1.Vertexes;
                    withBlock1.Some80h = BSReader.ReadByte();
                    withBlock1.Null1 = BSReader.ReadUInt16();
                    withBlock1.SomeNum2 = BSReader.ReadUInt32();
                    withBlock1.SomeNum3 = BSReader.ReadUInt32();
                    withBlock1.Null2 = BSReader.ReadUInt32();
                    withBlock1.Signature1 = BSReader.ReadUInt32();
                    withBlock1.SomeShit1 = BSReader.ReadUInt32();
                    withBlock1.SomeShit2 = BSReader.ReadUInt32();
                    withBlock1.SomeShit3 = BSReader.ReadUInt32();
                    withBlock1.Signature2 = BSReader.ReadUInt32();
                    uint head = BSReader.ReadUInt32();
                    while (head != 335544320)
                    {
                        switch (head & 255)
                        {
                            case 3:
                                {
                                    withBlock1.VertHead = head;
                                    withBlock1.Vertex = new Position3[withBlock1.Vertexes];
                                    for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                    {
                                        withBlock1.Vertex[j].X = BSReader.ReadSingle();
                                        withBlock1.Vertex[j].Y = BSReader.ReadSingle();
                                        withBlock1.Vertex[j].Z = BSReader.ReadSingle();
                                    }

                                    break;
                                }

                            case 4:
                                {
                                    withBlock1.WeightHead = head;
                                    withBlock1.Weight = new _Weight[withBlock1.Vertexes];
                                    for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                    {
                                        withBlock1.Weight[j].X = BSReader.ReadSingle();
                                        withBlock1.Weight[j].Y = BSReader.ReadSingle();
                                        withBlock1.Weight[j].Z = BSReader.ReadSingle();
                                        withBlock1.Weight[j].SomeByte = BSReader.ReadByte();
                                        withBlock1.Weight[j].CONN = BSReader.ReadByte();
                                        withBlock1.Weight[j].Null1 = BSReader.ReadUInt16();
                                    }

                                    break;
                                }

                            case 5:
                                {
                                    withBlock1.UVHead = head;
                                    withBlock1.UV = new Position3[withBlock1.Vertexes];
                                    for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                    {
                                        withBlock1.UV[j].X = BSReader.ReadSingle();
                                        withBlock1.UV[j].Y = BSReader.ReadSingle();
                                        withBlock1.UV[j].Z = BSReader.ReadSingle();
                                    }

                                    break;
                                }

                            case 6:
                                {
                                    withBlock1.ShiteHead = head;
                                    withBlock1.Shit = new uint[withBlock1.Vertexes];
                                    for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                        withBlock1.Shit[j] = BSReader.ReadUInt32();
                                    break;
                                }
                        }
                        head = BSReader.ReadUInt32();
                    }
                    withBlock1.EndSignature1 = head;
                    withBlock1.EndSignature2 = BSReader.ReadUInt32();
                    withBlock1.leftovers = new byte[] { };
                    _Groups.Add(withBlock1);
                }
                withBlock.Group = _Groups.ToArray();
                Array.Resize(ref withBlock.Group[withBlock.Group.Length - 1].leftovers, (int)(withBlock.BlockSize + offset - ByteStream.Position));
                withBlock.Group[withBlock.Group.Length - 1].leftovers = BSReader.ReadBytes((int)(withBlock.BlockSize + offset - ByteStream.Position));

                SubModel[i] = withBlock;
            }
            _Groups.Clear();
        }

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(SubModels);
            for (int i = 0; i <= SubModels - 1; i++)
            {
                {
                    var withBlock = SubModel[i];
                    NSWriter.Write(withBlock.Vertexes);
                    NSWriter.Write(withBlock.BlockSize);
                    NSWriter.Write(withBlock.k);
                    NSWriter.Write(withBlock.c);
                    NSWriter.Write(withBlock.Null1);
                    NSWriter.Write(withBlock.Something);
                    NSWriter.Write(withBlock.Null2);
                    for (int a = 0; a <= withBlock.Group.Length - 1; a++)
                    {
                        {
                            var withBlock1 = withBlock.Group[a];
                            NSWriter.Write(withBlock1.SomeNum1);
                            NSWriter.Write(withBlock1.Vertexes);
                            NSWriter.Write(withBlock1.Some80h);
                            NSWriter.Write(withBlock1.Null1);
                            NSWriter.Write(withBlock1.SomeNum2);
                            NSWriter.Write(withBlock1.SomeNum3);
                            NSWriter.Write(withBlock1.Null2);
                            NSWriter.Write(withBlock1.Signature1);
                            NSWriter.Write(withBlock1.SomeShit1);
                            NSWriter.Write(withBlock1.SomeShit2);
                            NSWriter.Write(withBlock1.SomeShit3);
                            NSWriter.Write(withBlock1.Signature2);
                            if (withBlock1.VertHead > 0)
                            {
                                NSWriter.Write(withBlock1.VertHead);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.Vertex[j].X);
                                    NSWriter.Write(withBlock1.Vertex[j].Y);
                                    NSWriter.Write(withBlock1.Vertex[j].Z);
                                }
                            }
                            if (withBlock1.WeightHead > 0)
                            {
                                NSWriter.Write(withBlock1.WeightHead);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.Weight[j].X);
                                    NSWriter.Write(withBlock1.Weight[j].Y);
                                    NSWriter.Write(withBlock1.Weight[j].Z);
                                    NSWriter.Write(withBlock1.Weight[j].SomeByte);
                                    NSWriter.Write(withBlock1.Weight[j].CONN);
                                    NSWriter.Write(withBlock1.Weight[j].Null1);
                                }
                            }
                            if (withBlock1.UVHead > 0)
                            {
                                NSWriter.Write(withBlock1.UVHead);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.UV[j].X);
                                    NSWriter.Write(withBlock1.UV[j].Y);
                                    NSWriter.Write(withBlock1.UV[j].Z);
                                }
                            }
                            if (withBlock1.ShiteHead > 0)
                            {
                                NSWriter.Write(withBlock1.ShiteHead);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                    NSWriter.Write(withBlock1.Shit[j]);
                            }
                            NSWriter.Write(withBlock1.EndSignature1);
                            NSWriter.Write(withBlock1.EndSignature2);
                        }
                    }
                    NSWriter.Write(withBlock.Group[withBlock.Group.Length - 1].leftovers);
                }
            }
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }


        public void Import(RawData[][] RawData)
        {
            SubModels = RawData.Length;
            Array.Resize(ref SubModel, SubModels);
            for (int i = 0; i <= SubModels - 1; i++)
            {
                var Groups = Math.Ceiling(RawData[i].Length / (double)33);
                SubModel[i].Vertexes = RawData[i].Length;
                SubModel[i].BlockSize = (uint)(12 + Groups * (48 + 16) + RawData[i].Length * (12 + 16 + 12 + 4) + 20);
                SubModel[i].k = (ushort)((SubModel[i].BlockSize - 20) / 16);
                SubModel[i].c = 24576;
                SubModel[i].Null1 = 0;
                SubModel[i].Something = 0x70000FA;
                SubModel[i].Null2 = 0;
                Array.Resize(ref SubModel[i].Group, (int)Groups);
                uint offset = 0;
                for (int j = 0; j <= SubModel[i].Group.Length - 1; j++)
                {
                    {
                        var withBlock = SubModel[i].Group[j];
                        withBlock.SomeNum1 = 0x6C018000;
                        if (j < SubModel[i].Group.Length - 1)
                            withBlock.Vertexes = 33;
                        else
                            withBlock.Vertexes = (byte)(RawData[i].Length - 33 * (SubModel[i].Group.Length - 1));
                        Array.Resize(ref withBlock.Vertex, withBlock.Vertexes);
                        Array.Resize(ref withBlock.Weight, withBlock.Vertexes);
                        Array.Resize(ref withBlock.UV, withBlock.Vertexes);
                        Array.Resize(ref withBlock.Shit, withBlock.Vertexes);
                        withBlock.Some80h = 128;
                        withBlock.Null1 = 0;
                        withBlock.SomeNum2 = 0x30024000;
                        withBlock.SomeNum3 = 0x512;
                        withBlock.Null2 = 0;
                        withBlock.Signature1 = 0x1000101;
                        withBlock.SomeShit1 = 0x64018001;
                        withBlock.SomeShit2 = (uint)withBlock.Vertexes * 4;
                        withBlock.SomeShit3 = (uint)withBlock.Vertexes + withBlock.Some80h << 8;
                        withBlock.Signature2 = 0x1000104;
                        withBlock.VertHead = 0x68008003 + (uint)withBlock.Vertexes * 65536;  // 0x0380XX68
                        withBlock.WeightHead = 0x6C008004 + (uint)withBlock.Vertexes * 65536;
                        withBlock.UVHead = 0x68008005 + (uint)withBlock.Vertexes * 65536;
                        withBlock.ShiteHead = 0x6E008006 + (uint)withBlock.Vertexes * 65536;
                        for (int k = 0; k <= withBlock.Vertexes - 1; k++)
                        {
                            withBlock.Vertex[k].X = RawData[i][k + offset].X;
                            withBlock.Vertex[k].Y = RawData[i][k + offset].Y;
                            withBlock.Vertex[k].Z = RawData[i][k + offset].Z;
                            withBlock.Weight[k].X = RawData[i][k + offset].U;
                            withBlock.Weight[k].Y = RawData[i][k + offset].V;
                            withBlock.Weight[k].Z = RawData[i][k + offset].W;
                            withBlock.Weight[k].SomeByte = 127;
                            withBlock.Weight[k].CONN = RawData[i][k + offset].CONN == true ? (byte)0 : (byte)128;
                            withBlock.UV[k].X = RawData[i][k + offset].Nx;
                            withBlock.UV[k].Y = RawData[i][k + offset].Ny;
                            withBlock.UV[k].Z = RawData[i][k + offset].Nz;
                            withBlock.Shit[k] = RawData[i][k + offset].Emission;
                        }
                        offset += withBlock.Vertexes;
                        withBlock.EndSignature1 = 0x14000000;
                        withBlock.EndSignature2 = 0x1000101;
                        Array.Resize(ref withBlock.leftovers, 20);
                        SubModel[i].Group[j] = withBlock;
                    }
                }
            }
            UpdateStream();
        }

        #region STRUCTURES
        public struct _SubModel
        {
            // Primary Header
            public int Vertexes;
            public uint BlockSize;
            public ushort k, c;
            public uint Null1;
            public uint Something;
            public uint Null2;
            public _Group[] Group;
        }
        public struct _Group
        {
            public uint SomeNum1;
            public byte Vertexes;
            public byte Some80h;
            public ushort Null1;
            public uint SomeNum2;
            public uint SomeNum3;
            public uint Null2;
            public uint Signature1;
            public uint SomeShit1;
            public uint SomeShit2;
            public uint SomeShit3;
            public uint Signature2;
            public uint VertHead;
            public Position3[] Vertex;
            public uint WeightHead;
            public _Weight[] Weight;
            public uint UVHead;
            public Position3[] UV;
            public uint ShiteHead;
            public uint[] Shit;
            public uint EndSignature1;
            public uint EndSignature2;
            public byte[] leftovers;
        }
        public struct Position3
        {
            public float X, Y, Z;
        }
        public struct _Weight
        {
            public float X, Y, Z;
            public byte SomeByte;
            public byte CONN;
            public ushort Null1;
        }
        public struct RawData
        {
            public float X, Y, Z;
            public float U, V, W;
            public bool CONN;
            public uint Emission;
            public float Nx, Ny, Nz;
        }
        #endregion
    }
}
