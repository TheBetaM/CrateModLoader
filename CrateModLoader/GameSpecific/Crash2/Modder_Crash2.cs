using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;
using CrateModLoader.GameSpecific.Crash2;
//Crash 2 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash2
{
    public sealed class Modder_Crash2 : Modder
    {
        internal const int RandomizeADIO = 0;
        internal const int RandomizeCratesIntoWood = 1;
        internal const int TurnCratesIntoWumpa = 2;
        internal const int RandomizeLevelOrder = 3;
        internal const int SceneryGreyscale = 4;
        internal const int SceneryRainbow = 5;
        internal const int SceneryColorSwizzle = 6;
        internal const int SceneryUntextured = 7;
        internal const int ZoneCloseCamera = 8;
        internal const int RandomizeMusic = 9;
        internal const int RandomizeMusicTracks = 10;
        internal const int RandomzieMusicInstruments = 11;
        internal const int BackwardsLevels = 12;
        internal const int RandomBackwardsLevels = 13;
        internal const int CameraBiggerFOV = 14;
        internal const int RandomizeCameraFOV = 15;
        internal const int VehicleLevelsOnFoot = 16;
        internal const int MirroredWorld = 17;
        internal const int RandomLevelsMirrored = 18;
        internal const int RandomizeWarpRoom = 19;
        internal const int RandomizeCrateContents = 20;
        internal const int RandomizeBosses = 21;
        internal const int RandomizeBoxCount = 24;

        public Modder_Crash2()
        {
            Game = new Game()
            {
                Name = Crash2_Text.GameTitle,
                ShortName = "Crash2",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = Crash2_Text.API_Credit,
                API_Link = "https://github.com/cbhacks/CrashEdit",
                Icon = Properties.Resources.icon_crash2,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
                    new RegionCode() {
                    Name = @"SCUS_941.54",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_941.54",
                    CodeName = "SCUS_94154", },
                    new RegionCode() {
                    Name = @"SCES_009.67",
                    Region = RegionType.PAL,
                    ExecName = "SCES_009.67",
                    CodeName = "SCES_00967", },
                    new RegionCode() {
                    Name = @"SCPS_100.47",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.47",
                    CodeName = "SCPS_10047", },
                },
            };
            ModCratesManualInstall = true;

            AddOption(RandomizeCratesIntoWood, new ModOption(Crash2_Text.Mod_AllCratesBlank, Crash2_Text.Mod_AllCratesBlankDesc));
            AddOption(TurnCratesIntoWumpa, new ModOption(Crash2_Text.Mod_AllCratesWumpa, Crash2_Text.Mod_AllCratesWumpaDesc));
            AddOption(RandomizeWarpRoom, new ModOption(Crash2_Text.Rand_WarpRoom, Crash2_Text.Rand_WarpRoomDesc));
            AddOption(BackwardsLevels, new ModOption(Crash2_Text.Mod_BackwardsLevels, Crash2_Text.Mod_BackwardsLevelsDesc));
            AddOption(RandomBackwardsLevels, new ModOption(Crash2_Text.Rand_BackwardsLevels, Crash2_Text.Rand_BackwardsLevelsDesc));
            AddOption(RandomizeCrateContents, new ModOption(Crash2_Text.Rand_CrateContents, Crash2_Text.Rand_CrateContentsDesc));
            AddOption(RandomizeBoxCount, new ModOption(Crash2_Text.Rand_CrateCounter, Crash2_Text.Rand_CrateCounterDesc));
            AddOption(RandomizeBosses, new ModOption(Crash2_Text.Rand_BossLevels, Crash2_Text.Rand_BossLevelsDesc));
            //AddOption(VehicleLevelsOnFoot, new ModOption("Vehicle Levels On Foot"));
            //AddOption(MirroredWorld, new ModOption("Mirrored World"));
            //AddOption(RandomLevelsMirrored, new ModOption("Random Levels Are Mirrored"));
            AddOption(CameraBiggerFOV, new ModOption(Crash2_Text.Mod_CameraWideFOV, Crash2_Text.Mod_CameraWideFOVDesc));
            AddOption(RandomizeCameraFOV, new ModOption(Crash2_Text.Rand_CameraFOV, Crash2_Text.Rand_CameraFOVDesc));
            AddOption(RandomizeADIO, new ModOption(Crash2_Text.Rand_SFX, Crash2_Text.Rand_SFXDesc));
            //AddOption(RandomizeMusic, new ModOption("Randomize Music")); //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
            //AddOption(RandomizeMusicTracks, new ModOption("Randomize Music Tracks")); //only swap midis
            //AddOption(RandomzieMusicInstruments, new ModOption("Randomize Music Instruments")); //only swap wavebanks
            AddOption(SceneryRainbow, new ModOption(Crash2_Text.Rand_WorldColors, Crash2_Text.Rand_WorldColorsDesc));
            AddOption(SceneryColorSwizzle, new ModOption(Crash2_Text.Rand_WorldPalette, Crash2_Text.Rand_WorldPaletteDesc));
            AddOption(SceneryGreyscale, new ModOption(Crash2_Text.Mod_GreyscaleWorld, Crash2_Text.Mod_GreyscaleWorldDesc));
            AddOption(SceneryUntextured, new ModOption(Crash2_Text.Mod_UntexturedWorld, Crash2_Text.Mod_UntexturedWorldDesc));

        }

        public override void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();

            EndModProcess();
        }

        protected override void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            CrateSettings_CrashTri.VerifyModCrates();
            ModCrates.InstallLayerMods(ModLoaderGlobals.ExtractedPath, 0);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.ExtractedPath);

            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs

            ErrorManager.EnterSkipRegion();

            bool CachingPass = false;
            if (GetOption(RandomizeMusic) || GetOption(RandomizeMusicTracks) || GetOption(RandomzieMusicInstruments))
            {
                CachingPass = true;
            }

            List<List<WavebankChunk>> wavebankChunks = new List<List<WavebankChunk>>();
            List<List<MusicEntry>> musicEntries = new List<List<MusicEntry>>();

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
                NSD nsd;
                try
                {
                    nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsfFile.FullName), GameVersion.Crash2);
                    nsd = NSD.Load(File.ReadAllBytes(nsdFile.FullName));
                }
                catch (LoadAbortedException)
                {
                    Console.WriteLine("Crash: LoadAbortedException: " + nsfFile.Name);
                    continue;
                    //return;
                }

                Crash2_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);

                if (!CachingPass)
                {
                    if (GetOption(RandomizeWarpRoom)) Crash2_Mods.Mod_RandomizeWarpRoom(nsf, nsd, NSF_Level, rand);
                    if (GetOption(BackwardsLevels) || GetOption(RandomBackwardsLevels)) Crash2_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, GetOption(RandomBackwardsLevels), rand);
                    if (GetOption(VehicleLevelsOnFoot) && !GetOption(BackwardsLevels)) Crash2_Mods.Mod_VehicleLevelsOnFoot(nsf, nsd, NSF_Level);
                    if (GetOption(CameraBiggerFOV) || GetOption(RandomizeCameraFOV)) Crash2_Mods.Mod_CameraFOV(nsf, rand, GetOption(RandomizeCameraFOV));
                    if (GetOption(RandomizeCratesIntoWood)) Crash2_Mods.Mod_RandomWoodCrates(nsf, rand);
                    if (GetOption(RandomizeCrateContents)) Crash2_Mods.Mod_RandomCrateContents(nsf, rand);
                    if (GetOption(RandomizeBosses)) Crash2_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    if (GetOption(TurnCratesIntoWumpa)) Crash2_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                    if (GetOption(RandomizeBoxCount)) CrashTri_Common.Rand_BoxCount(nsf, rand);
                    if (GetOption(MirroredWorld) || GetOption(RandomLevelsMirrored)) Mod_MirrorLevel(nsf, nsd, rand, GetOption(RandomLevelsMirrored));
                    if (GetOption(SceneryColorSwizzle)) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    if (GetOption(SceneryGreyscale)) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    if (GetOption(SceneryRainbow)) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    if (GetOption(SceneryUntextured)) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    if (GetOption(ZoneCloseCamera)) CrashTri_Common.Mod_Camera_Closeup(nsf);
                    if (GetOption(RandomizeMusic) || GetOption(RandomizeMusicTracks) || GetOption(RandomzieMusicInstruments))
                        Randomize_Music(nsf, rand, ref wavebankChunks, ref musicEntries, GetOption(RandomizeMusic), GetOption(RandomizeMusicTracks), GetOption(RandomzieMusicInstruments));
                    if (GetOption(RandomizeADIO)) Mod_RandomizeADIO(nsf, nsd, rand);

                    Crash2_Mods.Mod_Metadata(nsf, nsd, NSF_Level);
                }
                else
                {
                    if (GetOption(RandomizeMusic) || GetOption(RandomizeMusicTracks) || GetOption(RandomzieMusicInstruments)) CacheMusic(nsf, ref wavebankChunks, ref musicEntries);
                }

                PatchNSD(nsf, nsd);

                File.WriteAllBytes(nsfFile.FullName, nsf.Save());
                File.WriteAllBytes(nsdFile.FullName, nsd.Save());

                if (CachingPass && i == Math.Min(nsfs.Count, nsds.Count) - 1)
                {
                    CachingPass = false;
                    i = -1;
                }
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

        internal void Mod_RandomizeADIO(NSF nsf, NSD nsd, Random rand)
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
                        if (entry is SoundEntry)
                        {
                            int eid = oldeids[rand.Next(oldeids.Count)];
                            entry.EID = eid;
                            oldeids.Remove(eid);
                        }
                    }
                }
            }
        }

        internal string[] Crash2_LevelFileNames = new string[]
        {
            "0A",
            "0C",
            "0D",
            "0E",
            "0F",
            "10",
            "11",
            "12",
            "13",
            "15",
            "16",
            "17",
            "18",
            "19",
            "1A",
            "1B",
            "1D",
            "1E",
            "1F",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            //Bosses
            "06",
            "08",
            "03",
            "09",
            "07",
            //Other
            "02",
            "2D",
            "2E",
            "2F",
            "30",
        };

        internal Crash2_Levels GetLevelFromNSF(string NSf_Name)
        {
            for (int i = 0; i < Crash2_LevelFileNames.Length; i++)
            {
                if (NSf_Name.Contains("S00000" + Crash2_LevelFileNames[i]))
                {
                    return (Crash2_Levels)i;
                }
            }
            return Crash2_Levels.Unknown;
        }

        internal void CacheMusic(NSF nsf, ref List<List<WavebankChunk>> wavebankChunks, ref List<List<MusicEntry>> musicEntries)
        {
            List<WavebankChunk> waveBanks = new List<WavebankChunk>();
            List<MusicEntry> musicFiles = new List<MusicEntry>();
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk normchunk)
                {
                    foreach (Entry entry in normchunk.Entries)
                    {
                        if (entry is MusicEntry music)
                        {
                            musicFiles.Add(music);
                        }
                    }
                }
                else if (chunk is WavebankChunk wavechunk)
                {
                    waveBanks.Add(wavechunk);
                }
            }
            wavebankChunks.Add(waveBanks);
            musicEntries.Add(musicFiles);
        }

        internal void Randomize_Music(NSF nsf, Random rand, ref List<List<WavebankChunk>> wavebankChunks, ref List<List<MusicEntry>> musicEntries, bool RandomMusic, bool RandomTracks, bool RandomInstruments)
        {
            //unfinished
            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk normchunk)
                {
                    foreach (Entry entry in normchunk.Entries)
                    {
                        if (entry is MusicEntry music)
                        {
                            
                        }
                    }
                }
                else if (chunk is WavebankChunk wavechunk)
                {
                    
                }
            }
        }

        internal void Mod_MirrorLevel(NSF nsf, NSD nsd, Random rand, bool isRandom)
        {
            //unfinished

            foreach (Chunk chunk in nsf.Chunks)
            {
                if (chunk is NormalChunk zonechunk)
                {
                    foreach (Entry entry in zonechunk.Entries)
                    {
                        if (entry is ZoneEntry zone)
                        {
                            
                        }
                        else if (entry is SceneryEntry scen)
                        {
                            for (int i = 0; i < scen.Vertices.Count; i++)
                            {
                                SceneryVertex vertex = scen.Vertices[i];
                                int x = -vertex.X;
                                int y = vertex.Y;
                                int z = vertex.Z;
                                int unknownx = vertex.UnknownX;
                                int unknowny = vertex.UnknownY;
                                int unknownz = vertex.UnknownZ;
                                int color = vertex.Color;
                                scen.Vertices[i] = new SceneryVertex(x, y, z, unknownx, unknowny, unknownz);
                            }
                        }
                    }
                }
            }

            nsd.Spawns[0].SpawnX = -nsd.Spawns[0].SpawnX;
        }

    }
}
