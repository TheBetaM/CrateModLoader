using OpenTK;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Twinsanity;

namespace TwinsaityEditor
{
    public class ModelController : ItemController
    {
        public new Model Data { get; set; }

        public Vertex[] Vertices { get; set; }
        public uint[] Indices { get; set; }
        public bool IsLoaded { get; private set; }

        public ModelController(MainForm topform, Model item) : base (topform, item)
        {
            Data = item;
            AddMenu("Export to PLY", Menu_ExportPLY);
            AddMenu("Open model viewer", Menu_OpenViewer);
            IsLoaded = false;
            //LoadMeshData(); TODO preferences
        }

        protected override string GetName()
        {
            return string.Format("Model [ID {0:X8}/{0}]", Data.ID);
        }

        protected override void GenText()
        {
            //TODO use array
            var ex_lines = new List<string>();
            for (int i = 0; i < Data.SubModels.Count; ++i)
            {
                var sub = Data.SubModels[i];
                ex_lines.Add($"SubModel{i}");
                ex_lines.Add($"VertexCount: {sub.VertexCount} BlockSize: {sub.BlockSize}");
                ex_lines.Add($"K: {sub.k} C: {sub.c}");
                ex_lines.Add($"GroupCount: {sub.Groups.Count}");
                foreach (var j in sub.Groups)
                    ex_lines.Add($"VertexCount: {j.VertexCount}");
            }
            TextPrev = new string[3 + ex_lines.Count];
            TextPrev[0] = string.Format("ID: {0:X8}", Data.ID);
            TextPrev[1] = $"Size: {Data.Size}";
            TextPrev[2] = $"SubModel Count: {Data.SubModels.Count}";
            Array.Copy(ex_lines.ToArray(), 0, TextPrev, 3, ex_lines.Count);
        }

        public void LoadMeshData()
        {
            if (IsLoaded) return;
            List<Vertex> vtx = new List<Vertex>();
            List<uint> idx = new List<uint>();
            int off = 0;
            foreach (var s in Data.SubModels)
            {
                foreach (var g in s.Groups)
                {
                    if (g.VertHead > 0 && g.VDataHead > 0 && g.VertexCount >= 3)
                    {
                        vtx.Add(new Vertex(new Vector3(-g.Vertex[0].X, g.Vertex[0].Y, g.Vertex[0].Z), Color.FromArgb(g.VData[0].R, g.VData[0].G, g.VData[0].B)));
                        vtx.Add(new Vertex(new Vector3(-g.Vertex[1].X, g.Vertex[1].Y, g.Vertex[1].Z), Color.FromArgb(g.VData[1].R, g.VData[1].G, g.VData[1].B)));
                        for (int i = 2; i < g.VertexCount; ++i)
                        {
                            vtx.Add(new Vertex(new Vector3(-g.Vertex[i].X, g.Vertex[i].Y, g.Vertex[i].Z), Color.FromArgb(g.VData[i].R, g.VData[i].G, g.VData[i].B)));
                            int v1 = off + i - 2 + (i & 1);
                            int v2 = off + i - 1 - (i & 1);
                            int v3 = off + i;
                            if (g.VData[i].CONN == 128) continue;
                            Vector3 normal = VectorFuncs.CalcNormal(vtx[v1].Pos, vtx[v2].Pos, vtx[v3].Pos);
                            var v = vtx[v1];
                            v.Nor += normal;
                            vtx[v1] = v;
                            v = vtx[v2];
                            v.Nor += normal;
                            vtx[v2] = v;
                            v = vtx[v3];
                            v.Nor += normal;
                            vtx[v3] = v;
                            idx.Add((uint)v1);
                            idx.Add((uint)v2);
                            idx.Add((uint)v3);
                        }
                        off += g.VertexCount;
                    }
                }
            }
            //sort out duplicates
            for (int i = 0; i < vtx.Count; ++i)
            {
                Vector3 pos = vtx[i].Pos;
                uint col = vtx[i].Col;
                for (int j = i + 1; j < vtx.Count; ++j)
                {
                    if (vtx[j].Col == col && vtx[j].Pos == pos)
                    {
                        for (int k = 0; k < idx.Count; ++k)
                        {
                            if (idx[k] == j)
                                idx[k] = (uint)i;
                            else if (idx[k] > j)
                                idx[k]--;
                        }
                        var v = vtx[i];
                        v.Nor += vtx[j].Nor;
                        vtx[i] = v;
                        vtx.RemoveAt(j);
                    }
                }
            }
            Vertices = vtx.ToArray();
            Indices = idx.ToArray();
            IsLoaded = true;
        }

        private void Menu_ExportPLY()
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "PLY files (*.ply)|*.ply", FileName = GetName() };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, Data.ToPLY());
            }
        }

        private void Menu_OpenViewer()
        {
            MainFile.OpenMeshViewer(this);
        }
    }
}
