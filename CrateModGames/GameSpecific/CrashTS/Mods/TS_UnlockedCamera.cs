using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_UnlockedCamera : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Mod_UnlockedCamera;
        public override string Description => Twins_Text.Mod_UnlockedCameraDesc;

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.Camera)) continue;
                    TwinsSection cameras = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.Camera);
                    for (int i = 0; i < cameras.Records.Count; ++i)
                    {
                        Camera cam = (Camera)cameras.Records[i];
                        cam.Enabled = 0;
                        cameras.Records[i] = cam;
                    }
                }
            }
        }
    }
}
