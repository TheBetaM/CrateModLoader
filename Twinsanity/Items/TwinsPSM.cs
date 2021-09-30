using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.Items
{
    public class TwinsPSM
    {
        public List<TwinsPTC> PTCs = new List<TwinsPTC>();

        public void Load(BinaryReader reader, int size)
        {
            var startPos = reader.BaseStream.Position;
            while (reader.BaseStream.Position < startPos + size)
            {
                var ptc = new TwinsPTC();
                ptc.Load(reader, 0);
                PTCs.Add(ptc);
            }
        }

        public void Save(BinaryWriter writer)
        {
            foreach (var ptc in PTCs)
            {
                ptc.Save(writer);
            }
        }
    }
}
