using System;
using System.IO;
using System.Collections.Generic;

namespace Twinsanity
{
    public class Model : TwinsItem
    {
        public List<SubModel> SubModels { get; set; } = new List<SubModel>();

        public override void Load(BinaryReader reader, int size)
        {
            var sk = reader.BaseStream.Position;
            var count = reader.ReadInt32();

            SubModels.Clear();
            for (int i = 0; i < count; i++)
            {
                SubModel sub = new SubModel()
                {
                    VertexCount = reader.ReadInt32(),
                    BlockSize = reader.ReadUInt32(),
                    k = reader.ReadUInt16(),
                    c = reader.ReadUInt16(),
                    Null1 = reader.ReadUInt32(),
                    Something = reader.ReadUInt32(),
                    Null2 = reader.ReadUInt32()
                };
                int cnt = 0, offset = (int)(reader.BaseStream.Position - sk) - 12;
                sub.Groups = new List<Group>();
                while ((cnt < sub.VertexCount) && (reader.BaseStream.Position - sk < offset + sub.BlockSize))
                {
                    Group grp = new Group
                    {
                        SomeNum1 = reader.ReadUInt32(),
                        VertexCount = reader.ReadByte(),
                        Some80h = reader.ReadByte(),
                        Null1 = reader.ReadUInt16(),
                        SomeNum2 = reader.ReadUInt32(),
                        SomeNum3 = reader.ReadUInt32(),
                        Null2 = reader.ReadUInt32(),
                        Signature1 = reader.ReadUInt32(),
                        SomeShit1 = reader.ReadUInt32(),
                        SomeShit2 = reader.ReadUInt32(),
                        SomeShit3 = reader.ReadUInt32(),
                        Signature2 = reader.ReadUInt32()
                    };
                    cnt += grp.VertexCount;
                    uint head = reader.ReadUInt32();
                    while (head != 0x14000000)
                    {
                        switch (head & 255)
                        {
                            case 3:
                                {
                                    grp.VertHead = head;
                                    grp.Vertex = new Position3[grp.VertexCount];
                                    for (int j = 0; j < grp.VertexCount; j++)
                                    {
                                        grp.Vertex[j].X = reader.ReadSingle();
                                        grp.Vertex[j].Y = reader.ReadSingle();
                                        grp.Vertex[j].Z = reader.ReadSingle();
                                    }

                                    break;
                                }

                            case 4:
                                {
                                    grp.VDataHead = head;
                                    grp.VData = new VertexData[grp.VertexCount];
                                    for (int j = 0; j < grp.VertexCount; j++)
                                    {
                                        grp.VData[j].R = reader.ReadByte();
                                        grp.VData[j].PX = reader.ReadByte();
                                        grp.VData[j].X = reader.ReadUInt16();
                                        grp.VData[j].G = reader.ReadByte();
                                        grp.VData[j].PY = reader.ReadByte();
                                        grp.VData[j].Y = reader.ReadUInt16();
                                        grp.VData[j].B = reader.ReadByte();
                                        grp.VData[j].SomeFloat = reader.ReadSingle();
                                        grp.VData[j].CONN = reader.ReadByte();
                                        grp.VData[j].Null1 = reader.ReadUInt16();
                                    }

                                    break;
                                }

                            case 5:
                                {
                                    grp.UVHead = head;
                                    grp.UV = new Position3[grp.VertexCount];
                                    for (int j = 0; j < grp.VertexCount; j++)
                                    {
                                        grp.UV[j].X = reader.ReadSingle();
                                        grp.UV[j].Y = reader.ReadSingle();
                                        grp.UV[j].Z = reader.ReadSingle();
                                    }

                                    break;
                                }

                            case 6:
                                {
                                    grp.ShiteHead = head;
                                    grp.Shit = new uint[grp.VertexCount];
                                    for (int j = 0; j < grp.VertexCount; j++)
                                        grp.Shit[j] = reader.ReadUInt32();
                                    break;
                                }
                        }
                        head = reader.ReadUInt32();
                    }
                    grp.EndSignature1 = head;
                    grp.EndSignature2 = reader.ReadUInt32();
                    grp.leftovers = new byte[] { };
                    sub.Groups.Add(grp);
                }
                if (sub.Groups.Count > 0)
                {
                    long curPos = reader.BaseStream.Position;
                    Group group = sub.Groups[sub.Groups.Count - 1];
                    int leftoverSize = (int)(sub.BlockSize + offset - (reader.BaseStream.Position - sk));
                    leftoverSize -= 4;
                    reader.BaseStream.Position += leftoverSize;
                    int paddingBytes = reader.ReadInt32();
                    reader.BaseStream.Position = curPos;
                    leftoverSize = (int)(sub.BlockSize + paddingBytes + offset - (reader.BaseStream.Position - sk));
                    //Console.WriteLine("Padding: " + paddingBytes);

                    group.leftovers = new byte[leftoverSize];
                    group.leftovers = reader.ReadBytes(leftoverSize);
                    sub.Groups[sub.Groups.Count - 1] = group;
                }

                SubModels.Add(sub);
            }

            //Console.WriteLine("end pos: " + (reader.BaseStream.Position - sk) + " target: " + size);

            //Remain = reader.ReadBytes((size) - (int)(reader.BaseStream.Position - sk));

        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(SubModels.Count);
            for (int i = 0; i < SubModels.Count; ++i)
            {
                var sub = SubModels[i];
                writer.Write(sub.VertexCount);
                writer.Write(sub.BlockSize);
                writer.Write(sub.k);
                writer.Write(sub.c);
                writer.Write(sub.Null1);
                writer.Write(sub.Something);
                writer.Write(sub.Null2);
                for (int a = 0; a < sub.Groups.Count; ++a)
                {
                    var group = sub.Groups[a];
                    writer.Write(group.SomeNum1);
                    writer.Write(group.VertexCount);
                    writer.Write(group.Some80h);
                    writer.Write(group.Null1);
                    writer.Write(group.SomeNum2);
                    writer.Write(group.SomeNum3);
                    writer.Write(group.Null2);
                    writer.Write(group.Signature1);
                    writer.Write(group.SomeShit1);
                    writer.Write(group.SomeShit2);
                    writer.Write(group.SomeShit3);
                    writer.Write(group.Signature2);
                    if (group.VertHead > 0) //vertex positions
                    {
                        writer.Write(group.VertHead);
                        for (int j = 0; j < group.VertexCount; ++j)
                        {
                            writer.Write(group.Vertex[j].X);
                            writer.Write(group.Vertex[j].Y);
                            writer.Write(group.Vertex[j].Z);
                        }
                    }
                    if (group.VDataHead > 0) //vertex data
                    {
                        writer.Write(group.VDataHead);
                        for (int j = 0; j < group.VertexCount; ++j)
                        {
                            writer.Write(group.VData[j].R);
                            writer.Write(group.VData[j].PX);
                            writer.Write(group.VData[j].X);
                            writer.Write(group.VData[j].G);
                            writer.Write(group.VData[j].PY);
                            writer.Write(group.VData[j].Y);
                            writer.Write(group.VData[j].B);
                            writer.Write(group.VData[j].SomeFloat);
                            writer.Write(group.VData[j].CONN);
                            writer.Write(group.VData[j].Null1);
                        }
                    }
                    if (group.UVHead > 0) //textures?
                    {
                        writer.Write(group.UVHead);
                        for (int j = 0; j < group.VertexCount; ++j)
                        {
                            writer.Write(group.UV[j].X);
                            writer.Write(group.UV[j].Y);
                            writer.Write(group.UV[j].Z);
                        }
                    }
                    if (group.ShiteHead > 0) //lighting?
                    {
                        writer.Write(group.ShiteHead);
                        for (int j = 0; j < group.VertexCount; ++j)
                            writer.Write(group.Shit[j]);
                    }
                    writer.Write(group.EndSignature1);
                    writer.Write(group.EndSignature2);
                }
                if (sub.Groups.Count > 0)
                {
                    writer.Write(sub.Groups[sub.Groups.Count - 1].leftovers);
                }
            }
        }

        protected override int GetSize()
        {
            int size = 4;
            foreach (var i in SubModels)
            {
                size += 24;
                foreach (var j in i.Groups)
                {
                    size += 48;
                    if (j.VertHead > 0)
                    {
                        size += 4 + 12 * j.VertexCount;
                    }
                    if (j.VDataHead > 0)
                    {
                        size += 4 + 16 * j.VertexCount;
                    }
                    if (j.UVHead > 0)
                    {
                        size += 4 + 12 * j.VertexCount;
                    }
                    if (j.ShiteHead > 0)
                    {
                        size += 4 + 4 * j.VertexCount;
                    }
                    size += j.leftovers.Length;
                }
            }
            return size;
        }

        public void Import(RawData[][] RawData)
        {
            /*
            SubModels = RawData.Length;
            Array.Resize(ref SubModel, SubModels);
            for (int i = 0; i <= SubModels - 1; i++)
            {
                var Groups = Math.Ceiling(RawData[i].Length / (double)33);
                SubModel[i].VertexCount = RawData[i].Length;
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
                            withBlock.VertexCount = 33;
                        else
                            withBlock.VertexCount = (byte)(RawData[i].Length - 33 * (SubModel[i].Group.Length - 1));
                        Array.Resize(ref withBlock.Vertex, withBlock.VertexCount);
                        Array.Resize(ref withBlock.VertexData, withBlock.VertexCount);
                        Array.Resize(ref withBlock.UV, withBlock.VertexCount);
                        Array.Resize(ref withBlock.Shit, withBlock.VertexCount);
                        withBlock.Some80h = 128;
                        withBlock.Null1 = 0;
                        withBlock.SomeNum2 = 0x30024000;
                        withBlock.SomeNum3 = 0x512;
                        withBlock.Null2 = 0;
                        withBlock.Signature1 = 0x1000101;
                        withBlock.SomeShit1 = 0x64018001;
                        withBlock.SomeShit2 = (uint)withBlock.VertexCount * 4;
                        withBlock.SomeShit3 = (uint)withBlock.VertexCount + withBlock.Some80h << 8;
                        withBlock.Signature2 = 0x1000104;
                        withBlock.VertHead = 0x68008003 + (uint)withBlock.VertexCount * 65536;  // 0x0380XX68
                        withBlock.VDataHead = 0x6C008004 + (uint)withBlock.VertexCount * 65536;
                        withBlock.UVHead = 0x68008005 + (uint)withBlock.VertexCount * 65536;
                        withBlock.ShiteHead = 0x6E008006 + (uint)withBlock.VertexCount * 65536;
                        for (int k = 0; k <= withBlock.VertexCount - 1; k++)
                        {
                            withBlock.Vertex[k].X = RawData[i][k + offset].X;
                            withBlock.Vertex[k].Y = RawData[i][k + offset].Y;
                            withBlock.Vertex[k].Z = RawData[i][k + offset].Z;
                            withBlock.VertexData[k].X = RawData[i][k + offset].U;
                            withBlock.VertexData[k].Y = RawData[i][k + offset].V;
                            withBlock.VertexData[k].Z = RawData[i][k + offset].W;
                            withBlock.VertexData[k].SomeByte = 127;
                            withBlock.VertexData[k].CONN = RawData[i][k + offset].CONN == true ? (byte)0 : (byte)128;
                            withBlock.UV[k].X = RawData[i][k + offset].Nx;
                            withBlock.UV[k].Y = RawData[i][k + offset].Ny;
                            withBlock.UV[k].Z = RawData[i][k + offset].Nz;
                            withBlock.Shit[k] = RawData[i][k + offset].Diffuse;
                        }
                        offset += withBlock.VertexCount;
                        withBlock.EndSignature1 = 0x14000000;
                        withBlock.EndSignature2 = 0x1000101;
                        Array.Resize(ref withBlock.leftovers, 20);
                        SubModel[i].Group[j] = withBlock;
                    }
                }
            }
            UpdateStream();*/
        }

        public byte[] ToPLY()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter ply = new StreamWriter(stream) { AutoFlush = true })
                {
                    int vertexcount = 0, polycount = 0;
                    for (int i = 0; i < SubModels.Count; ++i)
                    {
                        for (int a = 0; a < SubModels[i].Groups.Count; ++a)
                        {
                            if (SubModels[i].Groups[a].VertHead > 0 && SubModels[i].Groups[a].VDataHead > 0)
                            {
                                vertexcount += SubModels[i].Groups[a].VertexCount;
                                for (int f = 0; f < SubModels[i].Groups[a].VertexCount - 2; ++f)
                                {
                                    if (SubModels[i].Groups[a].VData[f + 2].CONN != 128)
                                        ++polycount;
                                }
                            }
                        }
                    }
                    ply.WriteLine("ply");
                    ply.WriteLine("format ascii 1.0");
                    ply.WriteLine("element vertex {0}", vertexcount);
                    ply.WriteLine("property float x");
                    ply.WriteLine("property float y");
                    ply.WriteLine("property float z");
                    ply.WriteLine("property uchar red");
                    ply.WriteLine("property uchar green");
                    ply.WriteLine("property uchar blue");
                    ply.WriteLine("element face {0}", polycount);
                    ply.WriteLine("property list uchar int vertex_index");
                    ply.WriteLine("end_header");
                    foreach (var s in SubModels) //vertices
                    {
                        foreach (var g in s.Groups)
                        {
                            if (g.VertHead > 0 && g.VDataHead > 0)
                                for (int i = 0; i < g.VertexCount; ++i)
                                {
                                    byte red, green, blue;
                                    red = g.VData[i].R;
                                    green = g.VData[i].G;
                                    blue = g.VData[i].B;
                                    //if (g.ShiteHead > 0)
                                    //{
                                    //    red = (byte)((g.Shit[i] & 0xFF00) >> 8);
                                    //    green = (byte)((g.Shit[i] & 0xFF0000) >> 16);
                                    //    blue = (byte)((g.Shit[i] & 0xFF000000) >> 24);
                                    //}
                                    ply.WriteLine("{0} {1} {2} {3} {4} {5}", -g.Vertex[i].X, g.Vertex[i].Y, g.Vertex[i].Z, red, green, blue);
                                }
                        }
                    }
                    vertexcount = 0;
                    foreach (var s in SubModels) //polys
                    {
                        foreach (var g in s.Groups)
                        {
                            if (g.VertHead > 0 && g.VDataHead > 0)
                            {
                                for (int i = 0; i < g.VertexCount - 2; ++i)
                                {
                                    if (g.VData[i + 2].CONN != 128)
                                        ply.WriteLine("3 {0} {1} {2}", vertexcount + ((i & 0x1) == 0x1 ? i + 1 : i + 0), vertexcount + ((i & 0x1) == 0x1 ? i + 0 : i + 1), vertexcount + ((i & 0x1) == 0x1 ? i + 2 : i + 2));
                                }
                                vertexcount += g.VertexCount;
                            }
                        }
                    }
                    return stream.ToArray();
                }
            }
        }

        #region STRUCTURES
        public struct SubModel
        {
            // Primary Header
            public int VertexCount;
            public uint BlockSize;
            public ushort k, c;
            public uint Null1;
            public uint Something;
            public uint Null2;
            public List<Group> Groups;
        }
        public struct Group
        {
            public uint SomeNum1;
            public byte VertexCount;
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
            public uint VDataHead;
            public VertexData[] VData;
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
        public struct VertexData
        {
            public byte R, G, B;
            public byte PX, PY;
            public ushort X, Y;
            public float SomeFloat;
            public byte CONN;
            public ushort Null1;
        }
        public struct RawData
        {
            public float X, Y, Z;
            public float U, V, W;
            public bool CONN;
            public uint Diffuse;
            public float Nx, Ny, Nz;
        }
        #endregion
    }
}
