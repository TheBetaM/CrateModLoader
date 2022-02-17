using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Object_FortsExportedData : LevelObjectData<XFortsExportedData>
    {

        [Category("Settings")]
        public XFortsExportedData.EpochType Epoch { get; set; }
        [Category("Settings")]
        public XFortsExportedData.SkyboxType SkyBoxType { get; set; }
        [Category("Settings")]
        public XFortsExportedData.WaterTypes WaterType { get; set; }

        [Category("Settings")]
        public float FogNear { get; set; }
        [Category("Settings")]
        public float FogFar { get; set; }
        [Category("Settings")]
        public byte FogType { get; set; }
        [Category("Settings")]
        public ObjectVector3 SkyBoxPosition = new ObjectVector3(0, 0, 0);
        [Category("Settings")]
        public float WaterHeight { get; set; }

        [Browsable(false)]
        public override string Name { get; set; } = string.Empty;
        [Browsable(false)]
        public override ObjectVector3 Position { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Rotation { get; set; } = new ObjectVector3(0, 0, 0);
        [Browsable(false)]
        public override ObjectVector3 Scale { get; set; } = new ObjectVector3(1, 1, 1);

        public override void Load(XFortsExportedData data)
        {
            Epoch = data.Epoch;
            SkyBoxType = data.SkyBoxType;
            WaterType = data.WaterType;
            FogNear = data.FogNear;
            FogFar = data.FogFar;
            FogType = data.FogType;
            SkyBoxPosition = new ObjectVector3(data.SkyBoxPosition.X, data.SkyBoxPosition.Y, data.SkyBoxPosition.Z);
            WaterHeight = data.WaterHeight;
        }

        public override void Save(XFortsExportedData data)
        {
            data.Epoch = Epoch;
            data.SkyBoxType = SkyBoxType;
            data.WaterType = WaterType;
            data.FogNear = FogNear;
            data.FogFar = FogFar;
            data.FogType = FogType;
            data.SkyBoxPosition = new Vector3(SkyBoxPosition.X, SkyBoxPosition.Y, SkyBoxPosition.Z);
            data.WaterHeight = WaterHeight;
        }

        public override string ToString()
        {
            return "Worms Forts Exported Data";
        }
    }
}
