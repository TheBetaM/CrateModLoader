﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_Rand_Music : ModStruct<GenericModStruct>
    {
        List<string> MusicNames = new List<string>()
        {
            "ArcticAL",
            "AtmosphL",
            "AvalancL",
            "BanzaiBL",
            "CAndBurL",
            "CompactL",
            "CoralCaL",
            "CortexVL",
            "CraAsheL",
            "CrashteL",
            "CrateBaL",
            "CrTimeL",
            "DrainDaL",
            "DroidVoL",
            "EskimoRL",
            "FarenheL",
            "ForceL",
            "GauntleL",
            "GhostToL",
            "GoldRusL",
            "H2OhNoL",
            "IceStatL",
            "JuRumblL",
            "KnightTL",
            "RokNRolL",
            "RokRumbL",
            "SeashelL",
            "SinkingL",
            "SmokeyL",
            "SolarBoL",
            "ThemeL",
            "TornadoL",
            "TsunamiL",
            "WeatheiL",
            "WizardsL",
        };

        public override void ModPass(GenericModStruct mod)
        {
            string extrPath = mod.ExtractedPath;
            ConsoleMode console = mod.Console;

            string Music_GC_Extra = "Gauntl2";
            Random rand = GetRandom();

            if (console == ConsoleMode.PS2)
            {
                return;
            }
            
            string musicPath;
            string ext;
            
            if (console == ConsoleMode.GCN)
            {
                musicPath = extrPath + @"sfx\music\";
                ext = ".adp";
                if (!MusicNames.Contains(Music_GC_Extra))
                {
                    MusicNames.Add(Music_GC_Extra);
                }
            }
            else
            {
            
                musicPath = extrPath + @"Crashdat\sfx\Music\";
                ext = ".wav";
                if (MusicNames.Contains(Music_GC_Extra))
                {
                    MusicNames.Remove(Music_GC_Extra);
                }
            }

            int maxCount = MusicNames.Count;

            List<int> MusicToRand = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                File.Move(musicPath + MusicNames[i] + ext, musicPath + "mus" + i);
                MusicToRand.Add(i);
            }

            List<int> MusicRand = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                int r = rand.Next(MusicToRand.Count);
                MusicRand.Add(MusicToRand[r]);
                MusicToRand.RemoveAt(r);
            }

            for (int i = 0; i < maxCount; i++)
            {
                File.Move(musicPath + "mus" + i, musicPath + MusicNames[MusicRand[i]] + ext);
            }
        }
    }
}
