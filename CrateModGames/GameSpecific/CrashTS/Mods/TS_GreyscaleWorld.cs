using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_GreyscaleWorld : ModStruct<ChunkInfoSM>
    {
        public override string Name => Twins_Text.Mod_GreyscaleWorld;
        public override string Description => Twins_Text.Mod_GreyscaleWorldDesc;
        public override List<ConsoleMode> SupportedConosles => new List<ConsoleMode>() { ConsoleMode.PS2 };
        public override int Category => (int)ModProps.Misc;

        public override void ModPass(ChunkInfoSM info)
        {
            TwinsFile SM_Archive = info.File;

            if (SM_Archive.Type != TwinsFile.FileType.SM2) return;
            if (!SM_Archive.ContainsItem(6)) return;
            TwinsSection section = SM_Archive.GetItem<TwinsSection>(6);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Models) && section.Records.Count > 0)
            {
                TwinsSection model_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Models);

                foreach (TwinsItem item in model_section.Records)
                {
                    Model model = (Model)item;
                    for (int i = 0; i < model.SubModels.Count; i++)
                    {
                        for (int g = 0; g < model.SubModels[i].Groups.Count; g++)
                        {
                            for (int v = 0; v < model.SubModels[i].Groups[g].VData.Length; v++)
                            {
                                int maxVal = Math.Max(model.SubModels[i].Groups[g].VData[v].R, model.SubModels[i].Groups[g].VData[v].G);
                                maxVal = Math.Max(maxVal, model.SubModels[i].Groups[g].VData[v].B);
                                model.SubModels[i].Groups[g].VData[v].R = (byte)maxVal;
                                model.SubModels[i].Groups[g].VData[v].G = (byte)maxVal;
                                model.SubModels[i].Groups[g].VData[v].B = (byte)maxVal;
                            }
                        }
                    }
                }

            }
            if (section.ContainsItem((uint)RM_Graphics_Sections.Textures) && section.Records.Count > 0)
            {
                TwinsSection tex_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);

                foreach (TwinsItem item in tex_section.Records)
                {
                    Texture tex = (Texture)item;
                    for (int i = 0; i < tex.RawData.Length; i++)
                    {
                        int maxVal = Math.Max(tex.RawData[i].R, tex.RawData[i].G);
                        maxVal = Math.Max(maxVal, tex.RawData[i].B);
                        tex.RawData[i] = Color.FromArgb(tex.RawData[i].A, maxVal, maxVal, maxVal);
                    }
                    tex.UpdateImageData();
                }

            }

            SceneryData scenery = (SceneryData)SM_Archive.GetItem<TwinsItem>(0);
            if (scenery.LightsAmbient.Count > 0)
            {
                foreach (SceneryData.LightAmbient light in scenery.LightsAmbient)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsDirectional.Count > 0)
            {
                foreach (SceneryData.LightDirectional light in scenery.LightsDirectional)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsPoint.Count > 0)
            {
                foreach (SceneryData.LightPoint light in scenery.LightsPoint)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
            if (scenery.LightsNegative.Count > 0)
            {
                foreach (SceneryData.LightNegative light in scenery.LightsNegative)
                {
                    float maxVal = Math.Max(light.Color_R, light.Color_G);
                    maxVal = Math.Max(maxVal, light.Color_B);
                    light.Color_R = maxVal;
                    light.Color_G = maxVal;
                    light.Color_B = maxVal;
                }
            }
        }
    }
}
