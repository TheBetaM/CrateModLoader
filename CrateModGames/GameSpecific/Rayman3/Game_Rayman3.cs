using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Rayman3;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class Game_Rayman3 : Game
    {
        public override string Name => Rayman3_Text.GameTitle;
        public override string ShortName => "Rayman3";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode>
                {
                    ConsoleMode.GCN,
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                    //ConsoleMode.PC,
                };
        public override Type ModderClass => typeof(Modder_Rayman3);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "SLUS_206.01",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_206.01",
                    CodeName = "SLUS_20601", },
                    new RegionCode() {
                    Name = "SLES_512.22",
                    Region = RegionType.PAL,
                    ExecName = "SLES_512.22",
                    CodeName = "SLES_51222", },
                },
            [ConsoleMode.GCN] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "GRHE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GRHP",
                    Region = RegionType.PAL },
                },
            [ConsoleMode.XBOX] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 3, },
                    new RegionCode() {
                    Name = "Rayman 3",
                    Region = RegionType.PAL,
                    RegionNumber = 4, },
                },
            [ConsoleMode.PC] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "Rayman3.exe",
                    Region = RegionType.Global },
                    new RegionCode() {
                    Name = "rayman3.exe",
                    Region = RegionType.Global },
                    new RegionCode() {
                    Name = "RAYMAN3.EXE",
                    Region = RegionType.Global },
                    new RegionCode() {
                    Name = "Rayman3.EXE",
                    Region = RegionType.Global },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
            [(int)R3_ModProps.Textures_Loading] = "Textures - Loading",
            [(int)R3_ModProps.Textures_Menu] = "Textures - Menu",
            [(int)R3_ModProps.Textures_General] = "Textures - General",
        };
    }
}
