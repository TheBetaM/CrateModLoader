/* API by BetaM.
 * Mod Layers:
 * 1: Data.zip contents
 * Mod Passes:
 * lua -> LUA scripts (plain text) (Lua 5.0.1) (ps2) 
 * xom -> data containers
 * tga -> textures
 */
using CrateModLoader.GameSpecific.WormsForts;
namespace CrateModLoader.GameSpecific.Worms4
{
    public sealed class Modder_Worms4 : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                FindArchives(new Pipeline_ZIP(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_LUA(this), new Parser_XOM(this, WormsGame.Worms4), new Parser_TGA(this, ModderIsPreloading));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
