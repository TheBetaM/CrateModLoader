using Twinsanity;
using System.Collections.Generic;
using System;

namespace TwinsaityEditor
{
    public class CameraController : ItemController
    {
        public new Camera Data { get; set; }

        public CameraController(MainForm topform, Camera item) : base(topform, item)
        {
            Data = item;
            //AddMenu("Open editor", Menu_OpenEditor);
        }

        protected override string GetName()
        {
            return string.Format("Camera [ID {0}]", Data.ID);
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();

            text.Add(string.Format("ID: {0:X8}", Data.ID));
            text.Add($"Size: {Data.Size}");
            text.Add($"Other ({Data.Coords[0].X}, {Data.Coords[0].Y}, {Data.Coords[0].Z}, {Data.Coords[0].W})");
            text.Add($"Position ({Data.Coords[1].X}, {Data.Coords[1].Y}, {Data.Coords[1].Z}, {Data.Coords[1].W})");
            text.Add($"Size ({Data.Coords[2].X}, {Data.Coords[2].Y}, {Data.Coords[2].Z}, {Data.Coords[2].W})");
            text.Add($"Emabled: {Data.Enabled} SomeFloat: {Data.SomeFloat} SectionHead: {Data.SectionHead}");

            text.Add($"Instances: {Data.Instances.Count}");
            for (int i = 0; i < Data.Instances.Count; ++i)
            {
                string obj_name = MainFile.GetObjectName((ushort)MainFile.GetInstanceID(Data.Parent.Parent.ID, Data.Instances[i]));
                obj_name = Utils.TextUtils.TruncateObjectName(obj_name, (ushort)MainFile.GetInstanceID(Data.Parent.Parent.ID, Data.Instances[i]), "", " (Not in Objects)");

                text.Add($"Instance {Data.Instances[i]} {(obj_name != string.Empty ? $" - {obj_name}" : string.Empty)}");
            }

            string nullText = "N/A";
            text.Add("");
            text.Add($"Cam Header: {Data.CamHeader} {Data.CamHeader2}");
            string flag = Convert.ToString(Data.CamHeader, 2);
            if (flag.Length < 16)
            {
                while (flag.Length < 16)
                {
                    flag = "0" + flag;
                }
            }
            text.Add(flag);
            flag = Convert.ToString(Data.CamHeader2, 2);
            if (flag.Length < 16)
            {
                while (flag.Length < 16)
                {
                    flag = "0" + flag;
                }
            }
            text.Add(flag);
            if (Data.ParentType != SectionType.CameraDemo)
            {
                text.Add($"Short: {Data.UnkShort}");
            }
            text.Add($"Float: {Data.UnkFloat1}");
            if (Data.ParentType != SectionType.CameraDemo)
            {
                text.Add($"Byte: {Data.UnkByte}");
            }

            text.Add("");
            text.Add($"UnkCoord1 ({Data.UnkCoords1.X}, {Data.UnkCoords1.Y}, {Data.UnkCoords1.Z}, {Data.UnkCoords1.W})");
            text.Add($"UnkCoord2 ({Data.UnkCoords2.X}, {Data.UnkCoords2.Y}, {Data.UnkCoords2.Z}, {Data.UnkCoords2.W})");
            text.Add($"Float2: {(Data.UnkFloat2 != null ? Data.UnkFloat2.ToString() : nullText)}");
            text.Add($"Float3: {(Data.UnkFloat3 != null ? Data.UnkFloat3.ToString() : nullText)}");
            text.Add($"UInt1: {(Data.UnkUInt1 != null ? Data.UnkUInt1.ToString() : nullText)}");
            text.Add($"UInt2: {(Data.UnkUInt2 != null ? Data.UnkUInt2.ToString() : nullText)}");
            text.Add($"UInt3: {(Data.UnkUInt3 != null ? Data.UnkUInt3.ToString() : nullText)}");
            text.Add($"UInt4: {(Data.UnkUInt4 != null ? Data.UnkUInt4.ToString() : nullText)}");
            text.Add($"Int5: {(Data.UnkInt5 != null ? Data.UnkInt5.ToString() : nullText)}");
            text.Add($"Int6: {(Data.UnkInt6 != null ? Data.UnkInt6.ToString() : nullText)}");
            text.Add($"Float4: {(Data.UnkFloat4 != null ? Data.UnkFloat4.ToString() : nullText)}");
            text.Add($"Float5: {(Data.UnkFloat5 != null ? Data.UnkFloat5.ToString() : nullText)}");
            text.Add($"Float6: {(Data.UnkFloat6 != null ? Data.UnkFloat6.ToString() : nullText)}");
            text.Add($"Float7: {(Data.UnkFloat7 != null ? Data.UnkFloat7.ToString() : nullText)}");
            text.Add($"UInt7: {(Data.UnkUInt7 != null ? Data.UnkUInt7.ToString() : nullText)}");
            text.Add($"Int8: {(Data.UnkInt8 != null ? Data.UnkInt8.ToString() : nullText)}");
            text.Add($"UInt9: {(Data.UnkUInt9 != null ? Data.UnkUInt9.ToString() : nullText)}");
            text.Add($"Float8: {(Data.UnkFloat8 != null ? Data.UnkFloat8.ToString() : nullText)}");
            text.Add("");

            if (Data.CameraType1 != 3)
            {
                text.Add($"Camera 1 Type: {Data.CameraType1:X};");
                GenTextCamera(Data.CameraType1, Data.Cameras[0], ref text);
            }
            else
            {
                text.Add($"Camera 1: N/A");
            }
            text.Add("");

            if (Data.CameraType2 != 3)
            {
                text.Add($"Camera 2 Type: {Data.CameraType2:X};");
                GenTextCamera(Data.CameraType2, Data.Cameras[1], ref text);
            }
            else
            {
                text.Add($"Camera 2: N/A");
            }

            TextPrev = text.ToArray();

        }

        private void GenTextCamera(uint Type, object cam, ref List<string> text)
        {
            switch (Type)
            {
                default:
                    throw new NotImplementedException();
                case 0xA19:
                    Camera.Camera_Boss Camera1 = (Camera.Camera_Boss)cam;

                    text.Add($"Boss Camera");
                    text.Add($"Int {Camera1.unkInt}");
                    text.Add($"Float1 {Camera1.unkFloat1}");
                    text.Add($"Float2 {Camera1.unkFloat2}");
                    for (int i = 0; i < Camera1.unkMatrix1.Length; ++i)
                    {
                        text.Add($"Matrix1 {i}: {Camera1.unkMatrix1[i].X}; {Camera1.unkMatrix1[i].Y}; {Camera1.unkMatrix1[i].Z}; {Camera1.unkMatrix1[i].W}; ");
                    }
                    for (int i = 0; i < Camera1.unkMatrix1.Length; ++i)
                    {
                        text.Add($"Matrix2 {i}: {Camera1.unkMatrix2[i].X}; {Camera1.unkMatrix2[i].Y}; {Camera1.unkMatrix2[i].Z}; {Camera1.unkMatrix2[i].W}; ");
                    }
                    text.Add($"Vector: {Camera1.unkVector.X}; {Camera1.unkVector.Y}; {Camera1.unkVector.Z}; {Camera1.unkVector.W}; ");
                    text.Add($"Byte1 {Camera1.unkByte1}");
                    text.Add($"Float3 {Camera1.unkFloat3}");
                    text.Add($"Float4 {Camera1.unkFloat4}");
                    text.Add($"Float5 {Camera1.unkFloat5}");
                    text.Add($"Float6 {Camera1.unkFloat6}");
                    text.Add($"Byte2 {Camera1.unkByte2}");

                    break;
                case 0x1C02:
                    Camera.Camera_Point Camera2 = (Camera.Camera_Point)cam;

                    text.Add($"Camera Point");
                    text.Add($"Int {Camera2.unkInt}");
                    text.Add($"Float1 {Camera2.unkFloat1}");
                    text.Add($"Float2 {Camera2.unkFloat2}");
                    text.Add($"Vector: {Camera2.unkVector.X}; {Camera2.unkVector.Y}; {Camera2.unkVector.Z}; {Camera2.unkVector.W}; ");

                    break;
                case 0x1C03:
                    Camera.Camera_Line Camera3 = (Camera.Camera_Line)cam;

                    text.Add($"Camera Line");
                    text.Add($"Int {Camera3.unkInt}");
                    text.Add($"Float1 {Camera3.unkFloat1}");
                    text.Add($"Float2 {Camera3.unkFloat2}");
                    text.Add($"Bounding Box Vector 1: {Camera3.unkBoundingBoxVector1.X}; {Camera3.unkBoundingBoxVector1.Y}; {Camera3.unkBoundingBoxVector1.Z}; {Camera3.unkBoundingBoxVector1.W}; ");
                    text.Add($"Bounding Box Vector 2: {Camera3.unkBoundingBoxVector2.X}; {Camera3.unkBoundingBoxVector2.Y}; {Camera3.unkBoundingBoxVector2.Z}; {Camera3.unkBoundingBoxVector2.W}; ");

                    break;
                case 0x1C04:
                    Camera.Camera_Path Camera4 = (Camera.Camera_Path)cam;

                    text.Add($"Camera Path");
                    text.Add($"Int {Camera4.unkInt}");
                    text.Add($"Float1 {Camera4.unkFloat1}");
                    text.Add($"Float2 {Camera4.unkFloat2}");

                    for (int i = 0; i < Camera4.unkVectors.Length; ++i)
                    {
                        text.Add($"Vector {i}: {Camera4.unkVectors[i].X}; {Camera4.unkVectors[i].Y}; {Camera4.unkVectors[i].Z}; {Camera4.unkVectors[i].W}; ");
                    }

                    text.Add($"Int2 {Camera4.unkInt2}");
                    for (int i = 0; i < Camera4.unkData.Length - 7; i = i + 8)
                    {
                        text.Add($"Data Bytes {i / 8}: {Camera4.unkData[i + 0]}; {Camera4.unkData[i + 1]}; {Camera4.unkData[i + 2]}; {Camera4.unkData[i + 3]}; {Camera4.unkData[i + 4]}; {Camera4.unkData[i + 5]}; {Camera4.unkData[i + 6]}; {Camera4.unkData[i + 7]}; ");
                    }

                    break;
                case 0x1C05:
                    Camera.Camera_0x1C05 Camera5 = (Camera.Camera_0x1C05)cam;
                    text.Add($"NULL Camera");

                    break;
                case 0x1C06:
                    Camera.Camera_Spline Camera6 = (Camera.Camera_Spline)cam;

                    text.Add($"Camera Spline");
                    text.Add($"Int {Camera6.unkInt}");
                    text.Add($"Float1 {Camera6.unkFloat1}");
                    text.Add($"Float2 {Camera6.unkFloat2}");
                    text.Add($"Int2 {Camera6.unkUInt}");
                    text.Add($"Float3 {Camera6.unkFloat3}");
                    text.Add($"Short {Camera6.unkShort}");

                    for (int i = 0; i < Camera6.unkVectors.Length; ++i)
                    {
                        text.Add($"Vector {i}: {Camera6.unkVectors[i].X}; {Camera6.unkVectors[i].Y}; {Camera6.unkVectors[i].Z}; {Camera6.unkVectors[i].W}; ");
                        //text.Add($"Bytes {i}: {Camera6.unkData[(i * 4) + 0]}; {Camera6.unkData[(i * 4) + 1]}; {Camera6.unkData[(i * 4) + 2]}; {Camera6.unkData[(i * 4) + 3]}; ");
                    }

                    break;
                case 0x1C09:
                    Camera.Camera_0x1C09 Camera7 = (Camera.Camera_0x1C09)cam;

                    text.Add($"Unknown Camera 1");
                    text.Add($"Int {Camera7.unkInt}");
                    text.Add($"Float1 {Camera7.unkFloat1}");
                    text.Add($"Float2 {Camera7.unkFloat2}");

                    break;
                case 0x1C0B:
                    Camera.Camera_Point2 Camera8 = (Camera.Camera_Point2)cam;

                    text.Add($"Camera Point 2");
                    text.Add($"Int {Camera8.unkInt}");
                    text.Add($"Float1 {Camera8.unkFloat1}");
                    text.Add($"Float2 {Camera8.unkFloat2}");
                    text.Add($"Vector: {Camera8.unkVector.X}; {Camera8.unkVector.Y}; {Camera8.unkVector.Z}; {Camera8.unkVector.W}; ");
                    text.Add($"Float3 {Camera8.unkFloat3}");
                    text.Add($"Byte {Camera8.unkByte}");

                    break;
                case 0x1C0C:
                    Camera.Camera_0x1C0C Camera9 = (Camera.Camera_0x1C0C)cam;

                    text.Add($"Unknown Camera 2");
                    text.Add($"Byte1 {Camera9.unkByte1}");
                    text.Add($"Byte2 {Camera9.unkByte2}");
                    text.Add($"Byte3 {Camera9.unkByte3}");
                    text.Add($"Byte4 {Camera9.unkByte4}");

                    break;
                case 0x1C0D:
                    Camera.Camera_Line2 Camera10 = (Camera.Camera_Line2)cam;

                    text.Add($"Camera Line 2");
                    text.Add($"Int {Camera10.unkInt}");
                    text.Add($"Float1 {Camera10.unkFloat1}");
                    text.Add($"Float2 {Camera10.unkFloat2}");
                    text.Add($"Bounding Box Vector 1: {Camera10.unkBoundingBoxVector1.X}; {Camera10.unkBoundingBoxVector1.Y}; {Camera10.unkBoundingBoxVector1.Z}; {Camera10.unkBoundingBoxVector1.W}; ");
                    text.Add($"Bounding Box Vector 2: {Camera10.unkBoundingBoxVector2.X}; {Camera10.unkBoundingBoxVector2.Y}; {Camera10.unkBoundingBoxVector2.Z}; {Camera10.unkBoundingBoxVector2.W}; ");
                    text.Add($"Float3 {Camera10.unkFloat3}");
                    text.Add($"Float4 {Camera10.unkFloat4}");

                    break;
                case 0x1C0E:
                    Camera.Camera_0x1C0E Camera11 = (Camera.Camera_0x1C0E)cam;
                    text.Add($"EMPTY Camera");

                    break;
                case 0x1C0F:
                    Camera.Camera_Zone Camera12 = (Camera.Camera_Zone)cam;

                    text.Add($"Zone Camera");

                    text.Add($"Data1");
                    for (int i = 0; i < Camera12.Data1_Vectors.Length; i++)
                    {
                        text.Add($"Vector {i}: {Camera12.Data1_Vectors[i].X}; {Camera12.Data1_Vectors[i].Y}; {Camera12.Data1_Vectors[i].Z}; {Camera12.Data1_Vectors[i].W}; ");
                    }
                    text.Add($"Int1 {Camera12.Data1_unkInt1}");
                    text.Add($"Int2 {Camera12.Data1_unkInt2}");

                    text.Add($"Data2");
                    for (int i = 0; i < Camera12.Data2_Vectors.Length; i++)
                    {
                        text.Add($"Vector {i}: {Camera12.Data2_Vectors[i].X}; {Camera12.Data2_Vectors[i].Y}; {Camera12.Data2_Vectors[i].Z}; {Camera12.Data2_Vectors[i].W}; ");
                    }
                    text.Add($"Int1 {Camera12.Data2_unkInt1}");
                    text.Add($"Int2 {Camera12.Data2_unkInt2}");

                    break;

            }
        }

        private void Menu_OpenEditor()
        {
            MainFile.OpenEditor((SectionController)Node.Parent.Tag);
        }
    }
}