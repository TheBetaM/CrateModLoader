using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("WeaponSettingsData")]
    public class WeaponSettingsData : Container
    {
        public byte Weapon; // Can be -1 (FF)
        public byte Crates;
        public byte Delay;

        public override void Read(BinaryReader reader)
        {
            Weapon = reader.ReadByte();
            Crates = reader.ReadByte();
            Delay = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Weapon);
            writer.Write(Crates);
            writer.Write(Delay);
        }
    }
}
