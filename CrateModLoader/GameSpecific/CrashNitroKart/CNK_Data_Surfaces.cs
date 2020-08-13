using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{

    public enum SurfaceTypes
    {
        eSURFACETYPE_NONE = 0,
        eSURFACETYPE_TRACK = 1,
        eSURFACETYPE_TRACK_DIRT_FAST = 2,
        eSURFACETYPE_TRACK_STONE = 3,
        eSURFACETYPE_TRACK_ROCK = 4,
        eSURFACETYPE_TRACK_WOOD = 5,
        eSURFACETYPE_TRACK_METAL = 6,
        eSURFACETYPE_TRACK_METAL_SHEET = 7,
        eSURFACETYPE_TRACK_ALIEN_METAL = 8,
        eSURFACETYPE_TRACK_SAND_FAST = 9,
        eSURFACETYPE_TRACK_SNOW_HARD = 10,
        eSURFACETYPE_TRACK_FACTOR_FLOOR = 11,
        eSURFACETYPE_TRACK_GRASS_FAST = 12,
        eSURFACETYPE_TRACK_BLASTERA = 13,
        eSURFACETYPE_TRACK_BLASTERB = 14,
        eSURFACETYPE_TRACK_GRASS_SLOW = 15,
        eSURFACETYPE_TRACK_SAND_SLOW = 16,
        eSURFACETYPE_TRACK_WATER = 17,
        eSURFACETYPE_TRACK_DIRT_SLOW = 18,
        eSURFACETYPE_TRACK_SNOW_POWDER = 19,
        eSURFACETYPE_TRACK_GRAVEL = 20,
        eSURFACETYPE_TRACK_TAR = 21, // 0.5, 0.7,0.5,-0.6
        eSURFACETYPE_TRACK_ICE = 22, // 0.5, 0.9, 0.9
        eSURFACETYPE_TRACK_CONVEYOR = 23,
        eSURFACETYPE_MAGLEV = 24,
        eSURFACETYPE_MAGLEV_BLASTERA = 25,
        eSURFACETYPE_MAGLEV_BLASTERB = 26,
    }

    [ModCategory((int)ModProps.Surfaces)]
    static class CNK_Data_Surfaces
    {
        
        /// <summary> Percentage of m_MaxForwardSpeedNormal | Note : this also affects the boost speed </summary>
        public static float[] Surface_m_MinSpeedPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0, 0, 0, 0, 0 };
        /// <summary> Percentage of m_NormalLongFriction | we slowdown per second </summary>
        public static float[] Surface_m_SlowDownLongPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary> Percentage of m_AccelerationGainNormal | we slowdown per second </summary>
        public static float[] Surface_m_SlowDownAccelPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0, 0, 0.1f, 0.1f, 0.1f };
        /// <summary> Percentage of Current Boost speed | Lost while on this surface </summary>
        public static float[] Surface_m_SlowDownBoostPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.5f, 0.5f, 0, 0.1f, 0.1f, 0.1f };
        /// <summary> Percentage of Current Boost speed | gained while on this surface </summary>
        public static float[] Surface_m_SpeedBoostIncreasePercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0.1f, 0.1f };
        /// <summary> The percentage of braking | force we loose </summary>
        public static float[] Surface_m_BrakeLossPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0, 0, 0, 0 };
        /// <summary> Lat friction reduction percent | Note: When in a slide this number is ignored </summary>
        public static float[] Surface_m_LatFrictionLossPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.97f, 0, 0, 0, 0 };
        /// <summary> Long friction reduction percent | Note: When in a slide this number is ignored </summary>
        public static float[] Surface_m_LongFrictionLossPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.97f, 0, 0, 0, 0 };
        /// <summary> Slide friction reduction percent | Note: This is only when sliding and is an all round reduction loss </summary>
        public static float[] Surface_m_SlideFrictionLossPercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.25f, 0, 0, 0, 0 };
        /// <summary> Speed and Acceleration Increase | Note: This is mainly for mag-lev to get a speed inc </summary>
        public static float[] Surface_m_SpeedAccelIncreasePercent = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0.1f, 0.1f };
        /// <summary> Kart height offset for the surface </summary>
        public static float[] Surface_m_KartHeightOffset = new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -0.35f, 0, -0.6f, 0, 0, 0.25f, 0.25f, 0.25f };

        public static void CNK_Randomize_SufParams(Random randState)
        {
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
            //todo
            Surface_m_SpeedAccelIncreasePercent[(int)SurfaceTypes.eSURFACETYPE_TRACK_DIRT_FAST] = 0.02f;
        }
    }
}
