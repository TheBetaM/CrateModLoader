using System;
using System.IO;
// RCF API by NeoKesha
// Converted from VisualBasic

namespace RadcoreCementFile
{
    public class BinaryReader2 : BinaryReader
    {
        public BinaryReader2(System.IO.Stream stream) : base(stream) { }

        public override int ReadInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public override Int16 ReadInt16()
        {
            var data = base.ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        public override Int64 ReadInt64()
        {
            var data = base.ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        public override UInt32 ReadUInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        public override float ReadSingle()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToSingle(data, 0);
        }

    }

    public class BinaryWriter2 : BinaryWriter
    {
        public BinaryWriter2(System.IO.Stream stream) : base(stream) { }

        public void WriteBigEndian(UInt32 val)
        {
            byte[] data = BitConverter.GetBytes(val);
            Array.Reverse(data);
            Write(data);
        }

        public void WriteBigEndian(UInt16 val)
        {
            byte[] data = BitConverter.GetBytes(val);
            Array.Reverse(data);
            Write(data);
        }

        public void WriteBigEndian(UInt64 val)
        {
            byte[] data = BitConverter.GetBytes(val);
            Array.Reverse(data);
            Write(data);
        }

        public void WriteBigEndian(float val)
        {
            byte[] data = BitConverter.GetBytes(val);
            Array.Reverse(data);
            Write(data);
        }

    }


    public class RCF
    {
        public struct RCF_HEADER
        {
            public string signature;
            public byte Flag1;
            public bool Flag2;
            public bool Flag3;
            public bool Flag4;
            public UInt32 T1Offset;
            public UInt32 T1Size;
            public UInt32 T2Offset;
            public UInt32 T2Size;
            public UInt32 Gap1;
            public UInt32 Files;
            public RCF_TABLE1[] T1File;
            public UInt32 NamesAligment;
            public UInt32 Gap2;
            public RCF_TABLE2[] T2File;
        }
        public struct RCF_TABLE1
        {
            public UInt32 ID;
            public UInt32 Offset;
            public UInt32 Size;
            public UInt32 CompressedSize;
            public UInt32 Pos;
            public UInt32 CompressionFlag; // 0 - No Compression, 1 - Compressed
        }
        public struct RCF_TABLE2
        {
            public UInt32 SomeShit1;
            public UInt32 Align;
            public UInt32 Gap1;
            public UInt32 NameLen;
            public string Name;
            public UInt32 Gap2;
            public UInt32 Ref;
            public string External;
        }

        private string RCF_Path;
        public RCF_HEADER Header;

        public void OpenRCF(string Path)
        {
            if (!File.Exists(Path))
                return;
            RCF_Path = Path;
            FileStream RCF = new FileStream(RCF_Path, FileMode.Open, FileAccess.Read);
            BinaryReader RCFReader = new BinaryReader(RCF);
            BinaryReader2 RCFReader2 = new BinaryReader2(RCF);
            Header.signature = "";
            for (Int32 i = 1; i <= 32; i++)
            {
                byte ch = RCFReader.ReadByte();
                if (ch != 0x00)
                    Header.signature += (char)ch;
                else
                    break;
            }
            RCF.Position = 32;
            Header.Flag1 = RCFReader.ReadByte();
            Header.Flag2 = RCFReader.ReadBoolean();
            Header.Flag3 = RCFReader.ReadBoolean();
            Header.Flag4 = RCFReader.ReadBoolean();

            if (!Header.Flag3)
            {
                Header.T1Offset = RCFReader.ReadUInt32();
                Header.T1Size = RCFReader.ReadUInt32();
                Header.T2Offset = RCFReader.ReadUInt32();
                Header.T2Size = RCFReader.ReadUInt32();
                Header.Gap1 = RCFReader.ReadUInt32();
                Header.Files = RCFReader.ReadUInt32();
                Array.Resize(ref Header.T1File, (int)Header.Files);
                Array.Resize(ref Header.T2File, (int)Header.Files);
                RCF.Position = Header.T1Offset;
                for (Int32 i = 0; i <= Header.Files - 1; i++)
                {
                    Header.T1File[i].ID = RCFReader.ReadUInt32();
                    Header.T1File[i].Offset = RCFReader.ReadUInt32();
                    Header.T1File[i].Size = RCFReader.ReadUInt32();
                    Header.T1File[i].CompressedSize = Header.T1File[i].Size;
                    Header.T1File[i].CompressionFlag = 0;
                }
            }
            else
            {
                Header.T1Offset = RCFReader2.ReadUInt32();
                Header.T1Size = RCFReader2.ReadUInt32();
                Header.T2Offset = RCFReader2.ReadUInt32();
                Header.T2Size = RCFReader2.ReadUInt32();
                Header.Gap1 = RCFReader2.ReadUInt32();
                Header.Files = RCFReader2.ReadUInt32();
                Array.Resize(ref Header.T1File, (int)Header.Files);
                Array.Resize(ref Header.T2File, (int)Header.Files);
                RCF.Position = Header.T1Offset;
                if (Header.Flag2)
                {
                    for (Int32 i = 0; i <= Header.Files - 1; i++)
                    {
                        Header.T1File[i].ID = RCFReader2.ReadUInt32();
                        Header.T1File[i].Offset = RCFReader2.ReadUInt32();
                        Header.T1File[i].Size = RCFReader2.ReadUInt32();
                        Header.T1File[i].CompressedSize = Header.T1File[i].Size;
                        Header.T1File[i].CompressionFlag = 0;
                    }
                }
                else
                {
                    // CTTR GC frontend.rcf - compression handling not yet implemented
                    for (Int32 i = 0; i <= Header.Files - 1; i++)
                    {
                        Header.T1File[i].ID = RCFReader2.ReadUInt32();
                        Header.T1File[i].Offset = RCFReader2.ReadUInt32();
                        Header.T1File[i].CompressedSize = RCFReader2.ReadUInt32();
                        Header.T1File[i].Size = RCFReader2.ReadUInt32();
                        Header.T1File[i].CompressionFlag = RCFReader2.ReadUInt32();
                    }
                }
            }

            RCF.Position = Header.T2Offset;

            Header.NamesAligment = RCFReader.ReadUInt32();
            Header.Gap2 = RCFReader.ReadUInt32();
            for (Int32 i = 0; i <= Header.Files - 1; i++)
            {
                Header.T2File[i].SomeShit1 = RCFReader.ReadUInt32();
                Header.T2File[i].Align = RCFReader.ReadUInt32();
                Header.T2File[i].Gap1 = RCFReader.ReadUInt32();
                Header.T2File[i].NameLen = RCFReader.ReadUInt32();
                char[] tempName = RCFReader.ReadChars((int)Header.T2File[i].NameLen - 1);
                Header.T2File[i].Name = new string(tempName);
                Header.T2File[i].Gap2 = (uint)RCFReader.ReadInt32();
                Header.T2File[i].External = "";
            }

            Int32 n = 1;
            while (n < Header.Files)
            {
                if (Header.T1File[n].Offset < Header.T1File[n - 1].Offset)
                {
                    RCF_TABLE1 tmp = Header.T1File[n];
                    Header.T1File[n] = Header.T1File[n - 1];
                    Header.T1File[n - 1] = tmp;
                    if (n > 1)
                        n -= 2;
                }
                n += 1;
            }
            for (UInt32 i = 0; i <= Header.Files - 1; i++)
                Header.T1File[i].Pos = i;
            n = 1;
            while (n < Header.Files)
            {
                if (Header.T1File[n].ID < Header.T1File[n - 1].ID)
                {
                    RCF_TABLE1 tmp = Header.T1File[n];
                    Header.T1File[n] = Header.T1File[n - 1];
                    Header.T1File[n - 1] = tmp;
                    if (n > 1)
                        n -= 2;
                }
                n += 1;
            }
            for (Int32 i = 0; i <= Header.Files - 1; i++)
            {
                for (UInt32 j = 0; j <= Header.Files - 1; j++)
                {
                    if (i == Header.T1File[j].Pos)
                        Header.T2File[i].Ref = j;
                }
            }
            RCFReader.Close();
            RCFReader2.Close();
            RCF.Close();

            return;
        }
        /// <summary>
        ///     ''' Uses T2 index
        ///     ''' </summary>
        public void ExtractItem(UInt32 ind, string Path)
        {
            if (!System.IO.File.Exists(RCF_Path))
                return;
            if (Path[Path.Length - 1] != '\\')
                Path += @"\";
            FileStream RCF = new FileStream(RCF_Path, FileMode.Open, FileAccess.Read);
            BinaryReader RCFReader = new BinaryReader(RCF);
            string[] Folders = Header.T2File[ind].Name.Split('\\');
            FileStream File = new FileStream(Path + Folders[Folders.Length - 1], FileMode.Create, FileAccess.Write);
            BinaryWriter FileWriter = new BinaryWriter(File);
            RCF.Position = Header.T1File[Header.T2File[ind].Ref].Offset;
            FileWriter.Write(RCFReader.ReadBytes((int)Header.T1File[Header.T2File[ind].Ref].Size));
            FileWriter.Close();
            File.Close();
            RCFReader.Close();
            RCF.Close();
            return;
        }
        public void ExtractRCF(string Path)
        {
            if (!File.Exists(RCF_Path))
                return;
            if (Path[Path.Length - 1] != '\\')
                Path += @"\";
            FileStream RCF = new FileStream(RCF_Path, FileMode.Open, FileAccess.Read);
            BinaryReader RCFReader = new BinaryReader(RCF);
            for (Int32 i = 0; i <= Header.Files - 1; i++)
            {
                string check = Path;
                string[] Folders = Header.T2File[i].Name.Split('\\');
                for (Int32 j = 0; j <= Folders.Length - 2; j++)
                {
                    check += Folders[j] + @"\";
                    if (!Directory.Exists(check))
                        Directory.CreateDirectory(check);
                }
                check += Folders[Folders.Length - 1];
                //Console.WriteLine("RCF Extract: " + Folders[Folders.Length - 1] + " " + (i + 1).ToString() + @"\" + Header.Files.ToString());
                FileStream File = new FileStream(check, FileMode.Create, FileAccess.Write);
                BinaryWriter FileWriter = new BinaryWriter(File);
                RCF.Position = Header.T1File[Header.T2File[i].Ref].Offset;
                FileWriter.Write(RCFReader.ReadBytes((int)Header.T1File[Header.T2File[i].Ref].Size));
                FileWriter.Close();
                File.Close();
            }
            RCFReader.Close();
            RCF.Close();
            return;
        }
        /// <summary>
        ///     ''' Uses T1 index
        ///     ''' </summary>
        public void GetStream(UInt32 ind, ref MemoryStream MS)
        {
            MS = new MemoryStream((int)Header.T1File[ind].Size);
            FileStream RCF = new FileStream(RCF_Path, FileMode.Open, FileAccess.Read);
            BinaryReader RCFReader = new BinaryReader(RCF);
            RCF.Position = Header.T1File[ind].Offset;
            BinaryWriter MSW = new BinaryWriter(MS);
            MSW.Write(RCFReader.ReadBytes((int)Header.T1File[ind].Size));
            RCFReader.Close();
            RCF.Close();
            return;
        }
        public void GetStream(UInt32 ind, ref FileStream MS)
        {
            FileStream RCF = new FileStream(RCF_Path, FileMode.Open, FileAccess.Read);
            BinaryReader RCFReader = new BinaryReader(RCF);
            RCF.Position = Header.T1File[ind].Offset;
            BinaryWriter MSW = new BinaryWriter(MS);
            MSW.Write(RCFReader.ReadBytes((int)Header.T1File[ind].Size));
            RCFReader.Close();
            RCF.Close();
            return;
        }
        public void Pack(string NewPath, UInt32 Alignment = 2048)
        {
            if (NewPath == RCF_Path)
                return;

            FileStream NRCF = new FileStream(NewPath, FileMode.Create, FileAccess.Write);
            BinaryWriter NRCFWriter = new BinaryWriter(NRCF);
            BinaryWriter2 NRCFWriter2 = new BinaryWriter2(NRCF);
            //Console.WriteLine("RCF: Recalculating...");
            Recalculate(Alignment);
            for (Int32 i = 0; i <= 31; i++)
            {
                if (i < Header.signature.Length)
                    NRCFWriter.Write(Header.signature[i]);
                else
                    NRCFWriter.Write(System.Convert.ToByte(0));
            }
            //Console.WriteLine("RCF: Writing header...");
            NRCFWriter.Write(Header.Flag1);
            NRCFWriter.Write(Header.Flag2);
            NRCFWriter.Write(Header.Flag3);
            NRCFWriter.Write(Header.Flag4);

            if (!Header.Flag3)
            {
                NRCFWriter.Write(Header.T1Offset);
                NRCFWriter.Write(Header.T1Size);
                NRCFWriter.Write(Header.T2Offset);
                NRCFWriter.Write(Header.T2Size);
                NRCFWriter.Write(Header.Gap1);
                NRCFWriter.Write(Header.Files);
                NRCF.Position = Header.T1Offset;
                for (Int32 i = 0; i <= Header.Files - 1; i++)
                {
                    NRCFWriter.Write(Header.T1File[i].ID);
                    NRCFWriter.Write(Header.T1File[i].Offset);
                    NRCFWriter.Write(Header.T1File[i].Size);
                }
            }
            else
            {
                NRCFWriter2.WriteBigEndian(Header.T1Offset);
                NRCFWriter2.WriteBigEndian(Header.T1Size);
                NRCFWriter2.WriteBigEndian(Header.T2Offset);
                NRCFWriter2.WriteBigEndian(Header.T2Size);
                NRCFWriter2.WriteBigEndian(Header.Gap1);
                NRCFWriter2.WriteBigEndian(Header.Files);
                NRCF.Position = Header.T1Offset;

                if (Header.Flag2)
                {
                    for (Int32 i = 0; i <= Header.Files - 1; i++)
                    {
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].ID);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].Offset);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].Size);
                    }
                }
                else
                {
                    // CTTR GC frontend.rcf - compression handling not yet implemented
                    for (Int32 i = 0; i <= Header.Files - 1; i++)
                    {
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].ID);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].Offset);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].CompressedSize);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].Size);
                        NRCFWriter2.WriteBigEndian(Header.T1File[i].CompressionFlag);
                    }
                }
            }
            
            NRCF.Position = Header.T2Offset;
            NRCFWriter.Write(Header.NamesAligment);
            NRCFWriter.Write(Header.Gap2);
            for (Int32 i = 0; i <= Header.Files - 1; i++)
            {
                NRCFWriter.Write(Header.T2File[i].SomeShit1);
                NRCFWriter.Write(Header.T2File[i].Align);
                NRCFWriter.Write(Header.T2File[i].Gap1);
                NRCFWriter.Write(Header.T2File[i].NameLen);
                for (Int32 j = 0; j <= Header.T2File[i].NameLen - 2; j++)
                    NRCFWriter.Write(System.Convert.ToChar(Header.T2File[i].Name[j]));
                NRCFWriter.Write(Header.T2File[i].Gap2);
            }
            RCF ORCF = new RCF();
            ORCF.OpenRCF(RCF_Path);
            for (UInt32 i = 0; i <= Header.Files - 1; i++)
            {
                //Console.WriteLine("RCF Pack: " + Header.T2File[Header.T1File[i].Pos].Name + " " + (i + 1).ToString() + @"\" + Header.Files.ToString());
                NRCF.Position = Header.T1File[i].Offset;
                if (Header.T2File[Header.T1File[i].Pos].External == "")
                    ORCF.GetStream(i, ref NRCF);
                else
                    NRCFWriter.Write(File.ReadAllBytes(Header.T2File[Header.T1File[i].Pos].External));
            }
            NRCFWriter.Close();
            NRCF.Close();

            return;
        }
        public void Recalculate(UInt32 Alignment = 2048)
        {
            Header.NamesAligment = Alignment;
            Header.T1Offset = 60;
            if (Header.Flag2)
            {
                Header.T1Size = Header.Files * 12;
            }
            else
            {
                Header.T1Size = Header.Files * 20;
            }
            Header.T2Offset = ((Header.T1Offset + Header.T1Size - 1) / Alignment + 1) * Alignment;
            Header.T2Size = 8 + Header.Files * 20;
            for (Int32 i = 0; i <= Header.Files - 1; i++)
                Header.T2Size += Header.T2File[i].NameLen - 1;
            UInt32 offset = Header.T2Offset;
            UInt32 size = Header.T2Size;
            for (Int32 i = 0; i <= Header.Files - 1; i++)
            {
                UInt32 ind = Header.T2File[i].Ref;
                Header.T1File[ind].Offset = ((offset + size - 1) / Alignment + 1) * Alignment;
                if (Header.T2File[i].External != "")
                {
                    FileInfo FI = new FileInfo(Header.T2File[i].External);
                    Header.T1File[ind].Size = (uint)FI.Length;
                    if (Header.T1File[ind].CompressionFlag != 1)
                    {
                        Header.T1File[ind].CompressedSize = Header.T1File[ind].Size;
                    }
                    else
                    {
                        // todo: compressed size and setting the compression flag to 0 when external is not compressed
                    }
                }
                offset = Header.T1File[ind].Offset;
                size = Header.T1File[ind].CompressedSize;
            }
            return;
        }
    }
}