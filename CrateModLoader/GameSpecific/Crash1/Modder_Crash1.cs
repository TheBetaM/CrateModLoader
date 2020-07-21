using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)

namespace CrateModLoader
{
    public sealed class Modder_Crash1 : Modder
    {
        internal const int RandomizeADIO = 0;

        public Modder_Crash1()
        {
            Game = new Game()
            {
                Name = "Crash Bandicoot",
                ShortName = "Crash1",
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
                    Name = @"SCUS_949.00",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_949.00",
                    CodeName = "SCUS_94900", },
                    new RegionCode() {
                    Name = @"SCES_003.44",
                    Region = RegionType.PAL,
                    ExecName = "SCES_003.44",
                    CodeName = "SCES_00344", },
                    new RegionCode() {
                    Name = @"SCPS_100.31",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.31",
                    CodeName = "SCPS_10031", },
                },
            };
            ModCratesManualInstall = true;

            AddOption(RandomizeADIO, new ModOption("Randomize sound effects"));
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

            CrateSettings_CrashTri.VerifyModCrates();
            ModCrates.InstallLayerMods(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"temp\"), 0);

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

                NSF nsf;
                OldNSD nsd;
                try
                {
                    nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsfFile.FullName), GameVersion.Crash1);
                    nsd = OldNSD.Load(File.ReadAllBytes(nsdFile.FullName));
                }
                catch (LoadAbortedException)
                {
                    return;
                }

                if (GetOption(RandomizeADIO)) Mod_RandomizeADIO(nsf, nsd, rand);

                PatchNSD(nsf, nsd);

                File.WriteAllBytes(nsfFile.FullName, nsf.Save());
                File.WriteAllBytes(nsdFile.FullName, nsd.Save());
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

        internal void PatchNSD(NSF nsf, OldNSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
        }

        internal void Mod_RandomizeADIO(NSF nsf, OldNSD nsd, Random rand)
        {
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
        }
    }
}
