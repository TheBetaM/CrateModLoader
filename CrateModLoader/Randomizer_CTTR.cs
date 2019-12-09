using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTTR;
//CTTR API by NeoKesha

namespace CrateModLoader
{
    class Randomizer_CTTR
    {
        public void StartModProcess()
        {
            string feedbacktest = "";
            RCF newrcf = new RCF();
            newrcf.OpenRCF(""); // Path to target RCF
            object testItem = newrcf.ExtractItem(1, ""); // Extract if needed
            newrcf.Header.T2File[0].External = ""; // Path to external replacement
            newrcf.Recalculate(); //Maybe needs this?
            newrcf.Pack("", ref feedbacktest); // Pack the RCF
        }
    }

    class Randomizer_Titans
    {

    }

    class Randomizer_MoM
    {

    }
}
