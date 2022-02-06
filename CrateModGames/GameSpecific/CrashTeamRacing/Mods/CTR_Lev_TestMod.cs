using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_Lev_TestMod : ModStruct<CtrScene>
    {
        //public override bool NeedsCachePass => true;

        private List<byte> Weathers = new List<byte>();
        private List<byte> Intens = new List<byte>();
        private bool printer = false;

        public override void CachePass(CtrScene lev)
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

        public override void ModPass(CtrScene lev)
        {
            Random rand = GetRandom();

            foreach (PickupHeader pick in lev.pickups)
            {
                
                //if (pick.Event == CTREvent.WarpPad && pick.Name == "warppad#9")
                //{       pick.Name = "warppad#3"; }
                
                if (pick.ThreadID == CtrThreadID.CrateWeapon)
                {
                    pick.unk1 = 0xF;
                }
            }
            
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
