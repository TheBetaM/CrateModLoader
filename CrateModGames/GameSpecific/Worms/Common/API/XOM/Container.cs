using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    public abstract class Container
    {
        public uint ID = 0;
        byte[] ContPad = new byte[3];
        public XOM_File ParentFile;
        public XOM_TYPE ContType;
        public long Size = 0;
        public bool RawData = false;

        public void ReadBase(XOM_File file, BinaryReader reader)
        {
            ParentFile = file;

            if (RawData)
            {
                Read(reader);
                return;
            }

            string IDstring = new string(reader.ReadChars(4));
            if (IDstring != "CTNR")
            {
                Console.WriteLine("File: " + Path.GetFileName(ParentFile.FileName));
                Console.WriteLine("ID " + ID + " Type: " + ContType.Name + " Size: " + Size);
                Console.WriteLine("Container read fail! Not a container: " + IDstring);
                throw new Exception("Not a container.");
            }
            ContPad = reader.ReadBytes(0x3); //empty

            Read(reader);
        }

        public abstract void Read(BinaryReader reader);

        public virtual void WriteBase(BinaryWriter writer)
        {
            if (!RawData)
            {
                writer.Write("CTNR".ToCharArray());
                writer.Write(ContPad);
            }

            Write(writer);
        }

        public abstract void Write(BinaryWriter writer);
    }
}
