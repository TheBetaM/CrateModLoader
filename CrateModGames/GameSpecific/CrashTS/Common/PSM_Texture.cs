using System;
using System.Collections.Generic;
using Twinsanity;
using Twinsanity.Items;
using System.IO;
using CrateModLoader.ModProperties;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class PSM_Texture
    {
        private string Path = "";
        public Bitmap Image = null;

        public bool LoadTexture(string path)
        {
            Bitmap image = null;
            if (File.Exists(path))
            {
                Path = path;
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
                        image = map;
                    }

                }
            }
            if (image == null)
            {
                return false;
            }
            Image = image;
            return true;
        }

        public void UpdateTexture(string path = "")
        {
            if (path == "")
            {
                path = Path;
            }
            if (File.Exists(path))
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

                            Bitmap map = Image;

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
