using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrateModLoader
{
    class Modder_Bash
    {
        public string[] modOptions = { "No options available" };

        public enum Bash_Options
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
