using System;

namespace Twinsanity
{
    public class SoundbankDescriptions : BaseSection
    {
        public new string NodeName = "SoundbankDescriptions";
        public System.IO.MemoryStream SoundBank = new System.IO.MemoryStream();

        public override uint Recalculate()
        {
            Size = (uint)(Records + 1) * 12;
            for (int i = 0; i <= Records - 1; i++)
            {
                _Item[i].Base = Offset + Base;
                _Item[i].Offset = Size;
                Size += _Item[i].Recalculate();
            }
            ContentSize = Size - (uint)(Records + 1) * 12;
            Size += (uint)SoundBank.Length;
            return Size;
        }

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            File.Position = Offset + Base;
            if (File.Position < File.Length)
            {
                Header = Reader.ReadUInt32();
                Records = (int)Reader.ReadUInt32();
                ContentSize = Reader.ReadUInt32();
                Array.Resize(ref _Item, Records);
                for (int i = 0; i <= Records - 1; i++)
                {
                    SoundDescription SD = new SoundDescription();
                    SD.Offset = Reader.ReadUInt32();
                    SD.Base = Offset + Base;
                    SD.Size = Reader.ReadUInt32();
                    SD.ID = Reader.ReadUInt32();
                    uint Pos = (uint)File.Position;
                    SD.Load(ref File, ref Reader);
                    File.Position = Pos;
                    _Item[i] = SD;
                }
                int a = 0;
                for (int i = 0; i <= _Item.Length - 1; i++)
                    a += (int)_Item[i].Size;
                File.Position += a;
                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(SoundBank);
                Writer.Write(Reader.ReadBytes((int)(Size - a - 12 * (_Item.Length + 1))));
            }
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            base.Add_Item<SoundDescription>(pos, indexes);
        }

        public override void Delete_Item(int pos, int[] indexes)
        {
            SoundDescription RSB = (SoundDescription)_Item[indexes[pos]];
            int D = (int)RSB.SoundSize;
            int O = (int)RSB.SoundOffset;
            for (int i = indexes[pos]; i <= Records - 2; i++)
            {
                SoundDescription SB = (SoundDescription)_Item[i + 1];
                SB.SoundOffset -= (uint)D;
                _Item[i] = SB;
            }
            System.IO.MemoryStream MS = new System.IO.MemoryStream((int)SoundBank.Length - D);
            System.IO.BinaryWriter MSW = new System.IO.BinaryWriter(MS);
            System.IO.BinaryReader SBR = new System.IO.BinaryReader(SoundBank);
            SoundBank.Position = 0;
            MSW.Write(SBR.ReadBytes(O));
            SoundBank.Position += D;
            MSW.Write(SBR.ReadBytes((int)(SoundBank.Length - SoundBank.Position)));
            SoundBank = MS;
            SoundBank.Position = 0;
            Records -= 1;
            Array.Resize(ref _Item, Records);
        }

        public override void Save(ref System.IO.FileStream File, ref System.IO.BinaryWriter Writer)
        {
            File.Position = Offset + Base;
            if (Records > 0)
            {
                Writer.Write(Header);
                Writer.Write(Records);
                Writer.Write(ContentSize);
                for (int i = 0; i <= Records - 1; i++)
                {
                    Writer.Write(_Item[i].Offset);
                    Writer.Write(_Item[i].Size);
                    Writer.Write(_Item[i].ID);
                }
                for (int i = 0; i <= Records - 1; i++)
                    _Item[i].Save(ref File, ref Writer);
                Writer.Write(SoundBank.ToArray());
            }
        }
    }
}
