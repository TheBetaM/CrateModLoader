using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_ObjectTextures : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
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
    }
}
