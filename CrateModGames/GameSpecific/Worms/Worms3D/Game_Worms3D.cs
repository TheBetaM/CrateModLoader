using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Worms3D
{
    public class Game_Worms3D : Game
    {

        public override string Name => "Worms 3D";
        public override string ShortName => "Worms3D";
        public override string API_Credit => "API by BetaM";
        public override List<ConsoleMode> Consoles => new List<ConsoleMode> { ConsoleMode.PS2 }; // gamecube possible, but slow due to many files, todo: xbox
        public override Type ModderClass => typeof(Modder_Worms3D);

        public override Dictionary<ConsoleMode, RegionCode[]> RegionID => new Dictionary<ConsoleMode, RegionCode[]>()
        {
            [ConsoleMode.PS2] = new RegionCode[]
                {
                new RegionCode() {
                    Name = @"SLUS_208.94",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_208.94",
                    CodeName = "SLUS_20894", },
                    new RegionCode() {
                    Name = @"SLES_518.43",
                    Region = RegionType.PAL,
                    ExecName = "SLES_518.43",
                    CodeName = "SLES_51843", },
                },
        };

        public override Dictionary<int, string> PropertyCategories => new Dictionary<int, string>()
        {
            [0] = "Options",
        };
    }
}
