using System;
using System.Windows.Forms;
using System.IO;
using TwinsaityEditor.Properties;

namespace TwinsaityEditor
{
    public partial class EXEPatcher : Form
    {
        private string fileName;

        internal struct ExecutablePatchInfo
        {
            internal int LevelOff;
            internal int LevelSize;
            internal int ArchiveOff;
            internal int ArchiveSize;
        }

        private enum ExecutableIndex
        {
            Invalid = -1, // force enum start, do not use
            PAL,
            NTSCU,
            NTSCU2,
            NTSCJ
        }

        private readonly ExecutablePatchInfo[] executables = new ExecutablePatchInfo[]
        {
            new ExecutablePatchInfo() { LevelOff = 0x1F6708, LevelSize = 0x17, ArchiveOff = 0x1ED410, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F5E28, LevelSize = 0x17, ArchiveOff = 0x1ECB10, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F63A8, LevelSize = 0x17, ArchiveOff = 0x1ED090, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F6648, LevelSize = 0x17, ArchiveOff = 0x1ED310, ArchiveSize = 0x7 }
        };

        private ExecutablePatchInfo executable;

        public EXEPatcher()
        {
            InitializeComponent();
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = Settings.Default.ExePatcherPath,
                Filter = "NTSC-U executable|SLUS_209.09|NTSC-U 2.0 executable|SLUS_209.09|PAL executable|SLES_525.68|NTSC-J executable|SLPM_658.01"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.ExePatcherPath = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf('\\'));
                fileName = ofd.FileName;
                switch (ofd.FilterIndex)
                {
                    case 1:
                        LoadEXE(executables[(int)ExecutableIndex.NTSCU]);
                        break;
                    case 2:
                        LoadEXE(executables[(int)ExecutableIndex.NTSCU2]);
                        break;
                    case 3:
                        LoadEXE(executables[(int)ExecutableIndex.PAL]);
                        break;
                    case 4:
                        LoadEXE(executables[(int)ExecutableIndex.NTSCJ]);
                        break;
                }
                Show();
            }
            else
                Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox2.Checked;
        }

        private void LoadEXE(ExecutablePatchInfo executable)
        {
            this.executable = executable;
            BinaryReader reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            textBox1.MaxLength = executable.ArchiveSize;
            textBox2.MaxLength = executable.LevelSize;
            reader.BaseStream.Position = executable.ArchiveOff;
            char ch = '\0';
            do
            {
                ch = reader.ReadChar();
                textBox1.Text += ch;
            }
            while (ch != '\0');
            reader.BaseStream.Position = executable.LevelOff;
            do
            {
                ch = reader.ReadChar();
                textBox2.Text += ch;
            }
            while (ch != '\0');
            reader.Close();
        }

        private void PatchEXE(ExecutablePatchInfo executable)
        {
            label1.Visible = true;
            label1.Text = "Patching...";
            BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Open, FileAccess.Write));
            if (checkBox1.Checked)
            {
                writer.BaseStream.Position = executable.ArchiveOff;
                while (writer.BaseStream.Position < executable.ArchiveOff + executable.ArchiveSize)
                    writer.Write((byte)0);
                writer.BaseStream.Position = executable.ArchiveOff;
                for (int i = 0; i < executable.ArchiveSize && writer.BaseStream.Position < executable.ArchiveOff + executable.ArchiveSize && i < textBox1.Text.Length; ++i)
                {
                    writer.Write(textBox1.Text[i]);
                }
            }
            if (checkBox2.Checked)
            {
                writer.BaseStream.Position = executable.LevelOff;
                while (writer.BaseStream.Position < executable.LevelOff + executable.LevelSize)
                    writer.Write((byte)0);
                writer.BaseStream.Position = executable.LevelOff;
                for (int i = 0; i < executable.LevelSize && writer.BaseStream.Position < executable.LevelOff + executable.LevelSize && i < textBox2.Text.Length; ++i)
                {
                    writer.Write(textBox2.Text[i]);
                }
            }
            writer.Close();
            label1.Text = "Patched!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatchEXE(executable);
        }
    }
}
