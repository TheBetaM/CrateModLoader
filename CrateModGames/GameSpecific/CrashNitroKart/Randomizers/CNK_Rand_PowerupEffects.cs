using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_PowerupEffects : ModStruct<string>
    {
        public override string Name => CNK_Text.Rand_PowerupEffects;
        public override string Description => CNK_Text.Rand_PowerupEffectsDesc;

        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
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

        public override void ModPass(string path_gob_extracted)
        {

            bool Editing_CSV_PowerShield = true;
            bool Editing_CSV_BowlingBomb = true;
            bool Editing_CSV_FreezingMine = true;
            bool Editing_CSV_HomingMissle = true;
            bool Editing_CSV_InvincMask = true;
            bool Editing_CSV_RedEye = true;
            bool Editing_CSV_TNT = true;
            bool Editing_CSV_Tornado = true;
            bool Editing_CSV_TurboBoost = true;
            bool Editing_CSV_StaticShock = true;

            if (Editing_CSV_PowerShield)
            {
                string[] csv_PowerShield = File.ReadAllLines(path_gob_extracted + "common/weapons/powershield.csv");

                csv_PowerShield[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_Time.Value) + ",,,,# float ,# m_Time,";
                csv_PowerShield[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_RangeForZapping.Value) + ",,,,# float,# m_RangeForZapping,\"#15, 10\"";
                csv_PowerShield[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ZapSpeed.Value) + ",,,,# float,# m_ZapSpeed,";
                csv_PowerShield[4] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColorNonJuiced.Value) + ",,# vec3f,# m_ColorNonJuiced,";
                csv_PowerShield[5] = CNK_Common.FloatArray_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColorJuiced.Value) + ",,# vec3f,# m_ColorJuiced,";
                csv_PowerShield[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.PowerShield_m_ColRadius.Value) + ",,,,# float,# m_ColRadius,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/powershield.csv", csv_PowerShield);
            }
            if (Editing_CSV_BowlingBomb)
            {
                string[] csv_BowlingBomb = File.ReadAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv");

                csv_BowlingBomb[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Speed.Value) + ",,,,,,,";
                csv_BowlingBomb[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Acceleration.Value) + ",,,,,,,";
                csv_BowlingBomb[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Mass.Value) + ",,,,,,,";
                csv_BowlingBomb[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_Radius.Value) + ",,,,,,,";
                csv_BowlingBomb[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AirGravity.Value) + ",,,,,,,";
                csv_BowlingBomb[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_GroundGravity.Value) + ",,,,,,,";
                csv_BowlingBomb[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_BowlingBomb[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_BowlingBomb[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_TurnSpeed.Value) + ",,,,,,,";
                csv_BowlingBomb[11] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[12] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ViewRange.Value) + ",,,,,,,";
                csv_BowlingBomb[13] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_RangeInFront.Value) + ",,,,,,,";
                csv_BowlingBomb[14] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_BowlingBomb[15] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_BowlingBomb[16] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_BowlingBomb[17] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplosionBlastRadiusJuiced.Value) + ",,,,,,,";
                csv_BowlingBomb[18] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_DragCoef.Value) + ",,,,,,,";
                csv_BowlingBomb[19] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[20] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[21] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_HardLatFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[22] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_HardLongFriction.Value) + ",,,,,,,";
                csv_BowlingBomb[23] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_BackSpeed.Value) + ",,,,,,,";
                csv_BowlingBomb[24] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplScale.Value) + ",,,,,,,";
                csv_BowlingBomb[25] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.BowlingBomb_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/bowlingbomb.csv", csv_BowlingBomb);
            }
            if (Editing_CSV_FreezingMine)
            {
                string[] csv_FreezingMine = File.ReadAllLines(path_gob_extracted + "common/weapons/freezingmine.csv");

                csv_FreezingMine[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_NormalFreezeTime.Value) + ",,,,,,,";
                csv_FreezingMine[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_JuicedFreezeTime.Value) + ",,,,,,,";
                csv_FreezingMine[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_NormalWumpaFruitLost.Value) + ",,,,,,,";
                csv_FreezingMine[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_JuicedWumpaFruitLost.Value) + ",,,,,,,";
                csv_FreezingMine[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ThrowDistance.Value) + ",,,,,,,";
                csv_FreezingMine[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ThrowSpeedFactor.Value) + ",,,,,,,";
                csv_FreezingMine[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_BlastRadius.Value) + ",,,,,,,";
                csv_FreezingMine[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_BlastRadiusJuiced.Value) + ",,,,,,,";
                csv_FreezingMine[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ExplScale.Value) + ",,,,,,,";
                csv_FreezingMine[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.FreezingMine_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/freezingmine.csv", csv_FreezingMine);
            }
            if (Editing_CSV_HomingMissle)
            {
                string[] csv_HomingMissle = File.ReadAllLines(path_gob_extracted + "common/weapons/homingmissile.csv");

                csv_HomingMissle[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TrackingFrontDistance.Value) + ",,,,,,,";
                csv_HomingMissle[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_MaxSpeed.Value) + ",,,,,,,";
                csv_HomingMissle[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_MaxSpeedJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TimeLimit.Value) + ",,,,,,,";
                csv_HomingMissle[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AirGravityNormal.Value) + ",,,,,,,";
                csv_HomingMissle[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_GroundGravityNormal.Value) + ",,,,,,,";
                csv_HomingMissle[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_HomingMissle[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_HomingMissle[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Acceleration.Value) + ",,,,,,,";
                csv_HomingMissle[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[11] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TurnSpeed.Value) + ",,,,,,,";
                csv_HomingMissle[12] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[13] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Mass.Value) + ",,,,,,,";
                csv_HomingMissle[14] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_Radius.Value) + ",,,,,,,";
                csv_HomingMissle[15] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DelayTrackingUpdate.Value) + ",,,,,,,";
                csv_HomingMissle[16] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ViewRange.Value) + ",,,,,,,";
                csv_HomingMissle[17] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_RangeInFront.Value) + ",,,,,,,";
                csv_HomingMissle[18] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_RangeInBack.Value) + ",,,,,,,";
                csv_HomingMissle[19] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_HomingMissle[20] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_HomingMissle[21] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_HomingMissle[22] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplosionBlastRadiusJuiced.Value) + ",,,,,,,";
                csv_HomingMissle[23] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DragCoef.Value) + ",,,,,,,";
                csv_HomingMissle[24] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_HomingMissle[25] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_HomingMissle[26] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_HardLatFriction.Value) + ",,,,,,,";
                csv_HomingMissle[27] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_HardLongFriction.Value) + ",,,,,,,";
                csv_HomingMissle[28] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecayTime.Value) + ",,,,,,,";
                csv_HomingMissle[29] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecaySpeed.Value) + ",,,,,,,";
                csv_HomingMissle[30] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_DecayMin.Value) + ",,,,,,,";
                csv_HomingMissle[31] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplScale.Value) + ",,,,,,,";
                csv_HomingMissle[32] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.HomingMissle_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/homingmissile.csv", csv_HomingMissle);
            }
            if (Editing_CSV_InvincMask)
            {
                string[] csv_InvincMask = File.ReadAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv");

                csv_InvincMask[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalTime.Value) + ",,,,,,,";
                csv_InvincMask[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedTime.Value) + ",,,,,,,";
                csv_InvincMask[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalTimeTeamed.Value) + ",,,,,,,";
                csv_InvincMask[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedTimeTeamed.Value) + ",,,,,,,";
                csv_InvincMask[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_InvincMask[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_InvincMask[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamSpeed.Value) + ",,,,,,,";
                csv_InvincMask[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamBlastRange.Value) + ",,,,,,,";
                csv_InvincMask[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_TeamMeterFull.Value) + ",,,,,,,";
                csv_InvincMask[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ExplScale.Value) + ",,,,,,,";
                csv_InvincMask[11] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ExplScaleJuiced.Value) + ",,,,,,,";
                csv_InvincMask[12] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.InvincMask_m_ColRadius.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/invincibilitymask.csv", csv_InvincMask);
            }
            if (Editing_CSV_RedEye)
            {
                string[] csv_RedEye = File.ReadAllLines(path_gob_extracted + "common/weapons/redeye.csv");

                csv_RedEye[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Acceleration.Value) + ",,,,,,,";
                csv_RedEye[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Deceleration.Value) + ",,,,,,,";
                csv_RedEye[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_MaxSpeed.Value) + ",,,,,,,";
                csv_RedEye[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_MinSpeed.Value) + ",,,,,,,";
                csv_RedEye[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_TurnSpeed.Value) + ",,,,,,,";
                csv_RedEye[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Explosion_Radius.Value) + ",,,,,,,";
                csv_RedEye[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_RedEye[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Explosion_Radius_Juiced.Value) + ",,,,,,,";
                csv_RedEye[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Expl_Scale.Value) + ",,,,,,,";
                csv_RedEye[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_Expl_Scale_Juiced.Value) + ",,,,,,,";
                csv_RedEye[11] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.RedEye_FullSpeedTurnSlowdown.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/redeye.csv", csv_RedEye);
            }
            if (Editing_CSV_TNT)
            {
                string[] csv_TNT = File.ReadAllLines(path_gob_extracted + "common/weapons/tntcrate.csv");

                csv_TNT[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_Time.Value) + ",,,,,,,";
                csv_TNT[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_TimeBeforeHiddenChar.Value) + ",,,,,,,";
                csv_TNT[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_TimeHiddenChar.Value) + ",,,,,,,";
                csv_TNT[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_TNT[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_TNT[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplosionBlastRadius.Value) + ",,,,,,,";
                csv_TNT[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplScale.Value) + ",,,,,,,";
                csv_TNT[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TNT_m_ExplScaleJuiced.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tntcrate.csv", csv_TNT);
            }
            if (Editing_CSV_Tornado)
            {
                string[] csv_Tornado = File.ReadAllLines(path_gob_extracted + "common/weapons/tornado.csv");

                csv_Tornado[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TrackingFrontDistance.Value) + ",,,,,,,";
                csv_Tornado[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeed.Value) + ",,,,,,,";
                csv_Tornado[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeedJuiced.Value) + ",,,,,,,";
                csv_Tornado[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_MaxSpeedWithKart.Value) + ",,,,,,,";
                csv_Tornado[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TimeLimit.Value) + ",,,,,,,";
                csv_Tornado[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AirGravity.Value) + ",,,,,,,";
                csv_Tornado[7] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_GroundGravity.Value) + ",,,,,,,";
                csv_Tornado[8] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AirGravityMaglev.Value) + ",,,,,,,";
                csv_Tornado[9] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_GroundGravityMaglev.Value) + ",,,,,,,";
                csv_Tornado[10] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Acceleration.Value) + ",,,,,,,";
                csv_Tornado[11] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_AccelerationJuiced.Value) + ",,,,,,,";
                csv_Tornado[12] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TurnSpeed.Value) + ",,,,,,,";
                csv_Tornado[13] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TurnSpeedJuiced.Value) + ",,,,,,,";
                csv_Tornado[14] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Mass.Value) + ",,,,,,,";
                csv_Tornado[15] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_Radius.Value) + ",,,,,,,";
                csv_Tornado[16] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_DelayTrackingUpdate.Value) + ",,,,,,,";
                csv_Tornado[17] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_ViewRange.Value) + ",,,,,,,";
                csv_Tornado[18] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_RangeInFront.Value) + ",,,,,,,";
                csv_Tornado[19] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_RangeInBack.Value) + ",,,,,,,";
                csv_Tornado[20] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_LiftTime.Value) + ",,,,,,,";
                csv_Tornado[21] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_LiftForce.Value) + ",,,,,,,";
                csv_Tornado[22] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_FizzleTime.Value) + ",,,,,,,";
                csv_Tornado[23] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_Tornado[24] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_Tornado[25] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_DragCoef.Value) + ",,,,,,,";
                csv_Tornado[26] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_EasyLatFriction.Value) + ",,,,,,,";
                csv_Tornado[27] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_EasyLongFriction.Value) + ",,,,,,,";
                csv_Tornado[28] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_HardLatFriction.Value) + ",,,,,,,";
                csv_Tornado[29] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_HardLongFriction.Value) + ",,,,,,,";
                csv_Tornado[30] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_TargetAllDistance.Value) + ",,,,,,,";
                csv_Tornado[31] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.Tornado_m_ViewRangleOfTarget.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/tornado.csv", csv_Tornado);
            }
            if (Editing_CSV_TurboBoost)
            {
                string[] csv_TurboBoost = File.ReadAllLines(path_gob_extracted + "common/weapons/turboboost.csv");

                csv_TurboBoost[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TurboBoost_m_NormalTime.Value) + ",,,,,,,";
                csv_TurboBoost[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.TurboBoost_m_JuicedTime.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/turboboost.csv", csv_TurboBoost);
            }
            if (Editing_CSV_StaticShock)
            {
                string[] csv_StaticShock = File.ReadAllLines(path_gob_extracted + "common/weapons/staticshock.csv");

                csv_StaticShock[1] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_NormalTime.Value) + ",,,,,,,";
                csv_StaticShock[2] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_JuicedTime.Value) + ",,,,,,,";
                csv_StaticShock[3] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_NormalWumpaLoss.Value) + ",,,,,,,";
                csv_StaticShock[4] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_JuicedWumpaLoss.Value) + ",,,,,,,";
                csv_StaticShock[5] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_HomingSpeed.Value) + ",,,,,,,";
                csv_StaticShock[6] = CNK_Common.Float_To_CSV_Line(CNK_Data_Powerups.StaticShock_m_DistanceForHome.Value) + ",,,,,,,";

                File.WriteAllLines(path_gob_extracted + "common/weapons/staticshock.csv", csv_StaticShock);
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
