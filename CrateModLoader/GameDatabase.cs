using System;
using System.Collections.Generic;
using System.Reflection;

namespace CrateModLoader
{
    static class GameDatabase
    {
        /*
         * Adding a game:
         * 1. Add it as a Game object here.
         * 2. Create a class for it with functions named: StartModProcess, OptionChanged(int option, bool value), OpenModMenu, UpdateModOptions
         * 3. Done.
         * 
         */

        public enum GameType
        {
            Undefined = -1,
            CNK = 0,
            CTTR = 1,
            Titans = 2,
            MoM = 3,
            Twins = 4,
            TWOC = 5,
            Crash1 = 6,
            Crash2 = 7,
            Crash3 = 8,
            CTR = 9,
            Bash = 10
        }

        public static Game[] Games = new Game[]
        {
            new Game()
            {
                Name = "Crash Nitro Kart",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX
                },
                API_Credit = "Tools/API by BetaM, ManDude and eezstreet",
                Icon = Properties.Resources.icon_cnk,
                ModderClass = typeof(Modder_CNK),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_206.49;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_206.49",
                    CodeName = "SLUS_20649", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_515.11;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_515.11",
                    CodeName = "SLES_51511", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_660.67;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.67",
                    CodeName = "SLPM_66067", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "GCNE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "GCNP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "GC8JA4",
                    Region = RegionType.NTSC_J },
                }
            },
            new Game()
            {
                Name = "Crash Tag Team Racing",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.PSP,
                    ConsoleMode.XBOX,
                },
                API_Credit = "APIs by NeoKesha and BetaM",
                Icon = Properties.Resources.icon_crash,
                ModderClass = typeof(Modder_CTTR),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_211.91;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_211.91",
                    CodeName = "SLUS_21191", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_534.39;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_534.39",
                    CodeName = "SLES_53439", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_660.90;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_660.90",
                    CodeName = "SLPM_66090", },
                },
                RegionID_GCN = new RegionCode[] {
                    new RegionCode() {
                    Name = "G9RE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "G9RH7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RJ7D",
                    Region = RegionType.NTSC_J },
                    new RegionCode() {
                    Name = "G9RD7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RF7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "G9RP7D",
                    Region = RegionType.PAL },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10044",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULJM-05036",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "ULES-00168",
                    Region = RegionType.NTSC_J },
                }
            },
            new Game()
            {
                Name = "Crash of the Titans",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                },
                API_Credit = "API by NeoKesha",
                Icon = Properties.Resources.icon_titans,
                ModderClass = typeof(Modder_Titans),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_215.83;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_215.83",
                    CodeName = "SLUS_21583", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_548.41;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_548.41",
                    CodeName = "SLES_54841", },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10304",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00917",
                    Region = RegionType.PAL },
                },
                RegionID_WII = new RegionCode[] {
                    new RegionCode() {
                    Name = "RQJE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RQJP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RQJX7D",
                    Region = RegionType.PAL },
                }
            },
            new Game()
            {
                Name = "Crash Mind Over Mutant",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                },
                API_Credit = "API by NeoKesha",
                Icon = Properties.Resources.icon_titans,
                ModderClass = typeof(Modder_MoM),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_217.28;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_217.28",
                    CodeName = "SLUS_21728", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_552.04;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_552.04",
                    CodeName = "SLES_55204", },
                },
                RegionID_PSP = new RegionCode[] {
                    new RegionCode() {
                    Name = "ULUS-10377",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-01171",
                    Region = RegionType.PAL },
                },
                RegionID_WII = new RegionCode[] {
                    new RegionCode() {
                    Name = "RC8E7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RC8P7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RC8X7D",
                    Region = RegionType.PAL },
                }
            },
            new Game()
            {
                Name = "Crash Twinsanity",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.XBOX,
                },
                API_Credit = "API by NeoKesha, Smartkin, ManDude and Marko",
                Icon = Properties.Resources.icon_twins,
                ModderClass = typeof(Modder_Twins),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS2 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_209.09;1 ",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_525.68;1 ",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_658.01;1 ",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLUS_209.09;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_209.09",
                    CodeName = "SLUS_20909", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLES_525.68;1",
                    Region = RegionType.PAL,
                    ExecName = "SLES_525.68",
                    CodeName = "SLES_52568", },
                    new RegionCode() {
                    Name = @"BOOT2 = cdrom0:\SLPM_658.01;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SLPM_658.01",
                    CodeName = "SLPM_65801", },
                },
            },
            new Game()
            {
                Name = "Crash Bandicoot: The Wrath of Cortex",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS2,
                    ConsoleMode.GCN,
                    ConsoleMode.XBOX,
                },
                API_Credit = "No API Available",
                Icon = null,
                ModderClass = typeof(Modder_TWOC),
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
            },
            new Game()
            {
                Name = "Crash Bandicoot",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by chekwob and ManDude",
                Icon = null,
                ModderClass = typeof(Modder_Crash1),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_949.00;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_949.00",
                    CodeName = "SCUS_94900", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_003.44;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_003.44",
                    CodeName = "SCES_00344", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_100.31;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.31",
                    CodeName = "SCPS_10031", },
                },
            },
            new Game()
            {
                Name = "Crash Bandicoot 2",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by chekwob and ManDude",
                Icon = null,
                ModderClass = typeof(Modder_Crash2),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_941.54;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_941.54",
                    CodeName = "SCUS_94154", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_009.67;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_009.67",
                    CodeName = "SCES_00967", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_100.47;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.47",
                    CodeName = "SCPS_10047", },
                },
            },
            new Game()
            {
                Name = "Crash Bandicoot: Warped",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by chekwob and ManDude",
                Icon = null,
                ModderClass = typeof(Modder_Crash3),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_942.44;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_942.44",
                    CodeName = "SCUS_94244", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_014.20;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_014.20",
                    CodeName = "SCES_01420", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_100.73;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.73",
                    CodeName = "SCPS_10073", },
                },
            },
            new Game()
            {
                Name = "Crash Team Racing",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by DCxDemo",
                Icon = null,
                ModderClass = typeof(Modder_CTR),
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_944.26;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_944.26",
                    CodeName = "SCUS_94426", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_021.05;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_021.05",
                    CodeName = "SCES_02105", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_101.18;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_101.18",
                    CodeName = "SCPS_10118", },
                },
            },
            new Game()
            {
                Name = "Crash Bash",
                Consoles = new ConsoleMode[]
                {
                    ConsoleMode.PS1
                },
                API_Credit = "No API available",
                Icon = null,
                ModderClass = typeof(Modder_Bash),
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
            },
        };
    }

    public struct Game
    {
        /// <summary> Displayed name of the game. </summary>
        public string Name;
        /// <summary> Console types to check for game detection. </summary>
        public ConsoleMode[] Consoles;
        /// <summary> Detailed credit of the individual game's support. </summary>
        public string API_Credit;
        /// <summary> Display an icon or set to null to not display one. </summary>
        public System.Drawing.Image Icon;
        /// <summary> The individual game's class of which methods are invoked. </summary>
        public Type ModderClass;
        /// <summary> Set to true to enable mod menu. </summary>
        public bool ModMenuEnabled;
        /// <summary> Set to true to enable mod crates. </summary>
        public bool ModCratesSupported;
        /// <summary> List of region identifiers for PS1 games. </summary>
        public RegionCode[] RegionID_PS1;
        /// <summary> List of region identifiers for PS2 games. </summary>
        public RegionCode[] RegionID_PS2;
        /// <summary> List of region identifiers for PSP games. </summary>
        public RegionCode[] RegionID_PSP;
        /// <summary> List of region identifiers for GCN games. </summary>
        public RegionCode[] RegionID_GCN;
        /// <summary> List of region identifiers for WII games. </summary>
        public RegionCode[] RegionID_WII;
    }
    public struct RegionCode
    {
        public string Name;
        public RegionType Region;
        public string ExecName;
        public string CodeName;
    }
}
