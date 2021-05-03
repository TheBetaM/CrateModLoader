//RCF API by NeoKesha and BetaM
//Pure3D API by BetaM (based on https://github.com/handsomematt/Pure3D)
/* 
 * Mod Layers:
 * 1: All .RCF file contents (only replace files)
 * Mod Passes:
 * god -> LUA scripts (plain text)
 * mid -> roads (not yet implemented)
 * p3d -> Pure3D files
 * fig -> fight trees (plain text) (not yet implemented)
 */
namespace CrateModLoader.GameSpecific.CrashTTR
{
    public sealed class Modder_CTTR : Modder
    {
        public override async void StartModProcess()
        {
            FindArchives(new Pipeline_RCF(this));
            await StartPipelines(PipelinePass.Extract);

            FindFiles(new Parser_GOD(this), new Parser_P3D(this));
            await StartNewPass();

            await StartPipelines(PipelinePass.Build);

            ProcessBusy = false;
        }
    }
}
