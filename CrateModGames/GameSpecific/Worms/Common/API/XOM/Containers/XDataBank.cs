using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    //unfinished, has a lot of dynamic ID's
    //[XOM_TypeName("XDataBank")]
    public class XDataBank : Container
    {
        public byte UnkByte;
        public List<VInt> ResourcesInt;
        public List<VInt> ResourcesUint;
        public List<VInt> ResourcesString;
        public List<VInt> ResourcesFloat;
        public List<VInt> ResourcesVector;
        public List<VInt> ResourcesContainer;
        public List<VInt> ResourcesStringTable;
        public List<VInt> ResourcesColor;

        public override void Read(BinaryReader reader)
        {
            UnkByte = reader.ReadByte();

            ResourcesInt = new List<VInt>();
            ResourcesUint = new List<VInt>();
            ResourcesString = new List<VInt>();
            ResourcesFloat = new List<VInt>();
            ResourcesVector = new List<VInt>();
            ResourcesContainer = new List<VInt>();
            ResourcesStringTable = new List<VInt>();
            ResourcesColor = new List<VInt>();

            byte R1 = reader.ReadByte();



        }

        public override void Write(BinaryWriter writer)
        {
            
        }
    }
}
