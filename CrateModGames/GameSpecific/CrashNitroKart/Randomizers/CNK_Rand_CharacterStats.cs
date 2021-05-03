using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_CharacterStats : ModStruct<CSV>
    {
        private bool isRand = false;

        public CNK_Rand_CharacterStats()
        {
            isRand = CNK_Props_Main.Option_RandCharStats.Enabled;
        }


        public override void BeforeModPass()
        {
            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);
            if (!isRand)
            {
                return;
            }
            for (int i = 0; i < 16; i++)
            {
                Randomize_CharacterStats(randState, i);
            }
        }

        void Randomize_CharacterStats(Random randState, int targetDriver)
        {
            //Boost sources speed, length
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_AKU_DROP.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_LARGE.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_MEDIUM.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_SMALL.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_PAD.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_PAD.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_PAD.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_1.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_2.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_3.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_START.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_START.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_START.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_SUPER_ENGINE.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 0] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 1] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value[targetDriver, 2] = (float)randState.NextDouble() + 0.75f;

            CNK_Data_DriverStats.c_BoostMaxImpulsePerSecond.Value[targetDriver] = randState.Next(1, 4);
            CNK_Data_DriverStats.c_BoostSlidePushAngle.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_BoostSlidePushTime.Value[targetDriver] = 1;

            CNK_Data_DriverStats.c_BrakeForce.Value[targetDriver] = 1;

            CNK_Data_DriverStats.c_HiTurnFriction.Value[targetDriver, 0] = 1;
            CNK_Data_DriverStats.c_HiTurnFriction.Value[targetDriver, 1] = 1;
            CNK_Data_DriverStats.c_HiTurnFriction.Value[targetDriver, 2] = 1;
            CNK_Data_DriverStats.c_HiTurnStartAngle.Value[targetDriver] = 1;

            CNK_Data_DriverStats.c_InAirTurnRateNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.6f;
            CNK_Data_DriverStats.c_InAirTurnRateWumpa.Value[targetDriver] = CNK_Data_DriverStats.c_InAirTurnRateNormal.Value[targetDriver] + 0.2f;

            CNK_Data_DriverStats.c_NormalFriction.Value[targetDriver, 0] = 1;
            CNK_Data_DriverStats.c_NormalFriction.Value[targetDriver, 1] = 1;
            CNK_Data_DriverStats.c_NormalFriction.Value[targetDriver, 2] = 1;

            CNK_Data_DriverStats.c_SlideFrictionHigh.Value[targetDriver, 0] = (float)(randState.NextDouble() / 5f) + 0.75f;
            CNK_Data_DriverStats.c_SlideFrictionHigh.Value[targetDriver, 1] = CNK_Data_DriverStats.c_SlideFrictionHigh.Value[targetDriver, 0];
            CNK_Data_DriverStats.c_SlideFrictionHigh.Value[targetDriver, 2] = CNK_Data_DriverStats.c_SlideFrictionHigh.Value[targetDriver, 0];
            CNK_Data_DriverStats.c_SlideFrictionLow.Value[targetDriver, 0] = 1;
            CNK_Data_DriverStats.c_SlideFrictionLow.Value[targetDriver, 1] = 1;
            CNK_Data_DriverStats.c_SlideFrictionLow.Value[targetDriver, 2] = 1;
            CNK_Data_DriverStats.c_SlideFrictionNorm.Value[targetDriver, 0] = randState.Next(5, 10) / 10f;
            CNK_Data_DriverStats.c_SlideFrictionNorm.Value[targetDriver, 1] = CNK_Data_DriverStats.c_SlideFrictionNorm.Value[targetDriver, 0];
            CNK_Data_DriverStats.c_SlideFrictionNorm.Value[targetDriver, 2] = CNK_Data_DriverStats.c_SlideFrictionNorm.Value[targetDriver, 0];

            CNK_Data_DriverStats.c_SlideMaxAngle.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_SlideMinAngle.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_SlideTurnRateAwayFromSlide.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_SlideTurnRateInToSlide.Value[targetDriver] = 1;

            CNK_Data_DriverStats.c_TurnDecellForce.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_TurnDecellForceMax.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_TurnDecellSpeed.Value[targetDriver] = 1;

            CNK_Data_DriverStats.c_TurnRateAccel.Value[targetDriver] = 1;
            CNK_Data_DriverStats.c_TurnRateBrake.Value[targetDriver] = (float)(randState.NextDouble() / 2f) + 1f;

            //Speed
            CNK_Data_DriverStats.c_MaxForwardSpeedNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.9f;
            CNK_Data_DriverStats.c_MaxForwardSpeedWumpa.Value[targetDriver] = CNK_Data_DriverStats.c_MaxForwardSpeedNormal.Value[targetDriver] + 0.01f;

            //Accel
            CNK_Data_DriverStats.c_AccelerationGainNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.75f;
            CNK_Data_DriverStats.c_AccelerationGainWumpa.Value[targetDriver] = CNK_Data_DriverStats.c_AccelerationGainNormal.Value[targetDriver] + 0.01f;

            //Turning
            CNK_Data_DriverStats.c_TurnRateNormal.Value[targetDriver] = (float)randState.NextDouble() + 0.6f;
            CNK_Data_DriverStats.c_TurnRateWumpa.Value[targetDriver] = CNK_Data_DriverStats.c_TurnRateNormal.Value[targetDriver] + 0.01f;

            //UI Stats
            CNK_Data_DriverStats.c_UIStats_MaxValue.Value[targetDriver] = 7;
            CNK_Data_DriverStats.c_UIStats_Speed.Value[targetDriver] = (int)Math.Ceiling((CNK_Data_DriverStats.c_MaxForwardSpeedNormal.Value[targetDriver] / 1.9) * CNK_Data_DriverStats.c_UIStats_MaxValue.Value[targetDriver]);
            CNK_Data_DriverStats.c_UIStats_Acceleration.Value[targetDriver] = (int)Math.Ceiling((CNK_Data_DriverStats.c_AccelerationGainNormal.Value[targetDriver] / 1.75) * CNK_Data_DriverStats.c_UIStats_MaxValue.Value[targetDriver]);
            CNK_Data_DriverStats.c_UIStats_Turn.Value[targetDriver] = (int)Math.Ceiling((CNK_Data_DriverStats.c_TurnRateNormal.Value[targetDriver] / 1.6) * CNK_Data_DriverStats.c_UIStats_MaxValue.Value[targetDriver]);

        }

        public override void ModPass(CSV file)
        {
            if (file.FullName.ToLower().Contains("common/physics/kp") || file.FullName.ToLower().Contains(@"common\physics\kp"))
            {
                int csv_pos = 0;
                for (int p = 0; p < CNK_Common.DriverTypes.Length; p++)
                {
                    if (file.Name.ToLower().Contains("kp" + CNK_Common.DriverTypes[p]))
                    {
                        csv_pos = p;
                        break;
                    }
                }
                List<List<string>> table = file.Table;

                table[(int)KartPhysicsCharacterRows.c_AccelerationGainNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_AccelerationGainNormal.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_AccelerationGainNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_AccelerationGainNormal.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_AccelerationGainWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_AccelerationGainWumpa.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_AKU_DROP] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_AKU_DROP.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_LARGE] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_LARGE.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_MEDIUM] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_MEDIUM.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_JUMP_SMALL] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_JUMP_SMALL.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_PAD] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_PAD.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_1] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_1.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_2] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_2.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SLIDE_3] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SLIDE_3.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_START] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_START.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_SUPER_ENGINE] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_SUPER_ENGINE.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostInfo_eBOOST_TURBOBOOST_JUICED] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_BoostInfo_eBOOST_TURBOBOOST_JUICED.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_BoostMaxImpulsePerSecond][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_BoostMaxImpulsePerSecond.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_BoostSlidePushAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_BoostSlidePushAngle.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_BoostSlidePushTime][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_BoostSlidePushTime.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_BrakeForce][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_BrakeForce.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_HiTurnFriction] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_HiTurnFriction.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_HiTurnStartAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_HiTurnStartAngle.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_InAirTurnRateNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_InAirTurnRateNormal.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_InAirTurnRateWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_InAirTurnRateWumpa.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_MaxForwardSpeedNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_MaxForwardSpeedNormal.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_MaxForwardSpeedWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_MaxForwardSpeedWumpa.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_NormalFriction] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_NormalFriction.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_SlideFrictionHigh] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_SlideFrictionHigh.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_SlideFrictionLow] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_SlideFrictionLow.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_SlideFrictionNorm] = CNK_Common.FloatArray2_To_CSV_FullLine(CNK_Data_DriverStats.c_SlideFrictionNorm.Value, csv_pos);
                table[(int)KartPhysicsCharacterRows.c_SlideMaxAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_SlideMaxAngle.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_SlideMinAngle][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_SlideMinAngle.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_SlideTurnRateAwayFromSlide][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_SlideTurnRateAwayFromSlide.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_SlideTurnRateInToSlide][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_SlideTurnRateInToSlide.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnDecellForce][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnDecellForce.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnDecellForceMax][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnDecellForceMax.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnDecellSpeed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnDecellSpeed.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnRateAccel][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnRateAccel.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnRateBrake][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnRateBrake.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnRateNormal][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnRateNormal.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_TurnRateWumpa][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_TurnRateWumpa.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_UIStats_Acceleration][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_UIStats_Acceleration.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_UIStats_Speed][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_UIStats_Speed.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_UIStats_Turn][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_UIStats_Turn.Value[csv_pos]);
                table[(int)KartPhysicsCharacterRows.c_UIStats_MaxValue][0] = CNK_Common.Float_To_CSV_Word(CNK_Data_DriverStats.c_UIStats_MaxValue.Value[csv_pos]);


            }

        }

    }
}
