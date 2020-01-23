using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_Bash : Modder
    {
        public Modder_Bash()
        {
            Game = new Game()
            {
                Name = "Crash Bash",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = "No API available",
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_945.70;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_945.70",
                    CodeName = "SCUS_94570", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_028.34;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_028.34",
                    CodeName = "SCES_02834", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_101.40;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.40",
                    CodeName = "SCPS_10140", },
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
