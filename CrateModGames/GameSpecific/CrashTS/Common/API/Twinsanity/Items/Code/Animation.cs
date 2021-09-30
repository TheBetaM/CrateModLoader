using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity
{
    public class Animation : TwinsItem
    {
        public UInt32 Bitfield;
        public UInt32 UnkBlobSizePacked1;
        public UInt16 TimelineLength1;
        public List<BoneSettings> BonesSettings = new List<BoneSettings>();
        public List<Transformation> Transformations = new List<Transformation>();
        public List<Timeline> Timelines = new List<Timeline>();
        public UInt32 UnkBlobSizePacked2;
        public UInt16 TimelineLength2;
        public List<BoneSettings> BonesSettings2 = new List<BoneSettings>();
        public List<Transformation> Transformations2 = new List<Transformation>();
        public List<Timeline> Timelines2 = new List<Timeline>();

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            UnkBlobSizePacked1 &= ~0x7FU;
            UnkBlobSizePacked1 &= ~(0xFFEU << 0xA);
            UnkBlobSizePacked1 &= ~(0x3FFU << 0x16);
            UInt32 packed1 = (UInt32)BonesSettings.Count & 0x7F;
            packed1 |= (UInt32)(((Transformations.Count * 2) & 0xFFE) << 0xA);
            packed1 |= (UInt32)(Timelines.Count << 0x16);
            packed1 |= UnkBlobSizePacked1;
            writer.Write(packed1);
            UnkBlobSizePacked1 = packed1;
            writer.Write(TimelineLength1);
            foreach (var boneSetting in BonesSettings)
            {
                boneSetting.Write(writer);
            }
            foreach (var transformation in Transformations)
            {
                transformation.Write(writer);
            }
            foreach (var timeline in Timelines)
            {
                timeline.Write(writer);
            }
            UnkBlobSizePacked2 &= ~0x7FU;
            UnkBlobSizePacked2 &= ~(0xFFEU << 0xA);
            UnkBlobSizePacked2 &= ~(0x3FFU << 0x16);
            UInt32 packed2 = (UInt32)BonesSettings2.Count & 0x7F;
            packed2 |= (UInt32)(((Transformations2.Count * 2) & 0xFFE) << 0xA);
            packed2 |= (UInt32)(Timelines2.Count << 0x16);
            packed2 |= UnkBlobSizePacked2;
            writer.Write(packed2);
            UnkBlobSizePacked2 = packed2;
            writer.Write(TimelineLength2);
            foreach (var boneSetting in BonesSettings2)
            {
                boneSetting.Write(writer);
            }
            foreach (var transformation in Transformations2)
            {
                transformation.Write(writer);
            }
            foreach (var timeline in Timelines2)
            {
                timeline.Write(writer);
            }
        }

        public override void Load(BinaryReader reader, int size)
        {
            Bitfield = reader.ReadUInt32();
            UnkBlobSizePacked1 = reader.ReadUInt32();
            TimelineLength1 = reader.ReadUInt16();
            var bones = (UnkBlobSizePacked1 & 0x7F);
            var transformations = (UnkBlobSizePacked1 >> 0xA & 0xFFE) / 2;
            var timelines = (UnkBlobSizePacked1 >> 0x16);
            BonesSettings.Clear();
            for (var i = 0; i < bones; ++i)
            {
                BonesSettings.Add(new BoneSettings());
                BonesSettings[i].Read(reader);
            }
            Transformations.Clear();
            for (var i = 0; i < transformations; ++i)
            {
                Transformations.Add(new Transformation());
                Transformations[i].Read(reader);
            }
            Timelines.Clear();
            for (var i = 0; i < timelines; ++i)
            {
                Timelines.Add(new Timeline(TimelineLength1));
                Timelines[i].Read(reader);
            }
            UnkBlobSizePacked2 = reader.ReadUInt32();
            TimelineLength2 = reader.ReadUInt16();
            var blobSize = (UnkBlobSizePacked2 & 0x7F) * 0x8 + (UnkBlobSizePacked2 >> 0xA & 0xFFE) + (UnkBlobSizePacked2 >> 0x16) * TimelineLength2 * 0x2;

            bones = (UnkBlobSizePacked2 & 0x7F);
            transformations = (UnkBlobSizePacked2 >> 0xA & 0xFFE) / 2;
            timelines = (UnkBlobSizePacked2 >> 0x16);
            BonesSettings2.Clear();
            Transformations2.Clear();
            Timelines2.Clear();
            if (blobSize > 0)
            {
                for (var i = 0; i < bones; ++i)
                {
                    BonesSettings2.Add(new BoneSettings());
                    BonesSettings2[i].Read(reader);
                }
                for (var i = 0; i < transformations; ++i)
                {
                    Transformations2.Add(new Transformation());
                    Transformations2[i].Read(reader);
                }
                for (var i = 0; i < timelines; ++i)
                {
                    Timelines2.Add(new Timeline(TimelineLength2));
                    Timelines2[i].Read(reader);
                }
            }
        }

        public class BoneSettings
        {
            public Byte[] Unknown;
            public BoneSettings()
            {
                Unknown = new Byte[8];
            }
            public void Read(BinaryReader reader)
            {
                Unknown = reader.ReadBytes(Unknown.Length);
            }
            public void Write(BinaryWriter writer)
            {
                writer.Write(Unknown);
            }
        }

        public class Transformation
        {
            public Int16 Unknown;

            public Single Value
            {
                get
                {
                    return Unknown / 4096f;
                }
                set
                {
                    Unknown = (Int16)(value * 4096);
                }
            }

            public void Read(BinaryReader reader)
            {
                Unknown = reader.ReadInt16();
            }
            public void Write(BinaryWriter writer)
            {
                writer.Write(Unknown);
            }
        }

        public class Timeline
        {
            public List<Int16> UnknownInt16s;

            public Int16 GetOffset(int index)
            {
                return UnknownInt16s[index];
            }

            public void SetOffset(int index, Int16 value)
            {
                UnknownInt16s[index] = value;
            }

            public Timeline(UInt16 timelineLength)
            {
                UnknownInt16s = new List<Int16>(timelineLength);
            }
            public void Read(BinaryReader reader)
            {
                for (var i = 0; i < UnknownInt16s.Capacity; ++i)
                {
                    UnknownInt16s.Add(reader.ReadInt16());
                }
            }
            public void Write(BinaryWriter writer)
            {
                foreach (var offset in UnknownInt16s)
                {
                    writer.Write(offset);
                }
            }
        }

        protected override int GetSize()
        {
            var totalSize = 10; // Bitfield, blob packed, blob size helper
            totalSize += BonesSettings.Sum(d => d.Unknown.Length) + Transformations.Count * 2 + Timelines.Sum(r => r.UnknownInt16s.Count * 2);
            totalSize += 6; // blob packed 2, blob size helper 2
            totalSize += BonesSettings2.Sum(d => d.Unknown.Length) + Transformations2.Count * 2 + Timelines2.Sum(r => r.UnknownInt16s.Count * 2);
            return totalSize;
        }
    }
}
