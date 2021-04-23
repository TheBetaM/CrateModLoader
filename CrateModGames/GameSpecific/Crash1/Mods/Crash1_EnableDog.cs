using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1.Mods
{
    public class Crash1_EnableDog : ModStruct<NSF_Pair>
    {
        public override string Name => Crash1_Text.Mod_EnableDog;
        public override string Description => Crash1_Text.Mod_EnableDog;

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC1 != Crash1_Levels.L16_HeavyMachinery)
            {
                return;
            }

            GOOLEntry dog = pair.nsf.GetEntry<GOOLEntry>("Dog_C");
            if (dog != null)
            {
                dog.Instructions[0].Value = 0xBE0 | (0xBE0 << 12) | (0x81 << 24);
                dog.Instructions[1].Value = 0xBE0 | (0xBE0 << 12) | (0x81 << 24);
            }
        }
    }
}
