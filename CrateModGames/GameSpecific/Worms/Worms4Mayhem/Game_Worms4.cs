using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Worms4
{
    public class Game_Worms4 : Game
    {

        public override string Name => "Worms 4: Mayhem";
        public override string ShortName => "Worms4Mayhem";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS2 }; // todo: xbox
        public override Type ModderClass => typeof(Modder_Worms4);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"SLED_530.97",
                    Region = RegionType.PAL,
                    ExecName = "SLED_530.97",
                    CodeName = "SLED_53097", },
                    new RegionCode() {
                    Name = @"SLES_530.96",
                    Region = RegionType.PAL,
                    ExecName = "SLES_530.96",
                    CodeName = "SLES_53096", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
