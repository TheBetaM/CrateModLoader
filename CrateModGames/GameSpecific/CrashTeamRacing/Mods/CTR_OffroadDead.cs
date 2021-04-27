using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_OffroadDead : ModStruct<Scene>
    {
        public override string Name => "Driving Offroad Respawns The Driver";

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

        public CTR_OffroadDead(bool isrand)
        {
            isRandom = isrand;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(Scene lev)
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
