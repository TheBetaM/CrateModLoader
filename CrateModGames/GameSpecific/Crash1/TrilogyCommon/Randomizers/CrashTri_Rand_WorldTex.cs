using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_WorldTex : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (SceneryEntry e in pair.nsf.GetEntries<SceneryEntry>())
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
            foreach (NewSceneryEntry e in pair.nsf.GetEntries<NewSceneryEntry>())
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
    }
}
