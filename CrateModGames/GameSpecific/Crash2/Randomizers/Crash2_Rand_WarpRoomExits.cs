using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash2;

namespace CrateModLoader.GameSpecific.Crash2
{
    public class Crash2_Rand_WarpRoomExits : ModStruct<NSF_Pair>
    {
        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            if (pair.LevelC2 != Crash2_Levels.WarpRoom && pair.LevelC2 != Crash2_Levels.WarpRoom2 && pair.LevelC2 != Crash2_Levels.WarpRoom3 && pair.LevelC2 != Crash2_Levels.WarpRoom4 && pair.LevelC2 != Crash2_Levels.WarpRoom5)
            {
                return;
            }

            // spawn 0 - load wall
            // spawn 3 - new game, intro & turtle woods spawn
            // spawn 4 - load game spawn

            List<NSDSpawnPoint> OrigSpawns = new List<NSDSpawnPoint>();
            List<NSDSpawnPoint> SpawnsToRand = new List<NSDSpawnPoint>();
            for (int i = 0; i < pair.nsd.Spawns.Count; i++)
            {
                //Console.WriteLine("ID:" + i + " Zone: " + nsd.Spawns[i].ZoneEID);
                OrigSpawns.Add(pair.nsd.Spawns[i]);
                if (i != 4 && i != 0 && i < 27)
                {
                    SpawnsToRand.Add(pair.nsd.Spawns[i]);
                }
            }
            int spawnCount = SpawnsToRand.Count;
            List<NSDSpawnPoint> RandSpawns = new List<NSDSpawnPoint>();
            for (int i = 0; i < OrigSpawns.Count; i++)
            {
                if (i != 4 && i != 0 && i < 27)
                {
                    int r = rand.Next(SpawnsToRand.Count);
                    RandSpawns.Add(SpawnsToRand[r]);
                    SpawnsToRand.RemoveAt(r);
                }
                else
                {
                    RandSpawns.Add(null);
                }
            }
            pair.nsd.Spawns.Clear();
            for (int i = 0; i < OrigSpawns.Count; i++)
            {

                if (RandSpawns[i] != null)
                {
                    pair.nsd.Spawns.Add(RandSpawns[i]);
                }
                else
                {
                    if (i == 4)
                    {
                        pair.nsd.Spawns.Add(RandSpawns[3]);
                    }
                    else
                    {
                        pair.nsd.Spawns.Add(OrigSpawns[i]);
                    }
                }
            }


            List<int> LevelsToReplace = new List<int>();
            for (int i = 0; i < 35; i++)
            {
                LevelsToReplace.Add(i);
            }
            List<int> LevelsRand = new List<int>();
            for (int i = 0; i < 35; i++)
            {
                int r = rand.Next(LevelsToReplace.Count);
                LevelsRand.Add(LevelsToReplace[r]);
                LevelsToReplace.RemoveAt(r);
            }
        }
    }
}
