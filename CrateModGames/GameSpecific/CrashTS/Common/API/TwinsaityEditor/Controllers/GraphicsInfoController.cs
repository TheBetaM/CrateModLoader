using Twinsanity;
using System.Collections.Generic;

namespace TwinsaityEditor
{
    public class GraphicsInfoController : ItemController
    {
        public new GraphicsInfo Data { get; set; }

        public GraphicsInfoController(MainForm topform, GraphicsInfo item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return $"Graphics Info [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();
            text.Add($"ID: {Data.ID}");
            text.Add($"Bounding Box Vector 1: {Data.Coord1.X}; {Data.Coord1.Y}; {Data.Coord1.Z}; {Data.Coord1.W}");
            text.Add($"Bounding Box Vector 2: {Data.Coord2.X}; {Data.Coord2.Y}; {Data.Coord2.Z}; {Data.Coord2.W}");
            text.Add($"Skin ID: {Data.SkinID}");
            text.Add($"Blend Skin Model ID: {Data.BlendSkinID}");
            text.Add($"Type 1 Size: {Data.Type1.Length}");
            text.Add($"Type 2 Size: {Data.Type2.Length}");
            text.Add($"Collision Datas Size: {Data.CollisionData.Length}");
            text.Add($"Rigid Model Count: {Data.ModelIDs.Length}");
            if (Data.ModelIDs.Length > 0)
            {
                text.Add($"Rigid Models Linked:");
                for (int i = 0; i < Data.ModelIDs.Length; i++)
                {
                    text.Add(string.Format("Rigid Model #{0} ID: {1:X8}", Data.ModelIDs[i].ID, Data.ModelIDs[i].ModelID));
                }
            }

            if (Data.Type1.Length > 0)
            {
                text.Add($"Type 1 Structs:");
                for (int i = 0; i < Data.Type1.Length; i++)
                {
                    text.Add($"#{ i } Numbers: { Data.Type1[i].Numbers[0] }; { Data.Type1[i].Numbers[1] }; { Data.Type1[i].Numbers[2] }; { Data.Type1[i].Numbers[3] }; { Data.Type1[i].Numbers[4] }");
                    text.Add($"#{ i } Vector 1: { Data.Type1[i].Matrix[0].X }; { Data.Type1[i].Matrix[0].Y }; { Data.Type1[i].Matrix[0].Z }; { Data.Type1[i].Matrix[0].W }");
                    text.Add($"#{ i } Vector 2: { Data.Type1[i].Matrix[1].X }; { Data.Type1[i].Matrix[1].Y }; { Data.Type1[i].Matrix[1].Z }; { Data.Type1[i].Matrix[1].W }");
                    text.Add($"#{ i } Vector 3: { Data.Type1[i].Matrix[2].X }; { Data.Type1[i].Matrix[2].Y }; { Data.Type1[i].Matrix[2].Z }; { Data.Type1[i].Matrix[2].W }");
                    text.Add($"#{ i } Vector 4: { Data.Type1[i].Matrix[3].X }; { Data.Type1[i].Matrix[3].Y }; { Data.Type1[i].Matrix[3].Z }; { Data.Type1[i].Matrix[3].W }");
                    text.Add($"#{ i } Vector 5: { Data.Type1[i].Matrix[4].X }; { Data.Type1[i].Matrix[4].Y }; { Data.Type1[i].Matrix[4].Z }; { Data.Type1[i].Matrix[4].W }");
                    text.Add($"#{ i } T3 Matrix 1: { Data.Type3[i].Matrix[0].X }; { Data.Type3[i].Matrix[0].Y }; { Data.Type3[i].Matrix[0].Z }; { Data.Type3[i].Matrix[0].W }");
                    text.Add($"#{ i } T3 Matrix 2: { Data.Type3[i].Matrix[1].X }; { Data.Type3[i].Matrix[1].Y }; { Data.Type3[i].Matrix[1].Z }; { Data.Type3[i].Matrix[1].W }");
                    text.Add($"#{ i } T3 Matrix 3: { Data.Type3[i].Matrix[2].X }; { Data.Type3[i].Matrix[2].Y }; { Data.Type3[i].Matrix[2].Z }; { Data.Type3[i].Matrix[2].W }");
                    text.Add($"#{ i } T3 Matrix 4: { Data.Type3[i].Matrix[3].X }; { Data.Type3[i].Matrix[3].Y }; { Data.Type3[i].Matrix[3].Z }; { Data.Type3[i].Matrix[3].W }");
                }
            }
            if (Data.Type2.Length > 0)
            {
                text.Add($"Type 2 Structs:");
                for (int i = 0; i < Data.Type2.Length; i++)
                {
                    text.Add($"#{ i } Numbers: { Data.Type2[i].Numbers[0] }; { Data.Type2[i].Numbers[1] }");
                    text.Add($"#{ i } Matrix 1: { Data.Type2[i].Matrix[0].X }; { Data.Type2[i].Matrix[0].Y }; { Data.Type2[i].Matrix[0].Z }; { Data.Type2[i].Matrix[0].W }");
                    text.Add($"#{ i } Matrix 2: { Data.Type2[i].Matrix[1].X }; { Data.Type2[i].Matrix[1].Y }; { Data.Type2[i].Matrix[1].Z }; { Data.Type2[i].Matrix[1].W }");
                    text.Add($"#{ i } Matrix 3: { Data.Type2[i].Matrix[2].X }; { Data.Type2[i].Matrix[2].Y }; { Data.Type2[i].Matrix[2].Z }; { Data.Type2[i].Matrix[2].W }");
                    text.Add($"#{ i } Matrix 4: { Data.Type2[i].Matrix[3].X }; { Data.Type2[i].Matrix[3].Y }; { Data.Type2[i].Matrix[3].Z }; { Data.Type2[i].Matrix[3].W }");
                }
            }
            if (Data.CollisionData.Length > 0)
            {
                text.Add($"Collision data information:");
                for (var i = 0; i < Data.CollisionData.Length; ++i)
                {
                    var type4 = Data.CollisionData[i];
                    text.Add($"#{ i } Header:");
                    for (var j = 0; j < 11; ++j)
                    {
                        text.Add($" {type4.Header[j]}");
                    }
                    for (var j = 0; j < 7; ++j)
                    {
                        text.Add($"Blob block {j+1}");
                        switch (j)
                        {
                            case 0:
                                text.Add($"\tSize: {type4.Header[5]}");
                                for (var k = 0; k < type4.Header[j]; ++k)
                                {
                                    text.Add($"\t{type4.UnkVectors1[k]}");
                                }
                                break;
                            case 1:
                                text.Add($"\tSize: {type4.Header[6] - type4.Header[5]}");
                                break;
                            case 2:
                                text.Add($"\tSize: {type4.Header[7] - type4.Header[6]}");
                                break;
                            case 3:
                                text.Add($"\tSize: {type4.Header[8] - type4.Header[7]}");
                                break;
                            case 4:
                                text.Add($"\tSize: {type4.Header[9] - type4.Header[8]}");
                                break;
                            case 5:
                                text.Add($"\tSize: {type4.Header[10] - type4.Header[9]}");
                                break;
                            case 6:
                                text.Add($"\tSize: {type4.collisionDataBlob.Length - type4.Header[10]}");
                                break;
                        }
                    }
                }
            }

            TextPrev = text.ToArray();
        }
    }
}