using System;
using System.Collections.Generic;

namespace Twinsanity
{
    public class ID4Model : BaseItem
    {
        public new string NodeName = "ID4Model";
        public int SubModels;
        public _SubModel[] SubModel;

        private List<_Group> _Groups = new List<_Group>();

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(SubModels);
            for (int i = 0; i <= SubModels - 1; i++)
            {
                {
                    var withBlock = SubModel[i];
                    NSWriter.Write(withBlock.MaterialID);
                    NSWriter.Write(withBlock.BlockSize);
                    NSWriter.Write(withBlock.Vertexes);
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
                            NSWriter.Write(withBlock1.SomeGroup1Head);
                            Array.Resize(ref withBlock1.Group1Stuff, 2);
                            for (int n = 0; n <= 1; n++)
                                NSWriter.Write(withBlock1.Group1Stuff[n]);
                            NSWriter.Write(withBlock1.SomeGroup2Head);
                            Array.Resize(ref withBlock1.Group2Stuff, 8);
                            for (int n = 0; n <= 7; n++)
                                NSWriter.Write(withBlock1.Group2Stuff[n]);
                            NSWriter.Write(withBlock1.Signature2);
                            if (withBlock1.Struct3Head > 0)
                            {
                                NSWriter.Write(withBlock1.Struct3Head);
                                Array.Resize(ref withBlock1.Struct3, withBlock1.Vertexes);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.Struct3[j].ID1);
                                    NSWriter.Write(withBlock1.Struct3[j].ID2);
                                }
                                Array.Resize(ref withBlock1.Struct3End, 5);
                                for (int n = 0; n <= 4; n++)
                                    NSWriter.Write(withBlock1.Struct3End[n]);
                            }
                            if (withBlock1.Struct4Head > 0)
                            {
                                NSWriter.Write(withBlock1.Struct4Head);
                                Array.Resize(ref withBlock1.Struct4, withBlock1.Vertexes);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.Struct4[j].ID1);
                                    NSWriter.Write(withBlock1.Struct4[j].ID2);
                                }
                                NSWriter.Write(withBlock1.Struct4End);
                            }
                            if (withBlock1.Struct6Head > 0)
                            {
                                NSWriter.Write(withBlock1.Struct6Head);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                    NSWriter.Write(withBlock1.Struct6[j]);
                            }
                            if (withBlock1.Struct5Head > 0)
                            {
                                NSWriter.Write(withBlock1.Struct5Head);
                                Array.Resize(ref withBlock1.Struct5, withBlock1.Vertexes);
                                for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                {
                                    NSWriter.Write(withBlock1.Struct5[j].Float);
                                    NSWriter.Write(withBlock1.Struct5[j].a);
                                    NSWriter.Write(withBlock1.Struct5[j].b);
                                    NSWriter.Write(withBlock1.Struct5[j].ID);
                                    NSWriter.Write(withBlock1.Struct5[j].CONN);
                                    NSWriter.Write(withBlock1.Struct5[j].Null);
                                }
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

        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            SubModels = BSReader.ReadInt32();
            Array.Resize(ref SubModel, SubModels);
            for (int i = 0; i <= SubModels - 1; i++)
            {
                {
                    var withBlock = SubModel[i];
                    withBlock.MaterialID = BSReader.ReadUInt32();
                    withBlock.BlockSize = BSReader.ReadUInt32();
                    withBlock.Vertexes = BSReader.ReadInt32();
                    int cnt, offset;
                    cnt = 0;
                    offset = (int)ByteStream.Position;
                    withBlock.k = BSReader.ReadUInt16();
                    withBlock.c = BSReader.ReadUInt16();
                    withBlock.Null1 = BSReader.ReadUInt32();
                    withBlock.Something = BSReader.ReadUInt32();
                    withBlock.Null2 = BSReader.ReadUInt32();
                    Array.Resize(ref withBlock.Group, 0);
                    while ((cnt < withBlock.Vertexes) && (ByteStream.Position < offset + withBlock.BlockSize))
                    {
                        _Groups.Add(new _Group());
                        {
                            var withBlock1 = _Groups[_Groups.Count - 1];
                            Array.Resize(ref withBlock1.Struct3, 0);
                            Array.Resize(ref withBlock1.Struct4, 0);
                            Array.Resize(ref withBlock1.Struct5, 0);
                            Array.Resize(ref withBlock1.Struct6, 0);
                            withBlock1.SomeNum1 = BSReader.ReadUInt32();
                            withBlock1.Vertexes = BSReader.ReadByte();
                            cnt += withBlock1.Vertexes;
                            withBlock1.Some80h = BSReader.ReadByte();
                            withBlock1.Null1 = BSReader.ReadUInt16();
                            withBlock1.SomeNum2 = BSReader.ReadUInt32();
                            withBlock1.SomeNum3 = BSReader.ReadUInt32();
                            withBlock1.Null2 = BSReader.ReadUInt32();
                            withBlock1.Signature1 = BSReader.ReadUInt32();
                            withBlock1.SomeGroup1Head = BSReader.ReadUInt32();
                            Array.Resize(ref withBlock1.Group1Stuff, 2);
                            for (int n = 0; n <= 1; n++)
                                withBlock1.Group1Stuff[n] = BSReader.ReadUInt32();
                            withBlock1.SomeGroup2Head = BSReader.ReadUInt32();
                            Array.Resize(ref withBlock1.Group2Stuff, 8);
                            for (int n = 0; n <= 7; n++)
                                withBlock1.Group2Stuff[n] = BSReader.ReadUInt32();
                            withBlock1.Signature2 = BSReader.ReadUInt32();
                            uint head = BSReader.ReadUInt32();
                            while (head != 335544320)
                            {
                                switch (head & 255)
                                {
                                    case 3:
                                        {
                                            withBlock1.Struct3Head = head;
                                            Array.Resize(ref withBlock1.Struct3, withBlock1.Vertexes);
                                            for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                            {
                                                withBlock1.Struct3[j].ID1 = BSReader.ReadUInt32();
                                                withBlock1.Struct3[j].ID2 = BSReader.ReadUInt32();
                                            }
                                            Array.Resize(ref withBlock1.Struct3End, 5);
                                            for (int n = 0; n <= 4; n++)
                                                withBlock1.Struct3End[n] = BSReader.ReadUInt32();
                                            break;
                                        }

                                    case 4:
                                        {
                                            withBlock1.Struct4Head = head;
                                            Array.Resize(ref withBlock1.Struct4, withBlock1.Vertexes);
                                            for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                            {
                                                withBlock1.Struct4[j].ID1 = BSReader.ReadUInt32();
                                                withBlock1.Struct4[j].ID2 = BSReader.ReadUInt32();
                                            }
                                            withBlock1.Struct4End = BSReader.ReadUInt32();
                                            break;
                                        }

                                    case 6:
                                        {
                                            withBlock1.Struct6Head = head;
                                            Array.Resize(ref withBlock1.Struct6, withBlock1.Vertexes);
                                            for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                                withBlock1.Struct6[j] = BSReader.ReadUInt32();
                                            break;
                                        }

                                    case 5:
                                        {
                                            withBlock1.Struct5Head = head;
                                            Array.Resize(ref withBlock1.Struct5, withBlock1.Vertexes);
                                            for (int j = 0; j <= withBlock1.Vertexes - 1; j++)
                                            {
                                                withBlock1.Struct5[j].Float = BSReader.ReadSingle();
                                                withBlock1.Struct5[j].a = BSReader.ReadUInt32();
                                                withBlock1.Struct5[j].b = BSReader.ReadUInt32();
                                                withBlock1.Struct5[j].ID = BSReader.ReadByte();
                                                withBlock1.Struct5[j].CONN = BSReader.ReadByte();
                                                withBlock1.Struct5[j].Null = BSReader.ReadInt16();
                                            }

                                            break;
                                        }
                                }
                                head = BSReader.ReadUInt32();
                            }
                            withBlock1.EndSignature1 = head;
                            withBlock1.EndSignature2 = BSReader.ReadUInt32();
                            Array.Resize(ref withBlock1.leftovers, 0);
                        }
                    }
                    withBlock.Group = _Groups.ToArray();
                    _Groups.Clear();
                    Array.Resize(ref withBlock.Group[withBlock.Group.Length - 1].leftovers, (int)(withBlock.BlockSize + offset - ByteStream.Position));
                    withBlock.Group[withBlock.Group.Length - 1].leftovers = BSReader.ReadBytes((int)(withBlock.BlockSize + offset - ByteStream.Position));
                }
            }
        }

        #region STRUCTURES
        public struct _SubModel
        {
            // Primary Header
            public uint MaterialID;
            public uint BlockSize;
            public int Vertexes;
            public ushort k;
            public ushort c;
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
            public uint SomeGroup1Head;
            public uint[] Group1Stuff;
            public uint SomeGroup2Head;
            public uint[] Group2Stuff;
            public uint Signature2;
            public uint Struct3Head;
            public Position2[] Struct3;
            public uint[] Struct3End;
            public uint Struct4Head;
            public Position2[] Struct4;
            public uint Struct4End;
            public uint Struct6Head;
            public uint[] Struct6;
            public uint Struct5Head;
            public ID4Vertex[] Struct5;
            public uint EndSignature1;
            public uint EndSignature2;
            public byte[] leftovers;
        }
        public struct Position2
        {
            public uint ID1, ID2;
        }
        public struct ID4Vertex
        {
            public float Float;
            public uint a, b;
            public byte ID;
            public byte CONN;
            public Int16 Null;
        }
        public struct _Weight
        {
            public float X, Y, Z;
            public byte SomeByte;
            public byte CONN;
            public ushort Null1;
        }
        #endregion
    }
}
