using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.Items
{
    public class TwinsPTC
    {
        public uint TexID;
        public uint MatID;
        public Texture Texture;
        public Material Material;

        public void Load(BinaryReader reader, int size)
        {
            TexID = reader.ReadUInt32();
            MatID = reader.ReadUInt32();
            Texture = new Texture();
            Texture.Load(reader, 0);
            Material = new Material();
            Material.Load(reader, 0);
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(TexID);
            writer.Write(MatID);
            Texture.Save(writer);
            Material.Save(writer);
        }
    }
}
