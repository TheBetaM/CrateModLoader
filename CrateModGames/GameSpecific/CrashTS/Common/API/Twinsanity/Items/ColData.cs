using System.Collections.Generic;
using System.IO;

namespace Twinsanity
{
    public sealed class ColData : TwinsItem
    {
        private uint someNumber;
        private readonly uint mask = 0x3FFFF;
        private bool isEmpty;

        public ColData()
        {
            Triggers = new List<Trigger>();
            Groups = new List<GroupInfo>();
            Tris = new List<ColTri>();
            Vertices = new List<Pos>();
        }

        protected override int GetSize()
        {
            return isEmpty ? 0 : (20 + Triggers.Count * 32 + Groups.Count * 8 + Tris.Count * 8 + Vertices.Count * 16);
        }

        /// <summary>
        /// Write converted binary data to file.
        /// </summary>
        public override void Save(BinaryWriter writer)
        {
            if (isEmpty) return;
            if (someNumber > 0)
            {
                writer.Write(someNumber);
                writer.Write(Triggers.Count);
                writer.Write(Groups.Count);
                writer.Write(Tris.Count);
                writer.Write(Vertices.Count);
                for (int i = 0; i < Triggers.Count; i++)
                {
                    writer.Write(Triggers[i].X1);
                    writer.Write(Triggers[i].Y1);
                    writer.Write(Triggers[i].Z1);
                    writer.Write(Triggers[i].Flag1);
                    writer.Write(Triggers[i].X2);
                    writer.Write(Triggers[i].Y2);
                    writer.Write(Triggers[i].Z2);
                    writer.Write(Triggers[i].Flag2);
                }
                for (int i = 0; i < Groups.Count; i++)
                {
                    writer.Write(Groups[i].Size);
                    writer.Write(Groups[i].Offset);
                }
                for (int i = 0; i < Tris.Count; i++)
                {
                    ulong tmp = (ulong)Tris[i].Vert1 & mask;
                    tmp |= (ulong)(Tris[i].Vert2 & mask) << 18;
                    tmp |= (ulong)(Tris[i].Vert3 & mask) << 36;
                    tmp |= (ulong)(Tris[i].Surface & 0x3FF) << 54;
                    writer.Write(tmp);
                }
                for (int i = 0; i < Vertices.Count; i++)
                {
                    writer.Write(Vertices[i].X);
                    writer.Write(Vertices[i].Y);
                    writer.Write(Vertices[i].Z);
                    writer.Write(Vertices[i].W);
                }
            }
        }

        /////////PARENTS FUNCTION//////////
        public override void Load(BinaryReader reader, int size)
        {
            if (size < 20)
            {
                isEmpty = true;
                return;
            }
            someNumber = reader.ReadUInt32();
            uint triggerCount = reader.ReadUInt32();
            uint groupCount = reader.ReadUInt32();
            uint triCount = reader.ReadUInt32();
            uint vertexCount = reader.ReadUInt32();
            Triggers.Clear();
            Groups.Clear();
            Tris.Clear();
            Vertices.Clear();
            for (int i = 0; i < triggerCount; i++)
            {
                Trigger trg = new Trigger
                {
                    X1 = reader.ReadSingle(),
                    Y1 = reader.ReadSingle(),
                    Z1 = reader.ReadSingle(),
                    Flag1 = reader.ReadInt32(),
                    X2 = reader.ReadSingle(),
                    Y2 = reader.ReadSingle(),
                    Z2 = reader.ReadSingle(),
                    Flag2 = reader.ReadInt32()
                };
                Triggers.Add(trg);
            }
            for (int i = 0; i < groupCount; i++)
            {
                GroupInfo grp = new GroupInfo
                {
                    Size = reader.ReadUInt32(),
                    Offset = reader.ReadUInt32()
                };
                Groups.Add(grp);
            }
            for (int i = 0; i < triCount; i++)
            {
                ColTri tri = new ColTri();
                ulong legacy = reader.ReadUInt64();
                tri.Vert1 = (int)(legacy & mask);
                tri.Vert2 = (int)((legacy >> 18 * 1) & mask);
                tri.Vert3 = (int)((legacy >> 18 * 2) & mask);
                tri.Surface = (int)(legacy >> (18 * 3));
                Tris.Add(tri);
            }
            for (int i = 0; i < vertexCount; i++)
            {
                Pos vtx = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                Vertices.Add(vtx);
            }
        }

        #region STRUCTURES
        public struct Trigger
        {
            public float X1;
            public float Y1;
            public float Z1;
            public int Flag1;
            public float X2;
            public float Y2;
            public float Z2;
            public int Flag2;
        }
        public struct GroupInfo
        {
            public uint Size;
            public uint Offset;
        }
        public struct ColTri
        {
            public int Vert1;
            public int Vert2;
            public int Vert3;
            public int Surface;
        }
        #endregion

        public List<Trigger> Triggers { get; set; }
        public List<GroupInfo> Groups { get; set; }
        public List<ColTri> Tris { get; set; }
        public List<Pos> Vertices { get; set; }
    }
}
