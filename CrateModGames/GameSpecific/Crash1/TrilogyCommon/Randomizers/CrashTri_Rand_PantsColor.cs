using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_PantsColor : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;
        private SceneryColor targetColor;
        private OldSceneryColor targetOldColor;

        public CrashTri_Rand_PantsColor()
        {
            isRandom = Crash1_Props_Main.Option_RandPantsColor.Enabled || Crash2_Props_Main.Option_RandPantsColor.Enabled || Crash3_Props_Main.Option_RandPantsColor.Enabled;
        }

        public override void BeforeModPass()
        {
            if (isRandom)
            {
                rand = GetRandom();
                targetColor = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
                targetOldColor = new OldSceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), false);
            }
            else
            {
                targetOldColor = new OldSceneryColor((byte)(Crash1_Props_Misc.Prop_PantsColor.R * 255f), (byte)(Crash1_Props_Misc.Prop_PantsColor.G * 255f), (byte)(Crash1_Props_Misc.Prop_PantsColor.B * 255f), false);
                if (Crash2_Props_Misc.Prop_PantsColor.HasChanged)
                {
                    targetColor = new SceneryColor((byte)Crash2_Props_Misc.Prop_PantsColor.R, (byte)Crash2_Props_Misc.Prop_PantsColor.G, (byte)Crash2_Props_Misc.Prop_PantsColor.B, 0);
                }
                else if (Crash3_Props_Misc.Prop_PantsColor.HasChanged)
                {
                    targetColor = new SceneryColor((byte)Crash3_Props_Misc.Prop_PantsColor.R, (byte)Crash3_Props_Misc.Prop_PantsColor.G, (byte)Crash3_Props_Misc.Prop_PantsColor.B, 0);
                }
            }
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldModelEntry model in pair.nsf.GetEntries<OldModelEntry>())
            {
                if (model.EName.StartsWith("Wi"))
                {
                    // this does nothing...
                    // needs API update, Structs list is not used for saving
                    for (int i = 0; i < model.Structs.Count; ++i)
                    {
                        if (model.Structs[i] is OldSceneryColor col)
                        {
                            if (col.B > 0 && col.G < 110 && col.R < 110)
                            {
                                float intensity = col.B / 255f;
                                model.Structs[i] = new OldSceneryColor((byte)(targetOldColor.R * intensity), (byte)(targetOldColor.G * intensity), (byte)(targetOldColor.B * intensity), col.N);
                            }
                        }
                        else if (model.Structs[i] is OldModelTexture tex)
                        {
                            if (tex.B > 0 && tex.G < 110 && tex.R < 110)
                            {
                                float intensity = tex.B / 255f;
                                model.Structs[i] = new OldModelTexture(tex.UVIndex, tex.ClutX, tex.ClutY, tex.XOffU, tex.YOffU, tex.ColorMode, tex.BlendMode, tex.Segment,
                                    (byte)(targetOldColor.R * intensity), (byte)(targetOldColor.G * intensity), (byte)(targetOldColor.B * intensity),
                                    tex.N, tex.EID);
                            }
                        }
                    }
                }
            }
            foreach (ModelEntry e in pair.nsf.GetEntries<ModelEntry>())
            {
                if (e.EName.StartsWith("Cb") || e.EName.StartsWith("Cr") || e.EName.StartsWith("CR") ||
                    e.EName.StartsWith("Ch") || e.EName.StartsWith("CS") || e.EName.StartsWith("WiB") ||
                    e.EName.StartsWith("Cd") || e.EName.StartsWith("Cgb") || e.EName.StartsWith("CM"))
                {
                    // don't paint the back texture and shoes!
                    List<int> TexturedTris = new List<int>();
                    for (int t = 0; t < e.Triangles.Count; ++t)
                    {
                        if (e.Triangles[t].Tex > 0)
                        {
                            for (int i = 0; i < e.Triangles[t].Color.Length; i++)
                            {
                                TexturedTris.Add(e.Triangles[t].Color[i]);
                            }
                        }
                    }

                    for (int i = 0; i < e.Colors.Count; ++i)
                    {
                        if (e.Colors[i].Blue > 0 && e.Colors[i].Green < 110 && e.Colors[i].Red < 110 && !TexturedTris.Contains(i))
                        {
                            float intensity = e.Colors[i].Blue / 255f;
                            e.Colors[i] = new SceneryColor((byte)(targetColor.Red * intensity), (byte)(targetColor.Green * intensity), (byte)(targetColor.Blue * intensity), targetColor.Extra);
                        }
                    }
                }
            }
        }
    }
}
