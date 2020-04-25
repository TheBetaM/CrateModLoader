using System.Collections.Generic;

namespace CrateModLoader
{
    public sealed class Modder_RaymanM : Modder
    {
        public Modder_RaymanM()
        {
            Game = new Game()
            {
                Name = "Rayman M / Arena",
                ShortName = "RaymanM",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX,
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SLUS_202.72",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_202.72",
                    CodeName = "SLUS_20272", },
                    new RegionCode() {
                    Name = @"SLES_504.57",
                    Region = RegionType.PAL,
                    ExecName = "SLES_504.57",
                    CodeName = "SLES_50457", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GRYE",
                    Region = RegionType.NTSC_U },
                },
                RegionID_XBOX = new RegionCode[] {
                    new RegionCode() {
                    Name = "Rayman Arena",
                    Region = RegionType.NTSC_U,
                    RegionNumber = 7, },
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
