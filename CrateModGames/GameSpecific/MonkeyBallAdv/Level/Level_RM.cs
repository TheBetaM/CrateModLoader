using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Level_RM : Level<ChunkInfoRM>
    {
        public override Dictionary<int, string> CategoryNames
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    [0] = "Instance Group 0",
                    [1] = "Instance Group 1",
                    [2] = "Instance Group 2",
                    [3] = "Instance Group 3",
                    [4] = "Instance Group 4",
                    [5] = "Instance Group 5",
                    [6] = "Instance Group 6",
                    [7] = "Instance Group 7",
                    [8] = "Instance Group 8",
                    [9] = "Instance Group 9",
                };
            }
            set { }
        }

        public override void Load(ChunkInfoRM file)
        {
            TwinsFile RM = file.File;

            uint col_section = 9;
            if (RM.Type == TwinsFile.FileType.MonkeyBallRM) col_section = 10;
            ColData col = RM.GetItem<ColData>(col_section);
            if (col != null)
            {
                CrashTS.CollisionData_RM RM_Col = new CrashTS.CollisionData_RM();
                RM_Col.Load(col);
                if (RM_Col.Vertices.Count > 0)
                {
                    CollisionData.Add(RM_Col);
                }
            }

            uint mb_add = 0;
            int IDcounter = 0;
            if (RM.Type == TwinsFile.FileType.MonkeyBallRM) mb_add = 1;
            for (uint i = mb_add; i <= mb_add + 7; ++i)
            {
                if (RM.ContainsItem(i) && RM.GetItem<TwinsSection>(i).ContainsItem(6))
                {
                    if (RM.Type == TwinsFile.FileType.MonkeyBallRM)
                    {
                        foreach (InstanceMB ins in RM.GetItem<TwinsSection>(i).GetItem<TwinsSection>(6).Records)
                        {
                            Object_InstanceMB Inst = new Object_InstanceMB();
                            Inst.Load(ins);
                            Inst.ObjectCategory = (int)i;
                            Inst.ID = IDcounter;
                            IDcounter++;
                            ObjectData.Add(Inst);
                        }
                    }
                }
                IDcounter = 0;
            }

        }

        public override void Save(ChunkInfoRM file)
        {
            // todo: save collision changes


        }
    }
}
