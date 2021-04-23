using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_CrateContents : ModStruct<NSF_Pair>
    {
        public override string Name => CrashTri_Text.Rand_CrateContents;
        public override string Description => CrashTri_Text.Rand_CrateContentsDesc;

        private Random rand;

        public List<CrateContentTypes> C1_Crate_PossibleContents = new List<CrateContentTypes>()
        {
            CrateContentTypes.Wumpa_1,
            CrateContentTypes.Wumpa_2,
            CrateContentTypes.Wumpa_3,
            CrateContentTypes.Wumpa_4,
            CrateContentTypes.Wumpa_5,
            CrateContentTypes.Wumpa_6,
            CrateContentTypes.Wumpa_7,
            CrateContentTypes.Wumpa_8,
            CrateContentTypes.Wumpa_9,
            CrateContentTypes.Wumpa_10,
            CrateContentTypes.Wumpa_1_Anim,
            CrateContentTypes.Life,
            CrateContentTypes.Mask,
        };
        public List<CrateContentTypes> C2_Crate_PossibleContents = new List<CrateContentTypes>()
        {
            CrateContentTypes.Wumpa_1,
            CrateContentTypes.Wumpa_2,
            CrateContentTypes.Wumpa_3,
            CrateContentTypes.Wumpa_4,
            CrateContentTypes.Wumpa_5,
            CrateContentTypes.Wumpa_6,
            CrateContentTypes.Wumpa_7,
            CrateContentTypes.Wumpa_8,
            CrateContentTypes.Wumpa_9,
            CrateContentTypes.Wumpa_10,
            CrateContentTypes.Wumpa_1_Anim,
            CrateContentTypes.Life,
            CrateContentTypes.Mask,
        };
        public List<CrateContentTypes> C3_Crate_PossibleContents = new List<CrateContentTypes>()
        {
            CrateContentTypes.Wumpa_1,
            CrateContentTypes.Wumpa_2,
            CrateContentTypes.Wumpa_3,
            CrateContentTypes.Wumpa_4,
            CrateContentTypes.Wumpa_5,
            CrateContentTypes.Wumpa_6,
            CrateContentTypes.Wumpa_7,
            CrateContentTypes.Wumpa_8,
            CrateContentTypes.Wumpa_9,
            CrateContentTypes.Wumpa_10,
            CrateContentTypes.Wumpa_1_Anim,
            CrateContentTypes.Life,
            CrateContentTypes.Mask,
        };

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.isCrash1 && (Crash1_Common.VehicleLevelsList.Contains(pair.LevelC1) || Crash1_Common.ChaseLevelsList.Contains(pair.LevelC1)))
            {
                return; // mask crashes oops
            }
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34)
                    {
                        Crash1.CrateSubTypes sub = (Crash1.CrateSubTypes)ent.Subtype;
                        if (sub == Crash1.CrateSubTypes.Blank || sub == Crash1.CrateSubTypes.Pickup || sub == Crash1.CrateSubTypes.WoodSpring)
                        {
                            if (ent.VecX != (short)CrateContentTypes.Token_Brio && ent.VecX != (short)CrateContentTypes.Token_Cortex && ent.VecX != (short)CrateContentTypes.Token_Tawna)
                            {
                                ent.VecX = (short)C1_Crate_PossibleContents[rand.Next(C1_Crate_PossibleContents.Count)];
                            }
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
                        Crash2.CrateSubTypes sub = (Crash2.CrateSubTypes)ent.Subtype;
                        if (sub == Crash2.CrateSubTypes.Blank || sub == Crash2.CrateSubTypes.Pickup || sub == Crash2.CrateSubTypes.WoodSpring)
                        {
                            int r = rand.Next(C2_Crate_PossibleContents.Count);
                            ent.Settings[0] = new EntitySetting(0, (int)C2_Crate_PossibleContents[r]);
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
                        Crash3.CrateSubTypes sub = (Crash3.CrateSubTypes)ent.Subtype;
                        if (sub == Crash3.CrateSubTypes.Blank || sub == Crash3.CrateSubTypes.Pickup || sub == Crash3.CrateSubTypes.WoodSpring)
                        {
                            int r = rand.Next(C3_Crate_PossibleContents.Count);
                            ent.Settings[0] = new EntitySetting(0, (int)C3_Crate_PossibleContents[r]);
                        }
                    }
                }
            }
        }
    }
}
