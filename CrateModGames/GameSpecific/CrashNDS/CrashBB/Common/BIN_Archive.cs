using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CrateModLoader.GameSpecific.CrashNDS;

// Crash Boom Bang BIN archives, there are no filenames, empty file entries and nested archives are valid
namespace CrateModLoader.GameSpecific.CrashBB
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
            uint FileCount = reader.ReadUInt32();
            for (int i = 0; i < FileCount; i++)
            {
                FileEntry entry = new FileEntry();
                entry.Offset = reader.ReadUInt32();
                entry.Size = reader.ReadUInt32();
                Files.Add(entry);
                meta.PassCount++;
            }
            for (int i = 0; i < FileCount; i++)
            {
                if (Files[i].Size != 0)
                {
                    reader.BaseStream.Position = Files[i].Offset;
                    //Files[i].Data = reader.ReadBytes((int)Files[i].Size);

                    string entryName = string.Format("{0}{1}{2}{3:D3}", ExtDir, FileName, "_", i);
                    string signature = new string(System.Text.Encoding.ASCII.GetChars(reader.ReadBytes(4)));
                    reader.BaseStream.Position -= 4;
                    if (KnownExtensions.Contains(signature))
                    {
                        int sig = KnownExtensions.IndexOf(signature);
                        entryName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i,".", ExtRev[sig]);
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
                                if (FileNameFull.Contains("data.bin")) // it has only language files
                                {
                                    NewName = string.Format("{0}{1}{2}{3:D3}{4}{5}", ExtDir, FileName, "_", i, ".", "lng"); // not the actual extension, just for ModStruct convenience
                                    hasExt = true;
                                }
                                else if (KnownExtensions.Contains(sign))
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
            writer.Write(Files.Count);
            for (int i = 0; i < Files.Count; i++)
            {
                writer.Write((uint)0);
                writer.Write((uint)0);
            }

            int id = 0;
            foreach (FileInfo fileinfo in dir.EnumerateFiles())
            {
                while (Files[id].Size == 0)
                {
                    id++;
                }
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
                meta.PassIterator++;
            }

            writer.BaseStream.Position = 4;
            for (int i = 0; i < Files.Count; i++)
            {
                writer.Write(Files[i].Offset);
                writer.Write(Files[i].Size);
            }

            //Directory.Delete(ExtDir); //Some bins have the same name as existing folders
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
