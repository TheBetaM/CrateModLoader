using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("AudioControlContainer")]
    public class AudioControlContainer : Container
    {
        public float BounceVelocitySqToVolume; //400f
        public float MinBounceVolume; //0.1f
        public float GlobalRollOffFactor; //1f
        public float GlobalDopplerFactor; //1f
        public float GlobalDistanceFactor; //0.5f

        public override void Read(BinaryReader reader)
        {
            BounceVelocitySqToVolume = reader.ReadSingle();
            MinBounceVolume = reader.ReadSingle();
            GlobalRollOffFactor = reader.ReadSingle();
            GlobalDopplerFactor = reader.ReadSingle();
            GlobalDistanceFactor = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(BounceVelocitySqToVolume);
            writer.Write(MinBounceVolume);
            writer.Write(GlobalRollOffFactor);
            writer.Write(GlobalDopplerFactor);
            writer.Write(GlobalDistanceFactor);
        }
    }
}
