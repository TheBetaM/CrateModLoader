using Crash;
using System;
using System.Collections.Generic;
using System.IO;
//Crash 2 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash2
{
    public sealed class Modder_Crash2 : Modder
    {
        public override bool ModCrateRegionCheck => true;

        public Modder_Crash2() { }

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
            //if (Crash2_Props_Main.Option_RandMusic.Enabled || Crash2_Props_Main.Option_RandMusicTracks.Enabled || Crash2_Props_Main.Option_RandMusicInstruments.Enabled)
            //{
            //    CachingPass = true;
            //}
            //CrashTri_Common.ResetCache();

            //List<List<WavebankChunk>> wavebankChunks = new List<List<WavebankChunk>>();
            //List<List<MusicEntry>> musicEntries = new List<List<MusicEntry>>();

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
                    //if (Crash2_Props_Main.Option_RandMusicTracks.Enabled)
                        //CrashTri_Common.Cache_Music(nsf);
                }
                else
                {
                    //if (Crash2_Props_Misc.Option_AllCratesWumpa.Enabled) Crash2_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandWarpRoom.Enabled) Crash2_Mods.Mod_RandomizeWarpRoom(nsf, nsd, NSF_Level, rand);
                    //if (Crash2_Props_Main.Option_RandWarpRoomExits.Enabled) Crash2_Mods.Mod_RandomizeWarpRoomExits(nsf, nsd, NSF_Level, rand);
                    //if (Crash2_Props_Main.Option_BackwardsLevels.Enabled || Crash2_Props_Main.Option_RandBackwardsLevels.Enabled) Crash2_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Crash2_Props_Main.Option_RandBackwardsLevels.Enabled, rand);
                    //if (Crash2_Props_Main.Option_VehicleLevelsOnFoot.Enabled && !Crash2_Props_Main.Option_BackwardsLevels.Enabled) Crash2_Mods.Mod_VehicleLevelsOnFoot(nsf, nsd, NSF_Level);
                    //if (Crash2_Props_Main.Option_CameraBigFOV.Enabled || Crash2_Props_Misc.Option_RandCameraFOV.Enabled) Crash2_Mods.Mod_CameraFOV(nsf, rand, Crash2_Props_Misc.Option_RandCameraFOV.Enabled);
                    //if (Crash2_Props_Misc.Option_AllCratesBlank.Enabled) Crash2_Mods.Mod_RandomWoodCrates(nsf, rand);
                    //if (Crash2_Props_Misc.Option_RandCrates.Enabled) Crash2_Mods.Rand_WoodenCrates(nsf, rand, NSF_Level);
                    //if (Crash2_Props_Main.Option_RandBosses.Enabled) Crash2_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    //if (Crash2_Props_Main.Option_RandBoxCount.Enabled) Crash2_Mods.Rand_BoxCount(nsf, rand, NSF_Level);
                    //if (Crash2_Props_Misc.Option_AllEnemiesMissing.Enabled) Crash2_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, false);
                    //if (Crash2_Props_Main.Option_RandEnemiesAreCrates.Enabled) Crash2_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, true);
                    //if (Crash2_Props_Misc.Option_AllEnemiesAreCrates.Enabled) Crash2_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, false);
                    //if (Crash2_Props_Main.Option_RandEnemiesMissing.Enabled) Crash2_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, true);
                    //if (Crash2_Props_Misc.Option_RandEnemyPaths.Enabled) Crash2_Mods.Rand_EnemyPaths(nsf, rand, NSF_Level);
                    //if (Crash2_Props_Main.Option_RandCratesMissing.Enabled) Crash2_Mods.Rand_CratesMissing(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandCrateContents.Enabled) Crash2_Mods.Mod_RandomCrateContents(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandCrateParams.Enabled) Crash2_Mods.Mod_RandomCrateParams(nsf, rand, NSF_Level);
                    //if (Crash2_Props_Main.Option_RandInvisibleCrates.Enabled) Crash2_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, true);
                    //if (Crash2_Props_Main.Option_InvisibleCrates.Enabled) Crash2_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, false);
                    //if (Crash2_Props_Main.Option_MirroredWorld.Enabled || Crash2_Props_Main.Option_RandMirroredWorld.Enabled) Mod_MirrorLevel(nsf, nsd, rand, Crash2_Props_Main.Option_RandMirroredWorld.Enabled);
                    //if (Crash2_Props_Main.Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    //if (Crash2_Props_Main.Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    //if (Crash2_Props_Main.Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    //if (Crash2_Props_Main.Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    //if (Crash2_Props_Misc.Option_InvisibleWorld.Enabled) CrashTri_Common.Mod_Scenery_Invisible(nsf);
                    //if (NSF_Level != Crash2_Levels.Unknown && Crash2_Props_Main.Option_RandMusicTracks.Enabled) CrashTri_Common.Randomize_Music(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandWorldTex.Enabled) CrashTri_Common.Mod_RandomizeWGEOTex(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandPantsColor.Enabled || Crash2_Props_Misc.Prop_PantsColor.HasChanged) Crash2_Mods.Mod_PantsColor(nsf, PantsColor);
                    //if (Crash2_Props_Misc.Option_RandObjCol.Enabled) CrashTri_Common.Mod_RandomizeTGEOCol(nsf, rand);
                    //if (Crash2_Props_Main.Option_AllCratesAshed.Enabled) Crash2_Mods.Mod_AshedCrates(nsf, rand, false);
                    //if (Crash2_Props_Main.Option_RandCratesAshed.Enabled) Crash2_Mods.Mod_AshedCrates(nsf, rand, true);
                    //if (Crash2_Props_Misc.Option_RandObjTex.Enabled) CrashTri_Common.Mod_RandomizeTGEOTex(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandStreams.Enabled) CrashTri_Common.Mod_RandomizeSDIO(nsf, rand);
                    //if (Crash2_Props_Main.Option_RandObjPalette.Enabled) CrashTri_Common.Mod_SwizzleObjectColors(nsf, rand);
                    //if (Crash2_Props_Main.Option_UntexturedObj.Enabled) CrashTri_Common.Mod_RemoveTGEOTex(nsf, rand);
                    //if (Crash2_Props_Misc.Option_UncoloredObj.Enabled) CrashTri_Common.Mod_RemoveObjectColors(nsf, rand);

                    //Crash2_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);
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

        internal Crash2_Levels GetLevelFromNSF(string NSf_Name)
        {
            for (int i = 0; i < Crash2_Common.Crash2_LevelFileNames.Length; i++)
            {
                if (NSf_Name.Contains("S00000" + Crash2_Common.Crash2_LevelFileNames[i]))
                {
                    return (Crash2_Levels)i;
                }
            }
            return Crash2_Levels.Unknown;
        }

    }
}
