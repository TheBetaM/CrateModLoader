//Crash 3 API by chekwob and ManDude

namespace CrateModLoader
{
    sealed class Modder_Crash3
    {
        public string[] modOptions = { "No options available" };

        public enum ModOptions
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
