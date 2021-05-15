using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test
    public class TS_Rand_Surfaces : ModStruct<ChunkInfoRM>
    {
        internal Dictionary<int, int> randSurfaces = new Dictionary<int, int>();

        private List<DefaultEnums.SurfaceTypes> SurfacesToChange = new List<DefaultEnums.SurfaceTypes>()
        {
            DefaultEnums.SurfaceTypes.SURF_DEFAULT,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY_RIGID_ONLY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_SLIGHTLY_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_ICE,
            DefaultEnums.SurfaceTypes.SURF_ICE_LOW_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_GRASS,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_METAL,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_MUD,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_ROCK,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SAND,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SNOW,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_STONE_TILES,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_WOOD,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_METAL,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_ROCK,
            //DefaultEnums.SurfaceTypes.SURF_STICKY_SNOW,
        };

        private List<DefaultEnums.SurfaceTypes> SurfacesToPlace = new List<DefaultEnums.SurfaceTypes>()
        {
            DefaultEnums.SurfaceTypes.SURF_DEFAULT,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_MEDIUM_SLIPPY_RIGID_ONLY,
            DefaultEnums.SurfaceTypes.SURF_GENERIC_SLIGHTLY_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_ICE,
            DefaultEnums.SurfaceTypes.SURF_ICE_LOW_SLIPPY,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_GRASS,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_METAL,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_MUD,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_ROCK,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SAND,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_SNOW,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_STONE_TILES,
            DefaultEnums.SurfaceTypes.SURF_NORMAL_WOOD,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_METAL,
            DefaultEnums.SurfaceTypes.SURF_SLIPPY_ROCK,
            //DefaultEnums.SurfaceTypes.SURF_STICKY_SNOW,
        };


        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            randSurfaces = new Dictionary<int, int>();
            for (int i = 0; i < SurfacesToChange.Count; i++)
            {
                // surfaces can repeat
                randSurfaces.Add((int)SurfacesToChange[i], (int)SurfacesToPlace[randState.Next(SurfacesToPlace.Count)]);
            }
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (!RM_Archive.ContainsItem(9)) return;
            TwinsItem section = RM_Archive.GetItem<TwinsItem>(9);
            ColData colData = (ColData)section;

            for (int i = 0; i < colData.Tris.Count; i++)
            {
                if (randSurfaces.ContainsKey(colData.Tris[i].Surface))
                {
                    colData.Tris[i] = new ColData.ColTri()
                    {
                        Surface = randSurfaces[colData.Tris[i].Surface],
                        Vert1 = colData.Tris[i].Vert1,
                        Vert2 = colData.Tris[i].Vert2,
                        Vert3 = colData.Tris[i].Vert3,
                    };
                }
            }
        }
    }
}
