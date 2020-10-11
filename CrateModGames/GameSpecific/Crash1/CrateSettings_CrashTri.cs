namespace CrateModLoader.GameSpecific
{
    static class CrateSettings_CrashTri
    {
        public static void VerifyModCrates(string ShortName, RegionCode region)
        {
            bool modsdirty = false;
            foreach (var crate in ModCrates.SupportedMods)
            {
                if (!crate.IsActivated) continue;
                if (!crate.HasSettings || !crate.Settings.ContainsKey("GameRegion") || crate.Settings["GameRegion"] != region.Region.ToString())
                {
                    crate.IsActivated = false;
                    modsdirty = true;
                }
            }
            if (modsdirty)
                ModCrates.PopulateModList(true, ShortName);
        }
    }
}
