using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_KartStats : ModStruct<string>
    {
        public override string Name => CNK_Text.Rand_KartStats;
        public override string Description => CNK_Text.Rand_KartStatsDesc;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);

            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
            //todo

            //Accel
            CNK_Data_KartStats.m_AccelerationGainNormal.Value = randState.Next(24, 29) + (float)randState.NextDouble(); // 26f;
            CNK_Data_KartStats.m_AccelerationGainWumpa.Value = CNK_Data_KartStats.m_AccelerationGainNormal.Value + 3.5f;

            //Aku Respawn
            CNK_Data_KartStats.m_AkuDropHeight.Value = 3f;
            CNK_Data_KartStats.m_AkuDropSpeed.Value = 2f;
            CNK_Data_KartStats.m_AkuDropTime.Value = 1f;
            CNK_Data_KartStats.m_AkuDropTS_m_CancelMinPercent.Value = 0f;
            CNK_Data_KartStats.m_AkuDropTS_m_DecHoldTime.Value = 1.2f;
            CNK_Data_KartStats.m_AkuDropTS_m_DecSpeed.Value = 0.4f;
            CNK_Data_KartStats.m_AkuDropTS_m_IncSpeed.Value = 1f;
            CNK_Data_KartStats.m_AkuDropTS_m_MaxHoldTime.Value = 1f;
            CNK_Data_KartStats.m_AkuDropTS_m_MaxRepressTime.Value = 0.18f;
            CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value[0] = 0f;
            CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value[1] = 1f;
            CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value[2] = 0f;

            CNK_Data_KartStats.m_BoostInARowTimeTol.Value = 0.2f; //Uncapped reserves

            //Boost sources speed, length
            CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value[0] = 29.09090575f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value[1] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value[2] = 0f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value[0] = 31.27272044f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value[1] = 1.25f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value[2] = 1.25f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[0] = 28.74545175f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[1] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value[2] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value[0] = 27f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value[1] = 0.75f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value[2] = 0.75f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value[0] = 32f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value[1] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value[2] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value[0] = 27f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value[1] = 2f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value[2] = 0f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value[0] = 29.09090575f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value[1] = 2f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value[2] = 0f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value[0] = 31.27272044f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value[1] = 2f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value[2] = 2f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value[0] = 31.27272044f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value[1] = 1.5f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value[2] = 0f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value[0] = 32f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value[1] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value[2] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value[0] = 32f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value[1] = 2f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value[2] = 1f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[0] = 32f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[1] = 3f;
            CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[2] = 1.5f;

            CNK_Data_KartStats.m_BoostMaxImpulsePerSecond.Value = 120f; //Uncapped reserves
            CNK_Data_KartStats.m_BoostMaxTimeCap.Value = 999f; //Uncapped reserves

            CNK_Data_KartStats.m_BoostSlidePushAngle.Value[0] = 45f;
            CNK_Data_KartStats.m_BoostSlidePushAngle.Value[1] = 70f;
            CNK_Data_KartStats.m_BoostSlidePushAngle.Value[2] = 95f;

            CNK_Data_KartStats.m_BoostSlidePushTime.Value = 0.3f;

            CNK_Data_KartStats.m_BrakeForce.Value = 10f;

            CNK_Data_KartStats.m_CollisionRadius.Value = 0.52f;
            CNK_Data_KartStats.m_CollisionSphereOffset.Value[0] = 0f;
            CNK_Data_KartStats.m_CollisionSphereOffset.Value[1] = 0f;
            CNK_Data_KartStats.m_CollisionSphereOffset.Value[2] = 0.6f;

            CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value[1] = 1f;
            CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_DonutFriction.Value[0] = 5f;
            CNK_Data_KartStats.m_DonutFriction.Value[1] = 0f;
            CNK_Data_KartStats.m_DonutFriction.Value[2] = 0f;
            CNK_Data_KartStats.m_DonutMinMaxSpeed.Value[0] = 5f;
            CNK_Data_KartStats.m_DonutMinMaxSpeed.Value[1] = 15f;
            CNK_Data_KartStats.m_DonutTurnRateMax.Value = 720f;
            CNK_Data_KartStats.m_DonutTurnRateMin.Value = 70f;
            CNK_Data_KartStats.m_DonutTurnTotal.Value = 1f;

            CNK_Data_KartStats.m_DownforceGround.Value = 2.5f;
            CNK_Data_KartStats.m_DownforceInAirMagLev.Value = 11f;
            CNK_Data_KartStats.m_DownforceMagLev.Value = 4.5f;
            CNK_Data_KartStats.m_DownforceMagLevAirTime.Value = 0.1f;

            CNK_Data_KartStats.m_DragMaxStrength.Value = 0f;
            CNK_Data_KartStats.m_DragStrength.Value = 0f;

            CNK_Data_KartStats.m_GravityAir.Value = 4.5f;
            CNK_Data_KartStats.m_GravityGround.Value = 2f;

            CNK_Data_KartStats.m_HeightForBigAir.Value = 10f;

            CNK_Data_KartStats.m_HitByMissileFriction.Value = 3.5f;
            CNK_Data_KartStats.m_HitSlowdownSpeedForce.Value = 7f;
            CNK_Data_KartStats.m_HitSlowdownSpeedForceRev.Value = 0f;
            CNK_Data_KartStats.m_HitSlowdownSpeedMin.Value = 10f;
            CNK_Data_KartStats.m_HitStopAngle.Value = 45.57f;
            CNK_Data_KartStats.m_HitStopSpeed.Value = 10f;
            CNK_Data_KartStats.m_HitUpSlideTol.Value = 36.87f;

            CNK_Data_KartStats.m_HiTurnLatFriction.Value[0] = 60f;
            CNK_Data_KartStats.m_HiTurnLatFriction.Value[1] = 7f;
            CNK_Data_KartStats.m_HiTurnLatFriction.Value[2] = 0f;
            CNK_Data_KartStats.m_HiTurnStartAngle.Value = 15f;

            CNK_Data_KartStats.m_HitWallLatFricLoss.Value = 0.75f;
            CNK_Data_KartStats.m_HitWallLatMaxAng.Value = 90f;
            CNK_Data_KartStats.m_HitWallLatMinAng.Value = 35f;

            CNK_Data_KartStats.m_InAirFriction.Value[0] = 60f;
            CNK_Data_KartStats.m_InAirFriction.Value[1] = 5f;
            CNK_Data_KartStats.m_InAirFriction.Value[2] = 0f;
            CNK_Data_KartStats.m_InAirMinSpeed.Value = 10f;
            CNK_Data_KartStats.m_InAirTurnRateNormal.Value = 70f;
            CNK_Data_KartStats.m_InAirTurnRateWumpa.Value = 70f;

            CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value[0] = 1.25f;
            CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value[1] = 1.25f;
            CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_JumpAirTolerance.Value = 0.15f;
            CNK_Data_KartStats.m_JumpBeforeAirTimeTol.Value = 0.2f;
            CNK_Data_KartStats.m_JumpImpulseBase.Value = 7.8f;
            CNK_Data_KartStats.m_JumpImpulseBaseMagLev.Value = 14f;
            CNK_Data_KartStats.m_JumpImpulseUpMax.Value = 7.5f;
            CNK_Data_KartStats.m_JumpImpulseUpMin.Value = 0f;
            CNK_Data_KartStats.m_JumpImpulseUpPercent.Value = 0.4f;
            CNK_Data_KartStats.m_JumpMaxUpVelocity.Value = 30f;
            CNK_Data_KartStats.m_JumpTimeInAirBoost.Value[0] = 0.8f;
            CNK_Data_KartStats.m_JumpTimeInAirBoost.Value[1] = 1f;
            CNK_Data_KartStats.m_JumpTimeInAirBoost.Value[2] = 1.5f;

            CNK_Data_KartStats.m_LowSpeed.Value = 12f;

            CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value = randState.Next(23, 31) + (float)randState.NextDouble();
            CNK_Data_KartStats.m_MaxForwardSpeedWumpa.Value = CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value + 3f;

            CNK_Data_KartStats.m_MaxLinearVelXY.Value = 52f; // Extended speed cap, too high causes physics instability
            CNK_Data_KartStats.m_MaxLinearVelZ.Value = 52f;

            CNK_Data_KartStats.m_MaxReverseSpeed.Value = randState.Next(8, 15) + (float)randState.NextDouble();// 10f;

            CNK_Data_KartStats.m_MinHeightForAirNoJump.Value = 1f;

            CNK_Data_KartStats.m_NormalFriction.Value[0] = 55f;
            CNK_Data_KartStats.m_NormalFriction.Value[1] = 7f;
            CNK_Data_KartStats.m_NormalFriction.Value[2] = 0f;

            CNK_Data_KartStats.m_ResetGravStrength.Value = 2f;
            CNK_Data_KartStats.m_ResetMaxTime.Value = 2f;
            CNK_Data_KartStats.m_ResetWaitBeforeDrop.Value = 0.3f;

            CNK_Data_KartStats.m_ReverseGain.Value = 37f;

            CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_SlideBoostQuadratic.Value[0] = 1f;
            CNK_Data_KartStats.m_SlideBoostQuadratic.Value[1] = 0f;
            CNK_Data_KartStats.m_SlideBoostQuadratic.Value[2] = 0f;
            CNK_Data_KartStats.m_SlideBoostTime.Value = 1f;
            CNK_Data_KartStats.m_SlideEaseInSpeed.Value = 180f;
            CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value[0] = 0.67f;
            CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value[1] = 0.79f;
            CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value[2] = 0.842f;
            CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value[0] = 0.108f;
            CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value[1] = 0.1f;
            CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value[2] = 0.085f;
            CNK_Data_KartStats.m_SlideEaseOutSpeed.Value = 135f;
            CNK_Data_KartStats.m_SlideEndMaxTime.Value = 2f;
            CNK_Data_KartStats.m_SlideEndReduceTime.Value = 0.5f;
            CNK_Data_KartStats.m_SlideFrictionHigh.Value[0] = 19f;
            CNK_Data_KartStats.m_SlideFrictionHigh.Value[1] = 13f;
            CNK_Data_KartStats.m_SlideFrictionHigh.Value[2] = 19f;
            CNK_Data_KartStats.m_SlideFrictionLow.Value[0] = 0f;
            CNK_Data_KartStats.m_SlideFrictionLow.Value[1] = 0f;
            CNK_Data_KartStats.m_SlideFrictionLow.Value[2] = 0f;
            CNK_Data_KartStats.m_SlideFrictionNorm.Value[0] = 16f;
            CNK_Data_KartStats.m_SlideFrictionNorm.Value[1] = 12f;
            CNK_Data_KartStats.m_SlideFrictionNorm.Value[2] = 16f;
            CNK_Data_KartStats.m_SlideMaxAngle.Value = 95f;
            CNK_Data_KartStats.m_SlideMaxBoostCount.Value = 3; // If you go over 3 boosts it only affects the boost meter, doesn't let you boost more
            CNK_Data_KartStats.m_SlideMinAngle.Value = 45f;
            CNK_Data_KartStats.m_SlideMinimumSpeed.Value = 8f;
            CNK_Data_KartStats.m_SlideStartMinSteer.Value = 0.1f;
            CNK_Data_KartStats.m_SlideTurnRateAwayFromSlide.Value = 135f;
            CNK_Data_KartStats.m_SlideTurnRateInToSlide.Value = 70f;

            CNK_Data_KartStats.m_SlopeAccelExtra.Value = 0.5f;
            CNK_Data_KartStats.m_SlopeMaxAngle.Value = 60f;
            CNK_Data_KartStats.m_SlopeMinAngle.Value = 0f;

            CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_SpinOutFriction.Value[0] = 6f;
            CNK_Data_KartStats.m_SpinOutFriction.Value[1] = 6f;
            CNK_Data_KartStats.m_SpinOutFriction.Value[2] = 0f;
            CNK_Data_KartStats.m_SpinOutTotalLarge.Value = 2160f;
            CNK_Data_KartStats.m_SpinOutTotalNormal.Value = 1080f;
            CNK_Data_KartStats.m_SpinOutTurnRateMax.Value = 1080f;
            CNK_Data_KartStats.m_SpinOutTurnRateMin.Value = 360f;

            CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_StartLineTS_m_CancelMinPercent.Value = 0f;
            CNK_Data_KartStats.m_StartLineTS_m_DecHoldTime.Value = 0.57f;
            CNK_Data_KartStats.m_StartLineTS_m_DecSpeed.Value = 0.36f;
            CNK_Data_KartStats.m_StartLineTS_m_IncSpeed.Value = 0.56f;
            CNK_Data_KartStats.m_StartLineTS_m_MaxHoldTime.Value = 0.42f;
            CNK_Data_KartStats.m_StartLineTS_m_MaxRepressTime.Value = 0.2f;
            CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value[0] = 1f;
            CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value[1] = 0f;
            CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value[2] = 0f;

            CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value[0] = 0.7f;
            CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value[1] = 0.7f;
            CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value[2] = 1f;

            CNK_Data_KartStats.m_TurnDecellForce.Value = 3f;
            CNK_Data_KartStats.m_TurnDecellForceMax.Value = 20f;
            CNK_Data_KartStats.m_TurnDecellSpeed.Value = 12f;

            CNK_Data_KartStats.m_TurnRateAccel.Value = 10f;
            CNK_Data_KartStats.m_TurnRateBrake.Value = 110f;
            CNK_Data_KartStats.m_TurnRateNormal.Value = randState.Next(60, 80);// 70f;
            CNK_Data_KartStats.m_TurnRateWumpa = CNK_Data_KartStats.m_TurnRateNormal;

            CNK_Data_KartStats.m_WaitBeforeBrakeReverses.Value = 0.225f;

            CNK_Data_KartStats.m_WheelieMinTime.Value = 0.75f;
            CNK_Data_KartStats.m_WheelieSlideBoostMinPercent.Value = 0.25f;
        }

        public override void ModPass(string path_gob_extracted)
        {
            string[] csv_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kpbase.csv");
            //string[] csv_ai_kartphysicsbase = File.ReadAllLines(path_gob_extracted + "common/physics/kabase.csv");

            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainNormal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainNormal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AccelerationGainWumpa] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AccelerationGainWumpa.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropHeight] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropHeight.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_CancelMinPercent.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecHoldTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_DecSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_IncSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxHoldTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_MaxRepressTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInARowTimeTol] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_BoostInARowTimeTol.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxImpulsePerSecond.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostMaxTimeCap] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_BoostMaxTimeCap.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushAngle] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BoostSlidePushTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_BoostSlidePushTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_BrakeForce] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_BrakeForce.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionRadius] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_CollisionRadius.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CollisionSphereOffset] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CollisionSphereOffset.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutFriction] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutMinMaxSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_DonutMinMaxSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMax] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMax.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnRateMin] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnRateMin.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DonutTurnTotal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DonutTurnTotal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceGround] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceGround.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceInAirMagLev] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceInAirMagLev.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLev] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLev.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DownforceMagLevAirTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DownforceMagLevAirTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragMaxStrength] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DragMaxStrength.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_DragStrength] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_DragStrength.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityAir] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_GravityAir.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_GravityGround] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_GravityGround.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HeightForBigAir] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HeightForBigAir.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitByMissileFriction] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitByMissileFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForce] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForce.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedForceRev.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedMin] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitSlowdownSpeedMin.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitStopSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitStopSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitUpSlideTol] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitUpSlideTol.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnLatFriction] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_HiTurnLatFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HiTurnStartAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HiTurnStartAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatFricLoss] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatFricLoss.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMaxAng] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMaxAng.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_HitWallLatMinAng] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_HitWallLatMinAng.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirFriction] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InAirFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirMinSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_InAirMinSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateNormal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateNormal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InAirTurnRateWumpa] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_InAirTurnRateWumpa.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpAirTolerance] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpAirTolerance.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpBeforeAirTimeTol] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpBeforeAirTimeTol.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBase] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBase.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseBaseMagLev] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseBaseMagLev.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMax] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMax.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpMin] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpMin.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpImpulseUpPercent] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpImpulseUpPercent.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpMaxUpVelocity] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_JumpMaxUpVelocity.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_JumpTimeInAirBoost] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_JumpTimeInAirBoost.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_LowSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_LowSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedNormal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxForwardSpeedWumpa] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MaxForwardSpeedWumpa.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelXY] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelXY.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxLinearVelZ] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MaxLinearVelZ.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MaxReverseSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MaxReverseSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_MinHeightForAirNoJump] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_MinHeightForAirNoJump.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_NormalFriction] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_NormalFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetGravStrength] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_ResetGravStrength.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetMaxTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_ResetMaxTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ResetWaitBeforeDrop] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_ResetWaitBeforeDrop.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ReverseGain] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_ReverseGain.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostQuadratic] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostQuadratic.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideBoostTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideBoostTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseInSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseInSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEaseOutSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEaseOutSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndMaxTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndMaxTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideEndReduceTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideEndReduceTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionHigh] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionHigh.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionLow] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionLow.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideFrictionNorm] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SlideFrictionNorm.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMaxBoostCount] = CNK_Common.Int_To_CSV_Line(CNK_Data_KartStats.m_SlideMaxBoostCount.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideMinimumSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideMinimumSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideStartMinSteer] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideStartMinSteer.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateAwayFromSlide.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlideTurnRateInToSlide] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlideTurnRateInToSlide.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeAccelExtra] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeAccelExtra.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMaxAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMaxAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SlopeMinAngle] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SlopeMinAngle.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutFriction] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SpinOutFriction.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalLarge] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalLarge.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTotalNormal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTotalNormal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMax] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMax.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMin] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_SpinOutTurnRateMin.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_CancelMinPercent.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecHoldTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_DecSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_IncSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxHoldTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_MaxRepressTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForce] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForce.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellForceMax] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellForceMax.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnDecellSpeed] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnDecellSpeed.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateAccel] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateAccel.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateBrake] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateBrake.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateNormal] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateNormal.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_TurnRateWumpa] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_TurnRateWumpa.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WaitBeforeBrakeReverses] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_WaitBeforeBrakeReverses.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieMinTime] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieMinTime.Value);
            csv_kartphysicsbase[(int)KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent] = CNK_Common.Float_To_CSV_Line(CNK_Data_KartStats.m_WheelieSlideBoostMinPercent.Value);

            File.WriteAllLines(path_gob_extracted + "common/physics/kpbase.csv", csv_kartphysicsbase);
            File.WriteAllLines(path_gob_extracted + "common/physics/kabase.csv", csv_kartphysicsbase);
        }

    }
}
