using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public enum Drivers
    {
        Coco = 0,
        Cortex = 1,
        Crash = 2,
        Crunch = 3,
        Dingodile = 4,
        FakeCrash = 5,
        NGin = 6,
        Oxide = 7,
        NTrance = 8,
        NTropy = 9,
        Polar = 10,
        Pura = 11,
        RealVelo = 12,
        Tiny = 13,
        Zam = 14,
        Zem = 15,

        Nash = 16,
        Krunk = 17,
        EmperorVelo = 18,
        BigNorm = 19,
        SmallNorm = 20,
        Geary = 21,
        GearyMinion = 22,
        VeloMinion = 23,
    }

    [ModCategory((int)ModProps.DriverStats)]
    static class CNK_Data_DriverStats
    {
        public static string[] DriverNames = Enum.GetNames(typeof(Drivers));

        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_MaxForwardSpeedNormal  = new ModPropNamedFloatArray(new float[] 
        { 0.916666667f, 0.95f, 0.95f, 0.983333333f, 1f, 0.916666667f, 0.916666667f, 0.95f, 0.95f, 0.983333333f, 0.916666667f, 0.916666667f, 1f, 0.983333333f, 0.916666667f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_MaxForwardSpeedWumpa   = new ModPropNamedFloatArray(new float[] 
        { 0.925925926f, 0.955555556f, 0.955555556f, 0.985185185f, 1f, 0.925925926f, 0.925925926f, 0.955555556f, 0.955555556f, 0.985185185f, 0.925925926f, 0.925925926f, 1f, 0.985185185f, 0.925925926f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_AccelerationGainNormal = new ModPropNamedFloatArray(new float[] 
        { 0.85989011f, 0.906593407f, 0.906593407f, 0.85989011f, 0.85989011f, 0.766483516f, 0.85989011f, 0.85989011f, 0.85989011f, 0.953296703f, 0.85989011f, 0.85989011f, 1f, 0.85989011f, 0.85989011f, 0.766483516f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_AccelerationGainWumpa  = new ModPropNamedFloatArray(new float[] 
        { 0.85989011f, 0.906593407f, 0.906593407f, 0.85989011f, 0.85989011f, 0.766483516f, 0.85989011f, 0.85989011f, 0.85989011f, 0.953296703f, 0.85989011f, 0.85989011f, 1f, 0.85989011f, 0.85989011f, 0.766483516f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_BrakeForce             = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnRateNormal         = new ModPropNamedFloatArray(new float[] 
        { 0.902040816f, 0.608163265f, 0.608163265f, 0.510204082f, 0.510204082f, 0.510204082f, 0.902040816f, 0.706122449f, 0.706122449f, 0.706122449f, 1f, 1f, 0.706122449f, 0.510204082f, 1f, 0.706122449f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnRateWumpa          = new ModPropNamedFloatArray(new float[] 
        { 0.902040816f, 0.608163265f, 0.608163265f, 0.510204082f, 0.510204082f, 0.510204082f, 0.902040816f, 0.706122449f, 0.706122449f, 0.706122449f, 1f, 1f, 0.706122449f, 0.510204082f, 1f, 0.706122449f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnRateBrake          = new ModPropNamedFloatArray(new float[] 
        { 1.068571429f, 1.274285714f, 1.274285714f, 1.342857143f, 1.342857143f, 1.342857143f, 1.068571429f, 1.205714286f, 1.205714286f, 1.205714286f, 1f, 1f, 1.205714286f, 1.342857143f, 1f, 1.205714286f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnRateAccel          = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_HiTurnStartAngle       = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> lat / long / lat 2 long </summary>
        public static ModPropNamedFloatArray2 c_HiTurnFriction        = new ModPropNamedFloatArray2(new float[,] 
        { { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f  }, 
            { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> lat / long / lat 2 long </summary>
        public static ModPropNamedFloatArray2 c_NormalFriction        = new ModPropNamedFloatArray2(new float[,] 
        { { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f  }, 
            { 1f, 1f, 1f  }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_InAirTurnRateNormal    = new ModPropNamedFloatArray(new float[] 
        { 0.902040816f, 0.608163265f, 0.608163265f, 0.510204082f, 0.510204082f, 0.510204082f, 0.902040816f, 0.706122449f, 0.706122449f, 0.706122449f, 1f, 1f, 0.706122449f, 0.510204082f, 1f, 0.706122449f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_InAirTurnRateWumpa     = new ModPropNamedFloatArray(new float[] 
        { 0.902040816f, 0.608163265f, 0.608163265f, 0.510204082f, 0.510204082f, 0.510204082f, 0.902040816f, 0.706122449f, 0.706122449f, 0.706122449f, 1f, 1f, 0.706122449f, 0.510204082f, 1f, 0.706122449f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnDecellSpeed        = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnDecellForce        = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_TurnDecellForceMax     = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_SlideMaxAngle          = new ModPropNamedFloatArray(new float[]
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_SlideMinAngle          = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_SlideTurnRateInToSlide = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_SlideTurnRateAwayFromSlide = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> lat / long / lat 2 long </summary>
        public static ModPropNamedFloatArray2 c_SlideFrictionLow      = new ModPropNamedFloatArray2(new float[,]
        { { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> lat / long / lat 2 long </summary>
        public static ModPropNamedFloatArray2 c_SlideFrictionNorm     = new ModPropNamedFloatArray2(new float[,] 
        { { 0.9f, 0.9f, 0.9f }, { 0.6f, 0.6f, 0.6f }, { 0.6f, 0.6f, 0.6f }, { 0.5f, 0.5f, 0.5f }, { 0.5f, 0.5f, 0.5f }, { 0.5f, 0.5f, 0.5f }, { 0.9f, 0.9f, 0.9f },
            { 0.7f, 0.7f, 0.7f }, { 0.7f, 0.7f, 0.7f }, { 0.7f, 0.7f, 0.7f }, { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 0.7f, 0.7f, 0.7f }, { 0.5f, 0.5f, 0.5f }, { 1f, 1f, 1f }, { 0.7f, 0.7f, 0.7f } }, DriverNames);
        /// <summary> lat / long / lat 2 long </summary>
        public static ModPropNamedFloatArray2 c_SlideFrictionHigh     = new ModPropNamedFloatArray2(new float[,] 
        { { 0.931578947f, 0.931868132f, 0.931578947f }, { 0.726315789f, 0.727472527f, 0.726315789f }, { 0.726315789f, 0.727472527f, 0.726315789f }, { 0.657894737f, 0.659340659f, 0.657894737f }, 
            { 0.657894737f, 0.659340659f, 0.657894737f }, { 0.657894737f, 0.659340659f, 0.657894737f }, { 0.931578947f, 0.931868132f, 0.931578947f }, 
            { 0.794736842f, 0.795604396f, 0.794736842f }, { 0.794736842f, 0.795604396f, 0.794736842f }, { 0.794736842f, 0.795604396f, 0.794736842f }, 
            { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 0.794736842f, 0.795604396f, 0.794736842f }, { 0.657894737f, 0.659340659f, 0.657894737f }, { 1f, 1f, 1f }, { 0.794736842f, 0.795604396f, 0.794736842f } }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_BoostMaxImpulsePerSecond = new ModPropNamedFloatArray(new float[]
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_BoostSlidePushTime     = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_BoostSlidePushAngle    = new ModPropNamedFloatArray(new float[] 
        { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_JUMP_SMALL        = new ModPropNamedFloatArray2(new float[,] 
        { { 0.925925926f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.985185185f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.955555556f, 1f, 1f }, 
            { 0.985185185f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 1f, 1f, 1f }, { 0.985185185f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_JUMP_MEDIUM       = new ModPropNamedFloatArray2(new float[,] 
        { { 0.930423776f, 1f, 1f }, { 0.958254265f, 1f, 1f }, { 0.958254265f, 1f, 1f }, { 0.986084755f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.930423776f, 1f, 1f }, { 0.930423776f, 1f, 1f }, { 0.958254265f, 1f, 1f }, { 0.958254265f, 1f, 1f }, 
            { 0.986084755f, 1f, 1f }, { 0.930423776f, 1f, 1f }, { 0.930423776f, 1f, 1f }, { 1f, 1f, 1f }, { 0.986084755f, 1f, 1f }, { 0.930423776f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_JUMP_LARGE        = new ModPropNamedFloatArray2(new float[,] 
        { { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f}, { 0.961627899f, 1f, 1f }, { 0.9872093f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.961627899f, 1f, 1f }, 
            { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f }, { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f} }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_SLIDE_1           = new ModPropNamedFloatArray2(new float[,] 
        { { 0.925925926f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.985185185f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.955555556f, 1f, 1f }, { 0.955555556f, 1f, 1f }, 
            { 0.985185185f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 1f, 1f, 1f }, { 0.985185185f, 1f, 1f }, { 0.925925926f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_SLIDE_2           = new ModPropNamedFloatArray2(new float[,] 
        { { 0.931249992f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.986249998f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.958749995f, 1f, 1f }, 
            { 0.986249998f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 1f, 1f, 1f }, { 0.986249998f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_SLIDE_3           = new ModPropNamedFloatArray2(new float[,] 
        { { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.9872093f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.961627899f, 1f, 1f }, 
            { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f }, { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_PAD               = new ModPropNamedFloatArray2(new float[,] 
        { { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000036f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, 
            { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f }, { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_START             = new ModPropNamedFloatArray2(new float[,] 
        { { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.9872093f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.961627899f, 1f, 1f }, { 0.961627899f, 1f, 1f }, 
            { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f }, { 0.9872093f, 1f, 1f }, { 0.936046498f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_AKU_DROP          = new ModPropNamedFloatArray2(new float[,] 
        { { 0.931249992f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.986249998f, 1f, 1f },
            { 1f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.958749995f, 1f, 1f }, { 0.958749995f, 1f, 1f }, 
            { 0.986249998f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 1f, 1f, 1f }, { 0.986249998f, 1f, 1f }, { 0.931249992f, 1f, 1f }, { 1f, 1f, 1f} }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_TURBOBOOST        = new ModPropNamedFloatArray2(new float[,] 
        { { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000036f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, 
            { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f }, { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_TURBOBOOST_JUICED = new ModPropNamedFloatArray2(new float[,] 
        { { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000036f, 1f, 1f }, 
            { 1f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, 
            { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f }, { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> speed / time / wheelie </summary>
        public static ModPropNamedFloatArray2 c_BoostInfo_eBOOST_SUPER_ENGINE      = new ModPropNamedFloatArray2(new float[,] 
        { { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000036f, 1f, 1f },
            { 1f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000107f, 1f, 1f }, { 1.000000107f, 1f, 1f }, 
            { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f }, { 1.000000036f, 1f, 1f }, { 1.000000179f, 1f, 1f }, { 1f, 1f, 1f } }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_UIStats_Speed        = new ModPropNamedFloatArray(new float[] 
        { 2, 4, 4, 6, 7, 2, 2, 4, 4, 6, 2, 2, 7, 6, 2, 7 }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_UIStats_Acceleration = new ModPropNamedFloatArray(new float[]
        { 4, 5, 5, 4, 4, 2, 4, 4, 4, 6, 4, 4, 7, 4, 4, 2 }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_UIStats_Turn         = new ModPropNamedFloatArray(new float[] 
        { 6, 3, 3, 2, 2, 2, 6, 4, 4, 4, 7, 7, 4, 2, 7, 4 }, DriverNames);
        /// <summary> float </summary>
        public static ModPropNamedFloatArray c_UIStats_MaxValue     = new ModPropNamedFloatArray(new float[] 
        { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 }, DriverNames);

        public static void CNK_Randomize_CharacterStats(Random randState, int targetDriver)
        {

            //Boost sources speed, length
            c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;

            c_BoostMaxImpulsePerSecond.Value[targetDriver] = randState.Next(1, 4);
            c_BoostSlidePushAngle.Value[targetDriver] = 1;
            c_BoostSlidePushTime.Value[targetDriver] = 1;

            c_BrakeForce.Value[targetDriver] = 1;

            c_HiTurnFriction.Value[targetDriver, 0] = 1;
            c_HiTurnFriction.Value[targetDriver, 1] = 1;
            c_HiTurnFriction.Value[targetDriver, 2] = 1;
            c_HiTurnStartAngle.Value[targetDriver] = 1;

            c_InAirTurnRateNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.6f;
            c_InAirTurnRateWumpa.Value[targetDriver] = c_InAirTurnRateNormal.Value[targetDriver] + 0.2f;

            c_NormalFriction.Value[targetDriver, 0] = 1;
            c_NormalFriction.Value[targetDriver, 1] = 1;
            c_NormalFriction.Value[targetDriver, 2] = 1;

            c_SlideFrictionHigh.Value[targetDriver, 0] = (float)(randState.NextDouble() / 5f) + 0.75f;
            c_SlideFrictionHigh.Value[targetDriver, 1] = c_SlideFrictionHigh.Value[targetDriver, 0];
            c_SlideFrictionHigh.Value[targetDriver, 2] = c_SlideFrictionHigh.Value[targetDriver, 0];
            c_SlideFrictionLow.Value[targetDriver, 0] = 1;
            c_SlideFrictionLow.Value[targetDriver, 1] = 1;
            c_SlideFrictionLow.Value[targetDriver, 2] = 1;
            c_SlideFrictionNorm.Value[targetDriver, 0] = randState.Next(5, 10) / 10f;
            c_SlideFrictionNorm.Value[targetDriver, 1] = c_SlideFrictionNorm.Value[targetDriver, 0];
            c_SlideFrictionNorm.Value[targetDriver, 2] = c_SlideFrictionNorm.Value[targetDriver, 0];

            c_SlideMaxAngle.Value[targetDriver] = 1;
            c_SlideMinAngle.Value[targetDriver] = 1;
            c_SlideTurnRateAwayFromSlide.Value[targetDriver] = 1;
            c_SlideTurnRateInToSlide.Value[targetDriver] = 1;

            c_TurnDecellForce.Value[targetDriver] = 1;
            c_TurnDecellForceMax.Value[targetDriver] = 1;
            c_TurnDecellSpeed.Value[targetDriver] = 1;

            c_TurnRateAccel.Value[targetDriver] = 1;
            c_TurnRateBrake.Value[targetDriver] = (float)(randState.NextDouble() / 2f) + 1f;

            //Speed
            c_MaxForwardSpeedNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.9f;
            c_MaxForwardSpeedWumpa.Value[targetDriver] = c_MaxForwardSpeedNormal.Value[targetDriver] + 0.01f;

            //Accel
            c_AccelerationGainNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.75f;
            c_AccelerationGainWumpa.Value[targetDriver] = c_AccelerationGainNormal.Value[targetDriver] + 0.01f;

            //Turning
            c_TurnRateNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.6f;
            c_TurnRateWumpa.Value[targetDriver] = c_TurnRateNormal.Value[targetDriver] + 0.01f;

            //UI Stats
            c_UIStats_MaxValue.Value[targetDriver] = 7;
            c_UIStats_Speed.Value[targetDriver] = (int)Math.Ceiling((c_MaxForwardSpeedNormal.Value[targetDriver] / 1.9) * c_UIStats_MaxValue.Value[targetDriver]);
            c_UIStats_Acceleration.Value[targetDriver] = (int)Math.Ceiling((c_AccelerationGainNormal.Value[targetDriver] / 1.75) * c_UIStats_MaxValue.Value[targetDriver]);
            c_UIStats_Turn.Value[targetDriver] = (int)Math.Ceiling((c_TurnRateNormal.Value[targetDriver] / 1.6) * c_UIStats_MaxValue.Value[targetDriver]);
        }
    }

    public enum KartPhysicsCharacterRows
    {// This enum's comments are from the original CNK files, not made for this tool!
     /// <summary> float </summary>
        c_MaxForwardSpeedNormal = 1,
        /// <summary> float </summary>
        c_MaxForwardSpeedWumpa = 2,
        /// <summary> float </summary>
        c_AccelerationGainNormal = 3,
        /// <summary> float </summary>
        c_AccelerationGainWumpa = 4,
        /// <summary> float </summary>
        c_BrakeForce = 5,
        /// <summary> float </summary>
        c_TurnRateNormal = 7,
        /// <summary> float </summary>
        c_TurnRateWumpa = 8,
        /// <summary> float </summary>
        c_TurnRateBrake = 9,
        /// <summary> float </summary>
        c_TurnRateAccel = 10,
        /// <summary> float </summary>
        c_HiTurnStartAngle = 12,
        /// <summary> lat / long / lat 2 long </summary>
        c_HiTurnFriction = 13,
        /// <summary> lat / long / lat 2 long </summary>
        c_NormalFriction = 14,
        /// <summary> float </summary>
        c_InAirTurnRateNormal = 16,
        /// <summary> float </summary>
        c_InAirTurnRateWumpa = 17,
        /// <summary> float </summary>
        c_TurnDecellSpeed = 19,
        /// <summary> float </summary>
        c_TurnDecellForce = 20,
        /// <summary> float </summary>
        c_TurnDecellForceMax = 21,
        /// <summary> float </summary>
        c_SlideMaxAngle = 23,
        /// <summary> float </summary>
        c_SlideMinAngle = 24,
        /// <summary> float </summary>
        c_SlideTurnRateInToSlide = 25,
        /// <summary> float </summary>
        c_SlideTurnRateAwayFromSlide = 26,
        /// <summary> lat / long / lat 2 long </summary>
        c_SlideFrictionLow = 28,
        /// <summary> lat / long / lat 2 long </summary>
        c_SlideFrictionNorm = 29,
        /// <summary> lat / long / lat 2 long </summary>
        c_SlideFrictionHigh = 30,
        /// <summary> float </summary>
        c_BoostMaxImpulsePerSecond = 32,
        /// <summary> float </summary>
        c_BoostSlidePushTime = 33,
        /// <summary> float </summary>
        c_BoostSlidePushAngle = 34,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_JUMP_SMALL = 35,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_JUMP_MEDIUM = 36,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_JUMP_LARGE = 37,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_SLIDE_1 = 38,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_SLIDE_2 = 39,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_SLIDE_3 = 40,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_PAD = 41,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_START = 42,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_AKU_DROP = 43,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_TURBOBOOST = 44,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_TURBOBOOST_JUICED = 45,
        /// <summary> speed / time / wheelie </summary>
        c_BoostInfo_eBOOST_SUPER_ENGINE = 46,
        /// <summary> float </summary>
        c_UIStats_Speed = 48,
        /// <summary> float </summary>
        c_UIStats_Acceleration = 49,
        /// <summary> float </summary>
        c_UIStats_Turn = 50,
        /// <summary> float </summary>
        c_UIStats_MaxValue = 51,
    }
}
