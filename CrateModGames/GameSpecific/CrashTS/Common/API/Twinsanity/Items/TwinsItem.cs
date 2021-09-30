using System.IO;

namespace Twinsanity
{
    /// <summary>
    /// Represents a generic item in the chunk tree
    /// </summary>
    public class TwinsItem
    {
        protected virtual int GetSize()
        {
            if (Data == null) return -1;
            else return Data.Length;
        }

        public virtual void Save(BinaryWriter writer)
        {
            writer.Write(Data);
        }

        public virtual void Load(BinaryReader reader, int size)
        {
            Data = reader.ReadBytes(size);
        }

        public byte[] Data { get; set; }
        public uint ID { get; set; }
        public int Size { get => GetSize(); }
        public TwinsSection Parent { get; set; }
        public SectionType ParentType { get; set; }
    }
}
