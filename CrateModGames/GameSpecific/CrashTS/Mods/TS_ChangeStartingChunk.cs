using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //todo needs testing
    public class TS_ChangeStartingChunk : ModStruct<ExecutableInfo>
    {
        

        private Dictionary<ExecutableIndex, int> LevelSizes = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x17,
            [ExecutableIndex.NTSCU] = 0x17,
            [ExecutableIndex.NTSCU2] = 0x17,
            [ExecutableIndex.NTSCJ] = 0x17,
            [ExecutableIndex.XBOX_NTSC] = 0x17,
            [ExecutableIndex.XBOX_PAL] = 0x17,
        };
        private Dictionary<ExecutableIndex, int> LevelOffs = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x1F6708,
            [ExecutableIndex.NTSCU] = 0x1F5E28,
            [ExecutableIndex.NTSCU2] = 0x1F63A8,
            [ExecutableIndex.NTSCJ] = 0x1F6648,
            [ExecutableIndex.XBOX_NTSC] = 0x368870,
            [ExecutableIndex.XBOX_PAL] = 0x368858,
        };

        public override void ModPass(ExecutableInfo ExecFile)
        {
            using (FileStream file = new FileStream(ExecFile.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    string levelName = TS_Props_Misc.StartingChunk.Value;
                    int LevelSize = LevelSizes[ExecFile.Index];
                    int LevelOff = LevelOffs[ExecFile.Index];

                    writer.BaseStream.Position = LevelOff;
                    while (writer.BaseStream.Position < LevelOff + LevelSize)
                        writer.Write((byte)0);
                    writer.BaseStream.Position = LevelOff;
                    for (int i = 0; i < LevelSize && writer.BaseStream.Position < LevelOff + LevelSize && i < levelName.Length; ++i)
                    {
                        writer.Write(levelName[i]);
                    }
                }
            }
        }
    }
}
