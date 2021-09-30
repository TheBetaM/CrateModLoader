using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using OpenTK.Audio.OpenAL;


namespace TwinsaityEditor
{
    public partial class MHWorker
    {
        public System.IO.FileStream MH;
        public System.IO.FileStream MB;
        public System.IO.BinaryReader MHReader;
        public System.IO.BinaryReader MBReader;
        public uint SoundCount;
        public uint Interleave;
        private Record[] Sounds;
        private int SoundBuffer = 0;
        private int SoundSource = 0;
        private bool Playing = false;

        public MHWorker()
        {
            InitializeComponent();
        }

        private void InitDS()
        {
            SoundSource = AL.GenSource();
            SoundBuffer = AL.GenBuffer();
        }

        public void Init(string MHPath, string MBPath)
        {
            InitDS();
            MH = new System.IO.FileStream(MHPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            MB = new System.IO.FileStream(MBPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            MHReader = new System.IO.BinaryReader(MH);
            MBReader = new System.IO.BinaryReader(MB);
            LoadMH();
        }
        private void LoadMH()
        {
            SoundCount = MHReader.ReadUInt32();
            Interleave = MHReader.ReadUInt32();
            Array.Resize(ref Sounds, (int)SoundCount);
            for (int i = 0; i <= SoundCount - 1; i++)
            {
                Sounds[i].Type = MHReader.ReadUInt32();
                Sounds[i].Size = MHReader.ReadUInt32();
                Sounds[i].Offset = MHReader.ReadUInt32();
                Sounds[i].SampleRate = MHReader.ReadUInt32();
                Sounds[i].Skip = MHReader.ReadUInt32();
                Sounds[i].External = false;
                switch (Sounds[i].Type)
                {
                    case 0:
                        {
                            MB.Position = Sounds[i].Offset + 32;
                            for (int j = 0; j <= 31; j++)
                            {
                                char ch = MBReader.ReadChar();
                                if (Strings.Asc(ch) > 0)
                                    Sounds[i].Name += ch;
                                else
                                    break;
                            }
                            MB.Position = Sounds[i].Offset + 16;
                            byte[] b = new byte[4];
                            b = MBReader.ReadBytes(4);
                            Sounds[i].SampleRate = (uint)(b[0] * 256 * 256 * 256 + b[1] * 256 * 256 + b[2] * 256 + b[3]);
                            break;
                        }

                    case 1:
                        {
                            Sounds[i].Name = "Stereo";
                            break;
                        }

                    case 2:
                        {
                            Sounds[i].Name = "Reserved";
                            break;
                        }
                }
            }
            BuildTree();
        }
        private void BuildTree()
        {
            TreeView1.BeginUpdate();
            Label6.Text = "Sounds: " + SoundCount.ToString() + " Interleave: " + Interleave.ToString();
            TreeView1.Nodes.Clear();
            for (int i = 0; i <= SoundCount - 1; i++)
            {
                string name = "ID: " + i.ToString() + " ";
                TreeView1.Nodes.Add(name + Sounds[i].Name);
            }
            TreeView1.EndUpdate();
        }
        private void ShowInfo(uint index)
        {
            {
                var withBlock = Sounds[index];
                if (!Sounds[index].External)
                {
                    switch (withBlock.Type)
                    {
                        case 0:
                            {
                                Label1.Text = "Type: Mono";
                                break;
                            }

                        case 1:
                            {
                                Label1.Text = "Type: Stereo";
                                break;
                            }

                        case 2:
                            {
                                Label1.Text = "Type: Reserved";
                                break;
                            }
                    }
                    Label2.Text = "Size: " + withBlock.Size.ToString();
                    Label3.Text = "Offset: " + withBlock.Offset.ToString();
                    Label4.Text = "Sample Rate: " + withBlock.SampleRate.ToString();
                    Label5.Text = "Skip: " + withBlock.Skip.ToString();
                    if (!(withBlock.Type == 2))
                    {
                        uint time = (uint)Math.Round(withBlock.Size / (double)16 * 28 / withBlock.SampleRate / (withBlock.Type + 1));
                        Label8.Text = ToTime(time);
                    }
                    else
                        Label8.Text = "0:00";
                    Label10.Text = "Loaded: no";
                }
                else
                {
                    Label1.Text = "Type: External";
                    Label2.Text = "Size: ";
                    Label3.Text = "Offset: ";
                    Label4.Text = "Sample Rate: ";
                    Label5.Text = "Skip: ";
                    Label8.Text = "0:00";
                    Label10.Text = "Loaded: no";
                }
            }
        }
        private string ToTime(uint time)
        {
            uint seconds = time % 60;
            uint minutes = time / 60;
            return minutes.ToString() + ":" + ((seconds.ToString().Length == 1) ? "0" + seconds.ToString() : seconds.ToString());
        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowInfo((uint)TreeView1.SelectedNode.Index);
            TrackBar1.Value = 0;
            Playing = false;
            if (SoundBuffer != 0)
            {
                AL.BindBufferToSource(SoundSource, 0);
                AL.DeleteBuffer(SoundBuffer);
                SoundBuffer = 0;
            }
        }
        private void LoadBuffer(uint index)
        {
            if (!(Sounds[index].Type == 2))
            {
                System.IO.MemoryStream SoundStream = new System.IO.MemoryStream();
                System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(SoundStream);
                Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                SoundStream.Position = 44;
                if (Sounds[index].Type == 0)
                {
                    System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                    System.IO.BinaryWriter ADPCMWriter = new System.IO.BinaryWriter(ADPCM);
                    if (!Sounds[index].External)
                    {
                        MB.Position = Sounds[index].Offset + 48;
                        ADPCMWriter.Write(MBReader.ReadBytes((int)Sounds[index].Size));
                    }
                    else
                        ADPCMWriter.Write(Sounds[index].Stream.ToArray());
                    ADPCM.Position = 0;
                    ADPCM_Device.ADPCM2PCM(ADPCM, ref PCM);
                    SoundStream.Position = 0;
                    WriteHeader(ref SoundStream, Sounds[index].SampleRate, 1, (uint)PCM.Length);
                    StreamWriter.Write(PCM.ToArray());
                }
                else
                {
                    System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCM_R = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCM_L = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCM_R = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCM_L = new System.IO.MemoryStream();
                    System.IO.BinaryWriter ADPCMWriter = new System.IO.BinaryWriter(ADPCM);
                    System.IO.BinaryWriter PCMWriter = new System.IO.BinaryWriter(PCM);
                    if (!Sounds[index].External)
                    {
                        MB.Position = Sounds[index].Offset;
                        ADPCMWriter.Write(MBReader.ReadBytes((int)Sounds[index].Size));
                    }
                    else
                        ADPCMWriter.Write(Sounds[index].Stream.ToArray());
                    ADPCM.Position = 0;
                    ADPCM_Device.ADPCM_Demux(ADPCM, ref ADPCM_R, ref ADPCM_L, Interleave);
                    ADPCM_R.Position = 0;
                    PCM_R.Position = 0;
                    ADPCM_L.Position = 0;
                    PCM_L.Position = 0;
                    ADPCM_Device.ADPCM2PCM(ADPCM_R, ref PCM_R);
                    ADPCM_Device.ADPCM2PCM(ADPCM_L, ref PCM_L);
                    ADPCM_Device.PCM_Mux(ref PCM, PCM_R, PCM_L);
                    SoundStream.Position = 0;
                    WriteHeader(ref SoundStream, Sounds[index].SampleRate, 2, (uint)PCM.Length);
                    StreamWriter.Write(PCM.ToArray());
                }
                SoundStream.Position = 0;

                AL.BindBufferToSource(SoundSource, SoundBuffer);
                AL.BufferData(SoundBuffer, ALFormat.Stereo16, SoundStream.GetBuffer(), SoundStream.GetBuffer().Length, 44100);
                

                TrackBar1.Maximum = SoundStream.GetBuffer().Length - 1;
                Label10.Text = "Loaded: yes";
            }
            else
                Interaction.MsgBox("No data to play");
        }
        private void WriteHeader(ref System.IO.MemoryStream WAVE, uint SampleRate, UInt16 Channels, uint SoundSize)
        {
            System.IO.BinaryWriter WAVEWriter = new System.IO.BinaryWriter(WAVE);
            char[] Header = new[] { 'R', 'I', 'F', 'F' };
            int FileSize = (int)SoundSize + 32; // FinalSize - 8 position 4
            char[] WAVHeader = new[] { 'W', 'A', 'V', 'E' };
            char[] fmtHeader = new[] { 'f', 'm', 't', ' ' };
            int SubChunk1Size = 16;
            Int16 Format = 1;
            // Dim Chanells As Int16 
            // Dim SampleRate As int
            int BitRate = (int)SampleRate * Channels * 2;
            Int16 Align = (short)(Channels * 2);
            Int16 BPS = 16;
            char[] SubChunk2Id = new[] { 'd', 'a', 't', 'a' };
            // Dim SubChunk2Size As int 
            WAVEWriter.Write(Header);
            WAVEWriter.Write(FileSize);
            WAVEWriter.Write(WAVHeader);
            WAVEWriter.Write(fmtHeader);
            WAVEWriter.Write(SubChunk1Size);
            WAVEWriter.Write(Format);
            WAVEWriter.Write(Channels);
            WAVEWriter.Write(SampleRate);
            WAVEWriter.Write(BitRate);
            WAVEWriter.Write(Align);
            WAVEWriter.Write(BPS);
            WAVEWriter.Write(SubChunk2Id);
            WAVEWriter.Write(SoundSize);
        }
        private void Convert(uint index)
        {
            if (!(Sounds[index].Type == 2))
            {
                SaveWAV.FileName = Sounds[index].Name;
                if (SaveWAV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.MemoryStream SoundStream = new System.IO.MemoryStream();
                    System.IO.BinaryWriter StreamWriter = new System.IO.BinaryWriter(SoundStream);
                    Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                    SoundStream.Position = 44;
                    if (Sounds[index].Type == 0)
                    {
                        System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                        System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                        System.IO.BinaryWriter ADPCMWriter = new System.IO.BinaryWriter(ADPCM);
                        MB.Position = Sounds[index].Offset + 48;
                        ADPCMWriter.Write(MBReader.ReadBytes((int)Sounds[index].Size));
                        ADPCM.Position = 0;
                        ADPCM_Device.ADPCM2PCM(ADPCM, ref PCM);
                        SoundStream.Position = 0;
                        WriteHeader(ref SoundStream, Sounds[index].SampleRate, 1, (uint)PCM.Length);
                        StreamWriter.Write(PCM.ToArray());
                    }
                    else
                    {
                        System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                        System.IO.MemoryStream ADPCM_R = new System.IO.MemoryStream();
                        System.IO.MemoryStream ADPCM_L = new System.IO.MemoryStream();
                        System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                        System.IO.MemoryStream PCM_R = new System.IO.MemoryStream();
                        System.IO.MemoryStream PCM_L = new System.IO.MemoryStream();
                        System.IO.BinaryWriter ADPCMWriter = new System.IO.BinaryWriter(ADPCM);
                        System.IO.BinaryWriter PCMWriter = new System.IO.BinaryWriter(PCM);
                        MB.Position = Sounds[index].Offset;
                        ADPCMWriter.Write(MBReader.ReadBytes((int)Sounds[index].Size));
                        ADPCM.Position = 0;
                        ADPCM_Device.ADPCM_Demux(ADPCM, ref ADPCM_R, ref ADPCM_L, Interleave);
                        ADPCM_R.Position = 0;
                        PCM_R.Position = 0;
                        ADPCM_L.Position = 0;
                        PCM_L.Position = 0;
                        ADPCM_Device.ADPCM2PCM(ADPCM_R, ref PCM_R);
                        ADPCM_Device.ADPCM2PCM(ADPCM_L, ref PCM_L);
                        ADPCM_Device.PCM_Mux(ref PCM, PCM_R, PCM_L);
                        SoundStream.Position = 0;
                        WriteHeader(ref SoundStream, Sounds[index].SampleRate, 2, (uint)PCM.Length);
                        StreamWriter.Write(PCM.ToArray());
                    }
                    SoundStream.Position = 0;
                    System.IO.FileStream File = new System.IO.FileStream(SaveWAV.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    System.IO.BinaryWriter FileWriter = new System.IO.BinaryWriter(File);
                    FileWriter.Write(SoundStream.ToArray());
                    FileWriter.Close();
                    File.Close();
                }
            }
            else
                Interaction.MsgBox("No data to play");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            LoadBuffer((uint)TreeView1.SelectedNode.Index);
        }
        private struct Record
        {
            public uint Type;
            public uint Size;
            public uint Offset;
            public uint SampleRate;
            public uint Skip;
            public string Name;
            public bool External;
            public System.IO.MemoryStream Stream;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AL.SourcePlay(SoundSource);
            Playing = true;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            if (AL.GetSourceState(SoundSource) == ALSourceState.Playing)
                AL.Source(SoundSource, ALSourcef.SecOffset, TrackBar1.Value);
                //SoundBuffer.CurrentPosition = TrackBar1.Value;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AL.SourcePause(SoundSource);
            Playing = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            /*SoundBuffer.Stop();
            SoundBuffer.CurrentPosition = 0;
            Playing = false;
            int WP = 0;
            int playPos = 0;
            SoundBuffer.GetCurrentPosition(out playPos, out WP);
            TrackBar1.Value = playPos;
            SharpDX.Multimedia.WaveFormat[] waveFormats = new SharpDX.Multimedia.WaveFormat[] { };
            var NULL = 0;
            SoundBuffer.GetFormat(waveFormats, 0, out NULL);
            Label9.Text = ToTime((uint)(TrackBar1.Value / waveFormats[0].AverageBytesPerSecond));*/
            AL.SourceStop(SoundSource);
            AL.BindBufferToSource(SoundSource, 0);
            AL.DeleteBuffer(SoundBuffer);
            SoundBuffer = 0;
            Label9.Text = "0:00";
        }

        private void UpdateBar_Tick(object sender, EventArgs e)
        {
            if (AL.GetSourceState(SoundSource) == ALSourceState.Playing)
            {
                int playPos = 0;
                //SoundBuffer.GetCurrentPosition(out playPos, out WP);
                AL.GetSource(SoundSource, ALGetSourcei.ByteOffset, out playPos);
                TrackBar1.Value = playPos;
                int freq = 0, size = 0;
                AL.GetBuffer(SoundBuffer, ALGetBufferi.Frequency, out freq);
                AL.GetBuffer(SoundBuffer, ALGetBufferi.Size, out size);
                Label9.Text = ToTime((uint)(TrackBar1.Value / (size/freq)));
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Convert((uint)TreeView1.SelectedNode.Index);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (OpenWAV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Array.Resize(ref Sounds, Sounds.Length + 1);
                SoundCount += 1;
                System.IO.FileStream WAVFile = new System.IO.FileStream(OpenWAV.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader WAVReader = new System.IO.BinaryReader(WAVFile);
                char[] Header;
                int FileSize;
                char[] WAVHeader;
                char[] fmtHeader;
                int SubChunk1Size;
                Int16 Format;
                Int16 Chanells;
                uint SampleRate;
                uint BitRate;
                UInt16 Align;
                UInt16 BPS;
                char[] SubChunk2Id;
                int SubChunk2Size;
                Header = WAVReader.ReadChars(4);
                FileSize = WAVReader.ReadInt32();
                WAVHeader = WAVReader.ReadChars(4);
                fmtHeader = WAVReader.ReadChars(4);
                SubChunk1Size = WAVReader.ReadInt32();
                Format = WAVReader.ReadInt16();
                Chanells = WAVReader.ReadInt16();
                SampleRate = WAVReader.ReadUInt32();
                BitRate = WAVReader.ReadUInt32();
                Align = WAVReader.ReadUInt16();
                BPS = WAVReader.ReadUInt16();
                SubChunk2Id = WAVReader.ReadChars(4);
                SubChunk2Size = WAVReader.ReadInt32();
                System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                System.IO.BinaryWriter PCMWriter = new System.IO.BinaryWriter(PCM);
                PCMWriter.Write(WAVReader.ReadBytes(SubChunk2Size));
                Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                if (Chanells == 2)
                {
                    System.IO.MemoryStream PCML = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCMR = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCML = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCMR = new System.IO.MemoryStream();
                    ADPCM_Device.PCM_Demux(PCM, ref PCMR, ref PCML);
                    PCMR.Position = 0;
                    PCML.Position = 0;
                    ADPCM_Device.PCM2ADPCM(ref ADPCMR, PCMR);
                    ADPCM_Device.PCM2ADPCM(ref ADPCML, PCML);
                    ADPCM_Device.ADPCM_Mux(ref ADPCM, ADPCMR, ADPCML, Interleave);
                    Sounds[Sounds.Length - 1].Type = 1;
                }
                else if (Chanells == 1)
                {
                    PCM.Position = 0;
                    ADPCM_Device.PCM2ADPCM(ref ADPCM, PCM);
                    Sounds[Sounds.Length - 1].Type = 0;
                }
                Sounds[Sounds.Length - 1].Size = (uint)ADPCM.Length;
                Sounds[Sounds.Length - 1].Offset = 0;
                Sounds[Sounds.Length - 1].SampleRate = SampleRate;
                Sounds[Sounds.Length - 1].Skip = 0;
                Sounds[Sounds.Length - 1].External = true;
                Sounds[Sounds.Length - 1].Name = "External";
                Sounds[Sounds.Length - 1].Stream = new System.IO.MemoryStream();
                Sounds[Sounds.Length - 1].Stream.Write(ADPCM.ToArray(), 0, (int)ADPCM.Length);
                BuildTree();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            int index = TreeView1.SelectedNode.Index;
            if (OpenWAV.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream WAVFile = new System.IO.FileStream(OpenWAV.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader WAVReader = new System.IO.BinaryReader(WAVFile);
                char[] Header;
                int FileSize;
                char[] WAVHeader;
                char[] fmtHeader;
                int SubChunk1Size;
                Int16 Format;
                Int16 Chanells;
                uint SampleRate;
                uint BitRate;
                UInt16 Align;
                UInt16 BPS;
                char[] SubChunk2Id;
                int SubChunk2Size;
                Header = WAVReader.ReadChars(4);
                FileSize = WAVReader.ReadInt32();
                WAVHeader = WAVReader.ReadChars(4);
                fmtHeader = WAVReader.ReadChars(4);
                SubChunk1Size = WAVReader.ReadInt32();
                Format = WAVReader.ReadInt16();
                Chanells = WAVReader.ReadInt16();
                SampleRate = WAVReader.ReadUInt32();
                BitRate = WAVReader.ReadUInt32();
                Align = WAVReader.ReadUInt16();
                BPS = WAVReader.ReadUInt16();
                SubChunk2Id = WAVReader.ReadChars(4);
                SubChunk2Size = WAVReader.ReadInt32();
                System.IO.MemoryStream ADPCM = new System.IO.MemoryStream();
                System.IO.MemoryStream PCM = new System.IO.MemoryStream();
                System.IO.BinaryWriter PCMWriter = new System.IO.BinaryWriter(PCM);
                PCMWriter.Write(WAVReader.ReadBytes(SubChunk2Size));
                Twinsanity.ADPCM ADPCM_Device = new Twinsanity.ADPCM();
                if (Chanells == 2)
                {
                    System.IO.MemoryStream PCML = new System.IO.MemoryStream();
                    System.IO.MemoryStream PCMR = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCML = new System.IO.MemoryStream();
                    System.IO.MemoryStream ADPCMR = new System.IO.MemoryStream();
                    ADPCM_Device.PCM_Demux(PCM, ref PCMR, ref PCML);
                    PCMR.Position = 0;
                    PCML.Position = 0;
                    ADPCM_Device.PCM2ADPCM(ref ADPCMR, PCMR);
                    ADPCM_Device.PCM2ADPCM(ref ADPCML, PCML);
                    ADPCM_Device.ADPCM_Mux(ref ADPCM, ADPCMR, ADPCML, Interleave);
                    Sounds[index].Type = 1;
                }
                else if (Chanells == 1)
                {
                    PCM.Position = 0;
                    ADPCM_Device.PCM2ADPCM(ref ADPCM, PCM);
                    Sounds[index].Type = 0;
                }
                Sounds[index].Size = (uint)ADPCM.Length;
                Sounds[index].Offset = 0;
                Sounds[index].SampleRate = SampleRate;
                Sounds[index].Skip = 0;
                Sounds[index].External = true;
                Sounds[index].Name = "External";
                Sounds[index].Stream = new System.IO.MemoryStream();
                Sounds[index].Stream.Write(ADPCM.ToArray(), 0, (int)ADPCM.Length);
                BuildTree();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            int index = TreeView1.SelectedNode.Index;
            for (int i = TreeView1.SelectedNode.Index + 1; i <= Sounds.Length - 1; i++)
                Sounds[i - 1] = Sounds[i];
            Array.Resize(ref Sounds, Sounds.Length - 1);
            SoundCount -= 1;
            TreeView1.SelectedNode = TreeView1.Nodes[index];
            BuildTree();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (SaveMBH.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileStream NewMH = new System.IO.FileStream(SaveMBH.FileName + ".MH", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.FileStream NewMB = new System.IO.FileStream(SaveMBH.FileName + ".MB", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.BinaryWriter NewMHWriter = new System.IO.BinaryWriter(NewMH);
                System.IO.BinaryWriter NewMBWriter = new System.IO.BinaryWriter(NewMB);
                NewMHWriter.Write(SoundCount);
                NewMHWriter.Write(Interleave);
                uint undefined_offset = 0;
                bool found = false;
                for (int i = 0; i <= Sounds.Length - 1; i++)
                {
                    if (!Sounds[i].External)
                    {
                        if (Sounds[i].Name.Contains("undefined"))
                        {
                            if (!found)
                            {
                                undefined_offset = (uint)NewMB.Position;
                                found = true;
                                MB.Position = Sounds[i].Offset;
                                NewMBWriter.Write(MBReader.ReadBytes((int)Sounds[i].Size));
                                Sounds[i].Offset = undefined_offset;
                                NewMHWriter.Write(Sounds[i].Type);
                                NewMHWriter.Write(Sounds[i].Size);
                                NewMHWriter.Write(Sounds[i].Offset);
                                NewMHWriter.Write(Sounds[i].SampleRate);
                                NewMHWriter.Write(Sounds[i].Skip);
                            }
                            else
                            {
                                Sounds[i].Offset = undefined_offset;
                                NewMHWriter.Write(Sounds[i].Type);
                                NewMHWriter.Write(Sounds[i].Size);
                                NewMHWriter.Write(Sounds[i].Offset);
                                NewMHWriter.Write(Sounds[i].SampleRate);
                                NewMHWriter.Write(Sounds[i].Skip);
                            }
                        }
                        else
                        {
                            uint offset = (uint)NewMB.Position;
                            MB.Position = Sounds[i].Offset;
                            NewMBWriter.Write(MBReader.ReadBytes((int)Sounds[i].Size));
                            Sounds[i].Offset = offset;
                            if (Sounds[i].Type == 2)
                            {
                                Sounds[i].Size = 0;
                                Sounds[i].Offset = 0;
                            }
                            NewMHWriter.Write(Sounds[i].Type);
                            NewMHWriter.Write(Sounds[i].Size);
                            NewMHWriter.Write(Sounds[i].Offset);
                            NewMHWriter.Write(Sounds[i].SampleRate);
                            NewMHWriter.Write(Sounds[i].Skip);
                        }
                    }
                    else if (Sounds[i].Type == 0)
                    {
                        Sounds[i].Size = (uint)Sounds[i].Stream.Length + 48;
                        char[] Head = new[] { 'M', 'S', 'V', 'p' };
                        byte[] Version = new byte[] { 0, 0, 0, 32 };
                        uint Null = 0;
                        byte[] SZ = new byte[] { (byte)(((Sounds[i].Size - 48) & 4278190080) >> 24), (byte)(((Sounds[i].Size - 48) & 16711680) >> 16), (byte)(((Sounds[i].Size - 48) & 65280) >> 8), (byte)((Sounds[i].Size - 48) & 255) };
                        byte[] SR = new byte[] { (byte)((Sounds[i].SampleRate & 4278190080) >> 24), (byte)((Sounds[i].SampleRate & 16711680) >> 16), (byte)((Sounds[i].SampleRate & 65280) >> 8), (byte)(Sounds[i].SampleRate & 255) };
                        // Null
                        // Null
                        // Null
                        char[] Name = new char[16];
                        for (int j = 0; j <= 15; j++)
                        {
                            if (j + 1 <= Sounds[i].Name.Length)
                                Name[j] = Sounds[i].Name[j];
                            else
                                Name[j] = Strings.Chr(0);
                        }
                        uint offset = (uint)NewMB.Position;
                        MB.Position = Sounds[i].Offset;
                        NewMBWriter.Write(Head);
                        NewMBWriter.Write(Version);
                        NewMBWriter.Write(Null);
                        NewMBWriter.Write(SZ);
                        NewMBWriter.Write(SR);
                        NewMBWriter.Write(Null);
                        NewMBWriter.Write(Null);
                        NewMBWriter.Write(Null);
                        NewMBWriter.Write(Name);
                        NewMBWriter.Write(Sounds[i].Stream.ToArray());
                        Sounds[i].Offset = offset;
                        NewMHWriter.Write(Sounds[i].Type);
                        NewMHWriter.Write(Sounds[i].Size);
                        NewMHWriter.Write(Sounds[i].Offset);
                        NewMHWriter.Write(Sounds[i].SampleRate);
                        NewMHWriter.Write(Sounds[i].Skip);
                    }
                    else if (Sounds[i].Type == 1)
                    {
                        uint offset = (uint)NewMB.Position;
                        MB.Position = Sounds[i].Offset;
                        NewMBWriter.Write(Sounds[i].Stream.ToArray());
                        Sounds[i].Offset = offset;
                        NewMHWriter.Write(Sounds[i].Type);
                        NewMHWriter.Write(Sounds[i].Size);
                        NewMHWriter.Write(Sounds[i].Offset);
                        NewMHWriter.Write(Sounds[i].SampleRate);
                        NewMHWriter.Write(Sounds[i].Skip);
                    }
                }
                NewMBWriter.Close();
                NewMHWriter.Close();
                NewMH.Close();
                NewMB.Close();
            }
        }

        private void MHWorker_Load(object sender, EventArgs e)
        {
        }

        private void MHWorker_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            MH.Close();
            MB.Close();
        }
    }
}
