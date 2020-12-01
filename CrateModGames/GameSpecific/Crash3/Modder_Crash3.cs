using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Crash3;
using CrateModLoader.ModProperties;
//Crash 3 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash3
{
    public sealed class Modder_Crash3 : Modder
    {

        public override Game Game => new Game()
        {
            Name = Crash3_Text.GameTitle,
            ShortName = "Crash3",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
            API_Credit = Crash3_Text.API_Credit,
            API_Link = "https://github.com/cbhacks/CrashEdit",
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS1] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SCUS_942.44",
                    Region = RegionType.NTSC_U,
                    ExecName = "SCUS_942.44",
                    CodeName = "SCUS_94244", },
                    new RegionCode() {
                    Name = @"SCES_014.20",
                    Region = RegionType.PAL,
                    ExecName = "SCES_014.20",
                    CodeName = "SCES_01420", },
                    new RegionCode() {
                    Name = @"SCPS_100.73",
                    Region = RegionType.NTSC_J,
                    ExecName = "SCPS_100.73",
                    CodeName = "SCPS_10073", },
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

        public static ModPropOption Option_AllCratesBlank = new ModPropOption(Crash3_Text.Mod_AllCratesBlank, Crash3_Text.Mod_AllCratesBlankDesc);
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(Crash3_Text.Mod_AllCratesWumpa, Crash3_Text.Mod_AllCratesWumpaDesc);
        public static ModPropOption Option_RandWarpRoom = new ModPropOption(Crash3_Text.Rand_WarpRoom, Crash3_Text.Rand_WarpRoomDesc) { Hidden = true }; //unstable, unfinished
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash3_Text.Mod_BackwardsLevels, Crash3_Text.Mod_BackwardsLevelsDesc);
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(Crash3_Text.Rand_BackwardsLevels, Crash3_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_RandCrateContents = new ModPropOption(Crash3_Text.Rand_CrateContents, Crash3_Text.Rand_CrateContentsDesc);
        public static ModPropOption Option_RandBoxCount = new ModPropOption(Crash3_Text.Rand_CrateCounter, Crash3_Text.Rand_CrateCounterDesc);
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(Crash3_Text.Mod_CameraWideFOV, Crash3_Text.Mod_CameraWideFOVDesc);
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(Crash3_Text.Rand_CameraFOV, Crash3_Text.Rand_CameraFOVDesc);
        public static ModPropOption Option_RandSounds = new ModPropOption(Crash3_Text.Rand_SFX, Crash3_Text.Rand_SFXDesc);
        public static ModPropOption Option_RandStreams = new ModPropOption(Crash3_Text.Rand_Streams, Crash3_Text.Rand_StreamsDesc);
        public static ModPropOption Option_RandWorldColors = new ModPropOption(Crash3_Text.Rand_WorldColors, Crash3_Text.Rand_WorldColorsDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(Crash3_Text.Rand_WorldPalette, Crash3_Text.Rand_WorldPaletteDesc);
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption(Crash3_Text.Mod_GreyscaleWorld, Crash3_Text.Mod_GreyscaleWorldDesc);
        public static ModPropOption Option_UntexturedWorld = new ModPropOption(Crash3_Text.Mod_UntexturedWorld, Crash3_Text.Mod_UntexturedWorldDesc);
        public static ModPropOption Option_RandWorldTex = new ModPropOption(Crash3_Text.Rand_WorldTex, Crash3_Text.Rand_WorldTexDesc);
        public static ModPropOption Option_RandObjCol = new ModPropOption(Crash3_Text.Rand_ObjCol, Crash3_Text.Rand_ObjColDesc);
        public static ModPropOption Option_RandObjTex = new ModPropOption(Crash3_Text.Rand_ObjTex, Crash3_Text.Rand_ObjTexDesc);

        public static ModPropOption Option_RandFlyingLevels = new ModPropOption(Crash3_Text.Rand_FlyingLevels, Crash3_Text.Rand_FlyingLevelsDesc) { Hidden = true }; //unfinished
        public static ModPropOption Option_RandBikeLevels = new ModPropOption("Randomize Bike Levels", "") { Hidden = true };
        public static ModPropOption Option_RandBosses = new ModPropOption("Randomize Boss Levels", "") { Hidden = true };
        public static ModPropOption Option_VehicleLevelsOnFoot = new ModPropOption("Vehicle Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "") { Hidden = true };
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "") { Hidden = true }; //only swap midis
        public static ModPropOption Option_RandMusicInstruments = new ModPropOption("Randomize Music Instruments", "") { Hidden = true }; //only swap wavebanks

        public Modder_Crash3()
        {

            ModCratesManualInstall = true;
            
        }

        public override void StartModProcess()
        {
            // there is nothing for us to do here...

            ModProcess();
        }

        void ModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            CrateSettings_CrashTri.VerifyModCrates(Game.ShortName, GameRegion);
            ModCrates.InstallLayerMods(ConsolePipeline.ExtractedPath, 0);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
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
                NewNSD nsd;
                try
                {
                    nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsfFile.FullName), GameVersion.Crash3);
                    nsd = NewNSD.Load(File.ReadAllBytes(nsdFile.FullName));
                }
                catch (LoadAbortedException)
                {
                    Console.WriteLine("Crash: LoadAbortedException: " + nsfFile.Name);
                    continue;
                    //return;
                }

                Crash3_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);

                if (Option_AllCratesWumpa.Enabled) Crash3_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                if (Option_RandWarpRoom.Enabled) Crash3_Mods.Mod_RandomizeWRButtons(nsf, nsd, NSF_Level, rand);
                if (Option_BackwardsLevels.Enabled || Option_RandBackwardsLevels.Enabled) Crash3_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Option_RandBackwardsLevels.Enabled, rand);
                if (Option_CameraBigFOV.Enabled || Option_RandCameraFOV.Enabled) Crash3_Mods.Mod_CameraFOV(nsf, rand, Option_RandCameraFOV.Enabled);
                if (Option_AllCratesBlank.Enabled) Crash3_Mods.Mod_RandomWoodCrates(nsf, rand);
                if (Option_RandCrateContents.Enabled) Crash3_Mods.Mod_RandomCrateContents(nsf, rand);
                if (Option_RandBosses.Enabled) Crash3_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                if (Option_RandFlyingLevels.Enabled) Crash3_Mods.Mod_RandomizeFlyingLevels(nsf, nsd, NSF_Level, rand, false);
                if (Option_RandBoxCount.Enabled) CrashTri_Common.Rand_BoxCount(nsf,rand);
                if (Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                if (Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                if (Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                if (Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                if (Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                if (Option_RandWorldTex.Enabled) CrashTri_Common.Mod_RandomizeWGEOTex(nsf, rand);
                if (Option_RandObjCol.Enabled) CrashTri_Common.Mod_RandomizeTGEOCol(nsf, rand);
                if (Option_RandObjTex.Enabled) CrashTri_Common.Mod_RandomizeTGEOTex(nsf, rand);
                if (Option_RandStreams.Enabled) CrashTri_Common.Mod_RandomizeSDIO(nsf, rand);

                Crash3_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);

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

        internal string[] Crash3_LevelFileNames = new string[]
        {
            "0A",
            "0B",
            "0C",
            "0D",
            "0E",
            "0F",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "1A",
            "1B",
            "1C",
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
            "03",
            "04",
            "05",
            "07",
            //Other
            "02",
        };

        internal Crash3_Levels GetLevelFromNSF(string NSF_Name)
        {
            for (int i = 0; i < Crash3_LevelFileNames.Length; i++)
            {
                if (NSF_Name.Contains("S00000" + Crash3_LevelFileNames[i]))
                {
                    return (Crash3_Levels)i;
                }
            }
            return Crash3_Levels.Unknown;
        }
    }
}
