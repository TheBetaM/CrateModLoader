using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Crash;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Object_OldEntity : LevelObjectData<OldEntity>
    {

        [Category("Settings"), DisplayName("Path"), Description("The entity's path.")]
        public List<ObjectVector3> Path { get; set; } = new List<ObjectVector3>();

        public short EntityID { get; set; }
        public byte Type { get; set; }
        public byte Subtype { get; set; }

        public short Flags { get; set; }
        public short VectorX { get; set; }
        public short VectorY { get; set; }
        public short VectorZ { get; set; }
        public byte Spawn { get; set; }

        public OldZoneEntry Zone;

        public override void Load(OldEntity data)
        {
            int xoffset = BitConv.FromInt32(Zone.Layout, 0);
            int yoffset = BitConv.FromInt32(Zone.Layout, 4);
            int zoffset = BitConv.FromInt32(Zone.Layout, 8);

            if (data.Positions.Count > 0)
            {
                int x = (Type != 34 ? data.Positions[0].X : data.Positions[0].X + 50) + xoffset;
                int y = (Type != 34 ? data.Positions[0].Y : data.Positions[0].Y + 50) + yoffset;
                int z = (Type != 34 ? data.Positions[0].Z : data.Positions[0].Z + 50) + zoffset;
                Position = new ObjectVector3(x / 100f, y / 100f, z / 100f);
            }

            EntityID = data.ID;
            Type = data.Type;
            Subtype = data.Subtype;
            Flags = data.Flags;
            VectorX = data.VecX;
            VectorY = data.VecY;
            VectorZ = data.VecZ;
            Spawn = data.Spawn;

            for (int i = 0; i < data.Positions.Count; i++)
            {
                int x = (Type != 34 ? data.Positions[i].X : data.Positions[i].X + 50) + xoffset;
                int y = (Type != 34 ? data.Positions[i].Y : data.Positions[i].Y + 50) + yoffset;
                int z = (Type != 34 ? data.Positions[i].Z : data.Positions[i].Z + 50) + zoffset;
                Path.Add(new ObjectVector3(data.Positions[i].X / 100f, data.Positions[i].Y / 100f, data.Positions[i].Z / 100f));
            }

        }

        public override void Save(OldEntity data)
        {
            data.ID = EntityID;
            data.Type = Type;
            data.Subtype = Subtype;
            data.Flags = Flags;
            data.VecX = VectorX;
            data.VecY = VectorY;
            data.VecZ = VectorZ;
            data.Spawn = Spawn;

            data.Positions.Clear();
            for (int i = 0; i < Path.Count; i++)
            {
                data.Positions.Add(new EntityPosition((short)Path[i].X, (short)Path[i].Y, (short)Path[i].Z));
            }
            if (data.Positions.Count == 0) data.Positions.Add(new EntityPosition());
            data.Positions[0] = new EntityPosition((short)Position.X, (short)Position.Y, (short)Position.Z);
        }

        public override string ToString()
        {
            string TypeText = string.Empty;
            switch (Type)
            {
                case 0x0: TypeText = "WillC"; break;
                case 0x1: TypeText = "MonkC"; break;
                case 0x2: TypeText = "SkunC"; break;
                case 0x3: TypeText = "FruiC"; break;
                case 0x4: TypeText = "DispC"; break;
                case 0x5: TypeText = "DoctC"; break;
                case 0x6: TypeText = "SnakC"; break;
                case 0x7: TypeText = "WartC"; break;
                case 0x8: TypeText = "PoDoC"; break;
                case 0x9: TypeText = "PoRoC"; break;
                case 0xA: TypeText = "PoREC"; break;
                case 0xB: TypeText = "PoPlC"; break;
                case 0xC: TypeText = "MafiC"; break;
                case 0xD: TypeText = "Dog_C/PoCoC"; break;
                case 0xE: TypeText = "PoObC"; break;
                case 0xF: TypeText = "PinsC"; break;
                case 0x10: TypeText = "BaraC"; break;
                case 0x11: TypeText = "FatsC"; break;
                case 0x12: TypeText = "PinOC"; break;
                case 0x13: TypeText = "TurtC"; break;
                case 0x14: TypeText = "ChefC"; break;
                case 0x15: TypeText = "CheOC"; break;
                case 0x16: TypeText = "JunOC"; break;
                case 0x17: TypeText = "BridC"; break;
                case 0x18: TypeText = "HyeaC"; break;
                case 0x19: TypeText = "PlanC"; break;
                case 0x1A: TypeText = "CliOC"; break;
                case 0x1B: TypeText = "BeaOC"; break;
                case 0x1C: TypeText = "RivOC"; break;
                case 0x1D: TypeText = "ShadC"; break;
                case 0x1E: TypeText = "KonOC"; break;
                case 0x1F: TypeText = "CrabC"; break;
                case 0x20: TypeText = "WarpC"; break;
                case 0x21: TypeText = "WalOC"; break;
                case 0x22: TypeText = "BoxsC"; break;
                case 0x23: TypeText = "PillC"; break;
                case 0x24: TypeText = "frogC"; break;
                case 0x25: TypeText = "RRooC"; break;
                case 0x26: TypeText = "SheNC"; break;
                case 0x27: TypeText = "RooOC"; break;
                case 0x28: TypeText = "BrioC"; break;
                case 0x29: TypeText = "BriOC"; break;
                case 0x2A: TypeText = "RuiOC"; break;
                case 0x2B: TypeText = "SpidC"; break;
                case 0x2C: TypeText = "MapOC"; break;
                case 0x2D: TypeText = "KongC"; break;
                case 0x2E: TypeText = "RWaOC"; break;
                case 0x2F: TypeText = "LizaC"; break;
                case 0x30: TypeText = "Opt0C"; break;
                case 0x31: TypeText = "CortC"; break;
                case 0x32: TypeText = "CorOC"; break;
                case 0x33: TypeText = "VilOC"; break;
                case 0x34: TypeText = "GamOC"; break;
                case 0x35: TypeText = "CasOC"; break;
                case 0x36: TypeText = "LabAC"; break;
                case 0x37: TypeText = "WateC"; break;
                case 0x38: TypeText = "BonoC"; break;
                case 0x39: TypeText = "CardC"; break;
                case 0x3A: TypeText = "GemsC"; break;
                case 0x3B: TypeText = "IsldC"; break;
                case 0x3C: TypeText = "AsciC"; break;
                case 0x3D: TypeText = "WinGC"; break;

                default:
                    TypeText = "(Unknown)"; break;
            }

            return "Entity " + ID + ": " + TypeText;
        }
    }
}
