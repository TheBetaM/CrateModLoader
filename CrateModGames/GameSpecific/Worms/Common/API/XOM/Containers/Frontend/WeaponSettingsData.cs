using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("WeaponSettingsData")]
    public class WeaponSettingsData : Container
    {
        public byte Weapon; // Can be -1 (FF) ammo
        public byte Crates;
        public byte Delay;
        public uint Power; // 3D

        public override void Read(BinaryReader reader)
        {
            if (ParentFile.FileGame == WormsGame.Forts)
            {
                Weapon = reader.ReadByte();
                Crates = reader.ReadByte();
                Delay = reader.ReadByte();
            }
            else if (ParentFile.FileGame == WormsGame.Worms4)
            {
                Weapon = (byte)reader.ReadUInt32();
                Crates = (byte)reader.ReadUInt32();
                Delay = (byte)reader.ReadUInt32();
            }
            else
            {
                Weapon = (byte)reader.ReadUInt32();
                Crates = (byte)reader.ReadUInt32();
                Power = reader.ReadUInt32();
                Delay = (byte)reader.ReadUInt32();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            if (ParentFile.FileGame == WormsGame.Forts)
            {
                writer.Write(Weapon);
                writer.Write(Crates);
                writer.Write(Delay);
            }
            else if (ParentFile.FileGame == WormsGame.Worms4)
            {
                writer.Write((uint)Weapon);
                writer.Write((uint)Crates);
                writer.Write((uint)Delay);
            }
            else
            {
                writer.Write((uint)Weapon);
                writer.Write((uint)Crates);
                writer.Write(Power);
                writer.Write((uint)Delay);
            }
            
        }
    }
}
