using System;
using System.Windows.Forms;

namespace CrateModLoader
{
    public abstract class ModPropertyBase
    {

        public string Name;
        public string Description;
        /// <summary>
        /// Property name in code
        /// </summary>
        public string CodeName;

        /// <summary>
        /// UI category tab ID
        /// </summary>
        public int? Category = null;
        /// <summary>
        /// Value changed from default
        /// </summary>
        public bool HasChanged = false;
        /// <summary>
        /// Hidden from UI
        /// </summary>
        public bool Hidden = false;

        public abstract void GenerateUI(Control parent, ref int offset);

        public abstract void ValueChange(object sender, EventArgs e);

        public abstract void Serialize(ref string input);

        public abstract void DeSerialize(string input);

        public abstract void ResetToDefault();

        public abstract void FocusUI(object sender, object e);

    }
}
