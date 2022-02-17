using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Object_OldZone : LevelObjectData<OldZoneEntry>
    {
        [Browsable(false)]
        public override ObjectVector3 WorldScale => new ObjectVector3(0.004f);

        public override void Load(OldZoneEntry data)
        {
            int xoffset = BitConv.FromInt32(data.Layout, 0);
            int yoffset = BitConv.FromInt32(data.Layout, 4);
            int zoffset = BitConv.FromInt32(data.Layout, 8);
            int x2 = BitConv.FromInt32(data.Layout, 12);
            int y2 = BitConv.FromInt32(data.Layout, 16);
            int z2 = BitConv.FromInt32(data.Layout, 20);

            Position = new ObjectVector3(xoffset + (x2 / 2), yoffset + (y2 / 2), zoffset + (z2 / 2));
            Scale = new ObjectVector3(x2 * WorldScale.X, y2 * WorldScale.X, z2 * WorldScale.X);

            Name = data.EName;
        }

        public override void Save(OldZoneEntry data)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}