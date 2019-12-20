using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Crash;
//Crash 2 API by chekwob and ManDude

namespace CrateModLoader
{
    public sealed class Modder_Crash2
    {
        public string gameName = "Crash 2";
        public string apiCredit = "API by chekwob and ManDude";
        public System.Drawing.Image gameIcon = null;
        public bool ModMenuEnabled = false;
        public bool ModCratesSupported = true;
        public string[] modOptions = {
            "Randomize sound effects"
        };

        public bool RandomizeADIO = false;

        public enum ModOptions
        {
            RandomizeADIO = 0
        }

        public void OptionChanged(int option, bool value)
        {
            switch ((ModOptions)option)
            {
                case ModOptions.RandomizeADIO:
                    RandomizeADIO = value;
                    break;
            }
        }

        public void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();

            EndModProcess();
        }

        private void ModProcess()
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
                if (RandomizeADIO) Mod_RandomizeADIO(nsfFile, nsdFile, rand);
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

        public void EndModProcess()
        {
            // ...or here
        }

        private void Mod_RandomizeADIO(FileInfo nsfFile, FileInfo nsdFile, Random rand)
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
            for (int i = 0; i < nsf.Chunks.Count; ++i)
            {
                if (nsf.Chunks[i] is SoundChunk soundchunk)
                {
                    List<int> oldeids = new List<int>();
                    foreach (Entry entry in soundchunk.Entries)
                    {
                        oldeids.Add(entry.EID);
                    }
                    for (int j = 0; j < soundchunk.Entries.Count; ++j)
                    {
                        int eid = oldeids[rand.Next(oldeids.Count)];
                        soundchunk.Entries[j].EID = eid;
                        oldeids.Remove(eid);
                    }
                }
            }

            // edit NSD
            for (int i = 0; i < nsf.Chunks.Count; i++)
            {
                if (nsf.Chunks[i] is EntryChunk chunk)
                {
                    List<int> nsdchunkentries = new List<int>();
                    for (int j = 0; j < nsd.Index.Count; ++j)
                    {
                        NSDLink link = nsd.Index[j];
                        if (i * 2 + 1 == link.ChunkID)
                        {
                            nsdchunkentries.Add(j);
                        }
                    }
                    for (int j = 0; j < chunk.Entries.Count; ++j)
                    {
                        Entry entry = chunk.Entries[j];
                        if (entry.EID != nsd.Index[nsdchunkentries[j]].EntryID)
                        {
                            //MessageBox.Show($"NSD hash map is not in correct order. Entry {entry.EName} in chunk {i*2+1} will be swapped.", "NSD hash map mismatch");
                            int k = j;
                            for (; k < nsdchunkentries.Count; ++k)
                                if (entry.EID == nsd.Index[nsdchunkentries[k]].EntryID) break;
                            var temp = nsd.Index[nsdchunkentries[j]];
                            nsd.Index[nsdchunkentries[j]] = nsd.Index[nsdchunkentries[k]];
                            nsd.Index[nsdchunkentries[k]] = temp;
                        }
                    }
                }
            }
            List<int> eids = new List<int>();
            foreach (NSDLink link in nsd.Index)
            {
                eids.Add(link.EntryID);
            }
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (!(chunk is EntryChunk))
                    continue;
                foreach (Entry entry in ((EntryChunk)chunk).Entries)
                {
                    if (entry is ZoneEntry)
                    {
                        foreach (Entity ent in ((ZoneEntry)entry).Entities)
                        {
                            if (ent.LoadListA != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListA.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return eids.IndexOf(a) - eids.IndexOf(b);
                                    });
                                }
                            }
                            if (ent.LoadListB != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListB.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return eids.IndexOf(a) - eids.IndexOf(b);
                                    });
                                }
                            }
                        }
                    }
                    else if (entry is NewZoneEntry)
                    {
                        foreach (Entity ent in ((NewZoneEntry)entry).Entities)
                        {
                            if (ent.LoadListA != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListA.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return eids.IndexOf(a) - eids.IndexOf(b);
                                    });
                                }
                            }
                            if (ent.LoadListB != null)
                            {
                                foreach (EntityPropertyRow<int> row in ent.LoadListB.Rows)
                                {
                                    List<int> values = (List<int>)row.Values;
                                    values.Sort(delegate (int a, int b) {
                                        return eids.IndexOf(a) - eids.IndexOf(b);
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
