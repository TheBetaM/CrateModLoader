using System;
using System.Collections.Generic;


namespace CrateModLoader.ModProperties.GUI
{
    public abstract class ModPropertyGUI_Base
    {

        public abstract void GenerateUI(object parent, ref int offset, bool showTitle = true);

        public abstract void UpdateUI();

        public abstract void ValueChange(object sender, EventArgs e);

        public abstract void FocusUI(object sender, EventArgs e);

    }
}
