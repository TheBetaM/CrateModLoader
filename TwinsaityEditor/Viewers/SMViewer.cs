using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public partial class SMViewer : ThreeDViewer
    {
        private FileController file;
        private ChunkLinks links;

        public SMViewer(FileController file, Form pform)
        {
            this.file = file;
            if (file.Data.ContainsItem(5))
            {
                links = file.Data.GetItem<ChunkLinks>(5);
            }
            zFar = 2000F;
        }

        //protected override void RenderHUD()
        //{
        //    return;
        //}

        protected override void RenderObjects()
        {
            //put all object rendering code here
            GL.PushMatrix();

            if (links != null)
            {
                GL.LineWidth(2);
                GL.DepthMask(false);
                foreach (var l in links.Links)
                {
                    Color cur_color = colors[(links.Links.IndexOf(l) + 2) % colors.Length];
                    GL.PushMatrix();
                    GL.Scale(-1, 1, 1);
                    if (l.HasWall)
                    {
                        GL.Color4(Color.FromArgb(95, cur_color));
                        GL.Begin(PrimitiveType.Quads);
                        GL.Vertex4(l.LoadWall[0].ToArray());
                        GL.Vertex4(l.LoadWall[1].ToArray());
                        GL.Vertex4(l.LoadWall[2].ToArray());
                        GL.Vertex4(l.LoadWall[3].ToArray());
                        GL.End();
                        GL.Color4(cur_color);
                        GL.Begin(PrimitiveType.LineLoop);
                        GL.Vertex4(l.LoadWall[0].ToArray());
                        GL.Vertex4(l.LoadWall[1].ToArray());
                        GL.Vertex4(l.LoadWall[2].ToArray());
                        GL.Vertex4(l.LoadWall[3].ToArray());
                        GL.End();
                        GL.Begin(PrimitiveType.Lines);
                        GL.Vertex4(l.LoadWall[0].ToArray());
                        GL.Vertex4(l.LoadWall[2].ToArray());
                        GL.Vertex4(l.LoadWall[1].ToArray());
                        GL.Vertex4(l.LoadWall[3].ToArray());
                        GL.End();
                        Matrix3 rot_mat = Matrix3.Identity;
                        rot_mat *= Matrix3.CreateRotationX(-rot.Y / 180 * MathHelper.Pi);
                        rot_mat *= Matrix3.CreateRotationY(-rot.X / 180 * MathHelper.Pi);
                        rot_mat *= Matrix3.CreateRotationZ(rot.Z / 180 * MathHelper.Pi);
                        RenderString3D(l.Path, cur_color,
                            -(l.LoadWall[0].X + l.LoadWall[1].X + l.LoadWall[2].X + l.LoadWall[3].X) / 4,
                            (l.LoadWall[0].Y + l.LoadWall[1].Y + l.LoadWall[2].Y + l.LoadWall[3].Y) / 4,
                            (l.LoadWall[0].Z + l.LoadWall[1].Z + l.LoadWall[2].Z + l.LoadWall[3].Z) / 4,
                            ref rot_mat);
                    }
                    if (l.HasTree)
                    {
                        ChunkLinks.ChunkLink.LinkTree tree = l.TreeRoot;
                        while (tree != null)
                        {
                            GL.Begin(PrimitiveType.Lines);
                            for (int i = 0; i < 6; ++i)
                            {
                                switch (i)
                                {
                                    case 0: GL.Color4(Color.Red); break;
                                    case 1: GL.Color4(Color.Green); break;
                                    case 2: GL.Color4(Color.Blue); break;
                                    case 3: GL.Color4(Color.Yellow); break;
                                    case 4: GL.Color4(Color.Magenta); break;
                                    case 5: GL.Color4(Color.Cyan); break;
                                }
                                int i1 = i >= 4 ? 1 - (i - 4) : (0 + 2 * i) % 8;
                                int i2 = i >= 4 ? i1 + 2 : (1 + 2 * i) % 8;
                                int i3 = i >= 4 ? i2 + 2 : (2 + 2 * i) % 8;
                                int i4 = i >= 4 ? i3 + 2 : (3 + 2 * i) % 8;
                                Vector3 mid_vec = new Vector3(tree.LoadArea[i1].X + tree.LoadArea[i2].X + tree.LoadArea[i3].X + tree.LoadArea[i4].X,
                                    tree.LoadArea[i1].Y + tree.LoadArea[i2].Y + tree.LoadArea[i3].Y + tree.LoadArea[i4].Y,
                                    tree.LoadArea[i1].Z + tree.LoadArea[i2].Z + tree.LoadArea[i3].Z + tree.LoadArea[i4].Z) / 4;
                                Vector3 nor_vec = new Vector3(tree.AreaMatrix[i].X, tree.AreaMatrix[i].Y, tree.AreaMatrix[i].Z);
                                Vector3 unk_vec = new Vector3(tree.UnknownMatrix[i].X, tree.UnknownMatrix[i].Y, tree.UnknownMatrix[i].Z);
                                GL.Vertex3(mid_vec);
                                GL.Vertex3(mid_vec + nor_vec);
                                GL.Vertex3(mid_vec);
                                GL.Vertex3(mid_vec + unk_vec);
                            }
                            GL.End();
                            GL.Enable(EnableCap.Lighting);
                            GL.Color4(Color.FromArgb(95, cur_color));
                            GL.Begin(PrimitiveType.QuadStrip);
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.End();
                            GL.Begin(PrimitiveType.Quads);
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.End();
                            GL.Disable(EnableCap.Lighting);
                            GL.Color4(cur_color);
                            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                            GL.Begin(PrimitiveType.QuadStrip);
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.End();
                            GL.Begin(PrimitiveType.Quads);
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.End();
                            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                            GL.Begin(PrimitiveType.Lines);
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[0].ToArray());
                            GL.Vertex4(tree.LoadArea[4].ToArray());
                            GL.Vertex4(tree.LoadArea[2].ToArray());
                            GL.Vertex4(tree.LoadArea[6].ToArray());
                            GL.Vertex4(tree.LoadArea[1].ToArray());
                            GL.Vertex4(tree.LoadArea[5].ToArray());
                            GL.Vertex4(tree.LoadArea[3].ToArray());
                            GL.Vertex4(tree.LoadArea[7].ToArray());
                            GL.End();

                            if (tree.Next != null)
                            {
                                tree = tree.Next;
                            }
                            else
                                break;
                        }
                    }
                    GL.PopMatrix();
                }
                GL.DepthMask(true);
                GL.LineWidth(1);
            }

            GL.PopMatrix();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
