using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    // todo: test, could be better?
    public class TS_Rand_StartingChunk : ModStruct<ExecutableInfo>
    {
        public override string Name => Twins_Text.Rand_StartingLevel;
        public override string Description => Twins_Text.Rand_StartingLevelDesc;
        public override CreditContributors Contributors => new CreditContributors(ModLoaderGlobals.Contributor_BetaM);

        internal List<uint> musicTypes = new List<uint>();
        internal List<uint> randMusicList = new List<uint>();

        public static List<ChunkType> possibleStartingChunks = new List<ChunkType>()
        {
            ChunkType.Earth_Hub_Beach,
            ChunkType.Earth_Cavern_CavEnt,
            ChunkType.Earth_Hub_HubD,
            ChunkType.Earth_DocAmok_DocAmok1,
            ChunkType.Earth_Totem_L03Stock,
            ChunkType.Ice_Hub_LabExt,
            ChunkType.Ice_IceClimb_CaveEnt,
            ChunkType.Ice_SlipSlide_L05Start,
            ChunkType.Ice_HighSeas_GPA01,
            ChunkType.School_Sch_Hub_Sch_Hub,
            ChunkType.School_Boiler_Boiler_1,
            ChunkType.School_Crash_CrashEnt,
            ChunkType.School_Cortex_CoGPA01,
            ChunkType.School_Rooftop_Roof01,
            //ChunkType.AltEarth_Lab_LabExt, // you spawn at the bottom LOL
            ChunkType.AltEarth_RockSlid_L10Start,
            ChunkType.AltEarth_Hub_AltA,
            ChunkType.AltEarth_Hub_AltDoc,
            ChunkType.AltEarth_Core_CoreA,

            //Could do more, but would rather keep it balanced between the hubs
            //ChunkType.Earth_Totem_L03River,
            //ChunkType.Earth_Hub_HubA,
            //ChunkType.Earth_Hub_BossArea,
        };

        public override void ModPass(ExecutableInfo executable)
        {
            Random randState = new Random();
            ChunkType randChunk = possibleStartingChunks[randState.Next(possibleStartingChunks.Count)];
            for (int i = 0; i < Twins_Data.All_Chunks.Count; i++)
            {
                if (Twins_Data.All_Chunks[i].Chunk == randChunk)
                {
                    TS_Props_Misc.CreditsChunk.Value = @"Levels\" + Twins_Data.All_Chunks[i].Path;
                    TS_Props_Misc.CreditsChunk.HasChanged = true;
                    TS_Props_Misc.Option_SwapStartAndCreditsChunk.Value = 1;
                    TS_Props_Misc.Option_SwapStartAndCreditsChunk.HasChanged = true;
                    break;
                }
            }
        }
    }
}
