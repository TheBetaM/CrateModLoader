using System.Collections.Generic;
using System.IO;
using System;

namespace Twinsanity
{
    public sealed class SceneryData : TwinsItem
    {

        public uint HeaderUnk1;
        public string ChunkName;
        public uint HeaderUnk2;
        public uint HeaderUnk3;
        public byte HeaderUnk4;
        public byte[] HeaderBuffer;
        public uint SkydomeID;
        public SceneryStruct SceneryRoot;
        public List<LightAmbient> LightsAmbient;
        public List<LightDirectional> LightsDirectional;
        public List<LightPoint> LightsPoint;
        public List<LightNegative> LightsNegative;

        public uint unkVar5;

        public SceneryData()
        {

        }

        protected override int GetSize()
        {

            int count = 4 + 4 + ChunkName.Length + 4 + 4 + 1;
            if ((HeaderUnk1 & 0x10000) != 0)
            {
                count += 4;
            }

            if ((HeaderUnk1 & 0x20000) != 0)
            {
                count += HeaderBuffer.Length;
                count += 4 + 4 + 4 + 4 + 4;
                if (LightsAmbient.Count > 0)
                {
                    count += LightsAmbient.Count * (4 + 4 + 4 + 4 + 4 + 4 + 16 + 32);
                }
                if (LightsDirectional.Count > 0)
                {
                    count += LightsDirectional.Count * (4 + 4 + 4 + 4 + 4 + 4 + 16 + 32 + 16 + 2);
                }
                if (LightsPoint.Count > 0)
                {
                    count += LightsPoint.Count * (4 + 4 + 4 + 4 + 4 + 4 + 16 + 32 + 2);
                }
                if (LightsNegative.Count > 0)
                {
                    count += LightsNegative.Count * (4 + 4 + 4 + 4 + 4 + 4 + 16 + 32 + 16 + 4 + 4 + 4 + 4 + 2 + 2);
                }
            }

            if (HeaderUnk3 == 0x160A)
            {
                count += 4;
                CountScenery(SceneryRoot, ref count);
            }

            return count;
        }

        private void CountScenery(SceneryStruct ptr, ref int count)
        {
            CountSceneryModel(ptr.Model, ref count);
            for (int i = 0; i < ptr.Links.Length; i++)
            {
                count += 4;
            }
            for (int i = 0; i < ptr.Links.Length; i++)
            {
                if (ptr.Links[i] is SceneryModelStruct)
                {
                    CountSceneryModel((SceneryModelStruct)ptr.Links[i], ref count);
                }
                else if (ptr.Links[i] is SceneryStruct)
                {
                    CountScenery((SceneryStruct)ptr.Links[i], ref count);
                }
            }
        }

        private void CountSceneryModel(SceneryModelStruct ptr, ref int count)
        {
            count += 4;
            if (ptr.Header == 0x1613)
            {
                count += 4;
                if (ptr.Models.Count != 0)
                {
                    for (int i = 0; i < ptr.Models.Count; i++)
                    {
                        count += 32;
                        count += 4;
                        count += 16 * ptr.Models[i].ModelMatrix.Length;
                    }
                }
            }
            count += ptr.UnkPos.Length * 16;
        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(HeaderUnk1);
            writer.Write((uint)ChunkName.Length);
            writer.Write(ChunkName.ToCharArray());
            writer.Write(HeaderUnk2);
            writer.Write(HeaderUnk3);
            writer.Write(HeaderUnk4);
            if ((HeaderUnk1 & 0x10000) != 0)
            {
                writer.Write(SkydomeID);
            }

            if ((HeaderUnk1 & 0x20000) != 0)
            {
                writer.Write(HeaderBuffer);

                uint LightAmbientNum = (uint)LightsAmbient.Count;
                uint LightDirectionalNum = (uint)LightsDirectional.Count;
                uint LightPointNum = (uint)LightsPoint.Count;
                uint LightNegativeNum = (uint)LightsNegative.Count;
                uint LightsNum = LightAmbientNum + LightDirectionalNum + LightPointNum + LightNegativeNum;

                writer.Write(LightsNum);
                writer.Write(LightAmbientNum);
                writer.Write(LightDirectionalNum);
                writer.Write(LightPointNum);
                writer.Write(LightNegativeNum);

                if (LightAmbientNum > 0)
                {
                    for (int i = 0; i < LightAmbientNum; i++)
                    {
                        writer.Write(LightsAmbient[i].Flags);
                        writer.Write(LightsAmbient[i].Radius);
                        writer.Write(LightsAmbient[i].Color_R);
                        writer.Write(LightsAmbient[i].Color_G);
                        writer.Write(LightsAmbient[i].Color_B);
                        writer.Write(LightsAmbient[i].Color_Unk);
                        writer.Write(LightsAmbient[i].Position.X); writer.Write(LightsAmbient[i].Position.Y); writer.Write(LightsAmbient[i].Position.Z); writer.Write(LightsAmbient[i].Position.W);
                        writer.Write(LightsAmbient[i].Vector1.X);
                        writer.Write(LightsAmbient[i].Vector1.Y);
                        writer.Write(LightsAmbient[i].Vector1.Z);
                        writer.Write(LightsAmbient[i].Vector1.W);
                        writer.Write(LightsAmbient[i].Vector2.X);
                        writer.Write(LightsAmbient[i].Vector2.Y);
                        writer.Write(LightsAmbient[i].Vector2.Z);
                        writer.Write(LightsAmbient[i].Vector2.W);
                    }
                }
                if (LightDirectionalNum > 0)
                {
                    for (int i = 0; i < LightDirectionalNum; i++)
                    {
                        writer.Write(LightsDirectional[i].Flags);
                        writer.Write(LightsDirectional[i].Radius);
                        writer.Write(LightsDirectional[i].Color_R);
                        writer.Write(LightsDirectional[i].Color_G);
                        writer.Write(LightsDirectional[i].Color_B);
                        writer.Write(LightsDirectional[i].Color_Unk);
                        writer.Write(LightsDirectional[i].Position.X); writer.Write(LightsDirectional[i].Position.Y); writer.Write(LightsDirectional[i].Position.Z); writer.Write(LightsDirectional[i].Position.W);
                        writer.Write(LightsDirectional[i].Vector1.X);
                        writer.Write(LightsDirectional[i].Vector1.Y);
                        writer.Write(LightsDirectional[i].Vector1.Z);
                        writer.Write(LightsDirectional[i].Vector1.W);
                        writer.Write(LightsDirectional[i].Vector2.X);
                        writer.Write(LightsDirectional[i].Vector2.Y);
                        writer.Write(LightsDirectional[i].Vector2.Z);
                        writer.Write(LightsDirectional[i].Vector2.W);
                        writer.Write(LightsDirectional[i].Vector3.X);
                        writer.Write(LightsDirectional[i].Vector3.Y);
                        writer.Write(LightsDirectional[i].Vector3.Z);
                        writer.Write(LightsDirectional[i].Vector3.W);
                        writer.Write(LightsDirectional[i].unkShort);
                    }
                }
                if (LightPointNum > 0)
                {
                    for (int i = 0; i < LightPointNum; i++)
                    {
                        writer.Write(LightsPoint[i].Flags);
                        writer.Write(LightsPoint[i].Radius);
                        writer.Write(LightsPoint[i].Color_R);
                        writer.Write(LightsPoint[i].Color_G);
                        writer.Write(LightsPoint[i].Color_B);
                        writer.Write(LightsPoint[i].Color_Unk);
                        writer.Write(LightsPoint[i].Position.X); writer.Write(LightsPoint[i].Position.Y); writer.Write(LightsPoint[i].Position.Z); writer.Write(LightsPoint[i].Position.W);
                        writer.Write(LightsPoint[i].Vector1.X);
                        writer.Write(LightsPoint[i].Vector1.Y);
                        writer.Write(LightsPoint[i].Vector1.Z);
                        writer.Write(LightsPoint[i].Vector1.W);
                        writer.Write(LightsPoint[i].Vector2.X);
                        writer.Write(LightsPoint[i].Vector2.Y);
                        writer.Write(LightsPoint[i].Vector2.Z);
                        writer.Write(LightsPoint[i].Vector2.W);
                        writer.Write(LightsPoint[i].unkShort);
                    }
                }
                if (LightNegativeNum > 0)
                {
                    for (int i = 0; i < LightNegativeNum; i++)
                    {
                        writer.Write(LightsNegative[i].Flags);
                        writer.Write(LightsNegative[i].Radius);
                        writer.Write(LightsNegative[i].Color_R);
                        writer.Write(LightsNegative[i].Color_G);
                        writer.Write(LightsNegative[i].Color_B);
                        writer.Write(LightsNegative[i].Color_Unk);
                        writer.Write(LightsNegative[i].Position.X); writer.Write(LightsNegative[i].Position.Y); writer.Write(LightsNegative[i].Position.Z); writer.Write(LightsNegative[i].Position.W);
                        writer.Write(LightsNegative[i].Vector1.X);
                        writer.Write(LightsNegative[i].Vector1.Y);
                        writer.Write(LightsNegative[i].Vector1.Z);
                        writer.Write(LightsNegative[i].Vector1.W);
                        writer.Write(LightsNegative[i].Vector2.X);
                        writer.Write(LightsNegative[i].Vector2.Y);
                        writer.Write(LightsNegative[i].Vector2.Z);
                        writer.Write(LightsNegative[i].Vector2.W);
                        writer.Write(LightsNegative[i].Vector3.X);
                        writer.Write(LightsNegative[i].Vector3.Y);
                        writer.Write(LightsNegative[i].Vector3.Z);
                        writer.Write(LightsNegative[i].Vector3.W);
                        writer.Write(LightsNegative[i].unkFloat1);
                        writer.Write(LightsNegative[i].unkFloat2);
                        writer.Write(LightsNegative[i].unkUInt1);
                        writer.Write(LightsNegative[i].unkUInt2);
                        writer.Write(LightsNegative[i].unkUShort1);
                        writer.Write(LightsNegative[i].unkUShort2);
                    }
                }
            }

            if (HeaderUnk3 == 0x160A)
            {
                writer.Write(unkVar5);
                SaveScenery(SceneryRoot, writer);
            }

        }

        public override void Load(BinaryReader reader, int size)
        {
            //long start_pos = reader.BaseStream.Position;

            HeaderUnk1 = reader.ReadUInt32();
            uint chunkNameLength = reader.ReadUInt32();
            ChunkName = new string(reader.ReadChars((int)chunkNameLength));
            HeaderUnk2 = reader.ReadUInt32();
            HeaderUnk3 = reader.ReadUInt32();
            HeaderUnk4 = reader.ReadByte();
            if ((HeaderUnk1 & 0x10000) != 0)
            {
                SkydomeID = reader.ReadUInt32();
            }

            LightsAmbient = new List<LightAmbient>();
            LightsDirectional = new List<LightDirectional>();
            LightsPoint = new List<LightPoint>();
            LightsNegative = new List<LightNegative>();

            if ((HeaderUnk1 & 0x20000) != 0)
            {
                HeaderBuffer = reader.ReadBytes(0x400);

                uint LightsNum = reader.ReadUInt32();

                uint LightAmbientNum = reader.ReadUInt32();
                uint LightDirectionalNum = reader.ReadUInt32();
                uint LightPointNum = reader.ReadUInt32();
                uint LightNegativeNum = reader.ReadUInt32();

                if (LightAmbientNum > 0)
                {
                    for (int i = 0; i < LightAmbientNum; i++)
                    {
                        LightAmbient light = new LightAmbient();

                        light.Flags = reader.ReadBytes(4);
                        light.Radius = reader.ReadSingle();
                        light.Color_R = reader.ReadSingle();
                        light.Color_G = reader.ReadSingle();
                        light.Color_B = reader.ReadSingle();
                        light.Color_Unk = reader.ReadSingle();
                        light.Position = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        LightsAmbient.Add(light);
                    }
                }
                if (LightDirectionalNum > 0)
                {
                    for (int i = 0; i < LightDirectionalNum; i++)
                    {
                        LightDirectional light = new LightDirectional();

                        light.Flags = reader.ReadBytes(4);
                        light.Radius = reader.ReadSingle();
                        light.Color_R = reader.ReadSingle();
                        light.Color_G = reader.ReadSingle();
                        light.Color_B = reader.ReadSingle();
                        light.Color_Unk = reader.ReadSingle();
                        light.Position = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        light.Vector3 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.unkShort = reader.ReadUInt16();

                        LightsDirectional.Add(light);
                    }
                }
                if (LightPointNum > 0)
                {
                    for (int i = 0; i < LightPointNum; i++)
                    {
                        LightPoint light = new LightPoint();

                        light.Flags = reader.ReadBytes(4);
                        light.Radius = reader.ReadSingle();
                        light.Color_R = reader.ReadSingle();
                        light.Color_G = reader.ReadSingle();
                        light.Color_B = reader.ReadSingle();
                        light.Color_Unk = reader.ReadSingle();
                        light.Position = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        light.unkShort = reader.ReadUInt16();

                        LightsPoint.Add(light);
                    }
                }
                if (LightNegativeNum > 0)
                {
                    for (int i = 0; i < LightNegativeNum; i++)
                    {
                        LightNegative light = new LightNegative();

                        light.Flags = reader.ReadBytes(4);
                        light.Radius = reader.ReadSingle();
                        light.Color_R = reader.ReadSingle();
                        light.Color_G = reader.ReadSingle();
                        light.Color_B = reader.ReadSingle();
                        light.Color_Unk = reader.ReadSingle();
                        light.Position = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.Vector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                        light.Vector3 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        light.unkFloat1 = reader.ReadSingle();
                        light.unkFloat2 = reader.ReadSingle();
                        light.unkUInt1 = reader.ReadUInt32();
                        light.unkUInt2 = reader.ReadUInt32();
                        light.unkUShort1 = reader.ReadUInt16();
                        light.unkUShort2 = reader.ReadUInt16();

                        LightsNegative.Add(light);
                    }
                }
            }

            SceneryRoot = null;
            if (HeaderUnk3 == 0x160A)
            {
                unkVar5 = reader.ReadUInt32();
                SceneryRoot = LoadScenery(reader);
            }
            else
            {
                //Console.WriteLine("no scenery!! bug?");
            }

            //Console.WriteLine("end pos: " + (reader.BaseStream.Position - start_pos) + " target: " + size);
        }

        private SceneryModelStruct LoadSceneryModel(BinaryReader reader)
        {
            SceneryModelStruct scenery = new SceneryModelStruct();
            scenery.Header = reader.ReadUInt32();
            scenery.Models = new List<ScenerySubModel>();
            if (scenery.Header == 0x1613)
            {
                ushort modelCount = reader.ReadUInt16();
                ushort specialModelCount = reader.ReadUInt16();

                if (modelCount + specialModelCount != 0)
                {
                    for (int i = 0; i < modelCount + specialModelCount; i++)
                    {
                        ScenerySubModel newModel = new ScenerySubModel();
                        newModel.ModelMatrix = new Pos[4];
                        newModel.ModelBoundingBoxVector1 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        newModel.ModelBoundingBoxVector2 = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        scenery.Models.Add(newModel);
                    }
                    for (int i = 0; i < modelCount + specialModelCount; i++)
                    {
                        if (i > modelCount - 1)
                        {
                            scenery.Models[i].ModelID = reader.ReadUInt32();
                            scenery.Models[i].isSpecial = true;
                        }
                        else
                        {
                            scenery.Models[i].ModelID = reader.ReadUInt32();
                            scenery.Models[i].isSpecial = false;
                        }
                    }
                    for (int i = 0; i < modelCount + specialModelCount; i++)
                    {
                        scenery.Models[i].ModelMatrix[0] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        scenery.Models[i].ModelMatrix[1] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        scenery.Models[i].ModelMatrix[2] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        scenery.Models[i].ModelMatrix[3] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    }
                }
            }
            scenery.UnkPos = new Pos[5];
            scenery.UnkPos[0] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            scenery.UnkPos[1] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            scenery.UnkPos[2] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            scenery.UnkPos[3] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            scenery.UnkPos[4] = new Pos(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            return scenery;
        }

        private SceneryStruct LoadScenery(BinaryReader reader)
        {
            SceneryStruct scen = new SceneryStruct();
            int[] sceneryTypes = new int[8];

            scen.Model = LoadSceneryModel(reader);

            scen.Links = new object[8];

            for (int i = 0; i < 8; i++)
            {
                sceneryTypes[i] = reader.ReadInt32();
            }

            for (int i = 0; i < 8; i++)
            {
                if (sceneryTypes[i] == 0x1600)
                {
                    scen.Links[i] = LoadScenery(reader);
                }
                else if (sceneryTypes[i] == 0x1605)
                {
                    scen.Links[i] = LoadSceneryModel(reader);
                }
                else
                {
                    // if type 3 - it's nothing
                    scen.Links[i] = null;
                }
            }

            return scen;
        }

        private void SaveScenery(SceneryStruct ptr, BinaryWriter writer)
        {
            SaveSceneryModel(ptr.Model, writer);

            for (int i = 0; i < ptr.Links.Length; i++)
            {
                if (ptr.Links[i] is SceneryModelStruct)
                {
                    writer.Write(0x1605);
                }
                else if (ptr.Links[i] is SceneryStruct)
                {
                    writer.Write(0x1600);
                }
                else
                {
                    writer.Write(3);
                }
            }

            for (int i = 0; i < ptr.Links.Length; i++)
            {
                if (ptr.Links[i] is SceneryModelStruct)
                {
                    SaveSceneryModel((SceneryModelStruct)ptr.Links[i], writer);
                }
                else if (ptr.Links[i] is SceneryStruct)
                {
                    SaveScenery((SceneryStruct)ptr.Links[i], writer);
                }
            }

        }

        private void SaveSceneryModel(SceneryModelStruct ptr, BinaryWriter writer)
        {
            writer.Write(ptr.Header);

            if (ptr.Header == 0x1613)
            {

                short modelCount = 0;
                for (int i = 0; i < ptr.Models.Count; i++)
                {
                    if (ptr.Models[i].isSpecial)
                    {
                        break;
                    }
                    modelCount++;
                }

                short specialModelCount = (short)(ptr.Models.Count - modelCount);

                writer.Write(modelCount);
                writer.Write(specialModelCount);

                if (ptr.Models.Count != 0)
                {
                    for (int i = 0; i < ptr.Models.Count; i++)
                    {
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector1.X);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector1.Y);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector1.Z);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector1.W);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector2.X);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector2.Y);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector2.Z);
                        writer.Write(ptr.Models[i].ModelBoundingBoxVector2.W);
                    }
                    for (int i = 0; i < ptr.Models.Count; i++)
                    {
                        writer.Write(ptr.Models[i].ModelID);
                    }
                    for (int i = 0; i < ptr.Models.Count; i++)
                    {
                        for (int d = 0; d < ptr.Models[i].ModelMatrix.Length; d++)
                        {
                            writer.Write(ptr.Models[i].ModelMatrix[d].X);
                            writer.Write(ptr.Models[i].ModelMatrix[d].Y);
                            writer.Write(ptr.Models[i].ModelMatrix[d].Z);
                            writer.Write(ptr.Models[i].ModelMatrix[d].W);
                        }
                    }
                }
            }

            for (int i = 0; i < ptr.UnkPos.Length; i++)
            {
                writer.Write(ptr.UnkPos[i].X);
                writer.Write(ptr.UnkPos[i].Y);
                writer.Write(ptr.UnkPos[i].Z);
                writer.Write(ptr.UnkPos[i].W);
            }

        }

        public class SceneryModelStruct
        {
            public uint Header;
            public List<ScenerySubModel> Models;
            public Pos[] UnkPos; //4 
        }

        public class SceneryStruct
        {
            public SceneryModelStruct Model;
            public object[] Links; //8
        }

        public class ScenerySubModel
        {
            public bool isSpecial;
            public uint ModelID;
            public Pos ModelBoundingBoxVector1;
            public Pos ModelBoundingBoxVector2;
            public Pos[] ModelMatrix; // 4
        }

        public class LightBase
        {
            public byte[] Flags; //4
            public float Radius;
            public float Color_R;
            public float Color_G;
            public float Color_B;
            public float Color_Unk;
            public Pos Position;
            public Pos Vector1;
            public Pos Vector2;
        }

        public class LightAmbient : LightBase
        {

        }

        public class LightDirectional : LightBase
        {
            public Pos Vector3;
            public ushort unkShort;
        }

        public class LightPoint : LightBase
        {
            public ushort unkShort;
        }

        public class LightNegative : LightBase
        {
            public Pos Vector3;
            public float unkFloat1;
            public float unkFloat2;
            public uint unkUInt1;
            public uint unkUInt2;
            public ushort unkUShort1;
            public ushort unkUShort2;
        }

    }

}
