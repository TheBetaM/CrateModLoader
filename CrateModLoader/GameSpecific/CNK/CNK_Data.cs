using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CNK
{
    static class CNK_Data
    {
        public static int DriverAmount = 16;

        // These variable comments are from the original CNK files, not made for this tool!
        /// <summary> float | The minimum height we need to get without jumping before we set the in-air state (Note: This is from the center of the collision sphere!!!) </summary>
        public static float m_MinHeightForAirNoJump = 1f;
        /// <summary> float | The maximum we will allow our XY velocity to get. | 40 (Trying to limit aberrant behavior.) </summary>
        public static float m_MaxLinearVelXY = 33f;
        /// <summary> float | The maximum we will allow our Z velocity to get </summary>
        public static float m_MaxLinearVelZ = 50f;
        /// <summary> float | The collision sphere radius for the kart (m) </summary>
        public static float m_CollisionRadius = 0.52f;
        /// <summary> X, Y, Z | The collision sphere offset position from the kart (m) </summary>
        public static float[] m_CollisionSphereOffset = new float[] { 0, 0, 0.6f };
        /// <summary> float | The NORMAL maximum FORWARD SPEED of the kart (m/sec) | 27 </summary>
        public static float m_MaxForwardSpeedNormal = 24f;
        /// <summary> float | The WUMPA maximum FORWARD SPEED of the kart (m/sec) | 30 </summary>
        public static float m_MaxForwardSpeedWumpa = 27f;
        /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
        public static float m_MaxReverseSpeed = 10f;
        /// <summary> float | The NORMAL ACCELERATION GAIN of the kart (m/sec) | "18 (2.12s), 20 (1.82s), 22 (1.58s), 24 (1.40s), 26 (1.25s)" | 22 </summary>
        public static float m_AccelerationGainNormal = 26f;
        /// <summary> float | The WUMPA ACCELERATION GAIN of the kart (m/sec) | 25 </summary>
        public static float m_AccelerationGainWumpa = 29.5f;
        /// <summary> float | The REVERSE ACCELERATION GAIN of the kart (m/sec) | 37 </summary>
        public static float m_ReverseGain = 37f;
        /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
        public static float m_BrakeForce = 10f;
        /// <summary> float | Speed to determine when we are in low speed driving model (m/sec) </summary>
        public static float m_LowSpeed = 12f;
        /// <summary> float | The amount of GRAVITY when in AIR (x times gravity) </summary>
        public static float m_GravityAir = 4.5f;
        /// <summary> float | The amount of GRAVITY when on GROUND (x times gravity) </summary>
        public static float m_GravityGround = 2f;
        /// <summary> float | The amount of DOWNFORCE when in MAGLEV (x times gravity) </summary>
        public static float m_DownforceMagLev = 4.5f;
        /// <summary> float | The amount of DOWNFORCE when in MAGLEV and IN AIR (x times gravity) (Note: This is ONLY applied after we have gained air for m_DownforceMagLevAirTime seconds) | "9, 12, 14" </summary>
        public static float m_DownforceInAirMagLev = 11f;
        /// <summary> float | The amount of DOWNFORCE when on GROUND (x times gravity)</summary>
        public static float m_DownforceGround = 2.5f;
        /// <summary> float | The time we allow in AIR before we apply m_DownforceMagLevInAir | 0.3 </summary>
        public static float m_DownforceMagLevAirTime = 0.1f;
        /// <summary> float | The minimum angle that this kicks in (r) </summary>
        public static float m_SlopeMinAngle = 0f;
        /// <summary> float | The maximum angle where we achieve full extra (r) </summary>
        public static float m_SlopeMaxAngle = 60f;
        /// <summary> float | The acceleration increase (percent) </summary>
        public static float m_SlopeAccelExtra = 0.5f;
        /// <summary> float | The NORMAL kart turn rate (r/sec) | 80 </summary>
        public static float m_TurnRateNormal = 70f;
        /// <summary> float | The WUMPA kart turn rate (r/sec) | 80 </summary>
        public static float m_TurnRateWumpa = 70f;
        /// <summary> float | The kart additional turn rate when brake is pressed (r/sec) | 90 </summary>
        public static float m_TurnRateBrake = 110f;
        /// <summary> float | The kart additional turn rate when accelerator and not brake is pressed (r/sec) </summary>
        public static float m_TurnRateAccel = 10f;
        /// <summary> float | The angle from AT.VEL that we consider ourselves to be hard turning (rad) </summary>
        public static float m_HiTurnStartAngle = 15f;
        /// <summary> lat / long / lat 2 long | GROUND hi-turn friction parameters | 160 </summary>
        public static float[] m_HiTurnLatFriction = new float[] { 60f, 7f, 0f };
        /// <summary> lat / long / lat 2 long | GROUND normal friction parameters | 155 </summary>
        public static float[] m_NormalFriction = new float[] { 55f, 7f, 0f };
        /// <summary> float | The speed where turn decelleration effect kicks in (m/sec) </summary>
        public static float m_TurnDecellSpeed = 12f;
        /// <summary> float | The turn decell force (m/sec) (Formula is (Speed-m_TurnDecellSpeed) * m_TurnDecellForce) </summary>
        public static float m_TurnDecellForce = 3f;
        /// <summary> float | The maximum the turn decelleration force can be (m/sec) </summary>
        public static float m_TurnDecellForceMax = 20f;
        /// <summary> float | The NORMAL kart turn rate in air (r/sec) </summary>
        public static float m_InAirTurnRateNormal = 70f;
        /// <summary> float | The WUMPAed kart turn rate in air (r/sec) </summary>
        public static float m_InAirTurnRateWumpa = 70f;
        /// <summary> float | If we are > this speed then long friction is applied to the kart </summary>
        public static float m_InAirMinSpeed = 10f;
        /// <summary> 3 floats | Friction while in air, the long is removed when we are less or equal the above speed </summary>
        public static float[] m_InAirFriction = new float[] { 60, 5, 0 };
        /// <summary> float | The time in air (without jumping to initiate) we allow the user to still activate a jump | 0.1 </summary>
        public static float m_JumpAirTolerance = 0.15f;
        /// <summary> float | The base impulse for air (hop) | 7 </summary>
        public static float m_JumpImpulseBase = 7.8f;
        /// <summary> float | The base impulse for air (When in MAG-LEV mode) | "10, 12, 14" </summary>
        public static float m_JumpImpulseBaseMagLev = 14f;
        /// <summary> float | The minimum UP (z-axis) before we start using it for addition JUMP IMPULSE </summary>
        public static float m_JumpImpulseUpMin = 0f;
        /// <summary> float | The maximum UP that we add to the JUMP IMPULSE | "15, 12 ,10" </summary>
        public static float m_JumpImpulseUpMax = 7.5f;
        /// <summary> float | The modifier for the above values | "0.3, 0.25" </summary>
        public static float m_JumpImpulseUpPercent = 0.4f;
        /// <summary> float | The maximum UP velocity the kart can have (caps the jump impulse) </summary>
        public static float m_JumpMaxUpVelocity = 30f;
        /// <summary> float | Tollerance for jump before air timer </summary>
        public static float m_JumpBeforeAirTimeTol = 0.2f;
        /// <summary> small / medium / large | Time in air after jumping before we get a boost (SMALL / MED / LARGE) </summary>
        public static float[] m_JumpTimeInAirBoost = new float[] { 0.8f, 1f, 1.5f };
        /// <summary> float | The minimum we must be steering to initiate a slide on landing from air | 0.1 </summary>
        public static float m_SlideStartMinSteer = 0.1f;
        /// <summary> float | The minimum speed we can be going to maintain our slide | 12 </summary>
        public static float m_SlideMinimumSpeed = 8f;
        /// <summary> float | This is the optimum time for boost, any time after will not allow us to boost | 0.95 </summary>
        public static float m_SlideBoostTime = 1f;
        /// <summary> integer | If we exceed this number then no more boosts are given </summary>
        public static int m_SlideMaxBoostCount = 3;
        /// <summary> float | This is the percentage of the TimeStep we reduce the SlideEndCurrTime when no steering is applied </summary>
        public static float m_SlideEndReduceTime = 0.5f;
        /// <summary> float | When our accumulated SlideEndTime reaches +- this figure we end sliding.  Which can end in a spin-out depending on which way our steering ended. </summary>
        public static float m_SlideEndMaxTime = 2f;
        /// <summary> float | Ease in speed for turning (r/sec) (Note: This is on top of m_SlideTurnRateInToSlide or m_SlideTurnRateAwayFromSlide depending on the interpolation direction) </summary>
        public static float m_SlideEaseInSpeed = 180f;
        /// <summary> float | Ease out speed for turning (r/sec) </summary>
        public static float m_SlideEaseOutSpeed = 135f;
        /// <summary> 3 floats | Ease out percentage of angle between VEL and AT (Inner, Neutral, Outer) | 0.5, 0.6, 0.75;0.9,9.8,0.7;0.85,0.8,0.7 (assuming 15 degrees off on return) </summary>
        public static float[] m_SlideEaseOutPercentBetween = new float[] { 0.67f, 0.79f, 0.842f };
        /// <summary> 3 floats | Rotate out speed for velocity rotation          (Inner, Neutral, Outer) | 1, 1, 0.25; 0.05, 0.1, 0.15; 0.108, 0.1, 0.086 </summary>
        public static float[] m_SlideEaseOutRotVelSpeed = new float[] { 0.108f, 0.1f, 0.085f };
        /// <summary> 3 floats | Slide boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) | 1,0,0 </summary>
        public static float[] m_SlideBoostQuadratic = new float[] { 1f, 0f, 0f };
        /// <summary> float | The maximum angle the kart can get from the velocity direction (rad) | 100 </summary>
        public static float m_SlideMaxAngle = 95f;
        /// <summary> float | The minimum angle the kart can get from the velocity direction (rad) | 50 </summary>
        public static float m_SlideMinAngle = 45f;
        /// <summary> float | The speed we turn the kart into the slide (r/sec) </summary>
        public static float m_SlideTurnRateInToSlide = 70f;
        /// <summary> float | The speed we turn the kart away from the slide (r/sec) | 90 </summary>
        public static float m_SlideTurnRateAwayFromSlide = 135f;
        /// <summary> lat / long / lat 2 long | Friction params when we are steering in to the slide </summary>
        public static float[] m_SlideFrictionLow = new float[] { 0, 0, 0 };
        /// <summary> lat / long / lat 2 long | Friction params when we aren't steering | 10, 6, 10 </summary>
        public static float[] m_SlideFrictionNorm = new float[] { 16, 12, 16 };
        /// <summary> lat / long / lat 2 long | Friction param when we are steering away from the slide | 16, 10, 16 </summary>
        public static float[] m_SlideFrictionHigh = new float[] { 19, 13, 19 };
        /// <summary> float | The speed we spin out (rad / sec) } Real amount interpolated </summary>
        public static float m_SpinOutTurnRateMin = 360f;
        /// <summary> float | The speed we spin out (rad / sec) } between these numbers </summary>
        public static float m_SpinOutTurnRateMax = 1080f;
        /// <summary> float | The total amount we normal spin out in radians </summary>
        public static float m_SpinOutTotalNormal = 1080f;
        /// <summary> float | The total amount we large spin out in radians </summary>
        public static float m_SpinOutTotalLarge = 2160f;
        /// <summary> lat / long / lat 2 long | Lat/Long friction applied during SPIN-OUT </summary>
        public static float[] m_SpinOutFriction = new float[] { 6, 6, 0 };
        /// <summary> float | This is the maximum amount of boost we could ever gain in a second | 60, 36 </summary>
        public static float m_BoostMaxImpulsePerSecond = 32f;
        /// <summary> float | The maximum time we can EVER accumulate from boosts (sec) | 10 </summary>
        public static float m_BoostMaxTimeCap = 5f;
        /// <summary> float | The amount of time we FORCE a SLIDE BOOST to be pushed in a direction </summary>
        public static float m_BoostSlidePushTime = 0.3f;
        /// <summary> Inner, Neutral, Outer | The angle that we apply the boost from the KART VELOCITY direction. This is interpolated down to 0 over time | 40; 29,43.5,58; 30,30,30 </summary>
        public static float[] m_BoostSlidePushAngle = new float[] { 45, 70, 95 };
        /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.75  </summary>
        public static float[] m_BoostInfo_eBOOST_JUMP_SMALL = new float[] { 27f, 0.75f, 0.75f };
        /// <summary> speed / time / wheelie | All the BOOST information | 32.13 (134%), 1.0 </summary>
        public static float[] m_BoostInfo_eBOOST_JUMP_MEDIUM = new float[] { 28.74545175f, 1f, 1f };
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.25 </summary>
        public static float[] m_BoostInfo_eBOOST_JUMP_LARGE = new float[] { 31.27272044f, 1.25f, 1.25f };
        /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.5 </summary>
        public static float[] m_BoostInfo_eBOOST_SLIDE_1 = new float[] { 27f, 2f, 0f };
        /// <summary> speed / time / wheelie | All the BOOST information | 30 (125%), 0.5 </summary>
        public static float[] m_BoostInfo_eBOOST_SLIDE_2 = new float[] { 29.09090575f, 2f, 0f };
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 2.0 </summary>
        public static float[] m_BoostInfo_eBOOST_SLIDE_3 = new float[] { 31.27272044f, 2f, 2f };
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
        public static float[] m_BoostInfo_eBOOST_PAD = new float[] { 32f, 1f, 1f };
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
        public static float[] m_BoostInfo_eBOOST_START = new float[] { 31.27272044f, 1.5f, 0f };
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
        public static float[] m_BoostInfo_eBOOST_AKU_DROP = new float[] { 29.09090575f, 1f, 0f };
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
        public static float[] m_BoostInfo_eBOOST_TURBOBOOST = new float[] { 32f, 2f, 1f };
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 3.0 </summary>
        public static float[] m_BoostInfo_eBOOST_TURBOBOOST_JUICED = new float[] { 32f, 3f, 1.5f };
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 0.0 </summary>
        public static float[] m_BoostInfo_eBOOST_SUPER_ENGINE = new float[] { 32f, 1f, 1f };
        /// <summary> float | The tolerance between 'boosts in a row' that we allow (sec) | 1 </summary>
        public static float m_BoostInARowTimeTol = 1f;
        /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time | 0 </summary>
        public static float m_StartLineTS_m_CancelMinPercent = 0f;
        /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2, "0.18, 2", 0.2 </summary>
        public static float m_StartLineTS_m_MaxRepressTime = 0.2f;
        /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | "0.4, 0.36, 0.6", "0.48, 0.43", 0.45 </summary>
        public static float m_StartLineTS_m_MaxHoldTime = 0.42f;
        /// <summary> float | The time we start to decrement the turbo start from | "0.45, 0.56, 0.8","0.68, 0.63", 0.65 </summary>
        public static float m_StartLineTS_m_DecHoldTime = 0.57f;
        /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | "0.2, 0.45, 0.5","0.4, 0.35", 0.35 </summary>
        public static float m_StartLineTS_m_DecSpeed = 0.36f;
        /// <summary> float | The speed we increment the counter at (1.0 / seconds) | "0.5, 0.45, 0.75","0.6, 0.55", 0.6 </summary>
        public static float m_StartLineTS_m_IncSpeed = 0.56f;
        /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) </summary>
        public static float[] m_StartLineTS_m_Quadratic = new float[] { 1, 0, 0 };
        /// <summary> float | When we hit a plane at less than this cos(angle) </summary>
        public static float m_HitStopAngle = 45.57f;
        /// <summary> float | and also at this speed, we will stop </summary>
        public static float m_HitStopSpeed = 10f;
        /// <summary> float | When we are sliding and hit a plane if the cos(angle) is > this then we will drop out of our slide </summary>
        public static float m_HitUpSlideTol = 36.87f;
        /// <summary> float | The minimum speed we must be going before we apply any slowdown force (m/sec) </summary>
        public static float m_HitSlowdownSpeedMin = 10f;
        /// <summary> float | The slowdown force we apply when going forwards (m/sec) </summary>
        public static float m_HitSlowdownSpeedForce = 7f;
        /// <summary> float | The slowdown force we apply when going backwards (m/sec) </summary>
        public static float m_HitSlowdownSpeedForceRev = 0f;
        /// <summary> float | Less or equal Full lateral friction loss we be applied </summary>
        public static float m_HitWallLatMinAng = 35f;
        /// <summary> float | >= No lateral friction loss we be applied </summary>
        public static float m_HitWallLatMaxAng = 90f;
        /// <summary> float | Friction loss (percentage) when at full angle with the wall, this is linearly interpolated from 0 -> value. </summary>
        public static float m_HitWallLatFricLoss = 0.75f;
        /// <summary> min, max | The speed bracket we must be between before we can start a donut | 5, 15 </summary>
        public static float[] m_DonutMinMaxSpeed = new float[] { 5, 15 };
        /// <summary> float | Total amount we turn when we initiate a donut (rad) | 180 </summary>
        public static float m_DonutTurnTotal = 1f;
        /// <summary> float | The maximum turn-rate when in a donut (r/sec) } Similar to the spin-out these </summary>
        public static float m_DonutTurnRateMax = 720f;
        /// <summary> float | The minimum turn-rate when in a donut (r/sec) } are interpolated between </summary>
        public static float m_DonutTurnRateMin = 70f;
        /// <summary> lat / long / lat 2 long | Friction parameter when in a donut </summary>
        public static float[] m_DonutFriction = new float[] { 5, 0, 0 };
        /// <summary> float | The maximum time we stay in the RESET state for </summary>
        public static float m_ResetMaxTime = 2f;
        /// <summary> float | The time we wait before dropping the kart with gravity | 0.45 </summary>
        public static float m_ResetWaitBeforeDrop = 0.3f;
        /// <summary> float | The gravity strength for dropping the kart </summary>
        public static float m_ResetGravStrength = 2f;
        /// <summary> float | Height we need to obtain before getting BIG AIR | 4 </summary>
        public static float m_HeightForBigAir = 10f;
        /// <summary> float | Drag strength per meter </summary>
        public static float m_DragStrength = 0f;
        /// <summary> float | Maximum drag strength </summary>
        public static float m_DragMaxStrength = 0f;
        /// <summary> Max Forward / Accel Gain / Turn Rate | Invincibiliy pickup config (percentage increase on wumpa level, i.e. use 1.1 for gain) | 1.2 </summary>
        public static float[] m_InvincibiliyMaxForwardSpeed = new float[] { 1.25f, 1.25f, 1 };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Squashed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static float[] m_SquashedMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | CTF config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
        public static float[] m_CtfFlagMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Shocked config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static float[] m_ShockedMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Cursed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static float[] m_CursedMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Spikey-Fruit config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static float[] m_SpikeyFruitMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Time Bubble config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
        public static float[] m_TimeBubbleMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> Max Forward / Accel Gain / Turn Rate | Tropy-clocks config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static float[] m_TropyClocksMaxForwardSpeed = new float[] { 0.7f, 0.7f, 1f };
        /// <summary> float | The TOTAL TIME to stay in AKU-DROP state (sec) | 1 </summary>
        public static float m_AkuDropTime = 1f;
        /// <summary> float | The HEIGHT to drop from (m) </summary>
        public static float m_AkuDropHeight = 3f;
        /// <summary> float | The SPEED we drop at (m/sec) | 3 </summary>
        public static float m_AkuDropSpeed = 2f;
        /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time </summary>
        public static float m_AkuDropTS_m_CancelMinPercent = 0f;
        /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2 </summary>
        public static float m_AkuDropTS_m_MaxRepressTime = 0.18f;
        /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | 0.4, 0.36, 0.6 </summary>
        public static float m_AkuDropTS_m_MaxHoldTime = 1f;
        /// <summary> float | The time we start to decrement the turbo start from | 0.45, 0.56, 0.8 </summary>
        public static float m_AkuDropTS_m_DecHoldTime = 1.2f;
        /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | 0.2, 0.45, 0.5 </summary>
        public static float m_AkuDropTS_m_DecSpeed = 0.4f;
        /// <summary> float | The speed we increment the counter at (1.0 / seconds) | 0.5, 0.45, 0.75 </summary>
        public static float m_AkuDropTS_m_IncSpeed = 1f;
        /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!), </summary>
        public static float[] m_AkuDropTS_m_Quadratic = new float[] { 0f, 1f, 0f };
        /// <summary> float | The minimum amount of time spent in a wheelie | 32.4 </summary>
        public static float m_WheelieMinTime = 0.75f;
        /// <summary> float | The minimum percentage we must obtain before we get a slide boost wheelie </summary>
        public static float m_WheelieSlideBoostMinPercent = 0.25f;
        /// <summary> float | This is the lat / long friction when we are hit by a missile | 5 </summary>
        public static float m_HitByMissileFriction = 3.5f;
        /// <summary> float | This is the amount of time to pause before the brake button makes the kart reverse | 0.25 </summary>
        public static float m_WaitBeforeBrakeReverses = 0.225f;

        //Temp values, maybe set them up as a class?
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

        public enum KartPhysicsBaseRows
        { // This enum's comments are from the original CNK files, not made for this tool!
            /// <summary> float | The minimum height we need to get without jumping before we set the in-air state (Note: This is from the center of the collision sphere!!!) </summary>
            m_MinHeightForAirNoJump = 1,
            /// <summary> float | The maximum we will allow our XY velocity to get. | 40 (Trying to limit aberrant behavior.) </summary>
            m_MaxLinearVelXY = 3,
            /// <summary> float | The maximum we will allow our Z velocity to get </summary>
            m_MaxLinearVelZ = 4,
            /// <summary> float | The collision sphere radius for the kart (m) </summary>
            m_CollisionRadius = 6,
            /// <summary> X, Y, Z | The collision sphere offset position from the kart (m) </summary>
            m_CollisionSphereOffset = 7,
            /// <summary> float | The NORMAL maximum FORWARD SPEED of the kart (m/sec) | 27 </summary>
            m_MaxForwardSpeedNormal = 9,
            /// <summary> float | The WUMPA maximum FORWARD SPEED of the kart (m/sec) | 30 </summary>
            m_MaxForwardSpeedWumpa = 10,
            /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
            m_MaxReverseSpeed = 11,
            /// <summary> float | The NORMAL ACCELERATION GAIN of the kart (m/sec) | "18 (2.12s), 20 (1.82s), 22 (1.58s), 24 (1.40s), 26 (1.25s)" | 22 </summary>
            m_AccelerationGainNormal = 12,
            /// <summary> float | The WUMPA ACCELERATION GAIN of the kart (m/sec) | 25 </summary>
            m_AccelerationGainWumpa = 13,
            /// <summary> float | The REVERSE ACCELERATION GAIN of the kart (m/sec) | 37 </summary>
            m_ReverseGain = 14,
            /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
            m_BrakeForce = 15,
            /// <summary> float | Speed to determine when we are in low speed driving model (m/sec) </summary>
            m_LowSpeed = 16,
            /// <summary> float | The amount of GRAVITY when in AIR (x times gravity) </summary>
            m_GravityAir = 17,
            /// <summary> float | The amount of GRAVITY when on GROUND (x times gravity) </summary>
            m_GravityGround = 18,
            /// <summary> float | The amount of DOWNFORCE when in MAGLEV (x times gravity) </summary>
            m_DownforceMagLev = 19,
            /// <summary> float | The amount of DOWNFORCE when in MAGLEV and IN AIR (x times gravity) (Note: This is ONLY applied after we have gained air for m_DownforceMagLevAirTime seconds) | "9, 12, 14" </summary>
            m_DownforceInAirMagLev = 20,
            /// <summary> float | The amount of DOWNFORCE when on GROUND (x times gravity)</summary>
            m_DownforceGround = 21,
            /// <summary> float | The time we allow in AIR before we apply m_DownforceMagLevInAir | 0.3 </summary>
            m_DownforceMagLevAirTime = 22,
            /// <summary> float | The minimum angle that this kicks in (r) </summary>
            m_SlopeMinAngle = 24,
            /// <summary> float | The maximum angle where we achieve full extra (r) </summary>
            m_SlopeMaxAngle = 25,
            /// <summary> float | The acceleration increase (percent) </summary>
            m_SlopeAccelExtra = 26,
            /// <summary> float | The NORMAL kart turn rate (r/sec) | 80 </summary>
            m_TurnRateNormal = 28,
            /// <summary> float | The WUMPA kart turn rate (r/sec) | 80 </summary>
            m_TurnRateWumpa = 29,
            /// <summary> float | The kart additional turn rate when brake is pressed (r/sec) | 90 </summary>
            m_TurnRateBrake = 30,
            /// <summary> float | The kart additional turn rate when accelerator and not brake is pressed (r/sec) </summary>
            m_TurnRateAccel = 31,
            /// <summary> float | The angle from AT.VEL that we consider ourselves to be hard turning (rad) </summary>
            m_HiTurnStartAngle = 33,
            /// <summary> lat / long / lat 2 long | GROUND hi-turn friction parameters | 160 </summary>
            m_HiTurnLatFriction = 34,
            /// <summary> lat / long / lat 2 long | GROUND normal friction parameters | 155 </summary>
            m_NormalFriction = 35,
            /// <summary> float | The speed where turn decelleration effect kicks in (m/sec) </summary>
            m_TurnDecellSpeed = 37,
            /// <summary> float | The turn decell force (m/sec) (Formula is (Speed-m_TurnDecellSpeed) * m_TurnDecellForce) </summary>
            m_TurnDecellForce = 38,
            /// <summary> float | The maximum the turn decelleration force can be (m/sec) </summary>
            m_TurnDecellForceMax = 39,
            /// <summary> float | The NORMAL kart turn rate in air (r/sec) </summary>
            m_InAirTurnRateNormal = 41,
            /// <summary> float | The WUMPAed kart turn rate in air (r/sec) </summary>
            m_InAirTurnRateWumpa = 42,
            /// <summary> float | If we are > this speed then long friction is applied to the kart </summary>
            m_InAirMinSpeed = 43,
            /// <summary> 3 floats | Friction while in air, the long is removed when we are less or equal the above speed </summary>
            m_InAirFriction = 44,
            /// <summary> float | The time in air (without jumping to initiate) we allow the user to still activate a jump | 0.1 </summary>
            m_JumpAirTolerance = 46,
            /// <summary> float | The base impulse for air (hop) | 7 </summary>
            m_JumpImpulseBase = 47,
            /// <summary> float | The base impulse for air (When in MAG-LEV mode) | "10, 12, 14" </summary>
            m_JumpImpulseBaseMagLev = 48,
            /// <summary> float | The minimum UP (z-axis) before we start using it for addition JUMP IMPULSE </summary>
            m_JumpImpulseUpMin = 49,
            /// <summary> float | The maximum UP that we add to the JUMP IMPULSE | "15, 12 ,10" </summary>
            m_JumpImpulseUpMax = 50,
            /// <summary> float | The modifier for the above values | "0.3, 0.25" </summary>
            m_JumpImpulseUpPercent = 51,
            /// <summary> float | The maximum UP velocity the kart can have (caps the jump impulse) </summary>
            m_JumpMaxUpVelocity = 52,
            /// <summary> float | Tollerance for jump before air timer </summary>
            m_JumpBeforeAirTimeTol = 53,
            /// <summary> small / medium / large | Time in air after jumping before we get a boost (SMALL / MED / LARGE) </summary>
            m_JumpTimeInAirBoost = 54,
            /// <summary> float | The minimum we must be steering to initiate a slide on landing from air | 0.1 </summary>
            m_SlideStartMinSteer = 56,
            /// <summary> float | The minimum speed we can be going to maintain our slide | 12 </summary>
            m_SlideMinimumSpeed = 57,
            /// <summary> float | This is the optimum time for boost, any time after will not allow us to boost | 0.95 </summary>
            m_SlideBoostTime = 58,
            /// <summary> float | If we exceed this number then no more boosts are given </summary>
            m_SlideMaxBoostCount = 59,
            /// <summary> float | This is the percentage of the TimeStep we reduce the SlideEndCurrTime when no steering is applied </summary>
            m_SlideEndReduceTime = 60,
            /// <summary> float | When our accumulated SlideEndTime reaches +- this figure we end sliding.  Which can end in a spin-out depending on which way our steering ended. </summary>
            m_SlideEndMaxTime = 61,
            /// <summary> float | Ease in speed for turning (r/sec) (Note: This is on top of m_SlideTurnRateInToSlide or m_SlideTurnRateAwayFromSlide depending on the interpolation direction) </summary>
            m_SlideEaseInSpeed = 62,
            /// <summary> float | Ease out speed for turning (r/sec) </summary>
            m_SlideEaseOutSpeed = 63,
            /// <summary> 3 floats | Ease out percentage of angle between VEL and AT (Inner, Neutral, Outer) | 0.5, 0.6, 0.75;0.9,9.8,0.7;0.85,0.8,0.7 (assuming 15 degrees off on return) </summary>
            m_SlideEaseOutPercentBetween = 64,
            /// <summary> 3 floats | Rotate out speed for velocity rotation          (Inner, Neutral, Outer) | 1, 1, 0.25; 0.05, 0.1, 0.15; 0.108, 0.1, 0.086 </summary>
            m_SlideEaseOutRotVelSpeed = 65,
            /// <summary> 3 floats | Slide boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) | 1,0,0 </summary>
            m_SlideBoostQuadratic = 66,
            /// <summary> float | The maximum angle the kart can get from the velocity direction (rad) | 100 </summary>
            m_SlideMaxAngle = 68,
            /// <summary> float | The minimum angle the kart can get from the velocity direction (rad) | 50 </summary>
            m_SlideMinAngle = 69,
            /// <summary> float | The speed we turn the kart into the slide (r/sec) </summary>
            m_SlideTurnRateInToSlide = 70,
            /// <summary> float | The speed we turn the kart away from the slide (r/sec) | 90 </summary>
            m_SlideTurnRateAwayFromSlide = 71,
            /// <summary> lat / long / lat 2 long | Friction params when we are steering in to the slide </summary>
            m_SlideFrictionLow = 72,
            /// <summary> lat / long / lat 2 long | Friction params when we aren't steering | 10, 6, 10 </summary>
            m_SlideFrictionNorm = 73,
            /// <summary> lat / long / lat 2 long | Friction param when we are steering away from the slide | 16, 10, 16 </summary>
            m_SlideFrictionHigh = 74,
            /// <summary> float | The speed we spin out (rad / sec) } Real amount interpolated </summary>
            m_SpinOutTurnRateMin = 76,
            /// <summary> float | The speed we spin out (rad / sec) } between these numbers </summary>
            m_SpinOutTurnRateMax = 77,
            /// <summary> float | The total amount we normal spin out in radians </summary>
            m_SpinOutTotalNormal = 78,
            /// <summary> float | The total amount we large spin out in radians </summary>
            m_SpinOutTotalLarge = 79,
            /// <summary> lat / long / lat 2 long | Lat/Long friction applied during SPIN-OUT </summary>
            m_SpinOutFriction = 80,
            /// <summary> float | This is the maximum amount of boost we could ever gain in a second | 60, 36 </summary>
            m_BoostMaxImpulsePerSecond = 82,
            /// <summary> float | The maximum time we can EVER accumulate from boosts (sec) | 10 </summary>
            m_BoostMaxTimeCap = 83,
            /// <summary> float | The amount of time we FORCE a SLIDE BOOST to be pushed in a direction </summary>
            m_BoostSlidePushTime = 84,
            /// <summary> Inner, Neutral, Outer | The angle that we apply the boost from the KART VELOCITY direction. This is interpolated down to 0 over time | 40; 29,43.5,58; 30,30,30 </summary>
            m_BoostSlidePushAngle = 85,
            /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.75  </summary>
            m_BoostInfo_eBOOST_JUMP_SMALL = 86,
            /// <summary> speed / time / wheelie | All the BOOST information | 32.13 (134%), 1.0 </summary>
            m_BoostInfo_eBOOST_JUMP_MEDIUM = 87,
            /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.25 </summary>
            m_BoostInfo_eBOOST_JUMP_LARGE = 88,
            /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.5 </summary>
            m_BoostInfo_eBOOST_SLIDE_1 = 89,
            /// <summary> speed / time / wheelie | All the BOOST information | 30 (125%), 0.5 </summary>
            m_BoostInfo_eBOOST_SLIDE_2 = 90,
            /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 2.0 </summary>
            m_BoostInfo_eBOOST_SLIDE_3 = 91,
            /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
            m_BoostInfo_eBOOST_PAD = 92,
            /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
            m_BoostInfo_eBOOST_START = 93,
            /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
            m_BoostInfo_eBOOST_AKU_DROP = 94,
            /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
            m_BoostInfo_eBOOST_TURBOBOOST = 95,
            /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 3.0 </summary>
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED = 96,
            /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 0.0 </summary>
            m_BoostInfo_eBOOST_SUPER_ENGINE = 97,
            /// <summary> float | The tolerance between 'boosts in a row' that we allow (sec) | 1 </summary>
            m_BoostInARowTimeTol = 98,
            /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time | 0 </summary>
            m_StartLineTS_m_CancelMinPercent = 100,
            /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2, "0.18, 2", 0.2 </summary>
            m_StartLineTS_m_MaxRepressTime = 101,
            /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | "0.4, 0.36, 0.6", "0.48, 0.43", 0.45 </summary>
            m_StartLineTS_m_MaxHoldTime = 102,
            /// <summary> float | The time we start to decrement the turbo start from | "0.45, 0.56, 0.8","0.68, 0.63", 0.65 </summary>
            m_StartLineTS_m_DecHoldTime = 103,
            /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | "0.2, 0.45, 0.5","0.4, 0.35", 0.35 </summary>
            m_StartLineTS_m_DecSpeed = 104,
            /// <summary> float | The speed we increment the counter at (1.0 / seconds) | "0.5, 0.45, 0.75","0.6, 0.55", 0.6 </summary>
            m_StartLineTS_m_IncSpeed = 105,
            /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) </summary>
            m_StartLineTS_m_Quadratic = 106,
            /// <summary> float | When we hit a plane at less than this cos(angle) </summary>
            m_HitStopAngle = 108,
            /// <summary> float | and also at this speed, we will stop </summary>
            m_HitStopSpeed = 109,
            /// <summary> float | When we are sliding and hit a plane if the cos(angle) is > this then we will drop out of our slide </summary>
            m_HitUpSlideTol = 110,
            /// <summary> float | The minimum speed we must be going before we apply any slowdown force (m/sec) </summary>
            m_HitSlowdownSpeedMin = 111,
            /// <summary> float | The slowdown force we apply when going forwards (m/sec) </summary>
            m_HitSlowdownSpeedForce = 112,
            /// <summary> float | The slowdown force we apply when going backwards (m/sec) </summary>
            m_HitSlowdownSpeedForceRev = 113,
            /// <summary> float | Less or equal Full lateral friction loss we be applied </summary>
            m_HitWallLatMinAng = 114,
            /// <summary> float | >= No lateral friction loss we be applied </summary>
            m_HitWallLatMaxAng = 115,
            /// <summary> float | Friction loss (percentage) when at full angle with the wall, this is linearly interpolated from 0 -> value. </summary>
            m_HitWallLatFricLoss = 116,
            /// <summary> min, max | The speed bracket we must be between before we can start a donut | 5, 15 </summary>
            m_DonutMinMaxSpeed = 118,
            /// <summary> float | Total amount we turn when we initiate a donut (rad) | 180 </summary>
            m_DonutTurnTotal = 119,
            /// <summary> float | The maximum turn-rate when in a donut (r/sec) } Similar to the spin-out these </summary>
            m_DonutTurnRateMax = 120,
            /// <summary> float | The minimum turn-rate when in a donut (r/sec) } are interpolated between </summary>
            m_DonutTurnRateMin = 121,
            /// <summary> lat / long / lat 2 long | Friction parameter when in a donut </summary>
            m_DonutFriction = 122,
            /// <summary> float | The maximum time we stay in the RESET state for </summary>
            m_ResetMaxTime = 124,
            /// <summary> float | The time we wait before dropping the kart with gravity | 0.45 </summary>
            m_ResetWaitBeforeDrop = 125,
            /// <summary> float | The gravity strength for dropping the kart </summary>
            m_ResetGravStrength = 126,
            /// <summary> float | Height we need to obtain before getting BIG AIR | 4 </summary>
            m_HeightForBigAir = 127,
            /// <summary> float | Drag strength per meter </summary>
            m_DragStrength = 129,
            /// <summary> float | Maximum drag strength </summary>
            m_DragMaxStrength = 130,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Invincibiliy pickup config (percentage increase on wumpa level, i.e. use 1.1 for gain) | 1.2 </summary>
            m_InvincibiliyMaxForwardSpeed = 132,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Squashed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
            m_SquashedMaxForwardSpeed = 133,
            /// <summary> Max Forward / Accel Gain / Turn Rate | CTF config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
            m_CtfFlagMaxForwardSpeed = 134,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Shocked config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
            m_ShockedMaxForwardSpeed = 135,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Cursed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
            m_CursedMaxForwardSpeed = 136,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Spikey-Fruit config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
            m_SpikeyFruitMaxForwardSpeed = 137,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Time Bubble config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
            m_TimeBubbleMaxForwardSpeed = 138,
            /// <summary> Max Forward / Accel Gain / Turn Rate | Tropy-clocks config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
            m_TropyClocksMaxForwardSpeed = 139,
            /// <summary> float | The TOTAL TIME to stay in AKU-DROP state (sec) | 1 </summary>
            m_AkuDropTime = 141,
            /// <summary> float | The HEIGHT to drop from (m) </summary>
            m_AkuDropHeight = 142,
            /// <summary> float | The SPEED we drop at (m/sec) | 3 </summary>
            m_AkuDropSpeed = 143,
            /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time </summary>
            m_AkuDropTS_m_CancelMinPercent = 144,
            /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2 </summary>
            m_AkuDropTS_m_MaxRepressTime = 145,
            /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | 0.4, 0.36, 0.6 </summary>
            m_AkuDropTS_m_MaxHoldTime = 146,
            /// <summary> float | The time we start to decrement the turbo start from | 0.45, 0.56, 0.8 </summary>
            m_AkuDropTS_m_DecHoldTime = 147,
            /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | 0.2, 0.45, 0.5 </summary>
            m_AkuDropTS_m_DecSpeed = 148,
            /// <summary> float | The speed we increment the counter at (1.0 / seconds) | 0.5, 0.45, 0.75 </summary>
            m_AkuDropTS_m_IncSpeed = 149,
            /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!), </summary>
            m_AkuDropTS_m_Quadratic = 150,
            /// <summary> float | The minimum amount of time spent in a wheelie | 32.4 </summary>
            m_WheelieMinTime = 152,
            /// <summary> float | The minimum percentage we must obtain before we get a slide boost wheelie </summary>
            m_WheelieSlideBoostMinPercent = 153,
            /// <summary> float | This is the lat / long friction when we are hit by a missile | 5 </summary>
            m_HitByMissileFriction = 155,
            /// <summary> float | This is the amount of time to pause before the brake button makes the kart reverse | 0.25 </summary>
            m_WaitBeforeBrakeReverses = 157,
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
        public enum Drivers
        {
            Coco = 0,
            Crash = 1,
            Cortex = 2,
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
        }
        public static string[] DriverTypes = new string[] { "coco", "crash", "cortex", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem" };
        public enum PadInfoNameID
        {
            Track_Earth1 = 0,
            Track_Earth2 = 1,
            Track_Earth3 = 2,
            Boss_EarthBoss= 3,
            Arena_EarthArena = 4,
            Track_Barin1 = 5,
            Track_Barin2 = 6,
            Track_Barin3 = 7,
            Boss_BarinBoss = 8,
            Arena_BarinArena = 9,
            Track_Fenomena1 = 10,
            Track_Fenomena2 = 11,
            Track_Fenomena3 = 12,
            Boss_FenomenaBoss = 13,
            Arena_FenomenaArena = 14,
            Track_Teknee1 = 15,
            Track_Teknee2 = 16,
            Track_Teknee3 = 17,
            Boss_TekneeBoss = 18,
            Arena_TekneeArena = 19,
            Boss_VeloBoss = 20,
            Warp_Earth_To_Citadel = 21,
            Warp_Barin_To_Citadel = 22,
            Warp_Fenomena_To_Citadel = 23,
            Warp_Teknee_To_Citadel = 24,
            Warp_Citadel_To_Earth = 25,
            Warp_Barin_To_Earth = 26,
            Warp_Teknee_To_Earth = 27,
            Warp_Citadel_To_Barin = 28,
            Warp_Earth_To_Barin = 29,
            Warp_Fenomena_To_Barin = 30,
            Warp_Citadel_To_Fenomena = 31,
            Warp_Barin_To_Fenomena = 32,
            Warp_Teknee_To_Fenomena = 33,
            Warp_Citadel_To_Teknee = 34,
            Warp_Fenomena_To_Teknee = 35,
            Warp_Earth_To_Teknee = 36,
            Warp_Vault_To_Citadel = 37,
            Warp_Citadel_To_Vault = 38,
            GemCup_Red = 39,
            GemCup_Green = 40,
            GemCup_Purple = 41,
            GemCup_Blue = 42,
            Track_VeloRace = 43,
        }
        public static string[] PadInfoName = new string[]
        {
            "earth1",
            "earth2",
            "earth3",
            "earthboss",
            "eartharena",
            "barin1",
            "barin2",
            "barin3",
            "barinboss",
            "barinarena",
            "fenom1",
            "fenom2",
            "fenom3",
            "fenomboss",
            "fenomarena",
            "tek1",
            "tek2",
            "tek3",
            "tekboss",
            "tekarena",
            "velo",
            "citadela",
            "citadelb",
            "citadelf",
            "citadelt",
            "citadeleb",
            "earthb",
            "eartht",
            "citadelbb",
            "barina",
            "barinf",
            "citadelfb",
            "fenomb",
            "fenomt",
            "citadeltb",
            "tekneea",
            "tekf",
            "gemc",
            "citadelg",
            "redcup",
            "greencup",
            "purplecup",
            "bluecup",
            "velotime",
        };
        public enum SubModeID
        {
            Hub = 0,
            Trophy = 1,
            CNK_Challenge = 2,
            Relic = 3,
            Boss = 4,
            Crystal = 5,
            Gem = 6,
        }
        public static string[] SubModeName = new string[]
        {
            "hub", "trophy", "ctr", "relic", "boss", "crystal", "gem"
        };
        public enum RewardID
        {
            Trophy = 0,
            Key = 1,
            Relic = 2,
            Relic_Sapphire = 2,
            Relic_Gold = 3,
            Relic_Platinum = 4,
            Token_Blue = 5,
            Token_Green = 6,
            Token_Red = 7,
            Token_Purple = 8,
            Token_Yellow = 9,
            Gem_Blue = 10,
            Gem_Green = 11,
            Gem_Red = 12,
            Gem_Purple = 13,
            Gem_Yellow = 14,
        }
        public static string[] RewardName = new string[]
        {
            "trophy", "key", "relic", "relic2", "relic3", "token_blue", "token_green", "token_red", "token_purple", "token_yellow", "gem_blue", "gem_green", "gem_red", "gem_purple", "gem_yellow"
        };
        public enum TrackID
        {
            Earth_1 = 0,
            Earth_2 = 1,
            Earth_3 = 2,
            Arena_1 = 3,
            Barin_1 = 4,
            Barin_2 = 5,
            Barin_3 = 6,
            Arena_2 = 7,
            Fenom_1 = 8,
            Fenom_2 = 9,
            Fenom_3 = 10,
            Arena_3 = 11,
            Teknee_1 = 12,
            Teknee_2 = 13,
            Teknee_3 = 14,
            Arena_4 = 15,
            Arena_5 = 16,
            VeloRace = 17,
            Citadel = 18,
            Hub_1 = 19,
            Hub_2 = 20,
            Hub_3 = 21,
            Hub_4 = 22,
            Secr = 23,
        }
        public static string[] TrackName = new string[]
        {
            "earth1", "earth2", "earth3", "arena1", "barin1", "barin2", "barin3", "arena2", "fenom1", "fenom2", "fenom3", "arena3", "teknee1", "teknee2", "teknee3", "arena4", "arena5", "velorace" , "citadel" , "hub1" , "hub2" , "hub3" , "hub4" , "secr"
        };

        public enum AdventureTracksManagerRows
        {
            Grid_Start_Row = 1,
            Number_Rows = 2,
        }
        public static int Adv_TracksManager_GridStartRow = 15;
        public static int Adv_TracksManager_NumberRows = 58;

        public static List<AdvTracksManagerEntry> Adv_TracksManager_EntryList = new List<AdvTracksManagerEntry>()
        {
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Trophy, RewardID.Trophy, 0),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Trophy, RewardID.Trophy, 0),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Trophy, RewardID.Trophy, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.CNK_Challenge, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Relic, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_EarthBoss, SubModeID.Boss, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_EarthArena, SubModeID.Crystal, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Trophy, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Trophy, RewardID.Trophy, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Trophy, RewardID.Trophy, 5),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.CNK_Challenge, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Relic, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_BarinBoss, SubModeID.Boss, RewardID.Trophy, 6),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_BarinArena, SubModeID.Crystal, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Trophy, RewardID.Trophy, 6),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Trophy, RewardID.Trophy, 7),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Trophy, RewardID.Trophy, 8),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.CNK_Challenge, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Relic, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_FenomenaBoss, SubModeID.Boss, RewardID.Trophy, 9),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_FenomenaArena, SubModeID.Crystal, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Trophy, RewardID.Trophy, 9),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Trophy, RewardID.Trophy, 10),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Trophy, RewardID.Trophy, 11),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.CNK_Challenge, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Relic, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_TekneeBoss, SubModeID.Boss, RewardID.Trophy, 12),
            new AdvTracksManagerEntry(PadInfoNameID.Arena_TekneeArena, SubModeID.Crystal, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Boss_VeloBoss, SubModeID.Boss, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Barin, SubModeID.Trophy, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Barin, SubModeID.Trophy, RewardID.Key, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Fenomena_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Barin_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Teknee, SubModeID.Trophy, RewardID.Key, 3),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Red, SubModeID.Gem, RewardID.Token_Red, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Blue, SubModeID.Gem, RewardID.Token_Blue, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Green, SubModeID.Gem, RewardID.Token_Green, 4),
            new AdvTracksManagerEntry(PadInfoNameID.GemCup_Purple, SubModeID.Gem, RewardID.Token_Purple, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Vault, SubModeID.Trophy, RewardID.Key, 4),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Blue, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Red, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Green, 1),
            new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Purple, 1),
        };

        public static List<GoalsToRewardsEntry> Adv_GoalsToRewards_EntryList = new List<GoalsToRewardsEntry>()
        {
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_1, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Barin_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Barin_3, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Arena_2, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Fenom_1, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Fenom_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Fenom_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_3, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.CNK_Challenge, RewardID.Token_Red),
            new GoalsToRewardsEntry(TrackID.Teknee_1, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.CNK_Challenge, RewardID.Token_Green),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Teknee_2, SubModeID.Boss, RewardID.Key),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.Trophy, RewardID.Trophy),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.CNK_Challenge, RewardID.Token_Blue),
            new GoalsToRewardsEntry(TrackID.Teknee_3, SubModeID.Relic, RewardID.Relic),
            new GoalsToRewardsEntry(TrackID.Arena_4, SubModeID.Crystal, RewardID.Token_Purple),
            new GoalsToRewardsEntry(TrackID.Earth_1, SubModeID.Gem, RewardID.Gem_Red),
            new GoalsToRewardsEntry(TrackID.Earth_2, SubModeID.Gem, RewardID.Gem_Green),
            new GoalsToRewardsEntry(TrackID.Earth_3, SubModeID.Gem, RewardID.Gem_Purple),
            new GoalsToRewardsEntry(TrackID.Barin_1, SubModeID.Gem, RewardID.Gem_Blue),
            new GoalsToRewardsEntry(TrackID.VeloRace, SubModeID.Relic, RewardID.Relic),
        };

        public enum PadInfoEventID
        {
            Null = 0,
            One = 1,
            UsingWarpPad = 2,
            AccessTrack = 3,
            CrystalRequirements = 4,
            WinTrophy = 5,
            WinRelic = 6,
            WinToken = 7,
            CrystalArena = 8,
            WinKey = 9,
            WorldGreeting = 10,
            OpeningWorldGate = 11,
            MultiKeyWorldGate = 12,
            WinGem = 13,
            GemCup = 14,
            GemCupRequirements = 15,
            SecretTracks = 16,
            EarthBossGreeting = 17,
            EarthBossChallenge = 18,
            EarthBossWin = 19,
            BarinBossGreeting = 20,
            BarinBossChallenge = 21,
            BarinBossWin = 22,
            FenomBossGreeting = 23,
            FenomBossChallenge = 24,
            FenomBossWin = 25,
            TekneeBossGreeting = 26,
            TekneeBossChallenge = 27,
            TekneeBossWin = 28,
            VeloChallenge = 29,
            HangTimeBoost = 30,
            PowerSliding = 31,
            SlideBoost = 32,
            SlideBoostCombo = 33,
            BoostCounter = 34,
            ChooseDriver = 35,
            BoostGauge = 36,
            ResetBoost = 37,
            SlowSurfaces = 38,
            StartBoost = 39,
            BowlingBomb = 40,
            TNT = 41,
            BrakeSlide = 42,
            WumpaFruit = 43,
        }
        public static string[] PadInfoEventName =
        {
            "0",
            "1",
            "UsingWarpPad",
            "AccessTrack",
            "CrystalRequirements",
            "WinTrophy",
            "WinRelic",
            "WinToken",
            "CrystalArena",
            "WinKey",
            "WorldGreeting",
            "OpeningWorldGate",
            "MultiKeyWorldGate",
            "WinGem",
            "GemCup",
            "GemCupRequirements",
            "SecretTracks",
            "EarthBossGreeting",
            "EarthBossChallenge",
            "EarthBossWin",
            "BarinBossGreeting",
            "BarinBossChallenge",
            "BarinBossWin",
            "FenomBossGreeting",
            "FenomBossChallenge",
            "FenomBossWin",
            "TekneeBossGreeting",
            "TekneeBossChallenge",
            "TekneeBossWin",
            "VeloChallenge",
            "HangTimeBoost",
            "PowerSliding",
            "SlideBoost",
            "SlideBoostCombo",
            "BoostCounter",
            "ChooseDriver",
            "BoostGauge",
            "ResetBoost",
            "SlowSurfaces",
            "StartBoost",
            "BowlingBomb",
            "TNT",
            "BrakeSlide",
            "WumpaFruit",
        };

        public enum PadInfoDescID
        {
            world_earth1 = 0,
            world_earth2 = 1,
            world_earth3 = 2,
            warp_kongo = 3,
            world_arena1 = 4,
            world_barin1 = 5,
            world_barin2 = 6,
            world_barin3 = 7,
            warp_nash = 8,
            world_arena2 = 9,
            world_fenom1 = 10,
            world_fenom2 = 11,
            world_fenom3 = 12,
            warp_norm = 13,
            world_arena3 = 14,
            world_teknee1 = 15,
            world_teknee2 = 16,
            world_teknee3 = 17,
            warp_otto = 18,
            world_arena4 = 19,
            velo_race_title = 20,
            world_citadel = 21,
            world_adv_hub_earth = 22,
            world_adv_hub_barin = 23,
            world_adv_hub_fenom = 24,
            world_adv_hub_teknee = 25,
            world_adv_hub_gem = 26,
            world_adv_gem_cup_red = 27,
            world_adv_gem_cup_green = 28,
            world_adv_gem_cup_purple = 29,
            world_adv_gem_cup_blue = 30,
            world_velo = 31,
            world_arena5 = 32,
        }
        public static string[] PadInfoDescTypes =
        {
            "world_earth1",
            "world_earth2",
            "world_earth3",
            "warp_kongo",
            "world_arena1",
            "world_barin1",
            "world_barin2",
            "world_barin3",
            "warp_nash",
            "world_arena2",
            "world_fenom1",
            "world_fenom2",
            "world_fenom3",
            "warp_norm",
            "world_arena3",
            "world_teknee1",
            "world_teknee2",
            "world_teknee3",
            "warp_otto",
            "world_arena4",
            "velo_race_title",
            "world_citadel",
            "world_adv_hub_earth",
            "world_adv_hub_barin",
            "world_adv_hub_fenom",
            "world_adv_hub_teknee",
            "world_adv_hub_gem",
            "world_adv_gem_cup_red",
            "world_adv_gem_cup_green",
            "world_adv_gem_cup_purple",
            "world_adv_gem_cup_blue",
            "world_velo",
            "world_arena5",
        };

        public static List<WarpPadInfoEntry> Adv_WarpPadInfo_EntryList = new List<WarpPadInfoEntry>()
        {
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth1,PadInfoDescID.world_earth1,TrackID.Earth_1,PadInfoEventID.Null,PadInfoEventID.UsingWarpPad,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.HangTimeBoost},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth2,PadInfoDescID.world_earth2,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.UsingWarpPad,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.PowerSliding},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Earth3,PadInfoDescID.world_earth3,TrackID.Earth_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlideBoost, PadInfoEventID.EarthBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_EarthBoss,PadInfoDescID.warp_kongo,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.EarthBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.EarthBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_EarthArena,PadInfoDescID.world_arena1,TrackID.Arena_1,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin1,PadInfoDescID.world_barin1,TrackID.Barin_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlideBoostCombo},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin2,PadInfoDescID.world_barin2,TrackID.Barin_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BoostCounter},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Barin3,PadInfoDescID.world_barin3,TrackID.Barin_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.ChooseDriver, PadInfoEventID.BarinBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_BarinBoss,PadInfoDescID.warp_nash,TrackID.Barin_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.BarinBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.BarinBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_BarinArena,PadInfoDescID.world_arena2,TrackID.Arena_2,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken , PadInfoEventID.StartBoost }),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena1,PadInfoDescID.world_fenom1,TrackID.Fenom_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BoostGauge},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena2,PadInfoDescID.world_fenom2,TrackID.Fenom_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.ResetBoost},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Fenomena3,PadInfoDescID.world_fenom3,TrackID.Fenom_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.SlowSurfaces, PadInfoEventID.FenomBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_FenomenaBoss,PadInfoDescID.warp_norm,TrackID.Fenom_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.FenomBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.FenomBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_FenomenaArena,PadInfoDescID.world_arena3,TrackID.Arena_3,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken, PadInfoEventID.BowlingBomb }),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee1,PadInfoDescID.world_teknee1,TrackID.Teknee_1,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.TNT},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee2,PadInfoDescID.world_teknee2,TrackID.Teknee_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.BrakeSlide},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Track_Teknee3,PadInfoDescID.world_teknee3,TrackID.Teknee_3,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.AccessTrack,PadInfoEventID.CrystalRequirements,new PadInfoEventID[] {PadInfoEventID.WinTrophy, PadInfoEventID.WumpaFruit, PadInfoEventID.TekneeBossGreeting},PadInfoEventID.WinRelic,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_TekneeBoss,PadInfoDescID.warp_otto,TrackID.Teknee_2,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.TekneeBossChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinKey, PadInfoEventID.TekneeBossWin},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Arena_TekneeArena,PadInfoDescID.world_arena4,TrackID.Arena_4,PadInfoEventID.Null,PadInfoEventID.CrystalArena,PadInfoEventID.Null,PadInfoEventID.CrystalRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinToken}),
            new WarpPadInfoEntry(PadInfoNameID.Boss_VeloBoss,PadInfoDescID.velo_race_title,TrackID.VeloRace,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.VeloChallenge,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Earth,PadInfoDescID.world_adv_hub_earth,TrackID.Hub_1,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.OpeningWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.OpeningWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Barin,PadInfoDescID.world_adv_hub_barin,TrackID.Hub_2,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Barin_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Teknee_To_Fenomena,PadInfoDescID.world_adv_hub_fenom,TrackID.Hub_3,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Earth_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Fenomena_To_Teknee,PadInfoDescID.world_adv_hub_teknee,TrackID.Hub_4,PadInfoEventID.One,PadInfoEventID.WorldGreeting,PadInfoEventID.Null,PadInfoEventID.MultiKeyWorldGate,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Vault_To_Citadel,PadInfoDescID.world_citadel,TrackID.Citadel,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Warp_Citadel_To_Vault,PadInfoDescID.world_adv_hub_gem,TrackID.Secr,PadInfoEventID.One,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Red,PadInfoDescID.world_adv_gem_cup_red,TrackID.Earth_1,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Green,PadInfoDescID.world_adv_gem_cup_green,TrackID.Earth_2,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Purple,PadInfoDescID.world_adv_gem_cup_purple,TrackID.Earth_3,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.GemCup_Blue,PadInfoDescID.world_adv_gem_cup_blue,TrackID.Barin_1,PadInfoEventID.Null,PadInfoEventID.GemCup,PadInfoEventID.Null,PadInfoEventID.GemCupRequirements,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.WinGem},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
            new WarpPadInfoEntry(PadInfoNameID.Track_VeloRace,PadInfoDescID.world_velo,TrackID.VeloRace,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.Null,PadInfoEventID.SecretTracks,PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null},PadInfoEventID.Null,new PadInfoEventID[] {PadInfoEventID.Null}),
        };


        public static void CNK_Randomize_KartStats(ref Random randState)
        {
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;

            m_AccelerationGainNormal = 26f;
            m_AccelerationGainWumpa = 29.5f;
            m_AkuDropHeight = 3f;
            m_AkuDropSpeed = 2f;
            m_AkuDropTime = 1f;
            m_AkuDropTS_m_CancelMinPercent = 0f;
            m_AkuDropTS_m_DecHoldTime = 1.2f;
            m_AkuDropTS_m_DecSpeed = 0.4f;
            m_AkuDropTS_m_IncSpeed = 1f;
            m_AkuDropTS_m_MaxHoldTime = 0.18f;
            m_AkuDropTS_m_MaxRepressTime = 0.18f;
            m_AkuDropTS_m_Quadratic[0] = 0f;
            m_AkuDropTS_m_Quadratic[1] = 1f;
            m_AkuDropTS_m_Quadratic[2] = 0f;
            m_BoostInARowTimeTol = 1f;
            m_BoostInfo_eBOOST_AKU_DROP[0] = 29.09090575f;
            m_BoostInfo_eBOOST_AKU_DROP[1] = 1f;
            m_BoostInfo_eBOOST_AKU_DROP[2] = 0f;
            m_BoostInfo_eBOOST_JUMP_LARGE[0] = 31.27272044f;
            m_BoostInfo_eBOOST_JUMP_LARGE[1] = 1.25f;
            m_BoostInfo_eBOOST_JUMP_LARGE[2] = 1.25f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM[0] = 28.74545175f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM[1] = 1f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM[2] = 1f;
            m_BoostInfo_eBOOST_JUMP_SMALL[0] = 27f;
            m_BoostInfo_eBOOST_JUMP_SMALL[1] = 0.75f;
            m_BoostInfo_eBOOST_JUMP_SMALL[2] = 0.75f;
            m_BoostInfo_eBOOST_PAD[0] = 32f;
            m_BoostInfo_eBOOST_PAD[1] = 1f;
            m_BoostInfo_eBOOST_PAD[2] = 1f;
            m_BoostInfo_eBOOST_SLIDE_1[0] = 27f;
            m_BoostInfo_eBOOST_SLIDE_1[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_1[2] = 0f;
            m_BoostInfo_eBOOST_SLIDE_2[0] = 29.09090575f;
            m_BoostInfo_eBOOST_SLIDE_2[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_2[2] = 0f;
            m_BoostInfo_eBOOST_SLIDE_3[0] = 31.27272044f;
            m_BoostInfo_eBOOST_SLIDE_3[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_3[2] = 2f;
            m_BoostInfo_eBOOST_START[0] = 31.27272044f;
            m_BoostInfo_eBOOST_START[1] = 1.5f;
            m_BoostInfo_eBOOST_START[2] = 0f;
            m_BoostInfo_eBOOST_SUPER_ENGINE[0] = 32f;
            m_BoostInfo_eBOOST_SUPER_ENGINE[1] = 1f;
            m_BoostInfo_eBOOST_SUPER_ENGINE[2] = 1f;
            m_BoostInfo_eBOOST_TURBOBOOST[0] = 32f;
            m_BoostInfo_eBOOST_TURBOBOOST[1] = 2f;
            m_BoostInfo_eBOOST_TURBOBOOST[2] = 1f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED[0] = 32f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED[1] = 3f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED[2] = 1.5f;
            m_BoostMaxImpulsePerSecond = 32f;
            m_BoostMaxTimeCap = 5f;
            m_BoostSlidePushAngle[0] = 45f;
            m_BoostSlidePushAngle[1] = 70f;
            m_BoostSlidePushAngle[2] = 95f;
            m_BoostSlidePushTime = 0.3f;
            m_BrakeForce = 10f;
            m_CollisionRadius = 0.52f;
            m_CollisionSphereOffset[0] = 0f;
            m_CollisionSphereOffset[1] = 0f;
            m_CollisionSphereOffset[2] = 0.6f;
            m_CtfFlagMaxForwardSpeed[0] = 0.7f;
            m_CtfFlagMaxForwardSpeed[1] = 0.7f;
            m_CtfFlagMaxForwardSpeed[2] = 1f;
            m_CursedMaxForwardSpeed[0] = 0.7f;
            m_CursedMaxForwardSpeed[1] = 1f;
            m_CursedMaxForwardSpeed[2] = 1f;
            m_DonutFriction[0] = 5f;
            m_DonutFriction[1] = 0f;
            m_DonutFriction[2] = 0f;
            m_DonutMinMaxSpeed[0] = 5f;
            m_DonutMinMaxSpeed[1] = 15f;
            m_DonutTurnRateMax = 720f;
            m_DonutTurnRateMin = 70f;
            m_DonutTurnTotal = 1f;
            m_DownforceGround = 2.5f;
            m_DownforceInAirMagLev = 11f;
            m_DownforceMagLev = 4.5f;
            m_DownforceMagLevAirTime = 0.1f;
            m_DragMaxStrength = 0f;
            m_DragStrength = 0f;
            m_GravityAir = 4.5f;
            m_GravityGround = 2f;
            m_HeightForBigAir = 10f;
            m_HitByMissileFriction = 3.5f;
            m_HitSlowdownSpeedForce = 7f;
            m_HitSlowdownSpeedForceRev = 0f;
            m_HitSlowdownSpeedMin = 10f;
            m_HitStopAngle = 45.57f;
            m_HitStopSpeed = 10f;
            m_HitUpSlideTol = 36.87f;
            m_HiTurnLatFriction[0] = 60f;
            m_HiTurnLatFriction[1] = 7f;
            m_HiTurnLatFriction[2] = 0f;
            m_HiTurnStartAngle = 15f;
            m_HitWallLatFricLoss = 0.75f;
            m_HitWallLatMaxAng = 90f;
            m_HitWallLatMinAng = 35f;
            m_InAirFriction[0] = 60f;
            m_InAirFriction[1] = 5f;
            m_InAirFriction[2] = 0f;
            m_InAirMinSpeed = 10f;
            m_InAirTurnRateNormal = 70f;
            m_InAirTurnRateWumpa = 70f;
            m_InvincibiliyMaxForwardSpeed[0] = 1.25f;
            m_InvincibiliyMaxForwardSpeed[1] = 1.25f;
            m_InvincibiliyMaxForwardSpeed[2] = 1f;
            m_JumpAirTolerance = 0.15f;
            m_JumpBeforeAirTimeTol = 0.2f;
            m_JumpImpulseBase = 7.8f;
            m_JumpImpulseBaseMagLev = 14f;
            m_JumpImpulseUpMax = 7.5f;
            m_JumpImpulseUpMin = 0f;
            m_JumpImpulseUpPercent = 0.4f;
            m_JumpMaxUpVelocity = 30f;
            m_JumpTimeInAirBoost[0] = 0.8f;
            m_JumpTimeInAirBoost[1] = 1f;
            m_JumpTimeInAirBoost[2] = 1.5f;
            m_LowSpeed = 12f;
            m_MaxForwardSpeedNormal = 24f;
            m_MaxForwardSpeedWumpa = 27f;
            m_MaxLinearVelXY = 33f;
            m_MaxLinearVelZ = 50f;
            m_MaxReverseSpeed = 10f;
            m_MinHeightForAirNoJump = 1f;
            m_NormalFriction[0] = 55f;
            m_NormalFriction[1] = 7f;
            m_NormalFriction[2] = 0f;
            m_ResetGravStrength = 2f;
            m_ResetMaxTime = 2f;
            m_ResetWaitBeforeDrop = 0.3f;
            m_ReverseGain = 37f;
            m_ShockedMaxForwardSpeed[0] = 0.7f;
            m_ShockedMaxForwardSpeed[1] = 0.7f;
            m_ShockedMaxForwardSpeed[2] = 1f;
            m_SlideBoostQuadratic[0] = 1f;
            m_SlideBoostQuadratic[1] = 0f;
            m_SlideBoostQuadratic[2] = 0f;
            m_SlideBoostTime = 1f;
            m_SlideEaseInSpeed = 180f;
            m_SlideEaseOutPercentBetween[0] = 0.67f;
            m_SlideEaseOutPercentBetween[1] = 0.79f;
            m_SlideEaseOutPercentBetween[2] = 0.842f;
            m_SlideEaseOutRotVelSpeed[0] = 0.108f;
            m_SlideEaseOutRotVelSpeed[1] = 0.1f;
            m_SlideEaseOutRotVelSpeed[2] = 0.085f;
            m_SlideEaseOutSpeed = 135f;
            m_SlideEndMaxTime = 2f;
            m_SlideEndReduceTime = 0.5f;
            m_SlideFrictionHigh[0] = 19f;
            m_SlideFrictionHigh[1] = 13f;
            m_SlideFrictionHigh[2] = 19f;
            m_SlideFrictionLow[0] = 0f;
            m_SlideFrictionLow[1] = 0f;
            m_SlideFrictionLow[2] = 0f;
            m_SlideFrictionNorm[0] = 16f;
            m_SlideFrictionNorm[1] = 12f;
            m_SlideFrictionNorm[2] = 16f;
            m_SlideMaxAngle = 95f;
            m_SlideMaxBoostCount = randState.Next(1,9);
            m_SlideMinAngle = 45f;
            m_SlideMinimumSpeed = 8f;
            m_SlideStartMinSteer= 0.1f;
            m_SlideTurnRateAwayFromSlide = 135f;
            m_SlideTurnRateInToSlide = 70f;
            m_SlopeAccelExtra = 0.5f;
            m_SlopeMaxAngle = 60f;
            m_SlopeMinAngle = 0f;
            m_SpikeyFruitMaxForwardSpeed[0] = 0.7f;
            m_SpikeyFruitMaxForwardSpeed[1] = 0.7f;
            m_SpikeyFruitMaxForwardSpeed[2] = 1f;
            m_SpinOutFriction[0] = 6f;
            m_SpinOutFriction[1] = 6f;
            m_SpinOutFriction[2] = 0f;
            m_SpinOutTotalLarge = 2160f;
            m_SpinOutTotalNormal = 1080f;
            m_SpinOutTurnRateMax = 1080f;
            m_SpinOutTurnRateMin = 360f;
            m_SquashedMaxForwardSpeed[0] = 0.7f;
            m_SquashedMaxForwardSpeed[1] = 0.7f;
            m_SquashedMaxForwardSpeed[2] = 1f;
            m_StartLineTS_m_CancelMinPercent = 0f;
            m_StartLineTS_m_DecHoldTime = 0.57f;
            m_StartLineTS_m_DecSpeed = 0.36f;
            m_StartLineTS_m_IncSpeed = 0.56f;
            m_StartLineTS_m_MaxHoldTime = 0.42f;
            m_StartLineTS_m_MaxRepressTime = 0.2f;
            m_StartLineTS_m_Quadratic[0] = 1f;
            m_StartLineTS_m_Quadratic[1] = 0f;
            m_StartLineTS_m_Quadratic[2] = 0f;
            m_TimeBubbleMaxForwardSpeed[0] = 0.7f;
            m_TimeBubbleMaxForwardSpeed[1] = 0.7f;
            m_TimeBubbleMaxForwardSpeed[2] = 1f;
            m_TropyClocksMaxForwardSpeed[0] = 0.7f;
            m_TropyClocksMaxForwardSpeed[1] = 0.7f;
            m_TropyClocksMaxForwardSpeed[2] = 1f;
            m_TurnDecellForce = 3f;
            m_TurnDecellForceMax = 20f;
            m_TurnDecellSpeed = 12f;
            m_TurnRateAccel = 10f;
            m_TurnRateBrake = 110f;
            m_TurnRateNormal = 70f;
            m_TurnRateWumpa = 70f;
            m_WaitBeforeBrakeReverses = 0.225f;
            m_WheelieMinTime = 0.75f;
            m_WheelieSlideBoostMinPercent = 0.25f;

        }

        public static void CNK_Randomize_CharacterStats(ref Random randState, int targetDriver)
        {
            //float target_val = 0;
            //target_val = randState.NextDouble() + 0.5;

            c_AccelerationGainNormal[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_AccelerationGainWumpa[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_AKU_DROP[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_LARGE[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_MEDIUM[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_JUMP_SMALL[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_PAD[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_1[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_2[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SLIDE_3[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_START[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_START[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_START[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_SUPER_ENGINE[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_BoostInfo_eBOOST_TURBOBOOST_JUICED[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_BoostMaxImpulsePerSecond[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_BoostSlidePushAngle[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_BoostSlidePushTime[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_BrakeForce[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_HiTurnFriction[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_HiTurnFriction[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_HiTurnFriction[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_HiTurnStartAngle[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_InAirTurnRateNormal[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_InAirTurnRateWumpa[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_MaxForwardSpeedNormal[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_MaxForwardSpeedWumpa[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_NormalFriction[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_NormalFriction[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_NormalFriction[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionHigh[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionHigh[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionHigh[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionLow[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionLow[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionLow[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionNorm[targetDriver, 0] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionNorm[targetDriver, 1] = (float)randState.NextDouble() + 0.5f;
            c_SlideFrictionNorm[targetDriver, 2] = (float)randState.NextDouble() + 0.5f;
            c_SlideMaxAngle[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_SlideMinAngle[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_SlideTurnRateAwayFromSlide[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_SlideTurnRateInToSlide[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnDecellForce[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnDecellForceMax[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnDecellSpeed[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnRateAccel[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnRateBrake[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnRateNormal[targetDriver] = (float)randState.NextDouble() + 0.5f;
            c_TurnRateWumpa[targetDriver] = (float)randState.NextDouble() + 0.5f;

            c_UIStats_MaxValue[targetDriver] = 7;
            c_UIStats_Speed[targetDriver] = (int)Math.Ceiling((c_MaxForwardSpeedNormal[targetDriver] / 1.5) * c_UIStats_MaxValue[targetDriver]);
            c_UIStats_Acceleration[targetDriver] = (int)Math.Ceiling((c_AccelerationGainNormal[targetDriver] / 1.5) * c_UIStats_MaxValue[targetDriver]);
            c_UIStats_Turn[targetDriver] = (int)Math.Ceiling((c_TurnRateNormal[targetDriver] / 1.5) * c_UIStats_MaxValue[targetDriver]);
        }

        public static void CNK_Randomize_ReqsRewards()
        {
            Adv_TracksManager_EntryList.Clear();

            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_EarthBoss, SubModeID.Boss, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_EarthArena, SubModeID.Crystal, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_BarinBoss, SubModeID.Boss, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_BarinArena, SubModeID.Crystal, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_FenomenaBoss, SubModeID.Boss, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_FenomenaArena, SubModeID.Crystal, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.CNK_Challenge, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Relic, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_TekneeBoss, SubModeID.Boss, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_TekneeArena, SubModeID.Crystal, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_VeloBoss, SubModeID.Boss, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Barin, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Fenomena, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Teknee, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Barin, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Fenomena_To_Teknee, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Barin_To_Fenomena, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Teknee, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Red, SubModeID.Gem, RewardID.Token_Red, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Blue, SubModeID.Gem, RewardID.Token_Blue, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Green, SubModeID.Gem, RewardID.Token_Green, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Purple, SubModeID.Gem, RewardID.Token_Purple, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Vault, SubModeID.Trophy, RewardID.Key, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Blue, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Red, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Green, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Purple, 0));

            Adv_GoalsToRewards_EntryList.Add(new GoalsToRewardsEntry(TrackID.Arena_5, SubModeID.Crystal, RewardID.Token_Purple)); // Adds unused Terra Drome crystal challenge
            for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
            {
                Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(Adv_GoalsToRewards_EntryList[i].Track, Adv_GoalsToRewards_EntryList[i].Submode, Adv_GoalsToRewards_EntryList[i].Reward);
            }
        }

        public static void CNK_Randomize_WarpPads()
        {
            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, Adv_WarpPadInfo_EntryList[i].Track, Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
            }
        }
    }

    struct AdvTracksManagerEntry
    {
        public CNK_Data.PadInfoNameID PadName;
        public CNK_Data.SubModeID Submode;
        public CNK_Data.RewardID RewardNeeded;
        public int NumberNeeded;

        public AdvTracksManagerEntry(CNK_Data.PadInfoNameID pName, CNK_Data.SubModeID sMode, CNK_Data.RewardID rewNeed, int num)
        {
            PadName = pName;
            Submode = sMode;
            RewardNeeded = rewNeed;
            NumberNeeded = num;
        }
    }
    struct GoalsToRewardsEntry
    {
        public CNK_Data.TrackID Track;
        public CNK_Data.SubModeID Submode;
        public CNK_Data.RewardID Reward;

        public GoalsToRewardsEntry(CNK_Data.TrackID tck, CNK_Data.SubModeID sMode, CNK_Data.RewardID rew)
        {
            Track = tck;
            Submode = sMode;
            Reward = rew;
        }
    }
    struct WarpPadInfoEntry
    {
        public CNK_Data.PadInfoNameID PadName;
        public CNK_Data.PadInfoDescID PadDesc;
        public CNK_Data.TrackID Track;
        public CNK_Data.PadInfoEventID isWarpGate;
        public CNK_Data.PadInfoEventID PrimaryActEvent;
        public CNK_Data.PadInfoEventID SecondaryEvent;
        public CNK_Data.PadInfoEventID LockedEvent;
        public CNK_Data.PadInfoEventID LockedEvent2;
        public CNK_Data.PadInfoEventID[] BaseRewardEvent;
        public CNK_Data.PadInfoEventID RelicWonEvent;
        public CNK_Data.PadInfoEventID[] TokenWonEvent;

        public WarpPadInfoEntry(CNK_Data.PadInfoNameID pName, CNK_Data.PadInfoDescID pDesc, CNK_Data.TrackID tck, CNK_Data.PadInfoEventID WarpGate, CNK_Data.PadInfoEventID PrimEvent, CNK_Data.PadInfoEventID SecEvent, CNK_Data.PadInfoEventID LockEvent, CNK_Data.PadInfoEventID LockEvent2, CNK_Data.PadInfoEventID[] BaseRewEvent, CNK_Data.PadInfoEventID RelicEvent, CNK_Data.PadInfoEventID[] TokenEvent)
        {
            PadName = pName;
            PadDesc = pDesc;
            Track = tck;
            isWarpGate = WarpGate;
            PrimaryActEvent = PrimEvent;
            SecondaryEvent = SecEvent;
            LockedEvent = LockEvent;
            LockedEvent2 = LockEvent2;
            BaseRewardEvent = BaseRewEvent;
            RelicWonEvent = RelicEvent;
            TokenWonEvent = TokenEvent;
        }
    }
}
