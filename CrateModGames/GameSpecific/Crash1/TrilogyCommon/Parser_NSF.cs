using System;
using System.Collections.Generic;
using Crash;
using System.IO;
using CrateModLoader.GameSpecific.Crash2;
using CrateModLoader.GameSpecific.Crash3;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.Crash1.TrilogyCommon
{
    public class Parser_NSF : ModParser<NSF_Pair>
    {
        public override bool DisableAsync => true; // API failed during multithreading
        private GameVersion game;
        private RegionType region;
        private Dictionary<int, NSF_Pair> LevelPairs;

        public Parser_NSF(Modder mod, GameVersion thisGame, RegionType r) : base(mod)
        {
            game = thisGame;
            region = r;

            LevelPairs = new Dictionary<int, NSF_Pair>();
            if (game == GameVersion.Crash2)
            {
                for (int i = 0; i <= (int)Crash2_Levels.WarpRoom5; i++)
                {
                    if (Enum.IsDefined(typeof(Crash2_Levels), i))
                    {
                        LevelPairs.Add(i, new NSF_Pair(region));
                    }
                }
            }
            else if (game == GameVersion.Crash3)
            {
                for (int i = 0; i <= (int)Crash3_Levels.WarpRoom; i++)
                {
                    if (Enum.IsDefined(typeof(Crash3_Levels), i))
                    {
                        LevelPairs.Add(i, new NSF_Pair(region));
                    }
                }
            }
            else
            {
                for (int i = 0; i <= (int)Crash1_Levels.Bonus_Cortex; i++)
                {
                    if (Enum.IsDefined(typeof(Crash1_Levels), i))
                    {
                        LevelPairs.Add(i, new NSF_Pair(region));
                    }
                }
            }
        }

        public override List<string> Extensions => new List<string>() { ".NSF",".NSF;1", };

        public override async Task<NSF_Pair> LoadObject(MemoryFile file, Dictionary<string, MemoryFile> AllFiles)
        {
            NSF_Pair pair;
            int NSF_Level = -1;

            if (game == GameVersion.Crash2)
            {
                NSF_Level = (int)GetLevelFromNSF2(file.FullName);
            }
            else if (game == GameVersion.Crash3)
            {
                NSF_Level = (int)GetLevelFromNSF3(file.FullName);
            }
            else
            {
                NSF_Level = (int)GetLevelFromNSF1(file.FullName);
            }

            if (!LevelPairs.ContainsKey(NSF_Level))
            {
                NSF_Level = LevelPairs.Count + 1;
                LevelPairs.Add(NSF_Level, new NSF_Pair(region));
                LevelPairs[NSF_Level].LevelC1 = Crash1_Levels.Unknown;
                LevelPairs[NSF_Level].LevelC2 = Crash2_Levels.Unknown;
                LevelPairs[NSF_Level].LevelC3 = Crash3_Levels.Unknown;
            }
            pair = LevelPairs[NSF_Level];
            MemoryFile nsdfile = AllFiles[file.FullName.Replace(".NSF", ".NSD")];

            NSF nsf = null;
            OldNSD oldnsd = null;
            NSD nsd = null;
            NewNSD newnsd = null;
            try
            {
                byte[] bytes = new byte[file.Stream.Length];
                file.Stream.Position = 0;
                await file.Stream.ReadAsync(bytes, 0, (int)file.Stream.Length);
                nsf = NSF.LoadAndProcess(bytes, game);
                pair.nsf = nsf;

                bytes = new byte[nsdfile.Stream.Length];
                nsdfile.Stream.Position = 0;
                await nsdfile.Stream.ReadAsync(bytes, 0, (int)nsdfile.Stream.Length);

                if (game == GameVersion.Crash2)
                {
                    nsd = NSD.Load(bytes);
                    pair.nsd = nsd;
                    pair.LevelC2 = (Crash2_Levels)NSF_Level;
                    pair.isCrash2 = true;
                }
                else if (game == GameVersion.Crash3)
                {
                    newnsd = NewNSD.Load(bytes);
                    pair.newnsd = newnsd;
                    pair.LevelC3 = (Crash3_Levels)NSF_Level;
                    pair.isCrash3 = true;
                }
                else
                {
                    oldnsd = OldNSD.Load(bytes);
                    pair.oldnsd = oldnsd;
                    pair.LevelC1 = (Crash1_Levels)NSF_Level;
                    pair.isCrash1 = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is LoadAbortedException)
                {
                    Console.WriteLine("Crash: LoadAbortedException: " + file.FullName + "\n" + ex.Message);
                    pair = null; 
                }
                else if (ex is LoadSkippedException)
                {
                    Console.WriteLine("Crash: LoadSkippedException: " + file.FullName + "\n" + ex.Message);
                    pair = null;
                }
                else
                    throw;
            }

            return pair;
        }
        public override async void SaveObject(NSF_Pair thing, MemoryFile file, Dictionary<string, MemoryFile> AllFiles)
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

            byte[] bytes;

            bytes = thing.nsf.Save();
            file.Stream.Position = 0;
            file.Stream.SetLength(bytes.Length);
            await file.Stream.WriteAsync(bytes, 0, bytes.Length);

            if (game == GameVersion.Crash2)
            {
                bytes = thing.nsd.Save();
            }
            else if (game == GameVersion.Crash3)
            {
                bytes = thing.newnsd.Save();
            }
            else
            {
                bytes = thing.oldnsd.Save();
            }

            MemoryFile nsdfile = AllFiles[file.FullName.Replace(".NSF", ".NSD")];

            nsdfile.Stream.Position = 0;
            nsdfile.Stream.SetLength(bytes.Length);
            await nsdfile.Stream.WriteAsync(bytes, 0, bytes.Length);

            thing = null;
        }

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
