using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //todo needs testing
    public class TS_StartSpawnCredits : ModStruct<ExecutableInfo>
    {
        private Dictionary<ExecutableIndex, int> StartLevelPathOff = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x79938,
            [ExecutableIndex.NTSCU] = 0x79748,
            [ExecutableIndex.NTSCU2] = 0x79820,
            [ExecutableIndex.NTSCJ] = 0x79978,
            [ExecutableIndex.XBOX_NTSC] = 0x265200,
            [ExecutableIndex.XBOX_PAL] = 0x265110,
        };
        private Dictionary<ExecutableIndex, int> StartLevelPathOff2 = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x79958,
            [ExecutableIndex.NTSCU] = 0x79768,
            [ExecutableIndex.NTSCU2] = 0x79840,
            [ExecutableIndex.NTSCJ] = 0x79998,
            [ExecutableIndex.XBOX_NTSC] = 0x265230,
            [ExecutableIndex.XBOX_PAL] = 0x265140,
        };

        public override void ModPass(ExecutableInfo ExecFile)
        {
            uint CreditsChunkPointer = 0;

            using (FileStream file = new FileStream(ExecFile.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (BinaryReader reader = new BinaryReader(file))
                {
                    reader.BaseStream.Position = StartLevelPathOff2[ExecFile.Index];
                    CreditsChunkPointer = reader.ReadUInt32();
                }

                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    writer.BaseStream.Position = StartLevelPathOff[ExecFile.Index];
                    writer.Write(CreditsChunkPointer);
                }
            }

        }
    }
}
