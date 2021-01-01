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
            foreach (NewZoneEntry zone in nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
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
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
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
            if (detonators.Count > 0)
            {
                foreach (Entity detonator in detonators)
                {
                    detonator.Victims.Clear();
                    foreach (Entity nitro in nitros)
                    {
                        detonator.Victims.Add(new EntityVictim((short)nitro.ID.Value));
                    }
                }
            }
        }

        public static void Fix_BoxCount(NSF nsf)
        {
            int boxcount = 0;
            List<Entity> willys = new List<Entity>();
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
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
            foreach (NewZoneEntry zone in nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
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
            foreach (SceneryEntry entry in nsf.GetEntries<SceneryEntry>())
            {
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
            foreach (NewSceneryEntry entry in nsf.GetEntries<NewSceneryEntry>())
            {
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
            foreach (OldSceneryEntry entry in nsf.GetEntries<OldSceneryEntry>())
            {
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

        public static void Mod_Scenery_Rainbow(NSF nsf, Random rand)
        {
            foreach (SceneryEntry entry in nsf.GetEntries<SceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (NewSceneryEntry entry in nsf.GetEntries<NewSceneryEntry>())
            {
                for (int i = 0; i < entry.Colors.Count; i++)
                {
                    SceneryColor color = entry.Colors[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Colors[i] = new SceneryColor(r, g, b, color.Extra);
                }
            }
            foreach (OldSceneryEntry entry in nsf.GetEntries<OldSceneryEntry>())
            {
                for (int i = 0; i < entry.Vertices.Count; i++)
                {
                    OldSceneryVertex color = entry.Vertices[i];
                    byte r = (byte)rand.Next(256);
                    byte g = (byte)rand.Next(256);
                    byte b = (byte)rand.Next(256);
                    entry.Vertices[i] = new OldSceneryVertex(color.X, color.Y, color.Z, r, g, b, color.FX);
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
            foreach (SceneryEntry entry in nsf.GetEntries<SceneryEntry>())
            {
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
            foreach (NewSceneryEntry entry in nsf.GetEntries<NewSceneryEntry>())
            {
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
            foreach (OldSceneryEntry entry in nsf.GetEntries<OldSceneryEntry>())
            {
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

        public static void Mod_Scenery_Untextured(NSF nsf)
        {
            foreach (SceneryEntry entry in nsf.GetEntries<SceneryEntry>())
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
            foreach (NewSceneryEntry entry in nsf.GetEntries<NewSceneryEntry>())
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
            foreach (OldSceneryEntry entry in nsf.GetEntries<OldSceneryEntry>())
            {
                for (int i = 0; i < entry.Polygons.Count; i++)
                {
                    OldSceneryPolygon poly = entry.Polygons[i];
                    entry.Polygons[i] = new OldSceneryPolygon(poly.VertexA, poly.VertexB, poly.VertexC, 0xFFF, 0, 0, 0);
                }
            }
        }

        public static void Mod_Camera_Closeup(NSF nsf)
        {
            foreach (ZoneEntry entry in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in entry.Entities)
                {
                    entity.ExtraProperties.Remove(0x142);
                }
            }
        }

        public static void Mod_RandomizeADIO(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is SoundChunk soundchunk)
                {
                    // randomization must be per-chunk, unless you want to come up with your own bin packing algorithm just for this...
                    List<int> oldeids = new List<int>();
                    foreach (Entry entry in soundchunk.Entries)
                    {
                        oldeids.Add(entry.EID);
                    }
                    foreach (Entry entry in soundchunk.Entries)
                    {
                        if (entry is SoundEntry)
                        {
                            int eid = oldeids[rand.Next(oldeids.Count)];
                            entry.EID = eid;
                            oldeids.Remove(eid);
                        }
                    }
                }
            }
        }

        public static void Mod_RandomizeWGEOTex(NSF nsf, Random rand)
        {
            foreach (SceneryEntry e in nsf.GetEntries<SceneryEntry>())
            {
                List<ModelTexture> tex_list = new List<ModelTexture>(e.Textures);
                List<ModelExtendedTexture> anim_list = new List<ModelExtendedTexture>(e.AnimatedTextures);
                e.Textures.Clear();
                e.AnimatedTextures.Clear();
                while (tex_list.Count > 0)
                {
                    var t = tex_list[rand.Next(tex_list.Count)];
                    e.Textures.Add(t);
                    tex_list.Remove(t);
                }
                while (anim_list.Count > 0)
                {
                    var t = anim_list[rand.Next(anim_list.Count)];
                    e.AnimatedTextures.Add(t);
                    anim_list.Remove(t);
                }
            }
            foreach (NewSceneryEntry e in nsf.GetEntries<NewSceneryEntry>())
            {
                List<ModelTexture> tex_list = new List<ModelTexture>(e.Textures);
                List<ModelExtendedTexture> anim_list = new List<ModelExtendedTexture>(e.AnimatedTextures);
                e.Textures.Clear();
                e.AnimatedTextures.Clear();
                while (tex_list.Count > 0)
                {
                    var t = tex_list[rand.Next(tex_list.Count)];
                    e.Textures.Add(t);
                    tex_list.Remove(t);
                }
                while (anim_list.Count > 0)
                {
                    var t = anim_list[rand.Next(anim_list.Count)];
                    e.AnimatedTextures.Add(t);
                    anim_list.Remove(t);
                }
            }
            /* Per-poly randomization (too random!)
                * 
            foreach (SceneryEntry e in nsf.GetEntries<SceneryEntry>())
            {
                SceneryEntry e = (SceneryEntry)en;
                int texture_count = e.Textures.Count;
                int anim_count = e.AnimatedTextures.Count;
                for (int i = 0; i < e.Triangles.Count; i++)
                {
                    SceneryTriangle tri = e.Triangles[i];
                    short new_tex = (short)(tri.Animated ? rand.Next(anim_count) : rand.Next(texture_count));
                    e.Triangles[i] = new SceneryTriangle(tri.VertexA, tri.VertexB, tri.VertexC, new_tex, tri.Animated);
                }
                for (int i = 0; i < e.Quads.Count; i++)
                {
                    SceneryQuad quad = e.Quads[i];
                    short new_tex = (short)(quad.Animated ? rand.Next(anim_count) : rand.Next(texture_count));
                    e.Quads[i] = new SceneryQuad(quad.VertexA, quad.VertexB, quad.VertexC, quad.VertexD, new_tex, quad.Unknown, quad.Animated);
                }
            }
            foreach (NewSceneryEntry e in nsf.GetEntries<NewSceneryEntry>())
            {
                NewSceneryEntry e = (NewSceneryEntry)en;
                int texture_count = e.Textures.Count;
                int anim_count = e.AnimatedTextures.Count;
                for (int i = 0; i < e.Triangles.Count; i++)
                {
                    SceneryTriangle tri = e.Triangles[i];
                    short new_tex = (short)(tri.Animated ? rand.Next(anim_count) : rand.Next(texture_count));
                    e.Triangles[i] = new SceneryTriangle(tri.VertexA, tri.VertexB, tri.VertexC, new_tex, tri.Animated);
                }
                for (int i = 0; i < e.Quads.Count; i++)
                {
                    SceneryQuad quad = e.Quads[i];
                    short new_tex = (short)(quad.Animated ? rand.Next(anim_count) : rand.Next(texture_count));
                    e.Quads[i] = new SceneryQuad(quad.VertexA, quad.VertexB, quad.VertexC, quad.VertexD, new_tex, quad.Unknown, quad.Animated);
                }
            }*/
        }

        public static void Mod_RandomizeTGEOCol(NSF nsf, Random rand)
        {
            foreach (ModelEntry e in nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    e.Colors[i] = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
                }
            }
        }

        public static void Mod_RandomizeTGEOTex(NSF nsf, Random rand)
        {
            foreach (ModelEntry e in nsf.GetEntries<ModelEntry>())
            {
                List<ModelTexture> tex_list = new List<ModelTexture>(e.Textures);
                List<ModelExtendedTexture> anim_list = new List<ModelExtendedTexture>(e.AnimatedTextures);
                e.Textures.Clear();
                e.AnimatedTextures.Clear();
                while (tex_list.Count > 0)
                {
                    var t = tex_list[rand.Next(tex_list.Count)];
                    e.Textures.Add(t);
                    tex_list.Remove(t);
                }
                while (anim_list.Count > 0)
                {
                    var t = anim_list[rand.Next(anim_list.Count)];
                    e.AnimatedTextures.Add(t);
                    anim_list.Remove(t);
                }
            }
        }

        // shouldn't this cause some serious memory read corruption? lol
        public static void Mod_RemoveTGEOTex(NSF nsf, Random rand)
        {
            foreach (ModelEntry e in nsf.GetEntries<ModelEntry>())
            {
                e.Textures.Clear();
                e.AnimatedTextures.Clear();
            }
        }

        public static void Mod_RemoveObjectColors(NSF nsf, Random rand)
        {
            foreach (ModelEntry e in nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    byte intensity = Math.Max(e.Colors[i].Red, e.Colors[i].Green);
                    intensity = Math.Max(intensity, e.Colors[i].Blue);
                    e.Colors[i] = new SceneryColor(intensity, intensity, intensity, 0);
                }
            }
        }

        public static void Mod_SwizzleObjectColors(NSF nsf, Random rand)
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

            foreach (ModelEntry e in nsf.GetEntries<ModelEntry>())
            {
                for (int i = 0; i < e.Colors.Count; ++i)
                {
                    SceneryColor color = e.Colors[i];
                    int r = color.Red;
                    int g = color.Green;
                    int b = color.Blue;
                    e.Colors[i] = new SceneryColor(
                        (byte)((r_r * r + r_g * g + r_b * b) / r_s),
                        (byte)((g_r * r + g_g * g + g_b * b) / g_s),
                        (byte)((b_r * r + b_g * g + b_b * b) / b_s),
                        color.Extra
                    );
                }
            }
        }

        public static void Mod_RandomizeSDIO(NSF nsf, Random rand)
        {
            var streams = nsf.GetEntries<SpeechEntry>();
            var eids = new List<int>();
            foreach (var stream in streams)
            {
                eids.Add(stream.EID);
            }
            foreach (SpeechEntry stream in streams)
            {
                int i = rand.Next(eids.Count);
                stream.EID = eids[i];
                eids.RemoveAt(i);
            }
        }

        public static List<SEP> Cache_MIDI = new List<SEP>();

        public static void ResetCache()
        {
            Cache_MIDI = new List<SEP>();
        }
        public static void Cache_Music(NSF nsf)
        {
            foreach (MusicEntry music in nsf.GetEntries<MusicEntry>())
            {
                Cache_MIDI.Add(music.SEP);
            }
        }

        public static void Randomize_Music(NSF nsf, Random rand)
        {
            //List<MusicEntry> CacheEntryList = new List<MusicEntry>();

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk datachunk)
                {
                    for (int i = 0; i < datachunk.Entries.Count; i++)
                    {
                        if (datachunk.Entries[i] is MusicEntry music)
                        {
                            SEP target_track = Cache_MIDI[rand.Next(Cache_MIDI.Count)];
                            SEP this_track = music.SEP;
                            List<SEQ> trackList = new List<SEQ>();

                            for (int t = 0; t < this_track.SEQs.Count; t++)
                            {
                                int len = this_track.SEQs[t].Data.Length;
                                byte[] new_track_data = target_track.SEQs[0].Data;
                                int flen = Math.Min(len, new_track_data.Length);
                                byte[] trimmed_data = new byte[flen];
                                for (int b = 0; b < flen; b++)
                                {
                                    trimmed_data[b] = new_track_data[b];
                                }

                                SEQ track = new SEQ(target_track.SEQs[0].Resolution, target_track.SEQs[0].Tempo, target_track.SEQs[0].Rhythm, trimmed_data);
                                trackList.Add(track);
                            }

                            this_track = new SEP(trackList);
                            datachunk.Entries[i] = new MusicEntry(music.VHEID, music.VB0EID, music.VB1EID, music.VB2EID, music.VB3EID, music.VB4EID, music.VB5EID, music.VB6EID, music.VH, this_track, music.EID);

                            /*
                            MusicEntry CacheEntry = new MusicEntry(music.VHEID, music.VB0EID, music.VB1EID, music.VB2EID, music.VB3EID, music.VB4EID, music.VB5EID, music.VB6EID, music.VH, target_track, music.EID);
                            CacheEntryList.Add(CacheEntry);
                            datachunk.Entries.RemoveAt(i);
                            i--;
                            */
                        }
                    }
                }
            }

            /*
            if (CacheEntryList.Count > 0)
            {
                for (int i = 0; i < CacheEntryList.Count; i++)
                {
                    NormalChunk addchunk = new NormalChunk();
                    addchunk.Entries.Add(CacheEntryList[i]);
                    nsf.Chunks.Add(addchunk);
                }
            }
            */
        }
    }
}
