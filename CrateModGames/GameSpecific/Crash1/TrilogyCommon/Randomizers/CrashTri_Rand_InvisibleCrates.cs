using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_InvisibleCrates : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;

        public CrashTri_Rand_InvisibleCrates()
        {
            isRandom = Crash1_Props_Main.Option_RandInvisibleCrates.Enabled || Crash2_Props_Main.Option_RandInvisibleCrates.Enabled || Crash3_Props_Main.Option_RandInvisibleCrates.Enabled;
        }

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
                        if (!isRandom || (isRandom && rand.Next(2) == 0))
                        {
                            ent.VecY |= 0x1;
                        }
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
                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                            {
                                byte SetA = ent.Settings[1].ValueA;
                                int SetB = ent.Settings[1].ValueB;

                                SetA |= 1 << (int)Crash2_CrateParamFlagsA.Invisible;

                                ent.Settings[1] = new EntitySetting(SetA, SetB);
                            }
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
                        if (ent.Settings.Count > 1)
                        {
                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                            {
                                byte SetA = ent.Settings[1].ValueA;
                                int SetB = ent.Settings[1].ValueB;

                                SetA |= 1 << (int)Crash3_CrateParamFlagsA.Invisible;

                                ent.Settings[1] = new EntitySetting(SetA, SetB);
                            }
                        }
                    }
                }
            }
        }
    }
}
