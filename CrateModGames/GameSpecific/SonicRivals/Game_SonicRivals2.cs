using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Sonic2Rivals
{
    public class Game_SonicRivals2 : Game
    {

        public override string Name => "Sonic Rivals 2";
        public override string ShortName => "SonicRivals2";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PSP };
        public override Type ModderClass => typeof(Modder_SonicRivals2);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10323",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00940",
                    Region = RegionType.PAL },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
