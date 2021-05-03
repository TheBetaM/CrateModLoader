using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_DisablePopups : ModStruct<CSV>
    {
        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() == "unlockables.csv")
            {
                for (int i = 0; i < file.Table.Count; i++)
                {
                    if (file.Table[i].Count == 3 && file.Table[i][2] == "1")
                    {
                        file.Table[i][2] = "0";
                    }
                }
            }
        }

    }
}
