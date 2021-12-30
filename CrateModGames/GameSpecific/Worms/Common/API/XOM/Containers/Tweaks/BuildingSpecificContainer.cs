using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("BuildingSpecificContainer")]
    public class BuildingSpecificContainer : Container
    {
        public float ProjectileSizeScale;
        public float DamageIncrease;
        public float DamageRadiusIncrease;
        public float DeathExplosionDamage;
        public float DeathExplosionRadius;
        public byte BuildingNum;
        public byte FiringPlatformSize;
        public byte ResourceCost;
        public byte AttachedToBuilding; // can be -1 (FF)
        public byte BuildingRequired; //can be -1 (FF)
        public byte MinPercBlocksRemainingBeforeDeath;
        public ushort BuildingHealth;
        public byte Indestructible; // bool
        public float BlockHealthVariance;
        public VInt ResourceName = new VInt();
        public VInt BrickifiedFileName = new VInt();

        public override void Read(BinaryReader reader)
        {
            ProjectileSizeScale = reader.ReadSingle();
            DamageIncrease = reader.ReadSingle();
            DamageRadiusIncrease = reader.ReadSingle();
            DeathExplosionDamage = reader.ReadSingle();
            DeathExplosionRadius = reader.ReadSingle();
            BuildingNum = reader.ReadByte();
            FiringPlatformSize = reader.ReadByte();
            ResourceCost = reader.ReadByte();
            AttachedToBuilding = reader.ReadByte();
            BuildingRequired = reader.ReadByte();
            MinPercBlocksRemainingBeforeDeath = reader.ReadByte();
            BuildingHealth = reader.ReadUInt16();
            Indestructible = reader.ReadByte();
            BlockHealthVariance = reader.ReadSingle();
            ResourceName.Read(reader);
            BrickifiedFileName.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(ProjectileSizeScale);
            writer.Write(DamageIncrease);
            writer.Write(DamageRadiusIncrease);
            writer.Write(DeathExplosionDamage);
            writer.Write(DeathExplosionRadius);
            writer.Write(BuildingNum);
            writer.Write(FiringPlatformSize);
            writer.Write(ResourceCost);
            writer.Write(AttachedToBuilding);
            writer.Write(BuildingRequired);
            writer.Write(MinPercBlocksRemainingBeforeDeath);
            writer.Write(BuildingHealth);
            writer.Write(Indestructible);
            writer.Write(BlockHealthVariance);
            ResourceName.Write(writer);
            BrickifiedFileName.Write(writer);
        }
    }
}
