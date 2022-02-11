using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Game_CrashTR : Game
    {
        public override string Name => "CTR: Crash Team Racing";
        public override string ShortName => "CrashTR";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS1 };
        public override string API_Credit => "API by DCxDemo";
        public override string API_Link => "https://github.com/DCxDemo/CTR-tools";
        public override Type ModderClass => typeof(Modder_CTR);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS1] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SCUS_944.26",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_944.26",
                    CodeName = "SCUS_94426", },
                    new RegionCode() {
                    Name = @"SCES_021.05",
                    Region = RegionType.PAL,
                    ExecName = "SCES_021.05",
                    CodeName = "SCES_02105", },
                    new RegionCode() {
                    Name = @"SCPS_101.18",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.18",
                    CodeName = "SCPS_10118", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
