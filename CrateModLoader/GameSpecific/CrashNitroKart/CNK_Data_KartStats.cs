using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    static partial class CNK_Data
    {
        public static ModProperty m_AccelerationGainNormal_proptest = new ModProperty();

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


    }
}
