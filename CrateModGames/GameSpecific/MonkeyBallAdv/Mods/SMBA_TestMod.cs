using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_TestMod : ModStruct<XmlDocument>
    {

        private uint Rolling_PowerJumpSize = 50;
        private uint Rolling_SquashThreshold = 70;
        private uint Rolling_SquashRecoverDistance = 30;

        public override void ModPass(XmlDocument xml)
        {
            if (xml.HasChildNodes && xml.FirstChild.Name == "settings")
            {
                xml["settings"]["rolling"]["power_jump_size"].InnerText = Rolling_PowerJumpSize.ToString();
                xml["settings"]["rolling"]["squash_threshold"].InnerText = Rolling_SquashThreshold.ToString();
                xml["settings"]["rolling"]["squash_recover_distance"].InnerText = Rolling_SquashRecoverDistance.ToString();
            }
            if (xml.HasChildNodes && xml.FirstChild.Name == "challengemode")
            {
                Random rand = new Random(ModLoaderGlobals.RandomizerSeed);
                int Count_Beginner = xml["challengemode"]["beginner"].ChildNodes.Count;
                int Count_Advanced = xml["challengemode"]["advanced"].ChildNodes.Count;
                int Count_Expert = xml["challengemode"]["expert"].ChildNodes.Count;
                int Count_PSP = xml["challengemode"]["psp"].ChildNodes.Count;

                xml["challengemode"]["beginner"].ChildNodes[0].InnerText = @"levels\partygames\bounce";

            }
        }
    }
}
