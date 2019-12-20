using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTRFramework;
//CTR API by DCxDemo

namespace CrateModLoader
{
    class Modder_CTR
    {
        public string[] modOptions = { "No options available" };

        public enum CTR_Options
        {
            RandomizeSomething = 0,
        }

        public void OptionChanged(int option, bool value)
        {
            // TODO
        }

        public void UpdateModOptions()
        {
            Program.ModProgram.PrepareOptionsList(modOptions);
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
