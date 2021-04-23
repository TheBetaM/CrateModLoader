using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_SpeedUpMaskHints : ModStruct<string>
    {
        public override string Name => CNK_Text.Mod_SpeedUpMaskHint;
        public override string Description => CNK_Text.Mod_SpeedUpMaskHintDesc;

        public override void ModPass(string path_gob_extracted)
        {
            string[] csv_HintsConfig = File.ReadAllLines(path_gob_extracted + "common/hints/config.csv");

            csv_HintsConfig[5] = "500,# Boom duration";
            csv_HintsConfig[6] = "250,# Rotation duration";
            csv_HintsConfig[7] = "0.12,# Rotation speed";
            csv_HintsConfig[9] = "500,# Wait for end of smoke delay";
            csv_HintsConfig[10] = "250,# Camera move time";

            File.WriteAllLines(path_gob_extracted + "common/hints/config.csv", csv_HintsConfig);
        }

    }
}
