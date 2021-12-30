using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("Campaign")]
    public class Campaign : Container
    {

        public List<VInt> Missions;
        public VInt LevelName = new VInt();
        public VInt ScriptName = new VInt();
        private uint levelType;
        public VInt Brief = new VInt();
        public VInt Image = new VInt();
        public uint LevelNumber;
        public VInt Lock = new VInt();
        public ByteBool LongestWins = new ByteBool(); // Movie?
        public float AIPathNodeStartYOffset;
        public float AIPathNodeCollisionStep;

        public LevelTypes LevelType
        {
            get { return (LevelTypes)levelType; }
            set { levelType = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Missions = new List<VInt>();
            byte ContCount = reader.ReadByte();
            for (int i = 0; i < ContCount; i++)
            {
                VInt id = new VInt();
                id.Read(reader);
                Missions.Add(id);
            }

            LevelName.Read(reader);
            ScriptName.Read(reader);
            levelType = reader.ReadUInt32();
            Brief.Read(reader);
            Image.Read(reader);
            LevelNumber = reader.ReadUInt32();
            Lock.Read(reader);
            LongestWins.Read(reader);
            AIPathNodeStartYOffset = reader.ReadSingle();
            AIPathNodeCollisionStep = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)Missions.Count);
            for (int i = 0; i < Missions.Count; i++)
            {
                Missions[i].Write(writer);
            }

            LevelName.Write(writer);
            ScriptName.Write(writer);
            writer.Write(levelType);
            Brief.Write(writer);
            Image.Write(writer);
            writer.Write(LevelNumber);
            Lock.Write(writer);
            LongestWins.Write(writer);
            writer.Write(AIPathNodeStartYOffset);
            writer.Write(AIPathNodeCollisionStep);
        }
    }
}
