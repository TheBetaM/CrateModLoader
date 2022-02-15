using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("XFortsExportedData")]
    public class XFortsExportedData : Container
    {
        private byte epoch;
        public float FogNear;
        public float FogFar;
        public byte FogType;
        private byte skyBoxType;
        public Vector3 SkyBoxPosition = new Vector3();
        private byte waterType;
        public float WaterHeight;

        public EpochType Epoch
        {
            get {return (EpochType)epoch; }
            set { epoch = (byte)value; }
        }
        public SkyboxType SkyBoxType
        {
            get { return (SkyboxType)skyBoxType; }
            set { skyBoxType = (byte)value; }
        }
        public WaterTypes WaterType
        {
            get { return (WaterTypes)waterType; }
            set { waterType = (byte)value; }
        }

        public List<RefPoint> RefPoints;
        public List<BuildPoint> BuildPoints;
        public List<PhantomSphere> PhantomSpheres;
        public List<PhantomBox> PhantomBoxes;
        public List<VInt> SceneryEffects;

        public enum EpochType : byte
        {
            Medieval = 0,
            Oriental = 1,
            Egypt = 2,
            Greek = 3,
        }
        public enum SkyboxType : byte
        {
            MedievalDay = 0,
            MedievalEvening,
            MedievalNight,
            GreekEvening,
            GreekDay,
            GreekNight,
            OrientalDay,
            OrientalEvening,
            OrientalNight,
            EgyptDay,
            EgyptEvening,
            EgyptNight,
            ColiseumOfDoomDay,
            UnusedRedSkyEvening,
            MordredAndMorganaHell,
            AQuestDay,
            PharaohEnoughDay,
            KingdomIsBornNight,
            TowerOfPowerEvening,
            ChessMateEvening,
        }
        public enum WaterTypes : byte
        {
            WATER = 0,
            LAVA = 1,
            CLOUD = 2,
            SAND = 3,
        }
        public enum BuildingTypes : byte
        {
            TOWER = 0,
            KEEP,
            CASTLE,
            CITADEL,
            WALL,
            HOSPITAL,
            REFINERY,
            SCIENCELAB,
            WONDER,
            Unused1,
            Unused2,
            Unused3,
            Unused4,
            Unused5,
            STRONGHOLD,
            LIGHT,
            NONE = 0xFF,
        }

        public override void Read(BinaryReader reader)
        {
            epoch = reader.ReadByte();
            FogNear = reader.ReadSingle();
            FogFar = reader.ReadSingle();
            FogType = reader.ReadByte();
            skyBoxType = reader.ReadByte();
            SkyBoxPosition.Read(reader);
            waterType = reader.ReadByte();
            WaterHeight = reader.ReadSingle();

            RefPoints = new List<RefPoint>();
            VInt RefNameLength = new VInt(reader);
            for (int i = 0; i < RefNameLength.Value; i++)
            {
                RefPoint point = new RefPoint();
                point.NameID.Read(reader);

                point.NamePoint = ParentFile.Strings[(int)point.NameID.Value];

                RefPoints.Add(point);
            }
            RefNameLength.Read(reader);
            for (int i = 0; i < RefNameLength.Value; i++)
            {
                RefPoints[i].Pos.Read(reader);
            }
            RefNameLength.Read(reader);
            for (int i = 0; i < RefNameLength.Value ; i++)
            {
                RefPoints[i].Rot.Read(reader);
            }

            BuildPoints = new List<BuildPoint>();
            byte BNameLength = reader.ReadByte();
            for (int i = 0; i < BNameLength; i++)
            {
                BuildPoint point = new BuildPoint();
                point.NameID.Read(reader);

                point.NamePoint = ParentFile.Strings[(int)point.NameID.Value];

                BuildPoints.Add(point);
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].Pos.Read(reader);
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].VictoryLocation.Read(reader);
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].buildingType = reader.ReadByte();
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].BonusType = reader.ReadByte();
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].BuildingName.Read(reader);
                BuildPoints[i].NameBuilding = ParentFile.Strings[(int)BuildPoints[i].BuildingName.Value];
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].PlayerID = reader.ReadUInt32();
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].Rot = reader.ReadSingle();
            }
            reader.ReadByte();
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].Connections = reader.ReadByte();
            }

            PhantomSpheres = new List<PhantomSphere>();
            byte pSphereCount = reader.ReadByte();
            if (pSphereCount > 0)
            {
                for (int i = 0; i < pSphereCount; i++)
                {
                    PhantomSphere sphere = new PhantomSphere();
                    sphere.NameID.Read(reader);
                    sphere.NamePoint = ParentFile.Strings[(int)sphere.NameID.Value];
                    PhantomSpheres.Add(sphere);
                }
            }
            PhantomBoxes = new List<PhantomBox>();
            byte pCubeCount = reader.ReadByte();
            if (pCubeCount > 0)
            {
                for (int i = 0; i < pCubeCount; i++)
                {
                    PhantomBox box = new PhantomBox();
                    box.NameID.Read(reader);
                    box.NamePoint = ParentFile.Strings[(int)box.NameID.Value];
                    PhantomBoxes.Add(box);
                }
            }
            reader.ReadByte();
            if (pSphereCount > 0)
            {
                for (int i = 0; i < PhantomSpheres.Count; i++)
                {
                    PhantomSpheres[i].Pos.Read(reader);
                }
            }
            reader.ReadByte();
            if (pCubeCount > 0)
            {
                for (int i = 0; i < PhantomBoxes.Count; i++)
                {
                    PhantomBoxes[i].Pos.Read(reader);
                }
            }
            reader.ReadByte();
            if (pSphereCount > 0)
            {
                for (int i = 0; i < PhantomSpheres.Count; i++)
                {
                    PhantomSpheres[i].Radius = reader.ReadSingle();
                }
            }
            reader.ReadByte();
            if (pCubeCount > 0)
            {
                for (int i = 0; i < PhantomBoxes.Count; i++)
                {
                    PhantomBoxes[i].Extents.Read(reader);
                }
            }

            SceneryEffects = new List<VInt>();
            byte ContCount = reader.ReadByte();
            for (int i = 0; i < ContCount; i++)
            {
                VInt id = new VInt();
                id.Read(reader);
                SceneryEffects.Add(id);
            }

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(epoch);
            writer.Write(FogNear);
            writer.Write(FogFar);
            writer.Write(FogType);
            writer.Write(skyBoxType);
            SkyBoxPosition.Write(writer);
            writer.Write(waterType);
            writer.Write(WaterHeight);

            VInt RefNameLength = new VInt();
            RefNameLength.Value = (uint)RefPoints.Count;
            RefNameLength.Write(writer);
            for (int i = 0; i < RefPoints.Count; i++)
            {
                RefPoints[i].NameID.Write(writer);
            }
            RefNameLength.Write(writer);
            for (int i = 0; i < RefPoints.Count; i++)
            {
                RefPoints[i].Pos.Write(writer);
            }
            RefNameLength.Write(writer);
            for (int i = 0; i < RefPoints.Count; i++)
            {
                RefPoints[i].Rot.Write(writer);
            }

            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].NameID.Write(writer);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].Pos.Write(writer);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].VictoryLocation.Write(writer);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                writer.Write(BuildPoints[i].buildingType);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                writer.Write(BuildPoints[i].BonusType);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                BuildPoints[i].BuildingName.Write(writer);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                writer.Write(BuildPoints[i].PlayerID);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                writer.Write(BuildPoints[i].Rot);
            }
            writer.Write((byte)BuildPoints.Count);
            for (int i = 0; i < BuildPoints.Count; i++)
            {
                writer.Write(BuildPoints[i].Connections);
            }

            writer.Write((byte)PhantomSpheres.Count);
            for (int i = 0; i < PhantomSpheres.Count; i++)
            {
                PhantomSpheres[i].NameID.Write(writer);
            }
            writer.Write((byte)PhantomBoxes.Count);
            for (int i = 0; i < PhantomBoxes.Count; i++)
            {
                PhantomBoxes[i].NameID.Write(writer);
            }

            writer.Write((byte)PhantomSpheres.Count);
            for (int i = 0; i < PhantomSpheres.Count; i++)
            {
                PhantomSpheres[i].Pos.Write(writer);
            }
            writer.Write((byte)PhantomBoxes.Count);
            for (int i = 0; i < PhantomBoxes.Count; i++)
            {
                PhantomBoxes[i].Pos.Write(writer);
            }

            writer.Write((byte)PhantomSpheres.Count);
            for (int i = 0; i < PhantomSpheres.Count; i++)
            {
                writer.Write(PhantomSpheres[i].Radius);
            }
            writer.Write((byte)PhantomBoxes.Count);
            for (int i = 0; i < PhantomBoxes.Count; i++)
            {
                PhantomBoxes[i].Extents.Write(writer);
            }

            writer.Write((byte)SceneryEffects.Count);
            for (int i = 0; i < SceneryEffects.Count; i++)
            {
                SceneryEffects[i].Write(writer);
            }
        }

        public class RefPoint
        {
            public VInt NameID = new VInt();
            public Vector3 Pos = new Vector3();
            public Vector3 Rot = new Vector3();

            public string NamePoint;
        }

        public class BuildPoint
        {
            public VInt NameID = new VInt();
            public Vector3 Pos = new Vector3();
            public ByteBool VictoryLocation = new ByteBool();
            public byte buildingType;
            public byte BonusType;
            public VInt BuildingName = new VInt();
            public uint PlayerID;
            public float Rot;
            public byte Connections;

            public string NamePoint;
            public string NameBuilding;

            public BuildingTypes BuildingType
            {
                get { return (BuildingTypes)buildingType; }
                set { buildingType = (byte)value; }
            }
        }

        public class PhantomBox
        {
            public VInt NameID = new VInt();
            public Vector3 Pos = new Vector3();
            public Vector3 Extents = new Vector3();

            public string NamePoint;
        }

        public class PhantomSphere
        {
            public VInt NameID = new VInt();
            public Vector3 Pos = new Vector3();
            public float Radius;

            public string NamePoint;
        }
    }
}
