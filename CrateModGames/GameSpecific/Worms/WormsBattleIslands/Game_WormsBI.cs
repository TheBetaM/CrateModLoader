using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsBI
{
    public class Game_WormsBI : Game
    {

        public override string Name => "Worms: Battle Islands";
        public override string ShortName => "WormsBI";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PSP }; //todo: wii
        public override Type ModderClass => typeof(Modder_WormsBI);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "NPUH-10045",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "NPEH-00019",
                    Region = RegionType.PAL },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
