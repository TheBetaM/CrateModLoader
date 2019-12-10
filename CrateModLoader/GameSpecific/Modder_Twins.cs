using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity;
//Twinsanity API by NeoKesha

namespace CrateModLoader
{
    class Modder_Twins
    {
        public string gameName = "Twinsanity";
        public string apiCredit = "API by NeoKesha";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_twins;
        public string[] modOptions = { "No options available" };

        public void StartModProcess()
        {
            RM2 mainArchive = new RM2();
            mainArchive.LoadRM2(""); // load rm2
            // stuff
        }
    }
}
