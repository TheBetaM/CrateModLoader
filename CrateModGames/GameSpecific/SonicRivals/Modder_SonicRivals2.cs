using CrateModLoader.GameSpecific.SonicRivals;
namespace CrateModLoader.GameSpecific.Sonic2Rivals
{
    public sealed class Modder_SonicRivals2 : Modder
    {
        public override async void StartModProcess()
        {
            FindFiles(new Parser_TXT(this));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
