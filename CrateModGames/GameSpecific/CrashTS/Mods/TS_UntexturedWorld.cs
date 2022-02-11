using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_UntexturedWorld : ModStruct<ChunkInfoSM>
    {
        public override void ModPass(ChunkInfoSM info)
        {
            TwinsFile SM_Archive = info.File;

            if (!SM_Archive.ContainsItem(6)) return;
            TwinsSection section = SM_Archive.GetItem<TwinsSection>(6);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Materials) && section.Records.Count > 0)
            {
                TwinsSection mat_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Materials);

                foreach (TwinsItem item in mat_section.Records)
                {
                    Material mat = (Material)item;
                    for (int i = 0; i < mat.Shaders.Count; i++)
                    {
                        mat.Shaders[i].TextureId = 0;
                        mat.Shaders[i].TxtMapping = TwinsShader.TextureMapping.OFF;
                    }
                }

            }
        }
    }
}
