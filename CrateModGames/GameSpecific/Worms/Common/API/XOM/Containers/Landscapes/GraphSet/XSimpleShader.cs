using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //[XOM_TypeName("XSimpleShader")]
    public class XSimpleShader : Container
    {
        public List<VInt> TextureStages = new List<VInt>();
        public List<VInt> Attributes = new List<VInt>();
        private uint UnkInt;
        public VInt Name = new VInt();

        public override void Read(BinaryReader reader)
        {
            byte TexCount = reader.ReadByte();
            for (int i = 0; i < TexCount; i++)
            {
                VInt Tex = new VInt();
                Tex.Read(reader);
                TextureStages.Add(Tex);
            }
            byte AtCount = reader.ReadByte();
            for (int i = 0; i < AtCount; i++)
            {
                VInt Atr = new VInt();
                Atr.Read(reader);
                TextureStages.Add(Atr);
            }

            UnkInt = reader.ReadUInt32();
            Name.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)TextureStages.Count);
            for (int i = 0; i < TextureStages.Count; i++)
            {
                TextureStages[i].Write(writer);
            }
            writer.Write((byte)Attributes.Count);
            for (int i = 0; i < Attributes.Count; i++)
            {
                Attributes[i].Write(writer);
            }
            writer.Write(UnkInt);
            Name.Write(writer);
        }
    }
}
