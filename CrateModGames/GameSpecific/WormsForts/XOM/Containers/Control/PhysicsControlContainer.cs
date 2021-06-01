using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("PhysicsControlContainer")]
    public class PhysicsControlContainer : Container
    {
        public float WindScale; //0.005f
        public float DefaultWindFactorAffectAllFortPot; //0.8f
        public float LowGravityMultiplier; //0.25f
        public float FortPotMaxFallDamageMultiplier; //2f

        public override void Read(BinaryReader reader)
        {
            WindScale = reader.ReadSingle();
            DefaultWindFactorAffectAllFortPot = reader.ReadSingle();
            LowGravityMultiplier = reader.ReadSingle();
            FortPotMaxFallDamageMultiplier = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(WindScale);
            writer.Write(DefaultWindFactorAffectAllFortPot);
            writer.Write(LowGravityMultiplier);
            writer.Write(FortPotMaxFallDamageMultiplier);
        }
    }
}
