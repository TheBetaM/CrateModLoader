using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_CrateParams : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34)
                    {
                        //todo
                        //ent.VecY |= 0x1;
                    }
                }
            }
            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type != null && ent.Type == 34)
                    {
                        if (ent.Settings.Count > 1)
                        {
                            byte SetA = ent.Settings[1].ValueA;
                            int SetB = ent.Settings[1].ValueB;

                            int cratePreset = rand.Next(6);

                            switch (cratePreset)
                            {
                                default:
                                    break;
                                case 0:
                                    SetB |= 1 << (int)Crash2.CrateParamFlagsB.Wave;
                                    break;
                                case 1:
                                    SetB |= 1 << (int)Crash2.CrateParamFlagsB.Space;
                                    break;
                                case 2:
                                    SetB |= 1 << (int)Crash2.CrateParamFlagsB.NitroNoHop;
                                    break;
                            }

                            ent.Settings[1] = new EntitySetting(SetA, SetB);
                        }
                    }
                }
            }
            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type != null && ent.Type == 34)
                    {
                        if (ent.Settings.Count > 1 && ent.Subtype != (int)Crash3.CrateSubTypes.Slot && ent.Subtype != (int)Crash3.CrateSubTypes.Clock)
                        {
                            byte SetA = ent.Settings[1].ValueA;
                            int SetB = ent.Settings[1].ValueB;

                            int cratePreset = rand.Next(6);

                            switch (cratePreset)
                            {
                                default:
                                    break;
                                case 0:
                                    SetB |= 1 << (int)Crash3.CrateParamFlagsB.Wave;
                                    break;
                                case 1:
                                    SetB |= 1 << (int)Crash3.CrateParamFlagsB.Space;
                                    break;
                                case 2:
                                    SetB |= 1 << (int)Crash3.CrateParamFlagsB.NitroNoHop;
                                    break;
                            }

                            ent.Settings[1] = new EntitySetting(SetA, SetB);
                        }
                    }
                }
            }
        }
    }
}
