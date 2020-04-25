using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_Persona3 : Modder
    {
        public Modder_Persona3()
        {
            Game = new Game()
            {
                Name = "Persona 3",
                ShortName = "Persona3",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_215.69",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_215.69",
                    CodeName = "SLUS_21569",
                    VersionNumber = 0, },
                    new RegionCode() {
                    Name = @"SLES_550.18",
                    Region = RegionType.PAL,
                    ExecName = "SLES_550.18",
                    CodeName = "SLES_55018",
                    VersionNumber = 0, },
                    new RegionCode() {
                    Name = @"SLPM_664.45",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_664.45",
                    CodeName = "SLPM_66445",
                    VersionNumber = 0, },
                    //FES
                    new RegionCode() {
                    Name = @"SLUS_216.21",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_216.21",
                    CodeName = "SLUS_21621",
                    VersionNumber = 1, },
                    new RegionCode() {
                    Name = @"SLES_553.54",
                    Region = RegionType.PAL,
                    ExecName = "SLES_553.54",
                    CodeName = "SLES_55354",
                    VersionNumber = 1, },
                    new RegionCode() {
                    Name = @"SLPM_666.90",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_666.90",
                    CodeName = "SLPM_66690",
                    VersionNumber = 1, },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10512",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-01523",
                    Region = RegionType.PAL },
                    //todo: NTSC-J
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
