using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    static partial class CNK_Data
    {

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
            Nash = 16,
            Krunk = 17,
            EmperorVelo = 18,
            BigNorm = 19,
            SmallNorm = 20,
            Geary = 21,
            GearyMinion = 22,
            VeloMinion = 23,
        }
        public static string[] DriverTypes = new string[] { "coco", "crash", "cortex", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem" };
        public static string[] DriverModelTypes = new string[] { "coco", "crash", "ncortex", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem", "barinboss", "earthboss", "empvelo", "fenombigboss", "fenomlittleboss", "tekneeboss", "tekneeminion", "velominion" };
        public static string[] DriverAudioTypes = new string[] { "cob", "crb", "dnc", "cnb", "ddl", "fcb", "ngn", "oxd", "ntn", "ntp", "plr", "pur", "rvl", "tny", "zam", "zem", "nsh", "kgo", "vlo", "bnm", "lnm", "oto", "scr", "vlm" };

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
        

        public static void CNK_Randomize_SufParams(Random randState)
        {
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
            //todo
            Surface_m_SpeedAccelIncreasePercent[(int)SurfaceTypes.eSURFACETYPE_TRACK_DIRT_FAST] = 0.02f;
        }

        public static void CNK_Randomize_StaticShock(Random randState)
        {
            StaticShock_m_NormalTime = randState.Next(15, 45) * 100;
            StaticShock_m_JuicedTime = StaticShock_m_NormalTime * 2;
            StaticShock_m_NormalWumpaLoss = randState.Next(1, 5);
            StaticShock_m_JuicedWumpaLoss = StaticShock_m_NormalWumpaLoss + randState.Next(0, 2);
            StaticShock_m_HomingSpeed = randState.Next(9,15);
            StaticShock_m_DistanceForHome = randState.Next(9,15);
        }

        public static void CNK_Randomize_PowerShield(Random randState)
        {
            PowerShield_m_Time = randState.Next(4, 16) * 1000f;
            PowerShield_m_ZapSpeed = randState.Next(6, 12);
            //Sadly, it doesn't seem like this actually changes the shield's color
            PowerShield_m_ColorNonJuiced[0] = (float)randState.NextDouble(); 
            PowerShield_m_ColorNonJuiced[1] = (float)randState.NextDouble();
            PowerShield_m_ColorNonJuiced[2] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[0] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[1] = (float)randState.NextDouble();
            PowerShield_m_ColorJuiced[2] = (float)randState.NextDouble();
            PowerShield_m_RangeForZapping = randState.Next(50, 100) / 10f;
            PowerShield_m_ColRadius = PowerShield_m_RangeForZapping / 4.3f;
        }

        public static void CNK_Randomize_TurboBoost(Random randState)
        {
            TurboBoost_m_NormalTime = randState.Next(60,120) * 100f;
            TurboBoost_m_JuicedTime = TurboBoost_m_NormalTime * 1.5f;
        }

        public static void CNK_Randomize_TNTCrate(Random randState)
        {
            TNT_m_Time = 4300;
            TNT_m_TimeBeforeHiddenChar = 2500;
            TNT_m_TimeHiddenChar = 4300;
            TNT_m_NormalWumpaLoss = randState.Next(1, 5);
            TNT_m_JuicedWumpaLoss = TNT_m_JuicedWumpaLoss + randState.Next(0, 2);
            TNT_m_ExplosionBlastRadius = randState.Next(300, 800) / 100f;
            TNT_m_ExplScale = (float)randState.NextDouble() + 0.25f;
            TNT_m_ExplScaleJuiced = TNT_m_ExplScale + 0.1f;
        }

        public static void CNK_Randomize_FreezingMine(Random randState)
        {
            FreezingMine_m_NormalFreezeTime = randState.Next(15, 45) * 100;
            FreezingMine_m_JuicedFreezeTime = randState.Next(9, 15) * 1000;
            FreezingMine_m_NormalWumpaFruitLost = randState.Next(1, 5);
            FreezingMine_m_JuicedWumpaFruitLost = FreezingMine_m_NormalWumpaFruitLost + randState.Next(0, 2);
            FreezingMine_m_ThrowDistance = randState.Next(16, 32);
            FreezingMine_m_ThrowSpeedFactor = (float)randState.NextDouble() + 0.75f;
            FreezingMine_m_BlastRadius = (float)randState.NextDouble() + randState.Next(1, 5);
            FreezingMine_m_BlastRadiusJuiced = FreezingMine_m_BlastRadius * 2f;
            FreezingMine_m_ExplScale = (float)randState.NextDouble() + 0.1f;
            FreezingMine_m_ExplScaleJuiced = FreezingMine_m_ExplScale * 2f;
        }

        public static void CNK_Randomize_RedEye(Random randState)
        {
            RedEye_Acceleration = randState.Next(25,75) + (float)randState.NextDouble();
            RedEye_Deceleration = randState.Next(10,20) + (float)randState.NextDouble();
            RedEye_MaxSpeed = randState.Next(45,75);
            RedEye_MinSpeed = randState.Next(24,32);
            RedEye_TurnSpeed = randState.Next(8,12) + (float)randState.NextDouble();
            RedEye_Explosion_Radius = randState.Next(2,5) + (float)randState.NextDouble();
            RedEye_TurnSpeedJuiced = RedEye_TurnSpeed + 2f;
            RedEye_Explosion_Radius_Juiced = RedEye_Explosion_Radius + 4f;
            RedEye_Expl_Scale = (float)randState.NextDouble() + 0.1f;
            RedEye_Expl_Scale_Juiced = RedEye_Expl_Scale * 2f;
            RedEye_FullSpeedTurnSlowdown = 4f;
        }

        public static void CNK_Randomize_InvincMask(Random randState)
        {
            InvincMask_m_NormalTime = randState.Next(60, 100) * 100;
            InvincMask_m_JuicedTime = InvincMask_m_NormalTime + ((int)Math.Ceiling(InvincMask_m_NormalTime/2f));
            InvincMask_m_NormalTimeTeamed = randState.Next(80, 160) * 100;
            InvincMask_m_JuicedTimeTeamed = InvincMask_m_NormalTimeTeamed + ((int)Math.Ceiling(InvincMask_m_NormalTimeTeamed / 3f));
            InvincMask_m_NormalWumpaLoss = randState.Next(1, 5);
            InvincMask_m_JuicedWumpaLoss = InvincMask_m_NormalWumpaLoss + randState.Next(0, 2);
            InvincMask_m_TeamSpeed = 15f;
            InvincMask_m_TeamBlastRange = 40f;
            InvincMask_m_TeamMeterFull = 5f;
            InvincMask_m_ExplScale = (float)randState.NextDouble() + 0.5f;
            InvincMask_m_ExplScaleJuiced = 1.5f * InvincMask_m_ExplScale;
            InvincMask_m_ColRadius = randState.Next(1, 3) + (float)randState.NextDouble();
        }

        public static void CNK_Randomize_BowlingBomb(Random randState)
        {
            BowlingBomb_m_Speed = randState.Next(50,80);
            BowlingBomb_m_Acceleration = randState.Next(70,90);
            BowlingBomb_m_AccelerationJuiced = BowlingBomb_m_Acceleration * 1.125f;
            BowlingBomb_m_Mass = 2500f;
            BowlingBomb_m_Radius = 1f;
            BowlingBomb_m_AirGravity = 12f;
            BowlingBomb_m_GroundGravity = 4f;
            BowlingBomb_m_AirGravityMaglev = 17f;
            BowlingBomb_m_GroundGravityMaglev = 15f;
            BowlingBomb_m_TurnSpeed = 0.9f;
            BowlingBomb_m_TurnSpeedJuiced = BowlingBomb_m_TurnSpeed;
            BowlingBomb_m_ViewRange = 0.993f;
            BowlingBomb_m_RangeInFront = 150f;
            BowlingBomb_m_NormalWumpaLoss = randState.Next(1, 5);
            BowlingBomb_m_JuicedWumpaLoss = BowlingBomb_m_NormalWumpaLoss + randState.Next(0, 2);
            BowlingBomb_m_ExplosionBlastRadius = randState.Next(25, 75) / 10f;
            BowlingBomb_m_ExplosionBlastRadiusJuiced = BowlingBomb_m_ExplosionBlastRadius * 1.6f;
            BowlingBomb_m_DragCoef = 0.00125f;
            BowlingBomb_m_EasyLatFriction = 30f;
            BowlingBomb_m_EasyLongFriction = 1f;
            BowlingBomb_m_HardLatFriction = 30f;
            BowlingBomb_m_HardLongFriction = 1f;
            BowlingBomb_m_BackSpeed = randState.Next(30, 60);
            BowlingBomb_m_ExplScale = (float)randState.NextDouble() + 0.5f;
            BowlingBomb_m_ExplScaleJuiced = BowlingBomb_m_ExplScale * 1.5f;
        }

        public static void CNK_Randomize_HomingMissle(Random randState)
        {
            HomingMissle_m_TrackingFrontDistance = 50f;
            HomingMissle_m_MaxSpeed = randState.Next(40,80);
            HomingMissle_m_MaxSpeedJuiced = HomingMissle_m_MaxSpeed * (70f/60f);
            HomingMissle_m_TimeLimit = 15000;
            HomingMissle_m_AirGravityNormal = 8f;
            HomingMissle_m_GroundGravityNormal = 1.25f;
            HomingMissle_m_AirGravityMaglev = 8f;
            HomingMissle_m_GroundGravityMaglev = HomingMissle_m_AirGravityMaglev;
            HomingMissle_m_Acceleration = randState.Next(35,55);
            HomingMissle_m_AccelerationJuiced = HomingMissle_m_Acceleration * (55f/45f);
            HomingMissle_m_TurnSpeed = 5f;
            HomingMissle_m_TurnSpeedJuiced = HomingMissle_m_TurnSpeed * (8f/5f);
            HomingMissle_m_Mass = 1000f;
            HomingMissle_m_Radius = 1f;
            HomingMissle_m_DelayTrackingUpdate = 100;
            HomingMissle_m_ViewRange = 0.2f;
            HomingMissle_m_RangeInFront = 140;
            HomingMissle_m_RangeInBack = 0f;
            HomingMissle_m_NormalWumpaLoss = randState.Next(1, 5);
            HomingMissle_m_JuicedWumpaLoss = HomingMissle_m_NormalWumpaLoss + randState.Next(0, 2);
            HomingMissle_m_ExplosionBlastRadius = 1f;
            HomingMissle_m_ExplosionBlastRadiusJuiced = HomingMissle_m_ExplosionBlastRadius;
            HomingMissle_m_DragCoef = 0.00125f;
            HomingMissle_m_EasyLatFriction = 15f;
            HomingMissle_m_EasyLongFriction = 1f;
            HomingMissle_m_HardLatFriction = 55f;
            HomingMissle_m_HardLongFriction = 1f;
            HomingMissle_m_DecayTime = 5000;
            HomingMissle_m_DecaySpeed = 2f;
            HomingMissle_m_DecayMin = 40f;
            HomingMissle_m_ExplScale = 0.45f;
            HomingMissle_m_ExplScaleJuiced = HomingMissle_m_ExplScale;
        }

        public static void CNK_Randomize_Tornado(Random randState)
        {
            Tornado_m_TrackingFrontDistance = 35f;
            Tornado_m_MaxSpeed = randState.Next(45,60);
            Tornado_m_MaxSpeedJuiced = Tornado_m_MaxSpeed;
            Tornado_m_MaxSpeedWithKart = 10f;
            Tornado_m_TimeLimit = 30000;
            Tornado_m_AirGravity = 6f;
            Tornado_m_GroundGravity = 1.5f;
            Tornado_m_AirGravityMaglev = Tornado_m_AirGravity;
            Tornado_m_GroundGravityMaglev = 8f;
            Tornado_m_Acceleration = randState.Next(40,60);
            Tornado_m_AccelerationJuiced = Tornado_m_Acceleration;
            Tornado_m_TurnSpeed = randState.Next(6,10);
            Tornado_m_TurnSpeedJuiced = Tornado_m_TurnSpeed;
            Tornado_m_Mass = 50f;
            Tornado_m_Radius = 2.5f;
            Tornado_m_DelayTrackingUpdate = 100;
            Tornado_m_ViewRange = 0f;
            Tornado_m_RangeInFront = 0f;
            Tornado_m_RangeInBack = 0f;
            Tornado_m_LiftTime = 3000;
            Tornado_m_LiftForce = 30f;
            Tornado_m_FizzleTime = 1000;
            Tornado_m_NormalWumpaLoss = randState.Next(3, 7);
            Tornado_m_JuicedWumpaLoss = Tornado_m_NormalWumpaLoss + randState.Next(0, 2);
            Tornado_m_DragCoef = 0.01f;
            Tornado_m_EasyLatFriction = 30f;
            Tornado_m_EasyLongFriction = 1f;
            Tornado_m_HardLatFriction = 50f;
            Tornado_m_HardLongFriction = 1f;
            Tornado_m_TargetAllDistance = 18f;
            Tornado_m_ViewRangleOfTarget = 0.707f;
        }

        public static void CNK_Randomize_WeaponSelection(Random randState)
        {
            //todo
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
        }

        public static void CNK_Randomize_KartStats(Random randState)
        {
            double target_val = 0;
            target_val = randState.NextDouble() + 0.5;
            //todo

            //Accel
            m_AccelerationGainNormal = randState.Next(24, 29) + (float)randState.NextDouble(); // 26f;
            m_AccelerationGainWumpa = m_AccelerationGainNormal + 3.5f;

            //Aku Respawn
            m_AkuDropHeight = 3f;
            m_AkuDropSpeed = 2f;
            m_AkuDropTime = 1f;
            m_AkuDropTS_m_CancelMinPercent = 0f;
            m_AkuDropTS_m_DecHoldTime = 1.2f;
            m_AkuDropTS_m_DecSpeed = 0.4f;
            m_AkuDropTS_m_IncSpeed = 1f;
            m_AkuDropTS_m_MaxHoldTime = 1f;
            m_AkuDropTS_m_MaxRepressTime = 0.18f;
            m_AkuDropTS_m_Quadratic[0] = 0f;
            m_AkuDropTS_m_Quadratic[1] = 1f;
            m_AkuDropTS_m_Quadratic[2] = 0f;

            m_BoostInARowTimeTol = 0.2f; //Uncapped reserves

            //Boost sources speed, length
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

            m_BoostMaxImpulsePerSecond = 120f; //Uncapped reserves
            m_BoostMaxTimeCap = 999f; //Uncapped reserves

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

            m_MaxForwardSpeedNormal = randState.Next(23,31) + (float)randState.NextDouble();
            m_MaxForwardSpeedWumpa = m_MaxForwardSpeedNormal + 3f;

            m_MaxLinearVelXY = 52f; // Extended speed cap, too high causes physics instability
            m_MaxLinearVelZ = 52f;

            m_MaxReverseSpeed = randState.Next(8, 15) + (float)randState.NextDouble();// 10f;

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
            m_SlideMaxBoostCount = 3; // If you go over 3 boosts it only affects the boost meter, doesn't let you boost more
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
            m_TurnRateNormal = randState.Next(60, 80);// 70f;
            m_TurnRateWumpa = m_TurnRateNormal;

            m_WaitBeforeBrakeReverses = 0.225f;

            m_WheelieMinTime = 0.75f;
            m_WheelieSlideBoostMinPercent = 0.25f;

        }

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

            c_BoostMaxImpulsePerSecond[targetDriver] = randState.Next(1,4);
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
            c_SlideFrictionNorm[targetDriver, 0] = randState.Next(5,10) / 10f;
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

        public static void CNK_Randomize_ReqsRewards(Random randState)
        {
            Adv_TracksManager_EntryList.Clear();
            //todo
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth1, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Trophy, RewardID.Trophy, 0));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth2, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Trophy, RewardID.Trophy, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.CNK_Challenge, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Earth3, SubModeID.Relic, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_EarthBoss, SubModeID.Boss, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_EarthArena, SubModeID.Crystal, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Trophy, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin1, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Trophy, RewardID.Trophy, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin2, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Trophy, RewardID.Trophy, 5));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.CNK_Challenge, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Barin3, SubModeID.Relic, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_BarinBoss, SubModeID.Boss, RewardID.Trophy, 6));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_BarinArena, SubModeID.Crystal, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Trophy, RewardID.Trophy, 6));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena1, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Trophy, RewardID.Trophy, 7));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena2, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Trophy, RewardID.Trophy, 8));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.CNK_Challenge, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Fenomena3, SubModeID.Relic, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_FenomenaBoss, SubModeID.Boss, RewardID.Trophy, 9));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_FenomenaArena, SubModeID.Crystal, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Trophy, RewardID.Trophy, 9));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee1, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Trophy, RewardID.Trophy, 10));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee2, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Trophy, RewardID.Trophy, 11));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.CNK_Challenge, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_Teknee3, SubModeID.Relic, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_TekneeBoss, SubModeID.Boss, RewardID.Trophy, 12));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Arena_TekneeArena, SubModeID.Crystal, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Boss_VeloBoss, SubModeID.Boss, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Barin, SubModeID.Trophy, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Barin, SubModeID.Trophy, RewardID.Key, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Fenomena_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Barin_To_Fenomena, SubModeID.Trophy, RewardID.Key, 2));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Earth_To_Teknee, SubModeID.Trophy, RewardID.Key, 3));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Red, SubModeID.Gem, RewardID.Token_Red, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Blue, SubModeID.Gem, RewardID.Token_Blue, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Green, SubModeID.Gem, RewardID.Token_Green, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.GemCup_Purple, SubModeID.Gem, RewardID.Token_Purple, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Warp_Citadel_To_Vault, SubModeID.Trophy, RewardID.Key, 4));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Blue, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Red, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Green, 1));
            Adv_TracksManager_EntryList.Add(new AdvTracksManagerEntry(PadInfoNameID.Track_VeloRace, SubModeID.Relic, RewardID.Gem_Purple, 1));

            Adv_GoalsToRewards_EntryList.Add(new GoalsToRewardsEntry(TrackID.Arena_5, SubModeID.Crystal, RewardID.Token_Purple)); // Adds unused Terra Drome crystal challenge
            for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
            {
                Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(Adv_GoalsToRewards_EntryList[i].Track, Adv_GoalsToRewards_EntryList[i].Submode, Adv_GoalsToRewards_EntryList[i].Reward);
            }
        }

        public static void CNK_Randomize_WarpPads(Random randState)
        {
            //todo: hubs? boss cutscenes, velorace testing

            //Tracks
            List<TrackID> ValidTracks = new List<TrackID>();
            List<PadInfoDescID> ValidDesc = new List<PadInfoDescID>();
            ValidTracks.Add(TrackID.Earth_1);
            ValidDesc.Add(PadInfoDescID.world_earth1);
            ValidTracks.Add(TrackID.Earth_2);
            ValidDesc.Add(PadInfoDescID.world_earth2);
            ValidTracks.Add(TrackID.Earth_3);
            ValidDesc.Add(PadInfoDescID.world_earth3);
            ValidTracks.Add(TrackID.Barin_1);
            ValidDesc.Add(PadInfoDescID.world_barin1);
            ValidTracks.Add(TrackID.Barin_2);
            ValidDesc.Add(PadInfoDescID.world_barin2);
            ValidTracks.Add(TrackID.Barin_3);
            ValidDesc.Add(PadInfoDescID.world_barin3);
            ValidTracks.Add(TrackID.Fenom_1);
            ValidDesc.Add(PadInfoDescID.world_fenom1);
            ValidTracks.Add(TrackID.Fenom_2);
            ValidDesc.Add(PadInfoDescID.world_fenom2);
            ValidTracks.Add(TrackID.Fenom_3);
            ValidDesc.Add(PadInfoDescID.world_fenom3);
            ValidTracks.Add(TrackID.Teknee_1);
            ValidDesc.Add(PadInfoDescID.world_teknee1);
            ValidTracks.Add(TrackID.Teknee_2);
            ValidDesc.Add(PadInfoDescID.world_teknee2);
            ValidTracks.Add(TrackID.Teknee_3);
            ValidDesc.Add(PadInfoDescID.world_teknee3);
            //ValidTracks.Add(TrackID.VeloRace); testing required
            //ValidDesc.Add(PadInfoDescID.world_velo);

            PadInfoNameID[] ValidReplacements = new PadInfoNameID[]
            {
                PadInfoNameID.Track_Earth1,
                PadInfoNameID.Track_Earth2,
                PadInfoNameID.Track_Earth3,
                PadInfoNameID.Track_Barin1,
                PadInfoNameID.Track_Barin2,
                PadInfoNameID.Track_Barin3,
                PadInfoNameID.Track_Fenomena1,
                PadInfoNameID.Track_Fenomena2,
                PadInfoNameID.Track_Fenomena3,
                PadInfoNameID.Track_Teknee1,
                PadInfoNameID.Track_Teknee2,
                PadInfoNameID.Track_Teknee3,
            };

            int targetPos;
            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacements.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidTracks.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDesc[targetPos], ValidTracks[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidTracks.RemoveAt(targetPos);
                    ValidDesc.RemoveAt(targetPos);
                }
            }

            //Crystal Arenas
            List<TrackID> ValidArenas = new List<TrackID>();
            List<PadInfoDescID> ValidDescArena = new List<PadInfoDescID>();
            ValidArenas.Add(TrackID.Arena_1);
            ValidArenas.Add(TrackID.Arena_2);
            ValidArenas.Add(TrackID.Arena_3);
            ValidArenas.Add(TrackID.Arena_4);
            ValidArenas.Add(TrackID.Arena_5);
            ValidDescArena.Add(PadInfoDescID.world_arena1);
            ValidDescArena.Add(PadInfoDescID.world_arena2);
            ValidDescArena.Add(PadInfoDescID.world_arena3);
            ValidDescArena.Add(PadInfoDescID.world_arena4);
            ValidDescArena.Add(PadInfoDescID.world_arena5);

            PadInfoNameID[] ValidReplacementsArena = new PadInfoNameID[]
            {
                PadInfoNameID.Arena_EarthArena,
                PadInfoNameID.Arena_BarinArena,
                PadInfoNameID.Arena_FenomenaArena,
                PadInfoNameID.Arena_TekneeArena,
            };

            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacementsArena.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidArenas.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDescArena[targetPos], ValidArenas[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidArenas.RemoveAt(targetPos);
                    ValidDescArena.RemoveAt(targetPos);
                }
            }

            //Bosses
            List<TrackID> ValidBosses = new List<TrackID>();
            List<PadInfoDescID> ValidDescBoss = new List<PadInfoDescID>();
            ValidBosses.Add(TrackID.Earth_2);
            ValidBosses.Add(TrackID.Barin_3);
            ValidBosses.Add(TrackID.Fenom_1);
            ValidBosses.Add(TrackID.Teknee_2);
            ValidDescBoss.Add(PadInfoDescID.warp_kongo);
            ValidDescBoss.Add(PadInfoDescID.warp_nash);
            ValidDescBoss.Add(PadInfoDescID.warp_norm);
            ValidDescBoss.Add(PadInfoDescID.warp_otto);
            //ValidBosses.Add(TrackID.VeloRace);
            //ValidDescBoss.Add(PadInfoDescID.velo_race_title);

            PadInfoNameID[] ValidReplacementsBoss = new PadInfoNameID[]
            {
                PadInfoNameID.Boss_EarthBoss,
                PadInfoNameID.Boss_BarinBoss,
                PadInfoNameID.Boss_FenomenaBoss,
                PadInfoNameID.Boss_TekneeBoss,
            };

            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (ValidReplacementsBoss.Contains(Adv_WarpPadInfo_EntryList[i].PadName))
                {
                    targetPos = randState.Next(0, ValidBosses.Count);
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, ValidDescBoss[targetPos], ValidBosses[targetPos], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                    ValidBosses.RemoveAt(targetPos);
                    ValidDescBoss.RemoveAt(targetPos);
                }
            }

            //Gem Cups
            List<TrackID> GemTracks = new List<TrackID>();
            GemTracks.Add(TrackID.Earth_1);
            GemTracks.Add(TrackID.Earth_2);
            GemTracks.Add(TrackID.Earth_3);
            GemTracks.Add(TrackID.Barin_1);
            GemTracks.Add(TrackID.Barin_2);
            GemTracks.Add(TrackID.Barin_3);
            GemTracks.Add(TrackID.Fenom_1);
            GemTracks.Add(TrackID.Fenom_2);
            GemTracks.Add(TrackID.Fenom_3);
            GemTracks.Add(TrackID.Teknee_1);
            GemTracks.Add(TrackID.Teknee_2);
            GemTracks.Add(TrackID.Teknee_3);
            //GemTracks.Add(TrackID.VeloRace); testing required

            for (int i = 0; i < GemCup_Red.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Red[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Blue.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Blue[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Green.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Green[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }
            for (int i = 0; i < GemCup_Purple.Length; i++)
            {
                targetPos = randState.Next(0, GemTracks.Count);
                GemCup_Purple[i] = GemTracks[targetPos];
                GemTracks.RemoveAt(targetPos);
            }

            for (int i = 0; i < Adv_GoalsToRewards_EntryList.Count; i++)
            {
                if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Blue)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Blue[0], SubModeID.Gem, RewardID.Gem_Blue);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Green)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Green[0], SubModeID.Gem, RewardID.Gem_Green);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Purple)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Purple[0], SubModeID.Gem, RewardID.Gem_Purple);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Red)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Red[0], SubModeID.Gem, RewardID.Gem_Red);
                }
                else if (Adv_GoalsToRewards_EntryList[i].Reward == RewardID.Gem_Yellow)
                {
                    Adv_GoalsToRewards_EntryList[i] = new GoalsToRewardsEntry(GemCup_Yellow[0], SubModeID.Gem, RewardID.Gem_Yellow);
                }
            }
            for (int i = 0; i < Adv_WarpPadInfo_EntryList.Count; i++)
            {
                if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Red)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Red[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Blue)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Blue[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Green)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Green[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Purple)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Purple[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
                else if (Adv_WarpPadInfo_EntryList[i].PadName == PadInfoNameID.GemCup_Yellow)
                {
                    Adv_WarpPadInfo_EntryList[i] = new WarpPadInfoEntry(Adv_WarpPadInfo_EntryList[i].PadName, Adv_WarpPadInfo_EntryList[i].PadDesc, GemCup_Yellow[0], Adv_WarpPadInfo_EntryList[i].isWarpGate, Adv_WarpPadInfo_EntryList[i].PrimaryActEvent, Adv_WarpPadInfo_EntryList[i].SecondaryEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent, Adv_WarpPadInfo_EntryList[i].LockedEvent2, Adv_WarpPadInfo_EntryList[i].BaseRewardEvent, Adv_WarpPadInfo_EntryList[i].RelicWonEvent, Adv_WarpPadInfo_EntryList[i].TokenWonEvent);
                }
            }
        }
    }

    
}
