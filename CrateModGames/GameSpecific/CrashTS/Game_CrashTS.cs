using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Game_CrashTS : Game
    {
        public override string Name => Twins_Text.GameTitle;
        public override string ShortName => "CrashTS";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                };
        public override string API_Credit => Twins_Text.API_Credit;
        public override string API_Link => "https://github.com/Smartkin/twinsanity-editor";
        public override Type TextClass => typeof(Twins_Text);
        public override Type ModderClass => typeof(Modder_Twins);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_209.09",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"SLES_525.68",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"SLPM_658.01",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                },
            [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7,
                    ExecName = "default.xbe" },
                    new RegionCode() {
                    Name = "Crash Twinsanity",
                    Region = RegionType.PAL,
                    RegionNumber = 4,
                    ExecName = "default.xbe" },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [(int)ModProps.Options] = "Options",
            [(int)ModProps.Misc] = "Misc.",
            [(int)ModProps.Character] = "Character",
            [(int)ModProps.Textures] = "Textures",
            [(int)ModProps.Galleries] = "Galleries",
        };
    }
}
