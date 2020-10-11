using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBash
{
    public sealed class Modder_Bash : Modder
    {
        public override Game Game => new Game()
        {
            Name = "Crash Bash",
            ShortName = "CrashBash",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
            API_Credit = string.Empty,
            API_Link = string.Empty,
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS1] = new RegionCode[]
                {
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
            },
        };

        public Modder_Bash()
        {
            
        }

        public override void StartModProcess()
        {
            // TODO
        }

    }
}
