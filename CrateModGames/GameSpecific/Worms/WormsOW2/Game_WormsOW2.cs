using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsOW2
{
    public class Game_WormsOW2 : Game
    {

        public override string Name => "Worms: Open Warfare 2";
        public override string ShortName => "WormsOW2";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PSP }; //todo: NDS
        public override Type ModderClass => typeof(Modder_WormsOW2);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10260",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00819",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.NDS] = new RegionCode[]
            {
                new RegionCode()
                {
                    Name = "AW2E78",
                    Region = RegionType.NTSC_U,
                },
                new RegionCode()
                {
                    Name = "AW2P78",
                    Region = RegionType.PAL,
                },
            },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
