using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    // todo: research values
    //[XOM_TypeName("CrateContainer")]
    public class CrateContainer : Container
    {
        public float IsStatic; //50f
        public float FrictionCoefficient; //0.25f
        public byte VelocityAtRestLimit; //0x01
        public byte RotSpeedAtRestLimit; //0x00
        public float ExplosionRadius; //0.2f
        public float ExplosionInnerRadius; //0.0125f
        public float ExplosionDamage; //1f
        public uint ExplosionForce; //0x4
        public float CreateHeightAboveGround; //0.2f
        public float Unk10; //0.2f
        public float Unk11; //2f
        public uint Unk12; //0x4
        public float Unk13; //20f
        public float Unk14; //5f
        public float Unk15; //800f

        public override void Read(BinaryReader reader)
        {
            IsStatic = reader.ReadSingle();
            FrictionCoefficient = reader.ReadSingle();
            VelocityAtRestLimit = reader.ReadByte();
            RotSpeedAtRestLimit = reader.ReadByte();
            ExplosionRadius = reader.ReadSingle();
            ExplosionInnerRadius = reader.ReadSingle();
            ExplosionDamage = reader.ReadSingle();
            ExplosionForce = reader.ReadUInt32();
            CreateHeightAboveGround = reader.ReadSingle();
            Unk10 = reader.ReadSingle();
            Unk11 = reader.ReadSingle();
            Unk12 = reader.ReadUInt32();
            Unk13 = reader.ReadSingle();
            Unk14 = reader.ReadSingle();
            Unk15 = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(IsStatic);
            writer.Write(FrictionCoefficient);
            writer.Write(VelocityAtRestLimit);
            writer.Write(RotSpeedAtRestLimit);
            writer.Write(ExplosionRadius);
            writer.Write(ExplosionInnerRadius);
            writer.Write(ExplosionDamage);
            writer.Write(ExplosionForce);
            writer.Write(CreateHeightAboveGround);
            writer.Write(Unk10);
            writer.Write(Unk11);
            writer.Write(Unk12);
            writer.Write(Unk13);
            writer.Write(Unk14);
            writer.Write(Unk15);
        }
    }
}
