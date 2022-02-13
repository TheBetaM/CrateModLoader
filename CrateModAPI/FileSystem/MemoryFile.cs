using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace CrateModLoader
{
    public class MemoryFile : IDisposable
    {
        public MemoryStream Stream = null;
        public string FullName = string.Empty;

        public bool Loaded => Stream != null;

        public static MemoryFile FromStream(Stream InStream, string Name)
        {
            MemoryFile file = new MemoryFile();
            file.FullName = Name;
            file.Stream = new MemoryStream((int)InStream.Length);
            InStream.CopyTo(file.Stream);
            return file;
        }
        public void FromStreamSync(Stream InStream, string Name)
        {
            FullName = Name;
            Stream = new MemoryStream((int)InStream.Length);
            InStream.CopyTo(Stream);
        }
        public async Task FromStreamAsync(Stream InStream, string Name)
        {
            FullName = Name;
            Stream = new MemoryStream((int)InStream.Length);
            await InStream.CopyToAsync(Stream);
        }

        public void Dispose()
        {
            Stream.Dispose();
            Stream.Close();
            Stream = null;
            FullName = string.Empty;
        }
    }
}
