using System;
using System.Collections.Generic;
using Twinsanity;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTS
{

    public enum RM_Sections
    {
        Graphics = 11,
        Code = 10,
        Particles = 8,
        ColData = 9,
        Instances1 = 0,
        Instances2 = 1,
        Instances3 = 2,
        Instances4 = 3,
        Instances5 = 4,
        Instances6 = 5,
        Instances7 = 6,
        Instances8 = 7,
    }
    public enum RM_Graphics_Sections
    {
        Textures = 0,
        Materials = 1,
        Models = 2,
        RigidModels = 3,
        Skin = 4,
        BlendSkin = 5,
        Meshes = 6,
        LodModels = 7,
        Skydome = 8,
    }
    public enum RM_Code_Sections
    {
        Object = 0,
        Script = 1,
        Animation = 2,
        OGI = 3,
        CodeModel = 4,
        Unknown = 5,
        SE = 6,
        // also used for japanese
        SE_Eng = 7,
        SE_Fre = 8,
        SE_Ger = 9,
        SE_Spa = 10,
        SE_Ita = 11,
        SE_Unused = 12,
    }
    public enum RM_Instance_Sections
    {
        UnknownInstance = 0,
        AIPosition = 1,
        AIPath = 2,
        Position = 3,
        Path = 4,
        CollisionSurface = 5,
        ObjectInstance = 6,
        Trigger = 7,
        Camera = 8,
    }

    public enum LevelType
    {
        Island = 0,
        Hub1 = 0,
        Jungle = 1,
        Level1 = 1,
        Cavern = 2,
        Level2 = 2,
        Totem = 3,
        Level3 = 3,
        Iceberg = 4,
        Hub2 = 4,
        IceClimb = 5,
        Level4 = 5,
        SlipSlide = 6,
        Level5 = 6,
        HiSeas = 7,
        Level6 = 7,
        Academy = 8,
        Hub3 = 8,
        Boiler = 9,
        Level7 = 9,
        Classroom = 10,
        Level8 = 10,
        Rooftop = 11,
        Level9 = 11,
        TwinsanityIsland = 12,
        Hub4 = 12,
        Rockslide = 13,
        Level10 = 13,
        BP = 14,
        Level11 = 14,
        AntAgony = 15,
        Level12 = 15,
    }
    public enum ChunkType
    {
        Invalid = -1,
        Earth_Hub_AlwaysOn = 0,
        Earth_Hub_Beach = 1,
        Earth_Hub_BossArea = 2,
        Earth_Hub_Docent = 3,
        Earth_Hub_HighPath = 4,
        Earth_Hub_HubA = 5,
        Earth_Hub_HubB = 6,
        Earth_Hub_HubBoat1 = 7,
        Earth_Hub_HubBoat2 = 8,
        Earth_Hub_HubC = 9,
        Earth_Hub_HubD = 10,
        Earth_Hub_Pier = 11,
        Earth_Hub_TotemEx = 12,
        Earth_Cavern_AntFight = 13,
        Earth_Cavern_CavAllOn = 14,
        Earth_Cavern_CavBridg = 15,
        Earth_Cavern_CavEnt = 16,
        Earth_Cavern_CavernEnd = 17,
        Earth_Cavern_CortThro = 18,
        Earth_Cavern_CrysCave = 19,
        Earth_Cavern_Escape = 20,
        Earth_Cavern_NitroCav = 21,
        Earth_Cavern_Tunnel01 = 22,
        Earth_Cavern_Tunnel02 = 23,
        Earth_Cavern_Tunnel03 = 24,
        Earth_DocAmok_DocAmok1 = 25,
        Earth_DocAmok_DocAmok2 = 26,
        Earth_DocAmok_DocAmok3 = 27,
        Earth_DocAmok_DocAmok4 = 28,
        Earth_Totem_L03AllOn = 29,
        Earth_Totem_L03Beach = 30,
        Earth_Totem_L03Chase = 31,
        Earth_Totem_L03Creep = 32,
        Earth_Totem_L03River = 33,
        Earth_Totem_L03Stock = 34,
        Ice_Hub_Airship = 35,
        Ice_Hub_AlwaysOn = 36,
        Ice_Hub_LabExt = 37,
        Ice_Hub_LabInt = 38,
        Ice_Hub_Psycho = 39,
        Ice_Hub_PTCorr = 40,
        Ice_Hub_PTExit = 41,
        Ice_Hub_ShipEnt = 42,
        Ice_Hub_SlipEnt = 43,
        Ice_IceClimb_BergCorr = 44,
        Ice_IceClimb_BergExt = 45,
        Ice_IceClimb_BergInt = 46,
        Ice_IceClimb_CaveEnt = 47,
        Ice_IceClimb_UkaFight = 48,
        Ice_IceClimb_UkaTrans = 49,
        Ice_SlipSlide_L05Brawl = 50,
        Ice_SlipSlide_L05ChasA = 51,
        Ice_SlipSlide_L05ChasB = 52,
        Ice_SlipSlide_L05Grind = 53,
        Ice_SlipSlide_L05River = 54,
        Ice_SlipSlide_L05Roids = 55,
        Ice_SlipSlide_L05Start = 56,
        Ice_HighSeas_GPA01 = 57,
        Ice_HighSeas_GPA02 = 58,
        Ice_HighSeas_GPA03 = 59,
        Ice_HighSeas_GPA04 = 60,
        Ice_HighSeas_GPA05 = 61,
        Ice_HighSeas_GPA06 = 62,
        Ice_HighSeas_GPA07 = 63,
        Ice_HighSeas_GPA08 = 64,
        Ice_HighSeas_GPA09 = 65,
        Ice_HighSeas_GPA10 = 66,
        Ice_HighSeas_GPA11 = 67,
        Ice_HighSeas_GPA12 = 68,
        School_Sch_Hub_Boil2Lck = 69,
        School_Sch_Hub_Sch_Hub = 70,
        School_Sch_Hub_SchShip = 71,
        School_Sch_Hub_SLK01 = 72,
        School_Boiler_Boiler_1 = 73,
        School_Boiler_Boiler_2 = 74,
        School_Boiler_Boiler_3 = 75,
        School_Boiler_Boiler_4 = 76,
        School_Boiler_Boiler_5 = 77,
        School_Boiler_Boiler_6 = 78,
        School_Boiler_Boiler_X = 79,
        School_Crash_CrashEnt = 80,
        School_Crash_CrGPA01 = 81,
        School_Crash_CrGPA02 = 82,
        School_Crash_CrGPA03 = 83,
        School_Crash_CrGPA04 = 84,
        School_Crash_CrGPA05 = 85,
        School_Crash_CrGPA06 = 86,
        School_Crash_CrGPA07 = 87,
        School_Crash_CrGPA08 = 88,
        School_Crash_CrLib = 89,
        School_Cortex_CoGPA01 = 90,
        School_Cortex_CoGPA02 = 91,
        School_Cortex_CoGPA03 = 92,
        School_Cortex_CoGPA04 = 93,
        School_Cortex_CoGPA05 = 94,
        School_Cortex_CoGPA06 = 95,
        School_Cortex_CoGPA07 = 96,
        School_Cortex_CoGPA08 = 97,
        School_Rooftop_BusChase = 98,
        School_Rooftop_Roof01 = 99,
        School_Rooftop_Roof02 = 100,
        School_Rooftop_Roof03 = 101,
        School_Rooftop_Roof04 = 102,
        School_Rooftop_Roof05 = 103,
        School_Rooftop_RoofCor1 = 104,
        School_Rooftop_RoofCor2 = 105,
        School_Amberly_AmberCor = 106,
        School_Amberly_Amberly = 107,
        AltEarth_Lab_AltLabIn = 108,
        AltEarth_Lab_LabExt = 109,
        AltEarth_Lab_Psycho = 110,
        AltEarth_Lab_PTCorr = 111,
        AltEarth_Lab_PTExit = 112,
        AltEarth_RockSlid_L10ChasA = 113,
        AltEarth_RockSlid_L10ChasB = 114,
        AltEarth_RockSlid_L10End = 115,
        AltEarth_RockSlid_L10Roids = 116,
        AltEarth_RockSlid_L10Start = 117,
        AltEarth_Hub_AltA = 118,
        AltEarth_Hub_AltDoc = 119,
        AltEarth_Hub_AltDoc_B = 120,
        AltEarth_Hub_AltDoc_C = 121,
        AltEarth_Hub_AltTunl = 122,
        AltEarth_Hub_AlwaysOn = 123,
        AltEarth_Hub_CoreEnt = 124,
        AltEarth_Hub_SlipJoin = 125,
        AltEarth_Core_AftTreas = 126,
        AltEarth_Core_CoreA = 127,
        AltEarth_Core_CoreB = 128,
        AltEarth_Core_CoreC = 129,
        AltEarth_Core_CoreD = 130,
        AltEarth_Core_PreTreas = 131,
        AltEarth_Core_Throne = 132,
        AltEarth_Core_Treasure = 133,
        Default = 134,
    }
    public enum GemID
    {
        GEM_RED = 771,
        GEM_GREEN = 772,
        GEM_BLUE = 773,
        GEM_PURPLE = 775,
        GEM_YELLOW = 776,
        GEM_CLEAR = 777,
    }
    public enum GemType
    {
        GEM_RED = 5,
        GEM_GREEN = 3,
        GEM_BLUE = 1,
        GEM_PURPLE = 4,
        GEM_YELLOW = 6,
        GEM_CLEAR = 2,
    }

    public enum MusicID
    {
        BP = 7,
        ClassroomCortex = 8,
        Henchmania = 9,
        WormChase = 10,
        TitleTheme = 27,
        Cavern = 28,
        BeeChase = 29,
        MechaBandicoot = 30,
        TotemRiver = 31,
        BossTikimon = 32,
        IcebergLab = 33,
        IceClimb = 34,
        BossUka = 35,
        WalrusChase = 36,
        Academy = 37,
        AcademyNoLaugh = 38,
        Undefined = 39,
        BossDingodile = 40,
        Rooftop = 41,
        IcebergLabFast = 53,
        SlipSlide = 54,
        BossNGin = 55,
        Hijinks = 56,
        Boiler = 57,
        ClassroomCrash = 58,
        BossAmberly = 59,
        AltLab = 60,
        Rockslide = 61,
        TwinsanityIsland = 62,
        AntAgony = 63,
        BossTwins = 64,
        BoilerUnused = 136,
    }

    public enum GameObjectScriptOrder
    {
        OnSpawn = 0,
        OnTrigger = 1,
        OnDamage = 2, // Explosion/Bodyslam/Gunfire/Nina punch
        OnTouch = 3,
        OnHeadbutt = 4,
        OnLand = 5, // Something landed on top of this object
        OnGettingSpinAttacked = 6,
        OnGettingBodyslamAttacked = 7,
        OnGettingSlideAttacked = 8,
        OnPhysicsCollision = 9,
        Unk10 = 10, // Some kind of damage
    }
    public enum CharacterGameObjectScriptOrder
    {
        OnSpawn = 0,
        OnTrigger = 1,
        OnDamage = 2, // Explosion/Bodyslam/Gunfire/Nina punch
        OnTouch = 3,
        OnHeadbutt = 4,
        OnLand = 5, // Something landed on top of this object
        OnGettingSpinAttacked = 6,
        OnGettingBodyslamAttacked = 7,
        OnGettingSlideAttacked = 8,
        OnPhysicsCollision = 9,
        Unk10 = 10, // Some kind of damage
                    // Character-specific from here on
        OnFallingDeath = 11,
        OnIdle = 12,
        OnShuffleFeet = 13,
        OnWalk = 14,
        OnRun = 15,
        OnStrafeLeft = 16,
        OnStrafeRight = 17,
        OnSpin = 18,
        OnSpinEnd = 19,
        OnSpinPunch = 20,
        OnSpinPunchEnd = 21,
        OnSlideJump = 22,
        OnStandingJump = 23,
        OnRunningJump = 24,
        OnDoubleJump = 25,
        OnMaxedDoubleJump = 26,
        OnSuperKneeDropHang = 27,
        OnSuperKneeDrop = 28,
        OnFlyingKick = 29,
        OnStompKick = 30,
        OnRadialBlastHang = 31,
        OnRadialBlast = 32,
        OnShortFall = 33,
        OnLongFall = 34,
        Unk35 = 35,
        Unk36 = 36,
        OnFlyingKickFall = 37,
        Unk38 = 38,
        OnSoftLand = 39,
        OnLandWhileMoving = 40,
        OnHardLand = 41,
        OnSuperKneeDropLand = 42,
        Unk43 = 43,
        OnFlyingKickLand = 44,
        OnStompKickLand = 45,
        OnStandToCrouch = 46,
        OnCrouchToCrawl = 47,
        OnCrawlToCrouch = 48,
        OnCrouchToStand = 49,
        OnCrawlToStand = 50,
        OnRunToSlide = 51,
        Unk52 = 52,
        OnSlideToCrouch = 53,
        OnSlideToStand = 54,
        OnThrowHandExtend = 55,
        OnAbortThrow = 56,
        OnWallJumpReel = 57,
        OnCeilingReel = 58,
        OnAbortReel = 59,
        OnThrowPunch = 60,
        OnCeilingPropel = 61,
        OnWallJumpHold = 62,
        OnWallJumpRelease = 63,
        OnWallJumpPropel = 64,
        Unk65 = 65,
        OnLeaveCoOp = 66,
        OnDefaultDeath = 67,
        OnBodyslam = 68,
        OnSpinThrow = 69,
        OnJumpThrow = 70,
        OnDrawMultitool = 71,
        OnSheathMultitool = 72,
        OnFireMultitool = 73,
        OnEnterVehicleMode = 74,
        OnExitVehicleMode = 75,
        OnEnterVehicleModeIdle = 76,
        OnExitVehicleModeIdle = 77,
        Unk78 = 78,
        OnDefaultDeath2 = 79,
        OnRecoil1 = 80,
        OnRecoil2 = 81,
        OnRecoil3 = 82,
        OnRecoil4 = 83,
        OnVehicleModifierHold = 84,
        OnVehicleModifierRelease = 85,
        OnSkateForwardsStraight = 86,
        OnSkateForwardsLeft = 87,
        OnSkateForwardsRight = 88,
        OnSkateBackwardsStraight = 89,
        OnSkateBackwardsLeft = 90,
        OnSkateBackwardsRight = 91,
        OnSkateCrouchStraight = 92,
        OnSkateCrouchRight = 93,
        OnSkateCrouchLeft = 94,
        OnSkateLand = 95,
        OnSkateFlatSpin = 96,
        OnSkateSpinOver = 97,
        OnVehicleGroundTrickForward = 98,
        OnVehicleGroundTrickBackward = 99,
        OnStandingJump2 = 100,
        OnRunningJump2 = 101,
        OnAboutToWinBrawl = 102,
        OnWinBrawl = 103,
        OnAboutToLoseBrawl = 104,
        OnLoseBrawl = 105,
        OnChargeMultitool = 106,
        OnFailToFireMultitool = 107,
        OnFailToRadialBlast = 108,
        OnSkateJump = 109,
        OnSkateImpact = 110,
    }

    public enum PropertyFlags : uint
    {
        NotSolidToPlayer = 0x8104,
        //Solid, does not damage characters
        SolidObject = 0x7D00,
        GenericObject = 0x7D36,
        DisableObject = 0xC0000000,
    }

    public enum CharacterInstanceFloats
    {
        // 1 for everyone
        Static1 = 0,

        /// <summary> Object gravity while not grounded </summary>
        AirGravity = 1,

        // different for mechabandicoot only (7.2 vs 5.2)
        Unk3 = 2,

        /// <summary> Object gravity on the ground </summary>
        BaseGravity = 3,
        /// <summary> Percentage of run speed as integer? Walk-to-run speed scaling? </summary>
        WalkSpeedPercentage = 4,

        // 0 for everyone
        Static6 = 5,

        /// <summary> Walking speed on the ground (Crash/Cortex/Nina 2.5, Mecha 12) </summary>
        WalkSpeed = 6,
        /// <summary> Runninng speed on the ground (Crash 9, Cortex/Nina 7) </summary>
        RunSpeed = 7,
        /// <summary> Set to 0 to disable strafing </summary>
        StrafingSpeed = 8,
        /// <summary> Forward force after starting a spin throw | crash 10 and nina 7 </summary> 
        SpinThrowForwardForce = 9,
        /// <summary> Length of spinning time </summary>
        SpinLength = 10,
        /// <summary> Delay between spins </summary>
        SpinDelay = 11,

        // crash only, default 0.15
        Unk13 = 12,
        // crash only, default 0.5
        Unk14 = 13,
        // 1 for everyone
        Static15 = 14,

        /// <summary> Speed while in mid-air | crash 8, cortex 6, nina 7 </summary>
        JumpAirSpeed = 15,
        /// <summary> Single jump height </summary>
        JumpHeight = 16,
        /// <summary> Jump arc variable </summary>
        JumpArcUnk18 = 17,
        /// <summary> Jump arc variable </summary>
        JumpArcUnk19 = 18,
        /// <summary> Jump speed off of edge (when long jump and flying kick aren't stored) | crash default 8 </summary>
        JumpEdgeSpeed = 19,
        /// <summary> Set to 0 to disable double jump, does nothing for Mecha </summary>
        DoubleJumpHeight = 20,
        /// <summary> Mandatory for double jump to work, default 64 </summary>
        DoubleJumpUnk22 = 21,
        /// <summary> Mandatory for double jump to work, default 72.951 </summary>
        DoubleJumpArcUnk = 22,
        /// <summary> Slide jump speed </summary>
        SlideJumpUnk24 = 23,
        /// <summary> Slide jump variable </summary>
        SlideJumpUnk25 = 24,
        /// <summary> Slide jump variable </summary>
        SlideJumpUnk26 = 25,
        /// <summary> Slide jump arc variable </summary>
        SlideJumpUnk27 = 26,

        // crash only, default 0.05
        Unk28 = 27,
        // crash only, different in the demo (1 instead of 0.4)
        Unk29 = 28,
        // crash and nina, default 0.05
        Unk30 = 29,
        // crash and nina, default 0.05
        Unk31 = 30,

        /// <summary> Set to 0 to disable bodyslam, does nothing for Nina, Mecha </summary>
        BodyslamHangTime = 31,
        /// <summary> How much the bodyslam flies upward after it starts </summary>
        BodyslamUpwardForce = 32,
        /// <summary> How much the bodyslam slams downward after it's done flying upward </summary>
        BodyslamGravityForce = 33,
        /// <summary> How long the Flying Kick lasts, Set to 0 to disable flying kick, unused move, replaces bodyslam of the single jump (not double jump), does nothing for Nina, Cortex, Mecha </summary>
        FlyingKickHangTime = 34,
        /// <summary> How fast the flying kick is </summary>
        FlyingKickForwardSpeed = 35,
        /// <summary> Fall gravity after the flying kick ends (only applies when walking off edges for some reason) </summary>
        FlyingKickGravity = 36,
        /// <summary> Set to 0 to disable radial blast, radial blast replaces single jump bodyslam </summary>
        RadialBlastTimeToStart = 37,
        /// <summary> Radial blast velocity value </summary>
        RadialBlastUnk39 = 38,
        /// <summary> Radial blast velocity value </summary>
        RadialBlastUnk40 = 39,
        /// <summary> Speed of crawling </summary>
        CrawlSpeed = 40,
        /// <summary> Time from standing to crouched, Set to 0 to disable crawl from standing (though crawling from sliding still possible if that's enabled, and breaks the game if this is set to 0) </summary>
        CrawlTimeFromStand = 41,
        /// <summary> Time from crouched to standing </summary>
        CrawlTimeToStand = 42,
        /// <summary> Time from crawling to running </summary>
        CrawlTimeToRun = 43,
        /// <summary> Set to 0 to disable slide </summary>
        SlideSpeed = 44,
        /// <summary> Slide variable </summary>
        SlideSlowdownTime = 45,
        /// <summary> Slide variable </summary>
        SlideSlowdownTime2 = 46,
        /// <summary> Slide variable </summary>
        SlideSlowdownTime3 = 47,
        /// <summary> Slide variable </summary>
        SlideUnk49 = 48,
        /// <summary> Slide variable </summary>
        SlideUnk50 = 49,
        /// <summary> How long should the firing button be held to start charging </summary>
        GunButtonHoldTimeToStartCharging = 50,
        /// <summary> How long the gun charges the shot </summary>
        GunChargeTime = 51,
        /// <summary> How long until the next charged shot fires while still holding the firing button </summary>
        GunTimeBetweenChargedShots = 52,
        /// <summary> How fast can regular shots be fired </summary>
        GunTimeBetweenShots = 53,

        // cortex/mecha/frontend only, default 0.5
        Unk55 = 54,

        /// <summary> How long the radial blast charges while in mid-air </summary>
        RadialBlastChargeTime = 55,
    }

    public enum CharacterInstanceFlags
    {
        // 0x10000 / 0x20000
        Unk1 = 0,
        // 0x20000 / 0x30000
        Unk2 = 1,

        // running rotation, 0x20000 / 0x30000
        GroundRotationSpeed = 2,

        // 0x58E38 crash only
        Unk4 = 3,

        /// 0x1AAAA / 0x30000
        CrawlRotationSpeed = 4,

        // 0x4000 crash only
        Unk6 = 5,

        // 0x40000 / 0x30000 / 0x20000 / 0xC000
        JumpRotationSpeed = 6,

        /// 0x40000 crash only
        Unk8 = 7,

        /// default 0 for all
        SlideJumpRotationSpeed = 8,
    }

    public enum CharacterID
    {
        Crash = 0,
        Cortex = 1,
        Coco = 2, // Unused
        Nina = 3,
        Frontend = 4, // Also Maybe Evil Crash
        Mechabandicoot = 5,
    }

    public enum ExecutableIndex
    {
        Invalid = -1,
        PAL,
        NTSCU,
        NTSCU2,
        NTSCJ,
        XBOX_NTSC,
        XBOX_PAL
    }

    public class ExecutableInfo
    {
        public string Path;
        public ExecutableIndex Index;
        public string BDpath;

        public ExecutableInfo(string p, ExecutableIndex i, string b)
        {
            Path = p;
            Index = i;
            BDpath = b;
        }
    }

    public class ChunkInfoRM
    {
        public TwinsFile File;
        public ChunkType Type;

        public ChunkInfoRM(TwinsFile f, ChunkType t)
        {
            File = f;
            Type = t;
        }

    }
    public class ChunkInfoSM
    {
        public TwinsFile File;
        public ChunkType Type;

        public ChunkInfoSM(TwinsFile f, ChunkType t)
        {
            File = f;
            Type = t;
        }

    }
    public class ChunkInfoFull
    {
        public TwinsFile FileRM;
        public TwinsFile FileSM;
        public ChunkType Type;

        public ChunkInfoFull(TwinsFile rm, TwinsFile sm, ChunkType t)
        {
            FileRM = rm;
            FileSM = sm;
            Type = t;
        }

    }

    public static class Twins_Common
    {
        public static ChunkType ChunkPathToType(string path)
        {
            ChunkType type = ChunkType.Invalid;

            for (int i = 0; i < Twins_Data.All_Chunks.Count; i++)
            {
                if (path.ToLower().Contains(Twins_Data.All_Chunks[i].Path.ToLower()))
                {
                    type = Twins_Data.All_Chunks[i].Chunk;
                    break;
                }
            }

            if (type == ChunkType.Invalid)
            {
                Console.WriteLine("invalid Chunk");
                Console.WriteLine("any chunk path: " + Twins_Data.All_Chunks[0].Path.ToLower());
                Console.WriteLine("file path: " + path.ToLower());
            }

            return type;
        }

        public static string GetDataPath(GenericModStruct mod)
        {
            string bdPath = mod.ExtractedPath;
            if (mod.Console == ConsoleMode.PS2)
            {
                bdPath = System.IO.Path.Combine(mod.ExtractedPath, @"CRASH6\CRASH\");
            }
            return bdPath;
        }
        public static string GetDataPath(ConsoleMode console, string ExtractedPath)
        {
            string bdPath = ExtractedPath;
            if (console == ConsoleMode.PS2)
            {
                bdPath = System.IO.Path.Combine(ExtractedPath, @"CRASH6\CRASH\");
            }
            return bdPath;
        }

        public static ExecutableInfo GetEXE(GenericModStruct mod)
        {
            RegionType region = mod.Region;
            ConsoleMode console = mod.Console;
            string bdPath = GetDataPath(mod);
            string filePath = System.IO.Path.Combine(mod.ExtractedPath, mod.ExecutableFileName);

            ExecutableIndex index = ExecutableIndex.Invalid;
            if (console == ConsoleMode.XBOX)
            {
                if (region == RegionType.PAL)
                    index = ExecutableIndex.XBOX_PAL;
                else if (region == RegionType.NTSC_U)
                    index = ExecutableIndex.XBOX_NTSC;
            }
            else
            {
                if (region == RegionType.NTSC_U)
                {
                    using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        reader.BaseStream.Position = 0x1ECB10;
                        char ch = reader.ReadChar();

                        if (ch == 'C')
                            index = ExecutableIndex.NTSCU;
                        else
                            index = ExecutableIndex.NTSCU2;
                    }
                }
                else if (region == RegionType.PAL)
                    index = ExecutableIndex.PAL;
                else if (region == RegionType.NTSC_J)
                    index = ExecutableIndex.NTSCJ;
            }

            return new ExecutableInfo(filePath, index, bdPath);
        }

    }
}
