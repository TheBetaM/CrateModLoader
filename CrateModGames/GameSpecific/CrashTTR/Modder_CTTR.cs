using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
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
        public override void StartModProcess()
        {
            ProcessBusy = true;

            ModProcess();
        }

        public async void ModProcess()
        {
            string basePath = ConsolePipeline.ExtractedPath;

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

            //Mods
            FindFiles(new Parser_GOD(this), new Parser_GOD(this));
            await StartNewPass();

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

            ProcessBusy = false;
        }

        public async Task ExtractRCFAsync(string Path, int i, List<RCF_Manager> Managers)
        { 
            await Managers[i].ExtractAsync(this, Path, Path.Substring(0, Path.Length - 4) + @"\");
        }
        public async Task PackRCFAsync(string Path, int i, List<RCF_Manager> Managers)
        {
            await Managers[i].PackAsync(Path, Path.Substring(0, Path.Length - 4) + @"\");
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
    }
}
