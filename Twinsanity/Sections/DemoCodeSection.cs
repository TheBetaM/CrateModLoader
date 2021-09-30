using System;

namespace Twinsanity
{
    public class DemoCodeSection : CodeSection
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
                                DemoGameObjects GObjs = new DemoGameObjects();
                                GObjs.Offset = ElementOffset;
                                GObjs.Base = Offset + Base;
                                GObjs.Size = ElementSize;
                                GObjs.ID = ElementID;
                                GObjs.Load(ref File, ref Reader);
                                _Item[i] = GObjs;
                                break;
                            }

                        case 1:
                            {
                                Scripts Srpts = new Scripts();
                                Srpts.Offset = ElementOffset;
                                Srpts.Base = Offset + Base;
                                Srpts.Size = ElementSize;
                                Srpts.ID = ElementID;
                                Srpts.Load(ref File, ref Reader);
                                _Item[i] = Srpts;
                                break;
                            }

                        case 2:
                            {
                                Animations Anims = new Animations();
                                Anims.Offset = ElementOffset;
                                Anims.Base = Offset + Base;
                                Anims.Size = ElementSize;
                                Anims.ID = ElementID;
                                Anims.Load(ref File, ref Reader);
                                _Item[i] = Anims;
                                break;
                            }

                        case 3:
                            {
                                OGIs OGInfos = new OGIs();
                                OGInfos.Offset = ElementOffset;
                                OGInfos.Base = Offset + Base;
                                OGInfos.Size = ElementSize;
                                OGInfos.ID = ElementID;
                                OGInfos.Load(ref File, ref Reader);
                                _Item[i] = OGInfos;
                                break;
                            }

                        case 4:
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

                        case 5:
                            {
                                SoundDescriptions SDs = new SoundDescriptions();
                                SDs.Offset = ElementOffset;
                                SDs.Base = Offset + Base;
                                SDs.Size = ElementSize;
                                SDs.ID = ElementID;
                                SDs.Load(ref File, ref Reader);
                                _Item[i] = SDs;
                                break;
                            }

                        default:
                            {
                                Sound S = new Sound();
                                if (_Item[i - 1] is SoundDescriptions)
                                {
                                    S.Shift = (_Item[i - 1].Records + 1) * 12;
                                    for (int j = 0; j <= _Item[i - 1].Records - 1; j++)
                                        S.Shift += (int)_Item[i - 1]._Item[j].Size;
                                }
                                else
                                    S.Shift = 0;
                                S.Offset = ElementOffset;
                                S.Base = Offset + Base;
                                S.Size = ElementSize;
                                S.ID = ElementID;
                                S.Load(ref File, ref Reader);
                                _Item[i] = S;
                                break;
                            }
                    }
                    File.Position = Pos;
                }
            }
        }
    }
}
