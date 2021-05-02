using System;
using System.Xml;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_EnablePowerJump : ModStruct<XmlDocument>
    {
        public override string Name => "Story Mode: Enable Power Jump By Default";
        public override string Description => "Enables Power Jump from the start by holding the Square button.";

        public override void ModPass(XmlDocument xml)
        {
            if (xml.HasChildNodes && xml.FirstChild.Name == "settings")
            {
                xml["settings"]["rolling"]["default_power_jump_enabled"].InnerText = 1.ToString();
            }
        }
    }
}
