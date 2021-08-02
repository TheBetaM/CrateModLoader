using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_KartStats : ModStruct<CSV>
    {
        private bool isRand = false;

        public CNK_Rand_KartStats()
        {
            isRand = CNK_Props_Main.Option_RandKartStats.Enabled;
        }

        public override void BeforeModPass()
        {
            Random randState = GetRandom();

            if (!isRand)
            {
                return;
            }

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

        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower() != "kpbase.csv" && file.Name.ToLower() != "kabase.csv")
            {
                return;
            }
            List<List<string>> table = file.Table;

            table[(int)KartPhysicsBaseRows.m_AccelerationGainNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AccelerationGainNormal.Value);
            table[(int)KartPhysicsBaseRows.m_AccelerationGainWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AccelerationGainWumpa.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropHeight][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropHeight.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTime.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_CancelMinPercent][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_CancelMinPercent.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecHoldTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_DecHoldTime.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_DecSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_DecSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_IncSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_IncSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxHoldTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_MaxHoldTime.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_MaxRepressTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_AkuDropTS_m_MaxRepressTime.Value);
            table[(int)KartPhysicsBaseRows.m_AkuDropTS_m_Quadratic] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_AkuDropTS_m_Quadratic.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInARowTimeTol][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_BoostInARowTimeTol.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_AKU_DROP] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_AKU_DROP.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_LARGE] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_LARGE.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_MEDIUM] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_MEDIUM.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_JUMP_SMALL] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_JUMP_SMALL.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_PAD] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_PAD.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_1] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_1.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_2] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_2.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SLIDE_3] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_SLIDE_3.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_START] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_START.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_SUPER_ENGINE] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_SUPER_ENGINE.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST.Value);
            table[(int)KartPhysicsBaseRows.m_BoostInfo_eBOOST_TURBOBOOST_JUICED] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value);
            table[(int)KartPhysicsBaseRows.m_BoostMaxImpulsePerSecond][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_BoostMaxImpulsePerSecond.Value);
            table[(int)KartPhysicsBaseRows.m_BoostMaxTimeCap][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_BoostMaxTimeCap.Value);
            table[(int)KartPhysicsBaseRows.m_BoostSlidePushAngle] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_BoostSlidePushAngle.Value);
            table[(int)KartPhysicsBaseRows.m_BoostSlidePushTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_BoostSlidePushTime.Value);
            table[(int)KartPhysicsBaseRows.m_BrakeForce][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_BrakeForce.Value);
            table[(int)KartPhysicsBaseRows.m_CollisionRadius][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_CollisionRadius.Value);
            table[(int)KartPhysicsBaseRows.m_CollisionSphereOffset] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_CollisionSphereOffset.Value);
            table[(int)KartPhysicsBaseRows.m_CtfFlagMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_CtfFlagMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_CursedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_CursedMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_DonutFriction] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_DonutFriction.Value);
            table[(int)KartPhysicsBaseRows.m_DonutMinMaxSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_DonutMinMaxSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_DonutTurnRateMax][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DonutTurnRateMax.Value);
            table[(int)KartPhysicsBaseRows.m_DonutTurnRateMin][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DonutTurnRateMin.Value);
            table[(int)KartPhysicsBaseRows.m_DonutTurnTotal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DonutTurnTotal.Value);
            table[(int)KartPhysicsBaseRows.m_DownforceGround][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DownforceGround.Value);
            table[(int)KartPhysicsBaseRows.m_DownforceInAirMagLev][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DownforceInAirMagLev.Value);
            table[(int)KartPhysicsBaseRows.m_DownforceMagLev][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DownforceMagLev.Value);
            table[(int)KartPhysicsBaseRows.m_DownforceMagLevAirTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DownforceMagLevAirTime.Value);
            table[(int)KartPhysicsBaseRows.m_DragMaxStrength][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DragMaxStrength.Value);
            table[(int)KartPhysicsBaseRows.m_DragStrength][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_DragStrength.Value);
            table[(int)KartPhysicsBaseRows.m_GravityAir][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_GravityAir.Value);
            table[(int)KartPhysicsBaseRows.m_GravityGround][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_GravityGround.Value);
            table[(int)KartPhysicsBaseRows.m_HeightForBigAir][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HeightForBigAir.Value);
            table[(int)KartPhysicsBaseRows.m_HitByMissileFriction][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitByMissileFriction.Value);
            table[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForce][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitSlowdownSpeedForce.Value);
            table[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedForceRev][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitSlowdownSpeedForceRev.Value);
            table[(int)KartPhysicsBaseRows.m_HitSlowdownSpeedMin][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitSlowdownSpeedMin.Value);
            table[(int)KartPhysicsBaseRows.m_HitStopAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitStopAngle.Value);
            table[(int)KartPhysicsBaseRows.m_HitStopSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitStopSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_HitUpSlideTol][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitUpSlideTol.Value);
            table[(int)KartPhysicsBaseRows.m_HiTurnLatFriction] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_HiTurnLatFriction.Value);
            table[(int)KartPhysicsBaseRows.m_HiTurnStartAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HiTurnStartAngle.Value);
            table[(int)KartPhysicsBaseRows.m_HitWallLatFricLoss][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitWallLatFricLoss.Value);
            table[(int)KartPhysicsBaseRows.m_HitWallLatMaxAng][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitWallLatMaxAng.Value);
            table[(int)KartPhysicsBaseRows.m_HitWallLatMinAng][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_HitWallLatMinAng.Value);
            table[(int)KartPhysicsBaseRows.m_InAirFriction] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_InAirFriction.Value);
            table[(int)KartPhysicsBaseRows.m_InAirMinSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_InAirMinSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_InAirTurnRateNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_InAirTurnRateNormal.Value);
            table[(int)KartPhysicsBaseRows.m_InAirTurnRateWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_InAirTurnRateWumpa.Value);
            table[(int)KartPhysicsBaseRows.m_InvincibiliyMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_InvincibiliyMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_JumpAirTolerance][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpAirTolerance.Value);
            table[(int)KartPhysicsBaseRows.m_JumpBeforeAirTimeTol][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpBeforeAirTimeTol.Value);
            table[(int)KartPhysicsBaseRows.m_JumpImpulseBase][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpImpulseBase.Value);
            table[(int)KartPhysicsBaseRows.m_JumpImpulseBaseMagLev][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpImpulseBaseMagLev.Value);
            table[(int)KartPhysicsBaseRows.m_JumpImpulseUpMax][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpImpulseUpMax.Value);
            table[(int)KartPhysicsBaseRows.m_JumpImpulseUpMin][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpImpulseUpMin.Value);
            table[(int)KartPhysicsBaseRows.m_JumpImpulseUpPercent][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpImpulseUpPercent.Value);
            table[(int)KartPhysicsBaseRows.m_JumpMaxUpVelocity][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_JumpMaxUpVelocity.Value);
            table[(int)KartPhysicsBaseRows.m_JumpTimeInAirBoost] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_JumpTimeInAirBoost.Value);
            table[(int)KartPhysicsBaseRows.m_LowSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_LowSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_MaxForwardSpeedNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MaxForwardSpeedNormal.Value);
            table[(int)KartPhysicsBaseRows.m_MaxForwardSpeedWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MaxForwardSpeedWumpa.Value);
            table[(int)KartPhysicsBaseRows.m_MaxLinearVelXY][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MaxLinearVelXY.Value);
            table[(int)KartPhysicsBaseRows.m_MaxLinearVelZ][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MaxLinearVelZ.Value);
            table[(int)KartPhysicsBaseRows.m_MaxReverseSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MaxReverseSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_MinHeightForAirNoJump][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_MinHeightForAirNoJump.Value);
            table[(int)KartPhysicsBaseRows.m_NormalFriction] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_NormalFriction.Value);
            table[(int)KartPhysicsBaseRows.m_ResetGravStrength][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_ResetGravStrength.Value);
            table[(int)KartPhysicsBaseRows.m_ResetMaxTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_ResetMaxTime.Value);
            table[(int)KartPhysicsBaseRows.m_ResetWaitBeforeDrop][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_ResetWaitBeforeDrop.Value);
            table[(int)KartPhysicsBaseRows.m_ReverseGain][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_ReverseGain.Value);
            table[(int)KartPhysicsBaseRows.m_ShockedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_ShockedMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SlideBoostQuadratic] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideBoostQuadratic.Value);
            table[(int)KartPhysicsBaseRows.m_SlideBoostTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideBoostTime.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEaseInSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideEaseInSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEaseOutPercentBetween] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideEaseOutPercentBetween.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEaseOutRotVelSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideEaseOutRotVelSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEaseOutSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideEaseOutSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEndMaxTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideEndMaxTime.Value);
            table[(int)KartPhysicsBaseRows.m_SlideEndReduceTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideEndReduceTime.Value);
            table[(int)KartPhysicsBaseRows.m_SlideFrictionHigh] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideFrictionHigh.Value);
            table[(int)KartPhysicsBaseRows.m_SlideFrictionLow] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideFrictionLow.Value);
            table[(int)KartPhysicsBaseRows.m_SlideFrictionNorm] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SlideFrictionNorm.Value);
            table[(int)KartPhysicsBaseRows.m_SlideMaxAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideMaxAngle.Value);
            table[(int)KartPhysicsBaseRows.m_SlideMaxBoostCount][0] = CNK_Common.Int_To_CSV_Word(CNK_Data_KartStats.m_SlideMaxBoostCount.Value);
            table[(int)KartPhysicsBaseRows.m_SlideMinAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideMinAngle.Value);
            table[(int)KartPhysicsBaseRows.m_SlideMinimumSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideMinimumSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SlideStartMinSteer][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideStartMinSteer.Value);
            table[(int)KartPhysicsBaseRows.m_SlideTurnRateAwayFromSlide][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideTurnRateAwayFromSlide.Value);
            table[(int)KartPhysicsBaseRows.m_SlideTurnRateInToSlide][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlideTurnRateInToSlide.Value);
            table[(int)KartPhysicsBaseRows.m_SlopeAccelExtra][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlopeAccelExtra.Value);
            table[(int)KartPhysicsBaseRows.m_SlopeMaxAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlopeMaxAngle.Value);
            table[(int)KartPhysicsBaseRows.m_SlopeMinAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SlopeMinAngle.Value);
            table[(int)KartPhysicsBaseRows.m_SpikeyFruitMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SpikeyFruitMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_SpinOutFriction] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SpinOutFriction.Value);
            table[(int)KartPhysicsBaseRows.m_SpinOutTotalLarge][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SpinOutTotalLarge.Value);
            table[(int)KartPhysicsBaseRows.m_SpinOutTotalNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SpinOutTotalNormal.Value);
            table[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMax][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SpinOutTurnRateMax.Value);
            table[(int)KartPhysicsBaseRows.m_SpinOutTurnRateMin][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_SpinOutTurnRateMin.Value);
            table[(int)KartPhysicsBaseRows.m_SquashedMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_SquashedMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_CancelMinPercent][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_CancelMinPercent.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecHoldTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_DecHoldTime.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_DecSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_DecSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_IncSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_IncSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxHoldTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_MaxHoldTime.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_MaxRepressTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_StartLineTS_m_MaxRepressTime.Value);
            table[(int)KartPhysicsBaseRows.m_StartLineTS_m_Quadratic] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_StartLineTS_m_Quadratic.Value);
            table[(int)KartPhysicsBaseRows.m_TimeBubbleMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_TimeBubbleMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_TropyClocksMaxForwardSpeed] = CNK_Common.FloatArray_To_CSV_FullLine(CNK_Data_KartStats.m_TropyClocksMaxForwardSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_TurnDecellForce][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnDecellForce.Value);
            table[(int)KartPhysicsBaseRows.m_TurnDecellForceMax][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnDecellForceMax.Value);
            table[(int)KartPhysicsBaseRows.m_TurnDecellSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnDecellSpeed.Value);
            table[(int)KartPhysicsBaseRows.m_TurnRateAccel][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnRateAccel.Value);
            table[(int)KartPhysicsBaseRows.m_TurnRateBrake][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnRateBrake.Value);
            table[(int)KartPhysicsBaseRows.m_TurnRateNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnRateNormal.Value);
            table[(int)KartPhysicsBaseRows.m_TurnRateWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_TurnRateWumpa.Value);
            table[(int)KartPhysicsBaseRows.m_WaitBeforeBrakeReverses][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_WaitBeforeBrakeReverses.Value);
            table[(int)KartPhysicsBaseRows.m_WheelieMinTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_WheelieMinTime.Value);
            table[(int)KartPhysicsBaseRows.m_WheelieSlideBoostMinPercent][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_KartStats.m_WheelieSlideBoostMinPercent.Value);

        }

    }
}
