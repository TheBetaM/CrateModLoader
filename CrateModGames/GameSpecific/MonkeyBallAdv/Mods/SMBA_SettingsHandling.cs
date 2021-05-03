using System;
using System.Collections.Generic;
using System.Xml;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_SettingsHandling : ModStruct<XmlDocument>
    {
        public override void ModPass(XmlDocument xml)
        {
            if (xml.HasChildNodes && xml.FirstChild.Name == "settings")
            {
                xml["settings"]["challenge"]["time"].InnerText = SMBA_Props_Settings.Setting_Challenge_Time.Value.ToString();
                xml["settings"]["challenge"]["start_lives"].InnerText = SMBA_Props_Settings.Setting_Challenge_StartLives.Value.ToString();
                xml["settings"]["challenge"]["max_lives"].InnerText = SMBA_Props_Settings.Setting_Challenge_MaxLives.Value.ToString();
                xml["settings"]["challenge"]["continues"].InnerText = SMBA_Props_Settings.Setting_Challenge_Continues.Value.ToString();
                xml["settings"]["challenge"]["extra_life"].InnerText = SMBA_Props_Settings.Setting_Challenge_ExtraLife.Value.ToString();
                xml["settings"]["challenge"]["banana_score"].InnerText = SMBA_Props_Settings.Setting_Challenge_BananaScore.Value.ToString();

                xml["settings"]["rolling"]["power_jump_size"].InnerText = SMBA_Props_Settings.Setting_Rolling_PowerJumpSize.Value.ToString();
                xml["settings"]["rolling"]["squash_threshold"].InnerText = SMBA_Props_Settings.Setting_Rolling_SquashThres.Value.ToString();
                xml["settings"]["rolling"]["squash_recover_distance"].InnerText = SMBA_Props_Settings.Setting_Rolling_SquashRecoverDist.Value.ToString();
            }
        }
    }
}
