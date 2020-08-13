using System;
using System.Windows.Forms;

namespace CrateModLoader
{
    public abstract class ModPropertyBase
    {

        public string Name;
        public string Description;
        public string CodeName;

        public int? Category = null;
        public bool HasChanged = false;

        public abstract void GenerateUI(TabPage page, ref int offset);

        public abstract void ValueChange(object sender, System.EventArgs e);

        public abstract void Serialize(ref string input);

        public abstract void DeSerialize(string input);

        public abstract void ResetToDefault();

        public abstract void FocusUI(object sender, object e);

    }
}
