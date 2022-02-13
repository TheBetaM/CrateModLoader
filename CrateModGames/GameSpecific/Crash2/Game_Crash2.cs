using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModGames.GameSpecific.Crash2;

namespace CrateModLoader.GameSpecific.Crash2
{
    public class Game_Crash2 : Game
    {
        public override string Name => Crash2_Text.GameTitle;
        public override string ShortName => "Crash2";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS1 };
        public override string API_Credit => CrashTri_Text.API_Credit;
        public override string API_Link => "https://github.com/cbhacks/CrashEdit";
        public override Type ModderClass => typeof(Modder_Crash2);
        //public override bool UseLegacyMethod => true;

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS1] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SCUS_941.54",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_941.54",
                    CodeName = "SCUS_94154", },
                    new RegionCode() {
                    Name = @"SCES_009.67",
                    Region = RegionType.PAL,
                    ExecName = "SCES_009.67",
                    CodeName = "SCES_00967", },
                    new RegionCode() {
                    Name = @"SCPS_100.47",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.47",
                    CodeName = "SCPS_10047", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
