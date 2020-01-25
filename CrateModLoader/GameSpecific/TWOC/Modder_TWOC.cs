using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_TWOC : Modder
    {
        public Modder_TWOC()
        {
            Game = new Game()
            {
                Name = "Crash Bandicoot: The Wrath of Cortex",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX,
                },
                API_Credit = "No API Available",
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_202.38;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_202.38",
                    CodeName = "SLUS_20238", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_503.86;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_503.86",
                    CodeName = "SLES_50386", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_740.03;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_740.03",
                    CodeName = "SLPM_74003", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GCBE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCBP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GCBJA4",
                    Region = RegionType.NTSC_J },
                },
            };

            Options.Add(0, new ModOption("No Options Available"));
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
