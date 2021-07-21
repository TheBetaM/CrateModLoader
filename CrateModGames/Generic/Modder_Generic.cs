// Generic modder, only executes generic mods (it will not populate any properties or execute any mods, so just use as a placeholder)
namespace CrateModLoader.GameSpecific.Generic
{
    public sealed class Modder_Generic : Modder
    {
        public override async void StartModProcess()
        {
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
