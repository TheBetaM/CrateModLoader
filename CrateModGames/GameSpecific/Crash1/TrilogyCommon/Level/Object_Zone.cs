using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash2
{
    public class Object_Zone : LevelObjectData<ZoneEntry>
    {
        [Category("Settings")]
        public string Name { get; private set; }

        public override void Load(ZoneEntry data)
        {
            int xoffset = BitConv.FromInt32(data.Layout, 0);
            int yoffset = BitConv.FromInt32(data.Layout, 4);
            int zoffset = BitConv.FromInt32(data.Layout, 8);
            int x2 = BitConv.FromInt32(data.Layout, 12);
            int y2 = BitConv.FromInt32(data.Layout, 16);
            int z2 = BitConv.FromInt32(data.Layout, 20);

            Position = new ObjectVector3(xoffset / 100f, yoffset / 100f, zoffset / 100f);
            x2 = Math.Abs(x2);
            y2 = Math.Abs(y2);
            z2 = Math.Abs(z2);
            Scale = new ObjectVector3(x2 / 100f, y2 / 100f, z2 / 100f);

            Name = data.EName;
        }

        public override void Save(ZoneEntry data)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}