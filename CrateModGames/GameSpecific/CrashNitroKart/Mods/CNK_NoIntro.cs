using System;
using System.Collections.Generic;
using System.IO;
using CrateModGames.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.CrashNitroKart
{
    public class CNK_NoIntro : ModStruct<CNK_GenericMod>
    {
        public override string Name => CNK_Text.Mod_RemoveIntroVideos;
        public override string Description => CNK_Text.Mod_RemoveIntroVideosDesc;

        public override void ModPass(CNK_GenericMod mod)
        {
            string path = mod.mainPath;
            ConsoleMode console = mod.console; 

            if (console == ConsoleMode.PS2)
            {
                if (File.Exists(path + "VIDEO/INTRO/ALCHEMY.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/ALCHEMY.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCO.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCO.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCODUT.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCODUT.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCOENG.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCOENG.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCOFRE.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCOFRE.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCOGER.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCOGER.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCOITA.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCOITA.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/FCOSPA.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/FCOSPA.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCO.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCO.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCODUT.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCODUT.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCOENG.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCOENG.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCOFRE.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCOFRE.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCOGER.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCOGER.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCOITA.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCOITA.SFD;1");
                if (File.Exists(path + "VIDEO/INTRO/SCOSPA.SFD;1"))
                    File.Delete(path + "VIDEO/INTRO/SCOSPA.SFD;1");
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
