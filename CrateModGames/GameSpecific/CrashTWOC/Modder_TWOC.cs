using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
/*
 * Mod Passes:
 * TWOC_GenericMod -> extraction path + console type
 * TWOC_File_AI -> AI files
 * TWOC_File_CRT -> Crate files
 * TWOC_File_WMP -> Wumpa files
 */

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public sealed class Modder_TWOC : Modder
    {
        private bool MainBusy = false;
        private int CurrentPass = 0;
        private float PassPercentMod = 49f;
        private int PassPercentAdd = 1;

        public Modder_TWOC() { }

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
            if (ConsolePipeline.Metadata.Console == ConsoleMode.GCN)
            {
                // rebuilding the GC version makes it not boot for some reason...
                return;
            }

            MainBusy = true;

            //Generic mods
            StartCachePass(ConsolePipeline.ExtractedPath);
            StartModPass(ConsolePipeline.ExtractedPath);

            bool Editing_AI = CheckModsForAI();
            bool Editing_CRT = CheckModsForCRT();
            bool Editing_WMP = CheckModsForWMP();

            List<FileInfo> Files_AI = new List<FileInfo>();
            List<FileInfo> Files_CRT = new List<FileInfo>();
            List<FileInfo> Files_WMP = new List<FileInfo>();
            DirectoryInfo adi = new DirectoryInfo(ConsolePipeline.ExtractedPath);
            Recursive_LoadFiles(adi, ref Files_AI, ref Files_CRT, ref Files_WMP);
            PassCount = 0;
            if (Editing_AI)
            {
                PassCount += Files_AI.Count;
            }
            if (Editing_CRT)
            {
                PassCount += Files_CRT.Count;
            }
            if (Editing_WMP)
            {
                PassCount += Files_WMP.Count;
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

                if (Editing_AI)
                {
                    foreach (FileInfo File in Files_AI)
                    {
                        editTaskList.Add(Edit_AI(File));
                    }
                }
                if (Editing_CRT)
                {
                    foreach (FileInfo File in Files_CRT)
                    {
                        editTaskList.Add(Edit_CRT(File));
                    }
                }
                if (Editing_WMP)
                {
                    foreach (FileInfo File in Files_WMP)
                    {
                        editTaskList.Add(Edit_WMP(File));
                    }
                }

                await Task.WhenAll(editTaskList);

                CurrentPass++;
                PassBusy = false;
            }

            MainBusy = false;
        }

        void Recursive_LoadFiles(DirectoryInfo di, ref List<FileInfo> Files_AI, ref List<FileInfo> Files_CRT, ref List<FileInfo> Files_WMP)
        {
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                Recursive_LoadFiles(dir, ref Files_AI, ref Files_CRT, ref Files_WMP);
            }
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.Extension.ToLower() == ".ai")
                    Files_AI.Add(file);
                if (file.Extension.ToLower() == ".crt")
                    Files_CRT.Add(file);
                if (file.Extension.ToLower() == ".wmp")
                    Files_WMP.Add(file);
            }
        }

        bool CheckModsForAI()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<TWOC_File_AI>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForWMP()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<TWOC_File_WMP>)
                {
                    return true;
                }
            }
            return false;
        }
        bool CheckModsForCRT()
        {
            foreach (ModPropertyBase Prop in ActiveProps)
            {
                if (Prop.TargetMod is ModStruct<TWOC_File_CRT>)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task Edit_AI(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {

                TWOC_File_AI ai = new TWOC_File_AI();
                ai.Load(file.FullName);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(ai);
                        break;
                    default:
                    case 1:
                        StartModPass(ai);
                        break;
                }

                ai.Save(file.FullName);

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        private async Task Edit_CRT(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {

                TWOC_File_CRT crt = new TWOC_File_CRT();
                crt.Load(file.FullName);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(crt);
                        break;
                    default:
                    case 1:
                        StartModPass(crt);
                        break;
                }

                crt.Save(file.FullName);

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }

        private async Task Edit_WMP(FileInfo file)
        {
            //Console.WriteLine("Editing: " + path);

            await Task.Run(
            () =>
            {

                TWOC_File_WMP wmp = new TWOC_File_WMP();
                wmp.Load(file.FullName);

                switch (CurrentPass)
                {
                    case 0:
                        StartCachePass(wmp);
                        break;
                    default:
                    case 1:
                        StartModPass(wmp);
                        break;
                }

                wmp.Save(file.FullName);

            }
            );

            PassIterator++;
            PassPercent = (int)((PassIterator / (float)PassCount) * PassPercentMod) + PassPercentAdd;
        }



    }


}
