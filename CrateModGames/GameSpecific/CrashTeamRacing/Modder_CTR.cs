//CTR API by DCxDemo (https://github.com/DCxDemo/CTR-tools) 
/* Mod Layers/Pipelines:
 * 1: BIGFILE.BIG contents
 * 2: MPK model archives (not yet implemented)
 * Mod Passes:
 * LNG -> language files
 * CTR -> model files
 * LEV -> level files
 */
namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public sealed class Modder_CTR : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                FindArchives(new Pipeline_BIG(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_LNG(this), new Parser_LEV(this), new Parser_CTR(this));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
