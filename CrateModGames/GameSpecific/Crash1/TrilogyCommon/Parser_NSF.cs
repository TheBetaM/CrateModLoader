using System;
using System.Collections.Generic;
using Crash;
using System.IO;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Parser_NSF : ModParser<NSF_Pair>
    {
        public override bool DisableAsync => true; // API fails during multithreading
        private GameVersion game;
        private RegionType region;

        public Parser_NSF(Modder mod, GameVersion thisGame, RegionType r) : base(mod)
        {
            game = thisGame;
            region = r;
        }

        public override List<string> Extensions => new List<string>() { ".NSF" };

        public override NSF_Pair LoadObject(string filePath)
        {
            NSF_Pair pair;
            NSF nsf = null;
            OldNSD oldnsd = null;
            NSD nsd = null;
            NewNSD newnsd = null;
            string nsf_filename = filePath;
            string nsd_filename = Path.ChangeExtension(filePath, ".NSD");
            
            try
            {
                nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsf_filename), game);
                if (game == GameVersion.Crash2)
                {
                    nsd = NSD.Load(File.ReadAllBytes(nsd_filename));
                }
                else if (game == GameVersion.Crash3)
                {
                    newnsd = NewNSD.Load(File.ReadAllBytes(nsd_filename));
                }
                else
                {
                    oldnsd = OldNSD.Load(File.ReadAllBytes(nsd_filename));
                }
            }
            catch (Exception ex)
            {
                if (ex is LoadAbortedException)
                {
                    Console.WriteLine("Crash: LoadAbortedException: " + filePath + "\n" + ex.Message);
                }
                else if (ex is LoadSkippedException)
                {
                    Console.WriteLine("Crash: LoadSkippedException: " + filePath + "\n" + ex.Message);
                }
                else
                    throw;
            }

            if (game == GameVersion.Crash2)
            {
                Crash2_Levels NSF_Level = GetLevelFromNSF2(Path.GetFileName(filePath));
                pair = new NSF_Pair(nsf, nsd, NSF_Level, region);
            }
            else if (game == GameVersion.Crash3)
            {
                Crash3_Levels NSF_Level = GetLevelFromNSF3(Path.GetFileName(filePath));
                pair = new NSF_Pair(nsf, newnsd, NSF_Level, region);
            }
            else
            {
                Crash1_Levels NSF_Level = GetLevelFromNSF1(Path.GetFileName(filePath));
                pair = new NSF_Pair(nsf, oldnsd, NSF_Level, region);
            }

            return pair;
        }

        public override void SaveObject(NSF_Pair thing, string filePath)
        {
            if (game == GameVersion.Crash2)
            {
                PatchNSD(thing.nsf, thing.nsd);
            }
            else if (game == GameVersion.Crash3)
            {
                PatchNSD(thing.nsf, thing.newnsd);
            }
            else
            {
                PatchNSD(thing.nsf, thing.oldnsd);
            }

            File.WriteAllBytes(filePath, thing.nsf.Save());

            if (game == GameVersion.Crash2)
            {
                File.WriteAllBytes(Path.ChangeExtension(filePath, ".NSD"), thing.nsd.Save());
            }
            else if (game == GameVersion.Crash3)
            {
                File.WriteAllBytes(Path.ChangeExtension(filePath, ".NSD"), thing.newnsd.Save());
            }
            else
            {
                File.WriteAllBytes(Path.ChangeExtension(filePath, ".NSD"), thing.oldnsd.Save());
            }
        }

        internal Crash1_Levels GetLevelFromNSF1(string nsf_name)
        {
            for (int i = 0; i < Crash1_Common.Crash1_LevelFileNames.Length; i++)
            {
                if (nsf_name.Contains("S00000" + Crash1_Common.Crash1_LevelFileNames[i]))
                {
                    return (Crash1_Levels)i;
                }
            }
            return Crash1_Levels.Unknown;
        }
        internal Crash2_Levels GetLevelFromNSF2(string NSf_Name)
        {
            for (int i = 0; i < Crash2_Common.Crash2_LevelFileNames.Length; i++)
            {
                if (NSf_Name.Contains("S00000" + Crash2_Common.Crash2_LevelFileNames[i]))
                {
                    return (Crash2_Levels)i;
                }
            }
            return Crash2_Levels.Unknown;
        }
        internal Crash3_Levels GetLevelFromNSF3(string NSF_Name)
        {
            for (int i = 0; i < Crash3_Common.Crash3_LevelFileNames.Length; i++)
            {
                if (NSF_Name.Contains("S00000" + Crash3_Common.Crash3_LevelFileNames[i]))
                {
                    return (Crash3_Levels)i;
                }
            }
            return Crash3_Levels.Unknown;
        }

        internal void PatchNSD(NSF nsf, OldNSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
        }
        internal void PatchNSD(NSF nsf, NSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;

            // patch object entity count
            nsd.EntityCount = 0;
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (!(chunk is EntryChunk))
                    continue;
                foreach (Entry entry in ((EntryChunk)chunk).Entries)
                {
                    if (entry is ZoneEntry zone)
                        foreach (Entity ent in zone.Entities)
                            if (ent.ID != null)
                                ++nsd.EntityCount;
                }
            }

            // fix loadlists
            int[] eids = new int[nsd.Index.Count];
            for (int i = 0; i < eids.Length; ++i)
                eids[i] = nsd.Index[i].EntryID;
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (!(chunk is EntryChunk))
                    continue;
                foreach (Entry entry in ((EntryChunk)chunk).Entries)
                {
                    if (entry is ZoneEntry zone)
                    {
                        foreach (Entity ent in zone.Entities)
                        {
                            if (ent.LoadListA != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListA.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                                    });
                                }
                            }
                            if (ent.LoadListB != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListB.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
        internal void PatchNSD(NSF nsf, NewNSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;

            // patch object entity count
            nsd.EntityCount = 0;
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (!(chunk is EntryChunk))
                    continue;
                foreach (Entry entry in ((EntryChunk)chunk).Entries)
                {
                    if (entry is NewZoneEntry zone)
                        foreach (Entity ent in zone.Entities)
                            if (ent.ID != null)
                                ++nsd.EntityCount;
                }
            }

            // fix loadlists
            int[] eids = new int[nsd.Index.Count];
            for (int i = 0; i < eids.Length; ++i)
                eids[i] = nsd.Index[i].EntryID;
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (!(chunk is EntryChunk))
                    continue;
                foreach (Entry entry in ((EntryChunk)chunk).Entries)
                {
                    if (entry is NewZoneEntry zone)
                    {
                        foreach (Entity ent in zone.Entities)
                        {
                            if (ent.LoadListA != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListA.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                                    });
                                }
                            }
                            if (ent.LoadListB != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListB.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return Array.IndexOf(eids, a) - Array.IndexOf(eids, b);
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
