using Crash;
using System;
using System.Collections.Generic;
using CrateModLoader.GameSpecific.Crash1;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific
{
    static class CrashTri_Common
    {
        public static void Fix_Detonator(NSF nsf)
        {
            List<Entity> nitros = new List<Entity>();
            List<Entity> detonators = new List<Entity>();
            foreach (NewZoneEntry zone in nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 34)
                    {
                        if (entity.Subtype == 18 && entity.ID.HasValue)
                        {
                            nitros.Add(entity);
                        }
                        else if (entity.Subtype == 24)
                        {
                            detonators.Add(entity);
                        }
                    }
                }
            }
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 34)
                    {
                        if (entity.Subtype == 18 && entity.ID.HasValue)
                        {
                            nitros.Add(entity);
                        }
                        else if (entity.Subtype == 24)
                        {
                            detonators.Add(entity);
                        }
                    }
                }
            }
            if (detonators.Count > 0)
            {
                foreach (Entity detonator in detonators)
                {
                    detonator.Victims.Clear();
                    foreach (Entity nitro in nitros)
                    {
                        detonator.Victims.Add(new EntityVictim((short)nitro.ID.Value));
                    }
                }
            }
        }

        public static void Fix_BoxCount(NSF nsf)
        {
            int boxcount = 0;
            List<Entity> willys = new List<Entity>();
            foreach (ZoneEntry zone in nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 0 && entity.Subtype == 0)
                    {
                        willys.Add(entity);
                    }
                    else if (entity.Type == 34)
                    {
                        switch (entity.Subtype)
                        {
                            case 0: // tnt
                            case 2: // empty
                            case 3: // spring
                            case 4: // continue
                            case 6: // fruit
                            case 8: // life
                            case 9: // doctor
                            case 10: // pickup
                            case 11: // pow
                            case 13: // ghost
                            case 17: // auto pickup
                            case 18: // nitro
                            case 20: // auto empty
                            case 21: // empty 2
                                boxcount++;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            foreach (NewZoneEntry zone in nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 0 && entity.Subtype == 0)
                    {
                        willys.Add(entity);
                    }
                    else if (entity.Type == 34)
                    {
                        switch (entity.Subtype)
                        {
                            case 0: // tnt
                            case 2: // empty
                            case 3: // spring
                            case 4: // continue
                            case 6: // fruit
                            case 8: // life
                            case 9: // doctor
                            case 10: // pickup
                            case 11: // pow
                            case 13: // ghost
                            case 17: // auto pickup
                            case 18: // nitro
                            case 20: // auto empty
                            case 21: // empty 2
                            case 25: // slot
                                boxcount++;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (entity.Type == 36)
                    {
                        if (entity.Subtype == 1)
                        {
                            boxcount++;
                        }
                    }
                }
            }
            foreach (Entity willy in willys)
            {
                if (willy.BoxCount.HasValue)
                {
                    willy.BoxCount = new EntitySetting(0, boxcount);
                }
            }
        }

        public static void InsertStringsInByteArray(ref byte[] array, int index, int len, List<string> str)
        {
            int word = 0;
            int letter = 0;
            for (int i = index; i < index + len; i++)
            {
                array[i] = (byte)str[word][letter];
                letter++;
                if (letter >= str[word].Length)
                {
                    letter = 0;
                    word++;
                    i++;
                    array[i] = (byte)0;
                    if (word >= str.Count)
                    {
                        i = index + len;
                    }
                }
            }
        }


    }

    public class EntityTypePair
    {
        public int Type;
        public int Subtype;

        public EntityTypePair(int t, int s)
        {
            Type = t;
            Subtype = s;
        }
    }

    public class NSF_Pair
    {
        public NSF nsf;
        public NSD nsd;
        public OldNSD oldnsd;
        public NewNSD newnsd;
        public bool isCrash1 = false;
        public bool isCrash2 = false;
        public bool isCrash3 = false;
        public Crash1_Levels LevelC1;
        public Crash2_Levels LevelC2;
        public Crash3_Levels LevelC3;
        public RegionType region;

        public NSF_Pair(RegionType r)
        {
            region = r;
        }
        public NSF_Pair(NSF n)
        {
            nsf = n;
        }
        public NSF_Pair(NSF n, NSD d, Crash2_Levels c, RegionType r)
        {
            nsf = n;
            nsd = d;
            LevelC2 = c;
            isCrash2 = true;
            region = r;
        }
        public NSF_Pair(NSF n, OldNSD d, Crash1_Levels c, RegionType r)
        {
            nsf = n;
            oldnsd = d;
            LevelC1 = c;
            isCrash1 = true;
            region = r;
        }
        public NSF_Pair(NSF n, NewNSD d, Crash3_Levels c, RegionType r)
        {
            nsf = n;
            newnsd = d;
            LevelC3 = c;
            isCrash3 = true;
            region = r;
        }
    }
}
