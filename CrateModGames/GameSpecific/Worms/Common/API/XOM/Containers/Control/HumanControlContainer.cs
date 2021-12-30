using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("HumanControlContainer")]
    public class HumanControlContainer : Container
    {
        public float IncreaseInertia; //0.005f
        public float DecreaseInertia; //0.001f
        public float MaxInertia; //0.1f
        public float MinInertia; //0.001f
        public float MouseXSpeed; //64f
        public float MouseYSpeed; //32f
        public float MouseZSpeed; //1f

        public override void Read(BinaryReader reader)
        {
            IncreaseInertia = reader.ReadSingle();
            DecreaseInertia = reader.ReadSingle();
            MaxInertia = reader.ReadSingle();
            MinInertia = reader.ReadSingle();
            MouseXSpeed = reader.ReadSingle();
            MouseYSpeed = reader.ReadSingle();
            MouseZSpeed = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(IncreaseInertia);
            writer.Write(DecreaseInertia);
            writer.Write(MaxInertia);
            writer.Write(MinInertia);
            writer.Write(MouseXSpeed);
            writer.Write(MouseYSpeed);
            writer.Write(MouseZSpeed);
        }
    }
}
