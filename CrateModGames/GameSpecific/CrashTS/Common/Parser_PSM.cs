using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTS
{
    public class Parser_PSM : ModParser<PSM_Texture>
    {
        private Dictionary<string, ModProp_TextureFile> TexDict;

        public Parser_PSM(Modder mod, ConsoleMode cons, string ExtPath, bool Preload = false) : base(mod)
        {
            ConsoleMode console = cons;
            string bdPath = Twins_Common.GetDataPath(cons, ExtPath);
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
                    TexDict.Add(bdPath + Tex.AssetPath, Tex);
                    if (!Preload)
                    {
                        ForceParser = true;
                    }
                }
            }
        }

        public override List<string> Extensions => new List<string>() { ".PSM" };

        public override PSM_Texture LoadObject(string filePath)
        {
            PSM_Texture tex = new PSM_Texture();
            bool load = tex.LoadTexture(filePath);

            if (TexDict.ContainsKey(filePath))
            {
                if (!TexDict[filePath].Value)
                {
                    TexDict[filePath].Resource = tex.Image;
                    TexDict[filePath].Value = true;
                }
                else
                {
                    tex.Image = TexDict[filePath].Resource;
                }
            }

            return tex;
        }

        public override void SaveObject(PSM_Texture tex, string filePath)
        {
            tex.UpdateTexture(filePath);
        }
    }
}
