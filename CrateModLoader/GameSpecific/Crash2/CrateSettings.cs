namespace CrateModLoader.GameSpecific.Crash2
{
    static class CrateSettings
    {
        public static void VerifyModCrates()
        {
            bool modsdirty = false;
            foreach (var crate in ModCrates.SupportedMods)
            {
                if (!crate.IsActivated) continue;
                if (!crate.HasSettings || !crate.Settings.ContainsKey("GameRegion") || crate.Settings["GameRegion"] != Program.ModProgram.targetRegion.ToString())
                {
                    crate.IsActivated = false;
                    modsdirty = true;
                }
            }
            if (modsdirty)
                ModCrates.UpdateModList();
        }
    }
}
