using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;
using CrateModLoader.GameSpecific.Crash3;
//Crash 3 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash3
{
    public sealed class Modder_Crash3 : Modder
    {
        internal const int RandomizeADIO = 0;
        internal const int RandomizeCratesIntoWood = 1;
        internal const int TurnCratesIntoWumpa = 2;
        internal const int RandomizeLevelOrder = 3;
        internal const int SceneryGreyscale = 4;
        internal const int SceneryRainbow = 5;
        internal const int SceneryColorSwizzle = 6;
        internal const int SceneryUntextured = 7;
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
        internal const int RandomizeFlyingLevels = 21;
        internal const int RandomizeBikeLevels = 22;
        internal const int RandomizeBosses = 23;
        internal const int RandomizeBoxCount = 24;

        public Modder_Crash3()
        {
            Game = new Game()
            {
                Name = Crash3_Text.GameTitle,
                ShortName = "Crash3",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = Crash3_Text.API_Credit,
                API_Link = "https://github.com/cbhacks/CrashEdit",
                Icon = Properties.Resources.icon_crash3,
                ModCratesSupported = true,
                RegionID_PS1 = new RegionCode[] {
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
            };
            ModCratesManualInstall = true;

            AddOption(RandomizeCratesIntoWood, new ModOption(Crash3_Text.Mod_AllCratesBlank, Crash3_Text.Mod_AllCratesBlankDesc));
            AddOption(TurnCratesIntoWumpa, new ModOption(Crash3_Text.Mod_AllCratesWumpa, Crash3_Text.Mod_AllCratesWumpaDesc));
            AddOption(RandomizeWarpRoom, new ModOption(Crash3_Text.Rand_WarpRoom, Crash3_Text.Rand_WarpRoomDesc));
            AddOption(BackwardsLevels, new ModOption(Crash3_Text.Mod_BackwardsLevels, Crash3_Text.Mod_BackwardsLevelsDesc));
            AddOption(RandomBackwardsLevels, new ModOption(Crash3_Text.Rand_BackwardsLevels, Crash3_Text.Rand_BackwardsLevelsDesc));
            AddOption(RandomizeCrateContents, new ModOption(Crash3_Text.Rand_CrateContents, Crash3_Text.Rand_CrateContentsDesc));
            AddOption(RandomizeBoxCount, new ModOption(Crash3_Text.Rand_CrateCounter, Crash3_Text.Rand_CrateCounterDesc));
            AddOption(RandomizeFlyingLevels, new ModOption(Crash3_Text.Rand_FlyingLevels, Crash3_Text.Rand_FlyingLevelsDesc));
            //AddOption(RandomizeBikeLevels, new ModOption("Randomize Bike Levels"));
            //AddOption(RandomizeBosses, new ModOption("Randomize Final Boss Level"));
            //AddOption(MirroredWorld, new ModOption("Mirrored World"));
            //AddOption(RandomLevelsMirrored, new ModOption("Random Levels Are Mirrored"));
            AddOption(CameraBiggerFOV, new ModOption(Crash3_Text.Mod_CameraWideFOV, Crash3_Text.Mod_CameraWideFOVDesc));
            AddOption(RandomizeCameraFOV, new ModOption(Crash3_Text.Rand_CameraFOV, Crash3_Text.Rand_CameraFOVDesc));
            AddOption(RandomizeADIO, new ModOption(Crash3_Text.Rand_SFX, Crash3_Text.Rand_SFXDesc));
            //AddOption(RandomizeMusic, new ModOption("Randomize Music")); //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
            //AddOption(RandomizeMusicTracks, new ModOption("Randomize Music Tracks")); //only swap midis
            //AddOption(RandomzieMusicInstruments, new ModOption("Randomize Music Instruments")); //only swap wavebanks
            AddOption(SceneryRainbow, new ModOption(Crash3_Text.Rand_WorldColors, Crash3_Text.Rand_WorldColorsDesc));
            AddOption(SceneryColorSwizzle, new ModOption(Crash3_Text.Rand_WorldPalette, Crash3_Text.Rand_WorldPaletteDesc));
            AddOption(SceneryGreyscale, new ModOption(Crash3_Text.Mod_GreyscaleWorld, Crash3_Text.Mod_GreyscaleWorldDesc));
            AddOption(SceneryUntextured, new ModOption(Crash3_Text.Mod_UntexturedWorld, Crash3_Text.Mod_UntexturedWorldDesc));
            
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

                if (GetOption(TurnCratesIntoWumpa)) Crash3_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                if (GetOption(RandomizeWarpRoom)) Crash3_Mods.Mod_RandomizeWRButtons(nsf, nsd, NSF_Level, rand);
                if (GetOption(BackwardsLevels) || GetOption(RandomBackwardsLevels)) Crash3_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, GetOption(RandomBackwardsLevels), rand);
                if (GetOption(CameraBiggerFOV) || GetOption(RandomizeCameraFOV)) Crash3_Mods.Mod_CameraFOV(nsf, rand, GetOption(RandomizeCameraFOV));
                if (GetOption(RandomizeCratesIntoWood)) Crash3_Mods.Mod_RandomWoodCrates(nsf, rand);
                if (GetOption(RandomizeCrateContents)) Crash3_Mods.Mod_RandomCrateContents(nsf, rand);
                if (GetOption(RandomizeBosses)) Crash3_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                if (GetOption(RandomizeFlyingLevels)) Crash3_Mods.Mod_RandomizeFlyingLevels(nsf, nsd, NSF_Level, rand, false);
                if (GetOption(RandomizeBoxCount)) CrashTri_Common.Rand_BoxCount(nsf,rand);
                if (GetOption(RandomizeADIO)) Mod_RandomizeADIO(nsf, nsd, rand);
                if (GetOption(SceneryColorSwizzle)) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                if (GetOption(SceneryGreyscale)) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                if (GetOption(SceneryRainbow)) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                if (GetOption(SceneryUntextured)) CrashTri_Common.Mod_Scenery_Untextured(nsf);

                Crash3_Mods.Mod_Metadata(nsf, nsd, NSF_Level);

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

        internal void Mod_RandomizeADIO(NSF nsf, NewNSD nsd, Random rand)
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

        internal Crash3_Levels GetLevelFromNSF(string NSf_Name)
        {
            for (int i = 0; i < Crash3_LevelFileNames.Length; i++)
            {
                if (NSf_Name.Contains("S00000" + Crash3_LevelFileNames[i]))
                {
                    return (Crash3_Levels)i;
                }
            }
            return Crash3_Levels.Unknown;
        }
    }
}
