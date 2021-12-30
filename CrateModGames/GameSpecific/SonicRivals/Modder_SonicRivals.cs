namespace CrateModLoader.GameSpecific.SonicRivals
{
    public sealed class Modder_SonicRivals : Modder
    {
        public override async void StartModProcess()
        {
            FindFiles(new Parser_TXT(this));
            await StartNewPass();

            ProcessBusy = false;
        }
    }
}
