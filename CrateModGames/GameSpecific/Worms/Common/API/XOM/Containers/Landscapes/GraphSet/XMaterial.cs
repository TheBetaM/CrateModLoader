using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public enum ColorSources
    {
        Material = 0,
        Color1,
        Color2,
    }

    //[XOM_TypeName("XMaterial")]
    public class XMaterial : Container
    {
        public Color Diffuse = new Color();
        public Color Ambient = new Color();
        public Color Specular = new Color();
        public Color Emissive = new Color();
        private uint diffuseSource;
        private uint ambientSource;
        private uint specularSource;
        private uint emissiveSource;
        public float Power;

        public ColorSources DiffuseSource
        {
            get { return (ColorSources)diffuseSource; }
            set { diffuseSource = (uint)value; }
        }
        public ColorSources AmbientSource
        {
            get { return (ColorSources)ambientSource; }
            set { ambientSource = (uint)value; }
        }
        public ColorSources SpecularSource
        {
            get { return (ColorSources)specularSource; }
            set { specularSource = (uint)value; }
        }
        public ColorSources EmissiveSource
        {
            get { return (ColorSources)emissiveSource; }
            set { emissiveSource = (uint)value; }
        }

        public override void Read(BinaryReader reader)
        {
            Diffuse.Read(reader);
            Ambient.Read(reader);
            Specular.Read(reader);
            Emissive.Read(reader);
            diffuseSource = reader.ReadUInt32();
            ambientSource = reader.ReadUInt32();
            specularSource = reader.ReadUInt32();
            emissiveSource = reader.ReadUInt32();
            Power = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer)
        {
            Diffuse.Write(writer);
            Ambient.Write(writer);
            Specular.Write(writer);
            Emissive.Write(writer);
            writer.Write(diffuseSource);
            writer.Write(ambientSource);
            writer.Write(specularSource);
            writer.Write(emissiveSource);
            writer.Write(Power);
        }
    }
}
