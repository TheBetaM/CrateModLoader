﻿using System.Collections.Generic;
using System.IO;
using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.Rayman3
{
    // Common Gamecube texture format
    public class Parser_TPL : ModParser<TPL_File>
    {
        private Dictionary<string, ModProp_TextureFile> TexDict;

        public Parser_TPL(Modder mod, string ExtPath, bool Preload = false) : base(mod)
        {
            string path = ExtPath; //Ray3_Common.GetDataPath(cons, ExtPath);
            TexDict = new Dictionary<string, ModProp_TextureFile>();
            LoadOnly = Preload;
            if (Preload)
            {
                ForceParser = true;
            }

            foreach (ModPropertyBase Prop in mod.ActiveProps)
            {
                if (Prop is ModProp_TextureFile Tex && Tex.AssetPath != string.Empty && Path.GetExtension(Tex.AssetPath).ToLower() == ".png")
                {
                    TexDict.Add(path + Tex.AssetPath, Tex);
                    if (!Preload)
                    {
                        ForceParser = true;
                    }
                }
            }
        }

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

                    if (TexDict.ContainsKey(ext.FullName))
                    {
                        if (!TexDict[ext.FullName].Value)
                        {
                            TexDict[ext.FullName].FileToResource(ext.FullName);
                        }
                        else
                        {
                            TexDict[ext.FullName].ResourceToFile(ext.FullName);
                        }
                    }

                }
            }

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
