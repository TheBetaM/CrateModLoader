using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using System.IO;
using System.Drawing;

namespace CrateModGames.GameSpecific.Rayman3
{
    public class ModProp_TextureFile : ModPropExternalResource<Bitmap>
    {

        public ModProp_TextureFile(bool b) : base(b)
        {
            BrowseFilter = "Image Files (*.png;*.bmp)|*.png;*.bmp|";
            PreloadBonus = true;
        }
        public ModProp_TextureFile(bool b, string name, string desc) : base(b, name, desc)
        {
            BrowseFilter = "Image Files (*.png;*.bmp)|*.png;*.bmp|";
            PreloadBonus = true;
        }

        public override bool TryResource(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                Bitmap img = new Bitmap(path);

                if (img != null)
                {
                    Resource = img;
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public override void ResourceToFile(string outputPath)
        {
            if (Loaded)
            {
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                Resource.Save(outputPath);
            }
        }

        public override void FileToResource(string inputPath)
        {
            try
            {
                Bitmap temp = new Bitmap(inputPath);
                Resource = new Bitmap(temp);
                temp.Dispose();
                Value = true;
            }
            catch
            {
                
            }
        }

    }
}
