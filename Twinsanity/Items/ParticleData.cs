using System.Collections.Generic;
using System;
using System.IO;

namespace Twinsanity
{
    public sealed class ParticleData : TwinsItem
    {

        //Works for all versions AND Xbox's .PTL files, how nice. (may even work for TWOC with some adjustments)
        public bool IsStandalone = false;
        public bool IsDefault = false;

        public long DataSize;

        public uint Header1;
        public uint ParticleTypeCount;
        public ParticleSystemDefinition[] ParticleTypes;
        public uint ParticleInstanceCount;
        public List<ParticleSystemInstance> ParticleInstances;
        
        public uint ParticleTextureID_1;
        public uint ParticleMaterialID_1;
        public uint ParticleTextureID_2;
        public uint ParticleMaterialID_2;
        public uint ParticleTextureID_3;
        public uint ParticleMaterialID_3;
        public byte[] Remain;
        public uint DecalTextureID;
        public uint DecalMaterialID;

        public ParticleData()
        {

        }

        protected override int GetSize()
        {
            if (Header1 == 0x616E6942)
            {
                return (int)DataSize;
            }
            int count = 12;
            if (IsDefault)
            {
                count += 28;
            }
            count += Remain.Length;
            count += (int)ParticleInstanceCount * 0x44;
            if (ParticleTypeCount > 0)
            {
                for (int i = 0; i < ParticleTypes.Length; i++)
                {
                    count += 0x10;
                    count += ParticleTypes[i].Remain.Length;
                }
            }
            return count;
        }

        public override void Save(BinaryWriter writer)
        {
            if (Header1 == 0x616E6942)
            {
                writer.Write(Data);
                return;
            }

            if (IsDefault)
            {
                writer.Write(ParticleTextureID_1);
                writer.Write(ParticleMaterialID_1);
                writer.Write(ParticleTextureID_2);
                writer.Write(ParticleMaterialID_2);
                writer.Write(ParticleTextureID_3);
                writer.Write(ParticleMaterialID_3);
            }

            writer.Write(Header1);
            writer.Write(ParticleTypeCount);

            if (ParticleTypeCount > 0)
            {
                for (int i = 0; i < ParticleTypeCount; i++)
                {
                    string tempName = ParticleTypes[i].Name.Replace(' ', '\0');
                    char[] tempName2 = tempName.ToCharArray();
                    writer.Write(tempName2);
                    writer.Write(ParticleTypes[i].Remain);
                }
            }

            if (!IsDefault)
            {
                writer.Write(ParticleInstanceCount);
            }
            if (ParticleInstanceCount > 0 && ParticleInstanceCount < 65536)
            {
                for (int i = 0; i < ParticleInstanceCount; i++)
                {
                    writer.Write(ParticleInstances[i].Position.X);
                    writer.Write(ParticleInstances[i].Position.Y);
                    writer.Write(ParticleInstances[i].Position.Z);
                    writer.Write(ParticleInstances[i].Position.W);
                    writer.Write(ParticleInstances[i].Rot_X);
                    writer.Write(ParticleInstances[i].Rot_Y);
                    writer.Write(ParticleInstances[i].Rot_Z);
                    writer.Write(ParticleInstances[i].UnkZero);
                    string tempName = ParticleInstances[i].Name.Replace(' ', '\0');
                    char[] tempName2 = tempName.ToCharArray();
                    writer.Write(tempName2);
                    for (int a = 0; a < ParticleInstances[i].UnkShorts.Length; a++)
                    {
                        writer.Write(ParticleInstances[i].UnkShorts[a]);
                    }
                    writer.Write(ParticleInstances[i].EndZero);
                }
            }

            if (IsDefault)
            {
                writer.Write(DecalTextureID);
                writer.Write(DecalMaterialID);
            }

            if (Remain.Length > 0)
            {
                writer.Write(Remain);
            }
        }

        private bool isMonkeyBall = false;
        public void Load(BinaryReader reader, int size, bool isMB)
        {
            isMonkeyBall = isMB;
            Load(reader, size);
        }

        public override void Load(BinaryReader reader, int size)
        {
            long start_pos = reader.BaseStream.Position;
            DataSize = size;

            Header1 = reader.ReadUInt32();

            // Some PTL files are "BinaryIntermediate" files
            if (Header1 == 0x616E6942)
            {
                ParticleInstanceCount = 0;
                ParticleTypeCount = 0;
                reader.BaseStream.Position = start_pos;
                Data = reader.ReadBytes(size);
                return;
            }

            //Default.rm has some pre-header data: 3x (texture ID + material ID)
            if (Header1 > 0xFF)
            {
                IsDefault = true;
                ParticleTextureID_1 = Header1;
                ParticleMaterialID_1 = reader.ReadUInt32();
                ParticleTextureID_2 = reader.ReadUInt32();
                ParticleMaterialID_2 = reader.ReadUInt32();
                ParticleTextureID_3 = reader.ReadUInt32();
                ParticleMaterialID_3 = reader.ReadUInt32();
                Header1 = reader.ReadUInt32();

                if (isMonkeyBall)
                {
                    // todo
                    int RemainBytes1 = (int)((start_pos + size) - reader.BaseStream.Position);
                    if (RemainBytes1 > 0)
                    {
                        Remain = reader.ReadBytes(RemainBytes1);
                    }
                    else
                    {
                        Remain = new byte[0];
                    }
                    return;
                }
            }

            ParticleTypeCount = reader.ReadUInt32();

            ParticleTypes = new ParticleSystemDefinition[ParticleTypeCount];

            // size 0x33C (0x330 if header is 0x1c)
            if (ParticleTypeCount > 0)
            {
                for (int i = 0; i < ParticleTypeCount; i++)
                {
                    ParticleTypes[i] = new ParticleSystemDefinition();
                    string tempName = new string(reader.ReadChars(0x10));
                    ParticleTypes[i].Name = tempName.Replace('\0', ' ');
                    int bufferSize = 0x320;
                    if (Header1 == 0x1E)
                    {
                        bufferSize += 0xC;
                    }
                    ParticleTypes[i].Remain = reader.ReadBytes(bufferSize);
                }
            }

            uint InstanceCheck = reader.ReadUInt32();
            ParticleInstanceCount = InstanceCheck;
            if (!IsDefault && ParticleInstanceCount > 0 && ParticleInstanceCount < 65536)
            {
                ParticleInstances = new List<ParticleSystemInstance>();

                // size 0x44
                for (int i = 0; i < ParticleInstanceCount; i++)
                {
                    ParticleSystemInstance ParticleInstance = new ParticleSystemInstance();
                    ParticleInstance.Position = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    ParticleInstance.Rot_X = reader.ReadUInt16();
                    ParticleInstance.Rot_Y = reader.ReadUInt16();
                    ParticleInstance.Rot_Z = reader.ReadUInt16();
                    ParticleInstance.UnkZero = reader.ReadUInt32();
                    string tempName = new string(reader.ReadChars(0x10));
                    ParticleInstance.Name = tempName.Replace('\0', ' ');
                    ParticleInstance.UnkShorts = new ushort[12];
                    for (int a = 0; a < ParticleInstance.UnkShorts.Length; a++)
                    {
                        ParticleInstance.UnkShorts[a] = reader.ReadUInt16();
                    }
                    ParticleInstance.EndZero = reader.ReadUInt16();

                    ParticleInstances.Add(ParticleInstance);
                }
            }
            else
            {
                ParticleInstanceCount = 0;
            }

            // Default.rm has some extra data (decal stuff)
            if (IsDefault)
            {
                DecalTextureID = InstanceCheck;
                DecalMaterialID = reader.ReadUInt32();
            }

            // todo: more data after this in default

            int RemainBytes = (int)((start_pos + size) - reader.BaseStream.Position);
            if (RemainBytes > 0)
            {
                Remain = reader.ReadBytes(RemainBytes);
            }
            else
            {
                Remain = new byte[0];
            }
        }

        public class ParticleSystemDefinition
        {
            public string Name;
            public byte[] Remain;
        }

        public class ParticleSystemInstance
        {
            public string Name;
            public Pos Position;
            public ushort Rot_X;
            public ushort Rot_Y;
            public ushort Rot_Z;
            public uint UnkZero;
            public ushort[] UnkShorts; //12
            public ushort EndZero;
        }

    }
}
