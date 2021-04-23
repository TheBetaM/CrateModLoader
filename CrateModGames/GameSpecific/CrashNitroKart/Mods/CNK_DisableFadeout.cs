using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_DisableFadeout : ModStruct<string>
    {
        public override string Name => CNK_Text.Mod_DisableFadeout;
        public override string Description => CNK_Text.Mod_DisableFadeoutDesc;

        public override void ModPass(string path_gob_extracted)
        {
            DirectoryInfo dir_hud = new DirectoryInfo(path_gob_extracted + "common/hud/");
            foreach (FileInfo file in dir_hud.EnumerateFiles())
            {
                string[] csv_HUD_Config = File.ReadAllLines(file.FullName);

                if (csv_HUD_Config[0] == "[FaderStuff],Fader")
                {
                    csv_HUD_Config[0] = "";
                }
                if (csv_HUD_Config[1] == "player,0")
                {
                    csv_HUD_Config[1] = "";
                }
                if (csv_HUD_Config[1] == "player,0 0")
                {
                    csv_HUD_Config[1] = "";
                }
                if (csv_HUD_Config[0] == "[FadeStuff],Fader")
                {
                    csv_HUD_Config[0] = "";
                }

                File.WriteAllLines(file.FullName, csv_HUD_Config);
            }

            if (Directory.Exists(path_gob_extracted + "hud/"))
            {
                dir_hud = new DirectoryInfo(path_gob_extracted + "hud/");
                foreach (FileInfo file in dir_hud.EnumerateFiles())
                {
                    string[] csv_HUD_Config = File.ReadAllLines(file.FullName);

                    if (csv_HUD_Config[0] == "[FaderStuff],Fader")
                    {
                        csv_HUD_Config[0] = "";
                    }
                    if (csv_HUD_Config[1] == "player,0")
                    {
                        csv_HUD_Config[1] = "";
                    }
                    if (csv_HUD_Config[1] == "player,0 0")
                    {
                        csv_HUD_Config[1] = "";
                    }
                    if (csv_HUD_Config[0] == "[FadeStuff],Fader")
                    {
                        csv_HUD_Config[0] = "";
                    }

                    File.WriteAllLines(file.FullName, csv_HUD_Config);
                }
            }
        }

    }
}
