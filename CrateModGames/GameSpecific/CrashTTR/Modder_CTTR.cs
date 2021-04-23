using System;
using System.Collections.Generic;
//RCF API by NeoKesha
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
/* 
 * Mod Layers:
 * 1: All .RCF file contents (only replace files)
 * Mod Passes:
 * string -> extraction path
 */

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public sealed class Modder_CTTR : Modder
    {
        internal string path_RCF_default = "";
        internal string path_RCF_common = "";
        internal string path_RCF_frontend = "";
        //internal string path_executable = "";
        internal string path_RCF_onfoot0 = "";
        internal string path_RCF_onfoot1 = "";
        internal string path_RCF_onfoot2 = "";
        internal string path_RCF_onfoot3 = "";
        internal string path_RCF_onfoot4 = ""; // PSP
        internal string path_RCF_onfoot5 = "";
        internal string path_RCF_onfoot6 = "";
        internal string path_RCF_onfoot7 = ""; // GC
        internal string path_RCF_advent1 = "";
        internal string path_RCF_advent2 = "";
        internal string path_RCF_advent3 = "";
        internal string path_RCF_adventa = "";
        internal string path_RCF_dino1 = "";
        internal string path_RCF_dino2 = "";
        internal string path_RCF_dino3 = "";
        internal string path_RCF_dinoa = "";
        internal string path_RCF_egypt1 = "";
        internal string path_RCF_egypt2 = "";
        internal string path_RCF_egypt3 = "";
        internal string path_RCF_egypta = ""; // PSP/PS2
        internal string path_RCF_fairy1 = "";
        internal string path_RCF_fairy2 = "";
        internal string path_RCF_fairy3 = "";
        internal string path_RCF_fairys = "";
        internal string path_RCF_solar1 = "";
        internal string path_RCF_solar2 = "";
        internal string path_RCF_solar3 = "";
        internal string path_RCF_solars = "";
        internal string path_RCF_0 = ""; // XBOX
        internal string path_RCF_1 = ""; // XBOX
        internal string path_RCF_2 = ""; // XBOX
        internal string path_RCF_3 = ""; // XBOX
        internal string path_RCF_4 = ""; // XBOX
        internal string path_RCF_5 = ""; // XBOX
        internal string path_RCF_6 = ""; // XBOX
        internal string path_RCF_sound = "";
        internal string path_RCF_english = "";
        internal string path_RCF_movies = "";
        internal string basePath = "";

        public Modder_CTTR() { }

        public void SetPaths(ConsoleMode console, string exec_name = "")
        {
            path_RCF_default = "";
            path_RCF_common = "";
            path_RCF_frontend = "";
            //path_executable = "";
            path_RCF_onfoot0 = "";
            path_RCF_onfoot1 = "";
            path_RCF_onfoot2 = "";
            path_RCF_onfoot3 = "";
            path_RCF_onfoot4 = "";
            path_RCF_onfoot5 = "";
            path_RCF_onfoot6 = "";
            path_RCF_onfoot7 = "";
            path_RCF_advent1 = "";
            path_RCF_advent2 = "";
            path_RCF_advent3 = "";
            path_RCF_adventa = "";
            path_RCF_dino1 = "";
            path_RCF_dino2 = "";
            path_RCF_dino3 = "";
            path_RCF_dinoa = "";
            path_RCF_egypt1 = "";
            path_RCF_egypt2 = "";
            path_RCF_egypt3 = "";
            path_RCF_egypta = "";
            path_RCF_fairy1 = "";
            path_RCF_fairy2 = "";
            path_RCF_fairy3 = "";
            path_RCF_fairys = "";
            path_RCF_solar1 = "";
            path_RCF_solar2 = "";
            path_RCF_solar3 = "";
            path_RCF_solars = "";
            path_RCF_0 = "";
            path_RCF_1 = "";
            path_RCF_2 = "";
            path_RCF_3 = "";
            path_RCF_4 = "";
            path_RCF_5 = "";
            path_RCF_6 = ""; 
            path_RCF_sound = "";
            path_RCF_english = "";
            path_RCF_movies = "";

            if (console == ConsoleMode.PS2)
            {
                //path_executable = exec_name;
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
                path_RCF_movies = @"MOVIES.RCF";
            }
            else if (console == ConsoleMode.PSP)
            {
                //path_executable = @"PSP_GAME\SYSDIR\BOOT.BIN";
                path_RCF_default = @"adefault\default.rcf";
                path_RCF_advent1 = @"advent\advent1.rcf";
                path_RCF_advent2 = @"advent\advent2.rcf";
                path_RCF_advent3 = @"advent\advent3.rcf";
                path_RCF_adventa = @"advent\adventa.rcf";
                path_RCF_common = @"common\common.rcf";
                path_RCF_dino1 = @"dino\dino1.rcf";
                path_RCF_dino2 = @"dino\dino2.rcf";
                path_RCF_dino3 = @"dino\dino3.rcf";
                path_RCF_dinoa = @"dino\dinoa.rcf";
                path_RCF_egypt1 = @"egypt\egypt1.rcf";
                path_RCF_egypt2 = @"egypt\egypt2.rcf";
                path_RCF_egypt3 = @"egypt\egypt3.rcf";
                path_RCF_egypta = @"egypt\egypta.rcf";
                path_RCF_english = @"english.rcf";
                path_RCF_fairy1 = @"fairy\fairy1.rcf";
                path_RCF_fairy2 = @"fairy\fairy2.rcf";
                path_RCF_fairy3 = @"fairy\fairy3.rcf";
                path_RCF_fairys = @"fairy\fairys.rcf";
                path_RCF_frontend = @"common\frontend.rcf";
                path_RCF_solar1 = @"solar\solar1.rcf";
                path_RCF_solar2 = @"solar\solar2.rcf";
                path_RCF_solar3 = @"solar\solar3.rcf";
                path_RCF_solars = @"solar\solars.rcf";
                path_RCF_onfoot0 = @"onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"onfoot\onfoot3.rcf";
                path_RCF_onfoot4 = @"onfoot\onfoot4.rcf";
                path_RCF_onfoot5 = @"onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"onfoot\onfoot6.rcf";
                path_RCF_movies = @"movies.rcf";
            }
            else if (console == ConsoleMode.GCN)
            {
                // path_executable = @"sys\main.dol";
                path_RCF_default = @"adefault\default.rcf";
                path_RCF_advent1 = @"advent\advent1.rcf";
                path_RCF_advent2 = @"advent\advent2.rcf";
                path_RCF_advent3 = @"advent\advent3.rcf";
                path_RCF_adventa = @"advent\adventa.rcf";
                path_RCF_common = @"common\common.rcf";
                path_RCF_dino1 = @"dino\dino1.rcf";
                path_RCF_dino2 = @"dino\dino2.rcf";
                path_RCF_dino3 = @"dino\dino3.rcf";
                path_RCF_dinoa = @"dino\dinoa.rcf";
                path_RCF_egypt1 = @"egypt\egypt1.rcf";
                path_RCF_egypt2 = @"egypt\egypt2.rcf";
                path_RCF_egypt3 = @"egypt\egypt3.rcf";
                path_RCF_english = @"english.rcf";
                path_RCF_fairy1 = @"fairy\fairy1.rcf";
                path_RCF_fairy2 = @"fairy\fairy2.rcf";
                path_RCF_fairy3 = @"fairy\fairy3.rcf";
                path_RCF_fairys = @"fairy\fairys.rcf";
                path_RCF_frontend = @"common\frontend.rcf";
                path_RCF_solar1 = @"solar\solar1.rcf";
                path_RCF_solar2 = @"solar\solar2.rcf";
                path_RCF_solar3 = @"solar\solar3.rcf";
                path_RCF_solars = @"solar\solars.rcf";
                path_RCF_onfoot0 = @"onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"onfoot\onfoot6.rcf";
                path_RCF_onfoot7 = @"onfoot\onfoot7.rcf";
                path_RCF_movies = @"movies.rcf";
            }
            else
            {
                //path_executable = @"default.xbe";
                path_RCF_advent1 = @"advent\advent1.rcf";
                path_RCF_advent2 = @"advent\advent2.rcf";
                path_RCF_advent3 = @"advent\advent3.rcf";
                path_RCF_adventa = @"advent\adventa.rcf";
                path_RCF_common = @"common\common.rcf";
                path_RCF_dino1 = @"dino\dino1.rcf";
                path_RCF_dino2 = @"dino\dino2.rcf";
                path_RCF_dino3 = @"dino\dino3.rcf";
                path_RCF_dinoa = @"dino\dinoa.rcf";
                path_RCF_egypt1 = @"egypt\egypt1.rcf";
                path_RCF_egypt2 = @"egypt\egypt2.rcf";
                path_RCF_egypt3 = @"egypt\egypt3.rcf";
                path_RCF_fairy1 = @"fairy\fairy1.rcf";
                path_RCF_fairy2 = @"fairy\fairy2.rcf";
                path_RCF_fairy3 = @"fairy\fairy3.rcf";
                path_RCF_fairys = @"fairy\fairys.rcf";
                path_RCF_frontend = @"common\frontend.rcf";
                path_RCF_solar1 = @"solar\solar1.rcf";
                path_RCF_solar2 = @"solar\solar2.rcf";
                path_RCF_solar3 = @"solar\solar3.rcf";
                path_RCF_solars = @"solar\solars.rcf";
                path_RCF_onfoot0 = @"onfoot\onfoot.rcf";
                path_RCF_onfoot1 = @"onfoot\onfoot1.rcf";
                path_RCF_onfoot2 = @"onfoot\onfoot2.rcf";
                path_RCF_onfoot3 = @"onfoot\onfoot3.rcf";
                path_RCF_onfoot5 = @"onfoot\onfoot5.rcf";
                path_RCF_onfoot6 = @"onfoot\onfoot6.rcf";
                path_RCF_0 = @"0\0.rcf";
                path_RCF_1 = @"1\1.rcf";
                path_RCF_2 = @"2\2.rcf";
                path_RCF_3 = @"3\3.rcf";
                path_RCF_4 = @"4\4.rcf";
                path_RCF_5 = @"5\5.rcf";
                path_RCF_6 = @"6\6.rcf";
                path_RCF_sound = @"sound\sound.rcf";
            }
        }

        public override void StartModProcess()
        {
            SetPaths(ConsolePipeline.Metadata.Console, GameRegion.ExecName);
            basePath = ConsolePipeline.ExtractedPath;

            BeforeModPass();

            RCF_Manager.cachedRCF = null;

            List<string> all_RCF = new List<string> {
                path_RCF_default,
                path_RCF_common,
                path_RCF_frontend,
                path_RCF_onfoot0,
                path_RCF_onfoot1,
                path_RCF_onfoot2,
                path_RCF_onfoot3,
                path_RCF_onfoot4,
                path_RCF_onfoot5,
                path_RCF_onfoot6,
                path_RCF_onfoot7,
                path_RCF_advent1,
                path_RCF_advent2,
                path_RCF_advent3,
                path_RCF_adventa,
                path_RCF_dino1,
                path_RCF_dino2,
                path_RCF_dino3,
                path_RCF_dinoa,
                path_RCF_egypt1,
                path_RCF_egypt2,
                path_RCF_egypt3,
                path_RCF_egypta,
                path_RCF_fairy1,
                path_RCF_fairy2,
                path_RCF_fairy3,
                path_RCF_fairys,
                path_RCF_solar1,
                path_RCF_solar2,
                path_RCF_solar3,
                path_RCF_solars,
                path_RCF_0,
                path_RCF_1,
                path_RCF_2,
                path_RCF_3,
                path_RCF_4,
                path_RCF_5,
                path_RCF_6,
                //path_RCF_sound,
                //path_RCF_english,
                //path_RCF_movies,
            };

            if (ModCrates.HasLayerModsActive(EnabledModCrates, 1)) // these take forever, so only if they're needed
            {
                all_RCF.Add(path_RCF_movies);
                all_RCF.Add(path_RCF_english);
                all_RCF.Add(path_RCF_sound);
            }

            for (int i = 0; i < all_RCF.Count; i++)
            {
                if (all_RCF[i] != "")
                {
                    Modify_RCF(all_RCF[i]);
                }
            }

        }

        void Modify_RCF(string path)
        {
            string path_extr = basePath + @"cml_extr\";
            RCF_Manager.Extract(basePath + path, path_extr);

            ModCrates.InstallLayerMods(EnabledModCrates, path_extr, 1);

            StartModPass(path_extr);

            RCF_Manager.Pack(basePath + path, path_extr);
        }
    }
}
