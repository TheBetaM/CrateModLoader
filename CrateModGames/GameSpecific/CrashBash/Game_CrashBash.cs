using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBash
{
    public class Game_CrashBash : Game
    {

        public override string Name => "Crash Bash";
        public override string ShortName => "CrashBash";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS1 };
        public override Type ModderClass => typeof(Modder_Bash);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS1] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"SCUS_945.70",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_945.70",
                    CodeName = "SCUS_94570", },
                    new RegionCode() {
                    Name = @"SCES_028.34",
                    Region = RegionType.PAL,
                    ExecName = "SCES_028.34",
                    CodeName = "SCES_02834", },
                    new RegionCode() {
                    Name = @"SCPS_101.40",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.40",
                    CodeName = "SCPS_10140", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
