using System;

namespace Twinsanity
{
    public class GraphicsSection : BaseSection
    {
        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            File.Position = Offset + Base;
            if (File.Position < File.Length && Size > 0)
            {
                Header = Reader.ReadUInt32();
                Records = (int)Reader.ReadUInt32();
                ContentSize = Reader.ReadUInt32();
                Array.Resize(ref _Item, Records);
                for (int i = 0; i <= Records - 1; i++)
                {
                    uint ElementOffset = Reader.ReadUInt32();
                    uint ElementSize = Reader.ReadUInt32();
                    uint ElementID = Reader.ReadUInt32();
                    uint Pos = (uint)File.Position;
                    switch (ElementID)
                    {
                        case 0:
                            {
                                Textures Texs = new Textures();
                                Texs.Offset = ElementOffset;
                                Texs.Base = Offset + Base;
                                Texs.Size = ElementSize;
                                Texs.ID = ElementID;
                                Texs.Load(ref File, ref Reader);
                                _Item[i] = Texs;
                                break;
                            }

                        case 1:
                            {
                                Materials Mtrls = new Materials();
                                Mtrls.Offset = ElementOffset;
                                Mtrls.Base = Offset + Base;
                                Mtrls.Size = ElementSize;
                                Mtrls.ID = ElementID;
                                Mtrls.Load(ref File, ref Reader);
                                _Item[i] = Mtrls;
                                break;
                            }

                        case 2:
                            {
                                Models Mdls = new Models();
                                Mdls.Offset = ElementOffset;
                                Mdls.Base = Offset + Base;
                                Mdls.Size = ElementSize;
                                Mdls.ID = ElementID;
                                Mdls.Load(ref File, ref Reader);
                                _Item[i] = Mdls;
                                break;
                            }

                        case 3:
                        case 6:
                            {
                                GCs GCs = new GCs();
                                GCs.Offset = ElementOffset;
                                GCs.Base = Offset + Base;
                                GCs.Size = ElementSize;
                                GCs.ID = ElementID;
                                GCs.Load(ref File, ref Reader);
                                _Item[i] = GCs;
                                break;
                            }

                        case 7:
                            {
                                Terrains Ts = new Terrains();
                                Ts.Offset = ElementOffset;
                                Ts.Base = Offset + Base;
                                Ts.Size = ElementSize;
                                Ts.ID = ElementID;
                                Ts.Load(ref File, ref Reader);
                                _Item[i] = Ts;
                                break;
                            }

                        case 4:
                            {
                                ID4Models Mdls = new ID4Models();
                                Mdls.Offset = ElementOffset;
                                Mdls.Base = Offset + Base;
                                Mdls.Size = ElementSize;
                                Mdls.ID = ElementID;
                                Mdls.Load(ref File, ref Reader);
                                _Item[i] = Mdls;
                                break;
                            }

                        default:
                            {
                                BaseSection BS = new BaseSection();
                                BS.Offset = ElementOffset;
                                BS.Base = Offset + Base;
                                BS.Size = ElementSize;
                                BS.ID = ElementID;
                                BS.Load(ref File, ref Reader);
                                _Item[i] = BS;
                                break;
                            }
                    }
                    File.Position = Pos;
                }
            }
        }


        public override void Add_Item(int pos, params int[] indexes)
        {
            _Item[indexes[pos]].Add_Item(pos - 1, indexes);
        }
        public override void Delete_Item(int pos, params int[] indexes)
        {
            _Item[indexes[pos]].Delete_Item(pos - 1, indexes);
        }
    }
}
