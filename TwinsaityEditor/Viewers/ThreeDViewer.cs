using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TwinsaityEditor
{
    public abstract class ThreeDViewer : GLControl
    {
        //add to preferences later
        protected static readonly Color[] colors = new[]
        {
            Color.Gray,
            Color.SlateGray,
            Color.DodgerBlue,
            Color.OrangeRed,
            Color.Red,
            Color.Pink,
            Color.LimeGreen,
            Color.DarkSlateBlue,
            Color.SaddleBrown,
            Color.LightSteelBlue,
            Color.SandyBrown,
            Color.Peru,
            Color.RoyalBlue,
            Color.DimGray,
            Color.Coral,
            Color.AliceBlue,
            Color.LightGray,
            Color.Cyan,
            Color.MediumTurquoise,
            Color.DarkSlateGray,
            Color.DarkSalmon,
            Color.DarkRed,
            Color.DarkCyan,
            Color.MediumVioletRed,
            Color.MediumOrchid,
            Color.DarkGray,
            Color.Yellow,
            Color.Goldenrod
        };
        protected static readonly float indicator_size = 0.5f;
        protected static Matrix3 identity_mat = Matrix3.Identity;

        protected VertexBufferData[] vtx;

        protected Dictionary<char, Vertex[]> charVtx;
        private Dictionary<char, int> charVtxOffs;
        private int charVtxBuf, charVtxBufLen;

        protected Vector3 pos, rot;
        private Matrix3 cam_rot_mat;
        private float sca, range;
        private Timer refresh;
        private bool k_w, k_a, k_s, k_d, k_e, k_q, m_l, k_shift, k_ctrl;
        private int m_x, m_y;
        private EventHandler _inputHandle;
        private static FontWrapper.FontService _fntService = new FontWrapper.FontService();
        private Dictionary<char, int> textureCharMap;
        private Dictionary<char, float> charAdvanceX;
        private Dictionary<char, float> charBearingX;
        private Dictionary<char, float> charBearingY;
        private Dictionary<char, float> charHeight;
        protected float size, zNear, zFar;

        protected long timeRenderObj = 0, timeRenderObj_min = long.MaxValue, timeRenderObj_max = 0;
        protected long timeRenderHud = 0, timeRenderHud_min = long.MaxValue, timeRenderHud_max = 0;

        public ThreeDViewer()
        {
            size = 24F;
            List<FileInfo> fonts = (List<FileInfo>)_fntService.GetFontFiles(new DirectoryInfo("Fonts/"), false);
            _fntService.SetFont(fonts[0].FullName);
            _fntService.Size = size;
            charVtx = new Dictionary<char, Vertex[]>();
            charVtxOffs = new Dictionary<char, int>();
            charVtxBufLen = 0;
            textureCharMap = new Dictionary<char, int>();
            charAdvanceX = new Dictionary<char, float>();
            charBearingX = new Dictionary<char, float>();
            charBearingY = new Dictionary<char, float>();
            charHeight = new Dictionary<char, float>();

            pos = new Vector3(0, 0, 0);
            rot = new Vector3(0, 0, 0);
            sca = 1F;
            range = 100;
            zNear = range / 100;
            zFar = 250F;

            _inputHandle = (sender, e) =>
            {
                if (e is MouseEventArgs)
                {
                    Invalidate();
                }
                else
                {
                    float speed = range / 250;
                    if (k_shift)
                        speed *= 5f;
                    else if (k_ctrl)
                        speed *= 0.2f;
                    int v = 0, h = 0, d = 0;
                    if (k_w)
                        d++;
                    if (k_a)
                        h++;
                    if (k_s)
                        d--;
                    if (k_d)
                        h--;
                    if (k_e)
                        v++;
                    if (k_q)
                        v--;
                    Vector3 delta = new Vector3(h, v, d) * speed;
                    cam_rot_mat = Matrix3.CreateFromAxisAngle(new Vector3(0, 1, 0), rot.X / 180 * MathHelper.Pi);
                    cam_rot_mat *= Matrix3.CreateFromAxisAngle(new Vector3(1, 0, 0), rot.Y / 180 * MathHelper.Pi);
                    cam_rot_mat *= Matrix3.CreateFromAxisAngle(new Vector3(0, 0, 1), rot.Z / 180 * MathHelper.Pi);

                    Vector3 fin_delta = cam_rot_mat * delta;

                    pos -= fin_delta;

                    if ((h | v | d) != 0)
                        Invalidate();
                }
            };

            refresh = new Timer
            {
                Interval = (int)Math.Floor(1.0/60*1000), //Set to ~60fps by default, TODO: Add to Preferences later
                Enabled = true
            };

            refresh.Tick += delegate (object sender, EventArgs e)
            {
                _inputHandle(sender, e);
                Invalidate();
            };

            ParentChanged += ThreeDViewer_ParentChanged;
        }

        private void ThreeDViewer_ParentChanged(object sender, EventArgs e)
        {
            Form par = (Form)Parent;
            par.Icon = Properties.Resources.icon;
            ParentChanged -= ThreeDViewer_ParentChanged;
        }

        protected abstract void RenderObjects();
        //protected abstract void RenderHUD();
        protected virtual void RenderHUD()
        {
            RenderString2D($"RenderObjects: {timeRenderObj}ms (max: {timeRenderObj_max}ms, min: {timeRenderObj_min}ms)\nRenderHUD: {timeRenderHud}ms (max: {timeRenderHud_max}ms, min: {timeRenderHud_min}ms)", 0, 0, 10, Color.White);
        }

        private void ResetCamera()
        {
            pos = new Vector3(0, 0, 0);
            rot = new Vector3(0, 0, 0);

            timeRenderObj = 0; timeRenderObj_min = long.MaxValue; timeRenderObj_max = 0;
            timeRenderHud = 0; timeRenderHud_min = long.MaxValue; timeRenderHud_max = 0;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    m_l = true;
                    break;
                //case MouseButtons.Right:
                //    m_r = true;
                //    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    m_l = false;
                    break;
                //case MouseButtons.Right:
                //    m_r = false;
                //    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_l)
            {
                rot.X += (e.X - m_x);
                rot.Y += (e.Y - m_y);
                rot.X += rot.X > 180 ? -360 : rot.X < -180 ? 360 : 0;
                if (rot.Y > 90)
                    rot.Y = 90;
                if (rot.Y < -90)
                    rot.Y = -90;
            }
            m_x = e.X;
            m_y = e.Y;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            range -= e.Delta / 120 * 30;
            if (range > 750f)
                range = 750f;
            else if (range < 20f)
                range = 20f;
            zNear = range / 100F;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.W:
                case Keys.A:
                case Keys.S:
                case Keys.D:
                case Keys.Q:
                case Keys.E:
                case Keys.R:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.W:
                    k_w = true;
                    break;
                case Keys.A:
                    k_a = true;
                    break;
                case Keys.S:
                    k_s = true;
                    break;
                case Keys.D:
                    k_d = true;
                    break;
                case Keys.Q:
                    k_q = true;
                    break;
                case Keys.E:
                    k_e = true;
                    break;
                case Keys.ShiftKey:
                    k_shift = true;
                    break;
                case Keys.ControlKey:
                    k_ctrl = true;
                    break;
                case Keys.R:
                    ResetCamera();
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyCode)
            {
                case Keys.W:
                    k_w = false;
                    break;
                case Keys.A:
                    k_a = false;
                    break;
                case Keys.S:
                    k_s = false;
                    break;
                case Keys.D:
                    k_d = false;
                    break;
                case Keys.Q:
                    k_q = false;
                    break;
                case Keys.E:
                    k_e = false;
                    break;
                case Keys.ShiftKey:
                    k_shift = false;
                    break;
                case Keys.ControlKey:
                    k_ctrl = false;
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MakeCurrent();
            GL.Viewport(Location, Size);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            var proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver3, (float)Width/Height, zNear, zFar*1.5F);
            GL.LoadMatrix(ref proj);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Scale(sca, sca, sca);
            GL.Rotate(rot.Y,1,0,0);
            GL.Rotate(rot.X,0,1,0);
            GL.Rotate(rot.Z,0,0,1);
            Vector3 delta = new Vector3(0, 0, -1) * range / 25f;
            Matrix4 rot_matrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.DegreesToRadians(rot.X));
            rot_matrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.DegreesToRadians(rot.Y));
            rot_matrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), MathHelper.DegreesToRadians(rot.Z));

            Vector3 fin_delta = new Vector3(rot_matrix * new Vector4(delta, 1f));
            GL.Translate(-pos + fin_delta);
            GL.PushMatrix();
            GL.Translate(pos.X*2, 0, 0);
            GL.Scale(-1, 1, 1);
            DrawAxes(pos.X, pos.Y, pos.Z, 1);
            GL.PopMatrix();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            RenderObjects();
            RenderChars();
            watch.Stop();
            timeRenderObj = watch.ElapsedMilliseconds;
            timeRenderObj_max = Math.Max(timeRenderObj_max, timeRenderObj);
            timeRenderObj_min = Math.Min(timeRenderObj_min, timeRenderObj);
            watch = System.Diagnostics.Stopwatch.StartNew();
            DrawHUD();
            watch.Stop();
            timeRenderHud = watch.ElapsedMilliseconds;
            timeRenderHud_max = Math.Max(timeRenderHud_max, timeRenderHud);
            timeRenderHud_min = Math.Min(timeRenderHud_min, timeRenderHud);
            SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.AlphaFunc(AlphaFunction.Greater, 0);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Fastest);
            GL.ClearColor(Color.MidnightBlue); //TODO: Add clear color to Preferences later
            // Lighting settings. Lighting must be enabled for them to take effect, logically
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0, 0, 0, 1 });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.25f, 0.25f, 0.25f, 1 }); // set some minimum light parameters so less shading doesn't make things too dark
            GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, 1.5f); // reduce direct light intensity
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);
            GL.GenBuffers(1, out charVtxBuf);
            base.OnLoad(e);
        }

        protected void InitVBO(int count)
        {
            MakeCurrent();
            vtx = new VertexBufferData[count];
            for (int i = 0; i < count; ++i)
            {
                if (i > 4)
                {
                    vtx[i] = null;
                }
                else
                {
                    vtx[i] = new VertexBufferData();
                }
            }
        }

        protected void UpdateVBO(int id)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vtx[id].ID);
            if (vtx[id].Vtx.Length > vtx[id].LastSize)
                GL.BufferData(BufferTarget.ArrayBuffer, Vertex.SizeOf * vtx[id].Vtx.Length, vtx[id].Vtx, BufferUsageHint.StaticDraw);
            else
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, Vertex.SizeOf * vtx[id].Vtx.Length, vtx[id].Vtx);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            vtx[id].LastSize = vtx[id].Vtx.Length;
        }

        protected void DrawAxes(float x, float y, float z, float size)
        {
            float new_ind_size = indicator_size * size;
            GL.PushMatrix();
            GL.Translate(x, y, z);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(+new_ind_size, 0, 0);
            GL.Vertex3(-new_ind_size / 2, 0, 0);
            GL.Color3(0f, 1f, 0f);
            GL.Vertex3(0, +new_ind_size, 0);
            GL.Vertex3(0, -new_ind_size / 2, 0);
            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(0, 0, +new_ind_size);
            GL.Vertex3(0, 0, -new_ind_size / 2);
            GL.End();
            GL.PopMatrix();
        }

        private void RenderChars()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, charVtxBuf);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            foreach (var k in charVtx.Keys)
            {
                if (charVtxOffs[k] == 0) continue;
                if (charVtxBufLen < charVtx[k].Length)
                {
                    GL.BufferData(BufferTarget.ArrayBuffer, Vertex.SizeOf * charVtx[k].Length, charVtx[k], BufferUsageHint.DynamicDraw);
                    charVtxBufLen = charVtx[k].Length;
                }
                else
                {
                    GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, Vertex.SizeOf * charVtxOffs[k], charVtx[k]);
                }
                GL.BindTexture(TextureTarget.Texture2D, textureCharMap[k]);
                GL.VertexPointer(3, VertexPointerType.Float, Vertex.SizeOf, Vertex.OffsetOfPos);
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, Vertex.SizeOf, Vertex.OffsetOfCol);
                GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeOf, Vertex.OffsetOfTex);
                GL.DrawArrays(PrimitiveType.Quads, 0, charVtxOffs[k]);
                charVtxOffs[k] = 0;
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void DrawHUD()
        {
            RenderHUD();
            GL.DepthMask(false);
            GL.Disable(EnableCap.DepthTest);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 10);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            RenderChars();
            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
        }

        protected int LoadTextTexture(ref Bitmap text, int quality = 0, bool flip_y = false)
        {
            if (flip_y)
                text.RotateFlip(RotateFlipType.RotateNoneFlipY);

            GL.GenTextures(1, out int texture);

            GL.BindTexture(TextureTarget.Texture2D, texture);

            switch (quality)
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

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToBorder);

            BitmapData data = text.LockBits(new Rectangle(0, 0, text.Width, text.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            text.UnlockBits(data);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            return texture;
        }

        protected void RenderString3D(string s, Color col, float x_off, float y_off, float z_off, ref Matrix3 rot_mat, float size_fac = 1F)
        {
            size_fac /= size;
            float x = 0, y = 0;
            foreach (char c in s)
            {
                if (c == '\n')
                {
                    y -= size;
                    continue;
                }
                if (!charAdvanceX.ContainsKey(c))
                    AddCharData(c);
                x += charAdvanceX[c];
            }
            x /= -2;
            Vector3 off = new Vector3(x_off, y_off, z_off);
            foreach (char c in s)
            {
                if (c == '\n')
                {
                    x = 0;
                    y += size;
                    continue;
                }
                if (c != ' ')
                {
                    if (!textureCharMap.ContainsKey(c))
                        GenCharTex(c);
                    GL.BindTexture(TextureTarget.Texture2D, textureCharMap[c]);
                    GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out float w);
                    GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out float h);
                    float y_lo = y - (charHeight[c] - charBearingY[c]);
                    float y_hi = y_lo + h;
                    if (!charVtx.ContainsKey(c))
                    {
                        charVtx.Add(c, new Vertex[256]);
                        charVtxOffs.Add(c, 0);
                    }
                    else if (charVtxOffs[c] + 4 >= charVtx[c].Length)
                    {
                        var arr = charVtx[c];
                        Array.Resize(ref arr, arr.Length * 2);
                        charVtx[c] = arr;
                    }
                    float gBearingX = charBearingX[c];
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX, y_lo, 0) * size_fac * rot_mat + off, new Vector2(0, 1), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX + w, y_lo, 0) * size_fac * rot_mat + off, new Vector2(1, 1), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX + w, y_hi, 0) * size_fac * rot_mat + off, new Vector2(1, 0), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX, y_hi, 0) * size_fac * rot_mat + off, new Vector2(0, 0), col);
                }

                x += charAdvanceX[c];
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        protected void RenderString2D(string s, float x, float y, float text_size, Color col, TextAnchor anchor = TextAnchor.TopLeft)
        {
            float text_size_fac = text_size / size;
            float start_x = x;
            foreach (char c in s)
            {
                if ((anchor == TextAnchor.BotLeft || anchor == TextAnchor.BotMiddle || anchor == TextAnchor.BotRight) && c == '\n')
                {
                    y -= text_size;
                    continue;
                }
                if (!charAdvanceX.ContainsKey(c))
                    AddCharData(c);
                if (anchor == TextAnchor.TopMiddle || anchor == TextAnchor.TopRight || anchor == TextAnchor.BotMiddle || anchor == TextAnchor.BotRight)
                    x += 0 + charAdvanceX[c] * text_size_fac;
            }
            if (anchor == TextAnchor.TopMiddle || anchor == TextAnchor.TopRight || anchor == TextAnchor.BotMiddle || anchor == TextAnchor.BotRight)
            {
                if (anchor == TextAnchor.BotMiddle || anchor == TextAnchor.TopMiddle)
                    x = start_x - (x - start_x) / 2;
                else
                    x = start_x - (x - start_x);
            }
            start_x = x;
            foreach (char c in s)
            {
                if (c == '\n')
                {
                    x = start_x;
                    y += text_size;
                    continue;
                }
                if (c != ' ')
                {
                    if (!textureCharMap.ContainsKey(c))
                        GenCharTex(c);
                    GL.BindTexture(TextureTarget.Texture2D, textureCharMap[c]);
                    GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out float w);
                    GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out float h);
                    w *= text_size_fac;
                    h *= text_size_fac;
                    float y_hi = 0, y_lo = 0;
                    switch (anchor)
                    {
                        case TextAnchor.TopLeft:
                        case TextAnchor.TopMiddle:
                        case TextAnchor.TopRight:
                            y_hi = y + (size - charBearingY[c]) * text_size_fac;
                            y_lo = y_hi + h;
                            break;
                        case TextAnchor.BotLeft:
                        case TextAnchor.BotMiddle:
                        case TextAnchor.BotRight:
                            y_lo = y + (charHeight[c] - charBearingY[c]) * text_size_fac;
                            y_hi = y_lo - h;
                            break;
                    }
                    if (!charVtx.ContainsKey(c))
                    {
                        charVtx.Add(c, new Vertex[256]);
                        charVtxOffs.Add(c, 0);
                    }
                    else if (charVtxOffs[c] + 4 >= charVtx[c].Length)
                    {
                        var arr = charVtx[c];
                        Array.Resize(ref arr, arr.Length * 2);
                        charVtx[c] = arr;
                    }
                    float gBearingX = charBearingX[c];
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX, y_lo, 0), new Vector2(0, 1), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX + w, y_lo, 0), new Vector2(1, 1), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX + w, y_hi, 0), new Vector2(1, 0), col);
                    charVtx[c][charVtxOffs[c]++] = new Vertex(new Vector3(x + gBearingX, y_hi, 0), new Vector2(0, 0), col);
                }
                x += charAdvanceX[c] * text_size_fac;
            }
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void GenCharTex(char c)
        {
            Bitmap bmp = _fntService.RenderString(c.ToString(), Color.White, Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
            textureCharMap.Add(c, LoadTextTexture(ref bmp));
            bmp.Dispose();
        }

        private void AddCharData(char c)
        {
            var face = _fntService.FontFace;
            face.LoadGlyph(face.GetCharIndex(c), SharpFont.LoadFlags.Default, SharpFont.LoadTarget.Normal);

            charAdvanceX.Add(c, (float)face.Glyph.Advance.X);
            charBearingX.Add(c, (float)face.Glyph.Metrics.HorizontalBearingX);
            charBearingY.Add(c, (float)face.Glyph.Metrics.HorizontalBearingY);
            charHeight.Add(c, (float)face.Glyph.Metrics.Height);
        }

        protected void SetPosition(Vector3 pos)
        {
            this.pos = pos;
        }

        protected override void Dispose(bool disposing)
        {
            refresh.Dispose();
            if (vtx != null)
            {
                for (int i = 0; i < vtx.Length; ++i)
                {
                    if (vtx[i] != null)
                    {
                        GL.DeleteBuffer(vtx[i].ID);
                    }
                }
            }
            GL.DeleteBuffer(charVtxBuf);
            foreach (var t in textureCharMap.Values)
            {
                GL.DeleteTexture(t);
            }
            base.Dispose(disposing);
        }
    }
}
