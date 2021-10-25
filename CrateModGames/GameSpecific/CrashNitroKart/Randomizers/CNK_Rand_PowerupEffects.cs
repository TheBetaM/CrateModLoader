using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_PowerupEffects : ModStruct<CSV>
    {
        private bool isRand = false;

        public CNK_Rand_PowerupEffects()
        {
            isRand = CNK_Props_Main.Option_RandWeaponEffects.Enabled;
        }


        public override void BeforeModPass()
        {
            Random randState = GetRandom();
            if (!isRand)
            {
                return;
            }
            CNK_Randomize_PowerShield(randState);
            CNK_Randomize_BowlingBomb(randState);
            CNK_Randomize_FreezingMine(randState);
            CNK_Randomize_HomingMissle(randState);
            CNK_Randomize_InvincMask(randState);
            CNK_Randomize_RedEye(randState);
            CNK_Randomize_TNTCrate(randState);
            CNK_Randomize_Tornado(randState);
            CNK_Randomize_TurboBoost(randState);
            CNK_Randomize_StaticShock(randState);
        }

        public override void ModPass(CSV file)
        {
            if (file.Name.ToLower().Contains("powershield"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.PowerShield_m_Time.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.PowerShield_m_RangeForZapping.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.PowerShield_m_ZapSpeed.Value);
                table[4] = CNK_Common.FloatArray_To_CSV_FullLine(table[4], CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value);
                table[5] = CNK_Common.FloatArray_To_CSV_FullLine(table[5], CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.PowerShield_m_ColRadius.Value);
            }
            if (file.Name.ToLower().Contains("bowlingbomb"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_Speed.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_Acceleration.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_AccelerationJuiced.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_Mass.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_Radius.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_AirGravity.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_GroundGravity.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_AirGravityMaglev.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_GroundGravityMaglev.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_TurnSpeed.Value);
                table[11][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_TurnSpeedJuiced.Value);
                table[12][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_ViewRange.Value);
                table[13][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_RangeInFront.Value);
                table[14][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_NormalWumpaLoss.Value);
                table[15][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_JuicedWumpaLoss.Value);
                table[16][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadius.Value);
                table[17][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadiusJuiced.Value);
                table[18][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_DragCoef.Value);
                table[19][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_EasyLatFriction.Value);
                table[20][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_EasyLongFriction.Value);
                table[21][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_HardLatFriction.Value);
                table[22][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_HardLongFriction.Value);
                table[23][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_BackSpeed.Value);
                table[24][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_ExplScale.Value);
                table[25][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.BowlingBomb_m_ExplScaleJuiced.Value);
            }
            if (file.Name.ToLower().Contains("freezingmine"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_NormalFreezeTime.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_JuicedFreezeTime.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_NormalWumpaFruitLost.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_JuicedWumpaFruitLost.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_ThrowDistance.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_ThrowSpeedFactor.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_BlastRadius.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_BlastRadiusJuiced.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_ExplScale.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.FreezingMine_m_ExplScaleJuiced.Value);
            }
            if (file.Name.ToLower().Contains("homingmissile"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_TrackingFrontDistance.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_MaxSpeed.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_MaxSpeedJuiced.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_TimeLimit.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_AirGravityNormal.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_GroundGravityNormal.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_AirGravityMaglev.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_GroundGravityMaglev.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_Acceleration.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_AccelerationJuiced.Value);
                table[11][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_TurnSpeed.Value);
                table[12][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_TurnSpeedJuiced.Value);
                table[13][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_Mass.Value);
                table[14][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_Radius.Value);
                table[15][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_DelayTrackingUpdate.Value);
                table[16][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_ViewRange.Value);
                table[17][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_RangeInFront.Value);
                table[18][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_RangeInBack.Value);
                table[19][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_NormalWumpaLoss.Value);
                table[20][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_JuicedWumpaLoss.Value);
                table[21][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadius.Value);
                table[22][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadiusJuiced.Value);
                table[23][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_DragCoef.Value);
                table[24][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_EasyLatFriction.Value);
                table[25][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_EasyLongFriction.Value);
                table[26][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_HardLatFriction.Value);
                table[27][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_HardLongFriction.Value);
                table[28][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_DecayTime.Value);
                table[29][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_DecaySpeed.Value);
                table[30][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_DecayMin.Value);
                table[31][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_ExplScale.Value);
                table[32][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.HomingMissle_m_ExplScaleJuiced.Value);
            }
            if (file.Name.ToLower().Contains("invincibilitymask"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_NormalTime.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_JuicedTime.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_JuicedTimeTeamed.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_NormalWumpaLoss.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_JuicedWumpaLoss.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_TeamSpeed.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_TeamBlastRange.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_TeamMeterFull.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_ExplScale.Value);
                table[11][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_ExplScaleJuiced.Value);
                table[12][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.InvincMask_m_ColRadius.Value);
            }
            if (file.Name.ToLower().Contains("redeye"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Acceleration.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Deceleration.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_MaxSpeed.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_MinSpeed.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_TurnSpeed.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Explosion_Radius.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_TurnSpeedJuiced.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Explosion_Radius_Juiced.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Expl_Scale.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_Expl_Scale_Juiced.Value);
                table[11][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.RedEye_FullSpeedTurnSlowdown.Value);
            }
            if (file.Name.ToLower().Contains("tntcrate"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_Time.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_TimeBeforeHiddenChar.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_TimeHiddenChar.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_NormalWumpaLoss.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_JuicedWumpaLoss.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_ExplosionBlastRadius.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_ExplScale.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TNT_m_ExplScaleJuiced.Value);
            }
            if (file.Name.ToLower().Contains("tornado"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_TrackingFrontDistance.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_MaxSpeed.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_MaxSpeedJuiced.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_MaxSpeedWithKart.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_TimeLimit.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_AirGravity.Value);
                table[7][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_GroundGravity.Value);
                table[8][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_AirGravityMaglev.Value);
                table[9][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_GroundGravityMaglev.Value);
                table[10][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_Acceleration.Value);
                table[11][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_AccelerationJuiced.Value);
                table[12][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_TurnSpeed.Value);
                table[13][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_TurnSpeedJuiced.Value);
                table[14][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_Mass.Value);
                table[15][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_Radius.Value);
                table[16][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_DelayTrackingUpdate.Value);
                table[17][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_ViewRange.Value);
                table[18][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_RangeInFront.Value);
                table[19][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_RangeInBack.Value);
                table[20][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_LiftTime.Value);
                table[21][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_LiftForce.Value);
                table[22][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_FizzleTime.Value);
                table[23][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_NormalWumpaLoss.Value);
                table[24][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_JuicedWumpaLoss.Value);
                table[25][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_DragCoef.Value);
                table[26][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_EasyLatFriction.Value);
                table[27][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_EasyLongFriction.Value);
                table[28][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_HardLatFriction.Value);
                table[29][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_HardLongFriction.Value);
                table[30][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_TargetAllDistance.Value);
                table[31][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.Tornado_m_ViewRangleOfTarget.Value);
            }
            if (file.Name.ToLower().Contains("turboboost"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TurboBoost_m_NormalTime.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.TurboBoost_m_JuicedTime.Value);
            }
            if (file.Name.ToLower().Contains("staticshock"))
            {
                List<List<string>> table = file.Table;
                table[1][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_NormalTime.Value);
                table[2][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_JuicedTime.Value);
                table[3][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_NormalWumpaLoss.Value);
                table[4][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_JuicedWumpaLoss.Value);
                table[5][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_HomingSpeed.Value);
                table[6][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_Powerups.StaticShock_m_DistanceForHome.Value);
            }

        }

        public void CNK_Randomize_StaticShock(Random randState)
        {
            CNK_Data_Powerups.StaticShock_m_NormalTime.Value = randState.Next(15, 45) * 100;
            CNK_Data_Powerups.StaticShock_m_JuicedTime.Value = CNK_Data_Powerups.StaticShock_m_NormalTime.Value * 2;
            CNK_Data_Powerups.StaticShock_m_NormalWumpaLoss.Value = randState.Next(1, 5);
            CNK_Data_Powerups.StaticShock_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.StaticShock_m_NormalWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.StaticShock_m_HomingSpeed.Value = randState.Next(9, 15);
            CNK_Data_Powerups.StaticShock_m_DistanceForHome.Value = randState.Next(9, 15);
        }

        public void CNK_Randomize_PowerShield(Random randState)
        {
            CNK_Data_Powerups.PowerShield_m_Time.Value = randState.Next(4, 16) * 1000f;
            CNK_Data_Powerups.PowerShield_m_ZapSpeed.Value = randState.Next(6, 12);
            //Sadly, it doesn't seem like this actually changes the shield's color
            CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value[0] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value[1] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value[2] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value[0] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value[1] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value[2] = (float)randState.NextDouble();
            CNK_Data_Powerups.PowerShield_m_RangeForZapping.Value = randState.Next(50, 100) / 10f;
            CNK_Data_Powerups.PowerShield_m_ColRadius.Value = CNK_Data_Powerups.PowerShield_m_RangeForZapping.Value / 4.3f;
        }

        public void CNK_Randomize_TurboBoost(Random randState)
        {
            CNK_Data_Powerups.TurboBoost_m_NormalTime.Value = randState.Next(60, 120) * 100f;
            CNK_Data_Powerups.TurboBoost_m_JuicedTime.Value = CNK_Data_Powerups.TurboBoost_m_NormalTime.Value * 1.5f;
        }

        public void CNK_Randomize_TNTCrate(Random randState)
        {
            CNK_Data_Powerups.TNT_m_Time.Value = 4300;
            CNK_Data_Powerups.TNT_m_TimeBeforeHiddenChar.Value = 2500;
            CNK_Data_Powerups.TNT_m_TimeHiddenChar.Value = 4300;
            CNK_Data_Powerups.TNT_m_NormalWumpaLoss.Value = randState.Next(1, 5);
            CNK_Data_Powerups.TNT_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.TNT_m_JuicedWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.TNT_m_ExplosionBlastRadius.Value = randState.Next(300, 800) / 100f;
            CNK_Data_Powerups.TNT_m_ExplScale.Value = (float)randState.NextDouble() + 0.25f;
            CNK_Data_Powerups.TNT_m_ExplScaleJuiced.Value = CNK_Data_Powerups.TNT_m_ExplScale.Value + 0.1f;
        }

        public void CNK_Randomize_FreezingMine(Random randState)
        {
            CNK_Data_Powerups.FreezingMine_m_NormalFreezeTime.Value = randState.Next(15, 45) * 100;
            CNK_Data_Powerups.FreezingMine_m_JuicedFreezeTime.Value = randState.Next(9, 15) * 1000;
            CNK_Data_Powerups.FreezingMine_m_NormalWumpaFruitLost.Value = randState.Next(1, 5);
            CNK_Data_Powerups.FreezingMine_m_JuicedWumpaFruitLost.Value = CNK_Data_Powerups.FreezingMine_m_NormalWumpaFruitLost.Value + randState.Next(0, 2);
            CNK_Data_Powerups.FreezingMine_m_ThrowDistance.Value = randState.Next(16, 32);
            CNK_Data_Powerups.FreezingMine_m_ThrowSpeedFactor.Value = (float)randState.NextDouble() + 0.75f;
            CNK_Data_Powerups.FreezingMine_m_BlastRadius.Value = (float)randState.NextDouble() + randState.Next(1, 5);
            CNK_Data_Powerups.FreezingMine_m_BlastRadiusJuiced.Value = CNK_Data_Powerups.FreezingMine_m_BlastRadius.Value * 2f;
            CNK_Data_Powerups.FreezingMine_m_ExplScale.Value = (float)randState.NextDouble() + 0.1f;
            CNK_Data_Powerups.FreezingMine_m_ExplScaleJuiced.Value = CNK_Data_Powerups.FreezingMine_m_ExplScale.Value * 2f;
        }

        public void CNK_Randomize_RedEye(Random randState)
        {
            CNK_Data_Powerups.RedEye_Acceleration.Value = randState.Next(25, 75) + (float)randState.NextDouble();
            CNK_Data_Powerups.RedEye_Deceleration.Value = randState.Next(10, 20) + (float)randState.NextDouble();
            CNK_Data_Powerups.RedEye_MaxSpeed.Value = randState.Next(45, 75);
            CNK_Data_Powerups.RedEye_MinSpeed.Value = randState.Next(24, 32);
            CNK_Data_Powerups.RedEye_TurnSpeed.Value = randState.Next(8, 12) + (float)randState.NextDouble();
            CNK_Data_Powerups.RedEye_Explosion_Radius.Value = randState.Next(2, 5) + (float)randState.NextDouble();
            CNK_Data_Powerups.RedEye_TurnSpeedJuiced.Value = CNK_Data_Powerups.RedEye_TurnSpeed.Value + 2f;
            CNK_Data_Powerups.RedEye_Explosion_Radius_Juiced.Value = CNK_Data_Powerups.RedEye_Explosion_Radius.Value + 4f;
            CNK_Data_Powerups.RedEye_Expl_Scale.Value = (float)randState.NextDouble() + 0.1f;
            CNK_Data_Powerups.RedEye_Expl_Scale_Juiced.Value = CNK_Data_Powerups.RedEye_Expl_Scale.Value * 2f;
            CNK_Data_Powerups.RedEye_FullSpeedTurnSlowdown.Value = 4f;
        }

        public void CNK_Randomize_InvincMask(Random randState)
        {
            CNK_Data_Powerups.InvincMask_m_NormalTime.Value = randState.Next(60, 100) * 100;
            CNK_Data_Powerups.InvincMask_m_JuicedTime.Value = CNK_Data_Powerups.InvincMask_m_NormalTime.Value + ((int)Math.Ceiling(CNK_Data_Powerups.InvincMask_m_NormalTime.Value / 2f));
            CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value = randState.Next(80, 160) * 100;
            CNK_Data_Powerups.InvincMask_m_JuicedTimeTeamed.Value = CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value + ((int)Math.Ceiling(CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value / 3f));
            CNK_Data_Powerups.InvincMask_m_NormalWumpaLoss.Value = randState.Next(1, 5);
            CNK_Data_Powerups.InvincMask_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.InvincMask_m_NormalWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.InvincMask_m_TeamSpeed.Value = 15f;
            CNK_Data_Powerups.InvincMask_m_TeamBlastRange.Value = 40f;
            CNK_Data_Powerups.InvincMask_m_TeamMeterFull.Value = 5f;
            CNK_Data_Powerups.InvincMask_m_ExplScale.Value = (float)randState.NextDouble() + 0.5f;
            CNK_Data_Powerups.InvincMask_m_ExplScaleJuiced.Value = 1.5f * CNK_Data_Powerups.InvincMask_m_ExplScale.Value;
            CNK_Data_Powerups.InvincMask_m_ColRadius.Value = randState.Next(1, 3) + (float)randState.NextDouble();
        }

        public void CNK_Randomize_BowlingBomb(Random randState)
        {
            CNK_Data_Powerups.BowlingBomb_m_Speed.Value = randState.Next(50, 80);
            CNK_Data_Powerups.BowlingBomb_m_Acceleration.Value = randState.Next(70, 90);
            CNK_Data_Powerups.BowlingBomb_m_AccelerationJuiced.Value = CNK_Data_Powerups.BowlingBomb_m_Acceleration.Value * 1.125f;
            CNK_Data_Powerups.BowlingBomb_m_Mass.Value = 2500f;
            CNK_Data_Powerups.BowlingBomb_m_Radius.Value = 1f;
            CNK_Data_Powerups.BowlingBomb_m_AirGravity.Value = 12f;
            CNK_Data_Powerups.BowlingBomb_m_GroundGravity.Value = 4f;
            CNK_Data_Powerups.BowlingBomb_m_AirGravityMaglev.Value = 17f;
            CNK_Data_Powerups.BowlingBomb_m_GroundGravityMaglev.Value = 15f;
            CNK_Data_Powerups.BowlingBomb_m_TurnSpeed.Value = 0.9f;
            CNK_Data_Powerups.BowlingBomb_m_TurnSpeedJuiced.Value = CNK_Data_Powerups.BowlingBomb_m_TurnSpeed.Value;
            CNK_Data_Powerups.BowlingBomb_m_ViewRange.Value = 0.993f;
            CNK_Data_Powerups.BowlingBomb_m_RangeInFront.Value = 150f;
            CNK_Data_Powerups.BowlingBomb_m_NormalWumpaLoss.Value = randState.Next(1, 5);
            CNK_Data_Powerups.BowlingBomb_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.BowlingBomb_m_NormalWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadius.Value = randState.Next(25, 75) / 10f;
            CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadiusJuiced.Value = CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadius.Value * 1.6f;
            CNK_Data_Powerups.BowlingBomb_m_DragCoef.Value = 0.00125f;
            CNK_Data_Powerups.BowlingBomb_m_EasyLatFriction.Value = 30f;
            CNK_Data_Powerups.BowlingBomb_m_EasyLongFriction.Value = 1f;
            CNK_Data_Powerups.BowlingBomb_m_HardLatFriction.Value = 30f;
            CNK_Data_Powerups.BowlingBomb_m_HardLongFriction.Value = 1f;
            CNK_Data_Powerups.BowlingBomb_m_BackSpeed.Value = randState.Next(30, 60);
            CNK_Data_Powerups.BowlingBomb_m_ExplScale.Value = (float)randState.NextDouble() + 0.5f;
            CNK_Data_Powerups.BowlingBomb_m_ExplScaleJuiced.Value = CNK_Data_Powerups.BowlingBomb_m_ExplScale.Value * 1.5f;
        }

        public void CNK_Randomize_HomingMissle(Random randState)
        {
            CNK_Data_Powerups.HomingMissle_m_TrackingFrontDistance.Value = 50f;
            CNK_Data_Powerups.HomingMissle_m_MaxSpeed.Value = randState.Next(40, 80);
            CNK_Data_Powerups.HomingMissle_m_MaxSpeedJuiced.Value = CNK_Data_Powerups.HomingMissle_m_MaxSpeed.Value * (70f / 60f);
            CNK_Data_Powerups.HomingMissle_m_TimeLimit.Value = 15000;
            CNK_Data_Powerups.HomingMissle_m_AirGravityNormal.Value = 8f;
            CNK_Data_Powerups.HomingMissle_m_GroundGravityNormal.Value = 1.25f;
            CNK_Data_Powerups.HomingMissle_m_AirGravityMaglev.Value = 8f;
            CNK_Data_Powerups.HomingMissle_m_GroundGravityMaglev.Value = CNK_Data_Powerups.HomingMissle_m_AirGravityMaglev.Value;
            CNK_Data_Powerups.HomingMissle_m_Acceleration.Value = randState.Next(35, 55);
            CNK_Data_Powerups.HomingMissle_m_AccelerationJuiced.Value = CNK_Data_Powerups.HomingMissle_m_Acceleration.Value * (55f / 45f);
            CNK_Data_Powerups.HomingMissle_m_TurnSpeed.Value = 5f;
            CNK_Data_Powerups.HomingMissle_m_TurnSpeedJuiced.Value = CNK_Data_Powerups.HomingMissle_m_TurnSpeed.Value * (8f / 5f);
            CNK_Data_Powerups.HomingMissle_m_Mass.Value = 1000f;
            CNK_Data_Powerups.HomingMissle_m_Radius.Value = 1f;
            CNK_Data_Powerups.HomingMissle_m_DelayTrackingUpdate.Value = 100;
            CNK_Data_Powerups.HomingMissle_m_ViewRange.Value = 0.2f;
            CNK_Data_Powerups.HomingMissle_m_RangeInFront.Value = 140;
            CNK_Data_Powerups.HomingMissle_m_RangeInBack.Value = 0f;
            CNK_Data_Powerups.HomingMissle_m_NormalWumpaLoss.Value = randState.Next(1, 5);
            CNK_Data_Powerups.HomingMissle_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.HomingMissle_m_NormalWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadius.Value = 1f;
            CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadiusJuiced.Value = CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadius.Value;
            CNK_Data_Powerups.HomingMissle_m_DragCoef.Value = 0.00125f;
            CNK_Data_Powerups.HomingMissle_m_EasyLatFriction.Value = 15f;
            CNK_Data_Powerups.HomingMissle_m_EasyLongFriction.Value = 1f;
            CNK_Data_Powerups.HomingMissle_m_HardLatFriction.Value = 55f;
            CNK_Data_Powerups.HomingMissle_m_HardLongFriction.Value = 1f;
            CNK_Data_Powerups.HomingMissle_m_DecayTime.Value = 5000;
            CNK_Data_Powerups.HomingMissle_m_DecaySpeed.Value = 2f;
            CNK_Data_Powerups.HomingMissle_m_DecayMin.Value = 40f;
            CNK_Data_Powerups.HomingMissle_m_ExplScale.Value = 0.45f;
            CNK_Data_Powerups.HomingMissle_m_ExplScaleJuiced = CNK_Data_Powerups.HomingMissle_m_ExplScale;
        }

        public void CNK_Randomize_Tornado(Random randState)
        {
            CNK_Data_Powerups.Tornado_m_TrackingFrontDistance.Value = 35f;
            CNK_Data_Powerups.Tornado_m_MaxSpeed.Value = randState.Next(45, 60);
            CNK_Data_Powerups.Tornado_m_MaxSpeedJuiced.Value = CNK_Data_Powerups.Tornado_m_MaxSpeed.Value;
            CNK_Data_Powerups.Tornado_m_MaxSpeedWithKart.Value = 10f;
            CNK_Data_Powerups.Tornado_m_TimeLimit.Value = 30000;
            CNK_Data_Powerups.Tornado_m_AirGravity.Value = 6f;
            CNK_Data_Powerups.Tornado_m_GroundGravity.Value = 1.5f;
            CNK_Data_Powerups.Tornado_m_AirGravityMaglev.Value = CNK_Data_Powerups.Tornado_m_AirGravity.Value;
            CNK_Data_Powerups.Tornado_m_GroundGravityMaglev.Value = 8f;
            CNK_Data_Powerups.Tornado_m_Acceleration.Value = randState.Next(40, 60);
            CNK_Data_Powerups.Tornado_m_AccelerationJuiced.Value = CNK_Data_Powerups.Tornado_m_Acceleration.Value;
            CNK_Data_Powerups.Tornado_m_TurnSpeed.Value = randState.Next(6, 10);
            CNK_Data_Powerups.Tornado_m_TurnSpeedJuiced.Value = CNK_Data_Powerups.Tornado_m_TurnSpeed.Value;
            CNK_Data_Powerups.Tornado_m_Mass.Value = 50f;
            CNK_Data_Powerups.Tornado_m_Radius.Value = 2.5f;
            CNK_Data_Powerups.Tornado_m_DelayTrackingUpdate.Value = 100;
            CNK_Data_Powerups.Tornado_m_ViewRange.Value = 0f;
            CNK_Data_Powerups.Tornado_m_RangeInFront.Value = 0f;
            CNK_Data_Powerups.Tornado_m_RangeInBack.Value = 0f;
            CNK_Data_Powerups.Tornado_m_LiftTime.Value = 3000;
            CNK_Data_Powerups.Tornado_m_LiftForce.Value = 30f;
            CNK_Data_Powerups.Tornado_m_FizzleTime.Value = 1000;
            CNK_Data_Powerups.Tornado_m_NormalWumpaLoss.Value = randState.Next(3, 7);
            CNK_Data_Powerups.Tornado_m_JuicedWumpaLoss.Value = CNK_Data_Powerups.Tornado_m_NormalWumpaLoss.Value + randState.Next(0, 2);
            CNK_Data_Powerups.Tornado_m_DragCoef.Value = 0.01f;
            CNK_Data_Powerups.Tornado_m_EasyLatFriction.Value = 30f;
            CNK_Data_Powerups.Tornado_m_EasyLongFriction.Value = 1f;
            CNK_Data_Powerups.Tornado_m_HardLatFriction.Value = 50f;
            CNK_Data_Powerups.Tornado_m_HardLongFriction.Value = 1f;
            CNK_Data_Powerups.Tornado_m_TargetAllDistance.Value = 18f;
            CNK_Data_Powerups.Tornado_m_ViewRangleOfTarget.Value = 0.707f;
        }

        public void CNK_Randomize_WeaponSelection(Random randState)
        {
            //todo
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
        }
    }
}
