using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    public class CTR_DisableWeather : ModStruct<CtrScene>
    {
        public override void ModPass(CtrScene lev)
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
