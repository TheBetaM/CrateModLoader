using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    //stability problems...
    public class CrashTri_Rand_WoodenCrates : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.isCrash1)
            {
                if (Crash1_Common.ChaseLevelsList.Contains(pair.LevelC1) || Crash1_Common.VehicleLevelsList.Contains(pair.LevelC1))
                {
                    return;
                }
            }

            List<CrateSubTypes> AvailableTypes = new List<CrateSubTypes>();

            List<CrateSubTypes> PossibleList = new List<CrateSubTypes>()
            {
                CrateSubTypes.Aku,
                CrateSubTypes.Blank,
                CrateSubTypes.Blank2,
                CrateSubTypes.Fruit,
                CrateSubTypes.Life,
                CrateSubTypes.Pickup,
                //CrateSubTypes.Pow,
                //CrateSubTypes.TNT, // walls of TNT...
                //CrateSubTypes.WoodSpring,
            };


            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        if (!AvailableTypes.Contains((CrateSubTypes)ent.Subtype))
                        {
                            AvailableTypes.Add((CrateSubTypes)ent.Subtype);
                        }
                    }
                }
            }
            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        if (!AvailableTypes.Contains((CrateSubTypes)ent.Subtype))
                        {
                            AvailableTypes.Add((CrateSubTypes)ent.Subtype);
                        }
                    }
                }
            }
            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        if (!AvailableTypes.Contains((CrateSubTypes)ent.Subtype))
                        {
                            AvailableTypes.Add((CrateSubTypes)ent.Subtype);
                        }
                    }
                }
            }



            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        if (pair.LevelC1 == Crash1_Levels.L22_LightsOut || pair.LevelC1 == Crash1_Levels.L23_FumblingInTheDark)
                        {
                            if (ent.Subtype != (byte)CrateSubTypes.Aku && ent.VecX != (short)CrateContentTypes.Mask)
                            {
                                ent.Subtype = (byte)AvailableTypes[rand.Next(AvailableTypes.Count)];
                            }
                        }
                        else
                        {
                            if (ent.VecX != (short)CrateContentTypes.Token_Brio && ent.VecX != (short)CrateContentTypes.Token_Cortex && ent.VecX != (short)CrateContentTypes.Token_Tawna)
                            {
                                ent.Subtype = (byte)AvailableTypes[rand.Next(AvailableTypes.Count)];
                            }
                        }
                    }
                }
            }
            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        ent.Subtype = (byte)AvailableTypes[rand.Next(AvailableTypes.Count)];
                    }
                }
            }
            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.Type == 34 && PossibleList.Contains((CrateSubTypes)ent.Subtype))
                    {
                        ent.Subtype = (byte)AvailableTypes[rand.Next(AvailableTypes.Count)];
                    }
                }
            }
        }
    }
}
