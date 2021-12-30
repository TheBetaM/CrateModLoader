using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrateModLoader.GameSpecific.WormsForts.XOM;
using System.IO;
using System.Reflection;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class XOM_TYPE
    {
        public uint unkInt1;
        public uint ContainerCount;
        public uint unkInt2;
        public byte[] Data; // 0x10
        public string Name; // 0x20 max
        public Type KnownType = null;
        public List<Container> Containers;

        public static Dictionary<string, Type> SupportedTypes;

        public static void GetSupported()
        {
            SupportedTypes = new Dictionary<string, Type>();

            Assembly assembly = Assembly.GetAssembly(typeof(XOM_TYPE));
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || !typeof(Container).IsAssignableFrom(type)) // only get non-abstract modders
                    continue;

                XOM_TypeName tName = (XOM_TypeName)type.GetCustomAttribute(typeof(XOM_TypeName), false);

                if (tName == null)
                    continue;

                SupportedTypes.Add(tName.Name, type);
            }
        }
    }

    public class XOM_TypeName : Attribute
    {
        public string Name { get; set; }

        public XOM_TypeName(string type)
        {
            Name = type;
        }
    }

    public enum WormsGame
    {
        Forts = 0,
        W3D,
        Worms4,
        OW,
        OW2,
        BattleIslands,
    }

    public class XOM_File
    {
        public WormsGame FileGame = WormsGame.Forts;
        public string FileName;

        public byte[] HeaderPad1; // 0x14
        public uint RootContainer; // container list order ID
        public byte[] HeaderPad2; // 0x1C

        public List<XOM_TYPE> Types;
        public byte[] GUID; //0x20
        public List<string> Strings;
        public List<Container> Containers; // order determines type

        public void Read(string fileName)
        {
            FileName = fileName;

            using (var br = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000, FileOptions.SequentialScan))
            {
                byte[] buffer = new byte[br.Length];
                br.Read(buffer, 0, buffer.Length);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (BinaryReader reader = new BinaryReader(memoryStream))
                    {
                        Read(reader);
                    }
                }
            }
        }
        public void Write(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    Write(writer);
                }
            }
        }

        void Read(BinaryReader reader)
        {
            string IDstring = new string(reader.ReadChars(4));
            if (IDstring != "MOIK")
            {
                Console.WriteLine("Not an XOM file!");
                return;
            }

            Types = new List<XOM_TYPE>();
            Strings = new List<string>();
            Containers = new List<Container>();

            // Header
            HeaderPad1 = reader.ReadBytes(0x14);
            uint TypeCount = reader.ReadUInt32();
            uint ContainerCount = reader.ReadUInt32();
            RootContainer = reader.ReadUInt32();
            HeaderPad2 =  reader.ReadBytes(0x1C);

            // Types
            for (int i = 0; i < TypeCount; i++)
            {
                reader.ReadBytes(0x04); // TYPE
                XOM_TYPE newType = new XOM_TYPE();
                newType.unkInt1 = reader.ReadUInt32();
                newType.ContainerCount = reader.ReadUInt32();
                newType.unkInt2 = reader.ReadUInt32();
                newType.Data = reader.ReadBytes(0x10);
                string typeName = new string(reader.ReadChars(0x20));
                typeName = typeName.Trim('\0');
                newType.Name = typeName;

                newType.KnownType = null;
                newType.Containers = new List<Container>();

                Types.Add(newType);
            }

            // GUID / SCHM
            GUID = reader.ReadBytes(0x20);

            // Strings
            reader.ReadBytes(0x04); //STRS
            uint StringCount = reader.ReadUInt32();
            StringCount--;
            uint SectionSize = reader.ReadUInt32();
            reader.ReadUInt32(); // first offset 0, empty string ID 0
            List<uint> StrOffsets = new List<uint>();
            for (int i = 0; i < StringCount; i++)
            {
                StrOffsets.Add(reader.ReadUInt32());
            }

            // String 0 is empty
            long pos = reader.BaseStream.Position;
            reader.ReadByte();
            Strings.Add("");
            for (int i = 0; i < StringCount; i++)
            {
                long start = pos + StrOffsets[i];
                reader.BaseStream.Position = start;
                int len = 0;
                while (reader.ReadChar() != '\0' && len < SectionSize)
                {
                    len++;
                }
                reader.BaseStream.Position = start;
                Strings.Add(new string(reader.ReadChars(len)));
                reader.ReadByte();
            }
            reader.BaseStream.Position = pos + SectionSize;

            //Container Types
            for (int i = 0; i < Types.Count; i++)
            {
                for (int a = 0; a < Types[i].ContainerCount; a++)
                {
                    XOM_TYPE ContType = Types[i];
                    Container newCont = null;
                    if (XOM_TYPE.SupportedTypes.ContainsKey(ContType.Name))
                    {
                        ContType.KnownType = XOM_TYPE.SupportedTypes[ContType.Name];
                        newCont = (Container)Activator.CreateInstance(XOM_TYPE.SupportedTypes[ContType.Name]);
                    }
                    else
                    {
                        newCont = new UnknownContainer();
                    }
                    newCont.ContType = ContType;
                    if (ContType.Name == "XGraphSet" || ContType.Name == "XAnimClipLibrary")
                    {
                        // no header
                        newCont.RawData = true;
                    }

                    ContType.Containers.Add(newCont);
                    Containers.Add(newCont);
                }
            }

            if (Containers.Count < ContainerCount)
                throw new Exception("Container count mismatch!");

            // Containers
            for (int i = 0; i < ContainerCount; i++)
            {
                Container newCont = Containers[i];
                newCont.ID = (uint)i + 1;

                // ideally all structure sizes would be known in which case this wouldn't be needed
                long startCont = reader.BaseStream.Position;
                if (newCont is UnknownContainer)
                {
                    long contSize = 0;
                    bool FoundEnd = false;
                    int HeaderSize = 7;
                    if (newCont.RawData)
                    {
                        HeaderSize = 0;
                    }
                    else
                    {
                        reader.BaseStream.Position += 4;
                    }

                    while (reader.BaseStream.Position < reader.BaseStream.Length - 7 && !FoundEnd)
                    {
                        long TestPos = reader.BaseStream.Position;
                        byte test1 = reader.ReadByte();
                        if (test1 == 0x43) //C
                        {
                            test1 = reader.ReadByte();
                            if (test1 == 0x54) //T
                            {
                                test1 = reader.ReadByte();
                                if (test1 == 0x4E) //N
                                {
                                    test1 = reader.ReadByte();
                                    if (test1 == 0x52) //R
                                    {
                                        reader.ReadByte();
                                        byte shortTest = reader.ReadByte();
                                        if (shortTest == 0 || shortTest == 1)
                                        {
                                            contSize = reader.BaseStream.Position - (6 + HeaderSize); // 6 + 7
                                            contSize = contSize - startCont;
                                            FoundEnd = true;
                                        }
                                    }
                                }
                            }
                        }
                        reader.BaseStream.Position = TestPos + 1;
                    }
                    if (!FoundEnd)
                    {
                        contSize = reader.BaseStream.Length - startCont;
                        contSize -= HeaderSize;
                    }
                    newCont.Size = contSize;
                    reader.BaseStream.Position = startCont;
                }
                
                try
                {
                    newCont.ReadBase(this, reader);
                }
                catch
                {
                    long erSize = reader.BaseStream.Position - startCont;
                    Console.WriteLine("File: " + FileName);
                    Console.WriteLine("Container read fail: ID " + newCont.ID + " Type: " + newCont.ContType.Name + " Size: " + erSize + " Expected Size: " + newCont.Size);
                    throw new Exception("Container read fail!");
                }
            }

            //Console.WriteLine("XOM loaded.");
        }
        
        void Write(BinaryWriter writer)
        {
            writer.Write("MOIK".ToCharArray());

            // Header
            writer.Write(HeaderPad1);
            writer.Write(Types.Count);
            writer.Write(Containers.Count);
            writer.Write(RootContainer);
            writer.Write(HeaderPad2);

            // Types
            for (int i = 0; i < Types.Count; i++)
            {
                writer.Write("TYPE".ToCharArray());
                writer.Write(Types[i].unkInt1);
                writer.Write(Types[i].ContainerCount);
                writer.Write(Types[i].unkInt2);
                writer.Write(Types[i].Data);

                char[] TName = Types[i].Name.ToCharArray();
                char[] FullPName = new char[0x20];
                TName.CopyTo(FullPName, 0);
                writer.Write(FullPName);
            }

            // GUID / SCHM
            writer.Write(GUID);

            // Strings
            writer.Write("STRS".ToCharArray());
            writer.Write(Strings.Count);

            int SectionSize = 1;
            List<int> StrOffsets = new List<int>();
            StrOffsets.Add(0);

            long STRSsectionstart = writer.BaseStream.Position;
            writer.Write((uint)0);
            for (int i = 0; i < Strings.Count; i++)
            {
                writer.Write((uint)0);
            }

            long SizeCountStart = writer.BaseStream.Position;
            // String 0 is empty
            writer.Write((byte)0);

            for (int s = 1; s < Strings.Count; s++)
            {
                StrOffsets.Add((int)(writer.BaseStream.Position - SizeCountStart));
                writer.Write(Strings[s].ToCharArray());
                writer.Write((byte)0);
            }
            long CNTRsectionstart = writer.BaseStream.Position;

            writer.BaseStream.Position = STRSsectionstart;

            SectionSize = (int)(CNTRsectionstart - SizeCountStart);
            writer.Write(SectionSize);
            for (int i = 0; i < StrOffsets.Count; i++)
            {
                writer.Write(StrOffsets[i]);
            }

            writer.BaseStream.Position = CNTRsectionstart;

            // Containers
            for (int i = 0; i < Containers.Count; i++)
            {
                Containers[i].WriteBase(writer);
            }

            //Console.WriteLine("XOM saved.");
        }

        public XOM_TYPE GetXomType(Type thisType)
        {
            XOM_TYPE xType = null;
            foreach (XOM_TYPE t in Types)
            {
                if (t.KnownType != null && t.KnownType == thisType)
                {
                    xType = t;
                    break;
                }
            }
            return xType;
        }

        public T GetContainer<T>(string cname) where T : NamedContainer
        {
            XOM_TYPE xType = GetXomType(typeof(T));
            if (xType == null) return null;

            foreach (Container cont in xType.Containers)
            {
                NamedContainer nam = (NamedContainer)cont;
                if (nam.Name == cname)
                {
                    return (T)cont;
                }
            }
            return null;
        }

        public T GetContainer<T>() where T : Container
        {
            XOM_TYPE xType = GetXomType(typeof(T));
            if (xType == null) return null;

            foreach (Container cont in xType.Containers)
            {
                return (T)cont;
            }
            return null;
        }

        public List<T> GetContainers<T>() where T : Container
        {
            List<T> entries = new List<T>();

            XOM_TYPE xType = GetXomType(typeof(T));
            if (xType == null) return entries;
            
            foreach (Container cont in xType.Containers)
            {
                entries.Add((T)cont);
            }
            return entries;
        }
    }

}
