using System;
using System.Collections.Generic;
using System.IO;
using RadcoreCementFile;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_File_WMP
    {

        // Wumpa positions file, max 256 elements
        public TWOC_File_WMP()
        {
            Wumpas = new List<TWOC_Vector3>();
        }
        public TWOC_File_WMP(string path, bool isGC)
        {
            Wumpas = new List<TWOC_Vector3>();
            if (!isGC)
            {
                Load(path);
            }
            else
            {
                LoadGC(path);
            }
        }
        public List<TWOC_Vector3> Wumpas;

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader reader = new BinaryReader(fileStream);
                uint Count = reader.ReadUInt32();
                for (uint i = 0; i < Count; i++)
                {
                    Wumpas.Add(new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                }
            }
        }
        public void LoadGC(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader2 reader = new BinaryReader2(fileStream);
                uint Count = reader.ReadUInt32();
                for (uint i = 0; i < Count; i++)
                {
                    Wumpas.Add(new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                }
            }
        }

        public void Save(string path)
        {
            if (Wumpas.Count > 256)
            {
                while (Wumpas.Count > 256)
                {
                    Wumpas.RemoveAt(255);
                }
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(Wumpas.Count);
                for (int i = 0; i < Wumpas.Count; i++)
                {
                    writer.Write(Wumpas[i].X);
                    writer.Write(Wumpas[i].Y);
                    writer.Write(Wumpas[i].Z);
                }
            }
        }
        public void SaveGC(string path)
        {
            if (Wumpas.Count > 256)
            {
                while (Wumpas.Count > 256)
                {
                    Wumpas.RemoveAt(255);
                }
            }
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryWriter2 writer = new BinaryWriter2(fileStream);
                writer.Write(Wumpas.Count);
                for (int i = 0; i < Wumpas.Count; i++)
                {
                    writer.Write(Wumpas[i].X);
                    writer.Write(Wumpas[i].Y);
                    writer.Write(Wumpas[i].Z);
                }
            }
        }

    }
}