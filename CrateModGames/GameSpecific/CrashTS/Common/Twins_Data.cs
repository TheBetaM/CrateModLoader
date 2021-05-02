using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity;

namespace CrateModLoader.GameSpecific.CrashTS
{
    static class Twins_Data
    {

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
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Boil2Lck,LevelType.Boiler,@"school\sch_hub\boil2lck"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_Sch_Hub,LevelType.Hub3,@"school\sch_hub\sch_hub"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SchShip,LevelType.Hub3,@"school\sch_hub\schship"),
            new TwinsLevelChunk(ChunkType.School_Sch_Hub_SLK01,LevelType.Hub3,@"school\sch_hub\slk01"),
            new TwinsLevelChunk(ChunkType.Default,LevelType.Hub1,@"startup\default"),
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

        // Object export/import stuff

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

            TwinsSection gfx_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Graphics);
            TwinsSection tex_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);
            TwinsSection mat_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Materials);
            TwinsSection mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Models);
            TwinsSection mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.RigidModels);
            TwinsSection armdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Skin);
            TwinsSection acmdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.BlendSkin);

            TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
            TwinsSection anim_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Animation);
            TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
            TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
            TwinsSection ogi_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.OGI);
            TwinsSection comdl_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.CodeModel);
            TwinsSection sfx_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE);
            TwinsSection sfx_eng_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Eng);
            TwinsSection sfx_fre_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Fre);
            TwinsSection sfx_ger_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Ger);
            TwinsSection sfx_ita_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Ita);
            TwinsSection sfx_spa_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Spa);
            TwinsSection sfx_unu_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Unused);

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

            List<ushort> animList = targetObject.cAnims;
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

            List<ushort> objList = targetObject.cObjects;
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

            List<ushort> ogiList = targetObject.cOGIs;
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

            List<ushort> scriptList = targetObject.cScripts;
            List<ushort> export_script = GetValidIDs(ref scriptList);

            for (int i = 0; i < script_section.Records.Count; i++)
            {
                if (export_script.Contains((ushort)script_section.Records[i].ID))
                {
                    if (gameObject.list_scripts == null)
                    {
                        gameObject.list_scripts = new List<TwinsItem>();
                    }
                    gameObject.list_scripts.Add(script_section.Records[i]);
                }
            }

            List<ushort> codemodelList = targetObject.cCM;
            List<ushort> export_comdl = GetValidIDs(ref codemodelList);

            for (int i = 0; i < comdl_section.Records.Count; i++)
            {
                if (export_comdl.Contains((ushort)comdl_section.Records[i].ID))
                {
                    if (gameObject.list_scriptpacks == null)
                    {
                        gameObject.list_scriptpacks = new List<TwinsItem>();
                    }
                    gameObject.list_scriptpacks.Add(comdl_section.Records[i]);
                }
            }

            /* exporting/importing sound data is broken right now
            
            ushort[] soundList = targetObject.cSounds;
            List<ushort> export_sounds = GetValidIDs(ref soundList);
            
            for (int i = 0; i < sfx_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_section.Records[i].ID))
                {
                    if (gameObject.list_sounds == null)
                    {
                        gameObject.list_sounds = new List<TwinsItem>();
                    }
                    gameObject.list_sounds.Add(sfx_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_eng_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_eng_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_english == null)
                    {
                        gameObject.list_sounds_english = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_english.Add(sfx_eng_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_ger_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_ger_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_german == null)
                    {
                        gameObject.list_sounds_german = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_german.Add(sfx_ger_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_fre_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_fre_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_french == null)
                    {
                        gameObject.list_sounds_french = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_french.Add(sfx_fre_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_ita_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_ita_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_italian == null)
                    {
                        gameObject.list_sounds_italian = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_italian.Add(sfx_ita_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_spa_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_spa_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_spanish == null)
                    {
                        gameObject.list_sounds_spanish = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_spanish.Add(sfx_spa_section.Records[i]);
                }
            }
            for (int i = 0; i < sfx_unu_section.Records.Count; i++)
            {
                if (export_sounds.Contains((ushort)sfx_unu_section.Records[i].ID))
                {
                    if (gameObject.list_sounds_unused == null)
                    {
                        gameObject.list_sounds_unused = new List<TwinsItem>();
                    }
                    gameObject.list_sounds_unused.Add(sfx_unu_section.Records[i]);
                }
            }
            */

            List<uint> export_mdl = new List<uint>();
            List<uint> export_armdl = new List<uint>();
            List<uint> export_acmdl = new List<uint>();
            if (gameObject.list_ogi != null)
            {
                for (int ogi = 0; ogi < gameObject.list_ogi.Count; ogi++)
                {
                    GraphicsInfo This_GI = (GraphicsInfo)gameObject.list_ogi[ogi];
                    if (This_GI.SkinID != 0)
                    {
                        export_armdl.Add(This_GI.SkinID);
                    }
                    if (This_GI.BlendSkinID != 0)
                    {
                        export_acmdl.Add(This_GI.BlendSkinID);
                    }
                    if (This_GI.ModelIDs.Length > 0)
                    {
                        for (int i = 0; i < This_GI.ModelIDs.Length; i++)
                        {
                            export_mdl.Add(This_GI.ModelIDs[i].ModelID);
                        }
                    }
                }
            }
            if (export_mdl.Count > 0)
            {
                for (int i = 0; i < mdl_section.Records.Count; i++)
                {
                    if (export_mdl.Contains(mdl_section.Records[i].ID))
                    {
                        if (gameObject.list_rigidmodels == null)
                        {
                            gameObject.list_rigidmodels = new List<TwinsItem>();
                        }
                        gameObject.list_rigidmodels.Add(mdl_section.Records[i]);
                    }
                }
            }
            if (export_armdl.Count > 0)
            {
                for (int i = 0; i < armdl_section.Records.Count; i++)
                {
                    if (export_armdl.Contains(armdl_section.Records[i].ID))
                    {
                        if (gameObject.list_skins == null)
                        {
                            gameObject.list_skins = new List<TwinsItem>();
                        }
                        gameObject.list_skins.Add(armdl_section.Records[i]);
                    }
                }
            }
            if (export_acmdl.Count > 0)
            {
                for (int i = 0; i < acmdl_section.Records.Count; i++)
                {
                    if (export_acmdl.Contains(acmdl_section.Records[i].ID))
                    {
                        if (gameObject.list_blendskins == null)
                        {
                            gameObject.list_blendskins = new List<TwinsItem>();
                        }
                        gameObject.list_blendskins.Add(acmdl_section.Records[i]);
                    }
                }
            }

            List<uint> export_mat = new List<uint>();
            List<uint> export_mesh = new List<uint>();
            if (gameObject.list_rigidmodels != null)
            {
                for (int mdl = 0; mdl < gameObject.list_rigidmodels.Count; mdl++)
                {
                    RigidModel This_MDL = (RigidModel)gameObject.list_rigidmodels[mdl];
                    export_mesh.Add(This_MDL.MeshID);
                    for (int i = 0; i < This_MDL.MaterialIDs.Length; i++)
                    {
                        export_mat.Add(This_MDL.MaterialIDs[i]);
                    }
                }
            }
            if (gameObject.list_skins != null)
            {
                if (RM_Archive.Type == TwinsFile.FileType.RM2)
                {
                    for (int mdl = 0; mdl < gameObject.list_skins.Count; mdl++)
                    {
                        Skin ARM_MDL = (Skin)gameObject.list_skins[mdl];
                        for (int i = 0; i < ARM_MDL.MaterialIDs.Length; i++)
                        {
                            export_mat.Add(ARM_MDL.MaterialIDs[i]);
                        }
                    }
                }
                else if (RM_Archive.Type == TwinsFile.FileType.RMX)
                {
                    for (int mdl = 0; mdl < gameObject.list_skins.Count; mdl++)
                    {
                        SkinX ARM_MDL = (SkinX)gameObject.list_skins[mdl];
                        for (int i = 0; i < ARM_MDL.MaterialIDs.Length; i++)
                        {
                            export_mat.Add(ARM_MDL.MaterialIDs[i]);
                        }
                    }
                }
            }
            if (export_mesh.Count > 0)
            {
                for (int i = 0; i < mesh_section.Records.Count; i++)
                {
                    if (export_mesh.Contains(mesh_section.Records[i].ID))
                    {
                        if (gameObject.list_models == null)
                        {
                            gameObject.list_models = new List<TwinsItem>();
                        }
                        gameObject.list_models.Add(mesh_section.Records[i]);
                    }
                }
            }
            if (export_mat.Count > 0)
            {
                for (int i = 0; i < mat_section.Records.Count; i++)
                {
                    if (export_mat.Contains(mat_section.Records[i].ID))
                    {
                        if (gameObject.list_materials == null)
                        {
                            gameObject.list_materials = new List<TwinsItem>();
                        }
                        gameObject.list_materials.Add(mat_section.Records[i]);
                    }
                }
            }

            List<uint> export_tex = new List<uint>();
            if (export_mat.Count > 0)
            {
                for (int mat = 0; mat < gameObject.list_materials.Count; mat++)
                {
                    Material This_MAT = (Material)gameObject.list_materials[mat];
                    for (int sh = 0; sh < This_MAT.Shaders.Count; sh++)
                    {
                        if (This_MAT.Shaders[sh].TextureId != 0)
                        {
                            export_tex.Add(This_MAT.Shaders[sh].TextureId);
                        }
                    }
                }
            }
            if (export_tex.Count > 0)
            {
                for (int i = 0; i < tex_section.Records.Count; i++)
                {
                    if (export_tex.Contains(tex_section.Records[i].ID))
                    {
                        if (gameObject.list_textures == null)
                        {
                            gameObject.list_textures = new List<TwinsItem>();
                        }
                        gameObject.list_textures.Add(tex_section.Records[i]);
                    }
                }
            }

            bool loadedTemplate = false;
            for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
            {
                if (!RM_Archive.ContainsItem(section_id)) continue;
                TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                if (section.Records.Count > 0)
                {
                    if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                    TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
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
                                    Properties = instance.Flags,
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
                for (uint section_id = (uint)RM_Sections.Instances1; section_id <= (uint)RM_Sections.Instances8; section_id++)
                {
                    if (!RM_Archive.ContainsItem(section_id)) continue;
                    TwinsSection section = RM_Archive.GetItem<TwinsSection>(section_id);
                    if (section.Records.Count > 0)
                    {
                        if (!section.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) continue;
                        TwinsSection instances = section.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
                        for (int i = 0; i < instances.Records.Count; ++i)
                        {
                            Instance instance = (Instance)instances.Records[i];
                            if (instance.ObjectID == (ushort)objectID)
                            {
                                gameObject.instanceTemplate = new InstanceTemplate()
                                {
                                    ObjectID = instance.ObjectID,
                                    Properties = instance.Flags,
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
        public static List<ushort> GetValidIDs(ref List<ushort> itemList)
        {
            List<ushort> validItems = new List<ushort>();

            for (int i = 0; i < itemList.Count; i++)
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

            if (cachedGameObjects[targetObject].list_subobjects != null && cachedGameObjects[targetObject].list_subobjects.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_subobjects.Count; i++)
                {
                    ImportGameObject(ref RM_Archive, cachedGameObjects[targetObject].list_subobjects[i], ref importedObjects);
                }
            }

            TwinsSection gfx_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Graphics);
            TwinsSection tex_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Textures);
            TwinsSection mat_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Materials);
            TwinsSection mesh_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Models);
            TwinsSection mdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.RigidModels);
            TwinsSection armdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.Skin);
            TwinsSection acmdl_section = gfx_section.GetItem<TwinsSection>((uint)RM_Graphics_Sections.BlendSkin);

            TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
            TwinsSection anim_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Animation);
            TwinsSection object_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Object);
            TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
            TwinsSection ogi_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.OGI);
            TwinsSection comdl_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.CodeModel);
            TwinsSection sfx_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE);
            TwinsSection sfx_eng_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Eng);
            TwinsSection sfx_fre_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Fre);
            TwinsSection sfx_ger_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Ger);
            TwinsSection sfx_ita_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Ita);
            TwinsSection sfx_spa_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Spa);
            TwinsSection sfx_unu_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.SE_Unused);

            if (SectionContainsItemID(ref object_section.Records, (uint)objectID))
            {
               return;
            }

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
            if (cachedGameObjects[targetObject].list_models != null && cachedGameObjects[targetObject].list_models.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_models.Count; i++)
                {
                    if (!SectionContainsItemID(ref mesh_section.Records, cachedGameObjects[targetObject].list_models[i].ID))
                    {
                        mesh_section.Records.Add(cachedGameObjects[targetObject].list_models[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_rigidmodels != null && cachedGameObjects[targetObject].list_rigidmodels.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_rigidmodels.Count; i++)
                {
                    if (!SectionContainsItemID(ref mdl_section.Records, cachedGameObjects[targetObject].list_rigidmodels[i].ID))
                    {
                        mdl_section.Records.Add(cachedGameObjects[targetObject].list_rigidmodels[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_skins != null && cachedGameObjects[targetObject].list_skins.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_skins.Count; i++)
                {
                    if (!SectionContainsItemID(ref armdl_section.Records, cachedGameObjects[targetObject].list_skins[i].ID))
                    {
                        armdl_section.Records.Add(cachedGameObjects[targetObject].list_skins[i]);
                    }
                }
            }
            if (cachedGameObjects[targetObject].list_blendskins != null && cachedGameObjects[targetObject].list_blendskins.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_blendskins.Count; i++)
                {
                    if (!SectionContainsItemID(ref acmdl_section.Records, cachedGameObjects[targetObject].list_blendskins[i].ID))
                    {
                        acmdl_section.Records.Add(cachedGameObjects[targetObject].list_blendskins[i]);
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
            if (cachedGameObjects[targetObject].list_scriptpacks != null && cachedGameObjects[targetObject].list_scriptpacks.Count > 0)
            {
                for (int i = 0; i < cachedGameObjects[targetObject].list_scriptpacks.Count; i++)
                {
                    if (!SectionContainsItemID(ref comdl_section.Records, cachedGameObjects[targetObject].list_scriptpacks[i].ID))
                    {
                        comdl_section.Records.Add(cachedGameObjects[targetObject].list_scriptpacks[i]);
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
            return new InstanceTemplate() { ObjectID = 0, Properties = 0 };
        }

    }

    struct TwinsLevelChunk
    {
        public ChunkType Chunk;
        /// <summary> The game level that the chunk belongs to </summary>
        public LevelType LevelArea;
        /// <summary> Lowercase, no extension </summary>
        public string Path;

        public TwinsLevelChunk(ChunkType ch, LevelType la, string p)
        {
            Chunk = ch;
            LevelArea = la;
            Path = p;
        }
    }

    struct TwinsGem
    {
        public ChunkType chunk;
        public GemType type;
        public Vector3 pos;

        public TwinsGem(ChunkType ch, GemType gem, Vector3 p)
        {
            chunk = ch;
            type = gem;
            pos = p;
        }

        public TwinsGem(ChunkType ch, Vector3 p)
        {
            chunk = ch;
            type = GemType.GEM_RED;
            pos = p;
        }
    }

    struct CachedGameObject
    {
        public GameObject mainObject;
        public List<TwinsItem> list_anims;
        public List<TwinsItem> list_rigidmodels;
        public List<TwinsItem> list_skins;
        public List<TwinsItem> list_scriptpacks;
        public List<TwinsItem> list_blendskins;
        public List<TwinsItem> list_materials;
        public List<TwinsItem> list_models;
        public List<TwinsItem> list_ogi;
        public List<TwinsItem> list_scripts;
        public List<TwinsItem> list_sounds;
        public List<TwinsItem> list_sounds_english;
        public List<TwinsItem> list_sounds_french;
        public List<TwinsItem> list_sounds_german;
        public List<TwinsItem> list_sounds_italian;
        public List<TwinsItem> list_sounds_spanish;
        public List<TwinsItem> list_sounds_unused;
        public List<TwinsItem> list_textures;
        public List<ObjectID> list_subobjects;
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
