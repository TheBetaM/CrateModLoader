using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    [ModCategory((int)ModProps.KartStats)] [ExecutesMods(typeof(CNK_Rand_KartStats))]
    static class CNK_Data_KartStats
    {

        // These variable comments are from the original CNK files, not made for this tool!
        /// <summary> float | The minimum height we need to get without jumping before we set the in-air state (Note: This is from the center of the collision sphere!!!) </summary>
        public static ModPropFloat m_MinHeightForAirNoJump = new ModPropFloat(1f);
        /// <summary> float | The maximum we will allow our XY velocity to get. | 40 (Trying to limit aberrant behavior.) </summary>
        public static ModPropFloat m_MaxLinearVelXY = new ModPropFloat(33f);
        /// <summary> float | The maximum we will allow our Z velocity to get </summary>
        public static ModPropFloat m_MaxLinearVelZ = new ModPropFloat(50f);
        /// <summary> float | The collision sphere radius for the kart (m) </summary>
        public static ModPropFloat m_CollisionRadius = new ModPropFloat(0.52f);
        /// <summary> X, Y, Z | The collision sphere offset position from the kart (m) </summary>
        public static ModPropFloatArray m_CollisionSphereOffset = new ModPropFloatArray(new float[] { 0, 0, 0.6f });
        /// <summary> float | The NORMAL maximum FORWARD SPEED of the kart (m/sec) | 27 </summary>
        public static ModPropFloat m_MaxForwardSpeedNormal = new ModPropFloat(24f);
        /// <summary> float | The WUMPA maximum FORWARD SPEED of the kart (m/sec) | 30 </summary>
        public static ModPropFloat m_MaxForwardSpeedWumpa = new ModPropFloat(27f);
        /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
        public static ModPropFloat m_MaxReverseSpeed = new ModPropFloat(10f);
        /// <summary> float | The NORMAL ACCELERATION GAIN of the kart (m/sec) | "18 (2.12s), 20 (1.82s), 22 (1.58s), 24 (1.40s), 26 (1.25s)" | 22 </summary>
        public static ModPropFloat m_AccelerationGainNormal = new ModPropFloat(26f);
        /// <summary> float | The WUMPA ACCELERATION GAIN of the kart (m/sec) | 25 </summary>
        public static ModPropFloat m_AccelerationGainWumpa = new ModPropFloat(29.5f);
        /// <summary> float | The REVERSE ACCELERATION GAIN of the kart (m/sec) | 37 </summary>
        public static ModPropFloat m_ReverseGain = new ModPropFloat(37f);
        /// <summary> float | The maximum REVERSE SPEED of the kart (m/sec) </summary>
        public static ModPropFloat m_BrakeForce = new ModPropFloat(10f);
        /// <summary> float | Speed to determine when we are in low speed driving model (m/sec) </summary>
        public static ModPropFloat m_LowSpeed = new ModPropFloat(12f);
        /// <summary> float | The amount of GRAVITY when in AIR (x times gravity) </summary>
        public static ModPropFloat m_GravityAir = new ModPropFloat(4.5f);
        /// <summary> float | The amount of GRAVITY when on GROUND (x times gravity) </summary>
        public static ModPropFloat m_GravityGround = new ModPropFloat(2f);
        /// <summary> float | The amount of DOWNFORCE when in MAGLEV (x times gravity) </summary>
        public static ModPropFloat m_DownforceMagLev = new ModPropFloat(4.5f);
        /// <summary> float | The amount of DOWNFORCE when in MAGLEV and IN AIR (x times gravity) (Note: This is ONLY applied after we have gained air for m_DownforceMagLevAirTime seconds) | "9, 12, 14" </summary>
        public static ModPropFloat m_DownforceInAirMagLev = new ModPropFloat(11f);
        /// <summary> float | The amount of DOWNFORCE when on GROUND (x times gravity)</summary>
        public static ModPropFloat m_DownforceGround = new ModPropFloat(2.5f);
        /// <summary> float | The time we allow in AIR before we apply m_DownforceMagLevInAir | 0.3 </summary>
        public static ModPropFloat m_DownforceMagLevAirTime = new ModPropFloat(0.1f);
        /// <summary> float | The minimum angle that this kicks in (r) </summary>
        public static ModPropFloat m_SlopeMinAngle = new ModPropFloat(0f);
        /// <summary> float | The maximum angle where we achieve full extra (r) </summary>
        public static ModPropFloat m_SlopeMaxAngle = new ModPropFloat(60f);
        /// <summary> float | The acceleration increase (percent) </summary>
        public static ModPropFloat m_SlopeAccelExtra = new ModPropFloat(0.5f);
        /// <summary> float | The NORMAL kart turn rate (r/sec) | 80 </summary>
        public static ModPropFloat m_TurnRateNormal = new ModPropFloat(70f);
        /// <summary> float | The WUMPA kart turn rate (r/sec) | 80 </summary>
        public static ModPropFloat m_TurnRateWumpa = new ModPropFloat(70f);
        /// <summary> float | The kart additional turn rate when brake is pressed (r/sec) | 90 </summary>
        public static ModPropFloat m_TurnRateBrake = new ModPropFloat(110f);
        /// <summary> float | The kart additional turn rate when accelerator and not brake is pressed (r/sec) </summary>
        public static ModPropFloat m_TurnRateAccel = new ModPropFloat(10f);
        /// <summary> float | The angle from AT.VEL that we consider ourselves to be hard turning (rad) </summary>
        public static ModPropFloat m_HiTurnStartAngle = new ModPropFloat(15f);
        /// <summary> lat / long / lat 2 long | GROUND hi-turn friction parameters | 160 </summary>
        public static ModPropFloatArray m_HiTurnLatFriction = new ModPropFloatArray(new float[] { 60f, 7f, 0f });
        /// <summary> lat / long / lat 2 long | GROUND normal friction parameters | 155 </summary>
        public static ModPropFloatArray m_NormalFriction = new ModPropFloatArray(new float[] { 55f, 7f, 0f });
        /// <summary> float | The speed where turn decelleration effect kicks in (m/sec) </summary>
        public static ModPropFloat m_TurnDecellSpeed = new ModPropFloat(12f);
        /// <summary> float | The turn decell force (m/sec) (Formula is (Speed-m_TurnDecellSpeed) * m_TurnDecellForce) </summary>
        public static ModPropFloat m_TurnDecellForce = new ModPropFloat(3f);
        /// <summary> float | The maximum the turn decelleration force can be (m/sec) </summary>
        public static ModPropFloat m_TurnDecellForceMax = new ModPropFloat(20f);
        /// <summary> float | The NORMAL kart turn rate in air (r/sec) </summary>
        public static ModPropFloat m_InAirTurnRateNormal = new ModPropFloat(70f);
        /// <summary> float | The WUMPAed kart turn rate in air (r/sec) </summary>
        public static ModPropFloat m_InAirTurnRateWumpa = new ModPropFloat(70f);
        /// <summary> float | If we are > this speed then long friction is applied to the kart </summary>
        public static ModPropFloat m_InAirMinSpeed = new ModPropFloat(10f);
        /// <summary> 3 floats | Friction while in air, the long is removed when we are less or equal the above speed </summary>
        public static ModPropFloatArray m_InAirFriction = new ModPropFloatArray(new float[] { 60, 5, 0 });
        /// <summary> float | The time in air (without jumping to initiate) we allow the user to still activate a jump | 0.1 </summary>
        public static ModPropFloat m_JumpAirTolerance = new ModPropFloat(0.15f);
        /// <summary> float | The base impulse for air (hop) | 7 </summary>
        public static ModPropFloat m_JumpImpulseBase = new ModPropFloat(7.8f);
        /// <summary> float | The base impulse for air (When in MAG-LEV mode) | "10, 12, 14" </summary>
        public static ModPropFloat m_JumpImpulseBaseMagLev = new ModPropFloat(14f);
        /// <summary> float | The minimum UP (z-axis) before we start using it for addition JUMP IMPULSE </summary>
        public static ModPropFloat m_JumpImpulseUpMin = new ModPropFloat(0f);
        /// <summary> float | The maximum UP that we add to the JUMP IMPULSE | "15, 12 ,10" </summary>
        public static ModPropFloat m_JumpImpulseUpMax = new ModPropFloat(7.5f);
        /// <summary> float | The modifier for the above values | "0.3, 0.25" </summary>
        public static ModPropFloat m_JumpImpulseUpPercent = new ModPropFloat(0.4f);
        /// <summary> float | The maximum UP velocity the kart can have (caps the jump impulse) </summary>
        public static ModPropFloat m_JumpMaxUpVelocity = new ModPropFloat(30f);
        /// <summary> float | Tollerance for jump before air timer </summary>
        public static ModPropFloat m_JumpBeforeAirTimeTol = new ModPropFloat(0.2f);
        /// <summary> small / medium / large | Time in air after jumping before we get a boost (SMALL / MED / LARGE) </summary>
        public static ModPropFloatArray m_JumpTimeInAirBoost = new ModPropFloatArray(new float[] { 0.8f, 1f, 1.5f });
        /// <summary> float | The minimum we must be steering to initiate a slide on landing from air | 0.1 </summary>
        public static ModPropFloat m_SlideStartMinSteer = new ModPropFloat(0.1f);
        /// <summary> float | The minimum speed we can be going to maintain our slide | 12 </summary>
        public static ModPropFloat m_SlideMinimumSpeed = new ModPropFloat(8f);
        /// <summary> float | This is the optimum time for boost, any time after will not allow us to boost | 0.95 </summary>
        public static ModPropFloat m_SlideBoostTime = new ModPropFloat(1f);
        /// <summary> integer | If we exceed this number then no more boosts are given </summary>
        public static ModPropInt m_SlideMaxBoostCount = new ModPropInt(3);
        /// <summary> float | This is the percentage of the TimeStep we reduce the SlideEndCurrTime when no steering is applied </summary>
        public static ModPropFloat m_SlideEndReduceTime = new ModPropFloat(0.5f);
        /// <summary> float | When our accumulated SlideEndTime reaches +- this figure we end sliding.  Which can end in a spin-out depending on which way our steering ended. </summary>
        public static ModPropFloat m_SlideEndMaxTime = new ModPropFloat(2f);
        /// <summary> float | Ease in speed for turning (r/sec) (Note: This is on top of m_SlideTurnRateInToSlide or m_SlideTurnRateAwayFromSlide depending on the interpolation direction) </summary>
        public static ModPropFloat m_SlideEaseInSpeed = new ModPropFloat(180f);
        /// <summary> float | Ease out speed for turning (r/sec) </summary>
        public static ModPropFloat m_SlideEaseOutSpeed = new ModPropFloat(135f);
        /// <summary> 3 floats | Ease out percentage of angle between VEL and AT (Inner, Neutral, Outer) | 0.5, 0.6, 0.75;0.9,9.8,0.7;0.85,0.8,0.7 (assuming 15 degrees off on return) </summary>
        public static ModPropFloatArray m_SlideEaseOutPercentBetween = new ModPropFloatArray(new float[] { 0.67f, 0.79f, 0.842f });
        /// <summary> 3 floats | Rotate out speed for velocity rotation          (Inner, Neutral, Outer) | 1, 1, 0.25; 0.05, 0.1, 0.15; 0.108, 0.1, 0.086 </summary>
        public static ModPropFloatArray m_SlideEaseOutRotVelSpeed = new ModPropFloatArray(new float[] { 0.108f, 0.1f, 0.085f });
        /// <summary> 3 floats | Slide boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) | 1,0,0 </summary>
        public static ModPropFloatArray m_SlideBoostQuadratic = new ModPropFloatArray(new float[] { 1f, 0f, 0f });
        /// <summary> float | The maximum angle the kart can get from the velocity direction (rad) | 100 </summary>
        public static ModPropFloat m_SlideMaxAngle = new ModPropFloat(95f);
        /// <summary> float | The minimum angle the kart can get from the velocity direction (rad) | 50 </summary>
        public static ModPropFloat m_SlideMinAngle = new ModPropFloat(45f);
        /// <summary> float | The speed we turn the kart into the slide (r/sec) </summary>
        public static ModPropFloat m_SlideTurnRateInToSlide = new ModPropFloat(70f);
        /// <summary> float | The speed we turn the kart away from the slide (r/sec) | 90 </summary>
        public static ModPropFloat m_SlideTurnRateAwayFromSlide = new ModPropFloat(135f);
        /// <summary> lat / long / lat 2 long | Friction params when we are steering in to the slide </summary>
        public static ModPropFloatArray m_SlideFrictionLow = new ModPropFloatArray(new float[] { 0, 0, 0 });
        /// <summary> lat / long / lat 2 long | Friction params when we aren't steering | 10, 6, 10 </summary>
        public static ModPropFloatArray m_SlideFrictionNorm = new ModPropFloatArray(new float[] { 16, 12, 16 });
        /// <summary> lat / long / lat 2 long | Friction param when we are steering away from the slide | 16, 10, 16 </summary>
        public static ModPropFloatArray m_SlideFrictionHigh = new ModPropFloatArray(new float[] { 19, 13, 19 });
        /// <summary> float | The speed we spin out (rad / sec) } Real amount interpolated </summary>
        public static ModPropFloat m_SpinOutTurnRateMin = new ModPropFloat(360f);
        /// <summary> float | The speed we spin out (rad / sec) } between these numbers </summary>
        public static ModPropFloat m_SpinOutTurnRateMax = new ModPropFloat(1080f);
        /// <summary> float | The total amount we normal spin out in radians </summary>
        public static ModPropFloat m_SpinOutTotalNormal = new ModPropFloat(1080f);
        /// <summary> float | The total amount we large spin out in radians </summary>
        public static ModPropFloat m_SpinOutTotalLarge = new ModPropFloat(2160f);
        /// <summary> lat / long / lat 2 long | Lat/Long friction applied during SPIN-OUT </summary>
        public static ModPropFloatArray m_SpinOutFriction = new ModPropFloatArray(new float[] { 6, 6, 0 });
        /// <summary> float | This is the maximum amount of boost we could ever gain in a second | 60, 36 </summary>
        public static ModPropFloat m_BoostMaxImpulsePerSecond = new ModPropFloat(32f);
        /// <summary> float | The maximum time we can EVER accumulate from boosts (sec) | 10 </summary>
        public static ModPropFloat m_BoostMaxTimeCap = new ModPropFloat(5f);
        /// <summary> float | The amount of time we FORCE a SLIDE BOOST to be pushed in a direction </summary>
        public static ModPropFloat m_BoostSlidePushTime = new ModPropFloat(0.3f);
        /// <summary> Inner, Neutral, Outer | The angle that we apply the boost from the KART VELOCITY direction. This is interpolated down to 0 over time | 40; 29,43.5,58; 30,30,30 </summary>
        public static ModPropFloatArray m_BoostSlidePushAngle = new ModPropFloatArray(new float[] { 45, 70, 95 });
        /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.75  </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_JUMP_SMALL = new ModPropFloatArray(new float[] { 27f, 0.75f, 0.75f });
        /// <summary> speed / time / wheelie | All the BOOST information | 32.13 (134%), 1.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_JUMP_MEDIUM = new ModPropFloatArray(new float[] { 28.74545175f, 1f, 1f });
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.25 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_JUMP_LARGE = new ModPropFloatArray(new float[] { 31.27272044f, 1.25f, 1.25f });
        /// <summary> speed / time / wheelie | All the BOOST information | 28.56 (119%), 0.5 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_SLIDE_1 = new ModPropFloatArray(new float[] { 27f, 2f, 0f });
        /// <summary> speed / time / wheelie | All the BOOST information | 30 (125%), 0.5 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_SLIDE_2 = new ModPropFloatArray(new float[] { 29.09090575f, 2f, 0f });
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 2.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_SLIDE_3 = new ModPropFloatArray(new float[] { 31.27272044f, 2f, 2f });
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_PAD = new ModPropFloatArray(new float[] { 32f, 1f, 1f });
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_START = new ModPropFloatArray(new float[] { 31.27272044f, 1.5f, 0f });
        /// <summary> speed / time / wheelie | All the BOOST information | 35.7 (149%), 1.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_AKU_DROP = new ModPropFloatArray(new float[] { 29.09090575f, 1f, 0f });
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 2.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_TURBOBOOST = new ModPropFloatArray(new float[] { 32f, 2f, 1f });
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 3.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_TURBOBOOST_JUICED = new ModPropFloatArray(new float[] { 32f, 3f, 1.5f });
        /// <summary> speed / time / wheelie | All the BOOST information | 36.84 (154%), 0.0 </summary>
        public static ModPropFloatArray m_BoostInfo_eBOOST_SUPER_ENGINE = new ModPropFloatArray(new float[] { 32f, 1f, 1f });
        /// <summary> float | The tolerance between 'boosts in a row' that we allow (sec) | 1 </summary>
        public static ModPropFloat m_BoostInARowTimeTol = new ModPropFloat(1f);
        /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time | 0 </summary>
        public static ModPropFloat m_StartLineTS_m_CancelMinPercent = new ModPropFloat(0f);
        /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2, "0.18, 2", 0.2 </summary>
        public static ModPropFloat m_StartLineTS_m_MaxRepressTime = new ModPropFloat(0.2f);
        /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | "0.4, 0.36, 0.6", "0.48, 0.43", 0.45 </summary>
        public static ModPropFloat m_StartLineTS_m_MaxHoldTime = new ModPropFloat(0.42f);
        /// <summary> float | The time we start to decrement the turbo start from | "0.45, 0.56, 0.8","0.68, 0.63", 0.65 </summary>
        public static ModPropFloat m_StartLineTS_m_DecHoldTime = new ModPropFloat(0.57f);
        /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | "0.2, 0.45, 0.5","0.4, 0.35", 0.35 </summary>
        public static ModPropFloat m_StartLineTS_m_DecSpeed = new ModPropFloat(0.36f);
        /// <summary> float | The speed we increment the counter at (1.0 / seconds) | "0.5, 0.45, 0.75","0.6, 0.55", 0.6 </summary>
        public static ModPropFloat m_StartLineTS_m_IncSpeed = new ModPropFloat(0.56f);
        /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!) </summary>
        public static ModPropFloatArray m_StartLineTS_m_Quadratic = new ModPropFloatArray(new float[] { 1, 0, 0 });
        /// <summary> float | When we hit a plane at less than this cos(angle) </summary>
        public static ModPropFloat m_HitStopAngle = new ModPropFloat(45.57f);
        /// <summary> float | and also at this speed, we will stop </summary>
        public static ModPropFloat m_HitStopSpeed = new ModPropFloat(10f);
        /// <summary> float | When we are sliding and hit a plane if the cos(angle) is > this then we will drop out of our slide </summary>
        public static ModPropFloat m_HitUpSlideTol = new ModPropFloat(36.87f);
        /// <summary> float | The minimum speed we must be going before we apply any slowdown force (m/sec) </summary>
        public static ModPropFloat m_HitSlowdownSpeedMin = new ModPropFloat(10f);
        /// <summary> float | The slowdown force we apply when going forwards (m/sec) </summary>
        public static ModPropFloat m_HitSlowdownSpeedForce = new ModPropFloat(7f);
        /// <summary> float | The slowdown force we apply when going backwards (m/sec) </summary>
        public static ModPropFloat m_HitSlowdownSpeedForceRev = new ModPropFloat(0f);
        /// <summary> float | Less or equal Full lateral friction loss we be applied </summary>
        public static ModPropFloat m_HitWallLatMinAng = new ModPropFloat(35f);
        /// <summary> float | >= No lateral friction loss we be applied </summary>
        public static ModPropFloat m_HitWallLatMaxAng = new ModPropFloat(90f);
        /// <summary> float | Friction loss (percentage) when at full angle with the wall, this is linearly interpolated from 0 -> value. </summary>
        public static ModPropFloat m_HitWallLatFricLoss = new ModPropFloat(0.75f);
        /// <summary> min, max | The speed bracket we must be between before we can start a donut | 5, 15 </summary>
        public static ModPropFloatArray m_DonutMinMaxSpeed = new ModPropFloatArray(new float[] { 5, 15 });
        /// <summary> float | Total amount we turn when we initiate a donut (rad) | 180 </summary>
        public static ModPropFloat m_DonutTurnTotal = new ModPropFloat(1f);
        /// <summary> float | The maximum turn-rate when in a donut (r/sec) } Similar to the spin-out these </summary>
        public static ModPropFloat m_DonutTurnRateMax = new ModPropFloat(720f);
        /// <summary> float | The minimum turn-rate when in a donut (r/sec) } are interpolated between </summary>
        public static ModPropFloat m_DonutTurnRateMin = new ModPropFloat(70f);
        /// <summary> lat / long / lat 2 long | Friction parameter when in a donut </summary>
        public static ModPropFloatArray m_DonutFriction = new ModPropFloatArray(new float[] { 5, 0, 0 });
        /// <summary> float | The maximum time we stay in the RESET state for </summary>
        public static ModPropFloat m_ResetMaxTime = new ModPropFloat(2f);
        /// <summary> float | The time we wait before dropping the kart with gravity | 0.45 </summary>
        public static ModPropFloat m_ResetWaitBeforeDrop = new ModPropFloat(0.3f);
        /// <summary> float | The gravity strength for dropping the kart </summary>
        public static ModPropFloat m_ResetGravStrength = new ModPropFloat(2f);
        /// <summary> float | Height we need to obtain before getting BIG AIR | 4 </summary>
        public static ModPropFloat m_HeightForBigAir = new ModPropFloat(10f);
        /// <summary> float | Drag strength per meter </summary>
        public static ModPropFloat m_DragStrength = new ModPropFloat(0f);
        /// <summary> float | Maximum drag strength </summary>
        public static ModPropFloat m_DragMaxStrength = new ModPropFloat(0f);
        /// <summary> Max Forward / Accel Gain / Turn Rate | Invincibiliy pickup config (percentage increase on wumpa level, i.e. use 1.1 for gain) | 1.2 </summary>
        public static ModPropFloatArray m_InvincibiliyMaxForwardSpeed = new ModPropFloatArray(new float[] { 1.25f, 1.25f, 1 });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Squashed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static ModPropFloatArray m_SquashedMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | CTF config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
        public static ModPropFloatArray m_CtfFlagMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Shocked config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static ModPropFloatArray m_ShockedMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Cursed config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static ModPropFloatArray m_CursedMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Spikey-Fruit config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static ModPropFloatArray m_SpikeyFruitMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Time Bubble config (percentage increate on wumpa level, i.e. use 0.9 for loss) </summary>
        public static ModPropFloatArray m_TimeBubbleMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> Max Forward / Accel Gain / Turn Rate | Tropy-clocks config (percentage increate on wumpa level, i.e. use 0.9 for loss) | 0.7 </summary>
        public static ModPropFloatArray m_TropyClocksMaxForwardSpeed = new ModPropFloatArray(new float[] { 0.7f, 0.7f, 1f });
        /// <summary> float | The TOTAL TIME to stay in AKU-DROP state (sec) | 1 </summary>
        public static ModPropFloat m_AkuDropTime = new ModPropFloat(1f);
        /// <summary> float | The HEIGHT to drop from (m) </summary>
        public static ModPropFloat m_AkuDropHeight = new ModPropFloat(3f);
        /// <summary> float | The SPEED we drop at (m/sec) | 3 </summary>
        public static ModPropFloat m_AkuDropSpeed = new ModPropFloat(2f);
        /// <summary> float | While we are less than this percent the we don't enforce wait time for repress and also clear the hold time </summary>
        public static ModPropFloat m_AkuDropTS_m_CancelMinPercent = new ModPropFloat(0f);
        /// <summary> float | The time we wait between presses / when we run out from on press before a new press can take effect | 0.2 </summary>
        public static ModPropFloat m_AkuDropTS_m_MaxRepressTime = new ModPropFloat(0.18f);
        /// <summary> float | The time we can hold the accelerator for before we stop increasing the counter | 0.4, 0.36, 0.6 </summary>
        public static ModPropFloat m_AkuDropTS_m_MaxHoldTime = new ModPropFloat(1f);
        /// <summary> float | The time we start to decrement the turbo start from | 0.45, 0.56, 0.8 </summary>
        public static ModPropFloat m_AkuDropTS_m_DecHoldTime = new ModPropFloat(1.2f);
        /// <summary> float | The speed we decrement the counter at (1.0 / seconds) | 0.2, 0.45, 0.5 </summary>
        public static ModPropFloat m_AkuDropTS_m_DecSpeed = new ModPropFloat(0.4f);
        /// <summary> float | The speed we increment the counter at (1.0 / seconds) | 0.5, 0.45, 0.75 </summary>
        public static ModPropFloat m_AkuDropTS_m_IncSpeed = new ModPropFloat(1f);
        /// <summary> 3 floats | Turbo start boost quadratic inputs (Note: You MUST make sure that invalid values are not entered here!), </summary>
        public static ModPropFloatArray m_AkuDropTS_m_Quadratic = new ModPropFloatArray(new float[] { 0f, 1f, 0f });
        /// <summary> float | The minimum amount of time spent in a wheelie | 32.4 </summary>
        public static ModPropFloat m_WheelieMinTime = new ModPropFloat(0.75f);
        /// <summary> float | The minimum percentage we must obtain before we get a slide boost wheelie </summary>
        public static ModPropFloat m_WheelieSlideBoostMinPercent = new ModPropFloat(0.25f);
        /// <summary> float | This is the lat / long friction when we are hit by a missile | 5 </summary>
        public static ModPropFloat m_HitByMissileFriction = new ModPropFloat(3.5f);
        /// <summary> float | This is the amount of time to pause before the brake button makes the kart reverse | 0.25 </summary>
        public static ModPropFloat m_WaitBeforeBrakeReverses = new ModPropFloat(0.225f);
    }
}
