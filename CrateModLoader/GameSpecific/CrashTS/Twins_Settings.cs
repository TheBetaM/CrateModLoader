using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Misc)]
    static class Twins_Settings
    {

        //public static ModPropString ArchiveName = new ModPropString("Crash");
        public static ModPropString StartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x17);
        public static ModPropString UnsafeStartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x2D);

        //EXE patching support based on Twinsanity Editor code
        internal struct ExecutablePatchInfo
        {
            internal int LevelOff;
            internal int LevelSize;
            internal int ArchiveOff;
            internal int ArchiveSize;
        }

        private enum ExecutableIndex
        {
            Invalid = -1,
            PAL,
            NTSCU,
            NTSCU2,
            NTSCJ,
            XBOX_NTSC,
            XBOX_PAL
        }

        private static readonly ExecutablePatchInfo[] executables = new ExecutablePatchInfo[]
        {
            new ExecutablePatchInfo() { LevelOff = 0x1F6708, LevelSize = 0x17, ArchiveOff = 0x1ED410, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F5E28, LevelSize = 0x17, ArchiveOff = 0x1ECB10, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F63A8, LevelSize = 0x17, ArchiveOff = 0x1ED090, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x1F6648, LevelSize = 0x17, ArchiveOff = 0x1ED310, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x368870, LevelSize = 0x17, ArchiveOff = 0x1ED310, ArchiveSize = 0x7 },
            new ExecutablePatchInfo() { LevelOff = 0x368858, LevelSize = 0x17, ArchiveOff = 0x1ED310, ArchiveSize = 0x7 },
        };

        public static void PatchEXE(string StartChunk = @"Levels\Earth\Hub\Beach")
        {
            string filePath = Path.Combine(ModLoaderGlobals.ExtractedPath, ModLoaderGlobals.ExecutableName);

            ExecutablePatchInfo executable;
            if (ModLoaderGlobals.Console == ConsoleMode.XBOX)
            {
                if (ModLoaderGlobals.Region == RegionType.PAL)
                {
                    executable = executables[(int)ExecutableIndex.XBOX_PAL];
                }
                else if (ModLoaderGlobals.Region == RegionType.NTSC_U)
                {
                    executable = executables[(int)ExecutableIndex.XBOX_NTSC];
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (ModLoaderGlobals.Region == RegionType.NTSC_U)
                {
                    using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                    {
                        reader.BaseStream.Position = executables[(int)ExecutableIndex.NTSCU].ArchiveOff;
                        char ch = reader.ReadChar();

                        if (ch == 'C')
                        {
                            executable = executables[(int)ExecutableIndex.NTSCU];
                        }
                        else
                        {
                            executable = executables[(int)ExecutableIndex.NTSCU2];
                        }
                    }
                }
                else if (ModLoaderGlobals.Region == RegionType.PAL)
                {
                    executable = executables[(int)ExecutableIndex.PAL];
                }
                else if (ModLoaderGlobals.Region == RegionType.NTSC_J)
                {
                    executable = executables[(int)ExecutableIndex.NTSCJ];
                }
                else
                {
                    return;
                }
            }

            BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Open, FileAccess.Write));
            /*
            if (ModCrates.HasSetting("ArchiveName") && ModLoaderGlobals.Console != ConsoleMode.XBOX)
            {
                string archiveName = ModCrates.GetSetting("ArchiveName");
                writer.BaseStream.Position = executable.ArchiveOff;
                while (writer.BaseStream.Position < executable.ArchiveOff + executable.ArchiveSize)
                    writer.Write((byte)0);
                writer.BaseStream.Position = executable.ArchiveOff;
                for (int i = 0; i < executable.ArchiveSize && writer.BaseStream.Position < executable.ArchiveOff + executable.ArchiveSize && i < archiveName.Length; ++i)
                {
                    writer.Write(archiveName[i]);
                }
            }
            */
            string levelName = StartChunk;

            int LevelSize = executable.LevelSize;
            if (UnsafeStartingChunk.HasChanged)
            {
                levelName = UnsafeStartingChunk.Value;
                LevelSize = 0x2D;
            }
            else if (StartingChunk.HasChanged)
            {
                levelName = StartingChunk.Value;
            }

            writer.BaseStream.Position = executable.LevelOff;
            while (writer.BaseStream.Position < executable.LevelOff + LevelSize)
                writer.Write((byte)0);
            writer.BaseStream.Position = executable.LevelOff;
            for (int i = 0; i < LevelSize && writer.BaseStream.Position < executable.LevelOff + LevelSize && i < levelName.Length; ++i)
            {
                writer.Write(levelName[i]);
            }
            writer.Close();
        }

    }
}
