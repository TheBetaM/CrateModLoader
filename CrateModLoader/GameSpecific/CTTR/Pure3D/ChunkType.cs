using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pure3D
{
    /// <summary>
    /// Applies a Pure3D type identifier to a Chunk type, allowing it to be read.
    /// </summary>
    public class ChunkType : Attribute
    {
        public uint TypeID { get; set; }

        public ChunkType(uint type)
        {
            TypeID = type;
        }

        /// <summary>
        /// Gets all Chunk classes with the ChunkType attribute applied.
        /// </summary>
        public static IEnumerable<Type> GetSupported()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Chunks.Root));
            foreach (Type type in assembly.GetTypes())
                if (type.IsDefined(typeof(ChunkType), false))
                    yield return type;
        }
    }
}
