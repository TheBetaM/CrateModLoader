using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_Rayman2 : Modder
    {
        public Modder_Rayman2()
        {
            Game = new Game()
            {
                Name = "Rayman 2",
                ShortName = "Rayman2",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1,
                    ConsoleMode.PS2,
                    //ConsoleMode.DC,
                    //ConsoleMode.NDS,
                    //ConsoleMode.N3DS,
                    //ConsoleMode.N64,
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = null,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_012.35",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_012.35",
                    CodeName = "SLUS_012.35", },
                    new RegionCode() {
                    Name = @"SLES_029.05",
                    Region = RegionType.PAL,
                    ExecName = "SLES_029.05",
                    CodeName = "SLES_029.05", },
                    new RegionCode() {
                    Name = @"SLES_029.06",
                    Region = RegionType.PAL,
                    ExecName = "SLES_029.06",
                    CodeName = "SLES_029.06", },
                },
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_201.38",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_201.38",
                    CodeName = "SLUS_20138", },
                    new RegionCode() {
                    Name = @"SLES_500.44",
                    Region = RegionType.PAL,
                    ExecName = "SLES_500.44",
                    CodeName = "SLES_500.44", },
                    new RegionCode() {
                    Name = @"SLPS_250.29",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPS_250.29",
                    CodeName = "SLPS_25029", },
                },
            };
        }

        public override void StartModProcess()
        {
            // TODO
            ModProcess();
        }

        protected override void ModProcess()
        {
            // TODO
            EndModProcess();
        }

        protected override void EndModProcess()
        {
            // TODO
        }
    }
}
