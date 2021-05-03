using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_DisableFadeout : ModStruct<CSV>
    {
        public override void ModPass(CSV file)
        {
            if (file.FullName.Contains("/hud/") || file.FullName.Contains(@"\hud\"))
            {
                for (int i = 0; i < file.Table.Count; i++)
                {
                    if (file.Table[i][0] == "[FaderStuff]" || file.Table[i][0] == "[FadeStuff]")
                    {
                        file.Table[i] = new List<string>() { "" };
                        file.Table[i + 1] = new List<string>() { "" };
                    }
                }
            }
        }

    }
}
