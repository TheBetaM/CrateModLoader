using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("Mission")]
    public class Mission : Container
    {
        public VInt LevelName = new VInt();
        public VInt ScriptName = new VInt();
        private uint levelType;
        public VInt Brief = new VInt();
        public VInt Image = new VInt();
        public uint LevelNumber;
        public VInt Lock = new VInt();
        public VInt Movie = new VInt();
        public float AIPathNodeStartYOffset;
        public float AIPathNodeCollisionStep;

        public LevelTypes LevelType
        {
            get { return (LevelTypes)levelType; }
            set { levelType = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            LevelName.Read(reader);
            ScriptName.Read(reader);
            levelType = reader.ReadUInt32();
            Brief.Read(reader);
            Image.Read(reader);
            LevelNumber = reader.ReadUInt32();
            Lock.Read(reader);
            Movie.Read(reader);
            AIPathNodeStartYOffset = reader.ReadSingle();
            AIPathNodeCollisionStep = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            LevelName.Write(writer);
            ScriptName.Write(writer);
            writer.Write(levelType);
            Brief.Write(writer);
            Image.Write(writer);
            writer.Write(LevelNumber);
            Lock.Write(writer);
            Movie.Write(writer);
            writer.Write(AIPathNodeStartYOffset);
            writer.Write(AIPathNodeCollisionStep);
        }
    }
}
