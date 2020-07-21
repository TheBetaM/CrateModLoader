using System.Windows.Forms;
using System.Collections.Generic;

namespace CrateModLoader
{
    /*
     * Adding a game:
     * 1. Make a new modder class that inherits this abstract class.
     * 2. Fill its Game member with the appropriate info in the class constructor.
     * 3. Override StartModProcess (at least, there are more modding functions that can be overriden but are optional).
     * 4. Done.
     * 
     */

    /// <summary>
    /// Abstract Modder class, inherited by all game modders
    /// </summary>
    public abstract class Modder
    {
        public Dictionary<int,ModOption> Options { get; } = new Dictionary<int,ModOption>();

        // Use this instead of Options.Add!
        /// <summary>
        /// Adds an option to the dictionary if the region and console is allowed for it.
        /// </summary>
        /// <param name="id">Option ID in dictionary</param>
        /// <param name="option">Option constructor</param>
        public virtual void AddOption(int id, ModOption option)
        {
            if (option.AllowedConsoles.Count > 0 && !option.AllowedConsoles.Contains(Program.ModProgram.isoType))
            {
                return;
            }
            if (option.AllowedRegions.Count > 0 && !option.AllowedRegions.Contains(Program.ModProgram.targetRegion))
            {
                return;
            }
            Options.Add(id, option);
        }

        // Use this! Mostly for error handling and when options may be missing.
        /// <summary>
        /// Gets the enabled state of the mod option (false if option doesn't exist)
        /// </summary>
        /// <param name="id">Option ID in dictionary</param>
        public virtual bool GetOption(int id)
        {
            if (Options.ContainsKey(id) && Options[id].Enabled)
            {
                return true;
            }
            return false;
        }

        public bool ModCratesManualInstall = false; // A game might require some type of verification (i.e. file integrity, region matching) before installing layer0 mod crates.

        public abstract void StartModProcess();
        protected virtual void ModProcess() { }
        protected virtual void EndModProcess() { }

        public virtual void OpenModMenu()
        {
            MessageBox.Show("This game doesn't have a mod menu!", "Error", MessageBoxButtons.OK);
        }

        public Game Game { get; protected set; }
    }
}
