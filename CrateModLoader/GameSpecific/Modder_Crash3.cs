﻿//Crash 3 API by chekwob and ManDude

namespace CrateModLoader
{
    sealed class Modder_Crash3
    {
        public string gameName = "Crash 3";
        public string apiCredit = "API by chekwob and ManDude";
        public System.Drawing.Image gameIcon = null;
        public bool ModMenuEnabled = false;
        public bool ModCratesSupported = true;
        public string[] modOptions = { "No options available" };

        public enum ModOptions
        {
            RandomizeSomething = 0,
        }

        public void OptionChanged(int option, bool value)
        {
            // TODO
        }

        public void StartModProcess()
        {
            // TODO
            ModProcess();
        }

        public void ModProcess()
        {
            // TODO
            EndModProcess();
        }

        public void EndModProcess()
        {
            // TODO
        }
    }
}
