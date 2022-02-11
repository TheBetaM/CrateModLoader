using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //todo needs testing
    public class TS_ChangeCreditsChunk : ModStruct<GenericModStruct>
    {
        

        private Dictionary<ExecutableIndex, int> CreditsLevelSizes = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x22,
            [ExecutableIndex.NTSCU] = 0x22,
            [ExecutableIndex.NTSCU2] = 0x22,
            [ExecutableIndex.NTSCJ] = 0x22,
            [ExecutableIndex.XBOX_NTSC] = 0x1D,
            [ExecutableIndex.XBOX_PAL] = 0x1D,
        };
        private Dictionary<ExecutableIndex, int> CreditsLevelOffs = new Dictionary<ExecutableIndex, int>()
        {
            [ExecutableIndex.PAL] = 0x1F6720,
            [ExecutableIndex.NTSCU] = 0x1F5E40,
            [ExecutableIndex.NTSCU2] = 0x1F63C0,
            [ExecutableIndex.NTSCJ] = 0x1F6660,
            [ExecutableIndex.XBOX_NTSC] = 0x36888C,
            [ExecutableIndex.XBOX_PAL] = 0x368874,
        };

        public override void ModPass(GenericModStruct mod)
        {
            ExecutableInfo ExecFile = Twins_Common.GetEXE(mod);
            using (FileStream file = new FileStream(ExecFile.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    string levelName = TS_Props_Misc.StartingChunk.Value;
                    int LevelSize = CreditsLevelSizes[ExecFile.Index];
                    int LevelOff = CreditsLevelOffs[ExecFile.Index];

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
