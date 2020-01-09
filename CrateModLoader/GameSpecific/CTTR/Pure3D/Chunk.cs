using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
// Pure3D API by handsomematt (https://github.com/handsomematt/Pure3D) with modifications by BetaM

namespace Pure3D
{
    public abstract class Chunk
    {
        public uint Type;
        public List<Chunk> Children;
        public File File;
        public Chunk Parent;
        public long chunkStart;
        public uint headerSize;
        public uint chunkSize;
        public long chunkEnd;

        public bool IsRoot
        {
            get
            {
                return this == File.RootChunk;
            }
        }

        public Chunk(File file, uint type)
        {
            Children = new List<Chunk>();
            Type = type;
            File = file;
        }

        public T[] GetChildren<T>() where T : Chunk
        {
            return Children.FindAll(delegate (Chunk c) { return c is T; }).Cast<T>().ToArray();
        }

        public T[] GetChildrenByName<T>(string name) where T : Chunks.Named
        {
            return Children.FindAll(delegate (Chunk c) { return c is T && ((Chunks.Named)c).Name == name; }).Cast<T>().ToArray();
        }

        public T GetChildByName<T>(string name) where T : Chunks.Named
        {
            return (T)Children.Find(delegate (Chunk c) { return c is T && ((Chunks.Named)c).Name == name; });
        }

        public int GetChildIndexByName<T>(string name) where T : Chunks.Named
        {
            if (GetChildByName<T>(name) != null)
            {
                Chunk targetChunk = GetChildByName<T>(name);
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i] == targetChunk)
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }

        public void ReadChildren(Stream stream)
        {
            // todo: probably move all this logic to a seperate method.
            while (stream.Position < chunkEnd)
            {
                uint type = new BinaryReader(stream).ReadUInt32();
                Chunk chunk = NewChunkFromType(File, type);

                // sort hierarchy
                chunk.Parent = this;
                Children.Add(chunk);

                chunk.Read(stream, true, chunkEnd);
            }
        }

        public void WriteChildren(Stream stream)
        {
            if (Children.Count > 0)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    new BinaryWriter(stream).Write(Children[i].Type);

                    Children[i].Write(stream, true, chunkEnd);
                }
            }
        }

        public void Read(Stream stream, bool readChildren, long parentChunkEnd)
        {
            BinaryReader reader = new BinaryReader(stream);
            chunkStart = stream.Position - 4;
            headerSize = reader.ReadUInt32();
            chunkSize = reader.ReadUInt32();
            Console.WriteLine($"Header size: {headerSize}, chunk size {chunkSize}.");

            if (headerSize > chunkSize)
                throw new Exception($"Header size {headerSize} greater then chunk size {chunkSize}.");

            if (!readChildren)
                headerSize = chunkSize;

            if ((stream.Position + chunkSize - 12) > parentChunkEnd)
                throw new Exception("Chunk size too high.");

            chunkEnd = chunkStart + chunkSize;

            ReadHeader(stream, headerSize - 12);

            Console.WriteLine(this.ToString());

            if (readChildren)
                ReadChildren(stream);

            if (stream.Position != chunkEnd)
                throw new Exception($"Stream position expected {chunkEnd} but is {stream.Position}");
        }

        public abstract void ReadHeader(Stream stream, long length);

        public void Write(Stream stream, bool writeChildren, long parentChunkEnd)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            long headerPos = writer.BaseStream.Position;
            writer.Write(headerSize);
            long chunkSizePos = writer.BaseStream.Position;
            writer.Write(chunkSize);

            WriteHeader(stream);

            headerSize = (uint)(writer.BaseStream.Length - (headerPos - 4));
            long tempPos = writer.BaseStream.Position;
            writer.BaseStream.Position = headerPos;
            writer.Write(headerSize);
            writer.BaseStream.Position = tempPos;

            if (writeChildren)
            {
                WriteChildren(stream);
            }

            chunkSize = (uint)(writer.BaseStream.Length - (headerPos - 4));
            tempPos = writer.BaseStream.Position;
            writer.BaseStream.Position = chunkSizePos;
            writer.Write(chunkSize);
            writer.BaseStream.Position = tempPos;
            //Console.WriteLine($"Header size: {headerSize}, chunk size {chunkSize}.");

        }

        public abstract void WriteHeader(Stream stream);

        protected static Dictionary<uint, Type> chunkTypeDictionary = null;
        public static Chunk NewChunkFromType(File file, uint type)
        {
            // cache the list
            if (chunkTypeDictionary == null)
            {
                chunkTypeDictionary = new Dictionary<uint, Type>();

                foreach (var chunk in ChunkType.GetSupported())
                {
                    ChunkType chunkAttr = (ChunkType)chunk.GetCustomAttribute(typeof(ChunkType), false);
                    chunkTypeDictionary[chunkAttr.TypeID] = chunk;
                }
            }

            if (!chunkTypeDictionary.ContainsKey(type))
                return new Chunks.Unknown(file, type);

            Type chunkType = chunkTypeDictionary[type];
            return (Chunk)Activator.CreateInstance(chunkType, new object[] { file, type });
            
        }
    }
}
