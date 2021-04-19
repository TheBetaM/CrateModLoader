using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTTR;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class Game_CrashTTR : Game
    {
        public override string Name => CTTR_Text.GameTitle;
        public override string ShortName => "CrashTTR";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.PSP,
                    ConsoleMode.XBOX,
                };
        public override string API_Credit => CTTR_Text.API_Credit;
        public override Type TextClass => typeof(CTTR_Text);
        public override Type ModderClass => typeof(Modder_CTTR);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_211.91",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_211.91",
                    CodeName = "SLUS_21191", },
                    new RegionCode() {
                    Name = @"SLES_534.39",
                    Region = RegionType.PAL,
                    ExecName = "SLES_534.39",
                    CodeName = "SLES_53439", },
                    new RegionCode() {
                    Name = @"SLPM_660.90",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.90",
                    CodeName = "SLPM_66090", },
                },
            [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "G9RE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "G9RH",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RJ",
                    Region = RegionType.NTSC_J },
                    new RegionCode() {
                    Name = "G9RD",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RF",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RP",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10044",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00168",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "ULJM-05036",
                    Region = RegionType.NTSC_J },
                },
            [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Tag Team Racing",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Tag Team Racing",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
            [1] = "Misc.",
        };
    }
}
