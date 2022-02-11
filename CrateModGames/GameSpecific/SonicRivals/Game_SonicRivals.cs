using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.SonicRivals
{
    public class Game_SonicRivals : Game
    {

        public override string Name => "Sonic Rivals";
        public override string ShortName => "SonicRivals";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PSP };
        public override Type ModderClass => typeof(Modder_SonicRivals);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10195",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00622",
                    Region = RegionType.PAL },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
