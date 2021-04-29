using System.Collections.Generic;
using System.IO;
using System;

namespace CrateModLoader.GameSpecific.Rayman3
{
    // Common Gamecube texture format
    public class Parser_TPL : ModParser<TPL_File>
    {
        public Parser_TPL(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".TPL" };
        public override List<string> SecondaryList => new List<string>()
        {
            "menu.tpl",
            "fix.tpl",
            "lodmeca.tpl",
            "lodps2_01.tpl",
            "lodps2_02.tpl",
            "lodps2_03.tpl",
            "lodps2_04.tpl",
            "lodps2_05.tpl",
            "lodps2_06.tpl",
            "lodps2_07.tpl",
            "lodps2_08.tpl",
        };
        public override bool SecondarySkip => false;

        public override TPL_File LoadObject(string filePath)
        {
            Ray3_Common.GCN_ExportTextures(filePath);

            File.Delete(filePath);
            string name = Path.GetFileName(filePath);
            string dir = filePath.Substring(0, filePath.Length - name.Length);
            DirectoryInfo di = new DirectoryInfo(dir);

            TPL_File tpl = new TPL_File(name, filePath, dir);

            foreach (FileInfo ext in di.EnumerateFiles())
            {
                if (ext.Extension.ToLower() == ".png" && ext.Name.Contains(name))
                {
                    tpl.Textures.Add(ext);
                }
            }

            Ray3_Common.Custom_Texture_Handle(tpl);

            return tpl;
        }

        public override void SaveObject(TPL_File thing, string filePath)
        {
            Ray3_Common.GCN_ImportTextures(filePath + @".png");

            foreach (FileInfo ext in thing.Textures)
            {
                File.Delete(ext.FullName);
            }
        }
    }
}
