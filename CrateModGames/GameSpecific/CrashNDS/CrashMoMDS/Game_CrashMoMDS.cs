using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public class Game_CrashMoMDS : Game
    {

        public override string Name => "Crash Mind Over Mutant";
        public override string ShortName => "CrashMoMDS";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.NDS };
        public override Type ModderClass => typeof(Modder_CrashMoMDS);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.NDS] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"CR8E7D",
                    Region = RegionType.NTSC_U, },
                    new RegionCode() {
                    Name = @"CR8P7D",
                    Region = RegionType.PAL, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
