using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsOW
{
    public class Game_WormsOW : Game
    {

        public override string Name => "Worms: Open Warfare";
        public override string ShortName => "WormsOW";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PSP }; //todo: NDS
        public override Type ModderClass => typeof(Modder_WormsOW);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10065",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00268",
                    Region = RegionType.PAL },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
