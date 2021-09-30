using System;

namespace Twinsanity
{
    /// <summary>
    /// Represents an unknown section in the chunk tree
    /// </summary>
    public class BaseSection : BaseObject
    {
        public uint Header;
        public uint ContentSize;
        public string NodeName = "Unknown Section";

        protected override void Load<T>(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            File.Position = Offset + Base;
            if (File.Position < File.Length && Size > 0)
            {
                Header = Reader.ReadUInt32();
                Records = Reader.ReadInt32();
                ContentSize = Reader.ReadUInt32();
                Array.Resize(ref _Item, Records);
                for (int i = 0; i <= Records - 1; i++)
                {
                    T UItem = new T
                    {
                        Offset = Reader.ReadUInt32(),
                        Base = Offset + Base,
                        Size = Reader.ReadUInt32(),
                        ID = Reader.ReadUInt32()
                    };

                    uint Pos = (uint)File.Position;
                    UItem.Load(ref File, ref Reader);
                    _Item[i] = UItem;
                    File.Position = Pos;
                }
            }
        }

        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            Load<BaseItem>(ref File, ref Reader);
        }

        public override uint Recalculate()
        {
            if (Records > 0)
            {
                Size = (uint)(Records + 1) * 12;
                for (int i = 0; i <= Records - 1; i++)
                {
                    _Item[i].Base = Offset + Base;
                    _Item[i].Offset = Size;
                    Size += _Item[i].Recalculate();
                }
                ContentSize = (uint)(Size - (Records + 1) * 12);
            }
            else
                Size = 0;
            return Size;
        }


        public override BaseObject Get_Item(int pos, params int[] indexes)
        {
            if (pos <= 1)
                return _Item[indexes[pos]];
            else
                return _Item[indexes[pos]].Get_Item(pos - 1, indexes);
        }

        public override void Put_Item(BaseObject It, int pos, params int[] indexes)
        {
            if (pos <= 1)
            {
                _Item[indexes[pos]] = It;
                _Item[indexes[pos]].UpdateStream();
            }
            else
                _Item[indexes[pos]].Put_Item(It, pos - 1, indexes);
        }

        public override System.IO.MemoryStream Get_Stream(int pos, params int[] indexes)
        {
            return _Item[indexes[pos]].Get_Stream(pos - 1, indexes);
        }

        public override void Put_Stream(System.IO.MemoryStream It, int pos, params int[] indexes)
        {
            _Item[indexes[pos]].Put_Stream(It, pos - 1, indexes);
        }

        protected virtual void Add_Item<T>(int pos, params int[] indexes) where T : BaseItem, new()
        {
            if (Records > 0)
            {
                uint max_id = 0;
                for (int i = 0; i <= Records - 1; i++)
                {
                    if (_Item[i].ID > max_id)
                        max_id = _Item[i].ID;
                }
                Records++;
                Array.Resize(ref _Item, Records);
                _Item[Records - 1] = new T { ID = max_id + 1 };
                _Item[Records - 1].UpdateStream();
            }
            else
            {
                Records = 1;
                Array.Resize(ref _Item, Records);
                _Item[Records - 1] = new T { ID = 0 };
                _Item[Records - 1].UpdateStream();
            }
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            Add_Item<BaseItem>(pos, indexes);
        }

        public override void Delete_Item(int pos, params int[] indexes)
        {
            for (int i = indexes[pos]; i <= Records - 2; i++)
                _Item[i] = _Item[i + 1];
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
            }
        }

        public override void UpdateStream()
        {
            throw new NotImplementedException();
        }
    }
}
