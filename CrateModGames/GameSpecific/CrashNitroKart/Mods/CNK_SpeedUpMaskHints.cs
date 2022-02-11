using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_SpeedUpMaskHints : ModStruct<CSV>
    {
        public override void ModPass(CSV file)
        {
            if (file.FullName.Contains("/hints/config.csv") || file.FullName.Contains("/hints/config.csv"))
            {
                file.Table[5][0] = "500";
                file.Table[6][0] = "250";
                file.Table[7][0] = "0.12";
                file.Table[9][0] = "500";
                file.Table[10][0] = "250";
            }
        }

    }
}
