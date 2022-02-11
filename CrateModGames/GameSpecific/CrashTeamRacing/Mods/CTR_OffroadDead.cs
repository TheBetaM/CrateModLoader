using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_OffroadDead : ModStruct<CtrScene>
    {
        private Random rand;
        private bool isRandom;

        private List<TerrainFlags> Offroad = new List<TerrainFlags>()
        {
            TerrainFlags.Grass,
            TerrainFlags.Mud,
            TerrainFlags.SlowDirt,
            TerrainFlags.SlowGrass,
            TerrainFlags.Snow,
            TerrainFlags.Water,
        };

        public CTR_OffroadDead()
        {
            isRandom = CTR_Props_Main.Option_OffroadDead.Disabled;
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(CtrScene lev)
        {
            if (!isRandom || rand.Next(2) == 0)
            {
                foreach (QuadBlock quad in lev.quads)
                {
                    if (quad.quadFlags.HasFlag(QuadFlags.Ground) && Offroad.Contains(quad.terrainFlag))
                    {
                        quad.quadFlags |= QuadFlags.KillRacer;
                    }
                }
            }
        }
    }
}
