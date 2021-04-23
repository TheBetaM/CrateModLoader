using Crash;
using System;
using System.Collections.Generic;
using System.IO;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)

namespace CrateModLoader.GameSpecific.Crash1
{
    public sealed class Modder_Crash1 : Modder
    {
        public override bool ModCrateRegionCheck => true;

        public Modder_Crash1() { }

        public override void StartModProcess()
        {
            Random rand = new Random(ModLoaderGlobals.RandomizerSeed);

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs

            bool CachingPass = false;
            //CrashTri_Common.ResetCache();

            if (Crash1_Props_Main.Option_RandMusicTracks.Enabled)
            {
                CachingPass = true;
            }

            if (Crash1_Props_Main.Option_AddCavernLevel.Enabled)
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
                    //if (Crash1_Props_Main.Option_RandMusicTracks.Enabled)
                        //CrashTri_Common.Cache_Music(nsf);
                }
                else
                {
                    //if (Crash1_Props_Misc.Option_AllCratesWumpa.Enabled) Crash1_Mods.Mod_TurnCratesIntoWumpa(nsf, rand, NSF_Level);
                    //if (Crash1_Props_Main.Option_RandCrates.Enabled) Crash1_Mods.Mod_RandomCrates(nsf, rand, NSF_Level);
                    //if (Crash1_Props_Main.Option_RandBonusRounds.Enabled) Crash1_Mods.Mod_RandomizeBonusRounds(nsf, nsd, NSF_Level, rand);
                    //if (Crash1_Props_Main.Option_BackwardsLevels.Enabled || Crash1_Props_Main.Option_RandBackwardsLevels.Enabled) Crash1_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Crash1_Props_Main.Option_RandBackwardsLevels.Enabled, rand);
                    //if (Crash1_Props_Main.Option_BackwardsHogLevels.Enabled) Crash1_Mods.Mod_HogLevelsBackwards(nsf, nsd, NSF_Level);
                    //if (Crash1_Props_Main.Option_CameraBigFOV.Enabled || Crash1_Props_Misc.Option_RandCameraFOV.Enabled) Crash1_Mods.Mod_CameraFOV(nsf, rand, Crash1_Props_Misc.Option_RandCameraFOV.Enabled);
                    //if (Crash1_Props_Misc.Option_AllCratesBlank.Enabled) Crash1_Mods.Mod_RandomWoodCrates(nsf, rand, NSF_Level);
                    //if (Crash1_Props_Main.Option_RandCrateContents.Enabled) Crash1_Mods.Mod_RandomCrateContents(nsf, rand, NSF_Level);
                    //if (Crash1_Props_Main.Option_RandInvisibleCrates.Enabled) Crash1_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, true);
                    //if (Crash1_Props_Main.Option_InvisibleCrates.Enabled) Crash1_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, false);
                    //if (Crash1_Props_Main.Option_RandBosses.Enabled) Crash1_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    //if (Crash1_Props_Main.Option_AddStormyAscent.Enabled) Crash1_Mods.Mod_AddStormyAscent(nsf, nsd, NSF_Level, GameRegion.Region);
                    //if (Crash1_Props_Main.Option_AddCavernLevel.Enabled) Crash1_Mods.Mod_AddCavernLevel(nsf, nsd, NSF_Level, GameRegion.Region);
                    //if (Crash1_Props_Main.Option_RandMap.Enabled) Crash1_Mods.Mod_RandomizeMap(nsf, nsd, NSF_Level, rand, GameRegion.Region);
                    //if (Crash1_Props_Main.Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    //if (Crash1_Props_Main.Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    //if (Crash1_Props_Main.Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    //if (Crash1_Props_Main.Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    //if (Crash1_Props_Misc.Option_InvisibleWorld.Enabled) CrashTri_Common.Mod_Scenery_Invisible(nsf);
                    //if (Crash1_Props_Main.Option_RandPantsColor.Enabled || Crash1_Props_Misc.Prop_PantsColor.HasChanged) Crash1_Mods.Mod_PantsColor(nsf, PantsColor);
                    //if (NSF_Level != Crash1_Levels.MapMainMenu && Crash1_Props_Main.Option_RandMusicTracks.Enabled) CrashTri_Common.Randomize_Music(nsf, rand);
                    //if (Crash1_Props_Main.Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                    //if (Crash1_Props_Main.Option_RandLightCol.Enabled) Crash1_Mods.Mod_RandomLightColor(nsf, rand);

                    //if (NSF_Level == Crash1_Levels.L16_HeavyMachinery && Crash1_Props_Main.Option_EnableDog.Enabled) Crash1_Mods.Mod_EnableDog(nsf);

                    //Crash1_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);
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
