using System.Windows.Forms;

namespace CrateModLoader.ModProperties
{
    public abstract class ModPropertyBase
    {

        public string Name;
        public string Description;

        public int Category;

        public abstract void GenerateUI(TabPage page, ref int offset);

        public abstract void ValueChange(object sender, System.EventArgs e);

        public abstract void ResetToDefault();

    }
}
