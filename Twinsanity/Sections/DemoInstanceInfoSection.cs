using System;

namespace Twinsanity
{
    public class DemoInstanceInfoSection : InstanceInfoSection
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
                        case 1:
                            {
                                Behaviors Insts = new Behaviors();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 2:
                            {
                                FuckingShits Insts = new FuckingShits();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 3:
                            {
                                Positions Insts = new Positions();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 4:
                            {
                                Paths Insts = new Paths();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 5:
                            {
                                SurfaceBehaviours Insts = new SurfaceBehaviours();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 6:
                            {
                                DemoInstances Insts = new DemoInstances();
                                Insts.Offset = ElementOffset;
                                Insts.Base = Offset + Base;
                                Insts.Size = ElementSize;
                                Insts.ID = ElementID;
                                Insts.Load(ref File, ref Reader);
                                _Item[i] = Insts;
                                break;
                            }

                        case 7:
                            {
                                Triggers Trigs = new Triggers();
                                Trigs.Offset = ElementOffset;
                                Trigs.Base = Offset + Base;
                                Trigs.Size = ElementSize;
                                Trigs.ID = ElementID;
                                Trigs.Load(ref File, ref Reader);
                                _Item[i] = Trigs;
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
    }
}
