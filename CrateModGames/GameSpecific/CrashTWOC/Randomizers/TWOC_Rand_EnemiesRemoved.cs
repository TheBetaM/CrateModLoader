﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public class TWOC_Rand_EnemiesRemoved : ModStruct<TWOC_File_AI>
    {
        List<string> BannedEnemies = new List<string>()
            {
                "crystal",
                "clock",
                "probe",
                "crate gem",
                "green gem",
                "bonus gem",
                "flying clock",
                "super slam",
                "flying crystal",
                "flying crategem",
                "flying probe",
                "blue gem",
                "double jump",
                "red gem",
                "flying bonusgem",
                "tiptoe",
                "super spin",
                "sprint",
                "space cortex",
                "space crunch",
                "water crunch",
                "yellow gem",
                "purple gem",
                "bazooka",
                "coco",
                "space lo-lo",
                "earth crunch",
                "space rok-ko",
                "space wa-wa",
                "space py-ro",
                "atlas crunch",
                "turtle",

            };

        private Random rand;

        public override void BeforeModPass()
        {
            rand = GetRandom();
        }

        public override void ModPass(TWOC_File_AI AIFile)
        {
            for (int a = 0; a < AIFile.AI.Count; a++)
            {
                if (!BannedEnemies.Contains(AIFile.AI[a].Name) && rand.Next(2) == 0)
                {
                    AIFile.AI.RemoveAt(a);
                    a--;
                }
            }
        }
    }
}
