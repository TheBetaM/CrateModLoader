﻿using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.Crash1;
using CrateModLoader.ModProperties;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)

namespace CrateModLoader.GameSpecific.Crash1
{
    public sealed class Modder_Crash1 : Modder
    {

        public override Game Game => new Game()
        {
            Name = Crash1_Text.GameTitle,
            ShortName = "Crash1",
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
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

        public static ModPropOption Option_RandBonusRounds = new ModPropOption("Randomize Bonus Rounds", "") { Hidden = true, }; //todo
        public static ModPropOption Option_RandMap = new ModPropOption("Randomize Level Order", "Shuffle the order of which levels you enter. The Cortex boss is still the last level to play.");
        public static ModPropOption Option_AddStormyAscent = new ModPropOption("Add Stormy Ascent", "Replaces The Great Hall with Stormy Ascent. Works with all other features like Backwards Levels and Randomize Level Order. (Tokens removed from the level to ensure stability)");
        public static ModPropOption Option_RandCrates = new ModPropOption(CrashTri_Text.Rand_Crates, CrashTri_Text.Rand_CratesDesc);
        public static ModPropOption Option_BackwardsLevels = new ModPropOption(Crash1_Text.Mod_BackwardsLevels, Crash1_Text.Mod_BackwardsLevelsDesc);
        public static ModPropOption Option_RandBackwardsLevels = new ModPropOption(Crash1_Text.Rand_BackwardsLevels, Crash1_Text.Rand_BackwardsLevelsDesc);
        public static ModPropOption Option_BackwardsHogLevels = new ModPropOption(Crash1_Text.Mod_BackwardsHogLevels, Crash1_Text.Mod_BackwardsHogLevelsDesc);
        public static ModPropOption Option_RandEnemiesMissing = new ModPropOption("Random Enemies Removed", "") { Hidden = true, }; //todo
        public static ModPropOption Option_RandCrateContents = new ModPropOption(CrashTri_Text.Rand_CrateContents, CrashTri_Text.Rand_CrateContentsDesc);
        public static ModPropOption Option_InvisibleCrates = new ModPropOption(Crash1_Text.Mod_InvisibleCrates, Crash1_Text.Mod_InvisibleCratesDesc);
        public static ModPropOption Option_RandInvisibleCrates = new ModPropOption(Crash1_Text.Rand_InvisibleCrates, Crash1_Text.Rand_InvisibleCratesDesc);
        public static ModPropOption Option_RandBosses = new ModPropOption(Crash1_Text.Rand_BossLevels, Crash1_Text.Rand_BossLevelsDesc);
        public static ModPropOption Option_RandLightCol = new ModPropOption(Crash1_Text.Rand_LightCol, Crash1_Text.Rand_LightColDesc);
        public static ModPropOption Option_EnableDog = new ModPropOption(Crash1_Text.Mod_EnableDog, Crash1_Text.Mod_EnableDog);
        public static ModPropOption Option_CameraBigFOV = new ModPropOption(CrashTri_Text.Mod_CameraWideFOV, CrashTri_Text.Mod_CameraWideFOVDesc) { Hidden = true, };

        public static ModPropOption Option_RandMusicTracks = new ModPropOption("Randomize Music Tracks", "Music tracks are randomized, still played using the level's instruments."); //only swaps midis
        public static ModPropOption Option_RandSounds = new ModPropOption(CrashTri_Text.Rand_SFX, CrashTri_Text.Rand_SFXDesc);
        public static ModPropOption Option_RandWorldColors = new ModPropOption(CrashTri_Text.Rand_WorldColors, CrashTri_Text.Rand_WorldColorsDesc);
        public static ModPropOption Option_RandWorldPalette = new ModPropOption(CrashTri_Text.Rand_WorldPalette, CrashTri_Text.Rand_WorldPaletteDesc);

        // less used
        [ModCategory(1)]
        public static ModPropOption Option_AllEnemiesMissing = new ModPropOption("All Enemies Removed", "") { Hidden = true, }; // { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_AllCratesBlank = new ModPropOption(CrashTri_Text.Mod_AllCratesBlank, CrashTri_Text.Mod_AllCratesBlankDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_AllCratesWumpa = new ModPropOption(Crash1_Text.Mod_AllCratesWumpa, Crash1_Text.Mod_AllCratesWumpaDesc) { ModMenuOnly = true };
        [ModCategory(1)]
        public static ModPropOption Option_RandCameraFOV = new ModPropOption(Crash1_Text.Rand_CameraFOV, Crash1_Text.Rand_CameraFOVDesc) { ModMenuOnly = true };

        // unfinished
        public static ModPropOption Option_RandPantsColor = new ModPropOption("Randomize Pants Color", "") { Hidden = true };

        [ModCategory(1)]
        public static ModPropColor Prop_PantsColor = new ModPropColor(new int[4] { 0, 0, 255, 255 }, "Pants Color", "")
        { Hidden = true };

        public static ModPropOption Option_AddCavernLevel = new ModPropOption("Add Caved In", "Replaces Papu Papu with the unused cavern level.");// { Hidden = true, };
        public static ModPropOption Option_HogLevelsOnFoot = new ModPropOption("Hog Levels On Foot", "") { Hidden = true };
        public static ModPropOption Option_MirroredWorld = new ModPropOption("Mirrored World", "") { Hidden = true };
        public static ModPropOption Option_RandMirroredWorld = new ModPropOption("Random Levels Are Mirrored", "") { Hidden = true };
        public static ModPropOption Option_RandMusic = new ModPropOption("Randomize Music", "") { Hidden = true }; //shuffle tracks from different levels (must be identical to vanilla playback, just in a different level)
        public static ModPropOption Option_RandMusicInstruments = new ModPropOption("Randomize Music Instruments", "") { Hidden = true }; //only swap wavebanks
        public static ModPropOption Option_GreyscaleWorld = new ModPropOption("Greyscale World", "") { Hidden = true };
        public static ModPropOption Option_UntexturedWorld = new ModPropOption("Untextured/Greyscale World", "") { Hidden = true };

        public Modder_Crash1()
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
            CrashTri_Common.ResetCache();

            if (Option_RandMusicTracks.Enabled)
            {
                CachingPass = true;
            }
            

            OldSceneryColor PantsColor = new OldSceneryColor(0, 0, 0, false);
            if (Option_RandPantsColor.Enabled)
            {
                PantsColor = new OldSceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), false);
                // just so that it doesn't affect gameplay randomizers
                rand = new Random(ModLoaderGlobals.RandomizerSeed);
            }

            if (Prop_PantsColor.HasChanged)
            {
                PantsColor = new OldSceneryColor((byte)(Prop_PantsColor.Value[0] * 255f), (byte)(Prop_PantsColor.Value[1] * 255f), (byte)(Prop_PantsColor.Value[2] * 255f), false);
            }

            if (Option_AddCavernLevel.Enabled)
            {
                File.Delete(Path.Combine(ConsolePipeline.ExtractedPath, @"S0\S0000004.NSD"));
                File.Copy(Path.Combine(ConsolePipeline.ExtractedPath, @"S0\S000000A.NSD"), Path.Combine(ConsolePipeline.ExtractedPath, @"S0\S0000004.NSD"));
            }

            for (int i = 0; i < Math.Min(nsfs.Count, nsds.Count); ++i)
            {
                FileInfo nsfFile = nsfs[i];
                FileInfo nsdFile = nsds[i];
                if (Path.GetFileNameWithoutExtension(nsfFile.Name) != Path.GetFileNameWithoutExtension(nsdFile.Name))
                {
                    //MessageBox.Show($"NSF /NSD file pair mismatch. First mismatch:\n\n{nsfFile.Name}\n{nsdFile.Name}");
                    continue;
                }

                NSF nsf;
                OldNSD nsd;
                try
                {
                    nsf = NSF.LoadAndProcess(File.ReadAllBytes(nsfFile.FullName), GameVersion.Crash1);
                    nsd = OldNSD.Load(File.ReadAllBytes(nsdFile.FullName));
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

                Crash1_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);
                
                if (CachingPass)
                {
                    if (Option_HogLevelsOnFoot.Enabled) 
                        Crash1_Mods.Cache_NormalCrashData(nsf, nsd, NSF_Level);
                    if (Option_RandMusicTracks.Enabled)
                        CrashTri_Common.Cache_Music(nsf);
                }
                else
                {
                    if (Option_AllCratesWumpa.Enabled) Crash1_Mods.Mod_TurnCratesIntoWumpa(nsf, rand, NSF_Level);
                    if (Option_RandCrates.Enabled) Crash1_Mods.Mod_RandomCrates(nsf, rand, NSF_Level);
                    if (Option_RandBonusRounds.Enabled) Crash1_Mods.Mod_RandomizeBonusRounds(nsf, nsd, NSF_Level, rand);
                    if (Option_BackwardsLevels.Enabled || Option_RandBackwardsLevels.Enabled) Crash1_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Option_RandBackwardsLevels.Enabled, rand);
                    if (Option_BackwardsHogLevels.Enabled) Crash1_Mods.Mod_HogLevelsBackwards(nsf, nsd, NSF_Level);
                    if (Option_CameraBigFOV.Enabled || Option_RandCameraFOV.Enabled) Crash1_Mods.Mod_CameraFOV(nsf, rand, Option_RandCameraFOV.Enabled);
                    if (Option_AllCratesBlank.Enabled) Crash1_Mods.Mod_RandomWoodCrates(nsf, rand, NSF_Level);
                    if (Option_RandCrateContents.Enabled) Crash1_Mods.Mod_RandomCrateContents(nsf, rand, NSF_Level);
                    if (Option_RandInvisibleCrates.Enabled) Crash1_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, true);
                    if (Option_InvisibleCrates.Enabled) Crash1_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, false);
                    if (Option_RandBosses.Enabled) Crash1_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    if (Option_AddStormyAscent.Enabled) Crash1_Mods.Mod_AddStormyAscent(nsf, nsd, NSF_Level, GameRegion.Region);
                    if (Option_AddCavernLevel.Enabled) Crash1_Mods.Mod_AddCavernLevel(nsf, nsd, NSF_Level, GameRegion.Region);
                    if (Option_RandMap.Enabled) Crash1_Mods.Mod_RandomizeMap(nsf, nsd, NSF_Level, rand, GameRegion.Region);
                    if (Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    if (Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    if (Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    if (Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    if (Option_RandPantsColor.Enabled || Prop_PantsColor.HasChanged) Crash1_Mods.Mod_PantsColor(nsf, PantsColor);
                    if (NSF_Level != Crash1_Levels.MapMainMenu && Option_RandMusicTracks.Enabled) CrashTri_Common.Randomize_Music(nsf, rand);
                    if (Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                    if (Option_RandLightCol.Enabled) Crash1_Mods.Mod_RandomLightColor(nsf, rand);

                    if (NSF_Level == Crash1_Levels.L16_HeavyMachinery && Option_EnableDog.Enabled) Crash1_Mods.Mod_EnableDog(nsf);

                    Crash1_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);
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

        internal void PatchNSD(NSF nsf, OldNSD nsd)
        {
            // edit NSD
            nsd.ChunkCount = nsf.Chunks.Count;
            var indexdata = nsf.MakeNSDIndex();
            nsd.HashKeyMap = indexdata.Item1;
            nsd.Index = indexdata.Item2;
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
            "04",
            //Bonus
            "24",
            "25",
            "33",
            "34",
        };

        internal Crash1_Levels GetLevelFromNSF(string nsf_name)
        {
            for (int i = 0; i < Crash1_LevelFileNames.Length; i++)
            {
                if (nsf_name.Contains("S00000" + Crash1_LevelFileNames[i]))
                {
                    return (Crash1_Levels)i;
                }
            }
            return Crash1_Levels.Unknown;
        }
    }
}
