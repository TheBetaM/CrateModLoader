using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBash
{
    public sealed class Modder_Bash : Modder
    {
        public Modder_Bash()
        {
            Game = new Game()
            {
                Name = "Crash Bash",
                ShortName = "CrashBash",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = Properties.Resources.icon_crashbash,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SCUS_945.70",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_945.70",
                    CodeName = "SCUS_94570", },
                    new RegionCode() {
                    Name = @"SCES_028.34",
                    Region = RegionType.PAL,
                    ExecName = "SCES_028.34",
                    CodeName = "SCES_02834", },
                    new RegionCode() {
                    Name = @"SCPS_101.40",
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
