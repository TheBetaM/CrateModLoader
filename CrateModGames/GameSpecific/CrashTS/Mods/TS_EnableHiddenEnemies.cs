using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    public class TS_EnableHiddenEnemies : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Mod_UnusedEnemies;
        public override string Description => Twins_Text.Mod_UnusedEnemiesDesc;
        public override CreditContributors Contributors => new CreditContributors(ModLoaderGlobals.Contributor_BetaM);

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
                        if (instance.ObjectID == (uint)ObjectID.GLOBAL_BAT_DARKPURPLE)
                        {
                            if (instance.Flags > (uint)PropertyFlags.DisableObject)
                            {
                                instance.Flags -= (uint)PropertyFlags.DisableObject;
                            }
                        }
                        else if (instance.ObjectID == (uint)ObjectID.SCHOOL_FROGENSTEIN)
                        {
                            instance.Flags = 0x188B2E;
                        }
                        instances.Records[i] = instance;
                    }
                }
            }
        }
    }
}
