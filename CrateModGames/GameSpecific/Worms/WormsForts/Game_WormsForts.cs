using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Game_WormsForts : Game
    {

        public override string Name => "Worms Forts: Under Siege";
        public override string ShortName => "WormsForts";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS2 }; // todo: xbox
        public override Type ModderClass => typeof(Modder_WormsForts);
        public override bool EnableLevelEditor => true;

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"SLUS_210.93",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_210.93",
                    CodeName = "SLUS_21093", },
                    new RegionCode() {
                    Name = @"SLES_523.42",
                    Region = RegionType.PAL,
                    ExecName = "SLES_523.42",
                    CodeName = "SLES_52342", },
                    new RegionCode() {
                    Name = @"SLES_527.34",
                    Region = RegionType.PAL,
                    ExecName = "SLES_527.34",
                    CodeName = "SLES_52734", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
