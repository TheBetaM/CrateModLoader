using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Crates_AllBlank : ModStruct<NSF_Pair>
    {

        private List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Outline, CrateSubTypes.Slot
        };

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34 && !((pair.LevelC1 == Crash1_Levels.L22_LightsOut || pair.LevelC1 == Crash1_Levels.L23_FumblingInTheDark) && ent.Subtype == (int)CrateSubTypes.Aku))
                    {
                        if (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype))
                        {
                            ent.Subtype = (byte)CrateSubTypes.Blank;
                        }
                    }
                }
            }

            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type != null && ent.Type == 34)
                    {
                        if (ent.Subtype != null && Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype))
                        {
                            int entType = (int)CrateSubTypes.Blank;
                            ent.Subtype = entType;
                        }
                    }
                }
            }

            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type != null && ent.Type == 34)
                    {
                        if (ent.Subtype != null && Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype))
                        {
                            int entType = (int)CrateSubTypes.Blank;
                            ent.Subtype = entType;
                        }
                    }
                }
            }

            CrashTri_Common.Fix_Detonator(pair.nsf);
            CrashTri_Common.Fix_BoxCount(pair.nsf);
        }
    }
}
