using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_File_AI
    {

        // AI objects, max 96
        public TWOC_File_AI()
        {
            AI = new List<AIObject>();
        }
        public TWOC_File_AI(string path, bool isGC)
        {
            AI = new List<AIObject>();
            if (!isGC)
            {
                Load(path);
            }
            else
            {

            }
        }
        public List<AIObject> AI;

        public class AIObject
        {
            public string Name;
            public List<TWOC_Vector3> Pos;
        }

        public void Load(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryReader reader = new BinaryReader(fileStream);
                uint Count = reader.ReadUInt32();
                for (uint i = 0; i < Count; i++)
                {
                    AIObject Object = new AIObject();
                    string tempName = new string(reader.ReadChars(0x10));
                    Object.Name = tempName.Trim('\0');

                    uint VectorCount = reader.ReadUInt32();
                    Object.Pos = new List<TWOC_Vector3>();
                    for (int v = 0; v < VectorCount; v++)
                    {
                        Object.Pos.Add(new TWOC_Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                    }

                    AI.Add(Object);
                }
            }
        }

        public void Save(string path)
        {
            if (AI.Count > 96)
            {
                while (AI.Count > 96)
                {
                    AI.RemoveAt(95);
                }
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(AI.Count);
                for (int i = 0; i < AI.Count; i++)
                {
                    if (AI[i].Name.Length > 16)
                    {
                        AI[i].Name = AI[i].Name.Substring(0, 16);
                    }
                    else if (AI[i].Name.Length < 16)
                    {
                        while (AI[i].Name.Length < 16)
                        {
                            AI[i].Name += '\0';
                        }
                    }
                    char[] tempName2 = AI[i].Name.ToCharArray();
                    writer.Write(tempName2);

                    writer.Write(AI[i].Pos.Count);

                    for (int v = 0; v < AI[i].Pos.Count; v++)
                    {
                        writer.Write(AI[i].Pos[v].X);
                        writer.Write(AI[i].Pos[v].Y);
                        writer.Write(AI[i].Pos[v].Z);
                    }
                }
            }
        }

    }
}