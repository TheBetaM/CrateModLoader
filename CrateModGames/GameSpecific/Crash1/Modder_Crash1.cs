using Crash;
using CrateModLoader.GameSpecific.Crash1.TrilogyCommon;
//Crash 1 API by chekwob and ManDude (https://github.com/cbhacks/CrashEdit)
//Version number and seed are displayed in the title screen.
/*
 * Mod Passes:
 * NSF_Pair -> NSF and NSD pair
 */
namespace CrateModLoader.GameSpecific.Crash1
{
    public sealed class Modder_Crash1 : Modder
    {
        public override bool ModCrateRegionCheck => true;

        public override async void StartModProcess()
        {
            FindFiles(new Parser_NSF(this, GameVersion.Crash1, GameRegion.Region));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
