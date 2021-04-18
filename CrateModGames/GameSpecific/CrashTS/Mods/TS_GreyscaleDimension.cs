using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: testing
    public class TS_GreyscaleDimension : ModStruct<ChunkInfoSM>
    {
        public override string Name => Twins_Text.Mod_GreyscaleDimension;
        public override string Description => Twins_Text.Mod_GreyscaleDimensionDesc;
        public override CreditContributors Contributors => new CreditContributors(ModLoaderGlobals.Contributor_BetaM);

        public override void ModPass(ChunkInfoSM info)
        {
            TwinsFile SM_Archive = info.File;
            ChunkType chunkType = info.Type;
            List<ChunkType> AllowedChunks = new List<ChunkType>()
            {
                ChunkType.AltEarth_Core_AftTreas,
                ChunkType.AltEarth_Core_CoreA,
                ChunkType.AltEarth_Core_CoreB,
                ChunkType.AltEarth_Core_CoreC,
                ChunkType.AltEarth_Core_CoreD,
                ChunkType.AltEarth_Core_PreTreas,
                ChunkType.AltEarth_Core_Throne,
                ChunkType.AltEarth_Core_Treasure,
                ChunkType.AltEarth_Hub_AltA,
                ChunkType.AltEarth_Hub_AltDoc,
                ChunkType.AltEarth_Hub_AltDoc_B,
                ChunkType.AltEarth_Hub_AltDoc_C,
                ChunkType.AltEarth_Hub_AltTunl,
                ChunkType.AltEarth_Hub_AlwaysOn,
                ChunkType.AltEarth_Hub_CoreEnt,
                ChunkType.AltEarth_Hub_SlipJoin,
                ChunkType.AltEarth_Lab_AltLabIn,
                ChunkType.AltEarth_Lab_LabExt,
                ChunkType.AltEarth_Lab_Psycho,
                ChunkType.AltEarth_Lab_PTCorr,
                ChunkType.AltEarth_Lab_PTExit,
                ChunkType.AltEarth_RockSlid_L10ChasA,
                ChunkType.AltEarth_RockSlid_L10ChasB,
                ChunkType.AltEarth_RockSlid_L10End,
                ChunkType.AltEarth_RockSlid_L10Roids,
                ChunkType.AltEarth_RockSlid_L10Start,
            };

            if (!AllowedChunks.Contains(chunkType))
            {
                return;
            }

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
