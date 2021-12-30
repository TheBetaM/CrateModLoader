using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;
using System.Drawing;
using TGASharpLib;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class TGA_File
    {
        public string Path;
        public Bitmap Image;
    }

    public class Parser_TGA : ModParser<TGA_File>
    {
        private Dictionary<string, ModProp_TextureFile> TexDict;

        public Parser_TGA(Modder mod, bool Preload = false) : base(mod)
        {
            string path = mod.ConsolePipeline.ExtractedPath + @"DATA\";
            TexDict = new Dictionary<string, ModProp_TextureFile>();
            LoadOnly = Preload;
            if (Preload)
            {
                ForceParser = true;
            }

            foreach (ModPropertyBase Prop in mod.ActiveProps)
            {
                if (Prop is ModProp_TextureFile Tex && Tex.AssetPath != string.Empty && Extensions.Contains(Path.GetExtension(Tex.AssetPath).ToUpper()))
                {
                    TexDict.Add(path + Tex.AssetPath, Tex);
                    if (!Preload)
                    {
                        ForceParser = true;
                    }
                }
            }
        }

        public override List<string> Extensions => new List<string>() { ".TGA" };

        public override TGA_File LoadObject(string filePath)
        {
            TGA tar = TGA.FromFile(filePath);

            Bitmap temp = tar.ToBitmap();
            Bitmap tex = new Bitmap(temp);
            temp.Dispose();

            if (TexDict.ContainsKey(filePath))
            {
                if (!TexDict[filePath].Value)
                {
                    TexDict[filePath].Resource = tex;
                    TexDict[filePath].Value = true;
                }
                else
                {
                    tex = TexDict[filePath].Resource;
                }
            }

            TGA_File file = new TGA_File();
            file.Path = filePath;
            file.Image = tex;

            return file;
        }

        public override void SaveObject(TGA_File tex, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            TGA tar = TGA.FromBitmap(tex.Image);
            tar.Save(filePath);
        }
    }
}
