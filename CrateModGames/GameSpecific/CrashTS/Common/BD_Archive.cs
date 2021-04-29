using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTS
{
    // Based on API code
    public class BD_Archive
    {

        private Modder meta;
        Dictionary<uint, string> Files;
        Dictionary<uint, uint> Sizes;
        Dictionary<string, string> OutFiles;
        Dictionary<string, uint> OutSizes;

        public async Task ExtractAsync(Modder mod, string in_name, string out_dir)
        {
            meta = mod;
            string bdname = in_name;
            string bhname = Path.ChangeExtension(in_name, ".BH");
            if (!File.Exists(bhname))
                throw new ArgumentException("BH file could not be found.");
            if (!File.Exists(bdname))
                throw new ArgumentException("BD file could not be found.");

            Files = new Dictionary<uint, string>();
            Sizes = new Dictionary<uint, uint>();

            using (BinaryReader hr = new BinaryReader(new FileStream(bhname, FileMode.Open)))
            {
                int magic = hr.ReadInt32();
                while (hr.BaseStream.Position < hr.BaseStream.Length)
                {
                    int namelen = hr.ReadInt32();
                    string name = Encoding.ASCII.GetString(hr.ReadBytes(namelen));
                    uint offset = hr.ReadUInt32();
                    uint size = hr.ReadUInt32();
                    if (name.Contains(Path.DirectorySeparatorChar.ToString()))
                    {
                        string dirs = name.Substring(0, name.LastIndexOf(Path.DirectorySeparatorChar));
                        Directory.CreateDirectory(Path.Combine(out_dir, dirs));
                    }
                    Files.Add(offset, Path.Combine(out_dir, name));
                    Sizes.Add(offset, size);
                    meta.PassCount++;
                }
            }

            IList<Task> editTaskList = new List<Task>();

            foreach (KeyValuePair<uint, string> file in Files)
            {
                editTaskList.Add(ExtractFileAsync(bdname, file.Value, file.Key));
            }

            await Task.WhenAll(editTaskList);
            editTaskList.Clear();

        }

        private async Task ExtractFileAsync(string bdname, string Path, uint offset)
        {
            FileStream BD = new FileStream(bdname, FileMode.Open, FileAccess.Read, FileShare.Read);
            BD.Seek(offset, SeekOrigin.Begin);

            Stream file = File.Open(Path, FileMode.Create, FileAccess.Write);
            uint size = Sizes[offset];
            byte[] Data = new byte[size];
            try
            {
                await BD.ReadAsync(Data, 0, (int)size);
            }
            catch
            {
                Console.WriteLine("Read Error: " + Path);
            }
            try
            {
                await file.WriteAsync(Data, 0, (int)size);
            }
            catch
            {
                Console.WriteLine("Write Error: " + Path);
            }

            file.Close();
            BD.Close();

            meta.PassIterator++;
            //meta.PassPercent = (int)((meta.PassIterator / (float)meta.PassCount) * 20f) + 5;
        }

        public async Task CompileAsync(Modder mod, string in_name, string out_dir)
        {
            meta = mod;
            string bdname = in_name;
            string bhname = Path.ChangeExtension(in_name, ".BH");
            OutFiles = new Dictionary<string, string>();
            OutSizes = new Dictionary<string, uint>();

            string filename = string.Empty;
            DirectoryInfo di = new DirectoryInfo(out_dir);
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                LockDirectory(Path.Combine(filename, dir.Name), dir);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                LockFile(Path.Combine(filename, file.Name), file);
            }

            Dictionary<string, uint> Offsets = new Dictionary<string, uint>();
            uint off = 0;
            foreach (KeyValuePair<string, uint> size in OutSizes)
            {
                Offsets.Add(size.Key, off);
                off += size.Value;
            }

            using (BinaryWriter hw = new BinaryWriter(new FileStream(bhname, FileMode.Create, FileAccess.Write)))
            {
                hw.Write(0x501);
                foreach (KeyValuePair<string, string> path in OutFiles)
                {
                    byte[] name = Encoding.ASCII.GetBytes(path.Value);
                    hw.Write(name.Length);
                    hw.Write(name);
                    hw.Write((uint)Offsets[path.Key]);
                    hw.Write(OutSizes[path.Key]);
                    meta.PassCount++;
                }
            }
            FileStream BD = new FileStream(bdname, FileMode.Create, FileAccess.Write, FileShare.Write);
            BD.Close();

            IList<Task> editTaskList = new List<Task>();

            foreach (KeyValuePair<string, string> path in OutFiles)
            {
                editTaskList.Add(CompileFileAsync(bdname, path.Key, Offsets[path.Key], OutSizes[path.Key]));
            }

            await Task.WhenAll(editTaskList);
            editTaskList.Clear();
        }

        private async Task CompileFileAsync(string bdname, string Path, uint offset, uint size)
        {
            FileStream BD = new FileStream(bdname, FileMode.Open, FileAccess.Write, FileShare.Write);
            BD.Seek(offset, SeekOrigin.Begin);

            Stream file = File.Open(Path, FileMode.Open, FileAccess.Read);
            //uint size = (uint)file.Length;
            byte[] Data = new byte[size];
            try
            {
                await file.ReadAsync(Data, 0, (int)size);
            }
            catch
            {
                Console.WriteLine("Read Error: " + Path);
            }
            try
            {
                await BD.WriteAsync(Data, 0, (int)size);
            }
            catch
            {
                Console.WriteLine("Write Error: " + Path);
            }

            file.Close();
            BD.Close();

            meta.PassIterator++;
            //meta.PassPercent = (int)((meta.PassIterator / (float)meta.PassCount) * 20f) + 5;
        }

        internal void LockDirectory(string dname, DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                LockDirectory(Path.Combine(dname, dir.Name), dir);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                LockFile(Path.Combine(dname, file.Name), file);
            }
        }

        internal void LockFile(string filename, FileInfo file)
        {
            OutFiles.Add(file.FullName, filename);
            OutSizes.Add(file.FullName, (uint)file.Length);
            //filemap.Add(new BinaryReader(file.Open(FileMode.Open, FileAccess.Read)), filename);
        }
    }
}