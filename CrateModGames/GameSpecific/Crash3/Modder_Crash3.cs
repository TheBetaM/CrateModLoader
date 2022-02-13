using Crash;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
//Crash 3 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the pause menu in the Warp Room.
/*
 * Mod Passes:
 * NSF_Pair -> NSF and NSD pair
 */
namespace CrateModLoader.GameSpecific.Crash3
{
    public sealed class Modder_Crash3 : Modder
    {
        public override bool ModCrateRegionCheck => true;
        //public override bool StreamedModder => true;

        public override async void StartModProcess()
        {
            FindFiles(new Parser_NSF(this, GameVersion.Crash3, GameRegion.Region));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
