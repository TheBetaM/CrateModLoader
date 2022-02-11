using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Game_CrashTitans : Game
    {
        public override string Name => "Crash of the Titans";
        public override string ShortName => "CrashTitans";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                };
        public override string API_Credit => "API by NeoKesha and BetaM";
        public override Type ModderClass => typeof(Modder_Titans);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_215.83",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_215.83",
                    CodeName = "SLUS_21583", },
                    new RegionCode() {
                    Name = @"SLES_548.41",
                    Region = RegionType.PAL,
                    ExecName = "SLES_548.41",
                    CodeName = "SLES_54841", },
                },
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10304",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00917",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.WII] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "RQJE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RQJP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RQJX7D",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.XBOX360] = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Of The Titans",
                        Region = RegionType.Global, }
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
