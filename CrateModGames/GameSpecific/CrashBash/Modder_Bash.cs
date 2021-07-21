using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBash
{
    public sealed class Modder_Bash : Modder
    {
        public override async void StartModProcess()
        {
            FindFiles();
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
