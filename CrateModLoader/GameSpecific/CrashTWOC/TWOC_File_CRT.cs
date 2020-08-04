using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    class TWOC_File_CRT
    {

        public TWOC_File_CRT()
        {
            Crates = new List<TWOC_Crate>();

        }

        string Path;
        List<TWOC_Crate> Crates;
        int CrateCount => Crates.Count;
        int Header; // usually 4

        class TWOC_Crate
        {
            int Type = 0;
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader reader = new BinaryReader(fileStream);

            }
        }

        public void Save(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(4);
            }
        }

    }
}
