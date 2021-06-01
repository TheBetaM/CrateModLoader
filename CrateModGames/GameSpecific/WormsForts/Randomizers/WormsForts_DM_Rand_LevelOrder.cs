using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.WormsForts.XOM;

namespace CrateModLoader.GameSpecific.WormsForts
{
    //unfinished
    public class WF_DM_Rand_LevelOrder : ModStruct<XOM_File>
    {
        private List<string> DM_Names = new List<string>()
        {
            "Text.Level.Deathmatch1",
            "Text.Level.Deathmatch2",
            "Text.Level.Deathmatch3",
            "Text.Level.Deathmatch4",
            "Text.Level.Deathmatch5",
            "Text.Level.Deathmatch6",
            "Text.Level.Deathmatch7",
            "Text.Level.Deathmatch8",
            "Text.Level.Deathmatch9",
            "Text.Level.Deathmatch10",
        };
        private List<string> DM_Campaign_Names = new List<string>()
        {
            "FE.Trials.1",
            "FE.Trials.2",
            "FE.Trials.3",
            "FE.Trials.4",
            "FE.Trials.5",
            "FE.Trials.6",
            "FE.Trials.7",
            "FE.Trials.8",
            "FE.Trials.9",
            "FE.Trials.10",
        };

        private Random rand;

        public WF_DM_Rand_LevelOrder()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(XOM_File file)
        {
            if (!file.FileName.Contains("Local.xom")) return;

            LevelDetails[] DM_Levels = new LevelDetails[10];
            uint[] Campaign_Levels = new uint[10];

            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is LevelDetails lev)
                {
                    if (DM_Names.Contains(file.Strings[lev.LevelName.RawValue]))
                    {
                        DM_Levels[DM_Names.IndexOf(file.Strings[lev.LevelName.RawValue])] = new LevelDetails()
                        {
                            LevelName = lev.LevelName,
                            ScriptName = lev.ScriptName,
                            LevelType = lev.LevelType,
                            Brief = lev.Brief,
                            Image = lev.Image,
                            LevelNumber = lev.LevelNumber,
                            Lock = lev.Lock,
                            Movie = lev.Movie,
                            AIPathNodeStartYOffset = lev.AIPathNodeStartYOffset,
                            AIPathNodeCollisionStep = lev.AIPathNodeCollisionStep,
                        };
                    }
                }
                else if (file.Containers[i] is Campaign cam)
                {
                    if (DM_Campaign_Names.Contains(file.Strings[cam.LevelName.RawValue]))
                    {
                        int ID = DM_Campaign_Names.IndexOf(file.Strings[cam.LevelName.RawValue]);
                        Campaign_Levels[ID] = cam.ScriptName.RawValue;
                    }
                }
            }

            List<int> ToRand = new List<int>();
            List<int> RandList = new List<int>();
            for (int i = 0; i < DM_Levels.Length; i++)
            {
                RandList.Add(i);
            }
            while (RandList.Count > 0)
            {
                int r = rand.Next(RandList.Count);
                ToRand.Add(RandList[r]);
                RandList.RemoveAt(r);
            }

            for (int i = 0; i < file.Containers.Count; i++)
            {
                if (file.Containers[i] is LevelDetails lev)
                {
                    if (DM_Names.Contains(file.Strings[lev.LevelName.RawValue]))
                    {
                        int ID = DM_Names.IndexOf(file.Strings[lev.LevelName.RawValue]);
                        lev.LevelName = DM_Levels[ToRand[ID]].LevelName;
                        lev.ScriptName = DM_Levels[ToRand[ID]].ScriptName;
                        lev.LevelType = DM_Levels[ToRand[ID]].LevelType;
                        lev.Brief = DM_Levels[ToRand[ID]].Brief;
                        lev.Image = DM_Levels[ToRand[ID]].Image;
                        lev.LevelNumber = DM_Levels[ToRand[ID]].LevelNumber;
                        //lev.Lock = DM_Levels[ToRand[ID]].Lock;
                        lev.Movie = DM_Levels[ToRand[ID]].Movie;
                        lev.AIPathNodeStartYOffset = DM_Levels[ToRand[ID]].AIPathNodeStartYOffset;
                        lev.AIPathNodeCollisionStep = DM_Levels[ToRand[ID]].AIPathNodeCollisionStep;
                    }
                }
                else if (file.Containers[i] is Campaign cam)
                {
                    if (DM_Campaign_Names.Contains(file.Strings[cam.LevelName.RawValue]))
                    {
                        //int ID = DM_Campaign_Names.IndexOf(file.Strings[cam.LevelName.Value]);
                        //cam.ScriptName.Value = Campaign_Levels[ToRand[ID]];
                    }
                }
            }
        }
    }
}
