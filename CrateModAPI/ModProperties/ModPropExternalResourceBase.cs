using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public abstract class ModPropExternalResourceBase : ModProperty<bool>
    {
        // Value is true if the property contains a valid resource
        public bool Loaded => Value != false;
        public string ResourcePath = ""; // relative for Mod Crates, absolute for in-UI modding
        public bool AbsolutePath = false;
        public string BrowseFilter = ""; // OpenFileDialog filter
        public bool SaveAsRawData = false;

        public ModPropExternalResourceBase(bool b) : base(b)
        {
        }
        public ModPropExternalResourceBase(bool b, string name, string desc) : base(b, name, desc)
        {
        }

        /// <summary>
        /// Test if resource file is valid. Return true if it is.
        /// </summary>
        public abstract bool TryResource(string path);

        /// <summary>
        /// Save resource to file.
        /// </summary>
        public abstract void ResourceToFile(string outputPath);

        /// <summary>
        /// Load resource from file.
        /// </summary>
        public abstract void FileToResource(string inputPath);

        public override void Serialize(ref string line)
        {
            base.Serialize(ref line);
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            
        }

    }
}