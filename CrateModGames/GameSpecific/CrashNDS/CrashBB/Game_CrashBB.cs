using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBB
{
    public class Game_CrashBB : Game
    {

        public override string Name => "Crash Boom Bang!";
        public override string ShortName => "CrashBB";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.NDS };
        public override Type ModderClass => typeof(Generic.Modder_Generic);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.NDS] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"ACFE7D",
                    Region = RegionType.NTSC_U, },
                    new RegionCode() {
                    Name = @"ACFP7D",
                    Region = RegionType.PAL, },
                    new RegionCode() {
                    Name = @"ACFJ7D",
                    Region = RegionType.NTSC_J, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
