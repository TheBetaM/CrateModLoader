using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrateModLoader;
using CrateModLoader.ModProperties;
using Twinsanity;
using Twinsanity.Items;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Textures), ModAllowedConsoles(ConsoleMode.PS2)]
    public static class Twins_Data_Textures
    {

        public static ModProp_TextureFile Texture_Icons = new ModProp_TextureFile(false, "Icons", "");

        public static ModProp_TextureFile Texture_Titles_Crash = new ModProp_TextureFile(false, "Game Logo", "");
        public static ModProp_TextureFile Texture_Titles_Hub01 = new ModProp_TextureFile(false, "Logo - N.Sanity Island", "");
        public static ModProp_TextureFile Texture_Titles_Hub02 = new ModProp_TextureFile(false, "Logo - Iceberg Lab", "");
        public static ModProp_TextureFile Texture_Titles_Hub03 = new ModProp_TextureFile(false, "Logo - Academy Of Evil", "");
        public static ModProp_TextureFile Texture_Titles_Hub04 = new ModProp_TextureFile(false, "Logo - Twinsanity Island", "");
        public static ModProp_TextureFile Texture_Titles_Level01 = new ModProp_TextureFile(false, "Logo - Jungle Bungle", "");
        public static ModProp_TextureFile Texture_Titles_Level02 = new ModProp_TextureFile(false, "Logo - Cavern Catastrophe", "");
        public static ModProp_TextureFile Texture_Titles_Level03 = new ModProp_TextureFile(false, "Logo - Totem Hokum", "");
        public static ModProp_TextureFile Texture_Titles_Level04 = new ModProp_TextureFile(false, "Logo - Ice Climb", "");
        public static ModProp_TextureFile Texture_Titles_Level05 = new ModProp_TextureFile(false, "Logo - Slip Slide Icecapades", "");
        public static ModProp_TextureFile Texture_Titles_Level06 = new ModProp_TextureFile(false, "Logo - Hi Seas Hijinks", "");
        public static ModProp_TextureFile Texture_Titles_Level07 = new ModProp_TextureFile(false, "Logo - Gone A Bit Coco", "");
        public static ModProp_TextureFile Texture_Titles_Level08 = new ModProp_TextureFile(false, "Logo - Boiler Room Doom", "");
        public static ModProp_TextureFile Texture_Titles_Level09 = new ModProp_TextureFile(false, "Logo - Classroom Chaos", "");
        public static ModProp_TextureFile Texture_Titles_Level10 = new ModProp_TextureFile(false, "Logo - Rooftop Rampage", "");
        public static ModProp_TextureFile Texture_Titles_Level11 = new ModProp_TextureFile(false, "Logo - Rockslide Rumble", "");
        public static ModProp_TextureFile Texture_Titles_Level12 = new ModProp_TextureFile(false, "Logo - Bandicoot Pursuit", "");
        public static ModProp_TextureFile Texture_Titles_Level13 = new ModProp_TextureFile(false, "Logo - Ant Agony", "");

        public static ModProp_TextureFile Texture_Loading_01 = new ModProp_TextureFile(false, "Loading - 01", "");
        public static ModProp_TextureFile Texture_Loading_02 = new ModProp_TextureFile(false, "Loading - 02", "");
        public static ModProp_TextureFile Texture_Loading_03 = new ModProp_TextureFile(false, "Loading - 03", "");

        public static ModProp_TextureFile Texture_Credits = new ModProp_TextureFile(false, "Credits", "");

        public static ModProp_TextureFile Texture_Legal = new ModProp_TextureFile(false, "Legal Display", "");

        public static ModProp_TextureFile Texture_GameOver_Crash = new ModProp_TextureFile(false, "Game Over - Crash", "");
        public static ModProp_TextureFile Texture_GameOver_Cortex = new ModProp_TextureFile(false, "Game Over - Cortex", "");
        public static ModProp_TextureFile Texture_GameOver_Nina = new ModProp_TextureFile(false, "Game Over - Nina", "");
        public static ModProp_TextureFile Texture_GameOver_Mecha = new ModProp_TextureFile(false, "Game Over - Mecha", "");
        public static ModProp_TextureFile Texture_GameOver_CrashAndCortex = new ModProp_TextureFile(false, "Game Over - Crash And Cortex", "");

        public static ModProp_TextureFile Texture_Decals = new ModProp_TextureFile(false, "Decals", "")
        { Hidden = true, }; // changing it does nothing, game seems to load it from default instead?

        public static ModProp_TextureFile Texture_Font_Arial = new ModProp_TextureFile(false, "Font - Arial (Debug)", "");
        [ModAllowedRegions(RegionType.NTSC_U)]
        public static ModProp_TextureFile Texture_Font_Crash = new ModProp_TextureFile(false, "Font - Crash (NTSC-U)", "");
        [ModAllowedRegions(RegionType.PAL)]
        public static ModProp_TextureFile Texture_Font_CrashPAL = new ModProp_TextureFile(false, "Font - Crash (PAL)", "");
        [ModAllowedRegions(RegionType.NTSC_J)]
        public static ModProp_TextureFile Texture_Font_CrashJPN = new ModProp_TextureFile(false, "Font - Crash (NTSC-J)", "");

        public static void Textures_Preload(string basePath, RegionType region)
        {
            string langMod = "American";
            if (region == RegionType.PAL)
            {
                langMod = "English";
            }
            else if (region == RegionType.NTSC_J)
            {
                langMod = "Japanese";
            }

            LoadTexture(basePath + @"Language\GameOver\Crash.psm", ref Texture_GameOver_Crash);
            LoadTexture(basePath + @"Language\GameOver\Cortex.psm", ref Texture_GameOver_Cortex);
            LoadTexture(basePath + @"Language\GameOver\Mecha.psm", ref Texture_GameOver_Mecha);
            LoadTexture(basePath + @"Language\GameOver\Nina.psm", ref Texture_GameOver_Nina);
            LoadTexture(basePath + @"Language\GameOver\CrashAndCortex.psm", ref Texture_GameOver_CrashAndCortex);

            LoadTexture(basePath + @"Language\Credits\CreditNew.psm", ref Texture_Credits);

            LoadTexture(basePath + @"Language\Legal\" + langMod + ".psm", ref Texture_Legal);

            LoadTexture(basePath + @"Language\Loading\Loading1.psm", ref Texture_Loading_01);
            LoadTexture(basePath + @"Language\Loading\Loading2.psm", ref Texture_Loading_02);
            LoadTexture(basePath + @"Language\Loading\Loading3.psm", ref Texture_Loading_03);

            LoadTexture(basePath + @"Startup\Icons.psm", ref Texture_Icons);

            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Crash.psm", ref Texture_Titles_Crash);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub01.psm", ref Texture_Titles_Hub01);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub02.psm", ref Texture_Titles_Hub02);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub03.psm", ref Texture_Titles_Hub03);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub04.psm", ref Texture_Titles_Hub04);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level01.psm", ref Texture_Titles_Level01);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level02.psm", ref Texture_Titles_Level02);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level03.psm", ref Texture_Titles_Level03);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level04.psm", ref Texture_Titles_Level04);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level05.psm", ref Texture_Titles_Level05);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level06.psm", ref Texture_Titles_Level06);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level07.psm", ref Texture_Titles_Level07);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level08.psm", ref Texture_Titles_Level08);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level09.psm", ref Texture_Titles_Level09);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level10.psm", ref Texture_Titles_Level10);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level11.psm", ref Texture_Titles_Level11);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level12.psm", ref Texture_Titles_Level12);
            LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level13.psm", ref Texture_Titles_Level13);

            LoadTexture(basePath + @"Startup\Decal.ptc", ref Texture_Decals);

            LoadTexture(basePath + @"Startup\Fonts\Arial.psf", ref Texture_Font_Arial);

            if (region == RegionType.PAL)
            {
                LoadTexture(basePath + @"Startup\Fonts\Crash_Euro.psf", ref Texture_Font_CrashPAL);
            }
            else if (region == RegionType.NTSC_J)
            {
                LoadTexture(basePath + @"Startup\Fonts\Crash_Jpn.psf", ref Texture_Font_CrashJPN);
            }
            else
            {
                LoadTexture(basePath + @"Startup\Fonts\Crash.psf", ref Texture_Font_Crash);
            }

            LoadTexture(basePath + @"Extras\Bosses\Boss01.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss01);
            LoadTexture(basePath + @"Extras\Bosses\Boss02.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss02);
            LoadTexture(basePath + @"Extras\Bosses\Boss03.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss03);
            LoadTexture(basePath + @"Extras\Bosses\Boss04.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss04);
            LoadTexture(basePath + @"Extras\Bosses\Boss05.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss05);
            LoadTexture(basePath + @"Extras\Bosses\Boss06.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss06);
            LoadTexture(basePath + @"Extras\Bosses\Boss07.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss07);
            LoadTexture(basePath + @"Extras\Bosses\Boss08.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss08);
            LoadTexture(basePath + @"Extras\Bosses\Boss09.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss09);
            LoadTexture(basePath + @"Extras\Bosses\Boss10.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss10);
            LoadTexture(basePath + @"Extras\Bosses\Boss11.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss11);
            LoadTexture(basePath + @"Extras\Bosses\Boss12.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss12);
            LoadTexture(basePath + @"Extras\Bosses\Boss13.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss13);
            LoadTexture(basePath + @"Extras\Bosses\Boss14.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss14);
            LoadTexture(basePath + @"Extras\Bosses\Boss15.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss15);
            LoadTexture(basePath + @"Extras\Bosses\Boss16.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss16);

            LoadTexture(basePath + @"Extras\Concept\Concept01.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept01);
            LoadTexture(basePath + @"Extras\Concept\Concept02.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept02);
            LoadTexture(basePath + @"Extras\Concept\Concept03.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept03);
            LoadTexture(basePath + @"Extras\Concept\Concept04.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept04);
            LoadTexture(basePath + @"Extras\Concept\Concept05.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept05);
            LoadTexture(basePath + @"Extras\Concept\Concept06.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept06);
            LoadTexture(basePath + @"Extras\Concept\Concept07.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept07);
            LoadTexture(basePath + @"Extras\Concept\Concept08.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept08);
            LoadTexture(basePath + @"Extras\Concept\Concept09.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept09);
            LoadTexture(basePath + @"Extras\Concept\Concept10.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept10);
            LoadTexture(basePath + @"Extras\Concept\Concept11.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept11);
            LoadTexture(basePath + @"Extras\Concept\Concept12.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept12);
            LoadTexture(basePath + @"Extras\Concept\Concept13.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept13);
            LoadTexture(basePath + @"Extras\Concept\Concept14.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept14);
            LoadTexture(basePath + @"Extras\Concept\Concept15.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept15);
            LoadTexture(basePath + @"Extras\Concept\Concept16.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept16);
            LoadTexture(basePath + @"Extras\Concept\Concept17.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept17);
            LoadTexture(basePath + @"Extras\Concept\Concept18.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept18);
            LoadTexture(basePath + @"Extras\Concept\Concept19.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept19);
            LoadTexture(basePath + @"Extras\Concept\Concept20.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept20);
            LoadTexture(basePath + @"Extras\Concept\Concept21.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept21);
            LoadTexture(basePath + @"Extras\Concept\Concept22.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept22);
            LoadTexture(basePath + @"Extras\Concept\Concept23.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept23);
            LoadTexture(basePath + @"Extras\Concept\Concept24.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept24);
            LoadTexture(basePath + @"Extras\Concept\Concept25.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept25);
            LoadTexture(basePath + @"Extras\Concept\Concept26.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept26);
            LoadTexture(basePath + @"Extras\Concept\Concept27.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept27);
            LoadTexture(basePath + @"Extras\Concept\Concept28.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept28);
            LoadTexture(basePath + @"Extras\Concept\Concept29.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept29);
            LoadTexture(basePath + @"Extras\Concept\Concept30.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept30);
            LoadTexture(basePath + @"Extras\Concept\Concept31.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept31);
            LoadTexture(basePath + @"Extras\Concept\Concept32.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept32);

            LoadTexture(basePath + @"Extras\Enemies\Enemy01.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy01);
            LoadTexture(basePath + @"Extras\Enemies\Enemy02.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy02);
            LoadTexture(basePath + @"Extras\Enemies\Enemy03.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy03);
            LoadTexture(basePath + @"Extras\Enemies\Enemy04.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy04);
            LoadTexture(basePath + @"Extras\Enemies\Enemy05.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy05);
            LoadTexture(basePath + @"Extras\Enemies\Enemy06.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy06);
            LoadTexture(basePath + @"Extras\Enemies\Enemy07.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy07);
            LoadTexture(basePath + @"Extras\Enemies\Enemy08.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy08);
            LoadTexture(basePath + @"Extras\Enemies\Enemy09.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy09);
            LoadTexture(basePath + @"Extras\Enemies\Enemy10.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy10);
            LoadTexture(basePath + @"Extras\Enemies\Enemy11.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy11);
            LoadTexture(basePath + @"Extras\Enemies\Enemy12.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy12);
            LoadTexture(basePath + @"Extras\Enemies\Enemy13.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy13);
            LoadTexture(basePath + @"Extras\Enemies\Enemy14.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy14);
            LoadTexture(basePath + @"Extras\Enemies\Enemy15.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy15);
            LoadTexture(basePath + @"Extras\Enemies\Enemy16.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy16);
            LoadTexture(basePath + @"Extras\Enemies\Enemy17.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy17);
            LoadTexture(basePath + @"Extras\Enemies\Enemy18.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy18);
            LoadTexture(basePath + @"Extras\Enemies\Enemy19.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy19);
            LoadTexture(basePath + @"Extras\Enemies\Enemy20.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy20);
            LoadTexture(basePath + @"Extras\Enemies\Enemy21.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy21);
            LoadTexture(basePath + @"Extras\Enemies\Enemy22.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy22);
            LoadTexture(basePath + @"Extras\Enemies\Enemy23.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy23);
            LoadTexture(basePath + @"Extras\Enemies\Enemy24.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy24);
            LoadTexture(basePath + @"Extras\Enemies\Enemy25.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy25);
            LoadTexture(basePath + @"Extras\Enemies\Enemy26.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy26);
            LoadTexture(basePath + @"Extras\Enemies\Enemy27.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy27);
            LoadTexture(basePath + @"Extras\Enemies\Enemy28.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy28);
            LoadTexture(basePath + @"Extras\Enemies\Enemy29.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy29);
            LoadTexture(basePath + @"Extras\Enemies\Enemy30.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy30);
            LoadTexture(basePath + @"Extras\Enemies\Enemy31.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy31);
            LoadTexture(basePath + @"Extras\Enemies\Enemy32.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy32);

            LoadTexture(basePath + @"Extras\Unseen\Unseen01.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen01);
            LoadTexture(basePath + @"Extras\Unseen\Unseen02.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen02);
            LoadTexture(basePath + @"Extras\Unseen\Unseen03.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen03);
            LoadTexture(basePath + @"Extras\Unseen\Unseen04.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen04);
            LoadTexture(basePath + @"Extras\Unseen\Unseen05.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen05);
            LoadTexture(basePath + @"Extras\Unseen\Unseen06.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen06);
            LoadTexture(basePath + @"Extras\Unseen\Unseen07.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen07);
            LoadTexture(basePath + @"Extras\Unseen\Unseen08.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen08);
            LoadTexture(basePath + @"Extras\Unseen\Unseen09.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen09);
            LoadTexture(basePath + @"Extras\Unseen\Unseen10.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen10);
            LoadTexture(basePath + @"Extras\Unseen\Unseen11.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen11);
            LoadTexture(basePath + @"Extras\Unseen\Unseen12.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen12);
            LoadTexture(basePath + @"Extras\Unseen\Unseen13.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen13);
            LoadTexture(basePath + @"Extras\Unseen\Unseen14.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen14);
            LoadTexture(basePath + @"Extras\Unseen\Unseen15.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen15);
            LoadTexture(basePath + @"Extras\Unseen\Unseen16.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen16);
            LoadTexture(basePath + @"Extras\Unseen\Unseen17.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen17);
            LoadTexture(basePath + @"Extras\Unseen\Unseen18.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen18);
            LoadTexture(basePath + @"Extras\Unseen\Unseen19.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen19);
            LoadTexture(basePath + @"Extras\Unseen\Unseen20.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen20);
            LoadTexture(basePath + @"Extras\Unseen\Unseen21.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen21);
            LoadTexture(basePath + @"Extras\Unseen\Unseen22.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen22);
            LoadTexture(basePath + @"Extras\Unseen\Unseen23.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen23);
            LoadTexture(basePath + @"Extras\Unseen\Unseen24.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen24);
            LoadTexture(basePath + @"Extras\Unseen\Unseen25.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen25);
            LoadTexture(basePath + @"Extras\Unseen\Unseen26.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen26);
            LoadTexture(basePath + @"Extras\Unseen\Unseen27.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen27);
            LoadTexture(basePath + @"Extras\Unseen\Unseen28.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen28);
            LoadTexture(basePath + @"Extras\Unseen\Unseen29.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen29);
            LoadTexture(basePath + @"Extras\Unseen\Unseen30.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen30);
            LoadTexture(basePath + @"Extras\Unseen\Unseen31.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen31);
            LoadTexture(basePath + @"Extras\Unseen\Unseen32.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen32);
            LoadTexture(basePath + @"Extras\Unseen\Unseen33.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen33);
            LoadTexture(basePath + @"Extras\Unseen\Unseen34.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen34);
            LoadTexture(basePath + @"Extras\Unseen\Unseen35.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen35);
            LoadTexture(basePath + @"Extras\Unseen\Unseen36.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen36);
            LoadTexture(basePath + @"Extras\Unseen\Unseen37.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen37);
            LoadTexture(basePath + @"Extras\Unseen\Unseen38.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen38);
            LoadTexture(basePath + @"Extras\Unseen\Unseen39.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen39);
            LoadTexture(basePath + @"Extras\Unseen\Unseen40.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen40);
            LoadTexture(basePath + @"Extras\Unseen\Unseen41.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen41);
            LoadTexture(basePath + @"Extras\Unseen\Unseen42.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen42);
            LoadTexture(basePath + @"Extras\Unseen\Unseen43.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen43);
            LoadTexture(basePath + @"Extras\Unseen\Unseen44.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen44);
            LoadTexture(basePath + @"Extras\Unseen\Unseen45.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen45);
            LoadTexture(basePath + @"Extras\Unseen\Unseen46.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen46);
            LoadTexture(basePath + @"Extras\Unseen\Unseen47.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen47);
            LoadTexture(basePath + @"Extras\Unseen\Unseen48.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen48);

            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity01);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity02);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity03);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity04);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity05);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity06);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity07);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity08);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity09);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity10);
            LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity11);

            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle01);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle02);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle03);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle04);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle05);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle06);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle07);
            LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle08);

            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern01);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern02);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern03);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern04);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern05);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern06);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern07);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern08);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern09);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern10);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern11);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern12);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern13);
            LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern14);

            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem01);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem02);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem03);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem04);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem05);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem06);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem07);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem08);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem09);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem10);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem11);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem12);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem13);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem14);
            LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem15);

            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub01);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub02);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub03);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub04);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub05);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub06);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub07);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub08);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub09);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub10);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub11);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub12);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub13);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub14);
            LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub15);

            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb01);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb02);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb03);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb04);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb05);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb06);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb07);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb08);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb09);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb10);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb11);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb12);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb13);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb14);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb15);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb16.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb16);
            LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb17.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb17);

            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip01.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip01);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip02.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip02);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip03.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip03);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip04.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip04);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip05.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip05);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip06.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip06);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip07.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip07);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip08.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip08);
            LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip09.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip09);

            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas01);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas02);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas03);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas04);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas05);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas06);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas07);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas08);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas09);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas10);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas11);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas12);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas13);
            LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas14);

            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy01);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy02);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy03);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy04);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy05);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy06);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy07);
            LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy08);

            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler01);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler02);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler03);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler04);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler05);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler06);
            LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler07);

            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom01);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom02);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom03);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom04);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom05);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom06);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom07);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom08);
            LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom09);

            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop01);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop02);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop03);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop04);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop05);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop06);
            LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop07);

            LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity01);
            LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity02);
            LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity03);
            LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity04);
            LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity05);

            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide01);
            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide02);
            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide03);
            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide04);
            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide05);
            LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide06);

            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit01);
            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit02);
            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit03);
            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit04);
            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit05);
            LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit06);

            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony01);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony02);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony03);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony04);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony05);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony06);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony07);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony08);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony09);
            LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony10);

        }

        public static void Textures_Mod(string basePath, RegionType region)
        {
            string langMod = "American";
            if (region == RegionType.PAL)
            {
                langMod = "English";
            }
            else if (region == RegionType.NTSC_J)
            {
                langMod = "Japanese";
            }

            SaveTexture(basePath + @"Language\GameOver\Crash.psm", ref Texture_GameOver_Crash);
            SaveTexture(basePath + @"Language\GameOver\Cortex.psm", ref Texture_GameOver_Cortex);
            SaveTexture(basePath + @"Language\GameOver\Mecha.psm", ref Texture_GameOver_Mecha);
            SaveTexture(basePath + @"Language\GameOver\Nina.psm", ref Texture_GameOver_Nina);
            SaveTexture(basePath + @"Language\GameOver\CrashAndCortex.psm", ref Texture_GameOver_CrashAndCortex);

            SaveTexture(basePath + @"Language\Credits\CreditNew.psm", ref Texture_Credits);

            SaveTexture(basePath + @"Language\Legal\" + langMod + ".psm", ref Texture_Legal);

            SaveTexture(basePath + @"Language\Loading\Loading1.psm", ref Texture_Loading_01);
            SaveTexture(basePath + @"Language\Loading\Loading2.psm", ref Texture_Loading_02);
            SaveTexture(basePath + @"Language\Loading\Loading3.psm", ref Texture_Loading_03);

            SaveTexture(basePath + @"Startup\Icons.psm", ref Texture_Icons);

            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Crash.psm", ref Texture_Titles_Crash);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub01.psm", ref Texture_Titles_Hub01);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub02.psm", ref Texture_Titles_Hub02);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub03.psm", ref Texture_Titles_Hub03);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub04.psm", ref Texture_Titles_Hub04);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level01.psm", ref Texture_Titles_Level01);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level02.psm", ref Texture_Titles_Level02);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level03.psm", ref Texture_Titles_Level03);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level04.psm", ref Texture_Titles_Level04);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level05.psm", ref Texture_Titles_Level05);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level06.psm", ref Texture_Titles_Level06);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level07.psm", ref Texture_Titles_Level07);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level08.psm", ref Texture_Titles_Level08);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level09.psm", ref Texture_Titles_Level09);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level10.psm", ref Texture_Titles_Level10);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level11.psm", ref Texture_Titles_Level11);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level12.psm", ref Texture_Titles_Level12);
            SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level13.psm", ref Texture_Titles_Level13);

            SaveTexture(basePath + @"Startup\Fonts\Arial.psf", ref Texture_Font_Arial);

            if (region == RegionType.PAL)
            {
                SaveTexture(basePath + @"Startup\Fonts\Crash_Euro.psf", ref Texture_Font_CrashPAL);
            }
            else if (region == RegionType.NTSC_J)
            {
                SaveTexture(basePath + @"Startup\Fonts\Crash_Jpn.psf", ref Texture_Font_CrashJPN);
            }
            else
            {
                SaveTexture(basePath + @"Startup\Fonts\Crash.psf", ref Texture_Font_Crash);
            }

            SaveTexture(basePath + @"Extras\Bosses\Boss01.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss01);
            SaveTexture(basePath + @"Extras\Bosses\Boss02.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss02);
            SaveTexture(basePath + @"Extras\Bosses\Boss03.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss03);
            SaveTexture(basePath + @"Extras\Bosses\Boss04.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss04);
            SaveTexture(basePath + @"Extras\Bosses\Boss05.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss05);
            SaveTexture(basePath + @"Extras\Bosses\Boss06.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss06);
            SaveTexture(basePath + @"Extras\Bosses\Boss07.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss07);
            SaveTexture(basePath + @"Extras\Bosses\Boss08.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss08);
            SaveTexture(basePath + @"Extras\Bosses\Boss09.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss09);
            SaveTexture(basePath + @"Extras\Bosses\Boss10.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss10);
            SaveTexture(basePath + @"Extras\Bosses\Boss11.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss11);
            SaveTexture(basePath + @"Extras\Bosses\Boss12.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss12);
            SaveTexture(basePath + @"Extras\Bosses\Boss13.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss13);
            SaveTexture(basePath + @"Extras\Bosses\Boss14.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss14);
            SaveTexture(basePath + @"Extras\Bosses\Boss15.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss15);
            SaveTexture(basePath + @"Extras\Bosses\Boss16.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss16);

            SaveTexture(basePath + @"Extras\Concept\Concept01.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept01);
            SaveTexture(basePath + @"Extras\Concept\Concept02.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept02);
            SaveTexture(basePath + @"Extras\Concept\Concept03.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept03);
            SaveTexture(basePath + @"Extras\Concept\Concept04.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept04);
            SaveTexture(basePath + @"Extras\Concept\Concept05.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept05);
            SaveTexture(basePath + @"Extras\Concept\Concept06.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept06);
            SaveTexture(basePath + @"Extras\Concept\Concept07.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept07);
            SaveTexture(basePath + @"Extras\Concept\Concept08.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept08);
            SaveTexture(basePath + @"Extras\Concept\Concept09.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept09);
            SaveTexture(basePath + @"Extras\Concept\Concept10.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept10);
            SaveTexture(basePath + @"Extras\Concept\Concept11.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept11);
            SaveTexture(basePath + @"Extras\Concept\Concept12.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept12);
            SaveTexture(basePath + @"Extras\Concept\Concept13.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept13);
            SaveTexture(basePath + @"Extras\Concept\Concept14.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept14);
            SaveTexture(basePath + @"Extras\Concept\Concept15.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept15);
            SaveTexture(basePath + @"Extras\Concept\Concept16.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept16);
            SaveTexture(basePath + @"Extras\Concept\Concept17.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept17);
            SaveTexture(basePath + @"Extras\Concept\Concept18.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept18);
            SaveTexture(basePath + @"Extras\Concept\Concept19.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept19);
            SaveTexture(basePath + @"Extras\Concept\Concept20.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept20);
            SaveTexture(basePath + @"Extras\Concept\Concept21.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept21);
            SaveTexture(basePath + @"Extras\Concept\Concept22.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept22);
            SaveTexture(basePath + @"Extras\Concept\Concept23.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept23);
            SaveTexture(basePath + @"Extras\Concept\Concept24.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept24);
            SaveTexture(basePath + @"Extras\Concept\Concept25.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept25);
            SaveTexture(basePath + @"Extras\Concept\Concept26.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept26);
            SaveTexture(basePath + @"Extras\Concept\Concept27.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept27);
            SaveTexture(basePath + @"Extras\Concept\Concept28.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept28);
            SaveTexture(basePath + @"Extras\Concept\Concept29.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept29);
            SaveTexture(basePath + @"Extras\Concept\Concept30.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept30);
            SaveTexture(basePath + @"Extras\Concept\Concept31.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept31);
            SaveTexture(basePath + @"Extras\Concept\Concept32.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept32);

            SaveTexture(basePath + @"Extras\Enemies\Enemy01.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy01);
            SaveTexture(basePath + @"Extras\Enemies\Enemy02.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy02);
            SaveTexture(basePath + @"Extras\Enemies\Enemy03.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy03);
            SaveTexture(basePath + @"Extras\Enemies\Enemy04.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy04);
            SaveTexture(basePath + @"Extras\Enemies\Enemy05.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy05);
            SaveTexture(basePath + @"Extras\Enemies\Enemy06.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy06);
            SaveTexture(basePath + @"Extras\Enemies\Enemy07.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy07);
            SaveTexture(basePath + @"Extras\Enemies\Enemy08.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy08);
            SaveTexture(basePath + @"Extras\Enemies\Enemy09.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy09);
            SaveTexture(basePath + @"Extras\Enemies\Enemy10.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy10);
            SaveTexture(basePath + @"Extras\Enemies\Enemy11.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy11);
            SaveTexture(basePath + @"Extras\Enemies\Enemy12.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy12);
            SaveTexture(basePath + @"Extras\Enemies\Enemy13.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy13);
            SaveTexture(basePath + @"Extras\Enemies\Enemy14.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy14);
            SaveTexture(basePath + @"Extras\Enemies\Enemy15.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy15);
            SaveTexture(basePath + @"Extras\Enemies\Enemy16.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy16);
            SaveTexture(basePath + @"Extras\Enemies\Enemy17.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy17);
            SaveTexture(basePath + @"Extras\Enemies\Enemy18.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy18);
            SaveTexture(basePath + @"Extras\Enemies\Enemy19.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy19);
            SaveTexture(basePath + @"Extras\Enemies\Enemy20.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy20);
            SaveTexture(basePath + @"Extras\Enemies\Enemy21.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy21);
            SaveTexture(basePath + @"Extras\Enemies\Enemy22.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy22);
            SaveTexture(basePath + @"Extras\Enemies\Enemy23.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy23);
            SaveTexture(basePath + @"Extras\Enemies\Enemy24.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy24);
            SaveTexture(basePath + @"Extras\Enemies\Enemy25.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy25);
            SaveTexture(basePath + @"Extras\Enemies\Enemy26.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy26);
            SaveTexture(basePath + @"Extras\Enemies\Enemy27.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy27);
            SaveTexture(basePath + @"Extras\Enemies\Enemy28.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy28);
            SaveTexture(basePath + @"Extras\Enemies\Enemy29.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy29);
            SaveTexture(basePath + @"Extras\Enemies\Enemy30.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy30);
            SaveTexture(basePath + @"Extras\Enemies\Enemy31.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy31);
            SaveTexture(basePath + @"Extras\Enemies\Enemy32.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy32);

            SaveTexture(basePath + @"Extras\Unseen\Unseen01.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen01);
            SaveTexture(basePath + @"Extras\Unseen\Unseen02.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen02);
            SaveTexture(basePath + @"Extras\Unseen\Unseen03.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen03);
            SaveTexture(basePath + @"Extras\Unseen\Unseen04.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen04);
            SaveTexture(basePath + @"Extras\Unseen\Unseen05.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen05);
            SaveTexture(basePath + @"Extras\Unseen\Unseen06.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen06);
            SaveTexture(basePath + @"Extras\Unseen\Unseen07.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen07);
            SaveTexture(basePath + @"Extras\Unseen\Unseen08.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen08);
            SaveTexture(basePath + @"Extras\Unseen\Unseen09.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen09);
            SaveTexture(basePath + @"Extras\Unseen\Unseen10.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen10);
            SaveTexture(basePath + @"Extras\Unseen\Unseen11.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen11);
            SaveTexture(basePath + @"Extras\Unseen\Unseen12.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen12);
            SaveTexture(basePath + @"Extras\Unseen\Unseen13.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen13);
            SaveTexture(basePath + @"Extras\Unseen\Unseen14.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen14);
            SaveTexture(basePath + @"Extras\Unseen\Unseen15.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen15);
            SaveTexture(basePath + @"Extras\Unseen\Unseen16.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen16);
            SaveTexture(basePath + @"Extras\Unseen\Unseen17.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen17);
            SaveTexture(basePath + @"Extras\Unseen\Unseen18.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen18);
            SaveTexture(basePath + @"Extras\Unseen\Unseen19.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen19);
            SaveTexture(basePath + @"Extras\Unseen\Unseen20.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen20);
            SaveTexture(basePath + @"Extras\Unseen\Unseen21.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen21);
            SaveTexture(basePath + @"Extras\Unseen\Unseen22.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen22);
            SaveTexture(basePath + @"Extras\Unseen\Unseen23.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen23);
            SaveTexture(basePath + @"Extras\Unseen\Unseen24.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen24);
            SaveTexture(basePath + @"Extras\Unseen\Unseen25.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen25);
            SaveTexture(basePath + @"Extras\Unseen\Unseen26.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen26);
            SaveTexture(basePath + @"Extras\Unseen\Unseen27.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen27);
            SaveTexture(basePath + @"Extras\Unseen\Unseen28.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen28);
            SaveTexture(basePath + @"Extras\Unseen\Unseen29.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen29);
            SaveTexture(basePath + @"Extras\Unseen\Unseen30.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen30);
            SaveTexture(basePath + @"Extras\Unseen\Unseen31.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen31);
            SaveTexture(basePath + @"Extras\Unseen\Unseen32.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen32);
            SaveTexture(basePath + @"Extras\Unseen\Unseen33.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen33);
            SaveTexture(basePath + @"Extras\Unseen\Unseen34.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen34);
            SaveTexture(basePath + @"Extras\Unseen\Unseen35.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen35);
            SaveTexture(basePath + @"Extras\Unseen\Unseen36.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen36);
            SaveTexture(basePath + @"Extras\Unseen\Unseen37.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen37);
            SaveTexture(basePath + @"Extras\Unseen\Unseen38.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen38);
            SaveTexture(basePath + @"Extras\Unseen\Unseen39.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen39);
            SaveTexture(basePath + @"Extras\Unseen\Unseen40.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen40);
            SaveTexture(basePath + @"Extras\Unseen\Unseen41.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen41);
            SaveTexture(basePath + @"Extras\Unseen\Unseen42.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen42);
            SaveTexture(basePath + @"Extras\Unseen\Unseen43.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen43);
            SaveTexture(basePath + @"Extras\Unseen\Unseen44.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen44);
            SaveTexture(basePath + @"Extras\Unseen\Unseen45.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen45);
            SaveTexture(basePath + @"Extras\Unseen\Unseen46.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen46);
            SaveTexture(basePath + @"Extras\Unseen\Unseen47.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen47);
            SaveTexture(basePath + @"Extras\Unseen\Unseen48.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen48);

            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity01);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity02);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity03);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity04);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity05);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity06);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity07);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity08);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity09);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity10);
            SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity11);

            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle01);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle02);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle03);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle04);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle05);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle06);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle07);
            SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle08);

            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern01);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern02);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern03);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern04);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern05);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern06);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern07);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern08);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern09);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern10);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern11);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern12);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern13);
            SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern14);

            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem01);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem02);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem03);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem04);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem05);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem06);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem07);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem08);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem09);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem10);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem11);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem12);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem13);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem14);
            SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem15);

            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub01);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub02);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub03);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub04);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub05);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub06);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub07);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub08);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub09);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub10);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub11);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub12);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub13);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub14);
            SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub15);

            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb01);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb02);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb03);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb04);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb05);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb06);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb07);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb08);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb09);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb10);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb11);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb12);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb13);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb14);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb15);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb16.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb16);
            SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb17.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb17);

            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip01.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip01);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip02.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip02);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip03.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip03);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip04.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip04);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip05.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip05);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip06.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip06);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip07.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip07);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip08.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip08);
            SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip09.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip09);

            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas01);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas02);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas03);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas04);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas05);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas06);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas07);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas08);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas09);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas10);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas11);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas12);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas13);
            SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas14);

            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy01);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy02);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy03);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy04);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy05);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy06);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy07);
            SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy08);

            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler01);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler02);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler03);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler04);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler05);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler06);
            SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler07);

            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom01);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom02);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom03);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom04);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom05);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom06);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom07);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom08);
            SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom09);

            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop01);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop02);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop03);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop04);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop05);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop06);
            SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop07);

            SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity01);
            SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity02);
            SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity03);
            SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity04);
            SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity05);

            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide01);
            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide02);
            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide03);
            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide04);
            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide05);
            SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide06);

            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit01);
            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit02);
            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit03);
            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit04);
            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit05);
            SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit06);

            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony01);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony02);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony03);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony04);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony05);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony06);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony07);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony08);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony09);
            SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony10);
        }

        public static void LoadTexture(string path, ref ModProp_TextureFile target)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    List<TwinsPTC> PTCs = null;
                    if (path.EndsWith("psf"))
                    {
                        TwinsPSF psf = new TwinsPSF();
                        psf.Load(reader, (int)fileStream.Length);
                        PTCs = psf.FontPages;
                    }
                    else if (path.EndsWith("ptc"))
                    {
                        TwinsPTC ptc = new TwinsPTC();
                        ptc.Load(reader, (int)fileStream.Length);
                        PTCs = new List<TwinsPTC>();
                        PTCs.Add(ptc);
                    }
                    else if (path.EndsWith("psm"))
                    {
                        TwinsPSM psm = new TwinsPSM();
                        psm.Load(reader, (int)fileStream.Length);
                        PTCs = psm.PTCs;
                    }

                    if (PTCs.Count > 0)
                    {
                        int ogWidth = PTCs[0].Texture.Width;
                        int ogHeight = PTCs[0].Texture.Height;
                        int maxWidth = PTCs[0].Texture.Width;
                        int maxHeight = PTCs[0].Texture.Height;

                        if (PTCs.Count > 1)
                        {
                            maxWidth += ogWidth;
                            if (PTCs.Count > 2)
                            {
                                maxWidth += ogWidth;
                            }
                            if (PTCs.Count > 3)
                            {
                                maxWidth += ogWidth;
                            }
                        }
                        int rows = (PTCs.Count / 4);
                        if (rows == 0)
                            rows = 1;

                        maxHeight = maxHeight * rows;

                        Bitmap map = new Bitmap(maxWidth, maxHeight);

                        int ptc = 0;
                        int col = 0;
                        int row = 0;
                        while (ptc < PTCs.Count)
                        {
                            int c = 0;
                            for (int y = row * ogHeight; y < ogHeight + (row * ogHeight); y++)
                            {
                                for (int x = col * ogWidth; x < ogWidth + (col * ogWidth); x++)
                                {
                                    if (c < PTCs[ptc].Texture.RawData.Length)
                                    {
                                        map.SetPixel(x, y, PTCs[ptc].Texture.RawData[c]);
                                    }
                                    else
                                    {
                                        map.SetPixel(x, y, Color.Black);
                                    }
                                    c++;
                                }
                            }
                            col++;
                            if (col == 4)
                            {
                                col = 0;
                                row++;
                            }
                            ptc++;
                        }
                        target.Resource = map;
                        target.Value = true;
                    }

                }
            }
        }

        public static void SaveTexture(string path, ref ModProp_TextureFile target)
        {
            if (target.HasChanged && File.Exists(path))
            {
                TwinsPSF psf = new TwinsPSF();
                TwinsPTC ptcfile = new TwinsPTC();
                TwinsPSM psm = new TwinsPSM();
                List<TwinsPTC> PTCs = null;
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        if (path.EndsWith("psf"))
                        {
                            psf = new TwinsPSF();
                            psf.Load(reader, (int)fileStream.Length);
                            PTCs = psf.FontPages;
                        }
                        else if (path.EndsWith("ptc"))
                        {
                            ptcfile = new TwinsPTC();
                            ptcfile.Load(reader, (int)fileStream.Length);
                            PTCs = new List<TwinsPTC>();
                            PTCs.Add(ptcfile);
                        }
                        else if (path.EndsWith("psm"))
                        {
                            psm = new TwinsPSM();
                            psm.Load(reader, (int)fileStream.Length);
                            PTCs = psm.PTCs;
                        }

                        if (PTCs != null && PTCs.Count > 0)
                        {
                            int ogWidth = PTCs[0].Texture.Width;
                            int ogHeight = PTCs[0].Texture.Height;
                            int maxWidth = PTCs[0].Texture.Width;
                            int maxHeight = PTCs[0].Texture.Height;

                            if (PTCs.Count > 1)
                            {
                                maxWidth += ogWidth;
                                if (PTCs.Count > 2)
                                {
                                    maxWidth += ogWidth;
                                }
                                if (PTCs.Count > 3)
                                {
                                    maxWidth += ogWidth;
                                }
                            }
                            int rows = (PTCs.Count / 4);
                            if (rows == 0)
                                rows = 1;

                            maxHeight = maxHeight * rows;

                            Bitmap map = target.Resource;

                            int ptc = 0;
                            int col = 0;
                            int row = 0;
                            while (ptc < PTCs.Count)
                            {
                                int c = 0;
                                for (int y = row * ogHeight; y < ogHeight + (row * ogHeight); y++)
                                {
                                    for (int x = col * ogWidth; x < ogWidth + (col * ogWidth); x++)
                                    {
                                        if (c < PTCs[ptc].Texture.RawData.Length)
                                        {
                                            PTCs[ptc].Texture.RawData[c] = map.GetPixel(x, y);
                                        }
                                        else
                                        {
                                            //PTCs[ptc].Texture.RawData[c] = Color.Black;
                                        }
                                        c++;
                                    }
                                }
                                col++;
                                if (col == 4)
                                {
                                    col = 0;
                                    row++;
                                }
                                PTCs[ptc].Texture.UpdateImageData();
                                ptc++;
                            }
                        }
                    }
                }
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        if (path.EndsWith("psf"))
                        {
                            psf.Save(writer);
                        }
                        else if (path.EndsWith("ptc"))
                        {
                            if (PTCs.Count > 0)
                            {
                                ptcfile.Texture = PTCs[0].Texture;
                            }
                            ptcfile.Save(writer);
                        }
                        else if (path.EndsWith("psm"))
                        {
                            psm.Save(writer);
                        }
                    }
                }
            }
        }

        

    }
}
