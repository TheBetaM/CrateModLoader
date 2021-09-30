using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;
using TwinsaityEditor.Properties;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class MHViewer : Form
    {
        private const uint msvp_key = 0x7056534D; //"MSVp" as a little endian value
        private readonly string[] type_names = { "mono MSVp", "stereo", "null" };

        private string fileName;
        private string mb_name;
        private int interleave;
        private List<Track> tracks;

        private SoundPlayer player;
        private byte[] sndData;
        private Track curTrack;
        private string curName;

        private Timer seekTimer;
        private Stopwatch seekWatch;

        public MHViewer()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Settings.Default.MHFilePath,
                Filter = "Music Header|*.MH"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.MHFilePath = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf('\\'));
                fileName = ofd.FileName;
                tracks = new List<Track>();
                InitializeComponent();
                PopulateList();
                splitContainer1.SplitterDistance = (int)(Width * 0.35F);
                player = new SoundPlayer();
                FormClosed += delegate (object sender, FormClosedEventArgs e)
                {
                    player.Stop();
                };
                seekTimer = new Timer
                {
                    Interval = 1000 / 50,
                    Enabled = false
                };

                seekTimer.Tick += delegate (object sender, EventArgs e)
                {
                    double length = 0;
                    if (curTrack.Type == 0)
                        length = (curTrack.Size - 0x30) / 0x10 * 28 / (double)curTrack.SampleRate;
                    else if (curTrack.Type == 1)
                        length = curTrack.Size / 0x20 * 28 / (double)curTrack.SampleRate;
                    double current = seekWatch.ElapsedMilliseconds / 1000.0;
                    if (current > length)
                    {
                        seekTimer.Enabled = false;
                        label7.Text = "Now Playing:\n0:00 / 0:00";
                        current = 0;
                    }
                    else
                        label7.Text = string.Format("Now Playing: {0}\n{1}:{2:00} / {3}:{4:00}", curName, Math.Floor(current / 60), (int)(((current / 60.0) - Math.Floor(current / 60.0)) * 60), Math.Floor(length / 60), (int)(((length / 60.0) - Math.Floor(length / 60.0)) * 60));
                    label7.Invalidate();
                    trackBar1.Value = (int)(current * 1000);
                    trackBar1.Maximum = (int)(length * 1000);
                };
                Show();
            }
            else
                Close();
        }

        private void PopulateList()
        {
            listBox1.Items.Clear();
            tracks.Clear();
            BinaryReader mh = new BinaryReader(new FileStream(fileName, FileMode.Open));
            int count = mh.ReadInt32();
            interleave = mh.ReadInt32();
            while (count-- > 0)
            {
                tracks.Add(new Track()
                {
                    Type = mh.ReadInt32(),
                    Size = mh.ReadInt32(),
                    Offset = mh.ReadUInt32(),
                    SampleRate = mh.ReadInt32(),
                    Unknown = mh.ReadInt32()
                });
            }
            mh.Close();
            mb_name = fileName.Substring(0, fileName.LastIndexOf('.')+1) + "MB";
            BinaryReader mb = new BinaryReader(new FileStream(mb_name, FileMode.Open));
            for (int i = 0; i < tracks.Count; ++i)
            {
                mb.BaseStream.Position = tracks[i].Offset;
                if (tracks[i].Type == 0)
                {
                    byte[] header = mb.ReadBytes(0x30);
                    if (BitConverter.ToUInt32(header, 0) != msvp_key
                        || BitConverter.ToUInt32(header, 4) != 0x20000000
                        || BitConverter.ToUInt32(header, 8) != 0
                        || BitConv.FlipBytes(BitConverter.ToUInt32(header, 12)) != tracks[i].Size - 0x30)
                    {
                        throw new Exception("Type 0 audio stream is in invalid format.");
                    }
                    else if (BitConv.FlipBytes(BitConverter.ToInt32(header, 16)) != tracks[i].SampleRate)
                        tracks[i].SampleRate = BitConv.FlipBytes(BitConverter.ToInt32(header, 16));
                    char[] name = new char[0x10];
                    Array.Copy(header, 0x20, name, 0, 0x10);
                    tracks[i].Name = new string(name);
                    if (tracks[i].Name.IndexOf('\0') > 0)
                    {
                        tracks[i].Name = tracks[i].Name.Substring(0, tracks[i].Name.IndexOf('\0'));
                        listBox1.Items.Add($"{tracks[i].Name} (Track {i})");
                    }
                    else
                    {
                        tracks[i].Name = string.Empty;
                        listBox1.Items.Add($"Track {i}");
                    }
                }
                else if (tracks[i].Type == 2)
                    listBox1.Items.Add($"Track {i} (null track)");
                else
                    listBox1.Items.Add($"Track {i}");
            }
            mb.Close();
            label1.Text = $"Track count: {tracks.Count}";
            label10.Text = $"Interleave: {interleave} bytes";
        }

        private class Track
        {
            public int Type;
            public int Size;
            public uint Offset;
            public int SampleRate;
            public int Unknown;
            public string Name;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Track selTrack = tracks[listBox1.SelectedIndex];
            groupBox1.Enabled = true;
            label2.Text = $"Type: {selTrack.Type} ({type_names[selTrack.Type]})";
            label3.Text = $"Name (type 0 only): {selTrack.Name}";
            label4.Text = $"Offset: {selTrack.Offset} bytes";
            label5.Text = $"Size: {selTrack.Size} bytes";
            label6.Text = $"Sample rate: {selTrack.SampleRate}Hz";
            label9.Text = $"Unknown: {selTrack.Unknown}";
            double seconds = 0;
            if (selTrack.Type == 0)
            {
                seconds = (selTrack.Size - 0x30) / 0x10 * 28 / selTrack.SampleRate;
            }
            else if (selTrack.Type == 1)
            {
                seconds = selTrack.Size / 0x20 * 28 / selTrack.SampleRate;
            }
            label8.Text = string.Format("Length: {0}:{1:00}", Math.Floor(seconds / 60), (int)(((seconds / 60.0) - Math.Floor(seconds / 60.0)) * 60));
            button1.Enabled = button3.Enabled = selTrack.Type != 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tracks[listBox1.SelectedIndex].Type == 2) return;
            player.Stop();
            player.Stream = null;
            button3.Enabled = trackBar1.Enabled = false;
            curTrack = tracks[listBox1.SelectedIndex];
            BinaryReader mb = new BinaryReader(new FileStream(mb_name, FileMode.Open));
            mb.BaseStream.Position = curTrack.Offset + (curTrack.Type == 0 ? 0x30 : 0);
            byte[] rawData = mb.ReadBytes(curTrack.Type == 0 ? curTrack.Size - 0x30 : curTrack.Size);
            mb.Close();
            sndData = curTrack.Type == 1 ? RIFF.SaveRiff(ADPCM.ToPCMStereo(rawData, rawData.Length, interleave), 2, curTrack.SampleRate) : RIFF.SaveRiff(ADPCM.ToPCMMono(rawData, rawData.Length), 1, curTrack.SampleRate);
            player.Stream = new MemoryStream(sndData);
            player.Play();
            seekWatch = Stopwatch.StartNew();
            curName = (string)listBox1.SelectedItem;
            button3.Enabled = button4.Enabled = trackBar1.Enabled = seekTimer.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.Stop();
            player.Stream = null;
            trackBar1.Value = 0;
            trackBar1.Enabled = seekTimer.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Track selTrack = tracks[listBox1.SelectedIndex];
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "WAV|*.wav";
            sfd.FileName = selTrack.Type != 0 ? $"{listBox1.SelectedIndex}" : $"{listBox1.SelectedIndex} - {selTrack.Name}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                BinaryWriter writer = new BinaryWriter(new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write));
                BinaryReader mb = new BinaryReader(new FileStream(mb_name, FileMode.Open));
                mb.BaseStream.Position = curTrack.Offset + (curTrack.Type == 0 ? 0x30 : 0);
                byte[] rawData = mb.ReadBytes(curTrack.Type == 0 ? curTrack.Size - 0x30 : curTrack.Size);
                mb.Close();
                writer.Write(curTrack.Type == 1 ? RIFF.SaveRiff(ADPCM.ToPCMStereo(rawData, rawData.Length, interleave), 2, curTrack.SampleRate) : RIFF.SaveRiff(ADPCM.ToPCMMono(rawData, rawData.Length), 1, curTrack.SampleRate));
                writer.Close();
            }
        }

        private void addTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "WAV files|*.wav";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryReader reader = new BinaryReader(new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read));
                if (new string(reader.ReadChars(4)) != "RIFF"
                    || reader.ReadInt32() != reader.BaseStream.Length - 8
                    || new string(reader.ReadChars(4)) != "WAVE"
                    || new string(reader.ReadChars(4)) != "fmt "
                    || reader.ReadInt32() != 16
                    || reader.ReadInt16() != 1)
                    return;
                ushort channels = reader.ReadUInt16();
                int samplerate = reader.ReadInt32();
                if (channels > 2 ||
                    reader.ReadInt32() != samplerate * channels * 2 ||
                    reader.ReadInt16() != channels * 2 ||
                    reader.ReadInt16() != 16 ||
                    new string(reader.ReadChars(4)) != "data")
                    return;
                BinaryWriter mb = new BinaryWriter(new FileStream(mb_name, FileMode.Append, FileAccess.Write));
                long mb_start_seek = mb.BaseStream.Position;
                if (mb_start_seek > uint.MaxValue)
                    return;
                int readsize = reader.ReadInt32();
                byte[] vag_data;
                if (channels == 1)
                {
                    vag_data = ADPCM.FromPCMMono(reader.ReadBytes(readsize));
                    mb.Write("MSVp".ToCharArray());
                    mb.Write(0x20000000);
                    mb.Write(0);
                    mb.Write(BitConv.FlipBytes(vag_data.Length));
                    mb.Write(BitConv.FlipBytes(samplerate));
                    mb.Write(0);
                    mb.Write(0);
                    mb.Write(0);
                    string fname_no_ext = ofd.SafeFileName.Substring(0, ofd.SafeFileName.LastIndexOf('.'));
                    for (int i = 0; i < 16; ++i)
                    {
                        if (i < fname_no_ext.Length)
                            mb.Write(fname_no_ext[i]);
                        else
                            mb.Write(0);
                    }
                    mb.Write(vag_data);
                }
                else if (channels == 2)
                {
                    vag_data = ADPCM.FromPCMStereo(reader.ReadBytes(readsize), interleave);
                    mb.Write(vag_data);
                }
            }
        }
    }
}
