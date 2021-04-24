using System;
using System.Collections.Generic;
using System.IO;
using CTRFramework.Big;
using CTRFramework.Lang;
using CTRFramework.Shared;
using CTRFramework;
using CTRFramework.Vram;
using System.Threading.Tasks;

//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers:
 * 1: BIGFILE.BIG contents
 * Mod Passes:
 * LNG -> language files
 * CTR -> model files
 * LEV -> level files
 * string -> BIGFILE contents path
 */

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {
        private bool MainBusy = false;
        private int CurrentPass = 0;
        private float PassPercentMod = 49f;
        private int PassPercentAdd = 1;

        public Modder_CTR() { }

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

            string path_Bigfile = "BIGFILE.BIG";
            string path_txt = "BIGFILE.TXT";
            string basePath = ConsolePipeline.ExtractedPath;
            string path_extr = ConsolePipeline.ExtractedPath + @"BIGFILE\";

            UpdateProcessMessage("Extracting BIGFILE.BIG...", 5);

            try
            {
                BigFile big = BigFile.FromFile(Path.Combine(basePath, path_Bigfile));
                big.Extract(path_extr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
            UpdateProcessMessage("Installing Mod Crates: Layer 1...", 6);
            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            bool Editing_LEV = CheckModsForLEV();
            bool Editing_CTR = CheckModsForCTR();

            List<FileInfo> Files_LNG = new List<FileInfo>();
            List<FileInfo> Files_CTR = new List<FileInfo>();
            List<FileInfo> Files_LEV = new List<FileInfo>();
            DirectoryInfo adi = new DirectoryInfo(path_extr);
            foreach (DirectoryInfo dir in adi.EnumerateDirectories())
            {
                Recursive_LoadFiles(dir, ref Files_LNG, ref Files_CTR, ref Files_LEV);
            }
            PassCount = Files_LNG.Count;
            if (Editing_CTR)
            {
                PassCount += Files_CTR.Count;
            }
            if (Editing_LEV)
            {
                PassCount += Files_LEV.Count;
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
                    PassPercentMod = 39f;
                    PassPercentAdd = 10;
                    UpdateProcessMessage("Cache Pass", 10);
                    BeforeCachePass();
                }
                else if (CurrentPass == 1)
                {
                    if (NeedsCache)
                    {
                        PassPercentMod = 43f;
                        PassPercentAdd = 50;
                        UpdateProcessMessage("Mod Pass", 50);
                    }
                    else
                    {
                        PassPercentMod = 83f;
                        UpdateProcessMessage("Mod Pass", 10);
                    }

                    BeforeModPass();
                }

                IList<Task> editTaskList = new List<Task>();

                foreach (FileInfo File in Files_LNG)
                {
                    editTaskList.Add(Edit_LNG(File));
                }
                if (Editing_CTR)
                {
                    foreach (FileInfo File in Files_CTR)
                    {
                        editTaskList.Add(Edit_CTR(File));
                    }
                }
                if (Editing_LEV)
                {
                    foreach (FileInfo File in Files_LEV)
                    {
                        editTaskList.Add(Edit_LEV(File));
                    }
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            File.Delete(Path.Combine(basePath, path_Bigfile));
            File.Move(ModLoaderGlobals.BaseDirectory + ".txt", Path.Combine(basePath, path_txt));
            Directory.Move(path_extr, Path.Combine(ModLoaderGlobals.BaseDirectory, @"BIGFILE\"));

            UpdateProcessMessage("Building BIGFILE.BIG...", 95);

            try
            {
                BigFile big = BigFile.FromFile(Path.Combine(basePath, path_txt));
                big.Save(Path.Combine(basePath, path_Bigfile));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            UpdateProcessMessage("Removing temporary files...", 98);
            Directory.Move(Path.Combine(ModLoaderGlobals.BaseDirectory, @"BIGFILE\"), path_extr);
            File.Delete(Path.Combine(basePath, path_txt));

            // Extraction cleanup
            if (Directory.Exists(basePath + @"BIGFILE\"))
            {
                DirectoryInfo di = new DirectoryInfo(basePath + @"BIGFILE\");

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(basePath + @"BIGFILE\");
            }

            MainBusy = false;
        }

        void Recursive_LoadFiles(DirectoryInfo di, ref List<FileInfo> Files_LNG, ref List<FileInfo> Files_CTR, ref List<FileInfo> Files_LEV)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadFiles(dir, ref Files_LNG, ref Files_CTR, ref Files_LEV);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".lng")
                    Files_LNG.Add(file);
                if (file.Extension.ToLower() == ".lev")
                    Files_LEV.Add(file);
                if (file.Extension.ToLower() == ".ctr")
                    Files_CTR.Add(file);
            }
        }

        bool CheckModsForLEV()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<Scene>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForCTR()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<CtrModel>)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task Edit_LNG(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {

                LNG lng = LNG.FromFile(file.FullName);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(lng);
                        break;
                    default:
                    case 1:
                        StartModPass(lng);
                        break;
                }

                lng.Save(file.FullName);

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        private async Task Edit_LEV(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);
            bool skip = false;

            await Task.Run(
            () =>
            {
                Scene lev = null;
                try
                {
                    lev = Scene.FromFile(file.FullName);
                }
                catch
                {
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

                    SaveLEV(lev, file.FullName);
                }

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        private async Task Edit_CTR(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);
            bool skip = false;

            await Task.Run(
            () =>
            {
                CtrModel model = null;
                try
                {
                    model = CtrModel.FromFile(file.FullName);
                }
                catch
                {
                    skip = true;
                }


                if (!skip)
                {
                    switch (CurrentPass)
                    {
                        case 0:
                            StartCachePass(model);
                            break;
                        default:
                        case 1:
                            StartModPass(model);
                            break;
                    }

                    model.Save(file.FullName.Substring(0, file.FullName.Length - file.Name.Length), file.Name);
                }

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        // From CTRFramework
        private void SaveLEV(Scene scn, string path)
        {
            using (BinaryWriterEx bw = new BinaryWriterEx(File.OpenWrite(path)))
            {

                bw.Jump(4);

                scn.header.Write(bw);

                bw.Jump(scn.header.ptrRestartPts + 4);

                foreach (Pose pa in scn.restartPts)
                    pa.Write(bw);

                bw.Jump(scn.header.ptrInstances + 4);

                foreach (PickupHeader ph in scn.pickups)
                    ph.Write(bw);


                bw.Jump(scn.mesh.ptrVertices + 4);

                foreach (Vertex v in scn.verts)
                    v.Write(bw);


                bw.Jump(scn.mesh.ptrQuadBlocks + 4);

                foreach (QuadBlock qb in scn.quads)
                    qb.Write(bw);

                bw.Jump(scn.header.ptrVcolAnim + 4);

                foreach (VertexAnim vc in scn.vertanims)
                    vc.Write(bw);

                bw.Jump(scn.mesh.ptrVisData + 4);

                foreach (VisData v in scn.visdata)
                    v.Write(bw);

                bw.Jump(scn.header.ptrAiNav + 4);
                scn.nav.Write(bw);

            }
        }
    }
}
