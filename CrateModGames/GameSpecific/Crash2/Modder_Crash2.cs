using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Crash2;
using CrateModGames.GameSpecific.Crash1;
using CrateModLoader.ModProperties;
//Crash 2 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash2
{
    public sealed class Modder_Crash2 : Modder
    {
        public override Game Game => new Game()
        {
            Name = Crash2_Text.GameTitle,
            ShortName = "Crash2",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
            API_Credit = CrashTri_Text.API_Credit,
            API_Link = "https://github.com/cbhacks/CrashEdit",
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS1] = new RegionCode[]
                {
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
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

        public static ModPropOption Option_RandWarpRoom = new ModPropOption("Randomize Level Order", "") { Hidden = true };
        public static ModPropOption Option_RandWarpRoomExits = new ModPropOption(Crash2_Text.Rand_WarpRoom, Crash2_Text.Rand_WarpRoomDesc);
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash2_Text.Mod_BackwardsLevels, Crash2_Text.Mod_BackwardsLevelsDesc);
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(CrashTri_Text.Rand_BackwardsLevels, CrashTri_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_RandCratesMissing = new ModPropOption(CrashTri_Text.Rand_CratesRemoved, CrashTri_Text.Rand_EnemiesRemovedDesc);
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption(CrashTri_Text.Rand_EnemiesRemoved, CrashTri_Text.Rand_EnemiesRemovedDesc); 
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(CrashTri_Text.Mod_InvisibleCrates, CrashTri_Text.Mod_InvisibleCratesDesc);
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(CrashTri_Text.Rand_InvisibleCrates, CrashTri_Text.Rand_InvisibleCratesDesc);
        public static ModPropOption Option_AllCratesAshed = new ModPropOption("All Crates Are Covered Up", "All crates are covered up to look the same on the outside in each level.");
        public static ModPropOption Option_RandCratesAshed = new ModPropOption("Random Crates Are Covered Up", "Random crate types are covered up to look the same on the outside in each level.");
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        public static ModPropOption Option_RandCrateParams = new ModPropOption(CrashTri_Text.Rand_CrateParams, CrashTri_Text.Rand_CrateParamsDesc);
        public static ModPropOption Option_RandBoxCount = new ModPropOption(CrashTri_Text.Rand_CrateCounter, CrashTri_Text.Rand_CrateCounterDesc);
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash2_Text.Rand_BossLevels, Crash2_Text.Rand_BossLevelsDesc);
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc);
        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        public static ModPropOption Option_RandStreams = new ModPropOption(CrashTri_Text.Rand_Streams, CrashTri_Text.Rand_StreamsDesc);
        public static ModPropOption Option_RandPantsColor = new ModPropOption(CrashTri_Text.Rand_PantsColor, CrashTri_Text.Rand_PantsColorDesc);
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(CrashTri_Text.Mod_GreyscaleWorld, CrashTri_Text.Mod_GreyscaleWorldDesc);
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(CrashTri_Text.Mod_UntexturedWorld, CrashTri_Text.Mod_UntexturedWorldDesc);
        public static ModPropOption Option_RandWorldTex = new ModPropOption(CrashTri_Text.Rand_WorldTex, CrashTri_Text.Rand_WorldTexDesc);
        public static ModPropOption Option_RandObjPalette = new ModPropOption(CrashTri_Text.Rand_ObjectPalette, CrashTri_Text.Rand_ObjectPaletteDesc); 

        [ModCategory(1)]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, CrashTri_Text.Prop_PantsColor, CrashTri_Text.Prop_PantsColorDesc);

        //less used
        [ModCategory(1)]
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_WoodenCrates, CrashTri_Text.Rand_WoodenCratesDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_RandEnemyPaths = new ModPropOption(CrashTri_Text.Rand_EnemyPaths, CrashTri_Text.Rand_EnemyPathsDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption(CrashTri_Text.Mod_RemoveEnemies, CrashTri_Text.Mod_RemoveEnemiesDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_RandObjCol = new ModPropOption(CrashTri_Text.Rand_ObjCol, CrashTri_Text.Rand_ObjColDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_RandObjTex = new ModPropOption(CrashTri_Text.Rand_ObjTex, CrashTri_Text.Rand_ObjTexDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_UncoloredObj = new ModPropOption(CrashTri_Text.Mod_GreyscaleObjects, CrashTri_Text.Mod_GreyscaleObjectsDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(CrashTri_Text.Mod_AllCratesWumpa, CrashTri_Text.Mod_AllCratesWumpaDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(CrashTri_Text.Rand_CameraFOV, CrashTri_Text.Rand_CameraFOVDesc) { ModMenuOnly = true };

        //unfinished
        [ModCategory(1)]
        public static ModPropOption Option_AllEnemiesAreCrates = new ModPropOption(Crash2_Text.Rand_AllEnemiesCrates, Crash2_Text.Rand_AllEnemiesCratesDesc) { ModMenuOnly = true, Hidden = true, };
        public static ModPropOption Option_RandEnemiesAreCrates = new ModPropOption(CrashTri_Text.Rand_EnemyCrates, CrashTri_Text.Rand_EnemyCratesDesc) { Hidden = true }; //unfinished
        public static ModPropOption Option_UntexturedObj = new ModPropOption("Untextured Objects", "") { Hidden = true }; // broken
        public static ModPropOption Option_VehicleLevelsOnFoot = new ModPropOption("Vehicle Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "") { Hidden = true };
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
        public static ModPropOption Option_RandMusicInstruments = new ModPropOption("Randomize Music Instruments", "") { Hidden = true }; //only swap wavebanks

        public Modder_Crash2()
        {
            ModCrateRegionCheck = true;

        }

        public override void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();
        }

        void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);

            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs

            bool CachingPass = false;
            if (Option_RandMusic.Enabled || Option_RandMusicTracks.Enabled || Option_RandMusicInstruments.Enabled)
            {
                CachingPass = true;
            }
            CrashTri_Common.ResetCache();

            SceneryColor PantsColor = new SceneryColor(0, 0, 0);
            if (Option_RandPantsColor.Enabled)
            {
                PantsColor = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
                // just so that it doesn't affect gameplay randomizers
                rand = new Random(ModLoaderGlobals.RandomizerSeed);
            }

            if (Prop_PantsColor.HasChanged)
            {
                PantsColor = new SceneryColor((byte)Prop_PantsColor.R, (byte)Prop_PantsColor.G, (byte)Prop_PantsColor.B, 0);
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
                catch (Exception ex)
                {
                    if (ex is LoadAbortedException)
                    {
                        Console.WriteLine("Crash: LoadAbortedException: " + nsfFile.Name + "\n" + ex.Message);
                        continue;
                        //return;
                    }
                    else if (ex is LoadSkippedException)
                    {
                        Console.WriteLine("Crash: LoadSkippedException: " + nsfFile.Name + "\n" + ex.Message);
                        continue;
                        //return;
                    }
                    else
                        throw;
                }

                Crash2_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);

                if (CachingPass)
                {
                    if (Option_RandMusicTracks.Enabled)
                        CrashTri_Common.Cache_Music(nsf);
                }
                else
                {
                    if (Option_AllCratesWumpa.Enabled) Crash2_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                    if (Option_RandWarpRoom.Enabled) Crash2_Mods.Mod_RandomizeWarpRoom(nsf, nsd, NSF_Level, rand);
                    if (Option_RandWarpRoomExits.Enabled) Crash2_Mods.Mod_RandomizeWarpRoomExits(nsf, nsd, NSF_Level, rand);
                    if (Option_BackwardsLevels.Enabled || Option_RandBackwardsLevels.Enabled) Crash2_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Option_RandBackwardsLevels.Enabled, rand);
                    if (Option_VehicleLevelsOnFoot.Enabled && !Option_BackwardsLevels.Enabled) Crash2_Mods.Mod_VehicleLevelsOnFoot(nsf, nsd, NSF_Level);
                    if (Option_CameraBigFOV.Enabled || Option_RandCameraFOV.Enabled) Crash2_Mods.Mod_CameraFOV(nsf, rand, Option_RandCameraFOV.Enabled);
                    if (Option_AllCratesBlank.Enabled) Crash2_Mods.Mod_RandomWoodCrates(nsf, rand);
                    if (Option_RandCrates.Enabled) Crash2_Mods.Rand_WoodenCrates(nsf, rand, NSF_Level);
                    if (Option_RandBosses.Enabled) Crash2_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    if (Option_RandBoxCount.Enabled) Crash2_Mods.Rand_BoxCount(nsf, rand, NSF_Level);
                    if (Option_AllEnemiesMissing.Enabled) Crash2_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, false);
                    if (Option_RandEnemiesAreCrates.Enabled) Crash2_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, true);
                    if (Option_AllEnemiesAreCrates.Enabled) Crash2_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, false);
                    if (Option_RandEnemiesMissing.Enabled) Crash2_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, true);
                    if (Option_RandEnemyPaths.Enabled) Crash2_Mods.Rand_EnemyPaths(nsf, rand, NSF_Level);
                    if (Option_RandCratesMissing.Enabled) Crash2_Mods.Rand_CratesMissing(nsf, rand);
                    if (Option_RandCrateContents.Enabled) Crash2_Mods.Mod_RandomCrateContents(nsf, rand);
                    if (Option_RandCrateParams.Enabled) Crash2_Mods.Mod_RandomCrateParams(nsf, rand, NSF_Level);
                    if (Option_RandInvisibleCrates.Enabled) Crash2_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, true);
                    if (Option_InvisibleCrates.Enabled) Crash2_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, false);
                    if (Option_MirroredWorld.Enabled || Option_RandMirroredWorld.Enabled) Mod_MirrorLevel(nsf, nsd, rand, Option_RandMirroredWorld.Enabled);
                    if (Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    if (Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    if (Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    if (Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    if (NSF_Level != Crash2_Levels.Unknown && Option_RandMusicTracks.Enabled) CrashTri_Common.Randomize_Music(nsf, rand);
                    if (Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                    if (Option_RandWorldTex.Enabled) CrashTri_Common.Mod_RandomizeWGEOTex(nsf, rand);
                    if (Option_RandPantsColor.Enabled || Prop_PantsColor.HasChanged) Crash2_Mods.Mod_PantsColor(nsf, PantsColor);
                    if (Option_RandObjCol.Enabled) CrashTri_Common.Mod_RandomizeTGEOCol(nsf, rand);
                    if (Option_AllCratesAshed.Enabled) Crash2_Mods.Mod_AshedCrates(nsf, rand, false);
                    if (Option_RandCratesAshed.Enabled) Crash2_Mods.Mod_AshedCrates(nsf, rand, true);
                    if (Option_RandObjTex.Enabled) CrashTri_Common.Mod_RandomizeTGEOTex(nsf, rand);
                    if (Option_RandStreams.Enabled) CrashTri_Common.Mod_RandomizeSDIO(nsf, rand);
                    if (Option_RandObjPalette.Enabled) CrashTri_Common.Mod_SwizzleObjectColors(nsf, rand);
                    if (Option_UntexturedObj.Enabled) CrashTri_Common.Mod_RemoveTGEOTex(nsf, rand);
                    if (Option_UncoloredObj.Enabled) CrashTri_Common.Mod_RemoveObjectColors(nsf, rand);

                    Crash2_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);
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
