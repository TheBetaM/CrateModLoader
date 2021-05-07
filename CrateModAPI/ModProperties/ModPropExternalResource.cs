using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace CrateModLoader.ModProperties
{
    // Intended to be overrriden for specific resource types
    public class ModPropExternalResource<T> : ModPropExternalResourceBase
    {

        public T Resource;

        public ModPropExternalResource(bool b) : base(b)
        {
        }
        public ModPropExternalResource(bool b, string name, string desc) : base(b, name, desc)
        {
        }
        public ModPropExternalResource(string p) : base(p)
        {
        }
        public ModPropExternalResource(string p, string name, string desc) : base(p, name, desc)
        {
        }

        /// <summary>
        /// Test if resource file is valid and load it into the Resoource variable. Return true if it is.
        /// </summary>
        public override bool TryResource(string path)
        {
            // Override this to test if resource is valid
            return false;
        }

        /// <summary>
        /// Save resource to file.
        /// </summary>
        public override void ResourceToFile(string outputPath)
        { }

        /// <summary>
        /// Load resource from file.
        /// </summary>
        public override void FileToResource(string inputPath)
        { }

        public override void ResetToDefault()
        {
            Resource = default(T);
            base.ResetToDefault();
        }

        public override void Serialize(ref string line)
        {
            if (Value)
            {
                base.Serialize(ref line);
                if (!SaveAsRawData)
                {
                    if (AbsolutePath)
                    {
                        line += "file:";
                    }
                    else
                    {
                        line += "crate:";
                    }
                    line += ResourcePath;
                }
                else
                {
                    line += "data:";
                    // resource must be serializable
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            bf.Serialize(ms, Resource);
                            line += Convert.ToBase64String(ms.ToArray());
                        }
                    }
                    catch
                    {

                    }
                    
                }
            }
        }

        public override void DeSerialize(string input, ModCrate crate)
        {
            Value = false;
            if (input != "")
            {
                if (input.StartsWith("file:"))
                {
                    string inputPath = input.Substring(5);
                    if (TryResource(inputPath))
                    {
                        ResourcePath = inputPath;
                        Value = true;
                    }
                }
                else if (input.StartsWith("crate:"))
                {
                    string inputPath = input.Substring(6);
                    if (crate != null)
                    {
                        if (crate.IsFolder)
                        {
                            string testPath = Path.Combine(crate.Path, inputPath);
                            if (TryResource(testPath))
                            {
                                ResourcePath = inputPath;
                                Value = true;
                            }
                        }
                        else
                        {
                            string targetPath = ModLoaderGlobals.BaseDirectory + "resource.dat";
                            using (ZipArchive archive = ZipFile.OpenRead(crate.Path))
                            {
                                foreach (ZipArchiveEntry entry in archive.Entries)
                                {
                                    if (entry.FullName == inputPath)
                                    {
                                        entry.ExtractToFile(targetPath, true);
                                    }
                                }
                            }
                            if (TryResource(targetPath))
                            {
                                ResourcePath = inputPath;
                                Value = true;
                            }
                            File.Delete(targetPath);
                        }
                    }
                    else
                    {
                        // loading from a settings file... no crate data, but needs a path to find the resources
                    }
                }
                else if (input.StartsWith("data:"))
                {
                    string inputData = input.Substring(5);

                    try
                    {
                        byte[] bytes = Convert.FromBase64String(inputData);
                        using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
                        {
                            ms.Write(bytes, 0, bytes.Length);
                            ms.Position = 0;
                            BinaryFormatter bf = new BinaryFormatter();
                            Resource = (T)bf.Deserialize(ms);
                        }
                        Value = true;
                    }
                    catch
                    {

                    }
                    
                }
                else
                {
                    Value = false;
                }
            }
        }

    }
}