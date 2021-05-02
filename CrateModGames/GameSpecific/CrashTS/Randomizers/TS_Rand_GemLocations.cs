using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test, randomize per file
    public class TS_Rand_GemLocations : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Rand_GemLocations;
        public override string Description => Twins_Text.Rand_GemLocationsDesc;

        internal List<uint> gemObjectList = new List<uint>();

        private List<TwinsGem> GemSpawnPoints = new List<TwinsGem>()
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
            //new TwinsGem(ChunkType.Earth_DocAmok_DocAmok2,new Vector3(46.554f,-27.209f,17.700f)),
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
            /* Disabled for now because of the chance to have to replay the level to get everything
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
            */
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
            /* Disabled for now because of the chance to have to replay the level to get everything
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
            */
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

        private List<TwinsGem> GemList;

        public override void BeforeModPass()
        {
            GemList = new List<TwinsGem>();

            Random randState = new Random(ModLoaderGlobals.RandomizerSeed);

            List<GemType> GemsToGo = new List<GemType>();
            List<TwinsGem> PossibleLocations = new List<TwinsGem>();
            int targetLocation = 0;
            int targetGem = 0;

            for (int level = 0; level < 16; level++)
            {
                GemsToGo.Add(GemType.GEM_BLUE);
                GemsToGo.Add(GemType.GEM_CLEAR);
                GemsToGo.Add(GemType.GEM_GREEN);
                GemsToGo.Add(GemType.GEM_PURPLE);
                GemsToGo.Add(GemType.GEM_RED);
                GemsToGo.Add(GemType.GEM_YELLOW);

                for (int spawn = 0; spawn < GemSpawnPoints.Count; spawn++)
                {
                    for (int ch = 0; ch < Twins_Data.All_Chunks.Count; ch++)
                    {
                        if (GemSpawnPoints[spawn].chunk == Twins_Data.All_Chunks[ch].Chunk && Twins_Data.All_Chunks[ch].LevelArea == (LevelType)level)
                        {
                            PossibleLocations.Add(GemSpawnPoints[spawn]);
                        }
                    }
                }

                while (GemsToGo.Count > 0)
                {
                    targetLocation = randState.Next(0, PossibleLocations.Count);
                    targetGem = randState.Next(0, GemsToGo.Count);
                    GemList.Add(new TwinsGem(PossibleLocations[targetLocation].chunk, GemsToGo[targetGem], PossibleLocations[targetLocation].pos));
                    PossibleLocations.RemoveAt(targetLocation);
                    GemsToGo.RemoveAt(targetGem);
                }

                GemsToGo.Clear();
                PossibleLocations.Clear();
            }


            gemObjectList = new List<uint>();
            gemObjectList.Add((uint)ObjectID.GEM_BLUE);
            gemObjectList.Add((uint)ObjectID.GEM_CLEAR);
            gemObjectList.Add((uint)ObjectID.GEM_GREEN);
            gemObjectList.Add((uint)ObjectID.GEM_PURPLE);
            gemObjectList.Add((uint)ObjectID.GEM_RED);
            gemObjectList.Add((uint)ObjectID.GEM_YELLOW);

        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;
            ChunkType chunkType = info.Type;

            if (chunkType == ChunkType.Invalid)
            {
                Console.WriteLine("Gem randomizer: Unknown chunk file: " + RM_Archive.FileName);
                return;
            }

            // Part 1: Remove existing gems

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
                        for (int d = 0; d < gemObjectList.Count; d++)
                        {
                            if (instance.ObjectID == gemObjectList[d])
                            {
                                instance.Pos.Y = instance.Pos.Y - 1000f; //todo: figure out how to get rid of them gracefully

                                /* Used this to generate vanilla gem locations instead of checking one-by-one
                                if (instance.ObjectID == (ushort)GemID.GEM_BLUE)
                                {
                                    Console.WriteLine("new TwinsGem(ChunkType." + chunkType + ",GemType.GEM_BLUE,new Vector3(" + instance.Pos.X + "f," + instance.Pos.Y + "f," + instance.Pos.Z + "f)),");
                                }
                                */

                                break;
                            }
                        }
                        instances.Records[i] = instance;
                    }
                }
            }

            // Part 2: Add new gems
            uint gem_section_id = (uint)RM_Sections.Instances1;
            if (!RM_Archive.ContainsItem(gem_section_id)) return;
            TwinsSection instances_group = RM_Archive.GetItem<TwinsSection>(gem_section_id);
            TwinsSection instances_section;
            if (instances_group.Records.Count > 0)
            {
                if (!instances_group.ContainsItem((uint)RM_Instance_Sections.ObjectInstance)) return;
                instances_section = instances_group.GetItem<TwinsSection>((uint)RM_Instance_Sections.ObjectInstance);
            }
            else
            {
                return;
            }


            for (int i = 0; i < GemList.Count; i++)
            {
                if (GemList[i].chunk == chunkType)
                {
                    Instance NewGem = new Instance();
                    NewGem.Pos = new Pos(GemList[i].pos.X, GemList[i].pos.Y, GemList[i].pos.Z, 1f);
                    if (GemList[i].type == GemType.GEM_BLUE)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_BLUE;
                    }
                    else if (GemList[i].type == GemType.GEM_CLEAR)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_CLEAR;
                    }
                    else if (GemList[i].type == GemType.GEM_GREEN)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_GREEN;
                    }
                    else if (GemList[i].type == GemType.GEM_PURPLE)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_PURPLE;
                    }
                    else if (GemList[i].type == GemType.GEM_RED)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_RED;
                    }
                    else if (GemList[i].type == GemType.GEM_YELLOW)
                    {
                        NewGem.ObjectID = (ushort)GemID.GEM_YELLOW;
                    }
                    NewGem.ID = (uint)instances_section.Records.Count;
                    NewGem.SomeNum1 = 10;
                    NewGem.SomeNum2 = 10;
                    NewGem.SomeNum3 = 10;
                    NewGem.RefList = -1;
                    NewGem.ScriptID = -1;
                    NewGem.Flags = 0x1CE;
                    NewGem.UnkI322 = new List<float>() { 1 };
                    NewGem.UnkI323 = new List<uint>() { 0, 255, (uint)GemList[i].type };

                    instances_section.Records.Add(NewGem);
                }
            }
        }
    }
}
