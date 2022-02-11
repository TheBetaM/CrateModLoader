using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CrateModLoader.GameSpecific.CrashNDS;

// Crash Titans DS ROM.BIN archive, filenames conveniently left in a file inside the archive
namespace CrateModLoader.GameSpecific.CrashTitansDS
{
    public class BIN_Rom_Archive
    {
        private Modder meta;
        public string FileName;
        public string ExtDir;
        public string FileNameFull;
        public List<FileEntry> Files = new List<FileEntry>();

        public void Read(Modder mod, string fileName)
        {
            meta = mod;
            FileNameFull = fileName;
            FileName = Path.GetFileNameWithoutExtension(fileName).TrimEnd('.');
            string DirName = Path.GetFileNameWithoutExtension(fileName);
            ExtDir = Path.ChangeExtension(fileName, "").TrimEnd('.') + @"\";
            Directory.CreateDirectory(ExtDir);

            using (var br = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                byte[] buffer = new byte[br.Length];
                br.Read(buffer, 0, buffer.Length);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(memoryStream))
                    {
                        Read(reader);
                    }
                }
            }
        }
        public void Write(Modder mod, string fileName)
        {
            meta = mod;
            meta.PassCount = Files.Count;
            meta.PassIterator = 0;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    Write(writer);
                }
            }
        }

        public void Read(BinaryReader reader)
        {
            int Offset = reader.ReadInt32();
            int FirstOffset = Offset;
            int Size = 0;
            while (reader.BaseStream.Position < FirstOffset)
            {
                FileEntry entry = new FileEntry();
                entry.Offset = Offset;
                Files.Add(entry);
                meta.PassCount++;

                if (reader.BaseStream.Position < FirstOffset) // last offset is end of file
                {
                    Offset = reader.ReadInt32();
                }
            }

            //Get the names from RomFiles.txt!
            for (int i = 0; i < Files.Count; i++)
            {
                reader.BaseStream.Position = Files[4049].Offset + (0x40 * i);
                Files[i].Name = new string(reader.ReadChars(0x39)).TrimEnd(' ');
            }

            for (int i = 0; i < Files.Count; i++)
            {
                reader.BaseStream.Position = Files[i].Offset;
                //Files[i].Data = reader.ReadBytes((int)Files[i].Size);
                if (i != Files.Count - 1)
                {
                    Size = Files[i + 1].Offset - Files[i].Offset;
                }
                else
                {
                    Size = (int)(reader.BaseStream.Length - Files[i].Offset);
                }

                string entryName = string.Format("{0}{1}", ExtDir, Files[i].Name);

                using (FileStream fileStream = new FileStream(entryName, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        writer.Write(reader.ReadBytes(Size));
                    }
                }
                meta.PassIterator++;
            }
        }

        public void Write(BinaryWriter writer)
        {
            DirectoryInfo dir = new DirectoryInfo(ExtDir);
            int offset = (Files.Count * 4) + 4;
            int id = 0;
            foreach (FileInfo fileinfo in dir.EnumerateFiles())
            {
                Files[id].Offset = offset;
                offset += (int)fileinfo.Length;
                id++;
            }

            for (int i = 0; i < Files.Count; i++)
            {
                writer.Write(Files[i].Offset);
            }
            writer.Write(offset); // last offset is end of file

            foreach (FileInfo fileinfo in dir.EnumerateFiles())
            {
                using (FileStream fileStream = fileinfo.OpenRead())
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        writer.Write(reader.ReadBytes((int)fileinfo.Length));
                    }
                }
                fileinfo.Delete();
                meta.PassIterator++;
            }

            Directory.Delete(ExtDir);
        }

        public class FileEntry
        {
            public int Offset;
            public string Name;
            public byte[] Data;
            public bool LZ10; // is LZ10 compressed
        }
    }
}
