using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class Game_CrashNK : Game
    {
        public override string Name => CNK_Text.GameTitle;
        public override string ShortName => "CrashNK";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX
                };
        public override string API_Credit => CNK_Text.API_Credit;
        public override Type TextClass => typeof(CNK_Text);
        public override Type ModderClass => typeof(Modder_CNK);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_206.49",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_206.49",
                    CodeName = "SLUS_20649", },
                    new RegionCode() {
                    Name = @"SLES_515.11",
                    Region = RegionType.PAL,
                    ExecName = "SLES_515.11",
                    CodeName = "SLES_51511", },
                    new RegionCode() {
                    Name = @"SLPM_660.67",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.67",
                    CodeName = "SLPM_66067", },
                },
            [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "GCNE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCNP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GC8JA4",
                    Region = RegionType.NTSC_J },
                },
            [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
                    new RegionCode() {
                    Name = "Crash Nitro Kart",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
            [(int)ModProps.KartStats] = CNK_Text.PropCategory_KartStats,
            [(int)ModProps.DriverStats] = CNK_Text.PropCategory_DriverStats,
            [(int)ModProps.Surfaces] = CNK_Text.PropCategory_Surfaces,
            [(int)ModProps.Powerups] = CNK_Text.PropCategory_Powerups,
            [(int)ModProps.Adventure] = CNK_Text.PropCategory_Adventure,
            [(int)ModProps.Textures] = "Textures",
        };

    }
}
