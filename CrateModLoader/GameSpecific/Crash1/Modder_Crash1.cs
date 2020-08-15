using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;
using CrateModLoader.GameSpecific.Crash1;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the main menu.

namespace CrateModLoader.GameSpecific.Crash1
{
    public sealed class Modder_Crash1 : Modder
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
        internal const int RandomizeMap = 19;
        internal const int RandomizeCrateContents = 20;
        internal const int RandomizeBosses = 21;
        internal const int BackwardsHogLevels = 22;

        public Modder_Crash1()
        {
            Game = new Game()
            {
                Name = Crash1_Text.GameTitle,
                ShortName = "Crash1",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = Crash1_Text.API_Credit,
                API_Link = "https://github.com/cbhacks/CrashEdit",
                Icon = Properties.Resources.icon_crash1,
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

            AddOption(RandomizeCratesIntoWood, new ModOption(Crash1_Text.Mod_AllCratesBlank, Crash1_Text.Mod_AllCratesBlankDesc));
            AddOption(TurnCratesIntoWumpa, new ModOption(Crash1_Text.Mod_AllCratesWumpa, Crash1_Text.Mod_AllCratesWumpaDesc));
            //AddOption(RandomizeMap, new ModOption("Randomize Map"));
            AddOption(BackwardsLevels, new ModOption(Crash1_Text.Mod_BackwardsLevels, Crash1_Text.Mod_BackwardsLevelsDesc));
            AddOption(RandomBackwardsLevels, new ModOption(Crash1_Text.Rand_BackwardsLevels, Crash1_Text.Rand_BackwardsLevelsDesc));
            AddOption(BackwardsHogLevels, new ModOption(Crash1_Text.Mod_BackwardsHogLevels, Crash1_Text.Mod_BackwardsHogLevelsDesc));
            AddOption(RandomizeCrateContents, new ModOption(Crash1_Text.Rand_CrateContents, Crash1_Text.Rand_CrateContentsDesc));
            AddOption(RandomizeBosses, new ModOption(Crash1_Text.Rand_BossLevels, Crash1_Text.Rand_BossLevelsDesc));
            //AddOption(VehicleLevelsOnFoot, new ModOption("Hog Levels On Foot"));
            //AddOption(MirroredWorld, new ModOption("Mirrored World"));
            //AddOption(RandomLevelsMirrored, new ModOption("Random Levels Are Mirrored"));
            AddOption(CameraBiggerFOV, new ModOption(Crash1_Text.Mod_CameraWideFOV, Crash1_Text.Mod_CameraWideFOVDesc));
            AddOption(RandomizeCameraFOV, new ModOption(Crash1_Text.Rand_CameraFOV, Crash1_Text.Rand_CameraFOVDesc));
            AddOption(RandomizeADIO, new ModOption(Crash1_Text.Rand_SFX, Crash1_Text.Rand_SFXDesc));
            //AddOption(RandomizeMusic, new ModOption("Randomize Music")); //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
            //AddOption(RandomizeMusicTracks, new ModOption("Randomize Music Tracks")); //only swap midis
            //AddOption(RandomzieMusicInstruments, new ModOption("Randomize Music Instruments")); //only swap wavebanks
            AddOption(SceneryRainbow, new ModOption(Crash1_Text.Rand_WorldColors, Crash1_Text.Rand_WorldColorsDesc));
            AddOption(SceneryColorSwizzle, new ModOption(Crash1_Text.Rand_WorldPalette, Crash1_Text.Rand_WorldPaletteDesc));
            //AddOption(SceneryGreyscale, new ModOption("Greyscale World")); 
            //AddOption(SceneryUntextured, new ModOption("Untextured/Greyscale World")); //todo
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
            /*
            if (GetOption(VehicleLevelsOnFoot))
            {
                CachingPass = true;
            }
            */

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
                    Console.WriteLine("Crash: LoadAbortedException: " + nsfFile.Name);
                    continue;
                    //return;
                }

                Crash1_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);
                
                if (CachingPass)
                {
                    if (GetOption(VehicleLevelsOnFoot)) 
                        Crash1_Mods.Cache_NormalCrashData(nsf, nsd, NSF_Level);
                }
                else
                {
                    if (GetOption(BackwardsLevels) || GetOption(RandomBackwardsLevels)) Crash1_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, GetOption(RandomBackwardsLevels), rand);
                    if (GetOption(BackwardsHogLevels)) Crash1_Mods.Mod_HogLevelsBackwards(nsf, nsd, NSF_Level);
                    if (GetOption(CameraBiggerFOV) || GetOption(RandomizeCameraFOV)) Crash1_Mods.Mod_CameraFOV(nsf, rand, GetOption(RandomizeCameraFOV));
                    if (GetOption(RandomizeADIO)) Mod_RandomizeADIO(nsf, nsd, rand);
                    if (GetOption(RandomizeCratesIntoWood)) Crash1_Mods.Mod_RandomWoodCrates(nsf, rand);
                    if (GetOption(RandomizeCrateContents)) Crash1_Mods.Mod_RandomCrateContents(nsf, rand, NSF_Level);
                    if (GetOption(RandomizeBosses)) Crash1_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    if (GetOption(TurnCratesIntoWumpa)) Crash1_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                    if (GetOption(SceneryColorSwizzle)) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    if (GetOption(SceneryGreyscale)) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    if (GetOption(SceneryRainbow)) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    if (GetOption(SceneryUntextured)) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    if (GetOption(ZoneCloseCamera)) CrashTri_Common.Mod_Camera_Closeup(nsf);

                    Crash1_Mods.Mod_Metadata(nsf, nsd, NSF_Level);
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

        internal string[] Crash1_LevelFileNames = new string[]
        {
            "03",
            "05",
            "06",
            "07",
            "09",
            "0C",
            "0E",
            "0F",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "18",
            "1A",
            "1C",
            "1D",
            "1E",
            "20",
            "22",
            "23",
            "28",
            "29",
            "2A",
            "2C",
            "2E",
            "37",
            //Bosses
            "0A",
            "17",
            "21",
            "08",
            "1B",
            "1F",
            //Other
            "19",
        };

        internal Crash1_Levels GetLevelFromNSF(string NSf_Name)
        {
            for (int i = 0; i < Crash1_LevelFileNames.Length; i++)
            {
                if (NSf_Name.Contains("S00000" + Crash1_LevelFileNames[i]))
                {
                    return (Crash1_Levels)i;
                }
            }
            return Crash1_Levels.Unknown;
        }
    }
}
