using System;
using System.IO;

namespace Twinsanity
{
    /// <summary>
    /// Represents an Unknown Item in the chunk tree
    /// </summary>
    public class BaseItem : BaseObject
    {
        /// <summary>
        /// Name of the node in the chunk tree
        /// </summary>
        public string NodeName = "Unknown Item";

        /// <summary>
        /// ByteStream used to read the information about the object
        /// </summary>
        public System.IO.MemoryStream ByteStream = new System.IO.MemoryStream();

        /// <summary>
        /// Load the item into the RAM
        /// </summary>
        /// <param name="File">File to load from</param>
        /// <param name="Reader">Reader to read from the file</param>
        public override void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader)
        {
            File.Position = Offset + Base;
            System.IO.BinaryWriter BSWriter = new System.IO.BinaryWriter(ByteStream);
            ByteStream.Position = 0;
            BSWriter.Write(Reader.ReadBytes((int)Size), 0, (int)Size);
            DataUpdate();
        }

        /// <summary>
        /// Recalculate the size
        /// </summary>
        /// <returns>New size</returns>
        public override uint Recalculate()
        {
            UpdateStream();
            return (uint)ByteStream.Length;
        }
        
        /// <summary>
        /// Update the memory stream of the object
        /// </summary>
        public override void UpdateStream()
        {
            System.IO.MemoryStream NewStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter NSWriter = new System.IO.BinaryWriter(NewStream);
            NSWriter.Write(ByteStream.ToArray());
            ByteStream = NewStream;
            Size = (uint)ByteStream.Length;
        }

        /// <summary>
        /// Get the current memory stream
        /// </summary>
        /// <param name="indexes">NO NEED RIGHT NOW</param>
        /// <param name="pos">NO NEED RIGHT NOW</param>
        /// <returns>Current stream in use by the object</returns>
        public override System.IO.MemoryStream Get_Stream(int pos, params int[] indexes)
        {
            return ByteStream;
        }

        /// <summary>
        /// Use new stream for the object
        /// </summary>
        /// <param name="indexes">NO NEED RIGHT NOW</param>
        /// <param name="pos">NO NEED RIGHT NOW</param>
        /// <param name="It">New stream to use</param>
        public override void Put_Stream(System.IO.MemoryStream It, int pos, params int[] indexes)
        {
            ByteStream = It;
            DataUpdate();
        }

        /// <summary>
        /// Save into a file stream
        /// </summary>
        /// <param name="File">Stream to save at</param>
        /// <param name="Writer">Writer to write with</param>
        public override void Save(ref System.IO.FileStream File, ref System.IO.BinaryWriter Writer)
        {
            File.Position = Offset + Base;
            Writer.Write(ByteStream.ToArray());
        }

        /// <summary>
        /// Should be implemented by the children.
        /// Update the object with newly written data.
        /// </summary>
        protected virtual void DataUpdate()
        {
            System.IO.BinaryReader BSReader = new System.IO.BinaryReader(ByteStream);
            ByteStream.Position = 0;
        }

        public override BaseObject Get_Item(int pos, params int[] indexes)
        {
            return null;
        }

        public override void Put_Item(BaseObject It, int pos, params int[] indexes)
        {
            throw new NotImplementedException();
        }

        public override void Add_Item(int pos, params int[] indexes)
        {
            throw new NotImplementedException();
        }

        public override void Delete_Item(int pos, params int[] indexes)
        {
            throw new NotImplementedException();
        }

        protected override void Load<T>(ref FileStream File, ref BinaryReader Reader)
        {
            throw new NotImplementedException();
        }
    }
}
