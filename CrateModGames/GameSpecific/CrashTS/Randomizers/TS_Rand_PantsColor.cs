using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;
using System.Drawing;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: testing
    public class TS_Rand_PantsColor : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Rand_PantsColor;
        public override string Description => Twins_Text.Rand_PantsColorDesc;
        public override CreditContributors Contributors => new CreditContributors(ModLoaderGlobals.Contributor_BetaM);

        private Color PantsColor;

        public override void BeforeModPass()
        {
            Random randState = new Random();
            PantsColor = Color.FromArgb(255, randState.Next(256), randState.Next(256), randState.Next(256));

            if (TS_Props_Misc.Prop_PantsColor.HasChanged)
            {
                PantsColor = Color.FromArgb(255, TS_Props_Misc.Prop_PantsColor.R, TS_Props_Misc.Prop_PantsColor.G, TS_Props_Misc.Prop_PantsColor.B);
            }
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (RM_Archive.Type != TwinsFile.FileType.RM2) return;
            if (!RM_Archive.ContainsItem(11)) return;
            TwinsSection section = RM_Archive.GetItem<TwinsSection>(11);
            if (section.ContainsItem((uint)RM_Graphics_Sections.Textures) && section.Records.Count > 0)
            {
                TwinsSection tex_section = section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);

                foreach (TwinsItem item in tex_section.Records)
                {
                    if (item.ID == 0x0B75575C || item.ID == 0x0325A8A8)
                    {
                        Texture tex = (Texture)item;
                        for (int i = 0; i < tex.RawData.Length; i++)
                        {
                            if (tex.RawData[i].B > tex.RawData[i].R + 8 && tex.RawData[i].B > tex.RawData[i].G + 8)
                            {
                                float intensity = tex.RawData[i].B / 255f;
                                tex.RawData[i] = Color.FromArgb(tex.RawData[i].A, (int)(PantsColor.R * intensity), (int)(PantsColor.G * intensity), (int)(PantsColor.B * intensity));
                            }
                        }
                        tex.UpdateImageData();
                    }
                }

            }
        }
    }
}
