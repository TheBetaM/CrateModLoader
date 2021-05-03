using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    //works but sucks
    public class CTR_Rand_SurfaceParams : ModStruct<Scene>
    {
        private List<TerrainFlags> ToReplace = new List<TerrainFlags>()
        {
            TerrainFlags.Asphalt,
            TerrainFlags.Dirt,
            TerrainFlags.Grass,
            TerrainFlags.Wood,
            //TerrainFlags.Water,
            TerrainFlags.Stone,
            TerrainFlags.Ice,
            TerrainFlags.Track,
            TerrainFlags.IcyRoad,
            TerrainFlags.Snow,
            //TerrainFlags.None,
            //TerrainFlags.HardPack,
            TerrainFlags.Metal,
            TerrainFlags.FastWater,
            TerrainFlags.Mud,
            //TerrainFlags.SideSlip,
            TerrainFlags.RiverAsphalt,
            TerrainFlags.SteamAsphalt,
            TerrainFlags.OceanAsphalt,
            TerrainFlags.SlowGrass,
            TerrainFlags.SlowDirt,
        };

        private Random rand;

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(Scene lev)
        {
            if (CTR_Common.GetTrackName(lev.path) != null)
            {
                Dictionary<TerrainFlags, TerrainFlags>  Replacer = new Dictionary<TerrainFlags, TerrainFlags>();

                List<TerrainFlags> Flags = new List<TerrainFlags>(ToReplace);
                int r = 0;
                int i = 0;
                while (Flags.Count > 0)
                {
                    r = rand.Next(Flags.Count);
                    Replacer.Add(ToReplace[i], Flags[r]);
                    Flags.RemoveAt(r);
                    i++;
                }

                foreach (QuadBlock quad in lev.quads)
                {
                    if (quad.quadFlags.HasFlag(QuadFlags.Ground) && ToReplace.Contains(quad.terrainFlag))
                    {
                        quad.terrainFlag = Replacer[quad.terrainFlag];
                    }
                }
            }
        }
    }
}
