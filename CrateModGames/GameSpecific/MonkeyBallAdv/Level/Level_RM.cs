using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrateModLoader.LevelAPI;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public class Level_RM : Level<ChunkInfoRM>
    {
        public override Dictionary<int, string> CategoryNames => new Dictionary<int, string>()
        {
            [0] = "Group 0",
            [1] = "Group 1",
            [2] = "Group 2",
            [3] = "Group 3",
            [4] = "Group 4",
            [5] = "Group 5",
            [6] = "Group 6",
            [7] = "Group 7",
            [8] = "Group 8",
            [9] = "Group 9",
        };

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
                CollisionData.Add(RM_Col);
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
