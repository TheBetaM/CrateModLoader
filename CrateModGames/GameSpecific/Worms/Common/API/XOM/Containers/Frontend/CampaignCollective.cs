using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("CampaignCollective")]
    public class CampaignCollective : Container
    {
        public List<VInt> Containers; 

        public override void Read(BinaryReader reader)
        {
            Containers = new List<VInt>();
            byte ContCount = reader.ReadByte();
            for (int i = 0; i < ContCount; i++)
            {
                VInt id = new VInt();
                id.Read(reader);
                Containers.Add(id);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)Containers.Count);
            for (int i = 0; i < Containers.Count; i++)
            {
                Containers[i].Write(writer);
            }
        }
    }
}
