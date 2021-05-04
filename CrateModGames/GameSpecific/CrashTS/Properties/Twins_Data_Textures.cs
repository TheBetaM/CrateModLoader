using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Textures), ModAllowedConsoles(ConsoleMode.PS2)]
    public static class Twins_Data_Textures
    {

        public static ModProp_TextureFile Texture_Icons = new ModProp_TextureFile(@"Startup\Icons.psm", "Icons", "");

        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Crash = new ModProp_TextureFile(@"Language\Titles\American\Crash.psm", "Game Logo", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Hub01 = new ModProp_TextureFile(@"Language\Titles\American\Hub01.psm", "Logo - N.Sanity Island", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Hub02 = new ModProp_TextureFile(@"Language\Titles\American\Hub02.psm", "Logo - Iceberg Lab", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Hub03 = new ModProp_TextureFile(@"Language\Titles\American\Hub03.psm", "Logo - Academy Of Evil", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Hub04 = new ModProp_TextureFile(@"Language\Titles\American\Hub04.psm", "Logo - Twinsanity Island", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level01 = new ModProp_TextureFile(@"Language\Titles\American\Level01.psm", "Logo - Jungle Bungle", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level02 = new ModProp_TextureFile(@"Language\Titles\American\Level02.psm", "Logo - Cavern Catastrophe", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level03 = new ModProp_TextureFile(@"Language\Titles\American\Level03.psm", "Logo - Totem Hokum", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level04 = new ModProp_TextureFile(@"Language\Titles\American\Level04.psm", "Logo - Ice Climb", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level05 = new ModProp_TextureFile(@"Language\Titles\American\Level05.psm", "Logo - Slip Slide Icecapades", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level06 = new ModProp_TextureFile(@"Language\Titles\American\Level06.psm", "Logo - Hi Seas Hijinks", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level07 = new ModProp_TextureFile(@"Language\Titles\American\Level07.psm", "Logo - Gone A Bit Coco", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level08 = new ModProp_TextureFile(@"Language\Titles\American\Level08.psm", "Logo - Boiler Room Doom", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level09 = new ModProp_TextureFile(@"Language\Titles\American\Level09.psm", "Logo - Classroom Chaos", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level10 = new ModProp_TextureFile(@"Language\Titles\American\Level10.psm", "Logo - Rooftop Rampage", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level11 = new ModProp_TextureFile(@"Language\Titles\American\Level11.psm", "Logo - Rockslide Rumble", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level12 = new ModProp_TextureFile(@"Language\Titles\American\Level12.psm", "Logo - Bandicoot Pursuit", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Titles_Level13 = new ModProp_TextureFile(@"Language\Titles\American\Level13.psm", "Logo - Ant Agony", "");

        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_CrashPAL = new ModProp_TextureFile(@"Language\Titles\English\Crash.psm", "Game Logo", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Hub01PAL = new ModProp_TextureFile(@"Language\Titles\English\Hub01.psm", "Logo - N.Sanity Island", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Hub02PAL = new ModProp_TextureFile(@"Language\Titles\English\Hub02.psm", "Logo - Iceberg Lab", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Hub03PAL = new ModProp_TextureFile(@"Language\Titles\English\Hub03.psm", "Logo - Academy Of Evil", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Hub04PAL = new ModProp_TextureFile(@"Language\Titles\English\Hub04.psm", "Logo - Twinsanity Island", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level01PAL = new ModProp_TextureFile(@"Language\Titles\English\Level01.psm", "Logo - Jungle Bungle", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level02PAL = new ModProp_TextureFile(@"Language\Titles\English\Level02.psm", "Logo - Cavern Catastrophe", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level03PAL = new ModProp_TextureFile(@"Language\Titles\English\Level03.psm", "Logo - Totem Hokum", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level04PAL = new ModProp_TextureFile(@"Language\Titles\English\Level04.psm", "Logo - Ice Climb", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level05PAL = new ModProp_TextureFile(@"Language\Titles\English\Level05.psm", "Logo - Slip Slide Icecapades", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level06PAL = new ModProp_TextureFile(@"Language\Titles\English\Level06.psm", "Logo - Hi Seas Hijinks", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level07PAL = new ModProp_TextureFile(@"Language\Titles\English\Level07.psm", "Logo - Gone A Bit Coco", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level08PAL = new ModProp_TextureFile(@"Language\Titles\English\Level08.psm", "Logo - Boiler Room Doom", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level09PAL = new ModProp_TextureFile(@"Language\Titles\English\Level09.psm", "Logo - Classroom Chaos", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level10PAL = new ModProp_TextureFile(@"Language\Titles\English\Level10.psm", "Logo - Rooftop Rampage", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level11PAL = new ModProp_TextureFile(@"Language\Titles\English\Level11.psm", "Logo - Rockslide Rumble", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level12PAL = new ModProp_TextureFile(@"Language\Titles\English\Level12.psm", "Logo - Bandicoot Pursuit", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Titles_Level13PAL = new ModProp_TextureFile(@"Language\Titles\English\Level13.psm", "Logo - Ant Agony", "");

        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_CrashJPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Crash.psm", "Game Logo", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Hub01JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Hub01.psm", "Logo - N.Sanity Island", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Hub02JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Hub02.psm", "Logo - Iceberg Lab", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Hub03JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Hub03.psm", "Logo - Academy Of Evil", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Hub04JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Hub04.psm", "Logo - Twinsanity Island", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level01JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level01.psm", "Logo - Jungle Bungle", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level02JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level02.psm", "Logo - Cavern Catastrophe", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level03JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level03.psm", "Logo - Totem Hokum", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level04JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level04.psm", "Logo - Ice Climb", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level05JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level05.psm", "Logo - Slip Slide Icecapades", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level06JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level06.psm", "Logo - Hi Seas Hijinks", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level07JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level07.psm", "Logo - Gone A Bit Coco", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level08JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level08.psm", "Logo - Boiler Room Doom", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level09JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level09.psm", "Logo - Classroom Chaos", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level10JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level10.psm", "Logo - Rooftop Rampage", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level11JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level11.psm", "Logo - Rockslide Rumble", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level12JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level12.psm", "Logo - Bandicoot Pursuit", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Titles_Level13JPN = new ModProp_TextureFile(@"Language\Titles\Japanese\Level13.psm", "Logo - Ant Agony", "");

        public static ModProp_TextureFile Texture_Loading_01 = new ModProp_TextureFile(@"Language\Loading\Loading1.psm", "Loading - 01", "");
        public static ModProp_TextureFile Texture_Loading_02 = new ModProp_TextureFile(@"Language\Loading\Loading2.psm", "Loading - 02", "");
        public static ModProp_TextureFile Texture_Loading_03 = new ModProp_TextureFile(@"Language\Loading\Loading3.psm", "Loading - 03", "");

        public static ModProp_TextureFile Texture_Credits = new ModProp_TextureFile(@"Language\Credits\CreditNew.psm", "Credits", "");

        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Legal = new ModProp_TextureFile(@"Language\Legal\American.psm", "Legal Display", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Legal_PAL = new ModProp_TextureFile(@"Language\Legal\English.psm", "Legal Display", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Legal_JPN = new ModProp_TextureFile(@"Language\Legal\Japanese.psm", "Legal Display", "");

        public static ModProp_TextureFile Texture_GameOver_Crash = new ModProp_TextureFile(@"Language\GameOver\Crash.psm", "Game Over - Crash", "");
        public static ModProp_TextureFile Texture_GameOver_Cortex = new ModProp_TextureFile(@"Language\GameOver\Cortex.psm", "Game Over - Cortex", "");
        public static ModProp_TextureFile Texture_GameOver_Nina = new ModProp_TextureFile(@"Language\GameOver\Mecha.psm", "Game Over - Nina", "");
        public static ModProp_TextureFile Texture_GameOver_Mecha = new ModProp_TextureFile(@"Language\GameOver\Nina.psm", "Game Over - Mecha", "");
        public static ModProp_TextureFile Texture_GameOver_CrashAndCortex = new ModProp_TextureFile(@"Language\GameOver\CrashAndCortex.psm", "Game Over - Crash And Cortex", "");

        [ModHidden]
        public static ModProp_TextureFile Texture_Decals = new ModProp_TextureFile(@"Startup\Decal.ptc", "Decals", ""); // changing it does nothing, game seems to load it from default instead?

        public static ModProp_TextureFile Texture_Font_Arial = new ModProp_TextureFile(@"Startup\Fonts\Arial.psf", "Font - Arial (Debug)", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Font_Crash = new ModProp_TextureFile(@"Startup\Fonts\Crash_Jpn.psf", "Font - Crash (NTSC-U)", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Font_CrashPAL = new ModProp_TextureFile(@"Startup\Fonts\Crash_Euro.psf", "Font - Crash (PAL)", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Font_CrashJPN = new ModProp_TextureFile(@"Startup\Fonts\Crash.psf", "Font - Crash (NTSC-J)", "");

    }
}
