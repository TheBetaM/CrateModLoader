/* API by BetaM.
 * Mod Passes:
 * TWOC_File_AI -> AI files
 * TWOC_File_CRT -> Crate files
 * TWOC_File_WMP -> Wumpa files
 */
namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public sealed class Modder_TWOC : Modder
    {
        public override async void StartModProcess()
        {
            FindFiles(new Parser_AI(this), new Parser_CRT(this), new Parser_WMP(this));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
