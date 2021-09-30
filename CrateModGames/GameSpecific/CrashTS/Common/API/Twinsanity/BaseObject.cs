namespace Twinsanity
{
    /// <summary>
    /// Interface of the base object
    /// </summary>
    public abstract class BaseObject
    {
        /// <summary>
        /// ID of the object
        /// </summary>
        public uint ID;

        /// <summary>
        /// Size of the object in bytes in the chunk
        /// </summary>
        public uint Size;

        /// <summary>
        /// Offset in bytes from the start of chunk's file
        /// </summary>
        public uint Offset;

        /// <summary>
        /// 
        /// </summary>
        public uint Base;

        /// <summary>
        /// Amount of contained items
        /// </summary>
        public int Records;

        /// <summary>
        /// Children
        /// </summary>
        public BaseObject[] _Item;

        /// <summary>
        /// Recalcuate new size of the object after changing values
        /// </summary>
        /// <returns>New size of the object</returns>
        public virtual uint Recalculate()
        {
            return Size;
        }

        /// <summary>
        /// Load the object into the stream
        /// </summary>
        /// <param name="File">Stream to load from</param>
        /// <param name="Reader">Reader to use</param>
        public abstract void Load(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader);

        /// <summary>
        /// Get the item in the tree
        /// </summary>
        /// <param name="pos">Position in the tree</param>
        /// <param name="indexes">Depth in the tree</param>
        /// <returns></returns>
        public abstract BaseObject Get_Item(int pos, params int[] indexes);

        /// <summary>
        /// Put the item in the tree
        /// </summary>
        /// <param name="It">Object to put</param>
        /// <param name="pos">Position to put at</param>
        /// <param name="indexes">Depth in the tree</param>
        public abstract void Put_Item(BaseObject It, int pos, params int[] indexes);

        /// <summary>
        /// Get the memory stream of the object
        /// </summary>
        /// <param name="pos">Position in the tree</param>
        /// <param name="indexes">Depth in the tree</param>
        /// <returns></returns>
        public abstract System.IO.MemoryStream Get_Stream(int pos, params int[] indexes);

        /// <summary>
        /// New memory stream to use
        /// </summary>
        /// <param name="It">Stream to use</param>
        /// <param name="pos">Position in the tree</param>
        /// <param name="indexes">Depth in the tree</param>
        public abstract void Put_Stream(System.IO.MemoryStream It, int pos, params int[] indexes);

        /// <summary>
        /// Update the memory stream of the object
        /// </summary>
        public abstract void UpdateStream();

        /// <summary>
        /// Add new item as a child
        /// </summary>
        /// <param name="pos">Position in the tree</param>
        /// <param name="indexes">Depth in the tree</param>
        public abstract void Add_Item(int pos, params int[] indexes);

        /// <summary>
        /// Delete the item including its children
        /// </summary>
        /// <param name="pos">Position in the tree</param>
        /// <param name="indexes">Depth in the tree</param>
        public abstract void Delete_Item(int pos, params int[] indexes);

        /// <summary>
        /// Save the object into a file
        /// </summary>
        /// <param name="File">File to save to</param>
        /// <param name="Writer">Writer to use</param>
        public abstract void Save(ref System.IO.FileStream File, ref System.IO.BinaryWriter Writer);

        /// <summary>
        /// Overrides array operator[]
        /// </summary>
        /// <param name="key">Index of array to access</param>
        /// <returns>BaseObject in the _Item[] array</returns>
        public virtual BaseObject this[int key]
        {
            get
            { 
                return _Item[key];
            }
            set
            {
                _Item[key] = value;
            }
        }


        /// <summary>
        /// Load the specified T object
        /// </summary>
        /// <typeparam name="T">Object to load</typeparam>
        /// <param name="File">File to load from</param>
        /// <param name="Reader">Reader to use</param>
        protected abstract void Load<T>(ref System.IO.FileStream File, ref System.IO.BinaryReader Reader) where T : BaseItem, new();
    }
}
