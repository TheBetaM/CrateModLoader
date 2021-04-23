using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    public class Crash1_Rand_LightColor : ModStruct<NSF_Pair>
    {
        public override string Name => Crash1_Text.Rand_LightCol;
        public override string Description => Crash1_Text.Rand_LightColDesc;

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                // TODO : make these values adjustable
                short r1 = (short)rand.Next(0x6000);
                short g1 = (short)rand.Next(0x6000);
                short b1 = (short)rand.Next(0x6000);
                short r2 = (short)rand.Next(0x200);
                short g2 = (short)rand.Next(0x200);
                short b2 = (short)rand.Next(0x200);
                BitConv.ToInt16(zone.Header, 0x32A, r1);
                BitConv.ToInt16(zone.Header, 0x32C, g1);
                BitConv.ToInt16(zone.Header, 0x32E, b1);
                BitConv.ToInt16(zone.Header, 0x35A, r1);
                BitConv.ToInt16(zone.Header, 0x35C, g1);
                BitConv.ToInt16(zone.Header, 0x35E, b1);
                BitConv.ToInt16(zone.Header, 0x342, r2);
                BitConv.ToInt16(zone.Header, 0x344, g2);
                BitConv.ToInt16(zone.Header, 0x346, b2);
                BitConv.ToInt16(zone.Header, 0x372, r2);
                BitConv.ToInt16(zone.Header, 0x374, g2);
                BitConv.ToInt16(zone.Header, 0x376, b2);
            }
        }
    }
}
