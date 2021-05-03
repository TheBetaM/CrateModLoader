using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_CratesIntoWumpa : ModStruct<NSF_Pair>
    {
        private List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.Blank, CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life,
            CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Slot, CrateSubTypes.Checkpoint, //CrateSubTypes.Outline,
        };

        private Random rand;
        private bool isRandom;

        public CrashTri_Rand_CratesIntoWumpa()
        {
            isRandom = Crash1_Props_Main.Option_RandCratesMissing.Enabled || Crash2.Crash2_Props_Main.Option_RandCratesMissing.Enabled || Crash3.Crash3_Props_Main.Option_RandCratesMissing.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                if (zone.EName != "c0_3Z" && zone.EName != "d3_TZ" && zone.EName != "d4_TZ") //cortex power ending, castle machinery last crates
                {
                    foreach (OldEntity ent in zone.Entities)
                    {
                        if (ent.Type == 34 && !((pair.LevelC1 == Crash1_Levels.L22_LightsOut || pair.LevelC1 == Crash1_Levels.L23_FumblingInTheDark) && ent.Subtype == (int)CrateSubTypes.Aku))
                        {
                            if (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype))
                            {
                                if (!isRandom || (isRandom && rand.Next(2) == 0))
                                {
                                    ent.Type = 3;
                                    ent.Subtype = 16;
                                    //ent.Flags = 0x18; // these flags should remain unaltered
                                    ent.VecX = 0;
                                    ent.VecY = 0;
                                    ent.VecZ = 0;
                                    while (ent.Positions.Count > 1)
                                    {
                                        ent.Positions.RemoveAt(1);
                                    }
                                }
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
                        if (ent.Subtype != null && Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype))
                        {
                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                            {
                                ent.Type = 3;
                                ent.Subtype = 16;
                                ent.AlternateID = null;
                                ent.TimeTrialReward = null;
                                ent.Victims.Clear();
                                ent.BonusBoxCount = null;
                                ent.BoxCount = null;
                                ent.DDASection = null;
                                ent.DDASettings = null;
                                ent.ZMod = null;
                                ent.OtherSettings = null;
                                ent.Settings.Clear();
                                ent.ExtraProperties.Clear();
                            }
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
                            if (!isRandom || (isRandom && rand.Next(2) == 0))
                            {
                                ent.Type = 3;
                                ent.Subtype = 16;
                                ent.AlternateID = null;
                                ent.TimeTrialReward = null;
                                ent.Victims.Clear();
                                ent.BonusBoxCount = null;
                                ent.BoxCount = null;
                                ent.DDASection = null;
                                ent.DDASettings = null;
                                ent.ZMod = null;
                                ent.OtherSettings = null;
                                ent.Scaling = 0;
                                ent.Settings.Clear();
                                ent.Settings.Add(new EntitySetting(0, 0));
                                ent.ExtraProperties.Clear();
                            }
                        }
                    }
                    else if (ent.Type != null && ent.Type == 8 && ent.Subtype == 1) // kite
                    {
                        if (!isRandom || (isRandom && rand.Next(2) == 0))
                        {
                            ent.Type = 3;
                            ent.Subtype = 16;
                            ent.AlternateID = null;
                            ent.TimeTrialReward = null;
                            ent.Victims.Clear();
                            ent.BonusBoxCount = null;
                            ent.BoxCount = null;
                            ent.DDASection = null;
                            ent.DDASettings = null;
                            ent.ZMod = null;
                            ent.OtherSettings = null;
                            ent.Scaling = 0;
                            ent.Settings.Clear();
                            ent.Settings.Add(new EntitySetting(0, 0));
                            ent.ExtraProperties.Clear();
                        }
                    }
                    else if (ent.Type != null && ent.Type == 0 && ent.Subtype == 0)
                    {
                        if (ent.BoxCount != null)
                        {
                            ent.BoxCount = new EntitySetting(0, 0);
                        }
                        if (ent.BonusBoxCount != null)
                        {
                            ent.BonusBoxCount = new EntitySetting(0, 0);
                        }
                    }
                }
            }

            CrashTri_Common.Fix_Detonator(pair.nsf);
            CrashTri_Common.Fix_BoxCount(pair.nsf);
        }
    }
}
