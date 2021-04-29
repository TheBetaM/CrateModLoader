using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_File_CRT
    {

        public enum CrateType : byte
        {
            NULL = 255,
            Wireframe = 0,
            Blank = 1,
            Life = 2,
            Aku = 3,
            Bounce = 4,
            Pickup = 5,
            Fruit = 6,
            Checkpoint = 7,
            Slot = 8,
            TNT = 9,
            Time1 = 10,
            Time2 = 11,
            Time3 = 12,
            IronBounce = 13,
            IronSwitch = 14,
            Steel = 15,
            Nitro = 16,
            NitroSwitch = 17,
            Proximity = 18,
            Reinforced = 19,
            Invisibility = 20,
        }

        public TWOC_File_CRT()
        {
            CrateGroups = new List<CrateGroup>();
            Header = 4;
        }
        public TWOC_File_CRT(string path, bool isGC)
        {
            CrateGroups = new List<CrateGroup>();
            if (!isGC)
            {
                Load(path);
            }
            else
            {
                Load(path);
            }
        }

        public List<CrateGroup> CrateGroups;
        public uint Header; // usually 4

        public class CrateGroup
        {
            public List<Crate> Crates; //min. 1?
            public ushort ID;
            public TWOC_Vector3 Rot;
            public ushort unkFlags;
        }
        
        public class Crate
        {
            public CrateType Type = CrateType.NULL;
            public CrateType TypeTT = CrateType.NULL;
            public CrateType Type3 = CrateType.NULL;
            public CrateType Type4 = CrateType.NULL;
            public byte[] unkFlags; //10
            public TWOC_Vector3 Pos;
            public byte[] unkFlags2; //14
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader reader = new BinaryReader(fileStream);
                Header = reader.ReadUInt32();
                ushort GroupCount = reader.ReadUInt16();
                CrateGroups = new List<CrateGroup>();
                for (int i = 0; i < GroupCount; i++)
                {
                    CrateGroup Group = new CrateGroup();
                    Group.Crates = new List<Crate>();
                    Crate BaseCrate = new Crate();
                    BaseCrate.Pos = new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    Group.ID = reader.ReadUInt16();
                    ushort CrateCount = reader.ReadUInt16();
                    Group.unkFlags = reader.ReadUInt16();
                    Group.Rot = new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    BaseCrate.unkFlags = reader.ReadBytes(10);
                    BaseCrate.Type = (CrateType)reader.ReadByte();
                    BaseCrate.TypeTT = (CrateType)reader.ReadByte();
                    BaseCrate.Type3 = (CrateType)reader.ReadByte();
                    BaseCrate.Type4 = (CrateType)reader.ReadByte();
                    BaseCrate.unkFlags2 = reader.ReadBytes(14);
                    Group.Crates.Add(BaseCrate);

                    for (int c = 0; c < CrateCount - 1; c++)
                    {
                        Crate Crate = new Crate();
                        Crate.Pos = new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        Crate.unkFlags = reader.ReadBytes(10);
                        Crate.Type = (CrateType)reader.ReadByte();
                        Crate.TypeTT = (CrateType)reader.ReadByte();
                        Crate.Type3 = (CrateType)reader.ReadByte();
                        Crate.Type4 = (CrateType)reader.ReadByte();
                        Crate.unkFlags2 = reader.ReadBytes(14);
                        Group.Crates.Add(Crate);
                    }

                    CrateGroups.Add(Group);
                }
            }
        }

        public void Save(string path)
        {
            VerifyCrates();
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(Header);
                writer.Write((ushort)CrateGroups.Count);
                for (int i = 0; i < CrateGroups.Count; i++)
                {
                    CrateGroup Group = CrateGroups[i];
                    writer.Write(Group.Crates[0].Pos.X);
                    writer.Write(Group.Crates[0].Pos.Y);
                    writer.Write(Group.Crates[0].Pos.Z);
                    writer.Write(Group.ID);
                    writer.Write((ushort)Group.Crates.Count);
                    writer.Write(Group.unkFlags);
                    writer.Write(Group.Rot.X);
                    writer.Write(Group.Rot.Y);
                    writer.Write(Group.Rot.Z);
                    writer.Write(Group.Crates[0].unkFlags);
                    writer.Write((byte)Group.Crates[0].Type);
                    writer.Write((byte)Group.Crates[0].TypeTT);
                    writer.Write((byte)Group.Crates[0].Type3);
                    writer.Write((byte)Group.Crates[0].Type4);
                    writer.Write(Group.Crates[0].unkFlags2);

                    for (int c = 0; c < Group.Crates.Count; c++)
                    {
                        if (c != 0)
                        {
                            Crate Crate = Group.Crates[c];
                            writer.Write(Crate.Pos.X);
                            writer.Write(Crate.Pos.Y);
                            writer.Write(Crate.Pos.Z);
                            writer.Write(Crate.unkFlags);
                            writer.Write((byte)Crate.Type);
                            writer.Write((byte)Crate.TypeTT);
                            writer.Write((byte)Crate.Type3);
                            writer.Write((byte)Crate.Type4);
                            writer.Write(Crate.unkFlags2);
                        }
                    }

                }
            }
        }

        public void VerifyCrates()
        {
            // Game breaks if there's more than 128 crate groups or 256 crates total
            if (CrateGroups.Count > 128)
            {
                while (CrateGroups.Count > 128)
                {
                    CrateGroups.RemoveAt(128);
                }
            }
            int CrateCount = 0;
            foreach (CrateGroup Group in CrateGroups)
            {
                CrateCount += Group.Crates.Count;
            }
            if (CrateCount > 256)
            {
                while (CrateCount > 256)
                {
                    CrateGroups.RemoveAt(CrateGroups.Count - 1);
                    CrateCount = 0;
                    foreach (CrateGroup Group in CrateGroups)
                    {
                        CrateCount += Group.Crates.Count;
                    }
                }
            }
        }

        public ushort GetCrateCount()
        {
            ushort CrateCount = 0;
            foreach (CrateGroup Group in CrateGroups)
            {
                CrateCount += (ushort)Group.Crates.Count;
            }
            return CrateCount;
        }

    }
}
