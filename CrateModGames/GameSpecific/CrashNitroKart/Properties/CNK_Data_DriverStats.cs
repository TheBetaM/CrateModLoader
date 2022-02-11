using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    [ModCategory((int)ModProps.DriverStats)] [ExecutesMods(typeof(CNK_Rand_CharacterStats))]
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
    }
}
