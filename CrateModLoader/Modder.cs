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
