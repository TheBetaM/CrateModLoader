namespace CrateModLoader.GameSpecific
{
    static class CrateSettings_CrashTri
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
