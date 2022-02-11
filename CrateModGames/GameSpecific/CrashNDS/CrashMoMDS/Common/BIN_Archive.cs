using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CrateModLoader.GameSpecific.CrashNDS;

// Crash MoM DS BIN archives, there are no filenames
namespace CrateModLoader.GameSpecific.CrashMoMDS
{
    public class BIN_Archive
    {
        private Modder meta;
        public string FileName;
        public string ExtDir;
        public string FileNameFull;
        public List<FileEntry> Files = new List<FileEntry>();

        public List<string> KnownExtensions = new List<string>()
        {
            "RGCN",
            "RLCN",
            "RCSN",
            "PFTN",
            "TFTN",
            "SFTN",
            "RNAN",
            "RECN",
            "CRAN",
            "BMD0", //model
            "BTX0", //tex
            "BCA0", //anim
            "BTP0", //tex pattern anim
            "BTA0", //uv anim
            "BMA0", //material anim
            "BVA0", //visibility anim
            "BSD0", //???
            "MASK",
            "MAS0", //may be cbb specific?
            "RCNN",
            "RAMN",
            "BTS2",
        };
        public List<string> ExtRev = new List<string>()
        {
            "NCGR",
            "NCLR",
            "NSCR",
            "NTFP",
            "NTFT",
            "NTFS",
            "NANR",
            "NCER",
            "NARC",
            "nsbmd",
            "nsbtx",
            "nsbca",
            "nsbtp",
            "nsbta",
            "nsbma",
            "nsbva",
            "nsbsd",
            "MASK",
            "nsmas",
            "NNCR",
            "NMAR",
            "BTS2D",
        };

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
                        Read(reader, memoryStream);
                    }
                }
            }
        }
        
        public void Write(Modder mod, string fileName)
        {
            meta = mod;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    Write(writer, fileStream);
                }
            }
        }

        public void Read(BinaryReader reader, Stream arcStream)
        {
            uint Offset = reader.ReadUInt32();
            uint Size = reader.ReadUInt32();
            uint FirstOffset = Offset;
            while (Offset != 0 && Size != 0 && reader.BaseStream.Position < FirstOffset)
            {
                FileEntry entry = new FileEntry();
                entry.Offset = Offset;
                entry.Size = Size;
                Files.Add(entry);
                meta.PassCount++;

                if (reader.BaseStream.Position < FirstOffset)
                {
                    Offset = reader.ReadUInt32();
                    Size = reader.ReadUInt32();
                }
            }

            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].Size != 0)
                {
                    reader.BaseStream.Position = Files[i].Offset;
                    //Files[i].Data = reader.ReadBytes((int)Files[i].Size);

                    string entryName = string.Format("{0}{1}{2}{3:D3}", ExtDir, FileName, "_", i);
                    string signature = new string(System.Text.Encoding.ASCII.GetChars(reader.ReadBytes(4)));
                    reader.BaseStream.Position -= 4;
                    if (FileNameFull.Contains("roominfo.bin")) // it has only roominfo
                    {
                        entryName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", "room"); // not the actual extension, just for ModStruct convenience
                    }
                    else if (FileNameFull.Contains("ptk.bin")) // it has only ptk
                    {
                        entryName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", "ptk");
                    }
                    else if (FileNameFull.Contains("texts_c")) // it has only mxb
                    {
                        entryName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", "mxb");
                    }
                    else if (KnownExtensions.Contains(signature))
                    {
                        int sig = KnownExtensions.IndexOf(signature);
                        entryName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", ExtRev[sig]);
                    }
                    else
                    {
                        byte LZtest = reader.ReadByte();
                        reader.BaseStream.Position--;
                        if (LZtest == 0x10)
                        {
                            Files[i].LZ10 = true;
                        }
                    }

                    using (FileStream fileStream = new FileStream(entryName, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        if (Files[i].LZ10)
                        {
                            NDS_LZ10_Compression.Decompress(arcStream, Files[i].Size, fileStream);
                        }
                        else
                        {
                            using (BinaryWriter writer = new BinaryWriter(fileStream))
                            {
                                writer.Write(reader.ReadBytes((int)Files[i].Size));
                            }
                        }
                    }

                    // checks for extension of decompressed file
                    if (Files[i].LZ10)
                    {
                        bool hasExt = false;
                        string NewName = "";
                        using (FileStream compFile = new FileStream(entryName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (BinaryReader creader = new BinaryReader(compFile))
                            {
                                string sign = new string(System.Text.Encoding.ASCII.GetChars(creader.ReadBytes(4)));
                                if (KnownExtensions.Contains(sign))
                                {
                                    int sig = KnownExtensions.IndexOf(sign);
                                    NewName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", ExtRev[sig]);
                                    hasExt = true;
                                }
                            }
                        }
                        if (hasExt)
                        {
                            File.Move(entryName, NewName);
                        }
                    }

                }
                meta.PassIterator++;
            }
        }

        public void Write(BinaryWriter writer, Stream arcStream)
        {
            meta.PassCount += Files.Count;
            DirectoryInfo dir = new DirectoryInfo(ExtDir);

            // offsets are calculated later due to compression
            int offsetCount = Files.Count * 8;
            int firstOffset = AlignInt(offsetCount, 0x40);
            for (int i = 0; i < Files.Count; i++)
            {
                writer.Write(0x00);
                writer.Write(0x00);
            }
            while (writer.BaseStream.Position < firstOffset)
            {
                writer.Write((uint)0);
            }

            int id = 0;
            foreach (FileInfo fileinfo in dir.EnumerateFiles())
            {
                Files[id].Offset = (uint)writer.BaseStream.Position;
                using (FileStream fileStream = fileinfo.OpenRead())
                {
                    if (Files[id].LZ10)
                    {
                        NDS_LZ10_Compression.Compress(fileStream, fileStream.Length, arcStream);
                    }
                    else
                    {
                        using (BinaryReader reader = new BinaryReader(fileStream))
                        {
                            writer.Write(reader.ReadBytes((int)fileinfo.Length));
                        }
                    }
                }
                Files[id].Size = (uint)(writer.BaseStream.Position - Files[id].Offset);
                fileinfo.Delete();

                //padding 0xFF bytes until 0x40 alignment
                if (id < Files.Count - 1)
                {
                    int align = AlignInt((int)writer.BaseStream.Position, 0x40);
                    while (writer.BaseStream.Position < align)
                    {
                        writer.Write((byte)0xFF);
                    }
                }
                meta.PassIterator++;
                id++;
            }

            //padding 0xFF bytes until 0x200 alignment at end of file
            if (writer.BaseStream.Position % 0x200 != 0)
            {
                int target = AlignInt((int)writer.BaseStream.Position, 0x200);
                while (writer.BaseStream.Position < target)
                {
                    writer.Write((byte)0xFF);
                }
            }

            /*
            int offset = AlignInt(offsetCount, 0x40);
            id = 0;
            foreach (FileInfo fileinfo in dir.EnumerateFiles())
            {
                Files[id].Offset = (uint)offset;
                Files[id].Size = (uint)fileinfo.Length; // this size is incorrect for compressed files, so it needs to be recalculated later
                offset += (int)fileinfo.Length;
                offset = AlignInt(offset, 0x40);
                id++;
            }
            */

            // back to the start now that offsets/sizes are correct
            writer.BaseStream.Position = 0;
            for (int i = 0; i < Files.Count; i++)
            {
                writer.Write(Files[i].Offset);
                writer.Write(Files[i].Size);
            }

            Directory.Delete(ExtDir);
        }

        int AlignInt(int num, int mul)
        {
            int rest = num % mul;
            if (rest == 0) return num;
            return num + mul - rest;
        }

        public class FileEntry
        {
            public uint Offset;
            public uint Size;
            public byte[] Data;
            public bool LZ10; // is LZ10 compressed
        }
    }
}
