using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashMoM
{
    public class Game_CrashMoM : Game
    {
        public override string Name => "Crash Mind Over Mutant";
        public override string ShortName => "CrashMoM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                };
        public override string API_Credit => "API by NeoKesha and BetaM";
        public override Type ModderClass => typeof(Modder_MoM);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_217.28",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_217.28",
                    CodeName = "SLUS_21728", },
                    new RegionCode() {
                    Name = @"SLES_552.04",
                    Region = RegionType.PAL,
                    ExecName = "SLES_552.04",
                    CodeName = "SLES_55204", },
                },
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10377",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-01171",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.WII] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "RC8E7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RC8P7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RC8X7D",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.XBOX360] = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Mind Over Mutant",
                        Region = RegionType.Global, }
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
