using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Scenery_Untextured : ModStruct<NSF_Pair>
    {
        public override void ModPass(NSF_Pair pair)
        {
            foreach (SceneryEntry entry in pair.nsf.GetEntries<SceneryEntry>())
            {
                for (int i = 0; i < entry.Triangles.Count; i++)
                {
                    SceneryTriangle tri = entry.Triangles[i];
                    entry.Triangles[i] = new SceneryTriangle(tri.VertexA, tri.VertexB, tri.VertexC, 0, false);
                }
                for (int i = 0; i < entry.Quads.Count; i++)
                {
                    SceneryQuad quad = entry.Quads[i];
                    entry.Quads[i] = new SceneryQuad(quad.VertexA, quad.VertexB, quad.VertexC, quad.VertexD, 0, 0, false);
                }
            }
            foreach (NewSceneryEntry entry in pair.nsf.GetEntries<NewSceneryEntry>())
            {
                for (int i = 0; i < entry.Triangles.Count; i++)
                {
                    SceneryTriangle tri = entry.Triangles[i];
                    entry.Triangles[i] = new SceneryTriangle(tri.VertexA, tri.VertexB, tri.VertexC, 0, false);
                }
                for (int i = 0; i < entry.Quads.Count; i++)
                {
                    SceneryQuad quad = entry.Quads[i];
                    entry.Quads[i] = new SceneryQuad(quad.VertexA, quad.VertexB, quad.VertexC, quad.VertexD, 0, 0, false);
                }
            }
            foreach (OldSceneryEntry entry in pair.nsf.GetEntries<OldSceneryEntry>())
            {
                for (int i = 0; i < entry.Polygons.Count; i++)
                {
                    OldSceneryPolygon poly = entry.Polygons[i];
                    entry.Polygons[i] = new OldSceneryPolygon(poly.VertexA, poly.VertexB, poly.VertexC, 0xFFF, 0, 0, 0);
                }
            }
        }
    }
}
