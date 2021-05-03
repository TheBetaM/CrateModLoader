using System;
using System.Xml;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_EnableJump : ModStruct<XmlDocument>
    {
        public override void ModPass(XmlDocument xml)
        {
            if (xml.HasChildNodes && xml.FirstChild.Name == "settings")
            {
                xml["settings"]["rolling"]["default_jump_enabled"].InnerText = 1.ToString();
            }
        }
    }
}
