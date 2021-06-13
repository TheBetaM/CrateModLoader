using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using CrateModLoader.LevelAPI;

namespace CrateModLoader.Forms.LevelEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Viewport3D : UserControl
    {
        public ModLoader ModProgram;
        private HelixViewport3D Viewport;
        private DefaultLights Lights;

        private List<Color> ColorList = new List<Color>()
        {
            Color.FromRgb(200, 200, 200),
            Color.FromRgb(255, 200, 200),
            Color.FromRgb(200, 255, 200),
            Color.FromRgb(200, 200, 255),
            Color.FromRgb(200, 255, 255),
            Color.FromRgb(255, 255, 200),
            Color.FromRgb(255, 200, 255),
        };

        public Viewport3D()
        {
            InitializeComponent();

            Viewport = new HelixViewport3D();
            PerspectiveCamera Cam = new PerspectiveCamera();
            Viewport.Camera = Cam;
            Viewport.CameraMode = CameraMode.WalkAround;
            Viewport.CameraRotationMode = CameraRotationMode.Turnball;
            this.AddChild(Viewport);
        }

        public void Create3DViewPort(LevelBase lev = null)
        {
            Viewport.Children.Clear();
            Lights = new DefaultLights();
            Viewport.Children.Add(Lights);

            if (lev != null)
            {
                if (lev.CollisionData != null && lev.CollisionData.Count > 0)
                {
                    int ColorID = 0;
                    foreach (CollisionDataBase Col in lev.CollisionData)
                    {
                        CreateCollision(ColorID, Col);
                        ColorID++;
                        if (ColorID >= ColorList.Count) ColorID = 0;
                    }
                }
            }

            foreach (LevelObjectDataBase data in lev.ObjectData)
            {
                CreateObjectVisual(data);
            }
        }

        void CreateCollision(int ColorID, CollisionDataBase col = null)
        {

            MeshBuilder Builder = new MeshBuilder();
            if (col.Normals != null)
            {
                Builder.CreateNormals = false;
            }
            if (col.TexCoords != null)
            {
                Builder.CreateTextureCoordinates = false;
            }

            List<Point3D> Points = new List<Point3D>();
            foreach (Vector4 vec in col.Vertices)
            {
                Points.Add(new Point3D(vec.X, vec.Y, vec.Z));
            }
            
            List<Vector3D> Normals = new List<Vector3D>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (col.Normals != null)
                {
                    Normals.Add(new Vector3D(col.Normals[i].X, col.Normals[i].Y, col.Normals[i].Z));
                }
                else
                {
                    Normals.Add(new Vector3D(0, 0, 0));
                }
            }
            List<Point> TexPoints = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (col.TexCoords != null)
                {
                    TexPoints.Add(new Point(col.TexCoords[i].X, col.TexCoords[i].Y));
                }
                else
                {
                    TexPoints.Add(new Point(0.5, 0.5));
                }
            }

            Builder.Append(Points, col.Indices, Normals, TexPoints);

            MeshGeometry3D Mesh = Builder.ToMesh();
            Material Mat = MaterialHelper.CreateMaterial(ColorList[ColorID]);
            MeshGeometryVisual3D Vis = new MeshGeometryVisual3D();
            Vis.MeshGeometry = Mesh;
            Vis.Material = Mat;

            Viewport.Children.Add(Vis);
        }

        void CreateObjectVisual(LevelObjectDataBase data)
        {
            int ColorID = data.ObjectCategory;
            if (ColorID >= ColorList.Count) ColorID = 0;

            MeshBuilder Builder = new MeshBuilder();
            Builder.AddBox(new Rect3D(data.Position.X, data.Position.Y, data.Position.Z, data.Scale.X, data.Scale.Y, data.Scale.Z));

            MeshGeometry3D Mesh = Builder.ToMesh();
            Material Mat = MaterialHelper.CreateMaterial(ColorList[ColorID]);
            MeshGeometryVisual3D Vis = new MeshGeometryVisual3D();
            Vis.MeshGeometry = Mesh;
            Vis.Material = Mat;

            Viewport.Children.Add(Vis);
        }

        public void MoveViewportToObject(LevelObjectDataBase data)
        {
            Viewport.Camera.Position = new Point3D(data.Position.X, data.Position.Y, data.Position.Z);
        }
    }
}
