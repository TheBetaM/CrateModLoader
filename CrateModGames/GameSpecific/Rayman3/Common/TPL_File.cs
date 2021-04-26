using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.Rayman3
{
    public class TPL_File
    {
        public string Name;
        public string FullName;
        public string FolderName;
        public List<FileInfo> Textures;

        public TPL_File(string N, string F, string Folder)
        {
            Name = N;
            FullName = F;
            FolderName = Folder + @"\";
            Textures = new List<FileInfo>();
        }

    }
}
