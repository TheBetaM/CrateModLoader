using Crash;
using System;
using System.Collections.Generic;
using System.IO;
//Crash 3 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.

namespace CrateModLoader.GameSpecific.Crash3
{
    public sealed class Modder_Crash3 : Modder
    {

        public Modder_Crash3()
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

            SceneryColor PantsColor = new SceneryColor(0, 0, 0);
            if (Crash3_Props_Main.Option_RandPantsColor.Enabled)
            {
                PantsColor = new SceneryColor((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256), 0);
                // just so that it doesn't affect gameplay randomizers
                rand = new Random(ModLoaderGlobals.RandomizerSeed);
            }

            if (Crash3_Props_Misc.Prop_PantsColor.HasChanged)
            {
                PantsColor = new SceneryColor((byte)Crash3_Props_Misc.Prop_PantsColor.R, (byte)Crash3_Props_Misc.Prop_PantsColor.G, (byte)Crash3_Props_Misc.Prop_PantsColor.B, 0);
            }

            bool CachingPass = false;
            if (Crash3_Props_Main.Option_RandMusic.Enabled || Crash3_Props_Main.Option_RandMusicTracks.Enabled || Crash3_Props_Main.Option_RandMusicInstruments.Enabled)
            {
                CachingPass = true;
            }
            CrashTri_Common.ResetCache();

            List<FileInfo> nsfs = new List<FileInfo>();
            List<FileInfo> nsds = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            AppendFileInfoDir(nsfs, nsds, di); // this should return all NSF/NSD file pairs


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

                Crash3_Levels NSF_Level = GetLevelFromNSF(nsfFile.Name);

                if (CachingPass)
                {
                    if (Crash3_Props_Main.Option_RandMusicTracks.Enabled)
                        CrashTri_Common.Cache_Music(nsf);
                }
                else
                {
                    if (Crash3_Props_Misc.Option_AllCratesWumpa.Enabled) Crash3_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                    if (Crash3_Props_Main.Option_RandWarpRoom.Enabled) Crash3_Mods.Mod_RandomizeWarpRoom(nsf, nsd, NSF_Level, rand);
                    if (Crash3_Props_Main.Option_BackwardsLevels.Enabled || Crash3_Props_Main.Option_RandBackwardsLevels.Enabled) Crash3_Mods.Mod_BackwardsLevels(nsf, nsd, NSF_Level, Crash3_Props_Main.Option_RandBackwardsLevels.Enabled, rand);
                    if (Crash3_Props_Main.Option_CameraBigFOV.Enabled || Crash3_Props_Misc.Option_RandCameraFOV.Enabled) Crash3_Mods.Mod_CameraFOV(nsf, rand, Crash3_Props_Misc.Option_RandCameraFOV.Enabled);
                    if (Crash3_Props_Misc.Option_AllCratesBlank.Enabled) Crash3_Mods.Mod_AllWoodCrates(nsf, rand);
                    if (Crash3_Props_Misc.Option_RandCrates.Enabled) Crash3_Mods.Rand_WoodenCrates(nsf, rand, NSF_Level);
                    if (Crash3_Props_Main.Option_RandBosses.Enabled) Crash3_Mods.Mod_RandomizeBosses(nsf, nsd, NSF_Level, rand, false);
                    if (Crash3_Props_Main.Option_RandFlyingLevels.Enabled) Crash3_Mods.Mod_RandomizeFlyingLevels(nsf, nsd, NSF_Level, rand, false);
                    if (Crash3_Props_Main.Option_RandBoxCount.Enabled) Crash3_Mods.Rand_BoxCount(nsf, rand, NSF_Level);
                    if (Crash3_Props_Misc.Option_AllEnemiesMissing.Enabled) Crash3_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, false);
                    if (Crash3_Props_Main.Option_RandEnemiesAreCrates.Enabled) Crash3_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, true);
                    if (Crash3_Props_Misc.Option_AllEnemiesAreCrates.Enabled) Crash3_Mods.Mod_EnemyCrates(nsf, rand, NSF_Level, false);
                    if (Crash3_Props_Main.Option_RandEnemiesMissing.Enabled) Crash3_Mods.Mod_RemoveEnemies(nsf, rand, NSF_Level, true);
                    if (Crash3_Props_Main.Option_RandCratesMissing.Enabled) Crash3_Mods.Rand_CratesMissing(nsf, rand);
                    if (Crash3_Props_Main.Option_RandCrateContents.Enabled) Crash3_Mods.Mod_RandomCrateContents(nsf, rand);
                    if (Crash3_Props_Main.Option_RandCrateParams.Enabled) Crash3_Mods.Mod_RandomCrateParams(nsf, rand, NSF_Level);
                    if (Crash3_Props_Main.Option_RandInvisibleCrates.Enabled) Crash3_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, true);
                    if (Crash3_Props_Main.Option_InvisibleCrates.Enabled) Crash3_Mods.Mod_InvisibleCrates(nsf, rand, NSF_Level, false);
                    if (Crash3_Props_Main.Option_RemoveWarpRoomBarriers.Enabled) Crash3_Mods.Mod_RemoveBarriers(nsf, NSF_Level);
                    
                    if (Crash3_Props_Main.Option_RandWorldPalette.Enabled) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                    if (Crash3_Props_Main.Option_GreyscaleWorld.Enabled) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                    if (Crash3_Props_Main.Option_RandWorldColors.Enabled) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                    if (Crash3_Props_Main.Option_UntexturedWorld.Enabled) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                    if (Crash3_Props_Misc.Option_InvisibleWorld.Enabled) CrashTri_Common.Mod_Scenery_Invisible(nsf);
                    if (Crash3_Props_Main.Option_RandWorldTex.Enabled) CrashTri_Common.Mod_RandomizeWGEOTex(nsf, rand);
                    if (Crash3_Props_Main.Option_RandPantsColor.Enabled || Crash3_Props_Misc.Prop_PantsColor.HasChanged) Crash3_Mods.Mod_PantsColor(nsf, PantsColor);
                    if (Crash3_Props_Misc.Option_RandObjCol.Enabled) CrashTri_Common.Mod_RandomizeTGEOCol(nsf, rand);
                    if (Crash3_Props_Main.Option_AllCratesAshed.Enabled) Crash3_Mods.Mod_AshedCrates(nsf, rand, false);
                    if (Crash3_Props_Main.Option_RandCratesAshed.Enabled) Crash3_Mods.Mod_AshedCrates(nsf, rand, true);
                    if (Crash3_Props_Misc.Option_RandObjTex.Enabled) CrashTri_Common.Mod_RandomizeTGEOTex(nsf, rand);
                    if (NSF_Level != Crash3_Levels.Unknown && Crash3_Props_Main.Option_RandMusicTracks.Enabled) CrashTri_Common.Randomize_Music(nsf, rand);
                    if (Crash3_Props_Main.Option_RandSounds.Enabled) CrashTri_Common.Mod_RandomizeADIO(nsf, rand);
                    if (Crash3_Props_Main.Option_RandStreams.Enabled) CrashTri_Common.Mod_RandomizeSDIO(nsf, rand);
                    if (Crash3_Props_Main.Option_RandObjPalette.Enabled) CrashTri_Common.Mod_SwizzleObjectColors(nsf, rand);
                    if (Crash3_Props_Main.Option_UntexturedObj.Enabled) CrashTri_Common.Mod_RemoveTGEOTex(nsf, rand);
                    if (Crash3_Props_Misc.Option_UncoloredObj.Enabled) CrashTri_Common.Mod_RemoveObjectColors(nsf, rand);

                    Crash3_Mods.Mod_Metadata(nsf, nsd, NSF_Level, GameRegion.Region);
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
