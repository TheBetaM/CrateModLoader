using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    // doesn't work very well, most of the time pickups become invisible
    public class CTR_Rand_PickupLocations : ModStruct<CtrScene>
    {
        public override bool NeedsCachePass => true;

        private Random rand;

        private List<CtrThreadID> ValidChecks = new List<CtrThreadID>()
        {
            CtrThreadID.CrateFruit,
            CtrThreadID.CrateNitro,
            CtrThreadID.CrateRelic1,
            CtrThreadID.CrateRelic2,
            CtrThreadID.CrateRelic3,
            CtrThreadID.CrateTNT,
            CtrThreadID.CrateWeapon,
            CtrThreadID.Crystal,
            CtrThreadID.SingleFruit,
            //CtrThreadID.LetterC,
            //CtrThreadID.LetterT,
            //CtrThreadID.LetterR,
        };

        class PositionBank
        {
            public List<Pose> Poses;

            public PositionBank()
            {
                Poses = new List<Pose>();
            }
        }
        private Dictionary<string, PositionBank> Banks;

        public override void BeforeCachePass()
        {
            rand = GetRandom();
            Banks = new Dictionary<string, PositionBank>();
        }

        private PositionBank GetBank(string path)
        {
            foreach (string lev in CTR_Common.TrackFolderNames)
            {
                if (path.Contains(lev))
                {
                    return Banks[lev];
                }
            }
            return null;
        }

        public override void CachePass(CtrScene lev)
        {
            if (lev.path.Contains("relic"))
            {
                string track = CTR_Common.GetTrackName(lev.path);
                PositionBank bank = new PositionBank();
                foreach (PickupHeader pick in lev.pickups)
                {
                    if (ValidChecks.Contains(pick.ThreadID))
                    {
                        Pose pose = new Pose();
                        pose.Position = pick.Pose.Position;
                        pose.Rotation = pick.Pose.Rotation;
                        bank.Poses.Add(pose);
                    }
                }
                Banks.Add(track, bank);
            }
        }

        public override void ModPass(CtrScene lev)
        {
            PositionBank bank = GetBank(lev.path);
            if (bank != null)
            {
                List<Pose> PosLeft = new List<Pose>(bank.Poses);
                int r = 0;
                foreach (PickupHeader pick in lev.pickups)
                {
                    if (ValidChecks.Contains(pick.ThreadID))
                    {
                        r = rand.Next(PosLeft.Count);
                        pick.Pose = PosLeft[r];
                        PosLeft.RemoveAt(r);
                    }
                }
            }
        }
    }
}