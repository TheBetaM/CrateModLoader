using System;
using System.Collections.Generic;
using CTRFramework.Shared;
using CTRFramework;

namespace CrateModLoader.GameSpecific.CrashTeamRacing.Mods
{
    // doesn't work very well, most of the time pickups become invisible
    public class CTR_Rand_PickupLocations : ModStruct<Scene>
    {
        public override bool NeedsCachePass => true;

        private Random rand;

        private List<CTREvent> ValidChecks = new List<CTREvent>()
        {
            CTREvent.CrateFruit,
            CTREvent.CrateNitro,
            CTREvent.CrateRelic1,
            CTREvent.CrateRelic2,
            CTREvent.CrateRelic3,
            CTREvent.CrateTNT,
            CTREvent.CrateWeapon,
            CTREvent.Crystal,
            CTREvent.SingleFruit,
            //CTREvent.LetterC,
            //CTREvent.LetterT,
            //CTREvent.LetterR,
        };

        class PositionBank
        {
            public List<Vector3s> Positions;
            public List<Vector3s> Angles;

            public PositionBank()
            {
                Positions = new List<Vector3s>();
                Angles = new List<Vector3s>();
            }
        }
        private Dictionary<string, PositionBank> Banks;

        public override void BeforeCachePass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
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

        public override void CachePass(Scene lev)
        {
            if (lev.path.Contains("relic"))
            {
                string track = CTR_Common.GetTrackName(lev.path);
                PositionBank bank = new PositionBank();
                foreach (PickupHeader pick in lev.pickups)
                {
                    if (ValidChecks.Contains(pick.Event))
                    {
                        bank.Positions.Add(pick.Position);
                        bank.Angles.Add(pick.Angle);
                    }
                }
                Banks.Add(track, bank);
            }
        }

        public override void ModPass(Scene lev)
        {
            PositionBank bank = GetBank(lev.path);
            if (bank != null)
            {
                List<Vector3s> PosLeft = new List<Vector3s>(bank.Positions);
                List<Vector3s> AngLeft = new List<Vector3s>(bank.Angles);
                int r = 0;
                foreach (PickupHeader pick in lev.pickups)
                {
                    if (ValidChecks.Contains(pick.Event))
                    {
                        r = rand.Next(PosLeft.Count);
                        pick.Position = PosLeft[r];
                        pick.Angle = AngLeft[r];
                        PosLeft.RemoveAt(r);
                        AngLeft.RemoveAt(r);
                    }
                }
            }
        }
    }
}