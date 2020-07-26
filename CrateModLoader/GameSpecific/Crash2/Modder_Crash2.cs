using Crash;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific;
using CrateModLoader.GameSpecific.Crash2;
//Crash 2 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)

namespace CrateModLoader
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


        public Modder_Crash2()
        {
            Game = new Game()
            {
                Name = "Crash Bandicoot 2: Cortex Strikes Back",
                ShortName = "Crash2",
                Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS1
                },
                API_Credit = "API by chekwob and ManDude",
                API_Link = "https://github.com/cbhacks/CrashEdit",
                Icon = Properties.Resources.icon_crash2,
                ModMenuEnabled = false,
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

            AddOption(RandomizeCratesIntoWood, new ModOption("All Crates Are Blank"));
            AddOption(TurnCratesIntoWumpa, new ModOption("All Crates Are Wumpa"));
            //AddOption(RandomizeLevelOrder, new ModOption("Randomize Level Order"));
            AddOption(RandomizeADIO, new ModOption("Randomize Sound Effects"));
            //AddOption(ZoneCloseCamera, new ModOption("Closer Camera"));
            AddOption(SceneryRainbow, new ModOption("Randomize World Colors"));
            AddOption(SceneryColorSwizzle, new ModOption("Randomize World Palette"));
            AddOption(SceneryGreyscale, new ModOption("Greyscale World"));
            AddOption(SceneryUntextured, new ModOption("Untextured World"));

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

            if (GetOption(RandomizeLevelOrder)) Randomize_LevelOrder(rand);

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

                if (GetOption(RandomizeADIO)) Mod_RandomizeADIO(nsf, nsd, rand);
                if (GetOption(RandomizeCratesIntoWood)) Crash2_Mods.Mod_RandomWoodCrates(nsf, rand);
                if (GetOption(TurnCratesIntoWumpa)) Crash2_Mods.Mod_TurnCratesIntoWumpa(nsf, rand);
                if (GetOption(SceneryColorSwizzle)) CrashTri_Common.Mod_Scenery_Swizzle(nsf, rand);
                if (GetOption(SceneryGreyscale)) CrashTri_Common.Mod_Scenery_Greyscale(nsf);
                if (GetOption(SceneryRainbow)) CrashTri_Common.Mod_Scenery_Rainbow(nsf, rand);
                if (GetOption(SceneryUntextured)) CrashTri_Common.Mod_Scenery_Untextured(nsf);
                if (GetOption(ZoneCloseCamera)) CrashTri_Common.Mod_Camera_Closeup(nsf);

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

        public enum Crash2_Levels
        {
            L01_TurleWoords = 17,
            L02_SnowGo = 3,
            L03_HangEight = 13,
            L04_ThePits = 18,
            L05_CrashDash = 12,
            L06_SnowBiz = 6,
            L07_AirCrash = 19,
            L08_BearIt = 16,
            L09_CrashCrush = 15,
            L10_TheEelDeal = 22,
            L11_PlantFood = 20,
            L12_SewerOrLater = 0,
            L13_BearDown = 21,
            L14_RoadToRuin = 10,
            L15_UnBearable = 11,
            L16_HanginOut = 2,
            L17_DigginIt = 9,
            L18_ColdHardCrash = 8,
            L19_Ruination = 4,
            L20_BeeHaving = 23,
            L21_PistonItAway = 5,
            L22_RockIt = 7,
            L23_NightFight = 1,
            L24_PackAttack = 14,
            L25_SpacedOut = 25,
            L26_TotallyBear = 24,
            L27_TotallyFly = 26,
            B01_RipperRoo = 27,
            B02_KomodoBros = 28,
            B03_TinyTiger = 29,
            B04_NGin = 30,
            B05_Cortex = 31,
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
            "03",
            "06",
            "07",
            "08",
            "09",
        };

        internal void Randomize_LevelOrder(Random rand)
        {
            DirectoryInfo di = new DirectoryInfo(ModLoaderGlobals.ExtractedPath);

            List<int> LevelsToGo = new List<int>();

            for (int i = 0; i < Crash2_LevelFileNames.Length - 7; i++)
            {
                File.Move(di.FullName + "S" + Crash2_LevelFileNames[i].Substring(0, 1) + @"\S00000" + Crash2_LevelFileNames[i] + ".NSD", di.FullName + "level" + i + ".NSD");
                File.Move(di.FullName + "S" + Crash2_LevelFileNames[i].Substring(0, 1) + @"\S00000" + Crash2_LevelFileNames[i] + ".NSF", di.FullName + "level" + i + ".NSF");
                LevelsToGo.Add(i);
            }

            int id = 0;
            while (LevelsToGo.Count > 0)
            {
                int target = rand.Next(LevelsToGo.Count);
                int i = LevelsToGo[target];
                File.Move(di.FullName + "level" + i + ".NSD", di.FullName + "S" + Crash2_LevelFileNames[id].Substring(0, 1) + @"\S00000" + Crash2_LevelFileNames[id] + ".NSD");
                File.Move(di.FullName + "level" + i + ".NSF", di.FullName + "S" + Crash2_LevelFileNames[id].Substring(0, 1) + @"\S00000" + Crash2_LevelFileNames[id] + ".NSF");
                LevelsToGo.RemoveAt(target);
                id++;
            }

        }

    }
}
