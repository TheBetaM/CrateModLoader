using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CTTR;
//CTTR API by NeoKesha
//Version number, seed and options are displayed in the Credits accessible from the main menu.

namespace CrateModLoader
{
    class Modder_CTTR
    {

        public string path_RCF_default = "";
        public string path_RCF_common = "";
        public string path_RCF_frontend = "";
        public string path_executable = "";
        public string path_RCF_onfoot0 = "";
        public string path_RCF_onfoot1 = "";
        public string path_RCF_onfoot2 = "";
        public string path_RCF_onfoot3 = "";
        public string path_RCF_onfoot4 = ""; // PSP
        public string path_RCF_onfoot5 = "";
        public string path_RCF_onfoot6 = "";
        public string path_RCF_onfoot7 = ""; // GC
        public string path_RCF_advent1 = "";
        public string path_RCF_advent2 = "";
        public string path_RCF_advent3 = "";
        public string path_RCF_adventa = "";
        public string path_RCF_dino1 = "";
        public string path_RCF_dino2 = "";
        public string path_RCF_dino3 = "";
        public string path_RCF_dinoa = "";
        public string path_RCF_egypt1 = "";
        public string path_RCF_egypt2 = "";
        public string path_RCF_egypt3 = "";
        public string path_RCF_egypta = ""; // PSP/PS2
        public string path_RCF_fairy1 = "";
        public string path_RCF_fairy2 = "";
        public string path_RCF_fairy3 = "";
        public string path_RCF_fairys = "";
        public string path_RCF_solar1 = "";
        public string path_RCF_solar2 = "";
        public string path_RCF_solar3 = "";
        public string path_RCF_solars = "";
        public string path_RCF_0 = ""; // XBOX
        public string path_RCF_1 = ""; // XBOX
        public string path_RCF_2 = ""; // XBOX
        public string path_RCF_3 = ""; // XBOX
        public string path_RCF_4 = ""; // XBOX
        public string path_RCF_5 = ""; // XBOX
        public string path_RCF_6 = ""; // XBOX
        public string path_RCF_sound = "";
        public string path_RCF_english = "";

        public Random randState = new Random();
        public string[] modOptions = { "Randomize playable characters", "Randomize hub entrances", "Randomize tracks", "Randomize minigames", "Randomize missions", "Add unused cutscenes", "Prevent sequence breaks" };
        public bool CTTR_rand_hubs = false; // todo, startup.god or genericobjectives.god (and maybe unlock message in missionobjectives_midway?)
        public bool CTTR_rand_tracks = false; // todo, genericobjectives + missionobjectives_x
        public bool CTTR_rand_minigames = false; // todo, genericobjectives.god
        public bool CTTR_rand_missions = false; // todo, genericobjectives, missionobjectives_x, level NIS+NPC
        public bool CTTR_rand_characters = false;
        public bool CTTR_add_unused_cutscenes = false; // todo, NIS + an objective?
        public bool CTTR_add_sequence_break_checks = false; // todo, genericobjectives
        public enum CTTR_Options
        {
            RandomizeCharacters = 0,
            RandomizeHubs = 1,
            RandomizeTracks = 2,
            RandomizeMinigames = 3,
            RandomizeMissions = 4,
            AddUnusedCutscenes = 5,
            PreventSequenceBreaks = 6,
        }

        public void OptionChanged(int option,bool value)
        {
            switch ((CTTR_Options)option)
            {
                case CTTR_Options.RandomizeCharacters:
                    CTTR_rand_characters = value;
                    break;
                case CTTR_Options.AddUnusedCutscenes:
                    CTTR_add_unused_cutscenes = value;
                    break;
                case CTTR_Options.PreventSequenceBreaks:
                    CTTR_add_sequence_break_checks = value;
                    break;
                case CTTR_Options.RandomizeHubs:
                    CTTR_rand_hubs = value;
                    break;
                case CTTR_Options.RandomizeMinigames:
                    CTTR_rand_minigames = value;
                    break;
                case CTTR_Options.RandomizeMissions:
                    CTTR_rand_missions = value;
                    break;
                case CTTR_Options.RandomizeTracks:
                    CTTR_rand_tracks = value;
                    break;
            }
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
        }

        public void SetPaths(ModLoader.ConsoleMode console, string exec_name = "")
        {
            if (console == ModLoader.ConsoleMode.PS2)
            {
                path_executable = exec_name;
                path_RCF_default = @"ADEFAULT\DEFAULT.RCF";
                path_RCF_advent1 = @"ADVENT\ADVENT1.RCF";
                path_RCF_advent2 = @"ADVENT\ADVENT2.RCF";
                path_RCF_advent3 = @"ADVENT\ADVENT3.RCF";
                path_RCF_adventa = @"ADVENT\ADVENTA.RCF";
                path_RCF_common = @"COMMON\COMMON.RCF";
                path_RCF_dino1 = @"DINO\DINO1.RCF";
                path_RCF_dino2 = @"DINO\DINO2.RCF";
                path_RCF_dino3 = @"DINO\DINO3.RCF";
                path_RCF_dinoa = @"DINO\DINOA.RCF";
                path_RCF_egypt1 = @"EGYPT\EGYPT1.RCF";
                path_RCF_egypt2 = @"EGYPT\EGYPT2.RCF";
                path_RCF_egypt3 = @"EGYPT\EGYPT3.RCF";
                path_RCF_egypta = @"EGYPT\EGYPTA.RCF";
                path_RCF_english = @"ENGLISH.RCF";
                path_RCF_fairy1 = @"FAIRY\FAIRY1.RCF";
                path_RCF_fairy2 = @"FAIRY\FAIRY2.RCF";
                path_RCF_fairy3 = @"FAIRY\FAIRY3.RCF";
                path_RCF_fairys = @"FAIRY\FAIRYS.RCF";
                path_RCF_frontend = @"COMMON\FRONTEND.RCF";
                path_RCF_solar1 = @"SOLAR\SOLAR1.RCF";
                path_RCF_solar2 = @"SOLAR\SOLAR2.RCF";
                path_RCF_solar3 = @"SOLAR\SOLAR3.RCF";
                path_RCF_solars = @"SOLAR\SOLARS.RCF";
                path_RCF_onfoot0 = @"ONFOOT\ONFOOT.RCF";
                path_RCF_onfoot1 = @"ONFOOT\ONFOOT1.RCF";
                path_RCF_onfoot2 = @"ONFOOT\ONFOOT2.RCF";
                path_RCF_onfoot3 = @"ONFOOT\ONFOOT3.RCF";
                path_RCF_onfoot5 = @"ONFOOT\ONFOOT5.RCF";
                path_RCF_onfoot6 = @"ONFOOT\ONFOOT6.RCF";
            }
            else if (console == ModLoader.ConsoleMode.PSP)
            {
                path_executable = @"PSP_GAME\SYSDIR\BOOT.BIN";
                path_RCF_default = @"PSP_GAME\USRDIR\adefault\default.rcf";
                path_RCF_advent1 = @"PSP_GAME\USRDIR\advent\advent1.rcf";
                path_RCF_advent2 = @"PSP_GAME\USRDIR\advent\advent2.rcf";
                path_RCF_advent3 = @"PSP_GAME\USRDIR\advent\advent3.rcf";
                path_RCF_adventa = @"PSP_GAME\USRDIR\advent\adventa.rcf";
                path_RCF_common = @"PSP_GAME\USRDIR\common\common.rcf";
                path_RCF_dino1 = @"PSP_GAME\USRDIR\dino\dino1.rcf";
                path_RCF_dino2 = @"PSP_GAME\USRDIR\dino\dino2.rcf";
                path_RCF_dino3 = @"PSP_GAME\USRDIR\dino\dino3.rcf";
                path_RCF_dinoa = @"PSP_GAME\USRDIR\dino\dinoa.rcf";
                path_RCF_egypt1 = @"PSP_GAME\USRDIR\egypt\egypt1.rcf";
                path_RCF_egypt2 = @"PSP_GAME\USRDIR\egypt\egypt2.rcf";
                path_RCF_egypt3 = @"PSP_GAME\USRDIR\egypt\egypt3.rcf";
                path_RCF_egypta = @"PSP_GAME\USRDIR\egypt\egypta.rcf";
                path_RCF_english = @"PSP_GAME\USRDIR\english.rcf";
                path_RCF_fairy1 = @"PSP_GAME\USRDIR\fairy\fairy1.rcf";
                path_RCF_fairy2 = @"PSP_GAME\USRDIR\fairy\fairy2.rcf";
                path_RCF_fairy3 = @"PSP_GAME\USRDIR\fairy\fairy3.rcf";
                path_RCF_fairys = @"PSP_GAME\USRDIR\fairy\fairys.rcf";
                path_RCF_frontend = @"PSP_GAME\USRDIR\common\frontend.rcf";
                path_RCF_solar1 = @"PSP_GAME\USRDIR\solar\solar1.rcf";
                path_RCF_solar2 = @"PSP_GAME\USRDIR\solar\solar2.rcf";
                path_RCF_solar3 = @"PSP_GAME\USRDIR\solar\solar3.rcf";
                path_RCF_solars = @"PSP_GAME\USRDIR\solar\solars.rcf";
                path_RCF_onfoot0 = @"PSP_GAME\USRDIR\onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"PSP_GAME\USRDIR\onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"PSP_GAME\USRDIR\onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"PSP_GAME\USRDIR\onfoot\onfoot3.rcf";
                path_RCF_onfoot4 = @"PSP_GAME\USRDIR\onfoot\onfoot4.rcf";
                path_RCF_onfoot5 = @"PSP_GAME\USRDIR\onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"PSP_GAME\USRDIR\onfoot\onfoot6.rcf";
            }
            else if (console == ModLoader.ConsoleMode.GCN)
            {
                path_executable = @"sys\main.dol";
                path_RCF_default = @"root\adefault\default.rcf";
                path_RCF_advent1 = @"root\advent\advent1.rcf";
                path_RCF_advent2 = @"root\advent\advent2.rcf";
                path_RCF_advent3 = @"root\advent\advent3.rcf";
                path_RCF_adventa = @"root\advent\adventa.rcf";
                path_RCF_common = @"root\common\common.rcf";
                path_RCF_dino1 = @"root\dino\dino1.rcf";
                path_RCF_dino2 = @"root\dino\dino2.rcf";
                path_RCF_dino3 = @"root\dino\dino3.rcf";
                path_RCF_dinoa = @"root\dino\dinoa.rcf";
                path_RCF_egypt1 = @"root\egypt\egypt1.rcf";
                path_RCF_egypt2 = @"root\egypt\egypt2.rcf";
                path_RCF_egypt3 = @"root\egypt\egypt3.rcf";
                path_RCF_english = @"root\english.rcf";
                path_RCF_fairy1 = @"root\fairy\fairy1.rcf";
                path_RCF_fairy2 = @"root\fairy\fairy2.rcf";
                path_RCF_fairy3 = @"root\fairy\fairy3.rcf";
                path_RCF_fairys = @"root\fairy\fairys.rcf";
                path_RCF_frontend = @"root\common\frontend.rcf";
                path_RCF_solar1 = @"root\solar\solar1.rcf";
                path_RCF_solar2 = @"root\solar\solar2.rcf";
                path_RCF_solar3 = @"root\solar\solar3.rcf";
                path_RCF_solars = @"root\solar\solars.rcf";
                path_RCF_onfoot0 = @"root\onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"root\onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"root\onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"root\onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"root\onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"root\onfoot\onfoot6.rcf";
                path_RCF_onfoot7 = @"root\onfoot\onfoot7.rcf";
            }
        }

        public void StartModProcess()
        {
            SetPaths(Program.ModProgram.isoType, Program.ModProgram.PS2_executable_name);

            randState = new Random(Program.ModProgram.randoSeed);

            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend + ";1", Program.ModProgram.extractedPath + path_RCF_frontend);

            bool Editing_Credits = true;
            bool Editing_DefaultCommon = false;

            if (CTTR_rand_characters)
            {
                Editing_DefaultCommon = true;
            }

            if (Editing_DefaultCommon)
            {
                EditDefaultAndCommon();
            }

            if (Editing_Credits)
            {
                Mod_EditCredits();
            }
            

            //Fixes names for PS2
            //File.Move(Program.ModProgram.extractedPath + path_RCF_frontend, Program.ModProgram.extractedPath + path_RCF_frontend + ";1");
        }

        void EditDefaultAndCommon()
        {
            List<int> randChars = new List<int>();
            if (CTTR_rand_characters)
            {
                int maxPlayableCharacters = 2;

                List<int> possibleChars = new List<int>();
                
                for (int i = 0; i < 8; i++)
                {
                    possibleChars.Add(i);
                }
                int targetChar = -1;
                for (int i = 0; i < maxPlayableCharacters; i++)
                {
                    targetChar = possibleChars[randState.Next(0, possibleChars.Count)];
                    randChars.Add(targetChar);
                }
            }

            //Warning: The CTTR API only likes paths with \ backslashes
            string feedback = "";
            string path_extr = "";
            RCF rcf_common= new RCF();
            rcf_common.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_common);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_common.ExtractRCF(ref feedback, path_extr);

            if (CTTR_rand_characters)
            {
                Randomize_Characters(path_extr, ref rcf_common, ref randChars);
            }

            rcf_common.Recalculate();
            rcf_common.Pack(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_common + "1", ref feedback);

            // Extraction cleanup
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_common);
            System.IO.File.Move(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_common + "1", AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_common);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }
            
            RCF rcf_default = new RCF();
            rcf_default.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_default);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_default.ExtractRCF(ref feedback, path_extr);

            if (CTTR_rand_characters)
            {
                Randomize_Characters(path_extr, ref rcf_default, ref randChars);
            }

            rcf_default.Recalculate();
            rcf_default.Pack(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_default + "1", ref feedback);

            // Extraction cleanup
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_default);
            System.IO.File.Move(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_default + "1", AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_default);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }
            
        }

        void Randomize_Characters(string path_extr, ref RCF rcf_file, ref List<int> randChars)
        {
            

            if (File.Exists(path_extr + @"design\permanent\genericobjectives.god"))
            {
                string[] startup_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\genericobjectives.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < startup_lines.Length; i++)
                {
                    LineList.Add(startup_lines[i]);
                }

                int characterList_Start = 0;
                int characterList_End = 0;
                List<string> DefaultUnlocks = new List<string>();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Objective", "UnlockDefaults", ref characterList_Start, ref characterList_End, ref DefaultUnlocks))
                {
                    DefaultUnlocks.Clear();
                    DefaultUnlocks.Add("this.SetName(\"UnlockDefaults\")");
                    for (int i = 0; i < randChars.Count; i++)
                    {
                        DefaultUnlocks.Add("this.AddAction_UnlockCar(\"" + CTTR_Data.DriverNames[randChars[i]] + "\",1)");
                    }
                }
                CTTR_Data.LUA_SaveObject(ref LineList, "Objective", "UnlockDefaults", ref DefaultUnlocks);

                startup_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    startup_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\genericobjectives.god", startup_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\genericobjectives.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\genericobjectives.god";
                        break;
                    }
                }
            }
            if (File.Exists(path_extr + @"design\permanent\skins.god"))
            {
                string[] skins_lines = System.IO.File.ReadAllLines(path_extr + @"design\permanent\skins.god");
                List<string> LineList = new List<string>();
                for (int i = 0; i < skins_lines.Length; i++)
                {
                    LineList.Add(skins_lines[i]);
                }

                int skin_Start = 0;
                int skin_End = 0;
                List<string> SkinObj = new List<string>();
                if (CTTR_Data.LUA_LoadObject(ref LineList, "Skin", "CrashDefault", ref skin_Start, ref skin_End, ref SkinObj))
                {
                    for (int i = 0; i < SkinObj.Count; i++)
                    {
                        if (SkinObj[i] == "this.SetOnfootSkinFilename(\"crash_onfoot_model\")")
                        {
                            SkinObj[i] = "this.SetOnfootSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                        else if (SkinObj[i] == "this.SetSpinSkinFilename(\"crash_spin_model\")")
                        {
                            SkinObj[i] = "this.SetSpinSkinFilename(\"" + CTTR_Data.DriverNames[randChars[0]] + "_onfoot_model\")";
                        }
                    }
                }
                CTTR_Data.LUA_SaveObject(ref LineList, "Skin", "CrashDefault", ref SkinObj);

                skins_lines = new string[LineList.Count];
                for (int i = 0; i < LineList.Count; i++)
                {
                    skins_lines[i] = LineList[i];
                }

                System.IO.File.WriteAllLines(path_extr + @"design\permanent\skins.god", skins_lines);

                for (int i = 0; i < rcf_file.Header.T2File.Length; i++)
                {
                    if (rcf_file.Header.T2File[i].Name == @"design\permanent\skins.god")
                    {
                        rcf_file.Header.T2File[i].External = path_extr + @"design\permanent\skins.god";
                        break;
                    }
                }
            }
        }

        void Mod_EditCredits()
        {
            //Warning: The CTTR API only likes paths with \ backslashes
            string feedback = "";
            string path_extr = "";
            RCF rcf_frontend = new RCF();
            rcf_frontend.OpenRCF(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            path_extr = AppDomain.CurrentDomain.BaseDirectory + @"temp\cml_extr\";
            Directory.CreateDirectory(path_extr);
            rcf_frontend.ExtractRCF(ref feedback, path_extr);

            // Proof of concept mod replacing attract movie with first gem cutscene
            string[] frontend_lines = System.IO.File.ReadAllLines(path_extr + @"design\levels\common\frontend.god");
            frontend_lines[64] = "PlayMovie(\"art/fmv/wny_midway_statue\",\"any\",\"any\",true)";

            // Editing credits to add CML metadata
            for (int i = 0; i < frontend_lines.Length; i++)
            {
                if (frontend_lines[i] == "screen.AddLine(\"\",0,\"\")")
                {
                    frontend_lines[i + 1] = "screen.AddLine(\"Crate Mod Loader " + Program.ModProgram.releaseVersionString + "\",0,\"\")";
                    frontend_lines[i + 2] = "screen.AddLine(\"Seed: " + Program.ModProgram.randoSeed + "\",0,\"\")";
                    frontend_lines[i + 3] = "screen.AddLine(\"Options: " + Program.ModProgram.optionsSelectedString + "\",0,\"\")";
                    frontend_lines[i + 4] = "screen.AddLineSpecial(\"creditscttr\",0,104,104,255,1.2,true)";
                    break;
                }
            }

            System.IO.File.WriteAllLines(path_extr + @"design\levels\common\frontend.god", frontend_lines);

            for (int i = 0; i < rcf_frontend.Header.T2File.Length; i++)
            {
                if (rcf_frontend.Header.T2File[i].Name == @"design\levels\common\frontend.god")
                {
                    rcf_frontend.Header.T2File[i].External = path_extr + @"design\levels\common\frontend.god";
                    //Console.WriteLine("external " + rcf_frontend.Header.T2File[i].External);
                    break;
                }
            }

            rcf_frontend.Recalculate();
            rcf_frontend.Pack(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend + "1", ref feedback);

            // Extraction cleanup
            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            System.IO.File.Move(AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend + "1", AppDomain.CurrentDomain.BaseDirectory + @"temp\" + path_RCF_frontend);
            if (Directory.Exists(path_extr))
            {
                DirectoryInfo di = new DirectoryInfo(path_extr);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(path_extr);
            }
        }
    }
}
