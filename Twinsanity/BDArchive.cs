using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Twinsanity
{
    /// <summary>
    /// BD/BH archive class
    /// </summary>
    public sealed class BDArchive
    {
        public static void ExtractAll(string in_name, string out_dir)
        {
            string bdname = in_name, bhname = in_name;
            if (in_name.EndsWith(".BH"))
            {
                bdname = in_name.Substring(0, in_name.Length-1) + "D";
            }
            else if (in_name.EndsWith(".bh"))
            {
                bdname = in_name.Substring(0, in_name.Length-1) + "d";
            }
            else if (in_name.EndsWith(".BD"))
            {
                bhname = in_name.Substring(0, in_name.Length-1) + "H";
            }
            else if (in_name.EndsWith(".bd"))
            {
                bhname = in_name.Substring(0, in_name.Length-1) + "h";
            }
            else
            {
                bdname += ".BD";
                bhname += ".BH";
            }
            
            if (!File.Exists(bhname))
                throw new ArgumentException("BH file could not be found.");
            if (!File.Exists(bdname))
                throw new ArgumentException("BD file could not be found.");

            using (BinaryReader hr = new BinaryReader(new FileStream(bhname, FileMode.Open)))
            using (BinaryReader dr = new BinaryReader(new FileStream(bdname, FileMode.Open)))
            {
                int magic = hr.ReadInt32();
                while (hr.BaseStream.Position < hr.BaseStream.Length)
                {
                    int namelen = hr.ReadInt32();
                    string name = Encoding.ASCII.GetString(hr.ReadBytes(namelen));
                    uint offset = hr.ReadUInt32();
                    uint size = hr.ReadUInt32();

                    if (name.Contains(System.IO.Path.DirectorySeparatorChar.ToString()))
                    {
                        string dirs = name.Substring(0, name.LastIndexOf(System.IO.Path.DirectorySeparatorChar));

                        Directory.CreateDirectory(System.IO.Path.Combine(out_dir, dirs));
                    }

                    using (BinaryWriter file = new BinaryWriter(new FileStream(System.IO.Path.Combine(out_dir, name), FileMode.Create, FileAccess.Write)))
                    {
                        dr.BaseStream.Position = offset;
                        while (size > 0) // the files can be HUGE so we read/write in 2MB chunks
                        {
                            uint readsize = Math.Min(size, 2 * 1024 * 1024);
                            file.Write(dr.ReadBytes((int)readsize));
                            size -= readsize;
                        }
                    }
                }
            }
        }

        public static void CompileAll(string in_name, string out_dir)
        {
            string bdname = in_name, bhname = in_name;
            if (in_name.EndsWith(".BH"))
            {
                bdname = in_name.Substring(0, in_name.Length - 1) + "D";
            }
            else if (in_name.EndsWith(".bh"))
            {
                bdname = in_name.Substring(0, in_name.Length - 1) + "d";
            }
            else if (in_name.EndsWith(".BD"))
            {
                bhname = in_name.Substring(0, in_name.Length - 1) + "H";
            }
            else if (in_name.EndsWith(".bd"))
            {
                bhname = in_name.Substring(0, in_name.Length - 1) + "h";
            }
            else
            {
                bdname += ".BD";
                bhname += ".BH";
            }

            using (BinaryWriter hw = new BinaryWriter(new FileStream(bhname, FileMode.Create, FileAccess.Write)))
            using (BinaryWriter dw = new BinaryWriter(new FileStream(bdname, FileMode.Create, FileAccess.Write)))
            {
                string filename = string.Empty;
                DirectoryInfo di = new DirectoryInfo(out_dir);
                Dictionary<BinaryReader,string> filemap = new Dictionary<BinaryReader,string>();

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    LockDirectory(System.IO.Path.Combine(filename, dir.Name), dir, filemap);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    LockFile(System.IO.Path.Combine(filename, file.Name), file, filemap);
                }

                hw.Write(0x501);
                foreach (var kvp in filemap)
                {
                    if (kvp.Key.BaseStream.Length > uint.MaxValue)
                        throw new IOException($"File {kvp.Value} is too large");
                    byte[] name = Encoding.ASCII.GetBytes(kvp.Value);
                    hw.Write(name.Length);
                    hw.Write(name);
                    hw.Write((uint)dw.BaseStream.Position);
                    uint size = (uint)kvp.Key.BaseStream.Length;
                    hw.Write(size);
                    while (size > 0) // write in 2MB chunks at a time
                    {
                        uint readsize = Math.Min(size, 2 * 1024 * 1024);
                        dw.Write(kvp.Key.ReadBytes((int)readsize));
                        size -= readsize;
                    }
                    kvp.Key.Close();
                }
            }
        }

        internal static void LockDirectory(string dname, DirectoryInfo di, Dictionary<BinaryReader,string> filemap)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                LockDirectory(System.IO.Path.Combine(dname, dir.Name), dir, filemap);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                LockFile(System.IO.Path.Combine(dname, file.Name), file, filemap);
            }
        }

        internal static void LockFile(string filename, FileInfo file, Dictionary<BinaryReader,string> filemap)
        {
            filemap.Add(new BinaryReader(file.Open(FileMode.Open, FileAccess.Read)), filename);
        }
    }
}
