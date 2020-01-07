using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity;

namespace CrateModLoader.GameSpecific.Twins
{
    static partial class Twins_Data
    {
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

        private enum RM2_Sections
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
        public enum RM2_Graphics_Sections
        {
            Textures = 0,
            Materials = 1,
            Meshes = 2,
            Models = 3,
            ArmatureModel = 4,
            ActorModel = 5,
            StaticModel = 6,
            Terrains = 7,
            Skydome = 8,
        }
        public enum RM2_Code_Sections
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
        public enum RM2_Instance_Sections
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

        public enum CharacterID
        {
            Crash = 0,
            Cortex = 1,
            Coco = 2, // Unused
            Nina = 3,
            Frontend = 4, // Also Maybe Evil Crash
            Mechabandicoot = 5,
        }
        public static float[] CharFloats_Static1 = new float[] 
        {
            1, 1, 1, 1, 1, 1
        };
        public static float[] CharFloats_AirGravity = new float[]
        {
            50, 50, 50, 50, 0, 50
        };
        public static float[] CharFloats_Unk3 = new float[]
        {
            5.2f, 5.2f, 5.2f, 5.2f, 0, 7.2f
        };
        public static float[] CharFloats_BaseGravity = new float[]
        {
            15, 15, 15, 15, 0, 15
        };
        public static float[] CharFloats_WalkSpeedPercentage = new float[]
        {
            50, 50, 50, 50, 0, 50
        };
        public static float[] CharFloats_Static6 = new float[]
        {
            0, 0, 0, 0, 0, 0
        };
        public static float[] CharFloats_WalkSpeed = new float[]
        {
            2.5f, 2.5f, 2.5f, 2.5f, 0, 12
        };
        public static float[] CharFloats_RunSpeed = new float[]
        {
            9, 7, 9, 7, 0, 0
        };
        public static float[] CharFloats_StrafingSpeed = new float[]
        {
            0, 5, 0, 0, 0, 0
        };
        public static float[] CharFloats_SpinThrowForwardForce = new float[]
        {
            10, 0, 10, 7, 0, 0
        };
        public static float[] CharFloats_SpinLength = new float[]
        {
            0.4f, 0.4f, 0.4f, 0.7f, 0, 0.25f
        };
        public static float[] CharFloats_SpinDelay = new float[]
        {
            0.15f, 0.75f, 0.15f, 0, 0, 0.25f
        };
        public static float[] CharFloats_Unk13 = new float[]
        {
            0.15f, 0, 0.15f, 0, 0, 0
        };
        public static float[] CharFloats_Unk14 = new float[]
        {
            0.5f, 0, 0.5f, 0, 0, 0
        };
        public static float[] CharFloats_Static15 = new float[]
        {
            1, 1, 1, 1, 0, 1
        };
        public static float[] CharFloats_JumpAirSpeed = new float[]
        {
            8, 6, 8, 7, 0, 8
        };
        public static float[] CharFloats_JumpHeight = new float[]
        {
            13, 5, 13, 13, 0, 13
        };
        public static float[] CharFloats_JumpArcUnk18 = new float[]
        {
            37.556f, 19.231f, 37.556f, 37.556f, 0, 37.556f
        };
        public static float[] CharFloats_JumpArcUnk19 = new float[]
        {
            57.874f, 22.569f, 57.874f, 57.874f, 0, 57.874f
        };
        public static float[] CharFloats_JumpEdgeSpeed = new float[]
        {
            8, 0, 8, 0, 0, 0
        };
        public static float[] CharFloats_DoubleJumpHeight = new float[]
        {
            16, 0, 16, 0, 0, 0
        };
        public static float[] CharFloats_DoubleJumpUnk22 = new float[]
        {
            64, 0, 64, 0, 0, 0
        };
        public static float[] CharFloats_DoubleJumpArcUnk = new float[]
        {
            72.951f, 0, 72.951f, 0, 0, 0
        };
        public static float[] CharFloats_SlideJumpUnk24 = new float[]
        {
            11, 0, 11, 0, 0, 0
        };
        public static float[] CharFloats_SlideJumpUnk25 = new float[]
        {
            5, 0, 5, 0, 0, 0
        };
        public static float[] CharFloats_SlideJumpUnk26 = new float[]
        {
            10, 0, 10, 0, 0, 0
        };
        public static float[] CharFloats_SlideJumpUnk27 = new float[]
        {
            14.958f, 0, 14.958f, 0, 0, 0
        };
        public static float[] CharFloats_Unk28 = new float[]
        {
            0.05f, 0, 0.05f, 0, 0, 0
        };
        public static float[] CharFloats_Unk29 = new float[]
        {
            0.4f, 0, 0.4f, 0, 0, 0
        };
        public static float[] CharFloats_Unk30 = new float[]
        {
            0.05f, 0, 0.05f, 0.05f, 0, 0
        };
        public static float[] CharFloats_Unk31 = new float[]
        {
            0.05f, 0, 0.05f, 0.05f, 0, 0
        };
        public static float[] CharFloats_BodyslamHangTime = new float[]
        {
            0.4f, 0, 0.4f, 0, 0, 0
        };
        public static float[] CharFloats_BodyslamUpwardForce = new float[]
        {
            10, 0, 10, 0, 0, 0
        };
        public static float[] CharFloats_BodyslamGravityForce = new float[]
        {
            400, 0, 400, 0, 0, 0
        };
        public static float[] CharFloats_FlyingKickHangTime = new float[]
        {
            0, 0, 0, 0, 0, 0
        };
        public static float[] CharFloats_FlyingKickForwardSpeed = new float[]
        {
            0, 0, 0, 0, 0, 0
        };
        public static float[] CharFloats_FlyingKickGravity = new float[]
        {
            0, 0, 0, 0, 0, 0
        };
        public static float[] CharFloats_RadialBlastTimeToStart = new float[]
        {
            0, 0.15f, 0, 0, 0, 0
        };
        public static float[] CharFloats_RadialBlastUnk39 = new float[]
        {
            0, 12, 0, 0, 0, 0
        };
        public static float[] CharFloats_RadialBlastUnk40 = new float[]
        {
            0, 30, 0, 0, 0, 0
        };
        public static float[] CharFloats_CrawlSpeed = new float[]
        {
            1.75f, 1.75f, 1.75f, 0, 0, 0
        };
        public static float[] CharFloats_CrawlTimeFromStand = new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        };
        public static float[] CharFloats_CrawlTimeToStand = new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        };
        public static float[] CharFloats_CrawlTimeToRun = new float[]
        {
            0.1f, 0.1f, 0.1f, 0, 0, 0
        };
        public static float[] CharFloats_SlideSpeed = new float[]
        {
            18, 10, 18, 0, 0, 0
        };
        public static float[] CharFloats_SlideSlowdownTime = new float[]
        {
            0.15f, 0.6f, 0.15f, 0, 0, 0
        };
        public static float[] CharFloats_SlideSlowdownTime2 = new float[]
        {
            0.2f, 0.3f, 0.2f, 0, 0, 0
        };
        public static float[] CharFloats_SlideSlowdownTime3 = new float[]
        {
            0.1f, 0.2f, 0.1f, 0, 0, 0
        };
        public static float[] CharFloats_SlideUnk49 = new float[]
        {
            0.3f, 0.3f, 0.3f, 0, 0, 0
        };
        public static float[] CharFloats_SlideUnk50 = new float[]
        {
            0.3f, 0.8f, 0.3f, 0, 0, 0
        };
        public static float[] CharFloats_GunButtonHoldTimeToStartCharging = new float[]
        {
            0, 0.25f, 0, 0, 0.25f, 0
        };
        public static float[] CharFloats_GunChargeTime = new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        };
        public static float[] CharFloats_GunTimeBetweenChargedShots = new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        };
        public static float[] CharFloats_GunTimeBetweenShots = new float[]
        {
            0, 0.05f, 0, 0, 0.05f, 0
        };
        public static float[] CharFloats_Unk55 = new float[]
        {
            0, 0.5f, 0, 0, 0.5f, 0
        };
        public static float[] CharFloats_RadialBlastChargeTime = new float[]
        {
            0, 0.1f, 0, 0, 0.1f, 0
        };

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

        public static List<TwinsLevelChunk> All_Chunks = new List<TwinsLevelChunk>()
        {
            new TwinsLevelChunk(ChunkType.AltEarth_Core_AftTreas,LevelType.AntAgony,@"altearth\core\afttreas"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_CoreA,LevelType.AntAgony,@"altearth\core\corea"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_CoreB,LevelType.AntAgony,@"altearth\core\coreb"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_CoreC,LevelType.AntAgony,@"altearth\core\corec"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_CoreD,LevelType.AntAgony,@"altearth\core\cored"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_PreTreas,LevelType.AntAgony,@"altearth\core\pretreas"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_Throne,LevelType.Hub4,@"altearth\core\throne"),
            new TwinsLevelChunk(ChunkType.AltEarth_Core_Treasure,LevelType.AntAgony,@"altearth\core\treasure"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltA,LevelType.Hub4,@"altearth\hub\alta"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltDoc,LevelType.BP,@"altearth\hub\altdoc"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltDoc_B,LevelType.BP,@"altearth\hub\altdoc_b"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltDoc_C,LevelType.BP,@"altearth\hub\altdoc_c"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltTunl,LevelType.Hub4,@"altearth\hub\alttunl"), // verify
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AlwaysOn,LevelType.Hub4,@"altearth\hub\alwayson"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_CoreEnt,LevelType.BP,@"altearth\hub\coreent"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_SlipJoin,LevelType.Hub4,@"altearth\hub\slipjoin"),
            new TwinsLevelChunk(ChunkType.AltEarth_Lab_AltLabIn,LevelType.Hub4,@"altearth\lab\altlabin"),
            new TwinsLevelChunk(ChunkType.AltEarth_Lab_LabExt,LevelType.Hub4,@"altearth\lab\labext"),
            new TwinsLevelChunk(ChunkType.AltEarth_Lab_Psycho,LevelType.Hub4,@"altearth\lab\psycho"),
            new TwinsLevelChunk(ChunkType.AltEarth_Lab_PTCorr,LevelType.Hub4,@"altearth\lab\ptcorr"),
            new TwinsLevelChunk(ChunkType.AltEarth_Lab_PTExit,LevelType.Hub4,@"altearth\lab\ptexit"),
            new TwinsLevelChunk(ChunkType.AltEarth_RockSlid_L10ChasA,LevelType.Rockslide,@"altearth\rockslid\l10chasa"),
            new TwinsLevelChunk(ChunkType.AltEarth_RockSlid_L10ChasB,LevelType.Rockslide,@"altearth\rockslid\l10chasb"),
            new TwinsLevelChunk(ChunkType.AltEarth_RockSlid_L10End,LevelType.Rockslide,@"altearth\rockslid\l10end"),
            new TwinsLevelChunk(ChunkType.AltEarth_RockSlid_L10Roids,LevelType.Rockslide,@"altearth\rockslid\l10roids"),
            new TwinsLevelChunk(ChunkType.AltEarth_RockSlid_L10Start,LevelType.Rockslide,@"altearth\rockslid\l10start"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_AntFight,LevelType.Cavern,@"earth\cavern\antfight"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CavAllOn,LevelType.Cavern,@"earth\cavern\cavallon"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CavBridg,LevelType.Cavern,@"earth\cavern\cavbridg"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CavEnt,LevelType.Cavern,@"earth\cavern\cavent"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CavernEnd,LevelType.Cavern,@"earth\cavern\cavrnend"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CortThro,LevelType.Cavern,@"earth\cavern\cortthro"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_CrysCave,LevelType.Cavern,@"earth\cavern\cryscave"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_Escape,LevelType.Cavern,@"earth\cavern\escape"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_NitroCav,LevelType.Cavern,@"earth\cavern\nitrocav"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_Tunnel01,LevelType.Cavern,@"earth\cavern\tunnel01"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_Tunnel02,LevelType.Cavern,@"earth\cavern\tunnel02"),
            new TwinsLevelChunk(ChunkType.Earth_Cavern_Tunnel03,LevelType.Cavern,@"earth\cavern\tunnel03"),
            new TwinsLevelChunk(ChunkType.Earth_DocAmok_DocAmok1,LevelType.Totem,@"earth\docamok\docamok1"),
            new TwinsLevelChunk(ChunkType.Earth_DocAmok_DocAmok2,LevelType.Totem,@"earth\docamok\docamok2"),
            new TwinsLevelChunk(ChunkType.Earth_DocAmok_DocAmok3,LevelType.Totem,@"earth\docamok\docamok3"),
            new TwinsLevelChunk(ChunkType.Earth_DocAmok_DocAmok4,LevelType.Totem,@"earth\docamok\docamok4"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_AlwaysOn,LevelType.Hub1,@"earth\hub\alwayson"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_Beach,LevelType.Hub1,@"earth\hub\beach"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_BossArea,LevelType.Jungle,@"earth\hub\bossarea"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_Docent,LevelType.Hub1,@"earth\hub\docent"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HighPath,LevelType.Jungle,@"earth\hub\highpath"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubA,LevelType.Jungle,@"earth\hub\huba"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubB,LevelType.Jungle,@"earth\hub\hubb"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubBoat1,LevelType.Hub1,@"earth\hub\hubboat1"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubBoat2,LevelType.Hub1,@"earth\hub\hubboat2"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubC,LevelType.Hub1,@"earth\hub\hubc"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_HubD,LevelType.Hub1,@"earth\hub\hubd"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_Pier,LevelType.Hub1,@"earth\hub\pier"),
            new TwinsLevelChunk(ChunkType.Earth_Hub_TotemEx,LevelType.Hub1,@"earth\hub\totemex"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03AllOn,LevelType.Totem,@"earth\totem\l03allon"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03Beach,LevelType.Totem,@"earth\totem\l03beach"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03Chase,LevelType.Totem,@"earth\totem\l03chase"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03Creep,LevelType.Totem,@"earth\totem\l03creep"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03River,LevelType.Totem,@"earth\totem\l03river"),
            new TwinsLevelChunk(ChunkType.Earth_Totem_L03Stock,LevelType.Totem,@"earth\totem\l03stock"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA01,LevelType.HiSeas,@"ice\highseas\gpa01"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA02,LevelType.HiSeas,@"ice\highseas\gpa02"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA03,LevelType.HiSeas,@"ice\highseas\gpa03"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA04,LevelType.HiSeas,@"ice\highseas\gpa04"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA05,LevelType.HiSeas,@"ice\highseas\gpa05"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA06,LevelType.HiSeas,@"ice\highseas\gpa06"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA07,LevelType.HiSeas,@"ice\highseas\gpa07"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA08,LevelType.HiSeas,@"ice\highseas\gpa08"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA09,LevelType.HiSeas,@"ice\highseas\gpa09"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA10,LevelType.HiSeas,@"ice\highseas\gpa10"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA11,LevelType.HiSeas,@"ice\highseas\gpa11"),
            new TwinsLevelChunk(ChunkType.Ice_HighSeas_GPA12,LevelType.HiSeas,@"ice\highseas\gpa12"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_Airship,LevelType.Hub2,@"ice\hub\airship"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_AlwaysOn,LevelType.Hub2,@"ice\hub\alwayson"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_LabExt,LevelType.Hub2,@"ice\hub\labext"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_LabInt,LevelType.Hub2,@"ice\hub\labint"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_Psycho,LevelType.Hub2,@"ice\hub\psycho"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_PTCorr,LevelType.Hub2,@"ice\hub\ptcorr"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_PTExit,LevelType.Hub2,@"ice\hub\ptexit"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_ShipEnt,LevelType.Hub2,@"ice\hub\shipent"),
            new TwinsLevelChunk(ChunkType.Ice_Hub_SlipEnt,LevelType.Hub2,@"ice\hub\slipent"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_BergCorr,LevelType.IceClimb,@"ice\iceclimb\bergcorr"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_BergExt,LevelType.IceClimb,@"ice\iceclimb\bergext"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_BergInt,LevelType.IceClimb,@"ice\iceclimb\bergint"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_CaveEnt,LevelType.IceClimb,@"ice\iceclimb\caveent"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_UkaFight,LevelType.Hub2,@"ice\iceclimb\ukafight"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_UkaTrans,LevelType.Hub2,@"ice\iceclimb\ukatrans"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05Brawl,LevelType.SlipSlide,@"ice\slipslide\l05brawl"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05ChasA,LevelType.SlipSlide,@"ice\slipslide\l05chasa"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05ChasB,LevelType.SlipSlide,@"ice\slipslide\l05chasb"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05Grind,LevelType.SlipSlide,@"ice\slipslide\l05grind"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05River,LevelType.SlipSlide,@"ice\slipslide\l05river"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05Roids,LevelType.SlipSlide,@"ice\slipslide\l05roids"),
            new TwinsLevelChunk(ChunkType.Ice_SlipSlide_L05Start,LevelType.SlipSlide,@"ice\slipslide\l05start"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_1,LevelType.Boiler,@"school\boiler\boiler_1"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_2,LevelType.Boiler,@"school\boiler\boiler_2"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_3,LevelType.Boiler,@"school\boiler\boiler_3"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_4,LevelType.Boiler,@"school\boiler\boiler_4"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_5,LevelType.Boiler,@"school\boiler\boiler_5"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_6,LevelType.Boiler,@"school\boiler\boiler_6"),
            new TwinsLevelChunk(ChunkType.School_Boiler_Boiler_X,LevelType.Boiler,@"school\boiler\boiler_x"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA01,LevelType.Classroom,@"school\cortex\cogpa01"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA02,LevelType.Classroom,@"school\cortex\cogpa02"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA03,LevelType.Classroom,@"school\cortex\cogpa03"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA04,LevelType.Classroom,@"school\cortex\cogpa04"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA05,LevelType.Classroom,@"school\cortex\cogpa05"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA06,LevelType.Classroom,@"school\cortex\cogpa06"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA07,LevelType.Classroom,@"school\cortex\cogpa07"),
            new TwinsLevelChunk(ChunkType.School_Cortex_CoGPA08,LevelType.Classroom,@"school\cortex\cogpa08"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrashEnt,LevelType.Classroom,@"school\crash\crashent"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA01,LevelType.Classroom,@"school\crash\crgpa01"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA02,LevelType.Classroom,@"school\crash\crgpa02"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA03,LevelType.Classroom,@"school\crash\crgpa03"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA04,LevelType.Classroom,@"school\crash\crgpa04"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA05,LevelType.Classroom,@"school\crash\crgpa05"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA06,LevelType.Classroom,@"school\crash\crgpa06"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA07,LevelType.Classroom,@"school\crash\crgpa07"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrGPA08,LevelType.Classroom,@"school\crash\crgpa08"),
            new TwinsLevelChunk(ChunkType.School_Crash_CrLib,LevelType.Classroom,@"school\crash\crlib"),
            new TwinsLevelChunk(ChunkType.School_Amberly_AmberCor,LevelType.Rooftop,@"school\madame\ambercor"),
            new TwinsLevelChunk(ChunkType.School_Amberly_Amberly,LevelType.Rooftop,@"school\madame\amberly"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_BusChase,LevelType.Rooftop,@"school\rooftop\buschase"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_Roof01,LevelType.Rooftop,@"school\rooftop\roof01"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_Roof02,LevelType.Rooftop,@"school\rooftop\roof02"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_Roof03,LevelType.Rooftop,@"school\rooftop\roof03"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_Roof04,LevelType.Rooftop,@"school\rooftop\roof04"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_Roof05,LevelType.Rooftop,@"school\rooftop\roof05"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_RoofCor1,LevelType.Rooftop,@"school\rooftop\roofcor1"),
            new TwinsLevelChunk(ChunkType.School_Rooftop_RoofCor2,LevelType.Rooftop,@"school\rooftop\roofcor2"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Boil2Lck,LevelType.Boiler,@"school\sch_hub\boil2lck"), // verify
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Sch_Hub,LevelType.Hub3,@"school\sch_hub\sch_hub"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SchShip,LevelType.Hub3,@"school\sch_hub\schship"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SLK01,LevelType.Hub3,@"school\sch_hub\slk01"),
        };

        public static List<TwinsGem> All_Gems = new List<TwinsGem>()
        {
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,GemType.GEM_GREEN,new Vector3(-97.65575f,8f,266.1835f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,GemType.GEM_RED,new Vector3(-104.8f,8f,-12f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,GemType.GEM_BLUE,new Vector3(8f,9.6f,12.79999f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,GemType.GEM_YELLOW,new Vector3(27.2f,1.6f,108.8f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,GemType.GEM_PURPLE,new Vector3(88f,9.6f,-14.40002f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,GemType.GEM_CLEAR,new Vector3(8f,12.99937f,-186.9314f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_BLUE,new Vector3(-6.401367f,24.01756f,60.79993f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_GREEN,new Vector3(64.20001f,4.8f,-87f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_PURPLE,new Vector3(119.4651f,13.6823f,92.02771f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_RED,new Vector3(154.4001f,4.8f,16f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_YELLOW,new Vector3(-8.174438f,22.4f,-51.20007f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_CLEAR,new Vector3(-163.1397f,4.8f,-51.10718f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc,GemType.GEM_GREEN,new Vector3(-144.7101f,0.8f,-107.9987f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,GemType.GEM_PURPLE,new Vector3(139.6206f,0.1059415f,68.17542f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,GemType.GEM_BLUE,new Vector3(-14.27686f,1.316345f,137.0558f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_CLEAR,new Vector3(75.20001f,12f,84.80005f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_YELLOW,new Vector3(-48.45877f,9.489095f,89.47549f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_RED,new Vector3(-20.80248f,11.55f,5.112657f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasA,GemType.GEM_RED,new Vector3(35.55548f,7.791886f,129.661f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,GemType.GEM_CLEAR,new Vector3(-229.2413f,120.2919f,5.918751f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,GemType.GEM_PURPLE,new Vector3(-265.3363f,172.6381f,-50.08163f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Roids,GemType.GEM_YELLOW,new Vector3(-215.0593f,-71.82281f,58.45757f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,GemType.GEM_BLUE,new Vector3(-25.30844f,-91.41412f,172.0988f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,GemType.GEM_GREEN,new Vector3(-19.16346f,45.38053f,-29.98228f)),
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,GemType.GEM_CLEAR,new Vector3(79.62399f,-17.01717f,50.43518f)),
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,GemType.GEM_PURPLE,new Vector3(111.6054f,-47.44042f,79.34602f)),
            new TwinsGem(ChunkType.Earth_Cavern_CavBridg,GemType.GEM_BLUE,new Vector3(-78.53198f,-6.914452f,50.18359f)),
            new TwinsGem(ChunkType.Earth_Cavern_Escape,GemType.GEM_RED,new Vector3(-35.70697f,26.1506f,15.27729f)),
            new TwinsGem(ChunkType.Earth_Cavern_NitroCav,GemType.GEM_GREEN,new Vector3(152.8216f,-41.90025f,-56.83078f)),
            new TwinsGem(ChunkType.Earth_Cavern_Tunnel02,GemType.GEM_YELLOW,new Vector3(-9.029061f,-10.96085f,3.384979f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok1,GemType.GEM_GREEN,new Vector3(-91.96573f,-20.25835f,54.1039f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok2,GemType.GEM_PURPLE,new Vector3(-41.36575f,-18.95425f,-9.680141f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_BLUE,new Vector3(-40.8331f,11.11265f,-101.5122f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_RED,new Vector3(-28.035f,8.17395f,-7.799316f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_CLEAR,new Vector3(75.79141f,13.55117f,-176.0295f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_CLEAR,new Vector3(-137.3173f,12.18633f,104.6265f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_PURPLE,new Vector3(-152.4951f,7.803614f,122.8785f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_YELLOW,new Vector3(-19.23212f,10.27694f,-56.64056f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_BLUE,new Vector3(16.80593f,8.854737f,-35.59075f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_GREEN,new Vector3(1.325815f,12.76332f,-15.9164f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_RED,new Vector3(-42.14584f,12.1575f,-62.07772f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,GemType.GEM_PURPLE,new Vector3(72.47044f,9.157012f,-64.04574f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,GemType.GEM_GREEN,new Vector3(33.76424f,8.509999f,-106.2756f)),
            new TwinsGem(ChunkType.Earth_Hub_Pier,GemType.GEM_YELLOW,new Vector3(18.07493f,-213.3366f,-468.4756f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Beach,GemType.GEM_RED,new Vector3(-35.28114f,-9.020504f,-5.951714f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Chase,GemType.GEM_CLEAR,new Vector3(73.37716f,4.741547f,0.2155212f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,GemType.GEM_YELLOW,new Vector3(75.43573f,27.4951f,-18.52006f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,GemType.GEM_BLUE,new Vector3(84.7923f,25.77177f,-39.30542f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA03,GemType.GEM_BLUE,new Vector3(-12.23604f,-0.462233f,23.77053f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,GemType.GEM_GREEN,new Vector3(-42.82871f,8.021708f,-12.1399f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA05,GemType.GEM_CLEAR,new Vector3(21.01998f,-1.060589f,-4.75337f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA06,GemType.GEM_YELLOW,new Vector3(8.593877f,-3.371867f,14.13962f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,GemType.GEM_RED,new Vector3(-24.52314f,9.9042f,-22.74868f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,GemType.GEM_PURPLE,new Vector3(8.359952f,13.60969f,-148.0028f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_YELLOW,new Vector3(23.13921f,-63.05058f,118.445f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_RED,new Vector3(53.48708f,-77.09535f,83.56358f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_PURPLE,new Vector3(58.75809f,-66.10101f,-115.0656f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_BLUE,new Vector3(108.385f,-79.05058f,-47.47729f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_CLEAR,new Vector3(-70.07782f,-95.22608f,-15.85516f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_GREEN,new Vector3(-16.82197f,-99.11121f,-46.0418f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_CLEAR,new Vector3(-3.421051f,0.3084259f,95.1685f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_YELLOW,new Vector3(-59.2377f,-15.23108f,-132.8586f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_RED,new Vector3(-6.437706f,-11.66944f,-74.4315f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_BLUE,new Vector3(-10.1212f,-53.0587f,8.050537f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_GREEN,new Vector3(-26.38887f,-14.20152f,-29.826f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_PURPLE,new Vector3(3.984783f,-5.858704f,-28.82296f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Brawl,GemType.GEM_RED,new Vector3(37.45079f,-34.82895f,106.5488f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,GemType.GEM_CLEAR,new Vector3(-39.06381f,42.81914f,-24.65672f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,GemType.GEM_YELLOW,new Vector3(153.9608f,-43.23351f,-5.100259f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,GemType.GEM_GREEN,new Vector3(-30.6158f,-120.4804f,244.4753f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,GemType.GEM_BLUE,new Vector3(-38.26909f,88.73341f,-197.7417f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,GemType.GEM_PURPLE,new Vector3(17.94812f,-40.0121f,163.8001f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_1,GemType.GEM_CLEAR,new Vector3(28.85219f,-3.918367f,62.12658f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_2,GemType.GEM_GREEN,new Vector3(-14.4f,0.8000002f,52.80002f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_3,GemType.GEM_RED,new Vector3(-8f,1.6f,-33.60001f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_4,GemType.GEM_PURPLE,new Vector3(-4.363602f,1.93657f,20.43105f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_5,GemType.GEM_YELLOW,new Vector3(46.4f,8.799999f,-17.6f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_X,GemType.GEM_BLUE,new Vector3(34.43597f,-36.66129f,-21.92944f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA03,GemType.GEM_PURPLE,new Vector3(0.448576f,-0.786973f,64.07467f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA03,GemType.GEM_RED,new Vector3(38.78387f,-0.9052219f,-72.34797f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA07,GemType.GEM_YELLOW,new Vector3(-0.1733313f,-1.962266f,19.78968f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA03,GemType.GEM_BLUE,new Vector3(-15.54741f,-2.607604f,-37.6863f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA04,GemType.GEM_CLEAR,new Vector3(-6.238373f,-10.31385f,-25.53635f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,GemType.GEM_GREEN,new Vector3(-12.40351f,58.12576f,-9.451543f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,GemType.GEM_RED,new Vector3(12.08768f,-2.6735f,166.759f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,GemType.GEM_YELLOW,new Vector3(39.12667f,3.317167f,138.1966f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof01,GemType.GEM_BLUE,new Vector3(163.4834f,24.41009f,10.86115f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,GemType.GEM_CLEAR,new Vector3(159.6622f,25.32991f,-128.0166f)),
            //new TwinsGem(ChunkType.School_Rooftop_Roof05,GemType.GEM_CLEAR,new Vector3(159.6622f,25.32991f,-128.0166f)), out of bounds gem
            new TwinsGem(ChunkType.School_Rooftop_Roof05,GemType.GEM_GREEN,new Vector3(74.34146f,43.41f,-39.19778f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,GemType.GEM_PURPLE,new Vector3(-19.15854f,38.41f,14.80222f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_GREEN,new Vector3(-5.347906f,-18.53108f,8.858836f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_RED,new Vector3(51.28447f,-13.25657f,-5.942781f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_PURPLE,new Vector3(34.86773f,-13.71656f,-36.90624f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_YELLOW,new Vector3(-60.98897f,-11.87395f,-59.4021f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_CLEAR,new Vector3(-35.78874f,-17.23424f,-14.95845f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_BLUE,new Vector3(-58.25388f,-12.73387f,-34.56901f)),
        };

        public static List<TwinsGem> GemSpawnPoints = new List<TwinsGem>()
        {
            
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,GemType.GEM_GREEN,new Vector3(-97.65575f,8f,266.1835f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,GemType.GEM_RED,new Vector3(-104.8f,8f,-12f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,GemType.GEM_BLUE,new Vector3(8f,9.6f,12.79999f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,GemType.GEM_YELLOW,new Vector3(27.2f,1.6f,108.8f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,GemType.GEM_PURPLE,new Vector3(88f,9.6f,-14.40002f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,GemType.GEM_CLEAR,new Vector3(8f,12.99937f,-186.9314f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_BLUE,new Vector3(-6.401367f,24.01756f,60.79993f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_GREEN,new Vector3(64.20001f,4.8f,-87f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_PURPLE,new Vector3(119.4651f,13.6823f,92.02771f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_RED,new Vector3(154.4001f,4.8f,16f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_YELLOW,new Vector3(-8.174438f,22.4f,-51.20007f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,GemType.GEM_CLEAR,new Vector3(-163.1397f,4.8f,-51.10718f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc,GemType.GEM_GREEN,new Vector3(-144.7101f,0.8f,-107.9987f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,GemType.GEM_PURPLE,new Vector3(139.6206f,0.1059415f,68.17542f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,GemType.GEM_BLUE,new Vector3(-14.27686f,1.316345f,137.0558f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_CLEAR,new Vector3(75.20001f,12f,84.80005f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_YELLOW,new Vector3(-48.45877f,9.489095f,89.47549f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,GemType.GEM_RED,new Vector3(-20.80248f,11.55f,5.112657f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasA,GemType.GEM_RED,new Vector3(35.55548f,7.791886f,129.661f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,GemType.GEM_CLEAR,new Vector3(-229.2413f,120.2919f,5.918751f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,GemType.GEM_PURPLE,new Vector3(-265.3363f,172.6381f,-50.08163f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Roids,GemType.GEM_YELLOW,new Vector3(-215.0593f,-71.82281f,58.45757f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,GemType.GEM_BLUE,new Vector3(-25.30844f,-91.41412f,172.0988f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,GemType.GEM_GREEN,new Vector3(-19.16346f,45.38053f,-29.98228f)),
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,GemType.GEM_CLEAR,new Vector3(79.62399f,-17.01717f,50.43518f)),
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,GemType.GEM_PURPLE,new Vector3(111.6054f,-47.44042f,79.34602f)),
            new TwinsGem(ChunkType.Earth_Cavern_CavBridg,GemType.GEM_BLUE,new Vector3(-78.53198f,-6.914452f,50.18359f)),
            new TwinsGem(ChunkType.Earth_Cavern_Escape,GemType.GEM_RED,new Vector3(-35.70697f,26.1506f,15.27729f)),
            new TwinsGem(ChunkType.Earth_Cavern_NitroCav,GemType.GEM_GREEN,new Vector3(152.8216f,-41.90025f,-56.83078f)),
            new TwinsGem(ChunkType.Earth_Cavern_Tunnel02,GemType.GEM_YELLOW,new Vector3(-9.029061f,-10.96085f,3.384979f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok1,GemType.GEM_GREEN,new Vector3(-91.96573f,-20.25835f,54.1039f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok2,GemType.GEM_PURPLE,new Vector3(-41.36575f,-18.95425f,-9.680141f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_BLUE,new Vector3(-40.8331f,11.11265f,-101.5122f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_RED,new Vector3(-28.035f,8.17395f,-7.799316f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,GemType.GEM_CLEAR,new Vector3(75.79141f,13.55117f,-176.0295f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_CLEAR,new Vector3(-137.3173f,12.18633f,104.6265f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_PURPLE,new Vector3(-152.4951f,7.803614f,122.8785f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,GemType.GEM_YELLOW,new Vector3(-19.23212f,10.27694f,-56.64056f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_BLUE,new Vector3(16.80593f,8.854737f,-35.59075f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_GREEN,new Vector3(1.325815f,12.76332f,-15.9164f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,GemType.GEM_RED,new Vector3(-42.14584f,12.1575f,-62.07772f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,GemType.GEM_PURPLE,new Vector3(72.47044f,9.157012f,-64.04574f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,GemType.GEM_GREEN,new Vector3(33.76424f,8.509999f,-106.2756f)),
            new TwinsGem(ChunkType.Earth_Hub_Pier,GemType.GEM_YELLOW,new Vector3(18.07493f,-213.3366f,-468.4756f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Beach,GemType.GEM_RED,new Vector3(-35.28114f,-9.020504f,-5.951714f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Chase,GemType.GEM_CLEAR,new Vector3(73.37716f,4.741547f,0.2155212f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,GemType.GEM_YELLOW,new Vector3(75.43573f,27.4951f,-18.52006f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,GemType.GEM_BLUE,new Vector3(84.7923f,25.77177f,-39.30542f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA03,GemType.GEM_BLUE,new Vector3(-12.23604f,-0.462233f,23.77053f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,GemType.GEM_GREEN,new Vector3(-42.82871f,8.021708f,-12.1399f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA05,GemType.GEM_CLEAR,new Vector3(21.01998f,-1.060589f,-4.75337f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA06,GemType.GEM_YELLOW,new Vector3(8.593877f,-3.371867f,14.13962f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,GemType.GEM_RED,new Vector3(-24.52314f,9.9042f,-22.74868f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,GemType.GEM_PURPLE,new Vector3(8.359952f,13.60969f,-148.0028f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_YELLOW,new Vector3(23.13921f,-63.05058f,118.445f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_RED,new Vector3(53.48708f,-77.09535f,83.56358f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_PURPLE,new Vector3(58.75809f,-66.10101f,-115.0656f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_BLUE,new Vector3(108.385f,-79.05058f,-47.47729f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_CLEAR,new Vector3(-70.07782f,-95.22608f,-15.85516f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,GemType.GEM_GREEN,new Vector3(-16.82197f,-99.11121f,-46.0418f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_CLEAR,new Vector3(-3.421051f,0.3084259f,95.1685f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_YELLOW,new Vector3(-59.2377f,-15.23108f,-132.8586f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,GemType.GEM_RED,new Vector3(-6.437706f,-11.66944f,-74.4315f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_BLUE,new Vector3(-10.1212f,-53.0587f,8.050537f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_GREEN,new Vector3(-26.38887f,-14.20152f,-29.826f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,GemType.GEM_PURPLE,new Vector3(3.984783f,-5.858704f,-28.82296f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Brawl,GemType.GEM_RED,new Vector3(37.45079f,-34.82895f,106.5488f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,GemType.GEM_CLEAR,new Vector3(-39.06381f,42.81914f,-24.65672f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,GemType.GEM_YELLOW,new Vector3(153.9608f,-43.23351f,-5.100259f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,GemType.GEM_GREEN,new Vector3(-30.6158f,-120.4804f,244.4753f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,GemType.GEM_BLUE,new Vector3(-38.26909f,88.73341f,-197.7417f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,GemType.GEM_PURPLE,new Vector3(17.94812f,-40.0121f,163.8001f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_1,GemType.GEM_CLEAR,new Vector3(28.85219f,-3.918367f,62.12658f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_2,GemType.GEM_GREEN,new Vector3(-14.4f,0.8000002f,52.80002f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_3,GemType.GEM_RED,new Vector3(-8f,1.6f,-33.60001f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_4,GemType.GEM_PURPLE,new Vector3(-4.363602f,1.93657f,20.43105f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_5,GemType.GEM_YELLOW,new Vector3(46.4f,8.799999f,-17.6f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_X,GemType.GEM_BLUE,new Vector3(34.43597f,-36.66129f,-21.92944f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA03,GemType.GEM_PURPLE,new Vector3(0.448576f,-0.786973f,64.07467f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA03,GemType.GEM_RED,new Vector3(38.78387f,-0.9052219f,-72.34797f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA07,GemType.GEM_YELLOW,new Vector3(-0.1733313f,-1.962266f,19.78968f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA03,GemType.GEM_BLUE,new Vector3(-15.54741f,-2.607604f,-37.6863f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA04,GemType.GEM_CLEAR,new Vector3(-6.238373f,-10.31385f,-25.53635f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,GemType.GEM_GREEN,new Vector3(-12.40351f,58.12576f,-9.451543f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,GemType.GEM_RED,new Vector3(12.08768f,-2.6735f,166.759f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,GemType.GEM_YELLOW,new Vector3(39.12667f,3.317167f,138.1966f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof01,GemType.GEM_BLUE,new Vector3(163.4834f,24.41009f,10.86115f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,GemType.GEM_CLEAR,new Vector3(159.6622f,25.32991f,-128.0166f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,GemType.GEM_GREEN,new Vector3(74.34146f,43.41f,-39.19778f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,GemType.GEM_PURPLE,new Vector3(-19.15854f,38.41f,14.80222f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_GREEN,new Vector3(-5.347906f,-18.53108f,8.858836f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_RED,new Vector3(51.28447f,-13.25657f,-5.942781f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_PURPLE,new Vector3(34.86773f,-13.71656f,-36.90624f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_YELLOW,new Vector3(-60.98897f,-11.87395f,-59.4021f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_CLEAR,new Vector3(-35.78874f,-17.23424f,-14.95845f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,GemType.GEM_BLUE,new Vector3(-58.25388f,-12.73387f,-34.56901f)),
            
            //end of vanilla
            // Hub 1 Locations
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(82.082f,2.080f,65.679f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(104.324f,34.417f,-6.961f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(109.113f,1.296f,-51.842f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(-2.866f,9.458f,-82.545f)),
            new TwinsGem(ChunkType.Earth_Hub_HubC,new Vector3(31.187f,1.203f,-38.175f)),
            new TwinsGem(ChunkType.Earth_Hub_HubC,new Vector3(-31.462f,14.347f,-6.329f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,new Vector3(-61.712f,11.439f,-79.345f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,new Vector3(55.237f,15.484f,-39.316f)),
            new TwinsGem(ChunkType.Earth_Hub_HubD,new Vector3(-43.516f,15.325f,-51.446f)),
            new TwinsGem(ChunkType.Earth_Hub_Pier,new Vector3(-22.009f,-214.786f,-381.800f)),
            new TwinsGem(ChunkType.Earth_Hub_Pier,new Vector3(-47.977f,-212.855f,-419.530f)),
            new TwinsGem(ChunkType.Earth_Hub_Pier,new Vector3(-17.973f,-213.555f,-481.881f)),
            new TwinsGem(ChunkType.Earth_Hub_TotemEx,new Vector3(28.321f,-0.825f,78.047f)),
            // Jungle locations
            new TwinsGem(ChunkType.Earth_Hub_BossArea,new Vector3(-2.939f,-14.977f,46.547f)),
            new TwinsGem(ChunkType.Earth_Hub_BossArea,new Vector3(11.414f,-14.209f,21.116f)),
            new TwinsGem(ChunkType.Earth_Hub_HighPath,new Vector3(5.050f,-1.891f,-5.263f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,new Vector3(-34.327f,2.623f,-13.761f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,new Vector3(-42.682f,1.950f,-57.799f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,new Vector3(-46.798f,2.318f,140.495f)),
            new TwinsGem(ChunkType.Earth_Hub_HubA,new Vector3(-118.444f,11.537f,84.018f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(-64.675f,2.211f,-14.507f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(11.573f,8.912f,-43.895f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(-13.745f,2.518f,-90.856f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(17.994f,35.765f,21.684f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(15.624f,27.640f,21.703f)),
            new TwinsGem(ChunkType.Earth_Hub_HubB,new Vector3(18.994f,26.892f,-14.504f)),
            // Cavern locations
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,new Vector3(88.819f,-73.223f,-0.391f)),
            new TwinsGem(ChunkType.Earth_Cavern_AntFight,new Vector3(66.784f,-75.072f,76.410f)),
            new TwinsGem(ChunkType.Earth_Cavern_CavBridg,new Vector3(-114.725f,-7.935f,43.744f)),
            new TwinsGem(ChunkType.Earth_Cavern_CavBridg,new Vector3(-101.018f,20.907f,25.644f)),
            new TwinsGem(ChunkType.Earth_Cavern_CortThro,new Vector3(57.757f,-46.703f,117.890f)),
            new TwinsGem(ChunkType.Earth_Cavern_CortThro,new Vector3(110.780f,-44.331f,73.604f)),
            new TwinsGem(ChunkType.Earth_Cavern_CrysCave,new Vector3(-0.095f,-9.690f,6.265f)),
            new TwinsGem(ChunkType.Earth_Cavern_CrysCave,new Vector3(-16.571f,24.911f,-15.692f)),
            new TwinsGem(ChunkType.Earth_Cavern_NitroCav,new Vector3(75.154f,-33.243f,-146.511f)),
            new TwinsGem(ChunkType.Earth_Cavern_NitroCav,new Vector3(115.745f,-50.455f,-87.838f)),
            new TwinsGem(ChunkType.Earth_Cavern_Tunnel02,new Vector3(43.833f,-0.850f,37.527f)),
            // Totem locations
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok1,new Vector3(73.450f,-21.558f,38.307f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok2,new Vector3(46.554f,-27.209f,17.700f)),
            new TwinsGem(ChunkType.Earth_DocAmok_DocAmok3,new Vector3(61.223f,-18.748f,16.121f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Beach,new Vector3(-21.121f,-11.432f,-6.162f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Beach,new Vector3(-75.335f,-11.269f,-26.262f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Chase,new Vector3(-73.707f,-3.147f,-3.143f)),
            new TwinsGem(ChunkType.Earth_Totem_L03River,new Vector3(-14.196f,1.462f,-11.677f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(34.682f,14.088f,-86.394f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(-15.346f,16.027f,-40.724f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(69.815f,15.984f,-5.670f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(53.571f,31.753f,29.935f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(-26.774f,24.588f,-75.603f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(40.721f,22.255f,-57.945f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(47.859f,28.951f,40.943f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(-6.190f,27.989f,25.409f)),
            new TwinsGem(ChunkType.Earth_Totem_L03Stock,new Vector3(50.108f,30.403f,-37.832f)),
            // Hub 2 locations
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-114.719f,-81.273f,-173.875f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(14.131f,-71.431f,126.410f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(83.561f,-76.913f,-70.844f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-4.852f,-74.724f,-92.784f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-43.845f,-84.400f,1.787f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(15.243f,-20.728f,-11.478f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-12.952f,-21.014f,27.061f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(77.180f,-18.819f,75.288f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-21.852f,-20.817f,21.272f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-93.829f,14.196f,-11.486f)),
            new TwinsGem(ChunkType.Ice_Hub_LabExt,new Vector3(-11.781f,-77.570f,146.419f)),
            new TwinsGem(ChunkType.Ice_Hub_Psycho,new Vector3(-10.179f,2.107f,-20.490f)),
            new TwinsGem(ChunkType.Ice_Hub_Psycho,new Vector3(-10.120f,1.698f,19.947f)),
            // Ice Climb locations
            new TwinsGem(ChunkType.Ice_IceClimb_CaveEnt,new Vector3(-19.767f,-0.050f,4.525f)),
            new TwinsGem(ChunkType.Ice_IceClimb_CaveEnt,new Vector3(11.670f,-2.373f,39.734f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,new Vector3(9.008f,-42.304f,-14.755f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,new Vector3(-21.977f,-10.533f,25.430f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergInt,new Vector3(38.294f,8.292f,12.389f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(39.407f,-21.332f,77.739f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(-45.327f,-20.691f,13.359f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(-8.435f,-11.468f,-100.663f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(4.446f,-2.586f,-91.078f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(12.294f,3.441f,-42.724f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(9.324f,-7.060f,-23.887f)),
            new TwinsGem(ChunkType.Ice_IceClimb_BergExt,new Vector3(-27.363f,-9.019f,63.353f)),
            // Slip Slide Icecapades locations
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(-156.018f,112.051f,5.853f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(-159.547f,87.571f,-16.474f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(9.631f,19.168f,-32.480f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(73.605f,0.495f,-30.790f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(123.775f,-23.265f,-15.233f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Grind,new Vector3(146.448f,-48.256f,-15.897f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-83.488f,55.341f,-173.847f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-98.350f,29.917f,-61.786f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-68.449f,19.095f,-80.716f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-99.080f,18.502f,-70.756f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-64.506f,-23.931f,53.031f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(-50.325f,-116.834f,225.266f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(31.552f,-171.178f,210.486f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05River,new Vector3(54.986f,-213.729f,88.604f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(-69.863f,168.136f,-255.719f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(0.474f,139.617f,-232.390f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(32.279f,121.431f,-238.479f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(23.464f,116.471f,-237.737f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(94.155f,-37.013f,148.916f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(103.494f,-86.426f,256.386f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Roids,new Vector3(100.430f,-92.969f,248.500f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,new Vector3(-27.793f,17.780f,62.670f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,new Vector3(-59.626f,13.651f,78.241f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,new Vector3(-57.953f,24.643f,88.966f)),
            new TwinsGem(ChunkType.Ice_SlipSlide_L05Start,new Vector3(102.180f,-129.872f,166.742f)),
            // Hi Seas locations
            new TwinsGem(ChunkType.Ice_HighSeas_GPA01,new Vector3(-78.763f,-33.564f,56.802f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA01,new Vector3(-67.409f,-53.263f,-40.404f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA02,new Vector3(-1.215f,-6.419f,8.825f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA03,new Vector3(19.643f,-0.940f,-23.006f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA03,new Vector3(-13.903f,-6.514f,-25.820f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,new Vector3(3.906f,-8.309f,21.550f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,new Vector3(20.930f,-2.874f,-26.214f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,new Vector3(-12.794f,5.494f,30.267f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA04,new Vector3(19.779f,-2.347f,24.969f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA05,new Vector3(-12.448f,-0.189f,-0.045f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA06,new Vector3(-46.059f,-16.172f,-2.599f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA06,new Vector3(-45.172f,0.207f,4.428f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,new Vector3(18.990f,-4.900f,29.335f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,new Vector3(-26.604f,-5.081f,29.281f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,new Vector3(20.675f,4.472f,28.813f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA07,new Vector3(20.983f,-4.025f,-23.933f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(17.791f,0.801f,18.304f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(11.739f,5.967f,-39.877f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(22.231f,14.415f,-205.587f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(18.298f,-7.336f,105.099f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(25.566f,-15.077f,249.239f)),
            new TwinsGem(ChunkType.Ice_HighSeas_GPA10,new Vector3(20.602f,-21.016f,294.119f)),
            // Hub 3 locations
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(-4.454f,-42.075f,-37.691f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(38.225f,-18.254f,-20.386f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(-48.613f,-26.903f,-94.151f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(14.499f,-10.609f,-88.310f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(-30.401f,-11.385f,-57.822f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(51.953f,-27.502f,-57.489f)),
            new TwinsGem(ChunkType.School_Sch_Hub_Sch_Hub,new Vector3(-64.722f,-27.309f,-8.395f)),
            // Boiler locations
            new TwinsGem(ChunkType.School_Sch_Hub_Boil2Lck,new Vector3(26.996f,-6.472f,17.713f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_1,new Vector3(37.532f,1.930f,-37.983f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_2,new Vector3(9.431f,5.734f,60.298f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_X,new Vector3(28.704f,-32.865f,12.726f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_X,new Vector3(-28.731f,-19.021f,-13.145f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_3,new Vector3(15.120f,-10.943f,18.279f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_4,new Vector3(14.539f,5.030f,26.574f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_4,new Vector3(14.615f,-5.009f,-73.348f)),
            new TwinsGem(ChunkType.School_Boiler_Boiler_5,new Vector3(21.684f,-2.137f,-33.911f)),
            // Classroom locations
            new TwinsGem(ChunkType.School_Crash_CrGPA02,new Vector3(-39.465f,-3.198f,-5.221f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA04,new Vector3(-11.444f,-21.465f,2.601f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA04,new Vector3(12.924f,-21.647f,2.610f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA06,new Vector3(24.368f,-1.134f,12.300f)),
            new TwinsGem(ChunkType.School_Crash_CrGPA08,new Vector3(9.459f,-0.500f,-26.588f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,new Vector3(13.460f,1.494f,-0.964f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,new Vector3(12.979f,17.804f,4.710f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,new Vector3(11.934f,33.294f,4.070f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,new Vector3(-4.720f,41.864f,-15.000f)),
            new TwinsGem(ChunkType.School_Crash_CrLib,new Vector3(7.791f,41.802f,-11.439f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA01,new Vector3(21.534f,-2.551f,3.753f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA02,new Vector3(13.486f,-8.115f,10.203f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA05,new Vector3(12.307f,-8.167f,-10.901f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA06,new Vector3(3.293f,0.682f,9.750f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA06,new Vector3(33.632f,0.597f,-92.908f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA07,new Vector3(10.853f,-2.838f,-52.754f)),
            new TwinsGem(ChunkType.School_Cortex_CoGPA08,new Vector3(-7.536f,-5.692f,10.340f)),
            // Rooftop locations
            new TwinsGem(ChunkType.School_Rooftop_RoofCor1,new Vector3(15.234f,0.562f,7.272f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof01,new Vector3(163.832f,20.342f,86.993f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof01,new Vector3(102.599f,18.244f,141.980f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof02,new Vector3(1.794f,1.308f,-11.364f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,new Vector3(165.433f,12.907f,-128.379f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,new Vector3(48.234f,16.845f,-128.505f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,new Vector3(44.780f,25.001f,-91.386f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,new Vector3(38.886f,16.719f,-9.694f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof03,new Vector3(93.656f,15.663f,-16.059f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof04,new Vector3(8.848f,-0.501f,2.907f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(38.026f,15.257f,-78.954f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(52.350f,15.251f,-77.127f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(73.394f,21.722f,-87.520f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(4.970f,20.586f,7.500f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(-16.001f,23.459f,-19.167f)),
            new TwinsGem(ChunkType.School_Rooftop_Roof05,new Vector3(-47.326f,8.348f,81.267f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,new Vector3(43.463f,-4.011f,89.289f)),
            new TwinsGem(ChunkType.School_Rooftop_BusChase,new Vector3(0.773f,-5.851f,70.449f)),
            // Hub 4 locations
            new TwinsGem(ChunkType.AltEarth_Lab_Psycho,new Vector3(-8.915f,1.534f,15.342f)),
            new TwinsGem(ChunkType.AltEarth_Lab_Psycho,new Vector3(-8.548f,1.189f,-14.304f)),
            new TwinsGem(ChunkType.AltEarth_Lab_Psycho,new Vector3(24.452f,1.588f,23.574f)),
            new TwinsGem(ChunkType.AltEarth_Lab_LabExt,new Vector3(11.644f,-21.043f,-9.024f)),
            new TwinsGem(ChunkType.AltEarth_Lab_LabExt,new Vector3(-11.323f,-21.754f,22.399f)),
            new TwinsGem(ChunkType.AltEarth_Lab_LabExt,new Vector3(-19.470f,-21.121f,17.012f)),
            new TwinsGem(ChunkType.AltEarth_Lab_AltLabIn,new Vector3(2.407f,1.308f,1.749f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(-44.710f,2.814f,49.946f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(16.888f,2.374f,99.765f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(93.597f,2.854f,73.015f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(17.613f,2.497f,-88.283f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(-67.642f,2.487f,18.726f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(-29.832f,3.437f,-3.851f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(-18.924f,2.375f,38.804f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(72.612f,2.184f,132.062f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltA,new Vector3(-34.652f,15.882f,59.527f)),
            new TwinsGem(ChunkType.AltEarth_Hub_SlipJoin,new Vector3(6.013f,-10.467f,-6.331f)),
            // Rockslide locations
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,new Vector3(-34.337f,47.691f,-33.170f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,new Vector3(-18.123f,21.742f,38.981f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,new Vector3(-25.121f,-115.874f,191.983f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Start,new Vector3(-21.318f,-108.356f,200.309f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Roids,new Vector3(136.375f,77.132f,-0.727f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Roids,new Vector3(60.185f,52.643f,-0.652f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10Roids,new Vector3(-240.652f,-76.856f,71.641f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasA,new Vector3(15.593f,148.685f,-226.162f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasA,new Vector3(23.942f,134.192f,-185.353f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasA,new Vector3(47.320f,0.261f,157.074f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(-286.885f,196.518f,-46.791f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(-257.183f,178.718f,-53.065f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(-218.329f,115.996f,31.380f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(-115.629f,72.219f,26.321f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(-152.018f,86.859f,44.844f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(80.236f,6.552f,-8.710f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(278.489f,-67.254f,1.542f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(301.495f,-92.745f,-0.856f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10ChasB,new Vector3(309.094f,-85.788f,-3.689f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10End,new Vector3(2.451f,3.275f,-92.386f)),
            new TwinsGem(ChunkType.AltEarth_RockSlid_L10End,new Vector3(-1.393f,-72.134f,-12.578f)),
            // Bandicoot Pursuit locations
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc,new Vector3(99.569f,1.121f,-135.892f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc,new Vector3(75.487f,0.921f,-149.332f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc,new Vector3(-136.895f,5.400f,-78.230f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_B,new Vector3(-165.393f,7.817f,17.259f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_B,new Vector3(-164.816f,5.838f,-54.211f)),
            //new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,new Vector3(-127.669f,1.753f,106.799f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,new Vector3(130.223f,-0.628f,110.677f)),
            new TwinsGem(ChunkType.AltEarth_Hub_AltDoc_C,new Vector3(83.313f,4.056f,92.852f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,new Vector3(25.247f,1.783f,-71.690f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,new Vector3(-35.097f,1.208f,-66.630f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,new Vector3(61.715f,1.492f,86.066f)),
            new TwinsGem(ChunkType.AltEarth_Hub_CoreEnt,new Vector3(-26.573f,2.608f,35.937f)),
            // Ant Agony locations
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-98.549f,7.419f,286.114f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-122.229f,1.185f,391.540f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-130.561f,1.983f,415.988f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-99.254f,1.391f,430.731f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-127.395f,1.383f,446.065f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreA,new Vector3(-97.804f,2.174f,507.236f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,new Vector3(-72.963f,7.992f,-10.188f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreB,new Vector3(63.351f,0.669f,15.546f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,new Vector3(76.405f,2.962f,121.345f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,new Vector3(76.566f,2.396f,90.157f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,new Vector3(-31.942f,7.422f,100.100f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreC,new Vector3(-12.780f,7.360f,105.565f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,new Vector3(19.446f,14.338f,-40.628f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,new Vector3(104.043f,1.521f,64.170f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,new Vector3(89.558f,1.577f,91.220f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,new Vector3(108.825f,1.365f,113.479f)),
            new TwinsGem(ChunkType.AltEarth_Core_CoreD,new Vector3(83.015f,1.367f,132.821f)),
            new TwinsGem(ChunkType.AltEarth_Core_PreTreas,new Vector3(-12.405f,0.955f,-50.334f)),
            new TwinsGem(ChunkType.AltEarth_Core_AftTreas,new Vector3(-12.269f,1.662f,-21.280f)),
            new TwinsGem(ChunkType.AltEarth_Core_AftTreas,new Vector3(-15.381f,0.840f,6.308f)),
            new TwinsGem(ChunkType.AltEarth_Core_AftTreas,new Vector3(-15.992f,1.140f,91.121f)),
            new TwinsGem(ChunkType.AltEarth_Core_AftTreas,new Vector3(-8.199f,1.216f,77.091f)),
        };

        

        public static void Twins_Randomize_Gems(ref Random randState)
        {
            All_Gems = new List<TwinsGem>();

            List<GemType> GemsToGo = new List<GemType>();
            List<TwinsGem> PossibleLocations = new List<TwinsGem>();
            int targetLocation = 0;
            int targetGem = 0;

            for (int level = 0; level < 16; level ++)
            {
                GemsToGo.Add(GemType.GEM_BLUE);
                GemsToGo.Add(GemType.GEM_CLEAR);
                GemsToGo.Add(GemType.GEM_GREEN);
                GemsToGo.Add(GemType.GEM_PURPLE);
                GemsToGo.Add(GemType.GEM_RED);
                GemsToGo.Add(GemType.GEM_YELLOW);

                for (int spawn = 0; spawn < GemSpawnPoints.Count; spawn ++)
                {
                    for (int ch = 0; ch < All_Chunks.Count; ch++)
                    {
                        if (GemSpawnPoints[spawn].chunk == All_Chunks[ch].Chunk && All_Chunks[ch].LevelArea == (LevelType)level)
                        {
                            PossibleLocations.Add(GemSpawnPoints[spawn]);
                        }
                    }
                }

                while (GemsToGo.Count > 0)
                {
                    targetLocation = randState.Next(0, PossibleLocations.Count);
                    targetGem = randState.Next(0, GemsToGo.Count);
                    All_Gems.Add(new TwinsGem(PossibleLocations[targetLocation].chunk, GemsToGo[targetGem], PossibleLocations[targetLocation].pos));
                    PossibleLocations.RemoveAt(targetLocation);
                    GemsToGo.RemoveAt(targetGem);
                }

                GemsToGo.Clear();
                PossibleLocations.Clear();
            }
        }

        public static ChunkType ChunkPathToType(string path,string bdpath)
        {
            ChunkType type = ChunkType.Invalid;

            for (int i = 0; i < All_Chunks.Count; i++)
            {
                string comparePath = bdpath.ToLower() + @"levels\" + All_Chunks[i].Path.ToLower() + ".rm2";
                if (comparePath == path.ToLower())
                {
                    type = All_Chunks[i].Chunk;
                    break;
                }
            }

            if (type == ChunkType.Invalid)
            {
                string comparePath1 = bdpath.ToLower() + All_Chunks[0].Path.ToLower();
                Console.WriteLine("invalid Chunk");
                Console.WriteLine("bd path: " + bdpath.ToLower());
                Console.WriteLine("any chunk path: " + All_Chunks[0].Path.ToLower());
                Console.WriteLine("file path: " + path.ToLower());
                Console.WriteLine("compare path: " + comparePath1);
            }

            return type;
        }

        public static List<CachedGameObject> cachedGameObjects = new List<CachedGameObject>();

        public static void ExportGameObject(ref TwinsFile RM_Archive, ObjectID objectID, ref List<ObjectID> objectsExported)
        {
            if (objectsExported.Contains(objectID))
            {
                return;
            }
            if (cachedGameObjects.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects.Count; i++)
                {
                    if (cachedGameObjects[i].mainObject.ID == (uint)objectID)
                    {
                        return;
                    }
                }
            }

            CachedGameObject gameObject = new CachedGameObject();

            TwinsSection gfx_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Graphics);
            TwinsSection tex_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Textures);
            TwinsSection mat_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Materials);
            TwinsSection mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Meshes);
            TwinsSection mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Models);
            TwinsSection armdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.ArmatureModel);
            TwinsSection acmdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.ActorModel);

            TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
            TwinsSection anim_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Animation);
            TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
            TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
            TwinsSection ogi_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.OGI);
            TwinsSection comdl_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.CodeModel);
            TwinsSection sfx_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE);
            TwinsSection sfx_eng_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Eng);
            TwinsSection sfx_fre_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Fre);
            TwinsSection sfx_ger_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Ger);
            TwinsSection sfx_ita_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Ita);
            TwinsSection sfx_spa_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Spa);
            TwinsSection sfx_unu_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Unused);

            // may need a check to see if the section exists?

            GameObject targetObject = null;
            for (int i = 0; i < object_section.Records.Count; i++)
            {
                if (object_section.Records[i].ID == (uint)objectID)
                {
                    targetObject = (GameObject)object_section.Records[i];
                }
            }
            if (targetObject == null)
            {
                return;
            }
            objectsExported.Add(objectID);

            ushort[] animList = targetObject.Anims;
            List<ushort> export_anim = GetValidIDs(ref animList);

            for (int i = 0; i < anim_section.Records.Count; i++)
            {
                if (export_anim.Contains((ushort)anim_section.Records[i].ID))
                {
                    if (gameObject.list_anims == null)
                    {
                        gameObject.list_anims = new List<TwinsItem>();
                    }
                    gameObject.list_anims.Add(anim_section.Records[i]);
                }
            }

            ushort[] objList = targetObject.Objects;
            List<ushort> export_objects = GetValidIDs(ref objList);
            for (int i = 0; i < object_section.Records.Count; i++)
            {
                if ((ushort)objectID != object_section.Records[i].ID && !objectsExported.Contains((ObjectID)object_section.Records[i].ID) && export_objects.Contains((ushort)object_section.Records[i].ID))
                {
                    ExportGameObject(ref RM_Archive, (ObjectID)object_section.Records[i].ID, ref objectsExported);
                    if (gameObject.list_subobjects == null)
                    {
                        gameObject.list_subobjects = new List<ObjectID>();
                    }
                    if (!gameObject.list_subobjects.Contains((ObjectID)object_section.Records[i].ID))
                    {
                        gameObject.list_subobjects.Add((ObjectID)object_section.Records[i].ID);
                    }
                }
                else if ((ushort)objectID == object_section.Records[i].ID)
                {
                    gameObject.mainObject = (GameObject)object_section.Records[i];
                }
            }

            ushort[] ogiList = targetObject.OGIs;
            List<ushort> export_ogi = GetValidIDs(ref ogiList);

            for (int i = 0; i < ogi_section.Records.Count; i++)
            {
                if (export_ogi.Contains((ushort)ogi_section.Records[i].ID))
                {
                    if (gameObject.list_ogi == null)
                    {
                        gameObject.list_ogi = new List<TwinsItem>();
                    }
                    gameObject.list_ogi.Add(ogi_section.Records[i]);
                }
            }

            ushort[] scriptList = targetObject.Scripts;
            List<ushort> export_script = GetValidIDs(ref scriptList);

            for (int i = 0; i < script_section.Records.Count; i++)
            {
                if (export_script.Contains((ushort)script_section.Records[i].ID))
                {
                    if (gameObject.list_scripts == null)
                    {
                        gameObject.list_scripts = new List<Script>();
                    }
                    gameObject.list_scripts.Add((Script)script_section.Records[i]);
                }
            }

            // gameobject -> codemodel
            ushort[] codemodelList = targetObject.cCM;
            List<ushort> export_comdl = GetValidIDs(ref codemodelList);

            for (int i = 0; i < comdl_section.Records.Count; i++)
            {
                if (export_comdl.Contains((ushort)comdl_section.Records[i].ID))
                {
                    if (gameObject.list_codemodels == null)
                    {
                        gameObject.list_codemodels = new List<TwinsItem>();
                    }
                    gameObject.list_codemodels.Add(comdl_section.Records[i]);
                }
            }

            ushort[] soundList = targetObject.Sounds;
            List<ushort> export_sounds = GetValidIDs(ref soundList);

            for (int i = 0; i < sfx_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_section.Records[i].ID))
                {
                    if (gameObject.list_sounds == null)
                    {
                        gameObject.list_sounds = new List<SoundEffect>();
                    }
                    gameObject.list_sounds.Add((SoundEffect)sfx_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_eng_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_eng_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_english == null)
                    {
                        gameObject.list_sounds_english = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_english.Add((SoundEffect)sfx_eng_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_ger_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_ger_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_german == null)
                    {
                        gameObject.list_sounds_german = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_german.Add((SoundEffect)sfx_ger_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_fre_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_fre_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_french == null)
                    {
                        gameObject.list_sounds_french = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_french.Add((SoundEffect)sfx_fre_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_ita_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_ita_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_italian == null)
                    {
                        gameObject.list_sounds_italian = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_italian.Add((SoundEffect)sfx_ita_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_spa_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_spa_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_spanish == null)
                    {
                        gameObject.list_sounds_spanish = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_spanish.Add((SoundEffect)sfx_spa_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_unu_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_unu_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_unused == null)
                    {
                        gameObject.list_sounds_unused = new List<SoundEffect>();
                    }
                    gameObject.list_sounds_unused.Add((SoundEffect)sfx_unu_section.Records[i]);
                }
            }

            // todo: ogi -> model, armaturemodel, actormodel
            List<uint> export_mdl = new List<uint>();
            List<uint> export_armdl = new List<uint>();
            List<uint> export_acmdl = new List<uint>();
            for (int ogi = 0; ogi < gameObject.list_ogi.Count; ogi++)
            {
                //export_armdl.Add(gameObject.list_ogi[ogi].ArmatureModelID);
                //export_acmdl.Add(gameObject.list_ogi[ogi].ActorModelID);
                //for (int i = 0; i < gameObject.list_ogi[ogi].ModelIDs.Length; i++)
                //{
                //    export_mdl.Add(gameObject.list_ogi[ogi].ModelIDs[i]);
                //}
            }
            for (int i = 0; i < mdl_section.Records.Count; i++)
            {
                if (export_mdl.Contains(mdl_section.Records[i].ID))
                {
                    if (gameObject.list_models == null)
                    {
                        gameObject.list_models = new List<Model>();
                    }
                    gameObject.list_models.Add((Model)mdl_section.Records[i]);
                }
            }
            for (int i = 0; i < armdl_section.Records.Count; i++)
            {
                if (export_armdl.Contains(armdl_section.Records[i].ID))
                {
                    if (gameObject.list_armaturemodels == null)
                    {
                        gameObject.list_armaturemodels = new List<TwinsItem>();
                    }
                    gameObject.list_armaturemodels.Add(armdl_section.Records[i]);
                }
            }
            for (int i = 0; i < acmdl_section.Records.Count; i++)
            {
                if (export_acmdl.Contains(acmdl_section.Records[i].ID))
                {
                    if (gameObject.list_actormodels == null)
                    {
                        gameObject.list_actormodels = new List<TwinsItem>();
                    }
                    gameObject.list_actormodels.Add(acmdl_section.Records[i]);
                }
            }

            List<uint> export_mat = new List<uint>();
            List<uint> export_mesh = new List<uint>();
            for (int mdl = 0; mdl < gameObject.list_models.Count; mdl++)
            {
                export_mesh.Add(gameObject.list_models[mdl].MeshID);
                for (int i = 0; i < gameObject.list_models[mdl].MaterialIDs.Length; i++)
                {
                    export_mat.Add(gameObject.list_models[mdl].MaterialIDs[i]);
                }
            }
            for (int i = 0; i < mesh_section.Records.Count; i++)
            {
                if (export_mesh.Contains(mesh_section.Records[i].ID))
                {
                    if (gameObject.list_meshes == null)
                    {
                        gameObject.list_meshes = new List<Mesh>();
                    }
                    gameObject.list_meshes.Add((Mesh)mesh_section.Records[i]);
                }
            }
            for (int i = 0; i < mat_section.Records.Count; i++)
            {
                if (export_mat.Contains(mat_section.Records[i].ID))
                {
                    if (gameObject.list_materials == null)
                    {
                        gameObject.list_materials = new List<Material>();
                    }
                    gameObject.list_materials.Add((Material)mat_section.Records[i]);
                }
            }

            List<uint> export_tex = new List<uint>();
            for (int mat = 0; mat < gameObject.list_materials.Count; mat++)
            {
                export_tex.Add(gameObject.list_materials[mat].Tex);
            }
            for (int i = 0; i < tex_section.Records.Count; i++)
            {
                if (export_tex.Contains(tex_section.Records[i].ID))
                {
                    if (gameObject.list_textures == null)
                    {
                        gameObject.list_textures = new List<Texture>();
                    }
                    gameObject.list_textures.Add((Texture)tex_section.Records[i]);
                }
            }

            bool loadedTemplate = false;
            for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                    for (int i = 0; i < instances.Records.Count; ++i)
                    {
                        Instance instance = (Instance)instances.Records[i];
                        if (instance.ObjectID == (ushort)objectID)
                        {
                            // try an instance without dependencies on other instances first
                            if ((instance.InstanceIDs == null || instance.InstanceIDs.Count == 0) && (instance.PathIDs == null || instance.PathIDs.Count == 0) && (instance.PositionIDs == null || instance.PositionIDs.Count == 0))
                            {
                                gameObject.instanceTemplate = new InstanceTemplate()
                                {
                                    ObjectID = instance.ObjectID,
                                    Properties = instance.UnkI32,
                                    Flags = instance.UnkI321,
                                    FloatVars = instance.UnkI322,
                                    IntVars = instance.UnkI323,
                                    InstancesNum = instance.SomeNum1,
                                    PositionsNum = instance.SomeNum2,
                                    PathsNum = instance.SomeNum3,
                                    InstanceIDs = instance.InstanceIDs,
                                    PositionIDs = instance.PositionIDs,
                                    PathIDs = instance.PathIDs
                                };
                                loadedTemplate = true;
                                break;
                            }
                        }
                    }
                    if (loadedTemplate)
                    {
                        break;
                    }
                }
                if (loadedTemplate)
                {
                    break;
                }
            }
            if (!loadedTemplate)
            {
                for (uint section_id = (uint)RM2_Sections.Instances1; section_id <= (uint)RM2_Sections.Instances8; section_id++)
                {
                    if (!RM_Archive.ContainsItem(section_id)) continue;
                    TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                    if (section.Records.Count > 0)
                    {
                        if (!section.ContainsItem((uint)RM2_Instance_Sections.ObjectInstance)) continue;
                        TwinsSection instances = section.GetItem<TwinsSection>((uint)RM2_Instance_Sections.ObjectInstance);
                        for (int i = 0; i < instances.Records.Count; ++i)
                        {
                            Instance instance = (Instance)instances.Records[i];
                            if (instance.ObjectID == (ushort)objectID)
                            {
                                gameObject.instanceTemplate = new InstanceTemplate()
                                {
                                    ObjectID = instance.ObjectID,
                                    Properties = instance.UnkI32,
                                    Flags = instance.UnkI321,
                                    FloatVars = instance.UnkI322,
                                    IntVars = instance.UnkI323,
                                    InstancesNum = instance.SomeNum1,
                                    PositionsNum = instance.SomeNum2,
                                    PathsNum = instance.SomeNum3,
                                    InstanceIDs = instance.InstanceIDs,
                                    PositionIDs = instance.PositionIDs,
                                    PathIDs = instance.PathIDs
                                };
                                loadedTemplate = true;
                                break;
                            }
                        }
                        if (loadedTemplate)
                        {
                            break;
                        }
                    }
                    if (loadedTemplate)
                    {
                        break;
                    }
                }
            }

            cachedGameObjects.Add(gameObject);
        }

        public static List<ushort> GetValidIDs(ref ushort[] itemList)
        {
            List<ushort> validItems = new List<ushort>();

            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] != 65535 && !validItems.Contains(itemList[i]))
                {
                    validItems.Add(itemList[i]);
                }
            }

            return validItems;
        }

        public static void ImportGameObject(ref TwinsFile RM_Archive, ObjectID objectID, ref List<ObjectID> importedObjects)
        {
            if (cachedGameObjects.Count <= 0)
            {
                return;
            }
            int targetObject = -1;
            for (int i = 0; i < cachedGameObjects.Count; i++)
            {
                if (cachedGameObjects[i].mainObject.ID == (uint)objectID)
                {
                    targetObject = i;
                    break;
                }
            }
            if (targetObject == -1)
            {
                return;
            }
            if (importedObjects.Contains(objectID))
            {
                return;
            }
            importedObjects.Add(objectID);

            if (cachedGameObjects[targetObject].list_subobjects != null & cachedGameObjects[targetObject].list_subobjects.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_subobjects.Count; i++)
                {
                    ImportGameObject(ref RM_Archive, cachedGameObjects[targetObject].list_subobjects[i], ref importedObjects);
                }
            }

            TwinsSection gfx_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Graphics);
            TwinsSection tex_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Textures);
            TwinsSection mat_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Materials);
            TwinsSection mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Meshes);
            TwinsSection mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.Models);
            TwinsSection armdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.ArmatureModel);
            TwinsSection acmdl_section = gfx_section.GetItem<TwinsSection>((uint)RM2_Graphics_Sections.ActorModel);

            TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM2_Sections.Code);
            TwinsSection anim_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Animation);
            TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Object);
            TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.Script);
            TwinsSection ogi_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.OGI);
            TwinsSection comdl_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.CodeModel);
            TwinsSection sfx_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE);
            TwinsSection sfx_eng_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Eng);
            TwinsSection sfx_fre_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Fre);
            TwinsSection sfx_ger_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Ger);
            TwinsSection sfx_ita_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Ita);
            TwinsSection sfx_spa_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Spa);
            TwinsSection sfx_unu_section = code_section.GetItem<TwinsSection>((uint)RM2_Code_Sections.SE_Unused);

            // may need a check to see if the section exists?

            if (cachedGameObjects[targetObject].list_textures != null && cachedGameObjects[targetObject].list_textures.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_textures.Count; i++)
                {
                    if (!SectionContainsItemID(ref tex_section.Records, cachedGameObjects[targetObject].list_textures[i].ID))
                    {
                        tex_section.Records.Add(cachedGameObjects[targetObject].list_textures[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_materials != null && cachedGameObjects[targetObject].list_materials.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_materials.Count; i++)
                {
                    if (!SectionContainsItemID(ref mat_section.Records, cachedGameObjects[targetObject].list_materials[i].ID))
                    {
                        mat_section.Records.Add(cachedGameObjects[targetObject].list_materials[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_meshes != null && cachedGameObjects[targetObject].list_meshes.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_meshes.Count; i++)
                {
                    if (!SectionContainsItemID(ref mesh_section.Records, cachedGameObjects[targetObject].list_meshes[i].ID))
                    {
                        mesh_section.Records.Add(cachedGameObjects[targetObject].list_meshes[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_models != null && cachedGameObjects[targetObject].list_models.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_models.Count; i++)
                {
                    if (!SectionContainsItemID(ref mdl_section.Records, cachedGameObjects[targetObject].list_models[i].ID))
                    {
                        mdl_section.Records.Add(cachedGameObjects[targetObject].list_models[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_armaturemodels != null && cachedGameObjects[targetObject].list_armaturemodels.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_armaturemodels.Count; i++)
                {
                    if (!SectionContainsItemID(ref armdl_section.Records, cachedGameObjects[targetObject].list_armaturemodels[i].ID))
                    {
                        armdl_section.Records.Add(cachedGameObjects[targetObject].list_armaturemodels[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_actormodels != null && cachedGameObjects[targetObject].list_actormodels.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_actormodels.Count; i++)
                {
                    if (!SectionContainsItemID(ref acmdl_section.Records, cachedGameObjects[targetObject].list_actormodels[i].ID))
                    {
                        acmdl_section.Records.Add(cachedGameObjects[targetObject].list_actormodels[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_anims != null && cachedGameObjects[targetObject].list_anims.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_anims.Count; i++)
                {
                    if (!SectionContainsItemID(ref anim_section.Records, cachedGameObjects[targetObject].list_anims[i].ID))
                    {
                        anim_section.Records.Add(cachedGameObjects[targetObject].list_anims[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_scripts != null && cachedGameObjects[targetObject].list_scripts.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_scripts.Count; i++)
                {
                    if (!SectionContainsItemID(ref script_section.Records, cachedGameObjects[targetObject].list_scripts[i].ID))
                    {
                        script_section.Records.Add(cachedGameObjects[targetObject].list_scripts[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_ogi != null && cachedGameObjects[targetObject].list_ogi.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_ogi.Count; i++)
                {
                    if (!SectionContainsItemID(ref ogi_section.Records, cachedGameObjects[targetObject].list_ogi[i].ID))
                    {
                        ogi_section.Records.Add(cachedGameObjects[targetObject].list_ogi[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_codemodels != null && cachedGameObjects[targetObject].list_codemodels.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_codemodels.Count; i++)
                {
                    if (!SectionContainsItemID(ref comdl_section.Records, cachedGameObjects[targetObject].list_codemodels[i].ID))
                    {
                        comdl_section.Records.Add(cachedGameObjects[targetObject].list_codemodels[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds != null && cachedGameObjects[targetObject].list_sounds.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_section.Records, cachedGameObjects[targetObject].list_sounds[i].ID))
                    {
                        sfx_section.Records.Add(cachedGameObjects[targetObject].list_sounds[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_english != null && cachedGameObjects[targetObject].list_sounds_english.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_english.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_eng_section.Records, cachedGameObjects[targetObject].list_sounds_english[i].ID))
                    {
                        sfx_eng_section.Records.Add(cachedGameObjects[targetObject].list_sounds_english[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_french != null && cachedGameObjects[targetObject].list_sounds_french.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_german.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_ger_section.Records, cachedGameObjects[targetObject].list_sounds_german[i].ID))
                    {
                        sfx_ger_section.Records.Add(cachedGameObjects[targetObject].list_sounds_german[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_german != null && cachedGameObjects[targetObject].list_sounds_german.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_french.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_fre_section.Records, cachedGameObjects[targetObject].list_sounds_french[i].ID))
                    {
                        sfx_fre_section.Records.Add(cachedGameObjects[targetObject].list_sounds_french[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_italian != null && cachedGameObjects[targetObject].list_sounds_italian.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_italian.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_ita_section.Records, cachedGameObjects[targetObject].list_sounds_italian[i].ID))
                    {
                        sfx_ita_section.Records.Add(cachedGameObjects[targetObject].list_sounds_italian[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_spanish != null && cachedGameObjects[targetObject].list_sounds_spanish.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_spanish.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_spa_section.Records, cachedGameObjects[targetObject].list_sounds_spanish[i].ID))
                    {
                        sfx_spa_section.Records.Add(cachedGameObjects[targetObject].list_sounds_spanish[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_sounds_unused != null && cachedGameObjects[targetObject].list_sounds_unused.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_sounds_unused.Count; i++)
                {
                    if (!SectionContainsItemID(ref sfx_unu_section.Records, cachedGameObjects[targetObject].list_sounds_unused[i].ID))
                    {
                        sfx_unu_section.Records.Add(cachedGameObjects[targetObject].list_sounds_unused[i]);
                    }
                }
            }

            object_section.Records.Add(cachedGameObjects[targetObject].mainObject);

        }

        private static bool SectionContainsItemID(ref List<TwinsItem> section, uint ID)
        {
            for (int i = 0; i < section.Count; i++)
            {
                if (section[i].ID == ID)
                {
                    return true;
                }
            }
            return false;
        }

        public static InstanceTemplate GetInstanceTemplateByObjectID(ObjectID objectID)
        {
            for (int i = 0; i < cachedGameObjects.Count; i++)
            {
                if (cachedGameObjects[i].mainObject.ID == (uint)objectID)
                {
                    return cachedGameObjects[i].instanceTemplate;
                }
            }
            return new InstanceTemplate();
        }

        public static void Twins_Randomize_Character(int charID, ref Random randState)
        {

            CharFloats_AirGravity[charID] = randState.Next(40, 60);
            CharFloats_WalkSpeed[charID] = randState.Next(20, 60) / 10f;
            CharFloats_RunSpeed[charID] = randState.Next(7, 14);
            CharFloats_SpinThrowForwardForce[charID] = randState.Next(7, 15);
            CharFloats_SpinLength[charID] = randState.Next(25, 100) / 100f;
            CharFloats_SpinDelay[charID] = randState.Next(10, 20) / 100f;
            CharFloats_JumpAirSpeed[charID] = randState.Next(6, 10);
            CharFloats_JumpHeight[charID] = randState.Next(13, 18);
            CharFloats_CrawlSpeed[charID] = randState.Next(125, 400) / 100f;
            CharFloats_SlideSpeed[charID] = randState.Next(10, 24);

            if (charID == (int)CharacterID.Crash)
            {
                CharFloats_DoubleJumpHeight[charID] = CharFloats_JumpHeight[charID] + randState.Next(1,5);
                CharFloats_SlideJumpUnk24[charID] = randState.Next(9, 16);
                CharFloats_BodyslamHangTime[charID] = randState.Next(20, 60) / 100f;
            }
            else if (charID == (int)CharacterID.Cortex)
            {
                CharFloats_StrafingSpeed[charID] = randState.Next(3, 10);
                CharFloats_GunChargeTime[charID] = randState.Next(10, 100) / 100f;
                CharFloats_GunTimeBetweenChargedShots[charID] = randState.Next(10, 100) / 100f;
                CharFloats_GunTimeBetweenShots[charID] = randState.Next(2, 10) / 100f;
            }
            else if (charID == (int)CharacterID.Nina)
            {

            }
            else if (charID == (int)CharacterID.Mechabandicoot)
            {
                CharFloats_WalkSpeed[charID] = randState.Next(120, 240) / 10f;
                CharFloats_RunSpeed[charID] = 0;
            }

        }

    }

    struct TwinsLevelChunk
    {
        public Twins_Data.ChunkType Chunk;
        /// <summary> The game level that the chunk belongs to </summary>
        public Twins_Data.LevelType LevelArea;
        /// <summary> Lowercase, no extension </summary>
        public string Path;

        public TwinsLevelChunk(Twins_Data.ChunkType ch, Twins_Data.LevelType la, string p)
        {
            Chunk = ch;
            LevelArea = la;
            Path = p;
        }
    }

    struct TwinsGem
    {
        public Twins_Data.ChunkType chunk;
        public Twins_Data.GemType type;
        public Vector3 pos;

        public TwinsGem(Twins_Data.ChunkType ch, Twins_Data.GemType gem, Vector3 p)
        {
            chunk = ch;
            type = gem;
            pos = p;
        }

        public TwinsGem(Twins_Data.ChunkType ch, Vector3 p)
        {
            chunk = ch;
            type = Twins_Data.GemType.GEM_RED;
            pos = p;
        }
    }

    struct CachedGameObject
    {
        public GameObject mainObject;
        public List<TwinsItem> list_anims;
        public List<Model> list_models;
        public List<TwinsItem> list_armaturemodels;
        public List<TwinsItem> list_codemodels;
        public List<TwinsItem> list_actormodels;
        public List<Material> list_materials;
        public List<Mesh> list_meshes;
        public List<TwinsItem> list_ogi;
        public List<Script> list_scripts;
        public List<SoundEffect> list_sounds;
        public List<SoundEffect> list_sounds_english;
        public List<SoundEffect> list_sounds_french;
        public List<SoundEffect> list_sounds_german;
        public List<SoundEffect> list_sounds_italian;
        public List<SoundEffect> list_sounds_spanish;
        public List<SoundEffect> list_sounds_unused;
        public List<Texture> list_textures;
        public List<Twins_Data.ObjectID> list_subobjects;
        public InstanceTemplate instanceTemplate;
    }

    struct InstanceTemplate
    {
        public ushort ObjectID;
        //UnkI32, in hex
        public uint Properties;
        public List<uint> Flags;
        public List<float> FloatVars;
        public List<uint> IntVars;

        //These are based on other instances in the same chunk so just use their count
        public List<ushort> InstanceIDs;
        public List<ushort> PathIDs;
        public List<ushort> PositionIDs;

        //These are always at 10?
        public int InstancesNum;
        public int PositionsNum;
        public int PathsNum;
    }

    struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float cx, float cy, float cz)
        {
            X = cx;
            Y = cy;
            Z = cz;
        }
    }
}
