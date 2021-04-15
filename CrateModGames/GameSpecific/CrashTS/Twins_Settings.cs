using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.ModProperties;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS
{
    [ModCategory((int)ModProps.Misc)]
    static class Twins_Settings
    {

        //public static ModPropString ArchiveName = new ModPropString("Crash");
        public static ModPropString StartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x17);
        public static ModPropString CreditsChunk = new ModPropString(@"Levels\Ice\Hub\LabExt", 0x22); //0x1C
        //public static ModPropString UnsafeStartingChunk = new ModPropString(@"Levels\Earth\Hub\Beach", 0x2D);
        [ModMenuOnly]
        public static ModPropOption Option_SwapStartAndCreditsChunk = new ModPropOption(Twins_Text.Mod_SwapStartAndCreditsChunk, Twins_Text.Mod_SwapStartAndCreditsChunkDesc);
        [ModMenuOnly]
        public static ModPropOption Option_StartAndCreditsSpawn = new ModPropOption(Twins_Text.Mod_StartAndCreditsSpawn, Twins_Text.Mod_StartAndCreditsSpawnDesc);

        //EXE patching support based on Twinsanity Editor code
        internal struct ExecutablePatchInfo
        {
            internal int LevelOff;
            internal int LevelSize;
            internal int ArchiveOff;
            internal int ArchiveSize;
            internal int CreditsLevelOff;
            internal int CreditsLevelSize;
            internal int StartLevelPathPatchOff; // pointer to Start Chunk Path
            internal int StartLevelPathPatchOff2; // pointer to Credits Chunk Path
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

        private static readonly Dictionary<ExecutableIndex, ExecutablePatchInfo> executables = new Dictionary<ExecutableIndex, ExecutablePatchInfo>()
        {
            [ExecutableIndex.PAL] = new ExecutablePatchInfo() {
                LevelOff = 0x1F6708, LevelSize = 0x17,
                ArchiveOff = 0x1ED410, ArchiveSize = 0x7,
                CreditsLevelOff = 0x1F6720, CreditsLevelSize = 0x22,
                StartLevelPathPatchOff = 0x79938, StartLevelPathPatchOff2 = 0x79958,
            },
            [ExecutableIndex.NTSCU] = new ExecutablePatchInfo() {
                LevelOff = 0x1F5E28, LevelSize = 0x17,
                ArchiveOff = 0x1ECB10, ArchiveSize = 0x7,
                CreditsLevelOff = 0x1F5E40, CreditsLevelSize = 0x22,
                StartLevelPathPatchOff = 0x79748, StartLevelPathPatchOff2 = 0x79768,
            },
            [ExecutableIndex.NTSCU2] = new ExecutablePatchInfo() {
                LevelOff = 0x1F63A8, LevelSize = 0x17,
                ArchiveOff = 0x1ED090, ArchiveSize = 0x7,
                CreditsLevelOff = 0x1F63C0, CreditsLevelSize = 0x22,
                StartLevelPathPatchOff = 0x79820, StartLevelPathPatchOff2 = 0x79840,
            },
            [ExecutableIndex.NTSCJ] = new ExecutablePatchInfo() {
                LevelOff = 0x1F6648, LevelSize = 0x17,
                ArchiveOff = 0x1ED310, ArchiveSize = 0x7,
                CreditsLevelOff = 0x1F6660, CreditsLevelSize = 0x22,
                StartLevelPathPatchOff = 0x79978, StartLevelPathPatchOff2 = 0x79998,
            },
            [ExecutableIndex.XBOX_NTSC] = new ExecutablePatchInfo() {
                LevelOff = 0x368870, LevelSize = 0x17,
                ArchiveOff = 0x1ED310, ArchiveSize = 0x7,
                CreditsLevelOff = 0x36888C, CreditsLevelSize = 0x1D,
                StartLevelPathPatchOff = 0x265200, StartLevelPathPatchOff2 = 0x265230,
            },
            [ExecutableIndex.XBOX_PAL] = new ExecutablePatchInfo() {
                LevelOff = 0x368858, LevelSize = 0x17,
                ArchiveOff = 0x1ED310, ArchiveSize = 0x7,
                CreditsLevelOff = 0x368874, CreditsLevelSize = 0x1D,
                StartLevelPathPatchOff = 0x265110, StartLevelPathPatchOff2 = 0x265140,
            },
        };

        public static void PatchEXE(ConsoleMode console, RegionType region, string basePath, string execName, string StartChunk = @"Levels\Earth\Hub\Beach")
        {
            string filePath = Path.Combine(basePath, execName);

            ExecutablePatchInfo executable;
            if (console == ConsoleMode.XBOX)
            {
                if (region == RegionType.PAL)
                {
                    executable = executables[ExecutableIndex.XBOX_PAL];
                }
                else if (region == RegionType.NTSC_U)
                {
                    executable = executables[ExecutableIndex.XBOX_NTSC];
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (region == RegionType.NTSC_U)
                {
                    using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                    {
                        reader.BaseStream.Position = executables[ExecutableIndex.NTSCU].ArchiveOff;
                        char ch = reader.ReadChar();

                        if (ch == 'C')
                        {
                            executable = executables[ExecutableIndex.NTSCU];
                        }
                        else
                        {
                            executable = executables[ExecutableIndex.NTSCU2];
                        }
                    }
                }
                else if (region == RegionType.PAL)
                {
                    executable = executables[ExecutableIndex.PAL];
                }
                else if (region == RegionType.NTSC_J)
                {
                    executable = executables[ExecutableIndex.NTSCJ];
                }
                else
                {
                    return;
                }
            }

            uint StartChunkPointer = 0;
            uint CreditsChunkPointer = 0;

            using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                reader.BaseStream.Position = executable.StartLevelPathPatchOff;
                StartChunkPointer = reader.ReadUInt32();

                reader.BaseStream.Position = executable.StartLevelPathPatchOff2;
                CreditsChunkPointer = reader.ReadUInt32();
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
            if (StartingChunk.HasChanged)
            {
                levelName = StartingChunk.Value;

                writer.BaseStream.Position = executable.LevelOff;
                while (writer.BaseStream.Position < executable.LevelOff + LevelSize)
                    writer.Write((byte)0);
                writer.BaseStream.Position = executable.LevelOff;
                for (int i = 0; i < LevelSize && writer.BaseStream.Position < executable.LevelOff + LevelSize && i < levelName.Length; ++i)
                {
                    writer.Write(levelName[i]);
                }
            }

            if (CreditsChunk.HasChanged)
            {
                LevelSize = executable.CreditsLevelSize;
                levelName = CreditsChunk.Value;

                writer.BaseStream.Position = executable.CreditsLevelOff;
                while (writer.BaseStream.Position < executable.CreditsLevelOff + LevelSize)
                    writer.Write((byte)0);
                writer.BaseStream.Position = executable.CreditsLevelOff;
                for (int i = 0; i < LevelSize && writer.BaseStream.Position < executable.CreditsLevelOff + LevelSize && i < levelName.Length; ++i)
                {
                    writer.Write(levelName[i]);
                }
            }

            if (Option_SwapStartAndCreditsChunk.Enabled)
            {
                writer.BaseStream.Position = executable.StartLevelPathPatchOff;
                writer.Write(CreditsChunkPointer);
                writer.BaseStream.Position = executable.StartLevelPathPatchOff2;
                writer.Write(StartChunkPointer);
            }
            if (Option_StartAndCreditsSpawn.Enabled)
            {
                writer.BaseStream.Position = executable.StartLevelPathPatchOff;
                writer.Write(CreditsChunkPointer);
            }


            writer.Close();
        }

    }
}
