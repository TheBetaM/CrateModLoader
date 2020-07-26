using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;

namespace CrateModLoader.GameSpecific
{
    // Common code based on CrashEdit by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
    static class CrashTri_Common
    {

        public static void Fix_Detonator(NSF nsf)
        {
            List<Entity> nitros = new List<Entity>();
            List<Entity> detonators = new List<Entity>();
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is EntryChunk)
                {
                    foreach (Entry entry in ((EntryChunk)chunk).Entries)
                    {
                        if (entry is NewZoneEntry)
                        {
                            foreach (Entity entity in ((NewZoneEntry)entry).Entities)
                            {
                                if (entity.Type == 34)
                                {
                                    if (entity.Subtype == 18 && entity.ID.HasValue)
                                    {
                                        nitros.Add(entity);
                                    }
                                    else if (entity.Subtype == 24)
                                    {
                                        detonators.Add(entity);
                                    }
                                }
                            }
                        }
                        if (entry is ZoneEntry)
                        {
                            foreach (Entity entity in ((ZoneEntry)entry).Entities)
                            {
                                if (entity.Type == 34)
                                {
                                    if (entity.Subtype == 18 && entity.ID.HasValue)
                                    {
                                        nitros.Add(entity);
                                    }
                                    else if (entity.Subtype == 24)
                                    {
                                        detonators.Add(entity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (detonators.Count > 0)
            {
                foreach (Entity detonator in detonators)
                {
                    detonator.Victims.Clear();
                    if (nitros.Count > 0)
                    {
                        foreach (Entity nitro in nitros)
                        {
                            detonator.Victims.Add(new EntityVictim((short)nitro.ID.Value));
                        }
                    }
                }
            }
        }

        public static void Fix_BoxCount(NSF nsf)
        {
            int boxcount = 0;
            List<Entity> willys = new List<Entity>();
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is EntryChunk entrychunk)
                {
                    foreach (Entry entry in entrychunk.Entries)
                    {
                        if (entry is ZoneEntry zone2)
                        {
                            foreach (Entity entity in zone2.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                                else if (entity.Type == 34)
                                {
                                    switch (entity.Subtype)
                                    {
                                        case 0: // tnt
                                        case 2: // empty
                                        case 3: // spring
                                        case 4: // continue
                                        case 6: // fruit
                                        case 8: // life
                                        case 9: // doctor
                                        case 10: // pickup
                                        case 11: // pow
                                        case 13: // ghost
                                        case 17: // auto pickup
                                        case 18: // nitro
                                        case 20: // auto empty
                                        case 21: // empty 2
                                            boxcount++;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        if (entry is NewZoneEntry zone3)
                        {
                            foreach (Entity entity in zone3.Entities)
                            {
                                if (entity.Type == 0 && entity.Subtype == 0)
                                {
                                    willys.Add(entity);
                                }
                                else if (entity.Type == 34)
                                {
                                    switch (entity.Subtype)
                                    {
                                        case 0: // tnt
                                        case 2: // empty
                                        case 3: // spring
                                        case 4: // continue
                                        case 6: // fruit
                                        case 8: // life
                                        case 9: // doctor
                                        case 10: // pickup
                                        case 11: // pow
                                        case 13: // ghost
                                        case 17: // auto pickup
                                        case 18: // nitro
                                        case 20: // auto empty
                                        case 21: // empty 2
                                        case 25: // slot
                                            boxcount++;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else if (entity.Type == 36)
                                {
                                    if (entity.Subtype == 1)
                                    {
                                        boxcount++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach (Entity willy in willys)
            {
                if (willy.BoxCount.HasValue)
                {
                    willy.BoxCount = new EntitySetting(0, boxcount);
                }
            }
        }

        public static void Mod_Scenery_Greyscale(NSF nsf)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (!(ck is EntryChunk))
                {
                    continue;
                }
                foreach (Entry en in ((EntryChunk)ck).Entries)
                {
                    if (en is SceneryEntry)
                    {
                        SceneryEntry entry = (SceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            byte avg = (byte)((r + g + b) / 3);
                            r = avg;
                            g = avg;
                            b = avg;
                            entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                        }
                    }
                    else if (en is NewSceneryEntry)
                    {
                        NewSceneryEntry entry = (NewSceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            byte avg = (byte)((r + g + b) / 3);
                            r = avg;
                            g = avg;
                            b = avg;
                            entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                        }
                    }
                    else if (en is OldSceneryEntry)
                    {
                        OldSceneryEntry entry = (OldSceneryEntry)en;
                        for (int i = 0; i < entry.Vertices.Count; i++)
                        {
                            OldSceneryVertex color = entry.Vertices[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            byte avg = (byte)((r + g + b) / 3);
                            r = avg;
                            g = avg;
                            b = avg;
                            entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
                        }
                    }
                }
            }
        }

        public static void Mod_Scenery_Rainbow(NSF nsf, Random rand)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (!(ck is EntryChunk))
                {
                    continue;
                }
                foreach (Entry en in ((EntryChunk)ck).Entries)
                {
                    if (en is SceneryEntry)
                    {
                        SceneryEntry entry = (SceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            r = (byte)rand.Next(0, 256);
                            g = (byte)rand.Next(0, 256);
                            b = (byte)rand.Next(0, 256);
                            entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                        }
                    }
                    else if (en is NewSceneryEntry)
                    {
                        NewSceneryEntry entry = (NewSceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            r = (byte)rand.Next(0, 256);
                            g = (byte)rand.Next(0, 256);
                            b = (byte)rand.Next(0, 256);
                            entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                        }
                    }
                    else if (en is OldSceneryEntry)
                    {
                        OldSceneryEntry entry = (OldSceneryEntry)en;
                        for (int i = 0; i < entry.Vertices.Count; i++)
                        {
                            OldSceneryVertex color = entry.Vertices[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            r = (byte)rand.Next(0, 256);
                            g = (byte)rand.Next(0, 256);
                            b = (byte)rand.Next(0, 256);
                            entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
                        }
                    }
                }
            }
        }

        public static void Mod_Scenery_Swizzle(NSF nsf, Random rand)
        {
            int r_r = rand.Next(2);
            int r_g = rand.Next(2);
            int r_b = rand.Next(2);
            int r_s = r_r + r_g + r_b;
            int g_r = rand.Next(2);
            int g_g = rand.Next(2);
            int g_b = rand.Next(2);
            int g_s = g_r + g_g + g_b;
            int b_r = rand.Next(2);
            int b_g = rand.Next(2);
            int b_b = rand.Next(2);
            int b_s = b_r + b_g + b_b;

            if (r_s == 0) r_s = 1;
            if (g_s == 0) g_s = 1;
            if (b_s == 0) b_s = 1;
            foreach (Chunk ck in nsf.Chunks)
            {
                if (!(ck is EntryChunk))
                {
                    continue;
                }
                foreach (Entry en in ((EntryChunk)ck).Entries)
                {
                    if (en is SceneryEntry)
                    {
                        SceneryEntry entry = (SceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            int r = color.Red;
                            int g = color.Green;
                            int b = color.Blue;
                            entry.Colors[i] = new SceneryColor(
                                (byte)((r_r * r + r_g * g + r_b * b) / r_s),
                                (byte)((g_r * r + g_g * g + g_b * b) / g_s),
                                (byte)((b_r * r + b_g * g + b_b * b) / b_s),
                                color.Extra
                            );
                        }
                    }
                    else if (en is NewSceneryEntry)
                    {
                        NewSceneryEntry entry = (NewSceneryEntry)en;
                        for (int i = 0; i < entry.Colors.Count; i++)
                        {
                            SceneryColor color = entry.Colors[i];
                            int r = color.Red;
                            int g = color.Green;
                            int b = color.Blue;
                            entry.Colors[i] = new SceneryColor(
                                (byte)((r_r * r + r_g * g + r_b * b) / r_s),
                                (byte)((g_r * r + g_g * g + g_b * b) / g_s),
                                (byte)((b_r * r + b_g * g + b_b * b) / b_s),
                                color.Extra
                            );
                        }
                    }
                    else if (en is OldSceneryEntry)
                    {
                        OldSceneryEntry entry = (OldSceneryEntry)en;
                        for (int i = 0; i < entry.Vertices.Count; i++)
                        {
                            OldSceneryVertex color = entry.Vertices[i];
                            byte r = color.Red;
                            byte g = color.Green;
                            byte b = color.Blue;
                            r = (byte)((r_r * r + r_g * g + r_b * b) / r_s);
                            g = (byte)((g_r * r + g_g * g + g_b * b) / g_s);
                            b = (byte)((b_r * r + b_g * g + b_b * b) / b_s);
                            entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
                        }
                    }
                }
            }
        }

        public static void Mod_Scenery_Untextured(NSF nsf)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (!(ck is EntryChunk))
                {
                    continue;
                }
                foreach (Entry en in ((EntryChunk)ck).Entries)
                {
                    if (en is SceneryEntry)
                    {
                        SceneryEntry e = (SceneryEntry)en;
                        for (int i = 0; i < e.Triangles.Count; i++)
                        {
                            SceneryTriangle tri = e.Triangles[i];
                            int vertexa = tri.VertexA;
                            int vertexb = tri.VertexB;
                            int vertexc = tri.VertexC;
                            short texture = 0;
                            bool animated = false;
                            e.Triangles[i] = new SceneryTriangle(vertexa, vertexb, vertexc, texture, animated);
                        }
                        for (int i = 0; i < e.Quads.Count; i++)
                        {
                            SceneryQuad quad = e.Quads[i];
                            int vertexa = quad.VertexA;
                            int vertexb = quad.VertexB;
                            int vertexc = quad.VertexC;
                            int vertexd = quad.VertexD;
                            short texture = 0;
                            byte unknown = 0;
                            bool animated = false;
                            e.Quads[i] = new SceneryQuad(vertexa, vertexb, vertexc, vertexd, texture, unknown, animated);
                        }
                    }
                    else if (en is NewSceneryEntry)
                    {
                        NewSceneryEntry e = (NewSceneryEntry)en;
                        for (int i = 0; i < e.Triangles.Count; i++)
                        {
                            SceneryTriangle tri = e.Triangles[i];
                            int vertexa = tri.VertexA;
                            int vertexb = tri.VertexB;
                            int vertexc = tri.VertexC;
                            short texture = 0;
                            bool animated = false;
                            e.Triangles[i] = new SceneryTriangle(vertexa, vertexb, vertexc, texture, animated);
                        }
                        for (int i = 0; i < e.Quads.Count; i++)
                        {
                            SceneryQuad quad = e.Quads[i];
                            int vertexa = quad.VertexA;
                            int vertexb = quad.VertexB;
                            int vertexc = quad.VertexC;
                            int vertexd = quad.VertexD;
                            short texture = 0;
                            byte unknown = 0;
                            bool animated = false;
                            e.Quads[i] = new SceneryQuad(vertexa, vertexb, vertexc, vertexd, texture, unknown, animated);
                        }
                    }
                    else if (en is OldSceneryEntry)
                    {
                        OldSceneryEntry entry = (OldSceneryEntry)en;
                        for (int i = 0; i < entry.Polygons.Count; i++)
                        {
                            OldSceneryPolygon poly = entry.Polygons[i];
                            entry.Polygons[i] = new OldSceneryPolygon(poly.VertexA, poly.VertexB, poly.VertexC, 0, 0, 0, 0);
                        }
                    }
                }
            }
        }

        public static void Mod_Camera_Closeup(NSF nsf)
        {
            foreach (Chunk ck in nsf.Chunks)
            {
                if (!(ck is EntryChunk))
                {
                    continue;
                }
                foreach (Entry en in ((EntryChunk)ck).Entries)
                {
                    if (!(en is ZoneEntry))
                    {
                        continue;
                    }
                    ZoneEntry entry = (ZoneEntry)en;
                    foreach (Entity entity in entry.Entities)
                    {
                        entity.ExtraProperties.Remove(0x142);
                    }
                }
            }
        }


    }
}
