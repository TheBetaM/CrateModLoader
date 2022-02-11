using System;
using System.Collections.Generic;
using Twinsanity;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv.Mods
{
    public class SMBA_Rand_BackwardsPuzzleLevels : ModStruct<ChunkInfoRM>
    {
        private bool isRandom = false;

        public SMBA_Rand_BackwardsPuzzleLevels()
        {
            isRandom = SMBA_Props_Main.Option_Rand_BackwardsPuzzleLevels.Enabled;
        }

        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(ChunkInfoRM rm)
        {
            if (isRandom && rand.Next(2) == 0)
                return;

            TwinsFile RM_Archive = rm.File;
            Pos SpawnPoint = null;
            Pos GoalPoint = null;
            ushort[] SpawnRot = new ushort[3];
            ushort[] GoalRot = new ushort[3];
            ushort[] SpawnRot2 = new ushort[3];
            ushort[] GoalRot2 = new ushort[3];
            bool hasGoal = false;
            bool onePlayer = false;

            for (uint section_id = 1; section_id <= 8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem(6)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>(6);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        InstanceMB instance = (InstanceMB)instances.Records[i];
                        if (instance.ObjectID == (ushort)DefaultEnums.ObjectID_MB.STANDARD_GOAL)
                        {
                            GoalPoint = new Pos(instance.Pos.X, instance.Pos.Y, instance.Pos.Z, instance.Pos.W);
                            SpawnRot = new ushort[3] { instance.RotX, instance.RotY, instance.RotZ };
                            SpawnRot2 = new ushort[3] { instance.COMRotX, instance.COMRotY, instance.COMRotZ };
                            hasGoal = true;
                        }
                        if (instance.ObjectID == (ushort)DefaultEnums.ObjectID_MB.PLAYER_MARKER)
                        {
                            SpawnPoint = new Pos(instance.Pos.X, instance.Pos.Y - 6f, instance.Pos.Z, instance.Pos.W);
                            GoalRot = new ushort[3] { instance.RotX, instance.RotY, instance.RotZ };
                            GoalRot2 = new ushort[3] { instance.COMRotX, instance.COMRotY, instance.COMRotZ };
                            onePlayer = !onePlayer;
                        }
                    }
                }
            }

            if (hasGoal && onePlayer)
            {
                for (uint section_id = 1; section_id <= 8; section_id++)
                {
                    if (!RM_Archive.ContainsItem(section_id)) continue;
                    TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                    if (section.Records.Count > 0)
                    {
                        if (!section.ContainsItem(6)) continue;
                        TwinsSection instances = section.GetItem<TwinsSection>(6);
                        for (int i = 0; i < instances.Records.Count; ++i)
                        {
                            InstanceMB instance = (InstanceMB)instances.Records[i];
                            if (instance.ObjectID == (ushort)DefaultEnums.ObjectID_MB.STANDARD_GOAL)
                            {
                                instance.Pos = SpawnPoint;
                                instance.RotX = SpawnRot[0];
                                instance.RotY = SpawnRot[1];
                                instance.RotZ = SpawnRot[2];
                                instance.COMRotX = SpawnRot2[0];
                                instance.COMRotY = SpawnRot2[1];
                                instance.COMRotZ = SpawnRot2[2];
                            }
                            if (instance.ObjectID == (ushort)DefaultEnums.ObjectID_MB.PLAYER_MARKER || instance.ObjectID == (ushort)DefaultEnums.ObjectID_MB.LEVEL_MASTER)
                            {
                                instance.Pos = GoalPoint;
                                instance.RotX = SpawnRot[0];
                                instance.RotY = SpawnRot[1];
                                instance.RotZ = SpawnRot[2];
                                instance.COMRotX = SpawnRot2[0];
                                instance.COMRotY = SpawnRot2[1];
                                instance.COMRotZ = SpawnRot2[2];
                            }
                            instances.Records[i] = instance;
                        }
                    }
                }
            }
        }
    }
}
