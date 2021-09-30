using System.Drawing;
using System.Windows.Forms;
using System;
using Twinsanity;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using Twinsanity.Items;
using System.Linq;

namespace TwinsaityEditor
{
    public partial class PTCViewer
    {

        public PTCViewer()
        {
            InitializeComponent();
        }

        private int texInd;

        public TwinsPTC SelectedPTC;

        public int TextureIndex
        {
            set
            {
                lblTextureIndex.Text = (value + 1).ToString() + "/" + PTCs.Count;
                texInd = value;
            }
            get
            {
                return texInd;
            }
        }
        public List<TwinsPTC> PTCs = new List<TwinsPTC>();

        public bool Mat = false;
        public uint CurTex = 0;
        public void Init()
        {
            int[] viewPort = new int[5];
            GL.GetInteger(GetPName.Viewport, viewPort);
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(viewPort[0], viewPort[0] + viewPort[2], viewPort[1] + viewPort[3], viewPort[1], -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Translate(0.0, 0.0, 0.0);
            GL.PushAttrib(AttribMask.DepthBufferBit);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }
        public bool Draw = true;
        private void TextureViewer_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.Points);
            if (cbViewFullPSM.Enabled && cbViewFullPSM.Checked)
            {
                var widthOffset = 0;
                var heighOffset = 0;
                var maxWidth = PTCs.Max(p => p.Texture.Width);
                var maxHeight = PTCs.Max(p => p.Texture.Height);
                for (int i = 0; i < PTCs.Count; ++i)
                {
                    for (int j = 0; j < PTCs[i].Texture.RawData.Length; ++j)
                    {
                        GL.Color4(PTCs[i].Texture.RawData[j]);
                        GL.Vertex2(widthOffset + (j % (PTCs[i].Texture.Width)), heighOffset + (j / (PTCs[i].Texture.Width)));
                    }
                    widthOffset += maxWidth;
                    if ((i + 1) % 4 == 0)
                    {
                        heighOffset += maxHeight;
                        widthOffset = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < SelectedPTC.Texture.RawData.Length; i++)
                {
                    GL.Color4(SelectedPTC.Texture.RawData[i]);
                    GL.Vertex2(i % (SelectedPTC.Texture.Width), i / (SelectedPTC.Texture.Width));
                }
            }
            GL.End();
            GlControl1.SwapBuffers();
            Application.DoEvents();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePNG.FileName = SelectedPTC.TexID.ToString();
            if (SavePNG.ShowDialog() == DialogResult.OK)
            {
                if (cbViewFullPSM.Enabled && cbViewFullPSM.Checked)
                {
                    Bitmap BMP = new Bitmap(PTCs.Max(p => p.Texture.Width) * 4, PTCs.Max(p => p.Texture.Height) * (PTCs.Count / 4));
                    var widthOffset = 0;
                    var heighOffset = 0;
                    var maxWidth = PTCs.Max(p => p.Texture.Width);
                    var maxHeight = PTCs.Max(p => p.Texture.Height);
                    for (int i = 0; i < PTCs.Count; ++i)
                    {
                        for (int j = 0; j < PTCs[i].Texture.RawData.Length; ++j)
                        {
                            BMP.SetPixel(widthOffset + (j % (PTCs[i].Texture.Width)),
                                heighOffset + (j / (PTCs[i].Texture.Width)), PTCs[i].Texture.RawData[j]);
                        }
                        widthOffset += maxWidth;
                        if ((i + 1) % 4 == 0)
                        {
                            heighOffset += maxHeight;
                            widthOffset = 0;
                        }
                    }
                    BMP.Save(SavePNG.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    Bitmap BMP = new Bitmap(SelectedPTC.Texture.Width, SelectedPTC.Texture.Height);
                    for (int i = 0; i < SelectedPTC.Texture.RawData.Length; i++)
                        BMP.SetPixel((i % SelectedPTC.Texture.Width), (i / SelectedPTC.Texture.Width), SelectedPTC.Texture.RawData[i]);
                    BMP.Save(SavePNG.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void btnPrevTexture_Click(object sender, EventArgs e)
        {
            TextureIndex--;
            if (TextureIndex < 0)
            {
                TextureIndex = PTCs.Count - 1;
            }
            SelectedPTC = PTCs[TextureIndex];
            Refresh();
        }

        public void UpdateTextureLabel()
        {
            lblTextureIndex.Text = (TextureIndex + 1).ToString() + "/" + PTCs.Count;
        }

        private void btnNextTexture_Click(object sender, EventArgs e)
        {
            TextureIndex++;
            if (TextureIndex >= PTCs.Count)
            {
                TextureIndex = 0;
            }
            SelectedPTC = PTCs[TextureIndex];
            Refresh();
        }

        private void cbViewFullPSM_CheckedChanged(object sender, EventArgs e)
        {
            btnNextTexture.Enabled = !cbViewFullPSM.Checked;
            btnPrevTexture.Enabled = !cbViewFullPSM.Checked;
            lblTextureIndex.Enabled = !cbViewFullPSM.Checked;
            Refresh();
        }

        public void EnablePSMCheckbox()
        {
            cbViewFullPSM.Enabled = true;
        }
    }
}
