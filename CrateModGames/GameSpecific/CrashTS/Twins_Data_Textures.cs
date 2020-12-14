using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrateModLoader;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTS;
using CrateModGames.GameSpecific.Rayman3;
using Twinsanity;
using Twinsanity.Items;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Textures)]
    public static class Twins_Data_Textures
    {

        public static ModProp_TextureFile Texture_Icons = new ModProp_TextureFile(false, "Icons", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

        public static ModProp_TextureFile Texture_Titles_Crash = new ModProp_TextureFile(false, "Game Logo", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Hub01 = new ModProp_TextureFile(false, "Logo - N.Sanity Island", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Hub02 = new ModProp_TextureFile(false, "Logo - Iceberg Lab", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Hub03 = new ModProp_TextureFile(false, "Logo - Academy Of Evil", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Hub04 = new ModProp_TextureFile(false, "Logo - Twinsanity Island", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level01 = new ModProp_TextureFile(false, "Logo - Jungle Bungle", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level02 = new ModProp_TextureFile(false, "Logo - Cavern Catastrophe", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level03 = new ModProp_TextureFile(false, "Logo - Totem Hokum", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level04 = new ModProp_TextureFile(false, "Logo - Ice Climb", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level05 = new ModProp_TextureFile(false, "Logo - Slip Slide Icecapades", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level06 = new ModProp_TextureFile(false, "Logo - Hi Seas Hijinks", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level07 = new ModProp_TextureFile(false, "Logo - Gone A Bit Coco", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level08 = new ModProp_TextureFile(false, "Logo - Boiler Room Doom", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level09 = new ModProp_TextureFile(false, "Logo - Classroom Chaos", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level10 = new ModProp_TextureFile(false, "Logo - Rooftop Rampage", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level11 = new ModProp_TextureFile(false, "Logo - Rockslide Rumble", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level12 = new ModProp_TextureFile(false, "Logo - Bandicoot Pursuit", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Titles_Level13 = new ModProp_TextureFile(false, "Logo - Ant Agony", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

        public static ModProp_TextureFile Texture_Loading_01 = new ModProp_TextureFile(false, "Loading - 01", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Loading_02 = new ModProp_TextureFile(false, "Loading - 02", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_Loading_03 = new ModProp_TextureFile(false, "Loading - 03", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

        public static ModProp_TextureFile Texture_Credits = new ModProp_TextureFile(false, "Credits", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

        public static ModProp_TextureFile Texture_Legal = new ModProp_TextureFile(false, "Legal Display", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

        public static ModProp_TextureFile Texture_GameOver_Crash = new ModProp_TextureFile(false, "Game Over - Crash", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_GameOver_Cortex = new ModProp_TextureFile(false, "Game Over - Cortex", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_GameOver_Nina = new ModProp_TextureFile(false, "Game Over - Nina", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_GameOver_Mecha = new ModProp_TextureFile(false, "Game Over - Mecha", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };
        public static ModProp_TextureFile Texture_GameOver_CrashAndCortex = new ModProp_TextureFile(false, "Game Over - Crash And Cortex", "")
        { AllowedConsoles = new List<ConsoleMode>() { ConsoleMode.PS2 }, };

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
        }

        public static void LoadTexture(string path, ref ModProp_TextureFile target)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    if (path.EndsWith("psf"))
                    {
                        TwinsPSF psf = new TwinsPSF();
                        psf.Load(reader, (int)fileStream.Length);
                    }
                    else if (path.EndsWith("ptc"))
                    {
                        TwinsPTC ptc = new TwinsPTC();
                        ptc.Load(reader, (int)fileStream.Length);
                    }
                    else if (path.EndsWith("psm"))
                    {
                        TwinsPSM psm = new TwinsPSM();
                        psm.Load(reader, (int)fileStream.Length);
                        int ogWidth = psm.PTCs[0].Texture.Width;
                        int ogHeight = psm.PTCs[0].Texture.Height;
                        int maxWidth = psm.PTCs[0].Texture.Width;
                        int maxHeight = psm.PTCs[0].Texture.Height;

                        if (psm.PTCs.Count > 1)
                        {
                            maxWidth += ogWidth;
                            if (psm.PTCs.Count > 2)
                            {
                                maxWidth += ogWidth;
                            }
                            if (psm.PTCs.Count > 3)
                            {
                                maxWidth += ogWidth;
                            }
                        }
                        int rows = (psm.PTCs.Count / 4);
                        if (rows == 0)
                            rows = 1;

                        maxHeight = maxHeight * rows;

                        Bitmap map = new Bitmap(maxWidth, maxHeight);

                        int ptc = 0;
                        int col = 0;
                        int row = 0;
                        while (ptc < psm.PTCs.Count)
                        {
                            int c = 0;
                            for (int y = row * ogHeight; y < ogHeight + (row * ogHeight); y++)
                            {
                                for (int x = col * ogWidth; x < ogWidth + (col * ogWidth); x++)
                                {
                                    if (c < psm.PTCs[ptc].Texture.RawData.Length)
                                    {
                                        map.SetPixel(x, y, psm.PTCs[ptc].Texture.RawData[c]);
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
                TwinsPSM psm = new TwinsPSM();
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        if (path.EndsWith("psf"))
                        {
                            TwinsPSF psf = new TwinsPSF();
                            psf.Load(reader, (int)fileStream.Length);
                        }
                        else if (path.EndsWith("ptc"))
                        {
                            TwinsPTC ptc = new TwinsPTC();
                            ptc.Load(reader, (int)fileStream.Length);
                        }
                        else if (path.EndsWith("psm"))
                        {
                            psm = new TwinsPSM();
                            psm.Load(reader, (int)fileStream.Length);
                            int ogWidth = psm.PTCs[0].Texture.Width;
                            int ogHeight = psm.PTCs[0].Texture.Height;
                            int maxWidth = psm.PTCs[0].Texture.Width;
                            int maxHeight = psm.PTCs[0].Texture.Height;

                            if (psm.PTCs.Count > 1)
                            {
                                maxWidth += ogWidth;
                                if (psm.PTCs.Count > 2)
                                {
                                    maxWidth += ogWidth;
                                }
                                if (psm.PTCs.Count > 3)
                                {
                                    maxWidth += ogWidth;
                                }
                            }
                            int rows = (psm.PTCs.Count / 4);
                            if (rows == 0)
                                rows = 1;

                            maxHeight = maxHeight * rows;

                            Bitmap map = target.Resource;

                            int ptc = 0;
                            int col = 0;
                            int row = 0;
                            while (ptc < psm.PTCs.Count)
                            {
                                int c = 0;
                                for (int y = row * ogHeight; y < ogHeight + (row * ogHeight); y++)
                                {
                                    for (int x = col * ogWidth; x < ogWidth + (col * ogWidth); x++)
                                    {
                                        if (c < psm.PTCs[ptc].Texture.RawData.Length)
                                        {
                                            psm.PTCs[ptc].Texture.RawData[c] = map.GetPixel(x, y);
                                        }
                                        else
                                        {
                                            //psm.PTCs[ptc].Texture.RawData[c] = Color.Black;
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
                                psm.PTCs[ptc].Texture.UpdateImageData();
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

                        }
                        else if (path.EndsWith("ptc"))
                        {

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
