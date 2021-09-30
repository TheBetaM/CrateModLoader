using System;
using System.Collections.Generic;

namespace Twinsanity
{
    public class Scenery : BaseItem
    {
        public new string NodeName = "Scenery";
        public uint Header;
        public uint LevelNameLength;
        public string LevelName;
        public uint Flags;
        public uint EntryHeader;
        public byte NullByte;
        public uint SBID;
        public byte[] Garbadge;
        public Entry3[] E3;

        private List<Entry3> _Entries = new List<Entry3>();

        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(Header);
            NSWriter.Write(LevelNameLength);
            NSWriter.Write(LevelName);
            NSWriter.Write(Flags);
            NSWriter.Write(EntryHeader);
            NSWriter.Write(NullByte);
            NSWriter.Write(SBID);
            NSWriter.Write(Garbadge);
            for (int j = 0; j <= E3.Length - 1; j++)
            {
                {
                    var withBlock = E3[j];
                    NSWriter.Write(withBlock.EntryHeader);
                    NSWriter.Write(withBlock.GCCount);
                    NSWriter.Write(withBlock.SBCount);
                    for (int i = 0; i <= withBlock.GCCount + withBlock.SBCount - 1; i++)
                    {
                        NSWriter.Write(withBlock.Vector1[i].X);
                        NSWriter.Write(withBlock.Vector1[i].Y);
                        NSWriter.Write(withBlock.Vector1[i].Z);
                        NSWriter.Write(withBlock.Vector1[i].W);
                        NSWriter.Write(withBlock.Vector2[i].X);
                        NSWriter.Write(withBlock.Vector2[i].Y);
                        NSWriter.Write(withBlock.Vector2[i].Z);
                        NSWriter.Write(withBlock.Vector2[i].W);
                    }
                    for (int i = 0; i <= withBlock.GCCount - 1; i++)
                        NSWriter.Write(withBlock.GCID[i]);
                    for (int i = 0; i <= withBlock.SBCount - 1; i++)
                        NSWriter.Write(withBlock.SBID[i]);
                    for (int i = 0; i <= withBlock.GCCount + withBlock.SBCount - 1; i++)
                    {
                        NSWriter.Write(withBlock.ChunkMatrix[i].x1);
                        NSWriter.Write(withBlock.ChunkMatrix[i].y1);
                        NSWriter.Write(withBlock.ChunkMatrix[i].z1);
                        NSWriter.Write(withBlock.ChunkMatrix[i].w1);
                        NSWriter.Write(withBlock.ChunkMatrix[i].x2);
                        NSWriter.Write(withBlock.ChunkMatrix[i].y2);
                        NSWriter.Write(withBlock.ChunkMatrix[i].z2);
                        NSWriter.Write(withBlock.ChunkMatrix[i].w2);
                        NSWriter.Write(withBlock.ChunkMatrix[i].x3);
                        NSWriter.Write(withBlock.ChunkMatrix[i].y3);
                        NSWriter.Write(withBlock.ChunkMatrix[i].z3);
                        NSWriter.Write(withBlock.ChunkMatrix[i].w3);
                        NSWriter.Write(withBlock.ChunkMatrix[i].x4);
                        NSWriter.Write(withBlock.ChunkMatrix[i].y4);
                        NSWriter.Write(withBlock.ChunkMatrix[i].z4);
                        NSWriter.Write(withBlock.ChunkMatrix[i].w4);
                    }
                    NSWriter.Write(withBlock.leftovers);
                }
            }
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        /////////PARENTS FUNCTION//////////
        protected override void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
            Array.Resize(ref E3, 0);
            Header = BSReader.ReadUInt32();
            LevelNameLength = BSReader.ReadUInt32();
            LevelName = BSReader.ReadChars((int)LevelNameLength).ToString();
            Flags = BSReader.ReadUInt32();
            EntryHeader = BSReader.ReadUInt32();
            NullByte = BSReader.ReadByte();
            SBID = BSReader.ReadUInt32();
            uint tmp = 0;
            int p = (int)ByteStream.Position;
            while ((!(tmp == 5651)) && (ByteStream.Position < ByteStream.Length))
            {
                tmp = BSReader.ReadByte();
                if (tmp == 19)
                {
                    ByteStream.Position -= 1;
                    tmp = BSReader.ReadUInt32();
                    if (!(tmp == 5651))
                        ByteStream.Position -= 3;
                    else
                        ByteStream.Position -= 4;
                }
            }
            p = (int)ByteStream.Position - p;
            ByteStream.Position -= p;
            Garbadge = BSReader.ReadBytes(p);
            if (tmp == 5651 && (ByteStream.Position < ByteStream.Length))
            {
                while ((ByteStream.Position < ByteStream.Length))
                {
                    Entry3 withBlock = new Entry3();
                    {
                        withBlock.EntryHeader = BSReader.ReadUInt32();
                        withBlock.GCCount = BSReader.ReadUInt16();
                        withBlock.SBCount = BSReader.ReadUInt16();
                        Array.Resize(ref withBlock.Vector1, withBlock.GCCount + withBlock.SBCount);
                        Array.Resize(ref withBlock.Vector2, withBlock.GCCount + withBlock.SBCount);
                        Array.Resize(ref withBlock.GCID, withBlock.GCCount);
                        Array.Resize(ref withBlock.SBID, withBlock.SBCount);
                        Array.Resize(ref withBlock.ChunkMatrix, withBlock.GCCount + withBlock.SBCount);
                        for (int i = 0; i <= withBlock.GCCount + withBlock.SBCount - 1; i++)
                        {
                            withBlock.Vector1[i].X = BSReader.ReadSingle();
                            withBlock.Vector1[i].Y = BSReader.ReadSingle();
                            withBlock.Vector1[i].Z = BSReader.ReadSingle();
                            withBlock.Vector1[i].W = BSReader.ReadSingle();
                            withBlock.Vector2[i].X = BSReader.ReadSingle();
                            withBlock.Vector2[i].Y = BSReader.ReadSingle();
                            withBlock.Vector2[i].Z = BSReader.ReadSingle();
                            withBlock.Vector2[i].W = BSReader.ReadSingle();
                        }
                        for (int i = 0; i <= withBlock.GCCount - 1; i++)
                            withBlock.GCID[i] = BSReader.ReadUInt32();
                        for (int i = 0; i <= withBlock.SBCount - 1; i++)
                            withBlock.SBID[i] = BSReader.ReadUInt32();
                        for (int i = 0; i <= withBlock.GCCount + withBlock.SBCount - 1; i++)
                        {
                            withBlock.ChunkMatrix[i].x1 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].y1 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].z1 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].w1 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].x2 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].y2 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].z2 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].w2 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].x3 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].y3 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].z3 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].w3 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].x4 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].y4 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].z4 = BSReader.ReadSingle();
                            withBlock.ChunkMatrix[i].w4 = BSReader.ReadSingle();
                        }
                        tmp = 0;
                        var len = ByteStream.Position;
                        while ((tmp != 5651) & (ByteStream.Position < ByteStream.Length))
                        {
                            tmp = BSReader.ReadByte();
                            if (tmp == 19)
                            {
                                ByteStream.Position -= 1;
                                tmp = BSReader.ReadUInt32();
                                if (tmp != 5651)
                                    ByteStream.Position -= 3;
                                else
                                    ByteStream.Position -= 4;
                            }
                        }
                        len = ByteStream.Position - len;
                        ByteStream.Position -= len;
                        withBlock.leftovers = BSReader.ReadBytes((int)len);
                    }
                    _Entries.Add(withBlock);
                }
                E3 = _Entries.ToArray();
            }
        }

        #region STRUCTURES
        public struct Entry3
        {
            public uint EntryHeader; // 0x13160000
            public ushort GCCount;
            public ushort SBCount;
            public RM2.Coordinate4[] Vector1;
            public RM2.Coordinate4[] Vector2;
            public uint[] GCID;
            public uint[] SBID;
            public Matrix4[] ChunkMatrix;
            // Dim EntryMatrix() As ExtMatrix4
            public byte[] leftovers;
        }

        public struct Matrix4
        {
            public float x1, y1, z1, w1;
            public float x2, y2, z2, w2;
            public float x3, y3, z3, w3;
            public float x4, y4, z4, w4;
        }
        public struct ExtMatrix4
        {
            public Matrix4 M;
            public uint[] leftovers;
        }
        #endregion
    }
}
