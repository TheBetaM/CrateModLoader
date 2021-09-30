using System;
using System.Collections.Generic;
using Twinsanity;
using System.Text;

namespace TwinsaityEditor
{
    public class DynamicSceneryDataController : ItemController
    {
        public new DynamicSceneryData Data { get; set; }

        public DynamicSceneryDataController(MainForm topform, DynamicSceneryData item) : base(topform, item)
        {
            Data = item;
        }

        protected override string GetName()
        {
            return $"Dynamic Scenery Data [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add($"Model Count: {Data.Models.Count}");

            if (Data.Models.Count > 0)
            {
                for (int i = 0; i < Data.Models.Count; i++)
                {
                    text.Add($"Model {i} ID: {string.Format("{0:X8}", Data.Models[i].ModelID)} Vars: {Data.Models[i].UnkInt1}, {Data.Models[i].unkInt2}, {Data.Models[i].unkByte}");

                    if (Data.Models[i].GI_Types.Count > 0)
                    {
                        for (int g = 0; g < Data.Models[i].GI_Types.Count; g++)
                        {
                            text.Add($"GI {g}: Blob Size: {Data.Models[i].GI_Types[g].unkBlob.Length}");
                            /*
                            string HBlob = "";
                            for (int b = 0; b < Data.Models[i].GI_Types[g].Header.Length; b++)
                            {
                                HBlob += string.Format("{0:X}", Data.Models[i].GI_Types[g].Header[b]);
                            }
                            text.Add($"GI Header: {HBlob}");
                            
                            string Blob = "";
                            for (int b = 0; b < Data.Models[i].GI_Types[g].unkBlob.Length; b++)
                            {
                                Blob += string.Format("{0:X}", Data.Models[i].GI_Types[g].unkBlob[b]);
                            }
                            text.Add($"Blob: {Blob}");
                            */

                        }
                    }

                    text.Add($"DynBlob Length: {Data.Models[i].dynBlob.Length}");
                    /*
                    string DynBlob = "";
                    for (int b = 0; b < Data.Models[i].dynBlob.Length; b++)
                    {
                        DynBlob += string.Format("{0:X}", Data.Models[i].dynBlob[b]);
                    }
                    text.Add($"DynBlob: {DynBlob}");
                    */

                    //text.Add($"Model {i} Position: {Data.Models[i].WorldPosition.X}; {Data.Models[i].WorldPosition.Y}; {Data.Models[i].WorldPosition.Z}; {Data.Models[i].WorldPosition.W}; ");
                    //text.Add($"Model {i} Rotation?: {Data.Models[i].LocalRotation[0]}; {Data.Models[i].LocalRotation[1]}; {Data.Models[i].LocalRotation[2]}; ");
                    //text.Add($"Bounding Box Vector 1: {Data.Models[i].BoundingBoxVector1.X}; {Data.Models[i].BoundingBoxVector1.Y}; {Data.Models[i].BoundingBoxVector1.Z}; {Data.Models[i].BoundingBoxVector1.W}; ");
                    //text.Add($"Bounding Box Vector 2: {Data.Models[i].BoundingBoxVector2.X}; {Data.Models[i].BoundingBoxVector2.Y}; {Data.Models[i].BoundingBoxVector2.Z}; {Data.Models[i].BoundingBoxVector2.W}; ");
                    text.Add("");
                }
            }

            TextPrev = text.ToArray();
        }
    }
}