using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class Game_Crash3 : Game
    {
        public override string Name => Crash3_Text.GameTitle;
        public override string ShortName => "Crash3";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS1 };
        public override string API_Credit => CrashTri_Text.API_Credit;
        public override string API_Link => "https://github.com/cbhacks/CrashEdit";
        public override Type ModderClass => typeof(Modder_Crash3);
        //public override bool UseLegacyMethod => true;
        public override bool EnableLevelEditor => true;

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS1] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SCUS_942.44",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_942.44",
                    CodeName = "SCUS_94244", },
                    new RegionCode() {
                    Name = @"SCES_014.20",
                    Region = RegionType.PAL,
                    ExecName = "SCES_014.20",
                    CodeName = "SCES_01420", },
                    new RegionCode() {
                    Name = @"SCPS_100.73",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.73",
                    CodeName = "SCPS_10073", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
