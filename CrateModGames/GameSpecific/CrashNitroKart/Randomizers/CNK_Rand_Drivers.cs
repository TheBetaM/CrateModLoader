using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_Rand_Drivers : ModStruct<CNK_GenericMod>
    {
        public override string Name => CNK_Text.Rand_Drivers;
        public override string Description => CNK_Text.Rand_DriversDesc;

        private Random randState;

        public override void BeforeModPass()
        {
            randState = new Random(ModLoaderGlobals.RandomizerSeed);
        }

        public override void ModPass(CNK_GenericMod mod)
        {
            string path_gob_extracted = mod.extrPath;
            ConsoleMode console = mod.console;

            //Replace model files
            string modelpath = path_gob_extracted;
            if (console == ConsoleMode.PS2)
            {
                modelpath += "/ps2/gfx/chars/";
            }
            else if (console == ConsoleMode.XBOX)
            {
                modelpath += "/xbox/gfx/chars/";
            }
            else
            {
                modelpath += "/gcn/gfx/chars/";
            }

            List<int> charList = new List<int>();
            List<int> charList_rand = new List<int>();

            //Change 22 to 24 to add geary and velo minions, untested
            for (int i = 0; i < 22; i++)
            {
                charList.Add(i);
                File.Move(modelpath + CNK_Common.DriverModelTypes[i] + ".igb", modelpath + "Driver" + i + ".igb");
            }

            for (int i = 0; i < 22; i++)
            {
                int target_id = randState.Next(0, charList.Count);
                charList_rand.Add(charList[target_id]);
                charList.RemoveAt(target_id);
            }

            for (int i = 0; i < 22; i++)
            {
                File.Move(modelpath + "Driver" + charList_rand[i] + ".igb", modelpath + CNK_Common.DriverModelTypes[i] + ".igb");
            }

            //Replace voices (todo: some drivers have voiceline IDs that others don't)
            // Caused crashing on the Gamecube version
            /*
            string[] csv_voices = File.ReadAllLines(path_gob_extracted + "common/audio/voices.csv");

            List<string> csv_Voices_LineList = new List<string>();
            for (int i = 0; i < csv_voices.Length; i++)
            {
                csv_Voices_LineList.Add(csv_voices[i]);
            }

            string cur_line = "";
            int targetChar = 0;
            for (int i = 2; i < csv_Voices_LineList.Count; i++)
            {
                cur_line = csv_Voices_LineList[i];
                if (cur_line.Length > 2)
                {
                    for (int a = 0; a < charList_rand.Count; a++)
                    {
                        if (cur_line.Substring(0, 3) == CNK_Data.DriverAudioTypes[a])
                        {
                            targetChar = charList_rand[a];
                            cur_line = cur_line.Substring(0, 7) + CNK_Data.DriverAudioTypes[targetChar].Substring(0,1) + "/" + CNK_Data.DriverAudioTypes[targetChar] + cur_line.Substring(12);
                        }
                    }
                }
                csv_Voices_LineList[i] = cur_line;
            }

            csv_voices = new string[csv_Voices_LineList.Count];
            for (int i = 0; i < csv_Voices_LineList.Count; i++)
            {
                csv_voices[i] = csv_Voices_LineList[i];
            }

            File.WriteAllLines(path_gob_extracted + "common/audio/voices.csv", csv_voices);
            */
        }

    }
}
