using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    //unfinished
    public class CrashTri_Rand_MirrorLevel : ModStruct<NSF_Pair>
    {
        private Random rand;
        private bool isRandom;

        public CrashTri_Rand_MirrorLevel(bool isrand)
        {
            isRandom = Crash1_Props_Main.Option_RandMirroredWorld.Enabled || Crash2_Props_Main.Option_RandMirroredWorld.Enabled || Crash3_Props_Main.Option_RandMirroredWorld.Enabled;
        }

        public override void BeforeModPass()
        {
             rand = GetRandom();
        }

        public override void ModPass(NSF_Pair pair)
        {
            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {

                        }
                        else if (entry is SceneryEntry scen)
                        {
                            for (int i = 0; i < scen.Vertices.Count; i++)
                            {
                                SceneryVertex vertex = scen.Vertices[i];
                                int x = -vertex.X;
                                int y = vertex.Y;
                                int z = vertex.Z;
                                int unknownx = vertex.UnknownX;
                                int unknowny = vertex.UnknownY;
                                int unknownz = vertex.UnknownZ;
                                int color = vertex.Color;
                                scen.Vertices[i] = new SceneryVertex(x, y, z, unknownx, unknowny, unknownz);
                            }
                        }
                    }
                }
            }

            pair.nsd.Spawns[0].SpawnX = -pair.nsd.Spawns[0].SpawnX;
        }
    }
}
