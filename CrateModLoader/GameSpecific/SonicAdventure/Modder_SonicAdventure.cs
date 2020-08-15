using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.SonicAdventure
{
    public sealed class Modder_SonicAdventure : Modder
    {
        public Modder_SonicAdventure()
        {
            Game = new Game()
            {
                Name = "Sonic Adventure",
                ShortName = "SonicAdv",
                Consoles = new List<ConsoleMode>
                {
                    //ConsoleMode.DC,
                    ConsoleMode.GCN,
                },
                API_Credit = string.Empty,
                API_Link = string.Empty,
                Icon = null,
                ModCratesSupported = true,
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GXSE",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GXSP",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GASJ",
                    Region = RegionType.NTSC_J },
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
