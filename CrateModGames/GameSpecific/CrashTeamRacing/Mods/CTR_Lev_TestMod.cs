using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Lev_TestMod : ModStruct<Scene>
    {
        public override string Name => "LEV Test";
        //public override bool NeedsCachePass => true;

        private List<byte> Weathers = new List<byte>();
        private List<byte> Intens = new List<byte>();
        private bool printer = false;

        public override void CachePass(Scene lev)
        {
            foreach (QuadBlock quad in lev.quads)
            {

                if (!Weathers.Contains(quad.WeatherType))
                {
                    Weathers.Add(quad.WeatherType);
                    Intens.Add(quad.WeatherIntensity);
                }
            }
        }

        public override void ModPass(Scene lev)
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
            /*
            foreach (PickupHeader pick in lev.pickups)
            {
                /*
                if (pick.Event == CTREvent.WarpPad)
                {
                    if (pick.Name == "warppad#9")
                    {
                        pick.Name = "warppad#3";
                    }
                }
                
                if (pick.Event == CTREvent.CrateWeapon || pick.Event == CTREvent.CrateFruit || pick.Event == CTREvent.CrateRelic1 || pick.Event == CTREvent.CrateRelic2 || pick.Event == CTREvent.CrateRelic3)
                {
                    double scale = rand.NextDouble() + 0.5d;
                    pick.Scale = new Vector4s((short)(pick.Scale.X * scale), (short)(pick.Scale.Y * scale), (short)(pick.Scale.Z * scale), 0);
                }
            }
            */
            /*
            foreach (Vertex v in lev.verts)
            {
                v.SetColor(Vcolor.Default, new Vector4b(Color.Green));
                v.SetColor(Vcolor.Morph, new Vector4b(Color.Green));
            }
            */
            /*
            if (!printer)
            {
                for (int i = 0; i < Weathers.Count; i++)
                {
                    Console.WriteLine("Weather: " + Weathers[i] + " - Intens: " + Intens[i]);
                }
                printer = true;
            }
            */
            /*
            foreach (QuadBlock quad in lev.quads)
            {
                
                if (quad.quadFlags.HasFlag(QuadFlags.Ground))
                {
                    quad.terrainFlag = TerrainFlags.Ice;
                }

                //quad.quadFlags = QuadFlags.Ground | QuadFlags.Invisible;
                //quad.terrainFlag = TerrainFlags.Asphalt | TerrainFlags.Dirt;
                //quad.TerrainFlagUnknown = 0;
            }
            */
        }
    }
}
