using Crash;
using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.Crash3
{
    public static class Crash3_Mods
    {
        public enum CrateSubTypes
        {
            TNT = 0,
            Blank = 2,
            WoodSpring = 3,
            Checkpoint = 4,
            Iron = 5,
            Fruit = 6, //Multiple bounce
            IronSwitch = 7,
            Life = 8,
            Aku = 9,
            Pickup = 10,
            Pow = 11, //allegedly
            Outline = 13,
            IronSpring = 15,
            AutoPickup = 17,
            Nitro = 18,
            AutoBlank = 20,
            Blank2 = 21,
            Steel = 23,
            Slot = 25,
        }

        public static List<CrateSubTypes> Crates_ToReplace = new List<CrateSubTypes>()
        {
            CrateSubTypes.TNT, CrateSubTypes.Nitro, CrateSubTypes.Steel, CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup, CrateSubTypes.WoodSpring, CrateSubTypes.Outline, CrateSubTypes.Slot
        };
        public static List<CrateSubTypes> Crates_Wood = new List<CrateSubTypes>()
        {
            CrateSubTypes.Blank, //CrateSubTypes.Fruit, CrateSubTypes.Life, CrateSubTypes.Aku, CrateSubTypes.Pickup
        };

        public static void Mod_RandomWoodCrates(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Subtype != null && (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype)))
                                    {
                                        int entType = (int)Crates_Wood[rand.Next(Crates_Wood.Count)];
                                        ent.Subtype = entType;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CrashTri_Common.Fix_Detonator(nsf);
            CrashTri_Common.Fix_BoxCount(nsf);

        }

        public static void Mod_TurnCratesIntoWumpa(NSF nsf, Random rand)
        {
            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is NewZoneEntry)
                        {
                            NewZoneEntry zone = (NewZoneEntry)entry;
                            foreach (Entity ent in zone.Entities)
                            {
                                if (ent.Type != null && ent.Type == 34)
                                {
                                    if (ent.Subtype != null && (Crates_ToReplace.Contains((CrateSubTypes)ent.Subtype) || Crates_Wood.Contains((CrateSubTypes)ent.Subtype) || ent.Subtype == (int)CrateSubTypes.Checkpoint))
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
                                        if (ent.Settings.Count > 0)
                                        {
                                            while (ent.Settings.Count > 0)
                                            {
                                                ent.Settings.RemoveAt(0);
                                            }
                                            ent.Settings.Add(new EntitySetting(0, 0));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CrashTri_Common.Fix_Detonator(nsf);
            CrashTri_Common.Fix_BoxCount(nsf);
        }

    }
}
