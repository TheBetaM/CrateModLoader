using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Twinsanity;

/*  CURRENT ISSUES:
 *  Texture applying and rendering is completely wrong, some values need to be normalized for OpenGL(Switch to shaders later to allow multiple renderings)
 *  Model doesn't draw until camera was rotated at least once
 */

namespace TwinsaityEditor
{
    public partial class ModelViewer
    {

        private _VertexFormat[] VBuffer = null;
        private uint[] IBuffer = null;

        public Model Model;

        public int[] Textures = new int[] { };
        public Texture[] TwinTextures = new Texture[] { };

        private bool Ready = false;
        private Color[] colors = new[] { Color.Gray, Color.Green, Color.Red, Color.DarkBlue, Color.Yellow, Color.Pink, Color.DarkCyan, Color.DarkGreen, Color.DarkRed, Color.Brown, Color.DarkMagenta, Color.Orange, Color.DarkSeaGreen, Color.Bisque, Color.Coral };
        private _Map[][] Map;
        private List<Color> Emission;

        private List<int> _bufferID = new List<int>();
        private List<int> _arrayID = new List<int>();

        private struct _Map
        {
            public uint start_vert;
            public uint end_vert;
            public bool tristrip;
        }

        private struct _VertexFormat
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 UV;
        }

        public ModelViewer()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Ready = false;
            this.Text = "Converting Model to VertexBuffer. Please wait...";
            Application.DoEvents();
            
            // Init Textures
            Bitmap BMP = new Bitmap(Properties.Resources.boatguy);
            Textures = new int[Textures.Length + Model.SubModels];

            for (int i = 0; i < Model.SubModels; i++)
            {
                if (i < TwinTextures.Length)
                {
                    Bitmap TexBMP = new Bitmap(System.Convert.ToInt32(TwinTextures[i].Width), System.Convert.ToInt32(TwinTextures[i].Height));
                    for (int j = 0; j <= TwinTextures[i].RawData.Length - 1; j++)
                        TexBMP.SetPixel((int)(j % TwinTextures[i].Width), (int)(j / TwinTextures[i].Width), TwinTextures[i].RawData[j]);
                    Textures[i] = LoadTexture(TexBMP);//new Texture2D(TexBMP.GetHbitmap());//renderDevice.CreateTexture2D(new Texture2DDescription { BindingOptions = BindingOptions.VertexBuffer, CpuAccessOptions = CpuAccessOptions.Read, Format = Format.B8G8R8A8Typeless, MipLevels = 0,  },new SubresourceData { SystemMemory = TexBMP.GetHbitmap() });//new Microsoft.DirectX.Direct3D.Texture(renderDevice, TexBMP, Usage.None, Pool.Managed);
                    //TexBMP.Dispose();
                }
                else
                    Textures[i] = LoadTexture(BMP);//renderDevice.CreateTexture2D(new Texture2DDescription { BindingOptions = BindingOptions.VertexBuffer, CpuAccessOptions = CpuAccessOptions.Read, Format = Format.B8G8R8A8Typeless, MipLevels = 0, }, new SubresourceData { SystemMemory = BMP.GetHbitmap() });//new Microsoft.DirectX.Direct3D.Texture(renderDevice, BMP, Usage.None, Pool.Managed);
            }
            //BMP.Dispose();

            // Init Buffers
            float x_max, x_min, y_max, y_min, z_min;
            x_max = 0;
            x_min = 0;
            y_max = 0;
            y_min = 0;
            z_min = 0;
            int Vertexes = 0;

            for (int i = 0; i <= Model.SubModels - 1; i++)
                Vertexes += Model.SubModel[i].Vertexes;
            
            Emission = new List<Color>();
            //var positions = new List<Vector3>();
            //var normals = new List<Vector3>();
            //var uvs = new List<Vector2>();
            var verteces = new List<_VertexFormat>();
            var indices = new uint[Vertexes];
            for (int i = 0; i < Model.SubModels; i++)
            {
                for (int j = 0; j < Model.SubModel[i].Group.Length; j++)
                {
                    for (int k = 0; k < Model.SubModel[i].Group[j].Vertexes; k++)
                    {
                        /*VertexData[cnt].X = Model.SubModel[i].Group[j].Vertex[k].X;
                        VertexData[cnt].Y = Model.SubModel[i].Group[j].Vertex[k].Y;
                        VertexData[cnt].Z = Model.SubModel[i].Group[j].Vertex[k].Z;
                        if (Model.SubModel[i].Group[j].UV.Length - 1 >= k)
                            VertexData[cnt].Normal = new Vector3(Model.SubModel[i].Group[j].UV[k].X, Model.SubModel[i].Group[j].UV[k].Y, Model.SubModel[i].Group[j].UV[k].Z);
                        else
                            VertexData[cnt].Normal = new Vector3(-Model.SubModel[i].Group[j].Weight[k].X, -Model.SubModel[i].Group[j].Weight[k].Y, -Model.SubModel[i].Group[j].Weight[k].Z);*/
                        var positions = new Vector3(Model.SubModel[i].Group[j].Vertex[k].X, Model.SubModel[i].Group[j].Vertex[k].Y, Model.SubModel[i].Group[j].Vertex[k].Z);

                        //vertexes[cnt].Position *= 10;

                        Vector3 normals;
                        if (Model.SubModel[i].Group[j].UV.Length > k)
                            normals = new Vector3(Model.SubModel[i].Group[j].UV[k].X, Model.SubModel[i].Group[j].UV[k].Y, Model.SubModel[i].Group[j].UV[k].Z);
                        else
                            normals = new Vector3(-Model.SubModel[i].Group[j].Weight[k].X, -Model.SubModel[i].Group[j].Weight[k].Y, -Model.SubModel[i].Group[j].Weight[k].Z);

                        if (Model.SubModel[i].Group[j].Shit.Length > k)
                        {
                            uint c = Model.SubModel[i].Group[j].Shit[k];
                            byte a, r, g, b;
                            r = (byte)(c & 0xFF);
                            g = (byte)((c >> 8) & 0xFF);
                            b = (byte)((c >> 16) & 0xFF);
                            a = (byte)((c >> 24) & 0xFF);
                            Emission.Add(Color.FromArgb(a, r, g, b));
                        }
                        else
                            Emission.Add(Color.Gray);

                        var uvs = new Vector2(Model.SubModel[i].Group[j].Weight[k].X * Model.SubModel[i].Group[j].Weight[k].Z, 1 - Model.SubModel[i].Group[j].Weight[k].Y * Model.SubModel[i].Group[j].Weight[k].Z);

                        var vertex = new _VertexFormat();
                        vertex.Position = positions;
                        vertex.Normal = normals;
                        vertex.UV = uvs;
                        verteces.Add(vertex);
                    }
                }

                var offset = 0;
                var ind_list = new List<uint>();

                //Calculate indices
                for (int j = 0; j <= Model.SubModel[i].Group.Length - 1; j++)
                {
                    for (int k = 0; k <= Model.SubModel[i].Group[j].Vertexes - 3; k++)
                    {
                        if (Model.SubModel[i].Group[j].Weight[k + 2].CONN != 128)
                        {
                            ind_list.Add((uint)(offset + k));
                        }
                    }
                    offset += Model.SubModel[i].Group[j].Vertexes;
                }
                indices = ind_list.ToArray();

            }

            VBuffer = verteces.ToArray();

            IBuffer = indices;

            _bufferID.Add(GL.GenBuffer());
            var bID = _bufferID[_bufferID.Count - 1];
            GL.BindBuffer(BufferTarget.ArrayBuffer, bID);
            GL.BufferData(BufferTarget.ArrayBuffer, VBuffer.Length * Marshal.SizeOf(new _VertexFormat()), VBuffer, BufferUsageHint.StaticDraw);
            

            _arrayID.Add(GL.GenBuffer());
            var aID = _arrayID[_arrayID.Count - 1];
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, aID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * IBuffer.Length, IBuffer, BufferUsageHint.StaticDraw);


            Position.X = (float)((x_max + x_min) / (double)2);
            Position.Y = (float)((y_max + y_min) / (double)2);
            Position.Z = z_min;
            Rotation = new Vector3(0, 0, 0);
            Up = new Vector3(0, 1, 0);
            InitPipeline();
            Ready = true;
            this.Text = "Use mouse to look, WASDQE to move, hold left shift to speed up";
            Application.DoEvents();
            this.Invalidate();
        }
        internal void InitPipeline()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Texture2D);
            GL.Light(LightName.Light0, LightParameter.Diffuse, Color.Yellow);

            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);

            GL.Disable(EnableCap.CullFace);

            GL.PushAttrib(AttribMask.ColorBufferBit | AttribMask.DepthBufferBit);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.TexCoordPointer(2, TexCoordPointerType.Float, Marshal.SizeOf(new _VertexFormat()), Marshal.SizeOf(new Vector3())*2);
            GL.VertexPointer(3, VertexPointerType.Float, Marshal.SizeOf(new _VertexFormat()), 0);
            GL.NormalPointer(NormalPointerType.Float, Marshal.SizeOf(new _VertexFormat()), Marshal.SizeOf(new Vector3()));

            SetupCamera();
        }

        private Vector3 Position, Rotation, Up;
        private void SetupCamera()
        {
            Matrix4 view = Matrix4.Identity;
            Matrix4 proj = Matrix4.Identity;
            Matrix4 rot_matrix = Matrix4.Identity;//Matrix4.RotationYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);

            //Create rotation matrix
            rot_matrix *= Matrix4.CreateRotationX(Rotation.X);
            rot_matrix *= Matrix4.CreateRotationY(Rotation.Y);
            rot_matrix *= Matrix4.CreateRotationZ(Rotation.Z);

            //Setup view and projection matrix
            Vector4 rot_vector = Vector4.Transform(new Vector4(0, 0, 1, 1), rot_matrix);
            view = Matrix4.LookAt(Position, new Vector3(Position.X + rot_vector.X, Position.Y + rot_vector.Y, Position.Z + rot_vector.Z), Up);
            proj = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)this.Width / this.Height, 1.0f, 10000.0f);

            //Apply the matrices
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);
            //GL.LoadIdentity();
            //GL.Translate(0, 0, -250);
            
        }

        private Vector2 _mousePrevPos = new Vector2(0, 0);
        private void GeoDataVis_Paint(object sender, PaintEventArgs e)
        {
            if (Ready && VBuffer != null)
            {
                GL.ClearColor(Color.DarkSlateBlue);
                GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
                SetupCamera();

                for (int i = 0; i <= Model.SubModels - 1; i++)
                {
                    GL.BindTexture(TextureTarget.Texture2D, Textures[i]);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferID[i]);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, _arrayID[i]);
                    GL.Material(MaterialFace.Front, MaterialParameter.Emission, Color4.Blue);

                    GL.DrawElements(PrimitiveType.Triangles, VBuffer.Length, DrawElementsType.UnsignedInt, 0);
                }

                GL.BindTexture(TextureTarget.Texture2D, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                View.SwapBuffers();

                //Output if any kind of error occured
                var er = GL.GetError();
                if (er != ErrorCode.NoError)
                    Console.WriteLine(er);

                float speed;
                KeyboardState state = Keyboard.GetState();
                if (View.Focused)
                {
                    if (state[Key.LShift])
                        speed = 0.05f;
                    else
                        speed = 0.005f;

                    if (state.IsKeyDown(Key.W))
                    {
                        Matrix4 rot_matrix = Matrix4.Identity;

                        //Create rotation matrix
                        rot_matrix *= Matrix4.CreateRotationX(Rotation.X);
                        rot_matrix *= Matrix4.CreateRotationY(Rotation.Y);
                        rot_matrix *= Matrix4.CreateRotationZ(Rotation.Z);

                        Vector4 rot_vector = Vector4.Transform(new Vector4(0, 0, speed, 1), rot_matrix);
                        Position.Z += rot_vector.Z;
                        Position.X += rot_vector.X;
                        Position.Y += rot_vector.Y;
                        //Console.WriteLine("W Key pressed");
                        //GL.Translate(0, 0, speed);
                    }
                    else if (state[Key.S])
                    {
                        Matrix4 rot_matrix = Matrix4.Identity;

                        //Create rotation matrix
                        rot_matrix *= Matrix4.CreateRotationX(Rotation.X);
                        rot_matrix *= Matrix4.CreateRotationY(Rotation.Y);
                        rot_matrix *= Matrix4.CreateRotationZ(Rotation.Z);

                        Vector4 rot_vector = Vector4.Transform(new Vector4(0, 0, speed, 1), rot_matrix);
                        Position.Z -= rot_vector.Z;
                        Position.X -= rot_vector.X;
                        Position.Y -= rot_vector.Y;
                        //GL.Translate(0, 0, -speed);
                    }
                    if (state[Key.D])
                    {
                        Matrix4 rot_matrix = Matrix4.Identity;

                        //Create rotation matrix
                        rot_matrix *= Matrix4.CreateRotationX(Rotation.X);
                        rot_matrix *= Matrix4.CreateRotationY(Rotation.Y);
                        rot_matrix *= Matrix4.CreateRotationZ(Rotation.Z);

                        Vector4 rot_vector = Vector4.Transform(new Vector4(speed, 0, 0, 1), rot_matrix);
                        Position.Z -= rot_vector.Z;
                        Position.X -= rot_vector.X;
                        //GL.Translate(speed, 0, 0);
                    }
                    else if (state[Key.A])
                    {
                        Matrix4 rot_matrix = Matrix4.Identity;

                        //Create rotation matrix
                        rot_matrix *= Matrix4.CreateRotationX(Rotation.X);
                        rot_matrix *= Matrix4.CreateRotationY(Rotation.Y);
                        rot_matrix *= Matrix4.CreateRotationZ(Rotation.Z);

                        Vector4 rot_vector = Vector4.Transform(new Vector4(speed, 0, 0, 1), rot_matrix);
                        Position.Z += rot_vector.Z;
                        Position.X += rot_vector.X;
                        //GL.Translate(-speed, 0, 0);
                    }
                    if (state[Key.E])
                        Position.Y += speed;
                    else if (state[Key.Q])
                        Position.Y -= speed;

                    MouseState MS = Mouse.GetState();
                    if (MS.LeftButton == OpenTK.Input.ButtonState.Pressed)
                    {
                        Rotation.Y += (float)((_mousePrevPos.X - MS.X) / (double)800 * Up.Y);
                        Rotation.X += (float)((_mousePrevPos.Y - MS.Y) / (double)800 * Up.Y);
                        //GL.Rotate(Rotation.Y, Position.X, Position.Y, Position.Z);
                        //GL.Rotate(Rotation.X, Position.X, Position.Y, Position.Z);
                    }
                    _mousePrevPos.X = MS.X;
                    _mousePrevPos.Y = MS.Y;
                }

                Application.DoEvents();
                View.Invalidate();
            }
        }

        private void UpdateModel()
        {
            Init();
        }

        private void ModelViewer_Load(object sender, EventArgs e)
        {
            UpdateModel();
        }

        internal int LoadTexture(Bitmap bitmap, int quality = 1, bool repeat = false, bool flip_y = false)
        {
            if (flip_y)
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            int texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texture);

            switch(quality)
            {
                case 0:
                default:
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
                    break;

                case 1:
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
                    break;
            }

            if (repeat)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            }

            //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

            bitmap.UnlockBits(bitmapData);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }
    }
}
