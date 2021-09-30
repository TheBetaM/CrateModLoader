using System.IO;
using System;
using System.Collections.Generic;

namespace Twinsanity
{
    public class MaterialDemo : TwinsItem
    {
        public string Name { get; set; }
        public ulong Header { get; set; }
        public int Unknown { get; set; }
        public List<TwinsShader> Shaders = new List<TwinsShader>();

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(Unknown);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(Shaders.Count);
            foreach (var shd in Shaders)
            {
                shd.Write(writer);
            }
        }

        public override void Load(BinaryReader reader, int size)
        {
            Header = reader.ReadUInt64();
            Unknown = reader.ReadInt32();
            var nameLen = reader.ReadInt32();
            Name = new string(reader.ReadChars(nameLen));
            var shdCnt = reader.ReadInt32();
            Shaders.Clear();
            for (var i = 0; i < shdCnt; ++i)
            {
                TwinsShader shd = new TwinsShader();
                shd.Read(reader, 0, true);
                Shaders.Add(shd);
            }
        }

        protected override int GetSize()
        {
            var shdLen = 0;
            foreach (var shd in Shaders)
            {
                shdLen += shd.GetLength();
            }
            return 20 + Name.Length + shdLen;
        }
    }
}
