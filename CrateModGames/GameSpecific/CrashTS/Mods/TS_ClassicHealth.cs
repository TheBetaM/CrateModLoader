using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_ClassicHealth : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Mod_ClassicHealth;
        public override string Description => Twins_Text.Mod_ClassicHealthDesc;

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (uint)ObjectID.CRASH || instance.ObjectID == (uint)ObjectID.CORTEX ||
                            instance.ObjectID == (uint)ObjectID.NINA || instance.ObjectID == (uint)ObjectID.MECHABANDICOOT)
                        {
                            instance.UnkI323[2] = 1;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
