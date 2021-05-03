using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_NoIntro : ModStruct<GenericModStruct>
    {
        public override void ModPass(GenericModStruct mod)
        {
            string path = mod.ExtractedPath + @"/ASSETS/";
            ConsoleMode console = mod.Console; 

            if (console == ConsoleMode.PS2)
            {
                if (File.Exists(path + "VIDEO/INTRO/ALCHEMY.SFD"))
                    File.Delete(path + "VIDEO/INTRO/ALCHEMY.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCO.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCO.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCODUT.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCODUT.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCOENG.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCOENG.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCOFRE.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCOFRE.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCOGER.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCOGER.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCOITA.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCOITA.SFD");
                if (File.Exists(path + "VIDEO/INTRO/FCOSPA.SFD"))
                    File.Delete(path + "VIDEO/INTRO/FCOSPA.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCO.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCO.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCODUT.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCODUT.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCOENG.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCOENG.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCOFRE.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCOFRE.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCOGER.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCOGER.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCOITA.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCOITA.SFD");
                if (File.Exists(path + "VIDEO/INTRO/SCOSPA.SFD"))
                    File.Delete(path + "VIDEO/INTRO/SCOSPA.SFD");
            }
            else if (console == ConsoleMode.GCN)
            {
                if (File.Exists(path + "video/intro/alchemy.sfd"))
                    File.Delete(path + "video/intro/alchemy.sfd");
                if (File.Exists(path + "video/intro/fco.sfd"))
                    File.Delete(path + "video/intro/fco.sfd");
                if (File.Exists(path + "video/intro/fcodut.sfd"))
                    File.Delete(path + "video/intro/fcodut.sfd");
                if (File.Exists(path + "video/intro/fcoeng.sfd"))
                    File.Delete(path + "video/intro/fcoeng.sfd");
                if (File.Exists(path + "video/intro/fcofre.sfd"))
                    File.Delete(path + "video/intro/fcofre.sfd");
                if (File.Exists(path + "video/intro/fcoger.sfd"))
                    File.Delete(path + "video/intro/fcoger.sfd");
                if (File.Exists(path + "video/intro/fcoita.sfd"))
                    File.Delete(path + "video/intro/fcoita.sfd");
                if (File.Exists(path + "video/intro/fcospa.sfd"))
                    File.Delete(path + "video/intro/fcospa.sfd");
                if (File.Exists(path + "video/intro/sco.sfd"))
                    File.Delete(path + "video/intro/sco.sfd");
                if (File.Exists(path + "video/intro/scodut.sfd"))
                    File.Delete(path + "video/intro/scodut.sfd");
                if (File.Exists(path + "video/intro/scoeng.sfd"))
                    File.Delete(path + "video/intro/scoeng.sfd");
                if (File.Exists(path + "video/intro/scofre.sfd"))
                    File.Delete(path + "video/intro/scofre.sfd");
                if (File.Exists(path + "video/intro/scoger.sfd"))
                    File.Delete(path + "video/intro/scoger.sfd");
                if (File.Exists(path + "video/intro/scoita.sfd"))
                    File.Delete(path + "video/intro/scoita.sfd");
                if (File.Exists(path + "video/intro/scospa.sfd"))
                    File.Delete(path + "video/intro/scospa.sfd");
            }
            else
            {
                if (File.Exists(path + "video/intro/alchemy.sfd"))
                    File.Delete(path + "video/intro/alchemy.sfd");
                if (File.Exists(path + "video/intro/fco.sfd"))
                    File.Delete(path + "video/intro/fco.sfd");
                if (File.Exists(path + "video/intro/fcodut.sfd"))
                    File.Delete(path + "video/intro/fcodut.sfd");
                if (File.Exists(path + "video/intro/fcoeng.sfd"))
                    File.Delete(path + "video/intro/fcoeng.sfd");
                if (File.Exists(path + "video/intro/fcofre.sfd"))
                    File.Delete(path + "video/intro/fcofre.sfd");
                if (File.Exists(path + "video/intro/fcoger.sfd"))
                    File.Delete(path + "video/intro/fcoger.sfd");
                if (File.Exists(path + "video/intro/fcoita.sfd"))
                    File.Delete(path + "video/intro/fcoita.sfd");
                if (File.Exists(path + "video/intro/fcospa.sfd"))
                    File.Delete(path + "video/intro/fcospa.sfd");
                if (File.Exists(path + "video/intro/sco.sfd"))
                    File.Delete(path + "video/intro/sco.sfd");
                if (File.Exists(path + "video/intro/scodut.sfd"))
                    File.Delete(path + "video/intro/scodut.sfd");
                if (File.Exists(path + "video/intro/scoeng.sfd"))
                    File.Delete(path + "video/intro/scoeng.sfd");
                if (File.Exists(path + "video/intro/scofre.sfd"))
                    File.Delete(path + "video/intro/scofre.sfd");
                if (File.Exists(path + "video/intro/scoger.sfd"))
                    File.Delete(path + "video/intro/scoger.sfd");
                if (File.Exists(path + "video/intro/scoita.sfd"))
                    File.Delete(path + "video/intro/scoita.sfd");
                if (File.Exists(path + "video/intro/scospa.sfd"))
                    File.Delete(path + "video/intro/scospa.sfd");
            }
        }

    }
}
