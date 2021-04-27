using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    //unfinished
    public class CTR_Rand_BackwardsTracks : ModStruct<Scene>
    {
        public override string Name => "Random Tracks Are Reversed";

        private Random rand;
        private bool isRandom;

        public CTR_Rand_BackwardsTracks(bool isrand)
        {
            isRandom = isrand;
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(Scene lev)
        {
            if (CTR_Common.GetTrackName(lev.path) != string.Empty)
            {
                if (!isRandom || rand.Next(2) == 0)
                {
                    foreach (Pose pa in lev.header.startGrid)
                    {
                        pa.Position.X -= 1000;
                        pa.Rotation.Y += 2048;
                    }

                    lev.restartPts.Reverse();

                    foreach (Pose pa in lev.restartPts)
                        pa.Rotation.Y += 2048;

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
                            qb.trackPos = (byte)(0xFE - qb.trackPos);
                    }

                    foreach (PickupHeader pick in lev.pickups)
                    {
                        if (pick.Event == CTREvent.FinishLap)
                        {
                            pick.Angle.Y += 2048;
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
