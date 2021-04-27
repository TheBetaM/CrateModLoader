using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Pure3D;
//RCF API by NeoKesha and BetaM
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
/* 
 * Mod Layers:
 * 1: All .RCF file contents (only replace files)
 * Mod Passes:
 * god -> LUA scripts (plain text)
 * mid -> roads (not yet implemented)
 * p3d -> Pure3D files
 * fig -> fight trees (plain text) (not yet implemented)
 */

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public sealed class Modder_CTTR : Modder
    {
        private bool MainBusy = false;
        private int CurrentPass = 0;
        private float PassPercentMod = 22f;
        private int PassPercentAdd = 30;

        public Modder_CTTR() { }

        public override void StartModProcess()
        {
            ProcessBusy = true;

            AsyncStart();
        }

        public async void AsyncStart()
        {
            UpdateProcessMessage("Starting...", 0);

            // Mod files
            ModProcess();

            while (MainBusy || PassBusy)
            {
                await Task.Delay(100);
            }

            ProcessBusy = false;
        }

        public async void ModProcess()
        {
            MainBusy = true;

            string basePath = ConsolePipeline.ExtractedPath;

            bool Editing_P3D = CheckModsForP3D();
            bool Editing_GOD = CheckModsForGOD();
            bool Editing_FIG = CheckModsForFIG();
            bool Editing_MID = CheckModsForMID();

            //Extract all RCF
            List<FileInfo> Files_RCF = new List<FileInfo>();
            List<RCF_Manager> Managers = new List<RCF_Manager>();
            DirectoryInfo adi = new DirectoryInfo(basePath);
            Recursive_LocateRCFs(adi, ref Files_RCF);

            List<string> RCF_Paths = new List<string>();
            foreach (FileInfo file in Files_RCF)
            {
                RCF_Paths.Add(file.FullName);
            }
            PassCount = Files_RCF.Count;
            PassIterator = 0;
            PassPercent = 0;

            UpdateProcessMessage("Extracting all RCF archives...", 5);
            PassBusy = true;

            IList<Task> extractTaskList = new List<Task>();

            for (int i = 0; i < RCF_Paths.Count; i++)
            {
                Managers.Add(new RCF_Manager(this, RCF_Paths[i]));
            }
            for (int i = 0; i < RCF_Paths.Count; i++)
            {
                extractTaskList.Add(ExtractRCFAsync(RCF_Paths[i], i, Managers));
            }

            await Task.WhenAll(extractTaskList);
            PassBusy = false;
            int FileCount = PassCount;

            // Mod all RCF
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 25);
            ModCrates.InstallLayerMods(EnabledModCrates, basePath, 1, true);
            for (int i = 0; i < RCF_Paths.Count; i++)
            {
                // Only allowing overwrite at the moment
                ModCrates.InstallLayerMods(EnabledModCrates, RCF_Paths[i].Substring(0, RCF_Paths[i].Length - 4) + @"\", 1, true);
            }

            UpdateProcessMessage("Preparing Mods...", 26);
            List<FileInfo> Files_GOD = new List<FileInfo>();
            List<FileInfo> Files_MID = new List<FileInfo>();
            List<FileInfo> Files_P3D = new List<FileInfo>();
            List<FileInfo> Files_FIG = new List<FileInfo>();
            DirectoryInfo bdi = new DirectoryInfo(basePath);
            Recursive_LocateFiles(bdi, ref Files_GOD, ref Files_MID, ref Files_P3D, ref Files_FIG);
            PassIterator = 0;
            PassCount = 0;
            if (Editing_GOD)
            {
                PassCount += Files_GOD.Count;
            }
            if (Editing_P3D)
            {
                PassCount += Files_P3D.Count;
            }
            if (Editing_MID)
            {
                PassCount += Files_MID.Count;
            }
            if (Editing_FIG)
            {
                PassCount += Files_FIG.Count;
            }

            bool NeedsCache = NeedsCachePass();
            CurrentPass = 0;
            if (!NeedsCache)
            {
                PassPercentMod = 83f;
                CurrentPass++;
            }

            while (CurrentPass < 2)
            {
                PassIterator = 0;
                PassBusy = true;
                if (CurrentPass == 0)
                {
                    PassPercentMod = 22f;
                    PassPercentAdd = 30;
                    UpdateProcessMessage("Cache Pass", 30);
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    if (NeedsCache)
                    {
                        PassPercentMod = 22f;
                        PassPercentAdd = 52;
                        UpdateProcessMessage("Mod Pass", 52);
                    }
                    else
                    {
                        PassPercentMod = 44f;
                        PassPercentAdd = 30;
                        UpdateProcessMessage("Mod Pass", 30);
                    }

                    BeforeModPass();
                }

                IList<Task> editTaskList = new List<Task>();
                if (Editing_GOD)
                {
                    foreach (FileInfo File in Files_GOD)
                    {
                        editTaskList.Add(Edit_GOD(File));
                    }
                }
                if (Editing_P3D)
                {
                    foreach (FileInfo File in Files_P3D)
                    {
                        editTaskList.Add(Edit_P3D(File));
                    }
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            // Pack all RCF
            UpdateProcessMessage("Packing all RCF archives...", 75);
            PassBusy = true;
            PassIterator = 0;
            PassCount = FileCount;

            IList<Task> packTaskList = new List<Task>();
            for (int i = 0; i < RCF_Paths.Count; i++)
            {
                packTaskList.Add(PackRCFAsync(RCF_Paths[i], i, Managers));
            }

            await Task.WhenAll(packTaskList);
            PassBusy = false;

            MainBusy = false;
        }

        public async Task ExtractRCFAsync(string Path, int i, List<RCF_Manager> Managers)
        { 
            await Managers[i].ExtractAsync(this, Path, Path.Substring(0, Path.Length - 4) + @"\");
        }
        public async Task PackRCFAsync(string Path, int i, List<RCF_Manager> Managers)
        {
            await Managers[i].PackAsync(Path, Path.Substring(0, Path.Length - 4) + @"\");
        }

        private async Task Edit_GOD(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {
                GOD_File lua = new GOD_File(file);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(lua);
                        break;
                    default:
                    case 1:
                        StartModPass(lua);
                        break;
                }

                lua.Write();
            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        private async Task Edit_P3D(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);
            bool skip = false;

            await Task.Run(
            () =>
            {
                Pure3D.File lev = new Pure3D.File();
                try
                {
                    lev.Load(file.FullName);
                }
                catch
                {
                    Console.WriteLine("Failed to load: " + file.FullName);
                    skip = true;
                }

                if (!skip)
                {
                    switch (CurrentPass)
                    {
                        case 0:
                            StartCachePass(lev);
                            break;
                        default:
                        case 1:
                            StartModPass(lev);
                            break;
                    }

                    lev.Save(file.FullName);
                }

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        bool CheckModsForGOD()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<GOD_File> || Prop.TargetMod is ModStruct<Pure3D.File, GOD_File> || Prop.TargetMod is ModStruct<GOD_File, Pure3D.File>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForP3D()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<Pure3D.File> || Prop.TargetMod is ModStruct<Pure3D.File, GOD_File> || Prop.TargetMod is ModStruct<GOD_File, Pure3D.File>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForMID()
        {
            // no struct yet
            /*
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<FileInfo>)
                {
                    return true;
                }
            }
            */
            return false;
        }
        bool CheckModsForFIG()
        {
            // no struct yet
            /*
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<FileInfo>)
                {
                    return true;
                }
            }
            */
            return false;
        }


        void Recursive_LocateRCFs(DirectoryInfo di, ref List<FileInfo> Files_RCF)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LocateRCFs(dir, ref Files_RCF);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".rcf")
                    Files_RCF.Add(file);
            }
        }

        void Recursive_LocateFiles(DirectoryInfo di, ref List<FileInfo> Files_GOD, ref List<FileInfo> Files_MID, ref List<FileInfo> Files_P3D, ref List<FileInfo> Files_FIG)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LocateFiles(dir, ref Files_GOD, ref Files_MID, ref Files_P3D, ref Files_FIG);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".god")
                    Files_GOD.Add(file);
                if (file.Extension.ToLower() == ".mid")
                    Files_MID.Add(file);
                if (file.Extension.ToLower() == ".p3d")
                    Files_P3D.Add(file);
                if (file.Extension.ToLower() == ".fig")
                    Files_FIG.Add(file);
            }
        }
    }
}
