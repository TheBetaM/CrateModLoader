using System;
using System.Collections.Generic;
using CrateModLoader.ModProperties;
using CrateModLoader.GameSpecific.CrashTS.Mods;

namespace CrateModLoader.GameSpecific.CrashTS
{

    [ModCategory((int)ModProps.Character)] [ExecutesMods(typeof(TS_Rand_CharParams))]
    static class Twins_Data_Characters
    {

        public static string[] CharacterNames = Enum.GetNames(typeof(CharacterID));

        public static ModPropNamedFloatArray CharFloats_Static1 = new ModPropNamedFloatArray(new float[]
        {
            1, 1, 1, 1, 1, 1
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_AirGravity = new ModPropNamedFloatArray(new float[]
        {
            50, 50, 50, 50, 0, 50
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk3 = new ModPropNamedFloatArray(new float[]
        {
            5.2f, 5.2f, 5.2f, 5.2f, 0, 7.2f
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_BaseGravity = new ModPropNamedFloatArray(new float[]
        {
            15, 15, 15, 15, 0, 15
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_WalkSpeedPercentage = new ModPropNamedFloatArray(new float[]
        {
            50, 50, 50, 50, 0, 50
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Static6 = new ModPropNamedFloatArray(new float[]
        {
            0, 0, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_WalkSpeed = new ModPropNamedFloatArray(new float[]
        {
            2.5f, 2.5f, 2.5f, 2.5f, 0, 12
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_RunSpeed = new ModPropNamedFloatArray(new float[]
        {
            9, 7, 9, 7, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_StrafingSpeed = new ModPropNamedFloatArray(new float[]
        {
            0, 5, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SpinThrowForwardForce = new ModPropNamedFloatArray(new float[]
        {
            10, 0, 10, 7, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SpinLength = new ModPropNamedFloatArray(new float[]
        {
            0.4f, 0.4f, 0.4f, 0.7f, 0, 0.25f
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SpinDelay = new ModPropNamedFloatArray(new float[]
        {
            0.15f, 0.75f, 0.15f, 0, 0, 0.25f
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk13 = new ModPropNamedFloatArray(new float[]
        {
            0.15f, 0, 0.15f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk14 = new ModPropNamedFloatArray(new float[]
        {
            0.5f, 0, 0.5f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Static15 = new ModPropNamedFloatArray(new float[]
        {
            1, 1, 1, 1, 0, 1
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_JumpAirSpeed = new ModPropNamedFloatArray(new float[]
        {
            8, 6, 8, 7, 0, 8
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_JumpHeight = new ModPropNamedFloatArray(new float[]
        {
            13, 5, 13, 13, 0, 13
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_JumpArcUnk18 = new ModPropNamedFloatArray(new float[]
        {
            37.556f, 19.231f, 37.556f, 37.556f, 0, 37.556f
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_JumpArcUnk19 = new ModPropNamedFloatArray(new float[]
        {
            57.874f, 22.569f, 57.874f, 57.874f, 0, 57.874f
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_JumpEdgeSpeed = new ModPropNamedFloatArray(new float[]
        {
            8, 0, 8, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_DoubleJumpHeight = new ModPropNamedFloatArray(new float[]
        {
            16, 0, 16, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_DoubleJumpUnk22 = new ModPropNamedFloatArray(new float[]
        {
            64, 0, 64, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_DoubleJumpArcUnk = new ModPropNamedFloatArray(new float[]
        {
            72.951f, 0, 72.951f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideJumpUnk24 = new ModPropNamedFloatArray(new float[]
        {
            11, 0, 11, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideJumpUnk25 = new ModPropNamedFloatArray(new float[]
        {
            5, 0, 5, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideJumpUnk26 = new ModPropNamedFloatArray(new float[]
        {
            10, 0, 10, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideJumpUnk27 = new ModPropNamedFloatArray(new float[]
        {
            14.958f, 0, 14.958f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk28 = new ModPropNamedFloatArray(new float[]
        {
            0.05f, 0, 0.05f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk29 = new ModPropNamedFloatArray(new float[]
        {
            0.4f, 0, 0.4f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk30 = new ModPropNamedFloatArray(new float[]
        {
            0.05f, 0, 0.05f, 0.05f, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk31 = new ModPropNamedFloatArray(new float[]
        {
            0.05f, 0, 0.05f, 0.05f, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_BodyslamHangTime = new ModPropNamedFloatArray(new float[]
        {
            0.4f, 0, 0.4f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_BodyslamUpwardForce = new ModPropNamedFloatArray(new float[]
        {
            10, 0, 10, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_BodyslamGravityForce = new ModPropNamedFloatArray(new float[]
        {
            400, 0, 400, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_FlyingKickHangTime = new ModPropNamedFloatArray(new float[]
        {
            0, 0, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_FlyingKickForwardSpeed = new ModPropNamedFloatArray(new float[]
        {
            0, 0, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_FlyingKickGravity = new ModPropNamedFloatArray(new float[]
        {
            0, 0, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_RadialBlastTimeToStart = new ModPropNamedFloatArray(new float[]
        {
            0, 0.15f, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_RadialBlastUnk39 = new ModPropNamedFloatArray(new float[]
        {
            0, 12, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_RadialBlastUnk40 = new ModPropNamedFloatArray(new float[]
        {
            0, 30, 0, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_CrawlSpeed = new ModPropNamedFloatArray(new float[]
        {
            1.75f, 1.75f, 1.75f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_CrawlTimeFromStand = new ModPropNamedFloatArray(new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_CrawlTimeToStand = new ModPropNamedFloatArray(new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_CrawlTimeToRun = new ModPropNamedFloatArray(new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideSpeed = new ModPropNamedFloatArray(new float[]
        {
            18, 10, 18, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideSlowdownTime = new ModPropNamedFloatArray(new float[]
        {
            0.15f, 0.6f, 0.15f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideSlowdownTime2 = new ModPropNamedFloatArray(new float[]
        {
            0.2f, 0.3f, 0.2f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideSlowdownTime3 = new ModPropNamedFloatArray(new float[]
        {
            0.1f, 0.2f, 0.1f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideUnk49 = new ModPropNamedFloatArray(new float[]
        {
            0.3f, 0.3f, 0.3f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_SlideUnk50 = new ModPropNamedFloatArray(new float[]
        {
            0.3f, 0.8f, 0.3f, 0, 0, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_GunButtonHoldTimeToStartCharging = new ModPropNamedFloatArray(new float[]
        {
            0, 0.25f, 0, 0, 0.25f, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_GunChargeTime = new ModPropNamedFloatArray(new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_GunTimeBetweenChargedShots = new ModPropNamedFloatArray(new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_GunTimeBetweenShots = new ModPropNamedFloatArray(new float[]
        {
            0, 0.05f, 0, 0, 0.05f, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_Unk55 = new ModPropNamedFloatArray(new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        }, CharacterNames);
        public static ModPropNamedFloatArray CharFloats_RadialBlastChargeTime = new ModPropNamedFloatArray(new float[]
        {
            0, 0.1f, 0, 0, 0.1f, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharInts_SpawnHealth = new ModPropNamedUIntArray(new uint[]
        {
            2, 2, 2, 2, 2, 2
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_Unk1 = new ModPropNamedUIntArray(new uint[]
        {
            0x10000, 0x20000, 0x10000, 0x10000, 0, 0x20000
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_Unk2 = new ModPropNamedUIntArray(new uint[]
        {
            0x20000, 0x30000, 0x20000, 0x20000, 0, 0xC000
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_GroundRotationSpeed = new ModPropNamedUIntArray(new uint[]
        {
            0x20000, 0x30000, 0x20000, 0x20000, 0, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_Unk4 = new ModPropNamedUIntArray(new uint[]
        {
            0x58E38, 0, 0, 0, 0, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_CrawlRotationSpeed = new ModPropNamedUIntArray(new uint[]
        {
            0x1AAAA, 0x30000, 0, 0, 0, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_Unk6 = new ModPropNamedUIntArray(new uint[]
        {
            0x4000, 0, 0, 0, 0, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_JumpRotationSpeed = new ModPropNamedUIntArray(new uint[]
        {
            0x40000, 0x30000, 0x40000, 0x20000, 0, 0xC000
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_Unk8 = new ModPropNamedUIntArray(new uint[]
        {
            0x40000, 0, 0, 0, 0, 0
        }, CharacterNames);

        public static ModPropNamedUIntArray CharFlags_SlideJumpRotationSpeed = new ModPropNamedUIntArray(new uint[]
        {
            0, 0, 0, 0, 0, 0
        }, CharacterNames);

    }
}
