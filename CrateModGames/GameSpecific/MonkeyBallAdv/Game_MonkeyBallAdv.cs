using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Game_MonkeyBallAdv : Game
    {
        public override string Name => "Super Monkey Ball Adventure";
        public override string ShortName => "MonkeyBallAdv";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    //ConsoleMode.PS2,
                    //ConsoleMode.GCN,
                    ConsoleMode.PSP,
                };
        public override string API_Credit => Twins_Text.API_Credit;
        public override string API_Link => "https://github.com/Smartkin/twinsanity-editor";
        public override Type ModderClass => typeof(Modder_MonkeyBallAdv);
        public override bool EnableLevelEditor => true;

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_212.72",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_212.72",
                    CodeName = "SLUS_21272", },
                    new RegionCode() {
                    Name = @"SLES_537.01",
                    Region = RegionType.PAL,
                    ExecName = "SLES_537.01",
                    CodeName = "SLES_53701", },
                },
            [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "G3LE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "G3LP",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10132",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00364",
                    Region = RegionType.PAL },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
