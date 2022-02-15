using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Object_OldZone : LevelObjectData<OldZoneEntry>
    {
        [Category("Settings")]
        public string Name { get; private set; }

        public override void Load(OldZoneEntry data)
        {
            int xoffset = BitConv.FromInt32(data.Layout, 0);
            int yoffset = BitConv.FromInt32(data.Layout, 4);
            int zoffset = BitConv.FromInt32(data.Layout, 8);
            int x2 = BitConv.FromInt32(data.Layout, 12);
            int y2 = BitConv.FromInt32(data.Layout, 16);
            int z2 = BitConv.FromInt32(data.Layout, 20);

            Position = new ObjectVector3(xoffset / 100f, yoffset / 100f, zoffset / 100f);
            Scale = new ObjectVector3(x2 / 100f, y2 / 100f, z2 / 100f);

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