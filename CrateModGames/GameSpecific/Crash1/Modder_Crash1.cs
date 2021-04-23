using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
/*
 * Mod Passes:
 * NSF_Pair -> NSF and NSD pair
 */

namespace CrateModLoader.GameSpecific.Crash1
{
    public sealed class Modder_Crash1 : Modder
    {
        public override bool ModCrateRegionCheck => true;
        public override bool AsyncProcess => true;
        private bool MainBusy = false;
        private int CurrentPass = 0;
        private float PassPercentMod = 49f;
        private int PassPercentAdd = 1;

        public Modder_Crash1() { }

        public override void StartModProcess()
        {
            ProcessBusy = true;

            AsyncStart();
        }

        public async void AsyncStart()
        {
            UpdateProcessMessage("Starting...", 0);

            // Mod files
            ModProcess();

            while (MainBusy || PassBusy)
            {
                await Task.Delay(100);
            }

            ProcessBusy = false;
        }

        public async void ModProcess()
        {
            MainBusy = true;

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);

            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs
            PassCount = nsfs.Count;

            bool NeedsCache = NeedsCachePass();
            CurrentPass = 0;
            if (!NeedsCache)
            {
                PassPercentMod = 99f;
                CurrentPass++;
            }

            while (CurrentPass < 2)
            {
                PassIterator = 0;
                PassBusy = true;
                if (CurrentPass == 0)
                {
                    PassPercentMod = 49f;
                    PassPercentAdd = 1;
                    UpdateProcessMessage("Cache Pass", 1);
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    if (NeedsCache)
                    {
                        PassPercentMod = 50f;
                        PassPercentAdd = 50;
                        UpdateProcessMessage("Mod Pass", 50);
                    }
                    else
                    {
                        PassPercentMod = 99f;
                        UpdateProcessMessage("Mod Pass", 1);
                    }

                    BeforeModPass();
                }

                IList<Task> editTaskList = new List<Task>();

                for (int i = 0; i < nsfs.Count; i++)
                {
                    editTaskList.Add(EditLevel(nsfs, nsds, i));
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            MainBusy = false;

        }

        private async Task EditLevel(List<FileInfo> nsfs, List<FileInfo> nsds, int iter)
        {
            //Console.WriteLine("Editing: " + path);
            NSF_Pair pair;
            NSF nsf = null;
            OldNSD nsd = null;
            bool skip = false;
            string nsf_filename = nsfs[iter].FullName;
            string nsd_filename = nsds[iter].FullName;

            //await Task.Run(
            //() =>
            //{


                try
                {
                    nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsf_filename), GameVersion.Crash1);
                    nsd = OldNSD.Load(File.ReadAllBytes(nsd_filename));
                }
                catch (Exception ex)
                {
                    if (ex is LoadAbortedException)
                    {
                        Console.WriteLine("Crash: LoadAbortedException: " + nsfs[iter].Name + "\n" + ex.Message);
                        skip = true;
                    }
                    else if (ex is LoadSkippedException)
                    {
                        Console.WriteLine("Crash: LoadSkippedException: " + nsfs[iter].Name + "\n" + ex.Message);
                        skip = true;
                    }
                    else
                        throw;
                }

                if (!skip)
                {
                    Crash1_Levels NSF_Level = GetLevelFromNSF(nsfs[iter].Name);
                    pair = new NSF_Pair(nsf, nsd, NSF_Level, GameRegion.Region);

                    switch (CurrentPass)
                    {
                        case 0:
                            StartCachePass(pair);
                            break;
                        default:
                        case 1:
                            StartModPass(pair);
                            break;
                    }

                    PatchNSD(nsf, nsd);

                    File.WriteAllBytes(nsf_filename, nsf.Save());
                    File.WriteAllBytes(nsd_filename, nsd.Save());
                }

            //}
            //);

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
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

        internal void PatchNSD(NSF nsf, OldNSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
        }

        internal Crash1_Levels GetLevelFromNSF(string nsf_name)
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
    }
}
