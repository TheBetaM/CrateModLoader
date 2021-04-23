using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash3
{
    public class Crash3_Rand_WarpRoom : ModStruct<NSF_Pair>
    {
        public override string Name => "Randomize Level Order";
        public override string Description => "Shuffles levels around (except for secret levels and ones with secret entrances). The last level is still the Cortex boss.";

        private Random rand;

        private Dictionary<Crash3_Levels, int> LevelValues = new Dictionary<Crash3_Levels, int>()
        {
            //[Crash3_Levels.WarpRoom] = 0x02,
            [Crash3_Levels.B02_Dingodile] = 0x03,
            [Crash3_Levels.B03_NTropy] = 0x04,
            [Crash3_Levels.B04_NGin] = 0x05,
            [Crash3_Levels.B01_TinyTiger] = 0x06,
            [Crash3_Levels.B05_Cortex] = 0x07,
            [Crash3_Levels.L03_OrientExpress] = 0x0A,
            [Crash3_Levels.L01_ToadVillage] = 0x0B,
            [Crash3_Levels.L04_BoneYard] = 0x0C,
            [Crash3_Levels.L18_TellNoTales] = 0x0D,
            [Crash3_Levels.L02_UnderPressure] = 0x0E,
            [Crash3_Levels.L06_GeeWiz] = 0x0F,
            [Crash3_Levels.L11_DinoMight] = 0x10,
            [Crash3_Levels.L10_MidnightRun] = 0x11,
            [Crash3_Levels.L09_TombTime] = 0x12,
            [Crash3_Levels.L17_ByeByeBlimps] = 0x13,
            [Crash3_Levels.L14_RoadCrash] = 0x14,
            [Crash3_Levels.L08_HogRide] = 0x15,
            [Crash3_Levels.L07_HangEmHigh] = 0x16,
            [Crash3_Levels.L24_MadBombers] = 0x17,
            [Crash3_Levels.L20_TombWader] = 0x18,
            [Crash3_Levels.L05_MakinWaves] = 0x19,
            [Crash3_Levels.L13_HighTime] = 0x1A,
            [Crash3_Levels.L19_FutureFrenzy] = 0x1B,
            [Crash3_Levels.L12_DeepTrouble] = 0x1C,
            [Crash3_Levels.L15_DoubleHeader] = 0x1D,
            [Crash3_Levels.L16_Sphynxinator] = 0x1E,
            [Crash3_Levels.L28_RingsOfPower] = 0x1F,
            [Crash3_Levels.L22_OrangeAsphalt] = 0x20,
            [Crash3_Levels.L26_SkiCrazed] = 0x21,
            [Crash3_Levels.L23_FlamingPassion] = 0x22,
            [Crash3_Levels.L21_GoneTomorrow] = 0x23,
            [Crash3_Levels.L25_BugLite] = 0x24,
            [Crash3_Levels.L27_Area51] = 0x25,
            [Crash3_Levels.L30_EggipusRex] = 0x26,
            [Crash3_Levels.L29_HotCoco] = 0x27,
        };
        private Dictionary<int, Crash3_Levels> ValuesToLevel = new Dictionary<int, Crash3_Levels>()
        {
            //[Crash3_Levels.WarpRoom] = 0x02,
            [0x03] = Crash3_Levels.B02_Dingodile,
            [0x04] = Crash3_Levels.B03_NTropy,
            [0x05] = Crash3_Levels.B04_NGin,
            [0x06] = Crash3_Levels.B01_TinyTiger,
            [0x07] = Crash3_Levels.B05_Cortex,
            [0x0A] = Crash3_Levels.L03_OrientExpress,
            [0x0B] = Crash3_Levels.L01_ToadVillage,
            [0x0C] = Crash3_Levels.L04_BoneYard,
            [0x0D] = Crash3_Levels.L18_TellNoTales,
            [0x0E] = Crash3_Levels.L02_UnderPressure,
            [0x0F] = Crash3_Levels.L06_GeeWiz,
            [0x10] = Crash3_Levels.L11_DinoMight,
            [0x11] = Crash3_Levels.L10_MidnightRun,
            [0x12] = Crash3_Levels.L09_TombTime,
            [0x13] = Crash3_Levels.L17_ByeByeBlimps,
            [0x14] = Crash3_Levels.L14_RoadCrash,
            [0x15] = Crash3_Levels.L08_HogRide,
            [0x16] = Crash3_Levels.L07_HangEmHigh,
            [0x17] = Crash3_Levels.L24_MadBombers,
            [0x18] = Crash3_Levels.L20_TombWader,
            [0x19] = Crash3_Levels.L05_MakinWaves,
            [0x1A] = Crash3_Levels.L13_HighTime,
            [0x1B] = Crash3_Levels.L19_FutureFrenzy,
            [0x1C] = Crash3_Levels.L12_DeepTrouble,
            [0x1D] = Crash3_Levels.L15_DoubleHeader,
            [0x1E] = Crash3_Levels.L16_Sphynxinator,
            [0x1F] = Crash3_Levels.L28_RingsOfPower,
            [0x20] = Crash3_Levels.L22_OrangeAsphalt,
            [0x21] = Crash3_Levels.L26_SkiCrazed,
            [0x22] = Crash3_Levels.L23_FlamingPassion,
            [0x23] = Crash3_Levels.L21_GoneTomorrow,
            [0x24] = Crash3_Levels.L25_BugLite,
            [0x25] = Crash3_Levels.L27_Area51,
            [0x26] = Crash3_Levels.L30_EggipusRex,
            [0x27] = Crash3_Levels.L29_HotCoco,
        };
        private List<Crash3_Levels> AllLevels = new List<Crash3_Levels>()
        {
            //Crash3_Levels.B02_Dingodile,
            //Crash3_Levels.B03_NTropy,
            //Crash3_Levels.B04_NGin,
            //Crash3_Levels.B01_TinyTiger,
            //Crash3_Levels.B05_Cortex,
            Crash3_Levels.L03_OrientExpress,
            Crash3_Levels.L01_ToadVillage,
            Crash3_Levels.L04_BoneYard,
            Crash3_Levels.L18_TellNoTales,
            Crash3_Levels.L02_UnderPressure,
            Crash3_Levels.L06_GeeWiz,
            Crash3_Levels.L11_DinoMight,
            Crash3_Levels.L10_MidnightRun,
            Crash3_Levels.L09_TombTime,
            Crash3_Levels.L17_ByeByeBlimps,
            Crash3_Levels.L14_RoadCrash,
            Crash3_Levels.L08_HogRide,
            //Crash3_Levels.L07_HangEmHigh,
            Crash3_Levels.L24_MadBombers,
            Crash3_Levels.L20_TombWader,
            Crash3_Levels.L05_MakinWaves,
            Crash3_Levels.L13_HighTime,
            //Crash3_Levels.L19_FutureFrenzy,
            Crash3_Levels.L12_DeepTrouble,
            Crash3_Levels.L15_DoubleHeader,
            Crash3_Levels.L16_Sphynxinator,
            //Crash3_Levels.L28_RingsOfPower,
            Crash3_Levels.L22_OrangeAsphalt,
            //Crash3_Levels.L26_SkiCrazed,
            Crash3_Levels.L23_FlamingPassion,
            Crash3_Levels.L21_GoneTomorrow,
            Crash3_Levels.L25_BugLite,
            //Crash3_Levels.L27_Area51,
            //Crash3_Levels.L30_EggipusRex,
            //Crash3_Levels.L29_HotCoco,
        };


        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair npair)
        {
            if (npair.LevelC3 != Crash3_Levels.WarpRoom)
            {
                return;
            }

            int LevelCount = 35;

            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < LevelCount; i++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < LevelCount; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Add(LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }

            List<Crash3_Levels> LevelsReplace = new List<Crash3_Levels>(AllLevels);
            List<Crash3_Levels> LevelsRandom = new List<Crash3_Levels>();
            for (int i = 0; i < AllLevels.Count; i++)
            {
                int r = rand.Next(LevelsReplace.Count);
                LevelsRandom.Add(LevelsReplace[r]);
                LevelsReplace.RemoveAt(r);
            }

            List<int> OrigValues = new List<int>();
            List<int> OrigValues_Names = new List<int>();

            int CortexID = 30;
            /*
            GOOLEntry warp = nsf.GetEntry<GOOLEntry>("WStOC");
            if (warp != null)
            {
                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        OrigValues.Add(warp.Instructions[4 + (i * 3)].Value);
                    }
                }

                for (int i = 0; i < LevelCount + 1; i++)
                {
                    if (i != CortexID)
                    {
                        if (i > CortexID)
                        {
                            warp.Instructions[4 + (i * 3)].Value = OrigValues[LevelsRand[i - 1]];
                        }
                        else
                        {
                            warp.Instructions[4 + (i * 3)].Value = OrigValues[LevelsRand[i]];
                        }
                    }
                }
            }
            */



            Dictionary<int, Crash3_Levels> SubtypeToLevel = new Dictionary<int, Crash3_Levels>();
            Dictionary<int, Crash3_Levels> SubtypeToLevelRand = new Dictionary<int, Crash3_Levels>();

            // Button Level
            foreach (NewZoneEntry zone in npair.nsf.GetEntries<NewZoneEntry>())
            {
                for (int i = 0; i < zone.Entities.Count; i++)
                {
                    if (zone.Entities[i].Type != null && zone.Entities[i].Subtype != null)
                    {
                        if (zone.Entities[i].Type == 73 && (int)zone.Entities[i].Subtype < 35) // button
                        {

                            int origSet = zone.Entities[i].Settings[0].ValueB;
                            Crash3_Levels origLevel = ValuesToLevel[origSet];
                            SubtypeToLevel.Add((int)zone.Entities[i].Subtype, origLevel);

                            if (AllLevels.Contains(origLevel))
                            {
                                int targetPos = AllLevels.IndexOf(origLevel);
                                Crash3_Levels targetLevel = LevelsRandom[targetPos];
                                int targetSet = LevelValues[targetLevel];
                                zone.Entities[i].Settings[0] = new EntitySetting(0, targetSet);
                                SubtypeToLevelRand.Add((int)zone.Entities[i].Subtype, targetLevel);
                            }
                            else
                            {
                                SubtypeToLevelRand.Add((int)zone.Entities[i].Subtype, Crash3_Levels.Unknown);
                            }

                            //zone.Entities[i].Subtype = LevelsRand[(int)zone.Entities[i].Subtype]; // buttons don't appear if it's not the right warp room
                        }
                    }
                }
            }

            // Button Visuals

            List<int> OrigValues_ButtonVis1 = new List<int>();
            List<int> OrigValues_ButtonVis2 = new List<int>();
            List<int> OrigValues_ButtonVis3 = new List<int>();
            List<int> OrigValues_ButtonVis4 = new List<int>();

            GOOLEntry warp2 = npair.nsf.GetEntry<GOOLEntry>("ButOC");
            if (warp2 != null)
            {
                for (int i = 0; i < LevelCount; i++)
                {
                    OrigValues_ButtonVis1.Add(warp2.Instructions[11 + (i * 8)].Value);
                    OrigValues_ButtonVis2.Add(warp2.Instructions[12 + (i * 8)].Value);
                    OrigValues_ButtonVis3.Add(warp2.Instructions[13 + (i * 8)].Value);
                    OrigValues_ButtonVis4.Add(warp2.Instructions[14 + (i * 8)].Value);
                }

                foreach (KeyValuePair<int, Crash3_Levels> pair in SubtypeToLevel)
                {
                    if (pair.Value != SubtypeToLevelRand[pair.Key] && SubtypeToLevelRand[pair.Key] != Crash3_Levels.Unknown)
                    {
                        int targetPos = 0;
                        foreach (KeyValuePair<int, Crash3_Levels> pair2 in SubtypeToLevelRand)
                        {
                            if (pair2.Value == pair.Value)
                            {
                                targetPos = pair2.Key;
                            }
                        }

                        warp2.Instructions[11 + (targetPos * 8)].Value = OrigValues_ButtonVis1[pair.Key];
                        warp2.Instructions[12 + (targetPos * 8)].Value = OrigValues_ButtonVis2[pair.Key];
                        warp2.Instructions[13 + (targetPos * 8)].Value = OrigValues_ButtonVis3[pair.Key];
                        warp2.Instructions[14 + (targetPos * 8)].Value = OrigValues_ButtonVis4[pair.Key];
                    }
                }
            }
        }
    }
}
