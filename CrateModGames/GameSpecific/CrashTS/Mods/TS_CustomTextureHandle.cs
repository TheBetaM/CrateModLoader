using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_CustomTextureHandle : ModStruct<GenericModStruct>
    {
        public override void PreloadPass(GenericModStruct mod)
        {
            string basePath = mod.ExtractedPath;
            if (mod.Console == ConsoleMode.PS2)
            {
                basePath = Path.Combine(mod.ExtractedPath, @"CRASH6\CRASH\");
            }

            string langMod = "American";
            if (mod.Region == RegionType.PAL)
            {
                langMod = "English";
            }
            else if (mod.Region == RegionType.NTSC_J)
            {
                langMod = "Japanese";
            }

            Twins_Common.LoadTexture(basePath + @"Language\GameOver\Crash.psm", ref Twins_Data_Textures.Texture_GameOver_Crash);
            Twins_Common.LoadTexture(basePath + @"Language\GameOver\Cortex.psm", ref Twins_Data_Textures.Texture_GameOver_Cortex);
            Twins_Common.LoadTexture(basePath + @"Language\GameOver\Mecha.psm", ref Twins_Data_Textures.Texture_GameOver_Mecha);
            Twins_Common.LoadTexture(basePath + @"Language\GameOver\Nina.psm", ref Twins_Data_Textures.Texture_GameOver_Nina);
            Twins_Common.LoadTexture(basePath + @"Language\GameOver\CrashAndCortex.psm", ref Twins_Data_Textures.Texture_GameOver_CrashAndCortex);

            Twins_Common.LoadTexture(basePath + @"Language\Credits\CreditNew.psm", ref Twins_Data_Textures.Texture_Credits);

            Twins_Common.LoadTexture(basePath + @"Language\Legal\" + langMod + ".psm", ref Twins_Data_Textures.Texture_Legal);

            Twins_Common.LoadTexture(basePath + @"Language\Loading\Loading1.psm", ref Twins_Data_Textures.Texture_Loading_01);
            Twins_Common.LoadTexture(basePath + @"Language\Loading\Loading2.psm", ref Twins_Data_Textures.Texture_Loading_02);
            Twins_Common.LoadTexture(basePath + @"Language\Loading\Loading3.psm", ref Twins_Data_Textures.Texture_Loading_03);

            Twins_Common.LoadTexture(basePath + @"Startup\Icons.psm", ref Twins_Data_Textures.Texture_Icons);

            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Crash.psm", ref Twins_Data_Textures.Texture_Titles_Crash);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub01.psm", ref Twins_Data_Textures.Texture_Titles_Hub01);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub02.psm", ref Twins_Data_Textures.Texture_Titles_Hub02);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub03.psm", ref Twins_Data_Textures.Texture_Titles_Hub03);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Hub04.psm", ref Twins_Data_Textures.Texture_Titles_Hub04);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level01.psm", ref Twins_Data_Textures.Texture_Titles_Level01);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level02.psm", ref Twins_Data_Textures.Texture_Titles_Level02);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level03.psm", ref Twins_Data_Textures.Texture_Titles_Level03);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level04.psm", ref Twins_Data_Textures.Texture_Titles_Level04);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level05.psm", ref Twins_Data_Textures.Texture_Titles_Level05);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level06.psm", ref Twins_Data_Textures.Texture_Titles_Level06);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level07.psm", ref Twins_Data_Textures.Texture_Titles_Level07);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level08.psm", ref Twins_Data_Textures.Texture_Titles_Level08);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level09.psm", ref Twins_Data_Textures.Texture_Titles_Level09);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level10.psm", ref Twins_Data_Textures.Texture_Titles_Level10);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level11.psm", ref Twins_Data_Textures.Texture_Titles_Level11);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level12.psm", ref Twins_Data_Textures.Texture_Titles_Level12);
            Twins_Common.LoadTexture(basePath + @"Language\Titles\" + langMod + @"\Level13.psm", ref Twins_Data_Textures.Texture_Titles_Level13);

            Twins_Common.LoadTexture(basePath + @"Startup\Decal.ptc", ref Twins_Data_Textures.Texture_Decals);

            Twins_Common.LoadTexture(basePath + @"Startup\Fonts\Arial.psf", ref Twins_Data_Textures.Texture_Font_Arial);

            if (mod.Region == RegionType.PAL)
            {
                Twins_Common.LoadTexture(basePath + @"Startup\Fonts\Crash_Euro.psf", ref Twins_Data_Textures.Texture_Font_CrashPAL);
            }
            else if (mod.Region == RegionType.NTSC_J)
            {
                Twins_Common.LoadTexture(basePath + @"Startup\Fonts\Crash_Jpn.psf", ref Twins_Data_Textures.Texture_Font_CrashJPN);
            }
            else
            {
                Twins_Common.LoadTexture(basePath + @"Startup\Fonts\Crash.psf", ref Twins_Data_Textures.Texture_Font_Crash);
            }

            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss01.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss01);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss02.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss02);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss03.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss03);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss04.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss04);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss05.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss05);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss06.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss06);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss07.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss07);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss08.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss08);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss09.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss09);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss10.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss10);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss11.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss11);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss12.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss12);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss13.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss13);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss14.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss14);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss15.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss15);
            Twins_Common.LoadTexture(basePath + @"Extras\Bosses\Boss16.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss16);

            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept01.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept01);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept02.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept02);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept03.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept03);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept04.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept04);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept05.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept05);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept06.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept06);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept07.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept07);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept08.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept08);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept09.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept09);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept10.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept10);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept11.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept11);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept12.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept12);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept13.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept13);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept14.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept14);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept15.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept15);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept16.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept16);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept17.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept17);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept18.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept18);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept19.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept19);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept20.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept20);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept21.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept21);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept22.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept22);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept23.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept23);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept24.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept24);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept25.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept25);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept26.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept26);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept27.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept27);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept28.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept28);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept29.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept29);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept30.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept30);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept31.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept31);
            Twins_Common.LoadTexture(basePath + @"Extras\Concept\Concept32.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept32);

            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy01.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy01);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy02.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy02);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy03.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy03);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy04.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy04);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy05.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy05);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy06.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy06);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy07.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy07);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy08.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy08);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy09.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy09);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy10.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy10);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy11.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy11);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy12.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy12);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy13.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy13);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy14.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy14);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy15.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy15);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy16.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy16);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy17.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy17);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy18.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy18);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy19.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy19);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy20.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy20);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy21.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy21);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy22.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy22);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy23.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy23);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy24.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy24);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy25.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy25);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy26.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy26);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy27.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy27);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy28.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy28);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy29.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy29);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy30.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy30);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy31.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy31);
            Twins_Common.LoadTexture(basePath + @"Extras\Enemies\Enemy32.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy32);

            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen01.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen01);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen02.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen02);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen03.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen03);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen04.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen04);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen05.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen05);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen06.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen06);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen07.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen07);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen08.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen08);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen09.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen09);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen10.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen10);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen11.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen11);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen12.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen12);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen13.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen13);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen14.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen14);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen15.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen15);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen16.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen16);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen17.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen17);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen18.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen18);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen19.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen19);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen20.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen20);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen21.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen21);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen22.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen22);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen23.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen23);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen24.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen24);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen25.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen25);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen26.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen26);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen27.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen27);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen28.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen28);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen29.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen29);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen30.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen30);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen31.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen31);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen32.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen32);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen33.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen33);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen34.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen34);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen35.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen35);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen36.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen36);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen37.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen37);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen38.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen38);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen39.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen39);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen40.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen40);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen41.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen41);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen42.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen42);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen43.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen43);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen44.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen44);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen45.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen45);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen46.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen46);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen47.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen47);
            Twins_Common.LoadTexture(basePath + @"Extras\Unseen\Unseen48.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen48);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity11);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle08);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern11);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern12);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern13);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern14);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem11);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem12);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem13);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem14);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\04-Totem\Totem15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem15);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub11);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub12);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub13);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub14);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub15);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb11);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb12);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb13);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb14);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb15);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb16.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb16);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb17.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb17);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip01.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip02.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip03.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip04.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip05.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip06.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip07.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip08.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\07-Slip\Slip09.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip09);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas10);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas11);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas12);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas13);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas14);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\09-Academy\Academy08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy08);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler07);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom09);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop07);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity05);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide06);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit06);

            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony01);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony02);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony03);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony04);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony05);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony06);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony07);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony08);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony09);
            Twins_Common.LoadTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony10);
        }

        public override void ModPass(GenericModStruct mod)
        {
            string basePath = mod.ExtractedPath;
            if (mod.Console == ConsoleMode.PS2)
            {
                basePath = Path.Combine(mod.ExtractedPath, @"CRASH6\CRASH\");
            }

            string langMod = "American";
            if (mod.Region == RegionType.PAL)
            {
                langMod = "English";
            }
            else if (mod.Region == RegionType.NTSC_J)
            {
                langMod = "Japanese";
            }

            Twins_Common.SaveTexture(basePath + @"Language\GameOver\Crash.psm", ref Twins_Data_Textures.Texture_GameOver_Crash);
            Twins_Common.SaveTexture(basePath + @"Language\GameOver\Cortex.psm", ref Twins_Data_Textures.Texture_GameOver_Cortex);
            Twins_Common.SaveTexture(basePath + @"Language\GameOver\Mecha.psm", ref Twins_Data_Textures.Texture_GameOver_Mecha);
            Twins_Common.SaveTexture(basePath + @"Language\GameOver\Nina.psm", ref Twins_Data_Textures.Texture_GameOver_Nina);
            Twins_Common.SaveTexture(basePath + @"Language\GameOver\CrashAndCortex.psm", ref Twins_Data_Textures.Texture_GameOver_CrashAndCortex);

            Twins_Common.SaveTexture(basePath + @"Language\Credits\CreditNew.psm", ref Twins_Data_Textures.Texture_Credits);

            Twins_Common.SaveTexture(basePath + @"Language\Legal\" + langMod + ".psm", ref Twins_Data_Textures.Texture_Legal);

            Twins_Common.SaveTexture(basePath + @"Language\Loading\Loading1.psm", ref Twins_Data_Textures.Texture_Loading_01);
            Twins_Common.SaveTexture(basePath + @"Language\Loading\Loading2.psm", ref Twins_Data_Textures.Texture_Loading_02);
            Twins_Common.SaveTexture(basePath + @"Language\Loading\Loading3.psm", ref Twins_Data_Textures.Texture_Loading_03);

            Twins_Common.SaveTexture(basePath + @"Startup\Icons.psm", ref Twins_Data_Textures.Texture_Icons);

            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Crash.psm", ref Twins_Data_Textures.Texture_Titles_Crash);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub01.psm", ref Twins_Data_Textures.Texture_Titles_Hub01);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub02.psm", ref Twins_Data_Textures.Texture_Titles_Hub02);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub03.psm", ref Twins_Data_Textures.Texture_Titles_Hub03);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Hub04.psm", ref Twins_Data_Textures.Texture_Titles_Hub04);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level01.psm", ref Twins_Data_Textures.Texture_Titles_Level01);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level02.psm", ref Twins_Data_Textures.Texture_Titles_Level02);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level03.psm", ref Twins_Data_Textures.Texture_Titles_Level03);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level04.psm", ref Twins_Data_Textures.Texture_Titles_Level04);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level05.psm", ref Twins_Data_Textures.Texture_Titles_Level05);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level06.psm", ref Twins_Data_Textures.Texture_Titles_Level06);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level07.psm", ref Twins_Data_Textures.Texture_Titles_Level07);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level08.psm", ref Twins_Data_Textures.Texture_Titles_Level08);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level09.psm", ref Twins_Data_Textures.Texture_Titles_Level09);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level10.psm", ref Twins_Data_Textures.Texture_Titles_Level10);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level11.psm", ref Twins_Data_Textures.Texture_Titles_Level11);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level12.psm", ref Twins_Data_Textures.Texture_Titles_Level12);
            Twins_Common.SaveTexture(basePath + @"Language\Titles\" + langMod + @"\Level13.psm", ref Twins_Data_Textures.Texture_Titles_Level13);

            Twins_Common.SaveTexture(basePath + @"Startup\Fonts\Arial.psf", ref Twins_Data_Textures.Texture_Font_Arial);

            if (mod.Region == RegionType.PAL)
            {
                Twins_Common.SaveTexture(basePath + @"Startup\Fonts\Crash_Euro.psf", ref Twins_Data_Textures.Texture_Font_CrashPAL);
            }
            else if (mod.Region == RegionType.NTSC_J)
            {
                Twins_Common.SaveTexture(basePath + @"Startup\Fonts\Crash_Jpn.psf", ref Twins_Data_Textures.Texture_Font_CrashJPN);
            }
            else
            {
                Twins_Common.SaveTexture(basePath + @"Startup\Fonts\Crash.psf", ref Twins_Data_Textures.Texture_Font_Crash);
            }

            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss01.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss01);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss02.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss02);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss03.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss03);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss04.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss04);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss05.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss05);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss06.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss06);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss07.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss07);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss08.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss08);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss09.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss09);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss10.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss10);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss11.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss11);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss12.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss12);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss13.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss13);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss14.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss14);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss15.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss15);
            Twins_Common.SaveTexture(basePath + @"Extras\Bosses\Boss16.psm", ref Twins_Data_Textures_Galleries.Gallery_Boss16);

            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept01.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept01);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept02.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept02);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept03.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept03);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept04.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept04);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept05.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept05);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept06.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept06);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept07.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept07);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept08.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept08);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept09.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept09);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept10.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept10);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept11.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept11);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept12.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept12);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept13.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept13);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept14.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept14);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept15.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept15);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept16.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept16);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept17.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept17);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept18.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept18);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept19.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept19);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept20.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept20);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept21.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept21);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept22.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept22);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept23.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept23);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept24.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept24);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept25.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept25);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept26.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept26);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept27.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept27);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept28.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept28);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept29.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept29);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept30.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept30);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept31.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept31);
            Twins_Common.SaveTexture(basePath + @"Extras\Concept\Concept32.psm", ref Twins_Data_Textures_Galleries.Gallery_Concept32);

            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy01.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy01);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy02.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy02);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy03.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy03);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy04.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy04);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy05.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy05);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy06.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy06);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy07.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy07);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy08.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy08);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy09.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy09);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy10.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy10);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy11.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy11);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy12.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy12);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy13.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy13);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy14.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy14);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy15.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy15);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy16.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy16);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy17.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy17);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy18.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy18);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy19.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy19);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy20.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy20);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy21.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy21);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy22.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy22);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy23.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy23);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy24.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy24);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy25.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy25);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy26.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy26);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy27.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy27);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy28.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy28);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy29.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy29);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy30.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy30);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy31.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy31);
            Twins_Common.SaveTexture(basePath + @"Extras\Enemies\Enemy32.psm", ref Twins_Data_Textures_Galleries.Gallery_Enemy32);

            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen01.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen01);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen02.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen02);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen03.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen03);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen04.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen04);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen05.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen05);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen06.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen06);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen07.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen07);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen08.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen08);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen09.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen09);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen10.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen10);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen11.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen11);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen12.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen12);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen13.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen13);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen14.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen14);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen15.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen15);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen16.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen16);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen17.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen17);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen18.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen18);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen19.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen19);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen20.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen20);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen21.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen21);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen22.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen22);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen23.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen23);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen24.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen24);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen25.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen25);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen26.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen26);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen27.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen27);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen28.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen28);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen29.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen29);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen30.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen30);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen31.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen31);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen32.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen32);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen33.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen33);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen34.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen34);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen35.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen35);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen36.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen36);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen37.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen37);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen38.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen38);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen39.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen39);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen40.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen40);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen41.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen41);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen42.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen42);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen43.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen43);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen44.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen44);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen45.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen45);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen46.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen46);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen47.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen47);
            Twins_Common.SaveTexture(basePath + @"Extras\Unseen\Unseen48.psm", ref Twins_Data_Textures_Galleries.Gallery_Unseen48);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\01-NSanity\NSanity11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryNSanity11);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\02-Jungle\Jungle08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryJungle08);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern11);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern12);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern13);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\03-Cavern\Cavern14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryCavern14);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem11);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem12);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem13);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem14);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\04-Totem\Totem15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTotem15);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub11);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub12);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub13);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub14);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\05-IceHub\IceHub15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceHub15);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb11);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb12);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb13);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb14);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb15.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb15);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb16.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb16);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\06-IceClimb\IceClimb17.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryIceClimb17);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip01.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip02.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip03.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip04.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip05.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip06.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip07.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip08.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\07-Slip\Slip09.psm", ref Twins_Data_Textures_Galleries.Gallery_StorySlip09);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas10);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas11.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas11);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas12.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas12);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas13.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas13);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\08-HighSeas\HighSeas14.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryHighSeas14);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\09-Academy\Academy08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAcademy08);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\10-Boiler\Boiler07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryBoiler07);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\11-Classroom\Classroom09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryClassroom09);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\12-Rooftop\Rooftop07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRooftop07);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\13-Twinsanity\Twinsanity05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryTwinsanity05);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\14-Rockslide\Rockslide06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryRockslide06);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\15-Pursuit\Pursuit06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryPursuit06);

            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony01.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony01);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony02.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony02);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony03.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony03);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony04.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony04);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony05.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony05);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony06.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony06);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony07.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony07);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony08.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony08);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony09.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony09);
            Twins_Common.SaveTexture(basePath + @"Extras\Storyboards\16-AntAgony\AntAgony10.psm", ref Twins_Data_Textures_Galleries.Gallery_StoryAntAgony10);
        }
    }
}
