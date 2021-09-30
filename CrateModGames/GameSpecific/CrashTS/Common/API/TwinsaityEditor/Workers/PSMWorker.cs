using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System;
using Twinsanity;
using OpenTK.Graphics.OpenGL;

namespace TwinsaityEditor
{
    public partial class PSMWorker
    {
        private PSM[] PSM_Surface;
        private uint Index = 0;
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
        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Index = 0;
                Array.Resize(ref PSM_Surface, OpenFileDialog1.FileNames.Length);
                for (int i = 0; i <= OpenFileDialog1.FileNames.Length - 1; i++)
                {
                    PSM_Surface[i] = new PSM();
                    if (OpenFileDialog1.FilterIndex == 1)
                        PSM_Surface[i].LoadPSM(OpenFileDialog1.FileNames[i], CheckBox1.Checked);
                    else if (OpenFileDialog1.FilterIndex == 2)
                        PSM_Surface[i].LoadPTC(OpenFileDialog1.FileNames[i], CheckBox1.Checked);
                    else if (OpenFileDialog1.FilterIndex == 3)
                        PSM_Surface[i].LoadPSF(OpenFileDialog1.FileNames[i], CheckBox1.Checked);
                }
                Label1.Text = (Index + 1).ToString() + @"\" + PSM_Surface.Length.ToString();
                GlControl1.Invalidate();
            }
        }
        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(BeginMode.Points);
            if (!(PSM_Surface == null))
            {
                {
                    var withBlock = PSM_Surface[Index];
                    ComboBox1.Items.Clear();
                    for (int i = 0; i <= withBlock.PSM_Segments.Length - 1; i++)
                        ComboBox1.Items.Add(withBlock.PSM_Segments[i].Name);
                    ComboBox1.SelectedIndex = 0;
                    uint shift_x = 0;
                    uint shift_y = 0;
                    for (int i = 0; i <= withBlock.PSM_Segments.Length - 1; i++)
                    {
                        if (shift_x + withBlock.PSM_Segments[i].Texture.Width > 512)
                        {
                            shift_x = 0;
                            shift_y += withBlock.PSM_Segments[i].Texture.Height;
                        }
                        for (int j = 0; j <= withBlock.PSM_Segments[i].Texture.RawData.Length - 1; j++)
                        {
                            if (CheckBox2.Checked)
                                GL.Color4((byte)255, withBlock.PSM_Segments[i].Texture.Index[j], withBlock.PSM_Segments[i].Texture.Index[j], withBlock.PSM_Segments[i].Texture.Index[j]);
                            else
                                GL.Color4(withBlock.PSM_Segments[i].Texture.RawData[j]);
                            GL.Vertex2(shift_x + j % withBlock.PSM_Segments[i].Texture.Width, shift_y + j / withBlock.PSM_Segments[i].Texture.Width);
                        }
                        shift_x += withBlock.PSM_Segments[i].Texture.Width;
                    }
                }
            }
            GL.End();
            GlControl1.SwapBuffers();
            Application.DoEvents();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {
            Init();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Index > 0)
                Index -= 1;
            else
                Index = (uint)PSM_Surface.Length - 1;
            Label1.Text = (Index + 1).ToString() + @"\" + PSM_Surface.Length.ToString();
            GlControl1.Invalidate();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Index < PSM_Surface.Length - 1)
                Index += 1;
            else
                Index = 0;
            Label1.Text = (Index + 1).ToString() + @"\" + PSM_Surface.Length.ToString();
            GlControl1.Invalidate();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!(PSM_Surface == null))
            {
                SaveFileDialog1.FileName = PSM_Surface[Index].PSM_Segments[0].Name;
                if (SaveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Bitmap BMP = new Bitmap(512, 512);
                    {
                        var withBlock = PSM_Surface[Index];
                        uint shift_x = 0;
                        uint shift_y = 0;
                        for (int i = 0; i <= withBlock.PSM_Segments.Length - 1; i++)
                        {
                            if (shift_x + withBlock.PSM_Segments[i].Texture.Width > 512)
                            {
                                shift_x = 0;
                                shift_y += withBlock.PSM_Segments[i].Texture.Height;
                            }
                            for (int j = 0; j <= withBlock.PSM_Segments[i].Texture.RawData.Length - 1; j++)
                                BMP.SetPixel((int)(shift_x + j % withBlock.PSM_Segments[i].Texture.Width), (int)(shift_y + j / withBlock.PSM_Segments[i].Texture.Width), withBlock.PSM_Segments[i].Texture.RawData[j]);
                            shift_x += withBlock.PSM_Segments[i].Texture.Width;
                        }
                    }
                    BMP.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }


        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            GlControl1.Invalidate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Bitmap BMP = new System.Drawing.Bitmap(OpenFileDialog2.FileName);
                if (PSM_Surface[Index].ReplacePSMSegment(BMP, ComboBox1.SelectedIndex) != 0)
                    Interaction.MsgBox("Inappropriate image.");
                string[] name = OpenFileDialog2.FileName.Split('.');
                PSM_Surface[Index].PSM_Segments[ComboBox1.SelectedIndex + 1].Name = name[name.Length - 2] + (ComboBox1.SelectedIndex + 1).ToString();
                GlControl1.Invalidate();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (!(PSM_Surface[Index].PSM_Segments.Length == 8))
                Interaction.MsgBox("Inappropriate PSM");
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Bitmap BMP = new System.Drawing.Bitmap(OpenFileDialog2.FileName);
                if (!(BMP.Width == 512) | !(BMP.Height == 512))
                    Interaction.MsgBox("Inappropriate image.");
                for (int r = 0; r <= 1; r++)
                {
                    for (int c = 0; c <= 3; c++)
                    {
                        System.Drawing.Bitmap SEG = new System.Drawing.Bitmap(128, 256);
                        for (int x = 0; x <= 127; x++)
                        {
                            for (int y = 0; y <= 255; y++)
                                SEG.SetPixel(x, y, BMP.GetPixel(x + c * 128, y + r * 256));
                        }
                        PSM_Surface[Index].ReplacePSMSegment(SEG, c + r * 4);
                        string path = OpenFileDialog2.FileName.Split('.')[0];
                        string[] name = path.Split('\\');
                        PSM_Surface[Index].PSM_Segments[c + r * 4].Name = name[name.Length - 1] + "_" + (r + c * 4 + 1).ToString();
                    }
                }
                GlControl1.Invalidate();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (OpenFileDialog1.FilterIndex == 1)
                    PSM_Surface[Index].SavePSM(SaveFileDialog2.FileName + ".psm");
                else if (OpenFileDialog1.FilterIndex == 2)
                    PSM_Surface[Index].SavePTC(SaveFileDialog2.FileName + ".ptc");
                else if (OpenFileDialog1.FilterIndex == 3)
                    PSM_Surface[Index].SavePSF(SaveFileDialog2.FileName + ".psf", CheckBox1.Checked);
            }
        }
    }
}
