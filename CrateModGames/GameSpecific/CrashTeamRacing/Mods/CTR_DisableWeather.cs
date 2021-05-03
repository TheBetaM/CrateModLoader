using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_DisableWeather : ModStruct<Scene>
    {
        public override void ModPass(Scene lev)
        {
            foreach (QuadBlock quad in lev.quads)
            {
                if (quad.quadFlags.HasFlag(QuadFlags.Ground))
                {
                    quad.WeatherType = 0;
                    quad.WeatherIntensity = 0;
                }
            }
        }
    }
}
