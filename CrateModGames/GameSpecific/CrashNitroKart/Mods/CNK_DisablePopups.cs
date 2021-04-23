using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_DisablePopups : ModStruct<string>
    {
        public override string Name => CNK_Text.Mod_DisableUnlockPopups;
        public override string Description => CNK_Text.Mod_DisableUnlockPopupsDesc;

        public override void ModPass(string path_gob_extracted)
        {
            string[] csv_Unlockables = File.ReadAllLines(path_gob_extracted + "common/gameprogression/unlockables.csv");

            string[] cur_line_split;
            for (int i = 0; i < csv_Unlockables.Length; i++)
            {
                cur_line_split = csv_Unlockables[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (cur_line_split.Length == 3 && cur_line_split[2] == "1")
                {
                    cur_line_split[2] = "0";
                    csv_Unlockables[i] = cur_line_split[0] + "," + cur_line_split[1] + "," + cur_line_split[2];
                }
            }

            File.WriteAllLines(path_gob_extracted + "common/gameprogression/unlockables.csv", csv_Unlockables);
        }

    }
}
