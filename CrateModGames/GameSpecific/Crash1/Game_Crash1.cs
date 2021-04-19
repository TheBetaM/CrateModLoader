using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.Crash1
{
    public class Game_Crash1 : Game
    {
        public override string Name => Crash1_Text.GameTitle;
        public override string ShortName => "Crash1";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS1 };
        public override string API_Credit => CrashTri_Text.API_Credit;
        public override string API_Link => "https://github.com/cbhacks/CrashEdit";
        public override Type ModderClass => typeof(Modder_Crash1);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS1] = new RegionCode[]
                {
                new RegionCode()
                {
                    Name = @"SCUS_949.00",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_949.00",
                    CodeName = "SCUS_94900", },
                new RegionCode()
                {
                    Name = @"SCES_003.44",
                    Region = RegionType.PAL,
                    ExecName = "SCES_003.44",
                    CodeName = "SCES_00344", },
                new RegionCode()
                {
                    Name = @"SCPS_100.31",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.31",
                    CodeName = "SCPS_10031", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
