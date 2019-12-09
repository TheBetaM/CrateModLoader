using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//CNK API by BetaM, ManDude and eezstreet.

namespace CrateModLoader
{
    class Randomizer_CNK
    {
        public void StartModProcess()
        {
            Process proc = Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_in.exe", "assets.gob extr");
            if (proc.HasExited)
            {
                
            }
        }

        public void EndModProcess()
        {
            Process proc = Process.Start(AppDomain.CurrentDomain.BaseDirectory + "/Tools/gobextract_out.exe", "assets.gob extr -create");
            if (proc.HasExited)
            {

            }
        }
    }
}
