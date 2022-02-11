using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_CameraFOV : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;
        private double FoV_Mod_1;
        private double FoV_Mod_2;
        private double FoV_Mod_3;

        public CrashTri_Rand_CameraFOV()
        {
            isRandom = Crash1.Crash1_Props_Misc.Option_RandCameraFOV.Enabled || Crash2.Crash2_Props_Misc.Option_RandCameraFOV.Enabled || Crash3.Crash3_Props_Misc.Option_RandCameraFOV.Enabled;
        }

        public override void BeforeModPass()
        {
            rand = GetRandom();

            FoV_Mod_1 = 1.5d;
            if (isRandom)
            {
                FoV_Mod_1 = rand.NextDouble() + 0.5d;
            }

            FoV_Mod_2 = 0.8d;
            if (isRandom)
            {
                FoV_Mod_2 = (rand.NextDouble() / 2d) + 0.75d;
            }

            FoV_Mod_3 = 0.8d;
            if (isRandom)
            {
                FoV_Mod_3 = (rand.NextDouble() / 2d) + 0.75d;
            }
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (OldZoneEntry zone in pair.nsf.GetEntries<OldZoneEntry>())
            {
                foreach (OldCamera cam in zone.Cameras)
                {
                    short newFOV = (short)Math.Floor(cam.Zoom * FoV_Mod_1);
                    cam.Zoom = newFOV;
                }
            }
            foreach (ZoneEntry zone in pair.nsf.GetEntries<ZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.FOV != null && ent.FOV.RowCount > 0)
                    {
                        for (int i = 0; i < ent.FOV.Rows.Count; i++)
                        {
                            for (int d = 0; d < ent.FOV.Rows[i].Values.Count; d++)
                            {
                                int newFOV = (int)Math.Floor(ent.FOV.Rows[i].Values[d].VictimID * FoV_Mod_2);
                                ent.FOV.Rows[i].Values[d] = new EntityVictim((short)newFOV);
                            }
                        }
                    }
                }
            }
            foreach (NewZoneEntry zone in pair.nsf.GetEntries<NewZoneEntry>())
            {
                foreach (Entity ent in zone.Entities)
                {
                    if (ent.FOV != null && ent.FOV.RowCount > 0)
                    {
                        for (int i = 0; i < ent.FOV.Rows.Count; i++)
                        {
                            for (int d = 0; d < ent.FOV.Rows[i].Values.Count; d++)
                            {
                                int newFOV = (int)Math.Floor(ent.FOV.Rows[i].Values[d].VictimID * FoV_Mod_3);
                                ent.FOV.Rows[i].Values[d] = new EntityVictim((short)newFOV);
                            }
                        }
                    }
                }
            }
        }
    }
}
