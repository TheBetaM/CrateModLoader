using Crash;
using System;
using System.Collections.Generic;
using System.IO;
//Crash 2 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)

namespace CrateModLoader
{
    public sealed class Modder_Crash2 : Modder
    {
        internal const int RandomizeADIO = 0;

        public Modder_Crash2()
        {
            Game = new Game()
            {
                Name = "Crash Bandicoot 2: Cortex Strikes Back",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by chekwob and ManDude",
                API_Link = "https://github.com/cbhacks/CrashEdit",
                Icon = null,
                ModMenuEnabled = false,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCUS_941.54;1",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_941.54",
                    CodeName = "SCUS_94154", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCES_009.67;1",
                    Region = RegionType.PAL,
                    ExecName = "SCES_009.67",
                    CodeName = "SCES_00967", },
                    new RegionCode() {
                    Name = @"BOOT = cdrom:\SCPS_100.47;1",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.47",
                    CodeName = "SCPS_10047", },
                },
            };

            Options.Add(RandomizeADIO, new ModOption("Randomize sound effects"));
        }

        public override void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();

            EndModProcess();
        }

        protected override void ModProcess()
        {
            Random rand = new Random(Program.ModProgram.randoSeed);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(Program.ModProgram.extractedPath);
            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs

            ErrorManager.EnterSkipRegion();

            for (int i = 0; i < Math.Min(nsfs.Count, nsds.Count); ++i)
            {
                FileInfo nsfFile = nsfs[i];
                FileInfo nsdFile = nsds[i];
                if (Path.GetFileNameWithoutExtension(nsfFile.Name) != Path.GetFileNameWithoutExtension(nsdFile.Name))
                {
                    //MessageBox.Show($"NSF/NSD file pair mismatch. First mismatch:\n\n{nsfFile.Name}\n{nsdFile.Name}");
                    continue;
                }
                if (Options[RandomizeADIO].Enabled) Mod_RandomizeADIO(nsfFile, nsdFile, rand);
            }

            ErrorManager.ExitSkipRegion();
        }

        private void AppendFileInfoDir(IList<FileInfo> nsfpaths, IList<FileInfo> nsdpaths, DirectoryInfo di)
        {
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                AppendFileInfoDir(nsfpaths, nsdpaths, dir);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToUpper() == ".NSF") nsfpaths.Add(file);
                else if (file.Extension.ToUpper() == ".NSD") nsdpaths.Add(file);
            }
        }

        protected override void EndModProcess()
        {
            // ...or here
        }

        internal void Mod_RandomizeADIO(FileInfo nsfFile, FileInfo nsdFile, Random rand)
        {
            NSF nsf;
            NSD nsd;
            try
            {
                nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsfFile.FullName), GameVersion.Crash2);
                nsd = NSD.Load(File.ReadAllBytes(nsdFile.FullName));
            }
            catch (LoadAbortedException)
            {
                return;
            }

            // edit NSF
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is SoundChunk soundchunk)
                {
                    List<int> oldeids = new List<int>();
                    foreach (Entry entry in soundchunk.Entries)
                    {
                        oldeids.Add(entry.EID);
                    }
                    foreach (Entry entry in soundchunk.Entries)
                    {
                        if (entry is SoundEntry soundentry)
                        {
                            int eid = oldeids[rand.Next(oldeids.Count)];
                            entry.EID = eid;
                            oldeids.Remove(eid);
                        }
                    }
                }
            }

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

            File.WriteAllBytes(nsfFile.FullName, nsf.Save());
            File.WriteAllBytes(nsdFile.FullName, nsd.Save());
        }
    }
}
