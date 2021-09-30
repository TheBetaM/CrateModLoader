using System;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public class SEController : ItemController
    {
        public new SoundEffect Data { get; set; }

        public byte[] RawData { get; set; }
        public byte[] SoundData { get; set; }

        private static SoundPlayer player = new SoundPlayer();

        public SEController(MainForm topform, SoundEffect item) : base(topform, item)
        {
            Data = item;
            LoadSoundData();
            AddMenu("Play sound", Menu_PlaySound);
            AddMenu("Export to .WAV", Menu_ExportWAV);
            AddMenu("Export to .VAG", Menu_ExportVAG);
            AddMenu("Replace sound from .WAV", Menu_ReplaceSoundWav);
        }

        protected override string GetName()
        {
            return $"Sound Effect [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[3];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"Frequency: {Data.FreqFac} ({Data.Freq}Hz) Unknown: {Data.UnkFlag}";
        }

        public void LoadSoundData()
        {
            RawData = new byte[Data.SoundSize];
            Array.Copy(Data.Parent.ExtraData, Data.SoundOffset, RawData, 0, Data.SoundSize);
            SoundData = RIFF.SaveRiff(ADPCM.ToPCMMono(RawData, (int)Data.SoundSize), 1, Data.Freq);
        }

        private void Menu_PlaySound()
        {
            player.Stop();
            player.Stream = new MemoryStream(SoundData);
            player.Play();
        }

        private void Menu_ReplaceSoundWav()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WAV|*.wav";
            ofd.FileName = Data.ID.ToString();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(file))
                {
                    file.Position = 0x16;
                    UInt16 channels = reader.ReadUInt16();
                    UInt32 frequency = reader.ReadUInt32();
                    file.Position = 0x28;
                    int len = reader.ReadInt32();
                    Byte[] PCM = reader.ReadBytes(len);
                    if (channels == 1)
                    {
                        Data.Freq = (ushort)frequency;
                        Byte[] newData = ADPCM.FromPCMMono(PCM);
                        UInt32 newSize = (uint)newData.Length;
                        InjectData(Data.SoundOffset, Data.SoundSize, newData);
                        Data.SoundSize = newSize;
                    } else
                    {
                        throw new ArgumentException("ATM only mono, sorry fam");
                    }
                    LoadSoundData();
                    GenText();
                }
            }
        }
        public void InjectData(UInt32 oldOffset, UInt32 oldSize, Byte[] newData)
        {
            Byte[] piece1 = new byte[oldOffset];
            Byte[] piece2 = new byte[Data.Parent.ExtraData.Length - oldOffset - oldSize];
            Array.Copy(Data.Parent.ExtraData, 0, piece1, 0, piece1.Length);
            Array.Copy(Data.Parent.ExtraData, oldOffset + oldSize, piece2, 0, piece2.Length);
            Data.Parent.ExtraData = new byte[piece1.Length + newData.Length + piece2.Length];
            Array.Copy(piece1, 0, Data.Parent.ExtraData, 0, piece1.Length);
            Array.Copy(newData, 0, Data.Parent.ExtraData, piece1.Length, newData.Length);
            Array.Copy(piece2, 0, Data.Parent.ExtraData, piece1.Length + newData.Length, piece2.Length);
            foreach (TwinsItem item in Data.Parent.Records)
            {
                SoundEffect se = (SoundEffect)item;
                if (se.SoundOffset > oldOffset)
                {
                    se.SoundOffset += (UInt32)(newData.Length - oldSize);
                }
            }
        }
        private void Menu_ExportWAV()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "WAV|*.wav";
            sfd.FileName = Data.ID.ToString();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file);
                writer.Write(SoundData);
                writer.Close();
            }
        }

        private void Menu_ExportVAG()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "VAG|*.vag";
            var id_str = Data.ID.ToString();
            sfd.FileName = id_str;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter writer = new BinaryWriter(file);
                writer.Write("VAGp".ToCharArray());
                writer.Write(20);
                writer.Write(0);
                writer.Write(RawData.Length);
                writer.Write(BitConv.FlipBytes(Data.Freq));
                writer.Write(0); writer.Write(0); writer.Write(0);
                char[] name = new char[16] { (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0 };
                Array.Copy(id_str.ToCharArray(), name, Math.Min(id_str.Length, 16));
                writer.Write(name);
                //writer.Write(0); writer.Write(0); writer.Write(0); writer.Write(0);
                writer.Write(RawData);
                writer.Close();
            }
        }
    }
}
