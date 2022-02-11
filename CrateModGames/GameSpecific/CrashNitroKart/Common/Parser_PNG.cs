using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class PNG_File
    {
        public string Path;
        public Bitmap Image;
    }

    public class Parser_PNG : ModParser<PNG_File>
    {
        private Dictionary<string, ModProp_TextureFile> TexDict;

        public Parser_PNG(Modder mod, ConsoleMode cons, string ExtPath, bool Preload = false) : base(mod)
        {
            ConsoleMode console = cons;
            string path = CNK_Common.GetDataPath(cons, ExtPath);
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

        public override List<string> Extensions => new List<string>() { ".PNG" };

        public override PNG_File LoadObject(string filePath)
        {
            Bitmap temp = new Bitmap(filePath);
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

            PNG_File file = new PNG_File();
            file.Path = filePath;
            file.Image = tex;

            return file;
        }

        public override void SaveObject(PNG_File tex, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            tex.Image.Save(filePath);
        }
    }
}
