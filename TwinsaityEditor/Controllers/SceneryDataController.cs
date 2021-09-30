using System;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor
{
    public class SceneryDataController : ItemController
    {
        public new SceneryData Data { get; set; }

        public SceneryDataController(MainForm topform, SceneryData item) : base(topform, item)
        {
            Data = item;
            //AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            return $"Scenery Data [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add(string.Format("Chunk Name: {0}", Data.ChunkName));
            text.Add(string.Format("Header: {0}", Data.HeaderUnk1));
            text.Add(string.Format("Skydome ID: {0:X8}", Data.SkydomeID));
            text.Add(string.Format("HeaderVars: {0}; {2} SceneryType {1};", Data.HeaderUnk2, Data.HeaderUnk3, Data.HeaderUnk4));
            text.Add(string.Format("UnkInt: {0}", Data.unkVar5));

            text.Add("\n");

            text.Add(string.Format("Lights: Ambient {0}; Directional {1}; Point {2}; Negative {3}", Data.LightsAmbient.Count, Data.LightsDirectional.Count, Data.LightsPoint.Count, Data.LightsNegative.Count));
            for (int i = 0; i < Data.LightsAmbient.Count; i++)
            {
                text.Add(string.Format("Ambient Light {0}", i));
                GenLightText(Data.LightsAmbient[i], ref text);
            }
            for (int i = 0; i < Data.LightsDirectional.Count; i++)
            {
                text.Add(string.Format("Directional Light {0}", i));
                GenLightText(Data.LightsDirectional[i], ref text);
                text.Add(string.Format("Short: {0}; Vector3: {1}; {2}; {3}; {4};", Data.LightsDirectional[i].unkShort, Data.LightsDirectional[i].Vector3.X, Data.LightsDirectional[i].Vector3.Y, Data.LightsDirectional[i].Vector3.Z, Data.LightsDirectional[i].Vector3.W));
            }
            for (int i = 0; i < Data.LightsPoint.Count; i++)
            {
                text.Add(string.Format("Point Light {0}", i));
                GenLightText(Data.LightsPoint[i], ref text);
                text.Add(string.Format("Short: {0}", Data.LightsPoint[i].unkShort));
            }
            for (int i = 0; i < Data.LightsNegative.Count; i++)
            {
                text.Add(string.Format("Negative Light {0}", i));
                GenLightText(Data.LightsNegative[i], ref text);
                text.Add(string.Format("Vector3: {1}; {2}; {3}; {4};", i, Data.LightsNegative[i].Vector3.X, Data.LightsNegative[i].Vector3.Y, Data.LightsNegative[i].Vector3.Z, Data.LightsNegative[i].Vector3.W));
                text.Add(string.Format("Floats: {0}; {1};", Data.LightsNegative[i].unkFloat1, Data.LightsNegative[i].unkFloat2));
                text.Add(string.Format("UInts: {0}; {1}; UShorts: {2}; {3}", i, Data.LightsNegative[i].unkUInt1, Data.LightsNegative[i].unkUInt2, Data.LightsNegative[i].unkUShort1, Data.LightsNegative[i].unkUShort2));
            }

            int depth = 0;
            if (Data.SceneryRoot != null)
            {
                text.Add("\n");
                GenSceneryText(Data.SceneryRoot, depth, ref text);
            }

            TextPrev = text.ToArray();
        }

        private void GenLightText(SceneryData.LightBase ptr, ref List<string> text)
        {
            text.Add(string.Format("Flags: {0:X};{1:X};{2:X};{3:X}; Radius: {4}; R: {5}; G: {6}; B: {7}; F: {8}", ptr.Flags[0], ptr.Flags[1], ptr.Flags[2], ptr.Flags[3], ptr.Radius, ptr.Color_R, ptr.Color_G, ptr.Color_B, ptr.Color_Unk));
            text.Add(string.Format("Position: {0}; {1}; {2}; {3};", ptr.Position.X, ptr.Position.Y, ptr.Position.Z, ptr.Position.W));
            //text.Add(string.Format("Vector1: {0}; {1}; {2}; {3};", ptr.Vector1.X, ptr.Vector1.Y, ptr.Vector1.Z, ptr.Vector1.W));
            //text.Add(string.Format("Vector2: {0}; {1}; {2}; {3};", ptr.Vector2.X, ptr.Vector2.Y, ptr.Vector2.Z, ptr.Vector2.W));
        }

        private void GenSceneryText(SceneryData.SceneryStruct ptr, int depth, ref List<string> text)
        {
            string add = "";
            for (int i = 0; i < depth; i++)
            {
                add += "     ";
            }
            //text.Add("\n");
            text.Add(add + "Scenery Node");
            GenModelText(ptr.Model, depth, ref text);
            //text.Add(add + "Links");
            for (int i = 0; i < ptr.Links.Length; i++)
            {
                if (ptr.Links[i] is SceneryData.SceneryModelStruct)
                {
                    text.Add(add + "Link " + i + ": Model");
                    GenModelText((SceneryData.SceneryModelStruct)ptr.Links[i], depth + 1, ref text);
                }
                else if (ptr.Links[i] is SceneryData.SceneryStruct)
                {
                    text.Add(add + "Link " + i + ": Scenery");
                    GenSceneryText((SceneryData.SceneryStruct)ptr.Links[i], depth + 1, ref text);
                }
            }
            //text.Add("\n");
        }

        private void GenModelText(SceneryData.SceneryModelStruct ptr, int depth, ref List<string> text)
        {
            string add = "";
            for (int i = 0; i < depth; i++)
            {
                add += "     ";
            }
            for (int a = 0; a < ptr.Models.Count; a++)
            {
                if (!ptr.Models[a].isSpecial)
                {
                    text.Add(string.Format(add + "Model {0} ID: {1:X8}", a, ptr.Models[a].ModelID));
                }
                else
                {
                    text.Add(string.Format(add + "Special Model {0} ID: {1:X8}", a, ptr.Models[a].ModelID));
                }
                //text.Add($"Model {a} Bounding Box Vector 1: { ptr.Models[a].ModelBoundingBoxVector1.X }; { ptr.Models[a].ModelBoundingBoxVector1.Y }; { ptr.Models[a].ModelBoundingBoxVector1.Z }; { ptr.Models[a].ModelBoundingBoxVector1.W }");
                //text.Add($"Model {a} Bounding Box Vector 2: { ptr.Models[a].ModelBoundingBoxVector2.X }; { ptr.Models[a].ModelBoundingBoxVector2.Y }; { ptr.Models[a].ModelBoundingBoxVector2.Z }; { ptr.Models[a].ModelBoundingBoxVector2.W }");
                //text.Add(add + $"Model {a} Matrix 1: { ptr.Models[a].ModelMatrix[0].X }; { ptr.Models[a].ModelMatrix[0].Y }; { ptr.Models[a].ModelMatrix[0].Z }; { ptr.Models[a].ModelMatrix[0].W }");
                //text.Add(add + $"Model {a} Matrix 2: { ptr.Models[a].ModelMatrix[1].X }; { ptr.Models[a].ModelMatrix[1].Y }; { ptr.Models[a].ModelMatrix[1].Z }; { ptr.Models[a].ModelMatrix[1].W }");
                //text.Add(add + $"Model {a} Matrix 3: { ptr.Models[a].ModelMatrix[2].X }; { ptr.Models[a].ModelMatrix[2].Y }; { ptr.Models[a].ModelMatrix[2].Z }; { ptr.Models[a].ModelMatrix[2].W }");
                text.Add(add + $"Model {a} Matrix 4 (Position): { ptr.Models[a].ModelMatrix[3].X }; { ptr.Models[a].ModelMatrix[3].Y }; { ptr.Models[a].ModelMatrix[3].Z }; { ptr.Models[a].ModelMatrix[3].W }");
            }
            /*
            for (int a = 0; a < ptr.UnkPos.Length; a++)
            {
                text.Add(add + $"Vector {a}: { ptr.UnkPos[a].X }; { ptr.UnkPos[a].Y }; { ptr.UnkPos[a].Z }; { ptr.UnkPos[a].W };");
            }
            */
            //text.Add("\n");
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor((ItemController)Node.Tag);
        }
    }
}