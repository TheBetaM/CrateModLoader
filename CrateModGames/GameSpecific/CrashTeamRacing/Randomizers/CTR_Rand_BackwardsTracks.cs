using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    //unfinished
    public class CTR_Rand_BackwardsTracks : ModStruct<CtrScene>
    {
        private Random rand;
        private bool isRandom;

        public CTR_Rand_BackwardsTracks()
        {
            isRandom = CTR_Props_Main.Option_Rand_BackwardsTracks.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(CtrScene lev)
        {
            if (CTR_Common.GetTrackName(lev.path) != string.Empty)
            {
                if (!isRandom || rand.Next(2) == 0)
                {
                    foreach (Pose pa in lev.header.startGrid)
                    {
                        pa.Position = new System.Numerics.Vector3(pa.Position.X - 1000, pa.Position.Y, pa.Position.Z);
                        pa.Rotation = new System.Numerics.Vector3(pa.Rotation.X, pa.Rotation.Y + 2048, pa.Rotation.Z);
                    }

                    lev.restartPts.Reverse();

                    foreach (Pose pa in lev.restartPts)
                        pa.Rotation = new System.Numerics.Vector3(pa.Rotation.X, pa.Rotation.Y + 2048, pa.Rotation.Z);

                    int maxpos = 0;
                    foreach (QuadBlock qb in lev.quads)
                    {
                        if (qb.trackPos != 0xFF)
                            if (qb.trackPos > maxpos)
                                maxpos = qb.trackPos;
                    }

                    foreach (QuadBlock qb in lev.quads)
                    {
                        if (qb.trackPos != 0xFF)
                            qb.trackPos = (byte)(maxpos - qb.trackPos);
                    }

                    foreach (PickupHeader pick in lev.pickups)
                    {
                        if (pick.ThreadID == CtrThreadID.StartBanner)
                        {
                            pick.Pose.Rotation = new System.Numerics.Vector3(pick.Pose.Rotation.X, pick.Pose.Rotation.Y + 2048, pick.Pose.Rotation.Z);
                        }
                    }

                    /*
                    lev.nav.paths.Reverse();
                    lev.nav.ptrs.Reverse();
                    foreach (AIPath path in lev.nav.paths)
                    {
                        path.frames.Reverse();
                        NavFrame frame = path.frames[0];
                        path.frames.Add(path.start);

                        foreach (NavFrame f in path.frames)
                        {
                            f.angle.Y += 2048;
                        }

                        path.start = frame;
                        path.frames.RemoveAt(0);
                    }
                    */
                }
            }
        }
    }
}
