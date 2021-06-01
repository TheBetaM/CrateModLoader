using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("PowerUpDataContainer")]
    public class PowerUpDataContainer : Container
    {
        // All 2f by default
        public float MortarDamageMultiplier;
        public float MortarRadiusMultiplier;
        public float ClusterDamageMultiplier;
        public float ClusterRadiusMultiplier;
        public float CatapultDamageMultiplier;
        public float CatapultRadiusMultiplier;
        public float FireGrenadeDamageMultiplier;
        public float FireGrenadeRadiusMultiplier;
        public float TowerStrengthMultiplier;
        public float KeepStrengthMultiplier;
        public float WormMoveMultiplier;

        public override void Read(BinaryReader reader)
        {
            MortarDamageMultiplier = reader.ReadSingle();
            MortarRadiusMultiplier = reader.ReadSingle();
            ClusterDamageMultiplier = reader.ReadSingle();
            ClusterRadiusMultiplier = reader.ReadSingle();
            CatapultDamageMultiplier = reader.ReadSingle();
            CatapultRadiusMultiplier = reader.ReadSingle();
            FireGrenadeDamageMultiplier = reader.ReadSingle();
            FireGrenadeRadiusMultiplier = reader.ReadSingle();
            TowerStrengthMultiplier = reader.ReadSingle();
            KeepStrengthMultiplier = reader.ReadSingle();
            WormMoveMultiplier = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(MortarDamageMultiplier);
            writer.Write(MortarRadiusMultiplier);
            writer.Write(ClusterDamageMultiplier);
            writer.Write(ClusterRadiusMultiplier);
            writer.Write(CatapultDamageMultiplier);
            writer.Write(CatapultRadiusMultiplier);
            writer.Write(FireGrenadeDamageMultiplier);
            writer.Write(FireGrenadeRadiusMultiplier);
            writer.Write(TowerStrengthMultiplier);
            writer.Write(KeepStrengthMultiplier);
            writer.Write(WormMoveMultiplier);
        }
    }
}
