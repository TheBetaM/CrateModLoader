using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public enum Drivers
    {
        Coco = 0,
        Cortex = 1,
        Crash = 2,
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

    public enum PowerupTypes
    {
        EXPLOSIVE_CRATE = 0,
        FREEZING_MINE = 1,
        HOMING_MISSLE = 2,
        BOWLING_BOMB = 3,
        TORNADO = 4,
        STATIC_SHOCK = 5,
        POWER_SHIELD = 6,
        INVINCIBILITY_MASK = 7,
        INVISIBILITY = 8,
        VOODOO_DOLL = 9, //Dummied out in retail :(
        TROPY_CLOCKS = 10,
        TURBO_BOOSTS = 11,
        SUPER_ENGINE = 12,
        REDEYE = 13,
        HOMING_MISSLE_X3 = 14,
        BOWLING_BOMB_X3 = 15,
        TURBO_BOOST_X3 = 16,
        EXPCRATE_X3 = 17,
        FREEZEMINE_X3 = 18,
        STATICSHOCK_X3 = 19,
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

    public enum PadInfoNameID
    {
        Track_Earth1 = 0,
        Track_Earth2 = 1,
        Track_Earth3 = 2,
        Boss_EarthBoss = 3,
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
        GemCup_Yellow = 44,
    }

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

    public enum WeaponSelectionRows
    {
        Track_Earth_1 = 11,
        Track_Earth_2 = 12,
        Track_Earth_3 = 13,
        Track_Barin_1 = 14,
        Track_Barin_2 = 15,
        Track_Barin_3 = 16,
        Track_Fenom_1 = 17,
        Track_Fenom_2 = 18,
        Track_Fenom_3 = 19,
        Track_Teknee_1 = 20,
        Track_Teknee_2 = 21,
        Track_Teknee_3 = 22,
        Track_VeloRace = 23,
        Track_Arena_1 = 24,
        Track_Arena_2 = 25,
        Track_Arena_3 = 26,
        Track_Arena_4 = 27,
        Track_Arena_5 = 28,
        Track_Arena_6 = 29,
        Track_Arena_7 = 30,
        Track_Lobby = 31,
        Mode_Adv_Trophy = 36,
        Mode_Adv_CNK = 37,
        Mode_Adv_Gem = 38,
        Mode_Adv_Boss = 39,
        Mode_Adv_Crystal = 40,
        Mode_Arcade = 41,
        Mode_Versus = 42,
        Mode_CrystalRace = 43,
        Mode_Battle_Point = 44,
        Mode_Battle_Time = 45,
        Mode_Battle_Domination = 46,
        Mode_Battle_CTF = 47,
        Mode_Battle_KOTR = 48,
        Mode_Battle_Crystal = 49,
        Mode_Lobby = 50,
        Rank_1st = 56,
        Rank_2nd = 57,
        Rank_3rd = 58,
        Rank_4th = 59,
        Rank_5th = 60,
        Rank_6th = 61,
        Rank_7th = 62,
        Rank_8th = 63,
        Progress_0 = 71,
        Progress_5 = 72,
        Progress_10 = 73,
        Progress_15 = 74,
        Progress_20 = 75,
        Progress_25 = 76,
        Progress_30 = 77,
        Progress_35 = 78,
        Progress_40 = 79,
        Progress_45 = 80,
        Progress_50 = 81,
        Progress_55 = 82,
        Progress_60 = 83,
        Progress_65 = 84,
        Progress_70 = 85,
        Progress_75 = 86,
        Progress_80 = 87,
        Progress_85 = 88,
        Progress_90 = 89,
        Progress_95 = 90,
        ActiveWep_EXPLOSIVE_CRATE = 101,
        ActiveWep_FREEZING_MINE = 102,
        ActiveWep_HOMING_MISSLE = 103,
        ActiveWep_BOWLING_BOMB = 104,
        ActiveWep_TORNADO = 105,
        ActiveWep_STATIC_SHOCK = 106,
        ActiveWep_POWER_SHIELD = 107,
        ActiveWep_INVINCIBILITY_MASKS = 108,
        ActiveWep_INVISIBILITY = 109,
        ActiveWep_VOODOO_DOLL = 110,
        ActiveWep_TROPY_CLOCK = 111,
        ActiveWep_TURBO_BOOSTS = 112,
        ActiveWep_SUPER_ENGINE = 113,
        ActiveWep_REDEYE = 114,
        ActiveWep_HOMING_MISSLE_X3 = 115,
        ActiveWep_BOWLING_BOMB_X3 = 116,
        ActiveWep_TURBO_BOOST_X3 = 117,
        ActiveWep_EXPCRATE_X3 = 118,
        ActiveWep_FREEZEMINE_X3 = 119,
        ActiveWep_STATICSHOCK_X3 = 120,
        ActivePower_POWER_SHIELD = 131,
        ActivePower_TURBO_BOOSTS = 132,
        ActivePower_INVINCIBILITY_MASKS = 133,
        ActivePower_TROPY_CLOCKS = 134,
        ActivePower_INVISIBILITY = 135,
        ActivePower_SUPER_ENGINE = 136,
        ActivePower_EXPLOSIVE_CRATE = 137,
        ActivePower_RESETTING = 138,
        ActivePower_CURSED = 139,
        ActivePower_GRACED = 140,
        ActivePower_ICED = 141,
        ActivePower_STATICSHOCKED = 142,
        ActivePower_SPIKYFRUIT = 143,
        ActivePower_MIMECUBE = 144,
        ActivePower_TIMEBUBBLE = 145,
        ActivePower_CLEANINGFLUID = 146,
        ActivePower_ROLLINGBRUSH = 147,
        ActivePower_WINDUPJAW = 148,
        ActivePower_TEETHSTRIP = 149,
        ActivePower_INVULNERABLE = 150,
        ActivePower_TEAMINVULNERABLE = 151,
        ActivePower_POWERSHIELD_ZAPPED = 152,
        KartsInFront_DensityRadius = 158,
        KartsInFront_0 = 161,
        KartsInFront_1 = 162,
        KartsInFront_2 = 163,
        KartsInFront_3 = 164,
        KartsInFront_4 = 165,
        KartsInFront_5 = 166,
        KartsInFront_6 = 167,
        KartsInFront_7 = 168,
        KartsInFront_8 = 169,
        KartsBehind_DensityRadius = 178,
        KartsBehind_0 = 181,
        KartsBehind_1 = 182,
        KartsBehind_2 = 183,
        KartsBehind_3 = 184,
        KartsBehind_4 = 185,
        KartsBehind_5 = 186,
        KartsBehind_6 = 187,
        KartsBehind_7 = 188,
        KartsBehind_8 = 189,
        Difficulty_Easiest = 201,
        Difficulty_Hardest = 202,
        Buddy_Range = 210,
        Buddy_Ahead = 211,
        Buddy_Behind = 212,
        Buddy_InRange = 213,
    }

    public struct AdvTracksManagerEntry
    {
        public PadInfoNameID PadName;
        public SubModeID Submode;
        public RewardID RewardNeeded;
        public int NumberNeeded;

        public AdvTracksManagerEntry(PadInfoNameID pName, SubModeID sMode, RewardID rewNeed, int num)
        {
            PadName = pName;
            Submode = sMode;
            RewardNeeded = rewNeed;
            NumberNeeded = num;
        }
    }
    public struct GoalsToRewardsEntry
    {
        public TrackID Track;
        public SubModeID Submode;
        public RewardID Reward;

        public GoalsToRewardsEntry(TrackID tck, SubModeID sMode, RewardID rew)
        {
            Track = tck;
            Submode = sMode;
            Reward = rew;
        }
    }
    public struct WarpPadInfoEntry
    {
        public PadInfoNameID PadName;
        public PadInfoDescID PadDesc;
        public TrackID Track;
        public PadInfoEventID isWarpGate;
        public PadInfoEventID PrimaryActEvent;
        public PadInfoEventID SecondaryEvent;
        public PadInfoEventID LockedEvent;
        public PadInfoEventID LockedEvent2;
        public PadInfoEventID[] BaseRewardEvent;
        public PadInfoEventID RelicWonEvent;
        public PadInfoEventID[] TokenWonEvent;

        public WarpPadInfoEntry(PadInfoNameID pName, PadInfoDescID pDesc, TrackID tck, PadInfoEventID WarpGate, PadInfoEventID PrimEvent, PadInfoEventID SecEvent, PadInfoEventID LockEvent, PadInfoEventID LockEvent2, PadInfoEventID[] BaseRewEvent, PadInfoEventID RelicEvent, PadInfoEventID[] TokenEvent)
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

    public static class CNK_Common
    {

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
            "yellowcup",
        };

        public static string[] RewardName = new string[]
        {
            "trophy", "key", "relic", "relic2", "relic3", "token_blue", "token_green", "token_red", "token_purple", "token_yellow", "gem_blue", "gem_green", "gem_red", "gem_purple", "gem_yellow"
        };

        public static string[] DriverTypes = new string[] { "coco", "cortex", "crash", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem" };
        public static string[] DriverModelTypes = new string[] { "coco", "ncortex", "crash", "crunch", "dingodile", "fakecrash", "ngin", "noxide", "ntrance", "ntropy", "polar", "pura", "realvelo", "tiny", "zam", "zem", "barinboss", "earthboss", "empvelo", "fenombigboss", "fenomlittleboss", "tekneeboss", "tekneeminion", "velominion" };
        public static string[] DriverAudioTypes = new string[] { "cob", "dnc", "crb", "cnb", "ddl", "fcb", "ngn", "oxd", "ntn", "ntp", "plr", "pur", "rvl", "tny", "zam", "zem", "nsh", "kgo", "vlo", "bnm", "lnm", "oto", "scr", "vlm" };

        public static string[] SubModeName = new string[]
        {
            "hub", "trophy", "ctr", "relic", "boss", "crystal", "gem"
        };

        public static string[] TrackName = new string[]
        {
            "earth1", "earth2", "earth3", "arena1", "barin1", "barin2", "barin3", "arena2", "fenom1", "fenom2", "fenom3", "arena3", "teknee1", "teknee2", "teknee3", "arena4", "arena5", "velorace" , "citadel" , "hub1" , "hub2" , "hub3" , "hub4" , "secr"
        };

        public static string GetDataPath(ConsoleMode console, string ExtractedPath)
        {
            string path_gob_extracted = ExtractedPath + @"\assets\";
            if (console == ConsoleMode.PS2)
            {
                path_gob_extracted = ExtractedPath + @"\ASSETS\";
            }
            return path_gob_extracted;
        }

        public static string Float_To_CSV_Line(float targetfloat)
        {
            string cur_line = String.Format("{0:0.#########}", targetfloat);
            cur_line = cur_line.Replace(',', '.'); // For some reason String.Format is still not enough
            cur_line += ",";
            return cur_line;
        }
        public static string Float_To_CSV_Word(float targetfloat)
        {
            string cur_line = String.Format("{0:0.#########}", targetfloat);
            cur_line = cur_line.Replace(',', '.'); // For some reason String.Format is still not enough
            return cur_line;
        }
        public static string FloatArray_To_CSV_Line(float[] targetfloat)
        {
            string cur_line = "";
            string[] line_vars = new string[targetfloat.Length];
            for (int i = 0; i < targetfloat.Length; i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.Length; i++)
            {
                cur_line += line_vars[i];
                cur_line += ",";
            }
            cur_line += ",";
            return cur_line;
        }
        public static List<string> FloatArray_To_CSV_FullLine(float[] targetfloat)
        {
            List<string> line = new List<string>();
            string[] line_vars = new string[targetfloat.Length];
            for (int i = 0; i < targetfloat.Length; i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.Length; i++)
            {
                line.Add(line_vars[i]);
            }
            return line;
        }
        public static string FloatArray2_To_CSV_Line(float[,] targetfloat, int targetCharacter)
        {
            string cur_line = "";
            string[] line_vars = new string[targetfloat.GetLength(1)];
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[targetCharacter, i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                cur_line += line_vars[i];
                cur_line += ",";
            }
            cur_line += ",";
            return cur_line;
        }
        public static List<string> FloatArray2_To_CSV_FullLine(float[,] targetfloat, int targetCharacter)
        {
            List<string> line = new List<string>();
            string[] line_vars = new string[targetfloat.GetLength(1)];
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                line_vars[i] = String.Format("{0:0.#########}", targetfloat[targetCharacter, i]);
                line_vars[i] = line_vars[i].Replace(',', '.'); // For some reason String.Format is still not enough
            }
            for (int i = 0; i < targetfloat.GetLength(1); i++)
            {
                line.Add(line_vars[i]);
            }
            return line;
        }
        public static string Int_To_CSV_Line(int targetInt)
        {
            string cur_line = targetInt.ToString();
            cur_line += ",";
            return cur_line;
        }
        public static string Int_To_CSV_Word(int targetInt)
        {
            string cur_line = targetInt.ToString();
            return cur_line;
        }
        public static string CSV_WeaponSelection_RowID_To_RowText(WeaponSelectionRows RowID, float[] RowTable)
        {
            string row_text = "";
            row_text += ",,";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.EXPLOSIVE_CRATE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.FREEZING_MINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.HOMING_MISSLE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.BOWLING_BOMB]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TORNADO]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.STATIC_SHOCK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.POWER_SHIELD]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.INVINCIBILITY_MASK]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.INVISIBILITY]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.VOODOO_DOLL]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TROPY_CLOCKS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TURBO_BOOSTS]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.SUPER_ENGINE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.REDEYE]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.HOMING_MISSLE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.BOWLING_BOMB_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.TURBO_BOOST_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.EXPCRATE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.FREEZEMINE_X3]);
            row_text += ",";
            row_text += Float_To_CSV_Line(RowTable[(int)PowerupTypes.STATICSHOCK_X3]);

            return row_text;
        }
        public static List<string> CSV_WeaponSelection_RowID_To_Row(WeaponSelectionRows RowID, float[] RowTable)
        {
            List<string> row = new List<string>();
            row.Add("");
            row.Add("");
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.EXPLOSIVE_CRATE]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.FREEZING_MINE]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.HOMING_MISSLE]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.BOWLING_BOMB]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.TORNADO]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.STATIC_SHOCK]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.POWER_SHIELD]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.INVINCIBILITY_MASK]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.INVISIBILITY]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.VOODOO_DOLL]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.TROPY_CLOCKS]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.TURBO_BOOSTS]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.SUPER_ENGINE]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.REDEYE]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.HOMING_MISSLE_X3]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.BOWLING_BOMB_X3]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.TURBO_BOOST_X3]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.EXPCRATE_X3]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.FREEZEMINE_X3]));
            row.Add(Float_To_CSV_Word(RowTable[(int)PowerupTypes.STATICSHOCK_X3]));

            return row;
        }
    }
}
