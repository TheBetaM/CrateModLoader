/* Mod Passes:
 * TWOC_GenericMod -> extraction path + console type
 * TWOC_File_AI -> AI files
 * TWOC_File_CRT -> Crate files
 * TWOC_File_WMP -> Wumpa files
 */
namespace CrateModLoader.GameSpecific.CrashTWOC
{
    public sealed class Modder_TWOC : Modder
    {
        public override void StartModProcess()
        {
            ProcessBusy = true;
            ModProcess();
        }

        public async void ModProcess()
        {
            //todo: Generic mods here

            FindFiles(new Parser_AI(this), new Parser_CRT(this), new Parser_WMP(this));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
