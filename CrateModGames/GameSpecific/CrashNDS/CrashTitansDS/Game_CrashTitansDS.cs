using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTitansDS
{
    public class Game_CrashTitansDS : Game
    {

        public override string Name => "Crash of the Titans";
        public override string ShortName => "CrashTitansDS";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.NDS };
        public override Type ModderClass => typeof(Generic.Modder_Generic);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.NDS] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"AQJE7D",
                    Region = RegionType.NTSC_U, },
                    new RegionCode() {
                    Name = @"AQJP7D",
                    Region = RegionType.PAL, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
