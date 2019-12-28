using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AltTunl,LevelType.Hub4,@"altearth\hub\alttunl"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_AlwaysOn,LevelType.Hub4,@"altearth\hub\alwayson"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_CoreEnt,LevelType.BP,@"altearth\hub\coreent"),
            new TwinsLevelChunk(ChunkType.AltEarth_Hub_SlipJoin,LevelType.Rockslide,@"altearth\hub\slipjoin"),
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
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_UkaFight,LevelType.IceClimb,@"ice\iceclimb\ukafight"),
            new TwinsLevelChunk(ChunkType.Ice_IceClimb_UkaTrans,LevelType.IceClimb,@"ice\iceclimb\ukatrans"),
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
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Boil2Lck,LevelType.Boiler,@"school\sch_hub\boil2lck"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Sch_Hub,LevelType.Hub3,@"school\sch_hub\sch_hub"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SchShip,LevelType.Hub3,@"school\sch_hub\schship"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SLK01,LevelType.Boiler,@"school\sch_hub\slk01"),
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
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(11.81f,1.38f,-24.95f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(10.24f,1.38f,-21.28f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(7.08f,0.92f,-17.34f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(4.62f,0.99f,-14.19f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(3.64f,0.80f,-10.73f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(4.39f,0.80f,-6.80f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(3.87f,0.87f,-3.29f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(0.15f,0.75f,1.39f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(-3.63f,0.57f,4.96f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(-7.68f,1.28f,10.25f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(-10.35f,0.90f,15.16f)),
            new TwinsGem(ChunkType.Earth_Hub_Beach,new Vector3(-13.98f,1.11f,19.93f)),
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
