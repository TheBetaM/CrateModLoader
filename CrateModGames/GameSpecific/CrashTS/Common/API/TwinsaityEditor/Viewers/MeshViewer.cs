using OpenTK.Graphics.OpenGL;
using System;
using System.Windows.Forms;

namespace TwinsaityEditor
{
    public class MeshViewer : ThreeDViewer
    {
        private ModelController mesh;
        private FileController file;

        private bool lighting, wire;

        public MeshViewer(ModelController mesh, Form pform)
        {
            //initialize variables here
            this.mesh = mesh;
            zFar = 50F;
            file = mesh.MainFile;
            lighting = true;
            wire = false;
            Tag = pform;
            InitVBO(1);
            pform.Text = "Loading mesh...";
            LoadMesh();
            pform.Text = "Initializing...";
        }

        protected override void RenderHUD()
        {
            base.RenderHUD();
            RenderString2D("Press L to toggle lighting\nPress X to toggle wireframe", 0, Height, 12, System.Drawing.Color.White, TextAnchor.BotLeft);
        }

        protected override void RenderObjects()
        {
            //put all object rendering code here
            var flags = lighting ? BufferPointerFlags.Normal : BufferPointerFlags.Default;
            if (lighting)
                GL.Enable(EnableCap.Lighting);
            vtx[0].DrawAllElements(PrimitiveType.Triangles, flags);
            if (lighting)
                GL.Disable(EnableCap.Lighting);
            if (wire)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                GL.Color3(System.Drawing.Color.Black);
                vtx[0].DrawAllElements(PrimitiveType.Triangles, BufferPointerFlags.None);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.L:
                case Keys.X:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.L:
                    lighting = !lighting;
                    break;
                case Keys.X:
                    wire = !wire;
                    break;
            }
        }

        public void LoadMesh()
        {
            mesh.LoadMeshData();
            float min_x = float.MaxValue, min_y = float.MaxValue, min_z = float.MaxValue, max_x = float.MinValue, max_y = float.MinValue, max_z = float.MinValue;
            foreach (var v in mesh.Vertices)
            {
                min_x = Math.Min(min_x, v.Pos.X);
                min_y = Math.Min(min_y, v.Pos.Y);
                min_z = Math.Min(min_z, v.Pos.Z);
                max_x = Math.Max(max_x, v.Pos.X);
                max_y = Math.Max(max_y, v.Pos.Y);
                max_z = Math.Max(max_z, v.Pos.Z);
            }
            vtx[0].Vtx = mesh.Vertices;
            vtx[0].VtxInd = mesh.Indices;
            zFar = Math.Max(zFar, Math.Max(max_x - min_x, Math.Max(max_y - min_y, max_z - min_z)));
            UpdateVBO(0);
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
