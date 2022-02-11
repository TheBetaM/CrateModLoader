using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{

    //[XOM_TypeName("WormAnimContainer")]
    public class WormAnimContainer : Container
    {
        public VInt AnimName = new VInt();
        public uint VoiceSample;
        public uint SoundEffect;
        public uint AnimType;
        public uint ShortAnim;
        public uint CurrentWormCanUse;
        public uint Situation;

        public override void Read(BinaryReader reader)
        {
            AnimName.Read(reader);
        }

        public override void Write(BinaryWriter writer)
        {
            AnimName.Write(writer);
        }
    }
}
