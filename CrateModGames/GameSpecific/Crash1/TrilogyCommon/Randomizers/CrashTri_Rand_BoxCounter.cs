using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_BoxCounter : ModStruct<NSF_Pair>
    {
        private Random rand;

        private Dictionary<Crash2_Levels, int> minBoxCounts_C2 = new Dictionary<Crash2_Levels, int>()
        {
            [Crash2_Levels.L02_SnowGo] = 4,
            [Crash2_Levels.L05_CrashDash] = 8,
            [Crash2_Levels.L08_BearIt] = 10,
            [Crash2_Levels.L09_CrashCrush] = 18,
            [Crash2_Levels.L12_SewerOrLater] = 14,
            [Crash2_Levels.L13_BearDown] = 2,
            [Crash2_Levels.L15_UnBearable] = 18,
            [Crash2_Levels.L16_HanginOut] = 16,
            [Crash2_Levels.L18_ColdHardCrash] = 26,
            [Crash2_Levels.L25_SpacedOut] = 2,
            [Crash2_Levels.L26_TotallyBear] = 8,
        };

        private Dictionary<Crash3_Levels, int> minBoxCounts_C3 = new Dictionary<Crash3_Levels, int>()
        {
            [Crash3_Levels.L02_UnderPressure] = 20,
            [Crash3_Levels.L04_BoneYard] = 27,
            [Crash3_Levels.L06_GeeWiz] = 1,
            [Crash3_Levels.L07_HangEmHigh] = 6,
            [Crash3_Levels.L09_TombTime] = 6,
            [Crash3_Levels.L11_DinoMight] = 22,
            [Crash3_Levels.L12_DeepTrouble] = 10,
            [Crash3_Levels.L13_HighTime] = 4,
            [Crash3_Levels.L16_Sphynxinator] = 20,
            [Crash3_Levels.L17_ByeByeBlimps] = 1,
            [Crash3_Levels.L19_FutureFrenzy] = 8,
            [Crash3_Levels.L21_GoneTomorrow] = 6,
            [Crash3_Levels.L24_MadBombers] = 1,
            [Crash3_Levels.L25_BugLite] = 9,
            [Crash3_Levels.L26_SkiCrazed] = 21,
            [Crash3_Levels.L28_RingsOfPower] = 1,
            [Crash3_Levels.L29_HotCoco] = 18,
        };

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            /*
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldEntity ent in zone.Entities)
                {
                    //todo Crash 1 version
                }
            }
            */

            List<Entity> willys = new List<Entity>();

            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 0 && entity.Subtype == 0)
                    {
                        willys.Add(entity);
                    }
                }
            }
            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity entity in zone.Entities)
                {
                    if (entity.Type == 0 && entity.Subtype == 0)
                    {
                        willys.Add(entity);
                    }
                }
            }

            foreach (Entity willy in willys)
            {
                if (willy.BoxCount.HasValue)
                {
                    int boxcount = willy.BoxCount.Value.ValueB;
                    if (pair.isCrash2 && minBoxCounts_C2.ContainsKey(pair.LevelC2))
                    {
                        boxcount = rand.Next(minBoxCounts_C2[pair.LevelC2], boxcount + 1);
                    }
                    else if (pair.isCrash3 && minBoxCounts_C3.ContainsKey(pair.LevelC3))
                    {
                        boxcount = rand.Next(minBoxCounts_C3[pair.LevelC3], boxcount + 1);
                    }
                    else
                    {
                        boxcount = rand.Next(0, boxcount + 1);
                    }
                    willy.BoxCount = new EntitySetting(0, boxcount);
                }
            }

        }
    }
}
