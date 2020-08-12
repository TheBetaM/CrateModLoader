using System;
using System.Collections.Generic;

namespace CrateModLoader.ModProperties
{
    public class ModCategory : Attribute
    {
        public int ID { get; set; }

        public ModCategory(int CategoryID)
        {
            ID = CategoryID;
        }

    }
}
