using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public class ColDataController : ItemController
    {
        public static CollisionImporter importer;

        public new ColData Data { get; set; }

        public ColDataController(MainForm topform, ColData item) : base (topform, item)
        {
            Data = item;
            AddMenu("Open RMViewer", Menu_OpenRMViewer);
            AddMenu("Open collision tree viewer", Menu_OpenColDataViewer);
            AddMenu("Export Collision Model", Menu_Export);
            AddMenu("Import Collision Model", Menu_Import);
        }

        protected override string GetName()
        {
            return $"Collision Data [ID {Data.ID}]";
        }

        protected override void GenText()
        {
            TextPrev = new string[6];// + Data.Triggers.Count + Data.Groups.Count + Data.Tris.Count + Data.Vertices.Count];
            TextPrev[0] = $"ID: {Data.ID}";
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"TriggerCount: {Data.Triggers.Count}";
            TextPrev[3] = $"GroupCount: {Data.Groups.Count}";
            TextPrev[4] = $"PolyCount: {Data.Tris.Count}";
            TextPrev[5] = $"VertexCount: {Data.Vertices.Count}";
            /*
            for (int i = 0; i < Data.Triggers.Count; ++i)
                TextPrev[6 + i] = "Trigger{i}: {Nodes {Data.Triggers[i].Flag1}~{Data.Triggers[i].Flag2;
            
            for (int i = 0; i < Data.Groups.Count; ++i)
                TextPrev[6 + Data.Triggers.Count + i] = "Group{i}: {"Offset: {Data.Groups[i].Offset} Count: {Data.Groups[i].Size;

            for (int i = 0; i < Data.Tris.Count; ++i)
                TextPrev[6 + Data.Triggers.Count + Data.Groups.Count + i] = "Polygon{i}: {Data.Tris[i].Vert1}|{Data.Tris[i].Vert2}|{Data.Tris[i].Vert3}|{Data.Tris[i].Surface;

            for (int i = 0; i < Data.Vertices.Count; ++i)
                TextPrev[6 + Data.Triggers.Count + Data.Groups.Count + Data.Tris.Count + i] = "Vertex{i}: ({Data.Vertices[i].X}, {Data.Vertices[i].Y}, {Data.Vertices[i].Z}, {Data.Vertices[i].W})";
                */
        }

        private void Menu_OpenRMViewer()
        {
            MainFile.OpenRMViewer();
        }

        private void Menu_OpenColDataViewer()
        {
            MainFile.OpenEditor(this);
        }

        private void Menu_Export()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wavefront OBJ file (*.obj)|*.obj";
            sfd.FileName = MainFile.SafeFileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write));
                writer.WriteLine("# Generated using the TwinsanityEditor @ https://github.com/Smartkin/twinsanity-editor");
                writer.WriteLine();
                foreach (var i in Data.Vertices)
                {
                    writer.WriteLine("v {0} {1} {2}", i.X, i.Y, i.Z);
                }
                //int g = 0;
                //foreach (var i in Data.Groups)
                //{
                //    writer.WriteLine("o Group {0}", g++);
                //    for (int d = (int)i.Offset; d < i.Size + i.Offset; ++d)
                //    {
                //        writer.WriteLine("f {0} {1} {2}", Data.Tris[d].Vert1 + 1, Data.Tris[d].Vert2 + 1, Data.Tris[d].Vert3 + 1);
                //    }
                //}
                //writer.Close();
                //return;
                Dictionary<int, List<ColData.ColTri>> polys = new Dictionary<int, List<ColData.ColTri>>();
                foreach (var i in Data.Tris)
                {
                    if (!polys.ContainsKey(i.Surface))
                        polys.Add(i.Surface, new List<ColData.ColTri>());
                    polys[i.Surface].Add(i);
                }
                //???
                foreach (var d in polys)
                {
                    writer.WriteLine("o Surface {0}", d.Key);
                    foreach (var i in d.Value)
                    {
                        writer.WriteLine("f {0} {1} {2}", i.Vert1 + 1, i.Vert2 + 1, i.Vert3 + 1);
                    }
                }
                writer.Close();
            }
        }

        private void Menu_Import()
        {
            if (importer == null)
            {
                importer = new CollisionImporter(this);
                importer.FormClosed += delegate
                {
                    importer = null;
                };
                importer.Show();
            }
            else
                importer.Select();
        }
    }
}
