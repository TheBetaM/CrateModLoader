using Crash;
using System;
using System.Collections.Generic;
using CrateModGames.GameSpecific.Crash1.TrilogyCommon;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class CrashTri_Rand_Music : ModStruct<NSF_Pair>
    {
        public override string Name => "Randomize Music Tracks";
        public override string Description => "Music tracks are randomized, still played using the level's instruments.";
        public override bool NeedsCachePass => true;

        private List<SEP> Cache_MIDI = new List<SEP>();
        private Random rand;

        public override void BeforeCachePass()
        {
            Cache_MIDI = new List<SEP>();
        }

        public override void CachePass(NSF_Pair pair)
        {
            foreach (MusicEntry music in pair.nsf.GetEntries<MusicEntry>())
            {
                Cache_MIDI.Add(music.SEP);
            }
        }

        public override void BeforeModPass()
        {
            rand = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(NSF_Pair pair)
        {
            //List<MusicEntry> CacheEntryList = new List<MusicEntry>();

            foreach (Chunk chunk in pair.nsf.Chunks)
            {
                if (chunk is NormalChunk datachunk)
                {
                    for (int i = 0; i < datachunk.Entries.Count; i++)
                    {
                        if (datachunk.Entries[i] is MusicEntry music)
                        {
                            SEP target_track = Cache_MIDI[rand.Next(Cache_MIDI.Count)];
                            SEP this_track = music.SEP;
                            List<SEQ> trackList = new List<SEQ>();

                            for (int t = 0; t < this_track.SEQs.Count; t++)
                            {
                                int len = this_track.SEQs[t].Data.Length;
                                byte[] new_track_data = target_track.SEQs[0].Data;
                                int flen = Math.Min(len, new_track_data.Length);
                                byte[] trimmed_data = new byte[flen];
                                for (int b = 0; b < flen; b++)
                                {
                                    trimmed_data[b] = new_track_data[b];
                                }

                                SEQ track = new SEQ(target_track.SEQs[0].Resolution, target_track.SEQs[0].Tempo, target_track.SEQs[0].Rhythm, trimmed_data);
                                trackList.Add(track);
                            }

                            this_track = new SEP(trackList);
                            datachunk.Entries[i] = new MusicEntry(music.VHEID, music.VB0EID, music.VB1EID, music.VB2EID, music.VB3EID, music.VB4EID, music.VB5EID, music.VB6EID, music.VH, this_track, music.EID);

                            /*
                            MusicEntry CacheEntry = new MusicEntry(music.VHEID, music.VB0EID, music.VB1EID, music.VB2EID, music.VB3EID, music.VB4EID, music.VB5EID, music.VB6EID, music.VH, target_track, music.EID);
                            CacheEntryList.Add(CacheEntry);
                            datachunk.Entries.RemoveAt(i);
                            i--;
                            */
                        }
                    }
                }
            }

            /*
            if (CacheEntryList.Count > 0)
            {
                for (int i = 0; i < CacheEntryList.Count; i++)
                {
                    NormalChunk addchunk = new NormalChunk();
                    addchunk.Entries.Add(CacheEntryList[i]);
                    nsf.Chunks.Add(addchunk);
                }
            }
            */
        }
    }
}
