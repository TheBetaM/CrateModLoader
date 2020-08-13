using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{

    [ModCategory((int)ModProps.DriverStats)]
    static class CNK_Data_DriverStats
    {
        //Temp values, maybe set them up as a class? todo
        /// <summary> float </summary>
        public static float[] c_MaxForwardSpeedNormal = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_MaxForwardSpeedWumpa = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_AccelerationGainNormal = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_AccelerationGainWumpa = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_BrakeForce = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnRateNormal = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnRateWumpa = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnRateBrake = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnRateAccel = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_HiTurnStartAngle = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> lat / long / lat 2 long </summary>
        public static float[,] c_HiTurnFriction = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> lat / long / lat 2 long </summary>
        public static float[,] c_NormalFriction = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> float </summary>
        public static float[] c_InAirTurnRateNormal = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_InAirTurnRateWumpa = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnDecellSpeed = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnDecellForce = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_TurnDecellForceMax = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_SlideMaxAngle = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_SlideMinAngle = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_SlideTurnRateInToSlide = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_SlideTurnRateAwayFromSlide = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> lat / long / lat 2 long </summary>
        public static float[,] c_SlideFrictionLow = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> lat / long / lat 2 long </summary>
        public static float[,] c_SlideFrictionNorm = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> lat / long / lat 2 long </summary>
        public static float[,] c_SlideFrictionHigh = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> float </summary>
        public static float[] c_BoostMaxImpulsePerSecond = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_BoostSlidePushTime = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_BoostSlidePushAngle = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_JUMP_SMALL = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_JUMP_MEDIUM = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_JUMP_LARGE = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_SLIDE_1 = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_SLIDE_2 = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_SLIDE_3 = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_PAD = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_START = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_AKU_DROP = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_TURBOBOOST = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_TURBOBOOST_JUICED = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> speed / time / wheelie </summary>
        public static float[,] c_BoostInfo_eBOOST_SUPER_ENGINE = new float[,] { { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 }, { 60, 7, 0 } };
        /// <summary> float </summary>
        public static float[] c_UIStats_Speed = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_UIStats_Acceleration = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_UIStats_Turn = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        /// <summary> float </summary>
        public static float[] c_UIStats_MaxValue = new float[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };

        public static void CNK_Randomize_CharacterStats(Random randState, int targetDriver)
        {

            //Boost sources speed, length
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_START[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;

            c_BoostMaxImpulsePerSecond[targetDriver] = randState.Next(1, 4);
            c_BoostSlidePushAngle[targetDriver] = 1;
            c_BoostSlidePushTime[targetDriver] = 1;

            c_BrakeForce[targetDriver] = 1;

            c_HiTurnFriction[targetDriver, 0] = 1;
            c_HiTurnFriction[targetDriver, 1] = 1;
            c_HiTurnFriction[targetDriver, 2] = 1;
            c_HiTurnStartAngle[targetDriver] = 1;

            c_InAirTurnRateNormal[targetDriver] = (float)randState.NextDouble() + 0.6f;
            c_InAirTurnRateWumpa[targetDriver] = c_InAirTurnRateNormal[targetDriver] + 0.2f;

            c_NormalFriction[targetDriver, 0] = 1;
            c_NormalFriction[targetDriver, 1] = 1;
            c_NormalFriction[targetDriver, 2] = 1;

            c_SlideFrictionHigh[targetDriver, 0] = (float)(randState.NextDouble() / 5f) + 0.75f;
            c_SlideFrictionHigh[targetDriver, 1] = c_SlideFrictionHigh[targetDriver, 0];
            c_SlideFrictionHigh[targetDriver, 2] = c_SlideFrictionHigh[targetDriver, 0];
            c_SlideFrictionLow[targetDriver, 0] = 1;
            c_SlideFrictionLow[targetDriver, 1] = 1;
            c_SlideFrictionLow[targetDriver, 2] = 1;
            c_SlideFrictionNorm[targetDriver, 0] = randState.Next(5, 10) / 10f;
            c_SlideFrictionNorm[targetDriver, 1] = c_SlideFrictionNorm[targetDriver, 0];
            c_SlideFrictionNorm[targetDriver, 2] = c_SlideFrictionNorm[targetDriver, 0];

            c_SlideMaxAngle[targetDriver] = 1;
            c_SlideMinAngle[targetDriver] = 1;
            c_SlideTurnRateAwayFromSlide[targetDriver] = 1;
            c_SlideTurnRateInToSlide[targetDriver] = 1;

            c_TurnDecellForce[targetDriver] = 1;
            c_TurnDecellForceMax[targetDriver] = 1;
            c_TurnDecellSpeed[targetDriver] = 1;

            c_TurnRateAccel[targetDriver] = 1;
            c_TurnRateBrake[targetDriver] = (float)(randState.NextDouble() / 2f) + 1f;

            //Speed
            c_MaxForwardSpeedNormal[targetDriver] = (float)randState.NextDouble() + 0.9f;
            c_MaxForwardSpeedWumpa[targetDriver] = c_MaxForwardSpeedNormal[targetDriver] + 0.01f;

            //Accel
            c_AccelerationGainNormal[targetDriver] = (float)randState.NextDouble() + 0.75f;
            c_AccelerationGainWumpa[targetDriver] = c_AccelerationGainNormal[targetDriver] + 0.01f;

            //Turning
            c_TurnRateNormal[targetDriver] = (float)randState.NextDouble() + 0.6f;
            c_TurnRateWumpa[targetDriver] = c_TurnRateNormal[targetDriver] + 0.01f;

            //UI Stats
            c_UIStats_MaxValue[targetDriver] = 7;
            c_UIStats_Speed[targetDriver] = (int)Math.Ceiling((c_MaxForwardSpeedNormal[targetDriver] / 1.9) * c_UIStats_MaxValue[targetDriver]);
            c_UIStats_Acceleration[targetDriver] = (int)Math.Ceiling((c_AccelerationGainNormal[targetDriver] / 1.75) * c_UIStats_MaxValue[targetDriver]);
            c_UIStats_Turn[targetDriver] = (int)Math.Ceiling((c_TurnRateNormal[targetDriver] / 1.6) * c_UIStats_MaxValue[targetDriver]);
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
