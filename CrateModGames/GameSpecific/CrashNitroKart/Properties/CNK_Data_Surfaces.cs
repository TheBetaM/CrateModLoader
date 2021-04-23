using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    [ModCategory((int)ModProps.Surfaces)]
    static class CNK_Data_Surfaces
    {
        public static string[] SurfaceNames = Enum.GetNames(typeof(SurfaceTypes));

        /// <summary> Percentage of m_MaxForwardSpeedNormal | Note : this also affects the boost speed </summary>
        public static ModPropNamedFloatArray Surface_m_MinSpeedPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Percentage of m_NormalLongFriction | we slowdown per second </summary>
        public static ModPropNamedFloatArray Surface_m_SlowDownLongPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Percentage of m_AccelerationGainNormal | we slowdown per second </summary>
        public static ModPropNamedFloatArray Surface_m_SlowDownAccelPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0, 0, 0.1f, 0.1f, 0.1f }, SurfaceNames);
        /// <summary> Percentage of Current Boost speed | Lost while on this surface </summary>
        public static ModPropNamedFloatArray Surface_m_SlowDownBoostPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.5f, 0.5f, 0, 0.1f, 0.1f, 0.1f }, SurfaceNames);
        /// <summary> Percentage of Current Boost speed | gained while on this surface </summary>
        public static ModPropNamedFloatArray Surface_m_SpeedBoostIncreasePercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0.1f, 0.1f }, SurfaceNames);
        /// <summary> The percentage of braking | force we loose </summary>
        public static ModPropNamedFloatArray Surface_m_BrakeLossPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Lat friction reduction percent | Note: When in a slide this number is ignored </summary>
        public static ModPropNamedFloatArray Surface_m_LatFrictionLossPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.97f, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Long friction reduction percent | Note: When in a slide this number is ignored </summary>
        public static ModPropNamedFloatArray Surface_m_LongFrictionLossPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.97f, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Slide friction reduction percent | Note: This is only when sliding and is an all round reduction loss </summary>
        public static ModPropNamedFloatArray Surface_m_SlideFrictionLossPercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.25f, 0, 0, 0, 0 }, SurfaceNames);
        /// <summary> Speed and Acceleration Increase | Note: This is mainly for mag-lev to get a speed inc </summary>
        public static ModPropNamedFloatArray Surface_m_SpeedAccelIncreasePercent = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.1f, 0.1f, 0.1f }, SurfaceNames);
        /// <summary> Kart height offset for the surface </summary>
        public static ModPropNamedFloatArray Surface_m_KartHeightOffset = new ModPropNamedFloatArray(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -0.35f, 0, -0.6f, 0, 0, 0.25f, 0.25f, 0.25f }, SurfaceNames);
    }
}
