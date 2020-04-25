using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_Persona4 : Modder
    {
        public Modder_Persona4()
        {
            Game = new Game()
            {
                Name = "Persona 4",
                ShortName = "Persona4",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_217.82",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_217.82",
                    CodeName = "SLUS_21782", },
                    new RegionCode() {
                    Name = @"SLES_554.74",
                    Region = RegionType.PAL,
                    ExecName = "SLES_554.74",
                    CodeName = "SLES_55474", },
                    new RegionCode() {
                    Name = @"SLPM_669.78",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_669.78",
                    CodeName = "SLPM_66978", },
                     new RegionCode() {
                    Name = @"SLPM_742.78",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_742.78",
                    CodeName = "SLPM_74278", },
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
