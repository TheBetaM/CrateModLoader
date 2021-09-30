using System.Drawing;
using System.Windows.Forms;
using System;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class GeoDataVis
    {
        /*public Microsoft.DirectX.Direct3D.Device D3D_Device;

        public Buffer VBuffer;
        public Buffer PBuffer;
        
        public Mark[][] Instances = new Mark[][] { };
        public Mesh SphMesh, BoxMesh;
        public GeoData GD;
        private bool Ready = false;
        private Color[] colors = new[] { Color.Gray, Color.Green, Color.Red, Color.DarkBlue, Color.Yellow, Color.Pink, Color.DarkCyan, Color.DarkGreen, Color.DarkRed, Color.Brown, Color.DarkMagenta, Color.Orange, Color.DarkSeaGreen, Color.Bisque, Color.Coral };
        public struct Mark
        {
            public Color c;
            public Vector3 Position;
            public Vector3 Rotation;
            public UInt16 ID;
            public Mesh TextMesh;
        }
        private struct TwinVertex
        {
            public UInt16 Surface;
            public float X, Y, Z, W;
        }
        public void Init()
        {
            Ready = false;
            this.Text = "Converting ID 9 to VertexBuffer. Please wait...";
            Application.DoEvents();
            PresentParameters PP = new PresentParameters();
            // Init D3D
            PP.Windowed = true;
            PP.SwapEffect = SwapEffect.Discard;
            PP.EnableAutoDepthStencil = true;
            PP.AutoDepthStencilFormat = DepthFormat.D16;
            D3D_Device = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, View, CreateFlags.HardwareVertexProcessing, PP);
            // Init Buffers
            float x_max, x_min, y_max, y_min, z_min;
            x_max = 0;
            x_min = 0;
            y_max = 0;
            y_min = 0;
            z_min = 0;
            Array.Resize(ref VertexData, GD.IndexSize * 3);
            UInt16[] surfs = new ushort[] { };
            int cnt = 0;
            if (VBuffer == null)
            {
                for (int i = 0; i <= GD.GroupSize - 1; i++)
                {
                    for (int j = 0; j <= GD.Groups[i].Size - 1; j++)
                    {
                        UInt16 @base = (ushort)(GD.Indexes[j + GD.Groups[i].Offset].Surface);
                        Color c;
                        // c = Color.FromArgb(base Mod 256, Math.Abs(Math.Round(255 * Math.Cos(base))), Math.Abs(Math.Round(255 * Math.Sin(base))))
                        bool flag = false;
                        int pos = 0;
                        for (int a = 0; a <= surfs.Length - 1; a++)
                        {
                            if (surfs[a] == @base)
                            {
                                flag = true;
                                pos = a;
                                a = surfs.Length;
                            }
                        }
                        if (!flag)
                        {
                            Array.Resize(ref surfs, surfs.Length + 1);
                            surfs[surfs.Length - 1] = @base;
                            pos = surfs.Length - 1;
                        }
                        c = colors[GD.Indexes[j + GD.Groups[i].Offset].Surface % colors.Length];
                        VertexData[cnt].X = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert1].X;
                        VertexData[cnt].Y = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert1].Y;
                        VertexData[cnt].Z = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert1].Z;
                        VertexData[cnt].Color = c.ToArgb();
                        float x1 = VertexData[cnt].X;
                        float y1 = VertexData[cnt].Y;
                        float z1 = VertexData[cnt].Z;
                        cnt += 1;
                        VertexData[cnt].X = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert2].X;
                        VertexData[cnt].Y = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert2].Y;
                        VertexData[cnt].Z = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert2].Z;
                        VertexData[cnt].Color = c.ToArgb();
                        float x2 = VertexData[cnt].X;
                        float y2 = VertexData[cnt].Y;
                        float z2 = VertexData[cnt].Z;
                        cnt += 1;
                        VertexData[cnt].X = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert3].X;
                        VertexData[cnt].Y = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert3].Y;
                        VertexData[cnt].Z = GD.Vertex[GD.Indexes[j + GD.Groups[i].Offset].Vert3].Z;
                        VertexData[cnt].Color = c.ToArgb();
                        float x3 = VertexData[cnt].X;
                        float y3 = VertexData[cnt].Y;
                        float z3 = VertexData[cnt].Z;
                        cnt += 1;
                        float nx = ((y1 - y2) * (z1 - z3)) - ((z1 - z2) * (y1 - y3));
                        float ny = ((z1 - z2) * (x1 - x3)) - ((x1 - x2) * (z1 - z3));
                        float nz = ((x1 - x2) * (y1 - y3)) - ((y1 - y2) * (x1 - x3));
                        Vector3 normal = new Vector3(nx, ny, nz);
                        normal.Normalize();
                        normal = -normal;
                        VertexData[cnt - 1].Normal = normal;
                        VertexData[cnt - 2].Normal = normal;
                        VertexData[cnt - 3].Normal = normal;
                        this.Text = (cnt / 3).ToString() + @"\" + GD.IndexSize.ToString();
                        Application.DoEvents();
                    }
                }
                VBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormalColored), VertexData.Length, D3D_Device, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionNormalColored.Format, Pool.Default);
                VBuffer.SetData(VertexData, 0, LockFlags.None);
            }
            for (int i = 0; i <= GD.Vertex.Length - 1; i++)
            {
                if (x_max == 0)
                    x_max = GD.Vertex[i].X;
                else
                    Math.Max(x_max, GD.Vertex[i].X);
                if (y_max == 0)
                    y_max = GD.Vertex[i].Y;
                else
                    Math.Max(y_max, GD.Vertex[i].Y);
                if (x_min == 0)
                    x_min = GD.Vertex[i].X;
                else
                    Math.Min(x_min, GD.Vertex[i].X);
                if (y_min == 0)
                    y_min = GD.Vertex[i].Y;
                else
                    Math.Min(y_min, GD.Vertex[i].Y);
                if (z_min == 0)
                    z_min = GD.Vertex[i].Z;
                else
                    Math.Min(z_min, GD.Vertex[i].Z);
            }
            TwinsanityEditorForm twinsanityEditorForm = (TwinsanityEditorForm)ParentForm;
            for (int i = 0; i <= twinsanityEditorForm.LevelData.Records - 1; i++)
            {
                if (twinsanityEditorForm.LevelData.Item[i].ID < 8)
                {
                    Array.Resize(ref Instances, Instances.Length + 1);
                    InstanceInfoSection IIS = (InstanceInfoSection)twinsanityEditorForm.LevelData.Item[i];
                    Instances INSTS = new Instances();
                    for (int a = 0; a <= IIS.Records - 1; a++)
                    {
                        if (IIS._Item[a].ID == 6)
                        {
                            INSTS = (Instances)IIS._Item[a];
                            a = IIS.Records;
                        }
                    }
                    Array.Resize(ref Instances[Instances.Length - 1], INSTS.Records);
                    for (int j = 0; j <= INSTS.Records - 1; j++)
                    {
                        Instance INST = (Instance)INSTS._Item[j];
                        Instances[Instances.Length - 1][j].Position.X = INST.X;
                        Instances[Instances.Length - 1][j].Position.Y = INST.Y;
                        Instances[Instances.Length - 1][j].Position.Z = INST.Z;
                        Instances[Instances.Length - 1][j].Rotation.X = (float)(INST.RX / (double)65535 * 2 * Math.PI);
                        Instances[Instances.Length - 1][j].Rotation.Y = (float)(INST.RY / (double)65535 * 2 * Math.PI);
                        Instances[Instances.Length - 1][j].Rotation.Z = (float)(INST.RZ / (double)65535 * 2 * Math.PI);
                        Instances[Instances.Length - 1][j].ID = (ushort)INST.ID;
                        Instances[Instances.Length - 1][j].c = colors[colors.Length - Instances.Length];
                        Instances[Instances.Length - 1][j].TextMesh = Mesh.TextFromFont(D3D_Device, new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), INST.ID.ToString(), 1.0F, 0.3F);
                        string dots = "";
                        for (int a = 0; a <= (j % 3); a++)
                            dots += ".";
                        this.Text = cnt.ToString() + @"\" + GD.IndexSize.ToString() + dots;
                        Application.DoEvents();
                    }
                }
            }
            SphMesh = Mesh.Sphere(D3D_Device, 1.0f, 4, 4);
            BoxMesh = Mesh.Box(D3D_Device, 1.0f, 1.0f, 1.0f);
            DI_Device = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
            DI_Device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            DI_Device.Acquire();
            DIM_Device = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Mouse);
            DIM_Device.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            DIM_Device.Acquire();
            Position.X = (float)((x_max + x_min) / (double)2);
            Position.Y = (float)((y_max + y_min) / (double)2);
            Position.Z = z_min;
            Rotation = new Vector3(0, 0, 0);
            Up = new Vector3(0, 1, 0);
            Ready = true;
            this.Text = " Use left mouse button to look, WASDQE to move";
            Application.DoEvents();
            this.Invalidate();
        }
        private Vector3 Position, Rotation, Up;
        private float Range = 500.0F;
        private float Attenuation = 1.0F;
        private void SetupCamera()
        {
            Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
            Vector4 rot_vector = Vector3.Transform(new Vector3(0, 0, 1), rot_matrix);
            D3D_Device.RenderState.CullMode = Cull.None;
            D3D_Device.Transform.Projection = Matrix.PerspectiveFovLH((float)(System.Convert.ToSingle(Math.PI) / (double)4), (float)(this.Width / (double)this.Height), 1.0F, 10000.0F);
            D3D_Device.Transform.View = Matrix.LookAtLH(Position, new Vector3(Position.X + rot_vector.X, Position.Y + rot_vector.Y, Position.Z + rot_vector.Z), Up);
            D3D_Device.RenderState.Lighting = true;
            D3D_Device.Lights[0].Type = LightType.Point;
            D3D_Device.Lights[0].Position = new Vector3(Position.X, Position.Y, Position.Z - 5);
            D3D_Device.Lights[0].Diffuse = Color.White;
            D3D_Device.Lights[0].Attenuation0 = Attenuation;
            D3D_Device.Lights[0].Range = Range;
            D3D_Device.Lights[0].Enabled = true;
            D3D_Device.Lights[0].Update();
        }
        private void GeoDataVis_Paint(object sender, PaintEventArgs e)
        {
            if (Ready & !(VBuffer == null))
            {
                D3D_Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Blue, 1.0F, 0);
                SetupCamera();
                D3D_Device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                D3D_Device.BeginScene();
                for (int i = 0; i <= GD.GroupSize - 1; i++)
                {
                    D3D_Device.SetStreamSource(0, VBuffer, 0);
                    D3D_Device.DrawPrimitives(PrimitiveType.TriangleList, (int)GD.Groups[i].Offset * 3, (int)GD.Groups[i].Size);
                }
                for (int i = 0; i <= Instances.Length - 1; i++)
                {
                    for (int j = 0; j <= Instances[i].Length - 1; j++)
                    {
                        Matrix M = D3D_Device.Transform.World;
                        D3D_Device.Transform.World = Matrix.RotationYawPitchRoll(Instances[i][j].Rotation.Y, Instances[i][j].Rotation.X, Instances[i][j].Rotation.Z) * Matrix.Translation(new Vector3(Instances[i][j].Position.X, Instances[i][j].Position.Y, Instances[i][j].Position.Z));
                        Microsoft.DirectX.Direct3D.Material boxMaterial = new Microsoft.DirectX.Direct3D.Material();
                        boxMaterial.Ambient = Instances[i][j].c;
                        boxMaterial.Diffuse = Instances[i][j].c;
                        D3D_Device.Material = boxMaterial;
                        Instances[i][j].TextMesh.DrawSubset(0);
                        D3D_Device.Transform.World = M;
                    }
                }
                if (DI_Device.GetCurrentKeyboardState()[Key.T])
                {
                    TwinsanityEditorForm twinsanityEditorForm = (TwinsanityEditorForm)ParentForm;
                    for (int i = 0; i <= 7; i++)
                    {
                        InstanceInfoSection INSTS = (InstanceInfoSection)twinsanityEditorForm.LevelData.Item[4 + i];
                        if (INSTS.Records > 0)
                        {
                            for (int j = 0; j <= INSTS._Item[7].Records - 1; j++)
                            {
                                Trigger TRIG = (Twinsanity.Trigger)INSTS._Item[7][j];
                                CustomVertex.PositionNormalColored[] vert = new CustomVertex.PositionNormalColored[8];
                                vert[0].X = TRIG.Coordinate[0].X;
                                vert[0].Y = TRIG.Coordinate[0].Y;
                                vert[0].Z = TRIG.Coordinate[0].Z;
                                vert[0].Normal = new Vector3(-1, -1, -1);
                                vert[0].Normal.Normalize();
                                vert[1].X = TRIG.Coordinate[0].X + TRIG.Coordinate[1].X;
                                vert[1].Y = TRIG.Coordinate[0].Y;
                                vert[1].Z = TRIG.Coordinate[0].Z;
                                vert[1].Normal = new Vector3(1, -1, -1);
                                vert[1].Normal.Normalize();
                                vert[2].X = TRIG.Coordinate[0].X + TRIG.Coordinate[1].X;
                                vert[2].Y = TRIG.Coordinate[0].Y;
                                vert[2].Z = TRIG.Coordinate[0].Z + TRIG.Coordinate[1].Z;
                                vert[2].Normal = new Vector3(1, -1, 1);
                                vert[2].Normal.Normalize();
                                vert[3].X = TRIG.Coordinate[0].X;
                                vert[3].Y = TRIG.Coordinate[0].Y;
                                vert[3].Z = TRIG.Coordinate[0].Z + TRIG.Coordinate[1].Z;
                                vert[3].Normal = new Vector3(-1, -1, 1);
                                vert[3].Normal.Normalize();
                                vert[4].X = TRIG.Coordinate[0].X;
                                vert[4].Y = TRIG.Coordinate[0].Y + TRIG.Coordinate[1].Y;
                                vert[4].Z = TRIG.Coordinate[0].Z;
                                vert[4].Normal = new Vector3(-1, 1, -1);
                                vert[4].Normal.Normalize();
                                vert[5].X = TRIG.Coordinate[0].X + TRIG.Coordinate[1].X;
                                vert[5].Y = TRIG.Coordinate[0].Y + TRIG.Coordinate[1].Y;
                                vert[5].Z = TRIG.Coordinate[0].Z;
                                vert[5].Normal = new Vector3(1, 1, -1);
                                vert[5].Normal.Normalize();
                                vert[6].X = TRIG.Coordinate[0].X + TRIG.Coordinate[1].X;
                                vert[6].Y = TRIG.Coordinate[0].Y + TRIG.Coordinate[1].Y;
                                vert[6].Z = TRIG.Coordinate[0].Z + TRIG.Coordinate[1].Z;
                                vert[6].Normal = new Vector3(1, 1, 1);
                                vert[6].Normal.Normalize();
                                vert[7].X = TRIG.Coordinate[0].X;
                                vert[7].Y = TRIG.Coordinate[0].Y + TRIG.Coordinate[1].Y;
                                vert[7].Z = TRIG.Coordinate[0].Z + TRIG.Coordinate[1].Z;
                                vert[7].Normal = new Vector3(-1, 1, 1);
                                vert[7].Normal.Normalize();
                                for (int a = 0; a <= 7; a++)
                                    vert[a].Color = Color.Cyan.ToArgb();
                                UInt16[] index = new ushort[] { 0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4, 0, 4, 1, 5, 2, 6, 3, 7 };
                                try
                                {
                                    D3D_Device.DrawIndexedUserPrimitives(PrimitiveType.LineList, 0, index.Length, index.Length / 2, index, true, vert);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            for (int j = 0; j <= INSTS._Item[3].Records - 1; j++)
                            {
                                Position POS = (Position)INSTS._Item[3][j];
                                Matrix M = D3D_Device.Transform.World;
                                D3D_Device.Transform.World = Matrix.Translation(new Vector3(POS.Pos.X, POS.Pos.Y, POS.Pos.Z));
                                Microsoft.DirectX.Direct3D.Material boxMaterial = new Microsoft.DirectX.Direct3D.Material();
                                boxMaterial.Ambient = Color.Purple;
                                boxMaterial.Diffuse = Color.Purple;
                                D3D_Device.Material = boxMaterial;
                                SphMesh.DrawSubset(0);
                                Mesh NumMesh = Mesh.TextFromFont(D3D_Device, new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), POS.ID.ToString(), 1.0f, 0.3f);
                                D3D_Device.Transform.World = Matrix.Translation(new Vector3(POS.Pos.X, POS.Pos.Y + 1.5f, POS.Pos.Z));
                                NumMesh.DrawSubset(0);
                                D3D_Device.Transform.World = M;
                            }
                            for (int j = 0; j <= INSTS._Item[1].Records - 1; j++)
                            {
                                Behavior BH = (Behavior)INSTS._Item[1][j];
                                Matrix M = D3D_Device.Transform.World;
                                D3D_Device.Transform.World = Matrix.Translation(new Vector3(BH.Cord.X, BH.Cord.Y, BH.Cord.Z));
                                Microsoft.DirectX.Direct3D.Material boxMaterial = new Microsoft.DirectX.Direct3D.Material();
                                boxMaterial.Ambient = Color.Green;
                                boxMaterial.Diffuse = Color.Green;
                                D3D_Device.Material = boxMaterial;
                                BoxMesh.DrawSubset(0);
                                Mesh NumMesh = Mesh.TextFromFont(D3D_Device, new System.Drawing.Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel), BH.ID.ToString(), 1.0f, 0.3f);
                                D3D_Device.Transform.World = Matrix.Translation(new Vector3(BH.Cord.X, BH.Cord.Y + 1.5f, BH.Cord.Z));
                                NumMesh.DrawSubset(0);
                                D3D_Device.Transform.World = M;
                            }
                            for (int j = 0; j <= INSTS._Item[2].Records - 1; j++)
                            {
                                FuckingShit FS = (FuckingShit)INSTS._Item[2][j];
                                CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[2];
                                verts[0].X = ((Behavior)INSTS._Item[1][FS.I16[0]]).Cord.X;
                                verts[0].Y = ((Behavior)INSTS._Item[1][FS.I16[0]]).Cord.Y;
                                verts[0].Z = ((Behavior)INSTS._Item[1][FS.I16[0]]).Cord.Z;
                                verts[0].Color = System.Drawing.Color.Red.ToArgb();
                                verts[1].X = ((Behavior)INSTS._Item[1][FS.I16[1]]).Cord.X;
                                verts[1].Y = ((Behavior)INSTS._Item[1][FS.I16[1]]).Cord.Y;
                                verts[1].Z = ((Behavior)INSTS._Item[1][FS.I16[1]]).Cord.Z;
                                verts[1].Color = System.Drawing.Color.Red.ToArgb();
                            }
                            for (int j = 0; j <= INSTS._Item[4].Records - 1; j++)
                            {
                                Path PATH = (Path)INSTS._Item[4][j];
                                Matrix M = D3D_Device.Transform.World;
                                Microsoft.DirectX.Direct3D.Material boxMaterial = new Microsoft.DirectX.Direct3D.Material();
                                boxMaterial.Ambient = Color.Orange;
                                boxMaterial.Diffuse = Color.Orange;
                                D3D_Device.Material = boxMaterial;
                                for (int k = 0; k <= PATH.Pos.Length - 1; k++)
                                {
                                    D3D_Device.Transform.World = Matrix.Translation(new Vector3(PATH.Pos[k].X, PATH.Pos[k].Y, PATH.Pos[k].Z));
                                    SphMesh.DrawSubset(0);
                                }
                                D3D_Device.Transform.World = M;
                                for (int k = 0; k <= PATH.Pos.Length - 2; k++)
                                {
                                    CustomVertex.PositionNormalColored[] vert = new CustomVertex.PositionNormalColored[2];
                                    vert[0].Position = new Vector3(PATH.Pos[k].X, PATH.Pos[k].Y, PATH.Pos[k].Z);
                                    vert[1].Position = new Vector3(PATH.Pos[k + 1].X, PATH.Pos[k + 1].Y, PATH.Pos[k + 1].Z);
                                    vert[0].Color = Color.Green.ToArgb();
                                    vert[1].Color = Color.Red.ToArgb();
                                    vert[0].Normal = vert[1].Position - vert[0].Position;
                                    vert[1].Normal = vert[0].Position - vert[1].Position;
                                    vert[0].Normal.Normalize();
                                    vert[1].Normal.Normalize();
                                    D3D_Device.DrawUserPrimitives(PrimitiveType.LineList, 1, vert);
                                }
                            }
                        }
                    }
                }
                D3D_Device.EndScene();
                D3D_Device.Present();
                if (DI_Device.GetCurrentKeyboardState()[Key.W])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0, 0, 1), rot_matrix);
                    Position.Z += rot_vector.Z;
                    Position.X += rot_vector.X;
                    Position.Y += rot_vector.Y;
                }
                else if (DI_Device.GetCurrentKeyboardState()[Key.S])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0, 0, 1), rot_matrix);
                    Position.Z -= rot_vector.Z;
                    Position.X -= rot_vector.X;
                    Position.Y -= rot_vector.Y;
                }
                if (DI_Device.GetCurrentKeyboardState()[Key.D])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(1, 0, 0), rot_matrix);
                    Position.Z += rot_vector.Z;
                    Position.X += rot_vector.X;
                }
                else if (DI_Device.GetCurrentKeyboardState()[Key.A])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(1, 0, 0), rot_matrix);
                    Position.Z -= rot_vector.Z;
                    Position.X -= rot_vector.X;
                }
                if (DI_Device.GetCurrentKeyboardState()[Key.E])
                    Position.Y += 1;
                else if (DI_Device.GetCurrentKeyboardState()[Key.Q])
                    Position.Y -= 1;
                MouseState MS = DIM_Device.CurrentMouseState;
                if (MS.GetMouseButtons()[0] != 0)
                {
                    Rotation.Y += (float)(MS.Y / (double)160 * Up.Y);
                    Rotation.X += (float)(MS.X / (double)160 * Up.Y);
                }

                Application.DoEvents();
                this.Invalidate();
            }
        }*/

        private void GeoDataVis_Load(object sender, EventArgs e)
        {
        }
    }
}
