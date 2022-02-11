using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Crash_RainbowPants : ModStruct<NSF_Pair>
    {
        private Random rand;
        private OldSceneryColor targetOldColor;

        public CrashTri_Crash_RainbowPants()
        {
            
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            SceneryColor targetColor = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
            int colorChange = rand.Next(3);

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

                    int count = 0;

                    for (int i = 0; i < e.Colors.Count; ++i)
                    {
                        if (e.Colors[i].Blue > 0 && e.Colors[i].Green < 110 && e.Colors[i].Red < 110 && !TexturedTris.Contains(i))
                        {
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        byte adder = (byte)((count / 8f) * 255);

                        for (int i = 0; i < e.Colors.Count; ++i)
                        {
                            if (e.Colors[i].Blue > 0 && e.Colors[i].Green < 110 && e.Colors[i].Red < 110 && !TexturedTris.Contains(i))
                            {
                                float intensity = e.Colors[i].Blue / 255f;
                                e.Colors[i] = new SceneryColor((byte)(targetColor.Red * intensity), (byte)(targetColor.Green * intensity), (byte)(targetColor.Blue * intensity), targetColor.Extra);

                                byte r = targetColor.Red;
                                byte g = targetColor.Green;
                                byte b = targetColor.Blue;
                                if (colorChange == 0)
                                {
                                    if (r < 255)
                                    {
                                        r += adder;
                                        if (g > 0)
                                        {
                                            g--;
                                        }
                                        if (b > 0)
                                        {
                                            b--;
                                        }
                                    }
                                    else
                                    {
                                        colorChange++;
                                    }
                                }
                                else if (colorChange == 1)
                                {
                                    if (g < 255)
                                    {
                                        g += adder;
                                        if (r > 0)
                                        {
                                            r--;
                                        }
                                        if (b > 0)
                                        {
                                            b--;
                                        }
                                    }
                                    else
                                    {
                                        colorChange++;
                                    }
                                }
                                else
                                {
                                    if (b < 255)
                                    {
                                        b += adder;
                                        if (r > 0)
                                        {
                                            r--;
                                        }
                                        if (g > 0)
                                        {
                                            g--;
                                        }
                                    }
                                    else
                                    {
                                        colorChange = 0;
                                    }
                                }

                                targetColor = new SceneryColor(r, g, b, targetColor.Extra);

                            }
                        }
                    }

                }
            }
        }
    }
}
