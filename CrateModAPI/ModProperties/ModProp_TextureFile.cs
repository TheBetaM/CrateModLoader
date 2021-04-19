using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace CrateModLoader.ModProperties
{
    public class ModProp_TextureFile : ModPropExternalResource<Bitmap>
    {

        public ModProp_TextureFile(bool b) : base(b)
        {
            BrowseFilter = "Image Files (*.png;*.bmp)|*.png;*.bmp|";
            PreloadBonus = true;
            AssetExtension = ".png";
        }
        public ModProp_TextureFile(bool b, string name, string desc) : base(b, name, desc)
        {
            BrowseFilter = "Image Files (*.png;*.bmp)|*.png;*.bmp|";
            PreloadBonus = true;
            AssetExtension = ".png";
        }

        public override bool TryResource(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                Bitmap temp = new Bitmap(path);

                if (temp != null)
                {
                    Bitmap img = new Bitmap(temp);
                    Resource = img;
                    temp.Dispose();
                    return true;
                }
                temp.Dispose();
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
