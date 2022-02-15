using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class Game_CrashTWOC : Game
    {
        public override string Name => "Crash Bandicoot: The Wrath of Cortex";
        public override string ShortName => "CrashTWOC";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    //ConsoleMode.GCN, // rebuilding the GC version makes it not boot for some reason...
                    //ConsoleMode.XBOX,
                };
        public override Type ModderClass => typeof(Modder_TWOC);
        public override bool EnableLevelEditor => true;

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_202.38",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_202.38",
                    CodeName = "SLUS_20238", },
                    new RegionCode() {
                    Name = @"SLES_503.86",
                    Region = RegionType.PAL,
                    ExecName = "SLES_503.86",
                    CodeName = "SLES_50386", },
                    new RegionCode() {
                    Name = @"SLPM_740.03",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_740.03",
                    CodeName = "SLPM_74003", },
                },
            [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "GCBE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCBP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GCBJA4",
                    Region = RegionType.NTSC_J },
                },
            [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.NTSC_U,
                        RegionNumber = 1, },
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.NTSC_U,
                        RegionNumber = 7, },
                    new RegionCode() {
                        Name = "Crash Bandicoot: tWoC",
                        Region = RegionType.PAL,
                        RegionNumber = 4, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
