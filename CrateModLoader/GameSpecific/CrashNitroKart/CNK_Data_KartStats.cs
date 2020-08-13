using System;
using CrateModLoader.ModProperties;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    [ModCategory((int)ModProps.KartStats)]
    static class CNK_Data_KartStats
    {

        //public static ModPropString m_Accel_proptest = new ModPropString("test");
        //public static ModPropBool m_Accel_booltest = new ModPropBool(true);
        //public static ModPropFloat m_Accel_floattest = new ModPropFloat(1.34f);
        //public static ModPropFloatArray m_Accel_floatatest = new ModPropFloatArray(new float[] { 2.5f , 1.22f, 313.177f });

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

        public static void CNK_Randomize_KartStats(Random randState)
        {
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
            //todo

            //Accel
            m_AccelerationGainNormal.Value = randState.Next(24, 29) + (float)randState.NextDouble(); // 26f;
            m_AccelerationGainWumpa.Value = m_AccelerationGainNormal.Value + 3.5f;

            //Aku Respawn
            m_AkuDropHeight.Value = 3f;
            m_AkuDropSpeed.Value = 2f;
            m_AkuDropTime.Value = 1f;
            m_AkuDropTS_m_CancelMinPercent.Value = 0f;
            m_AkuDropTS_m_DecHoldTime.Value = 1.2f;
            m_AkuDropTS_m_DecSpeed.Value = 0.4f;
            m_AkuDropTS_m_IncSpeed.Value = 1f;
            m_AkuDropTS_m_MaxHoldTime.Value = 1f;
            m_AkuDropTS_m_MaxRepressTime.Value = 0.18f;
            m_AkuDropTS_m_Quadratic.Value[0] = 0f;
            m_AkuDropTS_m_Quadratic.Value[1] = 1f;
            m_AkuDropTS_m_Quadratic.Value[2] = 0f;

            m_BoostInARowTimeTol.Value = 0.2f; //Uncapped reserves

            //Boost sources speed, length
            m_BoostInfo_eBOOST_AKU_DROP.Value[0] = 29.09090575f;
            m_BoostInfo_eBOOST_AKU_DROP.Value[1] = 1f;
            m_BoostInfo_eBOOST_AKU_DROP.Value[2] = 0f;
            m_BoostInfo_eBOOST_JUMP_LARGE.Value[0] = 31.27272044f;
            m_BoostInfo_eBOOST_JUMP_LARGE.Value[1] = 1.25f;
            m_BoostInfo_eBOOST_JUMP_LARGE.Value[2] = 1.25f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[0] = 28.74545175f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[1] = 1f;
            m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[2] = 1f;
            m_BoostInfo_eBOOST_JUMP_SMALL.Value[0] = 27f;
            m_BoostInfo_eBOOST_JUMP_SMALL.Value[1] = 0.75f;
            m_BoostInfo_eBOOST_JUMP_SMALL.Value[2] = 0.75f;
            m_BoostInfo_eBOOST_PAD.Value[0] = 32f;
            m_BoostInfo_eBOOST_PAD.Value[1] = 1f;
            m_BoostInfo_eBOOST_PAD.Value[2] = 1f;
            m_BoostInfo_eBOOST_SLIDE_1.Value[0] = 27f;
            m_BoostInfo_eBOOST_SLIDE_1.Value[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_1.Value[2] = 0f;
            m_BoostInfo_eBOOST_SLIDE_2.Value[0] = 29.09090575f;
            m_BoostInfo_eBOOST_SLIDE_2.Value[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_2.Value[2] = 0f;
            m_BoostInfo_eBOOST_SLIDE_3.Value[0] = 31.27272044f;
            m_BoostInfo_eBOOST_SLIDE_3.Value[1] = 2f;
            m_BoostInfo_eBOOST_SLIDE_3.Value[2] = 2f;
            m_BoostInfo_eBOOST_START.Value[0] = 31.27272044f;
            m_BoostInfo_eBOOST_START.Value[1] = 1.5f;
            m_BoostInfo_eBOOST_START.Value[2] = 0f;
            m_BoostInfo_eBOOST_SUPER_ENGINE.Value[0] = 32f;
            m_BoostInfo_eBOOST_SUPER_ENGINE.Value[1] = 1f;
            m_BoostInfo_eBOOST_SUPER_ENGINE.Value[2] = 1f;
            m_BoostInfo_eBOOST_TURBOBOOST.Value[0] = 32f;
            m_BoostInfo_eBOOST_TURBOBOOST.Value[1] = 2f;
            m_BoostInfo_eBOOST_TURBOBOOST.Value[2] = 1f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[0] = 32f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[1] = 3f;
            m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[2] = 1.5f;

            m_BoostMaxImpulsePerSecond.Value = 120f; //Uncapped reserves
            m_BoostMaxTimeCap.Value = 999f; //Uncapped reserves

            m_BoostSlidePushAngle.Value[0] = 45f;
            m_BoostSlidePushAngle.Value[1] = 70f;
            m_BoostSlidePushAngle.Value[2] = 95f;

            m_BoostSlidePushTime.Value = 0.3f;

            m_BrakeForce.Value = 10f;

            m_CollisionRadius.Value = 0.52f;
            m_CollisionSphereOffset.Value[0] = 0f;
            m_CollisionSphereOffset.Value[1] = 0f;
            m_CollisionSphereOffset.Value[2] = 0.6f;

            m_CtfFlagMaxForwardSpeed.Value[0] = 0.7f;
            m_CtfFlagMaxForwardSpeed.Value[1] = 0.7f;
            m_CtfFlagMaxForwardSpeed.Value[2] = 1f;

            m_CursedMaxForwardSpeed.Value[0] = 0.7f;
            m_CursedMaxForwardSpeed.Value[1] = 1f;
            m_CursedMaxForwardSpeed.Value[2] = 1f;

            m_DonutFriction.Value[0] = 5f;
            m_DonutFriction.Value[1] = 0f;
            m_DonutFriction.Value[2] = 0f;
            m_DonutMinMaxSpeed.Value[0] = 5f;
            m_DonutMinMaxSpeed.Value[1] = 15f;
            m_DonutTurnRateMax.Value = 720f;
            m_DonutTurnRateMin.Value = 70f;
            m_DonutTurnTotal.Value = 1f;

            m_DownforceGround.Value = 2.5f;
            m_DownforceInAirMagLev.Value = 11f;
            m_DownforceMagLev.Value = 4.5f;
            m_DownforceMagLevAirTime.Value = 0.1f;

            m_DragMaxStrength.Value = 0f;
            m_DragStrength.Value = 0f;

            m_GravityAir.Value = 4.5f;
            m_GravityGround.Value = 2f;

            m_HeightForBigAir.Value = 10f;

            m_HitByMissileFriction.Value = 3.5f;
            m_HitSlowdownSpeedForce.Value = 7f;
            m_HitSlowdownSpeedForceRev.Value = 0f;
            m_HitSlowdownSpeedMin.Value = 10f;
            m_HitStopAngle.Value = 45.57f;
            m_HitStopSpeed.Value = 10f;
            m_HitUpSlideTol.Value = 36.87f;

            m_HiTurnLatFriction.Value[0] = 60f;
            m_HiTurnLatFriction.Value[1] = 7f;
            m_HiTurnLatFriction.Value[2] = 0f;
            m_HiTurnStartAngle.Value = 15f;

            m_HitWallLatFricLoss.Value = 0.75f;
            m_HitWallLatMaxAng.Value = 90f;
            m_HitWallLatMinAng.Value = 35f;

            m_InAirFriction.Value[0] = 60f;
            m_InAirFriction.Value[1] = 5f;
            m_InAirFriction.Value[2] = 0f;
            m_InAirMinSpeed.Value = 10f;
            m_InAirTurnRateNormal.Value = 70f;
            m_InAirTurnRateWumpa.Value = 70f;

            m_InvincibiliyMaxForwardSpeed.Value[0] = 1.25f;
            m_InvincibiliyMaxForwardSpeed.Value[1] = 1.25f;
            m_InvincibiliyMaxForwardSpeed.Value[2] = 1f;

            m_JumpAirTolerance.Value = 0.15f;
            m_JumpBeforeAirTimeTol.Value = 0.2f;
            m_JumpImpulseBase.Value = 7.8f;
            m_JumpImpulseBaseMagLev.Value = 14f;
            m_JumpImpulseUpMax.Value = 7.5f;
            m_JumpImpulseUpMin.Value = 0f;
            m_JumpImpulseUpPercent.Value = 0.4f;
            m_JumpMaxUpVelocity.Value = 30f;
            m_JumpTimeInAirBoost.Value[0] = 0.8f;
            m_JumpTimeInAirBoost.Value[1] = 1f;
            m_JumpTimeInAirBoost.Value[2] = 1.5f;

            m_LowSpeed.Value = 12f;

            m_MaxForwardSpeedNormal.Value = randState.Next(23, 31) + (float)randState.NextDouble();
            m_MaxForwardSpeedWumpa.Value = m_MaxForwardSpeedNormal.Value + 3f;

            m_MaxLinearVelXY.Value = 52f; // Extended speed cap, too high causes physics instability
            m_MaxLinearVelZ.Value = 52f;

            m_MaxReverseSpeed.Value = randState.Next(8, 15) + (float)randState.NextDouble();// 10f;

            m_MinHeightForAirNoJump.Value = 1f;

            m_NormalFriction.Value[0] = 55f;
            m_NormalFriction.Value[1] = 7f;
            m_NormalFriction.Value[2] = 0f;

            m_ResetGravStrength.Value = 2f;
            m_ResetMaxTime.Value = 2f;
            m_ResetWaitBeforeDrop.Value = 0.3f;

            m_ReverseGain.Value = 37f;

            m_ShockedMaxForwardSpeed.Value[0] = 0.7f;
            m_ShockedMaxForwardSpeed.Value[1] = 0.7f;
            m_ShockedMaxForwardSpeed.Value[2] = 1f;

            m_SlideBoostQuadratic.Value[0] = 1f;
            m_SlideBoostQuadratic.Value[1] = 0f;
            m_SlideBoostQuadratic.Value[2] = 0f;
            m_SlideBoostTime.Value = 1f;
            m_SlideEaseInSpeed.Value = 180f;
            m_SlideEaseOutPercentBetween.Value[0] = 0.67f;
            m_SlideEaseOutPercentBetween.Value[1] = 0.79f;
            m_SlideEaseOutPercentBetween.Value[2] = 0.842f;
            m_SlideEaseOutRotVelSpeed.Value[0] = 0.108f;
            m_SlideEaseOutRotVelSpeed.Value[1] = 0.1f;
            m_SlideEaseOutRotVelSpeed.Value[2] = 0.085f;
            m_SlideEaseOutSpeed.Value = 135f;
            m_SlideEndMaxTime.Value = 2f;
            m_SlideEndReduceTime.Value = 0.5f;
            m_SlideFrictionHigh.Value[0] = 19f;
            m_SlideFrictionHigh.Value[1] = 13f;
            m_SlideFrictionHigh.Value[2] = 19f;
            m_SlideFrictionLow.Value[0] = 0f;
            m_SlideFrictionLow.Value[1] = 0f;
            m_SlideFrictionLow.Value[2] = 0f;
            m_SlideFrictionNorm.Value[0] = 16f;
            m_SlideFrictionNorm.Value[1] = 12f;
            m_SlideFrictionNorm.Value[2] = 16f;
            m_SlideMaxAngle.Value = 95f;
            m_SlideMaxBoostCount.Value = 3; // If you go over 3 boosts it only affects the boost meter, doesn't let you boost more
            m_SlideMinAngle.Value = 45f;
            m_SlideMinimumSpeed.Value = 8f;
            m_SlideStartMinSteer.Value = 0.1f;
            m_SlideTurnRateAwayFromSlide.Value = 135f;
            m_SlideTurnRateInToSlide.Value = 70f;

            m_SlopeAccelExtra.Value = 0.5f;
            m_SlopeMaxAngle.Value = 60f;
            m_SlopeMinAngle.Value = 0f;

            m_SpikeyFruitMaxForwardSpeed.Value[0] = 0.7f;
            m_SpikeyFruitMaxForwardSpeed.Value[1] = 0.7f;
            m_SpikeyFruitMaxForwardSpeed.Value[2] = 1f;

            m_SpinOutFriction.Value[0] = 6f;
            m_SpinOutFriction.Value[1] = 6f;
            m_SpinOutFriction.Value[2] = 0f;
            m_SpinOutTotalLarge.Value = 2160f;
            m_SpinOutTotalNormal.Value = 1080f;
            m_SpinOutTurnRateMax.Value = 1080f;
            m_SpinOutTurnRateMin.Value = 360f;

            m_SquashedMaxForwardSpeed.Value[0] = 0.7f;
            m_SquashedMaxForwardSpeed.Value[1] = 0.7f;
            m_SquashedMaxForwardSpeed.Value[2] = 1f;

            m_StartLineTS_m_CancelMinPercent.Value = 0f;
            m_StartLineTS_m_DecHoldTime.Value = 0.57f;
            m_StartLineTS_m_DecSpeed.Value = 0.36f;
            m_StartLineTS_m_IncSpeed.Value = 0.56f;
            m_StartLineTS_m_MaxHoldTime.Value = 0.42f;
            m_StartLineTS_m_MaxRepressTime.Value = 0.2f;
            m_StartLineTS_m_Quadratic.Value[0] = 1f;
            m_StartLineTS_m_Quadratic.Value[1] = 0f;
            m_StartLineTS_m_Quadratic.Value[2] = 0f;

            m_TimeBubbleMaxForwardSpeed.Value[0] = 0.7f;
            m_TimeBubbleMaxForwardSpeed.Value[1] = 0.7f;
            m_TimeBubbleMaxForwardSpeed.Value[2] = 1f;

            m_TropyClocksMaxForwardSpeed.Value[0] = 0.7f;
            m_TropyClocksMaxForwardSpeed.Value[1] = 0.7f;
            m_TropyClocksMaxForwardSpeed.Value[2] = 1f;

            m_TurnDecellForce.Value = 3f;
            m_TurnDecellForceMax.Value = 20f;
            m_TurnDecellSpeed.Value = 12f;

            m_TurnRateAccel.Value = 10f;
            m_TurnRateBrake.Value = 110f;
            m_TurnRateNormal.Value = randState.Next(60, 80);// 70f;
            m_TurnRateWumpa = m_TurnRateNormal;

            m_WaitBeforeBrakeReverses.Value = 0.225f;

            m_WheelieMinTime.Value = 0.75f;
            m_WheelieSlideBoostMinPercent.Value = 0.25f;

        }

    }

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
}
