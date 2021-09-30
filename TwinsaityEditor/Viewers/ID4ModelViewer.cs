using System.Drawing;
using System.Windows.Forms;
using System;
/*using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;*/
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class ID4ModelViewer
    {
        /*public Microsoft.DirectX.Direct3D.Device D3D_Device;
        public Microsoft.DirectX.DirectInput.Device DI_Device;
        public Microsoft.DirectX.DirectInput.Device DIM_Device;
        public VertexBuffer VBuffer;
        public VertexBuffer PBuffer;
        public ID4Model Model;
        public CustomVertex.PositionNormalColored[] VertexData = null;
        private bool Ready = false;
        private Color[] colors = new[] { Color.Gray, Color.Green, Color.Red, Color.DarkBlue, Color.Yellow, Color.Pink, Color.DarkCyan, Color.DarkGreen, Color.DarkRed, Color.Brown, Color.DarkMagenta, Color.Orange, Color.DarkSeaGreen, Color.Bisque, Color.Coral };
        private _Map[][] Map;
        private struct _Map
        {
            public uint start_vert;
            public uint end_vert;
            public bool tristrip;
        }
        public void Init()
        {
            Ready = false;
            this.Text = "Converting Model to VertexBuffer. Please wait...";
            Application.DoEvents();
            PresentParameters PP = new PresentParameters();
            // Init D3D
            PP.Windowed = true;
            PP.SwapEffect = SwapEffect.Discard;
            PP.EnableAutoDepthStencil = true;
            PP.AutoDepthStencilFormat = DepthFormat.D16;
            D3D_Device = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, View, CreateFlags.SoftwareVertexProcessing, PP);
            // Init Buffers
            float x_max, x_min, y_max, y_min, z_min;
            x_max = 0;
            x_min = 0;
            y_max = 0;
            y_min = 0;
            z_min = 0;
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
            this.Text = " Use mouse to look, WASDQE to move";
            Application.DoEvents();
            System.IO.FileStream Log = new System.IO.FileStream(Application.StartupPath + @"\log.txt", System.IO.FileMode.Create);
            System.IO.StreamWriter LogWriter = new System.IO.StreamWriter(Log);
            for (int i = 0; i <= Model.SubModels - 1; i++)
            {
                LogWriter.WriteLine("Sub Model " + i.ToString());
                for (int j = 0; j <= Model.SubModel[i].Group.Length - 1; j++)
                {
                    LogWriter.WriteLine(" Group " + j.ToString());
                    for (int k = 0; k <= Model.SubModel[i].Group[j].Vertexes - 1; k++)
                    {
                        LogWriter.WriteLine("  Summary Record " + k.ToString());
                        LogWriter.WriteLine("   S3ID1 " + Model.SubModel[i].Group[j].Struct3[k].ID1.ToString());
                        LogWriter.WriteLine("   S3ID2 " + Model.SubModel[i].Group[j].Struct3[k].ID2.ToString());
                        LogWriter.WriteLine("   S4ID1 " + Model.SubModel[i].Group[j].Struct4[k].ID1.ToString());
                        LogWriter.WriteLine("   S4ID2 " + Model.SubModel[i].Group[j].Struct4[k].ID2.ToString());
                        LogWriter.WriteLine("   S6COLOR " + Model.SubModel[i].Group[j].Struct6[k].ToString());
                        LogWriter.WriteLine("   S5D " + Model.SubModel[i].Group[j].Struct5[k].Float.ToString() + " " + Model.SubModel[i].Group[j].Struct5[k].a.ToString() + " " + Model.SubModel[i].Group[j].Struct5[k].b.ToString());
                        LogWriter.WriteLine("   S5ID " + Model.SubModel[i].Group[j].Struct5[k].ID.ToString());
                        LogWriter.WriteLine("   S5CONN " + Model.SubModel[i].Group[j].Struct5[k].CONN.ToString());
                    }
                }
            }
            LogWriter.Close();
            Log.Close();
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
        private void ID4ModelViewr_Load(object sender, EventArgs e)
        {
            if (Ready)
            {
                D3D_Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Blue, 1.0F, 0);
                SetupCamera();
                D3D_Device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                D3D_Device.BeginScene();
                for (int i = 0; i <= Model.SubModels - 1; i++)
                {
                    for (int j = 0; j <= Model.SubModel[i].Group.Length - 1; j++)
                    {
                        for (int k = 0; k <= Model.SubModel[i].Group[j].Vertexes - 3; k++)
                        {
                            CustomVertex.PositionNormalColored[] verts = new CustomVertex.PositionNormalColored[1];
                            verts[0].Position = new Vector3(Model.SubModel[i].Group[j].Struct5[k].Float * 2, 0, 0);
                            D3D_Device.DrawUserPrimitives(PrimitiveType.PointList, 1, verts);
                        }
                    }
                }
                D3D_Device.EndScene();
                D3D_Device.Present();
                if (DI_Device.GetCurrentKeyboardState()[Key.W])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0, 0, 0.1f), rot_matrix);
                    Position.Z += rot_vector.Z;
                    Position.X += rot_vector.X;
                    Position.Y += rot_vector.Y;
                }
                else if (DI_Device.GetCurrentKeyboardState()[Key.S])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0, 0, 0.1f), rot_matrix);
                    Position.Z -= rot_vector.Z;
                    Position.X -= rot_vector.X;
                    Position.Y -= rot_vector.Y;
                }
                if (DI_Device.GetCurrentKeyboardState()[Key.D])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0.1f, 0, 0), rot_matrix);
                    Position.Z += rot_vector.Z;
                    Position.X += rot_vector.X;
                }
                else if (DI_Device.GetCurrentKeyboardState()[Key.A])
                {
                    Matrix rot_matrix = Matrix.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                    Vector4 rot_vector = Vector3.Transform(new Vector3(0.1f, 0, 0), rot_matrix);
                    Position.Z -= rot_vector.Z;
                    Position.X -= rot_vector.X;
                }
                if (DI_Device.GetCurrentKeyboardState()[Key.E])
                    Position.Y += 0.1f;
                else if (DI_Device.GetCurrentKeyboardState()[Key.Q])
                    Position.Y -= 0.1f;
                MouseState MS = DIM_Device.CurrentMouseState;
                Rotation.Y += (float)(MS.Y / (double)160 * Up.Y);
                Rotation.X += (float)(MS.X / (double)160 * Up.Y);
                Application.DoEvents();
                this.Invalidate();
            }
        }*/
    }
}
